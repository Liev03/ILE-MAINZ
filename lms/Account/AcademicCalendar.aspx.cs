using lms.sis.SISPAGES;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace lms.Account
{
    public partial class AcademicCalendar : System.Web.UI.Page
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
                                    DisplayUploadedImage(connection, UserID);
                                    GetUserProfileImage(UserID);

                                   
                                }
                            }
                            else
                            {
                                // Handle the case where UserID is null or empty
                                // You may want to log an error or display a message.
                            }
                        }
                        else
                        {
                            // Handle the case where userEmail is null or empty
                            // You may want to log an error or display a message.
                        }
                    }
                }
            }
            if (!IsPostBack)
            {
                ConnectionClass conn = new ConnectionClass();
                List<Event> eventsList = new List<Event>();

                using (MySqlConnection connection = conn.GetConnection())
                {
                    connection.Open();


                    string select = "SELECT * FROM `events`";

                    using (MySqlCommand cmd = new MySqlCommand(select, connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string title = reader["title"].ToString();
                                    DateTime start = Convert.ToDateTime(reader["start"]);
                                    string description = reader["description"].ToString();

                                    eventsList.Add(new Event { Title = title, Date = start, Description = description });
                                }
                            }
                        }
                    }

                }

                ViewState["EventsList"] = eventsList;
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
        private void DisplayUploadedImage(MySqlConnection connection, string userID)
        {
            string fetchImageQuery = "SELECT ImageData FROM tblimages WHERE studentId = @studentID";
            using (MySqlCommand fetchCmd = new MySqlCommand(fetchImageQuery, connection))
            {
                fetchCmd.Parameters.AddWithValue("@studentID", userID);
                byte[] imageData = fetchCmd.ExecuteScalar() as byte[];

                if (imageData != null && imageData.Length > 0)
                {
                    string base64String = Convert.ToBase64String(imageData);
                    Image1.ImageUrl = "data:image/jpeg;base64," + base64String;
                }
                else
                {
                    Image1.ImageUrl = "Resources/default.jpg";
                }
            }
        }

        protected void ReadOnlyCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            List<Event> eventsList = ViewState["EventsList"] as List<Event>;

            if (eventsList != null)
            {
                foreach (Event ev in eventsList)
                {
                    if (e.Day.Date == ev.Date.Date)
                    {
                        ApplyStyleBasedOnData(ev, e.Cell);
                        e.Cell.ToolTip = ev.Description;
                        e.Cell.Text = ev.Title;
                    }
                }
            }

            // Disable selection and navigation for the read-only calendar
            e.Day.IsSelectable = false;
        }
        private void ApplyStyleBasedOnData(Event eventData, TableCell cell)
        {
            if (eventData.Description.ToLower().Contains("important"))
            {
                cell.Style.Add("box-shadow", "inset 0px 0px 5px red");
                cell.ForeColor = Color.Red;
            }
            else if (IsCloseToPresent(eventData.Date))
            {
                cell.Style.Add("box-shadow", "inset 0px 0px 5px orangeRed");
                cell.ForeColor = Color.OrangeRed;
            }
            else
            {
                cell.Style.Add("box-shadow", "inset 0px 0px 5px limeGreen");
                cell.ForeColor = Color.LimeGreen;
            }
        }
        private bool IsCloseToPresent(DateTime eventDate)
        {
            TimeSpan threshold = TimeSpan.FromDays(7);
            return Math.Abs((DateTime.Now - eventDate).TotalDays) <= threshold.TotalDays;
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Account/Logout.aspx");
        }
    }
}