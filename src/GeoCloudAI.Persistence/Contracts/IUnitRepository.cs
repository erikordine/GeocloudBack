using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IUnitRepository
    {
        Task<int> Add(Unit unit);
        Task<int> Update(Unit unit);
        Task<int> Delete(int id);

        Task<PageList<Unit>> Get(PageParams pageParams);
        Task<PageList<Unit>> GetByUnitType(int unitTypeId, PageParams pageParams);
        Task<Unit> GetById(int id);
    }
}