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
    public partial class PurchaseTickets: Form
    {
        private AttendeeDashboard attendeeDashboard;
        public PurchaseTickets(AttendeeDashboard attendeeDashboard)
        {
            InitializeComponent();
            this.attendeeDashboard = attendeeDashboard;
        }

        int ticketId;
        private void PurchaseTickets_Load(object sender, EventArgs e)
        {
            List<Events> events = new Controller.EventController().getAllEvents();

            if (events.Count > 0)
            {
                comboBox1.DataSource = events;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id";
            }
            else
            {
                MessageBox.Show("No events At the Moment");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int eventId = (int)comboBox1.SelectedValue;
            string ticketType = comboBox2.Text;

            Ticket selectedTicket = new Controller.TicketController().getTickeybyEventandType(eventId, ticketType);

            if (selectedTicket != null)
            {
                if( selectedTicket.Available == false)
                {
                    MessageBox.Show("Ticket is not available for purchase.");
                    return;
                }

                textBox1.Text = selectedTicket.Price.ToString();
                textBox2.Text = selectedTicket.Quantity.ToString();
                ticketId = selectedTicket.Id;
            }
            else
            {
                MessageBox.Show("Ticket not Available for Selected Type.");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int eventId = (int)comboBox1.SelectedValue;
            string ticketType = comboBox2.Text;
            int quantity = int.Parse(textBox3.Text);

            if (quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            Ticket selectedTicket = new Controller.TicketController().getTickeybyEventandType(eventId, ticketType);

            attendeeDashboard.paymentGateway(ticketId, quantity);

        }
    }
}
