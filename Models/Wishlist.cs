using System.ComponentModel.DataAnnotations;

namespace project_demo.Models
{
    public class Wishlist
    {
        [Key]
        public int WishlistId {  get; set; }
        public int UserId {  get; set; }
        public int ProductId {  get; set; }
    }
}
