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
    // This form is used by Admin to add new organizers to the system
    public partial class AddOrganizer: Form
    {
        public AddOrganizer()
        {
            InitializeComponent();
        }

        // Event handler for the Add Organizer button
        private void button1_Click(object sender, EventArgs e)
        {
            // Get input from text boxes
            string username = textBox1.Text;      // Organizer name
            string password = textBox2.Text;      // Organizer password
            string contact = textBox3.Text;       // Contact number
            string email = textBox4.Text;         // Email address

            // Create new Organizer object
            Organizers organizer = new Organizers(username, password, contact, email);

            // Validate all fields are filled
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            try
            {
                // Add organizer to database using OrganizerController
                new Controller.OrganizerController().addOrganizer(organizer);
                
                // Clear form after successful addition
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void AddOrganizer_Load(object sender, EventArgs e)
        {

        }
    }
}
