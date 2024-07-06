using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IEmployeeRoleRepository
    {
        Task<int> Add(EmployeeRole employeeRole);
        Task<int> Update(EmployeeRole employeeRole);
        Task<int> Delete(int id);

        Task<PageList<EmployeeRole>> Get(PageParams pageParams);
        Task<PageList<EmployeeRole>> GetByAccount(int accountId, PageParams pageParams);
        Task<EmployeeRole> GetById(int id);
    }
}