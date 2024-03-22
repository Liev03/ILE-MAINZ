<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MakeRequest.aspx.cs" Inherits="lms.LearningHub.MakeRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Make Request</title>
    <link href="CSS/Dashboard.css" rel="stylesheet" />
    <link href="CSS/MakeRequest.css" rel="stylesheet" />
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
                <br />
                <div class="PostRequestHolder">
                    <div class="TopPart">
                        <div class="ImageContainer">
                            <asp:Image ID="ImagePF" runat="server" class="RequestImage" CssClass="ImagePF" />
                        </div>
                        <div class="HoldInfos">
                            <asp:Label ID="Name" runat="server" Text="#" class="InfoLabel"></asp:Label>
                            <asp:Label ID="SID" runat="server" Text="#" class="InfoLabel"></asp:Label>
                            <asp:Label ID="Email" runat="server" Text="#" class="InfoLabel"></asp:Label>
                            <asp:Label ID="Contact" runat="server" Text="#" class="InfoLabel"></asp:Label>
                    </div>
                        </div>
                    <%--Bottom part na dito--%>
                    <div class="BottomPart">
                        <div class="Looking">
                            <p>Looking for</p>

                            <asp:RadioButton ID="ReqTutee" runat="server" Text="Tutee" GroupName="LookingFor" value="Tutee" />
                            <asp:RadioButton ID="ReqTutor" runat="server" Text="Tutor" GroupName="LookingFor" value="Tutor" />

                        </div>
                        <div class="Strand" runat="server" id="StrandRadios">
                            <p>Strand</p>

                            <asp:RadioButton ID="ReqStem" runat="server" Text="STEM" GroupName="Strand" value="STEM" />
                            <asp:RadioButton ID="ReqABM" runat="server" Text="ABM" GroupName="Strand" value="ABM" />
                            <asp:RadioButton ID="ReqHUMSS" runat="server" Text="HUMSS" GroupName="Strand" value="HUMSS" />
                            <asp:RadioButton ID="ReqGAS" runat="server" Text="GAS" GroupName="Strand" value="GAS" />

                        </div>

                        <div class="YearLevel" runat="server" id="YearLevelRadios">
                            <p>Year Level</p>
                            <asp:RadioButton ID="ReqFirstYear" runat="server" Text="First Year" GroupName="YearLevel" value="First Year" />
                            <asp:RadioButton ID="ReqSecondYear" runat="server" Text="Second Year" GroupName="YearLevel" value="Second Year" />
                        </div>

                        <div class="Avail">
                            <p>Availability</p>
                            <asp:CheckBox ID="ReqSun" runat="server" Text="Sunday" value="Sunday" />
                            <asp:CheckBox ID="ReqMon" runat="server" Text="Monday" value="Monday" />
                            <asp:CheckBox ID="ReqTues" runat="server" Text="Tuesday" value="Tuesday" />
                            <asp:CheckBox ID="ReqWed" runat="server" Text="Wednesday" value="Wednesday" />
                            <asp:CheckBox ID="ReqThur" runat="server" Text="Thursday" value="Thursday" />
                            <asp:CheckBox ID="ReqFri" runat="server" Text="Friday" value="Friday" />
                            <asp:CheckBox ID="ReqSat" runat="server" Text="Saturday" value="Saturday" />
                        </div>
                        <div class="Location">
                            <p>Location</p>
                            <asp:CheckBox ID="ReqHome" runat="server" Text="Home" value="Home" />
                            <asp:CheckBox ID="ReqSchool" runat="server" Text="School" value="School" />
                            <asp:CheckBox ID="ReqPublic" runat="server" Text="Public Place" value="Public Place" />
                        </div>

                        <asp:Button ID="ReqSubmit" runat="server" OnClick="ReqSubmit_Click" Text="Submit" />
                    </div>
                    <%--Here end--%>
                </div>

            
            </div>
        </div>
    </form>
</body>
</html>
