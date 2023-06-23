using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class CulturaAppService: AppService<Cultura>, ICulturaAppService
    {
        private readonly ICulturaService _Service;
        public CulturaAppService(ICulturaService service)
            :base(service)
        {
            _Service = service; 
        }
        public IEnumerable<Cultura> ByName(string name)
        {
            return _Service.ByName(name);
        }

        public CulturaView FindCultura(Guid objID)
        {
            return _Service.FindCultura(objID);
        }

        public IEnumerable<CulturaView> GetAllCultura()
        {
            return _Service.GetAllCultura();
        }
    }
}
