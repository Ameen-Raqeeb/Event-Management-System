using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagmentSystem.Model
{
    /// <summary>
    /// Represents an event in the Event Management System.
    /// This class contains all the essential information about an event including its details and organizer.
    /// </summary>
    class Events
    {
        /// <summary>
        /// Constructor for creating a new event with basic information.
        /// </summary>
        /// <param name="name">The name of the event</param>
        /// <param name="date">The date and time when the event will take place</param>
        /// <param name="location">The venue or location of the event</param>
        /// <param name="description">Detailed description of the event</param>
        /// <param name="organizer">The organizer responsible for the event</param>
        public Events(string name, DateTime date, string location, string description, Organizers organizer)
        {
            Name = name;
            Date = date;
            Location = location;
            Description = description;
            Organizer = organizer;
        }

        /// <summary>
        /// Unique identifier for the event
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name or title of the event
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Date and time when the event is scheduled
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Physical location or venue where the event will be held
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Detailed description of the event, including its purpose and activities
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Reference to the organizer who is managing this event
        /// </summary>
        public Organizers Organizer { get; set; }

        /// <summary>
        /// Indicates whether the event is available for registration/booking
        /// </summary>
        public bool Availability { get; set; }
    }
}
