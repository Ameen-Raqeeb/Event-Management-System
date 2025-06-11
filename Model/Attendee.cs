using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagmentSystem.Model
{
    class Attendee : User
    {
        public Attendee(string name, string password, string contactnumber, string gender) : base(name, password)
        {
            ContactNumbers = contactnumber;
            Gender = gender;
        }
        public string ContactNumbers { get; set; }
        public string Gender { get; set; }

       
    }
}
