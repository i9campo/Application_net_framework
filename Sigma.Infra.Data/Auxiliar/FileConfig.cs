using System;
using System.IO;
using System.Web;

namespace Sigma.Infra.Data.Auxiliar
{
    public class FileConfig
    {

        /// <summary>
        /// <para>Retorna o caminho do arquivo SHP. </para>
        /// <para>Necessário passar como parâmetro o nome do arquivo. </para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFilePath(string fileName)
        {
            return Path.Combine(HttpContext.Current.Server.MapPath("~/Shapes/"), fileName);
        }

        /// <summary>
        /// <para>Método utilizado para salvar o arquivo SHP temporariamente no servidor. </para>
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="fileName"></param>
        public static void SaveFileSHP(Byte[] byteArray, string fileName)
        {
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Shapes/"), fileName);
            
            // Aqui será feito uma verificação se o arquivo existe, 
            // para não duplicar o arquivo, ele será removido depois inserido novamente. 
            // Com o objetivo de atualizar as informações do arquivo. 
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Caso o arquivo não exista ou foi removido ele será adicionado na pasta temporaria. 
            if (!File.Exists(filePath))
            {
                FileStream stream = File.Create(filePath, byteArray.Length);
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Close();
            }
        }

        /// <summary>
        /// <para>Método utilizado para remover o arquivo SHP da pasta temporária. </para>
        /// <para>Necessário passar como parâmetro o nome do arquivo SHP. </para>
        /// </summary>
        /// <param name="name"></param>
        public static void RemoveFileSHP(String name)
        {
            Remove(name + ".shp");
            Remove(name + ".dbf");
        }

        /// <summary>
        /// <para>Método complementar ao RemoveFileSHP. </para>
        /// </summary>
        /// <param name="fileName"></param>
        public static void Remove(string fileName)
        {
            try
            {
                string file = Path.Combine(HttpContext.Current.Server.MapPath("~/Shapes/"), fileName);
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            catch (Exception ex) { }
        }
    }
}
