using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace lms
{
    public partial class Vviewfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate the GridView with file information
                BindFilesGrid();
            }
        }

        protected void gvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

            int selectedRowIndex = gvFiles.SelectedIndex;

            if (selectedRowIndex >= 0)
            {
                int selectedFileID = Convert.ToInt32(gvFiles.DataKeys[selectedRowIndex].Value);

                // Retrieve file data based on the selected file ID
                byte[] fileData = RetrieveFileData(selectedFileID);

                // Display the file content in the Label
                if (fileData != null)
                {
                    string fileContent = System.Text.Encoding.UTF8.GetString(fileData);
                    lblFileContent.Text = fileContent;
                }
            }
        }

        private void BindFilesGrid()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT FileID, FileName FROM UploadedFiles";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        gvFiles.DataSource = dt;
                        gvFiles.DataBind();
                    }
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
