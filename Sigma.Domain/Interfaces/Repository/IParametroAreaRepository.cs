using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IParametroAreaRepository : IRepository<ParametroArea>
    {
        ParametroAreaView GetAllParametroArea(Guid iDAreaServico, string safra, string area);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDAreaServico"></param>
        /// <returns>Retorna um objeto do tipo Parâmetro Área, a partir do objID da área serviço. </returns>
        ParametroAreaView GetParametroareaByAreaServico(Guid IDAreaServico);
    }
}
