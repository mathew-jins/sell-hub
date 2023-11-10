using System.ComponentModel.DataAnnotations;

namespace project_demo.Models
{
    public class Subcategory
    {
        [Key]
        public int SubcategoryId { get; set; }
        public int CategoryId { get; set; }
        public string SubcategoryName { get; set; }

    }
}
