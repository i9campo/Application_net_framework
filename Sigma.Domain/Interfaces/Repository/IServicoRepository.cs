﻿using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface IServicoRepository : IRepository<Servico>
    {
        IEnumerable<Servico> GetAllServico();
        /// <summary>
        /// <para>Retorna uma lista de serviço através do ID da Área selecionada com ID da Safra.</para>
        /// </summary>
        /// <param name="IDArea"></param>
        /// <param name="IDSafra"></param>
        /// <returns></returns>
        IEnumerable<Servico> GetByAreaSafra(Guid IDArea, Guid IDSafra);
        IEnumerable<AreaServicoView> GetServico(String iDArea, String iDSafra);

    }
}
