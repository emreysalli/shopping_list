namespace emre_yasin_salli_thesis.Models
{
    public class ShoppingListDetail
    {
        public int ShoppingListDetailId { get; set; }
        public string Explanation { get; set; }
        public bool? IsTaken { get; set; }
        public int ProductId { get; set; }
        public int ShoppingListId { get; set; }
        public virtual Product? Product { get; set; }
        public virtual ShoppingList? ShoppingList { get; set; } 
    }
}
