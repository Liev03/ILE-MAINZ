using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Professor
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected int GetActiveRoomsCount()
        {
            int activeRoomsCount = 0;
            try
            {
                string loggedInUserEmail = Session["LoggedInUserEmail"] as string;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM rooms WHERE status = 'Active' AND teacheremail = @loggedInUserEmail";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@loggedInUserEmail", loggedInUserEmail);

                        activeRoomsCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                activeRoomsCount = 0;
            }
            return activeRoomsCount;
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