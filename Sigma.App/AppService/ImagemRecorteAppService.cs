using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System.Threading.Tasks;

namespace Sigma.App.AppService
{
    public class ImagemRecorteAppService : AppService<ImagemSateliteRecortada>,  IImagemRecorteAppService
    {
        private readonly IImagemRecorteService _imagemRecorteService;
        public ImagemRecorteAppService(IImagemRecorteService service)
            : base(service) 
        {
            _imagemRecorteService = service;
        }

        public Task<TiffImage> GenerateCropImage(SplitImage obj)
        {
            return _imagemRecorteService.GenerateCropImage(obj);
        }
    }
}
