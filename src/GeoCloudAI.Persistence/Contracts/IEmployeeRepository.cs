using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IEmployeeRepository
    {
        Task<int> Add(Employee employee);
        Task<int> Update(Employee employee);
        Task<int> Delete(int id);

        Task<PageList<Employee>> Get(PageParams pageParams);
        Task<PageList<Employee>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<Employee>> GetByCompany(int companyId, PageParams pageParams);
        Task<Employee> GetById(int id); 
    }
}