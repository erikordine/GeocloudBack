using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IUnitTypeRepository
    {
        Task<int> Add(UnitType unitType);
        Task<int> Update(UnitType unitType);
        Task<int> Delete(int id);

        Task<PageList<UnitType>> Get(PageParams pageParams);
        Task<UnitType> GetById(int id);
    }
}