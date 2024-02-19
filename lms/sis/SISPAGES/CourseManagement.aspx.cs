using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;
using System.Management;
using System.Web.DynamicData;
using System.Security.Policy;

namespace lms.sis.SISPAGES
{
    public partial class CourseManagement : System.Web.UI.Page
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
                string selectquery = "SELECT * FROM `strandmanagement`";
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

        protected void CMadd_Click(object sender, EventArgs e)
        {
            ConnectionClass conn = new ConnectionClass();
            if (SN.Text == "" || des.Text == "")
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
                        string insertQuery = "INSERT INTO strandmanagement (StrandName, Description ) VALUES (@StrandName, @Description )";
                        using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@StrandName", SN.Text);
                            command.Parameters.AddWithValue("@Description", des.Text);
                            command.ExecuteNonQuery();
                        }
                        string insertSubjectQuery = "INSERT INTO subjectdetails (subjectCode) VALUES (@StrandName)";
                        using (MySqlCommand command = new MySqlCommand(insertSubjectQuery, connection))
                        {
                            command.Parameters.AddWithValue("@StrandName", $"{SN.Text}11");
                            command.ExecuteNonQuery();
                            command.Parameters["@StrandName"].Value = $"{SN.Text}12";
                            command.ExecuteNonQuery();
                        }
                        SN.Text = "";
                        des.Text = "";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!', 'addedd successfully!', 'success')", true);

                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','Name must be unique!! Please Include (lab)/(lec) at the end ','error')", true);
                    }
                    finally
                    {
                        string selectquery = "SELECT * FROM `strandmanagement`";
                        using (MySqlCommand command = new MySqlCommand(selectquery, connection))
                        {
                            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            Grid.DataSource = dataTable;
                            Grid.DataBind();
                        }
                    }

                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        protected void CMdelete_Click(object sender, EventArgs e)
        { 
            int selectedIndex = Grid.SelectedIndex;
          
            if (selectedIndex >= 0)
            {
                string subCode = Convert.ToString(Grid.Rows[selectedIndex].Cells[2].Text);
                DataKey selectedDataKey = Grid.DataKeys[selectedIndex];
                int StrandID = Convert.ToInt32(selectedDataKey["StrandID"]);
                ConnectionClass conn = new ConnectionClass();
                using (MySqlConnection connection = conn.GetConnection())
                {

                    string deleteQuery = "DELETE FROM strandmanagement WHERE StrandID = @StrandID";
                    try
                    {
                        connection.Open();

                        using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                        {
                            command.Parameters.AddWithValue("@StrandID", StrandID);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!','Deleted!', 'success')", true);
                                SN.Text = "";
                                des.Text = "";
                            }
                            else
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','Item not found...', 'error')", true);
                            }
                        }
                        string subjectdeleteQuery = $"DELETE FROM subjectdetails WHERE subjectCode  LIKE '%{subCode}%'";
                        using (MySqlCommand command = new MySqlCommand(subjectdeleteQuery, connection))
                        {
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!','Deleted!', 'success')", true);
                                SN.Text = "";
                                des.Text = "";
                            }
                            else
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','Item not found...', 'error')", true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','error')", true);
                    }
                    finally
                    {
                        string selectquery = "SELECT * FROM `strandmanagement`";
                        using (MySqlCommand command = new MySqlCommand(selectquery, connection))
                        {
                            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            Grid.DataSource = dataTable;
                            Grid.DataBind();
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
                if (SN.Text == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Something went wrong...','Please fill out all the fields ','error')", true);
                }
            }
        }

        protected void SN_TextChanged(object sender, EventArgs e)
        {
            string search = txtCMsearch.Text;
            ConnectionClass conn = new ConnectionClass();
            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();
                if (search == "")
                {
                    string selectAllquery = "SELECT * FROM strandmanagement";

                    using (MySqlCommand command = new MySqlCommand(selectAllquery, connection))
                    {
                        command.Parameters.AddWithValue("@search", search);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        Grid.DataSource = dataTable;
                        Grid.DataBind();
                    }

                }
                connection.Close();
                connection.Dispose();
            }

        }

        protected void CMsearch_Click(object sender, EventArgs e)
        {
            string search = txtCMsearch.Text;
            ConnectionClass conn = new ConnectionClass();

            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();

                string selectquery;

                if (!string.IsNullOrEmpty(search))
                {
                    selectquery = "SELECT * FROM strandmanagement WHERE StrandName = @search";
                }
                else
                {
                    selectquery = "SELECT * FROM strandmanagement";
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

        protected void CMcancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }


        protected void Grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = Grid.SelectedIndex;
            DataKey selectedDataKey = Grid.DataKeys[selectedIndex];
            int StrandID = Convert.ToInt32(selectedDataKey["StrandID"]);
            ConnectionClass connection = new ConnectionClass();
            using (MySqlConnection conn = connection.GetConnection())
            {
                conn.Open();
                string select = "SELECT * FROM `strandmanagement` WHERE StrandID = @StrandID;";

                using (MySqlCommand command = new MySqlCommand(select, conn))
                {
                    command.Parameters.AddWithValue("@StrandID", StrandID);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            SN.Text = reader.GetString("StrandName");
                            des.Text = reader.GetString("Description");
                            CMadd.Enabled = false;
                        }
                        else
                        {
                            CMadd.Enabled = true;
                        }
                    }
                }
            }
            Grid.BorderStyle = BorderStyle.None;
        }

        protected void CMupdate_Click(object sender, EventArgs e)
        {
            int selectedIndex = Grid.SelectedIndex;
            if (selectedIndex >= 0)
            {
                DataKey selectedDataKey = Grid.DataKeys[selectedIndex];
                if (selectedDataKey != null)
                {
                    int StrandID = Convert.ToInt32(selectedDataKey["StrandID"]);
                    ConnectionClass conn = new ConnectionClass();
                    using (MySqlConnection connection = conn.GetConnection())
                    {
                        string updatequery = "UPDATE `strandmanagement` SET `StrandName` = @StrandName, `Description` = @Description WHERE `StrandID` = @StrandID";
                        try
                        {
                            connection.Open();
                            using (MySqlCommand command = new MySqlCommand(updatequery, connection))
                            {
                                command.Parameters.AddWithValue("@StrandName", SN.Text);
                                command.Parameters.AddWithValue("@Description", des.Text);
                                command.Parameters.AddWithValue("@StrandID", StrandID);

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
                            SN.Text = "";
                            des.Text = "";
                            CMadd.Enabled = true;

                        }
                    }
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



