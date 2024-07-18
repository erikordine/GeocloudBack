using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface ILithologyGroupRepository
    {
        Task<int> Add(LithologyGroup lithologyGroup);
        Task<int> Update(LithologyGroup lithologyGroup);
        Task<int> Delete(int id);

        Task<PageList<LithologyGroup>> Get(PageParams pageParams);
        Task<PageList<LithologyGroup>> GetByAccount(int accountId, PageParams pageParams);
        Task<LithologyGroup> GetById(int id);
    }
}