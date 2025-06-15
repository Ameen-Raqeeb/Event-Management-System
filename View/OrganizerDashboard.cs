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
    // Main dashboard for organizers to manage their events and tickets
    public partial class OrganizerDashboard: Form
    {
        public OrganizerDashboard()
        {
            InitializeComponent();
        }

        // Method to switch between different forms in the main panel
        public void changePanel(object Form)
        {
            // Clear existing form from panel
            if (this.panel2.Controls.Count > 0)
            {
                this.panel2.Controls.RemoveAt(0);
            }
            // Add new form to panel
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(f);
            this.panel2.Tag = f;
            f.Show();
        }

        // Button click handlers for different organizer functions
        private void button1_Click(object sender, EventArgs e)
        {
            changePanel(new CreateEvent());  // Open Create Event form
        }

        private void button2_Click(object sender, EventArgs e)
        {
            changePanel(new EditEvent());    // Open Edit Event form
        }

        private void button3_Click(object sender, EventArgs e)
        {
            changePanel(new DeleteEvent());  // Open Delete Event form
        }

        private void button4_Click(object sender, EventArgs e)
        {
            changePanel(new AddTicket());    // Open Add Ticket form
        }

        private void button5_Click(object sender, EventArgs e)
        {
            changePanel(new EditTicket());   // Open Edit Ticket form
        }

        private void button6_Click(object sender, EventArgs e)
        {
            changePanel(new ViewEventDetails()); // Open View Event Details form
        }
    }
}
