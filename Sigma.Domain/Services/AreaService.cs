using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class AreaService : Service<Area> , IAreaService
    {
        private readonly IAreaRepository _repository;
        public AreaService(IAreaRepository repository)
            : base(repository)
        {
            _repository = repository; 
        }

        public AreaView GetFullArea(Guid objID)
        {
            return _repository.GetFullArea(objID);
        }

        public IEnumerable<Area> GetByPropriedade(Guid IDPropriedade)
        {
            return _repository.GetByPropriedade(IDPropriedade);
        }
        public IEnumerable<AreaView> VerifyAreaServico(Guid IDPropriedade)
        {
            return _repository.VerifyAreaServico(IDPropriedade);
        }

        public IEnumerable<AreaPropriedadeView> GetAreaPropriedade(Guid Area)
        {
            return _repository.GetAreaPropriedade(Area);
        }

        public IEnumerable<Area> GetByPropriedadeSafra(Guid IDPropriedade, Guid IDSafra)
        {
            return _repository.GetByPropriedadeSafra(IDPropriedade, IDSafra);
        }

        public IEnumerable<Area> GetAllArea()
        {
            return _repository.GetAllArea();
        }

        public IEnumerable<BNGAreaView> GetAreaBNGByPropriedade(Guid IDSafra, Guid IDPropriedade)
        {
            return _repository.GetAreaBNGByPropriedade(IDSafra, IDPropriedade); 
               
        }

        public IEnumerable<AreaView> FindFullAreaByPropriedade(Guid IDPropriedade)
        {
            return _repository.FindFullAreaByPropriedade(IDPropriedade); 
        }

        public IEnumerable<AreaGrid> GetAllAreaExistedGrid(Guid IDSafra, Guid IDPropriedade)
        {
            return _repository.GetAllAreaExistedGrid(IDSafra, IDPropriedade); 
        }
    }
}
