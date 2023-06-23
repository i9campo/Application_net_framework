using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System.Threading.Tasks;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IImagemRecorteRepository : IRepository<ImagemSateliteRecortada>
    {
        Task<TiffImage> GenerateCropImage(SplitImage obj);
    }
}
