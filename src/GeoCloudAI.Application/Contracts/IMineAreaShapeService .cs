using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IMineAreaShapeService
    {
        Task<MineAreaShapeDto> Add(MineAreaShapeDto mineAreaShapeDto);
        Task<MineAreaShapeDto> Update(MineAreaShapeDto mineAreaShapeDto);
        Task<int>              Delete(int mineAreaShapeId);

        Task<PageList<MineAreaShapeDto>> Get(PageParams pageParams);
        Task<PageList<MineAreaShapeDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<MineAreaShapeDto> GetById(int mineAreaShapeId);
    }
}