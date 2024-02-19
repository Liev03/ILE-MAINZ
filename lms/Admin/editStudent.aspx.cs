using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Admin
{
    public partial class WebForm12 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox3.TextChanged += new EventHandler(TextBox3_TextChanged);
            TextBox4.Enabled = false;
            if (!IsPostBack)
            {

                if (Request.QueryString["studentid"] != null)
                {
                    int studentId;
                    if (int.TryParse(Request.QueryString["studentid"], out studentId))
                    {
                        try
                        {
                            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                            using (MySqlConnection con = new MySqlConnection(connectionString))
                            {
                                con.Open();

                                string queryStudent = "SELECT * FROM student_info WHERE studentid = @studentid";

                                using (MySqlCommand commandStudent = new MySqlCommand(queryStudent, con))
                                {
                                    commandStudent.Parameters.AddWithValue("@studentid", studentId);

                                    using (MySqlDataReader readerStudent = commandStudent.ExecuteReader())
                                    {
                                        if (readerStudent.Read())
                                        {
                                            TextBox1.Text = readerStudent["firstname"].ToString();
                                            TextBox2.Text = readerStudent["lastname"].ToString();
                                            txtusername.Text = readerStudent["username"].ToString();
                                            TextBox3.Text = readerStudent["birthday"].ToString();
                                            TextBox4.Text = readerStudent["age"].ToString();

                                            TextBox1.Enabled = false;
                                            TextBox2.Enabled = false;
                                            TextBox3.Enabled = false;
                                            TextBox4.Enabled = false;
                                            TextBox5.Enabled = false;
                                            TextBox6.Enabled = false;

                                            ImagePreview.Enabled = false;
                                            FileUpload1.Enabled = false;

                                            string gender = readerStudent["gender"].ToString();
                                            if (gender == "Male")
                                            {
                                                RadioButton1.Checked = true;
                                                RadioButton1.Enabled = false;
                                                RadioButton2.Enabled = false;
                                            }
                                            else if (gender == "Female")
                                            {
                                                RadioButton2.Checked = true;
                                                RadioButton1.Enabled = false;
                                                RadioButton2.Enabled = false;
                                            }

                                            TextBox5.Text = readerStudent["contact"].ToString();
                                            TextBox6.Text = readerStudent["email"].ToString();

                                            byte[] imageBytes = (byte[])readerStudent["profileimage"];
                                            if (imageBytes != null && imageBytes.Length > 0)
                                            {
                                                string base64String = Convert.ToBase64String(imageBytes);
                                                ImagePreview.ImageUrl = "data:image/jpeg;base64," + base64String;
                                            }
                                            string status = readerStudent["status"].ToString();
                                            if (status == "Activated")
                                            {
                                                RadioButton3.Checked = true;
                                            }
                                            else if (status == "Deactivated")
                                            {
                                                RadioButton4.Checked = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                    }
                }
            }
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string dateStr = TextBox3.Text;
                DateTime dob;

                if (DateTime.TryParseExact(dateStr, "M/d/yyyy", null, DateTimeStyles.None, out dob))
                {
                    int age = CalculateAge(dob);

                    TextBox4.Text = "" + age + "";
                }
                else
                {
                    ShowErrorMessage("Invalid date format. Please enter a valid date in the format M/d/yyyy (e.g., 7/21/2005).");

                    //Label1.Text = "Invalid date format. Please enter a valid date in the format M/d/yyyy (e.g., 7/21/2005).";
                }
            }
            catch (FormatException)
            {
                ShowErrorMessage("Invalid date format. Please enter a valid date in the format M/d/yyyy (e.g., 7/21/2005).");

                //Label1.Text = "Invalid date format. Please enter a valid date in the format M/d/yyyy (e.g., 7/21/2005).";
            }
        }
        private int CalculateAge(DateTime dob)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - dob.Year;

            if (currentDate.Month < dob.Month || (currentDate.Month == dob.Month && currentDate.Day < dob.Day))
            {
                age--;
            }

            return age;
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


        protected void btnedit_Click(object sender, EventArgs e)
        {
            string firstName = TextBox1.Text;
            string lastName = TextBox2.Text;
            string email = TextBox6.Text;
            string username = txtusername.Text;
            string gender = RadioButton1.Checked ? "Male" : "Female";
            string status = RadioButton3.Checked ? "Activated" : "Deactivated";
            string birthday = TextBox3.Text;
            string contact = TextBox5.Text;
            int teacherage = int.Parse(TextBox4.Text);

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(TextBox4.Text) || string.IsNullOrWhiteSpace(TextBox3.Text) || string.IsNullOrWhiteSpace(TextBox5.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error', text: 'Please fill out all the textboxes and select a file'})", true);
                return;
            }
            else
            {
                try
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();

                        byte[] fileBytes = null;

                        if (FileUpload1.HasFile)
                        {
                            fileBytes = new byte[FileUpload1.PostedFile.InputStream.Length];
                            FileUpload1.PostedFile.InputStream.Read(fileBytes, 0, fileBytes.Length);
                        }


                        string userQuery = "UPDATE users SET email = @Email, profileimage = @ProfileImage, status = @Status WHERE username = @Username";
                        using (MySqlCommand userCmd = new MySqlCommand(userQuery, con))
                        {
                            userCmd.Parameters.AddWithValue("@Username", username);
                            userCmd.Parameters.AddWithValue("@Email", email);
                            userCmd.Parameters.AddWithValue("@ProfileImage", fileBytes ?? GetExistingProfileImage(username, con));
                            userCmd.Parameters.AddWithValue("@Status", status);

                            int userRowsAffected = userCmd.ExecuteNonQuery();

                            string studentQuery = "UPDATE student_info SET firstname = @FirstName, lastname = @LastName, email = @Email, age = @Age, gender = @Gender, birthday = @Birthday, contact = @Contact, profileimage = @ProfileImage, status = @Status WHERE username = @Username";
                            using (MySqlCommand studentCmd = new MySqlCommand(studentQuery, con))
                            {
                                studentCmd.Parameters.AddWithValue("@Username", username);
                                studentCmd.Parameters.AddWithValue("@FirstName", firstName);
                                studentCmd.Parameters.AddWithValue("@LastName", lastName);
                                studentCmd.Parameters.AddWithValue("@Email", email);
                                studentCmd.Parameters.AddWithValue("@Age", teacherage);
                                studentCmd.Parameters.AddWithValue("@Gender", gender);
                                studentCmd.Parameters.AddWithValue("@Birthday", birthday);
                                studentCmd.Parameters.AddWithValue("@Contact", contact);
                                studentCmd.Parameters.AddWithValue("@ProfileImage", fileBytes ?? GetExistingProfileImage(username, con));
                                studentCmd.Parameters.AddWithValue("@Status", status);

                                int studentRowsAffected = studentCmd.ExecuteNonQuery();

                                if (userRowsAffected > 0 && studentRowsAffected > 0)
                                {
                                    ShowSuccessMessage("The Student account has been updated successfully.");
                                    ClearInputFields();
                                }
                                else
                                {
                                    ShowErrorMessage("Error updating Student information or user information");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("Error processing: " + ex.Message);
                    // Log the exception for debugging
                }
            }
        }

        private void ClearInputFields()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            txtusername.Text = "";
            TextBox6.Text = "";
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
            ImagePreview.ImageUrl = "";
        }

        private byte[] GetExistingProfileImage(string username, MySqlConnection con)
        {
            string query = "SELECT profileimage FROM users WHERE username = @Username";
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                var result = cmd.ExecuteScalar();
                return result as byte[];
            }
        }

        private bool IsEmailUnique(string email)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT COUNT(*) FROM users WHERE email = @Email " +
                               "UNION ALL " +
                               "SELECT COUNT(*) FROM student_info WHERE email = @Email " +
                               "UNION ALL " +
                               "SELECT COUNT(*) FROM teacher_info WHERE email = @Email";

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int totalCount = 0;

                        while (reader.Read())
                        {
                            totalCount += reader.GetInt32(0);
                        }

                        return totalCount == 0;
                    }
                }
            }
        }

        protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                TextBox1.Enabled = true;
                TextBox2.Enabled = true;
                TextBox3.Enabled = true;
                TextBox4.Enabled = true;
                TextBox5.Enabled = true;
                TextBox6.Enabled = true;
                RadioButton1.Enabled = true;
                RadioButton2.Enabled = true;
                RadioButton3.Enabled = true;
                RadioButton4.Enabled = true;
                FileUpload1.Enabled = true;

            }
            else
            {
                TextBox1.Enabled = false;
                TextBox2.Enabled = false;
                TextBox3.Enabled = false;
                TextBox4.Enabled = false;
                TextBox5.Enabled = false;
                TextBox6.Enabled = false;
                RadioButton1.Enabled = false;
                RadioButton2.Enabled = false;
                RadioButton3.Enabled = false;
                RadioButton4.Enabled = false;
                FileUpload1.Enabled = false;
            }

        }
    }
}
