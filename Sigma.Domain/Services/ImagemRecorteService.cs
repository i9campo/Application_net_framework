using ConectionPath.ClassConection;
using Newtonsoft.Json;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Domain.Services
{
    public class ImagemRecorteService : Service<ImagemSateliteRecortada>, IImagemRecorteService
    {
        private readonly IImagemRecorteRepository _repository;
        private readonly IimagemSateliteRepository _imagemRepository; 
        public ImagemRecorteService(IImagemRecorteRepository repository, IimagemSateliteRepository imagemRepository)
            : base(repository)
        {
            _repository = repository;
            _imagemRepository = imagemRepository;   
        }
        public async Task<TiffImage> GenerateCropImage(SplitImage obj)
        {
            TiffImage tf = new TiffImage();

            ImagemSatelite objeto = _imagemRepository.Find(obj.objID);

            var split = await CropImageTiff(objeto.objID, obj.coord, objeto.orbita.ToString(), objeto.dateIMG.ToString());
            if (split)
            {
                string ServerPath = ConectionPathTiff.url_path_tiff + objeto.objID;

                string file = "CORTE_COLOR-" + objeto.orbita + "-" + objeto.dateIMG;
                string[] png = Directory.GetFiles(ServerPath, file + ".png");
                string[] jgw = Directory.GetFiles(ServerPath, file + ".jgw");

                tf.objID = objeto.objID;
                tf.name = Path.GetFileName(png[0]);
                tf.ImagemByte = PNGToByteArray(png[0]);
                tf.FileJGW = JGWToByteArray(jgw[0]);
            }
            return tf;

        }
        public async Task<bool> CropImageTiff(Guid IDFolder, string coordinates, string orb, string dt)
        {
            var objeto = new { IDFolder = IDFolder.ToString(), Coordinates = coordinates.ToString(), orbita = orb, ano = dt };
            var json = JsonConvert.SerializeObject(objeto);
            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(720);
                var response = await client.PostAsync(ConectionApiPython.Splitt_color, conteudo);
                response.Content.ReadAsStringAsync().Wait();
                if (response.IsSuccessStatusCode)
                    return true;
            }

            return false;
        }
        private byte[] PNGToByteArray(string img_path)
        {
            using (Image image = Image.FromFile(img_path))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
        private byte[] JGWToByteArray(string jgw_path)
        {
            using (FileStream fs = new FileStream(jgw_path, FileMode.Open, FileAccess.Read))
            {
                byte[] byteArray = new byte[fs.Length];
                fs.Read(byteArray, 0, byteArray.Length);
                return byteArray;
            }
        }
    }
}
