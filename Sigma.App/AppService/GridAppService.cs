using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sigma.App.AppService
{
    public class GridAppService : AppService<Grid>, IGridAppService
    {
        private readonly IGridService _Service;
        public GridAppService(IGridService service)
            :base(service)
        {
            _Service = service; 
        }

        #region C.R.U.D
        public bool AddLstGrid(IEnumerable<GridViewer> lst_grid)
        {
            return _Service.AddLstGrid(lst_grid);
        }
        public bool UpdateLstGrid(IEnumerable<GridViewer> lst_grid)
        {
            return _Service.UpdateLstGrid(lst_grid); 
        }
        public bool UpdateGrid(GridViewer grd)
        {
            return _Service.UpdateGrid(grd);
        }
        public bool DeleteGrid(String IDAreaServico)
        {
            return _Service.DeleteGrid(IDAreaServico);
        }
        #endregion

        #region SEARCH
        public GridViewer FindGrid(Guid objID)
        {
            return _Service.FindGrid(objID); 
        }

        public Grid GetByCodigo(int Codigo)
        {
            return _Service.GetByCodigo(Codigo);
        }
        public IEnumerable<GridViewer> GetByAreaServico(Guid IDAreaServico)
        {
            return _Service.GetByAreaServico(IDAreaServico); 
        }
        #endregion

        public IEnumerable<GridView> CorrecaoAcidez(Guid IDAreaServico)
        {
            return _Service.CorrecaoAcidez(IDAreaServico);
        }

        public IEnumerable<GridView> GetAllGeoJson()
        {
            return _Service.GetAllGeoJson();
        }

        public IEnumerable<GridView> GetByAreaServicoFull(Guid IDAreaServico)
        {
            return _Service.GetByAreaServicoFull(IDAreaServico);
        }

        public void RemoveGRID(Guid objID)
        {
            _Service.RemoveGRID(objID);
        }

        public ImportItensLabView ExistAnaliseByCodigo(int Codigo, Guid IDAreaServico)
        {
            return _Service.ExistAnaliseByCodigo(Codigo, IDAreaServico);
        }

        public IEnumerable<GridView> GetGrid(Guid objID, Guid IDSafra, Guid? IDAreaServico, int Type)
        {
            return _Service.GetGrid(objID, IDSafra, IDAreaServico, Type);
        }

        public GridView GetByGeoAreaServico(Guid IDAreaServico, string geo, string servico)
        {
            return _Service.GetByGeoAreaServico(IDAreaServico, geo, servico);
        }
 

        public IEnumerable<Grid> GetGridByAreaServico(Guid IDAreaServico)
        {
            return _Service.GetGridByAreaServico(IDAreaServico); 
            
        }

        public IEnumerable<Grid> GetAllGridByAreaServico(Guid IDAreaServico)
        {
            return _Service.GetAllGridByAreaServico(IDAreaServico); 
        }

        public bool UpdateFieldList(Guid objID, string newValue)
        {
            return _Service.UpdateFieldList(objID, newValue); 
        }

        public async Task<IEnumerable<GeoJsonSplitPoly>> SplitPoly(SplitPolyViewer obj)
        {
            return await _Service.SplitPoly(obj);
        }
    }
}
