using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface ILithologyGroupService
    {
        Task<LithologyGroupDto> Add(LithologyGroupDto lithologyGroupDto);
        Task<LithologyGroupDto> Update(LithologyGroupDto lithologyGroupDto);
        Task<int> Delete(int lithologyGroupId);

        Task<PageList<LithologyGroupDto>> Get(PageParams pageParams);
        Task<PageList<LithologyGroupDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<LithologyGroupDto> GetById(int lithologyGroupId);
    }
}