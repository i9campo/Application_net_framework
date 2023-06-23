using Sigma.App.Interfaces;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;
using WEBAPI.App_Start;
using static Sigma.Domain.ViewTables.OpenGeo;
namespace WEBAPI
{
    [AllowedOriginFilter]
    public class ShapeController : ApiController
    {
        public int numorbita { get; set; }
        public String link { get; set; }
        private readonly IShapeAppService _shapeAppService;
        private readonly IGeoConfigurationAppService _geoConfigAppService; 
        public List<GeoCoordinates> CoordTeste { get; set; }


        public ShapeController(IShapeAppService shapeAppService, IGeoConfigurationAppService geoConfigAppService)
        {
            _shapeAppService = shapeAppService;
            _geoConfigAppService = geoConfigAppService; 
        }

        [HttpGet]
        [ActionName("OpenGeoByAreaServico")]
        [Route("api/shape/OpenGeoByAreaServico")]
        public IEnumerable<GeoOBJ> OpenGeoByAreaServico(Guid ID, Guid IDAreaServico, string tipo)
        {
            return _shapeAppService.OpenGeoByAreaServico(ID, IDAreaServico, tipo); 
        }

        [HttpPut]
        [ActionName("GetFileByIDShape")]
        [Route("api/shape/GetFileByIDShape/{orbita}")]
        public IEnumerable<IEnumerable<FileExt>> GetFileByIDShape(int orbita, [FromBody]IEnumerable<Guid> IDShape)
        {
            List<string> teste = new List<string>();
            foreach (var item in IDShape)
            {
                teste.Add(item.ToString()); 
            }
            return _shapeAppService.GetFileByIDShape(teste, orbita);
        }

        [HttpGet]
        [ActionName("GetListShapeByAreaServico")]
        [Route("api/shape/GetListShapeByAreaServico")]
        public IEnumerable<BNG_Shape> GetListShapeByAreaServico(Guid IDSafra, Guid IDArea)
        {
            return _shapeAppService.GetListShapeByAreaServico(IDSafra, IDArea);
        }

        [HttpPost]
        [ActionName("shpcreatefileteste")]
        [Route("api/shape/shpcreatefile")]
        public byte[] ShpCreateFile(IEnumerable<GeoCoordText> obj)
        {
            return _shapeAppService.ShpCreateFile(obj);
        }

        [HttpPost]
        [ActionName("ExportSHPToBNG")]
        [Route("api/shape/ExportSHPToBNG")]
        public bool ExportSHPToBNG(IEnumerable<ImportShapeToBNG> obj)
        {
            return _shapeAppService.ExportSHPToBNG(obj); 
        }

        [HttpPut]
        [ActionName("OpenGeoSHP")]
        [Route("api/shape/OpenGeoSHP/{Orbita}")]
        public IEnumerable<FileExt> OpenGeoSHP(int Orbita, File64 file)
        {
            return _shapeAppService.OpenGeoSHP(Orbita, file);
        }

        [HttpPut]
        [ActionName("decodeGPXFILE")]
        [Route("api/shape/decodeGPXFILE/{Type}")]
        public String[] DecodeGPXFILE(String Type, GpxShp file)
        {
            Double number;
            file.fileText = file.fileText.Split(',')[1];

            string[] array = new string[] { file.fileText };
            
            List<string> ArrayFinal = new List<string>();

            string converted = file.fileText.Replace('-', '+');
            converted = converted.Replace('_', '/');

            byte[] data = Convert.FromBase64String(converted);

            string decodedString = System.Text.Encoding.UTF8.GetString(data);


            ArrayFinal.Add(decodedString);
            array = ArrayFinal.ToArray();

            return array;
        }

        [HttpPost]
        [ActionName("KMLGenerate")]
        [Route("api/shape/KMLGenerate")]
        public byte[] KMLGenerate(IEnumerable<KMLShape> obj)
        {
            return _shapeAppService.KMLGenerate(obj);
        }


        [HttpPut]
        [ActionName("GetKMLFile")]
        [Route("api/shape/GetKMLFile/{Type}")]
        public IEnumerable<KMLShape> GetKMLFile(String Type, teste obj)
        {
            return _shapeAppService.GetKMLFile(obj.text); 
        }
    }
}