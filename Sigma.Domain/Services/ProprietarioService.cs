using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class ProprietarioService: Service<Proprietario>, IProprietarioService
    {
        private readonly IProprietarioRepository _repository;

        public ProprietarioService(IProprietarioRepository repository)
            :base (repository)
        {
            _repository = repository; 
        }

        public IEnumerable<Proprietario> ByName(string name)
        {
            return _repository.ByName(name);
        }

        public IEnumerable<ProprietarioView> GetPfPj(string pfpj)
        {
            return _repository.GetPfPj(pfpj); 
        }

        public IEnumerable<Proprietario> GetBySafra(Guid IDSafra, Guid IDUsuario)
        {
            return _repository.GetBySafra(IDSafra, IDUsuario);
        }

        public IEnumerable<Proprietario_Viewer> GetAllProprietario(Guid IDUsuario)
        {
            return _repository.GetAllProprietario(IDUsuario); 
        }

        public IEnumerable<BNGProprietario> GetProprietaioBNGSafra(Guid IDSafra)
        {
            return _repository.GetProprietaioBNGSafra(IDSafra); 
        }
    }
}
