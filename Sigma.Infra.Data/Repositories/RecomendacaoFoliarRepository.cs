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
    public class RecomendacaoFoliarRepository : RepositoryBase<RecomendacaoFoliar>, IRecomendacaoFoliarRepository
    {
        public IEnumerable<RecomendacaoFoliar> GetByCultura(Guid IDCultura)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM RecomendacaoFoliar WHERE IDCultura = '" + IDCultura + "' ORDER BY elemento");
            return Context.Database.SqlQuery<RecomendacaoFoliar>(query.ToString());
        }

        public IEnumerable<RecomendacaoFoliarView> GetElementoRFNS(Guid objID, Guid IDCultura)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT NS.objID as IDNivelSolo, RF.objID as IDRecomendacao, RF.IDCultura, RF.excecao, RF.elemento, RF.descritivo, RF.codigo, NS.toxico, ");
            query.AppendLine("NS.deficiente, NS.maximo, NS.minimo, NS.b, NS.ca, NS.co, NS.cu,  NS.fe, NS.k,  NS.mg,  NS.mn, NS.mo, NS.n, NS.p, NS.s, NS.zn FROM RecomendacaoFoliar RF ");
            query.AppendLine("LEFT JOIN NivelSolo NS ON NS.elemento = RF.elemento ");
            query.AppendLine("WHERE RF.objID = '" + objID + "' AND NS.IDCultura = '" + IDCultura + "'");
            return Context.Database.SqlQuery<RecomendacaoFoliarView>(query.ToString());
        }

        public RecomendacaoFoliar LoadBy(string codigo, string elemento, bool excecao, Guid Cultura)
        {
            return Context.Set<RecomendacaoFoliar>().Where(o => o.codigo.Equals(codigo) && o.elemento.Equals(elemento) && o.excecao == excecao && o.IDCultura.Equals(Cultura)).FirstOrDefault();
        }
    }
}