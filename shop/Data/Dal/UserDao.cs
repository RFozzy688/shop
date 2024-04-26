using shop.Data.Context;
using shop.Data.Entites;
using shop.Services.Kdf;

namespace shop.Data.Dal
{
    public class UserDao
    {
        private readonly DataContext _context;
        private readonly IKdfService _kdfService;
        private readonly Object _dbLocker;
        public UserDao(DataContext context, IKdfService kdfService, object dbLocker)
        {
            _context = context;
            _kdfService = kdfService;
            _dbLocker = dbLocker;
        }

        public User? GetUserById(String id)
        {
            User? user;

            lock (_dbLocker)
            {
                try { user = _context.Users.Find(Guid.Parse(id)); }
                catch { user = null; }
            }

            return user;
        }

        public User? Authenticate(String email, String password)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null && _kdfService.GetDerivedKey(password, user.Salt) == user.DerivedKey)
            {
                return user;
            }

            return null;
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
