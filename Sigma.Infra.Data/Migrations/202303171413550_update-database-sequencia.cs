namespace Sigma.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabasesequencia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SequenciaImportacao", "IDLaboratorio", c => c.Guid());
            CreateIndex("dbo.SequenciaImportacao", "IDLaboratorio");
            AddForeignKey("dbo.SequenciaImportacao", "IDLaboratorio", "dbo.Laboratorio", "objID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SequenciaImportacao", "IDLaboratorio", "dbo.Laboratorio");
            DropIndex("dbo.SequenciaImportacao", new[] { "IDLaboratorio" });
            DropColumn("dbo.SequenciaImportacao", "IDLaboratorio");
        }
    }
}
