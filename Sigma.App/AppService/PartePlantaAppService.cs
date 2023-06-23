using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class PartePlantaAppService: AppService<PartePlanta>, IPartePlantaAppService
    {
        private readonly IPartePlantaService _Service;
        public PartePlantaAppService(IPartePlantaService service)
            :base(service)
        {
            _Service = service; 
        }
        public IEnumerable<PartePlanta> GetPartePlantaByCultura(Guid iDCultura)
        {
            return _Service.GetPartePlantaByCultura(iDCultura);
        }
    }
}
