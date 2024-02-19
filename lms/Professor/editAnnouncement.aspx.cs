using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Professor
{
    public partial class WebForm15 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["roomid"]) && !string.IsNullOrEmpty(Request.QueryString["announcementid"]))
                {
                    if (int.TryParse(Request.QueryString["roomid"], out int roomId) && int.TryParse(Request.QueryString["announcementid"], out int announcementd))
                    {
                        announcement(roomId, announcementd);
                    }
                }
            }
        }

        private void announcement(int roomId, int announcementid)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT announcementid, roomid, teacherid, teacheremail, teachername, profileimage, postcontent, datepost FROM announcements " +
                         "WHERE roomid = @roomid AND announcementid = @announcementid";


                    using (MySqlCommand command = new MySqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@roomid", roomId);
                        command.Parameters.AddWithValue("@announcementid", announcementid);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtannouncement.Text = reader["postcontent"].ToString();

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred while retrieving learningmaterials.");
            }
        }
     

            protected void btnedit_Click(object sender, EventArgs e)
            {
                string postcontent = txtannouncement.Text;

                if (int.TryParse(Request.QueryString["roomid"], out int roomId) && int.TryParse(Request.QueryString["announcementid"], out int announcementid))
                {
                    try
                    {
                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                        using (MySqlConnection con = new MySqlConnection(connectionString))
                        {
                            con.Open();

                        //string updateQuery = "UPDATE announcements SET postcontent = @postcontent WHERE roomid = @roomid AND announcementid = @announcementid";
                        string updateQuery = "UPDATE announcements SET postcontent = @postcontent, datepost = datepost WHERE roomid = @roomid AND announcementid = @announcementid";

                        using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, con))
                            {
                                updateCommand.Parameters.AddWithValue("@postcontent", postcontent);
                                updateCommand.Parameters.AddWithValue("@roomid", roomId);
                                updateCommand.Parameters.AddWithValue("@announcementid", announcementid);

                                int rowsAffected = updateCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    ShowSuccessMessage("Announcement updated successfully.");
                                }
                                else
                                {
                                    ShowErrorMessage("Announcement not found or not updated.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("An error occurred while updating the announcement.");
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
    }
}