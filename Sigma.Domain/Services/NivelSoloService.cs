using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Services
{
    public class NivelSoloService : Service<NivelSolo>, INivelSoloService
    {
        private readonly INivelSoloRepository _repository;

        public NivelSoloService(INivelSoloRepository repository)
            :base(repository)
        {
            _repository = repository;
        }

        public NivelSolo GetBy(string elemento, Cultura c)
        {
            return _repository.GetBy(elemento, c.objID);
        }

        public IEnumerable<NivelSolo> GetNivelByCultura(Guid IDCultura)
        {
            return _repository.GetNivelByCultura(IDCultura);
        }

        public IEnumerable<NivelSolo> GetNivelByElemento(Guid IDCultura, string elemento)
        {
            return _repository.GetNivelByElemento(IDCultura, elemento);
        }

        public NivelSolo[] GetSolo(Cultura c)
        {
            NivelSolo[] arrayN = new NivelSolo[13];
            arrayN[0] = GetBy("N ", c);
            arrayN[1] = GetBy("P ", c);
            arrayN[2] = GetBy("K ", c);
            arrayN[3] = GetBy("Ca", c);
            arrayN[4] = GetBy("Mg", c);
            arrayN[5] = GetBy("S ", c);
            arrayN[6] = GetBy("B ", c);
            arrayN[7] = GetBy("Zn", c);
            arrayN[8] = GetBy("Mn", c);
            arrayN[9] = GetBy("Fe", c);
            arrayN[10] = GetBy("Cu", c);
            arrayN[11] = GetBy("Mo", c);
            arrayN[12] = GetBy("Co", c);

            return arrayN;
        }
    }
}
