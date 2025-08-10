using System.ComponentModel.DataAnnotations.Schema;

namespace ZayShop.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public int ReviewCount { get; set; }
        public decimal Price { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
