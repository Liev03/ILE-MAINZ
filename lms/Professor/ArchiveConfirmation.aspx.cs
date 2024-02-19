using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Professor
{
    public partial class ArchiveConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["roomid"]))
                {
                    if (int.TryParse(Request.QueryString["roomid"], out int roomId))
                    {

                    }
                }
            } 
        }


        protected void btnarchiveyes_Click(object sender, EventArgs e)
        {

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    if (int.TryParse(Request.QueryString["roomid"], out int roomId))
                    {
                        string selectQuery = "SELECT status FROM rooms WHERE roomid = @roomid";

                        using (MySqlCommand commandSelect = new MySqlCommand(selectQuery, con))
                        {
                            commandSelect.Parameters.AddWithValue("@roomid", roomId);

                            object currentStatus = commandSelect.ExecuteScalar();

                            if (currentStatus != null && currentStatus.ToString() != "Archived")
                            {
                                string updateQuery = "UPDATE rooms SET status = 'Archived' WHERE roomid = @roomid";

                                using (MySqlCommand commandUpdate = new MySqlCommand(updateQuery, con))
                                {
                                    commandUpdate.Parameters.AddWithValue("@roomid", roomId);

                                    int rowsAffected = commandUpdate.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        ShowSuccessMessage("Room Archived successfully.");
                                        ClientScript.RegisterStartupScript(this.GetType(), "successMessage", "showSuccessMessage();", true);
                                    }
                                    else
                                    {
                                        ShowErrorMessage("Failed to Archive Rooms.");
                                    }
                                }
                            }
                            else
                            {
                                ShowErrorMessage("Room is already archived.");
                            }
                        }
                    }
                    else
                    {
                        ShowErrorMessage("Invalid or missing room ID in the query string.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred while updating room. Error: " + ex.Message);
            }
        }
        protected void btnarchiveno_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateRoom.aspx");
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
    }
}