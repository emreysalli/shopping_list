using emre_yasin_salli_thesis.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace emre_yasin_salli_thesis.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string ListName { get; set; }
        public bool IsShopping { get; set; }
        public bool IsComplete { get; set; }

        [ForeignKey("ApplicationUser")]
        public string? UserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ICollection<ShoppingListDetail> ShoppingListDetails { get; set; } = new List<ShoppingListDetail>();
    }
}
