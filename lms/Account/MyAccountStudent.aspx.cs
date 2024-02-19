using lms.sis.SISPAGES;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Account
{
    public partial class MyAccountStudent : System.Web.UI.Page
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
                            DisplayTeacherProfileInfo(userEmail);

                            if (!string.IsNullOrEmpty(UserID))
                            {
                                using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
                                {
                                    connection.Open();
                                    DisplayUploadedImage(connection, UserID);
                                    GetUserProfileImage(UserID);

                                    byte[] profileImageBytes = GetUserProfileImage(UserID);
                                    if (profileImageBytes != null)
                                    {
                                        string base64Image = Convert.ToBase64String(profileImageBytes);
                                        string imageSrc = "data:image/jpeg;base64," + base64Image;
                                        Image2.ImageUrl = imageSrc;
                                    }
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
            private void DisplayTeacherProfileInfo(string userEmail)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT studentId, name, citizenship, birthdate, address, Email FROM student_info WHERE email = @userEmail";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userEmail", userEmail);
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            TextBox1.Text = reader["name"].ToString();
                            TextBox2.Text = reader["citizenship"].ToString();
                            TextBox3.Text = reader["studentId"].ToString();
                            TextBox4.Text = reader["address"].ToString();
                            TextBox8.Text = reader["birthdate"].ToString();
                            TextBox9.Text = reader["Email"].ToString();
                            //txtusername.Text = reader["username"].ToString();

                            //byte[] imageBytes = reader["ImageData"] as byte[];
                            //if (imageBytes != null && imageBytes.Length > 0)
                            //{
                            //    string base64String = Convert.ToBase64String(imageBytes);
                            //    Image2.ImageUrl = "data:image/jpeg;base64," + base64String;
                            //}
                        }
                    }
                }
            }
        }

        protected void btnchangeimage_Click(object sender, EventArgs e)
        {
            string UserID = Session["LoggedInUserID"] as string;

            try
            {
                ConnectionClass conn = new ConnectionClass();
                using (MySqlConnection connection = conn.GetConnection())
                {
                    connection.Open();

                    string checkImageQuery = "SELECT COUNT(*) FROM tblimages WHERE studentId = @studentID";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkImageQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@studentID", Session["LoggedInUserID"]);
                        int imageCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (imageCount > 0)
                        {
                            byte[] imageData = FileUpload1.FileBytes;
                            int imageSize = FileUpload1.PostedFile.ContentLength;
                            string imageName = FileUpload1.FileName;

                            string updateQuery = "CALL spUpdateImage(@studentID, @imageName, @imageSize, @imageData)";
                            using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@studentID", Session["LoggedInUserID"]);
                                cmd.Parameters.AddWithValue("@imageName", imageName);
                                cmd.Parameters.AddWithValue("@imageSize", imageSize);
                                cmd.Parameters.AddWithValue("@imageData", imageData);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            byte[] imageData = FileUpload1.FileBytes;
                            int imageSize = FileUpload1.PostedFile.ContentLength;
                            string imageName = FileUpload1.FileName;

                            string insertQuery = "CALL spUploadImage(@imageName, @imageSize, @imageData, @studentID)";
                            using (MySqlCommand cmd = new MySqlCommand(insertQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@studentID", Session["LoggedInUserID"]);
                                cmd.Parameters.AddWithValue("@imageName", imageName);
                                cmd.Parameters.AddWithValue("@imageSize", imageSize);
                                cmd.Parameters.AddWithValue("@imageData", imageData);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Update the displayed image 
                        DisplayUploadedImage(connection, UserID);
                        // Get and display the user's profile image
                        byte[] profileImageBytes = GetUserProfileImage(UserID);
                        if (profileImageBytes != null)
                        {
                            string base64Image = Convert.ToBase64String(profileImageBytes);
                            string imageSrc = "data:image/jpeg;base64," + base64Image;
                            Image2.ImageUrl = imageSrc;
                        }
                    }
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success','Profile updated successfully.','success')", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", $"swal('Error','{ex.Message}','error')", true);
            }
        

        //    string username = txtusername.Text;
        //    try
        //    {
        //        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        //        using (MySqlConnection con = new MySqlConnection(connectionString))
        //        {
        //            con.Open();

        //            byte[] fileBytes = null;

        //            if (FileUpload1.HasFile)
        //            {
        //                fileBytes = new byte[FileUpload1.PostedFile.InputStream.Length];
        //                FileUpload1.PostedFile.InputStream.Read(fileBytes, 0, fileBytes.Length);
        //            }


        //            string userQuery = "UPDATE users SET profileimage = @ProfileImage WHERE username = @Username";
        //            using (MySqlCommand userCmd = new MySqlCommand(userQuery, con))
        //            {
        //                userCmd.Parameters.AddWithValue("@Username", username);
        //                userCmd.Parameters.AddWithValue("@ProfileImage", fileBytes ?? GetExistingProfileImage(username, con));

        //                int userRowsAffected = userCmd.ExecuteNonQuery();

        //                string teacherQuery = "UPDATE student_info SET  profileimage = @ProfileImage WHERE username = @Username";
        //                using (MySqlCommand teacherCmd = new MySqlCommand(teacherQuery, con))
        //                {
        //                    teacherCmd.Parameters.AddWithValue("@Username", username);
        //                    teacherCmd.Parameters.AddWithValue("@ProfileImage", fileBytes ?? GetExistingProfileImage(username, con));

        //                    int teacherRowsAffected = teacherCmd.ExecuteNonQuery();

        //                    if (userRowsAffected > 0 && teacherRowsAffected > 0)
        //                    {
        //                        ShowSuccessMessage("The Teacher Profile Picture has been updated successfully.");
        //                        ClearInputFields(); 
        //                        ClientScript.RegisterStartupScript(this.GetType(), "successMessage", "showSuccessMessage();", true);

        //                    }
        //                    else
        //                    {
        //                        ShowErrorMessage("Error updating Teacher Profile Picture");
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowErrorMessage("Error processing: " + ex.Message);
        //    }
        //}
        //private void ClearInputFields()
        //{
        //    Image2.ImageUrl = "";

        //}
        //private byte[] GetExistingProfileImage(string username, MySqlConnection con)
        //{
        //    string query = "SELECT profileimage FROM users WHERE username = @Username";
        //    using (MySqlCommand cmd = new MySqlCommand(query, con))
        //    {
        //        cmd.Parameters.AddWithValue("@Username", username);
        //        var result = cmd.ExecuteScalar();
        //        return result as byte[];
        //    }
    }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string userEmail = Session["LoggedInUser"] as string;
            string currentPassword = TextBox5.Text;
            string newPassword = TextBox6.Text;
            string confirmPassword = TextBox7.Text;

            if (ValidateCurrentPassword(userEmail, currentPassword) && newPassword == confirmPassword)
            {
                Changepassword(userEmail, newPassword);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!','Password updated successfully!', 'success')", true);
                TextBox5.Text = "";
                TextBox6.Text = "";
                TextBox7.Text = "";
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "swal('Something went wrong...','Failed to update password','error')", true);

            }
        }
        private void Changepassword(string userEmail, string newPassword)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE manageuser SET Password = @newPassword WHERE Email = @userEmail";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);
                    cmd.Parameters.AddWithValue("@userEmail", userEmail);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private bool ValidateCurrentPassword(string userEmail, string currentPassword)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT Password FROM manageuser WHERE Email = @userEmail";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userEmail", userEmail);
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader["Password"].ToString();

                            if (currentPassword == storedPassword)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }


        //string userEmail = Session["LoggedInUserEmail"] as string;
        //string currentPassword = TextBox5.Text;
        //string newPassword = TextBox6.Text;
        //string confirmPassword = TextBox7.Text;

        //if (ValidateCurrentPassword(userEmail, currentPassword) && newPassword == confirmPassword)
        //{
        //    ChangePassword(userEmail, newPassword);
        //    ShowSuccessMessage("Password updated successfully.");
        //    TextBox5.Text = "";
        //    TextBox6.Text = "";
        //    TextBox7.Text = "";

        //}
        //else
        //{
        //    ShowErrorMessage("Failed to update password.");

        //}


        //private void ChangePassword(string userEmail, string newPassword)
        //{
        //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        string query = "UPDATE users SET password = @newPassword WHERE email = @userEmail";

        //        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        //        {
        //            cmd.Parameters.AddWithValue("@newPassword", newPassword);
        //            cmd.Parameters.AddWithValue("@userEmail", userEmail);

        //            connection.Open();
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
        //private bool ValidateCurrentPassword(string userEmail, string currentPassword)
        //{
        //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        string query = "SELECT password FROM users WHERE email = @userEmail";

        //        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        //        {
        //            cmd.Parameters.AddWithValue("@userEmail", userEmail);
        //            connection.Open();

        //            using (MySqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    string storedPassword = reader["password"].ToString();

        //                    if (currentPassword == storedPassword)
        //                    {
        //                        return true;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return false;
        //}
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



        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                string studentid = TextBox3.Text;
                string email = TextBox9.Text;
                string smtppassword = TextBox10.Text;

                if (!string.IsNullOrEmpty(studentid) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(smtppassword))
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        if (!IsEmailExists(email, connection))
                        {
                            string query = "INSERT INTO smtp_credentials (smtp_email, smtp_password) VALUES (@smtp_email, @smtp_password)";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@smtp_email", email);
                                cmd.Parameters.AddWithValue("@smtp_password", smtppassword);

                                connection.Open();
                                cmd.ExecuteNonQuery();
                                ShowSuccessMessage("SMTP Password has been Inserted");
                                TextBox10.Text = "";
                            }
                        }
                        else
                        {
                            ShowErrorMessage("SMTP already exists.");
                        }
                    }
                }
                else
                {
                    ShowErrorMessage("Please provide SMTP password.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred: " + ex.Message);
            }
        }

        private bool IsEmailExists(string email, MySqlConnection connection)
        {
            using (MySqlConnection checkConnection = new MySqlConnection(connection.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM smtp_credentials WHERE smtp_email = @smtp_email";

                using (MySqlCommand cmd = new MySqlCommand(query, checkConnection))
                {
                    cmd.Parameters.AddWithValue("@smtp_email", email);

                    checkConnection.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                string email = TextBox9.Text;
                string newSmtppassword = TextBox10.Text;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string getCurrentPasswordQuery = "SELECT smtp_password FROM smtp_credentials WHERE smtp_email = @smtp_email";
                    using (MySqlCommand getCurrentPasswordCmd = new MySqlCommand(getCurrentPasswordQuery, connection))
                    {
                        getCurrentPasswordCmd.Parameters.AddWithValue("@smtp_email", email);

                        connection.Open();
                        object currentPasswordObj = getCurrentPasswordCmd.ExecuteScalar();

                        if (currentPasswordObj != null)
                        {
                            string currentPassword = currentPasswordObj.ToString();

                            if (!string.Equals(currentPassword, newSmtppassword))
                            {
                                if (!string.IsNullOrEmpty(newSmtppassword))
                                {
                                    string updateQuery = "UPDATE smtp_credentials SET smtp_password = @smtp_password WHERE smtp_email = @smtp_email";

                                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                                    {
                                        updateCmd.Parameters.AddWithValue("@smtp_email", email);
                                        updateCmd.Parameters.AddWithValue("@smtp_password", newSmtppassword);

                                        int rowsAffected = updateCmd.ExecuteNonQuery();

                                        if (rowsAffected > 0)
                                        {
                                            ShowSuccessMessage("SMTP Password has been updated.");
                                        }
                                        else
                                        {
                                            ShowErrorMessage("Failed to update SMTP Password.");
                                        }
                                    }
                                }
                                else
                                {
                                    ShowErrorMessage("New SMTP Password cannot be null or empty. No update performed.");
                                }
                            }
                            else
                            {
                                ShowErrorMessage("The new password is the same as the current password. No update performed.");
                            }
                        }
                        else
                        {
                            ShowErrorMessage("Email not found in the database.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred: " + ex.Message);
            }
        }
    }
 }
