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
    public partial class AdminDashboard: Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }
        public void changePanel(object Form) //a method loads another form
        {
            //if a panel has been loaded in panel 2 then itt removes it
            if (this.panel2.Controls.Count > 0)
            {
                this.panel2.Controls.RemoveAt(0);
            }
            Form f = Form as Form;   

            //makes it a child control
            f.TopLevel = false;
            f.Dock = DockStyle.Fill; //adds the new form to panel 2
            this.panel2.Controls.Add(f);
            this.panel2.Tag = f;
            f.Show(); //shows the form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            changePanel(new AddOrganizer()); //loads the add organizer form in panel 2
        }

        private void button2_Click(object sender, EventArgs e)
        {
            changePanel(new RevenueReport()); //loads the revenue report form in panel 2
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
