namespace Sigma.Infra.Data.Migrations
{
    using Sigma.Domain.Entities;
    using System;
    using System.Data.Entity.Migrations;
    internal sealed class Configuration : DbMigrationsConfiguration<Sigma.Infra.Data.Context.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Sigma.Infra.Data.Context.DBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //You can use the DbSet<T>
            //.AddOrUpdate() helper extension method
            ////  to avoid creating duplicate seed data.
            Empresa sigma = new Empresa()
            {
                objID = Guid.Parse("256b44ae-25e7-456f-9786-1814a5118b5e"),
                nome = "Sigma Soluções Agronômicas",
                site = "http://www.siccerrado.com.br",
                fone = "(61) 3601-3070",
                dataCadastro = new DateTime(2004, 01, 01),
                ativo = true
            };
            context.Empresa.AddOrUpdate(sigma);
        }
    }
}
