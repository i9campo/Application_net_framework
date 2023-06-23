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
    public class UnidadeDeLaboratorioRepository : RepositoryBase<UnidadeDeLaboratorio>, IUnidadeDeLaboratorioRepository
    {
        public UnidadeDeLaboratorioView FindDesc(Guid objID)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT undlb.objID, undlb.IDLaboratorio, lb.nome as laboratorio, undlb.b, undlb.p, undlb.ca,");
            query.AppendLine("undlb.co, undlb.cu, undlb.fe, undlb.k, undlb.mg, undlb.mn, undlb.mo, undlb.n,");
            query.AppendLine("undlb.s, undlb.zn FROM UnidadeDeLaboratorio undlb ");
            query.AppendLine("INNER JOIN Laboratorio lb ON lb.objID = undlb.IDLaboratorio ");
            query.AppendLine("WHERE undlb.objID = '" + objID + "'");
            return Context.Database.SqlQuery<UnidadeDeLaboratorioView>(query.ToString()).SingleOrDefault();
        }

        public IEnumerable<UnidadeDeLaboratorioView> GetAllDesc()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT undlb.objID, undlb.IDLaboratorio, lb.nome as laboratorio, undlb.b, undlb.p, undlb.ca,");
            query.AppendLine("undlb.co, undlb.cu, undlb.fe, undlb.k, undlb.mg, undlb.mn, undlb.mo, undlb.n,");
            query.AppendLine("undlb.s, undlb.zn FROM UnidadeDeLaboratorio undlb ");
            query.AppendLine("INNER JOIN Laboratorio lb ON lb.objID = undlb.IDLaboratorio ");
            return Context.Database.SqlQuery<UnidadeDeLaboratorioView>(query.ToString());
        }
    }
}
