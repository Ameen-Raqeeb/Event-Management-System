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
    public partial class Signup: Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //when user clicks the link, hides the current form and shows the login formm
        {
            this.Hide();
            new Form1().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string contact = textBox3.Text;
            string gender = comboBox1.Text;

            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(contact) ||
                string.IsNullOrWhiteSpace(gender))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            Attendee attendee = new Attendee(username, password, contact, gender); //creates a new attendee obeject

            new AttendeeController().addAttendee(attendee); //passes the object to the controller to add to the database

            this.Hide(); //once added closes the signup page
            new Form1().Show(); //shows the login page

        }
    }
}
