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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EventManagmentSystem.View
{
    // Form to allow attendees to purchase tickets
    public partial class PurchaseTickets : Form
    {
        // Reference to the AttendeeDashboard to call payment function
        private AttendeeDashboard attendeeDashboard;
        // Constructor that receives the dashboard instance
        public PurchaseTickets(AttendeeDashboard attendeeDashboard)
        {
            InitializeComponent();
            this.attendeeDashboard = attendeeDashboard;
        }

        // Stores selected ticket ID for purchase
        int ticketId;
        // Load event: populates event dropdown (comboBox1)
        private void PurchaseTickets_Load(object sender, EventArgs e)
        {
            // Get all available events
            List<Events> events = new Controller.EventController().getAllEvents();

            if (events.Count > 0)
            {
                // Bind the events to comboBox1
                comboBox1.DataSource = events;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id";
            }
            else
            {
                // If no events found, show message
                MessageBox.Show("No events At the Moment");
            }
        }

        // Triggered when ticket type is selected (comboBox2)
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected event ID
            int eventId = (int)comboBox1.SelectedValue;
            // Get selected ticket type (e.g. General/VIP)
            string ticketType = comboBox2.Text;

            // Fetch ticket using event ID and type
            Ticket selectedTicket = new Controller.TicketController().getTickeybyEventandType(eventId, ticketType);

            if (selectedTicket != null)
            {
                if (selectedTicket.Available == false)
                {
                    // If ticket is not available, show warning
                    MessageBox.Show("Ticket is not available for purchase.");
                    return;
                }

                // Display ticket price and quantity
                textBox1.Text = selectedTicket.Price.ToString();
                textBox2.Text = selectedTicket.Quantity.ToString();
                // Store ticket ID for later use in purchase
                ticketId = selectedTicket.Id;
            }
            else
            {
                MessageBox.Show("Ticket not Available for Selected Type.");
            }

        }

        // Purchase button click event
        private void button1_Click(object sender, EventArgs e)
        {
            // Get event and ticket info
            int eventId = (int)comboBox1.SelectedValue;
            string ticketType = comboBox2.Text;
            int quantity = int.Parse(textBox3.Text);

            // Validate quantity
            if (quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            // Get the selected ticket again to double-check data
            Ticket selectedTicket = new Controller.TicketController().getTickeybyEventandType(eventId, ticketType);

            // Call payment gateway method in AttendeeDashboard, passing ticket ID and quantity
            attendeeDashboard.paymentGateway(ticketId, quantity);

        }
    }
}
