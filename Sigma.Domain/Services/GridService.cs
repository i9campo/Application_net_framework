using ConectionPath.ClassConection;
using Newtonsoft.Json;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Domain.Services
{
    public class GridService : Service<Grid>, IGridService
    {
        private readonly IGridRepository _GridRepository;
        public GridService(IGridRepository _Repository)
            :base (_Repository)
        {
            _GridRepository = _Repository; 
        }

        #region C.R.U.D
        public bool AddLstGrid(IEnumerable<GridViewer> lst_grid)
        {
            bool result = false;
            foreach (var grd in lst_grid)
            {
                result = _GridRepository.AddLstGrid(grd);
                if (result == false)
                    break;
            }

            return result;
        }
        public bool UpdateLstGrid(IEnumerable<GridViewer> lst_grid)
        {
            bool result = false;
            foreach (var grd in lst_grid)
            {
                result = _GridRepository.UpdateGrid(grd);
                if (result == false)
                    break; 
            }

            return result; 
        }
        public bool UpdateGrid(GridViewer grd)
        {
            return _GridRepository.UpdateGrid(grd); ;
        }
        public bool DeleteGrid(String IDAreaServico)
        {
            return _GridRepository.DeleteGrid(IDAreaServico);
        }
        #endregion

        #region SEARCH
        public GridViewer FindGrid(Guid objID)
        {
            return _GridRepository.FindGrid(objID); 
        }

        public Grid GetByCodigo(int Codigo)
        {
            return _GridRepository.GetByCodigo(Codigo);
        }
        public IEnumerable<GridViewer> GetByAreaServico(Guid IDAreaServico)
        {
            return _GridRepository.GetByAreaServico(IDAreaServico); 
        }
        #endregion

        public IEnumerable<GridView> CorrecaoAcidez(Guid IDAreaServico)
        {
            return _GridRepository.CorrecaoAcidez(IDAreaServico);
        }


        public ImportItensLabView ExistAnaliseByCodigo(int Codigo, Guid IDAreaServico)
        {
            return _GridRepository.ExistAnaliseByCodigo(Codigo, IDAreaServico);
        }

        public IEnumerable<GridView> GetAllGeoJson()
        {
            return _GridRepository.GetAllGeoJson();
        }

        public IEnumerable<GridView> GetByAreaServicoFull(Guid IDAreaServico)
        {
            return _GridRepository.GetByAreaServicoFull(IDAreaServico);
        }

        public AnaliseSoloView GetMediaAnaliseSoloGrid(Guid objID, string profundidade)
        {
            return _GridRepository.GetMediaAnaliseSoloGrid(objID, profundidade);
        }

        public void RemoveGRID(Guid objID)
        {
            _GridRepository.RemoveGRID(objID);
        }

        public IEnumerable<GridView> GetGrid(Guid objID, Guid IDSafra, Guid? IDAreaServico, int Type)
        {
            return _GridRepository.GetGrid(objID, IDSafra, IDAreaServico, Type);
        }

        public GridView GetByGeoAreaServico(Guid IDAreaServico, string geo, string servico)
        {
            return _GridRepository.GetByGeoAreaServico(IDAreaServico, geo, servico);
        }

        public IEnumerable<Grid> GetGridByAreaServico(Guid IDAreaServico)
        {
            return _GridRepository.GetGridByAreaServico(IDAreaServico); 
        }

        public IEnumerable<Grid> GetAllGridByAreaServico(Guid IDAreaServico)
        {
            return _GridRepository.GetAllGridByAreaServico(IDAreaServico);
        }


        public bool UpdateFieldList(Guid objID, string newValue)
        {
            return _GridRepository.UpdateFieldList(objID, newValue); 
        }

        public async Task<IEnumerable<GeoJsonSplitPoly>> SplitPoly(SplitPolyViewer obj)
        {
            List<GeoJsonSplitPoly> oList = new List<GeoJsonSplitPoly>();
            try
            {
                return await SplitTiff(obj.poly1, obj.poly2); 
            }
            catch (Exception)
            {

            }
            return oList;
        }

        public async Task<IEnumerable<GeoJsonSplitPoly>> SplitTiff(string polygon1, string polygon2 )
        {
            List<GeoJsonSplitPoly> oList = new List<GeoJsonSplitPoly>();

            var objeto = new { poly1 = polygon1, poly2 = polygon2};

            var json = JsonConvert.SerializeObject(objeto);
            try
            {
                var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(720);

                    var response = await client.PostAsync("http://split.apipy.local:5000/split_polygon", conteudo);
                    response.Content.ReadAsStringAsync().Wait();
                    if (response.IsSuccessStatusCode)
                    {
                        var new_obj = await response.Content.ReadAsStringAsync();
                        var lst = JsonConvert.DeserializeObject<IEnumerable<string>>(new_obj);

                        foreach (var item in lst)
                        {
                            GeoJsonSplitPoly o = new GeoJsonSplitPoly();
                            o = _GridRepository.ObjSplitPoly(item);
                            oList.Add(o);
                        }

                        return oList;
                    }
                }
            }
            catch (Exception ex)
            {
                return oList;
            }

            return oList;
        }
    }
}
