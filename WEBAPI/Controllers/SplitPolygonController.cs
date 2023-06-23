using Newtonsoft.Json;
using Sigma.App.Interfaces;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;
using WEBAPI.App_Start;

namespace WEBAPI.Controllers
{
    [AllowedOriginFilter]
    public class SplitPolygonController : ApiController
    {
        private readonly IGeoConfigurationAppService _geoConfigurationAppService; 

        public SplitPolygonController(IGeoConfigurationAppService geoConfigurationAppService)
        {
            _geoConfigurationAppService = geoConfigurationAppService; 
        }
        /// <summary> Este método será utilizado para remover o poligono menor, que estiver acima da quantidade de divisão </summary>
        /// <returns></returns>
        private PyAPI RemoveLowPoly(PyAPI obj)
        {
            PyAPI retorno = new PyAPI(); 
            try
            {
                List<string> poly_lst = new List<string>();
                foreach (var join_poli in obj.poly)
                {
                    string join = join_poli.Replace("geometry::STPolyFromText('POLYGON((", "((").Replace("))", "))").Replace("', 0)", "");
                    if (!join.Contains("POLYGON"))
                        poly_lst.Add("POLYGON" + join);
                    else
                        poly_lst.Add(join);
                }
                retorno.poly = _geoConfigurationAppService.GenerateGeoJsonPoints(poly_lst);

                List<string> point_lst = new List<string>();
                for (int x = 0; x < obj.point.Count; x++)
                {
                    for (int y = 0; y < retorno.poly.Count; y++)
                    {
                        bool exist = _geoConfigurationAppService.GetWithinPoint(obj.point[x], retorno.poly[y]);
                        if (exist)
                        {
                            point_lst.Add(obj.point[x]);
                            break;
                        }
                    }
                }

                retorno.point = point_lst;

                return retorno; 
            }
            catch (Exception ex)
            {
                return retorno;
            }
        }

        /// <summary> Este método será utilizado para dividir o poligono através de uma string de coordenadas e a quantidade de vezes que será dividido este poligono. </summary>
        /// <param name="coord"></param>
        /// <param name="qtd_split"></param>
        /// <returns></returns>
        private PyAPI ConvertCoord(PyAPI obj, int orbita)
        {
            PyAPI retorno = new PyAPI();
            List<string> poly = new List<string>();
            for (int i = 0; i < obj.poly.Count; i++)
            {
                string convert_geom = _geoConfigurationAppService.GetGeoJson(obj.poly[i]);
                PyReturn obj_poly = JsonConvert.DeserializeObject<PyReturn>(convert_geom);

                string poly_str = "geometry::STPolyFromText('POLYGON(("; 
                foreach (var item in obj_poly.coordinates)
                {

                    double ta = double.Parse(item[0].ToString());
                    var coords = Auxiliar.ConversorCoordenadas.ConvertUTMDecimal(double.Parse(item[1].ToString()), double.Parse(item[0].ToString()), orbita, 's'); 
                    poly_str += coords[0].ToString().Replace(",",".") + "," + coords[1].ToString().Replace(",", ".") + ",";
                }
                poly_str = poly_str.Substring(0,(poly_str.Length - 1)) + "))',4326)" ;
                poly.Add(poly_str);
            }

            retorno.poly = poly;

            List<string> point = new List<string>();
            if (obj.point != null)
            {
                for (int i = 0; i < obj.point.Count; i++)
                {
                    string convert_geom = _geoConfigurationAppService.GetGeoJson(obj.point[i]);
                    PyReturnPoint obj_point = JsonConvert.DeserializeObject<PyReturnPoint>(convert_geom);
                    var coords = Auxiliar.ConversorCoordenadas.ConvertUTMDecimal(double.Parse(obj_point.coordinates[1].ToString()), double.Parse(obj_point.coordinates[0].ToString()), orbita, 's');
                    string poly_str = "geometry::STPointFromText('POINT(" + coords[0].ToString().Replace(",", ".") + " " + coords[1].ToString().Replace(",", ".") + ")',4326)";
                    point.Add(poly_str);
                }

                retorno.point = point;
            }
            return retorno; 
        }
        private async Task<PyAPI> SplitPolygon(string coord, int qtd_split)
        {
            PyAPI retorno = new PyAPI();
            var objeto = new { coordenadas = coord, divisao = qtd_split.ToString() };
            var json = JsonConvert.SerializeObject(objeto);
            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClientHandler = new HttpClientHandler())
            {
                try
                {
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.Timeout = TimeSpan.FromSeconds(720);
                        var response = await client.PostAsync("https://bng.pyapi.work/coordenadas", conteudo);
                        await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            var new_obj = await response.Content.ReadAsStringAsync();
                            retorno = JsonConvert.DeserializeObject<PyAPI>(new_obj);
                            if (retorno.poly.Count > qtd_split)
                                retorno = RemoveLowPoly(retorno);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return retorno;
        }
        private async void Teste()
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(720);

                var response = await client.GetAsync("http://localhost:5000/converteimg_tiff");
                // Executa o próximo comando somente após o término da requisição:
                response.Content.ReadAsStringAsync().Wait();
                if (response.IsSuccessStatusCode)
                {
                    var new_obj = await response.Content.ReadAsStringAsync();
                }
            }
        }
        [HttpPost]
        public async Task<IEnumerable<RetornoPonto>> Post(PostGeneratePoint item)
        {
            List<RetornoPonto> oPonto = new List<RetornoPonto>();
            int sub = 1;
            if (item.sub > 0)
                sub = item.sub;

            int ponto_str = 0; /// este numero será indicativo do ponto. 
            int ponto_principal = 0; /// Este ponto referencia o ponto principal.   
            List<string> retorno = new List<string>();

            foreach (var cd in item.lst)
            {
                ponto_principal++; 
                List<List<double>> list = new List<List<double>>();
                list = JsonConvert.DeserializeObject<List<List<double>>>(cd);
                List<double[]> new_list = new List<double[]>();
                foreach (var novo_item in list)
                {
                    var n_coord = Auxiliar.ConversorCoordenadas.ConvertDecimalUTM(novo_item[1], novo_item[0]);
                    double[] v_coord = new double[] { n_coord[0], n_coord[1] };
                    new_list.Add(v_coord);
                }
                
                string t = JsonConvert.SerializeObject(new_list);
                string new_coord = "POLYGON" + cd.Replace("[[", "((").Replace("],[", " , ").Replace("]]", "))").Replace(",-", " -");
                double tamanho_area = _geoConfigurationAppService.GetSize(new_coord);

                int tmh = 0;
                if (tamanho_area < 5)
                    tmh = 1;
                else if (tamanho_area > 5)
                    tmh = (int)(tamanho_area / 5);

                if (tmh > 1)
                {
                    if (sub > 1)
                    {
                        int new_sub = (int)(tmh * sub);

                        ponto_str = 0; 

                        /// Aqui será feita uma transformação dos dados geográficos em divisão. 
                        PyAPI result = await SplitPolygon(t, new_sub);
                        result = ConvertCoord(result, item.orb);

                        result.point = _geoConfigurationAppService.OrdePoint(result.point).ToList(); 

                        //var order_point = _geoConfigurationAppService.OrdePoint(result.point).ToList();

                        for (int i = 0; i < result.point.Count; i++)
                        {
                            RetornoPonto oPt = new RetornoPonto();
                            if (i > 0)
                                ponto_str++;

                            if (i == 0)
                            {
                                oPt.jsonField = "{\"NUM_PONTO\":\"" + ponto_principal + "\", \"TALHAO\": \"TESTE\",\"TIPO\": \"S\" }";
                                oPt.geoJson = _geoConfigurationAppService.GetGeoPointJSON(result.point[i]);
                            }
                            else if (ponto_str == sub)
                            {
                                ponto_str = 0;
                                ponto_principal++;

                                oPt.jsonField = "{\"NUM_PONTO\":\"" + ponto_principal + "\", \"TALHAO\": \"TESTE\",\"TIPO\": \"S\" }";
                                oPt.geoJson = _geoConfigurationAppService.GetGeoPointJSON(result.point[i]);
                            }
                            else if (i > 0 && ponto_str > 0)
                            {
                                oPt.jsonField = "{\"NUM_PONTO\":\"" + ponto_principal + "." + ponto_str + "\", \"TALHAO\": \"TESTE\",\"TIPO\": \"S\" }";
                                oPt.geoJson = _geoConfigurationAppService.GetGeoPointJSON(result.point[i]);
                            }
                            oPonto.Add(oPt);

                        }
                    }
                    else
                    {
                        ponto_str = 0;

                        RetornoPonto oPt = new RetornoPonto();
                        oPt.jsonField = "{\"NUM_PONTO\":\"" + ponto_principal + "\", \"TALHAO\": \"TESTE\",\"TIPO\": \"S\" }";
                        oPt.geoJson = _geoConfigurationAppService.GetGeoCenter("geometry::STPolyFromText('" + new_coord + "',4326)");
                        oPonto.Add(oPt);
                        ponto_principal++;
                    }
                }
                else
                {
                    if (sub > 1)
                    {
                        ponto_str = 0;

                        PyAPI sub_amostra = await SplitPolygon(t, sub);
                        sub_amostra = ConvertCoord(sub_amostra, item.orb);
                        for (int i = 0; i < sub_amostra.point.Count; i++)
                        {
                            RetornoPonto oPt = new RetornoPonto();
                            if (i > 0)
                                ponto_str++;

                            if (i == 0)
                            {
                                oPt.jsonField = "{\"NUM_PONTO\":\"" + ponto_principal + "\", \"TALHAO\": \"TESTE\",\"TIPO\": \"S\" }";
                                oPt.geoJson = _geoConfigurationAppService.GetGeoPointJSON(sub_amostra.point[i]);
                            }
                            else if (ponto_str == sub)
                            {
                                ponto_str = 0;
                                ponto_principal++;

                                oPt.jsonField = "{\"NUM_PONTO\":\"" + ponto_principal + "\", \"TALHAO\": \"TESTE\",\"TIPO\": \"S\" }";
                                oPt.geoJson = _geoConfigurationAppService.GetGeoPointJSON(sub_amostra.point[i]);
                            }
                            else if (i > 0 && ponto_str > 0)
                            {
                                oPt.jsonField = "{\"NUM_PONTO\":\"" + ponto_principal + "." + ponto_str + "\", \"TALHAO\": \"TESTE\",\"TIPO\": \"S\" }";
                                oPt.geoJson = _geoConfigurationAppService.GetGeoPointJSON(sub_amostra.point[i]);
                            }
                            oPonto.Add(oPt);
                        }
                        ponto_str++;
                    }
                    else
                    {
                        ponto_str = 0;

                        RetornoPonto oPt = new RetornoPonto();
                        oPt.jsonField = "{\"NUM_PONTO\":\"" + ponto_principal + "\", \"TALHAO\": \"TESTE\",\"TIPO\": \"S\" }";
                        oPt.geoJson = _geoConfigurationAppService.GetGeoCenter("geometry::STPolyFromText('" + new_coord + "',4326)");
                        oPonto.Add(oPt);
                        ponto_principal++;
                    }
                }
            }
            oPonto = oPonto.OrderBy(o => o.jsonField).ToList();
            return oPonto;
        }
        [HttpPost]
        [ActionName("SplitPoly")]
        [Route("api/SplitPolygon/SplitPoly")]
        public async Task<IEnumerable<RetornoPonto>> SplitPoly(PostGeneratePoint item)
        {
            List<RetornoPonto> oPonto = new List<RetornoPonto>();

            string collection_utm_coord_lst = ""; 

            foreach (var cd in item.lst)
            {
                List<List<double>> zones = new List<List<double>>();
                zones = JsonConvert.DeserializeObject<List<List<double>>>(cd);

                List<double[]> new_list = new List<double[]>();
                foreach (var novo_item in zones)
                {
                    var n_coord = Auxiliar.ConversorCoordenadas.ConvertDecimalUTM(novo_item[1], novo_item[0]);
                    double[] v_coord = new double[] { n_coord[0], n_coord[1] };
                    new_list.Add(v_coord);
                }

                string utm_coord = JsonConvert.SerializeObject(new_list);
                collection_utm_coord_lst += utm_coord + "_";
            }
            collection_utm_coord_lst = collection_utm_coord_lst.Substring(0, collection_utm_coord_lst.Length - 1);


            List<List<double>> poli = new List<List<double>>();
            poli = JsonConvert.DeserializeObject<List<List<double>>>(item.poligon);
            List<double[]> lst_coord_poli = new List<double[]>();
            foreach (var novo_item in poli)
            {
                var n_coord = Auxiliar.ConversorCoordenadas.ConvertDecimalUTM(novo_item[1], novo_item[0]);
                double[] v_coord = new double[] { n_coord[0], n_coord[1] };
                lst_coord_poli.Add(v_coord);
            }

            string utm_coord_poli = JsonConvert.SerializeObject(lst_coord_poli);

            PyAPI retorno = new PyAPI();
            var objeto = new { poly_principal = utm_coord_poli, Zones = collection_utm_coord_lst  };
            var json = JsonConvert.SerializeObject(objeto);

            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(720);

                //var response = await client.PostAsync("http://split.apipy.local:5000/coordenadas", conteudo);
                var response = await client.PostAsync("http://localhost:5000/splitpolytozones", conteudo);
                // Executa o próximo comando somente após o término da requisição:
                response.Content.ReadAsStringAsync().Wait();
                if (response.IsSuccessStatusCode)
                {
                    var new_obj = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PyAPI>(new_obj);

                    result = ConvertCoord(result, 23);

                    for (int i = 0; i < result.point.Count; i++)
                    {
                        RetornoPonto oPt = new RetornoPonto();
                        oPt.jsonField = "{\"TALHAO\": \"TESTE\",\"TIPO\": \"S\" }";
                        oPt.geoJson = _geoConfigurationAppService.GetGeoJson(result.poly[i]);
                        oPonto.Add(oPt);
                    }
                }
            }
            return oPonto; 
        }
    }
}