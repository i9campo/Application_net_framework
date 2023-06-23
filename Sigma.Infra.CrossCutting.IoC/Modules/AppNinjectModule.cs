using Ninject.Modules;
using Sigma.App.AppService;
using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.App.Interfaces._Base;

namespace Sigma.Infra.CrossCutting.IoC.Modules
{
    public class AppNinjectModule : NinjectModule
    {
        public override void Load()
        {
            //A
            Bind(typeof(IAppService<>)).To(typeof(AppService<>));
            Bind(typeof(IAduboAppService)).To(typeof(AduboAppService));
            Bind(typeof(IAmostraAppService)).To(typeof(AmostraAppService));
            Bind(typeof(IAmostraFoliarAppService)).To(typeof(AmostraFoliarAppService));
            Bind(typeof(IAnaliseSoloAppService)).To(typeof(AnaliseSoloAppService));
            Bind(typeof(IAreaAppService)).To(typeof(AreaAppService));
            Bind(typeof(IAreaServicoAppService)).To(typeof(AreaServicoAppService));
            Bind(typeof(IArquivoAreaAppService)).To(typeof(ArquivoAreaAppService)); 

            //C
            Bind(typeof(ICulturaAppService)).To(typeof(CulturaAppService));
            Bind(typeof(ICicloProducaoAppService)).To(typeof(CicloProducaoAppService));
            Bind(typeof(ICorretivoAppService)).To(typeof(CorretivoAppService));

            //E
            Bind(typeof(IEmpresaAppService)).To(typeof(EmpresaAppService));
            Bind(typeof(IExtracaoCulturaAppService)).To(typeof(ExtracaoCulturaAppService));
            Bind(typeof(IEstagioCulturaAppService)).To(typeof(EstagioCulturaAppService));

            //F
            Bind(typeof(IFertilizanteAppService)).To(typeof(FertilizanteAppService));
            Bind(typeof(IFaixaTeorAppService)).To(typeof(FaixaTeorAppService));
            Bind(typeof(IFormulacaoAduboAppService)).To(typeof(FormulacaoAduboAppService));
            Bind(typeof(IFornecedorAppService)).To(typeof(FornecedorAppService));

            //G
            Bind(typeof(IGeoConfigurationAppService)).To(typeof(GeoConfigurationAppService)); 
            Bind(typeof(IGridAppService)).To(typeof(GridAppService));


            //I
            Bind(typeof(IImagemAppService)).To(typeof(ImagemAppService));
            Bind(typeof(IImagemRecorteAppService)).To(typeof(ImagemRecorteAppService)); 
            Bind(typeof(IImagemSateliteAppService)).To(typeof(ImagemSateliteAppService));
            Bind(typeof(IItensAnaliseLaboratorioAppService)).To(typeof(ItensAnalisesLaboratorioAppService));


            //L
            Bind(typeof(ILaboratorioAppService)).To(typeof(LaboratorioAppService));

            //N
            Bind(typeof(INivelSoloAppService)).To(typeof(NivelSoloAppService));

            //P

            Bind(typeof(IParametroAreaAppService)).To(typeof(ParametroAreaAppService));
            Bind(typeof(IParametroPropriedadeAppService)).To(typeof(ParametroPropriedadeAppService));
            Bind(typeof(IPartePlantaAppService)).To(typeof(PartePlantaAppService));
            Bind(typeof(IPropriedadeAppService)).To(typeof(PropriedadeAppService));
            Bind(typeof(IProprietarioAppService)).To(typeof(ProprietarioAppService));
            Bind(typeof(IProdutividadeVariedadeAppService)).To(typeof(ProdutividadeVariedadeAppService));
            Bind(typeof(IProblemaAppService)).To(typeof(ProblemaAppService));
            Bind(typeof(IProdutoAppService)).To(typeof(ProdutoAppService));
            Bind(typeof(IProdutoSimuladorAppService)).To(typeof(ProdutoSimuladorAppService));

            //R
            Bind(typeof(IRegiaoAppService)).To(typeof(RegiaoAppService));
            Bind(typeof(IRecomendacaoFoliarAppService)).To(typeof(RecomendacaoFoliarAppService));
            Bind(typeof(IRolesAppService)).To(typeof(RolesAppService));
            Bind(typeof(IRestevaAppService)).To(typeof(RestevaAppService));

            //S
            Bind(typeof(ISafraAppService)).To(typeof(SafraAppService));
            Bind(typeof(IServicoAppService)).To(typeof(ServicoAppService));
            Bind(typeof(IShapeAppService)).To(typeof(ShapeAppService));
            Bind(typeof(ISimulacaoAppService)).To(typeof(SimulacaoAppService));
            Bind(typeof(ISaveTemporaryImgByteAppService)).To(typeof(SaveTemporaryImgByteAppService)); 
            Bind(typeof(ISequenciaImportacaoAppService)).To(typeof(SequenciaImportacaoAppService));

            //T
            Bind(typeof(ITeorFoliarAppService)).To(typeof(TeorFoliarAppService));
            Bind(typeof(ITeorSoloAppService)).To(typeof(TeorSoloAppService));
            Bind(typeof(ITipoAmostraAppService)).To(typeof(TipoAmostraAppService));
            Bind(typeof(ITipoSoloAppService)).To(typeof(TipoSoloAppService));

            //U
            Bind(typeof(IUsuarioAppService)).To(typeof(UsuarioAppService));
            Bind(typeof(IUsuarioAtivoAppService)).To(typeof(UsuarioAtivoAppService));
            Bind(typeof(IUnidadeMedidaAppService)).To(typeof(UnidadeMedidaAppService));
            Bind(typeof(IUnidadeDeLaboratorioAppService)).To(typeof(UnidadeDeLaboratorioAppService)); 
            Bind(typeof(IUsoProdutoAppService)).To(typeof(UsoProdutoAppService));

            //V
            Bind(typeof(IVariedadeCulturaAppService)).To(typeof(VariedadeCulturaAppService));
            //Reports
            //Bind(typeof(BalancoFoliarReport)).ToSelf();

        }
    }
}