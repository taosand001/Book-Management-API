using Book_Management_API.Dto;

namespace Book_Management_API.Interfaces.Services
{
    public interface IUserService
    {
        string Login(LoginDto user);
        void Register(UserDto user);
        void UpdateUserRole(string userName, UserRoleDto user);
        void DeleteUserAdminRole(string userName);
    }
}