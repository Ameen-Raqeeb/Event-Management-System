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
    public partial class EditEvent: Form
    {
        public EditEvent()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int eventId = (int)comboBox1.SelectedValue; //get the id of the selected id
            Events selectedEvent = new Controller.EventController().getEventById(eventId); //get the event detaills from the DB and creates a new object
            if (selectedEvent != null)
            {
                
                textBox2.Text = selectedEvent.Description;
                dateTimePicker1.Value = selectedEvent.Date;
                textBox3.Text = selectedEvent.Location;
                comboBox2.Text = selectedEvent.Availability.ToString();
            }
            else
            {
                MessageBox.Show("Event not found.");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void EditEvent_Load(object sender, EventArgs e)
        {
            List<Events> events = new Controller.EventController().getEventsbyOrganizer(Session.Id); //gets the all events from a specific organizer

            if (events.Count > 0)
            {
                comboBox1.DataSource = events; //sets the datasource for the dropdown menu as events
                comboBox1.DisplayMember = "Name"; //shows the events under the dropdown menu
                comboBox1.ValueMember = "Id"; //uses the event id as the value
            }
            else
            {
                MessageBox.Show("No events found for the organizer.");
            }
        }

        private void button1_Click(object sender, EventArgs e) //updates the event details
        {

            int eventId = (int)comboBox1.SelectedValue; 

            //gets the updated data from the fields

            String eventName = comboBox1.Text;
            DateTime dateTime = dateTimePicker1.Value;
            string description = textBox2.Text;
            string location = textBox3.Text;
            string availability = comboBox2.Text;
            Organizers organizer = new Controller.OrganizerController().getOrganizersfromId(Session.Id);

            //ensures all the fields are updated
            if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(location) || string.IsNullOrEmpty(availability))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            try
            {
                Events updatedEvent = new Events(eventName, dateTime, description, location, organizer ); //creates an event object called updated events
                updatedEvent.Id = eventId;
                updatedEvent.Availability = Convert.ToBoolean(availability);
                new Controller.EventController().updateEvent(updatedEvent);
                
                EditEvent_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating event: " + ex.Message);
            }
        }
    }
}
