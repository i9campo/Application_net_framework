using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;

namespace Sigma.App.Interfaces
{
    public interface IParametroAreaAppService : IAppService<ParametroArea>
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
