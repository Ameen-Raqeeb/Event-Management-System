using EventManagmentSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManagmentSystem.Controller
{
    // Controller class for managing ticket purchase operations
    class PurchaseController
    {
        // Database connection instance
        DbConnection dbConnection = new DbConnection();

        // Creates a new purchase record and associated payment
        // Handles the entire purchase process including:
        // 1. Creating the purchase record
        // 2. Creating the payment record
        // 3. Updating ticket quantities
        public void CreatePurchase(Purchase purchase, Payment payment)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();

                // Insert purchase record
                string query = "INSERT INTO purchase (ticket_id, attendee_id, quantity, total) VALUES (@ticketid, @attendeeID, @quantity, @total)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ticketid", purchase.Ticket_id);
                command.Parameters.AddWithValue("@attendeeID", purchase.Attendee_id);
                command.Parameters.AddWithValue("@quantity", purchase.Quantity);
                command.Parameters.AddWithValue("@total", purchase.Total);
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    // Get the ID of the newly created purchase
                    command.CommandText = "SELECT LAST_INSERT_ID()";
                    purchase.Id = Convert.ToInt32(command.ExecuteScalar());

                    // Insert payment record table in the DB
                    query = "INSERT INTO payment (purchase_id, type, number, name, expiry, ccv) VALUES (@purchaseId, @type, @number, @name, @expiry, @ccv)";
                    command.CommandText = query;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@purchaseId", purchase.Id);
                    command.Parameters.AddWithValue("@type", payment.CardType);
                    command.Parameters.AddWithValue("@number", payment.CardNumber);
                    command.Parameters.AddWithValue("@name", payment.NameOnCard);
                    command.Parameters.AddWithValue("@expiry", payment.ExpiryDate);
                    command.Parameters.AddWithValue("@ccv", payment.Ccv);

                    result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        // Update available ticket quantity
                        new TicketController().reduceTicketQuantity(purchase.Ticket_id, purchase.Quantity);
                        MessageBox.Show("Purchase Completed successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to create payment.");
                    }
                }
                else
                {
                    MessageBox.Show("Failed to create purchase.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
