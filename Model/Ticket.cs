using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagmentSystem.Model
{
    // Represents a Ticket entity in the system
    class Ticket
    {
        // Unique ID of the ticket
        public int Id { get; set; }
        // Event this ticket belongs to
        public Events Event { get; set; }

        // Type of the ticket (e.g., General, VIP)
        public string TicketType { get; set; }
        // Price of the ticket
        public double Price { get; set; }
        // Quantity available for this ticket type
        public int Quantity { get; set; }
        // Indicates if the ticket is available for sale
        public bool Available { get; set; }

        // Constructor to create a new ticket object
        public Ticket(Events eventItem, string ticketType, double price, int quantity)
        {
            Event = eventItem;          // Set the related event
            TicketType = ticketType;    // Set the ticket type
            Price = price;              // Set the price
            Quantity = quantity;        // Set the quantity
            Available = true;           // By default, ticket is available
        }

    }
}
