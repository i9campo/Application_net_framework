using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Service
{
    public interface ICicloProducaoService : IService<CicloProducao>
    {
        /// <summary> Método utilizado para carregar os dados do ciclo de produção a partir do ID da área serviço. </summary>
        /// <param name="objID"> ID referente ao ID da Área Serviço. </param>
        /// <param name="Tipo"> Tipo define o retorno da lista sé é por Ciclo de produção ou por Ciclo intermediário.</param>
        /// <returns></returns>
        IEnumerable<CicloViewer> GetAllCicloByAreaServico(Guid objID, string Tipo);


        /// <param name="objID"></param>
        /// <param name="tipoCiclo">tipoCiclo 0: Ciclo de Producao. tipoCiclo 1: Ciclo Intermediário.</param>
        /// <param name="retorno">retorno 0: Retorna o ciclo de produção  a partir do objID do CICLO, retorno 1 Retorna o ciclo de produção a partir do objID da área serviço.</param>
        /// <returns></returns>
        IEnumerable<CicloProducaoView> GetCiclo(Guid objID, int tipoCiclo, int retorno);

        /// <param name="objID"></param>
        /// <param name="tipoCiclo">tipoCiclo 0: Ciclo de Producao. tipoCiclo 1: Ciclo Intermediário.</param>
        /// <param name="retorno">retorno 0: Retorna o ciclo de produção  a partir do objID do CICLO, retorno 1 Retorna o ciclo de produção a partir do objID da área serviço.</param>
        /// <returns>Retorna uma lista de ciclo produção contendo um pedaço da área que não contém ciclo. </returns>
        IEnumerable<CicloProducaoView> GetCicloAndAreaServico(Guid objID, int tipoCiclo, int retorno);

        IEnumerable<AnaliseSolo> GetAnalises(Guid IDCicloProducao, string profundidade);

        /// <summary>
        /// <para>Retorna uma lista de Ciclo de produção a partir da área serviço. </para>
        /// <para>com uma condição, deve declarar o tipo "Type": CI ou CP</para>
        /// <para>CP Ciclo de produção ou CI Ciclo Intermediario.</para>
        /// </summary>
        /// <param name="IDAreaServico"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        IEnumerable<CicloProducaoView> GetAllByAreaServico(Guid IDAreaServico, string Type);

        /// <param name="IDSafra">Recebe o ID da safra. </param>
        /// <param name="IDPropriedade">Recebe o ID da Propriedade. </param>
        /// <param name="Type">Recebe o tipo de retorno. CI Retorna ciclo intermediário, CP Retorna ciclo de produção. </param>
        IEnumerable<CicloProducaoView> GetCicloByPropriedadeSafra(Guid IDSafra, Guid IDPropriedade, string Type);

   
    }
}
