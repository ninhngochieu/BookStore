using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class User
    {

        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        //Role
        public int RoleId { get; set; }
        public Role Role { get; set; }

        //User token
        public virtual UserToken UserToken { get; set; }
    }
}