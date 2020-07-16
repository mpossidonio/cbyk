using NuvemVulcao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuvemVulcao.Domain.Repositories
{

    public interface IRepository<T> where T : Entity
    {

        T Get(int id);
        Task<T> GetAsync(int id);

        T Get();
        Task<IEnumerable<T>> GetAsync();

    }
}
