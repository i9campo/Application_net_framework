using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IUnidadeDeLaboratorioRepository : IRepository<UnidadeDeLaboratorio>
    {
        /// <summary>
        /// <para>Retorna uma lista de Unidade de laboratorio contendo o nome do laboratorio.</para>
        /// </summary>
        /// <returns></returns>
        IEnumerable<UnidadeDeLaboratorioView> GetAllDesc();

        /// <summary>
        /// <para>Retorna uma unidade de laboratorio contendo o nome do laboratorio.</para>
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        UnidadeDeLaboratorioView FindDesc(Guid objID);
    }
}
