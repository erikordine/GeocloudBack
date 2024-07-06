using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IRegionService
    {
        Task<RegionDto> Add(RegionDto regionDto);
        Task<RegionDto> Update(RegionDto regionDto);
        Task<int>       Delete(int regionId);

        Task<PageList<RegionDto>> Get(PageParams pageParams);
        Task<PageList<RegionDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<RegionDto> GetById(int regionId);
    }
}