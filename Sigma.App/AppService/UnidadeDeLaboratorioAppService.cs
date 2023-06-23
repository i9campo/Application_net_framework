using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class UnidadeDeLaboratorioAppService : AppService<UnidadeDeLaboratorio>, IUnidadeDeLaboratorioAppService
    {
        private readonly IUnidadeDeLaboratorioService _Service;
        public UnidadeDeLaboratorioAppService(IUnidadeDeLaboratorioService service)
            :base(service)
        {
            _Service = service; 
        }
        public UnidadeDeLaboratorioView FindDesc(Guid objID)
        {
            return _Service.FindDesc(objID);
        }

        public IEnumerable<UnidadeDeLaboratorioView> GetAllDesc()
        {
            return _Service.GetAllDesc();
        }
    }
}
