using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using Sigma.Domain.ViewTables;
using System;

namespace Sigma.Domain.Interfaces.Service
{
    public interface IParametroAreaService : IService<ParametroArea>
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
