using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IAreaAppService : IAppService<Area>
    {
        /// <summary>
        /// <para>Retorna uma lista de área. </para>
        /// </summary>
        /// <returns></returns>
        IEnumerable<Area> GetAllArea();

        /// <summary> 
        /// Este método será utilizado para carregar a lista da área a partir do ID da safra e o ID da propriedade.  
        /// Com o objetivo de verificar a existência do GRID. 
        /// </summary>
        /// <param name="IDSafra"></param>
        /// <param name="IDPropriedade"></param>
        /// <returns>Retorna uma lista de dados da área que contém GRID.</returns>
        IEnumerable<AreaGrid> GetAllAreaExistedGrid(Guid IDSafra, Guid IDPropriedade);



        /// <summary>
        /// <para>Retorna um objeto área contendo informações adicionais.</para>
        /// <para>Como: Tipo área/ Propriedade</para>
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        AreaView GetFullArea(Guid objID);

        /// <summary>
        /// <para>Retorna todas as áreas atráves do ID de uma propriedade.</para>
        /// </summary>
        /// <param name="IDPropriedade"></param>
        /// <returns></returns>
        IEnumerable<Area> GetByPropriedade(Guid IDPropriedade);

        /// <summary>
        /// <para>Retorna todas as áreas da propriedade que possui algum serviço na safra</para>
        /// </summary>
        /// <param name="IDPropriedade"></param>
        /// <param name="IDSafra"></param>
        /// <returns></returns>
        IEnumerable<Area> GetByPropriedadeSafra(Guid IDPropriedade, Guid IDSafra);
        IEnumerable<AreaView> VerifyAreaServico(Guid iDPropriedade);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDPropriedade"></param>
        /// <returns>Retorna uma lista de área contendo dados geográficos a partir do ID da Propriedade. </returns>
        IEnumerable<AreaView> FindFullAreaByPropriedade(Guid IDPropriedade); 


        IEnumerable<AreaPropriedadeView> GetAreaPropriedade(Guid area);

        IEnumerable<BNGAreaView> GetAreaBNGByPropriedade(Guid IDSafra, Guid IDPropriedade);
    }
}
