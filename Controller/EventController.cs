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
    /// <summary>
    /// Controller class that manages all event-related operations in the system.
    /// Handles CRUD (Create, Read, Update, Delete) operations for events and their related data.
    /// </summary>
    class EventController
    {
        /// <summary>
        /// Database connection instance for executing database operations
        /// </summary>
        DbConnection dbConnection = new DbConnection();

        /// <summary>
        /// Creates a new event in the database
        /// </summary>
        /// <param name="events">Event object containing all the event details to be created</param>
        public void CreateEvent(Events events)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(dbConnection.connectionString);
                conn.Open();
                // SQL query to insert new event data
                string query = "INSERT INTO events (name, date, description, location, organizer_id) " +
                               "VALUES (@eventname, @eventdate, @eventdescription, @eventlocation, @organizerid)";
                MySqlCommand command = new MySqlCommand(query, conn);

                // Set parameter values from the event object
                command.Parameters.AddWithValue("@eventname", events.Name);
                command.Parameters.AddWithValue("@eventdate", events.Date);
                command.Parameters.AddWithValue("@eventdescription", events.Description);
                command.Parameters.AddWithValue("@eventlocation", events.Location);
                command.Parameters.AddWithValue("@organizerid", events.Organizer.Id);

                int result = command.ExecuteNonQuery();

                if (result > 0)
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

        /// <summary>
        /// Retrieves all events created by a specific organizer
        /// </summary>
        /// <param name="organizerId">ID of the organizer whose events to retrieve</param>
        /// <returns>List of events created by the specified organizer</returns>
        public List<Events> getEventsbyOrganizer(int organizerId)
        {
            List<Events> eventsList = new List<Events>();
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "SELECT * FROM events WHERE organizer_id = @organizerid";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@organizerid", organizerId);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Create event object from database record
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

        /// <summary>
        /// Retrieves a specific event by its ID
        /// </summary>
        /// <param name="eventId">ID of the event to retrieve</param>
        /// <returns>Event object if found, null otherwise</returns>
        public Events getEventById(int eventId)
        {
            Events eventItem = null;
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "SELECT * FROM events WHERE id = @eventid";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eventid", eventId);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    eventItem = new Events(
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
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return eventItem;
        }

        /// <summary>
        /// Updates an existing event's information in the database
        /// </summary>
        /// <param name="events">Event object containing updated information</param>
        public void updateEvent(Events events)
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
                int result = command.ExecuteNonQuery();

                if (result > 0)
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

        /// <summary>
        /// Deletes an event and all its related data (tickets and purchases)
        /// </summary>
        /// <param name="eventId">ID of the event to delete</param>
        public void deleteEvent(int eventId)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();

                // Step 1: Delete purchases related to tickets for the event
                string deletePurchaseQuery = @"
                    DELETE FROM purchase 
                    WHERE ticket_id IN (
                        SELECT id FROM ticket WHERE event_id = @event_id
                    )";
                MySqlCommand deletePurchaseCommand = new MySqlCommand(deletePurchaseQuery, connection);
                deletePurchaseCommand.Parameters.AddWithValue("@event_id", eventId);
                deletePurchaseCommand.ExecuteNonQuery();

                // Step 2: Delete tickets related to the event
                string deleteTicketQuery = "DELETE FROM ticket WHERE event_id = @event_id";
                MySqlCommand deleteTicketCommand = new MySqlCommand(deleteTicketQuery, connection);
                deleteTicketCommand.Parameters.AddWithValue("@event_id", eventId);
                deleteTicketCommand.ExecuteNonQuery();

                // Step 3: Delete the event itself
                string deleteEventQuery = "DELETE FROM events WHERE id = @event_id";
                MySqlCommand deleteEventCommand = new MySqlCommand(deleteEventQuery, connection);
                deleteEventCommand.Parameters.AddWithValue("@event_id", eventId);
                int result = deleteEventCommand.ExecuteNonQuery();

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

        /// <summary>
        /// Retrieves all events in the system
        /// </summary>
        /// <returns>List of all events</returns>
        public List<Events> getAllEvents()
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
