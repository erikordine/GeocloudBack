using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IDepositTypeRepository
    {
        Task<int> Add(DepositType depositType);
        Task<int> Update(DepositType depositType);
        Task<int> Delete(int id);

        Task<PageList<DepositType>> Get(PageParams pageParams);
        Task<PageList<DepositType>> GetByAccount(int accountId, PageParams pageParams);
        Task<DepositType> GetById(int id);
    }
}