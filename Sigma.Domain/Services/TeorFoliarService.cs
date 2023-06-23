﻿using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.Interfaces.Services;
using Sigma.Domain.Services._Base;
namespace Sigma.Domain.Services
{
    public class TeorFoliarService : Service<TeorFoliar>, ITeorFoliarService
    {
        private readonly ITeorFoliarRepository _repository;

        public TeorFoliarService(ITeorFoliarRepository repository)
            :base(repository)
        {
            _repository = repository;
        }
    }
}
