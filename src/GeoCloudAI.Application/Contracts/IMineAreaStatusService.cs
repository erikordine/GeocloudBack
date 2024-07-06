using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IMineAreaStatusService
    {
        Task<MineAreaStatusDto> Add(MineAreaStatusDto mineAreaStatusDto);
        Task<MineAreaStatusDto> Update(MineAreaStatusDto mineAreaStatusDto);
        Task<int>               Delete(int mineAreaStatusId);

        Task<PageList<MineAreaStatusDto>> Get(PageParams pageParams);
        Task<PageList<MineAreaStatusDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<MineAreaStatusDto> GetById(int mineAreaStatusId);
    }
}