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
    public partial class ViewEventDetails: Form
    {
        public ViewEventDetails()
        {
            InitializeComponent();
        }

        private void ViewEventDetails_Load(object sender, EventArgs e)
        {
            List<Events> events = new Controller.EventController().getEventsbyOrganizer(Session.Id); //gets all the events organized by the user

            //if events exists
            if (events.Count > 0)
            {
                comboBox1.DataSource = events; //sets the datasource as events list
                comboBox1.DisplayMember = "Name"; //displays the names of the events in the dropdown
                comboBox1.ValueMember = "Id"; //stores the id values
            }
            else
            {
                MessageBox.Show("No events found for the organizer.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int eventId = (int)comboBox1.SelectedValue;

            DataTable dataTable = new OrganizerController().getEventDetails(eventId); //gets the event details of the selected event using the organizer controller in a datatable

            dataGridView1.DataSource = dataTable; //merges the data to a datagrid view 
        }
    }
}
