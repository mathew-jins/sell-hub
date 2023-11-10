using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace project_demo.Models
{
    public class Product
    {
        [Key]
        public int ProductId {  get; set; }
        public int UserId { get; set; }
        public string ProductName {  get; set; }
        public string ProductDescription { get; set; }
        public int Price {  get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId {  get; set; }
        public string Type {  get; set; }
        public string Availability { get; set; }
        public string PostedDate {  get; set; }
    }
}
