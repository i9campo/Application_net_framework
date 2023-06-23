using Sigma.App.AppService;
using Sigma.App.Interfaces;
using Sigma.Domain.ViewTables;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI.Controllers
{
    [AllowedOriginFilter]
    public class ImagemRecorteController : ApiController
    {
        private readonly IImagemRecorteAppService _imagemRecorteAppService;
        public ImagemRecorteController(IImagemRecorteAppService imagemRecorteAppService)
        {
            _imagemRecorteAppService = imagemRecorteAppService; 
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        /**
         * POST: api/imagemsatelite/CropImage
         * 
         * This method is used to crop an image based on the provided SplitImage object.
         * It returns the cropped TiffImage.
         * 
         * Parameters:
         *     obj (SplitImage): The SplitImage object containing the necessary information for cropping the image.
         * 
         * Returns:
         *     Task<TiffImage>: The cropped TiffImage.
         */
        [HttpPost]
        [ActionName("CropImage")]
        [Route("api/imagemsatelite/CropImage")]
        public async Task<TiffImage> CropImage(SplitImage obj)
        {
            // Create a new instance of TiffImage
            TiffImage img = new TiffImage();

            // Generate the split image using the _imagemRecorteAppService
            img = await _imagemRecorteAppService.GenerateCropImage(obj);

            // Return the cropped image
            return img;
        }


        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}