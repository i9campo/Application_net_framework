using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using static Sigma.Domain.ViewTables.OpenGeo;

namespace Sigma.App.AppService
{
    public class ShapeAppService : AppService<Shape> , IShapeAppService
    {
        private readonly IShapeService _Service;
        public ShapeAppService(IShapeService service)
            :base(service)
        {
            _Service = service; 
        }
        //2568091

        public IEnumerable<BNG_Shape> GetListShapeByAreaServico(Guid IDSafra, Guid IDArea)
        {
            return _Service.GetListShapeByAreaServico(IDSafra, IDArea); 
        }

        public bool ExportSHPToBNG(IEnumerable<ImportShapeToBNG> obj)
        {
            return _Service.ExportSHPToBNG(obj); 
        }

        public IEnumerable<KMLShape> GetKMLFile(string KMLString)
        {
            return _Service.GetKMLFile(KMLString); 
        }

  

        public byte[] KMLGenerate(IEnumerable<KMLShape> obj)
        {
            return _Service.KMLGenerate(obj); 
        }

        public IEnumerable<GeoOBJ> OpenGeoByAreaServico(Guid? ID, Guid IDAreaServico, string tipo)
        {
            return _Service.OpenGeoByAreaServico(ID,IDAreaServico, tipo);
        }

        public IEnumerable<FileExt> OpenGeoSHP(int Orbita, File64 file)
        {
            return _Service.OpenGeoSHP(Orbita, file); 
        }

        public byte[] ShpCreateFile(IEnumerable<GeoCoordText> obj)
        {
          return _Service.ShpCreateFile(obj); 
        }

        public IEnumerable<IEnumerable<FileExt>> GetFileByIDShape(IEnumerable<string> IDShape, int orbita)
        {
            return _Service.GetFileByIDShape(IDShape, orbita); 
        }
    }
}
