using ConectionPath.ClassConection;
using FluentValidation.Results;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class ImagemSateliteController : ApiController
    {
        private readonly IImagemSateliteAppService _imagemSateliteAppService;
        private readonly IGeoConfigurationAppService _geoConfigurationAppService;
        private static readonly ImageConverter _imageConverter = new ImageConverter();

        public ImagemSateliteController(IImagemSateliteAppService imagemSatelite,  IGeoConfigurationAppService geoConfiguration)
        {
            _imagemSateliteAppService = imagemSatelite;
            _geoConfigurationAppService = geoConfiguration;
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
        public static byte[] RedimensionarImagem(string caminhoImagemOriginal, int novaLargura, int tamanhoMaximo)
        {
            byte[] bytesImagemRedimensionada;

            using (Image imagemOriginal = Image.FromFile(caminhoImagemOriginal))
            {
                int novaAltura = (int)Math.Round(imagemOriginal.Height * (decimal)novaLargura / imagemOriginal.Width);

                using (Bitmap novaImagem = new Bitmap(novaLargura, novaAltura))
                {
                    novaImagem.SetResolution(imagemOriginal.HorizontalResolution, imagemOriginal.VerticalResolution);

                    using (Graphics g = Graphics.FromImage(novaImagem))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(imagemOriginal, 0, 0, novaLargura, novaAltura);
                    }

                    using (MemoryStream ms = new MemoryStream())
                    {
                        ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                        EncoderParameters encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 50L);

                        novaImagem.Save(ms, jpgEncoder, encoderParameters);

                        bytesImagemRedimensionada = ms.ToArray();
                    }
                }
            }

            // Redimensionar a imagem até que o tamanho seja inferior ao tamanho máximo
            //while (bytesImagemRedimensionada.Length > tamanhoMaximo)
            //{
            //    novaLargura = (int)Math.Round(novaLargura * Math.Sqrt((double)tamanhoMaximo / bytesImagemRedimensionada.Length));
            //    bytesImagemRedimensionada = RedimensionarImagem(caminhoImagemOriginal, novaLargura, tamanhoMaximo);
            //}

            return bytesImagemRedimensionada;
        }

        [HttpGet]
        [ActionName("GetListBandasExist")]
        [Route("api/imagemsatelite/GetListBandasExist")]
        public IEnumerable<string> GetListBandasExist(Guid IDImage)
        {
            string ServerPath = ConectionPathTiff.url_path_tiff + IDImage;

            // Obtém a lista de todos os arquivos no diretório especificado
            string[] arquivos = Directory.GetFiles(ServerPath);

            List<string> files_names = new List<string>(); 

            // Exibe o nome de cada arquivo na lista
            foreach (string arquivo in arquivos)
            {
                string nomeArquivo = Path.GetFileName(arquivo).ToUpper();
                for (int i = 1; i < 13; i++)
                {
                    string file = "B";
                    if (i < 10)
                        file = file + i.ToString() + ".TIFF";

                    if (i == 10)
                        file = file + "11" + ".TIFF";

                    if (i == 11)
                        file = file + "12" + ".TIFF";

                    if (i == 12)
                        file = file + "QA" + ".TIFF";

                    if (nomeArquivo.Contains(file))
                    {
                        files_names.Add(file.Split('.')[0]);
                        break; 
                    }
                }
            }

            return files_names; 
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        [HttpGet]
        [ActionName("GetImagemPNG")]
        [Route("api/imagemsatelite/GetImagemPNG")]
        public TiffImage GetImagemPNG(Guid objID)
        {
            TiffImage img = new TiffImage();
            string ServerPath = ConectionPathTiff.url_path_tiff + objID;

            string[] jpg = Directory.GetFiles(ServerPath, "*.jpg");
            string[] jgw = Directory.GetFiles(ServerPath, "*.jgw");

            img.name = Path.GetFileName(jpg[0]); 
            img.ImagemByte = PNGToByteArray(jpg[0]);
            img.FileJGW = JGWToByteArray(jgw[0]);
            return img; 
        }

        [HttpPost]
        [ActionName("GetLstImgs")]
        [Route("api/imagemsatelite/GetLstImgs")]
        public IEnumerable<TiffImage> GetLstImgs(SplitImage obj)
        {
            List<TiffImage> lst_imgs = new List<TiffImage>();
            List<ImagemSatelite> lst = _imagemSateliteAppService.GetListGeoIMGS(obj.coord);
            for (int i = 0; i < lst.Count(); i++)
            {
                TiffImage img = new TiffImage(); 
                string ServerPath = ConectionPathTiff.url_path_tiff + lst[i].objID;

                string file = lst[i].banda + "-" + lst[i].orbita + "-" + lst[i].dateIMG + "-" + lst[i].satelite + "-TrueColor"; 

                string[] jpg = Directory.GetFiles(ServerPath, file + ".jpg");
                string[] jgw = Directory.GetFiles(ServerPath, file + "*.jgw");

                img.objID = lst[i].objID;
                img.name = Path.GetFileName(jpg[0]);
                img.ImagemByte = RedimensionarImagem(jpg[0], 120, 2);
                 
                img.FileJGW = JGWToByteArray(jgw[0]);
                lst_imgs.Add(img);
            }

            return lst_imgs;
        }

        [HttpPost]
        [ActionName("GenerateNDVI")]
        [Route("api/imagemsatelite/GenerateNDVI")]
        public async Task<TiffImage> GenerateNDVI(GenerateNDVI obj)
        {
            TiffImage img = new TiffImage();

            List<string> bd = obj.banda.Split('_').ToList();
            img = await _imagemSateliteAppService.GenerateNDVI(obj.objID, obj.coord, bd);
            return img; 
        }

        [HttpPost]
        [ActionName("GenerateSplitImage")]
        [Route("api/imagemsatelite/GenerateSplitImage")]
        public async Task<TiffImage> GenerateSplitImage(SplitImage obj)
        {
            TiffImage img = new TiffImage();
            img = await _imagemSateliteAppService.GenerateSplitImage(obj);
            return img;
        }


        [HttpPut]
        [ActionName("UpdateCoorddinatesImagem")]
        [Route("api/imagemsatelite/UpdateCoorddinatesImagem/{objID}")]
        public ValidationResult UpdateCoordinatesImagem(Guid objID, ImgGet obj)
        {
            ImagemSatelite item = _imagemSateliteAppService.Find(objID);
            if (item == null)
                return null; 


            item.polyIMG = _geoConfigurationAppService.GetGeoPolygon(obj.Coord); 
            return _imagemSateliteAppService.Update(item); 
        }

        [HttpPost]
        [ActionName("PostTiff")]
        [Route("api/imagemsatelite/PostTiff")]
        public async Task<bool> PostTiff(TiffPost item)
        {
            return await _imagemSateliteAppService.PostTiff(item); 
        }

        // PUT api/<controller>/5
        [HttpGet]
        [ActionName("updateval")]
        [Route("api/imagemsatelite/updateval")]
        public ImagemSateliteView Put(Guid objID, [FromUri] string[] coords)
        {

            return null;

            List<List<Double>> newcoord = new List<List<double>>();

            for (int i = 0; i < coords.Length; i++)
            {
                List<Double> coordsList = new List<Double>();

                Double y = Double.Parse(coords[i].Replace("[", " ").Replace("]", " ").Split(',')[0].ToString().Replace('.', ','));
                Double x = Double.Parse(coords[i].Replace("[", " ").Replace("]", " ").Split(',')[1].ToString().Replace('.', ','));

                coordsList.Add(x);
                coordsList.Add(y);


                newcoord.Add(coordsList);
            }

            string geojson = "";
            int cont = 0;
            for (int i = 0; i < newcoord.Count; i++)
            {
                if (i == 0)
                {
                    geojson = "POLYGON (( " + newcoord[i][0].ToString().Replace(',', '.') + " " + newcoord[i][1].ToString().Replace(',', '.') + " , ";
                }
                if (i > 0 && (cont + 1) < newcoord.Count)
                {
                    geojson += newcoord[i][0].ToString().Replace(',', '.') + " " + newcoord[i][1].ToString().Replace(',', '.') + " , ";
                }
                else if ((cont + 1) >= newcoord.Count)
                {
                    geojson += newcoord[i][0].ToString().Replace(',', '.') + " " + newcoord[i][1].ToString().Replace(',', '.') + " )) ";
                }
                cont += 1;
            }
            geojson = geojson.ToString();

            ImagemSatelite imgObject = _imagemSateliteAppService.Find(objID);
            //imgObject.geo = _imagemSateliteAppService.GetGeometry(geojson).geometrico;

            ImagemSateliteView oImagem = new ImagemSateliteView();
            //oImagem.Resultado = _imagemSateliteAppService.Update(imgObject);

            return oImagem;
        }

        public ValidationResult Put(Guid objID)
        {
            ImagemSatelite imgObject = _imagemSateliteAppService.Find(objID);
            imgObject.visualizar = true;
            return _imagemSateliteAppService.Update(imgObject);
        }

        [HttpPost]
        [ActionName("UpdateCoordsImg")]
        [Route("api/imagemsatelite/UpdateCoordsImg/")]
        public bool UpdateCoordsImg ( UpdateImg obj)
        {
            return _imagemSateliteAppService.UpdateCoordsImg(obj.geoString, obj.objID);
        }

        // DELETE api/<controller>/5
        public ValidationResult Delete(Guid objID)
        {
            ImagemSatelite obj = _imagemSateliteAppService.Find(objID);
            return _imagemSateliteAppService.Remove(obj);
        }
    }
}