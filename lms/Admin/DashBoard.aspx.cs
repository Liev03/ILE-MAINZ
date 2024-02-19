using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Admin
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected int GetTotalStudentCount()
        {
            int studentCount = 0; try
            {


                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM lmsusers WHERE usertype = 'student'";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        studentCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                studentCount = 0;
            }
            return studentCount;
        }
        protected int GetTotalTeacherCount()
        {
            int professorCount = 0; try
            {


                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM lmsusers WHERE usertype = 'teacher'";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        professorCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                professorCount = 0;
            }
            return professorCount;
        }
        protected int GetTotalRoomsCount()
        {
            int RoomsCount = 0; try
            {


                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM rooms";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        RoomsCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                RoomsCount = 0;
            }
            return RoomsCount;
        }
        protected int GetTotalNotificationCount()
        {
            int notificationCount = 0; try
            {
                string loggedInUserEmail = Session["LoggedInUserEmail"] as string;


                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM lmsnotification WHERE DATE(date) = CURDATE() AND receiver = @loggedInUserEmail";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@loggedInUserEmail", loggedInUserEmail);


                        notificationCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                notificationCount = 0;
            }
            return notificationCount;
        }
    }
}