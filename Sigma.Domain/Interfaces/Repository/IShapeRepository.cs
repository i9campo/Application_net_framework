using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using static Sigma.Domain.ViewTables.OpenGeo;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IShapeRepository : IRepository<Shape>
    {
        /// <returns>Retorna uma lista de dados do BNG referente a tabela. ("SHAPE") </returns>
        IEnumerable<BNG_Shape> GetListShapeByAreaServico(Guid IDSafra, Guid IDArea);

        /// <returns>Retorna uma lista de dados Geograficos a partir do ID do Shape. </returns>
        IEnumerable<FileExt> GetFileByIDShape(Guid IDShape, int orbita);

        /// <summary>
        /// <para>Retorna um GeoJson a partir de uma de uma string de coordenadas.</para>
        /// <para>Exemplo do site: https://docs.microsoft.com/pt-br/sql/t-sql/spatial-geography/stpolyfromtext-geography-data-type?view=sql-server-ver15</para>
        /// </summary>
        /// <param name="CoordString"></param>
        /// <returns></returns>
        string GetGeoJson(string CoordString);

        /// <summary>
        /// <para>Retorna um GeoJson a partir de uma de uma string de coordenadas.</para>
        /// <para>Exemplo do site: https://docs.microsoft.com/pt-br/sql/t-sql/spatial-geography/stpointfromtext-geography-data-type?view=sql-server-ver15
        /// </summary>
        /// <param name="CoordString"></param>
        /// <returns></returns>
        string GetGeoJsonPonto(string CoordString);

        /// <param name="ID"></param>
        /// <param name="IDAreaServico"></param>
        /// <returns>Este método retorna a lista de zonas contendo as informações geograficas em JSON e os dados da FIELDS registrada no banco. </returns>
        IEnumerable<GeoOBJ> OpenGeoZonas(Guid ID, Guid IDAreaServico);

        /// <summary>Este método será utilizado para armazenar os dados do SHAPE no BNG. </summary>
        /// <param name="obj"></param>
        /// <returns>Retorna True ou False.</returns>
        bool ExportSHPToBNG(ShapeBNG obj); 

    }
}
