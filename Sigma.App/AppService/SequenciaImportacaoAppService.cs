using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using System;

namespace Sigma.App.AppService
{
    public class SequenciaImportacaoAppService :  AppService<SequenciaImportacao>, ISequenciaImportacaoAppService
    {
        private readonly ISequenciaImportacaoService _SequenciaImportacaoAppService;
        public SequenciaImportacaoAppService(ISequenciaImportacaoService sequenciaImportacao)
            : base(sequenciaImportacao)
        {
            _SequenciaImportacaoAppService = sequenciaImportacao;
        }

        public SequenciaImportacao FindSequenciaByLaboratorio(Guid IDLaboratorio)
        {
            return _SequenciaImportacaoAppService.FindSequenciaByLaboratorio(IDLaboratorio);
        }
    }
}
