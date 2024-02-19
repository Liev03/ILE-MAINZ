using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Admin
{
    public partial class AddStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox3.TextChanged += new EventHandler(TextBox3_TextChanged);
            TextBox4.Enabled = false;

            string firstname = txtfirstname.Text;
            string lastname = txtlastname.Text;
            string generatedUsername = (firstname + lastname).ToLower();
            txtusername.Text = generatedUsername;
            txtusername.ReadOnly = true;
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            string profileimage = "";

            string firstName = txtfirstname.Text;
            string lastName = txtlastname.Text;
            string email = TextBox6.Text;
            string username = txtusername.Text;
            string toEmail = TextBox6.Text;

            int age;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(TextBox4.Text) || string.IsNullOrWhiteSpace(TextBox3.Text) || string.IsNullOrWhiteSpace(TextBox5.Text) || string.IsNullOrWhiteSpace(TextBox7.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error', text: 'Please fill out all the textboxes and select a file'})", true);
                return;
            }

            if (int.TryParse(TextBox4.Text, out age))
            {
                string gender = RadioButton1.Checked ? "Male" : "Female";
                string birthday = TextBox3.Text;
                string contact = TextBox5.Text;
                string password = TextBox7.Text;

                if (IsEmailUnique(email))

                {
                    if (IsUsernameUnique(username))
                    {


                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                        using (MySqlConnection con = new MySqlConnection(connectionString))
                        {
                            try
                            {
                                con.Open();

                                string userQuery = "INSERT INTO users (username, password, email, usertype, status, profileimage) VALUES (@Username, @Password, @Email, 'student', 'Activated', @ProfileImage)";
                                using (MySqlCommand userCmd = new MySqlCommand(userQuery, con))
                                {
                                    userCmd.Parameters.AddWithValue("Username", username);
                                    userCmd.Parameters.AddWithValue("@Password", password);
                                    userCmd.Parameters.AddWithValue("@Email", email);

                                    if (FileUpload1.HasFile)
                                    {
                                        byte[] imageData = FileUpload1.FileBytes;
                                        userCmd.Parameters.Add(new MySqlParameter("@ProfileImage", imageData));

                                        ImagePreview.Visible = true;
                                        ImagePreview.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageData);
                                    }
                                    else
                                    {
                                        string defaultImagePath = Server.MapPath("~/Resources/default.png");
                                        byte[] defaultImageData = File.ReadAllBytes(defaultImagePath);
                                        userCmd.Parameters.AddWithValue("@ProfileImage", defaultImageData);
                                    }


                                    int userRowsAffected = userCmd.ExecuteNonQuery();

                                    if (userRowsAffected > 0)

                                    {
                                        SendEmail(toEmail, email, password);

                                        string studentQuery = "INSERT INTO student_info (username, firstname, lastname, email, age, gender, birthday, contact, status, profileimage) VALUES (@Username, @FirstName, @LastName, @Email, @Age, @Gender, @Birthday, @Contact, 'Activated', @ProfileImage)";
                                        using (MySqlCommand studentCmd = new MySqlCommand(studentQuery, con))
                                        {
                                            studentCmd.Parameters.AddWithValue("@Username", username);
                                            studentCmd.Parameters.AddWithValue("@FirstName", firstName);
                                            studentCmd.Parameters.AddWithValue("@LastName", lastName);
                                            studentCmd.Parameters.AddWithValue("@Email", email);
                                            studentCmd.Parameters.AddWithValue("@Age", age);
                                            studentCmd.Parameters.AddWithValue("@Gender", gender);
                                            studentCmd.Parameters.AddWithValue("@Birthday", birthday);
                                            studentCmd.Parameters.AddWithValue("@Contact", contact);

                                            if (FileUpload1.HasFile)
                                            {
                                                byte[] imageData = FileUpload1.FileBytes;
                                                studentCmd.Parameters.Add(new MySqlParameter("@ProfileImage", imageData)); // Use a different parameter name
                                            }
                                            else
                                            {
                                                string defaultImagePath = Server.MapPath("~/Resources/default.png");
                                                byte[] defaultImageData = File.ReadAllBytes(defaultImagePath);
                                                studentCmd.Parameters.AddWithValue("@ProfileImage", defaultImageData);
                                            }



                                            int studentRowsAffected = studentCmd.ExecuteNonQuery();

                                            if (studentRowsAffected > 0)
                                            {
                                                ShowSuccessMessage("The student has been added successfully, and the account details have been sent to the email");
                                                txtfirstname.Text = "";
                                                txtlastname.Text = "";
                                                TextBox3.Text = "";
                                                TextBox4.Text = "";
                                                TextBox5.Text = "";
                                                txtusername.Text = "";
                                                TextBox6.Text = "";
                                                TextBox7.Text = "";
                                                RadioButton1.Checked = false;
                                                RadioButton2.Checked = false;
                                                ImagePreview.ImageUrl = "";

                                                ClientScript.RegisterStartupScript(this.GetType(), "successMessage", "showSuccessMessage();", true);


                                            }
                                            else
                                            {
                                                ShowErrorMessage("Error adding Student to table");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ShowErrorMessage("Error adding Student to table");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ShowErrorMessage("Error Processing");
                            }
                        }
                    }
                    else
                    {
                        ShowErrorMessage("Username already exists");
                    }
                }
                else
                {
                    ShowErrorMessage("Email already Exists");
                }
            }
            else
            {
                ShowErrorMessage("Invalid Age");
            }
        }
        private void SendEmail(string toEmail, string fromEmail, string password)
        {
            string subject = "Your Account Details";
            string body = $"Your account has been created.\n\nEmail: {fromEmail}\nPassword: {password}";

            MailMessage mail = new MailMessage("novalichesseniorhighschoolnhs@gmail.com", toEmail, subject, body);

            mail.IsBodyHtml = false;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("novalichesseniorhighschoolnhs@gmail.com", "kymucdmewcovazly");

      
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mail);
                ShowSuccessMessage("Email sent with account details.");

            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error sending email: " + ex.Message);
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
                }
            }
            catch (FormatException)
            {
                ShowErrorMessage("Invalid date format. Please enter a valid date in the format M/d/yyyy (e.g., 7/21/2005).");
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
        private bool IsUsernameUnique(string username)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT COUNT(*) FROM users WHERE username = @Username " +
                               "UNION ALL " +
                               "SELECT COUNT(*) FROM student_info WHERE username = @Username " +
                               "UNION ALL " +
                               "SELECT COUNT(*) FROM teacher_info WHERE username = @Username";

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

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

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Student.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtfirstname.Text = string.Empty;
            txtlastname.Text = string.Empty;
            TextBox3.Text = string.Empty;
            TextBox4.Text = string.Empty;
            TextBox5.Text = string.Empty;
            txtusername.Text = string.Empty;
            TextBox6.Text = string.Empty;
            TextBox7.Text = string.Empty;
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
            ImagePreview.ImageUrl = "";
        }
    }
}