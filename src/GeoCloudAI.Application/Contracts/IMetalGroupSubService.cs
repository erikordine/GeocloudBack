using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IMetalGroupSubService
    {
        Task<MetalGroupSubDto> Add(MetalGroupSubDto metalGroupSubDto);
        Task<MetalGroupSubDto> Update(MetalGroupSubDto metalGroupSubDto);
        Task<int> Delete(int metalGroupSubId);

        Task<PageList<MetalGroupSubDto>> Get(PageParams pageParams);
        Task<PageList<MetalGroupSubDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<MetalGroupSubDto>> GetByMetalGroup(int metalGroupId, PageParams pageParams);
        Task<MetalGroupSubDto> GetById(int metalGroupSubId);
    }
}