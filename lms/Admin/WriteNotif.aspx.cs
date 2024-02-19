using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Xml;
using System.Net;
using System.Net.Mail;
using Org.BouncyCastle.Crypto.Macs;


namespace lms.Admin
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["emails"]))
                {
                    

                    string[] studentEmails = Request.QueryString["emails"].Split(',');
                    string displayEmailss = string.Join(", ", studentEmails);
                    emailtxt.Text = displayEmailss;
                }               
                else if (Request.QueryString["studentid"] != null)
                {
                    int studentID = Convert.ToInt32(Request.QueryString["studentid"]);
                    try
                    {
                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                        using (MySqlConnection con = new MySqlConnection(connectionString))
                        {
                            con.Open();

                            string query = "SELECT studentid, firstname, Email FROM student_info WHERE studentid = @studentid";

                            using (MySqlCommand command = new MySqlCommand(query, con))
                            {
                                command.Parameters.AddWithValue("@studentid", studentID);

                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string studentEmail = reader["Email"].ToString();
                                        emailtxt.Text = studentEmail;
                                      
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("An error occurred while processing your request. Please try again later.");
                    }
                }
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

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {

            string recipientEmail = emailtxt.Text;
            string subject = txtsubject.Text;
            string messageText = txtMessage.Text;

            if (txtMessage.Text == "")
            {
                ErroSub2.Text = " * Please input a message";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "Swal.fire({icon: 'error',text: 'Something went wrong!'})", true);
            }
            else
            {
                try
                {
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                    smtpClient.Port = 587;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("novalichesseniorhighschoolnhs@gmail.com", "kymucdmewcovazly");
              
                    smtpClient.EnableSsl = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("novalichesseniorhighschoolnhs@gmail.com");
                    mailMessage.To.Add(recipientEmail);
                    mailMessage.Subject = subject;
                    mailMessage.Body = messageText;

                    smtpClient.Send(mailMessage);

                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();

                        string insertQuery = "INSERT INTO lmsnotification (sender, receiver, subject, message, date) VALUES (@sender, @Receiver, @Subject, @Message, @Date)";

                        using (MySqlCommand cmd = new MySqlCommand(insertQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@sender", "novalichesseniorhighschoolnhs@gmail.com");
                            cmd.Parameters.AddWithValue("@Receiver", recipientEmail);
                            cmd.Parameters.AddWithValue("@Subject", subject);
                            cmd.Parameters.AddWithValue("@Message", messageText);
                            cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                        "Swal.fire({icon: 'success',text: 'Email sent Successfully!'})", true);

                    txtsubject.Text = "";
                    txtMessage.Text = "";
                    ErroSub2.Text = "";
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                        "Swal.fire({icon: 'error',text: 'Something went wrong!'})", true);
                    txtsubject.Text = "";
                    txtMessage.Text = "";
                    ErroSub2.Text = "";
                }
            }
        }
    }
}