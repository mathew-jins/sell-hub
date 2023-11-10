using System.ComponentModel;

namespace project_demo.Models
{
    public class DetailProduct
    {
        public int ProductId {  get; set; }
        public int UserId {  get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Price { get; set; }
       
        public string Type {  get; set; }
        public string Availability {  get; set; }
        public string PostedDate { get; set; }
        [DisplayName("Owner")]
        public string Name { get; set; }
        [DisplayName("Category")]
        public string CategoryName { get; set; }
        [DisplayName("Subcategory")]
        public string SubcategoryName { get; set; }


    }
}
