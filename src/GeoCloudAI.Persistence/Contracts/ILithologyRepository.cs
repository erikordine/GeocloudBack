using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface ILithologyRepository
    {
        Task<int> Add(Lithology lithology);
        Task<int> Update(Lithology lithology);
        Task<int> Delete(int id);

        Task<PageList<Lithology>> Get(PageParams pageParams);
        Task<PageList<Lithology>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<Lithology>> GetByLithologyGroupSub(int groupSubId, PageParams pageParams);
        Task<Lithology> GetById(int id);
    }
}