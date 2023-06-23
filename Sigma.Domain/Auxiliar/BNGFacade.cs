using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatukGIS.NDK;

namespace Sigma.Domain.Auxiliar
{
    public  class BNGFacade
    {
        public static Double CalcularTamanhoArea(Double area)
        {
            Double rounded = Math.Round(area / 10000, 3);

            if ((rounded - (int)rounded) != 0)
            {
                String str = rounded.ToString().PadRight(3, '7');

                int indice = str.IndexOf(',') + 3;

                if (str.Length <= indice)
                {
                    return Double.Parse(str);
                }

                return Double.Parse(str.Remove((str.IndexOf(',') + 3), (str.Length - (str.IndexOf(',') + 3))));
            }

            return rounded;
        }


        public static int CalcularNumeroPontos(TGIS_Shape shape)
        {
            Double pontos = ((shape.Area() / 10000) / 5);

            return (pontos <= 1) ? 1 : (int)pontos;
        }

    }
}
