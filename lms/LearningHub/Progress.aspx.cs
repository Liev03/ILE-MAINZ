using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Diagnostics;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System.IO;
using iText.Layout;
using Paragraph = iText.Layout.Element.Paragraph;


namespace lms.LearningHub
{
    public partial class Progress : System.Web.UI.Page
    {
        private string tid
        {
            get { return ViewState["TransactionID"] as string; }
            set { ViewState["TransactionID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UID"] == null)
                {
                    Response.Redirect("/Account/Login.aspx");
                    return;
                }

                BindProgressGridView();
            }
        }
        protected void BindProgressGridView()
        {
            string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";
            string uid = Session["UID"].ToString();
            int sessionKey = Convert.ToInt32(Session["SessionKey"]);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query;

                if (sessionKey == 1)
                {

                    query = "SELECT t.tid AS TransactionID, " +
                            "CASE WHEN b.role = 'Tutor' THEN ut.name ELSE tutor.name END AS TuteeName, " +
                            "CASE WHEN b.role = 'Tutee' THEN ut.name ELSE tutor.name END AS TutorName, " +
                            "CASE WHEN b.role = 'Tutor' THEN ut.studentId ELSE tutor.studid END AS TuteeStudentID, " +
                            "CASE WHEN b.role = 'Tutee' THEN ut.studentId ELSE tutor.studid END AS TutorStudentID, " +
                            "b.yearlevel AS TuteeYearLevel, b.strand AS TuteeStrand, " +
                            "b.availability AS TutorAvailability, b.location AS TutorLocation, " +
                            "t.days, " +
                            "t.progress " +
                            "FROM transaction t " +
                            "INNER JOIN bulletin b ON t.requestor = b.rid OR t.client = b.rid " +
                            "INNER JOIN student_info ut ON t.client = ut.uid " +
                            "LEFT JOIN student_info tutor ON b.uid = tutor.uid";
                }
                else
                {

                    query = "SELECT t.tid AS TransactionID, " +
                            "CASE WHEN b.role = 'Tutor' THEN ut.name ELSE tutor.name END AS TuteeName, " +
                            "CASE WHEN b.role = 'Tutee' THEN ut.name ELSE tutor.name END AS TutorName, " +
                            "CASE WHEN b.role = 'Tutor' THEN ut.studentId ELSE tutor.studentId END AS TuteeStudentID, " +
                            "CASE WHEN b.role = 'Tutee' THEN ut.studentId ELSE tutor.studentId END AS TutorStudentID, " +
                            "b.yearlevel AS TuteeYearLevel, b.strand AS TuteeStrand, " +
                            "b.availability AS TutorAvailability, b.location AS TutorLocation, " +
                            "t.days, " +
                            "t.progress " +
                            "FROM transaction t " +
                            "INNER JOIN bulletin b ON t.requestor = b.rid OR t.client = b.rid " +
                            "INNER JOIN student_info ut ON t.client = ut.uid " +
                            "LEFT JOIN student_info tutor ON b.uid = tutor.uid " +
                            "WHERE b.uid = @UID OR t.requestor = @UID OR t.client = @UID";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    if (sessionKey != 1)
                    {
                        cmd.Parameters.AddWithValue("@UID", uid);
                    }

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);



                        foreach (DataRow row in dt.Rows)
                        {

                            row.SetField("days", $"{row["days"]}/14");
                            row.SetField("progress", row["progress"].ToString());
                        }

                        progressGridView.DataSource = dt;
                        progressGridView.DataBind();
                    }
                }
            }
        }
        private string GetStudentIDByRID(string rid, string role)
        {
            string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string columnName = role.ToLower() == "tutee" ? "requestor" : "client";
                string query = $"SELECT studentId FROM student_info WHERE uid = (SELECT {columnName} FROM transaction WHERE tid = @RID)";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@RID", rid);
                    return cmd.ExecuteScalar()?.ToString();
                }
            }
        }
        private string GetStudentNameByStudentID(string studentID)
        {
            string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT name FROM student_info WHERE studentId = @StudentID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    return cmd.ExecuteScalar()?.ToString();
                }
            }
        }
        protected void progressGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MoreCommand")
            {

                tid = e.CommandArgument.ToString();

                ScriptManager.RegisterStartupScript(this, GetType(), "ShowTransactionID", $"console.log('TransactionID: {tid}');", true);


                hidediv.Style["display"] = "none";

                lblTopMiddle.Text = "";
                lblCenter.Text = "";
                HtmlGenericControl additionalContent = (HtmlGenericControl)FindControl("additionalContent");
                if (additionalContent != null)
                {
                    additionalContent.Style["display"] = additionalContent.Style["display"] == "none" ? "block" : "none";
                }
            }
        }
        protected void progressGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfRowIndex = (HiddenField)e.Row.FindControl("hfRowIndex");
                hfRowIndex.Value = e.Row.RowIndex.ToString();
            }
        }
        protected void UpdateProgressToComplete(string tid)
        {
            string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE transaction SET progress = 'Complete' WHERE tid = @TID";

                using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@TID", tid);
                    cmd.ExecuteNonQuery();
                }
            }
            BindProgressGridView();
        }
        protected void Details_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowTransactionID", $"console.log('TransactionID: {tid}');", true);

            Button clickedButton = (Button)sender;
            string dayInformation = clickedButton.CommandArgument;

            string currentUserID = Session["UID"]?.ToString();

            if (!string.IsNullOrEmpty(tid) && !string.IsNullOrEmpty(currentUserID))
            {
                bool isCurrentUserTutor = IsCurrentUserTutor(tid, currentUserID);

                if (isCurrentUserTutor)
                {
                    btnEdit.Style["display"] = "block";
                    btnComplete.Style["display"] = "block";
                }
                else
                {
                    btnEdit.Style["display"] = "none";
                    btnComplete.Style["display"] = "none";
                }
            }


            HtmlGenericControl hidediv = (HtmlGenericControl)FindControl("hidediv");
            hidediv.Style["display"] = "block";

            CenterTextarea.Text = "";


            Label lblTopMiddle = (Label)hidediv.FindControl("lblTopMiddle");
            if (lblTopMiddle != null)
            {
                lblTopMiddle.Text = dayInformation;
            }


            if (!string.IsNullOrEmpty(tid))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ConsoleLogDetailsClick",
                    "console.log('Details_Click triggered.');", true);

                string buttonNumber = ((Button)sender).ID.Replace("btnDetails", "");


                string columnName = "day" + buttonNumber;

                ViewState["SelectedColumnName"] = columnName;
                ViewState["SelectedTID"] = tid;

                RetrieveDayDetailsFromDatabase(tid, columnName);


                lblCenter.Visible = true;
                editCenterForm.Style["display"] = "none";

                string columnValue = RetrieveDayDetailsFromDatabase(tid, columnName);

                bool isColumnEmpty = string.IsNullOrEmpty(columnValue);
                btnComplete.Visible = true;
                btnEdit.Visible = isColumnEmpty;

            }
        }
        private bool IsCurrentUserTutor(string transactionID, string currentUserID)
        {
            string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT tutor FROM `transaction` WHERE tid = @TransactionID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TransactionID", transactionID);

                    object tutorID = cmd.ExecuteScalar();

                    if (tutorID != null && tutorID.ToString() == currentUserID)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        private string RetrieveDayDetailsFromDatabase(string tid, string columnName)
        {
            try
            {
                string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT {columnName} FROM learning WHERE tid = @TID";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@TID", tid);

                        ScriptManager.RegisterStartupScript(this, GetType(), "ConsoleLogRetrieveDay",
                            "console.log('Retrieve Day triggered.');", true);

                        foreach (MySqlParameter parameter in cmd.Parameters)
                        {
                            Console.WriteLine($"Parameter {parameter.ParameterName}: {parameter.Value}");
                        }

                        object result = cmd.ExecuteScalar();


                        Console.WriteLine($"Result: {result}");

                        if (result != null)
                        {

                            lblCenter.Text = result.ToString();


                            lblCenter.Visible = true;
                            editCenterForm.Style["display"] = "none";
                        }
                        else
                        {

                            Console.WriteLine("No data found for the specified parameters.");
                        }


                        return result?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
        protected void Edit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewState["SelectedColumnName"] as string) && !string.IsNullOrEmpty(ViewState["SelectedTID"] as string))
            {

                string columnName = ViewState["SelectedColumnName"].ToString();
                string tid = ViewState["SelectedTID"].ToString();


                string columnValue = RetrieveDayDetailsFromDatabase(tid, columnName);


                btnEdit.Visible = string.IsNullOrEmpty(columnValue);

            }
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            string columnName = ViewState["SelectedColumnName"] as string;
            string tid = ViewState["SelectedTID"] as string;


            string newText = CenterTextarea.Text.Trim();

            if (string.IsNullOrEmpty(newText))
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "EmptyTextareaAlert", "alert('Text Area is empty. The details will not be saved.');", true);
                return;
            }

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(tid))
            {

                UpdateDayDetailsInDatabase(tid, columnName, newText);


                CenterTextarea.Text = "";
            }
        }
        private void UpdateDayDetailsInDatabase(string tid, string columnName, string newText)
        {
            try
            {
                string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = $"UPDATE learning SET {columnName} = @NewText WHERE tid = @TID";

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@NewText", newText);
                        cmd.Parameters.AddWithValue("@TID", tid);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {

                            lblCenter.Text = newText;

                        }
                        else
                        {
                            //Incase hindi gumana SHHSHSHSHS
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // pag inubo yung codes, dito punta

            }
        }
        protected void Close_Click(object sender, EventArgs e)
        {

            additionalContent.Style["display"] = "none";
            hidediv.Style["display"] = "none";


            lblTopMiddle.Text = "";
            lblCenter.Text = "";

        }
        protected void Complete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewState["SelectedColumnName"] as string) && !string.IsNullOrEmpty(ViewState["SelectedTID"] as string))
            {
                string columnName = ViewState["SelectedColumnName"].ToString();
                string tid = ViewState["SelectedTID"].ToString();

                string buttonNumber = columnName.Replace("day", "");
                UpdateDaysInTransactionTable(tid, buttonNumber);

                string columnValue = RetrieveDayDetailsFromDatabase(tid, columnName);

                btnComplete.Visible = string.IsNullOrEmpty(columnValue);
                btnEdit.Visible = string.IsNullOrEmpty(columnValue);

                if (columnName == "day14")
                {
                    UpdateProgressToComplete(tid);
                    btnComplete.Style["display"] = "none";
                }
            }

            if (string.IsNullOrEmpty(lblCenter.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ActionDenied", "alert('Action denied.');", true);
                return;
            }
        }
        protected void UpdateDaysInTransactionTable(string tid, string buttonNumber)
        {
            try
            {
                string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();


                    string updateQuery = "UPDATE transaction SET days = @Days WHERE tid = @TID";

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                    {

                        string daysValue = buttonNumber;

                        cmd.Parameters.AddWithValue("@Days", daysValue);
                        cmd.Parameters.AddWithValue("@TID", tid);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "DaysUpdateSuccess",
                                $"console.log('Days updated successfully: {daysValue}');", true);


                            DataTable dt = progressGridView.DataSource as DataTable;

                            if (dt != null)
                            {

                                foreach (DataRow row in dt.Rows)
                                {
                                    if (row["TransactionID"].ToString() == tid)
                                    {

                                        row.SetField("days", daysValue);
                                        break;
                                    }
                                }


                                progressGridView.DataSource = dt;
                                progressGridView.DataBind();
                            }
                        }
                        else
                        {
                            // If for some reason di gumana yung update (DI MANGYAYARI KASI DI KO ALAM MANGYAYARI IF HINDI)

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Pag inubo nanaman siya
            }
            BindProgressGridView();
        }
        protected void GeneratePDF_Click(object sender, EventArgs e) //PANG PeDoFile creation, wag galawin 
        {

            using (MemoryStream ms = new MemoryStream())
            {

                using (PdfWriter writer = new PdfWriter(ms))
                {

                    using (var pdf = new PdfDocument(writer))
                    {

                        using (var document = new Document(pdf))
                        {

                            document.Add(new Paragraph($"Transaction Progress Report ({DateTime.Now})").SetBold());


                            AddGridViewToDocument(document);

                            document.Close();
                        }
                    }
                }


                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=TransactionReport.pdf");
                Response.OutputStream.Write(ms.ToArray(), 0, ms.ToArray().Length);
            }
        }
        private void AddGridViewToDocument(Document document)
        {

            foreach (GridViewRow row in progressGridView.Rows)
            {

                var transactionID = row.Cells[0].Text;
                var tuteeName = row.Cells[1].Text;
                var tutorName = row.Cells[3].Text;
                var tutorStudentID = row.Cells[4].Text;
                var tuteeStudentID = row.Cells[2].Text;
                var strand = row.Cells[6].Text;
                var yearLevel = row.Cells[5].Text;
                var availabilities = row.Cells[7].Text;
                var location = row.Cells[8].Text;
                var days = row.Cells[9].Text;
                var progress = row.Cells[10].Text;
                var transactionDate = GetTransactionDate(transactionID);


                var formattedData = $"Transaction ID: {transactionID} - Tutor Name: {tutorName} - Tutor StudentID: {tutorStudentID} - " +
                                    $"Tutee Name: {tuteeName} - Tutee StudentID: {tuteeStudentID} - Strand: {strand} - " +
                                    $"Year Level: {yearLevel} - Availabilities: {availabilities} - Location: {location} - " +
                                    $"Days: {days} - Progress: {progress} - Date: {transactionDate}";

                document.Add(new Paragraph(formattedData).SetBold());
            }
        }
        private string GetTransactionDate(string transactionID)
        {
            string connectionString = "Server=localhost;Database=learninghub;Uid=root;Pwd=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT trandate FROM transaction WHERE tid = @TransactionID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TransactionID", transactionID);

                    object result = cmd.ExecuteScalar();


                    return result != DBNull.Value ? Convert.ToDateTime(result).ToString("yyyy-MM-dd") : string.Empty;
                }
            }
        }
    }
}