using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBAPI.Auxiliar
{
    public static class ConversorCoordenadas
    {

        /// Classe que converte coordenadas UTM para DECIMAL
        public static Double[] ConvertUTMDecimal(Double x, Double y, Double ZONA, Char SETOR = 's')
        {
                Double ZCENTRAL, EQ, SL, Q1, Q2, Q3, Q4, Q5;

                if (ZONA > 0)
                    ZCENTRAL = (6 * ZONA) - 183;
                else
                    ZCENTRAL = 3;

                EQ = 6378137;
                SL = 0.9996;
                Double E1 = 1 / 298.257223563; // Achatamento f = (EQ-PL) / EQ

                Double equad = (2 * E1) - Math.Pow(E1, 2); //e = Eixo de rotação
                Double elinquad = equad / (1 - equad);
                Double e1 = (1 - Math.Sqrt(1 - equad)) / (1 + Math.Sqrt(1 - equad));

                Q1 = equad * equad; // Primeira excentricidade - e2
                Q2 = Q1 * equad;
                Q3 = e1 * e1;
                Q4 = e1 * Q3;
                Q5 = e1 * Q4;

                Double m = (y - 10000000) / SL;
                Double mi = m / (EQ * ((1 - (equad / 4)) - 3 * (Q1 / 64) - (5 * (Q2 / 256))));

                Double aux6 = ((3 * (e1 / 2)) - (27 * (Q4 / 32))) * Math.Sin(2 * mi);
                Double aux7 = ((21 * (Q3 / 16)) - (55 * (Q5 / 32))) * Math.Sin(4 * mi);
                Double aux8 = (151 * (Q4 / 96)) * Math.Sin(6 * mi);

                Double lat1 = mi + aux6 + aux7 + aux8;
                Double c1 = elinquad * Math.Pow(Math.Cos(lat1), 2);
                Double t1 = Math.Pow(Math.Tan(lat1), 2);
                Double n1 = EQ / Math.Sqrt(1 - (equad * Math.Pow(Math.Sin(lat1), 2)));
                Double quoc = Math.Pow((1 - (equad * Math.Sin(lat1) * Math.Sin(lat1))), 3);
                Double r1 = EQ * ((1 - equad) / Math.Sqrt(quoc));
                Double d = (x - 500000) / (n1 * SL);

                Double aux9 = ((5 + (3 * t1) + (10 * c1) - (4 * c1 * c1) - 9 * elinquad) * d * d * d * d) / 24;
                Double aux10 = ((61 + (90 * t1) + (298 * c1) + (45 * t1 * t1) - (252 * elinquad) - (3 * c1 * c1)) * Math.Pow(d, 6)) / 720;
                Double aux11 = d - (1 + (2 * t1) + c1) * d * d * d / 6;
                Double aux12 = ((5 - (2 * c1) + (28 * t1) - (3 * c1 * c1) + (8 * elinquad) + (24 * t1 * t1)) * Math.Pow(d, 5)) / 120;

                Double lat = lat1 - ((n1 * Math.Tan(lat1)) / r1) * (d * (d / 2) - aux9 + aux10);
                Double lon = ((aux11 + aux12) / Math.Cos(lat1));

                lat = (lat / Math.PI) * 180;
                lon = ZCENTRAL + (lon / Math.PI) * 180;
                Double[] saida = new Double[2];
                saida[0] = lon;
                saida[1] = lat;

                return saida;

        }

            /// Classe que converte coordenadas Decimal para UTM
        public static Double[] ConvertDecimalUTM(Double x, Double y)
        {

                Double a, b, c, d, e, f, g, h, i, j, k, a0, b0, c0, d0, e0, s, a1, b1, c1, a2, b2, c2, d2, e2, f2, longitude, latitude, Setor = 0;

                //Verifica o quadrante das coordenadas América do Sul (W(-x) e S(-y)) ou América do Norte (W(-x) e N(+y)
                if (y < 0)
                    Setor = 1;

                int Zona = 0;

                if (y < 0)
                {
                    Zona = (int)(((180 + y) / 6) + 1);
                }
                else
                {
                    Zona = (int)((y / 6) + 31);
                }

                a = 6378137;
                b = 6356752.31424518;
                c = 0.9996;

                a1 = (6 * Zona) - 183;
                b1 = (x * -1) - a1;
                c1 = x * (Math.PI / 180);

                d = (a - b) / a;
                e = 1 / d;
                f = Math.Pow((a * b), (1f / 2f));

                g = Math.Sqrt(1f - Math.Pow((b / a), 2f));
                h = g * g / (1 - g * g);
                i = (a - b) / (a + b);
                j = a * (1f - g * g) / ((Math.Pow(1f - (Math.Pow(g * Math.Sin(c1), 2f)), (3f / 2f))));
                k = a / ((Math.Pow(1f - Math.Pow(g * Math.Sin(c1), 2f), (1f / 2f))));

                a0 = a * (1 - i + (5 * i * i / 4) * (1 - i) + (81 * (Math.Pow(i, 4) / 64)) * (1 - i));
                b0 = (3 * a * i / 2) * (1 - i - (7 * i * i / 8) * (1 - i) + 55 * (Math.Pow(i, 4) / 64));
                c0 = (15 * a * i * i / 16) * (1 - i + (3 * i * i / 4) * (1 - i));
                d0 = (35 * a * (Math.Pow(i, 3) / 48)) * (1 - i + 11 * i * i / 16);
                e0 = (315 * a * (Math.Pow(i, 4) / 51)) * (1 - i);
                s = a0 * c1 - b0 * Math.Sin(2 * c1) + c0 * Math.Sin(4 * c1) - d0 * Math.Sin(6 * c1) + e0 * Math.Sin(8 * c1);

                a2 = s * c;
                b2 = c * Math.Sin(c1) * Math.Cos(c1) * k / 2;
                c2 = (c * Math.Sin(c1) * (Math.Pow(Math.Cos(c1), 3) / 24)) * (5 - Math.Pow(Math.Tan(c1), 2) + 9 * h * Math.Pow(Math.Cos(c1), 2) + 4 * Math.Pow(h, 2) * Math.Pow(Math.Cos(c1), 4)) * k;
                d2 = c * Math.Cos(c1) * k;
                e2 = Math.Pow(Math.Cos(c1), 3) * (c / 6) * (1 - Math.Pow(Math.Tan(c1), 2) + h * Math.Pow(Math.Cos(c1), 2)) * k;
                f2 = (y - a1) * (Math.PI / 180);

                if (Setor > 0)
                {
                    latitude = 10000000 + (a2 + b2 * f2 * f2 + c2 * Math.Pow(f2, 4));
                }
                else
                {
                    latitude = (a2 + b2 * f2 * f2 + c2 * Math.Pow(f2, 4));
                }
                longitude = 500000 + (d2 * f2 + e2 * Math.Pow(f2, 3));

                Double[] saida = new Double[2];
                saida[0] = latitude;
                saida[1] = longitude;

                return saida;
        }
    }
}