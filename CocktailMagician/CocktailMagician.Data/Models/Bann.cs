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
            this.EnDateTime = endDateTime;
            this.User = user;
        }
        public string Id { get; set; }
        public string Reason { get; set; }
        public DateTime EnDateTime { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
