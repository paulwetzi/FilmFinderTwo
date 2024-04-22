using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 * 
 * This is the User-class
 * 
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 */

namespace FilmFinder
{
    public class User
    {
        // Properties
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Constructor
        public User(int id, string email, string password, string name, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Email = email;
            Password = password;
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        // Methods
        public void DisplayUserInfo()
        {
            Console.WriteLine($"User ID: {Id}");
            Console.WriteLine($"Email: {Email}");
            //Console.WriteLine($"Password: {Password}"); // Note: Be careful with displaying passwords; this is generally not a good idea
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Created At: {CreatedAt}");
            Console.WriteLine($"Updated At: {UpdatedAt}");
        }
    }
}

