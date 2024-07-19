using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface ILithologyGroupSubRepository
    {
        Task<int> Add(LithologyGroupSub lithologyGroupSub);
        Task<int> Update(LithologyGroupSub lithologyGroupSub);
        Task<int> Delete(int id);

        Task<PageList<LithologyGroupSub>> Get(PageParams pageParams);
        Task<PageList<LithologyGroupSub>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<LithologyGroupSub>> GetByLithologyGroup(int lithologyGroupId, PageParams pageParams);
        Task<LithologyGroupSub> GetById(int id);
    }
}