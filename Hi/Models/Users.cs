using System.ComponentModel.DataAnnotations;

namespace Hi.Models
{
    public class Users
    {
        public string key { get; set; }

        [Required]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
