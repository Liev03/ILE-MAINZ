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

namespace lms.Admin
{
    public partial class WebForm13 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["emails"]))
                {
                    string[] teacherEmails = Request.QueryString["emails"].Split(',');
                    string displayEmails = string.Join(", ", teacherEmails);

                    emailtxt.Text = displayEmails;
                }
                else if (Request.QueryString["teacherid"] != null)
                {
                    int teacherID = Convert.ToInt32(Request.QueryString["teacherid"]);
                    try
                    {
                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                        using (MySqlConnection con = new MySqlConnection(connectionString))
                        {
                            con.Open();

                            string query = "SELECT teacherid, firstname, Email FROM teacher_info WHERE teacherid = @teacherid";

                            using (MySqlCommand command = new MySqlCommand(query, con))
                            {
                                command.Parameters.AddWithValue("@teacherid", teacherID);

                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string email = reader["Email"].ToString();
                                        emailtxt.Text = email;
                                        emailtxt.Enabled = false;
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