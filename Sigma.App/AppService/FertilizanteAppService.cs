using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class FertilizanteAppService: AppService<Fertilizante>, IFertilizanteAppService
    {
        private readonly IFertilizanteService _Service;
        public FertilizanteAppService(IFertilizanteService service)
            :base(service)
        {
            _Service = service; 
        }
        public IEnumerable<FertilizanteView> GetByCP(Guid IDCiclo)
        {
            return _Service.GetByCP(IDCiclo);
        }

        public IEnumerable<FertilizanteView> GetByOpcao(int opcao, Guid IDCiclo)
        {
            return _Service.GetByOpcao(opcao, IDCiclo);
        }

        public int GetCountByCiclo(Guid IDCicloProducao)
        {
            return _Service.GetCountByCiclo(IDCicloProducao);
        }

        public FertilizanteView GetMediaCiclo(Guid IDCiclo, int opcao)
        {
            return _Service.GetMediaCiclo(IDCiclo, opcao);
        }
        public IEnumerable<FertilizanteView> GetByIDCultura(Guid IDCultura)
        {
            return _Service.GetByIDCultura(IDCultura);
        }

        public bool UpdateOptionChecked(UpdateFertilizanteMarcado obj)
        {
            return _Service.UpdateOptionChecked(obj); 
        }

        public IEnumerable<Options> GetOptionsByCP(Guid IDCiclo)
        {
            return _Service.GetOptionsByCP(IDCiclo);
        }
    }
}
