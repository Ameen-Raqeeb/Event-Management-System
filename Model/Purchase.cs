using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagmentSystem.Model
{
    // Represents a ticket purchase transaction in the system
    class Purchase
    {
        // Constructor to create a new purchase record
        public Purchase(int ticket_id, int quantity)
        {
            this.Ticket_id = ticket_id;
            this.Quantity = quantity;
            this.Attendee_id = Session.Id;
            this.Total = 0;
        }

        // Unique identifier for the purchase
        public int Id { get; set; }
        
        // ID of the ticket being purchased
        public int Ticket_id { get; set; }
        
        // Number of tickets purchased
        public int Quantity { get; set; }
        
        // ID of the attendee making the purchase
        public int Attendee_id { get; set; }
        
        // Total cost of the purchase
        public double Total { get; set; }
    }
}
