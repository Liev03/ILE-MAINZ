using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Admin
{
    public partial class WebForm7 : System.Web.UI.Page
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
                }
            }

        }

        private void BindRoomData(string searchTerm = "")
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                 
                    string query = "SELECT DISTINCT teachername, teacheremail, " +
                                   "(SELECT roomid  FROM rooms r WHERE r.teachername = rooms.teachername LIMIT 1) AS roomid " +
                                   "FROM rooms";

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += " WHERE teachername LIKE @searchTerm OR teacheremail LIKE @searchTerm";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            roomGridView.DataSource = dataTable;
                            roomGridView.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtsearch.Text;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query;

                if (string.IsNullOrEmpty(searchTerm))
                {
                    query = "SELECT DISTINCT teachername, teacheremail, " +
                                   "(SELECT roomid  FROM rooms r WHERE r.teachername = rooms.teachername LIMIT 1) AS roomid " +
                                   "FROM rooms";
                }
                else
                {
                    query = "SELECT teachername, teacheremail FROM rooms WHERE teachername LIKE @searchTerm OR teacheremail LIKE @searchTerm LIMIT 1;";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    }

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        roomGridView.DataSource = dataTable;
                        roomGridView.DataBind();
                    }
                }
            }
        }
    }
}