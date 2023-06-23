using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Services;

namespace Sigma.App.AppService
{
    public class FormulacaoAduboAppService: AppService<FormulacaoAdubo>, IFormulacaoAduboAppService
    {
        private readonly IFormulacaoAduboService _Service;
        public FormulacaoAduboAppService(IFormulacaoAduboService service)
            :base(service)
        {
            _Service = service; 
        }
    }
}
