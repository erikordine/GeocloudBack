using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IMineStatusService
    {
        Task<MineStatusDto> Add(MineStatusDto mineStatusDto);
        Task<MineStatusDto> Update(MineStatusDto mineStatusDto);
        Task<int>           Delete(int mineStatusId);

        Task<PageList<MineStatusDto>> Get(PageParams pageParams);
        Task<PageList<MineStatusDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<MineStatusDto> GetById(int mineStatusId);
    }
}