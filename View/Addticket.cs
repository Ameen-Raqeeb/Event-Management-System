using EventManagmentSystem.Model;   // Imports the Model namespace containing class definitions like Events and Ticket
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManagmentSystem.View
{
    // Form used to allow an event organizer to add tickets for their events
    public partial class AddTicket: Form
    {
        // Constructor that initializes the form's UI components
        public AddTicket()
        {
            InitializeComponent();
        }

        // Event that fires when the form loads
        private void AddTicket_Load(object sender, EventArgs e)
        {
            // Retrieves the list of events created by the currently logged-in organizer using their session ID
            List<Events> events = new Controller.EventController().getEventsbyOrganizer(Session.Id);

            if (events.Count > 0)
            {
                // If events exist, bind them to comboBox1 for selection
                comboBox1.DataSource = events;
                comboBox1.DisplayMember = "Name";   // Display the event name in the combo box
                comboBox1.ValueMember = "Id";       // Use the event ID as the selected value
            }
            else
            {
                // Show a message if no events are found
                MessageBox.Show("No events found for the organizer.");
            }
        }

        // Event handler for the "Add Ticket" button click
        private void button1_Click(object sender, EventArgs e)
        {
            // Get the selected event ID from comboBox1
            int eventId = (int)comboBox1.SelectedValue;
            // Fetch full event details using the selected event ID
            Events selectedEvent = new Controller.EventController().getEventById(eventId);
            // Get ticket type, price, and quantity from user inputs
            string ticketType = comboBox2.Text;
            double price = double.Parse(textBox2.Text);
            int quantity = int.Parse(textBox1.Text);

            // Create a new Ticket object with the entered details
            Ticket ticket = new Ticket(selectedEvent, ticketType, price, quantity);
            // Save the new ticket using the TicketController
            new Controller.TicketController().CreateTicket(ticket);

            // Refresh the form to update any data
            AddTicket_Load(sender, e);

        }

        // Optional: this can be used for additional logic when ticket type is changed
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Currently unused, but can be used to dynamically change UI or pricing
        }
    }
}
