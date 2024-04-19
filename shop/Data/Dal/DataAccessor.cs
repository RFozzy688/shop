using shop.Data.Context;

namespace shop.Data.Dal
{
    public class DataAccessor
    {
        private readonly DataContext _context;
        public UserDao UserDao { get; private set; }
        public DataAccessor(DataContext context)
        {
            _context = context;
            UserDao = new(_context);
        }
    }
}
