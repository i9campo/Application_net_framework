using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface ILaboratorioAppService : IAppService<Laboratorio>
    {
        /// <summary>
        /// Retorna lista de laboratório por CNPJ. 
        /// </summary>
        /// <param name="CNPJ"></param>
        /// <returns></returns>
        IEnumerable<Laboratorio> GetLaboratorioByCNPJ(string CNPJ);
    }
}
