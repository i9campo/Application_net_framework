using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class CicloProducaoAppService : AppService<CicloProducao>, ICicloProducaoAppService
    {
        private readonly ICicloProducaoService _Service; 
        public CicloProducaoAppService(ICicloProducaoService service)
            :base(service)
        {
            _Service = service; 
        }
        public IEnumerable<CicloProducaoView> GetAllByAreaServico(Guid IDAreaServico, string Type)
        {
            return _Service.GetAllByAreaServico(IDAreaServico, Type);
        }

        public IEnumerable<AnaliseSolo> GetAnalises(Guid IDCicloProducao, string profundidade)
        {
            return _Service.GetAnalises(IDCicloProducao, profundidade);
        }

        public IEnumerable<CicloProducaoView> GetCicloByPropriedadeSafra(Guid IDSafra, Guid IDPropriedade, string Type)
        {
            return _Service.GetCicloByPropriedadeSafra(IDSafra, IDPropriedade, Type); 
        }

        public IEnumerable<CicloProducaoView> GetCiclo(Guid objID, int tipoCiclo, int returno)
        {
            return _Service.GetCiclo(objID, tipoCiclo, returno);
        }

        public IEnumerable<CicloProducaoView> GetCicloAndAreaServico(Guid objID, int tipoCiclo, int retorno)
        {
            return _Service.GetCicloAndAreaServico(objID, tipoCiclo, retorno); 
        }

        public IEnumerable<CicloViewer> GetAllCicloByAreaServico(Guid objID, string Tipo)
        {
            return _Service.GetAllCicloByAreaServico(objID, Tipo); 
        }
    }
}
