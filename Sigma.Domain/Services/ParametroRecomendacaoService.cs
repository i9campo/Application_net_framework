using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class ParametroRecomendacaoService : Service<ParametroRecomendacao>, IParametroRecomendacaoService
    {
        private readonly IParametroRecomendacaoRepository _repository;
        public ParametroRecomendacaoService(IParametroRecomendacaoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<ParametroRecomendacao> FindParametroRecomendacao(Guid objID)
        {
            return _repository.FindParametroRecomendacao(objID);
        }
    }
}

