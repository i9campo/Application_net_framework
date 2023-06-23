using ConectionPath.ClassConection;
using FluentValidation.Results;
using Newtonsoft.Json;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Sigma.Domain.Services
{
    public class ImagemSateliteService : Service<ImagemSatelite>, IimagemSateliteService
    {
        private readonly IimagemSateliteRepository _repository;
        private readonly IGeoConfigRepository _geoConfigRepository;
        private readonly ISaveTemporaryImgByteRepository _saveTemporaryImg;
        public ImagemSateliteService(IimagemSateliteRepository repository, IGeoConfigRepository geoConfigRepository, ISaveTemporaryImgByteRepository saveTemporaryImg)
            : base(repository)
        {
            _repository = repository;
            _geoConfigRepository = geoConfigRepository;
            _saveTemporaryImg = saveTemporaryImg;
        }
        public ImagemSateliteView GetGeometry(string geocoord)
        {
            return _repository.GetGeometry(geocoord);
        }
        public bool UpdateCoordsImg(string geoString, Guid objID)
        {
            return _repository.UpdateCoordsImg(geoString, objID);
        }
        public async Task<bool> PostTiff(TiffPost obj)
        {
            if (obj == null)
                return false;

            Guid IDReferenteIMG = Guid.NewGuid();

            SaveTemporaryImgByte byteTemporary = new SaveTemporaryImgByte();
            byteTemporary.objID = Guid.NewGuid();
            byteTemporary.ConjuntoByteID = obj.objID;
            byteTemporary.idx = (obj.index + 1);
            byteTemporary.bts = obj.TiffFile;

            _saveTemporaryImg.Add(byteTemporary);

            ImagemSatelite objeto = _repository.Find(obj.objID);
            //var split_bands = obj.banda.Split('_');
            string[] split_bands = new string[obj.banda.Length];

            for (int i = 0; i < obj.banda.Length; i++)
            {
                split_bands[i] = obj.banda[i].ToString();
            }


            if (objeto == null)
            {
                ImagemSatelite ImgSave = new ImagemSatelite();
                ImgSave.objID = obj.objID;
                ImgSave.banda = split_bands[0] + split_bands[1] + split_bands[2];
                ImgSave.dateIMG = obj.data;
                ImgSave.orbita = obj.orbita.ToString();
                ImgSave.satelite = obj.landsat;
                ImgSave.extension = "PNG";
                ImgSave.visualizar = true;
                try
                {
                    _repository.Add(ImgSave);
                }
                catch (Exception ex)
                {
                    return false;

                }
            }
            if (((obj.index) + 1)  == obj.QTD)
            {
                List<byte[]> lstBytes = new List<byte[]>();

                IEnumerable<SaveTemporaryImgByte> lstSaveTemp = _saveTemporaryImg.GetListByConjuntID(obj.objID);
                foreach (var bt in lstSaveTemp)
                {
                    if (bt.bts != null)
                        lstBytes.Add(bt.bts);
                }

                Byte[] BMPFile = lstBytes.SelectMany(a => a).ToArray();

                /// Este processo encerra o armazenamento de dados temporario ao banco de dados. 
                _saveTemporaryImg.RemoveAllBytesByID(obj.objID);
                string ServerPath = ConectionPathTiff.url_path_tiff + obj.objID;

                // Verifique se a pasta não existe antes de criá-la
                if (!Directory.Exists(ServerPath))
                    Directory.CreateDirectory(ServerPath);

                // Adicione a extensão ".tiff" ao nome do arquivo
                string tiffFileName = Path.ChangeExtension(obj.name, "tiff");

                // Construa o caminho completo do arquivo, incluindo a pasta e o nome do arquivo
                string tiffFilePath = Path.Combine(ServerPath, tiffFileName);

                // Salve os bytes TIFF em um arquivo
                File.WriteAllBytes(tiffFilePath, BMPFile);

                string B1 = Path.Combine(ServerPath, "B" + split_bands[0] +".TIFF");
                string B2 = Path.Combine(ServerPath, "B" + split_bands[1] +".TIFF");
                string B3 = Path.Combine(ServerPath, "B" + split_bands[2] +".TIFF");
                string TrueColor = Path.Combine(ServerPath, "432-"+ obj.orbita.ToString() + "-"+ obj.data + "-"+ obj.landsat + ".png");
                if (File.Exists(B1) && File.Exists(B2) && File.Exists(B3) && !File.Exists(TrueColor))
                {
                    await TrueColorBands(obj, obj.objID);
                    return true; 
                }
            }
            return true; 
        }
        public async Task<TiffImage> GenerateSplitImage(SplitImage obj)
        {
            TiffImage tf = new TiffImage();

            ImagemSatelite objeto = _repository.Find(obj.objID); 
            var split = await SplitTiffColor(objeto.objID, obj.coord, objeto.orbita.ToString(), objeto.dateIMG.ToString());
            if (split)
            {
                string ServerPath = ConectionPathTiff.url_path_tiff + objeto.objID;

                string file = "CORTE_COLOR-" + objeto.orbita + "-" + objeto.dateIMG ;
                string[] png = Directory.GetFiles(ServerPath, file + ".png");
                string[] jgw = Directory.GetFiles(ServerPath, file + ".jgw");

                tf.objID = objeto.objID;
                tf.name = Path.GetFileName(png[0]);
                tf.ImagemByte = PNGToByteArray(png[0]);
                tf.FileJGW = JGWToByteArray(jgw[0]);
            }
            return tf; 
        }
        public async Task<TiffImage> GenerateNDVI(Guid IDFolder, string coordinates, IEnumerable<string> banda)
        {
            TiffImage tf = new TiffImage();

            ImagemSatelite objeto = _repository.Find(IDFolder);

            foreach (var bd in banda)
            {
                await SplitTiff(IDFolder, coordinates, bd, objeto.orbita.ToString());
            }

            List<string> orderBand = banda.ToList().OrderBy(x => x).ToList();
            string b1 = orderBand[0]; 
            string b2 = orderBand[1];

            string nome = b1 + b2 + "-" + objeto.orbita.ToString() + "-" + objeto.dateIMG.ToString() + "-NDVI";
            bool result = await JoinTwoBand(IDFolder, objeto.orbita, b1, b2, nome); 
            if (result)
            {
                string png = ConectionPathTiff.url_path_tiff + IDFolder + "\\" + nome + ".png";
                string jgw = ConectionPathTiff.url_path_tiff + IDFolder + "\\" + nome + ".jgw";

                tf.name = Path.GetFileName(nome);
                tf.ImagemByte = PNGToByteArray(png);
                tf.FileJGW = JGWToByteArray(jgw);


                // caminho completo para o arquivo
                string SPLIT_B1 = ConectionPathTiff.url_path_tiff + IDFolder + "\\SPLIT_" + b1 + ".TIFF";
                if (File.Exists(SPLIT_B1))
                    File.Delete(SPLIT_B1);

                // caminho completo para o arquivo
                string SPLIT_B2 = ConectionPathTiff.url_path_tiff + IDFolder + "\\SPLIT_" + b2 + ".TIFF";
                if (File.Exists(SPLIT_B2))
                    File.Delete(SPLIT_B2);

                // caminho completo para o arquivo
                string JOIN_TWO = ConectionPathTiff.url_path_tiff + IDFolder + "\\JOIN_TWO_BAND.TIFF";
                if (File.Exists(JOIN_TWO))
                    File.Delete(JOIN_TWO);
            }

            return tf; 
        }
        public async Task<bool> TrueColorBands(TiffPost obj, Guid IDFolder)
        {

            string[] split_bands = new string[obj.banda.Length];

            for (int i = 0; i < obj.banda.Length; i++)
            {
                split_bands[i] = obj.banda[i].ToString();
            }
            string bds = split_bands[0] + "_" + split_bands[1] + "_" + split_bands[2];

            var objeto = new { IDFolder = IDFolder.ToString(), orbita = obj.orbita.ToString(), dataImg = obj.data, satelite = obj.landsat, banda = bds};
            var json = JsonConvert.SerializeObject(objeto);

            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(720);

                var response = await client.PostAsync(ConectionApiPython.Convertimg_tiff, conteudo);
                response.Content.ReadAsStringAsync().Wait();
                if (response.IsSuccessStatusCode)
                {
                    return true; 
                }
            }

            return false;
        }
        public async Task<bool> SplitTiff(Guid objID, string coordinates, string banda, string orb)
        {
            var objeto = new { Coordinates = coordinates, IDFolder = objID, Banda = banda, orbita = orb  };
            var json = JsonConvert.SerializeObject(objeto);

            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(720);

                var response = await client.PostAsync(ConectionApiPython.Splitt_tiff, conteudo);
                response.Content.ReadAsStringAsync().Wait();
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }

            return false;
        }
        public async Task<bool> SplitTiffColor(Guid IDFolder, string coordinates, string orb, string dt)
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
        public async Task<bool> JoinTwoBand(Guid IDFolder, string orbita, string band1, string banda2, string nameFile)
        {
            var objeto = new { IDFolder = IDFolder.ToString(), orbita = orbita.ToString(), bd1 = band1, bd2 = banda2, nome = nameFile };
            var json = JsonConvert.SerializeObject(objeto);

            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(720);
                var response = await client.PostAsync(ConectionApiPython.MergeTwoBand, conteudo);
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
        public List<ImagemSatelite> GetListGeoIMGS(string coord)
        {
            return _repository.GetListGeoIMGS(coord); 
        }
    }
}
