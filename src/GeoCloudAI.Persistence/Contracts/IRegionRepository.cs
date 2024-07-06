using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IRegionRepository
    {
        Task<int> Add(Region region);
        Task<int> Update(Region region);
        Task<int> Delete(int id);

        Task<PageList<Region>> Get(PageParams pageParams);
        Task<PageList<Region>> GetByAccount(int accountId, PageParams pageParams);
        Task<Region> GetById(int id); 
    }
}