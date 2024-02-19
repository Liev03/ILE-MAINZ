using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace lms.sis.SISPAGES
{
    public partial class StudentManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Image1.ImageUrl = "/sis/Resources/default.jpg";

        }
        protected void SMadd_Click(object sender, EventArgs e)
        {
            //new add emailcheack
            Random random = new Random();
            int randomDigits = random.Next(1000, 9999);
            string fullname = $"{fname.Text} {mname.Text} {Surname.Text}";

            if (string.IsNullOrWhiteSpace(fname.Text) || string.IsNullOrWhiteSpace(Surname.Text) || string.IsNullOrWhiteSpace(Email.Text) || string.IsNullOrWhiteSpace(Contact.Text) || string.IsNullOrWhiteSpace(IEcontact.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "swal('Something went wrong...','Please fill out all the fields ','error')", true);
                return;
            }

            ConnectionClass conn = new ConnectionClass();

            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string checkEmailQuery = "SELECT COUNT(*) FROM manageuser WHERE Email = @Email";
                    using (MySqlCommand checkEmailCommand = new MySqlCommand(checkEmailQuery, connection))
                    {
                        checkEmailCommand.Parameters.AddWithValue("@Email", Email.Text);
                        int emailCount = Convert.ToInt32(checkEmailCommand.ExecuteScalar());

                        if (emailCount > 0)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "swal('Email already exists.','Please choose a different email address.','error')", true);
                            return;
                        }
                    }

                    UploadFiles(connection, transaction, randomDigits);
                    InsertStudentInfo(connection, transaction, randomDigits, fullname);
                    InsertEmergencyContact(connection, transaction, randomDigits);
                    InsertUser(connection, transaction, randomDigits, fullname);
                    transaction.Commit();

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!','Recorded!', 'success')", true);
                    ClearInputValues(this);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "swal('Something went wrong...','Files Missing','error')", true);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        //old
        //    Random random = new Random();
        //    int randomDigits = random.Next(1000, 9999);
        //    string fullname = $"{fname.Text} {mname.Text} {Surname.Text}";
        //    if (string.IsNullOrWhiteSpace(fname.Text) || string.IsNullOrWhiteSpace(Surname.Text) || string.IsNullOrWhiteSpace(Email.Text) || string.IsNullOrWhiteSpace(Contact.Text) || string.IsNullOrWhiteSpace(IEcontact.Text))
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "swal('Something went wrong...','Please fill out all the fields ','error')", true);
        //        return;
        //    }
        //    ConnectionClass conn = new ConnectionClass();
        //    using (MySqlConnection connection = conn.GetConnection())
        //    {
        //        connection.Open();
        //        MySqlTransaction transaction = connection.BeginTransaction();

        //        try
        //        {
        //            UploadFiles(connection, transaction, randomDigits);
        //            InsertStudentInfo(connection, transaction, randomDigits, fullname);
        //            InsertEmergencyContact(connection, transaction, randomDigits);
        //            InsertUser(connection, transaction, randomDigits, fullname);
        //            transaction.Commit();
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!','Recorded!', 'success')", true);
        //            ClearInputValues(this);
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "swal('Something went wrong...','Files Missing','error')", true);
        //        }
        //        finally
        //        {
        //            connection.Close();
        //            connection.Dispose();
        //        }
        //    }
        //}

        private void UploadFiles(MySqlConnection connection, MySqlTransaction transaction, int randomDigits)
        {
            if (FileUpload1.HasFile && FileUpload1.PostedFile.ContentLength > 0 &&
                FileUpload2.HasFile && FileUpload2.PostedFile.ContentLength > 0)
            {
                string name1 = "Good Moral" + FileUpload1.FileName;
                SaveFile(connection, "saveFile", FileUpload1.FileBytes, name1, randomDigits);

                string name2 = "Form 137" + FileUpload2.FileName;
                SaveFile(connection, "saveFile", FileUpload2.FileBytes, name2, randomDigits);
            }
            else
            {
                throw new Exception("Files are empty");
            }
        }

        private void InsertStudentInfo(MySqlConnection connection, MySqlTransaction transaction, int randomDigits, string fullname)
        {
            string insertStudInfoQuery = "INSERT INTO `student_info`(`studentId`, `name`, `Email`, `gender`, `citizenship`, `birthdate`, `address`, `contact`) VALUES (@value1, @value2,@value3, @value4,@value5, @value6,@value7,@value8)";
            using (MySqlCommand cmd = new MySqlCommand(insertStudInfoQuery, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@value1", randomDigits);
                cmd.Parameters.AddWithValue("@value2", fullname);
                cmd.Parameters.AddWithValue("@value3", Email.Text);
                cmd.Parameters.AddWithValue("@value4", genderRadioButtonList.SelectedValue);
                cmd.Parameters.AddWithValue("@value5", Citizenship.Text);
                cmd.Parameters.AddWithValue("@value6", Birthday.Text);
                cmd.Parameters.AddWithValue("@value7", Address.Text);
                cmd.Parameters.AddWithValue("@value8", Contact.Text);
                int rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        private void InsertEmergencyContact(MySqlConnection connection, MySqlTransaction transaction, int randomDigits)
        {
            string InsertEmergencyContact = "INSERT INTO `emergencycontact`(`studentId`, `guardianName`, `relationship`, `phone`) VALUES (@value1, @value2,@value3, @value4)";
            using (MySqlCommand cmd3 = new MySqlCommand(InsertEmergencyContact, connection, transaction))
            {
                cmd3.Parameters.AddWithValue("@value1", randomDigits);
                cmd3.Parameters.AddWithValue("@value2", IEname.Text);
                cmd3.Parameters.AddWithValue("@value3", IErelationship.Text);
                cmd3.Parameters.AddWithValue("@value4", IEcontact.Text);
                int rowsAffected = cmd3.ExecuteNonQuery();
            }
        }

        private void InsertUser(MySqlConnection connection, MySqlTransaction transaction, int randomDigits, string fullname)
        {

            string email = Email.Text;
            string password = Contact.Text;
            string toEmail = Email.Text;

            SendEmail(toEmail, email, password);

            string InsertUsers = "INSERT INTO `manageuser`(`UserID`, `Name`,`Email`,`Password`,`Role`) VALUES (@value1, @value2,@value3, @value4,@value5)";
            using (MySqlCommand cmd4 = new MySqlCommand(InsertUsers, connection, transaction))
            {
                cmd4.Parameters.AddWithValue("@value1", randomDigits);
                cmd4.Parameters.AddWithValue("@value2", fullname);
                cmd4.Parameters.AddWithValue("@value3", Email.Text);
                cmd4.Parameters.AddWithValue("@value4", Contact.Text);
                cmd4.Parameters.AddWithValue("@value5", "Student");
                int rowsAffected = cmd4.ExecuteNonQuery();
            }
        }

        private void SaveFile(MySqlConnection connection, string storedProcedure, byte[] doc, string fileName, int randomDigits)
        {
            using (MySqlCommand cmd = new MySqlCommand(storedProcedure, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pInt", randomDigits);
                cmd.Parameters.AddWithValue("@pFileName", fileName);
                cmd.Parameters.AddWithValue("@pData", doc);
                cmd.Parameters.AddWithValue("@pFileExtension", Path.GetExtension(fileName));
                cmd.ExecuteNonQuery();
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
                ShowSuccessMessage("Account created successfully. An email has been sent with account details.");

            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error sending email: " + ex.Message);
            }
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
        private void ClearInputValues(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                }
                else if (control is DropDownList)
                {
                    ((DropDownList)control).SelectedIndex = 0;
                }
                else if (control is CheckBox)
                {
                    ((CheckBox)control).Checked = false;
                }

                if (control.Controls.Count > 0)
                {
                    ClearInputValues(control);
                }
            }
        }
        protected void SMupdate_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialogScript", "$('#dialog').show();", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int findID;
            if (int.TryParse(targetId.Text, out findID))
            {
                ConnectionClass connection = new ConnectionClass();
                using (MySqlConnection conn = connection.GetConnection())
                {
                    conn.Open();
                    string infoQuery = "SELECT student_info.*, emergencycontact.* " +
                                       "FROM student_info " +
                                       "INNER JOIN emergencycontact ON student_info.studentId = emergencycontact.studentId " +
                                       "WHERE student_info.studentId = @StudentId;";
                    string imageQuery = "SELECT imageData FROM tblimages WHERE studentId = @StudentId;";
                    using (MySqlCommand infoCommand = new MySqlCommand(infoQuery, conn))
                    using (MySqlCommand imageCommand = new MySqlCommand(imageQuery, conn))
                    {
                        infoCommand.Parameters.AddWithValue("@StudentId", findID);
                        imageCommand.Parameters.AddWithValue("@StudentId", findID);
                        MySqlDataReader infoReader = infoCommand.ExecuteReader();
                        if (infoReader.HasRows && infoReader.Read())
                        {
                            string fullname = infoReader.GetString("name");
                            string[] nameParts = fullname.Split(' ');
                            string firstName = (nameParts.Length > 0) ? nameParts[0] : string.Empty;
                            string lastName = (nameParts.Length > 1) ? nameParts[nameParts.Length - 1] : string.Empty;
                            string middleName = (nameParts.Length > 2) ? nameParts[nameParts.Length - 2] : string.Empty;
                            fname.Text = firstName;
                            mname.Text = middleName;
                            Surname.Text = lastName;
                            Email.Text = infoReader.GetString("email");
                            Birthday.Text = infoReader.GetString("birthdate");
                            Citizenship.Text = infoReader.GetString("citizenship");
                            Contact.Text = infoReader.GetString("contact");
                            Address.Text = infoReader.GetString("address");
                            IEname.Text = infoReader.GetString("guardianName");
                            IErelationship.Text = infoReader.GetString("relationship");
                            IEcontact.Text = infoReader.GetString("phone");
                            string gender = infoReader.GetString("gender");
                            genderRadioButtonList.SelectedValue = gender;
                            infoReader.Close();
                            MySqlDataReader imageReader = imageCommand.ExecuteReader();
                            if (imageReader.HasRows && imageReader.Read())
                            {
                                byte[] imageData = (byte[])imageReader["imageData"];
                                if (imageData != null && imageData.Length > 0)
                                {
                                    string base64String = Convert.ToBase64String(imageData);
                                    Image1.ImageUrl = "data:image/jpeg;base64," + base64String;
                                }
                                else
                                {
                                    Image1.ImageUrl = "Resources/cat.PNG";
                                }
                            }

                            imageReader.Close();
                        }
                        else
                        {
                            infoReader.Close();
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('No Match','No matching record found.', 'error')", true);
                            ClearInputValues(this);
                        }
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Invalid Input','Please enter a valid student ID.', 'error')", true);
            }
        }
        protected void ActualUpdate_Click(object sender, EventArgs e)
        {
            int findID;
            if (int.TryParse(targetId.Text, out findID))
            {
                string fullname = $"{fname.Text} {mname.Text} {Surname.Text}";
                string gender = genderRadioButtonList.SelectedValue;
                ConnectionClass conn = new ConnectionClass();

                using (MySqlConnection connection = conn.GetConnection())
                {
                    connection.Open();
                    try
                    {
                        string updateStudInfoQuery = "UPDATE `student_info` SET `name` = @value1, `Email` = @value2, `gender` = @value3, `citizenship` = @value4, `birthdate` = @value5, `address` = @value6, `contact` = @value7 WHERE studentId = @findID";
                        using (MySqlCommand cmd = new MySqlCommand(updateStudInfoQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@value1", fullname);
                            cmd.Parameters.AddWithValue("@value2", Email.Text);
                            cmd.Parameters.AddWithValue("@value3", gender);
                            cmd.Parameters.AddWithValue("@value4", Citizenship.Text);
                            cmd.Parameters.AddWithValue("@value5", Birthday.Text);
                            cmd.Parameters.AddWithValue("@value6", Address.Text);
                            cmd.Parameters.AddWithValue("@value7", Contact.Text);
                            cmd.Parameters.AddWithValue("@findID", findID);
                            cmd.ExecuteNonQuery();
                        }
                        string updateEmergencycontactQuery = "UPDATE `emergencycontact` SET `guardianName` = @value1, `relationship` = @value2, `phone` = @value3 WHERE studentId  = @findID";
                        using (MySqlCommand cmd3 = new MySqlCommand(updateEmergencycontactQuery, connection))
                        {
                            cmd3.Parameters.AddWithValue("@value1", IEname.Text);
                            cmd3.Parameters.AddWithValue("@value2", IErelationship.Text);
                            cmd3.Parameters.AddWithValue("@value3", IEcontact.Text);
                            cmd3.Parameters.AddWithValue("@findID", findID);
                            cmd3.ExecuteNonQuery();
                        }

                        string updateManageUsersQuery = "UPDATE `manageuser` SET `Name` = @value1, `Email` = @value2 WHERE UserID  = @findID";
                        using (MySqlCommand cmd5 = new MySqlCommand(updateManageUsersQuery, connection))
                        {
                            cmd5.Parameters.AddWithValue("@value1", fullname);
                            cmd5.Parameters.AddWithValue("@value2", Email.Text);
                            cmd5.Parameters.AddWithValue("@findID", findID);
                            cmd5.ExecuteNonQuery();
                            ClearInputValues(this);
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!','Record updated!', 'success')", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "swal('Something went wrong...','Please try again','error')", true);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                        targetId.Text = "";

                    }
                }
            }
        }
        protected void SMcancel_Click(object sender, EventArgs e)
        {
            ClearInputValues(this);
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Account/Logout.aspx");
        }

    }
}