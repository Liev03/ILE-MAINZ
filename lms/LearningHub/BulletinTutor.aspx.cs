using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace lms.LearningHub
{
    public partial class BulletinTutor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    string uidValue = Session["UID"].ToString();
                    BindDataToRepeater();
                }
                else
                {

                }
            }

        }
        protected bool IsConnectButtonVisible(string rowUid)
        {
            if (Session["UID"] != null)
            {
                string sessionUid = Session["UID"].ToString();
                return rowUid != sessionUid;
            }
            return false;
        }
        protected void ConnectNow_Click(object sender, EventArgs e)
        {
            Button connectButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)connectButton.NamingContainer;
            int rid = Convert.ToInt32((item.FindControl("HiddenRid") as HiddenField).Value);

            string uid = Session["UID"].ToString();

            string connectionString = "Server=localhost;Database=learninghub;User=root;Password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO notification (Fuid, Frid) VALUES (@Fuid, @Frid)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Fuid", uid);
                    command.Parameters.AddWithValue("@Frid", rid);

                    command.ExecuteNonQuery();
                }
            }
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            BindDataToRepeater();
        }
        private void BindDataToRepeater()
        {
            string connectionString = "Server=localhost;Database=learninghub;User=root;Password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string selectedStrand = GetSelectedRadioButton("strandGroup");
                string selectedYearLevel = GetSelectedRadioButton("yearLevelGroup");
                List<string> selectedAvailability = GetSelectedCheckboxes("availGroup");
                List<string> selectedLocations = GetSelectedCheckboxes("locGroup");

                string query = "SELECT b.rid, u.uid, u.name, u.ImageName, b.looking, b.strand, b.availability, b.location, u.contact FROM bulletin b JOIN student_info u ON b.uid = u.uid WHERE b.looking = 'Tutor' AND b.visibility = ''";

                if (!string.IsNullOrEmpty(selectedStrand))
                {
                    query += $" AND b.strand = '{selectedStrand}'";
                }

                if (!string.IsNullOrEmpty(selectedYearLevel))
                {
                    query += $" AND b.yearlevel = '{selectedYearLevel}'";
                }

                if (selectedAvailability.Count > 0)
                {
                    query += " AND (";
                    for (int i = 0; i < selectedAvailability.Count; i++)
                    {
                        query += $"b.availability LIKE '%{selectedAvailability[i]}%'";
                        if (i < selectedAvailability.Count - 1)
                        {
                            query += " OR ";
                        }
                    }
                    query += ")";
                }

                if (selectedLocations.Count > 0)
                {
                    query += " AND (";
                    for (int i = 0; i < selectedLocations.Count; i++)
                    {
                        query += $"b.location LIKE '%{selectedLocations[i]}%'";
                        if (i < selectedLocations.Count - 1)
                        {
                            query += " OR ";
                        }
                    }
                    query += ")";
                }

                query += " AND b.visibility = ''";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        CardRepeater.DataSource = dataTable;
                        CardRepeater.DataBind();
                    }
                }
            }
        }
        protected string GetDirectLinkFromGoogleDrive(string googleDriveLink)
        {
            if (googleDriveLink.Contains("drive.google.com/file/d/"))
            {
                int start = googleDriveLink.IndexOf("drive.google.com/file/d/") + "drive.google.com/file/d/".Length;
                int end = googleDriveLink.IndexOf("/view");

                if (start != -1 && end != -1)
                {
                    string fileId = googleDriveLink.Substring(start, end - start);

                    return $"https://drive.google.com/uc?export=view&id={fileId}";
                }
            }

            return googleDriveLink;
        }
        private string GetSelectedRadioButton(string groupName)
        {
            foreach (Control control in Page.Controls)
            {
                if (control is HtmlForm)
                {
                    foreach (Control formControl in control.Controls)
                    {
                        if (formControl is RadioButton && (formControl as RadioButton).GroupName == groupName && (formControl as RadioButton).Checked)
                        {
                            return (formControl as RadioButton).Text;
                        }
                    }
                }
            }

            return null;
        }
        private List<string> GetSelectedCheckboxes(string checkBoxGroupName)
        {
            List<string> selectedCheckboxes = new List<string>();

            foreach (Control control in Page.Controls)
            {
                if (control is HtmlForm)
                {
                    foreach (Control formControl in control.Controls)
                    {
                        if (formControl is CheckBox && (formControl as CheckBox).Attributes["Group"] == checkBoxGroupName && (formControl as CheckBox).Checked)
                        {
                            selectedCheckboxes.Add((formControl as CheckBox).Text);
                        }
                    }
                }
            }

            return selectedCheckboxes;
        }
        protected void Clear_Click(object sender, EventArgs e)
        {
            ResetFilters();
            BindDataToRepeater();
        }
        private void ResetFilters()
        {
            ClearRadioButtonGroup("strandGroup");
            ClearRadioButtonGroup("yearLevelGroup");
            ClearCheckboxes("availGroup");
            ClearCheckboxes("locGroup");
        }
        private void ClearRadioButtonGroup(string groupName)
        {
            foreach (Control control in Page.Controls)
            {
                if (control is HtmlForm)
                {
                    foreach (Control formControl in control.Controls)
                    {
                        if (formControl is RadioButton && (formControl as RadioButton).GroupName == groupName)
                        {
                            (formControl as RadioButton).Checked = false;
                        }
                    }
                }
            }
        }
         private void ClearCheckboxes(string checkBoxGroupName)
        {
            foreach (Control control in Page.Controls)
            {
                if (control is HtmlForm)
                {
                    foreach (Control formControl in control.Controls)
                    {
                        if (formControl is CheckBox && (formControl as CheckBox).Attributes["Group"] == checkBoxGroupName)
                        {
                            (formControl as CheckBox).Checked = false;
                        }
                    }
                }
            }
        }
    }
}