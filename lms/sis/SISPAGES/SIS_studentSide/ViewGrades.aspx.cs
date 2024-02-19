using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using static System.Collections.Specialized.BitVector32;

namespace lms.sis.SISPAGES
{
    public partial class ViewGrades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayNewData();
                string Name = Session["Name"] as string;
                string id = Session["ID"] as string;

                int currentYear = DateTime.Now.Year;
                int numberOfYears = 1;
                sy.Text = ($"{currentYear}-{currentYear + numberOfYears}");


                ConnectionClass conn = new ConnectionClass();
                using (MySqlConnection connection = conn.GetConnection())
                {
                    connection.Open();
                    string selectquery = "SELECT * FROM `academicinformation` WHERE studentId = @id";
                    try
                    {
                        using (MySqlCommand command = new MySqlCommand(selectquery, connection))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                string year = reader.GetString("yearlevel");
                                Strand.Text = reader.GetString("strand");
                                Admission_Status.Text = reader.GetString("admissionStatus");
                                Sem.Text = reader.GetString("currentSemester");
                                reader.Close();

                                string gridquery = $"SELECT * FROM `studentgrade` WHERE studentID = {id} And year = {year}";

                                using (MySqlCommand command2 = new MySqlCommand(gridquery, connection))
                                {

                                    MySqlDataAdapter adapter = new MySqlDataAdapter(command2);
                                    DataTable dataTable = new DataTable();
                                    adapter.Fill(dataTable);
                                    grid.DataSource = dataTable;
                                    grid.DataBind();
                                }
                            }
                        }
                    }catch {
                        Response.Write("no data aquired");
                    }
                }

            }
        }

        protected void btnviewgrades_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Name = Session["Name"] as string;
            string id = Session["ID"] as string;

            int currentYear = DateTime.Now.Year;
            int numberOfYears = 1;
            sy.Text = ($"{currentYear}-{currentYear + numberOfYears}");


            ConnectionClass conn = new ConnectionClass();
            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();
                string selectquery = "SELECT * FROM `academicinformation` WHERE studentId = @id";
                try
                {
                    using (MySqlCommand command = new MySqlCommand(selectquery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            string year = btnviewgrades.SelectedValue;
                            Strand.Text = reader.GetString("strand");
                            Admission_Status.Text = reader.GetString("admissionStatus");
                            Sem.Text = reader.GetString("currentSemester");
                            reader.Close();

                            string gridquery = $"SELECT * FROM `studentgrade` WHERE studentID = {id} And year = {year}";
                            using (MySqlCommand command2 = new MySqlCommand(gridquery, connection))
                            {

                                MySqlDataAdapter adapter = new MySqlDataAdapter(command2);
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);
                                grid.DataSource = dataTable;
                                grid.DataBind();
                            }
                        }
                    }
                }
                catch
                {
                    Response.Write("no data aquired");
                }
            }
        }

        public class DataHelper
        {
            public DataTable GetNewData()
            {

                DataTable dataTable = new DataTable();
                ConnectionClass conn = new ConnectionClass();


                using (MySqlConnection connection = conn.GetConnection())
                {
                    connection.Open();

                    // Assuming "id" is the auto-incremented column
                    string query = "SELECT * FROM announcement ORDER BY AnnouncementID DESC LIMIT 1";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                return dataTable;
            }
        }

        protected void DisplayNewData()
        {
            DataHelper dataHelper = new DataHelper();
            DataTable newData = dataHelper.GetNewData();

            if (newData.Rows.Count > 0)
            {
                DataRow row = newData.Rows[0];
                string dataValue = row["Post"].ToString();

                lbl1.Text = dataValue;
                lbl1.Text = "ANNOUNCEMENT : " + lbl1.Text;
            }
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Account/Logout.aspx");
        }

    }
}



