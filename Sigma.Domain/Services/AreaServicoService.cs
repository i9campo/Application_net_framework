using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace Sigma.Domain.Services
{
    public class AreaServicoService : Service<AreaServico>, IAreaServicoService
    {
        private readonly IAreaServicoRepository _repository;

        public AreaServicoService(IAreaServicoRepository repository)
            : base(repository)
        {
            _repository = repository; 
        }
        public AreaServicoViewer FindAreaServico(Guid objID)
        {
            return _repository.FindAreaServico(objID);
        }

        public AreaServicoView FindFullAreaServico(Guid? objID, Guid? IDArea, Guid? IDSafra, bool? returngeo)
        {
            return _repository.FindFullAreaServico(objID, IDArea, IDSafra, returngeo);
        }
  

        public IEnumerable<AreaServicoView> GetAreaServico(Guid? IDAreaServico, Guid IDSafra, Guid? IDArea, Guid? IDPropriedade, Guid? IDServico, bool? returngeo, int returnquery)
        {
            return _repository.GetAreaServico(IDAreaServico, IDSafra, IDArea, IDPropriedade, IDServico, returngeo, returnquery);
        }
        public ImportItensLabView ExistAnaliseByCodigo(int Codigo)
        {
            return _repository.ExistAnaliseByCodigo(Codigo);
        }

        public IEnumerable<AreaServicoView> GetMaxServicoRegister(Guid iDSafra, Guid iDArea)
        {
            return _repository.GetMaxServicoRegister(iDSafra, iDArea);
        }
        public bool DeleteAllAreaServico(String objID)
        {
            return _repository.DeleteAllAreaServico(objID);
        }

        public DbGeography GetGeoAreaOrGrid(string geo, Guid? IDAreaServico, bool GridOrAreaServico)
        {
            return _repository.GetGeoAreaOrGrid(geo, IDAreaServico, GridOrAreaServico);
        }

        public AreaServicoView FindFilter(Guid? IDSafra, Guid? IDProprietario, Guid? IDPropriedade, Guid? IDArea)
        {
            return _repository.FindFilter(IDSafra, IDProprietario, IDPropriedade, IDArea); 
        }

        public bool UpdateGeo(Guid objID, string geoString, string jsonField, float tamanho)
        {
            return _repository.UpdateGeo(objID, geoString, jsonField, tamanho); 
        }
    }
}
