using shop.Data.Context;
using shop.Data.Entites;

namespace shop.Data.Dal
{
    public class ShopDao(DataContext context, Object dbLocker)
    {
        private readonly DataContext _context = context;
        private readonly Object _dbLocker = dbLocker;

        public List<Category> GetCategories()
        {
            List<Category> res;

            lock (_dbLocker)
            {
                res = _context.Categories.Where(c => c.IsActive).ToList();
            }

            return res;
        }

        public void AddCategory(String name, String slug, String description, String imageUrl)
        {
            AddCategory(new()
            {
                Name = name,
                Slug = slug,
                Description = description,
                ImageUrl = imageUrl,

                IsActive = true,
                Id = Guid.NewGuid(),
            });
        }

        public void AddCategory(Category category)
        {
            lock (_dbLocker)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
        }
    }
}
