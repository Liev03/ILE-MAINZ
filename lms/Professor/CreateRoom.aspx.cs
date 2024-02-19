using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace lms.Professor
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
            string teacheremail = Session["LoggedInUserEmail"] as string;

            if (string.IsNullOrEmpty(teacheremail))
            {
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT roomid, subjectname, description, schedule, section FROM rooms WHERE teacheremail = @teacheremail  AND status = 'Active'";

                if (!string.IsNullOrEmpty(subjectFilter))
                {
                    query += " AND subjectname = @subjectFilter";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@teacheremail", teacheremail);

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
            string teacheremail = Session["LoggedInUserEmail"] as string;

            if (string.IsNullOrEmpty(teacheremail))
            {
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT DISTINCT subjectname FROM rooms WHERE teacheremail = @teacheremail AND status = 'Active'";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@teacheremail", teacheremail);

                    using (var reader = cmd.ExecuteReader())
                    {
                        HashSet<string> uniqueSubjectNames = new HashSet<string>();

                        while (reader.Read())
                        {
                            string subjectName = reader["subjectname"].ToString();
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
                Response.Redirect("CreateRoom.aspx");
            }
            BindRoomData(DropDownList1.SelectedValue);
        }
    }
}
