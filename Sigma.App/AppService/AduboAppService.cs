using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Services;

namespace Sigma.App.AppService
{
    public class AduboAppService: AppService<Adubo>, IAduboAppService
    {
        private readonly IAduboService _AduboService;

        public AduboAppService(IAduboService aduboService)
            : base(aduboService)
        {
            _AduboService = aduboService;
        }
    }
}
