using Microsoft.AspNetCore.Mvc;
using shop.Data.Dal;

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
    }
}
