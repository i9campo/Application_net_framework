using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;

namespace Sigma.App.AppService
{
    public class ImagemAppService : AppService<Imagem>, IImagemAppService
    {
        private readonly IImagemService _Service;
        public ImagemAppService(IImagemService service)
            :base(service)
        {

        }
    }
}
