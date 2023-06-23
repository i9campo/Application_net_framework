using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class LaboratorioService : Service<Laboratorio>, ILaboratorioService
    {
        private readonly ILaboratorioRepository _repository;

        public LaboratorioService(ILaboratorioRepository repository)
            :base(repository)
        {
            _repository = repository;
        }
        public IEnumerable<Laboratorio> GetLaboratorioByCNPJ(string CNPJ)
        {
            return _repository.GetLaboratorioByCNPJ(CNPJ);
        }
    }
}
