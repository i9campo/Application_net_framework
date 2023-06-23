using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Service
{
    public interface IAnaliseSoloService : IService<AnaliseSolo>
    {
        /// <param name="objID">Este ID é referente ao objID da ánalise solo</param>
        /// <param name="getType"></param>
        /// <returns>Retorna um objeto do tipo Análise solo. </returns>
        AnaliseSoloView FindAnalise(Guid objID);

        /// <summary> Procedure used to load soil analysis data from the service area ID and return type, whether 0 or 1. </summary>
        /// <param name =   "IDAreaServico" >Service Area ID</param>
        /// <param name =   "retorno"   >True  ('1'),  Returns analysis list without sub samples. / False ('0'),  Returns analysis list with sub samples.</param>
        /// <returns>Return list soil analysis. </returns>
        IEnumerable<AnaliseSoloViewer> GetListByAreaServico(Guid IDAreaServico, bool retorno);

        AnaliseSoloView FindObject(string area, string grid);

        IEnumerable<AnaliseSolo> FindAnaliseAreaGrid(Guid? iDArea, Guid? iDGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDAreaServico">ID da Área Serviço. </param>
        /// <param name="IDGrid">ID do Grid a retornar, utilizar o Tipo 1</param>
        /// <param name="Perfil">"0" para profundidade de 00-20 e "1" para profundidade de 20-40</param>
        /// <param name="Und">"0" Análise padrão E "1" em Kg/Ha)</param>
        /// <param name="Tipo">
        ///                     "0" Média por área com IDGrid nulo
        ///                     "1" Média por ZM informando o IDGrid
        ///                     "2" Média por área com IDGrid nulo ou não
        ///                     "3" Média da área ponderada por ZM
        ///                     "4" Lista de média por ZM
        /// </param>
        /// <param name="RetornoP">
        ///                     "0" para retornar a média de PMehl e PRes
        ///                     "1" para retornar o valor de P do tipo marcado no parâmetro da área
        /// </param>
        /// 
        /// <returns>Retorna a média da análise de solo. </returns>
        IEnumerable<MediaAnalise> GetMediaAnaliseSolo(Guid IDAreaServico, Guid? IDGrid, int Perfil, int Und, int Tipo, int RetornoP);

        /// <summary>
        /// <para>Retorna analises de solo ponto de coleta a partir de uma propriedade.</para>
        /// </summary>
        /// <param name="IDPropriedade"></param>
        /// <returns></returns>
        IEnumerable<AnaliseSoloView> GetAnaliseByPropriedade(Guid IDPropriedade);
    }
}
