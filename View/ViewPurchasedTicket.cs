using EventManagmentSystem.Model;   // Importing the Model namespace of the Event Management System
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
    // This form is responsible for displaying the tickets purchased by the currently logged-in attendee
    public partial class ViewPurchasedTicket: Form
    {
        // Constructor to initialize the form components
        public ViewPurchasedTicket()
        {
            InitializeComponent();  // Initialize form controls and layout
        }

        // Event handler that executes when the form is loaded
        private void ViewPurchasedTicket_Load(object sender, EventArgs e)
        {
            // Retrieve all tickets purchased by the currently logged-in attendee using their session ID
            DataTable purchasedTickets = new Controller.AttendeeController().getAlltheTicketsByAttendee(Session.Id);

            // Check if the attendee has any purchased tickets
            if (purchasedTickets.Rows.Count > 0)
            {
                // Bind the retrieved ticket data to the DataGridView for display
                dataGridView1.DataSource = purchasedTickets;
            }
            else
            {
                // Show a message box if no tickets have been purchased
                MessageBox.Show("No tickets purchased yet.");
            }
        }
    }
}
