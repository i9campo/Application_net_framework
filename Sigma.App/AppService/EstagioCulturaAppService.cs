using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class EstagioCulturaAppService : AppService<EstagioCultura>, IEstagioCulturaAppService
    {
        private readonly IEstagioCulturaService _Service;
        public EstagioCulturaAppService(IEstagioCulturaService service)
            :base(service)
        {
            _Service = service; 
        }
        public IEnumerable<EstagioCultura> GetEstagioByCultura(Guid IDCultura)
        {
            return _Service.GetEstagioByCultura(IDCultura);
        }
    }
}
