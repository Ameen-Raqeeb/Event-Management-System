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
    public partial class AddTicket: Form
    {
        public AddTicket()
        {
            InitializeComponent();
        }

        private void AddTicket_Load(object sender, EventArgs e)
        {
            List<Events> events = new Controller.EventController().getEventsbyOrganizer(Session.Id);

            if (events.Count > 0)
            {
                comboBox1.DataSource = events;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id";
            }
            else
            {
                MessageBox.Show("No events found for the organizer.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int eventId = (int)comboBox1.SelectedValue;
            Events selectedEvent = new Controller.EventController().getEventById(eventId);
            string ticketType = comboBox2.Text;
            double price = double.Parse(textBox2.Text);
            int quantity = int.Parse(textBox1.Text);

            Ticket ticket = new Ticket(selectedEvent, ticketType, price, quantity);
            new Controller.TicketController().CreateTicket(ticket);

            AddTicket_Load(sender, e);

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
