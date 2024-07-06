using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface ICoreShedService
    {
        Task<CoreShedDto> Add(CoreShedDto coreShedDto);
        Task<CoreShedDto> Update(CoreShedDto coreShedDto);
        Task<int> Delete(int coreShedId);

        Task<PageList<CoreShedDto>> Get(PageParams pageParams);
        Task<PageList<CoreShedDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<CoreShedDto> GetById(int coreShedId);
    }
}