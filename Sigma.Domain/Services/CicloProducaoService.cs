using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sigma.Domain.Services
{
    public class CicloProducaoService : Service<CicloProducao>, ICicloProducaoService
    {
        private readonly IAreaServicoRepository _areaServico;
        private readonly ICulturaRepository _cultura;
        private readonly IVariedadeCulturaRepository _variedadeCultura; 

        private readonly ICicloProducaoRepository _repository;
        private readonly IFertilizanteRepository _fertilizante;
        private readonly IGeoConfigRepository _geoConfig; 

        public CicloProducaoService(ICicloProducaoRepository repository, IFertilizanteRepository fertilizante, IGeoConfigRepository geoConfig, IAreaServicoRepository areaServico, ICulturaRepository cultura, IVariedadeCulturaRepository variedadeCultura)
            : base(repository)
        {
            _repository = repository;
            _fertilizante = fertilizante;
            _geoConfig = geoConfig;

            _areaServico = areaServico;
            _cultura = cultura;
            _variedadeCultura = variedadeCultura; 
        }

        public IEnumerable<AnaliseSolo> GetAnalises(Guid IDCicloProducao, string profundidade)
        {
            return _repository.GetAnalises(IDCicloProducao, profundidade);
        }

        public IEnumerable<CicloProducaoView> GetAllByAreaServico(Guid IDAreaServico, string Type)
        {
            //SaldoCicloIntermediario(IDAreaServico); 
            return _repository.GetAllByAreaServico(IDAreaServico, Type);
        }

        public IEnumerable<CicloProducaoView> GetCiclo(Guid objID, int tipoCiclo, int retorno)
        {
            return _repository.GetCiclo(objID, tipoCiclo, retorno);
        }

        public void SaldoCicloIntermediario(Guid IDCicloProducao)
        {
            AnaliseSoloView oMedia = new AnaliseSoloView();
            oMedia.N = 0;
            oMedia.p2o5 = 0;
            oMedia.K2O = 0;
            oMedia.S = 0;
            oMedia.Ca = 0;
            oMedia.Cu = 0;
            oMedia.Co = 0;
            oMedia.MO = 0;

            // objID Fixo AreaServico; 
            string CIFix = "b40f61d4-a691-4654-93e9-9a34a8be5417"; // para teste.
            string ID = "2566C7EE-12FD-4A7E-878A-34BA0ADC2B53";


            // Retorna todos os ciclos intermediarios relacionado com a área serviço. 
            IEnumerable<CicloProducaoView> AllCI = _repository.GetAllByAreaServico(Guid.Parse(CIFix), "CI");




            foreach (var CI in AllCI)
            {
                AnaliseSoloView oAdicao_f = new AnaliseSoloView();
                AnaliseSoloView Exportacao = new AnaliseSoloView();
                Exportacao.N = 0;
                Exportacao.p2o5 = 0;
                Exportacao.K2O = 0;
                Exportacao.S = 0;
                Exportacao.Ca = 0;
                Exportacao.Mg = 0;
                Exportacao.Cu = 0;
                Exportacao.Co = 0;
                Exportacao.MO = 0;


                Exportacao = _repository.GetExportacaoCiclo(CI.objID);
                if (Exportacao != null)
                {
                    oMedia.N -= Exportacao.N;
                    oMedia.p2o5 -= Exportacao.p2o5;
                    oMedia.K2O -= Exportacao.K2O;
                    oMedia.S -= Exportacao.S;
                    oMedia.Ca -= Exportacao.Ca;
                    oMedia.Mg -= Exportacao.Mg;
                    oMedia.Cu -= Exportacao.Cu;
                    oMedia.Co -= Exportacao.Co;
                    oMedia.MO -= Exportacao.MO;
                }

                // Retorna os valores de STIntersection Ciclo de produção e o ciclo intermediario. 
                oAdicao_f = _repository.GetAdicao_F(Guid.Parse(ID), CI.objID);
                if (oAdicao_f != null)
                {
                    oMedia.N += oAdicao_f.N;
                    oMedia.p2o5 += oAdicao_f.p2o5;
                    oMedia.K2O += oAdicao_f.K2O;
                    oMedia.S += oAdicao_f.S;
                    oMedia.Ca += oAdicao_f.Ca;
                    oMedia.Mg += oAdicao_f.Mg;
                    oMedia.Cu += oAdicao_f.Cu;
                    oMedia.Co += oAdicao_f.Co;
                    oMedia.MO += oAdicao_f.MO;
                }
            }

            // Retorna um objeto Ciclo de produção. 
            CicloProducaoView objeto = GetCiclo(Guid.Parse(ID), 0, 1).FirstOrDefault(); 

            // Aqui preenche as media final. 
            oMedia.N    = Double.Parse(Math.Round(Double.Parse(oMedia.N.ToString()) / float.Parse(objeto.tamanho.ToString()), 2).ToString());
            oMedia.p2o5 = Double.Parse(Math.Round(float.Parse(oMedia.p2o5.ToString()) / float.Parse(objeto.tamanho.ToString()), 2).ToString());
            oMedia.K2O  = Double.Parse(Math.Round(float.Parse(oMedia.K2O.ToString()) / float.Parse(objeto.tamanho.ToString()), 2).ToString());
            oMedia.S    = Double.Parse(Math.Round(float.Parse(oMedia.S.ToString()) / float.Parse(objeto.tamanho.ToString()), 2).ToString());
            oMedia.Ca   = Double.Parse(Math.Round(float.Parse(oMedia.Ca.ToString()) / float.Parse(objeto.tamanho.ToString()), 2).ToString());
            oMedia.Mg   = Double.Parse(Math.Round(float.Parse(oMedia.Mg.ToString()) / float.Parse(objeto.tamanho.ToString()), 2).ToString());
            oMedia.Cu   = Double.Parse(Math.Round(float.Parse(oMedia.Cu.ToString()) / float.Parse(objeto.tamanho.ToString()), 2).ToString());
            oMedia.Co   = Double.Parse(Math.Round(float.Parse(oMedia.Co.ToString()) / float.Parse(objeto.tamanho.ToString()), 2).ToString());
            oMedia.MO   = Double.Parse(Math.Round(float.Parse(oMedia.MO.ToString()) / float.Parse(objeto.tamanho.ToString()), 2).ToString());
        }

        public IEnumerable<CicloProducaoView> GetCicloByPropriedadeSafra(Guid IDSafra, Guid IDPropriedade, string Type)
        {
            return _repository.GetCicloByPropriedadeSafra(IDSafra, IDPropriedade, Type); 
        }

        public IEnumerable<CicloProducaoView> GetCicloAndAreaServico(Guid objID, int tipoCiclo, int retorno)
        {
            List<CicloProducaoView> lst = new List<CicloProducaoView>();
            IEnumerable<CicloProducaoView> Ciclos = _repository.GetCiclo(objID, tipoCiclo, retorno); 
            if (Ciclos.Count() > 0)
            {
                IEnumerable<GeoDate> gd = _geoConfig.GetSplitMultGeoCiclo(objID, 0, tipoCiclo);
                foreach (var splitArea in gd)
                {
                    CicloProducaoView splcp = new CicloProducaoView();
                    splcp.objID = Guid.NewGuid();
                    splcp.ID = null;
                    splcp.IDAreaServico = objID;
                    splcp.IDCulturaAnterior = null;
                    splcp.StringGeoJson = splitArea.GeoString;
                    splcp.UnidadeMedida = null;
                    splcp.ciclo = 0;
                    splcp.codigo = 0;
                    splcp.cultura = "SEM CICLO ";
                    splcp.dataColheita = null;
                    splcp.dataPlantio = null;
                    splcp.dataRealPlantio = null;
                    splcp.dias = null;
                    splcp.geoJson = splitArea.GeoJson;
                    splcp.identificacao = "";
                    splcp.inoculante = true;
                    splcp.prodMaxima = 0;
                    splcp.prodMinima = 0;
                    splcp.prodReal = 0;
                    splcp.tamanho = _geoConfig.GetSize(splitArea.GeoString);
                    splcp.tipo = null;
                    lst.Add(splcp);
                }
            }
            else
            {
                CicloProducaoView item = new CicloProducaoView();
                item.objID = Guid.NewGuid(); 
                item.ID    =  null;
                item.IDAreaServico = objID;
                item.IDCulturaAnterior = null;
                item.StringGeoJson = _geoConfig.GetGeoAreaByIDAreaServico(objID);
                item.UnidadeMedida = null;
                item.ciclo = 0;
                item.codigo = 0;
                item.cultura = "SEM CICLO ";
                item.dataColheita = null;
                item.dataPlantio = null;
                item.dataRealPlantio = null;
                item.dias = null;
                item.geoJson = _geoConfig.GetGeoJsonByIDAreaServico(objID);
                item.identificacao = "";
                item.inoculante = true;
                item.prodMaxima = 0;
                item.prodMinima = 0;
                item.prodReal = 0;
                item.tamanho = _geoConfig.GetSize(item.StringGeoJson);
                item.tipo = null;
                lst.Add(item);
            }

            lst.AddRange(Ciclos); 

            return lst; 
        }

        public IEnumerable<CicloViewer> GetAllCicloByAreaServico(Guid objID, string Tipo)
        {
            List<CicloViewer> lst = _repository.GetAllCicloByAreaServico(objID, Tipo).ToList();
            foreach (var item in lst)
            {
                if (item.IDAreaServico != null)
                    item.oAreaServico = _areaServico.Find(item.IDAreaServico);

                if (item.IDCultura != null)
                    item.oCultura = _cultura.Find(Guid.Parse(item.IDCultura.ToString()));

                if (item.IDVariedadeCultura != null)
                    item.oVariedadeCultura = _variedadeCultura.Find(Guid.Parse(item.IDVariedadeCultura.ToString())); 

                if (item.IDCulturaAnterior != null)
                    item.oCulturaAnterior = _cultura.Find(Guid.Parse(item.IDCultura.ToString()));
            }

            return lst; 
        }
    }
}
