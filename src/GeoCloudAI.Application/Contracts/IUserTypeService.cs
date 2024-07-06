using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IUnitTypeService
    {
        Task<UnitTypeDto> Add(UnitTypeDto unitTypeDto);
        Task<UnitTypeDto> Update(UnitTypeDto unitTypeDto);
        Task<int>         Delete(int unitTypeId);

        Task<PageList<UnitTypeDto>> Get(PageParams pageParams);
        Task<UnitTypeDto> GetById(int unitTypeId);
    }
}