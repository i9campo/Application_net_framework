using FluentValidation.Results;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Infra.Data.Context;
using Sigma.Infra.Data.Context.DbConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace Sigma.Infra.Data.Repositories._Base
{
    public class RepositoryBase<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        private readonly IDbContext _dbContext;
        private readonly IDbSet<TEntity> _dbSet;

        public RepositoryBase()
        {
            _dbContext = new DBContext();
            _dbSet = _dbContext.Set<TEntity>();
        }

        protected DbContext Context
        {
            get { return (DbContext)_dbContext; }
        }

        public void RefreshEntity(TEntity entity)
        {
            _dbContext.Entry(entity).Reload();
        }

        public ValidationResult Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);

            ValidationResult vr = _dbContext.SaveChanges();

            if(!vr.IsValid)
                _dbContext.Set<TEntity>().Remove(entity);

            return vr;
        }
        public TEntity Find(Guid objID)
        {
            return _dbContext.Set<TEntity>().Find(objID);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public ValidationResult Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return (_dbContext.SaveChanges());
        }

        public ValidationResult Update(TEntity entity)
        {
            _dbContext.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Modified;
            return (_dbContext.SaveChanges());
        }

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (Context == null) return;
            Context.Dispose();
        }

        #endregion
    }
}
