using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigma.Domain.ViewTables;

namespace Sigma.Infra.Data.Repositories
{
    public class CulturaRepository : RepositoryBase<Cultura>, ICulturaRepository
    {
        public IEnumerable<Cultura> ByName(string name)
        {
            return Context.Set<Cultura>().Where(o => o.nome.Contains(name)).ToList();
        }

        public CulturaView FindCultura(Guid objID)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT *, UnidadeMedida.nome as UnidadeMedida FROM Cultura ");
            query.AppendLine("JOIN UnidadeMedida ON UnidadeMedida.objID = Cultura.IDUnidadeMedida ");
            query.AppendLine("WHERE Cultura.objID = '" + objID + "'");
            return Context.Database.SqlQuery<CulturaView>(query.ToString()).SingleOrDefault();
        }

        public IEnumerable<CulturaView> GetAllCultura()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT *, UnidadeMedida.nome as UnidadeMedida FROM Cultura ");
            query.AppendLine("JOIN UnidadeMedida ON UnidadeMedida.objID = Cultura.IDUnidadeMedida ");
            query.AppendLine("ORDER BY Cultura.nome ");
            return Context.Database.SqlQuery<CulturaView>(query.ToString());
        }
    }
}