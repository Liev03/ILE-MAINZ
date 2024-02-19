using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Student
{
    public partial class classroomMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoggedInUser"] == null)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    string userType = Session["Role"] as string;

                    if (userType == "teacher")
                    {
                        Response.Redirect("~/Professor/DashBoard.aspx");
                    }
                    else if (userType == "admin")
                    {
                        Response.Redirect("~/Admin/DashBoard.aspx");
                    }
                    else
                    {
                        string userEmail = Session["LoggedInUser"] as string;
                        string UserID = Session["ID"] as string;


                        if (!string.IsNullOrEmpty(userEmail))
                        {
                            lblUserEmail.Text = userEmail;

                            if (!string.IsNullOrEmpty(UserID))
                            {
                                using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
                                {
                                    connection.Open();
                                    GetUserProfileImage(UserID);

                                    byte[] profileImageBytes = GetUserProfileImage(UserID);
                                    if (profileImageBytes != null)
                                    {
                                        string base64Image = Convert.ToBase64String(profileImageBytes);
                                        string imageSrc = "data:image/jpeg;base64," + base64Image;
                                        Image1.ImageUrl = imageSrc;
                                    }
                                }
                            }
                        }
                    }

                    if (!IsPostBack)
                    {

                        if (!string.IsNullOrEmpty(Request.QueryString["roomid"]))
                        {

                            if (int.TryParse(Request.QueryString["roomid"], out int roomId))

                            {
                                try
                                {
                                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                                    using (MySqlConnection con = new MySqlConnection(connectionString))
                                    {
                                        con.Open();
                                        string queryRooms = "SELECT * FROM rooms WHERE roomid = @roomid";

                                        using (MySqlCommand commandRooms = new MySqlCommand(queryRooms, con))
                                        {
                                            commandRooms.Parameters.AddWithValue("@roomid", roomId);

                                            using (MySqlDataReader readerRooms = commandRooms.ExecuteReader())
                                            {
                                                if (readerRooms.Read())
                                                {
                                                    lblsubjectname.Text = readerRooms["subjectname"].ToString();
                                                    lblschedule.Text = readerRooms["schedule"].ToString();

                                                }
                                            }
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }

                    }
                }
            }
        }

        private byte[] GetUserProfileImage(string UserID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT ImageData FROM tblimages WHERE studentId = @studentID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@studentID", UserID);
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
    }
}