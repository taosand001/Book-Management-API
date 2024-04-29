using Book_Management_API.Database;
using Book_Management_API.Interfaces.Repositories;
using Book_Management_API.Model;

namespace Book_Management_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BookContext _context;

        public UserRepository(BookContext context)
        {
            _context = context;
        }

        public User GetUser(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.Username == userName);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers() => _context.Users.ToList();

    }
}
