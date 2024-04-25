using Book_Management_API.Data;
using Book_Management_API.Dto;

namespace Book_Management_API.Interfaces.Services
{
    public interface IUserService
    {
        string Login(LoginDto user);
        UserDto Register(CreateUserDto user);
        void UpdateUserRole(string userName, RoleType user);
        void DeleteUserAdminRole(string userName);
        List<DisplayUserDto> GetAllUsers();
    }
}