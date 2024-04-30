using AutoFixture.Kernel;
using Book_Management_API.Dto;
using Book_Management_API.Model;
using Book_Management_API.Data;

namespace Book_Management_API_Test.Data
{
    public class UserFailureSpecimenBuilder : ISpecimenBuilder
    {
        public object? Create(object request, ISpecimenContext context)
        {
            if (request is Type t && t == typeof(User))
            {
                return new User
                {
                    Id = 1,
                    PasswordHash = [],
                    PasswordSalt = [],
                    Role = RoleType.Admin,
                    Username = "Username"
                };
            }

            if (request is Type t2 && t2 == typeof(UserDto))
            {
                return null;
            }

            if (request is Type t3 && t3 == typeof(CreateUserDto))
            {
                return new CreateUserDto
                (
                    Username: "Username",
                    Password: "Password"
                );
            }

            if (request is Type t4 && t4 == typeof(LoginDto))
            {
                return null;
            }

            if (request is Type t5 && t5 == typeof(string))
            {
                return null;
            }

            return new NoSpecimen();
        }
    }
}
