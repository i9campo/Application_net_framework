using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatukGIS.NDK;

namespace Sigma.Domain.Auxiliar
{
    public class ConversorCoordenadasShape
    {
            public TGIS_LayerAbstract layer { get; set; }
            public TGIS_LayerSHP shape { get; set; }

            public TGIS_LayerVector layerFinal { get; set; }
            private Double valorbasey = 0;
            private Double valorbasex = 0;

            private bool isDecimal = false;
            private StringBuilder pontos = new StringBuilder();
            private int orbita { get; set; }
            private int tipo { get; set; }
            public TGIS_LayerVector Converter(int orbita, int tipo)
            {
                this.orbita = orbita;
                layer.Open();

                int lastUID = 0;

                if (layer.GetType() == typeof(TGIS_LayerSHP))
                {
                    lastUID = ((TGIS_LayerSHP)layer).GetLastUid();
                }
                else if (layer.GetType() == typeof(TGIS_LayerVector))
                {
                    lastUID = ((TGIS_LayerVector)layer).GetLastUid();
                }

                Double x = layer.Extent.XMin;
                Double y = layer.Extent.YMin;

                if (x < -33 && x > -74)
                {
                    isDecimal = true;
                }

                layerFinal = new TGIS_LayerVector();

                layerFinal.Name = layer.Name;

                if (!isDecimal)
                {
                    layerFinal.Name += "FILESAVE";
                }

                layerFinal.Open();

                CopyField(layer, layerFinal);

                for (int i = 1; i <= lastUID; i++)
                {
                    TGIS_Shape shp = null;

                    if (layer.GetType() == typeof(TGIS_LayerSHP))
                    {
                        shp = ((TGIS_LayerSHP)layer).GetShape(i);
                    }
                    else if (layer.GetType() == typeof(TGIS_LayerVector))
                    {
                        shp = ((TGIS_LayerVector)layer).GetShape(i);
                    }

                    if (shp.GetType().Equals(typeof(TGIS_ShapePolygon)))
                    {
                        TGIS_Shape poly = layerFinal.CreateShape(TGIS_ShapeType.gisShapeTypePolygon);
                        int parts = shp.GetNumParts();

                        if (parts > 1)
                        {
                            //string zm = shp.GetField(Fields.Zona).ToString();
                            //MessageBox.Show("A ZM ou área " + zm + " contém mais de um polígono, corrija", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        ConverterSHP(poly, (TGIS_ShapePolygon)shp);
                    }
                    else if (shp.GetType().Equals(typeof(TGIS_ShapePoint)))
                    {
                        if (tipo == 0)
                        {
                            TGIS_Shape point = layerFinal.CreateShape(TGIS_ShapeType.gisShapeTypePoint);
                            ConverterSHP(point, (TGIS_ShapePoint)shp);
                        }
                        else
                        {
                            if (i < lastUID)
                            {
                                TGIS_Shape point = layerFinal.CreateShape(TGIS_ShapeType.gisShapeTypePoint);
                                ConverterSHP(point, (TGIS_ShapePoint)shp);
                            }
                        }
                    }
                }

                return layerFinal;
            }


            private  void ConverterSHP(TGIS_Shape poly, TGIS_Shape shp)
            {
                Double Xmax = 0;
                Double Ymin = 0;

                if (isDecimal)
                {
                    Xmax = shp.Extent.XMax;
                    Ymin = shp.Extent.YMin;
                }

                int parts = shp.GetNumParts();
                int points = shp.GetNumPoints();
                for (int i = 0; i < parts; i++)
                {
                    poly.AddPart();
                    for (int y = 0; y < points; y++)
                    {
                        TGIS_Point ptInicial = shp.GetPoint(i, y);

                        Double[] coordenadasIniciais;
                        TGIS_Point pt;
                        if (!isDecimal)
                        {
                            coordenadasIniciais = ConversorCoordenadas.ConvertUTMDecimal(ptInicial.X, ptInicial.Y, orbita);
                            pt = new TGIS_Point(coordenadasIniciais[0], coordenadasIniciais[1]);

                            if (valorbasey == 0)
                                valorbasey = pt.Y;

                            double dify = (valorbasey - pt.Y);
                            double difx = (valorbasex - pt.X);

                            if ((valorbasey - pt.Y) < -2)
                            {
                                if (orbita == 22)
                                    orbita = 23;
                                else
                                    orbita = 22;

                                coordenadasIniciais = ConversorCoordenadas.ConvertUTMDecimal(ptInicial.X, ptInicial.Y, orbita);
                                pt.X = coordenadasIniciais[0];
                                pt.Y = coordenadasIniciais[1];
                            }
                        }
                        else
                        {
                            coordenadasIniciais = ConversorCoordenadas.ConvertDecimalUTM(ptInicial.Y, ptInicial.X, 1);
                            pt = new TGIS_Point(coordenadasIniciais[1], coordenadasIniciais[0]);

                            if (valorbasex == 0)
                                valorbasex = pt.X;

                            if ((valorbasex - pt.X) > 300000)
                            {
                                coordenadasIniciais = ConversorCoordenadas.ConvertDecimalUTM(ptInicial.Y, ptInicial.X, 2);
                                pt.X = coordenadasIniciais[1];
                                pt.Y = coordenadasIniciais[0];
                            }

                            if ((pt.X - valorbasex) > 300000)
                            {
                                coordenadasIniciais = ConversorCoordenadas.ConvertDecimalUTM(ptInicial.Y, ptInicial.X, 3);
                                pt.X = coordenadasIniciais[1];
                                pt.Y = coordenadasIniciais[0];
                            }
                        }

                        CopyFieldValue(poly, shp);

                        poly.AddPoint(pt);
                    }
                }

                if (isDecimal)
                {
                    double C1 = ConversorCoordenadas.calcfatorarea(Xmax, Ymin, 30, 1);
                    for (int i = 0; i < parts; i++)
                    {
                        Double area = shp.Area();
                        area = Math.Round((area / C1 / 10000), 2) - 0.02;
                        for (int j = 0; j < shp.Layer.Fields.Count; j++)
                        {
                            TGIS_FieldInfo info = shp.Layer.FieldInfo(j);
                            String NOME = info.Name;
                            if (NOME == "AREA")
                            {
                                poly.SetField(info.Name, area);
                            }
                        }
                    }
                }
            }

            private void CopyField(TGIS_LayerAbstract input, TGIS_LayerVector output)
            {

                TGIS_LayerVector ly = null;
                if (input.GetType() == typeof(TGIS_LayerSHP))
                {
                    ly = (TGIS_LayerSHP)input;
                }
                else if (input.GetType() == typeof(TGIS_LayerVector))
                {
                    ly = ((TGIS_LayerVector)input);
                }

                if (ly.Fields == null)
                    return;

                for (int i = 0; i < ly.Fields.Count; i++)
                {
                    TGIS_FieldInfo info = ly.FieldInfo(i);

                    output.AddField(info.Name, info.FieldType, info.Width, info.Decimal);
                }
            }

            private void CopyFieldValue(TGIS_Shape poly, TGIS_Shape shp)
            {
                for (int i = 0; i < shp.Layer.Fields.Count; i++)
                {
                    TGIS_FieldInfo info = shp.Layer.FieldInfo(i);
                    var value = shp.GetField(info.Name);
                    poly.SetField(info.Name, value);
                }
            }
        }
    }

