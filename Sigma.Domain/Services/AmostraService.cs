using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class AmostraService : Service<Amostra>, IAmostraService
    {
        private readonly IAmostraRepository _repository;

        public AmostraService(IAmostraRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Amostra> GetByCultura(Guid IDCultura, int? mediaT)
        {
            return _repository.GetByCultura(IDCultura, mediaT); 
        }
    }
}
