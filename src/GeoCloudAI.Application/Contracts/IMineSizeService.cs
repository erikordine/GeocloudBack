using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IMineSizeService
    {
        Task<MineSizeDto> Add(MineSizeDto mineSizeDto);
        Task<MineSizeDto> Update(MineSizeDto mineSizeDto);
        Task<int>         Delete(int mineSizeId);

        Task<PageList<MineSizeDto>> Get(PageParams pageParams);
        Task<PageList<MineSizeDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<MineSizeDto> GetById(int mineSizeId);
    }
}