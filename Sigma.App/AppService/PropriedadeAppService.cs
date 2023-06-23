using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class PropriedadeAppService : AppService<Propriedade>, IPropriedadeAppService
    {
        private readonly IPropriedadeService _Service;
        public PropriedadeAppService(IPropriedadeService service)
            :base(service)
        {
            _Service = service; 
        }


        public IEnumerable<Propriedade> ByProprietario(string IDProprietario)
        {
            return _Service.ByProprietario(IDProprietario);
        }

        public IEnumerable<Propriedade> GetAllPropriedade()
        {
            return _Service.GetAllPropriedade();
        }

        public IEnumerable<BNGPropriedade> GetPropriedadeBNGByProprietario(Guid IDSafra, Guid IDProprietario)
        {
             return _Service.GetPropriedadeBNGByProprietario(IDSafra, IDProprietario); 
        }

        public IEnumerable<Propriedade> HasServicoInSafra(Guid IDProprietario, Guid IDSafra)
        {
            return _Service.HasServicoInSafra(IDProprietario, IDSafra);
        }
    }
}
