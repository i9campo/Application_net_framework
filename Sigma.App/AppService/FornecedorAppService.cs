using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class FornecedorAppService : AppService<Fornecedor>, IFornecedorAppService
    {
        private readonly IFornecedorService _Service;
        public FornecedorAppService(IFornecedorService service)
            :base(service)
        {
            _Service = service; 
        }
        public IEnumerable<Fornecedor> GetPfPj(string pfpj)
        {
            return _Service.GetPfPj(pfpj);
        }
    }
}
