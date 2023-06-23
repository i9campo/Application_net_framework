using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces._Base
{
    public interface IAppService<TEntity> where TEntity : class
    {
        ValidationResult Add(TEntity entity);
        ValidationResult Update(TEntity entity);
        ValidationResult Remove(TEntity entity);

        TEntity Find(Guid objID);

        IEnumerable<TEntity> GetAll();
        void Dispose();
    }
}
