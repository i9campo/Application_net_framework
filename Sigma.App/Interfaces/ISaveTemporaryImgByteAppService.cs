using Sigma.App.Interfaces._Base;
using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Sigma.App.Interfaces
{
    public interface ISaveTemporaryImgByteAppService : IAppService<SaveTemporaryImgByte>
    {
        /// <summary>Este ID retorna todos os dados da lista de Byte temporário./// </summary>
        /// <param name="objID">ID Referente ao ConjuntoByteID. </param>
        /// <returns></returns>
        IEnumerable<SaveTemporaryImgByte> GetListByConjuntID(Guid objID); 
    }
}
