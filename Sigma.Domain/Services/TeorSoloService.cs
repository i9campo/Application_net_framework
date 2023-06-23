using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;
namespace Sigma.Domain.Services
{
    public class TeorSoloService : Service<TeorSolo>, ITeorSoloService
    {
        private readonly ITeorSoloRepository _repository;
        

        public TeorSoloService(ITeorSoloRepository repository)
            :base(repository)
        {
            _repository = repository;
            
        }
    }
}
