using FluentValidation.Results;
using IronXL;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class ItensAnalisesLaboratorioController : ApiController
    {
        private IItensAnaliseLaboratorioAppService _itensAnaliseLaboratorio;
        private IAreaServicoAppService _areaServicoAppService;
        private IGridAppService _gridAppService;
        private IAnaliseSoloAppService _analiseSoloAppService;
        private ITipoSoloAppService _tipoSoloAppService;
        public ItensAnalisesLaboratorioController(IItensAnaliseLaboratorioAppService itensAnaliseLaboratorio, ITipoSoloAppService tipoSoloAppService, IAnaliseSoloAppService analiseSoloAppService, IAreaServicoAppService areaServicoAppService, IGridAppService gridAppService)
        {
            _itensAnaliseLaboratorio = itensAnaliseLaboratorio;
            _areaServicoAppService = areaServicoAppService;
            _gridAppService = gridAppService;
            _analiseSoloAppService = analiseSoloAppService;
            _tipoSoloAppService = tipoSoloAppService;
        }

        // GET api/<controller>
        public IEnumerable<SequenciaImportacao> Get()
        {
            return _itensAnaliseLaboratorio.GetAll();
        }

        // GET api/<controller>/5
        public SequenciaImportacao Get(Guid objID)
        {
            return _itensAnaliseLaboratorio.Find(objID);
        }

        [HttpPost]
        [ActionName("putanaliselab")]
        [Route("api/itensanliselaboratorio/putanaliselab")]
        public int PutAnaliseLab([FromBody] IEnumerable<ImportItensLabView> obj)
        {
            int qtd = 0;
            List<ImportItensLabView> result = obj.ToList();
            for (int i = 0; i < result.Count(); i++)
            {
                try
                {
                    var Ids = _analiseSoloAppService.FindObject(result[i].Area.ToString(), result[i].Grid.ToString());
                    var objant = _analiseSoloAppService.FindAnaliseAreaGrid(Guid.Parse(Ids.IDAreaServico), Guid.Parse(Ids.IDGrid));
                    List<AnaliseSolo> analiseSolos = objant.ToList();
                    for (int a = 0; a < objant.Count(); a++)
                    {
                        string[] ponto = result[i].Nome.Split();
                        if (Convert.ToInt32(ponto[0]) == analiseSolos[a].ponto)
                        {
                            analiseSolos[a].ponto = Convert.ToInt32(result[i].NPonto);
                            var TipoSolo = _tipoSoloAppService.FindTipoSolo(result[i].TpSolo);
                            if (TipoSolo != null)
                                analiseSolos[a].IDTipoSolo   = Guid.Parse(TipoSolo.objID.ToString());

                            analiseSolos[a].profundidade = "00 - 20";
                            analiseSolos[a].data            = result[i].dataAtual;
                            analiseSolos[a].compactacao     = result[i].Compac;
                            analiseSolos[a].cacl2           = result[i].PHCaCl2 == null ? 0 : Double.Parse(result[i].PHCaCl2.ToString());
                            analiseSolos[a].mo              = result[i].MO      == null ? 0 : Double.Parse(result[i].MO.ToString());
                            analiseSolos[a].momicro         = result[i].Momicro == null ? 0 : Double.Parse(result[i].Momicro.ToString());
                            analiseSolos[a].pmehl           = result[i].PMeHl   == null ? 0 : Double.Parse(result[i].PMeHl.ToString());
                            analiseSolos[a].pres            = result[i].PRes    == null ? 0 : Double.Parse(result[i].PRes.ToString());
                            analiseSolos[a].k               = result[i].K       == null ? 0 : Double.Parse(result[i].K.ToString());
                            analiseSolos[a].s               = result[i].S       == null ? 0 : Double.Parse(result[i].S.ToString());
                            analiseSolos[a].ca              = result[i].Ca      == null ? 0 : Double.Parse(result[i].Ca.ToString());
                            analiseSolos[a].mg              = result[i].Mg      == null ? 0 : Double.Parse(result[i].Mg.ToString());
                            analiseSolos[a].al              = result[i].Al      == null ? 0 : Double.Parse(result[i].Al.ToString());
                            analiseSolos[a].ctc             = result[i].CTC     == null ? 0 : Double.Parse(result[i].CTC.ToString());
                            analiseSolos[a].argila          = result[i].Argila  == null ? 0 : Double.Parse(result[i].Argila.ToString());
                            analiseSolos[a].zn              = result[i].Zn      == null ? 0 : Double.Parse(result[i].Zn.ToString());
                            analiseSolos[a].fe              = result[i].Fe      == null ? 0 : Double.Parse(result[i].Fe.ToString());
                            analiseSolos[a].mn              = result[i].Mn      == null ? 0 : Double.Parse(result[i].Mn.ToString());
                            analiseSolos[a].co              = result[i].Co      == null ? 0 : Double.Parse(result[i].Co.ToString());

                            _analiseSoloAppService.Update(analiseSolos[a]);
                            qtd = qtd + 1;
                        }

                    }
                }
                catch (Exception e)
                {
                    return qtd;
                }

            }
            return qtd;
        }

        public bool PostSequenciaImportacao([FromBody]SequenciaLaboratorio obj )
        {





            return false; 
        }





        [HttpPut]
        [ActionName("updatelist")]
        [Route("api/itensanliselaboratorio/UpdateList")]
        public IEnumerable<ImportItensLabView> UpdateList([FromBody] IEnumerable<ImportItensLabView> lista)
        {
            List<ImportItensLabView> ImportCollection = new List<ImportItensLabView>();
            List <ImportItensLabView> NewList = lista.ToList();
            for (int i = 0; i < lista.Count(); i++)
            {
                ImportItensLabView obj = new ImportItensLabView();
                obj = NewList[i];
                obj.objID = Guid.NewGuid();

                int CodigoArea = 0;
                Int32.TryParse(NewList[i].Area, out CodigoArea);

                ImportItensLabView itemAreaServicoExist = _areaServicoAppService.ExistAnaliseByCodigo(CodigoArea);
                obj.AreaServicoAnaliseExiste = (itemAreaServicoExist == null) ? false : itemAreaServicoExist.AreaServicoAnaliseExiste;



                int CodigoGrid = 0;
                int.TryParse(obj.Grid, out CodigoGrid);

                ImportItensLabView itemGridExist = (obj.AreaServicoAnaliseExiste == false) ? null : _gridAppService.ExistAnaliseByCodigo(CodigoGrid, Guid.Parse(itemAreaServicoExist.IDAreaServico.ToString()));
                obj.GridAnaliseExiste = (itemGridExist == null) ? false : itemGridExist.GridAnaliseExiste;
                ImportCollection.Add(obj);

            }
            return ImportCollection;
        }


        [HttpPut]
        [ActionName("analisesolofile")]
        [Route("api/itensanliselaboratorio/analisesolofile")]
        public IEnumerable<ImportItensLabView> GetAnaliseSoloFilte([FromBody] ImportItensLabView objetoSelected)
        {
            /*  Este método será utilizado para carregar o arquivo '.csv' logo após que o usuário fazer a seleção. */
            //ImportItensLabView objetoItens = JsonConvert.DeserializeObject<ImportItensLabView>(objetoSelected);

            List<ImportItensLabView> ImportCollection = new List<ImportItensLabView>();

            /* Essa condição será utilizada para verificar se os dados foram preenchido ou se eles são igual a 'NULL' ,
            Caso a string seja igual a null, retorne uma lista de dados vazios.*/
            if (objetoSelected == null)
                return ImportCollection;

            string path = HttpContext.Current.Server.MapPath("~/FileItensAnalises/" + objetoSelected.fileName + ".csv");
            WorkSheet fileOpen;
            try
            {
                // Aqui será feito a abertura do arquivo. 
                WorkBook workbook = WorkBook.Load(path, ",");
                string header = workbook.DefaultWorkSheet.ToString();
                if (header.Contains(";"))
                {
                    workbook = WorkBook.Load(path, ";");
                }
                fileOpen = workbook.WorkSheets.First();
            }
            catch (Exception)
            {
                return ImportCollection;
            }



            for (int i = 2; i < (fileOpen.RowCount + 1); i++)
            {
                // fileOpen recebe a letra da coluna e a linha. EX: ( A1; A2; X1 )
                // Então o registro que recebe do objetoItens é a letra selecionada e o i é referente as linhas. 
                ImportItensLabView obj = new ImportItensLabView();
                obj.objID = Guid.NewGuid();
                obj.Area = String.IsNullOrEmpty(objetoSelected.Area.TrimStart()) ? " " : fileOpen[(objetoSelected.Area + i)].ToString();
                obj.Grid = String.IsNullOrEmpty(objetoSelected.Grid.TrimStart()) ? " " : fileOpen[(objetoSelected.Grid.TrimStart() + i)].ToString();
                obj.Nome = String.IsNullOrEmpty(objetoSelected.Nome.TrimStart()) ? " " : fileOpen[(objetoSelected.Nome.TrimStart() + i)].ToString();
                obj.NPonto = String.IsNullOrEmpty(objetoSelected.NPonto.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.NPonto.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.NPonto.TrimStart() + i)].ToString());

                obj.TpSolo = String.IsNullOrEmpty(objetoSelected.TpSolo.TrimStart()) ? " " : fileOpen[(objetoSelected.TpSolo.TrimStart() + i)].ToString();
                obj.Compac = String.IsNullOrEmpty(objetoSelected.Compac.TrimStart()) ? " " : fileOpen[(objetoSelected.Compac.TrimStart() + i)].ToString();

                obj.PHCaCl2 = String.IsNullOrEmpty(objetoSelected.PHCaCl2.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.PHCaCl2.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.PHCaCl2.TrimStart() + i)].ToString());

                obj.MO = String.IsNullOrEmpty(objetoSelected.MO.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.MO.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.MO.TrimStart() + i)].ToString());

                obj.PMeHl = String.IsNullOrEmpty(objetoSelected.PMeHl.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.PMeHl.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.PMeHl.TrimStart() + i)].ToString());

                obj.PRes = String.IsNullOrEmpty(objetoSelected.PRes.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.PRes.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.PRes.TrimStart() + i)].ToString());

                obj.K = String.IsNullOrEmpty(objetoSelected.K.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.K.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.K.TrimStart() + i)].ToString());

                obj.S = String.IsNullOrEmpty(objetoSelected.S.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.S.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.S.TrimStart() + i)].ToString());

                obj.Ca = String.IsNullOrEmpty(objetoSelected.Ca.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.Ca.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.Ca + i)].ToString());

                obj.Mg = String.IsNullOrEmpty(objetoSelected.Mg.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.Mg.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.Mg + i)].ToString());

                obj.Al = String.IsNullOrEmpty(objetoSelected.Al.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.Al.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.Al + i)].ToString());

                obj.CTC = String.IsNullOrEmpty(objetoSelected.CTC.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.CTC.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.CTC + i)].ToString());

                obj.Argila = String.IsNullOrEmpty(objetoSelected.Argila.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.Argila.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.Argila + i)].ToString());

                obj.B = String.IsNullOrEmpty(objetoSelected.B.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.B.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.B + i)].ToString());

                obj.Zn = String.IsNullOrEmpty(objetoSelected.Zn.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.Zn.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.Zn.TrimStart() + i)].ToString());

                obj.Fe = String.IsNullOrEmpty(objetoSelected.Fe.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.Fe.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.Fe.TrimStart() + i)].ToString());


                obj.Mn = String.IsNullOrEmpty(objetoSelected.Mn.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.Mn.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.Mn.TrimStart() + i)].ToString());

                obj.Cu = String.IsNullOrEmpty(objetoSelected.Cu.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.Cu.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.Cu.TrimStart() + i)].ToString());

                obj.Co = String.IsNullOrEmpty(objetoSelected.Co.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.Co.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.Co.TrimStart() + i)].ToString());

                obj.Momicro = String.IsNullOrEmpty(objetoSelected.Momicro.TrimStart()) ? "0" : (String.IsNullOrEmpty(fileOpen[(objetoSelected.Momicro.TrimStart() + i)].ToString())
                                                                                    ? "0" : fileOpen[(objetoSelected.Momicro.TrimStart() + i)].ToString());

                int CodigoAreaServico = 0;
                int.TryParse(obj.Area, out CodigoAreaServico);

                ImportItensLabView itemAreaServicoExist = _areaServicoAppService.ExistAnaliseByCodigo(CodigoAreaServico);
                obj.AreaServicoAnaliseExiste = (itemAreaServicoExist == null) ? false : itemAreaServicoExist.AreaServicoAnaliseExiste;

                int CodigoGrid = 0;
                int.TryParse(obj.Grid, out CodigoGrid);

                ImportItensLabView itemGridExist = (obj.AreaServicoAnaliseExiste == false) ? null : _gridAppService.ExistAnaliseByCodigo(CodigoGrid, Guid.Parse(itemAreaServicoExist.IDAreaServico.ToString()));
                obj.GridAnaliseExiste = (itemGridExist == null) ? false : itemGridExist.GridAnaliseExiste;


                ImportCollection.Add(obj);
            }


            // Quando todos os dados carregar, este código vai verificar sé o arquivo existe e logo em seguida vai remover o arquivo da pasta temporária. 
            //FileStream flsI = File.OpenRead(path);
            //flsI.Close();
            //if (File.Exists(path))
            //{
            //    File.Delete(path);
            //}

            return ImportCollection;
        }

        [HttpPost]
        [ActionName("importfileanalise")]
        [Route("api/itensanliselaboratorio/importfileanalise")]
        public bool ImportFileAnalise(Fields oFile)
        {
            /*
                Este método será utilizado para criar uma cópia de um arquivo em uma pasta temporária. 
                Com o objetivo de facilitar na hora de abrir ele no método 'analisesolofile'.
            */

            try
            {
                /* Aqui faz a remoção dos primeiros dados que é o data. 
                   Logo em seguida esses valores são convertidos de data para bytes. */
                oFile.data = oFile.data.Split(',')[1];

                Byte[] bytes = Convert.FromBase64String(oFile.data);

                string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/FileItensAnalises/"), oFile.fileName);
                filePath = Path.ChangeExtension(filePath, oFile.extension);
                File.WriteAllBytes(filePath, bytes);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // POST api/<controller>
        public ValidationResult Post([FromBody] SequenciaImportacao obj)
        {
            obj.Grid = obj.Grid == null ? "" : obj.Grid;
            obj.NPonto = obj.NPonto == null ? "" : obj.NPonto;
            obj.TpSolo = obj.TpSolo == null ? "" : obj.TpSolo;
            obj.Compac = obj.Compac == null ? "" : obj.Compac;
            obj.PHCaCl2 = obj.PHCaCl2 == null ? "" : obj.PHCaCl2;
            obj.MO = obj.MO == null ? "" : obj.MO;
            obj.PMeHl = obj.PMeHl == null ? "" : obj.PMeHl;
            obj.PRes = obj.PRes == null ? "" : obj.PRes;
            obj.K = obj.K == null ? "" : obj.K;
            obj.S = obj.S == null ? "" : obj.S;
            obj.Ca = obj.Ca == null ? "" : obj.Ca;
            obj.Mg = obj.Mg == null ? "" : obj.Mg;
            obj.Al = obj.Al == null ? "" : obj.Al;
            obj.CTC = obj.CTC == null ? "" : obj.CTC;
            obj.Argila = obj.Argila == null ? "" : obj.Argila;
            obj.B = obj.B == null ? "" : obj.B;
            obj.Zn = obj.Zn == null ? "" : obj.Zn;
            obj.Fe = obj.Fe == null ? "" : obj.Fe;
            obj.Mn = obj.Mn == null ? "" : obj.Mn;
            obj.Cu = obj.Cu == null ? "" : obj.Cu;
            obj.Co = obj.Co == null ? "" : obj.Co;
            obj.Momicro = obj.Momicro == null ? "" : obj.Momicro;

            obj.umphcacl2 = obj.umphcacl2 == null ? "" : obj.umphcacl2;
            obj.ummo = obj.ummo == null ? "" : obj.ummo;
            obj.umpmehl = obj.umpmehl == null ? "" : obj.umpmehl;
            obj.umpres = obj.umpres == null ? "" : obj.umpres;
            obj.umk2o = obj.umk2o == null ? "" : obj.umk2o;
            obj.ums = obj.ums == null ? "" : obj.ums;
            obj.umca = obj.umca == null ? "" : obj.umca;
            obj.ummg = obj.ummg == null ? "" : obj.ummg;
            obj.umal = obj.ummg == null ? "" : obj.ummg;
            obj.umctc = obj.ummg == null ? "" : obj.ummg;
            obj.umargila = obj.ummg == null ? "" : obj.ummg;
            obj.umb = obj.umb == null ? "" : obj.umb;
            obj.umzn = obj.umzn == null ? "" : obj.umzn;
            obj.umfe = obj.umfe == null ? "" : obj.umfe;
            obj.ummn = obj.ummn == null ? "" : obj.ummn;
            obj.umcu = obj.umcu == null ? "" : obj.umcu;
            obj.umco = obj.umcu == null ? "" : obj.umcu;
            obj.ummomicro = obj.ummomicro == null ? "" : obj.ummomicro;

            return _itensAnaliseLaboratorio.Add(obj);
        }

        // PUT api/<controller>/5
        public ValidationResult Put(Guid objID, [FromBody] SequenciaImportacao obj)
        {
            return _itensAnaliseLaboratorio.Update(obj);
        }

        // DELETE api/<controller>/5
        public ValidationResult Delete(Guid objID)
        {
            SequenciaImportacao obj = _itensAnaliseLaboratorio.Find(objID);
            return _itensAnaliseLaboratorio.Remove(obj);
        }
    }
}