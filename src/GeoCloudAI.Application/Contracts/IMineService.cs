using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IMineService
    {
        Task<MineDto> Add(MineDto mineDto);
        Task<MineDto> Update(MineDto mineDto);
        Task<int>     Delete(int mineId);

        Task<PageList<MineDto>> Get(PageParams pageParams);
        Task<PageList<MineDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<MineDto>> GetByRegion(int regionId, PageParams pageParams);
        Task<PageList<MineDto>> GetByDeposit(int depositId, PageParams pageParams);
        Task<MineDto> GetById(int mineId);
    }
}