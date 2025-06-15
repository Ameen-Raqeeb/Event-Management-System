using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagmentSystem.Model
{
    // Represents an event organizer in the system
    // Inherits basic user properties from the User class
    class Organizers : User
    {
        // Constructor to create a new organizer with basic information
        public Organizers(string name, string password, string contactnumber, string email) : base(name, password)
        {
            ContactNumbers = contactnumber;
            Email = email;
        }

        // Contact number(s) of the organizer
        public string ContactNumbers { get; set; }
        
        // Email address of the organizer
        public string Email { get; set; }
    }
}
