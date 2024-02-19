using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.LearningHub
{
    public partial class MakeRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindImage();
                if (Session["UID"] != null)
                {
                    string uidValue = Session["UID"].ToString();

                    string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        uidValue = Session["UID"].ToString();

                        string query = "SELECT * FROM student_info WHERE uid = @UID";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@UID", uidValue);

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string nameValue = reader["name"].ToString();
                                    Name.Text = "Name: " + nameValue;
                                    SID.Text = "Student ID: " + reader["studentId"].ToString();
                                    Email.Text = "Email: " + reader["Email"].ToString();
                                    Contact.Text = "Contact Number: " + reader["contact"].ToString();
                                    //string profilePictureLink = reader["pfp"].ToString();
                                    //ImagePF.ImageUrl = GetDirectLinkFromGoogleDrive(profilePictureLink);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void bindImage()
        {
            string uidValue = Session["UID"].ToString();
            string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string imageQuery = "SELECT imageData FROM tblimages WHERE studentId = @ID;";
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
                        }
                        else
                        {
                            ImagePF.ImageUrl = "../Resources/default.png";
                        }
                        imageReader.Close();
                    }
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
        protected void ReqSubmit_Click(object sender, EventArgs e)
        {
            string uid = Session["UID"].ToString();

            string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO bulletin (uid, name, looking, strand, yearlevel, availability, location, role, buldate) " +
                "VALUES (@uid, (SELECT name FROM student_info WHERE uid = @uid), @looking, @strand, @yearLevel, @availability, @location, @role, CURDATE());";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@uid", uid);

                    string lookingFor = (ReqTutee.Checked) ? "Tutee" : "Tutor";
                    command.Parameters.AddWithValue("@looking", lookingFor);

                    string strand = GetSelectedRadioValue(StrandRadios);
                    command.Parameters.AddWithValue("@strand", strand);

                    string yearLevel = GetSelectedRadioValue(YearLevelRadios);
                    command.Parameters.AddWithValue("@yearLevel", yearLevel);

                    string availability = string.Join(", ", new[] {
                ReqSun.Checked ? "Sunday" : "",
                ReqMon.Checked ? "Monday" : "",
                ReqTues.Checked ? "Tuesday" : "",
                ReqWed.Checked ? "Wednesday" : "",
                ReqThur.Checked ? "Thursday" : "",
                ReqFri.Checked ? "Friday" : "",
                ReqSat.Checked ? "Saturday" : ""
            }.Where(x => !string.IsNullOrEmpty(x)));

                    command.Parameters.AddWithValue("@availability", availability);

                    string location = string.Join(", ", new[] {
                ReqHome.Checked ? "Home" : "",
                ReqSchool.Checked ? "School" : "",
                ReqPublic.Checked ? "Public Place" : ""
            }.Where(x => !string.IsNullOrEmpty(x)));

                    command.Parameters.AddWithValue("@location", location);

                    string role = (lookingFor == "Tutee") ? "Tutor" : "Tutee";
                    command.Parameters.AddWithValue("@role", role);

                    command.ExecuteNonQuery();
                }
            }

            ReqTutee.Checked = false;
            ReqTutor.Checked = false;
            UncheckRadioButtons(StrandRadios);
            UncheckRadioButtons(YearLevelRadios);
            ReqSun.Checked = false;
            ReqMon.Checked = false;
            ReqTues.Checked = false;
            ReqWed.Checked = false;
            ReqThur.Checked = false;
            ReqFri.Checked = false;
            ReqSat.Checked = false;
            ReqHome.Checked = false;
            ReqSchool.Checked = false;
            ReqPublic.Checked = false;

            Response.Write("<script>alert('Success')</script>");
        }
        private void UncheckRadioButtons(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control is RadioButton radioButton)
                {
                    radioButton.Checked = false;
                }
            }
        }
        private string GetSelectedRadioValue(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    return radioButton.Text;
                }
            }
            return string.Empty;
        }


    }

}