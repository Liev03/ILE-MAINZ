using lms.sis.SISPAGES;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Account
{
    public partial class Announce : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayNewData();
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
        public class DataHelper
        {
            public DataTable GetNewData()
            {
                DataTable dataTable = new DataTable();
                ConnectionClass conn = new ConnectionClass();
                using (MySqlConnection connection = conn.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM announcement ORDER BY AnnouncementID DESC LIMIT 1";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                return dataTable;
            }
        }
        protected void DisplayNewData()
        {
            DataHelper dataHelper = new DataHelper();
            DataTable newData = dataHelper.GetNewData();

            if (newData.Rows.Count > 0)
            {
                StringBuilder announcements = new StringBuilder();

                foreach (DataRow row in newData.Rows)
                {
                    string dataValue = row["Post"].ToString();
                    announcements.Append("<div class='announcement'>" + "ANNOUNCEMENT : " + dataValue + "</div>");
                }

                lbl1.Text = announcements.ToString();
            }
        }
    }
}