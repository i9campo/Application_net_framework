using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
namespace Sigma.Domain.Services
{
    public class RestevaService :  Service<Resteva>, IRestevaService
    {
        private readonly IRestevaRepository _repository;

        public RestevaService(IRestevaRepository repository)
            :base (repository)
        {
            _repository = repository;
        }
    }
}
