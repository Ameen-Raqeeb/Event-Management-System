using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagmentSystem
{
    /// <summary>
    /// Database connection class that manages the connection string for MySQL database.
    /// This class provides a centralized location for database connection configuration.
    /// </summary>
    class DbConnection
    {
        /// <summary>
        /// Connection string for MySQL database.
        /// Contains server details, credentials, and database name.
        /// Format: server=hostname;uid=username;pwd=password;port=portnumber;database=dbname;
        /// </summary>
        public string connectionString = "server=localhost;uid=root;pwd=;port=3306;database=sdam_assignment;";
    }
}
