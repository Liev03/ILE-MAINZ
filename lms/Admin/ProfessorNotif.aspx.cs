using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Admin
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindTeacherData();
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
                        string query = "SELECT teacherid, CONCAT(firstName, ' ', lastName) AS Fullname, email FROM teacher_info";
                        using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    using (MySqlCommand command = new MySqlCommand(query, con))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                                    TeacherGridView.DataSource = dataTable;
                                    TeacherGridView.DataBind();
                        }
                    }
                }
            }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("An error occurred while processing your request. Please try again later.");

                        //lblMessage.Text = "An error occurred while processing your request. Please try again later.";

                    }
                }
            }
        }

        private void BindTeacherData(string searchTerm = "")
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT teacherid, CONCAT(firstName, ' ', lastName) AS Fullname, email FROM teacher_info ";

                    if (!string.IsNullOrEmpty(searchTerm))
                {
                        query += " WHERE teacherid LIKE @searchTerm OR CONCAT(firstName, ' ', lastName) LIKE @searchTerm OR email LIKE @searchTerm";
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

                            TeacherGridView.DataSource = dataTable;
                            TeacherGridView.DataBind();
                    }
                }
            }
                catch (Exception ex)
                {
                    ShowErrorMessage("An error occurred while processing your request. Please try again later.");

                    //lblMessage.Text = "An error occurred while processing your request. Please try again later.";

                }
            }
        }
        //protected void btnsearch_Click(object sender, ImageClickEventArgs e)
        //{
        //    string searchTerm = txtsearch.Text;
        //    BindProfessorData(searchTerm);

        //}

        //protected void btnrefresh_Click(object sender, EventArgs e)
        //{
        //    BindProfessorData();
        //    txtsearch.Text = "";
        //}

        protected void btnSendToAll_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> teacherEmails = GetTeacherEmails();

                string emailList = string.Join(",", teacherEmails);
                Response.Redirect($"WriteNotifProf.aspx?emails={emailList}");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred while processing your request. Please try again later.");

                //lblMessage.Text = "An error occurred while processing your request. Please try again later.";
            }
        }

        private List<string> GetTeacherEmails()
        {
            List<string> emails = new List<string>();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT email FROM teacher_info";

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

                    //lblMessage.Text = "An error occurred while processing your request. Please try again later.";
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
                    query = "SELECT teacherid, CONCAT(firstName, ' ', lastName) AS Fullname, email FROM teacher_info";
                }
                else
                {
                    // If there is a search term, filter the results.
                    query = "SELECT teacherid, CONCAT(firstName, ' ', lastName) AS Fullname, email FROM teacher_info WHERE teacherid LIKE @searchTerm OR CONCAT(firstName, ' ', lastName) LIKE @searchTerm OR email LIKE @searchTerm";
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

                        TeacherGridView.DataSource = dataTable;
                        TeacherGridView.DataBind();
                    }
                }
            }
        }
    }
}