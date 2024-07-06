using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IDepositRepository
    {
        Task<int> Add(Deposit deposit);
        Task<int> Update(Deposit deposit);
        Task<int> Delete(int id);

        Task<PageList<Deposit>> Get(PageParams pageParams);
        Task<PageList<Deposit>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<Deposit>> GetByRegion(int regionId, PageParams pageParams);
        Task<Deposit> GetById(int id); 
    }
}