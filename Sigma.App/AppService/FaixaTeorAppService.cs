using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class FaixaTeorAppService : AppService<FaixaTeor>, IFaixaTeorAppService
    {
        private readonly IFaixaTeorService _Service;
        public FaixaTeorAppService(IFaixaTeorService service)
            :base(service)
        {
            _Service = service; 
        }

        public IEnumerable<FaixaTeorView> GetByPartePlanta(Guid iDPartePlanta)
        {
            return _Service.GetByPartePlanta(iDPartePlanta);
        }
    }
}
