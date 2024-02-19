//using iText.IO.Colors;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.sis.SISPAGES
{
    public partial class EditProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                name.InnerText = Session["Name"] as string;
                id.InnerText = Session["ID"] as string;
                ConnectionClass conn = new ConnectionClass();
                if (!IsPostBack)
                {
                    using (MySqlConnection connection = conn.GetConnection())
                    {
                        connection.Open();
                        string select = "SELECT * FROM `student_info` where studentId = @StudentId";
                        using (MySqlCommand command = new MySqlCommand(select, connection))
                        {
                            command.Parameters.AddWithValue("@StudentId", Session["ID"]);
                            MySqlDataReader reader = command.ExecuteReader();

                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    editprobday.Text = reader.GetString("birthdate");
                                    Address.Text = reader.GetString("address");
                                    Contact.Text = reader.GetString("contact");
                                }
                            }
                            reader.Close();
                        }
                        DisplayUploadedImage(connection, Session["ID"]);
                    }
                }    
        }
        protected void btnepsave_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionClass conn = new ConnectionClass();
                using (MySqlConnection connection = conn.GetConnection())
                {
                    connection.Open();
                    string checkImageQuery = "SELECT COUNT(*) FROM tblimages WHERE studentId = @studentID";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkImageQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@studentID", Session["ID"]);
                        int imageCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (imageCount > 0)
                        {
                            byte[] imageData = btnepupload.FileBytes;
                            int imageSize = btnepupload.PostedFile.ContentLength;
                            string imageName = btnepupload.FileName;

                            string updateQuery = "CALL spUpdateImage(@studentID, @imageName, @imageSize, @imageData); " +
                                                 "UPDATE student_info SET address = @address, contact = @contact WHERE studentId = @studentID;";
                            using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@studentID", Session["ID"]);
                                cmd.Parameters.AddWithValue("@address", Address.Text);
                                cmd.Parameters.AddWithValue("@contact", Contact.Text);
                                cmd.Parameters.AddWithValue("@imageName", imageName);
                                cmd.Parameters.AddWithValue("@imageSize", imageSize);
                                cmd.Parameters.AddWithValue("@imageData", imageData);
                                cmd.ExecuteNonQuery();
                            }
                            string imgQuery = $"SELECT * FROM `tblimages` WHERE studentId = @StudentId";

                            using (MySqlCommand command = new MySqlCommand(imgQuery, connection))
                            {
                                command.Parameters.AddWithValue("@StudentId", Session["ID"]);
                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows && reader.Read())
                                    {
                                        imageData = (byte[])reader["ImageData"];
                                        int contentLength = reader.GetInt32("Size");
                                        string fileName = reader.GetString("Name");
                                        string serverPath = Server.MapPath("~/Images/");
                                        string filePath = Path.Combine(serverPath, fileName);
                                        File.WriteAllBytes(filePath, imageData);

                                        Image1.ImageUrl = "~/Images/" + fileName;
                                    }
                                }
                            }
                        }
                        else
                        {
                            byte[] imageData = btnepupload.FileBytes;
                            int imageSize = btnepupload.PostedFile.ContentLength;
                            string imageName = btnepupload.FileName;

                            string insertQuery = "CALL spUploadImage(@imageName, @imageSize, @imageData, @studentID); " +
                                                 "UPDATE student_info SET address = @address, contact = @contact WHERE studentId = @studentID;";
                            using (MySqlCommand cmd = new MySqlCommand(insertQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@studentID", Session["ID"]);
                                cmd.Parameters.AddWithValue("@address", Address.Text);
                                cmd.Parameters.AddWithValue("@contact", Contact.Text);
                                cmd.Parameters.AddWithValue("@imageName", imageName);
                                cmd.Parameters.AddWithValue("@imageSize", imageSize);
                                cmd.Parameters.AddWithValue("@imageData", imageData);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        DisplayUploadedImage(connection, Session["ID"]);
                    }
                    
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success','Profile updated successfully.','success')", true);
                }
            }
            catch (Exception ex)
            {

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", $"swal('Error','{ex.Message}','error')", true);
            }
        }
        private void DisplayUploadedImage(MySqlConnection connection, object studentID)
        {
            string fetchImageQuery = "SELECT ImageData FROM tblimages WHERE studentId = @studentID";
            using (MySqlCommand fetchCmd = new MySqlCommand(fetchImageQuery, connection))
            {
                fetchCmd.Parameters.AddWithValue("@studentID", studentID);
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
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Account/Logout.aspx");
        }

    }
}
