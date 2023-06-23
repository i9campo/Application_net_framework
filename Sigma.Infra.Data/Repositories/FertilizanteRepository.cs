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
    public class FertilizanteRepository : RepositoryBase<Fertilizante>, IFertilizanteRepository
    {
        public IEnumerable<FertilizanteView> GetByCP(Guid IDCiclo)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT f.* , ec.dapPrecoce as dias, um.nome as unidadeMedida, ec.codigo as estagio FROM Fertilizante f ");
            query.AppendLine("INNER JOIN EstagioCultura ec ON ec.objID = f.IDEstagioCultura ");
            query.AppendLine("INNER JOIN CicloProducao cp ON cp.objID = f.IDCicloProducao ");
            query.AppendLine("INNER JOIN Cultura c ON c.objID = cp.IDCultura ");
            query.AppendLine("INNER JOIN UnidadeMedida um ON um.objID = c.IDUnidadeMedida ");
            query.AppendLine("WHERE (f.IDCicloProducao ='" + IDCiclo + "') ORDER BY ec.dapPrecoce asc");
            return Context.Database.SqlQuery<FertilizanteView>(query.ToString()).ToList();
        }

        public IEnumerable<FertilizanteView> GetByIDCultura(Guid IDCultura)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select* from EstagioCultura where IDCultura = '"+IDCultura+"'");
            return Context.Database.SqlQuery<FertilizanteView>(query.ToString()).ToList();
        }

        public IEnumerable<FertilizanteView> GetByOpcao(int opcao, Guid IDCiclo)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT f.* , ec.dapPrecoce as dias, um.nome as unidadeMedida, ec.codigo as estagio FROM Fertilizante f ");
            query.AppendLine("INNER JOIN EstagioCultura ec ON ec.objID = f.IDEstagioCultura ");
            query.AppendLine("INNER JOIN Cultura c ON c.objID = ec.IDCultura ");
            query.AppendLine("INNER JOIN UnidadeMedida um ON um.objID = c.IDUnidadeMedida ");
            query.AppendLine("WHERE (f.IDCicloProducao = '" + IDCiclo + "') AND(f.opcao = '" + opcao + "')  ORDER BY ec.dapPrecoce asc");
            return Context.Database.SqlQuery<FertilizanteView>(query.ToString()).ToList();
        }

        public int GetCountByCiclo(Guid IDCicloProducao)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT COUNT(DISTINCT(opcao))  FROM Fertilizante ");
            query.AppendLine("WHERE (IDCicloProducao='" + IDCicloProducao + "')");
            return Context.Database.SqlQuery<int>(query.ToString()).SingleOrDefault();
        }

        public FertilizanteView GetMediaCiclo(Guid IDCiclo, int opcao)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM fGetMediaCiclo('" + IDCiclo + "','" + opcao + "')");
            return Context.Database.SqlQuery<FertilizanteView>(query.ToString()).SingleOrDefault();
        }

        public IEnumerable<Options> GetOptionsByCP(Guid IDCiclo)
        {
            return Context.Database.SqlQuery<Options>("EXEC get_list_options_fertilizante_by_ciclo '" + IDCiclo + "'").ToList();
        }

        public bool UpdateOptionChecked(UpdateFertilizanteMarcado obj)
        {
            return Context.Database.SqlQuery<bool>("EXEC UpdateFertilizanteMarcado '" + obj.IDCicloProducao + "'," + obj.opcao + "," + (obj.chk == true ? 1 : 0) + "").FirstOrDefault();
        }
    }
}

