using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Student
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        private int roomId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["roomid"], out roomId))
                {
                    //ViewState["RoomId"] = roomId;
                    Session["RoomId"] = roomId;
                    //DisplayUserProfileImage();
                }
                else
                {
                    ShowErrorMessage("Invalid roomid");
                }
            }
            DisplayAnnouncements();
        }
        private void DisplayAnnouncements()
        {
            if (Session["RoomId"] != null && int.TryParse(Session["RoomId"].ToString(), out int roomId))
            {
                try
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();


                        string query = "SELECT announcementid, teacheremail, teachername, profileimage, postcontent, datepost FROM announcements " +
                 "WHERE roomid = @roomid " +
                 "ORDER BY teacherid, datepost DESC";

                        int teacherId = Convert.ToInt32(Session["LoggedInUserID"]);

                        using (MySqlCommand command = new MySqlCommand(query, con))
                        {
                            command.Parameters.AddWithValue("@roomid", roomId);
                            command.Parameters.AddWithValue("@teacherid", teacherId);

                            DataTable dt = new DataTable();
                            using (MySqlDataAdapter da = new MySqlDataAdapter(command))
                            {
                                da.Fill(dt);
                            }

                            postGridView.DataSource = dt;
                            postGridView.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("An error occurred while retrieving announcements.");
                }
            }
        }
        //private void DisplayUserProfileImage()
        //{
        //    int teacherId = Convert.ToInt32(Session["LoggedInUserID"]);
        //    string teacherEmail = Session["LoggedInUserEmail"].ToString();

        //    byte[] profileImage = GetUserProfileImage(teacherEmail);

        //    if (profileImage != null)
        //    {
        //        string base64String = Convert.ToBase64String(profileImage);
        //        string imageUrl = $"data:image/png;base64,{base64String}";

        //        Image1.ImageUrl = imageUrl;
        //    }
        //    else
        //    {
        //        Image1.ImageUrl = "path/to/default/image.png";
        //    }
        //}
        protected string GetProfileImageUrl(object profileImage)
        {
            if (profileImage != DBNull.Value)
            {
                byte[] bytes = (byte[])profileImage;
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                return "data:image/png;base64," + base64String;
            }
            else
            {
                return "path/to/default/image.jpg";
            }
        }

        private byte[] GetUserProfileImage(string userEmail)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT profileimage FROM users WHERE email = @userEmail";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userEmail", userEmail);
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!(reader["profileimage"] is DBNull))
                            {
                                return (byte[])reader["profileimage"];
                            }
                        }
                    }
                }
            }

            return null;
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