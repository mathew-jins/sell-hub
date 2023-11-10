using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_demo.Models
{
    public class UserMaster
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address {  get; set; }
        public int Status {  get; set; }
        public string Role { get; set; }
        public string ProfilePath { get; set; }
        [NotMapped]
        public IFormFile ProfilePicture { get; set; }
    }
}

