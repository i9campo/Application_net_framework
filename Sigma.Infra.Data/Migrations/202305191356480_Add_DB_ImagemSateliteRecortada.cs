namespace Sigma.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_DB_ImagemSateliteRecortada : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImagemSateliteRecortada",
                c => new
                    {
                        objID = c.Guid(nullable: false),
                        orbita = c.String(),
                        banda = c.String(nullable: false, maxLength: 10),
                        dateIMG = c.String(nullable: false, maxLength: 11),
                        satelite = c.String(nullable: false, maxLength: 10),
                        extension = c.String(),
                        polyIMG = c.Geography(),
                        visualizar = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.objID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImagemSateliteRecortada");
        }
    }
}
