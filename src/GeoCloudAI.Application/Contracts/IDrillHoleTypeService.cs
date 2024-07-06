using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IDrillHoleTypeService
    {
        Task<DrillHoleTypeDto> Add(DrillHoleTypeDto drillHoleTypeDto);
        Task<DrillHoleTypeDto> Update(DrillHoleTypeDto drillHoleTypeDto);
        Task<int> Delete(int drillHoleTypeId);

        Task<PageList<DrillHoleTypeDto>> Get(PageParams pageParams);
        Task<PageList<DrillHoleTypeDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<DrillHoleTypeDto> GetById(int drillHoleTypeId);
    }
}