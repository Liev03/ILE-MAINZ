using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace lms
{
    public partial class file : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // Populate the dropdown with file names
                PopulateFileDropdown();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                string fileName = Path.GetFileName(fileUpload.FileName);
                string fileType = Path.GetExtension(fileUpload.FileName);
                byte[] fileData = fileUpload.FileBytes;

                // Save file information to the database
                SaveFileToDatabase(fileName, fileType, fileData);

                // Refresh the dropdown with the updated file list
                PopulateFileDropdown();
            }
        }

        protected void ddlFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle dropdown selection change if needed
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            int selectedFileID = Convert.ToInt32(ddlFiles.SelectedValue);

            byte[] fileData = RetrieveFileData(selectedFileID);

            // Send the file to the client for download
            if (fileData != null)
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", $"attachment; filename={ddlFiles.SelectedItem.Text}");
                Response.BinaryWrite(fileData);
                Response.End();
            }
        }

        private void PopulateFileDropdown()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT FileID, FileName FROM UploadedFiles";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        ddlFiles.DataSource = reader;
                        ddlFiles.DataTextField = "FileName";
                        ddlFiles.DataValueField = "FileID";
                        ddlFiles.DataBind();
                    }
                }
            }
        }

        private void SaveFileToDatabase(string fileName, string fileType, byte[] fileData)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO UploadedFiles (FileName, FileType, FileData) VALUES (@FileName, @FileType, @FileData)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FileName", fileName);
                    command.Parameters.AddWithValue("@FileType", fileType);
                    command.Parameters.AddWithValue("@FileData", fileData);
                    command.ExecuteNonQuery();
                }
            }
        }

        private byte[] RetrieveFileData(int fileID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT FileData FROM UploadedFiles WHERE FileID = @FileID";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FileID", fileID);
                    return command.ExecuteScalar() as byte[];
                }
            }
        }
    }
}