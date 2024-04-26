using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Professor
{
    public partial class StudentInvite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (Request.QueryString["roomid"] != null)
                {
                    string roomid = Request.QueryString["roomid"];
                    try
                    {
                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                        using (MySqlConnection con = new MySqlConnection(connectionString)) 
                        {
                            con.Open();
                            string query = "SELECT subjectname FROM rooms WHERE roomid = @roomid";

                            using (MySqlCommand command = new MySqlCommand(query, con))
                            {
                                command.Parameters.AddWithValue("@roomid", roomid);

                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string subjectname = reader["subjectname"].ToString();
                                        Label1.Text = subjectname;
                                        BindStudentData(roomid);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
        protected void BindStudentData(string roomid, string searchTerm = "")
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            string loggedinTeacherEmail = Session["LoggedInUserEmail"] as string;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT s.studentid, s.email AS StudentEmail, t.teacherid, t.email AS TeacherEmail, r.subjectname, r.roomid " +
                               "FROM student_Info s " +
                               "INNER JOIN teacher_info t " +
                               "INNER JOIN rooms r " +
                               "WHERE t.email = @TeacherEmail AND r.roomid = @roomidParam";


                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query += " AND (s.email LIKE @searchTerm)";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@TeacherEmail", loggedinTeacherEmail);
                    cmd.Parameters.AddWithValue("@roomidParam", roomid);

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    }

                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        roomlist.DataSource = dt;
                        roomlist.DataBind();
                    }
                }
            }
        }
        //protected void TextBox1_TextChanged(object sender, EventArgs e)
        //{
        //    string searchTerm = TextBox1.Text;
        //    BindStudentData(searchTerm);
        //}
        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                GridViewRow gvr = (GridViewRow)btn.NamingContainer;
                DropDownList ddl = (DropDownList)gvr.FindControl("ddlProcess");
                HiddenField hf = (HiddenField)gvr.FindControl("hfTnIdPkId");
                int tnidpk = int.Parse(hf.Value);
                string status = ddl.SelectedValue;


                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                GridViewRow selectedRow = (GridViewRow)((Button)sender).NamingContainer;

                string roomid = selectedRow.Cells[0].Text;
                string studentid = selectedRow.Cells[1].Text;
                string studentemail = selectedRow.Cells[2].Text;
                string teacherid = selectedRow.Cells[3].Text;
                string teacheremail = selectedRow.Cells[4].Text;
                string subjectname = selectedRow.Cells[5].Text;

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM invitation WHERE roomid = @roomid AND studentid = @studentid AND teacherid = @teacherid AND subjectname = @subjectname AND (status = 'Pending' OR status = 'Accepted')";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, con))

                    {
                        checkCmd.Parameters.AddWithValue("@roomid", roomid);

                        checkCmd.Parameters.AddWithValue("@studentid", studentid);
                        checkCmd.Parameters.AddWithValue("@teacherid", teacherid);
                        checkCmd.Parameters.AddWithValue("@subjectname", subjectname);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count == 0)
                        {
                            

                            string query = "INSERT INTO invitation (roomid, studentid, teacherid, teacheremail, studentemail, subjectname, status) " +
                                                "VALUES (@roomid, @studentid, @teacherid, @teacheremail, @studentemail, @subjectname, 'Pending') ";
                            using (MySqlCommand cmd = new MySqlCommand(query, con))
                            {
                                cmd.Parameters.AddWithValue("@roomid", roomid);

                                cmd.Parameters.AddWithValue("@studentid", studentid);
                                cmd.Parameters.AddWithValue("@teacherid", teacherid);
                                cmd.Parameters.AddWithValue("@teacheremail", teacheremail);
                                cmd.Parameters.AddWithValue("@studentemail", studentemail);
                                cmd.Parameters.AddWithValue("@subjectname", subjectname);

                                cmd.ExecuteNonQuery();
                            }

                            ShowSuccessMessage("Student Invited Successfully");
                        }
                        else

                        {
                            ShowErrorMessage("The Student has already Invited");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error: {ex.Message}");
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

    }
}
