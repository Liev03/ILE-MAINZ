using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.LearningHub
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["ID"] != null)
                    {
                        string uidValue = Session["ID"].ToString();
                        string yearlevel = Session["AcademicInfoYearLevel"].ToString();
                        string connectionString = "Server=localhost;Database=learninghub;User=root;Password=;";

                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();

                            string query = "SELECT * FROM student_info WHERE studentId  = @ID";
                            string imageQuery = "SELECT imageData FROM tblimages WHERE studentId = @ID;";

                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@ID", uidValue);

                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string birthdateString = reader["birthdate"].ToString();
                                        DateTime birthdate = DateTime.Parse(birthdateString);
                                        DateTime currentDate = DateTime.Now;
                                        int age = currentDate.Year - birthdate.Year - (currentDate.Month < birthdate.Month || (currentDate.Month == birthdate.Month && currentDate.Day < birthdate.Day) ? 1 : 0);
                                        InfoName.Text = "Name: " + reader["name"].ToString();
                                        InfoStudId.Text = "Student ID: " + reader["studentId"].ToString();
                                        InfoAge.Text = "Age: " + age;
                                        InfoSex.Text = "Sex: " + reader["gender"].ToString();
                                        InfoLocation.Text = "Location: " + reader["address"].ToString();
                                        InfoYearLvl.Text = "Year Level: " + yearlevel;
                                        ContactEmail.Text = "Email: " + reader["Email"].ToString();
                                        ContactNumber.Text = "Contact Number: " + reader["contact"].ToString();
                                        ContactSocmed.Text = "Social Media: " + reader["Email"].ToString();
                                        ContactBioLabel.Text = reader["Bio"].ToString();
                                        reader.Close();
                                    }
                                }
                                if (uidValue != null)
                                {
                                    using (MySqlCommand imageCommand = new MySqlCommand(imageQuery, connection))
                                    {
                                        imageCommand.Parameters.AddWithValue("@ID", uidValue);
                                        MySqlDataReader imageReader = imageCommand.ExecuteReader();
                                        if (imageReader.HasRows && imageReader.Read())
                                        {
                                            byte[] imageData = (byte[])imageReader["imageData"];
                                            if (imageData != null && imageData.Length > 0)
                                            {
                                                string base64String = Convert.ToBase64String(imageData);
                                                ImagePF.ImageUrl = "data:image/jpeg;base64," + base64String;
                                                imageReader.Close();
                                            }
                                            else
                                            {
                                                ImagePF.ImageUrl = "Resources/cat.PNG";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message + " Something Went Wrong");
                }
            }
        }
        private string GetDirectLinkFromGoogleDrive(string googleDriveLink)
        {

            if (googleDriveLink.Contains("drive.google.com/file/d/"))
            {
                int start = googleDriveLink.IndexOf("drive.google.com/file/d/") + "drive.google.com/file/d/".Length;
                int end = googleDriveLink.IndexOf("/view");

                if (start != -1 && end != -1)
                {
                    string fileId = googleDriveLink.Substring(start, end - start);

                    return $"https://drive.google.com/uc?export=view&id={fileId}";
                }
            }

            return googleDriveLink;
        }
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string newBio = BioTextarea.Text;

            if (BioExistsInDatabase())
            {
                UpdateBioInDatabase(newBio);
            }
            else
            {
                InsertBioIntoDatabase(newBio);
            }
            string updatedBio = RetrieveBioFromDatabase();
            ContactBioLabel.Text = updatedBio;
        }

        private void UpdateBioInDatabase(string newBio)
        {
            string connectionString = "Server=localhost;Database=learninghub;User=root;Password=;";
            string query = "UPDATE student_info SET Bio = @Bio WHERE studentId = @ID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    string uidValue = Session["ID"].ToString();

                    command.Parameters.AddWithValue("@Bio", newBio);
                    command.Parameters.AddWithValue("@ID", uidValue);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        private bool BioExistsInDatabase()
        {
            string connectionString = "Server=localhost;Database=learninghub;User=root;Password=;";
            string query = "SELECT COUNT(Bio) FROM student_info WHERE studentId = @ID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    string uidValue = Session["ID"].ToString();
                    command.Parameters.AddWithValue("@ID", uidValue);

                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }
        private void InsertBioIntoDatabase(string newBio)
        {
            string connectionString = "Server=localhost;Database=learninghub;User=root;Password=;";
            string query = "UPDATE student_info SET Bio = @Bio WHERE studentId = @ID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    string uidValue = Session["ID"].ToString();

                    command.Parameters.AddWithValue("@ID", uidValue);
                    command.Parameters.AddWithValue("@Bio", newBio);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        private string RetrieveBioFromDatabase()
        {
            string connectionString = "Server=localhost;Database=learninghub;User=root;Password=;";
            string query = "SELECT Bio FROM student_info WHERE studentId = @ID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (Session["ID"] != null)
                    {
                        string uidValue = Session["ID"].ToString();
                        command.Parameters.AddWithValue("@ID", uidValue);

                        connection.Open();

                        string bio = command.ExecuteScalar()?.ToString() ?? "";

                        return bio;
                    }
                    else
                    {
                        return "Student UID not found in session.";
                    }
                }
            }
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Index.aspx");
        }
    }
}