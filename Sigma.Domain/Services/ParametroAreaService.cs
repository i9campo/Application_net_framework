using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;

namespace Sigma.Domain.Services
{
    public class ParametroAreaService : Service<ParametroArea>, IParametroAreaService
    {
        private readonly IParametroAreaRepository _repository;

        public ParametroAreaService(IParametroAreaRepository repository)
            : base(repository)
        {
            _repository = repository;

        }

        public ParametroAreaView GetAllParametroArea(Guid IDAreaServico, string safra, string area)
        {
            return _repository.GetAllParametroArea(IDAreaServico, safra, area);
        }

        public ParametroAreaView GetParametroareaByAreaServico(Guid IDAreaServico)
        {
            return _repository.GetParametroareaByAreaServico(IDAreaServico); 
        }
    }
}
