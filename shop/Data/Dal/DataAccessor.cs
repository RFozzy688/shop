using shop.Data.Context;
using shop.Services.Kdf;

namespace shop.Data.Dal
{
    public class DataAccessor
    {
        private readonly DataContext _context;
        private readonly IKdfService _kdfService;
        public UserDao UserDao { get; private set; }
        public DataAccessor(DataContext context, IKdfService kdfService)
        {
            _context = context;
            _kdfService = kdfService;

            UserDao = new(_context, _kdfService);
        }
    }
}
