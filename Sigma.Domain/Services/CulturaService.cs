using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using System;
using System.Collections.Generic;
using Sigma.Domain.ViewTables;

namespace Sigma.Domain.Services
{
    public class CulturaService : Service<Cultura>, ICulturaService
    {
        private readonly ICulturaRepository _repository;

        public CulturaService(ICulturaRepository repository)
            :base(repository)
        {
            _repository = repository;
        }
        public IEnumerable<Cultura> ByName(string name)
        {
            return _repository.ByName(name);
        }

        public CulturaView FindCultura(Guid objID)
        {
            return _repository.FindCultura(objID);
        }

        public IEnumerable<CulturaView> GetAllCultura()
        {
            return _repository.GetAllCultura();
        }
    }
}
