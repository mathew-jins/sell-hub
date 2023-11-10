using Microsoft.EntityFrameworkCore;
using project_demo.Models;

namespace sell_hub.Models
{
    public class SellerContextcs:DbContext
    {
        string conStr = "Data Source=.\\sqlexpress;user id=sa;password=vfx@123;Database=SELLER";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conStr);
        }
        public DbSet<UserMaster> UserMasters { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<ProductImage> productImages {  get; set; }
        public DbSet<Chat> chats { get; set; }
    }
}
