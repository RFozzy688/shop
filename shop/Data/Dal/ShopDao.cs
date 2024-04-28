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

        public string GetCategoryId(string slug)
        {
            string id;

            lock (_dbLocker)
            {
                id = _context.Categories.Where(c => slug.CompareTo(c.Slug) == 0).FirstOrDefault().Id.ToString();
            }
            return id;
        }

        public List<Product> GetProducts(string categoryId)
        {
            List<Product> res;

            lock (_dbLocker)
            {
                res = _context.Products.Where(p => categoryId.CompareTo(p.CategoryId.ToString()) == 0).ToList();
            }

            return res;
        }

        public Product AddProduct(Guid? categortId, String name, Double price, String description, String imageUrl)
        {
            Product product = new()
            {
                Name = name,
                Price = price,
                Description = description,
                ImageUrl = imageUrl,
                CategoryId = categortId,

                IsActive = true,
                Id = Guid.NewGuid(),
            };
            return AddProduct(product);
        }

        public Product AddProduct(Product product)
        {
            lock (_dbLocker)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            return product;
        }
    }
}
