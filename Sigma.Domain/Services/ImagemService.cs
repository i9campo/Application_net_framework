using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
namespace Sigma.Domain.Services
{
    public class ImagemService : Service<Imagem>, IImagemService
    {
        private readonly IIMagemRepository _ImagemRepository;
        public ImagemService(IIMagemRepository _Repository)
            : base(_Repository)
        {
            _ImagemRepository = _Repository;
        }
    }
}
