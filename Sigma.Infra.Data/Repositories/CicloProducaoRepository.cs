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
    public class CicloProducaoRepository : RepositoryBase<CicloProducao>, ICicloProducaoRepository
    {
        public IEnumerable<CicloProducaoView> GetCiclo(Guid objID, int tipoCiclo, int retorno)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM fGetCicloProducao('"+objID+"' ,"+ tipoCiclo +" , "+ retorno +")");
            return Context.Database.SqlQuery<CicloProducaoView>(query.ToString()).ToList();
        }

        public AnaliseSoloView GetAdicao_F(Guid IDCicloProducao, Guid IDCicloIntermerdiario)
        {
            StringBuilder query = new StringBuilder();
            // Essa Query primerio recebe o ID do ciclo de produção depois o ID do ciclo intermediario. 
            query.AppendLine("SELECT * FROM fGetAdicaoFertilizateByCiclo('2566C7EE-12FD-4A7E-878A-34BA0ADC2B53', '" + IDCicloIntermerdiario + "')");
            return Context.Database.SqlQuery<AnaliseSoloView>(query.ToString()).FirstOrDefault();
        }

        public AnaliseSoloView GetExportacaoCiclo(Guid objID)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM fGetExportacaoCiclo('" + objID + "')");
            return Context.Database.SqlQuery<AnaliseSoloView>(query.ToString()).FirstOrDefault();
        }

        public IEnumerable<CicloProducaoView> GetAllByAreaServico(Guid IDAreaServico, string Type)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT CP.*, dbo.GetGeoJSON(CP.geo.MakeValid()) AS geoJson, UM.nome AS UnidadeMedida, C.nome AS cultura  FROM CicloProducao CP   ");
            query.AppendLine("INNER JOIN Cultura C ON C.objID = CP.IDCultura                                    ");
            query.AppendLine("INNER JOIN UnidadeMedida UM ON UM.objID = C.IDUnidadeMedida                       ");
            query.AppendLine("WHERE CP.tipo = '" + Type + "' AND CP.IDAreaServico = '" + IDAreaServico + "'     "); 
            return Context.Database.SqlQuery<CicloProducaoView>(query.ToString()).ToList();
        }

        /// <summary>
        /// Profundidade = Empty : Retorna todas
        /// Profundidade = 00 - 20
        /// Profundidade = 20 - 40
        /// </summary>
        /// <param name="IDCicloProducao"></param>
        /// <param name="profundidade"></param>
        /// <returns></returns>
        public IEnumerable<AnaliseSolo> GetAnalises(Guid IDCicloProducao, string profundidade)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT ans.* FROM AnaliseSolo ans");
            query.AppendLine("INNER JOIN CicloProducao cp ON cp.IDAreaServico = ans.IDAreaServico");
            query.AppendLine("WHERE cp.objID = '" + IDCicloProducao + "'");
            if (!String.IsNullOrEmpty(profundidade))
                query.AppendLine("AND ans.profundidade = '" + profundidade + "'");
            query.AppendLine("AND (cp.geo.STContains(ans.geo) = 1)");
            return Context.Database.SqlQuery<AnaliseSolo>(query.ToString()).ToList();
        }

        public IEnumerable<CicloProducaoView> GetCicloByPropriedadeSafra(Guid IDSafra, Guid IDPropriedade, string Type)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(" SELECT CP.*, Cultura.nome AS cultura, UnidadeMedida.nome AS UnidadeMedida FROM CicloProducao CP                  ");
            query.AppendLine(" LEFT JOIN Cultura ON Cultura.objID = CP.IDCultura                                                                ");
            query.AppendLine(" LEFT JOIN UnidadeMedida ON  UnidadeMedida.objID = Cultura.IDUnidadeMedida                                        "); 

            query.AppendLine(" INNER JOIN AreaServico ON AreaServico.objID = CP.IDAreaServico                                                   ");
            query.AppendLine(" INNER JOIN Safra ON Safra.objID = AreaServico.IDSafra                                                            ");
            query.AppendLine(" INNER JOIN Area ON Area.objID = AreaServico.IDArea                                                               ");
            query.AppendLine(" WHERE Area.IDPropriedade = '" + IDPropriedade + "' AND Safra.objID = '" + IDSafra + "' AND tipo = '" + Type + "' ");
            return Context.Database.SqlQuery<CicloProducaoView>(query.ToString()).ToList();
        }

        public IEnumerable<CicloProducaoView> GetCicloAndAreaServico(Guid IDCiclo, string GeoString)
        {
            return null; 
        }

        public IEnumerable<CicloViewer> GetAllCicloByAreaServico(Guid objID, string Tipo)
        {
            return Context.Database.SqlQuery<CicloViewer>("SELECT * FROM fGetListCicloProducaoByAreaServico('"+objID+"','"+Tipo+"')").ToList();
        }
    }
}