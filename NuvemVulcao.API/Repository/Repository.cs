using NuvemVulcao.Domain.Entities;
using NuvemVulcao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NuvemVulcao.API.Repository
{
    public class Repository<T> : IRepository<T>
        where T : Entity
    {
        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public T Get()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAsync()
        {
            throw new NotImplementedException();
        }
    }
}
