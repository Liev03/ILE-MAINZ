using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Professor
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {


                    BindRoomData();
                  

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

                string query = "SELECT roomid, subjectname, description, schedule, section FROM rooms WHERE teacheremail = @teacheremail AND status = 'Archived'";

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