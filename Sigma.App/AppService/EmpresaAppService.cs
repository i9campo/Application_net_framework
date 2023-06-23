using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class EmpresaAppService : AppService<Empresa>, IEmpresaAppService
    {
        private readonly IEmpresaService _Service;
        public EmpresaAppService(IEmpresaService service)
            :base(service)
        {
            _Service = service; 
        }

        public bool CheckedEmpresaActivateByUsuario(Guid IDUsuario)
        {
            return _Service.CheckedEmpresaActivateByUsuario(IDUsuario); 
        }

        public Empresa GetEmpresa(Guid IDUsuario)
        {
            return _Service.GetEmpresa(IDUsuario);
        }

        public IEnumerable<Empresa> GetListEmpresa(Guid IDUsuario)
        {
            return _Service.GetListEmpresa(IDUsuario);
        }

        public IEnumerable<UsuarioAtivoView> GetUserActivate(Guid IDEmpresa)
        {
            return _Service.GetUserActivate(IDEmpresa); 
        }
    }
}
