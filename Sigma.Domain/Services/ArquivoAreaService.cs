using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sigma.Domain.Services
{
    public class ArquivoAreaService : Service<ArquivoAreaView>, IArquivoAreaService
    {
        private readonly IArquivoAreaRepository _repository;
        private readonly IAnaliseSoloRepository _repositoryAnalise;
        private readonly IGridRepository _repositoryGrid;
        private readonly IAreaServicoRepository _repositoryAreaServico;

        public ArquivoAreaService(IArquivoAreaRepository repository, IAnaliseSoloRepository repositoryAnalies, IGridRepository repositoryGrid, IAreaServicoRepository repositoryAreaServico)
            :base(repository)
        {
            _repository = repository;
            _repositoryAnalise = repositoryAnalies;
            _repositoryGrid = repositoryGrid;
            _repositoryAreaServico = repositoryAreaServico; 
        }

        public IEnumerable<ArquivoAreaView> GetListArquivoAreaByAreaServico(Guid IDAreaServico)
        {
            List<ArquivoAreaView> lst = _repository.GetListArquivoAreaByAreaServico(IDAreaServico).ToList();
            foreach (var item in lst) 
            {
                item.objID           = Guid.NewGuid()        ;
            }

            return lst; 
        }

        public IEnumerable<ArquivoAreaView> GetListArquivoAreaBySafraArea(Guid IDSafra, Guid IDArea)
        {
            List<ArquivoAreaView> lst = _repository.GetListArquivoAreaBySafraArea(IDSafra, IDArea).ToList();
            foreach (var item in lst)
            {
                item.objID = Guid.NewGuid();
            }
            return lst;
        }

        public IEnumerable<ArquivoAreaView> GetListArquivoAreaBySafraPropriedade(Guid IDSafra, Guid IDPropriedade)
        {
            List<ArquivoAreaView> lst = _repository.GetListArquivoAreaBySafraPropriedade(IDSafra, IDPropriedade).ToList();
            foreach (var item in lst)
            {
                item.objID = Guid.NewGuid(); 
            }
            return lst; 
        }
    }
}
