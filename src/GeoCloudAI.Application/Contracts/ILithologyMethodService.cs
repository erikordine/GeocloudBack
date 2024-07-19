using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface ILithologyMethodService
    {
        Task<LithologyMethodDto> Add(LithologyMethodDto lithologyMethodDto);
        Task<LithologyMethodDto> Update(LithologyMethodDto lithologyMethodDto);
        Task<int> Delete(int lithologyGroupId);

        Task<PageList<LithologyMethodDto>> Get(PageParams pageParams);
        Task<PageList<LithologyMethodDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<LithologyMethodDto> GetById(int lithologyGroupId);
    }
}