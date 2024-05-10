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
    public partial class studentMasterPage : System.Web.UI.MasterPage
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
                        string name = Session["Name"] as string;


                        if (!string.IsNullOrEmpty(userEmail))
                        {
                            lblUserEmail.Text = name;

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

        private void DisplayFileDetailsInTextBox(string materialsid)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT FileName, duedate FROM learningmaterials WHERE materialsid = @materialsid";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@materialsid", materialsid);
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string fileName = reader["FileName"].ToString();
                            string dueDate = reader["duedate"].ToString();

                            
                            string fileDetails = $"File Name: {fileName}\nDue Date: {dueDate}";

                            
                            TB_duedate.Text = fileDetails;
                        }
                    }
                }
            }
        }


    }

}
