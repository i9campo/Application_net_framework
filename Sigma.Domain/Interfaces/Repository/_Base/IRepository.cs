using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository._Base
{
    public interface IRepository<TEntity>
      where TEntity : class
    {
        ValidationResult Add(TEntity entity);
        ValidationResult Update(TEntity entity);
        ValidationResult Remove(TEntity entity);
        TEntity Find(Guid objID);
        IEnumerable<TEntity> GetAll();
    }
}
