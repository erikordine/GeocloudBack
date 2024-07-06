using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IMetalGroupService
    {
        Task<MetalGroupDto> Add(MetalGroupDto metalGroupDto);
        Task<MetalGroupDto> Update(MetalGroupDto metalGroupDto);
        Task<int>           Delete(int metalGroupId);

        Task<PageList<MetalGroupDto>> Get(PageParams pageParams);
        Task<PageList<MetalGroupDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<MetalGroupDto>> GetByOreGeneticType(int oreGeneticTypeId, PageParams pageParams);
        Task<MetalGroupDto> GetById(int metalGroupId);
    }
}