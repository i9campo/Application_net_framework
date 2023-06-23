using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class SafraAppService : AppService<Safra>, ISafraAppService
    {
        private readonly ISafraService _safraService;

        public SafraAppService(ISafraService safraService)
            : base(safraService)
        {
            _safraService = safraService;
        }
        public IEnumerable<Safra> FindSafra(Guid IDSafra)
        {
            return _safraService.FindSafra(IDSafra);
        }

        public IEnumerable<SafraView> GetLstSafraBNG()
        {
            return _safraService.GetLstSafraBNG(); 
        }
    }
}
