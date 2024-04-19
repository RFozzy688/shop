using shop.Data.Context;
using shop.Data.Entites;
using shop.Services.Kdf;

namespace shop.Data.Dal
{
    public class UserDao
    {
        private readonly IKdfService _kdfService;
        private readonly DataContext _context;

        public UserDao(DataContext context, IKdfService kdfService)
        {
            _context = context;
            _kdfService = kdfService;
        }
        public bool IsEmailFree(String email)
        {
            return !_context.Users.Where(u => u.Email == email).Any();
        }

        public User? GetUserById(String id)
        {
            try { return _context.Users.Find(Guid.Parse(id)); }
            catch { return null; }
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

        public void SignupUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
