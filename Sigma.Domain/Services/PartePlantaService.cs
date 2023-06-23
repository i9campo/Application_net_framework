using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class PartePlantaService : Service<PartePlanta>, IPartePlantaService
    {
        private readonly IPartePlantaRepository _repository;
        public PartePlantaService(IPartePlantaRepository repository)
            : base(repository)
        {
            _repository = repository; 
        }
        public IEnumerable<PartePlanta> GetPartePlantaByCultura(Guid iDCultura)
        {
            return _repository.GetPartePlantaByCultura(iDCultura);
        }
    }
}
