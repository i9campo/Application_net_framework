using FluentValidation.Results;
using Sigma.App.Interfaces._Base;
using Sigma.Domain.Interfaces.Service._Base;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService._Base
{
    /// <summary>
    /// 1º AppService é genérico, fornece CRUD básico para classe mapeada no domínio.
    /// 2º Executa o SaveChanges  "AUTOMATICAMENTE", para operações complexas e transactions desative o AutomaticSaveChanges
    /// 3º Esta classe faz a validação genérica da entidade, NÃO utilize para entidades que podem ter regras diferentes de validação
    /// Ex: Corretivo, CicloProducao, Fertilizante....
    /// </summary>
    /// <typeparam name="TEntity">Classe Sigma.Domain.Entites</typeparam>
    public class AppService<TEntity> : IDisposable, IAppService<TEntity> where TEntity : class
    {
        private readonly IService<TEntity> _service;
        protected ValidationResult Result { get; set; }

        #region Ctor
        public AppService(IService<TEntity> service)
        {
            this._service = service;
            this.Result = new ValidationResult();
        }
        #endregion

        #region CRUD
        public ValidationResult Add(TEntity entity)
        {
            Result = _service.Add(entity);
            return Result;
        }

        public ValidationResult Update(TEntity entity)
        {
            Result = _service.Update(entity);
            return Result;
        }

        public ValidationResult Remove(TEntity entity)
        {

            Result = _service.Remove(entity);
            return Result;
        }

        public TEntity Find(Guid objID)
        {
            return _service.Find(objID);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _service.GetAll();
        }

        #endregion

        public void Dispose()
        {
            _service.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
