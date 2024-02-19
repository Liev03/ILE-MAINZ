using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lms.sis.SISPAGES
{
    public partial class AcademicCalendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConnectionClass conn = new ConnectionClass();
                List<Event> eventsList = new List<Event>();

                using (MySqlConnection connection = conn.GetConnection())
                {
                    connection.Open();


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

                }

                ViewState["EventsList"] = eventsList;
            }
        }
        protected void ReadOnlyCalendar_DayRender(object sender, DayRenderEventArgs e)
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

            // Disable selection and navigation for the read-only calendar
            e.Day.IsSelectable = false;
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
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Account/Logout.aspx");
        }
    }
}