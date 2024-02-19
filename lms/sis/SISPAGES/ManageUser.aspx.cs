using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace lms.sis.SISPAGES
{
    public partial class ManageUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rebind();
        }

        private void rebind()
        {
            ConnectionClass conn = new ConnectionClass();
            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();
                string selectquery = "SELECT UserID, Name, Email, Password, Role FROM manageuser;";
                using (MySqlCommand command = new MySqlCommand(selectquery, connection))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    Grid.DataSource = dataTable;
                    Grid.DataBind();
                }
                connection.Close();
                connection.Dispose();
            }
        }

        protected void MUsave_Click(object sender, EventArgs e)
        {
            ConnectionClass conn = new ConnectionClass();
            if (MUid.Text == "" || MUname.Text == "" || MUemail.Text == "" || txtpass.Text == "" || MUrole.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','Please fill out all the fields ','error')", true);
            }
            else
            {
                using (MySqlConnection connection = conn.GetConnection())
                {
                    try
                    {
                        connection.Open();
                        string insertQuery = "INSERT INTO manageuser (UserID, Name, Email, Password, Role) VALUES (@UserID, @Name, @Email, @Password, @Role)";
                        using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@UserID", MUid.Text);
                            command.Parameters.AddWithValue("@Name", MUname.Text);
                            command.Parameters.AddWithValue("@Email", MUemail.Text);
                            command.Parameters.AddWithValue("@Password", txtpass.Text);
                            command.Parameters.AddWithValue("@Role", MUrole.Text);
                            command.ExecuteNonQuery();
                        }
                        MUid.Text = "";
                        MUname.Text = "";
                        MUemail.Text = "";
                        txtpass.Text = "";
                        MUrole.Text = "";

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!', 'added successfully!', 'success')", true);
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','exception error','error')", true);
                    }
                    finally
                    {
                        rebind();
                    }
                }
            }
        }
        protected void Grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = Grid.SelectedIndex;
            DataKey selectedDataKey = Grid.DataKeys[selectedIndex];
            int UserID = Convert.ToInt32(selectedDataKey["UserID"]);
            ConnectionClass connection = new ConnectionClass();
            using (MySqlConnection conn = connection.GetConnection())
            {
                conn.Open();
                string select = "SELECT * FROM `manageuser` WHERE UserID = @UserID;";

                using (MySqlCommand command = new MySqlCommand(select, conn))
                {
                    command.Parameters.AddWithValue("@UserID", UserID );
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            MUid.Text = reader.GetString("UserID");
                            MUname.Text = reader.GetString("Name");
                            MUemail.Text = reader.GetString("Email");
                            txtpass.Text = reader.GetString("Password");
                            MUrole.Text = reader.GetString("Role");
                            MUsave.Enabled = false;
                        }
                        else
                        {
                            MUsave.Enabled = true;
                        }
                    }
                }
            }
            Grid.BorderStyle = BorderStyle.None;
        }
        protected void MUsearch_Click(object sender, EventArgs e)
        {
            string search = txtMUsearch.Text;
            ConnectionClass conn = new ConnectionClass();

            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();

                string selectquery;

                if (!string.IsNullOrEmpty(search))
                {
                    selectquery = "SELECT * FROM manageuser WHERE UserID = @search";
                }
                else
                {
                    selectquery = "SELECT * FROM manageuser";
                }

                using (MySqlCommand command = new MySqlCommand(selectquery, connection))
                {
                    if (!string.IsNullOrEmpty(search))
                    {
                        command.Parameters.AddWithValue("@search", search);
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        Grid.DataSource = dataTable;
                        Grid.DataBind();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "swal('Info', 'No data found for the search criteria', 'info');", true);
                    }
                }
                connection.Close();
                connection.Dispose();
            }

        }

        protected void MUcancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }


        protected void MUupdate_Click(object sender, EventArgs e)
        {
            //new add emailcheack
            int selectedIndex = Grid.SelectedIndex;
            if (selectedIndex >= 0)
            {
                DataKey selectedDataKey = Grid.DataKeys[selectedIndex];
                if (selectedDataKey != null)
                {
                    int UserID = Convert.ToInt32(selectedDataKey["UserID"]);
                    ConnectionClass conn = new ConnectionClass();
                    using (MySqlConnection connection = conn.GetConnection())
                    {
                        string updatequery = "UPDATE `manageuser` SET `Name` = @Name, `Email` = @Email, `Password` = @Password, `Role` = @Role WHERE `UserID` = @UserID";
                        try
                        {
                            connection.Open();

                            string checkEmailQuery = "SELECT COUNT(*) FROM manageuser WHERE Email = @Email AND UserID != @UserID";
                            using (MySqlCommand checkEmailCommand = new MySqlCommand(checkEmailQuery, connection))
                            {
                                checkEmailCommand.Parameters.AddWithValue("@Email", MUemail.Text);
                                checkEmailCommand.Parameters.AddWithValue("@UserID", UserID);
                                int emailCount = Convert.ToInt32(checkEmailCommand.ExecuteScalar());

                                if (emailCount > 0)
                                {
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Email already exists.','Please choose a different email address.','error')", true);
                                    return;
                                }
                            }

                            using (MySqlCommand command = new MySqlCommand(updatequery, connection))
                            {
                                command.Parameters.AddWithValue("@UserID", UserID);
                                command.Parameters.AddWithValue("@Name", MUname.Text);
                                command.Parameters.AddWithValue("@Email", MUemail.Text);
                                command.Parameters.AddWithValue("@Password", txtpass.Text);
                                command.Parameters.AddWithValue("@Role", MUrole.Text);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!','Changes Have Been Saved!', 'success')", true);
                                }
                                else
                                {
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','Please try again', 'error')", true);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','Please try again','error')", true);
                        }
                        finally
                        {
                            rebind();
                            Grid.SelectedIndex = -1;
                            MUid.Text = "";
                            MUname.Text = "";
                            MUemail.Text = "";
                            txtpass.Text = "";
                            MUrole.Text = "";
                            MUsave.Enabled = true;
                        }
                    }
                }
            }
        }
        //old
        //    int selectedIndex = Grid.SelectedIndex;
        //    if (selectedIndex >= 0)
        //    {
        //        DataKey selectedDataKey = Grid.DataKeys[selectedIndex];
        //        if (selectedDataKey != null)
        //        {
        //            int UserID = Convert.ToInt32(selectedDataKey["UserID"]);
        //            ConnectionClass conn = new ConnectionClass();
        //            using (MySqlConnection connection = conn.GetConnection())
        //            {
        //                //UserID, Name, Email, Password, Role, ImageName
        //                string updatequery = "UPDATE `manageuser` SET `Name` = @Name, `Email` = @Email, `Password` = @Password, `Role` = @Role WHERE `UserID` = @UserID";
        //                try
        //                {
        //                    connection.Open();
        //                    using (MySqlCommand command = new MySqlCommand(updatequery, connection))
        //                    {
        //                        command.Parameters.AddWithValue("@UserID", UserID);
        //                        command.Parameters.AddWithValue("@Name", MUname.Text);
        //                        command.Parameters.AddWithValue("@Email", MUemail.Text);
        //                        command.Parameters.AddWithValue("@Password", txtpass.Text);
        //                        command.Parameters.AddWithValue("@Role", MUrole.Text);

        //                        int rowsAffected = command.ExecuteNonQuery();

        //                        if (rowsAffected > 0)
        //                        {
        //                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!','Changes Have Been Saved!', 'success')", true);
        //                        }
        //                        else
        //                        {
        //                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','Please try again', 'error')", true);
        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','Please try again','error')", true);
        //                }
        //                finally
        //                {
        //                    rebind();
        //                    Grid.SelectedIndex = -1;
        //                    MUid.Text = "";
        //                    MUname.Text = "";
        //                    MUemail.Text = "";
        //                    txtpass.Text = "";
        //                    MUrole.Text = "";
        //                    MUsave.Enabled = true;
        //                }
        //            }
        //        }
        //    }
        //}

        protected void MUdelete_Click(object sender, EventArgs e)
        {
            int selectedIndex = Grid.SelectedIndex;
            if (selectedIndex >= 0)
            {
                DataKey selectedDataKey = Grid.DataKeys[selectedIndex];
                if (selectedDataKey != null)
                {
                    int UserID = Convert.ToInt32(selectedDataKey["UserID"]);
                    ConnectionClass conn = new ConnectionClass();
                    using (MySqlConnection connection = conn.GetConnection())
                    {
                        string deleteQuery = "DELETE FROM manageuser WHERE UserID = @UserID";
                        try
                        {
                            connection.Open();
                            using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                            {
                                command.Parameters.AddWithValue("@UserID", UserID);
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!','Deleted!', 'success')", true);
                                }
                                else
                                {
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','Item not found...', 'error')", true);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','','error')", true);
                        }
                        finally
                        {
                            rebind();
                            Grid.SelectedIndex = -1;
                            MUid.Text = "";
                            MUname.Text = "";
                            MUemail.Text = "";
                            txtpass.Text = "";
                            MUrole.Text = "";
                            MUsave.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','please select from the table below','error')", true);
                Grid.Focus();
                Grid.BorderStyle = BorderStyle.Solid;
            }
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Account/Logout.aspx");
        }
    }
}