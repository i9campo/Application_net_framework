using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sigma.Domain.Services
{
    public class ParametroPropriedadeService : Service<ParametroPropriedade>, IParametroPropriedadeService
    {
		private readonly IAnaliseSoloRepository _repositorysolo;
		private readonly ITipoSoloRepository _repositorytiposolo;
		private readonly IParametroPropriedadeRepository _repository;
        public ParametroPropriedadeService(IParametroPropriedadeRepository repository, IAnaliseSoloRepository repositorysolo, ITipoSoloRepository repositorytiposolo)
            : base(repository)
        {
            _repository = repository;
			_repositorysolo = repositorysolo;
			_repositorytiposolo = repositorytiposolo;
		}
		public IEnumerable<ParametroPropriedade> FindParametroPropriedade(Guid objID)
		{
			return _repository.FindParametroPropriedade(objID);
		}

        public ParametroPropriedade GetByAreaPropriedade(Guid IDSafra, Guid IDPropriedade)
        {
            return _repository.GetByAreaPropriedade(IDSafra, IDPropriedade);
        }

        public ParametroSoloView GetSolo(Guid IDAreaServico)
		{
			//List<String> lsRestritivo = new List<String>();

			ParametroSoloView oParametro = new ParametroSoloView();

			//var oAnaliseSolo = _repositorysolo.GetAnaliseSoloByAreaServico(IDAreaServico, 1);
			//Double cont = 0;
			//foreach (var item in oAnaliseSolo)
			//{
			//	if (item.IDTipoSolo != null)
			//	{
			//		var tiposolo = _repositorytiposolo.Find(Guid.Parse(item.IDTipoSolo.ToString()));

			//		if (tiposolo != null)
			//		{
			//			if (tiposolo.abreviacao.Contains("T"))
			//			{
			//				cont++;
			//			}
			//			else
			//			{
			//				lsRestritivo.Add(tiposolo.abreviacao);
			//			}
			//		}
			//	}
			//}
			//lsRestritivo = lsRestritivo.Distinct().ToList();
			//var restritivos = "";
			//foreach (var item in lsRestritivo)
			//{
			//	restritivos += item;
			//}

			//if (lsRestritivo.Count() > 0)
			//{
			//	oParametro.restritivo = restritivos;
			//}
			//else
			//{
			//	oParametro.restritivo = "";
			//}

			//oParametro.calc = (cont / oAnaliseSolo.Count() * 100);

			return oParametro;
		}
	}
}
