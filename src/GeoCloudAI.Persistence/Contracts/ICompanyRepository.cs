using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface ICompanyRepository
    {
        Task<int> Add(Company company);
        Task<int> Update(Company company);
        Task<int> Delete(int id);

        Task<PageList<Company>> Get(PageParams pageParams);
        Task<PageList<Company>> GetByAccount(int accountId, PageParams pageParams);
        Task<Company> GetById(int id); 
    }
}