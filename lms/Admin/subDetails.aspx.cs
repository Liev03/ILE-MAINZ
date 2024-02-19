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
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["roomid"] != null)
                {
                    int roomID;
                    if (int.TryParse(Request.QueryString["roomid"], out roomID))
                    {
                        try
                        {
                            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                            using (MySqlConnection con = new MySqlConnection(connectionString))
                            {
                                con.Open();

                                string query = "SELECT roomid, description, teachername FROM rooms WHERE roomid = @roomid";

                                using (MySqlCommand command = new MySqlCommand(query, con))
                                {
                                    command.Parameters.AddWithValue("@roomid", roomID);

                                    using (MySqlDataReader reader = command.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            string description = reader["description"].ToString();
                                            lbldetails.Text = description;

                                            string name = reader["teachername"].ToString();
                                            lblowner.Text = name;
                                        }
                                    }


                                    string queryStudents = "SELECT studentemail FROM invitation WHERE roomid = @roomid AND status = 'Accepted'";
                                    using (MySqlCommand commandStudents = new MySqlCommand(queryStudents, con))
                                    {
                                        commandStudents.Parameters.AddWithValue("@roomid", roomID);

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
                        }
                        catch (Exception ex)
                        {

                        }

                    }




                    else
                    {

                        lbldetails.Text = "Invalid roomid provided.";
                    }
                }
            } 
        }
     
        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MultiView1.ActiveViewIndex = index;
        }

      
    }
}