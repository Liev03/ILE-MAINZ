using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace lms.sis.SISPAGES
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //users
            ConnectionClass conn = new ConnectionClass();
            using (MySqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    // Write your SQL query to count students
                    string studentQuery = "SELECT COUNT(*) FROM manageuser WHERE Role = 'Student'";
                    MySqlCommand studentCommand = new MySqlCommand(studentQuery, connection);
                    int studentCount = Convert.ToInt32(studentCommand.ExecuteScalar());

                    // Write your SQL query to count strands
                    string strandQuery = "SELECT COUNT(*) FROM strandmanagement";
                    MySqlCommand strandCommand = new MySqlCommand(strandQuery, connection);
                    int strandCount = Convert.ToInt32(strandCommand.ExecuteScalar());

                    // Write your SQL query to count sections
                    string sectionQuery = "SELECT COUNT(*) FROM sectionmanagement";
                    MySqlCommand sectionCommand = new MySqlCommand(sectionQuery, connection);
                    int sectionCount = Convert.ToInt32(sectionCommand.ExecuteScalar());

                    // Write your SQL query to count users
                    string userQuery = "SELECT COUNT(*) FROM manageuser";
                    MySqlCommand userCommand = new MySqlCommand(userQuery, connection);
                    int userCount = Convert.ToInt32(userCommand.ExecuteScalar());

                    // Set the Labels' text properties to display the counts
                    lbl1.Text = studentCount.ToString();
                    lbl2.Text = "" + strandCount.ToString();
                    lbl3.Text = "" + sectionCount.ToString();
                    lbl4.Text = "" + userCount.ToString();

                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during the process
                    lbl1.Text = "Error: " + ex.Message;
                    lbl2.Text = "Error: " + ex.Message;
                    lbl3.Text = "Error: " + ex.Message;
                    lbl4.Text = "Error: " + ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }

        }
        protected void post_Click(object sender, EventArgs e)
        {
            ConnectionClass conn = new ConnectionClass();
            if (txtpost.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','Please fill out all the fields ','error')", true);
            }
            else
            {
                using (MySqlConnection connection = conn.GetConnection())
                {
                    try
                    {
                        connection.Open();
                        string insertQuery = "INSERT INTO announcement (`Post`) VALUES (@Post)";
                        using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Post", txtpost.Text);
                            command.ExecuteNonQuery();
                        }
                        txtpost.Text = "";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!', 'addedd successfully!', 'success')", true);
                    }
                    catch (Exception ex)
                    {
                        txtpost.Text = ex.Message;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','Please try again','error')", true);
                    }
                }
            }
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Account/Logout.aspx");
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin.aspx");
        }
    }
}