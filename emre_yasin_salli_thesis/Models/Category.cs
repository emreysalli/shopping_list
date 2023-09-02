using Microsoft.EntityFrameworkCore;

namespace emre_yasin_salli_thesis.Models
{
    [Index(nameof(Category.Name), IsUnique = true)]
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
