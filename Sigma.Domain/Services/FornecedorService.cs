using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using System.Collections.Generic;
namespace Sigma.Domain.Services
{
    public class FornecedorService : Service<Fornecedor>, IFornecedorService
    {
        private readonly IFornecedorRepository _repository;

        public FornecedorService(IFornecedorRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
        public IEnumerable<Fornecedor> GetPfPj(string pfpj)
        {
            return _repository.GetPfPj(pfpj);
        }
    }
}
