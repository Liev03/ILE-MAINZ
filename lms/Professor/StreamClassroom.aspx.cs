using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Professor
{
    public partial class WebForm10 : System.Web.UI.Page
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

                    DisplayUserProfileImage();


                }
                else
                {
                    ShowErrorMessage("Invalid roomid");
                }

            }
            DisplayAnnouncements();


        //      if (Session["LoggedInUserID"] != null)
        //{
        //    lblUserId.Text = Session["LoggedInUserID"].ToString();
        //}
        }

        protected void btncreatepost_Click(object sender, EventArgs e)
        {
            
            if (Session["RoomId"] != null && int.TryParse(Session["RoomId"].ToString(), out int roomId))
            {
                int teacherId = Convert.ToInt32(Session["LoggedInUserID"]);
                string teacherEmail = Session["LoggedInUserEmail"].ToString();

                string postContent = TextBox1.Text;

                DateTime currentDate = DateTime.Now;

                if (int.TryParse(Request.QueryString["roomid"], out int roomIdFromQueryString))
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();

                        string retrieveTeacherNameQuery = "SELECT firstname, lastname FROM teacher_info WHERE email = @teacheremail";

                        using (MySqlCommand retrieveNameCommand = new MySqlCommand(retrieveTeacherNameQuery, con))
                        {
                            retrieveNameCommand.Parameters.AddWithValue("@teacheremail", teacherEmail);

                            using (MySqlDataReader nameReader = retrieveNameCommand.ExecuteReader())
                            {
                                if (nameReader.Read())
                                {
                                    string teacherFirstName = nameReader["firstname"].ToString();
                                    string teacherLastName = nameReader["lastname"].ToString();
                                    string teacherFullName = $"{teacherFirstName} {teacherLastName}";

                                    nameReader.Close();

                                    string insertQuery = "INSERT INTO announcements (roomid, teacherid, teacheremail, teachername, profileimage, postcontent, datepost) " +
                                                         "VALUES (@roomid, @teacherid, @teacheremail, @teachername, @profileimage, @postcontent, @datepost)";

                                    using (MySqlCommand commandInsert = new MySqlCommand(insertQuery, con))
                                    {
                                        commandInsert.Parameters.AddWithValue("@roomid", roomIdFromQueryString);
                                        commandInsert.Parameters.AddWithValue("@teacherid", teacherId);
                                        commandInsert.Parameters.AddWithValue("@teacheremail", teacherEmail);
                                        commandInsert.Parameters.AddWithValue("@teachername", teacherFullName);

                                        byte[] profileImage = GetUserProfileImage(teacherEmail);
                                        commandInsert.Parameters.AddWithValue("@profileimage", profileImage);

                                        commandInsert.Parameters.AddWithValue("@postcontent", postContent);
                                        commandInsert.Parameters.AddWithValue("@datepost", currentDate);

                                        commandInsert.ExecuteNonQuery();

                                        //SendAnnouncementEmail(postContent, teacherEmail, roomIdFromQueryString);


                                        TextBox1.Text = "";
                                        ShowSuccessMessage("Your post has been successfully posted");
                                    }
                                }
                            }
                        }
                    }
                }
                DisplayAnnouncements();
            }
        }
        //private void SendAnnouncementEmail(string postContent, string teacherEmail, int roomIdFromQueryString)
        //{
        //    try
        //    {
        //        // Retrieve logged-in user's email from session
        //        string loggedInUserEmail = Session["LoggedInUserEmail"] as string;

        //        if (string.IsNullOrEmpty(loggedInUserEmail))
        //        {
        //            ShowErrorMessage("Unable to retrieve the logged-in user's email.");
        //            return;
        //        }

        //        // Retrieve SMTP credentials from the database
        //        string smtpConnectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        //        using (MySqlConnection smtpConnection = new MySqlConnection(smtpConnectionString))
        //        {   
        //            smtpConnection.Open();

        //            string smtpQuery = "SELECT smtp_email, smtp_password FROM smtp_credentials WHERE smtp_email = @smtp_email";

        //            using (MySqlCommand smtpCmd = new MySqlCommand(smtpQuery, smtpConnection))
        //            {
        //                smtpCmd.Parameters.AddWithValue("@smtp_email", loggedInUserEmail);

        //                using (MySqlDataReader reader = smtpCmd.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        string smtpEmail = reader["smtp_email"].ToString();
        //                        string smtpPassword = reader["smtp_password"].ToString();

        //                        string smtpServer = "smtp.gmail.com";
        //                        int smtpPort = 587;

        //                        reader.Close();

        //                        List<string> studentEmails = new List<string>();

        //                        string queryStudents = "SELECT studentemail FROM invitation WHERE roomid = @roomid AND status = 'Accepted'";

        //                        using (MySqlCommand commandStudents = new MySqlCommand(queryStudents, smtpConnection))
        //                        {
        //                            commandStudents.Parameters.AddWithValue("@roomid", roomIdFromQueryString);

        //                            using (MySqlDataReader readerStudents = commandStudents.ExecuteReader())
        //                            {
        //                                while (readerStudents.Read())
        //                                {
        //                                    string studentEmail = readerStudents["studentemail"].ToString();
        //                                    studentEmails.Add(studentEmail);
        //                                }
        //                            }
        //                        }

        //                        foreach (string studentEmail in studentEmails)
        //                        {
        //                            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
        //                            {
        //                                smtpClient.EnableSsl = true;
        //                                smtpClient.Credentials = new NetworkCredential(smtpEmail, smtpPassword);

        //                                string roomLink = "https://localhost:44304/Account/Login.aspx";

        //                                string subject = $"Novaliches High School: Announcements";
        //                                string body = $"Dear Student, <br/><br/>You have a room Announcements from {teacherEmail}. " +
        //                                              $"<br/>{postContent}. Click <a href=\"{roomLink}\">here</a> to view in the room." +
        //                                              $"<br/><br/>Best regards, <br/>{teacherEmail}";

        //                                MailMessage mailMessage = new MailMessage(smtpEmail, studentEmail, subject, body);
        //                                mailMessage.IsBodyHtml = true;

        //                                smtpClient.Send(mailMessage);
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        ShowErrorMessage("SMTP credentials not found for the logged-in user's email.");
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowErrorMessage($"Error sending email: {ex.Message}");
        //        throw;
        //    }
        //}
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
                                       "WHERE roomid = @roomid AND teacherid = @teacherid " +
                                       "ORDER BY datepost DESC ";

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
                // Provide a default image URL if the profile image is null
                return "path/to/default/image.jpg";
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
       
        
    }
}