using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using System;

namespace Sigma.App.Interfaces
{
    public interface ISimulacaoAppService : IAppService<Simulacao>
    {
        Simulacao GetAllSimulacao(Guid iDAreaServico, int opcao);
        Simulacao GetCultura();
    }
}
