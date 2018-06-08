using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CycleRoutesCore.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public string CurrentCity { get; set; }
        public string Phone { get; set; }
        public string About { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdministrator { get; set; }
        public string Image { get; set; }
        public DateTime RegisteredAt { get; set; }

        [NotMapped]
        public int RoutesCount { get; set; }
        [NotMapped]
        public int LikesCount { get; set; }
    }
}