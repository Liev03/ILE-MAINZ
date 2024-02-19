using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using K4os.Compression.LZ4.Internal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.sis.SISPAGES
{
    public partial class adminCalendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {        
            if (!IsPostBack)
            {
                LoadEvents();
            }
        }
        protected void LoadEvents()
        {
            LoadEventsFromDatabase();
            BindGrid();
        }
        private void LoadEventsFromDatabase()
        {
            ConnectionClass conn = new ConnectionClass();
            List<Event> eventsList = new List<Event>();

            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();

                try
                {
                    string select = "SELECT * FROM `events`";

                    using (MySqlCommand cmd = new MySqlCommand(select, connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string title = reader["title"].ToString();
                                    DateTime start = Convert.ToDateTime(reader["start"]);
                                    string description = reader["description"].ToString();

                                    eventsList.Add(new Event { Title = title, Date = start, Description = description });
                                }
                            }
                        }
                    }
                    ViewState["EventsList"] = eventsList;
                    BindGrid();
                }
                catch (Exception ex)
                {
                    txtEventTitle.Text = ex.Message;
                }
            }
        }
        protected void BindGrid()
        {
            ConnectionClass conn = new ConnectionClass();
            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();
                string selectquery = "SELECT * FROM `events`";
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
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            List<Event> eventsList = ViewState["EventsList"] as List<Event>;

            if (eventsList != null)
            {
                foreach (Event ev in eventsList)
                {
                    if (e.Day.Date == ev.Date.Date)
                    {
                        ApplyStyleBasedOnData(ev, e.Cell);
                        e.Cell.ToolTip = ev.Description;
                        e.Cell.Text = ev.Title;
                    }
                }
            }
        }
        protected void Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DeleteEventFromDatabase(e);
        }
        protected void Grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Grid.EditIndex = e.NewEditIndex;
            LoadEvents();
        }
        private void DeleteEventFromDatabase(GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(Grid.DataKeys[e.RowIndex].Value.ToString());
            ConnectionClass conn = new ConnectionClass();

            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM `events` WHERE `id` ='" + id + "'", connection);
                int t = cmd.ExecuteNonQuery();
                if (t > 0)
                {
                    Grid.EditIndex = -1;
                    LoadEvents();
                }
            }
            ShowSweetAlert("Success", "Event deleted successfully!", SweetAlertMessageType.Success);
        }
        protected void btnAddEvent_Click(object sender, EventArgs e)
        {
            AddEventToDatabase();
        }
        private void AddEventToDatabase()
        {
            string title = txtEventTitle.Text;
            string description = txtEventDescription.Text;
            DateTime selectedDate = Calendar1.SelectedDate;
            ConnectionClass conn = new ConnectionClass();

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || Calendar1.SelectedDate == DateTime.MinValue)
            {
                ShowSweetAlert("Warning", "Please enter both title, description, and select a date from the calendar.", SweetAlertMessageType.Warning);
                return;
            }

            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();
                try
                {
                    string insert = "INSERT INTO `events`(`title`, `start`, `description`) VALUES (@value1, @value2, @value3)";

                    using (MySqlCommand cmd = new MySqlCommand(insert, connection))
                    {
                        cmd.Parameters.AddWithValue("@value1", title);
                        cmd.Parameters.AddWithValue("@value2", selectedDate);
                        cmd.Parameters.AddWithValue("@value3", description);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", $"showAlert('{ex.Message}');", true);
                }
            }

            LoadEvents();
            ShowSweetAlert("Success", "Event added successfully!", SweetAlertMessageType.Success);
        }
        private void UpdateEventInDatabase(int id, string title, string description)
        {
            ConnectionClass conn = new ConnectionClass();
            using (MySqlConnection connection = conn.GetConnection())
            {
                connection.Open();
                try
                {
                    string updateQuery = "UPDATE `events` SET `title` = @title, `description` = @description WHERE `id` = @id";

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    ShowSweetAlert("Error", ex.Message, SweetAlertMessageType.Error);
                }
            }
        }
        private void ApplyStyleBasedOnData(Event eventData, TableCell cell)
        {
            if (eventData.Description.ToLower().Contains("important"))
            {
                cell.Style.Add("box-shadow", "inset 0px 0px 5px red");
                cell.ForeColor = Color.Red;
            }
            else if (IsCloseToPresent(eventData.Date))
            {
                cell.Style.Add("box-shadow", "inset 0px 0px 5px orangeRed");
                cell.ForeColor = Color.OrangeRed;
            }
            else
            {
                cell.Style.Add("box-shadow", "inset 0px 0px 5px limeGreen");
                cell.ForeColor = Color.LimeGreen;
            }
        }
        private bool IsCloseToPresent(DateTime eventDate)
        {
            TimeSpan threshold = TimeSpan.FromDays(7);
            return Math.Abs((DateTime.Now - eventDate).TotalDays) <= threshold.TotalDays;
        }

        private bool EventExists(DateTime date)
        {
            List<string> eventDataList = new List<string>();
            if (!string.IsNullOrEmpty(eventDataHiddenField.Value))
            {
                eventDataList = eventDataHiddenField.Value.Split(',').ToList();
            }
            return eventDataList.Contains(date.ToShortDateString());
        }

        protected void Grid_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
           
            LoadEvents();
            ShowSweetAlert("Success", "Event deleted successfully!", SweetAlertMessageType.Success);
        }

        protected void Grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(Grid.DataKeys[e.RowIndex].Value.ToString());
            string title = ((TextBox)Grid.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string desc = ((TextBox)Grid.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            UpdateEventInDatabase(id, title, desc);
            Grid.EditIndex = -1;
            LoadEvents();
            ShowSweetAlert("Success", "Event updated successfully!", SweetAlertMessageType.Success);
        }
        private void RegisterSweetAlertScript(string script)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "sweetAlert", script, true);
        }
        protected void ShowSweetAlert(string title, string message, SweetAlertMessageType type)
        {
            string script = $"Swal.fire({{ title: '{title}', text: '{message}', icon: '{type.ToString().ToLower()}' }});";
            RegisterSweetAlertScript(script);
        }
        public enum SweetAlertMessageType
        {
            Success,
            Error,
            Warning,
            Info
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
    }
}

