using System;
using TatukGIS.NDK;

namespace Sigma.Domain.Auxiliar
{
    public class SaveExtFile
    {
        public static void Save(TGIS_LayerVector layer, String caminhoArquivo)
        {
            Object tipo = layer.GetShape(layer.GetLastUid()).GetType();

            TGIS_ShapeType tipoShape = TGIS_ShapeType.gisShapeTypePolygon;

            if (tipo == typeof(TGIS_ShapePoint))
            {
                tipoShape = TGIS_ShapeType.gisShapeTypePoint;
            }
            else if (tipo == typeof(TGIS_ShapeArc))
            {
                tipoShape = TGIS_ShapeType.gisShapeTypeArc;
            }

            TGIS_LayerSHP novoLayer = new TGIS_LayerSHP();
            novoLayer.CS = layer.CS;
            novoLayer.Name = layer.Name;
            novoLayer.Path = caminhoArquivo;

            novoLayer.ImportLayer(layer, layer.Extent, tipoShape, "", true);

            novoLayer.Dispose();

        }
    }
}
