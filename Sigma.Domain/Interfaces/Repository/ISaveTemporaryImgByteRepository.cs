using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface ISaveTemporaryImgByteRepository : IRepository<SaveTemporaryImgByte>
    {
        /// <summary>Este ID retorna todos os dados da lista de Byte temporário./// </summary>
        /// <param name="objID">ID Referente ao ConjuntoByteID. </param>
        /// <returns></returns>
        IEnumerable<SaveTemporaryImgByte> GetListByConjuntID(Guid objID);

        /// <summary> Remove todos os dados da lista de save bytes temporario. </summary>
        /// <param name="IDFileByte"></param>
        void RemoveAllBytesByID(Guid IDFileByte); 
    }
}
