using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace project_demo.Models
{
    public class DetailReport
    {
        public int ReportId {  get; set; }
        public int ProductId {  get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Price {  get; set; }
        public string Type { get; set; }
        public string Availability { get; set; }
        public string PostedDate { get; set; }
        [DisplayName("Category")]
        public string CategoryName { get; set; }
        [DisplayName("Subcategory")]
        public string SubCategoryName {  get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Name {  get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

    }
}
