﻿using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;

namespace Sigma.Domain.Services
{
    public class AduboService : Service<Adubo>, IAduboService
    {
        private readonly IAduboRepository _repository;
        public AduboService(IAduboRepository repository)
            :base(repository)
        {
            _repository = repository;
        }
    }
}
