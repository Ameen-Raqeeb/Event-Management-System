using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagmentSystem.Model
{

    class Events
    {

        public Events(string name, DateTime date, string location, string description, Organizers organizer)
        {
            Name = name;
            Date = date;
            Location = location;
            Description = description;
            Organizer = organizer;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }


        public Organizers Organizer { get; set; }
        public bool Availability { get; set; }
    }
}
