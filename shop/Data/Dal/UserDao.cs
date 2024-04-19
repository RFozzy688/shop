﻿using shop.Data.Context;
using shop.Data.Entites;

namespace shop.Data.Dal
{
    public class UserDao
    {
        private readonly DataContext _context;
        public UserDao(DataContext context)
        {
            _context = context;
        }
        public bool IsEmailFree(String email)
        {
            return !_context.Users.Where(u => u.Email == email).Any();
        }

        public void SignupUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
