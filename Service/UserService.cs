using Book_Management_API.Data;
using Book_Management_API.Dto;
using Book_Management_API.Interfaces.Repositories;
using Book_Management_API.Interfaces.Services;
using Book_Management_API.Model;
using System.Security.Cryptography;

namespace Book_Management_API.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public string Login(LoginDto user)
        {
            var existingUser = _userRepository.GetUser(user.Username);
            if (existingUser == null || !VerifyPasswordHash(user.Password, existingUser.PasswordHash, existingUser.PasswordSalt))
            {
                throw new UnauthorizedErrorException("Username or password is incorrect");
            }
            var token = _jwtService.GenerateSecurityToken(existingUser);
            return token;

        }

        public void Register(UserDto user)
        {
            var newUser = CreateUser(user);
            _userRepository.AddUser(newUser);
        }

        public void UpdateUserRole(string userName, UserRoleDto user)
        {
            var existingUser = _userRepository.GetUser(userName);
            if (existingUser == null)
            {
                throw new NotFoundErrorException("User not found");
            }

            existingUser.Role = user.Role;
            _userRepository.UpdateUser(existingUser);
        }

        public void DeleteUserAdminRole(string userName)
        {
            var existingUser = _userRepository.GetUser(userName);
            if (existingUser == null)
            {
                throw new NotFoundErrorException("User not found");
            }
            existingUser.Role = RoleType.User;
            _userRepository.UpdateUser(existingUser);
        }

        private User CreateUser(UserDto user)
        {
            var existingUser = _userRepository.GetUser(user.Username);
            if (existingUser != null)
            {
                throw new ConflictErrorException("User already exists");
            }

            CreatePasswordHash(user.Password, out var passwordHash, out var passwordSalt);
            return new User
            {
                Username = user.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = user.Role
            };
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }

    }
}
