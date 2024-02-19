using System;
using System.Collections.Generic;
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
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    BindStudentData();
                 }
                catch (Exception ex)
                {

                }

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                    try
                    {
                        con.Open();
                string query = "SELECT studentid, CONCAT(firstName, ' ', lastName) AS Fullname, email FROM student_info";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    using (MySqlCommand command = new MySqlCommand(query, con))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            studentGridView.DataSource = dataTable;
                            studentGridView.DataBind();
                        }
                    }
                }
            }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("An error occurred while processing your request. Please try again later.");

                    }
                }
            }
        }
        private void BindStudentData(string searchTerm = "")
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {

                    con.Open();

                string query = "SELECT studentid, CONCAT(firstName, ' ', lastName) AS Fullname, email FROM student_info ";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query += " WHERE studentid LIKE @searchTerm OR CONCAT(firstName, ' ', lastName) LIKE @searchTerm OR email LIKE @searchTerm";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
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

        //protected void btnsearch_Click(object sender, ImageClickEventArgs e)
        //{
        //      string searchTerm = txtsearch.Text;
        //    BindStudentData(searchTerm);
        //}

        //protected void btnrefresh_Click(object sender, EventArgs e)
        //{
        //    txtsearch.Text = "";
        //    BindStudentData();

        //}

        protected void btnSendToAll_Click(object sender, EventArgs e)
        {
            try
            {
                List<string>  studentEmails = GetStudentEmails();

                string emailList = string.Join(",", studentEmails);
                Response.Redirect($"WriteNotif.aspx?emails={emailList}");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred while processing your request. Please try again later.");
            }
        }

        private List<string> GetStudentEmails()
        {
            List<string> emails = new List<string>();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT email FROM student_info";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string email = reader["email"].ToString();
                                emails.Add(email);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("An error occurred while processing your request. Please try again later.");
                }
            }

            return emails;
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
        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtsearch.Text;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query;

                if (string.IsNullOrEmpty(searchTerm))
                {
                    // If the search term is empty, fetch all data.
                    query = "SELECT studentid, CONCAT(firstName, ' ', lastName) AS Fullname, email FROM student_info";
                }
                else
                {
                    // If there is a search term, filter the results.
                    query = "SELECT studentid, CONCAT(firstName, ' ', lastName) AS Fullname, email FROM student_info WHERE studentid LIKE @searchTerm OR CONCAT(firstName, ' ', lastName) LIKE @searchTerm OR email LIKE @searchTerm";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
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
    }
}