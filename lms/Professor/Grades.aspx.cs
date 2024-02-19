using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Professor
{
    public partial class Grades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["roomid"]))
                {
                    if (int.TryParse(Request.QueryString["roomid"], out int roomId))
                    {


                        PopulateFileGridView2(roomId);



                    }
                }
            }
        }
        private void PopulateFileGridView2(int roomId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT studentworkid, materialsId, studentname, FileName, points FROM studentwork WHERE roomId = @roomId AND gradestatus = 'graded'";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@roomId", roomId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        gvgraded.DataSource = reader;
                        gvgraded.DataBind();
                    }
                }
            }
        }

        protected void gvgraded_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnprint_Click(object sender, EventArgs e)
        {

        }
    }
}