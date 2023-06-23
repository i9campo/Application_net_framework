using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Service._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Service
{
    public interface ISaveTemporaryImgByteService : IService<SaveTemporaryImgByte>
    {
        /// <summary>Este ID retorna todos os dados da lista de Byte temporário./// </summary>
        /// <param name="objID">ID Referente ao ConjuntoByteID. </param>
        /// <returns></returns>
        IEnumerable<SaveTemporaryImgByte> GetListByConjuntID(Guid objID);
    }
}
