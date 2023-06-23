using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;

namespace Sigma.App.Interfaces
{
    public interface ITipoSoloAppService : IAppService<TipoSolo>
    {
        TipoSolo FindTipoSolo(string tpSolo);
    }
}
