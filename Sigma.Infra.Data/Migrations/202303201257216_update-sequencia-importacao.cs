namespace Sigma.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesequenciaimportacao : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SequenciaImportacao", new[] { "IDLaboratorio" });
            AlterColumn("dbo.SequenciaImportacao", "IDLaboratorio", c => c.Guid(nullable: false));
            CreateIndex("dbo.SequenciaImportacao", "IDLaboratorio");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SequenciaImportacao", new[] { "IDLaboratorio" });
            AlterColumn("dbo.SequenciaImportacao", "IDLaboratorio", c => c.Guid());
            CreateIndex("dbo.SequenciaImportacao", "IDLaboratorio");
        }
    }
}
