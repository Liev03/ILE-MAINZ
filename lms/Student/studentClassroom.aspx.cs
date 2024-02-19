using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace lms.Student
{
    public partial class studentClassroom : System.Web.UI.Page
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
                                            lblsubjectname.Text = readerRooms["subjectname"].ToString();
                                            lblschedule.Text = readerRooms["schedule"].ToString();
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
                DisplayAnnouncements();
            }

        }
        private void DisplayAnnouncements()
        {
            if (int.TryParse(Request.QueryString["roomid"], out int roomId))
            {
                try
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();

                        string query = "SELECT teacheremail, postcontent, date FROM announcements " +
                                       "WHERE roomid = @roomid " +
                                       "ORDER BY date DESC";

                        using (MySqlCommand command = new MySqlCommand(query, con))
                        {
                            command.Parameters.AddWithValue("@roomid", roomId);

                            DataTable dt = new DataTable();
                            using (MySqlDataAdapter da = new MySqlDataAdapter(command))
                            {
                                da.Fill(dt);
                            }

                            postGridView.DataSource = dt;
                            postGridView.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                }
            }
        }
        protected void Menu1_MenuItemClick1(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MultiView1.ActiveViewIndex = index;
        }
    }
}