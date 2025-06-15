using EventManagmentSystem.Controller;
using EventManagmentSystem.Model;
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
    // Form to allow organizers to edit ticket information for their events
    public partial class EditTicket : Form
    {
        public EditTicket()
        {
            InitializeComponent();
        }

        // Stores the ID of the selected ticket
        int ticketId;

        // Loads all events created by the logged-in organizer
        private void EditTicket_Load(object sender, EventArgs e)
        {
            // Get events specific to the organizer from the session
            List<Events> events = new Controller.EventController().getEventsbyOrganizer(Session.Id);

            if (events.Count > 0)
            {
                // Bind events to comboBox1 for selection
                comboBox1.DataSource = events;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id";
            }
            else
            {
                MessageBox.Show("No events found for the organizer.");
            }
        }

        // When ticket type is selected, fetch ticket details from DB
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int eventId = (int)comboBox1.SelectedValue;
            string ticketType = comboBox2.Text;

            // Get ticket based on event ID and ticket type
            Ticket selectedTicket = new Controller.TicketController().getTickeybyEventandType(eventId, ticketType);

            if (selectedTicket != null)
            {
                // Display current ticket details in the UI
                textBox1.Text = selectedTicket.Price.ToString();
                textBox2.Text = selectedTicket.Quantity.ToString();
                comboBox3.Text = selectedTicket.Available.ToString();
                ticketId = selectedTicket.Id;
            }
            else
            {
                MessageBox.Show("Ticket not found.");
            }
        }

        // On clicking 'Update' button, validate and save ticket updates
        private void button1_Click(object sender, EventArgs e)
        {
            // Get selected event and type
            int eventId = (int)comboBox1.SelectedValue;
            string ticketType = comboBox2.Text;
            // Parse price and quantity, with fallback to 0
            double price = textBox1.Text == "" ? 0 : double.Parse(textBox1.Text);
            int quantity = textBox2.Text == "" ? 0 : int.Parse(textBox2.Text);

            // Validate numeric input
            if (!double.TryParse(textBox1.Text, out price) || !int.TryParse(textBox2.Text, out quantity))
            {
                MessageBox.Show("Please enter valid price and quantity.");
                return;
            }

            // Parse availability from comboBox3
            bool available = Convert.ToBoolean(comboBox3.Text);

            // Create new Ticket object with updated values
            Ticket updatedTicket = new Ticket(new Controller.EventController().getEventById(eventId), ticketType, price, quantity)
            {
                Available = available,
                Id = ticketId
            };

            // Call controller method to update in DB
            new TicketController().UpdateTicket(updatedTicket);

            // Reload the event list to reflect changes
            EditTicket_Load(sender, e);

        }
    }
}
