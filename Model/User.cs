using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagmentSystem.Model
{
    // Abstract base class for all user types in the system
    abstract class User
    {
        // Unique identifier for the user
        public int Id { get; set; }
        
        // Name of the user
        public string Name { get; set; }
        
        // User's password for authentication
        public string Password { get; set; }

        // Constructor to create a new user with basic information
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
