using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
namespace Sigma.Domain.Interfaces.Service
{
    public interface ITipoSoloService : IService<TipoSolo>
    {
        TipoSolo FindTipoSolo(string tpSolo);
    }
}
