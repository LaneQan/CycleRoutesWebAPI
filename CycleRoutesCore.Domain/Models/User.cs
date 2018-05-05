﻿namespace CycleRoutesCore.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdministrator { get; set; }
        public string Image { get; set; }
    }
}