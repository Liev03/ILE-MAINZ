using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;

namespace lms.Professor
{
    public partial class ViewAnnouncement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["roomid"]) && !string.IsNullOrEmpty(Request.QueryString["announcementid"]))
                {
                    if (int.TryParse(Request.QueryString["roomid"], out int roomId) && int.TryParse(Request.QueryString["announcementid"], out int announcementId))
                    {
                        DisplayAnnouncement(roomId, announcementId);
                        DisplayUserProfileImage();
                        DisplayComment();

                    }
                }
            }
        }
        private void DisplayAnnouncement(int roomId, int announcementId)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT announcementid, teacheremail, teachername, profileimage, postcontent, datepost FROM announcements " +
                                   "WHERE roomid = @roomid AND announcementid = @announcementid";

                    using (MySqlCommand command = new MySqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@roomid", roomId);
                        command.Parameters.AddWithValue("@announcementid", announcementId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                lblpostcontent.Text = reader["postcontent"].ToString();
                                lblteachername.Text = reader["teachername"].ToString();
                                lblteacheremail.Text = reader["teacheremail"].ToString();
                                lbldate.Text = reader["datepost"].ToString();

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred while retrieving announcements.");

            }
        }
        private void DisplayUserProfileImage()
        {
            if (Session["LoggedInUserEmail"] != null)
            {
                try
                {
                    int teacherId = Convert.ToInt32(Session["LoggedInUserID"]);
                    string teacherEmail = Session["LoggedInUserEmail"].ToString();

                    byte[] profileImage = GetUserProfileImage(teacherEmail);

                    if (profileImage != null)
                    {
                        string base64String = Convert.ToBase64String(profileImage);
                        string imageUrl = $"data:image/png;base64,{base64String}";

                        profileimagedis.ImageUrl = imageUrl;
                    }
                    else
                    {
                        profileimagedis.ImageUrl = "path/to/default/image.png";
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("An error occurred while displaying the user profile image.");
                }
            }
            else
            {
                Response.Redirect("~Account/Login.aspx");
            }
        }

        private byte[] GetUserProfileImage(string userEmail)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT profileimage FROM lmsusers WHERE email = @userEmail";

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

        //protected void btncomment_Click(object sender, EventArgs e)
        //{
        //    if (Session["RoomId"] != null && int.TryParse(Session["RoomId"].ToString(), out int roomId))
        //    {
        //        int studentId = Convert.ToInt32(Session["LoggedInUserID"]);
        //        string teacheremail = Session["LoggedInUserEmail"].ToString();

        //        string commentpost = txtcomment.Text;
        //        DateTime currentDate = DateTime.Now;

        //        if (int.TryParse(Request.QueryString["roomid"], out int roomIdFromQueryString) && int.TryParse(Request.QueryString["announcementid"], out int announcementId))
        //        {
        //            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        //            using (MySqlConnection con = new MySqlConnection(connectionString))
        //            {
        //                con.Open();

        //                string retrieveStudentNameQuery = "SELECT firstname, lastname FROM teacher_info WHERE email = @teacheremail";

        //                using (MySqlCommand retrieveNameCommand = new MySqlCommand(retrieveStudentNameQuery, con))
        //                {
        //                    retrieveNameCommand.Parameters.AddWithValue("@teacheremail", teacheremail);

        //                    using (MySqlDataReader nameReader = retrieveNameCommand.ExecuteReader())
        //                    {
        //                        if (nameReader.Read())
        //                        {
        //                            string teacherFirstName = nameReader["firstname"].ToString();
        //                            string teacherLastName = nameReader["lastname"].ToString();
        //                            string teacherFullName = $"{teacherFirstName} {teacherLastName}";

        //                            nameReader.Close();

        //                            string insertQuery = "INSERT INTO comment (announcementid, roomid, teacheremail, name, profileimage, commentpost, datepost) " +
        //                                                 "VALUES (@announcementid, @roomid,  @teacheremail, @name, @profileimage, @commentpost, @datepost)";

        //                            using (MySqlCommand commandInsert = new MySqlCommand(insertQuery, con))
        //                            {
        //                                commandInsert.Parameters.AddWithValue("@announcementid", announcementId);
        //                                commandInsert.Parameters.AddWithValue("@roomid", roomIdFromQueryString);
        //                                commandInsert.Parameters.AddWithValue("@teacheremail", teacheremail);
        //                                commandInsert.Parameters.AddWithValue("@name", teacherFullName);
        //                                byte[] profileImage = GetUserProfileImage(teacheremail);
        //                                commandInsert.Parameters.AddWithValue("@profileimage", profileImage);

        //                                commandInsert.Parameters.AddWithValue("@commentpost", commentpost);
        //                                commandInsert.Parameters.AddWithValue("@datepost", currentDate);

        //                                commandInsert.ExecuteNonQuery();

        //                                txtcomment.Text = "";
        //                                ShowSuccessMessage("Your Comment has been successfully posted");
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        DisplayComment();
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

        private void DisplayComment()
        {
            if (Session["RoomId"] != null && int.TryParse(Session["RoomId"].ToString(), out int roomId))
            {
                if (int.TryParse(Request.QueryString["announcementid"], out int announcementId))
                {
                    try
                    {
                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                        using (MySqlConnection con = new MySqlConnection(connectionString))
                        {
                            con.Open();

                            //count
                            string countQuery = "SELECT COUNT(*) FROM comment WHERE roomid = @roomid AND announcementid = @announcementid";
                            using (MySqlCommand countCommand = new MySqlCommand(countQuery, con))
                            {
                                countCommand.Parameters.AddWithValue("@roomid", roomId);
                                countCommand.Parameters.AddWithValue("@announcementid", announcementId);

                                int commentCount = Convert.ToInt32(countCommand.ExecuteScalar());
                                classCommentCountLabel.Text = commentCount.ToString();
                            }
                            //retrieve
                            string query = "SELECT teacheremail, studentemail, name, profileimage, commentpost, datepost " +
                                           "FROM comment " +
                                           "WHERE roomid = @roomid AND announcementid = @announcementid " +
                                           "ORDER BY datepost DESC";

                            using (MySqlCommand command = new MySqlCommand(query, con))
                            {
                                command.Parameters.AddWithValue("@roomid", roomId);
                                command.Parameters.AddWithValue("@announcementid", announcementId);

                                DataTable dt = new DataTable();
                                using (MySqlDataAdapter da = new MySqlDataAdapter(command))
                                {
                                    da.Fill(dt);
                                }

                                commentGridView.DataSource = dt;
                                commentGridView.DataBind();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("An error occurred while retrieving comments.");
                    }
                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["RoomId"] != null && int.TryParse(Session["RoomId"].ToString(), out int roomId))
            {
                int studentId = Convert.ToInt32(Session["LoggedInUserID"]);
                string teacheremail = Session["LoggedInUserEmail"].ToString();

                string commentpost = txtcomment.Text;

                // Check if txtcomment is not empty
                if (!string.IsNullOrWhiteSpace(commentpost))
                {

                    DateTime currentDate = DateTime.Now;

                if (int.TryParse(Request.QueryString["roomid"], out int roomIdFromQueryString) && int.TryParse(Request.QueryString["announcementid"], out int announcementId))
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();

                        string retrieveStudentNameQuery = "SELECT firstname, lastname FROM teacher_info WHERE email = @teacheremail";

                        using (MySqlCommand retrieveNameCommand = new MySqlCommand(retrieveStudentNameQuery, con))
                        {
                            retrieveNameCommand.Parameters.AddWithValue("@teacheremail", teacheremail);

                            using (MySqlDataReader nameReader = retrieveNameCommand.ExecuteReader())
                            {
                                if (nameReader.Read())
                                {
                                    string teacherFirstName = nameReader["firstname"].ToString();
                                    string teacherLastName = nameReader["lastname"].ToString();
                                    string teacherFullName = $"{teacherFirstName} {teacherLastName}";

                                    nameReader.Close();

                                    string insertQuery = "INSERT INTO comment (announcementid, roomid, teacheremail, name, profileimage, commentpost, datepost) " +
                                                         "VALUES (@announcementid, @roomid,  @teacheremail, @name, @profileimage, @commentpost, @datepost)";

                                    using (MySqlCommand commandInsert = new MySqlCommand(insertQuery, con))
                                    {
                                        commandInsert.Parameters.AddWithValue("@announcementid", announcementId);
                                        commandInsert.Parameters.AddWithValue("@roomid", roomIdFromQueryString);
                                        commandInsert.Parameters.AddWithValue("@teacheremail", teacheremail);
                                        commandInsert.Parameters.AddWithValue("@name", teacherFullName);
                                        byte[] profileImage = GetUserProfileImage(teacheremail);
                                        commandInsert.Parameters.AddWithValue("@profileimage", profileImage);

                                        commandInsert.Parameters.AddWithValue("@commentpost", commentpost);
                                        commandInsert.Parameters.AddWithValue("@datepost", currentDate);

                                        commandInsert.ExecuteNonQuery();

                                        txtcomment.Text = "";
                                        ShowSuccessMessage("Your Comment has been successfully posted");
                                    }
                                }
                            }
                        }
                    }
                }
                DisplayComment();
            }
            else
            {
                // Handle the case where txtcomment is empty
                ShowErrorMessage("Please enter a comment before posting.");
            }
        }
    }
}
}