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
    // Form for processing ticket purchase payments
    public partial class PaymentGateWay: Form
    {
        // Store ticket purchase details
        private int ticketId;    // ID of the ticket being purchased
        private int quantity;    // Number of tickets being purchased

        // Constructor initializes the payment gateway with ticket details
        public PaymentGateWay(int ticketId, int quantity)
        {
            InitializeComponent();
            this.ticketId = ticketId;
            this.quantity = quantity;
        }

        // Initialize the payment form when it loads
        private void PaymentGateWay_Load(object sender, EventArgs e)
        {
            // Set up date picker for card expiry
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.CustomFormat = "MM/yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            // Set Visa as default card type
            radioButton1.Checked = true;
        }

        // Handle the payment submission process
        private void button1_Click(object sender, EventArgs e)
        {
            // Get the selected card type
            string cardType = "";
            if (radioButton1.Checked)
            {
                cardType = "Visa";
            }
            else if (radioButton2.Checked)
            {
                cardType = "MasterCard";
            }

            // Validate the card number
            string cardNumber = textBox1.Text;
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length != 16)
            {
                MessageBox.Show("Please enter a valid card number.");
                return;
            }

            // Validate cardholder name
            string nameOnCard = textBox2.Text;
            if (string.IsNullOrEmpty(nameOnCard))
            {
                MessageBox.Show("Please enter the name on the card.");
                return;
            }

            // Validate expiry date
            DateTime expiryDate = dateTimePicker1.Value;
            if (expiryDate < DateTime.Now)
            {
                MessageBox.Show("Please enter a valid expiry date.");
                return;
            }

            // Validate CVV
            string cvv = textBox4.Text;
            if (string.IsNullOrEmpty(cvv) || cvv.Length != 3 || !cvv.All(char.IsDigit))
            {
                MessageBox.Show("Please enter a valid CVV.");
                return;
            }

            // Get ticket details and calculate total amount
            Ticket ticket = new Controller.TicketController().getTicketbyId(ticketId);
            double totalAmount = ticket.Price * quantity;

            // Create purchase record
            Purchase purchase = new Purchase(ticketId, quantity);
            purchase.Total = totalAmount;

            // Create payment record
            Payment payment = new Payment(0, cardType, cardNumber, nameOnCard, expiryDate, cvv);

            // Process the purchase and payment
            new PurchaseController().CreatePurchase(purchase, payment);

            // Close the form after successful processing
            this.Close();
        }

        // Handle MasterCard radio button selection
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
        }

        // Handle Visa radio button selection
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = false;
        }
    }
}
