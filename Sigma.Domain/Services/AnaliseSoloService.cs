

using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Interfaces.Service._Base;
using Sigma.Domain.Services._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class AnaliseSoloService : Service<AnaliseSolo>, IAnaliseSoloService
    {
        private readonly IAnaliseSoloRepository _AnaliseSoloRepository;
        private readonly IAreaServicoRepository _AreaServicoRepository;
        public AnaliseSoloService(IAnaliseSoloRepository repository, IAreaServicoRepository areaServicoRepository)
            :base (repository)
        {
            _AnaliseSoloRepository = repository;
            _AreaServicoRepository = areaServicoRepository;

        }
        #region Search
        public AnaliseSoloView FindAnalise(Guid objID)
        {
            return _AnaliseSoloRepository.FindAnalise(objID);
        }
        public AnaliseSoloView FindObject(string area, string grid)
        {
            return _AnaliseSoloRepository.FindObject(area, grid);
        }
        public IEnumerable<AnaliseSoloViewer> GetListByAreaServico(Guid IDAreaServico, bool retorno)
        {
            return _AnaliseSoloRepository.GetListByAreaServico(IDAreaServico, retorno);
        }
        public IEnumerable<AnaliseSoloView> GetAnaliseByPropriedade(Guid IDPropriedade)
        {
            return _AnaliseSoloRepository.GetAnaliseByPropriedade(IDPropriedade);
        }
        public IEnumerable<AnaliseSolo> FindAnaliseAreaGrid(Guid? iDArea, Guid? iDGrid)
        {
            return _AnaliseSoloRepository.FindAnaliseAreaGrid(iDArea, iDGrid);
        }
        #endregion
        #region Return's calc. 
        public IEnumerable<MediaAnalise> GetMediaAnaliseSolo(Guid IDAreaServico, Guid? IDGrid, int Perfil, int Und, int Tipo, int RetornoP)
        {
            return _AnaliseSoloRepository.GetMediaAnaliseSolo(IDAreaServico, IDGrid, Perfil, Und, Tipo, RetornoP);
        }
        #endregion
    }
}
