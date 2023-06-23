using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IGeoConfigRepository : IRepository<GeoView>
    {
        IEnumerable<LstPontos> SplitPoly(string poly);
        IEnumerable<string> OrdePoint(List<string> lstPoint);
        IEnumerable<string> OrderPoly(List<string> lstPoly);

        /// <param name="LineString">String contendo os dados geográficos do tipo linha ("POLILINHA"). </param>
        /// <param name="PolygonString">String contendo os dados geográficos do tipo ("Polygon"). </param>
        /// <param name="IDSelect">Este ID será referente a lista de arquivos abertos. </param>
        /// <returns>Retorna uma lista de dados geográficos atualizado a partir de um corte com uma LineString. </returns>
        IEnumerable<Polygon> SplitPolygon(string LineString, string PolygonString, Guid? IDSelect);

        bool GetWithinPoint(string poly, string point);

        /// <summary> Este método será utilizado para verificar sé o polygono é valido. </summary>
        /// <param name="points_poly"> string de pontos. </param>
        /// <returns> True or False </returns>
        bool CheckedPoly(string points_poly);

        IEnumerable<string> SelectedUnionLines(string Polygon, string MultLine, Guid? IDSelected);

        /// <summary>
        /// <para>Retorna dados geográficos de um polygon a partir do geoJSON. </para>
        /// </summary>
        /// <param name="geoJson"></param>
        /// <returns></returns>
        DbGeography GetGeoPolygon(string geoJson);

        /// <summary>
        /// <para>Retorna dados geográficos de um ponto a partir do geoJSON. </para>
        /// </summary>
        /// <param name="geoJson"></param>
        /// <returns></returns>
        DbGeography GetGeoPoint(string geoJson);

        /// <summary>
        /// Este método retorna uma string geo json vinculo com python. 
        /// </summary>
        /// <param name="strQUERY"></param>
        /// <returns></returns>
        string GetGeoPointJSON(string strQUERY);

        /// <summary>
        /// Este método retorna uma string geo json vinculada.
        /// </summary>
        /// <param name="stringQUERY"></param>
        /// <returns></returns>
        string GetGeoJson(string stringQUERY);

        /// <summary>
        /// Este método retorna uma string geo json vinculo com python. 
        /// </summary>
        /// <param name="strQUERY"></param>
        /// <returns></returns>
        string GetGeoCenter(string strQUERY);


        /// <summary>
        /// </summary>
        /// <param name="coord"></param>
        /// <returns>O Tamanho em Hectare a partir das coordenadas. </returns>
        double GetSize(string coord);

        /// <summary>
        /// Este Método verifica se o geo está no tamanho correto, caso não esteja ele reorienta o geo. 
        /// </summary>
        /// <param name="GeoString"></param>
        /// <returns>Retorna um novo geo reorientado. </returns>
        string GetReorientGeoString(string GeoString);

        List<string> GenerateGeoJsonPoints(List<string> lstpoly);
        string GetGeoAreaByIDAreaServico(Guid IDAreaServico);

        string GetGeoJsonByIDAreaServico(Guid IDAreaServico);

        string GetCreateGeoString(Guid IDCiclo, string GeoString);

        string GetCreateGeoJson(Guid IDCiclo, string GeoString);

        IEnumerable<GeoDate> GetSplitMultGeoCiclo(Guid GeoString, int? Index, int ciclo);

        /// <param name="GeoJson"></param>
        /// <returns>Retorna o GeoJson a partir de um GeoString. </returns>
        string GetGeoJson(string GeoJson, bool? mulltLine);

        /// <param name="coord">string contendo as cordenadas de uma linha.</param>
        /// <returns>Retorna o ponto inicial e final de uma linha</returns>
        LastFirstPoint GetFirstLastPoint(string coord);

        /// <summary>
        /// Este método será utilizado para remover uma linha de uma zona. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        PolyString SplitZones(LineSplitZones obj);

        /// <summary>
        /// Esté método será utilizado para remover o polygon que estiver dentro de outro. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Retorna um polygon do tipo linha. </returns>
        IEnumerable<string> SplitWitinPoly(LineSplitZones obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Retorna uma lista de polygon</returns>
        string GetSplitWithinPolygon(LineSplitZones obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Retorna uma lista de polygon</returns>
        string GetSplitPoly(LineSplitZones obj);

        /// <summary>
        /// Esté método será utilizado para remover o polygon que estiver dentro de outro. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Retorna uma todas as zonas. </returns>
        IEnumerable<string> GenerateZones(LineSplitZones obj);

        /// <summary>
        /// Esté método será utilizado para remover o polygon que estiver dentro de outro. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Retorna uma todas as zonas. </returns>
        IEnumerable<string> GenerateZonesInternal(LineSplitZones obj);

    }
}
