using FluentValidation.Results;
using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sigma.App.AppService
{
    public class ImagemSateliteAppService : AppService<ImagemSatelite>, IImagemSateliteAppService
    {
        private readonly IimagemSateliteService _Service;

        public ImagemSateliteAppService(IimagemSateliteService service)        
            :base(service)
        {
            _Service = service; 
        }

        public ImagemSateliteView GetGeometry(string geocoord)
        {
            return _Service.GetGeometry(geocoord);
        }

        public bool UpdateCoordsImg(string geoString, Guid objID)
        {
           return _Service.UpdateCoordsImg(geoString, objID);
        }
        public async Task<bool> PostTiff(TiffPost obj)
        {
            return await _Service.PostTiff(obj); 
        }

        public async Task<TiffImage> GenerateNDVI(Guid IDFolder, string coordinates, IEnumerable<string> banda)
        {
            return await _Service.GenerateNDVI(IDFolder, coordinates, banda); 
        }

        public List<ImagemSatelite> GetListGeoIMGS(string coord)
        {
            return _Service.GetListGeoIMGS(coord); 
        }

        public Task<TiffImage> GenerateSplitImage(SplitImage obj)
        {
            return _Service.GenerateSplitImage(obj); 
        }
    }
}
