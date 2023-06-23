using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using System;

namespace Sigma.Domain.Interfaces.Service
{
    public interface ISimulacaoService : IService<Simulacao>
    {
        Simulacao GetAllSimulacao(Guid iDAreaServico, int opcao);
        Simulacao GetCultura();
    }
}
