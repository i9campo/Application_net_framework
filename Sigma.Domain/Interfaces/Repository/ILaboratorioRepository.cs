using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface ILaboratorioRepository : IRepository<Laboratorio>
    {
        /// <summary>
        /// Retorna lista de laboratório por CNPJ. 
        /// </summary>
        /// <param name="CNPJ"></param>
        /// <returns></returns>
        IEnumerable<Laboratorio> GetLaboratorioByCNPJ(string CNPJ);
    }
}
