using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
namespace Sigma.Domain.Interfaces.Repository
{
    public interface ITipoSoloRepository :  IRepository<TipoSolo>
    {
        TipoSolo FindTipoSolo(string tpSolo);
    }
}
