using lms.sis.SISPAGES;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Account
{
    public partial class StudentReg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayNewData();
            if (!IsPostBack)
            {
                string Name = Session["Name"] as string;
                string id = Session["ID"] as string;
                StudName.InnerText = Name;
                studID.InnerText = id;

                int currentYear = DateTime.Now.Year;
                int numberOfYears = 1;
                schoolYear.InnerText = ($"{currentYear}-{currentYear + numberOfYears}");


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
                                Strand.InnerText = reader.GetString("strand");
                                AdmisionStatus.InnerText = reader.GetString("admissionStatus");
                                year.InnerText = reader.GetString("yearlevel");
                                section.InnerText = reader.GetString("section");
                                currentSemester.InnerText = reader.GetString("currentSemester");
                                reader.Close();

                                current.Value = year.InnerText;
                                currentSem.Value = "1st Sem";
                                prevSem.Value = "2nd Sem";
                                string yearFilter;

                                if (year.InnerText.Trim() == "11")
                                {
                                    yearFilter = "grade11subjects";
                                }
                                else
                                {
                                    yearFilter = "grade12subjects";
                                    school_year.Items.Add(new ListItem($"{currentYear - 1}-{currentYear + numberOfYears - 1}", "11"));
                                }
                                string gridquery = $"SELECT `subjectCode`, `subjectTitle`, `description`, `subjectUnit`, `semester` FROM {yearFilter}" +
                                                   " WHERE subjectCode LIKE @strand AND semester LIKE @sem";
                                using (MySqlCommand command2 = new MySqlCommand(gridquery, connection))
                                {
                                    command2.Parameters.AddWithValue("@strand", "%" + Strand.InnerText + "%");
                                    command2.Parameters.AddWithValue("@sem", "%" + currentSemester.InnerText + "%");
                                    MySqlDataAdapter adapter = new MySqlDataAdapter(command2);
                                    DataTable dataTable = new DataTable();
                                    adapter.Fill(dataTable);
                                    grid.DataSource = dataTable;
                                    grid.DataBind();
                                    Regformview.DataSource = dataTable;
                                    Regformview.DataBind();
                                }
                            }
                            else
                            {
                                Response.Write("no data acquired");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Response.Write($"Error executing SQL query: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            if (!IsPostBack)
            {
                if (Session["LoggedInUser"] == null)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    string userType = Session["Role"] as string;

                    if (userType == "teacher")
                    {
                        Response.Redirect("~/Professor/DashBoard.aspx");
                    }
                    else if (userType == "admin")
                    {
                        Response.Redirect("~/Admin/DashBoard.aspx");
                    }
                    else
                    {
                        string userEmail = Session["LoggedInUser"] as string;
                        string UserID = Session["ID"] as string;

                        if (!string.IsNullOrEmpty(userEmail))
                        {
                            lblUserEmail.Text = userEmail;


                            if (!string.IsNullOrEmpty(UserID))
                            {
                                using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
                                {
                                    connection.Open();
                                    DisplayUploadedImage(connection, UserID);
                                    GetUserProfileImage(UserID);


                                }
                            }
                            else
                            {
                                // Handle the case where UserID is null or empty
                                // You may want to log an error or display a message.
                            }
                        }
                        else
                        {
                            // Handle the case where userEmail is null or empty
                            // You may want to log an error or display a message.
                        }
                    }
                }
            }
        }
        private byte[] GetUserProfileImage(string UserID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT ImageData FROM tblimages WHERE studentId = @studentID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@studentID", UserID);
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!(reader["ImageData"] is DBNull))
                            {
                                return (byte[])reader["ImageData"];
                            }
                        }
                    }
                }
            }

            return null;
        }
        private void DisplayUploadedImage(MySqlConnection connection, string userID)
        {
            string fetchImageQuery = "SELECT ImageData FROM tblimages WHERE studentId = @studentID";
            using (MySqlCommand fetchCmd = new MySqlCommand(fetchImageQuery, connection))
            {
                fetchCmd.Parameters.AddWithValue("@studentID", userID);
                byte[] imageData = fetchCmd.ExecuteScalar() as byte[];

                if (imageData != null && imageData.Length > 0)
                {
                    string base64String = Convert.ToBase64String(imageData);
                    Image1.ImageUrl = "data:image/jpeg;base64," + base64String;
                }
                else
                {
                    Image1.ImageUrl = "Resources/default.jpg";
                }
            }
        }
        protected void display_Click(object sender, EventArgs e)
        {
            ConnectionClass conn = new ConnectionClass();

            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();
                string selectedValue = school_year.SelectedValue;
                string selectedValue2 = sem.SelectedValue;

                if (string.IsNullOrEmpty(selectedValue))
                {
                    return;
                }
                int currentYear = DateTime.Now.Year;
                year.InnerText = selectedValue;
                schoolYear.InnerText = school_year.SelectedItem.Text;
                currentSemester.InnerText = sem.SelectedValue;

                string yearFilter = $"grade{year.InnerText.Replace("-", "")}subjects";

                try
                {
                    string gridquery = $"SELECT `subjectCode`, `subjectTitle`, `description`, `subjectUnit`, `semester` FROM {yearFilter}" +
                                        " WHERE subjectCode LIKE @strand AND semester LIKE @sem";

                    using (MySqlCommand command2 = new MySqlCommand(gridquery, connection))
                    {
                        command2.Parameters.AddWithValue("@strand", "%" + Strand.InnerText + "%");
                        command2.Parameters.AddWithValue("@sem", "%" + currentSemester.InnerText + "%");

                        MySqlDataAdapter adapter = new MySqlDataAdapter(command2);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        grid.DataSource = dataTable;
                        grid.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
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
    }
}