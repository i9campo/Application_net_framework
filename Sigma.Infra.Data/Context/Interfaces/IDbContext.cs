using FluentValidation.Results;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Sigma.Infra.Data.Context.DbConfig
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        ValidationResult SaveChanges();
        void Dispose();
    }
}
