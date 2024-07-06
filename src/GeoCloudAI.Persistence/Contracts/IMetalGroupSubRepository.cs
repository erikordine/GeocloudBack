using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IMetalGroupSubRepository
    {
        Task<int> Add(MetalGroupSub metalGroupSub);
        Task<int> Update(MetalGroupSub metalGroupSub);
        Task<int> Delete(int id);

        Task<PageList<MetalGroupSub>> Get(PageParams pageParams);
        Task<PageList<MetalGroupSub>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<MetalGroupSub>> GetByMetalGroup(int metalGroupId, PageParams pageParams);
        Task<MetalGroupSub> GetById(int id);
    }
}