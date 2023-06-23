using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class ProprietarioAppService : AppService<Proprietario>, IProprietarioAppService
    {
        private readonly IProprietarioService _Service;
        public ProprietarioAppService(IProprietarioService service)
            :base(service)
        {
            _Service = service; 
        }

        public IEnumerable<Proprietario> ByName(string name)
        {
            return _Service.ByName(name);
        }

        public IEnumerable<ProprietarioView> GetPfPj(string pfpj)
        {
            return _Service.GetPfPj(pfpj);
        }

        public IEnumerable<Proprietario> GetBySafra(Guid IDSafra, Guid IDUsuario)
        {
            return _Service.GetBySafra(IDSafra, IDUsuario);
        }

        public IEnumerable<Proprietario_Viewer> GetAllProprietario(Guid IDUsuario)
        {
            return _Service.GetAllProprietario(IDUsuario);
        }

        public IEnumerable<BNGProprietario> GetProprietaioBNGSafra(Guid IDSafra)
        {
            return _Service.GetProprietaioBNGSafra(IDSafra);
        }
    }
}
