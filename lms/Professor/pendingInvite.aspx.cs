using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.Professor
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InvitationData();
            }
        }
        private void InvitationData(string searchTerm = "")
        {
            string loggedInProfessorEmail = Session["LoggedInUserEmail"] as string;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT invitationid, roomid, studentid, teacherid, teacheremail, studentemail, subjectname, status FROM invitation WHERE teacheremail = @loggedInProfessorEmail AND status = 'Pending' ";

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += " AND (roomid LIKE @searchTerm OR studentid LIKE @searchTerm OR teacheremail LIKE @searchTerm OR studentemail LIKE @searchTerm OR subjectname LIKE @searchTerm)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@loggedInProfessorEmail", loggedInProfessorEmail);

                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            pendinggrv.DataSource = dataTable;
                            pendinggrv.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtsearch.Text;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                string query;
                string loggedInProfessorEmail = Session["LoggedInUserEmail"] as string;

                if (string.IsNullOrEmpty(searchTerm))
                {
                    query = "SELECT invitationid, roomid,  studentid, teacherid, teacheremail, studentemail, subjectname, status FROM invitation WHERE teacheremail = @loggedInProfessorEmail AND status = 'Pending'";
                }
                else
                {
                    query = "SELECT invitationid, roomid, studentid, teacherid, teacheremail, studentemail, subjectname, status FROM invitation WHERE teacheremail = @loggedInProfessorEmail AND status = 'Pending' AND (studentid LIKE @searchTerm OR teacheremail LIKE @searchTerm OR studentemail LIKE @searchTerm OR subjectname LIKE @searchTerm OR status LIKE @searchTerm)";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@loggedInProfessorEmail", loggedInProfessorEmail);

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    }

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        pendinggrv.DataSource = dataTable;
                        pendinggrv.DataBind();
                    }
                }
            }
        }
        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
        Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            HiddenField hf = (HiddenField)gvr.FindControl("hfTnIdPkId");
            int invitationid = int.Parse(hf.Value);

     string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string updateQuery = "UPDATE invitation SET status = 'Cancelled' WHERE invitationid = @invitationid";

        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, con))
        {
            updateCmd.Parameters.AddWithValue("@invitationid", invitationid);
            int rowsAffected = updateCmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                ShowSuccessMessage("Invitation Status Updated to 'Cancelled'");
                InvitationData();
            }
            else
            {
                ShowErrorMessage("Failed to update invitation status");
                InvitationData();
            }
        }
    }
            //Button btn = (Button)sender;
            //GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            //HiddenField hf = (HiddenField)gvr.FindControl("hfTnIdPkId");
            //int invitationid = int.Parse(hf.Value);

            //string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            //using (MySqlConnection con = new MySqlConnection(connectionString))
            //{
            //    con.Open();

            //    string deleteQuery = "DELETE FROM invitation WHERE invitationid = @invitationid";

            //    using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, con))
            //    {
            //        deleteCmd.Parameters.AddWithValue("@invitationid", invitationid);
            //        int rowsAffected = deleteCmd.ExecuteNonQuery();

            //        if (rowsAffected > 0)
            //        {
            //            ShowSuccessMessage("Row Deleted Successfully");
            //            InvitationData();

            //        }
            //        else
            //        {
            //            ShowErrorMessage("Row Deletion Failed");
            //            InvitationData();

            //        }
            //    }
            //}
        }
   
        
        private void ShowErrorMessage(string message)
        {
            string script = $"Swal.fire({{ icon: 'error', text: '{message}' }})";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script, true);
        }
        private void ShowSuccessMessage(string message)
        {
            string script = $"Swal.fire({{ icon: 'success', text: '{message}' }})";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script, true);
        }
    }
}