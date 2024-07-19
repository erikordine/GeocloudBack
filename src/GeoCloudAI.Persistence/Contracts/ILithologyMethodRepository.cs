using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface ILithologyMethodRepository
    {
        Task<int> Add(LithologyMethod lithologyMethod);
        Task<int> Update(LithologyMethod lithologyMethod);
        Task<int> Delete(int id);

        Task<PageList<LithologyMethod>> Get(PageParams pageParams);
        Task<PageList<LithologyMethod>> GetByAccount(int accountId, PageParams pageParams);
        Task<LithologyMethod> GetById(int id);
    }
}