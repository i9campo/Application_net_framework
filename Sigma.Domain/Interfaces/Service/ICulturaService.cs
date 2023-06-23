﻿using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Service
{
    public interface ICulturaService : IService<Cultura>
    {
        IEnumerable<Cultura> ByName(String name);

        /// <summary>
        /// Retorna uma lista de Cultura contendo o nome das unidade de medida. 
        /// </summary>
        /// <returns></returns>
        IEnumerable<CulturaView> GetAllCultura();

        /// <summary>
        /// Retorna uma cultura contendo o nome da unidade de medida. 
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        CulturaView FindCultura(Guid objID);
    }
}
