using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class SafraService : Service<Safra> , ISafraService
    {
        private readonly ISafraRepository _repository;
        public SafraService(ISafraRepository repository)
            : base(repository)
        {
            _repository = repository; 
        }
        public IEnumerable<Safra> FindSafra(Guid IDSafra)
        {
            return _repository.FindSafra(IDSafra);
        }

        public IEnumerable<SafraView> GetLstSafraBNG()
        {
            return _repository.GetLstSafraBNG(); 
        }
    }
}
