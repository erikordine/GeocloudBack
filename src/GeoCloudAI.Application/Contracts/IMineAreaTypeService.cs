using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IMineAreaTypeService
    {
        Task<MineAreaTypeDto> Add(MineAreaTypeDto mineAreaTypeDto);
        Task<MineAreaTypeDto> Update(MineAreaTypeDto mineAreaTypeDto);
        Task<int>             Delete(int mineAreaTypeId);

        Task<PageList<MineAreaTypeDto>> Get(PageParams pageParams);
        Task<PageList<MineAreaTypeDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<MineAreaTypeDto> GetById(int mineAreaTypeId);
    }
}