<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="lms.LearningHub.Account" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Account</title>
    <link href="CSS/Dashboard.css" rel="stylesheet" />
    <link href="CSS/Account.css" rel="stylesheet" />
    <link rel="icon" href="../Resources/Novaliches Senior High School.png" type="image/x-icon" />
    <style>
        .hidden-form {
            display: none;
        }
    </style>
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

                <div class="GrandHolder">
                    <div class="TopPart">
                        <div class="ImageHolder">
                            <asp:Image ID="ImagePF" runat="server" CssClass="ImagePF" />
                        </div>
                        <div class="InfoHolder">
                            <asp:Label ID="InfoName" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="InfoStudId" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="InfoAge" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="InfoSex" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="InfoLocation" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="InfoYearLvl" runat="server" Text="Label"></asp:Label>
                        </div>
                        <div class="ContactHolder">
                            <h2>Contacts</h2>
                            <asp:Label ID="ContactEmail" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="ContactNumber" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="ContactSocmed" runat="server" Text="Label"></asp:Label>
                        </div>
                        <div class="Logout">
                            <%-- <asp:Button class="blog" ID="logout" runat="server" Text="Logout" OnClick="logout_Click"/>--%>
                        </div>
                    </div>
                    <div class="BottomPart">
                        <h1>Bio</h1>
                        <asp:Label ID="ContactBioLabel" runat="server" Text="Label"></asp:Label>
                        <asp:Button ID="editBioButton" runat="server" Text="Edit Bio" OnClientClick="showEditBioForm(); return false;" />
                        <div id="editBioForm" class="hidden-form" runat="server">
                            <asp:TextBox ID="BioTextarea" runat="server" TextMode="MultiLine" Rows="5" Columns="40"></asp:TextBox>
                            <br />
                            <asp:Button ID="saveButton" runat="server" Text="Save" OnClick="SaveButton_Click" OnClientClick="hideEditBioForm();" />
                        </div>
                    </div>
                </div>
                <script>
                    function showEditBioForm() {
                        var editBioForm = document.getElementById('<%= editBioForm.ClientID %>');
                        if (editBioForm) {
                            editBioForm.style.display = 'block';
                        }
                    }

                    function hideEditBioForm() {
                        var editBioForm = document.getElementById('<%= editBioForm.ClientID %>');
                        if (editBioForm) {
                            editBioForm.style.display = 'none';
                        }
                    }
                </script>

            </div>
        </div>
    </form>
</body>
</html>
