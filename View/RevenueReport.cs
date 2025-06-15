using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManagmentSystem.View
{
    // Displays revenue reports for events
    public partial class RevenueReport: Form
    {
        public RevenueReport()
        {
            InitializeComponent();
        }

        private void RevenueReport_Load(object sender, EventArgs e)
        {
            // Generate and display revenue report when form loads
            DataTable dataTable = new Controller.ReportController().generateRevenuereport();
            // Display report in data grid
            dataGridView1.DataSource = dataTable;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
