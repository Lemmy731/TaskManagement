using TaskManagementDomain.DTO;

namespace TaskManagementSystemApi.IService
{
    public interface IAccountService
    {
        Task<string> Register(UserDto registerDTO);
        Task<string> Login(LoginDTO loginDTO);
    }
}
