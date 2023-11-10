using System.ComponentModel.DataAnnotations;

namespace project_demo.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
