using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class VariedadeCulturaService : Service<VariedadeCultura>, IVariedadeCulturaService
    {
        private readonly IVariedadeCulturaRepository _repository;

        public VariedadeCulturaService(IVariedadeCulturaRepository repository)
            :base(repository)
        {
            _repository = repository;
        }
        public IEnumerable<VariedadeCultura> GetVariedadeCulturaByCultura(Guid IDCultura)
        {
            return _repository.GetVariedadeCulturaByCultura(IDCultura);
        }
    }
}
