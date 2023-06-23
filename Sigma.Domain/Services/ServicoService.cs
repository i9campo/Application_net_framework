using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class ServicoService : Service<Servico> , IServicoService
    {
        private readonly IServicoRepository _repository;
        public ServicoService(IServicoRepository repository)
            : base(repository)
        {
            _repository = repository; 
        }
        public IEnumerable<Servico> GetAllServico()
        {
            return _repository.GetAllServico();
        }

        public IEnumerable<Servico> GetByAreaSafra(Guid IDArea, Guid IDSafra)
        {
            return _repository.GetByAreaSafra(IDArea, IDSafra);
        }

        public IEnumerable<AreaServicoView> GetServico(String IDArea, String IDSafra)
        {
            return _repository.GetServico(IDArea, IDSafra);
        }
    }
}
