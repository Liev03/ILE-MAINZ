using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace lms.Student
{
    public partial class WebForm7 : System.Web.UI.Page
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

                        string studentid = Session["LoggedInUserID"] as string;

                        if (!string.IsNullOrEmpty(studentid))
                        {
                            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
                            {
                                connection.Open();
                                GetUserProfileImage(studentid);

                                byte[] profileImageBytes = GetUserProfileImage(studentid);
                                if (profileImageBytes != null)
                                {
                                    string base64Image = Convert.ToBase64String(profileImageBytes);
                                    string imageSrc = "data:image/jpeg;base64," + base64Image;
                                    Image1.ImageUrl = imageSrc;
                                }
                            }
                        }
                        //if (Session["LoggedInUserID"] != null)
                        //{
                        //    lblUserId.Text = Session["LoggedInUserID"].ToString();
                        //}
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

                        Image1.ImageUrl = imageUrl;
                    }
                    else
                    {
                        Image1.ImageUrl = "path/to/default/image.png";
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
        //private byte[] GetUserProfileImage(string userID)
        //{
        //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        string query = "SELECT ImageData FROM tblimages WHERE studentId = @studentID";

        //        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        //        {
        //            cmd.Parameters.AddWithValue("@studentID", userID);
        //            connection.Open();

        //            using (MySqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    if (!(reader["ImageData"] is DBNull))
        //                    {
        //                        return (byte[])reader["ImageData"];
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return null;
        //}

        private byte[] GetUserProfileImage(string studentid)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT ImageData FROM tblimages WHERE studentId = @studentID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@studentID", studentid);
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!(reader["ImageData"] is DBNull))
                            {
                                return (byte[])reader["ImageData"];
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
            try
            {
                if (Session["RoomId"] != null && int.TryParse(Session["RoomId"].ToString(), out int roomId))
                {
                    int studentId = Convert.ToInt32(Session["LoggedInUserID"]);
                    string studentemail = Session["LoggedInUserEmail"].ToString();
                    string UserID = Session["LoggedInUserID"] as string;
                    string studentid = Session["LoggedInUserID"].ToString();

                    string commentpost = txtcomment.Text;

                    // Check if txtcomment is not empty
                    if (!string.IsNullOrWhiteSpace(commentpost))
                    {
                        DateTime currentDate = DateTime.Now;
                        string teacheremail = lblteacheremail.Text;

                        if (int.TryParse(Request.QueryString["roomid"], out int roomIdFromQueryString) && int.TryParse(Request.QueryString["announcementid"], out int announcementId))
                        {
                            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                            using (MySqlConnection con = new MySqlConnection(connectionString))
                            {
                                con.Open();

                                string retrieveStudentNameQuery = "SELECT name FROM student_Info WHERE email = @studentemail";

                                using (MySqlCommand retrieveNameCommand = new MySqlCommand(retrieveStudentNameQuery, con))
                                {
                                    retrieveNameCommand.Parameters.AddWithValue("@studentemail", studentemail);

                                    using (MySqlDataReader nameReader = retrieveNameCommand.ExecuteReader())
                                    {
                                        if (nameReader.Read())
                                        {
                                            string fullname = nameReader["name"].ToString();
                                            nameReader.Close();

                                            byte[] imageData = GetUserProfileImage(studentid);

                                            string insertQuery = "INSERT INTO comment (announcementid, roomid, teacheremail, studentemail, name, profileimage, commentpost, datepost) " +
                                                                 "VALUES (@announcementid, @roomid,  @teacheremail, @studentemail, @name, @profileimage, @commentpost, @datepost)";

                                            using (MySqlCommand commandInsert = new MySqlCommand(insertQuery, con))
                                            {
                                                commandInsert.Parameters.AddWithValue("@announcementid", announcementId);
                                                commandInsert.Parameters.AddWithValue("@roomid", roomIdFromQueryString);
                                                commandInsert.Parameters.AddWithValue("@teacheremail", teacheremail);
                                                commandInsert.Parameters.AddWithValue("@studentemail", studentemail);
                                                commandInsert.Parameters.AddWithValue("@name", fullname);
                                                byte[] profileImage = GetUserProfileImage(studentid);
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
                        else
                        {
                            // Handle the case where txtcomment is empty
                            ShowErrorMessage("Please enter a comment before posting.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ShowErrorMessage("An error occurred while processing your request.");
            }

            DisplayComment();
        }

    }
}