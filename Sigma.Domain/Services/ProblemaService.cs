
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
namespace Sigma.Domain.Services
{ 
    public class ProblemaService : Service<Problema>, IProblemaService
    {
        private readonly IProblemaRepository _repository;
        public ProblemaService(IProblemaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
