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
    public class FaixaTeorRepository : RepositoryBase<FaixaTeor>, IFaixaTeorRepository
    {
        public IEnumerable<FaixaTeor> GetBy(string nutriente, Guid ec, Guid pp)
        {
            return Context.Set<FaixaTeor>().Where(o => o.nutriente.Equals(nutriente) && o.IDEstagioCultura.Equals(ec) && o.IDPartePlanta.Equals(pp)).OrderBy(o => o.nutriente).AsEnumerable();
        }

        public IEnumerable<FaixaTeorView> GetByPartePlanta(Guid iDPartePlanta)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT EstagioCultura.descricao as estagioCultura , *FROM FaixaTeor ");
            query.AppendLine("INNER JOIN EstagioCultura ON EstagioCultura.objID = FaixaTeor.IDEstagioCultura ");
            query.AppendLine("WHERE IDPartePlanta = '" + iDPartePlanta + "'");
            List<FaixaTeorView> lst = Context.Database.SqlQuery<FaixaTeorView>(query.ToString()).ToList();
            return lst;
        }
    }
}