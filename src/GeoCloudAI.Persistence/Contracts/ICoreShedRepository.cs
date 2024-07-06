using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface ICoreShedRepository
    {
        Task<int> Add(CoreShed coreShed);
        Task<int> Update(CoreShed coreShed);
        Task<int> Delete(int id);

        Task<PageList<CoreShed>> Get(PageParams pageParams);
        Task<PageList<CoreShed>> GetByAccount(int accountId, PageParams pageParams);
        Task<CoreShed> GetById(int id);
    }
}