namespace Sigma.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefertilizante : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Fertilizante", "GridCiclo_objID", "dbo.GridCiclo");
            DropIndex("dbo.Fertilizante", new[] { "GridCiclo_objID" });
            AddColumn("dbo.Fertilizante", "GridCiclo_objID1", c => c.Guid());
            AlterColumn("dbo.Fertilizante", "GridCiclo_objID", c => c.Int(nullable: false));
            CreateIndex("dbo.Fertilizante", "GridCiclo_objID1");
            AddForeignKey("dbo.Fertilizante", "GridCiclo_objID1", "dbo.GridCiclo", "objID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fertilizante", "GridCiclo_objID1", "dbo.GridCiclo");
            DropIndex("dbo.Fertilizante", new[] { "GridCiclo_objID1" });
            AlterColumn("dbo.Fertilizante", "GridCiclo_objID", c => c.Guid());
            DropColumn("dbo.Fertilizante", "GridCiclo_objID1");
            CreateIndex("dbo.Fertilizante", "GridCiclo_objID");
            AddForeignKey("dbo.Fertilizante", "GridCiclo_objID", "dbo.GridCiclo", "objID");
        }
    }
}
