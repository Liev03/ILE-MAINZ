using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace lms.Professor
{
    public partial class WebForm11 : System.Web.UI.Page
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
                        //PopulateFileDropdown(roomId, materialsId);
                        //ddlFiles.Enabled = false;
                        PopulateFileGridView(roomId, materialsId);

                        //BindFilesGrid(roomId, materialsId);

                    }
                }
            }
        }
        //protected void gvFiles_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int selectedRowIndex = gvFiles.SelectedIndex;

        //    if (selectedRowIndex >= 0)
        //    {
        //        int selectedmaterialsid = Convert.ToInt32(gvFiles.DataKeys[selectedRowIndex].Value);

        //        // Retrieve file data based on the selected file ID
        //        byte[] fileData = RetrieveFileData(selectedmaterialsid);

        //        // Display the file content in the Label
        //        if (fileData != null)
        //        {
        //            string fileContent = System.Text.Encoding.UTF8.GetString(fileData);
        //            lblFileContent.Text = fileContent;
        //        }
        //    }
        //}
        //private void BindFilesGrid(int roomId, int materialsId)
        //{
        //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string query = "SELECT materialsid, FileName FROM learningmaterials WHERE roomid = @roomid AND materialsid = @materialsid";
        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@roomid", roomId);
        //            command.Parameters.AddWithValue("@materialsid", materialsId);

        //            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
        //            {
        //                DataTable dt = new DataTable();
        //                adapter.Fill(dt);
        //                gvFiles.DataSource = dt;
        //                gvFiles.DataBind();
        //            }
        //        }
        //    }
        //}
        //private byte[] RetrieveFileData(int materialsid)
        //{
        //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string query = "SELECT FileData FROM learningmaterials ";
        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@materialsid", materialsid);
        //            return command.ExecuteScalar() as byte[];
        //        }
        //    }
        //}
        private void DisplayMaterials(int roomId, int materialsId)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT materialsid, materialsname, instructions, duedate, topic, posttype, points FROM learningmaterials " +
                                   "WHERE roomid = @roomid AND materialsid = @materialsid";

                    using (MySqlCommand command = new MySqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@roomid", roomId);
                        command.Parameters.AddWithValue("@materialsid", materialsId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtmaterialsname.Text = reader["materialsname"].ToString();
                                txtinstructions.Text = reader["instructions"].ToString();
                                txtduedate.Text = reader["duedate"].ToString();
                                txttopic.Text = reader["topic"].ToString();

                                string postType = reader["posttype"].ToString();
                                SetRadioButtonChecked(postType);

                                int points = Convert.ToInt32(reader["points"]);
                                drdpoints.SelectedValue = points.ToString();
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

        private void SetRadioButtonChecked(string postType)
        {
            switch (postType)
            {
                case "Assignment":
                    rbassignment.Checked = true;
                    break;
                case "Quiz":
                    rbquiz.Checked = true;
                    break;
                case "Materials":
                    rbmaterials.Checked = true;
                    break;
                default:
                    break;
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

        //protected void btnDownload_Click(object sender, EventArgs e)
        //{
        //    int selectedFileID = Convert.ToInt32(ddlFiles.SelectedValue);

        //    byte[] fileData = RetrieveFileData(selectedFileID);

        //    if (fileData != null)
        //    {
        //        Response.Clear();
        //        Response.ContentType = "application/octet-stream";
        //        Response.AddHeader("Content-Disposition", $"attachment; filename={ddlFiles.SelectedItem.Text}");
        //        Response.BinaryWrite(fileData);
        //        Response.End();
        //    }
        //}

        //protected void ddlFiles_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
        //private void PopulateFileDropdown(int roomId, int materialsId)
        //{
        //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string query = "SELECT materialsid, FileName FROM learningmaterials WHERE roomid = @roomid AND materialsid = @materialsid";
        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@roomid", roomId);
        //           command.Parameters.AddWithValue("@materialsid", materialsId);
        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                ddlFiles.DataSource = reader;
        //                ddlFiles.DataTextField = "FileName";
        //                ddlFiles.DataValueField = "materialsid";
        //                ddlFiles.DataBind();
        //            }
        //        }
        //    }
        //}
        //private byte[] RetrieveFileData(int materialsid)
        //{
        //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string query = "SELECT FileData FROM learningmaterials WHERE materialsid = @materialsid";
        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@materialsid", materialsid);
        //            return command.ExecuteScalar() as byte[];
        //        }
        //    }
        //}

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string materialsname = txtmaterialsname.Text;
            string instructions = txtinstructions.Text;
            string posttype = rbassignment.Checked ? "Assignment" : (rbquiz.Checked ? "Quiz" : "Materials");
            string points = drdpoints.SelectedItem.Text;

            int teacherId = Convert.ToInt32(Session["LoggedInUserID"]);
            string teacherEmail = Session["LoggedInUserEmail"].ToString();

            if (int.TryParse(Request.QueryString["roomid"], out int roomId) && int.TryParse(Request.QueryString["materialsid"], out int materialsId))
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string checkExistenceQuery = "SELECT COUNT(*) FROM learningmaterials WHERE materialsid = @materialsid";
                    using (MySqlCommand commandExistenceCheck = new MySqlCommand(checkExistenceQuery, con))
                    {
                        commandExistenceCheck.Parameters.AddWithValue("@materialsid", materialsId);
                        int count = Convert.ToInt32(commandExistenceCheck.ExecuteScalar());

                        if (count > 0)
                        {
                            try
                            {
                                byte[] fileData = null;
                                string fileName = "";
                                string fileType = "";

                                if (file.HasFile)
                                {
                                    fileName = Path.GetFileName(file.FileName);
                                    fileType = Path.GetExtension(file.FileName);
                                    fileData = file.FileBytes;
                                }
                                else
                                {
                                    string getFileInfoQuery = "SELECT FileName, FileType, FileData FROM learningmaterials WHERE materialsid = @materialsid";
                                    using (MySqlCommand commandFileInfo = new MySqlCommand(getFileInfoQuery, con))
                                    {
                                        commandFileInfo.Parameters.AddWithValue("@materialsid", materialsId);
                                        using (MySqlDataReader reader = commandFileInfo.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                fileName = reader["FileName"].ToString();
                                                fileType = reader["FileType"].ToString();
                                                fileData = (byte[])reader["FileData"];
                                            }
                                        }
                                    }
                                }

                                string updateQuery = "UPDATE learningmaterials SET teacherid = @teacherid, teacheremail = @teacheremail, " +
                                                    "materialsname = @materialsname, instructions = @instructions, " +
                                                    "posttype = @posttype, points = @points, duedate = @duedate, topic = @topic, " +
                                                    "FileName = @fileName, FileType = @fileType, FileData = @fileData " +
                                                    "WHERE materialsid = @materialsid";

                                using (MySqlCommand commandUpdate = new MySqlCommand(updateQuery, con))
                                {
                                    commandUpdate.Parameters.AddWithValue("@materialsid", materialsId);
                                    commandUpdate.Parameters.AddWithValue("@teacherid", teacherId);
                                    commandUpdate.Parameters.AddWithValue("@teacheremail", teacherEmail);
                                    commandUpdate.Parameters.AddWithValue("@materialsname", materialsname);
                                    commandUpdate.Parameters.AddWithValue("@instructions", instructions);
                                    commandUpdate.Parameters.AddWithValue("@posttype", posttype);
                                    commandUpdate.Parameters.AddWithValue("@points", points);
                                    commandUpdate.Parameters.AddWithValue("@duedate", txtduedate.Text);
                                    commandUpdate.Parameters.AddWithValue("@topic", txttopic.Text);
                                    commandUpdate.Parameters.AddWithValue("@fileName", fileName);
                                    commandUpdate.Parameters.AddWithValue("@fileType", fileType);
                                    commandUpdate.Parameters.AddWithValue("@fileData", fileData);

                                    commandUpdate.ExecuteNonQuery();

                                    ShowSuccessMessage("Your Materials have been successfully updated");


                                }


                                PopulateFileGridView(roomId, materialsId);


                                ClientScript.RegisterStartupScript(this.GetType(), "successMessage", "showSuccessMessage();", true);
                            }
                            catch (Exception ex)
                            {
                                ShowErrorMessage("Error updating record: " + ex.Message);
                            }
                        }
                        else
                        {
                            ShowErrorMessage("Record not found for update");
                        }
                    }
                }
            }
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
        protected void gvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = gvFiles.SelectedIndex;
            GridViewRow row = gvFiles.Rows[rowIndex];

            int selectedFileID = Convert.ToInt32(row.Cells[0].Text);
            byte[] fileData = RetrieveFileData(selectedFileID);

            if (fileData != null)
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", $"attachment; filename={row.Cells[1].Text}");
                Response.BinaryWrite(fileData);
                Response.End();
            }
        }


    }
}