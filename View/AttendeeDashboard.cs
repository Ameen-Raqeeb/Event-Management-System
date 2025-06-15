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
    public partial class AttendeeDashboard: Form
    {
        public AttendeeDashboard()
        {
            InitializeComponent();
        }

        public void changePanel(object Form) //replaces the form inside panel2 with another form
        {
            if (this.panel2.Controls.Count > 0) //if the form is inside the panel, remove it
            {
                this.panel2.Controls.RemoveAt(0);
            }
            Form f = Form as Form;
            f.TopLevel = false; //makes a child class
            f.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(f); //add the form to panel 2
            this.panel2.Tag = f;
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            changePanel(new ViewPurchasedTicket()); //loads the view purchased form to panel 2
        }

        private void button1_Click(object sender, EventArgs e)
        {
            changePanel(new PurchaseTickets(this)); //loads the purchase tickets form to panel 2
        }

        public void paymentGateway(int ticket_id, int quantity)
        {
           changePanel(new PaymentGateWay(ticket_id, quantity)); //loads the payment gateway form to panel 2
        }
    }
}
