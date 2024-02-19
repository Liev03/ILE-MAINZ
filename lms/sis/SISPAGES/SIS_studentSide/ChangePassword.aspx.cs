using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.sis.SISPAGES
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnchangepass_Click(object sender, EventArgs e)
        {
            string userEmail = Session["LoggedInUser"] as string;
            string currentPassword = cpass.Text;
            string newPassword = npass.Text;
            string confirmPassword = confirmpass.Text;

            if (ValidateCurrentPassword(userEmail, currentPassword) && newPassword == confirmPassword)
            {
                Changepassword(userEmail, newPassword);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!','Password updated successfully!', 'success')", true);
                cpass.Text = "";
                npass.Text = "";
                confirmpass.Text = "";
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "swal('Something went wrong...','Failed to update password','error')", true);

            }
        }
        private void Changepassword(string userEmail, string newPassword)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE manageuser SET Password = @newPassword WHERE Email = @userEmail";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);
                    cmd.Parameters.AddWithValue("@userEmail", userEmail);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private bool ValidateCurrentPassword(string userEmail, string currentPassword)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT Password FROM manageuser WHERE Email = @userEmail";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userEmail", userEmail);
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader["Password"].ToString();

                            if (currentPassword == storedPassword)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Account/Logout.aspx");
        }

    }
}
