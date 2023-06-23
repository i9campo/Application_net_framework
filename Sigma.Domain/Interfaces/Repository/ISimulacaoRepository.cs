using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface ISimulacaoRepository : IRepository<Simulacao>
    {
        Simulacao GetAllSimulacao(Guid iDAreaServico, int opcao);
        Simulacao GetCultura();
    }
}
