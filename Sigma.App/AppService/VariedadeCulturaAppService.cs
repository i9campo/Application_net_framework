using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class VariedadeCulturaAppService: AppService<VariedadeCultura>, IVariedadeCulturaAppService
    {
        private readonly IVariedadeCulturaService _Service;
        public VariedadeCulturaAppService(IVariedadeCulturaService service)
            :base(service)
        {
            _Service = service; 
        }
        public IEnumerable<VariedadeCultura> GetVariedadeCulturaByCultura(Guid IDCultura)
        {
            return _Service.GetVariedadeCulturaByCultura(IDCultura);
        }
    }
}
