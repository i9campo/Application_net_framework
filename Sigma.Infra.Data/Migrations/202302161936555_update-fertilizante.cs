namespace Sigma.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefertilizante : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fertilizante",
                c => new
                {
                    objID = c.Guid(nullable: false),
                    IDCicloProducao = c.Guid(nullable: false),
                    IDFornecedor = c.Guid(),
                    IDEstagioCultura = c.Guid(),
                    foliar = c.Boolean(nullable: false),
                    nome = c.String(nullable: false, maxLength: 80),
                    daedap = c.Int(nullable: false),
                    marcado = c.Int(nullable: false),
                    opcao = c.Int(nullable: false),
                    opcaoMarcada = c.Int(nullable: false),
                    qtde = c.Double(nullable: false),
                    eficiencia = c.Double(nullable: false),
                    densidade = c.Double(nullable: false),
                    custo = c.Double(nullable: false),
                    n = c.Double(nullable: false),
                    p2o5 = c.Double(nullable: false),
                    k2o = c.Double(nullable: false),
                    ca = c.Double(nullable: false),
                    mg = c.Double(nullable: false),
                    s = c.Double(nullable: false),
                    b = c.Double(nullable: false),
                    zn = c.Double(nullable: false),
                    cu = c.Double(nullable: false),
                    mn = c.Double(nullable: false),
                    co = c.Double(nullable: false),
                    mo = c.Double(nullable: false),
                })
                .PrimaryKey(t => t.objID)
                .ForeignKey("dbo.CicloProducao", t => t.IDCicloProducao)
                .ForeignKey("dbo.EstagioCultura", t => t.IDEstagioCultura)
                .ForeignKey("dbo.Fornecedor", t => t.IDFornecedor)
                .Index(t => t.IDCicloProducao)
                .Index(t => t.IDFornecedor)
                .Index(t => t.IDEstagioCultura);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UnidadeDeLaboratorio", "IDLaboratorio", "dbo.Laboratorio");
            DropForeignKey("dbo.Amostra", "IDCultura", "dbo.Cultura");
            DropForeignKey("dbo.Cultura", "IDUnidadeMedida", "dbo.UnidadeMedida");
            DropForeignKey("dbo.Resteva", "IDCultura", "dbo.Cultura");
            DropForeignKey("dbo.RecomendacaoFoliar", "IDCultura", "dbo.Cultura");
            DropForeignKey("dbo.NivelSolo", "IDCultura", "dbo.Cultura");
            DropForeignKey("dbo.ExtracaoCultura", "IDCultura", "dbo.Cultura");
            DropForeignKey("dbo.AreaServico", "IDServico", "dbo.Servico");
            DropForeignKey("dbo.AreaServico", "IDSafra", "dbo.Safra");
            DropForeignKey("dbo.AreaServico", "IDProprietarioFatura", "dbo.Proprietario");
            DropForeignKey("dbo.ParametroPropriedade", "AreaServico_objID", "dbo.AreaServico");
            DropForeignKey("dbo.ParametroArea", "IDUltimaCultura", "dbo.Cultura");
            DropForeignKey("dbo.ParametroArea", "IDAreaServico", "dbo.AreaServico");
            DropForeignKey("dbo.Imagem", "IDAreaServico", "dbo.AreaServico");
            DropForeignKey("dbo.AreaServico", "IDCultura", "dbo.Cultura");
            DropForeignKey("dbo.AreaServico", "IDArea", "dbo.Area");
            DropForeignKey("dbo.TeorSolo", "IDAmostraFoliar", "dbo.AmostraFoliar");
            DropForeignKey("dbo.TeorFoliar", "IDAmostraFoliar", "dbo.AmostraFoliar");
            DropForeignKey("dbo.AmostraFoliar", "IDPartePlanta", "dbo.PartePlanta");
            DropForeignKey("dbo.AmostraFoliar", "IDEstagioCultura", "dbo.EstagioCultura");
            DropForeignKey("dbo.Fertilizante", "IDFornecedor", "dbo.Fornecedor");
            DropForeignKey("dbo.Fertilizante", "IDEstagioCultura", "dbo.EstagioCultura");
            DropForeignKey("dbo.Fertilizante", "IDCicloProducao", "dbo.CicloProducao");
            DropForeignKey("dbo.CicloProducao", "IDVariedadeCultura", "dbo.VariedadeCultura");
            DropForeignKey("dbo.ProdutividadeVariedade", "IDVariedadeCultura", "dbo.VariedadeCultura");
            DropForeignKey("dbo.ProdutividadeVariedade", "IDUnidadeMedida", "dbo.UnidadeMedida");
            DropForeignKey("dbo.ProdutividadeVariedade", "IDRegiao", "dbo.Regiao");
            DropForeignKey("dbo.Propriedade", "IDRegiao", "dbo.Regiao");
            DropForeignKey("dbo.Propriedade", "IDProprietario", "dbo.Proprietario");
            DropForeignKey("dbo.Proprietario", "IDEmpresa", "dbo.Empresa");
            DropForeignKey("dbo.UsuarioAtivo", "IDUsuario", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "Usuario_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "Roles_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "Usuario_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProdutoSimulador", "IDUsuarioALT", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProdutoSimulador", "IDSimulacao", "dbo.Simulacao");
            DropForeignKey("dbo.Simulacao", "IDUsuarioINC", "dbo.AspNetUsers");
            DropForeignKey("dbo.Simulacao", "IDProximaCultura", "dbo.Cultura");
            DropForeignKey("dbo.Simulacao", "IDAreaServico", "dbo.AreaServico");
            DropForeignKey("dbo.ProdutoSimulador", "IDProduto", "dbo.Produto");
            DropForeignKey("dbo.UsoProduto", "IDProduto", "dbo.Produto");
            DropForeignKey("dbo.UsoProduto", "IDCultura", "dbo.Cultura");
            DropForeignKey("dbo.Produto", "IDUnidadeMedida", "dbo.UnidadeMedida");
            DropForeignKey("dbo.Produto", "IDFornecedor", "dbo.Fornecedor");
            DropForeignKey("dbo.ProprietarioFornecedor", "IDProprietario", "dbo.Proprietario");
            DropForeignKey("dbo.ProprietarioFornecedor", "IDFornecedor", "dbo.Fornecedor");
            DropForeignKey("dbo.Corretivo", "IDGrid", "dbo.Grid");
            DropForeignKey("dbo.Grid", "IDAreaServico", "dbo.AreaServico");
            DropForeignKey("dbo.AnaliseSolo", "IDTipoSolo", "dbo.TipoSolo");
            DropForeignKey("dbo.AnaliseSolo", "IDGrid", "dbo.Grid");
            DropForeignKey("dbo.AnaliseSolo", "IDAreaServico", "dbo.AreaServico");
            DropForeignKey("dbo.Corretivo", "IDFornecedor", "dbo.Fornecedor");
            DropForeignKey("dbo.Corretivo", "IDAreaServico", "dbo.AreaServico");
            DropForeignKey("dbo.ProdutoSimulador", "IDEstagioCultura", "dbo.EstagioCultura");
            DropForeignKey("dbo.ProdutoSimulador", "IDCultura", "dbo.Cultura");
            DropForeignKey("dbo.Claims", "Usuario_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsuarioAtivo", "IDEmpresa", "dbo.Empresa");
            DropForeignKey("dbo.Area", "IDPropriedade", "dbo.Propriedade");
            DropForeignKey("dbo.Problema", "IDArea", "dbo.Area");
            DropForeignKey("dbo.ParametroRecomendacao", "IDSafra", "dbo.Safra");
            DropForeignKey("dbo.SequenciaImportacao", "IDSafra", "dbo.Safra");
            DropForeignKey("dbo.ParametroPropriedade", "IDSafra", "dbo.Safra");
            DropForeignKey("dbo.ParametroPropriedade", "IDPropriedade", "dbo.Propriedade");
            DropForeignKey("dbo.ParametroRecomendacao", "IDArea", "dbo.Area");
            DropForeignKey("dbo.VariedadeCultura", "IDCultura", "dbo.Cultura");
            DropForeignKey("dbo.CicloProducao", "IDCulturaAnterior", "dbo.Cultura");
            DropForeignKey("dbo.CicloProducao", "IDCultura", "dbo.Cultura");
            DropForeignKey("dbo.CicloProducao", "IDAreaServico", "dbo.AreaServico");
            DropForeignKey("dbo.FaixaTeor", "IDPartePlanta", "dbo.PartePlanta");
            DropForeignKey("dbo.PartePlanta", "IDCultura", "dbo.Cultura");
            DropForeignKey("dbo.FaixaTeor", "IDEstagioCultura", "dbo.EstagioCultura");
            DropForeignKey("dbo.EstagioCultura", "IDCultura", "dbo.Cultura");
            DropForeignKey("dbo.AmostraFoliar", "IDAreaServico", "dbo.AreaServico");
            DropForeignKey("dbo.FormulacaoAdubo", "IDAdubo", "dbo.Adubo");
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.UnidadeDeLaboratorio", new[] { "IDLaboratorio" });
            DropIndex("dbo.Resteva", new[] { "IDCultura" });
            DropIndex("dbo.RecomendacaoFoliar", new[] { "IDCultura" });
            DropIndex("dbo.NivelSolo", new[] { "IDCultura" });
            DropIndex("dbo.ExtracaoCultura", new[] { "IDCultura" });
            DropIndex("dbo.ParametroArea", new[] { "IDUltimaCultura" });
            DropIndex("dbo.ParametroArea", new[] { "IDAreaServico" });
            DropIndex("dbo.Imagem", new[] { "IDAreaServico" });
            DropIndex("dbo.TeorSolo", new[] { "IDAmostraFoliar" });
            DropIndex("dbo.TeorFoliar", new[] { "IDAmostraFoliar" });
            DropIndex("dbo.AspNetUserRoles", new[] { "Usuario_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "Roles_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "Usuario_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Simulacao", new[] { "IDUsuarioINC" });
            DropIndex("dbo.Simulacao", new[] { "IDAreaServico" });
            DropIndex("dbo.Simulacao", new[] { "IDProximaCultura" });
            DropIndex("dbo.UsoProduto", new[] { "IDCultura" });
            DropIndex("dbo.UsoProduto", new[] { "IDProduto" });
            DropIndex("dbo.ProprietarioFornecedor", new[] { "IDFornecedor" });
            DropIndex("dbo.ProprietarioFornecedor", new[] { "IDProprietario" });
            DropIndex("dbo.AnaliseSolo", new[] { "IDGrid" });
            DropIndex("dbo.AnaliseSolo", new[] { "IDTipoSolo" });
            DropIndex("dbo.AnaliseSolo", new[] { "IDAreaServico" });
            DropIndex("dbo.Grid", new[] { "IDAreaServico" });
            DropIndex("dbo.Corretivo", new[] { "IDFornecedor" });
            DropIndex("dbo.Corretivo", new[] { "IDGrid" });
            DropIndex("dbo.Corretivo", new[] { "IDAreaServico" });
            DropIndex("dbo.Produto", new[] { "IDUnidadeMedida" });
            DropIndex("dbo.Produto", new[] { "IDFornecedor" });
            DropIndex("dbo.ProdutoSimulador", new[] { "IDUsuarioALT" });
            DropIndex("dbo.ProdutoSimulador", new[] { "IDEstagioCultura" });
            DropIndex("dbo.ProdutoSimulador", new[] { "IDCultura" });
            DropIndex("dbo.ProdutoSimulador", new[] { "IDProduto" });
            DropIndex("dbo.ProdutoSimulador", new[] { "IDSimulacao" });
            DropIndex("dbo.Claims", new[] { "Usuario_Id" });
            DropIndex("dbo.UsuarioAtivo", new[] { "IDUsuario" });
            DropIndex("dbo.UsuarioAtivo", new[] { "IDEmpresa" });
            DropIndex("dbo.Proprietario", new[] { "IDEmpresa" });
            DropIndex("dbo.Problema", new[] { "IDArea" });
            DropIndex("dbo.SequenciaImportacao", new[] { "IDSafra" });
            DropIndex("dbo.ParametroPropriedade", new[] { "AreaServico_objID" });
            DropIndex("dbo.ParametroPropriedade", new[] { "IDPropriedade" });
            DropIndex("dbo.ParametroPropriedade", new[] { "IDSafra" });
            DropIndex("dbo.ParametroRecomendacao", new[] { "IDArea" });
            DropIndex("dbo.ParametroRecomendacao", new[] { "IDSafra" });
            DropIndex("dbo.Area", new[] { "IDPropriedade" });
            DropIndex("dbo.Propriedade", new[] { "IDRegiao" });
            DropIndex("dbo.Propriedade", new[] { "IDProprietario" });
            DropIndex("dbo.ProdutividadeVariedade", new[] { "IDUnidadeMedida" });
            DropIndex("dbo.ProdutividadeVariedade", new[] { "IDVariedadeCultura" });
            DropIndex("dbo.ProdutividadeVariedade", new[] { "IDRegiao" });
            DropIndex("dbo.VariedadeCultura", new[] { "IDCultura" });
            DropIndex("dbo.CicloProducao", new[] { "IDCulturaAnterior" });
            DropIndex("dbo.CicloProducao", new[] { "IDVariedadeCultura" });
            DropIndex("dbo.CicloProducao", new[] { "IDCultura" });
            DropIndex("dbo.CicloProducao", new[] { "IDAreaServico" });
            DropIndex("dbo.Fertilizante", new[] { "IDEstagioCultura" });
            DropIndex("dbo.Fertilizante", new[] { "IDFornecedor" });
            DropIndex("dbo.Fertilizante", new[] { "IDCicloProducao" });
            DropIndex("dbo.PartePlanta", new[] { "IDCultura" });
            DropIndex("dbo.FaixaTeor", new[] { "IDPartePlanta" });
            DropIndex("dbo.FaixaTeor", new[] { "IDEstagioCultura" });
            DropIndex("dbo.EstagioCultura", new[] { "IDCultura" });
            DropIndex("dbo.AmostraFoliar", new[] { "IDPartePlanta" });
            DropIndex("dbo.AmostraFoliar", new[] { "IDEstagioCultura" });
            DropIndex("dbo.AmostraFoliar", new[] { "IDAreaServico" });
            DropIndex("dbo.AreaServico", new[] { "IDProprietarioFatura" });
            DropIndex("dbo.AreaServico", new[] { "IDCultura" });
            DropIndex("dbo.AreaServico", new[] { "IDServico" });
            DropIndex("dbo.AreaServico", new[] { "IDSafra" });
            DropIndex("dbo.AreaServico", new[] { "IDArea" });
            DropIndex("dbo.Cultura", new[] { "IDUnidadeMedida" });
            DropIndex("dbo.Amostra", new[] { "IDCultura" });
            DropIndex("dbo.FormulacaoAdubo", new[] { "IDAdubo" });
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.TipoAmostra");
            DropTable("dbo.SaveTemporaryImgByte");
            DropTable("dbo.UnidadeDeLaboratorio");
            DropTable("dbo.Laboratorio");
            DropTable("dbo.ImagemSatelite");
            DropTable("dbo.Resteva");
            DropTable("dbo.RecomendacaoFoliar");
            DropTable("dbo.NivelSolo");
            DropTable("dbo.ExtracaoCultura");
            DropTable("dbo.Servico");
            DropTable("dbo.ParametroArea");
            DropTable("dbo.Imagem");
            DropTable("dbo.TeorSolo");
            DropTable("dbo.TeorFoliar");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Simulacao");
            DropTable("dbo.UsoProduto");
            DropTable("dbo.UnidadeMedida");
            DropTable("dbo.ProprietarioFornecedor");
            DropTable("dbo.TipoSolo");
            DropTable("dbo.AnaliseSolo");
            DropTable("dbo.Grid");
            DropTable("dbo.Corretivo");
            DropTable("dbo.Fornecedor");
            DropTable("dbo.Produto");
            DropTable("dbo.ProdutoSimulador");
            DropTable("dbo.Claims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UsuarioAtivo");
            DropTable("dbo.Empresa");
            DropTable("dbo.Proprietario");
            DropTable("dbo.Problema");
            DropTable("dbo.SequenciaImportacao");
            DropTable("dbo.ParametroPropriedade");
            DropTable("dbo.Safra");
            DropTable("dbo.ParametroRecomendacao");
            DropTable("dbo.Area");
            DropTable("dbo.Propriedade");
            DropTable("dbo.Regiao");
            DropTable("dbo.ProdutividadeVariedade");
            DropTable("dbo.VariedadeCultura");
            DropTable("dbo.CicloProducao");
            DropTable("dbo.Fertilizante");
            DropTable("dbo.PartePlanta");
            DropTable("dbo.FaixaTeor");
            DropTable("dbo.EstagioCultura");
            DropTable("dbo.AmostraFoliar");
            DropTable("dbo.AreaServico");
            DropTable("dbo.Cultura");
            DropTable("dbo.Amostra");
            DropTable("dbo.FormulacaoAdubo");
            DropTable("dbo.Adubo");
        }
    }
}
