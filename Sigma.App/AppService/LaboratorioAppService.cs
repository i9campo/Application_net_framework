using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class LaboratorioAppService : AppService<Laboratorio> , ILaboratorioAppService
    {
        private readonly ILaboratorioService _Service;
        public LaboratorioAppService(ILaboratorioService service)
            :base(service)
        {
            _Service = service; 
        }
        public IEnumerable<Laboratorio> GetLaboratorioByCNPJ(string CNPJ)
        {
            return _Service.GetLaboratorioByCNPJ(CNPJ);
        }
    }
}
