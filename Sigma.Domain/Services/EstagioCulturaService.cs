using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class EstagioCulturaService : Service<EstagioCultura>, IEstagioCulturaService
    {
        private readonly IEstagioCulturaRepository _repository;
        public EstagioCulturaService(IEstagioCulturaRepository repository)
            :base (repository)
        {
            _repository = repository; 
        }
        public IEnumerable<EstagioCultura> GetEstagioByCultura(Guid IDCultura)
        {
            return _repository.GetEstagioByCultura(IDCultura);
        }
    }
}
