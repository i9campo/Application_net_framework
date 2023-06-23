using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.ViewTables;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class ArquivoAreaRepository : RepositoryBase<ArquivoAreaView>, IArquivoAreaRepository
    {
        public IEnumerable<ArquivoAreaView> GetListArquivoAreaByAreaServico(Guid IDAreaServico)
        {
            return Context.Database.SqlQuery<ArquivoAreaView>("SELECT * FROM fGetListArquivoAreaByAreaServico('" + IDAreaServico + "')").ToList(); 
        }

        public IEnumerable<ArquivoAreaView> GetListArquivoAreaBySafraArea(Guid IDSafra, Guid IDArea)
        {
            return Context.Database.SqlQuery<ArquivoAreaView>("SELECT * FROM fGetListArquivoAreaBySafraArea('" + IDSafra + "','" + IDArea + "')").ToList();
        }

        public IEnumerable<ArquivoAreaView> GetListArquivoAreaBySafraPropriedade(Guid IDArea, Guid IDSafra)
        {
            return Context.Database.SqlQuery<ArquivoAreaView>("SELECT * FROM fGetListArquivoAreaBySafraPropriedade('" + IDArea + "','"+ IDSafra +"')").ToList();
        }
    }
}
