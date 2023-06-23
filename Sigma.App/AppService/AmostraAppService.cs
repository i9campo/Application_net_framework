using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class AmostraAppService :  AppService<Amostra>, IAmostraAppService
    {
        private readonly IAmostraService _Service;
        public AmostraAppService(IAmostraService service)
            : base(service)
        {
            _Service = service;
        }

        public IEnumerable<Amostra> GetByCultura(Guid IDCultura, int? mediaT)
        {
            return _Service.GetByCultura(IDCultura, mediaT); 
        }
    }
}
