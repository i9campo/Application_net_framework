using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IGridRepository : IRepository<Grid>
    {
        #region C.R.U.D
        /// <summary> Stored Procedure using add grid by list in DB. </summary>
        /// <returns> return bool value </returns>
        bool AddLstGrid(GridViewer obj);

        /// <summary> Stored Procedure using update grid. </summary>
        /// <returns> return bool value </returns>
        bool UpdateGrid(GridViewer obj);

        /// <summary> Stored Procedure using delete grid all cascade Análise solo and Corretivo.  </summary>
        /// <returns>Return bool value. </returns>
        bool DeleteGrid(String iDAreaServico);
        #endregion
        #region SEARCH
        /// <returns>Return Grid by objID.</returns>
        GridViewer FindGrid(Guid objID); 

        /// <returns>Return Grid by Código. </returns>
        Grid GetByCodigo(int Codigo);

        /// <summary> Stored Procedure get list grid by área serviço. </summary>
        /// <returns>Return list GRID.</returns>
        IEnumerable<GridViewer> GetByAreaServico(Guid IDAreaServico);
        #endregion

        /// <param name="IDAreaServico">ID Referente a Área Serviço. </param>
        /// <returns>Retorna uma lista de grid a partir do ID da área serviço.</returns>
        IEnumerable<Grid> GetAllGridByAreaServico(Guid IDAreaServico);


        /// <param name="objID">Este parâmetro é referente ao ID do Grid selecionado. </param>
        /// <param name="newValue">Este parâmetro é referente ao JSON string referente ao Grid selecionado. </param>
        /// <returns> Retorna um valor boolean a partir do momento que concluir a atualização. Caso dê certo retorna true, senão false.  </returns>
        bool UpdateFieldList(Guid objID, string newValue);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDAreaServico"></param>
        /// <returns></returns>
        IEnumerable<Grid> GetGridByAreaServico(Guid IDAreaServico);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="IDSafra"></param>
        /// <param name="IDAreaServico"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        IEnumerable<GridView> GetGrid(Guid objID, Guid IDSafra, Guid? IDAreaServico, int Type);

        AnaliseSoloView GetMediaAnaliseSoloGrid(Guid objID, string profundidade);

        /// <summary>
        /// <para>Método utilizado para verificar se existe o código de importação da análise no Grid </para>
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        ImportItensLabView ExistAnaliseByCodigo(int Codigo, Guid IDAreaServico);


        IEnumerable<GridView> GetAllGeoJson();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objID"></param>
        void RemoveGRID(Guid objID);
        IEnumerable<GridView> CorrecaoAcidez(Guid iDAreaServico);

        /// <summary>
        /// <para>Retorna uma lista de grid contendo informações sobre as médias de cada zonas.</para>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        IEnumerable<GridView> GetByAreaServicoFull(Guid IDAreaServico);
   

        /// <summary>
        /// <para>Método utilizado para retornar o grid a partir do geo marcado. </para>
        /// </summary>
        /// <param name="IDAreaServico"></param>
        /// <param name="geo"></param>
        /// <returns></returns>
        GridView GetByGeoAreaServico(Guid IDAreaServico, string geo, string servico);

        /// <summary> Este método será utilizado para carregar os dados geograficos em formato JSON e STRING </summary>
        /// <param name="geo"> string em formato SQL, que será utilizado como conversão. </param>
        /// <returns>Retorna um objeto do tipo JSON. </returns>
        GeoJsonSplitPoly ObjSplitPoly(string geo); 
    }
}
