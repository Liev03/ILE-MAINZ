using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.LearningHub
{
    public partial class TutorNotif : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    string uid = Session["UID"].ToString();

                    BindRepeater(uid);
                }
            }

        }
        protected void BindRepeater(string uid)
        {
            string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT n.nid AS NotificationID, " +
                               "CASE WHEN b.role = 'Tutee' THEN u.name ELSE b.name END AS TutorName, " +
                               "CASE WHEN b.role = 'Tutor' THEN u.name ELSE b.name END AS TuteeName, " +
                               "b.strand AS Strand, " +
                               "b.yearlevel AS YearLevel, " +
                               "b.availability AS Availability, " +
                               "b.location AS Location " +
                               "FROM notification n " +
                               "INNER JOIN bulletin b ON n.Frid = b.rid " +
                               "INNER JOIN student_info u ON n.Fuid = u.uid " +
                               "WHERE b.uid = @UID AND b.role = 'Tutor'";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UID", uid);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        transactionRepeater.DataSource = dt;
                        transactionRepeater.DataBind();
                    }
                }
            }
        }
        protected void ViewMore_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string notificationID = button.CommandArgument;
        }
        protected void AcceptButton_Click(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Accept")
            {
                string notificationID = e.CommandArgument.ToString();

                int frid = 0;
                string fridQueryString = "SELECT Frid FROM notification WHERE nid = @NotificationID";

                string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(fridQueryString, connection))
                    {
                        cmd.Parameters.AddWithValue("@NotificationID", notificationID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                frid = reader.GetInt32("Frid");
                            }
                        }
                    }
                }
                InsertTransaction(notificationID);

                UpdateBulletinVisibility(frid, "Nada");

                RemoveNotification(notificationID);

                BindRepeater(Session["UID"].ToString());
            }
        }
        private void RemoveNotification(string notificationID)
        {
            string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string removeQuery = "DELETE FROM notification WHERE nid = @NotificationID";
                using (MySqlCommand removeCmd = new MySqlCommand(removeQuery, connection))
                {
                    removeCmd.Parameters.AddWithValue("@NotificationID", notificationID);
                    removeCmd.ExecuteNonQuery();
                }
            }
        }
        private void InsertTransaction(string notificationID)
        {
            string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string fetchDetailsQuery = "SELECT Frid, Fuid FROM notification WHERE nid = @NotificationID";

                using (MySqlCommand fetchCmd = new MySqlCommand(fetchDetailsQuery, connection))
                {
                    fetchCmd.Parameters.AddWithValue("@NotificationID", notificationID);

                    using (MySqlDataReader reader = fetchCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int requestor = reader.GetInt32("Frid");
                            int client = reader.GetInt32("Fuid");


                            int tutor = Convert.ToInt32(Session["UID"]);

                            reader.Close();

                            string insertQuery = "INSERT INTO transaction (requestor, client, tutor, days, trandate, progress) VALUES (@Requestor, @Client, @Tutor, @Days, @TranDate, @Progress)";
                            using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                            {
                                insertCmd.Parameters.AddWithValue("@Requestor", requestor);
                                insertCmd.Parameters.AddWithValue("@Client", client);
                                insertCmd.Parameters.AddWithValue("@Tutor", tutor);
                                insertCmd.Parameters.AddWithValue("@Days", 0);
                                insertCmd.Parameters.AddWithValue("@TranDate", DateTime.Now);
                                insertCmd.Parameters.AddWithValue("@Progress", "Ongoing");
                                insertCmd.ExecuteNonQuery();
                            }

                            string getLastInsertedIdQuery = "SELECT LAST_INSERT_ID()";
                            using (MySqlCommand getLastInsertedIdCmd = new MySqlCommand(getLastInsertedIdQuery, connection))
                            {
                                int lastInsertedId = Convert.ToInt32(getLastInsertedIdCmd.ExecuteScalar());


                                string insertLearningQuery = "INSERT INTO learning (tid) VALUES (@TID)";
                                using (MySqlCommand insertLearningCmd = new MySqlCommand(insertLearningQuery, connection))
                                {
                                    insertLearningCmd.Parameters.AddWithValue("@TID", lastInsertedId);
                                    insertLearningCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
        }
        private void UpdateBulletinVisibility(int rid, string visibilityValue)
        {
            string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE bulletin SET visibility = @Visibility WHERE rid = @RID";

                using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                {
                    updateCmd.Parameters.AddWithValue("@Visibility", visibilityValue);
                    updateCmd.Parameters.AddWithValue("@RID", rid);

                    updateCmd.ExecuteNonQuery();
                }
            }
        }
        protected void RejectButton_Click(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Reject")
            {
                string notificationID = e.CommandArgument.ToString();
                RemoveNotification(notificationID);
                BindRepeater(Session["UID"].ToString());
            }
        }

        protected void transactionRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}