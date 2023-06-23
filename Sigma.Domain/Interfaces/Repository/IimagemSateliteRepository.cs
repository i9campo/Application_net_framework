using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IimagemSateliteRepository : IRepository<ImagemSatelite>
    {
        /// <summary>
        /// <para>Retorna uma imagem de satelite com o Geo em formato de texto. </para>
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        ImagemSateliteView GetImagem(Guid objID);

        IEnumerable<ImagemSateliteView> GetListImg(string coords);

        /// <summary>
        /// <para>Retorna um Db Geometry. </para>
        /// </summary>
        /// <param name="geocoord"></param>
        /// <returns></returns>
        ImagemSateliteView GetGeometry(string geocoord);

        /// <summary>
        /// Retorna um geo em GeoJson. 
        /// </summary>
        /// <returns></returns>
        string SerializeGeo(Guid objID);

        bool UpdateCoordsImg(string geoString, Guid objID);
        /// <summary> Este método tem como objetivo carregar a lista de poligonos referente a imagem salva na pasta TIFF no servidor.  </summary>
        /// <param name="coord"> Coordenadas do poligono em formato "POLYGON((-0 -1, -2 -1)))"</param>
        /// <returns>Retorna uma lista do tipo Imagem Satelite. </returns>
        List<ImagemSatelite> GetListGeoIMGS(string coord);
    }

}
