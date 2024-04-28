using Microsoft.AspNetCore.Mvc;
using shop.Data.Dal;
using shop.Models.Shop;

namespace shop.Controllers
{
    public class ShopController(DataAccessor dataAccessor) : Controller
    {
        private readonly DataAccessor _dataAccessor = dataAccessor;

        public IActionResult Index()
        {
            ViewData["Categories"] = _dataAccessor.ShopDao.GetCategories();
            return View();
        }

        public IActionResult Category([FromRoute] String id)
        {
            ShopCategoryPageModel model = new()
            {
                Slug = id,
                CategoryId = _dataAccessor.ShopDao.GetCategoryId(id)
            };

            ViewData["Categories"] = _dataAccessor.ShopDao.GetCategories();
            ViewData["Products"] = _dataAccessor.ShopDao.GetProducts(model.CategoryId);

            return View(model);
        }
    }
}
