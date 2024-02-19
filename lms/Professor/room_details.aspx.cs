using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;


namespace lms.Professor
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string teacherEmail = Session["LoggedInUserEmail"] as string;
                if (string.IsNullOrEmpty(teacherEmail))
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    try
                    {
                        string teacherFullName = GetTeacherFullNameFromDatabase(teacherEmail);
                        if (!string.IsNullOrEmpty(teacherFullName))
                        {
                            teachername.Text = teacherFullName;
                            teachername.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        //lblMessage.Text = "An error occurred while processing your request. Please try again later.";
                    }
                }
            }
        }

        private string GetTeacherFullNameFromDatabase(string professorEmail)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            string fullName = "";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT firstname, lastname FROM teacher_info WHERE email = @email";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@email", professorEmail);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader["firstname"].ToString();
                            string lastName = reader["lastname"].ToString();
                            fullName = $"{firstName} {lastName}";
                        }
                    }
                }
            }

            return fullName;
        }
        private int GetTeacherIdFromDatabase(string professorEmail)
        {
            int teacherId = -1;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT teacherid FROM teacher_info WHERE email = @email";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@email", professorEmail);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            teacherId = Convert.ToInt32(reader["teacherid"]);
                        }
                    }
                }
            }

            return teacherId;
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {

            if (e.Item.Value == "1")
            {
                if (!AreTextboxesPopulated())
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                                "Swal.fire({icon: 'error',text: 'Please fill out all required fields.'})", true);

                    Menu1.Items[0].Selected = true;
                    return;
                }
            }


            int index = Int32.Parse(e.Item.Value);
            MultiView1.ActiveViewIndex = index;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateRoom.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

       
        private bool AreTextboxesPopulated()
        {
            if (string.IsNullOrEmpty(teachername.Text) || ddlStrand.SelectedIndex == 0 || ddlSubject.SelectedIndex == 0 || (!g11.Checked && !g12.Checked))
            {
                return false;
            }
            return true;
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string roombanner = "";
            string professorEmail = Session["LoggedInUserEmail"] as string;
            int teacherId = GetTeacherIdFromDatabase(professorEmail);

            if (teacherId == -1)
            {
                return;
            }
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO rooms (teacherid, teachername, teacheremail, gradeyear, subjectname, strand, section, schedule, description, roombanner, status) " +
                                              "VALUES (@teacherid, @teachername, @teacheremail, @gradeyear, @subjectname, @strand, @section, @schedule, @description, @roombanner, 'Active')";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@teacherid", teacherId);
                        cmd.Parameters.AddWithValue("@teachername", teachername.Text);
                        cmd.Parameters.AddWithValue("@teacheremail", professorEmail);
                        cmd.Parameters.AddWithValue("@gradeyear", GetSelectedGradeYear());
                        cmd.Parameters.AddWithValue("@subjectname", ddlSubject.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@strand", ddlStrand.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@section", txtsection.Text);
                        cmd.Parameters.AddWithValue("@schedule", schedule.Text);
                        cmd.Parameters.AddWithValue("@description", txtdescription.Text);

                        if (roomimage.HasFile)
                        {
                            byte[] imageData = roomimage.FileBytes;
                            cmd.Parameters.Add(new MySqlParameter("@roombanner", imageData));

                            ImagePreview.Visible = true;
                            ImagePreview.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageData);
                        }
                        else
                        {
                            string defaultImagePath = Server.MapPath("~/Resources/subjectcovernhs.jpg");
                            byte[] defaultImageData = File.ReadAllBytes(defaultImagePath);
                            cmd.Parameters.AddWithValue("@roombanner", defaultImageData);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }

                ShowSuccessMessage("Room successfully created!");

                teachername.Text = "";
                txtsection.Text = "";
                schedule.Text = "";
                txtdescription.Text = "";
                ddlStrand.SelectedIndex = 0; 
                ddlSubject.SelectedIndex = 0;

                ClientScript.RegisterStartupScript(this.GetType(), "successMessage", "showSuccessMessage();", true);

            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred while processing your request. Please try again later.");
            }
        }

        private string GetSelectedGradeYear()
        {
            if (g11.Checked)
            {
                return "Grade 11";
            }
            else if (g12.Checked)
            {
                return "Grade 12";
            }

            return ""; 
        }

        protected void GradeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStrandDropdown();

        }

        protected void StrandDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSubjectDropdown();


        }
        private void UpdateStrandDropdown()
        {
            string selectedGrade = g11.Checked ? "11" : g12.Checked ? "12" : "";
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT strand FROM subjects WHERE gradeyear = @Grade", con);
                cmd.Parameters.AddWithValue("@Grade", selectedGrade);
                MySqlDataReader reader = cmd.ExecuteReader();

                ddlStrand.Items.Clear();
                ddlStrand.Items.Add(new ListItem("Select Strand", "")); 

                while (reader.Read())
                {
                    ddlStrand.Items.Add(new ListItem(reader["strand"].ToString(), reader["strand"].ToString()));
                }
                reader.Close();
            }

            UpdateSubjectDropdown();
        }

        private void UpdateSubjectDropdown()
        {
            string selectedGrade = g11.Checked ? "11" : g12.Checked ? "12" : "";
            string selectedStrand = ddlStrand.SelectedValue;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT subjectname FROM subjects WHERE strand = @Strand AND gradeyear = @GradeYear", con);
                cmd.Parameters.AddWithValue("@Strand", selectedStrand);
                cmd.Parameters.AddWithValue("@GradeYear", selectedGrade); 

                MySqlDataReader reader = cmd.ExecuteReader();

                ddlSubject.Items.Clear();
                ddlSubject.Items.Add(new ListItem("Select Subject", "")); 

                while (reader.Read())
                {
                    ddlSubject.Items.Add(new ListItem(reader["subjectname"].ToString(), reader["subjectname"].ToString()));
                }
                reader.Close();
            }
        }
        private void ShowErrorMessage(string message)
        {
            string script = $"Swal.fire({{ icon: 'error', text: '{message}' }})";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script, true);
        }
        private void ShowSuccessMessage(string message)
        {
            string script = $"Swal.fire({{ icon: 'success', text: '{message}' }})";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script, true);
        }
    }
}