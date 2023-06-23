using FluentValidation.Results;
using Sigma.Infra.Data.Context.DbConfig;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Sigma.Infra.Data.Context.Common
{
    public class BaseContext : DbContext, IDbContext
    {
        public BaseContext(string connectionStringName)
            : base(connectionStringName)
        {

        }
        ValidationResult IDbContext.SaveChanges()
        {
            ValidationResult result = new ValidationResult();
            try
            {
                this.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var er in ex.EntityValidationErrors)
                {
                    foreach (var ve in er.ValidationErrors)
                    {
                        result.Errors.Add(new ValidationFailure(ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ValidationFailure(ex.GetType().Name, ex.Message));
                if (ex.InnerException != null)
                {
                    result.Errors.Add(new ValidationFailure(ex.InnerException.GetType().Name, ex.InnerException.Message));
                }
            }

            return result;
        }
    }
}