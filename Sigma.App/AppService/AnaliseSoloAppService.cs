using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class AnaliseSoloAppService : AppService<AnaliseSolo>, IAnaliseSoloAppService
    {
        private readonly IAnaliseSoloService _Service;
        public AnaliseSoloAppService(IAnaliseSoloService service)
            :base(service)
        {
            _Service = service; 
        }

        #region Search
        public AnaliseSoloView FindAnalise(Guid objID)
        {
            return _Service.FindAnalise(objID);
        }
        public AnaliseSoloView FindObject(string area, string grid)
        {
            return _Service.FindObject(area, grid);
        }
        public IEnumerable<AnaliseSoloViewer> GetListByAreaServico(Guid IDAreaServico, bool retorno)
        {
            return _Service.GetListByAreaServico(IDAreaServico, retorno); 
        }
        public IEnumerable<AnaliseSoloView> GetAnaliseByPropriedade(Guid IDPropriedade)
        {
            return _Service.GetAnaliseByPropriedade(IDPropriedade);
        }
        public IEnumerable<AnaliseSolo> FindAnaliseAreaGrid(Guid? iDArea, Guid? iDGrid)
        {
            return _Service.FindAnaliseAreaGrid(iDArea, iDGrid);
        }
        #endregion
        #region Return's calc. 
        public IEnumerable<MediaAnalise> GetMediaAnaliseSolo(Guid IDAreaServico, Guid? IDGrid, int Perfil, int Und, int Tipo, int RetornoP)
        {
            return _Service.GetMediaAnaliseSolo(IDAreaServico, IDGrid, Perfil, Und, Tipo, RetornoP);
        }
        #endregion
    }
}
