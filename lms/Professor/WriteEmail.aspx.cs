using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Professor
{
    public partial class WebForm17 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {


            string subjects = txtsubject.Text;
            string messageText = txtMessage.Text;
            string recipientEmail = emailtxt.Text;
            
            if (string.IsNullOrEmpty(subjects) || string.IsNullOrEmpty(messageText))
            {
                ShowErrorMessage("Subject and message and email are required.");
                return;
            }

            try
            {
                string loggedInUserEmail = Session["LoggedInUserEmail"] as string;

                if (string.IsNullOrEmpty(loggedInUserEmail))
                {
                    ShowErrorMessage("Unable to retrieve logged-in user's email.");
                    return;
                }

                string smtpConnectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                using (MySqlConnection smtpConnection = new MySqlConnection(smtpConnectionString))
                {
                    smtpConnection.Open();

                    string smtpQuery = "SELECT smtp_email, smtp_password FROM smtp_credentials WHERE smtp_email = @smtp_email";

                    using (MySqlCommand smtpCmd = new MySqlCommand(smtpQuery, smtpConnection))
                    {
                        smtpCmd.Parameters.AddWithValue("@smtp_email", loggedInUserEmail);

                        using (MySqlDataReader reader = smtpCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string smtpEmail = reader["smtp_email"].ToString();
                                string smtpPassword = reader["smtp_password"].ToString();

                                string smtpServer = "smtp.gmail.com";
                                int smtpPort = 587;

                                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                                {
                                    smtpClient.EnableSsl = true;
                                    smtpClient.Credentials = new NetworkCredential(smtpEmail, smtpPassword);

                                    string subject = subjects;
                                    string body = messageText;


                                    MailMessage mailMessage = new MailMessage(smtpEmail, recipientEmail, subject, body);
                                    mailMessage.IsBodyHtml = true;

                                    smtpClient.Send(mailMessage);

                                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                                    using (MySqlConnection con = new MySqlConnection(connectionString))
                                    {
                                        con.Open();

                                        string insertQuery = "INSERT INTO lmsnotification (sender, receiver, subject, message, date) VALUES (@sender, @Receiver, @Subject, @Message, @Date)";

                                        using (MySqlCommand cmd = new MySqlCommand(insertQuery, con))
                                        {
                                            cmd.Parameters.AddWithValue("@sender", smtpEmail);

                                            cmd.Parameters.AddWithValue("@Receiver", recipientEmail);
                                            cmd.Parameters.AddWithValue("@Subject", subject);
                                            cmd.Parameters.AddWithValue("@Message", messageText);
                                            cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                                            cmd.ExecuteNonQuery();
                                        }
                                    }


                                    ShowSuccessMessage("Your email was sent");
                                    txtsubject.Text = "";
                                    txtMessage.Text = "";
                                    emailtxt.Text = "";
                                }
                            }
                            else
                            {
                                ShowErrorMessage("SMTP credentials not found for the logged-in user's email.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("To send email you need to put SMTP password that will found in your my account page");
            }
        }

        
    }
}