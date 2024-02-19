using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Professor
{
    public partial class WebForm16 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["roomid"]) && !string.IsNullOrEmpty(Request.QueryString["materialsid"]))
                {
                    if (int.TryParse(Request.QueryString["roomid"], out int roomId) && int.TryParse(Request.QueryString["materialsid"], out int materialsId))
                    {

                        DisplayMaterials(roomId, materialsId);
                        PopulateFileGridView(roomId, materialsId);
                        PopulateFileGridView1(roomId, materialsId);
                        PopulateStudent(roomId, materialsId);
                        //PopulateFileGridView2(roomId, materialsId);
                        DisplayUserProfileImage();
                        DisplayComment();
                        //PopulateFileDropdown(roomId, materialsId);
                        //ddlFiles.Enabled = false;


                    }
                }
            }
        }
        private void DisplayMaterials(int roomId, int materialsId)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT materialsid, materialsname, teacheremail, instructions, duedate, topic, posttype, points, dateposted, teachername FROM learningmaterials " +
                                   "WHERE roomid = @roomid AND materialsid = @materialsid";

                    using (MySqlCommand command = new MySqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@roomid", roomId);
                        command.Parameters.AddWithValue("@materialsid", materialsId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblteacheremail.Text = reader["teacheremail"].ToString();
                                string materialsName = reader["materialsname"].ToString();
                                lblpost.Text = $"{materialsName} - {reader["posttype"].ToString()}";
                                lbldateposted.Text = reader["dateposted"].ToString();
                                lblteacher.Text = reader["teachername"].ToString();
                                lblinstructions.Text = reader["instructions"].ToString();
                                //lbldue.Text = (reader["duedate"] is DBNull || reader["duedate"] == null)
                                //    ? string.Empty
                                //    : Convert.ToDateTime(reader["duedate"]).ToString("yyyy-MM-dd");
                                lbldue.Text = (reader["duedate"] is DBNull || reader["duedate"] == null)
                                ? "No Due"
                                : Convert.ToDateTime(reader["duedate"]).ToString("yyyy-MM-dd");
                                lblpoints.Text = reader["points"].ToString();

                             
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred while retrieving learningmaterials.");
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
        private void PopulateFileGridView(int roomId, int materialsId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT materialsId, FileName FROM learningmaterials WHERE roomId = @roomId AND materialsId = @materialsId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@roomId", roomId);
                    command.Parameters.AddWithValue("@materialsId", materialsId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        gvFiles.DataSource = reader;
                        gvFiles.DataBind();
                    }
                }
            }
        }

        private byte[] RetrieveFileData(int materialsId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT FileData FROM learningmaterials WHERE materialsId = @materialsId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@materialsId", materialsId);
                    return command.ExecuteScalar() as byte[];
                }
            }
        }
        //protected void gvFiles_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int rowIndex = gvFiles.SelectedIndex;
        //    GridViewRow row = gvFiles.Rows[rowIndex];

        //    int selectedFileID = Convert.ToInt32(row.Cells[0].Text);
        //    byte[] fileData = RetrieveFileData(selectedFileID);

        //    if (fileData != null)
        //    {
        //        Response.Clear();
        //        Response.ContentType = "application/octet-stream";
        //        Response.AddHeader("Content-Disposition", $"attachment; filename={row.Cells[1].Text}");
        //        Response.BinaryWrite(fileData);
        //        Response.End();
        //    }
        //}

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["RoomId"] != null && int.TryParse(Session["RoomId"].ToString(), out int roomId))
            {
                int studentId = Convert.ToInt32(Session["LoggedInUserID"]);
                string temail = Session["LoggedInUserEmail"].ToString();

                string commentpost = txtcomment.Text;
                // Check if txtcomment is not empty
                if (!string.IsNullOrWhiteSpace(commentpost))
                {
                    DateTime currentDate = DateTime.Now;
                    string teacheremail = lblteacheremail.Text;

                    if (int.TryParse(Request.QueryString["roomid"], out int roomIdFromQueryString) && int.TryParse(Request.QueryString["materialsid"], out int materialsid))
                    {
                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                        using (MySqlConnection con = new MySqlConnection(connectionString))
                        {
                            con.Open();

                            string retrieveStudentNameQuery = "SELECT firstname, lastname FROM teacher_Info WHERE email = @teacheremail";

                            using (MySqlCommand retrieveNameCommand = new MySqlCommand(retrieveStudentNameQuery, con))
                            {
                                retrieveNameCommand.Parameters.AddWithValue("@teacheremail", temail);

                                using (MySqlDataReader nameReader = retrieveNameCommand.ExecuteReader())
                                {
                                    if (nameReader.Read())
                                    {
                                        string teacherFirstName = nameReader["firstname"].ToString();
                                        string teacherLastName = nameReader["lastname"].ToString();
                                        string teacherFullName = $"{teacherFirstName} {teacherLastName}";

                                        nameReader.Close();

                                        string insertQuery = "INSERT INTO materialscomment (materialsid, roomid, teacheremail, name, profileimage, commentpost, datepost) " +
                                                             "VALUES (@materialsid, @roomid, @teacheremail, @name, @profileimage, @commentpost, @datepost)";

                                        using (MySqlCommand commandInsert = new MySqlCommand(insertQuery, con))
                                        {
                                            commandInsert.Parameters.AddWithValue("@materialsid", materialsid);
                                            commandInsert.Parameters.AddWithValue("@roomid", roomIdFromQueryString);
                                            commandInsert.Parameters.AddWithValue("@teacheremail", teacheremail);
                                            commandInsert.Parameters.AddWithValue("@name", teacherFullName);

                                            byte[] profileImage = GetUserProfileImage(temail);
                                            commandInsert.Parameters.AddWithValue("@profileimage", profileImage);

                                            commandInsert.Parameters.AddWithValue("@commentpost", commentpost);
                                            commandInsert.Parameters.AddWithValue("@datepost", currentDate);

                                            commandInsert.ExecuteNonQuery();

                                            txtcomment.Text = "";
                                            ShowSuccessMessage("Your Comment has been successfully posted");

                                        }
                                    }

                                }
                            }
                        }
                    }
                    DisplayComment();
                }
                else
                {
                    // Handle the case where txtcomment is empty
                    ShowErrorMessage("Please enter a comment before posting.");
                }
            }
        }
        private void DisplayUserProfileImage()
        {
            if (Session["LoggedInUserEmail"] != null)
            {
                try
                {
                    int teacherId = Convert.ToInt32(Session["LoggedInUserID"]);
                    string teacherEmail = Session["LoggedInUserEmail"].ToString();

                    byte[] profileImage = GetUserProfileImage(teacherEmail);

                    if (profileImage != null)
                    {
                        string base64String = Convert.ToBase64String(profileImage);
                        string imageUrl = $"data:image/png;base64,{base64String}";

                        Image1.ImageUrl = imageUrl;
                    }
                    else
                    {
                        Image1.ImageUrl = "path/to/default/image.png";
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("An error occurred while displaying the user profile image.");
                }
            }
            else
            {
                Response.Redirect("~Account/Login.aspx");
            }
        }

        private byte[] GetUserProfileImage(string userEmail)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT profileimage FROM lmsusers WHERE email = @userEmail";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userEmail", userEmail);
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!(reader["profileimage"] is DBNull))
                            {
                                return (byte[])reader["profileimage"];
                            }
                        }
                    }
                }
            }

            return null;
        }
        protected string GetProfileImageUrl(object profileImage)
        {
            if (profileImage != DBNull.Value)
            {
                byte[] bytes = (byte[])profileImage;
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                return "data:image/png;base64," + base64String;
            }
            else
            {
                return "path/to/default/image.jpg";
            }
        }

        private void DisplayComment()
        {
            if (Session["RoomId"] != null && int.TryParse(Session["RoomId"].ToString(), out int roomId))
            {
                if (int.TryParse(Request.QueryString["materialsid"], out int materialsid))
                {
                    try
                    {
                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                        using (MySqlConnection con = new MySqlConnection(connectionString))
                        {
                            con.Open();

                            //count
                            string countQuery = "SELECT COUNT(*) FROM materialscomment WHERE roomid = @roomid AND materialsid = @materialsid";
                            using (MySqlCommand countCommand = new MySqlCommand(countQuery, con))
                            {
                                countCommand.Parameters.AddWithValue("@roomid", roomId);
                                countCommand.Parameters.AddWithValue("@materialsid", materialsid);

                                int commentCount = Convert.ToInt32(countCommand.ExecuteScalar());
                                classCommentCountLabel.Text = commentCount.ToString();
                            }
                            //retrieve
                            string query = "SELECT teacheremail, studentemail, name, profileimage, commentpost, datepost " +
                                           "FROM materialscomment " +
                                           "WHERE roomid = @roomid AND materialsid = @materialsid " +
                                           "ORDER BY datepost DESC";

                            using (MySqlCommand command = new MySqlCommand(query, con))
                            {
                                command.Parameters.AddWithValue("@roomid", roomId);
                                command.Parameters.AddWithValue("@materialsid", materialsid);

                                DataTable dt = new DataTable();
                                using (MySqlDataAdapter da = new MySqlDataAdapter(command))
                                {
                                    da.Fill(dt);
                                }

                                commentGridView.DataSource = dt;
                                commentGridView.DataBind();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("An error occurred while retrieving comments.");
                    }
                }
            }
        }



        private void PopulateFileGridView1(int roomId, int materialsId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                //string query = "SELECT studentworkid, materialsId, studentname, FileName, points FROM studentwork WHERE roomId = @roomId AND materialsId = @materialsId";
                string query = "SELECT studentworkid, materialsId, studentname, FileName, points FROM studentwork WHERE roomId = @roomId AND materialsId = @materialsId AND (gradestatus IS NULL OR gradestatus = '')";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@roomId", roomId);
                    command.Parameters.AddWithValue("@materialsId", materialsId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        gvwork.DataSource = reader;
                        gvwork.DataBind();
                    }
                }
            }
        }

        private byte[] RetrieveFileData1(int materialsId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT FileData FROM studentwork WHERE materialsId = @materialsId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@materialsId", materialsId);
                    return command.ExecuteScalar() as byte[];
                }
            }
        }

        //protected void gvwork_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int rowIndex = gvwork.SelectedIndex;
        //    GridViewRow row = gvwork.Rows[rowIndex];

        //    int selectedFileID = Convert.ToInt32(row.Cells[0].Text);
        //    byte[] fileData = RetrieveFileData(selectedFileID);

        //    if (fileData != null)
        //    {
        //        Response.Clear();
        //        Response.ContentType = "application/octet-stream";
        //        Response.AddHeader("Content-Disposition", $"attachment; filename={row.Cells[1].Text}");
        //        Response.BinaryWrite(fileData);
        //        Response.End();
        //    }

        //}

        protected void gvpeople_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void PopulateStudent(int roomId, int materialsId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                //string query = "SELECT studentemail FROM invitation WHERE roomid = @roomid AND status = 'Accepted'";
                string query = "SELECT studentemail FROM StudentWorkAndInvitationView WHERE roomid = @roomid AND invitationstatus = 'Accepted' AND (workstatus = 'Not Applicable' OR workstatus IS NULL)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@roomid", roomId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        gvpeople.DataSource = reader;
                        gvpeople.DataBind();
                    }
                }
            }
        }


        protected void btngrade_Click1(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["roomid"], out int roomId) && int.TryParse(Request.QueryString["materialsid"], out int materialsId))
            {
                try
                {
                    Button btn = (Button)sender;
                    GridViewRow gvr = (GridViewRow)btn.NamingContainer;
                    HiddenField hf = (HiddenField)gvr.FindControl("hfTnIdPkId");
                    int tnidpk = int.Parse(hf.Value);

                    string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                    GridViewRow selectedRow = (GridViewRow)((Button)sender).NamingContainer;

                    string studentworkid = selectedRow.Cells[3].Text;
                    TextBox txtgrade = (TextBox)selectedRow.FindControl("txtgrade");

                    if (string.IsNullOrWhiteSpace(txtgrade.Text))
                    {
                        ShowErrorMessage("Please enter a grade.");
                        return;
                    }

                    if (!int.TryParse(txtgrade.Text, out int grade) || grade < 0 || grade > 100)
                    {
                        ShowErrorMessage("Please enter a valid grade between 0 and 100.");
                        return;
                    }

                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();
                        string updateQuery = "UPDATE studentwork SET points = @Points, gradestatus = 'graded' WHERE studentworkid = @StudentWorkId";
                        using (MySqlCommand cmd = new MySqlCommand(updateQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@Points", grade);
                            cmd.Parameters.AddWithValue("@StudentWorkId", studentworkid);
                            cmd.ExecuteNonQuery();

                            ShowSuccessMessage("Student Work Graded Successfully");
                        }
                    }

                    PopulateFileGridView1(roomId, materialsId);
                    //PopulateFileGridView2(roomId, materialsId);
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    ShowErrorMessage("An error occurred while grading student work.");
                }
            }
        }
        //    if (int.TryParse(Request.QueryString["roomid"], out int roomId) && int.TryParse(Request.QueryString["materialsid"], out int materialsId))
        //    {
        //        try
        //        {
        //            Button btn = (Button)sender;
        //            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        //            HiddenField hf = (HiddenField)gvr.FindControl("hfTnIdPkId");
        //            int tnidpk = int.Parse(hf.Value);

        //            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        //            GridViewRow selectedRow = (GridViewRow)((Button)sender).NamingContainer;

        //            string studentworkid = selectedRow.Cells[0].Text;
        //            string grade = ((TextBox)selectedRow.FindControl("txtgrade")).Text;

        //            using (MySqlConnection con = new MySqlConnection(connectionString))
        //            {
        //                con.Open();
        //                string updateQuery = "UPDATE studentwork SET points = @Points, gradestatus = 'graded' WHERE studentworkid = @StudentWorkId";
        //                using (MySqlCommand cmd = new MySqlCommand(updateQuery, con))
        //                {
        //                    cmd.Parameters.AddWithValue("@Points", grade);
        //                    cmd.Parameters.AddWithValue("@StudentWorkId", studentworkid);
        //                    cmd.ExecuteNonQuery();

        //                    ShowSuccessMessage("Student Work Graded Successfully");

        //                }
        //            }


        //            PopulateFileGridView1(roomId, materialsId);
        //            PopulateFileGridView2(roomId, materialsId);

        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //}


        //private void PopulateFileGridView2(int roomId, int materialsId)
        //{
        //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        //string query = "SELECT studentworkid, materialsId, studentname, FileName, points FROM studentwork WHERE roomId = @roomId AND materialsId = @materialsId";
        //        string query = "SELECT studentworkid, materialsId, studentname, FileName, points FROM studentwork WHERE roomId = @roomId AND materialsId = @materialsId AND gradestatus = 'graded'";

        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@roomId", roomId);
        //            command.Parameters.AddWithValue("@materialsId", materialsId);
        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                gvgraded.DataSource = reader;
        //                gvgraded.DataBind();
        //            }
        //        }
        //    }
        //}


    }
}