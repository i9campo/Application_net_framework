using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class RecomendacaoFoliarService : Service<RecomendacaoFoliar>, IRecomendacaoFoliarService
    {
        private readonly IRecomendacaoFoliarRepository _repository;

        public RecomendacaoFoliarService(IRecomendacaoFoliarRepository repository)
            :base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<RecomendacaoFoliar> GetByCultura(Guid IDCultura)
        {
            return _repository.GetByCultura(IDCultura);
        }

        public IEnumerable<RecomendacaoFoliarView> GetElementoRFNS(Guid objID, Guid IDCultura)
        {
            return _repository.GetElementoRFNS(objID, IDCultura);
        }
    }
}
