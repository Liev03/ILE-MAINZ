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
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                teachername.Enabled = false;

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
                                            teachername.Text = readerRooms["teachername"].ToString();

                                            string gradeyear = readerRooms["gradeyear"].ToString();
                                            if (gradeyear == "Grade 11")
                                            {
                                                g11.Checked = true;
                                            }
                                            else if (gradeyear == "Grade 12")
                                            {
                                                g12.Checked = true;
                                            }

                                            UpdateStrandDropdown();

                                            ddlStrand.SelectedValue = readerRooms["strand"].ToString();
                                           
                                            UpdateSubjectDropdown();

                                            ddlSubject.SelectedValue = readerRooms["subjectname"].ToString();

                                            txtsection.Text = readerRooms["section"].ToString();
                                            schedule.Text = readerRooms["schedule"].ToString();
                                            txtdescription.Text = readerRooms["description"].ToString();



                                            byte[] imageBytes = readerRooms["roombanner"] as byte[];
                                            if (imageBytes != null && imageBytes.Length > 0)
                                            {
                                                string base64String = Convert.ToBase64String(imageBytes);
                                                ImagePreview.ImageUrl = "data:image/jpeg;base64," + base64String;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

        }

        //    private string GetTeacherFullNameFromDatabase(string professorEmail)
        //{
        //    string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        //    string fullName = "";
        //    using (MySqlConnection con = new MySqlConnection(connectionString))
        //    {
        //        con.Open();
        //        string query = "SELECT firstname, lastname FROM teacher_info WHERE email = @email";
        //        using (MySqlCommand cmd = new MySqlCommand(query, con))
        //        {
        //            cmd.Parameters.AddWithValue("@email", professorEmail);
        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    string firstName = reader["firstname"].ToString();
        //                    string lastName = reader["lastname"].ToString();
        //                    fullName = $"{firstName} {lastName}";
        //                }
        //            }
        //        }
        //    }

        //    return fullName;
        //}
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

        protected void btnupdate_Click(object sender, EventArgs e)
        {

            if (AreTextboxesPopulated())
            {
                try
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();

                        int roomId = int.Parse(Request.QueryString["roomid"]);

                        byte[] existingBanner = null;
                        using (MySqlCommand commandGetBanner = new MySqlCommand("SELECT roombanner FROM rooms WHERE roomid = @roomid", con))
                        {
                            commandGetBanner.Parameters.AddWithValue("@roomid", roomId);
                            existingBanner = commandGetBanner.ExecuteScalar() as byte[];
                        }

                        string updateQuery = @"UPDATE rooms SET teachername = @teachername, gradeyear = @gradeyear, strand = @strand, subjectname = @subjectname, section = @section, schedule = @schedule, description = @description, roombanner = @roombanner WHERE roomid = @roomid";

                        using (MySqlCommand commandUpdate = new MySqlCommand(updateQuery, con))
                        {
                            commandUpdate.Parameters.AddWithValue("@teachername", teachername.Text);
                            commandUpdate.Parameters.AddWithValue("@gradeyear", g11.Checked ? "Grade 11" : "Grade 12");
                            commandUpdate.Parameters.AddWithValue("@strand", ddlStrand.SelectedValue);
                            commandUpdate.Parameters.AddWithValue("@subjectname", ddlSubject.SelectedValue);
                            commandUpdate.Parameters.AddWithValue("@section", txtsection.Text);
                            commandUpdate.Parameters.AddWithValue("@schedule", schedule.Text);
                            commandUpdate.Parameters.AddWithValue("@description", txtdescription.Text);
                            commandUpdate.Parameters.AddWithValue("@roomid", roomId);

                            if (roomimage.HasFile)
                            {
                                using (Stream stream = roomimage.PostedFile.InputStream)
                                using (BinaryReader reader = new BinaryReader(stream))
                                {
                                    byte[] newBanner = reader.ReadBytes((int)stream.Length);
                                    commandUpdate.Parameters.AddWithValue("@roombanner", newBanner);
                                }
                            }
                            else
                            {
                                commandUpdate.Parameters.AddWithValue("@roombanner", existingBanner);
                            }

                            int rowsAffected = commandUpdate.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                ShowSuccessMessage("Room information updated successfully.");
                                ClientScript.RegisterStartupScript(this.GetType(), "successMessage", "showSuccessMessage();", true);

                            }
                            else
                            {
                                ShowErrorMessage("Failed to update room information.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("An error occurred while updating room information.");
                }
            }
            else
            {
                ShowErrorMessage("Please fill out all required fields.");
            }
        }
    }
}