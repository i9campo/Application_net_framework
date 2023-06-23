using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class AreaAppService : AppService<Area> , IAreaAppService
    {
        private readonly IAreaService _Service;
        public AreaAppService(IAreaService service)
            : base(service)
        {
            _Service = service;
        }
        public AreaView GetFullArea(Guid objID)
        {
            return _Service.GetFullArea(objID);
        }

        public IEnumerable<Area> GetByPropriedade(Guid IDPropriedade)
        {
            return _Service.GetByPropriedade(IDPropriedade);
        }

        public IEnumerable<Area> GetByPropriedadeSafra(Guid IDPropriedade, Guid IDSafra)
        {
            return _Service.GetByPropriedadeSafra(IDPropriedade, IDSafra);
        }

        public IEnumerable<AreaView> VerifyAreaServico(Guid IDPropriedade)
        {
            return _Service.VerifyAreaServico(IDPropriedade);
        }

        public IEnumerable<AreaPropriedadeView> GetAreaPropriedade(Guid Area)
        {
            return _Service.GetAreaPropriedade(Area);
        }
        public IEnumerable<Area> GetAllArea()
        {
            return _Service.GetAllArea();
        }

        public IEnumerable<BNGAreaView> GetAreaBNGByPropriedade(Guid IDSafra, Guid IDPropriedade)
        {
            return _Service.GetAreaBNGByPropriedade(IDSafra, IDPropriedade); 
        }



        public IEnumerable<AreaView> FindFullAreaByPropriedade(Guid IDPropriedade)
        {
            return _Service.FindFullAreaByPropriedade(IDPropriedade);
        }

        public IEnumerable<AreaGrid> GetAllAreaExistedGrid(Guid IDSafra, Guid IDPropriedade)
        {
            return _Service.GetAllAreaExistedGrid(IDSafra, IDPropriedade); 
        }
    }
}
