using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagmentSystem.Model
{
    // Static class to manage the current user's session information
    public static class Session
    {
        // ID of the currently logged-in user
        public static int Id { get; set; }
        
        // Username of the currently logged-in user
        public static string Username { get; set; }
        
        // Password of the currently logged-in user
        public static string Password { get; set; }
        
        // Type of user (e.g., Admin, Organizer, Attendee)
        public static string UserType { get; set; }
    }
}
