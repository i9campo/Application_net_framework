using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Domain.Auxiliar
{
    public static class ConversorCoordenadas
    {
        /// Classe que converte coordenadas UTM para DECIMAL - desabilitado
        public static Double[] ConvertUTMDecimal1(Double x, Double y, Double ZONA, Char SETOR = 's')
        {
            //Setor definido como "S" por atendermos somente áreas no Brasil

            Double ZCENTRAL, EQ, SL, e, ex, Q1, Q2, Q3, Q4, E1, C1, C2, C3, C4, LAT, C, T, N, R, D, F1, F2, F3, F4, Latitude, G1, G2, DELTA, E, Longitude = 0;
            Double x1 = 1, y1 = 1;
            if (SETOR == 's')
            {
                x1 = -1;
                y1 = -1;
            }

            //if (SETOR != 's')
            //{
            //    y = y + 10000000;
            //}

            if (ZONA > 0)
                ZCENTRAL = (6 * ZONA) - 183;
            else
                ZCENTRAL = 3;

            EQ = 6378137;
            SL = 0.9996;
            Double Ex1 = 1 / 298.257223563; // Achatamento f = (6378137-6356752.31424518) / 6378137

            e = (2 * Ex1) - Math.Pow(Ex1, 2); //e = Eixo de rotação
            ex = Math.Pow(e, 2) / (1 - Math.Pow(e, 2));

            Q1 = 500000 - x;
            Q2 = 10000000 - y;
            Q3 = Q2 / SL;
            Q4 = Q3 / (EQ * (1 - Math.Pow(e, 2) / 4 - 3 * Math.Pow(e, 4) / 64 - 5 * Math.Pow(e, 6) / 256));

            E1 = (1 - Math.Pow((1 - e * e), 0.5)) / (1 + Math.Pow((1 - e * e), 0.5));
            C1 = (3 * (E1 / 2)) - ((27 * Math.Pow(E1, 3)) / 32);
            C2 = 21 * Math.Pow(E1, 2) / 16 - (55 * Math.Pow(E1, 4) / 32);
            C3 = 151 * Math.Pow(E1, 3) / 96;
            C4 = 1097 * Math.Pow(E1, 4) / 512;
            LAT = Q4 + C1 * Math.Sin(2 * Q4) + C2 * Math.Sin(4 * Q4) + C3 * Math.Sin(6 * Q4) + C4 * Math.Sin(8 * Q4);

            C = ex * Math.Pow(Math.Cos(LAT), 2);
            T = Math.Pow(Math.Tan(LAT), 2);
            N = EQ / Math.Pow((1 - Math.Pow((e * Math.Sin(LAT)), 2)), 0.5);
            R = EQ * (1 - e * e) / (Math.Pow(1 - Math.Pow(e * Math.Sin(LAT), 2), 1.5));
            D = Q1 / (N * SL);

            F1 = N * Math.Tan(LAT) / R;
            F2 = D * (D / 2);
            F3 = (5 + 3 * T + 10 * C - 4 * C * C - 9 * ex) * Math.Pow(D, 4) / 24;
            F4 = (61 + 90 * T + 298 * C + 45 * T * T - 252 * ex - 3 * C * C) * Math.Pow(D, 6) / 720;

            Latitude = 180 * (LAT - F1 * (F2 + F3 + F4)) / Math.PI;

            G1 = (1 + 2 * T + C) * Math.Pow(D, 3) / 6;
            G2 = (5 - 2 * C + 28 * T - 3 * Math.Pow(C, 2) + 8 * ex + 24 * Math.Pow(T, 2)) * Math.Pow(D, 5) / 120;
            DELTA = (D - G1 + G2) / Math.Cos(LAT);
            E = DELTA * 180 / Math.PI;
            if (ZCENTRAL < E)
            {
                Longitude = (ZCENTRAL - E) * -1;
            }
            else
            {
                Longitude = ZCENTRAL - E;
            }

            Longitude = Longitude * x1;
            Latitude = Latitude * y1;


            Double[] saida = new Double[2];
            saida[0] = Longitude;
            saida[1] = Latitude;

            return saida;
        }

        /// Classe que converte coordenadas Decimal para UTM - desabilitado
        public static Double[] ConvertDecimalUTM2(Double x, Double y)
        {

            Double a, b, c, d, e, f, g, h, i, j, k, a0, b0, c0, d0, e0, s, a1, c1, a2, b2, c2, d2, e2, f2, longitude, latitude, Setor = 0;

            //Verifica o quadrante das coordenadas América do Sul (W(-x) e S(-y)) ou América do Norte (W(-x) e N(+y)
            if (y < 0)
                Setor = 1;

            // determinação da zona
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

            // latitude de origem
            a1 = (6 * Zona) - 183;
            //b1 = (x * -1) - a1;
            // radiano da latitude
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

        //Metodo que calcula a distância entre dois pontos decimais
        public static double calcDistancia(double lat_inicial, double long_inicial, double lat_final, double long_final)
        {

            double d2r = 0.017453292519943295769236;

            double dlong = (long_final - long_inicial) * d2r;
            double dlat = (lat_final - lat_inicial) * d2r;

            double temp_sin = Math.Sin(dlat / 2.0);
            double temp_cos = Math.Cos(lat_inicial * d2r);
            double temp_sin2 = Math.Sin(dlong / 2.0);

            double a = (temp_sin * temp_sin) + (temp_cos * temp_cos) * (temp_sin2 * temp_sin2);
            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

            return 6368.1 * c;

        }

        //Fator de conversão de área de poligono de coordenada decimal para metros
        public static double calcfatorarea(Double x, Double y, Double Pix, int F)
        {

            Double vx = 0.000009 * Pix;
            Double vy = 0.000009 * Pix;

            Double xInicial = x;
            Double yInicial = y;
            Double xFinal = xInicial + vx;
            Double yFinal = yInicial + vy;

            Double R1 = (Pix * Pix) + (Pix * Pix);
            Double R2 = (Math.Round(Math.Sqrt(R1), 2)) / 1000;
            Double A1 = Math.Cos(((90 - yFinal) / 180) * Math.PI);
            Double A2 = Math.Cos(((90 - yInicial) / 180) * Math.PI);
            Double A3 = Math.Sin(((90 - yFinal) / 180) * Math.PI);
            Double A4 = Math.Sin(((90 - yInicial) / 180) * Math.PI);
            Double A5 = Math.Cos(((xFinal - xInicial) / 180) * Math.PI);
            Double DI = (Math.Acos(A1 * A2 + A3 * A4 * A5)) * 6371;
            Double DIFI = R2 - DI;
            if (R2 > DI)
            {
                vx = 0.000001 + vx;
                xFinal = 0;
                xFinal = xInicial + vx;
                A5 = Math.Cos(((xFinal - xInicial) / 180) * Math.PI);
                Double DF = (Math.Acos(A1 * A2 + A3 * A4 * A5)) * 6371;
                Double DIFF = R2 - DF;
                Double DIFR = DIFI - DIFF;
                Double FatorD = ((DIFI / DIFR) * 0.000001) / Pix;
                vx = (0.000009 + FatorD) * Pix;
            }
            else if (DIFI > 0.000009)
            {
                vx = 0.000001 - vx;
                xFinal = 0;
                xFinal = xInicial + vx;
                A5 = Math.Cos(((xFinal - xInicial) / 180) * Math.PI);
                Double DF = (Math.Acos(A1 * A2 + A3 * A4 * A5)) * 6371;
                Double DIFF = R2 - DF;
                Double DIFR = DIFI - DIFF;
                Double FatorD = ((DIFI / DIFR) * 0.000001) / Pix;
                vx = (0.000009 - FatorD) * Pix;
            }
            Double C1 = (vx * vy) / (Pix * Pix);

            if (F == 1)
                return C1;
            else
                return vx;
        }

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

        /// Classe que converte coordenadas DECIMAL para UTM
        public static Double[] ConvertDecimalUTM(Double x, Double y, int tipo)
        {

            Double a, b, c, rlat, rlong, rz, e, e1, aux1, aux2, aux3, aux4, aux5, a0, b0, c0, a2, b2, longitude, latitude = 0;

            //Verifica o quadrante das coordenadas América do Sul (W(-x) e S(-y)) ou América do Norte (W(-x) e N(+y)
            //if (y < 0)
            //    Setor = 1;

            // determinação da zona
            int Zona = 0;
            if (y < 0)
            {
                if (tipo == 1)
                {
                    Zona = (int)(((180 + y) / 6) + 1);
                }

                if (tipo == 2)
                {
                    Zona = (int)(((180 + y) / 6));
                }

                if (tipo == 3)
                {
                    Zona = (int)(((180 + y) / 6) + 2);
                }
            }
            else
            {
                Zona = (int)((y / 6) + 31);
            }

            a = 6378137;
            b = 1 / 298.257223563;
            c = 0.9996;

            // radiano da latitude
            rlat = x * (Math.PI / 180);
            // radiano da longitude
            rlong = y * (Math.PI / 180);
            // latitude de origem
            rz = ((6 * Zona) - 183) * (Math.PI / 180);


            e = (2 * b) - Math.Pow(b, 2); //e = Eixo de rotação
            e1 = e / (1 - e);

            aux1 = a / (Math.Sqrt(1 - e * Math.Sin(rlat) * Math.Sin(rlat)));
            aux2 = Math.Pow(Math.Tan(rlat), 2);
            aux3 = e1 * Math.Pow(Math.Cos(rlat), 2);
            aux4 = (rlong - rz) * Math.Cos(rlat);
            a0 = (1 - e / 4 - 3 * Math.Pow(e, 2) / 64 - 5 * Math.Pow(e, 3) / 256) * rlat;
            b0 = (3 * e / 8 + 3 * Math.Pow(e, 2) / 32 + 45 * Math.Pow(e, 3) / 1024) * Math.Sin(2 * rlat);
            c0 = (15 * Math.Pow(e, 2) / 256 + 45 * Math.Pow(e, 3) / 1024) * Math.Sin(4 * rlat) - (35 * Math.Pow(e, 3) / 3072) * Math.Sin(6 * rlat);
            aux5 = a * (a0 - b0 + c0);

            a2 = (aux4 + ((1 - aux2 + aux3) * Math.Pow(aux4, 3)) / 6 + ((5 - 18 * aux2 + Math.Pow(aux2, 2) + 72 * aux3 - 58 * e1) * Math.Pow(aux4, 5) / 120));
            longitude = c * aux1 * a2 + 500000;

            b2 = (Math.Pow(aux4, 2) / 2 + ((5 - aux2 + 9 * aux3 + 4 * Math.Pow(aux3, 2)) * Math.Pow(aux4, 4)) / 24 + ((61 - 58 * aux2 + Math.Pow(aux2, 2) + 600 * aux3 - 330 * e1) * Math.Pow(aux4, 6)) / 720);

            latitude = 10000000 + c * (aux5 + aux1 * Math.Tan(rlat) * b2);

            Double[] saida = new Double[2];
            saida[0] = Math.Round(latitude, 13);
            saida[1] = Math.Round(longitude, 13);

            return saida;
        }

    }
}
