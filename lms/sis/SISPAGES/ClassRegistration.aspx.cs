using K4os.Compression.LZ4.Internal;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace lms.sis.SISPAGES
{
    public partial class ClassRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                sectionBind();
            }
        }
        protected void CRregis_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionClass conn = new ConnectionClass();
                long randomDigits = Convert.ToInt32(ID.Text);
                using (MySqlConnection connection = conn.GetConnection())
                {
                    connection.Open();

                    string checkExistingQuery = "SELECT COUNT(*) FROM academicinformation WHERE studentId = @studentId";

                    using (MySqlCommand checkCmd = new MySqlCommand(checkExistingQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@studentId", randomDigits);
                        int existingCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                        string query, message;
                        if (existingCount > 0)
                        {
                            query = "UPDATE academicinformation SET section = @value2,strand = @strand, admissionStatus = @value4, dateEnrolled = @value5, currentSemester = @Sem, yearlevel = @year WHERE studentId = @studentId";
                            message = "Success! Academic information updated successfully!";
                        }
                        else
                        {
                            query = "INSERT INTO academicinformation(studentId, section, strand, admissionStatus, dateEnrolled, currentSemester, yearlevel) VALUES (@studentId,@value2,@strand,@value4, @value5, @Sem, @year)";
                            message = "Success! Academic information added successfully!";
                        }
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@studentId", randomDigits);
                            cmd.Parameters.AddWithValue("@value2", dlsection.Text);
                            cmd.Parameters.AddWithValue("@strand", StandList.SelectedValue);
                            cmd.Parameters.AddWithValue("@value4", AS.Text);
                            cmd.Parameters.AddWithValue("@value5", DEnrolled.Text);
                            cmd.Parameters.AddWithValue("@Sem", Sem.SelectedValue);
                            cmd.Parameters.AddWithValue("@year", year.SelectedValue);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            string alertType = rowsAffected > 0 ? "success" : "error";
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('{(rowsAffected > 0 ? "Success!" : "Error!")}', '{message}', '{alertType}');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Error!', '{ex.Message}', 'error');", true);
            }
        }
        protected void ID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionClass conn = new ConnectionClass();
                long randomDigits = Convert.ToInt64(txtScMsearch.Text);
                using (MySqlConnection connection = conn.GetConnection())
                {
                    connection.Open();
                    string nameRetriever = $"SELECT  `name` FROM `student_info` WHERE studentId = {randomDigits}";
                    using (MySqlCommand command = new MySqlCommand(nameRetriever, connection))
                    {
                        MySqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                string fullname = reader.GetString("name");
                                CRname.Text = fullname;
                                ID.Text = randomDigits.ToString();
                                reader.Close();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Error!', 'Student not found.', 'error');", true);
                        }
                        }        
                        string strandQuery = "SELECT `StrandName` FROM `strandmanagement`";

                        using (MySqlCommand command = new MySqlCommand(strandQuery, connection))
                        {
                            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            StandList.DataTextField = "StrandName";
                            StandList.DataValueField = "StrandName";
                            StandList.DataSource = dataTable;
                            StandList.DataBind();
                        }
                    string acadQuery = $"SELECT  * FROM `academicinformation` WHERE studentId = {randomDigits}";
                    using (MySqlCommand command = new MySqlCommand(acadQuery, connection))
                    {
                        MySqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                dlsection.Text = reader.GetString("section");
                                Sem.Text = reader.GetString("currentSemester");;
                                year.Text = reader.GetString("yearlevel");
                                DEnrolled.Text = reader.GetString("dateEnrolled");
                                AS.Text = reader.GetString("admissionStatus");
                                StandList.Text = reader.GetString("strand");
                                reader.Close();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Error!', 'Something went wrong', 'error');", true);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Error!', '{ex.Message}', 'error');", true);
            }
        }

        protected void year_SelectedIndexChanged(object sender, EventArgs e)
        {
            sectionBind();
        }
        void sectionBind() { 
             ConnectionClass conn = new ConnectionClass();
            using (MySqlConnection connection = conn.GetConnection())
            {
                string sectionQuery = "SELECT `Section` FROM `sectionmanagement` WHERE Section LIKE @Year";

                using (MySqlCommand command = new MySqlCommand(sectionQuery, connection))
                {
                    command.Parameters.AddWithValue("@Year", "%" + year.SelectedValue + "%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dlsection.DataTextField = "Section";
                    dlsection.DataValueField = "Section";
                    dlsection.DataSource = dataTable;
                    dlsection.DataBind();
                }
            }
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Account/Logout.aspx");
        }

        protected void CRcancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClassRegistration.aspx");
        }
    }
}