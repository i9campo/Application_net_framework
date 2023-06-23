using Sigma.Domain.Entities;
using System;
using TatukGIS.NDK;

namespace Sigma.Domain.GerenciadorDeFields
{
    public class GerenciadorDeFields
    {
        public static void AdicionarFieldsParaShape(TGIS_Shape shape)
        {
            TGIS_LayerVector layer = shape.Layer;

            if (shape.GetField(Field.IdArea) == null)
            {
                layer.AddField(Field.IdArea, TGIS_FieldType.gisFieldTypeString, 50, 0);
            }

            if (shape.GetField(Field.IdPropriedade) == null)
            {
                layer.AddField(Field.IdPropriedade, TGIS_FieldType.gisFieldTypeString, 50, 0);
            }

            if (shape.GetField(Field.IdSafra) == null)
            {
                layer.AddField(Field.IdSafra, TGIS_FieldType.gisFieldTypeString, 50, 0);
            }

            if (shape.GetType().Equals(typeof(TGIS_ShapePolygon)))
            {
                int maxID = shape.Layer.GetLastUid();

                if (shape.GetField(Field.Area) == null)
                {
                    layer.AddField(Field.Area, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Pontos) == null)
                {
                    layer.AddField(Field.Pontos, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (maxID > 1)
                {

                    if (shape.GetField(Field.CodigoZM) == null)
                    {
                        layer.AddField(Field.CodigoZM, TGIS_FieldType.gisFieldTypeString, 50, 0);
                    }

                    if (shape.GetField(Field.Zona) == null)
                    {
                        layer.AddField(Field.Zona, TGIS_FieldType.gisFieldTypeString, 50, 0);
                    }

                    if (shape.GetField(Field.ZonaArea) == null)
                    {
                        layer.AddField(Field.ZonaArea, TGIS_FieldType.gisFieldTypeString, 50, 0);
                    }

                    if (shape.GetField(Field.ValorArea) == null)
                    {
                        layer.AddField(Field.ValorArea, TGIS_FieldType.gisFieldTypeString, 50, 0);
                    }

                    if (shape.GetField(Field.ZonaCodigo) == null)
                    {
                        layer.AddField(Field.ZonaCodigo, TGIS_FieldType.gisFieldTypeString, 50, 0);
                    }
                }
                else
                {
                    if (shape.GetField(Field.CodigoTL) == null)
                    {
                        layer.AddField(Field.CodigoTL, TGIS_FieldType.gisFieldTypeString, 50, 0);
                    }

                    if (shape.GetField(Field.Talhao) == null)
                    {
                        layer.AddField(Field.Talhao, TGIS_FieldType.gisFieldTypeString, 50, 0);
                    }

                    if (shape.GetField(Field.TalhaoArea) == null)
                    {
                        layer.AddField(Field.TalhaoArea, TGIS_FieldType.gisFieldTypeString, 50, 0);
                    }

                    if (shape.GetField(Field.TalhaoCodigo) == null)
                    {
                        layer.AddField(Field.TalhaoCodigo, TGIS_FieldType.gisFieldTypeString, 50, 0);
                    }
                }
            }
            else if (shape.GetType().Equals(typeof(TGIS_ShapePoint)))
            {
                if (shape.GetField(Field.NumeroPonto) == null)
                {
                    layer.AddField(Field.NumeroPonto, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Tipo) == null)
                {
                    layer.AddField(Field.Tipo, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Solo) == null)
                {
                    layer.AddField(Field.Solo, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.CompactacaoSolo) == null)
                {
                    layer.AddField(Field.CompactacaoSolo, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.ValorPonto) == null)
                {
                    layer.AddField(Field.ValorPonto, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.PontoTS) == null)
                {
                    layer.AddField(Field.PontoTS, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.PontoArg) == null)
                {
                    layer.AddField(Field.PontoArg, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Talhao) == null)
                {
                    layer.AddField(Field.Talhao, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Ca) == null)
                {
                    layer.AddField(Field.Ca, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Mg) == null)
                {
                    layer.AddField(Field.Mg, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.K) == null)
                {
                    layer.AddField(Field.K, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.P) == null)
                {
                    layer.AddField(Field.P, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.S) == null)
                {
                    layer.AddField(Field.S, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.B) == null)
                {
                    layer.AddField(Field.B, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Zn) == null)
                {
                    layer.AddField(Field.Zn, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Mn) == null)
                {
                    layer.AddField(Field.Mn, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Fe) == null)
                {
                    layer.AddField(Field.Fe, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Cu) == null)
                {
                    layer.AddField(Field.Cu, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.PhCaCl) == null)
                {
                    layer.AddField(Field.PhCaCl, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.MO) == null)
                {
                    layer.AddField(Field.MO, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Al) == null)
                {
                    layer.AddField(Field.Al, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.CTC) == null)
                {
                    layer.AddField(Field.CTC, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.CaCTC) == null)
                {
                    layer.AddField(Field.CaCTC, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.MgCTC) == null)
                {
                    layer.AddField(Field.MgCTC, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.KCTC) == null)
                {
                    layer.AddField(Field.KCTC, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.V) == null)
                {
                    layer.AddField(Field.V, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Lat) == null)
                {
                    layer.AddField(Field.Lat, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Long) == null)
                {
                    layer.AddField(Field.Long, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Argila) == null)
                {
                    layer.AddField(Field.Argila, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Silte) == null)
                {
                    layer.AddField(Field.Silte, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Areia) == null)
                {
                    layer.AddField(Field.Areia, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

            }
        }

        public static void AdicionarFieldsParaShapeZM(TGIS_Shape shape)
        {
            TGIS_LayerVector layer = shape.Layer;

            if (shape.GetField(Field.IdArea) == null)
            {
                layer.AddField(Field.IdArea, TGIS_FieldType.gisFieldTypeString, 50, 0);
            }

            if (shape.GetField(Field.IdPropriedade) == null)
            {
                layer.AddField(Field.IdPropriedade, TGIS_FieldType.gisFieldTypeString, 50, 0);
            }

            if (shape.GetField(Field.IdSafra) == null)
            {
                layer.AddField(Field.IdSafra, TGIS_FieldType.gisFieldTypeString, 50, 0);
            }

            if (shape.GetType().Equals(typeof(TGIS_ShapePolygon)))
            {

                if (shape.GetField(Field.Area) == null)
                {
                    layer.AddField(Field.Area, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Pontos) == null)
                {
                    layer.AddField(Field.Pontos, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.CodigoZM) == null)
                {
                    layer.AddField(Field.CodigoZM, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Zona) == null)
                {
                    layer.AddField(Field.Zona, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.ZonaArea) == null)
                {
                    layer.AddField(Field.ZonaArea, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.ValorArea) == null)
                {
                    layer.AddField(Field.ValorArea, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.ZonaCodigo) == null)
                {
                    layer.AddField(Field.ZonaCodigo, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }
            }
            else if (shape.GetType().Equals(typeof(TGIS_ShapePoint)))
            {
                if (shape.GetField(Field.NumeroPonto) == null)
                {
                    layer.AddField(Field.NumeroPonto, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Tipo) == null)
                {
                    layer.AddField(Field.Tipo, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Solo) == null)
                {
                    layer.AddField(Field.Solo, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.CompactacaoSolo) == null)
                {
                    layer.AddField(Field.CompactacaoSolo, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.ValorPonto) == null)
                {
                    layer.AddField(Field.ValorPonto, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.ValorPonto) == null)
                {
                    layer.AddField(Field.ValorPonto, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.PontoTS) == null)
                {
                    layer.AddField(Field.PontoTS, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.PontoArg) == null)
                {
                    layer.AddField(Field.PontoArg, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }
            }
        }

        public static void AdicionarFieldsParaShapeZMsolo(TGIS_Shape shape)
        {
            TGIS_LayerVector layer = shape.Layer;

            if (shape.GetType().Equals(typeof(TGIS_ShapePolygon)))
            {

                if (shape.GetField(Field.Area) == null)
                {
                    layer.AddField(Field.Area, TGIS_FieldType.gisFieldTypeNumber, 10, 2);
                }

                if (shape.GetField(Field.Zona) == null)
                {
                    layer.AddField(Field.Zona, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.CodigoZM) == null)
                {
                    layer.AddField(Field.CodigoZM, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Nomesolo) == null)
                {
                    layer.AddField(Field.Nomesolo, TGIS_FieldType.gisFieldTypeString, 10, 0);
                }

                if (shape.GetField(Field.Codigo) == null)
                {
                    layer.AddField(Field.Codigo, TGIS_FieldType.gisFieldTypeNumber, 10, 0);
                }

                if (shape.GetField(Field.Talhao) == null)
                {
                    layer.AddField(Field.Talhao, TGIS_FieldType.gisFieldTypeString, 50, 0);
                }

                if (shape.GetField(Field.Semente) == null)
                {
                    layer.AddField(Field.Semente, TGIS_FieldType.gisFieldTypeNumber, 10, 2);
                }

                if (shape.GetField(Field.Argila) == null)
                {
                    layer.AddField(Field.Argila, TGIS_FieldType.gisFieldTypeNumber, 10, 2);
                }

                if (shape.GetField(Field.Silte) == null)
                {
                    layer.AddField(Field.Silte, TGIS_FieldType.gisFieldTypeNumber, 10, 2);
                }

                if (shape.GetField(Field.Areia) == null)
                {
                    layer.AddField(Field.Areia, TGIS_FieldType.gisFieldTypeNumber, 10, 2);
                }
            }
        }

        public static void AdicionarFieldsParaShape(TGIS_Shape shape, String field)
        {
            if (shape.Layer.FindField(field) == -1)
            {
                shape.Layer.AddField(field, TGIS_FieldType.gisFieldTypeString, 50, 0);
            }
        }

        public static void SetarValorField(TGIS_Shape shape, String field, String valor)
        {
            if (shape.GetField(field) != null)
            {
                shape.SetField(field, valor);
            }
        }

        public static void SetarValorFieldNumero(TGIS_Shape shape, String field, Double valor)
        {
            if (shape.GetField(field) != null)
            {
                shape.SetField(field, valor);
            }
        }

        public static void DeletarField(TGIS_Shape shape, String field)
        {
            shape.Layer.DeleteField(field);

            shape.Layer.SaveAll();
        }

        public static void SetarContornoParaShape(TGIS_Shape shape)
        {
            if (shape.GetType().Equals(typeof(TGIS_ShapePolygon)))
            {
                shape.Params.Area.Pattern = TGIS_BrushStyle.gisBsClear;
                shape.Params.Area.OutlineStyle = TGIS_PenStyle.gisPsSolid;
                //shape.Params.Area.OutlineColor = Color.Black;
                shape.Params.Area.OutlineWidth = 50;
            }
            else if (shape.GetType().Equals(typeof(TGIS_ShapePoint)))
            {
                shape.Params.Area.Pattern = TGIS_BrushStyle.gisBsClear;
                shape.Params.Area.OutlineStyle = TGIS_PenStyle.gisPsSolid;
                //shape.Params.Area.OutlineColor = Color.Green;
                shape.Params.Area.OutlineWidth = 50;
            }
        }
    }
}