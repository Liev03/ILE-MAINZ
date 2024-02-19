using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.sis.SISPAGES
{
    public partial class SubjectModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string selectedValue = SMstrand.SelectedValue;
                SMstrand.SelectedValue = selectedValue;
            }
            ConnectionClass conn = new ConnectionClass();
            using (MySqlConnection connection = conn.GetConnection())
            {
                string subjectCodenQuery = "SELECT DISTINCT subjectCode FROM `subjectdetails`";

                using (MySqlCommand command = new MySqlCommand(subjectCodenQuery, connection))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    SMstrand.DataTextField = "subjectCode";
                    SMstrand.DataValueField = "subjectCode";
                    SMstrand.DataSource = dataTable;
                    SMstrand.DataBind();
                }
            }
        }
        protected void SMstrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void CRstrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Grid.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void Grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int number = Convert.ToInt32(Grid.DataKeys[e.RowIndex].Value.ToString());
                GridViewRow row = Grid.Rows[e.RowIndex];
                string subjectCode = ((TextBox)row.Cells[2].Controls[0]).Text;
                string subjectTitle = ((TextBox)row.Cells[3].Controls[0]).Text;
                string description = ((TextBox)row.Cells[4].Controls[0]).Text;
                string unit = ((TextBox)row.Cells[5].Controls[0]).Text;
                string semester = ((TextBox)row.Cells[6].Controls[0]).Text;

                ConnectionClass conn = new ConnectionClass();
                using (MySqlConnection connection = conn.GetConnection())
                {
                    connection.Open();
                    string updateQuery = "UPDATE subjectdetails SET subjectCode = @subjectCode,subjectTitle=@subjectTitle, description=@description, subjectUnit=@unit, semester=@semester WHERE number=@number";

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@number", number);
                        cmd.Parameters.AddWithValue("@subjectCode", subjectCode);
                        cmd.Parameters.AddWithValue("@subjectTitle", subjectTitle);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@unit", unit);
                        cmd.Parameters.AddWithValue("@semester", semester);
                        cmd.ExecuteNonQuery();
                    }
                }
                Grid.EditIndex = -1;
                BindGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success', 'Subject updated successfully!', 'success');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Error', '{ex.Message}', 'error');", true);
            }
        }
        protected void Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int number = Convert.ToInt32(Grid.DataKeys[e.RowIndex].Value.ToString());
                ConnectionClass conn = new ConnectionClass();

                using (MySqlConnection connection = conn.GetConnection())
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM subjectdetails WHERE number=@number";

                    using (MySqlCommand cmd = new MySqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@number", number);
                        cmd.ExecuteNonQuery();
                        BindGrid();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success', 'Subject deleted successfully!', 'success');", true);
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Error', '{ex.Message}', 'error');", true);
            }
        }
        protected void SubMadd_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionClass conn = new ConnectionClass();
                using (MySqlConnection connection = conn.GetConnection())
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO subjectdetails (subjectCode, subjectTitle, description, subjectUnit, semester) " +
                          "VALUES (@subjectCode, @subjectTitle, @description, @unit, @semester)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@subjectCode", SMstrand.SelectedValue);
                        cmd.Parameters.AddWithValue("@subjectTitle", subjectTitle.Text);
                        cmd.Parameters.AddWithValue("@description", description.Text);


                        if (int.TryParse(subjectUnit.Text, out int unitValue))
                        {
                            cmd.Parameters.AddWithValue("@unit", unitValue);
                            cmd.Parameters.AddWithValue("@semester", sem.SelectedValue);
                            cmd.ExecuteNonQuery();
                            if (string.IsNullOrEmpty(description.Text) && string.IsNullOrEmpty(subjectTitle.Text))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Error', 'Title and Description Cannot Be Empty', 'error');", true);
                                return;
                            }
                            else if (string.IsNullOrEmpty(sem.SelectedValue)) {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Error', 'Select a semester!', 'error');", true);
                                return;
                            }     
                            BindGrid();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success', 'Subject Added successfully!', 'success');", true);
                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Error', 'Unit should only accept numbers!', 'error');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Error', '{ex.Message}', 'error');", true);
            }

        }
        void BindGrid()
        {
            try
            {
                ConnectionClass conn = new ConnectionClass();
                using (MySqlConnection connection = conn.GetConnection())
                {
                    string sectionQuery = "SELECT * FROM `subjectdetails` WHERE subjectCode = @strand AND semester = @sem";

                    using (MySqlCommand command = new MySqlCommand(sectionQuery, connection))
                    {
                        command.Parameters.AddWithValue("@strand", SMstrand.SelectedValue);
                        command.Parameters.AddWithValue("@sem", sem.SelectedValue);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        Grid.DataSource = dataTable;
                        Grid.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Error', '{ex.Message}', 'error');", true);
            }
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Account/Logout.aspx");
        }

        protected void Grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                Grid.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Error', '{ex.Message}', 'error');", true);
            }
        }

        protected void SubMsearch_Click(object sender, EventArgs e)
        {
            string search = txtScMsearch.Text;
            ConnectionClass conn = new ConnectionClass();

            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();

                string selectquery;

                if (!string.IsNullOrEmpty(search))
                {
                    selectquery = $"SELECT * FROM subjectdetails WHERE subjectTitle LIKE '%{search}%'";
                }
                else
                {
                    selectquery = "SELECT * FROM subjectdetails";
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
    }
}
