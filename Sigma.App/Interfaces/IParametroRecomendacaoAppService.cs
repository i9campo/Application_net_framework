using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface IParametroRecomendacaoAppService : IAppService<ParametroRecomendacao>
    {
        IEnumerable<ParametroRecomendacao> FindParametroRecomendacao(Guid objID);
    }
}
