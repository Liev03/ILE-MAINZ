using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Student
{
    public partial class WebForm8 : System.Web.UI.Page
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
                        DisplayUserProfileImage();
                        DisplayComment();
                        DisplayPoints(roomId, materialsId);


                        PopulateFileGridView1(roomId, materialsId);
                        //PopulateFileDropdown(roomId, materialsId);
                        //ddlFiles.Enabled = false;

                        string UserID = Session["LoggedInUserID"] as string;

                        if (!string.IsNullOrEmpty(UserID))
                        {
                            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
                            {
                                connection.Open();
                                GetUserProfileImage(UserID);

                                byte[] profileImageBytes = GetUserProfileImage(UserID);
                                if (profileImageBytes != null)
                                {
                                    string base64Image = Convert.ToBase64String(profileImageBytes);
                                    string imageSrc = "data:image/jpeg;base64," + base64Image;
                                    Image1.ImageUrl = imageSrc;
                                }
                            }
                        }
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

                    string query = "SELECT materialsid, subjectname, teacherid, teacheremail, materialsname, instructions, duedate, topic, posttype, points, dateposted, teachername FROM learningmaterials " +
                                   "WHERE roomid = @roomid AND materialsid = @materialsid";

                    using (MySqlCommand command = new MySqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@roomid", roomId);
                        command.Parameters.AddWithValue("@materialsid", materialsId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblmaterialsid.Text = reader["materialsid"].ToString();
                                lblmaterialsname.Text = reader["materialsname"].ToString();
                                lblsubjectname.Text = reader["subjectname"].ToString();
                                lblteacherid.Text = reader["teacherid"].ToString();
                                lblteacheremail.Text = reader["teacheremail"].ToString();
                                string materialsName = reader["materialsname"].ToString();
                                lblpost.Text = $"{materialsName} - {reader["posttype"].ToString()}"; lbldateposted.Text = reader["dateposted"].ToString();
                                lblteacher.Text = reader["teachername"].ToString();
                                lblinstructions.Text = reader["instructions"].ToString();
                                // lbldue.Text = (reader["duedate"] is DBNull || reader["duedate"] == null)
                                //? string.Empty
                                //  : Convert.ToDateTime(reader["duedate"]).ToString("yyyy-MM-dd");
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


        private void DisplayPoints(int roomId, int materialsId)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT materialsid, points, gradestatus FROM studentwork " +
                                   "WHERE roomid = @roomid AND materialsid = @materialsid";

                    using (MySqlCommand command = new MySqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@roomid", roomId);
                        command.Parameters.AddWithValue("@materialsid", materialsId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblgradepoints.Text = reader["points"].ToString();
                                lblstatus.Text = reader["gradestatus"].ToString().ToUpper();



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
                string studentemail = Session["LoggedInUserEmail"].ToString();
                string UserID = Session["LoggedInUserID"] as string;

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

                            string retrieveStudentNameQuery = "SELECT name FROM student_Info WHERE email = @studentemail";

                            using (MySqlCommand retrieveNameCommand = new MySqlCommand(retrieveStudentNameQuery, con))
                            {
                                retrieveNameCommand.Parameters.AddWithValue("@studentemail", studentemail);

                                using (MySqlDataReader nameReader = retrieveNameCommand.ExecuteReader())
                                {
                                    if (nameReader.Read())
                                    {
                                        string fullname = nameReader["name"].ToString();
                                 

                                        nameReader.Close();
                                        byte[] imageData = GetUserProfileImage(UserID);

                                        string insertQuery = "INSERT INTO materialscomment (materialsid, roomid, teacheremail, studentemail, name, profileimage, commentpost, datepost) " +
                                                             "VALUES (@materialsid, @roomid,  @teacheremail, @studentemail, @name, @profileimage, @commentpost, @datepost)";

                                        using (MySqlCommand commandInsert = new MySqlCommand(insertQuery, con))
                                        {
                                            commandInsert.Parameters.AddWithValue("@materialsid", materialsid);
                                            commandInsert.Parameters.AddWithValue("@roomid", roomIdFromQueryString);
                                            commandInsert.Parameters.AddWithValue("@teacheremail", teacheremail);
                                            commandInsert.Parameters.AddWithValue("@studentemail", studentemail);
                                            commandInsert.Parameters.AddWithValue("@name", fullname);

                                            byte[] profileImage = GetUserProfileImage(UserID);
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

        protected void btnmarkasdone_Click(object sender, EventArgs e)
        {
            //int studentid = Convert.ToInt32(Session["LoggedInUserID"]);
            //string studentEmail = Session["LoggedInUserEmail"].ToString();
            //string teacherid = lblteacherid.Text;
            //string teacheremail = lblteacheremail.Text;
            //string subjectname = lblsubjectname.Text;
            //string materialsname = lblmaterialsname.Text;
            //string materialsid = lblmaterialsid.Text;
            ////int roomId;

            ////if (int.TryParse(Request.QueryString["roomid"], out roomId))
            //if (int.TryParse(Request.QueryString["roomid"], out int roomId) && int.TryParse(Request.QueryString["materialsid"], out int materialsId))

            //{
            //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            //    using (MySqlConnection con = new MySqlConnection(connectionString))
            //    {
            //        con.Open();

            //        string retrieveStudentNameQuery = "SELECT firstname, lastname FROM student_info WHERE email = @studentEmail";

            //        using (MySqlCommand retrieveNameCommand = new MySqlCommand(retrieveStudentNameQuery, con))
            //        {
            //            retrieveNameCommand.Parameters.AddWithValue("@studentemail", studentEmail);

            //            using (MySqlDataReader nameReader = retrieveNameCommand.ExecuteReader())
            //            {
            //                if (nameReader.Read())
            //                {
            //                    string studentFirstName = nameReader["firstname"].ToString();
            //                    string studentLastName = nameReader["lastname"].ToString();
            //                    string studentFullName = $"{studentFirstName} {studentLastName}";

            //                    nameReader.Close();


            //                    if (file.HasFile)
            //                    {
            //                        try
            //                        {
            //                            string fileName = Path.GetFileName(file.FileName);
            //                            string fileType = Path.GetExtension(file.FileName);
            //                            byte[] fileData = file.FileBytes;
            //                            //if (Request.Files.Count > 0)
            //                            //{
            //                            //    try
            //                            //    {
            //                            //        for (int i = 0; i < Request.Files.Count; i++)
            //                            //        {
            //                            //            HttpPostedFile uploadedFile = Request.Files[i];

            //                            //            // Extract information about the uploaded file
            //                            //            string fileName = Path.GetFileName(uploadedFile.FileName);
            //                            //            string fileType = Path.GetExtension(fileName);
            //                            //            byte[] fileData;

            //                            //            // Read the file data into a byte array
            //                            //            using (BinaryReader binaryReader = new BinaryReader(uploadedFile.InputStream))
            //                            //            {
            //                            //                fileData = binaryReader.ReadBytes(uploadedFile.ContentLength);
            //                            //            }

            //                            string insertQuery = "INSERT INTO studentwork (materialsid, teacherid, studentid, roomid, teacheremail, studentemail, studentname, FileName, FileType, FileData, subjectname, materialsname, workstatus) " +
            //                                                 "VALUES (@materialsid, @teacherid, @studentid, @roomid, @teacheremail, @studentemail, @studentname, @FileName, @FileType, @FileData, @subjectname, @materialsname, 'turnedin')";

            //                            using (MySqlCommand commandInsert = new MySqlCommand(insertQuery, con))
            //                            {
            //                                commandInsert.Parameters.AddWithValue("@materialsid", materialsid);
            //                                commandInsert.Parameters.AddWithValue("@teacherid", teacherid);
            //                                commandInsert.Parameters.AddWithValue("@studentid", studentid);
            //                                commandInsert.Parameters.AddWithValue("@roomid", roomId);
            //                                commandInsert.Parameters.AddWithValue("@teacheremail", teacheremail);
            //                                commandInsert.Parameters.AddWithValue("@studentEmail", studentEmail);
            //                                commandInsert.Parameters.AddWithValue("@studentname", studentFullName);
            //                                commandInsert.Parameters.AddWithValue("@fileName", fileName);
            //                                commandInsert.Parameters.AddWithValue("@fileType", fileType);
            //                                commandInsert.Parameters.AddWithValue("@fileData", fileData);
            //                                commandInsert.Parameters.AddWithValue("@subjectname", subjectname);
            //                                commandInsert.Parameters.AddWithValue("@materialsname", materialsname);

            //                                commandInsert.ExecuteNonQuery();

            //                                ShowSuccessMessage("Your Work have been successfully Turned In");

            //                                PopulateFileGridView1(roomId, materialsId);

            //                            }

            //                            ClientScript.RegisterStartupScript(this.GetType(), "successMessage", "showSuccessMessage();", true);
            //                            //DisplayMaterials();
            //                        }

            //                        catch (Exception ex)
            //                        {
            //                            // Handle file upload error
            //                            ShowErrorMessage("Error uploading file: " + ex.Message);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }






        private void PopulateFileGridView1(int roomId, int materialsId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT studentworkid, materialsId, FileName FROM studentwork WHERE roomId = @roomId AND materialsId = @materialsId";
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

        protected void gvwork_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = gvwork.SelectedIndex;
            GridViewRow row = gvwork.Rows[rowIndex];

            int selectedFileID = Convert.ToInt32(row.Cells[0].Text);
            byte[] fileData = RetrieveFileData1(selectedFileID);

            if (fileData != null)
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", $"attachment; filename={row.Cells[1].Text}");
                Response.BinaryWrite(fileData);
                Response.End();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int studentid = Convert.ToInt32(Session["LoggedInUserID"]);
            string studentEmail = Session["LoggedInUserEmail"].ToString();
            string teacherid = lblteacherid.Text;
            string teacheremail = lblteacheremail.Text;
            string subjectname = lblsubjectname.Text;
            string materialsname = lblmaterialsname.Text;
            string materialsid = lblmaterialsid.Text;
            //int roomId;

            //if (int.TryParse(Request.QueryString["roomid"], out roomId))
            if (int.TryParse(Request.QueryString["roomid"], out int roomId) && int.TryParse(Request.QueryString["materialsid"], out int materialsId))

            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string retrieveStudentNameQuery = "SELECT name FROM student_info WHERE email = @studentEmail";

                    using (MySqlCommand retrieveNameCommand = new MySqlCommand(retrieveStudentNameQuery, con))
                    {
                        retrieveNameCommand.Parameters.AddWithValue("@studentemail", studentEmail);

                        using (MySqlDataReader nameReader = retrieveNameCommand.ExecuteReader())
                        {
                            if (nameReader.Read())
                            {
                                string fullname = nameReader["name"].ToString();
                             

                                nameReader.Close();


                                if (file.HasFile)
                                {
                                    try
                                    {
                                        string fileName = Path.GetFileName(file.FileName);
                                        string fileType = Path.GetExtension(file.FileName);
                                        byte[] fileData = file.FileBytes;

                                        //string checkDuplicateQuery = "SELECT COUNT(*) FROM studentwork WHERE materialsid = @materialsid AND teacherid = @teacherid AND studentid = @studentid AND roomid = @roomid";

                                        //using (MySqlCommand checkDuplicateCommand = new MySqlCommand(checkDuplicateQuery, con))
                                        //{
                                        //    checkDuplicateCommand.Parameters.AddWithValue("@materialsid", materialsid);
                                        //    checkDuplicateCommand.Parameters.AddWithValue("@teacherid", teacherid);
                                        //    checkDuplicateCommand.Parameters.AddWithValue("@studentid", studentid);
                                        //    checkDuplicateCommand.Parameters.AddWithValue("@roomid", roomId);

                                        //    int duplicateCount = Convert.ToInt32(checkDuplicateCommand.ExecuteScalar());

                                        //    if (duplicateCount > 0)
                                        //    {
                                        //        // Duplicate record found, handle accordingly (show an error message, etc.)
                                        //        ShowErrorMessage("This work has already been submitted.");
                                        //    }
                                        //    else
                                        //    {

                                                //if (Request.Files.Count > 0)
                                                //{
                                                //    try
                                                //    {
                                                //        for (int i = 0; i < Request.Files.Count; i++)
                                                //        {
                                                //            HttpPostedFile uploadedFile = Request.Files[i];

                                                //            // Extract information about the uploaded file
                                                //            string fileName = Path.GetFileName(uploadedFile.FileName);
                                                //            string fileType = Path.GetExtension(fileName);
                                                //            byte[] fileData;

                                                //            // Read the file data into a byte array
                                                //            using (BinaryReader binaryReader = new BinaryReader(uploadedFile.InputStream))
                                                //            {
                                                //                fileData = binaryReader.ReadBytes(uploadedFile.ContentLength);
                                                //            }

                                                string insertQuery = "INSERT INTO studentwork (materialsid, teacherid, studentid, roomid, teacheremail, studentemail, studentname, FileName, FileType, FileData, subjectname, materialsname, workstatus) " +
                                                             "VALUES (@materialsid, @teacherid, @studentid, @roomid, @teacheremail, @studentemail, @studentname, @FileName, @FileType, @FileData, @subjectname, @materialsname, 'turnedin')";

                                                using (MySqlCommand commandInsert = new MySqlCommand(insertQuery, con))
                                                {
                                                    commandInsert.Parameters.AddWithValue("@materialsid", materialsid);
                                                    commandInsert.Parameters.AddWithValue("@teacherid", teacherid);
                                                    commandInsert.Parameters.AddWithValue("@studentid", studentid);
                                                    commandInsert.Parameters.AddWithValue("@roomid", roomId);
                                                    commandInsert.Parameters.AddWithValue("@teacheremail", teacheremail);
                                                    commandInsert.Parameters.AddWithValue("@studentEmail", studentEmail);
                                                    commandInsert.Parameters.AddWithValue("@studentname", fullname);
                                                    commandInsert.Parameters.AddWithValue("@fileName", fileName);
                                                    commandInsert.Parameters.AddWithValue("@fileType", fileType);
                                                    commandInsert.Parameters.AddWithValue("@fileData", fileData);
                                                    commandInsert.Parameters.AddWithValue("@subjectname", subjectname);
                                                    commandInsert.Parameters.AddWithValue("@materialsname", materialsname);

                                                    commandInsert.ExecuteNonQuery();

                                                    ShowSuccessMessage("Your Work have been successfully Added");

                                                    PopulateFileGridView1(roomId, materialsId);

                                                }

                                                ClientScript.RegisterStartupScript(this.GetType(), "successMessage", "showSuccessMessage();", true);
                                                //DisplayMaterials();
                                            }
                                    //    }
                                    //}

                                    catch (Exception ex)
                                    {
                                        // Handle file upload error
                                        ShowErrorMessage("Error uploading file: " + ex.Message);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void RemoveFileFromDatabase(int studentworkid)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM studentwork WHERE studentworkid = @studentworkid";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@studentworkid", studentworkid);
                    command.ExecuteNonQuery();
                }
                ShowSuccessMessage("Your file have been successfully Removed");

            }
        }


        protected void gvwork_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (int.TryParse(Request.QueryString["roomid"], out int roomId) && int.TryParse(Request.QueryString["materialsid"], out int materialsId))
            {
                if (e.CommandName == "RemoveFile" && !string.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    int studentworkid = Convert.ToInt32(e.CommandArgument);
                    RemoveFileFromDatabase(studentworkid);

                    PopulateFileGridView1(roomId, materialsId);
                }

            }



        }
    }
}