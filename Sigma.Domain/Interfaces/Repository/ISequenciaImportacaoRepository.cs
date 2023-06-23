using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using System;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface ISequenciaImportacaoRepository : IRepository<SequenciaImportacao>
    {
        SequenciaImportacao FindSequenciaByLaboratorio(Guid IDLaboratorio);
    }
}
