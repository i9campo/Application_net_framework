using Sigma.App.Interfaces._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IArquivoAreaAppService : IAppService<ArquivoAreaView>
    {
        /// <summary> Este método será utilizado para carregar a lista de arquivo da área a partir do ID da Área Serviço. </summary>
        /// <returns> Lista de dados referente a área serviço ("Área Serviço", "Grid", "Analises")</returns>
        IEnumerable<ArquivoAreaView> GetListArquivoAreaByAreaServico(Guid IDAreaServico);


        /// <summary> Este método será utilizado para carrega a lista de arquivo da área a partir do ID da área e safra. </summary>
        /// <returns>Lista de dados referente a área e safra.</returns>
        IEnumerable<ArquivoAreaView> GetListArquivoAreaBySafraPropriedade(Guid IDSafra, Guid IDPropriedade);

        /// <summary> Este método será utilizado para carrega a lista de arquivo da área a partir do ID da área e safra. </summary>
        /// <returns>Lista de dados referente a área e safra.</returns>
        IEnumerable<ArquivoAreaView> GetListArquivoAreaBySafraArea(Guid IDSafra, Guid IDArea);
    }
}
