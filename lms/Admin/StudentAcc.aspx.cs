using System.Collections.Generic;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Runtime.Remoting.Messaging;
using System.Data;

namespace lms.Admin
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {

                
                BindStudentData();
                    PopulateStatusDropDown();

                }
                catch (Exception ex)
                {

                }

                LoadGridViewWithStatus(DropDownList1.SelectedValue);

            }
        }
        private void LoadGridViewWithStatus(string statusFilter)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT studentid, CONCAT(firstName, ' ', lastName) AS Fullname, email FROM student_info";

                    if (!string.IsNullOrEmpty(statusFilter))
                    {
                        query += " WHERE status = @statusFilter";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        if (!string.IsNullOrEmpty(statusFilter))
                        {
                            cmd.Parameters.AddWithValue("@statusFilter", statusFilter);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            studentGridView.DataSource = dataTable;
                            studentGridView.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("An error occurred while processing your request. Please try again later.");
                }
            }
        }
        private void ShowErrorMessage(string message)
        {
            string script = $"Swal.fire({{ icon: 'error', text: '{message}' }})";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script, true);
        }
        private void ShowSuccessMessage(string message)
        {
            string script = $"Swal.fire({{ icon: 'success', text: '{message}' }})";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script, true);
        }
        private void BindStudentData(string statusFilter = "")
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {

                
                con.Open();

                    string query = "SELECT studentid, CONCAT(firstName, ' ', lastName) AS Fullname, email FROM student_info";

                    if (!string.IsNullOrEmpty(statusFilter))
                    {
                        query += " WHERE status = @statusFilter";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        if (!string.IsNullOrEmpty(statusFilter))
                        {
                            cmd.Parameters.AddWithValue("@statusFilter", statusFilter);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        studentGridView.DataSource = dataTable;
                        studentGridView.DataBind();
                    }
                }
            }
                catch (Exception ex)
                {
                    ShowErrorMessage("An error occurred while processing your request. Please try again later.");

                }
            }
        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {

            string searchTerm = txtsearch.Text;
            string statusFilter = DropDownList1.SelectedValue;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT studentid, CONCAT(firstName, ' ', lastName) AS Fullname, email FROM student_info";

                if (!string.IsNullOrEmpty(statusFilter))
                {
                    query += " WHERE status = @statusFilter";
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    if (!string.IsNullOrEmpty(statusFilter))
                    {
                        query += " AND (studentid LIKE @searchTerm OR CONCAT(firstName, ' ', lastName) LIKE @searchTerm OR email LIKE @searchTerm)";
                    }
                    else
                    {
                        query += " WHERE studentid LIKE @searchTerm OR CONCAT(firstName, ' ', lastName) LIKE @searchTerm OR email LIKE @searchTerm";
                    }
                }

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    }
                    if (!string.IsNullOrEmpty(statusFilter))
                    {
                        cmd.Parameters.AddWithValue("@statusFilter", statusFilter);
                    }

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        studentGridView.DataSource = dataTable;
                        studentGridView.DataBind();
                    }
                }
            }
        }
        private void PopulateStatusDropDown()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT DISTINCT status FROM student_info";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Status = reader["status"].ToString();
                            DropDownList1.Items.Add(new ListItem(Status, Status));
                        }
                    }
                }
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindStudentData(DropDownList1.SelectedValue);

        }
    }
}