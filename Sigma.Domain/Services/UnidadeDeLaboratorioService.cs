using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class UnidadeDeLaboratorioService : Service<UnidadeDeLaboratorio>, IUnidadeDeLaboratorioService
    {
        private readonly IUnidadeDeLaboratorioRepository _repository;
        public UnidadeDeLaboratorioService(IUnidadeDeLaboratorioRepository repository)
            :base(repository)
        {
            _repository = repository; 
        }

        public UnidadeDeLaboratorioView FindDesc(Guid objID)
        {
            return _repository.FindDesc(objID);
        }

        public IEnumerable<UnidadeDeLaboratorioView> GetAllDesc()
        {
            return _repository.GetAllDesc();
        }
    }
}
