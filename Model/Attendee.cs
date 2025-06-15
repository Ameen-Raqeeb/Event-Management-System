using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagmentSystem.Model
{
    // Represents an attendee in the event management system
    // Inherits basic user properties from the User class
    class Attendee : User
    {
        // Constructor to create a new attendee with basic information
        public Attendee(string name, string password, string contactnumber, string gender) : base(name, password)
        {
            ContactNumbers = contactnumber;
            Gender = gender;
        }

        // Contact number(s) of the attendee
        public string ContactNumbers { get; set; }
        
        // Gender of the attendee
        public string Gender { get; set; }
    }
}
