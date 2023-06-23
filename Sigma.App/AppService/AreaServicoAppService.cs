using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace Sigma.App.AppService
{
    public class AreaServicoAppService : AppService<AreaServico>, IAreaServicoAppService
    {
        private readonly IAreaServicoService _Service;
        public AreaServicoAppService(IAreaServicoService service)
            :base(service)
        {
            _Service = service; 
        }
        public AreaServicoViewer FindAreaServico(Guid objID)
        {
            return _Service.FindAreaServico(objID);
        }

        public AreaServicoView FindFullAreaServico(Guid? objID, Guid? IDArea, Guid? IDSafra, bool? returngeo)
        {
            return _Service.FindFullAreaServico(objID, IDSafra, IDArea, returngeo);
        }


        public AreaServicoView FindFilter(Guid? IDSafra, Guid? IDProprietario, Guid? IDPropriedade, Guid? IDArea)
        {
            return _Service.FindFilter(IDSafra, IDProprietario, IDPropriedade, IDArea); 
        }




        public IEnumerable<AreaServicoView> GetAreaServico(Guid? IDAreaServico, Guid IDSafra, Guid? IDArea, Guid? IDPropriedade, Guid? IDServico, bool? returngeo, int returnquery)
        {
            return _Service.GetAreaServico(IDAreaServico, IDSafra, IDArea, IDPropriedade, IDServico, returngeo, returnquery);
        }

        public ImportItensLabView ExistAnaliseByCodigo(int Codigo)
        {
            return _Service.ExistAnaliseByCodigo(Codigo);
        }

        public IEnumerable<AreaServicoView> GetMaxServicoRegister(Guid iDSafra, Guid iDArea)
        {
            return _Service.GetMaxServicoRegister(iDSafra, iDArea);
        }
        public bool DeleteAllAreaServico(String objID)
        {
            return _Service.DeleteAllAreaServico(objID);
        }


        public DbGeography GetGeoAreaOrGrid(string geo, Guid? IDAreaServico, bool GridOrAreaServico)
        {
            return _Service.GetGeoAreaOrGrid(geo, IDAreaServico, GridOrAreaServico);
        }

        public bool UpdateGeo(Guid objID, string geoString, string jsonField, float tamanho)
        {
            return _Service.UpdateGeo(objID, geoString, jsonField, tamanho); 
        }
    }
}
