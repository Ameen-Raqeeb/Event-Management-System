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
    class EventController
    {
        DbConnection dbConnection = new DbConnection();
        public void CreateEvent(Events events)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();
                string query = "INSERT INTO events (name, date, description, location, organizer_id) " +
                               "VALUES (@eventname, @eventdate, @eventdescription, @eventlocation, @organizerid)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eventname", events.Name);
                command.Parameters.AddWithValue("@eventdate", events.Date);
                command.Parameters.AddWithValue("@eventdescription", events.Description);
                command.Parameters.AddWithValue("@eventlocation", events.Locationn);
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
                command.Parameters.AddWithValue("@eventdescription", events.Description);
                command.Parameters.AddWithValue("@eventlocation", events.Locationn);
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

        public void deleteEvent(int eventId)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnection.connectionString);
                connection.Open();

                //Delete Ticket Associated with Event
                string deleteTicketQuery = "DELETE FROM ticket WHERE event_id = @eventid";
                MySqlCommand deleteTicketCommand = new MySqlCommand(deleteTicketQuery, connection);
                deleteTicketCommand.Parameters.AddWithValue("@eventid", eventId);
                deleteTicketCommand.ExecuteNonQuery();

                //Delete Event
                string query = "DELETE FROM events WHERE id = @eventid";
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
                while (reader.Read())
                {
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
    }
}
