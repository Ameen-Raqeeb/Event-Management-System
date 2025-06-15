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
    class AttendeeController
    {
        DbConnection dbConnection = new DbConnection();
        public void addAttendee(Attendee attendee) //a method to add a new attendee to the DB
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();

                string query = "INSERT INTO attendee (name, password, contactnumber, gender) value " + 
                    "(@name, @pasword, @contactnumber, @gender)";


                //inserts the the data into the database
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", attendee.Name);
                command.Parameters.AddWithValue("@pasword", attendee.Password);
                command.Parameters.AddWithValue("@contactnumber", attendee.ContactNumbers);
                command.Parameters.AddWithValue("@gender", attendee.Gender);

                int result = command.ExecuteNonQuery(); //runs the sql query and gets how many rows were effected 
                if (result > 0)
                {
                    MessageBox.Show("Attendee added successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to add attendee.");
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        public string getAttendeePassword(string name) //a method to get the password of the attendee
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "SELECT password FROM attendee WHERE name = @name"; //gets the password from the database where input name is == database name
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                string password = command.ExecuteScalar()?.ToString(); //converts the password retrived from the database to a stringg
                connection.Close();
                return password;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        public int getAttendeeId(string name) //a method to get the the attendee id
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "SELECT id FROM attendee WHERE name = @name"; //gets the id from the database where input name is == database name
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                int id = Convert.ToInt32(command.ExecuteScalar()); //converts the id retrived from the database to a stringg
                connection.Close();
                return id;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return -1; // Return -1 to indicate an error
            }
        }

        public DataTable getAlltheTicketsByAttendee(int attendeeId) //a method to get all the tickets purchased by a attendee
        {

            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = @"SELECT 
                                    e.name AS EventName,
                                    e.date AS EventDate,
                                    p.quantity AS QuantityBought,
                                    t.tickettype AS TicketType,
                                    p.total AS Total
                                FROM 
                                    purchase p
                                JOIN 
                                    ticket t ON p.ticket_id = t.id
                                JOIN 
                                    events e ON t.event_id = e.id
                                WHERE 
                                    p.attendee_id = @id;
                            ";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", attendeeId);
                MySqlDataReader reader = command.ExecuteReader(); //stores the result in the reader

                DataTable tickettable = new DataTable(); 

                // Define columns for the ticket table
                tickettable.Columns.Add("EventName", typeof(string));
                tickettable.Columns.Add("EventDate", typeof(DateTime));
                tickettable.Columns.Add("QuantityBought", typeof(int));
                tickettable.Columns.Add("TicketType", typeof(string));
                tickettable.Columns.Add("Total", typeof(decimal));

                //loops through each record in the reader and creates a new row
                while (reader.Read())
                {
                    DataRow row = tickettable.NewRow();
                    row["EventName"] = reader["EventName"];
                    row["EventDate"] = reader["EventDate"];
                    row["QuantityBought"] = reader["QuantityBought"];
                    row["TicketType"] = reader["TicketType"];
                    row["Total"] = reader["Total"];
                    tickettable.Rows.Add(row);
                }
                if (tickettable.Rows.Count == 0)
                {
                    MessageBox.Show("No tickets found for this attendee.");
                }
                 connection.Close();
                return tickettable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null; // Return null to indicate an error
            }
            
        }
    }
}
