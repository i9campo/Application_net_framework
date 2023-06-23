using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class NivelSoloAppService : AppService<NivelSolo>, INivelSoloAppService
    {
        private readonly INivelSoloService _Service;
        public NivelSoloAppService(INivelSoloService service)
            :base(service)
        {
            _Service = service; 
        }
        public IEnumerable<NivelSolo> GetNivelByCultura(Guid IDCultura)
        {
            return _Service.GetNivelByCultura(IDCultura);
        }

        public IEnumerable<NivelSolo> GetNivelByElemento(Guid IDCultura, string elemento)
        {
            return _Service.GetNivelByElemento(IDCultura, elemento);
        }
    }
}
