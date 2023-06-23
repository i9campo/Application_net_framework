using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.ViewTables;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class ImagemSateliteRepository : RepositoryBase<ImagemSatelite>, IimagemSateliteRepository
    {

        public IEnumerable<ImagemSateliteView> GetListImg(string Coords)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select* from dbo.fbngGetImagemSatelite( '" + Coords + "')");
            return Context.Database.SqlQuery<ImagemSateliteView>(query.ToString()).ToList();
        }
        public ImagemSateliteView GetGeometry(string GeoCordinates)
        {
            //Double calcY = (100 * 0.000536126397285); 
            //Double Y1 = (14.9289894529083 - calcY);
            //Double Y2 = (16.8554365050633 + calcY);

            //////Original.
            //StringBuilder query = new StringBuilder();
            //query.AppendLine("declare @g geometry; ");
            //query.AppendLine("set @g =  geometry::STPolyFromText('polygon ((" +
            //    "-49.02628134490047 -" + Y1.ToString().Replace(",", ".") + ", " +
            //    "-46.87633018988909 -" + Y1.ToString().Replace(",", ".") + ", " +
            //    "-46.87633018988909 -" + Y2.ToString().Replace(",", ".") + ", " +
            //    "-49.02628134490047 -" + Y2.ToString().Replace(",", ".") + ", " +
            //    "-49.02628134490047 -" + Y1.ToString().Replace(",", ".") + " " +
            //    "))', 4326)");
            //query.AppendLine("select @g.MakeValid() as geometrico ");


            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @g geometry; ");
            query.AppendLine("SET @g =  geometry::STPolyFromText('" + GeoCordinates + "', 4326)");
            query.AppendLine("SELECT NEWID() as objID , @g.MakeValid() as geometrico ");

            return Context.Database.SqlQuery<ImagemSateliteView>(query.ToString()).SingleOrDefault();
        }

        public ImagemSateliteView GetImagem(Guid objID)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT objID, banda, satelite, visualizar, dbo.GetGeoJSON(polyIMG) as geoJson, orbita, dateIMG, extension FROM ImagemSatelite WHERE objID ='" + objID + "'");
            return Context.Database.SqlQuery<ImagemSateliteView>(query.ToString()).SingleOrDefault();
        }

        public string SerializeGeo(Guid objID)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT geo.ToString() FROM ImagemSatelite WHERE objID='" + objID + "'");
            return Context.Database.SqlQuery<string>(query.ToString()).SingleOrDefault();
        }
        public bool UpdateCoordsImg(string geoString, Guid objID)
        {
            
            StringBuilder query = new StringBuilder();
            query.AppendLine("declare @g GEOMETRY = GEOMETRY::STPolyFromText('POLYGON ((" + geoString + "))', 4326)");
            query.AppendLine("UPDATE ImagemSatelite");
            query.AppendLine("SET geo = @g");
            query.AppendLine("where objID = '" + objID + "'");
            
            Context.Database.ExecuteSqlCommand(query.ToString());

            return false;
        }
        public List<ImagemSatelite> GetListGeoIMGS(string coord)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @GEO GEOMETRY = GEOMETRY::STPolyFromText('" + coord + "',4326)");
            query.AppendLine("SELECT * FROM ImagemSatelite WHERE @GEO.STWithin(GEOMETRY::STPolyFromText(polyIMG.ToString(),4326)) = 1;"); 
            return Context.Database.SqlQuery<ImagemSatelite>(query.ToString()).ToList();
        }
    }
}
