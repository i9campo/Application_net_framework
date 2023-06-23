﻿using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
namespace Sigma.Domain.Services
{
    public class ItensAnalisesLaboratorioService : Service<SequenciaImportacao>, IItensAnaliseLaboratorioService
    {
        private readonly IItensAnalisesLaboratorioRepository _repository;
        public ItensAnalisesLaboratorioService(IItensAnalisesLaboratorioRepository repository)
            :base(repository)
        {
            _repository = repository; 
        }
    }
}
