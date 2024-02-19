using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Student
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (!string.IsNullOrEmpty(Request.QueryString["roomid"]))
                {

                    if (int.TryParse(Request.QueryString["roomid"], out int roomId))

                    {
                        try
                        {
                            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                            using (MySqlConnection con = new MySqlConnection(connectionString))
                            {
                                con.Open();
                                string queryRooms = "SELECT * FROM rooms WHERE roomid = @roomid";

                                using (MySqlCommand commandRooms = new MySqlCommand(queryRooms, con))
                                {
                                    commandRooms.Parameters.AddWithValue("@roomid", roomId);

                                    using (MySqlDataReader readerRooms = commandRooms.ExecuteReader())
                                    {
                                        if (readerRooms.Read())
                                        {

                                            lblinstructormain.Text = readerRooms["teacheremail"].ToString();
                                        }
                                    }
                                }
                                string queryStudents = "SELECT studentemail FROM invitation WHERE roomid = @roomid AND status = 'Accepted'";
                                using (MySqlCommand commandStudents = new MySqlCommand(queryStudents, con))
                                {
                                    commandStudents.Parameters.AddWithValue("@roomid", roomId);

                                    DataTable dt = new DataTable();
                                    using (MySqlDataAdapter da = new MySqlDataAdapter(commandStudents))
                                    {
                                        da.Fill(dt);
                                    }

                                    if (dt.Rows.Count > 0)
                                    {
                                        studentlist.DataSource = dt;
                                        studentlist.DataBind();
                                    }
                                    else
                                    {
                                        studentlist.EmptyDataText = "No Students Found for this Room";
                                        studentlist.DataSource = null;
                                        studentlist.DataBind();
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
        }
    }
}