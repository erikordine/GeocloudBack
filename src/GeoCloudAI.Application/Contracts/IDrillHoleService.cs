using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IDrillHoleService
    {
        Task<DrillHoleDto> Add(DrillHoleDto drillHoleDto);
        Task<DrillHoleDto> Update(DrillHoleDto drillHoleDto);
        Task<int>          Delete(int drillHoleId);

        Task<PageList<DrillHoleDto>> Get(PageParams pageParams);
        Task<PageList<DrillHoleDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<DrillHoleDto>> GetByRegion(int regionId, bool direct, PageParams pageParams);
        Task<PageList<DrillHoleDto>> GetByDeposit(int depositId, bool direct, PageParams pageParams);
        Task<PageList<DrillHoleDto>> GetByMine(int mineId, bool direct, PageParams pageParams);
        Task<PageList<DrillHoleDto>> GetByMineArea(int mineAreaId, PageParams pageParams);
        Task<DrillHoleDto> GetById(int drillHoleId);
    }
}