<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TutorNotif.aspx.cs" Inherits="lms.LearningHub.TutorNotif" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Tutor Notification</title>
    <link href="CSS/Dashboard.css" rel="stylesheet" />
    <link href="CSS/Notif.css" rel="stylesheet" />
    <link rel="icon" href="../Resources/Novaliches Senior High School.png" type="image/x-icon" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="Dashboard">
            <div class="IconNavigation">
                <div class="Icon">
                    <img src="../Resources/Novaliches Senior High School.png" class="IconImage" />
                </div>
                <div class="Navigation">
                    <div class="dropdown">
                        <button class="dropbtn">Bulletin</button>
                        <div class="dropdown-content">
                            <a href="BulletinTutor.aspx">Tutor Bulletin</a>
                            <a href="BulletinTutee.aspx">Tutee Bulletin</a>
                        </div>
                    </div>
                    <div class="dropdown">
                        <button class="dropbtn">Notifications</button>
                        <div class="dropdown-content">
                            <a href="TutorNotif.aspx">Tutor Notification</a>
                            <a href="TuteeNotif.aspx">Tutee Notification</a>
                        </div>
                    </div>
                    <div class="dropdown">
                        <button class="dropbtn">Request</button>
                        <div class="dropdown-content">
                            <a href="MakeRequest.aspx">Make a Request</a>
                        </div>
                    </div>
                    <div class="dropdown">
                        <button class="dropbtn">Account</button>
                        <div class="dropdown-content">
                            <a href="Account.aspx">Account</a>
                            <a href="Progress.aspx">Progress Tracking</a>
                        </div>
                    </div>
                    <div class="dropdown">
                        <button class="dropbtn">LMS</button>
                        <div class="dropdown-content">
                            <a href="../Student/DashBoard.aspx">Go to LMS</a>

                        </div>
                    </div>
                </div>
            </div>
            <div class="Content">
                <div class="TitleOnly">
                    <p>TUTOR NOTIFICATION</p>
                </div>
                <div class="MainHolderNotif">
                    <asp:Repeater ID="transactionRepeater" runat="server" OnItemCommand="transactionRepeater_ItemCommand">
                        <ItemTemplate>
                            <div class="NotificationCard">

                                <asp:Label ID="tutorLabel" runat="server" Text="Tutor: "></asp:Label>
                                <asp:Label ID="tutorNameLabel" runat="server" Text='<%# Eval("TutorName") %>'></asp:Label>
                                <br />
                                <asp:Label ID="tuteeLabel" runat="server" Text="Tutee: "></asp:Label>
                                <asp:Label ID="tuteeNameLabel" runat="server" Text='<%# Eval("TuteeName") %>'></asp:Label>
                                <br />
                                <asp:Label ID="subjectLabel" runat="server" Text="Strand: "></asp:Label>
                                <asp:Label ID="subjectNameLabel" runat="server" Text='<%# Eval("Strand") %>'></asp:Label>
                                <br />

                                <div class="NotificationButton">
                                    <asp:Button ID="viewMoreButton" runat="server" Text="View More" CssClass="view-more-button"
                                        data-tutor='<%# Eval("TutorName") %>'
                                        data-tutee='<%# Eval("TuteeName") %>'
                                        data-strand='<%# Eval("Strand") %>'
                                        data-yearlevel='<%# Eval("YearLevel") %>'
                                        data-availability='<%# Eval("Availability") %>'
                                        data-location='<%# Eval("Location") %>' />
                                    <asp:Button ID="acceptButton" runat="server" Text="Accept" CssClass="accept-button"
                                        CommandName="Accept" CommandArgument='<%# Eval("NotificationID") %>' OnCommand="AcceptButton_Click"
                                        OnClientClick="return confirm('Are you sure you want to accept this notification?');" />
                                    <asp:Button ID="rejectButton" runat="server" Text="Reject" CssClass="reject-button"
                                        CommandName="Reject" CommandArgument='<%# Eval("NotificationID") %>' OnCommand="RejectButton_Click"
                                        OnClientClick="return confirm('Are you sure you want to reject this notification?');" />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div id="detailsModal" class="modal">
                        <div class="modal-content">
                            <span class="close" onclick="hideDetails()">&times;</span>
                            <div class="NotificationText">
                                <div class="label-container">
                                    <asp:Label ID="tutorLabelModal" runat="server" Text="Tutor: "></asp:Label>
                                    <asp:Label ID="tuteeLabelModal" runat="server" Text="Tutee: "></asp:Label>
                                    <asp:Label ID="strandLabelModal" runat="server" Text="Strand: "></asp:Label>

                                    <asp:Label ID="yearLevelLabelModal" runat="server" Text="Year Level: "></asp:Label>
                                    <asp:Label ID="availabilityLabelModal" runat="server" Text="Availability: "></asp:Label>
                                    <asp:Label ID="locationLabelModal" runat="server" Text="Location: "></asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var buttons = document.querySelectorAll(".view-more-button");
            buttons.forEach(function (button) {
                button.addEventListener("click", function (event) {
                    event.preventDefault();
                    var tutor = this.getAttribute("data-tutor");
                    var tutee = this.getAttribute("data-tutee");
                    var strand = this.getAttribute("data-strand");
                    var subject = this.getAttribute("data-subject");
                    var yearLevel = this.getAttribute("data-yearlevel");
                    var availability = this.getAttribute("data-availability");
                    var location = this.getAttribute("data-location");
                    showDetails(tutor, tutee, strand, subject, yearLevel, availability, location);
                });
            });
        });

        function showDetails(tutor, tutee, strand, subject, yearLevel, availability, location) {
            document.getElementById("tutorLabelModal").innerHTML = "Tutor: " + tutor;
            document.getElementById("tuteeLabelModal").innerHTML = "Tutee: " + tutee;
            document.getElementById("strandLabelModal").innerHTML = "Strand: " + strand;

            document.getElementById("yearLevelLabelModal").innerHTML = "Year Level: " + yearLevel;
            document.getElementById("availabilityLabelModal").innerHTML = "Availability: " + availability;
            document.getElementById("locationLabelModal").innerHTML = "Location: " + location;
            document.getElementById("detailsModal").style.display = "block";
            return false;
        }

        function hideDetails() {
            document.getElementById("detailsModal").style.display = "none";
        }
    </script>
</body>
</html>
