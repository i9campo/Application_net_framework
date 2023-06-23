using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IFertilizanteAppService : IAppService<Fertilizante>
    {
        /// <summary> 
        /// <para>Retorna uma lista de fertilizante a partir do ID do ciclo de produção.</para>
        /// <para>Aqui retorna uma lista detalhada contendo informação do estagio da cultura e os dias do ciclo.</para>
        /// </summary>
        /// <param name="IDCiclo"></param>
        /// <returns></returns>
        IEnumerable<FertilizanteView> GetByCP(Guid IDCiclo);

        /// <summary>  </summary>
        /// <param name="IDCiclo"></param>
        /// <returns></returns>
        IEnumerable<Options> GetOptionsByCP(Guid IDCiclo); 

        /// <summary> Método utilizado para alterar o estado da opção marcada de marcado para desmarcado ou virse e versa. </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool UpdateOptionChecked(UpdateFertilizanteMarcado obj);

        /// <summary>
        /// <para>Retorna a quantidade de opcao distintas de fertilizante.</para>
        /// <para>Através do ID do ciclo de produção.</para>
        /// </summary>
        /// <param name="IDCicloProducao"></param>
        /// <returns></returns>
        int GetCountByCiclo(Guid IDCicloProducao);

        /// <summary>
        /// <para>Retorna uma lista de fertilizante a partir do ID do ciclo de produação</para>
        /// <para>Também da opção selecionada. </para>
        /// </summary>
        /// <param name="opcao"></param>
        /// <param name="IDCiclo"></param>
        /// <returns></returns>
        IEnumerable<FertilizanteView> GetByOpcao(int opcao, Guid IDCiclo);

        /// <summary>
        /// <para>Este metodo retorna a media do ciclo para fertilizantes cadastrado.</para>
        /// <para>Através do IDciclo e a opcao selecionada.</para>
        /// </summary>
        /// <param name="IDCiclo"></param>
        /// <returns></returns>
        FertilizanteView GetMediaCiclo(Guid IDCiclo, int opcao);
        IEnumerable<FertilizanteView> GetByIDCultura(Guid iDCultura);
    }
}
