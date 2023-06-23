using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using System;

namespace Sigma.Domain.Services
{
    public class SequenciaImportacaoService : Service<SequenciaImportacao>, ISequenciaImportacaoService
    {
        private readonly ISequenciaImportacaoRepository _repository;
        public SequenciaImportacaoService(ISequenciaImportacaoRepository repository)
            :base (repository)
        {
            _repository = repository;
        }

        public SequenciaImportacao FindSequenciaByLaboratorio(Guid IDLaboratorio)
        {
            return _repository.FindSequenciaByLaboratorio(IDLaboratorio); 
        }
    }
}
