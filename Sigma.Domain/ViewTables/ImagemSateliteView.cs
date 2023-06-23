using Newtonsoft.Json;
using System;
using System.Data.Entity.Spatial;
namespace Sigma.Domain.ViewTables
{
    public class ImagemSateliteView
    {
        public Guid objID { get; set; }
        public string banda { get; set; }
        public string satelite { get; set; }
        public bool visualizar { get; set; }
        public string dateIMG { get; set; }
        public string geoJson { get; set; }
        public string tipoIMG { get; set; }
        public string extension { get; set; }

        public decimal? tamanhoPX { get; set; }

        public int? ano { get; set; }

        //public byte[]  imagem { get; set; }
        public float[] coords { get; set; }

        public bool? Intersects { get; set; }


        [JsonIgnore]
        public DbGeometry geo { get; set; }

        public byte[] IMG { get; set; }
    }

    public class TiffImage{
        public TiffImage()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public string name { get; set; }
        public Byte[] ImagemByte { get; set; }
        public Byte[] FileJGW { get; set; }
    }
    
    public class GenerateNDVI
    {
        public Guid objID { get; set; }
        public string coord { get; set; }
        public string banda { get; set; }
    }

    public class SplitImage
    {
        public Guid objID { get; set; }
        public string coord { get; set; }
    }


    public class UpdateImg { 
        public string geoString { get; set; }
       public Guid objID { get; set; }
    }
    public class ImgPost
    {
        public Guid objID { get; set; }
        public string satelite { get; set; }
        public string data { get; set; }
        public string banda { get; set; }

        public string orbita { get; set; }
        public Byte[] FileIMG { get; set; }
        public string GeoString { get; set; }
        public string extension { get; set; }
        public int index { get; set; }
        public int QTD { get; set; }
    }
    public class TiffPost
    {
        public Guid objID { get; set; }
        public int orbita { get; set; }
        public string name { get; set; }
        public string data { get; set; }
        public string banda { get; set; }
        public string landsat { get; set; }
        public string base64str { get; set; }
        public Byte[] TiffFile { get; set; }
        public string ext { get; set; }
        public int index { get; set; }
        public int QTD { get; set; }
    }
    public class ImgGet{ public string Coord { get; set; } }

}
