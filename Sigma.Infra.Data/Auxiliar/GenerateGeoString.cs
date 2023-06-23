using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Infra.Data.Auxiliar
{
    public class GenerateGeoString
    {
        /// <summary>
        /// <para>Método utilizado para gerar as coordenadas ascendentes, coordenadas em Texto "POLYGON((.... ".</para>
        /// </summary>
        /// <param name="ncord"></param>
        /// <returns></returns>
        public static string PolyAscending(List<string> ncord)
        {
            // Método utilizado para ordernar as cordenadas de forma ascendente. 
            string coords = "";
            for (int u = 0; u <= ncord.Count - 1; u++)
            {
                coords += ncord[u] + ", ";
            }
            coords = coords.Remove(coords.Length - 2, 2);
            return "POLYGON((" + coords + "))";
        }

        /// <summary>
        /// <para>Método utilizado para gerar as coordenadas descendentes, coordenadas em Texto "POLYGON((.... ".</para>
        /// </summary>
        /// <param name="ncord"></param>
        /// <returns></returns>
        public static string PolyDescending(List<string> ncord)
        {
            // Método utilziado para ordernar as cordenadas de forma descendente. 
            string fPoint = ncord[0];
            string lPoint = ncord[ncord.Count - 1];

            string coords = "";
            for (int u = ncord.Count - 1; u >= 0; u--)
            {
                coords += ncord[u] + ", ";
            }
            coords = coords.Remove(coords.Length - 2, 2);
            return "POLYGON((" + coords + "))";
        }
    }
}
