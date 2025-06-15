using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManagmentSystem
{
    /// <summary>
    /// Main program class that serves as the entry point for the Event Management System application.
    /// This class initializes the Windows Forms application and launches the main form.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// This method:
        /// 1. Enables visual styles for the application
        /// 2. Sets compatible text rendering
        /// 3. Launches the main form (Form1)
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Enable visual styles for modern Windows appearance
            Application.EnableVisualStyles();
            // Set compatible text rendering for better text display
            Application.SetCompatibleTextRenderingDefault(false);
            // Launch the main application form
            Application.Run(new Form1());
        }
    }
}
