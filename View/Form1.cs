using EventManagmentSystem.Controller;
using EventManagmentSystem.Model;
using EventManagmentSystem.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManagmentSystem
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide(); //hdies login page
            new Signup().Show(); //launches sign up page
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string password = textBox2.Text;

            string dbPassword = new AttendeeController().getAttendeePassword(user); //tries to get the password of the attendee

            if (dbPassword == null)
            {
                Admin admin = new Admin(user,password); //check if the password is an admin password
                string role = admin.authenticateAdmin(admin);
                if (role == "admin")
                {
                    MessageBox.Show("Admin login successful.");
                    this.Hide(); 
                    new AdminDashboard().Show();  //launches admindash board
                    return;
                }
                else
                {
                  string orgPass = new OrganizerController().getOrganizerPassword(user); //if admin doesnt have the password, checks the organizer password
                    if (orgPass != null)
                    {
                        if (orgPass == password)
                        {
                            Session.Username = user;
                            Session.Password = password;
                            Session.UserType = "organizer";
                            Session.Id = new OrganizerController().getOrganizerId(user);

                            MessageBox.Show("User login successful.");
                            this.Hide();
                            new OrganizerDashboard().Show();
                            return;
                        }
                        
                    }
                }
                //if no password is found proceeds with the below
                MessageBox.Show("User not found.");
                textBox1.Clear();
                textBox2.Clear();
                return;
            }

            if (dbPassword == password)//verified as an attendee
            {
                Session.Username = user;
                Session.Password = password;
                Session.UserType = "attendee";
                Session.Id = new AttendeeController().getAttendeeId(user);
                MessageBox.Show("Login successful.");
                this.Hide();
                new AttendeeDashboard().Show(); //shows the attendee dashboard
            }
            else
            {
                MessageBox.Show("Incorrect password.");
            }
        }
    }
}
