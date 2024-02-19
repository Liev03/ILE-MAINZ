using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Student
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {


                    BindRoomData();
                    PopulateSubjectsDropDown();


                }
                catch (Exception ex)
                {
                    ShowErrorMessage("An error occurred while processing your request. Please try again later.");
                }

            }
        }
        protected void BindRoomData(string subjectFilter = "")
        {
            string studentemail = Session["LoggedInUserEmail"] as string;

            if (string.IsNullOrEmpty(studentemail))
            {
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT roomid, invitation_subjectname, description, schedule, section, roomstatus FROM invitation_and_rooms_view WHERE studentemail = @studentemail AND status = 'Accepted' AND roomstatus = 'Active'";


                if (!string.IsNullOrEmpty(subjectFilter))
                {
                    query += " AND invitation_subjectname = @subjectFilter";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@studentemail", studentemail);

                    if (!string.IsNullOrEmpty(subjectFilter))
                    {
                        cmd.Parameters.AddWithValue("@subjectFilter", subjectFilter);
                    }


                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        roomRepeater.DataSource = dt;
                        roomRepeater.DataBind();
                    }
                }
            }
        }
        private void PopulateSubjectsDropDown()
        {
            string studentemail = Session["LoggedInUserEmail"] as string;

            if (string.IsNullOrEmpty(studentemail))
            {
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT DISTINCT invitation_subjectname FROM invitation_and_rooms_view WHERE studentemail = @studentemail AND status = 'Accepted' AND roomstatus = 'Active'";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@studentemail", studentemail);

                    using (var reader = cmd.ExecuteReader())
                    {
                        HashSet<string> uniqueSubjectNames = new HashSet<string>();

                        while (reader.Read())
                        {
                            string subjectName = reader["invitation_subjectname"].ToString();
                            uniqueSubjectNames.Add(subjectName);
                        }

                        var sortedSubjects = uniqueSubjectNames.OrderBy(subject => subject);

                        foreach (string subjectName in sortedSubjects)
                        {
                            DropDownList1.Items.Add(new ListItem(subjectName, subjectName));
                        }
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

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = DropDownList1.SelectedValue;

            if (selectedValue == "1")
            {
                Response.Redirect("classSubjects.aspx");
            }
            BindRoomData(DropDownList1.SelectedValue);
        }
    }
}
