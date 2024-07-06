using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IDrillBoxService
    {
        Task<DrillBoxDto> Add(DrillBoxDto drillBoxDto);
        Task<DrillBoxDto> Update(DrillBoxDto drillBoxDto);
        Task<int>         Delete(int drillBoxId);

        Task<PageList<DrillBoxDto>> Get(PageParams pageParams);
        Task<PageList<DrillBoxDto>> GetByAccount  (int accountId, PageParams pageParams);
        Task<PageList<DrillBoxDto>> GetByRegion   (int regionId, PageParams pageParams);
        Task<PageList<DrillBoxDto>> GetByDeposit  (int depositId, PageParams pageParams);
        Task<PageList<DrillBoxDto>> GetByMine     (int mineId, PageParams pageParams);
        Task<PageList<DrillBoxDto>> GetByMineArea (int mineAreaId, PageParams pageParams);
        Task<PageList<DrillBoxDto>> GetByDrillHole(int drillHoleId, PageParams pageParams);
        Task<DrillBoxDto> GetById(int drillBoxId);
    }
}