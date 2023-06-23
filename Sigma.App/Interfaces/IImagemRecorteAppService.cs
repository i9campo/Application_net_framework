using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System.Threading.Tasks;

namespace Sigma.App.Interfaces
{
    public interface IImagemRecorteAppService : IAppService<ImagemSateliteRecortada>
    {
        Task<TiffImage> GenerateCropImage(SplitImage obj); 
    }
}
