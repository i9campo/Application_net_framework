using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.ViewTables;
using Sigma.Infra.Data.Repositories._Base;
using System.Threading.Tasks;

namespace Sigma.Infra.Data.Repositories
{
    public class ImagemRecorteRepository : RepositoryBase<ImagemSateliteRecortada>, IImagemRecorteRepository
    {
        public Task<TiffImage> GenerateCropImage(SplitImage obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
