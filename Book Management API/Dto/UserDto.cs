using Book_Management_API.Data;

namespace Book_Management_API.Dto
{
    public record UserDto(
        string Username,
        string Password,
        RoleType Role);
}
