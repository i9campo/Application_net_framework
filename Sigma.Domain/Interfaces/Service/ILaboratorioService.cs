using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Services
{
    public interface ILaboratorioService : IService<Laboratorio>
    {
        /// <summary>
        /// Retorna lista de laboratório por CNPJ. 
        /// </summary>
        /// <param name="CNPJ"></param>
        /// <returns></returns>
        IEnumerable<Laboratorio> GetLaboratorioByCNPJ(string CNPJ);
    }
}
