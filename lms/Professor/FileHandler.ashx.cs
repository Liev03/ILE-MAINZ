using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lms.Professor
{
    /// <summary>
    /// Summary description for FileHandler
    /// </summary>
    public class FileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["materialsId"] != null)
            {
                int materialsId = Convert.ToInt32(context.Request.QueryString["materialsId"]);
                byte[] fileData = RetrieveFileData(materialsId);

                if (fileData != null)
                {
                    context.Response.Clear();
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", $"inline; filename=file_{materialsId}.pdf"); // Adjust filename and disposition as needed
                    context.Response.BinaryWrite(fileData);
                    context.Response.End();
                }
            }
        }

        public bool IsReusable
        {
            get { return false; }
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
    }
}