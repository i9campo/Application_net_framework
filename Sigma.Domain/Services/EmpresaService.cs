using Sigma.Domain.Entities;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class EmpresaService : Service<Empresa> , IEmpresaService
    {
        private readonly IEmpresaRepository _repository;
        public EmpresaService(IEmpresaRepository repository)
            : base(repository)
        {
            _repository = repository; 
        }

        public bool CheckedEmpresaActivateByUsuario(Guid IDUsuario)
        {
            return _repository.CheckedEmpresaActivateByUsuario(IDUsuario); 
        }

        public Empresa GetEmpresa(Guid IDUsuario)
        {
            return _repository.GetEmpresa(IDUsuario); 
        }

        public IEnumerable<Empresa> GetListEmpresa(Guid IDUsuario)
        {
            return _repository.GetListEmpresa(IDUsuario);
        }

        public IEnumerable<UsuarioAtivoView> GetUserActivate(Guid IDEmpresa)
        {
            return _repository.GetUserActivate(IDEmpresa); 
        }
    }
}
