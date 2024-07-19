using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface ILithologyGroupSubService
    {
        Task<LithologyGroupSubDto> Add(LithologyGroupSubDto lithologyGroupSubDto);
        Task<LithologyGroupSubDto> Update(LithologyGroupSubDto lithologyGroupSubDto);
        Task<int>                  Delete(int lithologyGroupSubId);

        Task<PageList<LithologyGroupSubDto>> Get(PageParams pageParams);
        Task<PageList<LithologyGroupSubDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<LithologyGroupSubDto>> GetByLithologyGroup(int groupId, PageParams pageParams);
        Task<LithologyGroupSubDto> GetById(int lithologyGroupSubId);
    }
}