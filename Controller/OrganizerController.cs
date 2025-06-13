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

        public void addOrganizer(Organizers organizer) //a method to add a new organizer to the DB
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "INSERT INTO organizer (name, password, contactnumber, email) VALUES " +
                    "(@username, @password, @contact, @email)";
                MySqlCommand command = new MySqlCommand(query, connection); 

                //adds the data to the DB
                command.Parameters.AddWithValue("@username", organizer.Name); 
                command.Parameters.AddWithValue("@password", organizer.Password);
                command.Parameters.AddWithValue("@contact", organizer.ContactNumbers);
                command.Parameters.AddWithValue("@email", organizer.Email);
                int result = command.ExecuteNonQuery(); //runs the sql command

                if (result > 0) //checks how many rows were affected
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

        public string getOrganizerPassword(string name) //a method to get the organizer passwords
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "SELECT password FROM organizer WHERE name = @name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                string password = command.ExecuteScalar()?.ToString(); //runs the query and converts the password to a string
                connection.Close();
                return password;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        public int getOrganizerId(string name) //a method to get the organizer ids
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "SELECT id FROM organizer WHERE name = @name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                int id = Convert.ToInt32(command.ExecuteScalar()); //runs the query and converts the password to an int
                connection.Close();
                return id;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return -1; // Return -1 to indicate an error
            }
        }

        public Organizers getOrganizersfromId(int id) //a method to get the organizer details from the id
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "SELECT * FROM organizer WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader(); //saves the retrived data under reader

                if (reader.Read())
                {
                    //creates an organiser object using the data from reader
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

        public DataTable getEventDetails(int event_id) //method to get details about the tickets purchases for a specific event
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

                //used to fill the data into the datatable
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
