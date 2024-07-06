using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface ICompanyTypeRepository
    {
        Task<int> Add(CompanyType companyType);
        Task<int> Update(CompanyType companyType);
        Task<int> Delete(int id);

        Task<PageList<CompanyType>> Get(PageParams pageParams);
        Task<PageList<CompanyType>> GetByAccount(int accountId, PageParams pageParams);
        Task<CompanyType> GetById(int id);
    }
}