using Microsoft.EntityFrameworkCore;

namespace emre_yasin_salli_thesis.Models
{
    [Index(nameof(Product.Name), IsUnique = true)]
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
