using SharpDX.Direct3D9;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.ViewTables;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class AnaliseSoloRepository : RepositoryBase<AnaliseSolo>, IAnaliseSoloRepository
    {
        public AnaliseSoloView FindAnalise(Guid objID)
        {
            return Context.Database.SqlQuery<AnaliseSoloView>("SELECT * FROM fGetAnaliseSolo('" + objID + "', 0 )").FirstOrDefault();
        }

        public IEnumerable<AnaliseSoloViewer> GetListByAreaServico(Guid IDAreaServico, bool retorno)
        {
            return Context.Database.SqlQuery<AnaliseSoloViewer>("EXEC find_analises '" + IDAreaServico + "', '" + retorno + "'").ToList();
        }

        public AnaliseSoloView FindObject(string area, string grid)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT                        ");    
            query.AppendLine("  A.objID as IDAreaServico ,  ");
            query.AppendLine("  G.objID as IDGrid           ");
            query.AppendLine("FROM AreaServico A            ");
            query.AppendLine("INNER JOIN Grid G ON G.IDAreaServico = A.objID   ");
            query.AppendLine("WHERE A.codigo = "+area+" and G.codigo = "+grid+"");
            return Context.Database.SqlQuery<AnaliseSoloView>(query.ToString()).SingleOrDefault();
        }
        public IEnumerable<AnaliseSolo> FindAnaliseAreaGrid(Guid? IDArea, Guid? IDGrid)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM AnaliseSolo ");
            query.AppendLine("WHERE  IDAreaServico = '" + IDArea + "' and IDGrid = '" + IDGrid + "' and subAmostra = 0");
            return Context.Database.SqlQuery<AnaliseSolo>(query.ToString()).ToList();
        }

        public IEnumerable<AnaliseSoloView> GetAnaliseByPropriedade(Guid IDPropriedade)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("declare @result table (             ");
            query.AppendLine("    objid	uniqueidentifier        ,");
            query.AppendLine("    idarea  uniqueidentifier      ,");
            query.AppendLine("    idareaservico uniqueidentifier,");
            query.AppendLine("    nome nvarchar(max)             ");
            query.AppendLine(")                                  ");

            query.AppendLine(" insert into @result (objid, idarea, idareaservico, nome) ");

            query.AppendLine(" select                                                   ");
            query.AppendLine("      ans.objid				as objid        ,           ");
            query.AppendLine("      ar.objid				as idarea       ,           ");
            query.AppendLine("      ans.idareaservico		as idareaservico,           ");
            query.AppendLine("      substring(replace(descricao, cast(convert(int, ponto) as varchar)+' ' ,' ' ), patindex('%[^0]%',descricao), 100)  as nome ");
            query.AppendLine(" from analisesolo ans                                     ");

            query.AppendLine(" inner join areaservico ars on ars.objid = ans.idareaservico  ");
            query.AppendLine(" inner join area ar on ar.objid = ars.idarea                  ");
            query.AppendLine(" inner join propriedade prop on prop.objid = ar.idpropriedade ");
            query.AppendLine(" where prop.objid = '" + IDPropriedade + "' and sequenciasuba = '' and ans.geo is not null ");


            query.AppendLine(" select * from @result ");
            query.AppendLine(" group by objid, idarea, idareaservico, nome ");
            query.AppendLine(" order by nome");

            //query.AppendLine("SELECT objID, IDArea, IDAreaServico, 'PC ' + nome + ' PONTOS DE COLETA' AS nome FROM fGetListPontAnaliseSolo('" + IDPropriedade + "') GROUP BY objID, IDArea, IDAreaServico, nome ORDER BY nome");
            return Context.Database.SqlQuery<AnaliseSoloView>(query.ToString()).ToList();
        }

        public IEnumerable<MediaAnalise> GetMediaAnaliseSolo(Guid IDAreaServico, Guid? IDGrid, int Perfil, int Und, int Tipo, int RetornoP)
        {
            StringBuilder query = new StringBuilder();
            if (IDGrid == null)
                query.AppendLine("SELECT * FROM fMediaAnalises('" + IDAreaServico + "','' ," + Perfil + ", " + Und + ", " + Tipo + ", " + RetornoP + ")");
            else
                query.AppendLine("SELECT * FROM fMediaAnalises('" + IDAreaServico + "','"+ IDGrid +"'," + Perfil + ", " + Und + ", " + Tipo + ", " + RetornoP + ")");

            return Context.Database.SqlQuery<MediaAnalise>(query.ToString()).ToList();
        }

 
    }
}
