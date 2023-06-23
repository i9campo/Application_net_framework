using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class SimulacaoAppService : AppService<Simulacao> , ISimulacaoAppService
    {
        private readonly ISimulacaoService _Service;
        public SimulacaoAppService(ISimulacaoService service)
            :base(service)
        {
            _Service = service; 
        }
        public Simulacao GetAllSimulacao(Guid IDAreaServico, int opcao)
        {
            return _Service.GetAllSimulacao(IDAreaServico, opcao);
        }
        public Simulacao GetCultura()
        {
            return _Service.GetCultura();
        }
    }
}
