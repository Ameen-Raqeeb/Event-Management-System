using EventManagmentSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManagmentSystem.Controller
{
    class TicketController
    {
        // Create a connection object to use the database
        DbConnection dbConnection = new DbConnection();

        // Method to create a new ticket in the database
        public void CreateTicket(Ticket ticket)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                // SQL insert query
                string query = "INSERT INTO ticket (event_id, tickettype, price, quantity, availability) VALUES (@eventid, @tickettype, @price, @quantity, @available)";
                MySqlCommand command = new MySqlCommand(query, connection);
                // Add values to parameters
                command.Parameters.AddWithValue("@eventid", ticket.Event.Id);
                command.Parameters.AddWithValue("@tickettype", ticket.TicketType);
                command.Parameters.AddWithValue("@price", ticket.Price);
                command.Parameters.AddWithValue("@quantity", ticket.Quantity);
                command.Parameters.AddWithValue("@available", ticket.Available);
                // Execute and check result
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Ticket created successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to create ticket.");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        // Get a ticket by event ID and ticket type
        public Ticket getTickeybyEventandType(int eventId, string ticketType)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "SELECT * FROM ticket WHERE event_id = @eventid AND tickettype = @tickettype";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eventid", eventId);
                command.Parameters.AddWithValue("@tickettype", ticketType);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Create Ticket object from fetched data
                    Ticket ticket = new Ticket(
                        new EventController().getEventById(Convert.ToInt32(reader["event_id"])),
                        reader["tickettype"].ToString(),
                        Convert.ToDouble(reader["price"]),
                        Convert.ToInt32(reader["quantity"])
                    )
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Available = Convert.ToBoolean(reader["availability"])
                    };
                    return ticket;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return null;
        }

        // Update ticket details such as price, quantity, availability
        public void UpdateTicket(Ticket ticket)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "UPDATE ticket SET price = @price, quantity = @quantity, availability = @available WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", ticket.Id);
                command.Parameters.AddWithValue("@price", ticket.Price);
                command.Parameters.AddWithValue("@quantity", ticket.Quantity);
                command.Parameters.AddWithValue("@available", ticket.Available);
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Ticket updated successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to update ticket.");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Get ticket details using the ticket ID
        public Ticket getTicketbyId(int ticketId)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "SELECT * FROM ticket WHERE id = @ticketid";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ticketid", ticketId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Ticket ticket = new Ticket(
                        new EventController().getEventById(Convert.ToInt32(reader["event_id"])),
                        reader["tickettype"].ToString(),
                        Convert.ToDouble(reader["price"]),
                        Convert.ToInt32(reader["quantity"])
                    )
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Available = Convert.ToBoolean(reader["availability"])
                    };
                    return ticket;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return null;
        }


        // Reduce the quantity of a specific ticket after a purchase
        public void reduceTicketQuantity(int ticketId, int quantity)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "UPDATE ticket SET quantity = quantity - @quantity WHERE id = @ticketid";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ticketid", ticketId);
                command.Parameters.AddWithValue("@quantity", quantity);
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Ticket quantity reduced successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to reduce ticket quantity.");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
