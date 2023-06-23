using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class PropriedadeService : Service<Propriedade>, IPropriedadeService
    {
        private readonly IPropriedadeRepository _repository;
        private readonly IAnaliseSoloRepository _repositoryAnalise;
        private readonly IGridRepository _repositoryGrid;
        private readonly IAreaServicoRepository _repositoryAreaServico; 


        public PropriedadeService(IPropriedadeRepository repository, IAnaliseSoloRepository repositoryAnalise, IGridRepository repositoryGrid, IAreaServicoRepository repositoryAreaServico)
            :base(repository)
        {
            _repository             = repository;
            _repositoryAnalise      = repositoryAnalise;
            _repositoryGrid         = repositoryGrid;
            _repositoryAreaServico  = repositoryAreaServico; 

        }

        public IEnumerable<Propriedade> ByProprietario(string IDProprietario)
        {
            return _repository.ByProprietario(IDProprietario);
        }

        public IEnumerable<Propriedade> GetAllPropriedade()
        {
            return _repository.GetAllPropriedade();
        }

        public IEnumerable<BNGPropriedade> GetPropriedadeBNGByProprietario(Guid IDSafra, Guid IDProprietario)
        {
            return _repository.GetPropriedadeBNGByProprietario(IDSafra, IDProprietario); 
        }

        public IEnumerable<Propriedade> HasServicoInSafra(Guid IDProprietario, Guid IDSafra)
        {
            return _repository.HasServicoInSafra(IDProprietario, IDSafra);
        }
    }
}
