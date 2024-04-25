﻿using Book_Management_API.Model;

namespace Book_Management_API.Interfaces.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUser(string user);
        void UpdateUser(User user);
    }
}