using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class FaixaTeorService : Service<FaixaTeor>, IFaixaTeorService
    {
        private readonly IFaixaTeorRepository _repository;

        public FaixaTeorService(IFaixaTeorRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
        public IEnumerable<FaixaTeor> GetBy(string nutriente, EstagioCultura ec, PartePlanta pp)
        {
            return _repository.GetBy(nutriente, ec.objID, pp.objID);
        }

        public IEnumerable<FaixaTeorView> GetByPartePlanta(Guid iDPartePlanta)
        {
            return _repository.GetByPartePlanta(iDPartePlanta);
        }
    }
}
