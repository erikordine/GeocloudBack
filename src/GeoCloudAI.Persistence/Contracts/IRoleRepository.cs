using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IRoleRepository
    {
        Task<int> Add(Role role);
        Task<int> Update(Role role);
        Task<int> Delete(int id);

        Task<PageList<Role>> Get(PageParams pageParams);
        Task<PageList<Role>> GetByAccount(int accountId, PageParams pageParams);
        Task<Role> GetById(int id);
    }
}