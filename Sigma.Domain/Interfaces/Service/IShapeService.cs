using Sigma.Domain.Interfaces.Service._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using static Sigma.Domain.ViewTables.OpenGeo;

namespace Sigma.Domain.Interfaces.Service
{
    public interface IShapeService : IService<Shape>
    {
        /// <returns>Retorna uma lista de dados do BNG referente a tabela. ("SHAPE") </returns>
        IEnumerable<BNG_Shape> GetListShapeByAreaServico(Guid IDSafra, Guid IDArea);

        /// <returns>Retorna uma lista de dados Geograficos a partir do ID do Shape. </returns>
        IEnumerable<IEnumerable<FileExt>> GetFileByIDShape(IEnumerable<string> IDShape, int orbita);

        /// <param name="ID">Parâmetro referente a linha do objeto selecionado em arquivos da área na tela de zoneamento. </param>
        /// <param name="IDAreaServico">Parâmetro referente ao objID da área serviço. </param>
        /// <param name="tipo"> Parâmetro referente ao tipo de retorno. 
        ///                     tipo: ("AREA") Retorna o Geo referente área serviço. 
        ///                     tipo: ("ZM") Retorna o Geo referente grid ("Zonas de Manejo"). 
        ///                     tipo: ("Point ou Points") Retorna o Geo referente aos pontos de coleta ("Aanálises"). 
        /// </param>
        /// <returns>Retorna dados geográficos junto com a lista atualizada de fields e rotulos. </returns>
        IEnumerable<GeoOBJ> OpenGeoByAreaServico(Guid? ID, Guid IDAreaServico, string tipo);

        /// <summary> Este método é utilizado para exportar do SIGMA WEB para o BNG. </summary>
        /// <param name="obj">Conjunto de dados referente ao arquivo selecionado SHP.</param>
        /// <returns>Retorna true ou false.</returns>
        bool ExportSHPToBNG(IEnumerable<ImportShapeToBNG> obj);

        /// <param name="Orbita"> Campo inicial será 1, caso o arquivo tenha coordenadas acima de 180 será necessário passar o valor da orbita para converter de UTM para Decimal. </param>
        /// <param name="file"> Campo file contém as informações do arquivo. </param>
        /// <returns>Retorna uma lista de dados geográficos em formato JSON, junto com informações auxiliares. </returns>
        IEnumerable<FileExt> OpenGeoSHP(int Orbita, File64 file);

        /// <param name="obj">Contém informações das coordenadas junto com o nome do arquivo selecionado. </param>
        /// <returns>Retorna um arquivo em bytpe para Download. </returns>
        byte[] ShpCreateFile(IEnumerable<GeoCoordText> obj);


        /// <summary> Este método será utilizado para carregar os dados em XML para KML. </summary>
        /// <param name="GeoString"></param>
        /// <returns></returns>
        byte[] KMLGenerate(IEnumerable<KMLShape> obj);

        /// <summary> Método utilizado para gerar um objeto do tipo KML. </summary>
        /// <param name="KMLString"></param>
        /// <returns>Retorna um objeto do tipo KML LIST.</returns>
        IEnumerable<KMLShape> GetKMLFile(string KMLString);
    }
}
