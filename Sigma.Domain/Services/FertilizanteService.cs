using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class FertilizanteService : Service<Fertilizante>, IFertilizanteService
    {
        private readonly IFertilizanteRepository _repository;

        public FertilizanteService(IFertilizanteRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<FertilizanteView> GetByCP(Guid IDCiclo)
        {
            return _repository.GetByCP(IDCiclo);
        }

        public IEnumerable<FertilizanteView> GetByOpcao(int opcao, Guid IDCiclo)
        {
            return _repository.GetByOpcao(opcao, IDCiclo);
        }

        public int GetCountByCiclo(Guid IDCicloProducao)
        {
            return _repository.GetCountByCiclo(IDCicloProducao);
        }

        public IEnumerable<FertilizanteView> GetByIDCultura(Guid IDCultura)
        {
            return _repository.GetByIDCultura(IDCultura);
        }

        public FertilizanteView GetMediaCiclo(Guid IDCiclo, int opcao)
        {
            return _repository.GetMediaCiclo(IDCiclo, opcao);
        }

        public bool UpdateOptionChecked(UpdateFertilizanteMarcado obj)
        {
            return _repository.UpdateOptionChecked(obj); 
        }

        public IEnumerable<Options> GetOptionsByCP(Guid IDCiclo)
        {
            return _repository.GetOptionsByCP(IDCiclo); 
        }
    }
}
