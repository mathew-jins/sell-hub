using System.ComponentModel.DataAnnotations;

namespace project_demo.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
