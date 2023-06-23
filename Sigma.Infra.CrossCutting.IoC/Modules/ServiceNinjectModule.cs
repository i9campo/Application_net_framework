using Ninject.Modules;
using Sigma.Domain.Interfaces.Service._Base;
using Sigma.Domain.Services._Base;
using Sigma.Domain.Services;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Interfaces.Service;
using Sigma.App.AppService;

namespace Sigma.Infra.CrossCutting.IoC.Modules
{
    public class ServiceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            //A
            Bind(typeof(IAmostraService)).To(typeof(AmostraService)); 
            Bind(typeof(IAmostraFoliarService)).To(typeof(AmostraFoliarService));
            Bind(typeof(IAnaliseSoloService)).To(typeof(AnaliseSoloService));
            Bind(typeof(IAduboService)).To(typeof(AduboService));
            Bind(typeof(IAreaService)).To(typeof(AreaService));
            Bind(typeof(IAreaServicoService)).To(typeof(AreaServicoService));
            Bind(typeof(IArquivoAreaService)).To(typeof(ArquivoAreaService)); 

            //C
            Bind(typeof(ICulturaService)).To(typeof(CulturaService));
            Bind(typeof(ICorretivoService)).To(typeof(CorretivoService));
            Bind(typeof(ICicloProducaoService)).To(typeof(CicloProducaoService)); 
            //E
            Bind(typeof(IEmpresaService)).To(typeof(EmpresaService));
            Bind(typeof(IEstagioCulturaService)).To(typeof(EstagioCulturaService));
            Bind(typeof(IExtracaoCulturaService)).To(typeof(ExtracaoCulturaService));
            //F
            Bind(typeof(IFaixaTeorService)).To(typeof(FaixaTeorService));
            Bind(typeof(IFormulacaoAduboService)).To(typeof(FormulacaoAduboService));
            Bind(typeof(IFornecedorService)).To(typeof(FornecedorService));
            Bind(typeof(IFertilizanteService)).To(typeof(FertilizanteService));
            //G
            Bind(typeof(IGridService)).To(typeof(GridService));
            Bind(typeof(IGeoConfigService)).To(typeof(GeoConfigService)); 
            //I
            Bind(typeof(IImagemService)).To(typeof(ImagemService));
            Bind(typeof(IImagemRecorteService)).To(typeof(ImagemRecorteService));
            Bind(typeof(IimagemSateliteService)).To(typeof(ImagemSateliteService));
            Bind(typeof(IItensAnaliseLaboratorioService)).To(typeof(ItensAnalisesLaboratorioService)); 
            //L
            Bind(typeof(ILaboratorioService)).To(typeof(LaboratorioService));
            //N
            Bind(typeof(INivelSoloService)).To(typeof(NivelSoloService));
            //P
            Bind(typeof(IParametroAreaService)).To(typeof(ParametroAreaService));
            Bind(typeof(IParametroPropriedadeService)).To(typeof(ParametroPropriedadeService));
            Bind(typeof(IProprietarioFornecedorService)).To(typeof(ProprietarioFornecedorService));
            Bind(typeof(IPartePlantaService)).To(typeof(PartePlantaService));
            Bind(typeof(IPropriedadeService)).To(typeof(PropriedadeService));
            Bind(typeof(IProprietarioService)).To(typeof(ProprietarioService));
            Bind(typeof(IProblemaService)).To(typeof(ProblemaService));
            Bind(typeof(IProdutoService)).To(typeof(ProdutoService));
            Bind(typeof(IProdutoSimuladorService)).To(typeof(ProdutoSimuladorService));
            Bind(typeof(IProdutividadeVariedadeService)).To(typeof(ProdutividadeVariedadeService));
            //R
            Bind(typeof(IRecomendacaoFoliarService)).To(typeof(RecomendacaoFoliarService));
            Bind(typeof(IRestevaService)).To(typeof(RestevaService));
            Bind(typeof(IRegiaoService)).To(typeof(RegiaoService));
            Bind(typeof(IRolesService)).To(typeof(RolesService));
            //S
            Bind(typeof(ISimulacaoService)).To(typeof(SimulacaoService));
            Bind(typeof(IShapeService)).To(typeof(ShapeService)); 
            Bind(typeof(ISafraService)).To(typeof(SafraService));
            Bind(typeof(IServicoService)).To(typeof(ServicoService));
            Bind(typeof(IService<>)).To(typeof(Service<>));
            Bind(typeof(ISaveTemporaryImgByteService)).To(typeof(SaveTemporaryImgByteService));
            Bind(typeof(ISequenciaImportacaoService)).To(typeof(SequenciaImportacaoService)); 

            //T
            Bind(typeof(ITeorFoliarService)).To(typeof(TeorFoliarService));
            Bind(typeof(ITeorSoloService)).To(typeof(TeorSoloService));
            Bind(typeof(ITipoAmostraService)).To(typeof(TipoAmostraService));
            Bind(typeof(ITipoSoloService)).To(typeof(TipoSoloService));
            //U
            Bind(typeof(IUsuarioAtivoService)).To(typeof(UsuarioAtivoService)); 
            Bind(typeof(IUnidadeMedidaService)).To(typeof(UnidadeMedidaService));
            Bind(typeof(IUnidadeDeLaboratorioService)).To(typeof(UnidadeDeLaboratorioService)); 
            Bind(typeof(IUsoProdutoService)).To(typeof(UsoProdutoService));
            Bind(typeof(IUsuarioService)).To(typeof(UsuarioService)); 
            //V
            Bind(typeof(IVariedadeCulturaService)).To(typeof(VariedadeCulturaService));
        }
    }
}
