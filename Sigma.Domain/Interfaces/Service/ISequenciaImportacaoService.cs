using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using System;

namespace Sigma.Domain.Interfaces.Service
{
    public interface ISequenciaImportacaoService : IService<SequenciaImportacao>
    {
        SequenciaImportacao FindSequenciaByLaboratorio(Guid IDLaboratorio);
    }
}
