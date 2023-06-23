using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using Sigma.Domain.ViewTables;
using System.Threading.Tasks;

namespace Sigma.Domain.Interfaces.Service
{
    public interface IImagemRecorteService : IService<ImagemSateliteRecortada>
    {
        Task<TiffImage> GenerateCropImage(SplitImage obj);
    }
}
