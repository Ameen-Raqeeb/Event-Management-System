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
    // Form for adding tickets to events
    public partial class AddTicket: Form
    {
        // Initialize the form components
        public AddTicket()
        {
            InitializeComponent();
        }

        // Load event list when form opens
        private void AddTicket_Load(object sender, EventArgs e)
        {
            // Get events for current organizer
            List<Events> events = new Controller.EventController().getEventsbyOrganizer(Session.Id);

            if (events.Count > 0)
            {
                // Populate event dropdown
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

        // Handle add ticket button click
        private void button1_Click(object sender, EventArgs e)
        {
            // Get selected event details
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

        // Handle ticket type selection change
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Can be used to update UI based on ticket type
        }
    }
}
