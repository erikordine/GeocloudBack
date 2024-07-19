using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface ILithologyService
    {
        Task<LithologyDto> Add(LithologyDto lithologyDto);
        Task<LithologyDto> Update(LithologyDto lithologyDto);
        Task<int> Delete(int lithologyId);

        Task<PageList<LithologyDto>> Get(PageParams pageParams);
        Task<PageList<LithologyDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<LithologyDto>> GetByLithologyGroupSub(int groupSubId, PageParams pageParams);
        Task<LithologyDto> GetById(int lithologyId);
    }
}