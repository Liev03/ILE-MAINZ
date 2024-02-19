using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;

namespace lms.sis.SISPAGES
{
    public partial class SectionManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rebind();
        }
        protected void ScMadd_Click(object sender, EventArgs e)
        {
            ConnectionClass conn = new ConnectionClass();
            if (txtsec.Text == "" || txtroom.Text == "")
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
                        string insertQuery = "INSERT INTO sectionmanagement (`Section`, `Classroom`) VALUES (@Section, @Classroom)";
                        using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Section", txtsec.Text);
                            command.Parameters.AddWithValue("@Classroom", txtroom.Text);
                            command.ExecuteNonQuery();
                        }

                        txtsec.Text = "";
                        txtroom.Text = "";

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!', 'addedd successfully!', 'success')", true);

                    }
                    catch (Exception ex)
                    {
                        txtsec.Text = ex.Message;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','Name must be unique!! Please Include (lab)/(lec) at the end ','error')", true);
                    }
                    finally
                    {
                        rebind();
                    }
                }
            }
        }

        protected void ScMupdate_Click(object sender, EventArgs e)
        {
            int selectedIndex = Grid.SelectedIndex;
            if (selectedIndex >= 0)
            {
                DataKey selectedDataKey = Grid.DataKeys[selectedIndex];
                if (selectedDataKey != null)
                {
                    int SectionID = Convert.ToInt32(selectedDataKey["SectionID"]);
                    ConnectionClass conn = new ConnectionClass();
                    using (MySqlConnection connection = conn.GetConnection())
                    {
                        string updatequery = "UPDATE `sectionmanagement` SET `Section` = @Section, `Classroom` = @Classroom, `SectionID` = @SectionID WHERE `SectionID` = @SectionID";
                        try
                        {
                            connection.Open();
                            using (MySqlCommand command = new MySqlCommand(updatequery, connection))
                            {
                                command.Parameters.AddWithValue("@Section", txtsec.Text);
                                command.Parameters.AddWithValue("@Classroom", txtroom.Text);
                                command.Parameters.AddWithValue("@SectionID", SectionID);

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
                            txtsec.Text = "";
                            txtroom.Text = "";
                            ScMadd.Enabled = true;

                        }
                    }
                }
            }
        }
        protected void ScMdelete_Click(object sender, EventArgs e)
        {
            int selectedIndex = Grid.SelectedIndex;
            if (selectedIndex >= 0)
            {
                DataKey selectedDataKey = Grid.DataKeys[selectedIndex];
                if (selectedDataKey != null)
                {
                    int SectionID = Convert.ToInt32(selectedDataKey["SectionID"]);
                    ConnectionClass conn = new ConnectionClass();
                    using (MySqlConnection connection = conn.GetConnection())
                    {
                        string deleteQuery = "DELETE FROM sectionmanagement WHERE SectionID = @SectionID";
                        try
                        {
                            connection.Open();
                            using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                            {
                                command.Parameters.AddWithValue("@SectionID", SectionID);
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
                            txtsec.Text = "";
                            txtroom.Text = "";
                            ScMadd.Enabled = true;
                        }
                    }
                }
            }
            else {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','please select from the table below','error')", true);
                Grid.Focus();
                Grid.BorderStyle = BorderStyle.Solid;
            }
        }
        private void rebind()
        {
            ConnectionClass conn = new ConnectionClass();
            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();
                string selectquery = "SELECT * FROM `sectionmanagement`";
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
       
        protected void Grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = Grid.SelectedIndex;
            DataKey selectedDataKey = Grid.DataKeys[selectedIndex];
            int SectionID = Convert.ToInt32(selectedDataKey["SectionID"]);
            ConnectionClass connection = new ConnectionClass();
            using (MySqlConnection conn = connection.GetConnection())
            {
                conn.Open();
                string select = "SELECT * FROM `sectionmanagement` WHERE SectionID = @SectionID;";

                using (MySqlCommand command = new MySqlCommand(select, conn))
                {
                    command.Parameters.AddWithValue("@SectionID", SectionID);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            txtsec.Text = reader.GetString("Section");
                            txtroom.Text = reader.GetString("Classroom");
                            ScMadd.Enabled = false;
                        }
                        else {
                            ScMadd.Enabled = true;
                        }
                    }
                }
            }
            Grid.BorderStyle = BorderStyle.None;
        }

        protected void ScMsearch_Click(object sender, EventArgs e)
        {
            string search = txtScMsearch.Text;
            ConnectionClass conn = new ConnectionClass();

            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();

                string selectquery;

                if (!string.IsNullOrEmpty(search))
                {
                    selectquery = $"SELECT * FROM sectionmanagement WHERE Section LIKE '%{search}%'";
                }
                else
                {
                    selectquery = "SELECT * FROM sectionmanagement";
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
        protected void Sccancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Account/Logout.aspx");
        }
    }
}