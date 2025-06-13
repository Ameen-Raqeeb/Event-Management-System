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
    public partial class DeleteEvent: Form
    {
        public DeleteEvent()
        {
            InitializeComponent();
        }

        private void DeleteEvent_Load(object sender, EventArgs e)
        {
            List<Events> events = new Controller.EventController().getEventsbyOrganizer(Session.Id); //gets the lists of all the events under a specific organizer

            if (events.Count > 0)
            {
                comboBox1.DataSource = events; //sets the data source for the dropdown menu
                comboBox1.DisplayMember = "Name"; //includes the event to the dropdown menu
                comboBox1.ValueMember = "Id"; //value of each item is assigned to the event id
            }
            else
            {
                MessageBox.Show("No events found for the organizer.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int EventId = (int)comboBox1.SelectedValue; //gets the id of the selected event
            new Controller.EventController().deleteEvent(EventId); //calls the method to delete the event

            DeleteEvent_Load(sender, e); 
        }
    }
}
