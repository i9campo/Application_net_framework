using Sigma.Domain.Entities;
using Sigma.Domain.IdentityEntities;
using Sigma.Infra.Data.Context.Common;
using Sigma.Infra.Data.Context.DbConfig;
using Sigma.Infra.Data.Context.IdentityConfig;
using Sigma.Infra.Data.Context.MapConfig;
using Sigma.Infra.Data.Context.MapConfig.IdentityConfig;
using Sigma.Infra.Data.MapConfig;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Sigma.Infra.Data.Context
{
    public class DBContext : BaseContext
    {
        public DBContext()
            :base("SigmaDB")
        {

        }
        //A
        public DbSet<Adubo> Adubo { get; set; }
        public DbSet<AmostraFoliar> AmostraFoliar { get; set; }
        public DbSet<Amostra> Amostra { get; set; }
        public DbSet<AnaliseSolo> AnaliseSolo { get; set; }

        public DbSet<Area> Area { get; set; }
        public DbSet<AreaServico> AreaServico { get; set; }
        //C
        public DbSet<CicloProducao> CicloProducao { get; set; }
        public DbSet<Corretivo> Corretivo { get; set; }
        public DbSet<Cultura> Cultura { get; set; }
        //E
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<EstagioCultura> EstagioCultura { get; set; }
        public DbSet<ExtracaoCultura> ExtracaoCultura { get; set; }
        //F
        public DbSet<FaixaTeor> FaixaTeor { get; set; }
        public DbSet<Fertilizante> Fertilizante { get; set; }
        public DbSet<FormulacaoAdubo> FormulacaoAdubo { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        //G
        public DbSet<Grid> Grid { get; set; }
        //I
        public DbSet<Imagem> Imagem { get; set; }
        public DbSet<ImagemSatelite> ImagemSatelites { get; set; }
        public DbSet<ImagemSateliteRecortada> ImagemSateliteRecortada { get; set; }
        //L
        public DbSet<Laboratorio> Laboratorio { get; set; }
        //N
        public DbSet<NivelSolo> NivelSolo { get; set; }
        //P

        public DbSet<ParametroArea> ParametroArea { get; set; }

        public DbSet<ParametroPropriedade> ParametroPropriedade { get; set; }
        public DbSet<ParametroRecomendacao> ParametroRecomendacao { get; set; }
        public DbSet<PartePlanta> PartePlanta { get; set; }
        public DbSet<Problema> Problema { get; set; }
        public DbSet<ProdutividadeVariedade> ProdutividadeVariedade { get; set; }
        public DbSet<ProdutoSimulador> ProdutoSimulador { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Proprietario> Proprietario { get; set; }
        public DbSet<Propriedade> Propriedade { get; set; }
        public DbSet<ProprietarioFornecedor> ProprietarioFornecedor { get; set; }
        //R
        public DbSet<RecomendacaoFoliar> RecomendacaoFoliar { get; set; }
        public DbSet<Regiao> Regiao { get; set; }
        public DbSet<Resteva> Resteva { get; set; }
        //S
        public DbSet<Safra> Safra { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<Simulacao> Simulacao { get; set; }
        public DbSet<SequenciaImportacao> SequenciaImportacaos { get; set; }
        public DbSet<SaveTemporaryImgByte> SaveTemporaryImgBytes { get; set; }
        //T
        public DbSet<TeorFoliar> TeorFoliar { get; set; }
        public DbSet<TeorSolo> TeorSolo { get; set; }
        public DbSet<TipoAmostra> TipoAmostra { get; set; }
        public DbSet<TipoSolo> TipoSolo { get; set; }
        //U
        public DbSet<UnidadeMedida> UnidadeMedida { get; set; }
        public DbSet<UnidadeDeLaboratorio> UnidadeDeLaboratorio { get; set; }
        public DbSet<UsoProduto> UsoProduto { get; set; }
        public DbSet<UsuarioAtivo> UsuarioAtivo { get; set; }
        //V
        public DbSet<VariedadeCultura> VariedadeCultura { get; set; }

        ////-> ####### ORDENAR ##########   ^^^^^^

        ////TABELAS ASP.NET IDENTITY
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UserClaims> UserClaims { get; set; }
        public DbSet<Claims> Claims { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(o => o.Name == "objID")
                .Configure(o => o.IsKey());

            //A
            modelBuilder.Configurations.Add(new AduboConfig());
            modelBuilder.Configurations.Add(new AmostraFoliarConfig());
            modelBuilder.Configurations.Add(new AmostraConfig());
            modelBuilder.Configurations.Add(new AnaliseSoloConfig());
            modelBuilder.Configurations.Add(new AreaConfig());
            modelBuilder.Configurations.Add(new AreaServicoConfig());
            //C
            modelBuilder.Configurations.Add(new CicloProducaoConfig());
            modelBuilder.Configurations.Add(new CorretivoConfig());
            modelBuilder.Configurations.Add(new CulturaConfig());
            //E
            modelBuilder.Configurations.Add(new EmpresaConfig());
            modelBuilder.Configurations.Add(new EstagioCulturaConfig());
            modelBuilder.Configurations.Add(new ExtracaoCulturaConfig());
            //F
            modelBuilder.Configurations.Add(new FaixaTeorConfig());
            modelBuilder.Configurations.Add(new FertilizanteConfig());
            modelBuilder.Configurations.Add(new FormulacaoAduboConfig());
            modelBuilder.Configurations.Add(new FornecedorConfig());
            //G
            modelBuilder.Configurations.Add(new GridConfig());
            //I 
            modelBuilder.Configurations.Add(new ImagemConfig());
            modelBuilder.Configurations.Add(new ImagemSateliteConfig());
            modelBuilder.Configurations.Add(new ImagemSateliteRecortadaConfig()); 
            //L
            modelBuilder.Configurations.Add(new LaboratorioConfig());
            //N
            modelBuilder.Configurations.Add(new NivelSoloConfig());
            //P
            modelBuilder.Configurations.Add(new ParametroAreaConfig());
            modelBuilder.Configurations.Add(new ParametroPropriedadeConfig());
            modelBuilder.Configurations.Add(new ParametroRecomendacaoConfig());
            modelBuilder.Configurations.Add(new PartePlantaConfig());
            modelBuilder.Configurations.Add(new ProblemaConfig());
            modelBuilder.Configurations.Add(new ProdutividadeVariedadeConfig());
            modelBuilder.Configurations.Add(new ProdutoConfig());
            modelBuilder.Configurations.Add(new ProdutoSimuladorConfig());
            modelBuilder.Configurations.Add(new PropriedadeConfig());
            modelBuilder.Configurations.Add(new ProprietarioConfig());
            modelBuilder.Configurations.Add(new ProprietarioFornecedorConfig());
            //R
            modelBuilder.Configurations.Add(new RecomendacaoFoliarConfig());
            modelBuilder.Configurations.Add(new RestevaConfig());
            modelBuilder.Configurations.Add(new RegiaoConfig());
            //S
            modelBuilder.Configurations.Add(new SafraConfig());
            modelBuilder.Configurations.Add(new ServicoConfig());
            modelBuilder.Configurations.Add(new SimulacaoConfig());
            modelBuilder.Configurations.Add(new SequenciaImportacaoConfig());
            modelBuilder.Configurations.Add(new SaveTemporaryImgByteConfig()); 
            //T
            modelBuilder.Configurations.Add(new TeorFoliarConfig());
            modelBuilder.Configurations.Add(new TeorSoloConfig());
            modelBuilder.Configurations.Add(new TipoAmostraConfig());
            modelBuilder.Configurations.Add(new TipoSoloConfig());
            //U
            modelBuilder.Configurations.Add(new UnidadeMedidaConfig());
            modelBuilder.Configurations.Add(new UnidadeDeLaboratorioConfig());
            modelBuilder.Configurations.Add(new UsoProdutoConfig());
            //V
            modelBuilder.Configurations.Add(new VariedadeCulturaConfig());

            //-> ####### ORDENAR ##########   ^^^^^^

            //TABELAS ASP.NET IDENTITY
            modelBuilder.Configurations.Add(new UsuarioConfig());
            modelBuilder.Configurations.Add(new ClaimsConfig());
            modelBuilder.Configurations.Add(new UsersClaimConfig());
            modelBuilder.Configurations.Add(new RolesConfig());
            modelBuilder.Configurations.Add(new UserRolesConfig());
            modelBuilder.Configurations.Add(new UserLoginConfig());
            modelBuilder.Configurations.Add(new UsuarioAtivoConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
