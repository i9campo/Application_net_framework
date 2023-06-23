using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Service;
using Sigma.Domain.Services._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class SaveTemporaryImgByteService : Service<SaveTemporaryImgByte>, ISaveTemporaryImgByteService
    {
        private readonly ISaveTemporaryImgByteRepository _repository;
        public SaveTemporaryImgByteService(ISaveTemporaryImgByteRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<SaveTemporaryImgByte> GetListByConjuntID(Guid objID)
        {
            return _repository.GetListByConjuntID(objID); 
        }
    }

}
