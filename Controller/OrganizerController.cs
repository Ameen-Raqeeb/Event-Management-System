using EventManagmentSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManagmentSystem.Controller
{
    class OrganizerController
    {
        DbConnection dbConnection = new DbConnection();

        public void addOrganizer(Organizers organizer)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "INSERT INTO organizer (name, password, contactnumber, email) VALUES " +
                    "(@username, @password, @contact, @email)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", organizer.Name);
                command.Parameters.AddWithValue("@password", organizer.Password);
                command.Parameters.AddWithValue("@contact", organizer.ContactNumbers);
                command.Parameters.AddWithValue("@email", organizer.Email);
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Organizer added successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to add organizer.");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public string getOrganizerPassword(string name)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "SELECT password FROM organizer WHERE name = @name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                string password = command.ExecuteScalar()?.ToString();
                connection.Close();
                return password;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        public int getOrganizerId(string name)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "SELECT id FROM organizer WHERE name = @name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                int id = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return id;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return -1; // Return -1 to indicate an error
            }
        }

        public Organizers getOrganizersfromId(int id)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "SELECT * FROM organizer WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Organizers organizer = new Organizers(
                        reader["name"].ToString(),
                        reader["password"].ToString(),
                        reader["contactnumber"].ToString(),
                        reader["email"].ToString()
                    );
                    organizer.Id = Convert.ToInt32(reader["id"]);
                    connection.Close();
                    return organizer;
                }
                else
                {
                    connection.Close();
                    return null; // No organizer found with the given ID
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        public DataTable getEventDetails(int event_id)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();

                string query = @"SELECT 
                                    a.name AS 'Attendee Name',
                                    a.contactnumber AS 'Attendee Contact',
                                    p.quantity AS 'Tickets Bought',
                                    p.total AS Total,
                                    t.tickettype AS 'Ticket Type'
                                FROM 
                                    purchase p
                                JOIN 
                                    attendee a ON p.attendee_id = a.id
                                JOIN 
                                    ticket t ON p.ticket_id = t.id
                                WHERE 
                                    t.event_id = @event_id;
                                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@event_id", event_id);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                connection.Close();
                return dataTable; // Return the DataTable containing event details
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null; // Return null in case of an error
            }
        }
    }

  
}
