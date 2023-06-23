using Sigma.App.AppService._Base;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace Sigma.App.AppService
{
    public class SaveTemporaryImgByteAppService : AppService<SaveTemporaryImgByte>, ISaveTemporaryImgByteAppService
    {
        private readonly ISaveTemporaryImgByteService _saveTemporaryImgByte; 
        public SaveTemporaryImgByteAppService(ISaveTemporaryImgByteService saveTemporaryImgByte)
            : base(saveTemporaryImgByte)
        {
            _saveTemporaryImgByte = saveTemporaryImgByte;
        }


        public IEnumerable<SaveTemporaryImgByte> GetListByConjuntID(Guid objID)
        {
            return _saveTemporaryImgByte.GetListByConjuntID(objID);
        }
    }
}
