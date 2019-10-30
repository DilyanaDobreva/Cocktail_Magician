using System;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Models
{
    public class Bann
    {
        public Bann()
        {

        }
        public Bann(string reason, DateTime endDateTime, User user)
        {
            this.Reason = reason;
            this.EndDateTime = endDateTime;
            this.User = user;
        }
        public string Id { get; set; }
        public string Reason { get; set; }
        public DateTime EndDateTime { get; set; }
<<<<<<< HEAD
=======
        public bool IsDeleted { get; set; }
>>>>>>> f1c077fc7ca1c09a2a1de7f3378bbabb698838aa
        public string UserId { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }
    }
}
