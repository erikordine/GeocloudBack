using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IUserService
    {
        Task<UserDto> Add(UserDto userDto);
        Task<string>  Register(UserDto userDto);
        Task<UserDto> Update(UserDto userDto);
        Task<int>     Delete(int userId);

        Task<PageList<UserDto>> Get(PageParams pageParams);
        Task<PageList<UserDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<UserDto> GetById(int userId);
        Task<UserDto> GetByEmail(string email);

        Task<UserDto> Login(string email, string password);
        Task<string>  GenerateToken(UserDto userDto);
    }
}