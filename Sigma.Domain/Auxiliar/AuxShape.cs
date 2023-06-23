using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
namespace Sigma.Domain.Auxiliar
{
    public static class AuxShape
    {
        /// <summary> Este Método será utilizado para remover os arquivos zipado e pastas temporárias. </summary>
        /// <param name="ShapeFilePath"></param>
        public static void DeleteFiles(string PathFile)
        {
            //// Este comando vai capturar todos os arquivos que contém na pasta que foi especificada no caminho PathFile. 
            var ExcludeFiles = Directory.GetFiles(PathFile);
            foreach (var item in ExcludeFiles)
            {
                bool IsFileLocked = false;

                FileInfo fl = new FileInfo(item);
                fl.Refresh(); 

                FileStream stream = null;
                try
                {
                    stream = fl.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                }
                catch (IOException)
                {
                    IsFileLocked = true;
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }

                FileInfo Fl = new FileInfo(item);
                //while (IsFileLocked)
                    //Thread.Sleep(1000);
                Fl.Delete();
            }

            //// Este comando será utilizado para remover a pasta temporária. 
            Directory.Delete(PathFile);
        }
    }
}