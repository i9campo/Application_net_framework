using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class SaveTemporaryImgByteRepository : RepositoryBase<SaveTemporaryImgByte>, ISaveTemporaryImgByteRepository
    {
        public IEnumerable<SaveTemporaryImgByte> GetListByConjuntID(Guid objID)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM SaveTemporaryImgByte WHERE ConjuntoByteID = '" + objID + "' ORDER BY idx");
            return Context.Database.SqlQuery<SaveTemporaryImgByte>(query.ToString()).ToList();
        }

        public void RemoveAllBytesByID(Guid IDFileByte)
        {
            string query = "DELETE SaveTemporaryImgByte WHERE ConjuntoByteID = '" + IDFileByte + "'";
            Context.Database.ExecuteSqlCommand(query); 
        }
    }

}
