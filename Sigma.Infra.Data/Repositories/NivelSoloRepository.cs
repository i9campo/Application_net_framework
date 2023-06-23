using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class NivelSoloRepository : RepositoryBase<NivelSolo>, INivelSoloRepository
    {
        public NivelSolo GetBy(string elemento, Guid c)
        {
            return Context.Set<NivelSolo>().Where(o => o.IDCultura.Equals(c) && o.elemento.Equals(elemento)).FirstOrDefault();
        }

        public IEnumerable<NivelSolo> GetNivelByCultura(Guid IDCultura)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM NivelSolo WHERE IDCultura = '" + IDCultura + "' ORDER BY elemento");
            return Context.Database.SqlQuery<NivelSolo>(query.ToString());
        }

        public IEnumerable<NivelSolo> GetNivelByElemento(Guid IDCultura, string elemento)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM NivelSolo WHERE IDCultura = '" + IDCultura + "' and elemento = '" + elemento + "' ORDER BY elemento");
            return Context.Database.SqlQuery<NivelSolo>(query.ToString());
        }
    }
}