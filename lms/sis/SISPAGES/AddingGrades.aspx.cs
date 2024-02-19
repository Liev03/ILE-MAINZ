using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.sis.SISPAGES
{
    public partial class AddingGrades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int currentYear = DateTime.Now.Year;
            int numberOfYears = 1;
            sy.Text = ($"{currentYear}-{currentYear + numberOfYears}");

            int rowCount = Convert.ToInt32(ViewState["RowCount"]);
            for (int i = 1; i <= rowCount; i++)
            {
                TableRow newRow = new TableRow();

                TableCell cell1 = new TableCell();
                TextBox newTextBoxSubject = new TextBox();
                newTextBoxSubject.ID = "TextBoxSubject" + i;
                newTextBoxSubject.CssClass = "form-control w-100 bg-white";
                newTextBoxSubject.ReadOnly = true;
                cell1.Controls.Add(newTextBoxSubject);

                TableCell cell2 = new TableCell();
                TextBox newGrade = new TextBox();
                newGrade.ID = "Grade" + i;
                newGrade.CssClass = "form-control w-100 requires";
                newGrade.TextMode = TextBoxMode.Number;
                newGrade.TextChanged += Process_Click;
                cell2.Controls.Add(newGrade);

                TableCell cell3 = new TableCell();
                TextBox newRemarks = new TextBox();
                newRemarks.ID = "Remarks" + i;
                newRemarks.ReadOnly = true;
                newRemarks.CssClass = "form-control w-100 bg-white";
                cell3.Controls.Add(newRemarks);

                newRow.Cells.Add(cell1);
                newRow.Cells.Add(cell2);
                newRow.Cells.Add(cell3);

                table.Rows.Add(newRow);
            }

        }
        private void ClearDynamicControls()
        {
            table.Rows.Clear();
            ViewState["Subjects"] = null;
            ViewState["RowCount"] = 0;
        }
        protected void AGsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAGsearch.Text))
                {
                    ClearDynamicControls();
                    return;
                }
                else
                {
                    long search = Convert.ToInt32(txtAGsearch.Text);
                    ConnectionClass conn = new ConnectionClass();
                    using (MySqlConnection connection = conn.GetConnection())
                    {
                        connection.Open();
                        string selectquery = "SELECT `studentId`, `section`, `currentSemester`, `yearlevel`, `strand` FROM `academicinformation` WHERE studentId = @search";
                        string selectquery2 = "SELECT `name` FROM `student_info` WHERE studentId = @search";

                        using (MySqlCommand command = new MySqlCommand(selectquery2, connection))
                        {
                            command.Parameters.AddWithValue("@search", search);
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    ADname.Text = reader.GetString("name");
                                }
                                else
                                {
                                    ClearDynamicControls();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", "swal('Error!', 'Student Not Found.', 'error');", true);
                                    return;
                                }
                            }
                        }
                        using (MySqlCommand command = new MySqlCommand(selectquery, connection))
                        {
                            command.Parameters.AddWithValue("@search", search);
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    ADid.Text = reader.GetString("studentId");
                                    ADsec.Text = reader.GetString("section");
                                    sem.Text = reader.GetString("currentSemester");
                                    year.Text = reader.GetString("yearlevel");
                                    strandtxt.Text = reader.GetString("strand");
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", "swal('Error!', 'Student Academic Info Not Found!Please Make Sure The Student is Registered in Class', 'error');", true);
                                    return;
                                }
                            }
                        }
                        string selectQuery = (year.Text == "11") ? "SELECT subjectCode, subjectTitle FROM grade11subjects" : "SELECT subjectCode, subjectTitle FROM grade12subjects";
                        selectQuery += " WHERE subjectCode LIKE @strand AND semester LIKE @sem";
                        using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                        {
                            command.Parameters.AddWithValue("@strand", "%" + strandtxt.Text + "%");
                            command.Parameters.AddWithValue("@sem", "%" + sem.Text + "%");
                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);
                                ViewState["Subjects"] = dataTable.AsEnumerable().Select(row => row.Field<string>("subjectTitle")).ToList();
                                int rowCount = dataTable.Rows.Count;
                                for (int i = 1; i <= rowCount; i++)
                                {
                                    TableRow newRow = new TableRow();

                                    TableCell cell1 = new TableCell();
                                    TextBox newTextBoxSubject = new TextBox();
                                    newTextBoxSubject.ID = "TextBoxSubject" + i;
                                    newTextBoxSubject.CssClass = "form-control w-100";
                                    newTextBoxSubject.ReadOnly = true;
                                    newTextBoxSubject.Text = dataTable.Rows[i - 1]["subjectTitle"].ToString();
                                    cell1.Controls.Add(newTextBoxSubject);

                                    TableCell cell2 = new TableCell();
                                    TextBox newGrade = new TextBox();
                                    newGrade.ID = "Grade" + i;
                                    newGrade.CssClass = "form-control w-100";
                                    newGrade.TextMode = TextBoxMode.Number;
                                    newGrade.TextChanged += Process_Click;
                                    cell2.Controls.Add(newGrade);

                                    TableCell cell3 = new TableCell();
                                    TextBox newRemarks = new TextBox();
                                    newRemarks.ID = "Remarks" + i;
                                    newRemarks.ReadOnly = true;
                                    newRemarks.CssClass = "form-control w-100 bg-white";
                                    cell3.Controls.Add(newRemarks);

                                    newRow.Cells.Add(cell1);
                                    newRow.Cells.Add(cell2);
                                    newRow.Cells.Add(cell3);

                                    table.Rows.Add(newRow);
                                }
                                ViewState["RowCount"] = rowCount;
                            }
                        }
                    }
                }
            }
            catch (FormatException)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", "swal('Error!', 'Please enter a valid numeric search value.', 'error');", true);
                return;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", $"swal('Error!', 'An exception occurred: {ex.Message}', 'error');", true);
            }
        }
        protected void AGsave_Click(object sender, EventArgs e)
        {
            ConnectionClass conn = new ConnectionClass();
            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();
                string checkQuery = "SELECT * FROM studentgrade WHERE studentID = @studentID AND semester = @semester AND SchoolYear = @schoolYear AND year = @year ";

                using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@studentID", ADid.Text);
                    checkCommand.Parameters.AddWithValue("@year", year.Text);
                    checkCommand.Parameters.AddWithValue("@semester", sem.Text);
                    checkCommand.Parameters.AddWithValue("@schoolYear", sy.Text);

                    using (MySqlDataReader reader = checkCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ADid.Text = reader.GetString("studentId");
                            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", "swal('Error!', 'Grades for this student, semester, and school year already exist.', 'error');", true);
                            return;
                        }
                    }
                }
                string insertStudInfoQuery = "INSERT INTO `studentgrade`(`studentID`, `subject`, `grade`, `remarks`, `semester`, `year`, `SchoolYear`) VALUES (@value1, @value2, @value3, @value4, @value5,@value6,@SchoolYear)";
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(insertStudInfoQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@value1", ADid.Text);
                        cmd.Parameters.Add("@value2", MySqlDbType.String);
                        cmd.Parameters.Add("@value3", MySqlDbType.Float);
                        cmd.Parameters.Add("@value4", MySqlDbType.String);
                        cmd.Parameters.AddWithValue("@value5", sem.Text);
                        cmd.Parameters.AddWithValue("@value6", year.Text);
                        cmd.Parameters.AddWithValue("@SchoolYear", sy.Text);

                        int rowIndex = 0;
                        foreach (TableRow row in table.Rows)
                        {
                            TextBox subject = row.Cells[0].Controls[0] as TextBox;
                            TextBox gradeTextBox = row.Cells[1].Controls[0] as TextBox;
                            TextBox remarksTextBox = row.Cells[2].Controls[0] as TextBox;

                            if (string.IsNullOrEmpty(subject.Text) || string.IsNullOrEmpty(gradeTextBox.Text))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", "swal('Error!', 'Please fill in all required fields.', 'error');", true);
                                return;
                            }
                            int gradeValue;
                            if (int.TryParse(gradeTextBox.Text, out gradeValue))
                            {
                                if (gradeValue >= 0 && gradeValue <= 100)
                                {
                                    if (remarksTextBox.Text.Trim().ToLower() != "Invalid")
                                    {
                                        cmd.Parameters["@value2"].Value = subject.Text;
                                        cmd.Parameters["@value3"].Value = float.Parse(gradeTextBox.Text);
                                        cmd.Parameters["@value4"].Value = remarksTextBox.Text;
                                        int rowsAffected = cmd.ExecuteNonQuery();
                                        rowIndex++;
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", "swal('Error!', 'Invalid value in remarks.', 'error');", true);
                                        return;
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", "swal('Error!', 'Please enter valid grades between 0 and 100.', 'error');", true);
                                    return;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", "swal('Error!', 'Please enter valid numeric grades.', 'error');", true);
                                return;
                            }
                        }

                        ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", "swal('Success!', 'Grades saved successfully.', 'success');", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", $"swal('Error!', 'An exception occurred: {ex.Message}', 'error');", true);
                }
            }
        }
        protected void Process_Click(object sender, EventArgs e)
        {
            TextBox gradeTextBox = sender as TextBox;
            if (gradeTextBox != null)
            {
                int rowIndex = int.Parse(gradeTextBox.ID.Substring("Grade".Length));
                TextBox remarksTextBox = table.FindControl("Remarks" + rowIndex) as TextBox;
                TextBox subjectsTextBox = table.FindControl("TextBoxSubject" + rowIndex) as TextBox;
                if (remarksTextBox != null && subjectsTextBox != null)
                {
                    int gradeValue;
                    if (int.TryParse(gradeTextBox.Text, out gradeValue))
                    {
                        remarksTextBox.Text = (gradeValue >= 75 && gradeValue <= 100) ? "Pass" : (gradeValue < 75) ? "Fail" : "Invalid";

                        List<string> subjects = ViewState["Subjects"] as List<string>;

                        if (subjects != null && rowIndex <= subjects.Count)
                        {
                            subjectsTextBox.Text = subjects[rowIndex - 1];
                        }
                    }
                }
            }
        }
        protected void AGcancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Account/Logout.aspx");
        }
        string upSem;
        string upYear;

        protected void updateSearch_TextChanged(object sender, EventArgs e)
        {
            BindGridView();
        }
        protected void btnUpdateGrade_Click(object sender, EventArgs e)
        {
            BindGridView();
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
        }

        private void BindGridView()
        {
            ConnectionClass conn = new ConnectionClass();

            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();
                string academicInfoQuery = "SELECT * FROM academicinformation WHERE studentId = @studentId";

                using (MySqlCommand academicInfoCommand = new MySqlCommand(academicInfoQuery, connection))
                {
                    academicInfoCommand.Parameters.AddWithValue("@studentId", updateSearch.Text);

                    using (MySqlDataReader reader = academicInfoCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                upSem = reader.GetString(reader.GetOrdinal("currentSemester"));
                                upYear = reader.GetString(reader.GetOrdinal("yearlevel"));
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "NoDataAlert", "swal('Warning', 'No academic data found for the student.', 'warning');", true);
                            return;
                        }
                    }
                }
                string selectGrade = "SELECT * FROM studentgrade WHERE studentID = @studentId AND semester = @semester AND year = @year ";
                using (MySqlCommand gradeCommand = new MySqlCommand(selectGrade, connection))
                {
                    gradeCommand.Parameters.AddWithValue("@studentId", updateSearch.Text);
                    gradeCommand.Parameters.AddWithValue("@semester", upSem);
                    gradeCommand.Parameters.AddWithValue("@year", upYear);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(gradeCommand);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ConnectionClass conn = new ConnectionClass();
            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();
                try
                {
                    int studentID;
                    if (int.TryParse(GridView1.DataKeys[e.RowIndex].Value.ToString(), out studentID))
                    {
                        GridViewRow row = GridView1.Rows[e.RowIndex];
                        string Grade = ((TextBox)row.Cells[3].Controls[0]).Text;
                        string sub = ((TextBox)row.Cells[2].Controls[0]).Text;
                        if (int.TryParse(Grade, out int gradeValue) && gradeValue > 100)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Error', 'Invalid Grade.', 'error');", true);
                            ((TextBox)row.Cells[4].Controls[0]).Text = "Invalid";
                            return;
                        }
                        string updateQuery = "UPDATE `studentgrade` SET `grade` = @grade WHERE `studentID` = @studentID AND `subject` = @sub";
                        using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@studentID", studentID);
                            cmd.Parameters.AddWithValue("@grade", Grade);
                            cmd.Parameters.AddWithValue("@sub", sub);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", "swal('Success!', 'Grades saved successfully.', 'success');", true);
                                GridView1.EditIndex = -1;
                                BindGridView();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Error', 'Update failed. No rows affected.', 'error');", true);
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Error', 'Invalid student ID format.', 'error');", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Error', '{ex.Message}', 'error');", true);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        protected void Grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                GridView1.EditIndex = -1;
                BindGridView();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Error', '{ex.Message}', 'error');", true);
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                foreach (DataControlFieldCell cell in e.Row.Cells)
                {
                    if (cell.ContainingField is BoundField)
                    {
                        BoundField boundField = (BoundField)cell.ContainingField;
                        if (boundField.HeaderText == "subject" ||
                            boundField.HeaderText == "semester" ||
                            boundField.HeaderText == "year" ||
                            boundField.HeaderText == "SchoolYear" ||
                            boundField.HeaderText == "remarks")
                        {
                            TextBox textBox = (TextBox)cell.Controls[0];
                            textBox.ReadOnly = true;
                        }
                    }
                }
            }
        }

    }
}
