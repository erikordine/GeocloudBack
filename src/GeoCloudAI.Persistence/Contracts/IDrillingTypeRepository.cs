using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IDrillingTypeRepository
    {
        Task<int> Add(DrillingType drillingType);
        Task<int> Update(DrillingType drillingType);
        Task<int> Delete(int id);

        Task<PageList<DrillingType>> Get(PageParams pageParams);
        Task<PageList<DrillingType>> GetByAccount(int accountId, PageParams pageParams);
        Task<DrillingType> GetById(int id);
    }
}