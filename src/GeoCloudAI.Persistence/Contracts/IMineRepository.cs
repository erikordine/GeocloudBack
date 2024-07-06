using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IMineRepository
    {
        Task<int> Add(Mine mine);
        Task<int> Update(Mine mine);
        Task<int> Delete(int id);

        Task<PageList<Mine>> Get(PageParams pageParams);
        Task<PageList<Mine>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<Mine>> GetByRegion(int regionId, PageParams pageParams);
        Task<PageList<Mine>> GetByDeposit(int depositId, PageParams pageParams);
        Task<Mine> GetById(int id); 
    }
}