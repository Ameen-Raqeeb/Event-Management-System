using EventManagmentSystem.Model;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace EventManagmentSystem.Controller
{
    class EventController
    {
        DbConnection dbConnection = new DbConnection();  //created a new database connection
        public void CreateEvent(Events events) // A method Insert new events to the DB
        {
            try
            {
                MySqlConnection conn= new MySqlConnection(dbConnection.connectionString);
                conn.Open(); //opens DB connection
                string query = "INSERT INTO events (name, date, description, location, organizer_id) " +
                               "VALUES (@eventname, @eventdate, @eventdescription, @eventlocation, @organizerid)"; //SQL query to insert the data in the DB
                MySqlCommand command = new MySqlCommand(query, conn);

                //fills the SQL query using the data from the events object
                command.Parameters.AddWithValue("@eventname", events.Name);
                command.Parameters.AddWithValue("@eventdate", events.Date);
                command.Parameters.AddWithValue("@eventdescription", events.Description);
                command.Parameters.AddWithValue("@eventlocation", events.Location);
                command.Parameters.AddWithValue("@organizerid", events.Organizer.Id);

                int result = command.ExecuteNonQuery(); //runs the sql query and checks how many rows were affecfed

                if (result > 0) //checks if the insertion was succesdful 
                {
                    MessageBox.Show("Event created successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to create event.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }




        //A method to retrive all the events created by one specific organizer
        public List<Events> getEventsbyOrganizer(int organizerId) //gets a list of all events of one specific user
        {
            List<Events> eventsList = new List<Events>();
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "SELECT * FROM events WHERE organizer_id = @organizerid";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@organizerid", organizerId);
                MySqlDataReader reader = command.ExecuteReader(); //runs the query to get the results to read


                while (reader.Read()) //read the data recieved
                {
                    //creates a new event object using the data gotten from the DB
                    Events eventItem = new Events(
                        reader["name"].ToString(),
                        Convert.ToDateTime(reader["date"]),
                        reader["location"].ToString(),
                        reader["description"].ToString(),
                        new OrganizerController().getOrganizersfromId(Convert.ToInt32(reader["organizer_id"]))
                    )
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Availability = Convert.ToBoolean(reader["availability"])
                    };
                    eventsList.Add(eventItem);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return eventsList;
        }




        public Events getEventById(int eventId) //a method to get one event by it's id
        {
            Events eventItem = null; //created am empty variable to store in case an event is found
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "SELECT * FROM events WHERE id = @eventid";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eventid", eventId);
                MySqlDataReader reader = command.ExecuteReader(); //runs the query

                if (reader.Read()) //if an event is found
                {
                    eventItem = new Events( //create a new event object
                        reader["name"].ToString(),
                        Convert.ToDateTime(reader["date"]),
                        reader["location"].ToString(),
                        reader["description"].ToString(),
                        new OrganizerController().getOrganizersfromId(Convert.ToInt32(reader["organizer_id"])) //gets the organizer details
                    )
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Availability = Convert.ToBoolean(reader["availability"])
                    };
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return eventItem;
        }


        public void updateEvent(Events events) //a method to update the event details
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "UPDATE events SET name = @eventname, date = @eventdate, description = @eventdescription, " +
                               "location = @eventlocation, organizer_id = @organizerid, availability = @availability WHERE id = @eventid";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eventname", events.Name);
                command.Parameters.AddWithValue("@eventdate", events.Date);
                command.Parameters.AddWithValue("@eventdescription", events.Location);
                command.Parameters.AddWithValue("@eventlocation", events.Description);
                command.Parameters.AddWithValue("@organizerid", events.Organizer.Id);
                command.Parameters.AddWithValue("@availability", events.Availability);
                command.Parameters.AddWithValue("@eventid", events.Id);
                int result = command.ExecuteNonQuery(); //runs the query and checks how many rows were affected

                if (result > 0) //if atleast one row was affected
                {
                    MessageBox.Show("Event updated successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to update event.");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        public void deleteEvent(int eventId) //a method to delete an event
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();



                string query = "DELETE FROM events WHERE id = @eventid"; //deletes the event from the DB
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eventid", eventId);
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Event deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to delete event.");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        

        public List<Events> getAllEvents() //provides a list of all the events created
        {
            List<Events> eventsList = new List<Events>();
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
               string query = "SELECT * FROM events";
 

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) //loop through each row
                {
                    Events eventItem = new Events( //creates an event object with the data
                        reader["name"].ToString(),
                        Convert.ToDateTime(reader["date"]),
                        reader["location"].ToString(),
                        reader["description"].ToString(),
                        new OrganizerController().getOrganizersfromId(Convert.ToInt32(reader["organizer_id"])) //gets the organizer details
                    )
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Availability = Convert.ToBoolean(reader["availability"])
                    };
                    eventsList.Add(eventItem);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return eventsList;
        }
    }
}
