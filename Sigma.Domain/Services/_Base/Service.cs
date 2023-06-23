using FluentValidation.Results;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.Interfaces.Service._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services._Base
{
    public class Service<TEntity> : IService<TEntity>
         where TEntity : class
    {
        #region Propriedades

        private readonly IRepository<TEntity> _repository;
        protected IRepository<TEntity> Repository
        {
            get { return _repository; }
        }

        #endregion

        #region Ctor

        public Service(
            IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Crud
        public virtual TEntity Find(Guid objID)
        {
            return _repository.Find(objID);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }
        
        public virtual ValidationResult Add(TEntity entity)
        {

            if (entity.GetType() == typeof(Corretivo))
                throw new Exception("O CORRETIVO POSSUI REGRA DE CADASTRO ESPECÍFICO, NÃO UTILIZE O SERVIÇO GENÉRICO PARA CADASTRAR UM CORRETIVO");

            var selfValidationEntity = entity as ISelfValidation;

            if (selfValidationEntity != null && !selfValidationEntity.IsValid)
                return selfValidationEntity.ValidationResult;

            return _repository.Add(entity); 
        }

        public virtual ValidationResult Update(TEntity entity)
        {
            try { 
                if (entity.GetType() == typeof(Corretivo))
                    throw new Exception("O CORRETIVO POSSUI REGRA DE ATUALIZAÇÃO ESPECÍFICA, NÃO UTILIZE O SERVIÇO GENÉRICO PARA ATUALIZAR O CORRETIVO");
            }catch(Exception e)
            {
               
            }
            var selfValidationEntity = entity as ISelfValidation;
            if (selfValidationEntity != null && !selfValidationEntity.IsValid)
                return selfValidationEntity.ValidationResult;

            return _repository.Update(entity);
        }

        public virtual ValidationResult Remove(TEntity entity)
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {
                ValidationResult command = _repository.Remove(entity);
                if (!command.IsValid)
                {
                    foreach (var error in command.Errors)
                    {
                        validationResult.Errors.Add(error);
                    }
                }
            }
            catch(Exception ex)
            {
                validationResult.Errors.Add(new ValidationFailure("ERRO", ex.Message));
            }



            return validationResult;
        }

        #endregion

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
