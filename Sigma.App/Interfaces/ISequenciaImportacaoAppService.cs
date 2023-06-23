using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using System;

namespace Sigma.App.Interfaces
{
    public interface ISequenciaImportacaoAppService : IAppService<SequenciaImportacao>
    {
        SequenciaImportacao FindSequenciaByLaboratorio(Guid IDLaboratorio); 
    }
}
