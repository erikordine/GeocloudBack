using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IUnitService
    {
        Task<UnitDto> Add(UnitDto unitDto);
        Task<UnitDto> Update(UnitDto unitDto);
        Task<int>     Delete(int unitId);

        Task<PageList<UnitDto>> Get(PageParams pageParams);
        Task<PageList<UnitDto>> GetByUnitType(int unitTypeId, PageParams pageParams);
        Task<UnitDto> GetById(int unitId);
    }
}