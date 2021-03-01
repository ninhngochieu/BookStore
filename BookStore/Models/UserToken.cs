using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class UserToken
    {
        public UserToken()
        {
            CreateAt = DateTime.Now;
        }
        [ForeignKey("User")]
        public int Id { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreateAt { get; set; }

        //User
        public virtual User User { get; set; }
    }
}
