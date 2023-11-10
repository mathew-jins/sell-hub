using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sell_hub.Models
{
    public class ProductImage
    {
        [Key]
        public int ProductImageId {  get; set; }
        public int ProductId {  get; set; }
        public string ImagePath {  get; set; }
        [NotMapped]
        public IFormFile GalleryPath {  get; set; }
    }
}
