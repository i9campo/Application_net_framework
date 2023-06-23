using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IParametroRecomendacaoRepository : IRepository<ParametroRecomendacao>
    {
        IEnumerable<ParametroRecomendacao> FindParametroRecomendacao(Guid objID);
    }
}
