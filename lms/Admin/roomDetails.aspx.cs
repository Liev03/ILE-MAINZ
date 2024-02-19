using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace lms.Admin
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["teacheremail"] != null)
                {
                    string Teacheremail = Request.QueryString["teacheremail"];

                    try
                    {
                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                        using (MySqlConnection con = new MySqlConnection(connectionString))
                        {
                            con.Open();

                            string query = "SELECT roomid, teacheremail FROM rooms WHERE teacheremail = @teacheremail";

                            using (MySqlCommand command = new MySqlCommand(query, con))
                            {
                                command.Parameters.AddWithValue("@teacheremail", Teacheremail);

                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())

                                        Session["TeacherEmail"] = Teacheremail;

                                    {
                                        string teacherEmail = Request.QueryString["teacheremail"];
                                        Label2.Text = teacherEmail;
                                        BindSubjectData(teacherEmail);
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

        private void BindSubjectData(string teacherEmail)

        {
            string Teacheremail = Session["TeacherEmail"] as string;

            if (!string.IsNullOrEmpty(Teacheremail))
            {

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT subjectname, roomid FROM rooms WHERE teacheremail = @teacheremail";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@teacheremail", teacherEmail);

                        connection.Open();

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable subjectsDataTable = new DataTable();
                            adapter.Fill(subjectsDataTable);

                            roomdetailsGridView.DataSource = subjectsDataTable;
                            roomdetailsGridView.DataBind();
                        }
                    }
                }
            }

        }
    }
}