using Ninject.Modules;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Infra.Data.Repositories;
using Sigma.Infra.Data.Repositories._Base;

namespace Sigma.Infra.CrossCutting.IoC.Modules
{
    public class RepositoryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            //A
            Bind(typeof(IAmostraRepository)).To(typeof(AmostraRepository)); 
            Bind(typeof(IAmostraFoliarRepository)).To(typeof(AmostraFoliarRepository));
            Bind(typeof(IAduboRepository)).To(typeof(AduboRepository));
            Bind(typeof(IAnaliseSoloRepository)).To(typeof(AnaliseSoloRepository));
            Bind(typeof(IAreaRepository)).To(typeof(AreaRepository));
            Bind(typeof(IAreaServicoRepository)).To(typeof(AreaServicoRepository));
            Bind(typeof(IArquivoAreaRepository)).To(typeof(ArquivoAreaRepository)); 
            //C
            Bind(typeof(ICorretivoRepository)).To(typeof(CorretivoRepository));
            Bind(typeof(ICulturaRepository)).To(typeof(CulturaRepository));
            Bind(typeof(ICicloProducaoRepository)).To(typeof(CicloProducaoRepository));
            //E
            Bind(typeof(IEmpresaRepository)).To(typeof(EmpresaRepository));
            Bind(typeof(IEstagioCulturaRepository)).To(typeof(EstagioCulturaRepository));
            Bind(typeof(IExtracaoCulturaRepository)).To(typeof(ExtracaoCulturaRepository));
            //F
            Bind(typeof(IFaixaTeorRepository)).To(typeof(FaixaTeorRepository));
            Bind(typeof(IFormulacaoAduboRepository)).To(typeof(FormulacaoAduboRepository));
            Bind(typeof(IFornecedorRepository)).To(typeof(FornecedorRepository));
            Bind(typeof(IFertilizanteRepository)).To(typeof(FertilizanteRepository));
            //G
            Bind(typeof(IGridRepository)).To(typeof(GridRepository));
            Bind(typeof(IGeoConfigRepository)).To(typeof(GeoConfigRepositoy)); 
            //I
            Bind(typeof(IIMagemRepository)).To(typeof(ImagemRepository));
            Bind(typeof(IImagemRecorteRepository)).To(typeof(ImagemRecorteRepository)); 
            Bind(typeof(IimagemSateliteRepository)).To(typeof(ImagemSateliteRepository));
            //L
            Bind(typeof(ILaboratorioRepository)).To(typeof(LaboratorioRepository));
            //N
            Bind(typeof(INivelSoloRepository)).To(typeof(NivelSoloRepository));
            //P
            Bind(typeof(IParametroAreaRepository)).To(typeof(ParaMetroAreaRepository));
            Bind(typeof(IParametroPropriedadeRepository)).To(typeof(ParametroPropriedadeRepository));
            Bind(typeof(IPartePlantaRepository)).To(typeof(PartePlantaRepository));
            Bind(typeof(IPropriedadeRepository)).To(typeof(PropriedadeRepository));
            Bind(typeof(IProprietarioRepository)).To(typeof(ProprietarioRepository));
            Bind(typeof(IProprietarioFornecedorRepository)).To(typeof(ProprietarioFornecedorRepository));
            Bind(typeof(IProblemaRepository)).To(typeof(ProblemaRepository));
            Bind(typeof(IProdutividadeVariedadeRepository)).To(typeof(ProdutividadeVariedadeRepository));
            Bind(typeof(IProdutoRepository)).To(typeof(ProdutoRepository));
            Bind(typeof(IProdutoSimuladorRepository)).To(typeof(ProdutoSimuladorRepository));
            //R
            Bind(typeof(IRegiaoRepository)).To(typeof(RegiaoRepository));
            Bind(typeof(IRecomendacaoFoliarRepository)).To(typeof(RecomendacaoFoliarRepository));
            Bind(typeof(IRepository<>)).To(typeof(RepositoryBase<>));
            Bind(typeof(IRestevaRepository)).To(typeof(RestevaRepository));
            Bind(typeof(IRolesRepository)).To(typeof(RolesRepository));
            //S
            Bind(typeof(ISafraRepository)).To(typeof(SafraRepository));
            Bind(typeof(IServicoRepository)).To(typeof(ServicoRepository));
            Bind(typeof(IShapeRepository)).To(typeof(ShapeRepository));
            Bind(typeof(ISimulacaoRepository)).To(typeof(SimulacaoRepository));
            Bind(typeof(ISaveTemporaryImgByteRepository)).To(typeof(SaveTemporaryImgByteRepository)); 
            Bind(typeof(ISequenciaImportacaoRepository)).To(typeof(SequenciaImportacaoRepository));

            //T
            Bind(typeof(ITeorFoliarRepository)).To(typeof(TeorFoliarRepository));
            Bind(typeof(ITeorSoloRepository)).To(typeof(TeorSoloRepository));
            Bind(typeof(ITipoAmostraRepository)).To(typeof(TipoAmostraRepository));
            Bind(typeof(ITipoSoloRepository)).To(typeof(TipoSoloRepository));
            //U
            Bind(typeof(IUsuarioAtivoRepository)).To(typeof(UsuarioAtivoRepository)); 
            Bind(typeof(IUsuarioRepository)).To(typeof(UsuarioRepository)); 
            Bind(typeof(IUnidadeMedidaRepository)).To(typeof(UnidadeMedidaRepository));
            Bind(typeof(IUnidadeDeLaboratorioRepository)).To(typeof(UnidadeDeLaboratorioRepository)); 
            Bind(typeof(IUsoProdutoRepository)).To(typeof(UsoProdutoRepository));
            //V
            Bind(typeof(IVariedadeCulturaRepository)).To(typeof(VariedadeCulturaRepository));
        }
    }
}
