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
    // Form for purchasing event tickets
    public partial class PurchaseTickets : Form
    {
        // Reference to parent dashboard
        private AttendeeDashboard attendeeDashboard;

        public PurchaseTickets(AttendeeDashboard attendeeDashboard)
        {
            InitializeComponent();
            this.attendeeDashboard = attendeeDashboard;
        }

        // Store selected ticket ID
        int ticketId;

        // Load events when form opens
        private void PurchaseTickets_Load(object sender, EventArgs e)
        {
            // Get all available events
            List<Events> events = new Controller.EventController().getAllEvents();

            if (events.Count > 0)
            {
                // Populate event dropdown
                comboBox1.DataSource = events;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id";
            }
            else
            {
                MessageBox.Show("No events At the Moment");
            }
        }

        // Handle ticket type selection
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected event and ticket type
            int eventId = (int)comboBox1.SelectedValue;
            string ticketType = comboBox2.Text;

            // Get ticket details
            Ticket selectedTicket = new Controller.TicketController().getTickeybyEventandType(eventId, ticketType);

            if (selectedTicket != null)
            {
                // Check ticket availability
                if (selectedTicket.Available == false)
                {
                    MessageBox.Show("Ticket is not available for purchase.");
                    return;
                }

                // Display ticket details
                textBox1.Text = selectedTicket.Price.ToString();
                textBox2.Text = selectedTicket.Quantity.ToString();
                ticketId = selectedTicket.Id;
            }
            else
            {
                MessageBox.Show("Ticket not Available for Selected Type.");
            }
        }

        // Handle purchase button click
        private void button1_Click(object sender, EventArgs e)
        {
            // Get purchase details
            int eventId = (int)comboBox1.SelectedValue;
            string ticketType = comboBox2.Text;
            int quantity = int.Parse(textBox3.Text);

            // Validate quantity
            if (quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            // Get ticket details
            Ticket selectedTicket = new Controller.TicketController().getTickeybyEventandType(eventId, ticketType);

            // Open payment gateway
            attendeeDashboard.paymentGateway(ticketId, quantity);
        }
    }
}
