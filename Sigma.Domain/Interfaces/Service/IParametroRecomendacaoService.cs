using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Service
{
    public interface IParametroRecomendacaoService : IService <ParametroRecomendacao>
    {
        IEnumerable<ParametroRecomendacao> FindParametroRecomendacao(Guid objID);
    }
}
