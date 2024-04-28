using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Data.Dal;
using shop.Models.Shop;
using shop.Services.Upload;

namespace shop.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class ShopApiController(DataAccessor dataAccessor, IUploadServise uploadServise) : ControllerBase
    {
        private readonly DataAccessor _dataAccessor = dataAccessor;
        private readonly IUploadServise _uploadServise = uploadServise;

        [HttpPost("category")]
        public object DoPost(ShopCategoryFormModel model)
        {
            if (String.IsNullOrEmpty(model.Slug) ||
                String.IsNullOrEmpty(model.Name) ||
                String.IsNullOrEmpty(model.Description))
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

                return "Missing required data";
            }

            try
            {
                _dataAccessor.ShopDao.AddCategory(
                    name: model.Name,
                    description: model.Description,
                    slug: model.Slug,
                    imageUrl: _uploadServise.SaveFormFile(model.Image, "wwwroot/img/shop")
                );

                Response.StatusCode = StatusCodes.Status201Created;

                return "Ok";
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return ex.Message;
            }
        }

        [HttpPost("product")]
        public object AddProduct(ShopProductFormModel model)
        {
            if (model?.Price == null ||
                String.IsNullOrEmpty(model.Name) ||
                String.IsNullOrEmpty(model.Description))
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

                return "Missing required data";
            }
            
            try
            {
                _dataAccessor.ShopDao.AddProduct(
                    categortId: model.CategoryId,
                    name: model.Name,
                    description: model.Description,
                    price: model.Price,
                    imageUrl: _uploadServise.SaveFormFile(model.Image, "wwwroot/img/shop")
                );

                Response.StatusCode = StatusCodes.Status201Created;

                return "Ok";
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return ex.Message;
            }
        }
    }
}
