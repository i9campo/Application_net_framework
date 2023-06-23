using FluentValidation.Results;
using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sigma.App.Interfaces
{
    public interface IImagemSateliteAppService : IAppService<ImagemSatelite>
    {
        Task<TiffImage> GenerateNDVI(Guid IDFolder, string coordinates, IEnumerable<string> banda);


        /// <summary>Este método será utilizado para carregara os dados da imagem. </summary>
        /// <param name="obj"></param>
        /// <returns>Retorna um objeto do tipo Image Bytes. </returns>
        Task<TiffImage> GenerateSplitImage(SplitImage obj); 


        /// <summary>
        /// <para>Retorna um Db Geometry. </para>
        /// </summary>
        /// <param name="geocoord"></param>
        /// <returns></returns>
        ImagemSateliteView GetGeometry(string geocoord);

        /// <summary> Atualiza os dados </summary>
        /// <param name="geoString"></param>
        /// <param name="objID"></param>
        /// <returns></returns>
        bool UpdateCoordsImg(string geoString, Guid objID);

        /// <summary> Este método será utilizado para carregar os arquivo tiff no servidor. </summary>
        /// <param name="obj">Objeto referente aos bytes e dados do arquivo tiff. </param>
        /// <returns>return true or false</returns>
        Task<bool> PostTiff(TiffPost obj);

        /// <summary> Este método tem como objetivo carregar a lista de poligonos referente a imagem salva na pasta TIFF no servidor.  </summary>
        /// <param name="coord"> Coordenadas do poligono em formato "POLYGON((-0 -1, -2 -1)))"</param>
        /// <returns>Retorna uma lista do tipo Imagem Satelite. </returns>
        List<ImagemSatelite> GetListGeoIMGS(string coord); 
    }
}
