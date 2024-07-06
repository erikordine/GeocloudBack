using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IUserRepository
    {
        Task<int> Add(User user);
        Task<int> Update(User user);
        Task<int> Delete(int id);

        Task<PageList<User>> Get(PageParams pageParams);
        Task<PageList<User>> GetByAccount(int accountId, PageParams pageParams);
        Task<User> GetByEmail(string email);
        Task<User> GetById(int id); 

        Task<User> Login(string email, string password);
    }
}