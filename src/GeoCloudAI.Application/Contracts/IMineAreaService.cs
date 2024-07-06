using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IMineAreaService
    {
        Task<MineAreaDto> Add(MineAreaDto mineAreaDto);
        Task<MineAreaDto> Update(MineAreaDto mineAreaDto);
        Task<int>         Delete(int mineAreaId);

        Task<PageList<MineAreaDto>> Get(PageParams pageParams);
        Task<PageList<MineAreaDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<MineAreaDto>> GetByRegion(int regionId, PageParams pageParams);
        Task<PageList<MineAreaDto>> GetByDeposit(int depositId, PageParams pageParams);
        Task<PageList<MineAreaDto>> GetByMine(int mineId, PageParams pageParams);
        Task<MineAreaDto> GetById(int mineAreaId);
    }
}