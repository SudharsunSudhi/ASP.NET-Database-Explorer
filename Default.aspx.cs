using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Task_1
{
    public partial class _Default : System.Web.UI.Page
    {
        private string connectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            connectionString = ConfigurationManager.AppSettings["MyConnectionString"].ToString();
        }

        protected void btnTestConnection_Click(object sender, EventArgs e)
        {
            
            string connectionString = txtConnectionString.Text.Trim();

            
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                lblConnectionStatus.Text = "Please enter a valid connection string.";
                return;
            }

           
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
           
                connection.Open();

                
                lblConnectionStatus.Text = "Connection successful!";
                PopulateTablesDropdown();
                tablesContainer.Style["display"] = "block";
            }
            catch (Exception ex)
            {
                
                lblConnectionStatus.Text = "Connection failed: " + ex.Message;
            }
            finally
            {
               
                connection.Close();
            }
        }

        protected void PopulateTablesDropdown()
        {
            try
            { 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", connection);
                DataTable tables = new DataTable();
                adapter.Fill(tables);

                    ddlTables.DataSource = tables;
                    ddlTables.DataTextField = "TABLE_NAME"; // Set the text field to the TABLE_NAME column
                    ddlTables.DataValueField = "TABLE_NAME"; // Optionally, set the value field if needed
                    ddlTables.DataBind();
                }
            }
            catch (ArgumentException ex)
            {
                
                lblConnectionStatus.Text = "Connection string error: " + ex.Message;
            }
        }

        protected void ddlTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateColumnsList(ddlTables.SelectedValue);
            columnsContainer.Style["display"] = "block";
        }

        protected void PopulateColumnsList(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'", connection);
                DataTable columns = new DataTable();
                adapter.Fill(columns);

                bulletedColumns.DataSource = columns;
                bulletedColumns.DataTextField = "COLUMN_NAME";
                bulletedColumns.DataBind();
            }
        }

        protected void btnExecuteQuery_Click(object sender, EventArgs e)
        {
            // Get the query from the text box
            string query = txtQuery.Text;

            // Execute the query and bind the results to the GridView
            BindQueryResultsToGridView(query);
        }

        private void BindQueryResultsToGridView(string query)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable results = new DataTable();
                adapter.Fill(results);

                // Bind the DataTable to the GridView
                gridQueryResults.DataSource = results;
                gridQueryResults.DataBind();
            }
        }
    }
}