using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace Sigma.App.Interfaces
{
    public interface IAreaServicoAppService : IAppService<AreaServico>
    {
        /// <summary> Method using for get area serviço by ID.  </summary>
        AreaServicoViewer FindAreaServico(Guid objID);


        /// <returns> Method using for update geo in area servico. </returns>
        bool UpdateGeo(Guid objID, string geoString, string jsonField, float tamanho); 


        /// <summary>
        /// <para>Retorna a área serviço contendo informações adicionais. </para>
        /// <para>Como: Safra, Área, Propriedade, Proprietário e Serviço. </para>
        /// <para>Dados exibido na consulta: ID da propriedade, Área Serviço, Descrição da Safra, Nome do Proprietário, ID do Proprietário. </para>
        /// <para>Parâmetros utilizados podem ser nulos, porém quando o objID ID é null, significa que a consulta será realizada pela área safra ou por área ou safra. </para>
        /// <para>Já o parâmetro returngeo será utilizado para que possa retornar geo ou não, true retorna geo, false não retorna geo. </para>
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="returngeo"></param>
        /// <returns></returns>
        AreaServicoView FindFullAreaServico(Guid? objID, Guid? IDArea, Guid? IDSafra, bool? returngeo);

        /// <param name="IDSafra"></param>
        /// <param name="IDProprietario"></param>
        /// <param name="IDPropriedade"></param>
        /// <param name="IDArea"></param>
        /// <returns>Retorna os dados para preencher o filtro da área serviço e zoneamento. </returns>
        AreaServicoView FindFilter(Guid? IDSafra, Guid? IDProprietario, Guid? IDPropriedade, Guid? IDArea);

  


        /// <summary>
        /// <para>Retorna uma lista de área serviço de forma dinâmica. </para>
        /// <para>O parâmetro returngeo, ele retorna uma lista de área serviço que contém geo se for true, agora se for false retorna uma lista com ou sem geo. </para>
        /// <para>O parâmetro returnquery, ele retornar uma lista de área serviço contendo informações sobre os grids que contém e não contém geo. </para>
        /// </summary>
        /// <param name="IDSafra"></param>
        /// <param name="IDArea"></param>
        /// <param name="IDPropriedade"></param>
        /// <param name="returngeo"></param>
        /// <returns></returns>
        IEnumerable<AreaServicoView> GetAreaServico(Guid? IDAreaServico, Guid IDSafra, Guid? IDArea, Guid? IDPropriedade, Guid? IDServico, bool? returngeo, int returnquery);

        /// <summary>
        /// <para>Método utilizado para verificar se existe o código de importação da análise na área serviço. </para>
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        ImportItensLabView ExistAnaliseByCodigo(int Codigo);


        /// <summary>
        /// <para>Retorna o último registro de nº de serviço. </para>
        /// </summary>
        /// <param name="IDSafra"></param>
        /// <param name="IDArea"></param>
        /// <returns></returns>

        /// <summary>
        /// <para>Método utilizado para carregar o geo em DbGeography. </para>
        /// </summary>
        /// <param name="geo"></param>
        /// <param name="IDAreaServico"></param>
        /// <param name="GridOrAreaServico"></param>
        /// <returns></returns>
        DbGeography GetGeoAreaOrGrid(string geo, Guid? IDAreaServico, bool GridOrAreaServico);
        IEnumerable<AreaServicoView> GetMaxServicoRegister(Guid iDSafra, Guid iDArea);
        bool DeleteAllAreaServico(String objID);
    }
}
