using Microsoft.AspNetCore.Mvc;

namespace shop.Models.Shop
{
    public class ShopProductFormModel
    {
        [FromForm(Name = "category-id")]
        public Guid? CategoryId { get; set; }

        [FromForm(Name = "product-price")]
        public double Price { get; set; }

        [FromForm(Name = "product-name")]
        public string Name { get; set; }

        [FromForm(Name = "product-description")]
        public string Description { get; set; }

        [FromForm(Name = "product-image")]
        public IFormFile Image { get; set; }
    }
}
