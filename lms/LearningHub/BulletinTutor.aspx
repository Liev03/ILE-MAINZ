<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulletinTutor.aspx.cs" Inherits="lms.LearningHub.BulletinTutor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Tutor Bulletin</title>
    <link href="CSS/Bulletin.css" rel="stylesheet" />
    <link href="CSS/Dashboard.css" rel="stylesheet" />
    <link rel="icon" href="../Resources/Novaliches Senior High School.png" type="image/x-icon" />
    <style>
        .HiddenDiv {
            display: none;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 50%;
            background-color: white;
            padding: 20px;
            z-index: 2;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

            .HiddenDiv img {
                width: 50%;
                height: auto;
            }

        .BottomLayer {
            display: none;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 50%;
            background-color: white;
            padding: 20px;
            z-index: 1;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .close {
            position: absolute;
            top: 0;
            right: 0;
            cursor: pointer;
            font-size: 20px;
            padding: 10px;
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
                <div class="MainContentHolder">
                    <div class="FilterSide">
                        <p>FILTERS </p>
                        <div class="Strands">
                            <p>Strand </p>
                            <asp:RadioButton ID="STEM" runat="server" Text="STEM" GroupName="strandGroup" />
                            <asp:RadioButton ID="ABM" runat="server" Text="ABM" GroupName="strandGroup" />
                            <asp:RadioButton ID="HUMSS" runat="server" Text="HUMSS" GroupName="strandGroup" />
                            <asp:RadioButton ID="GAS" runat="server" Text="GAS" GroupName="strandGroup" />
                        </div>
                        <div class="Yearlvl">
                            <p>Year Level </p>
                            <div class="Holder">
                                <asp:RadioButton ID="FirstLVL" runat="server" Text="First Year" GroupName="yearLevelGroup" />
                            </div>
                            <div class="Holder">
                                <asp:RadioButton ID="SecondLVL" runat="server" Text="Second Year" GroupName="yearLevelGroup" />
                            </div>
                        </div>
                        <div class="Availability">
                            <p>Availability </p>
                            <div class="Holder">
                                <asp:CheckBox ID="Monday" runat="server" Text="Monday" value="Monday" Group="availGroup" />
                            </div>
                            <div class="Holder">
                                <asp:CheckBox ID="Tuesday" runat="server" Text="Tuesday" value="Tuesday" Group="availGroup" />
                            </div>
                            <div class="Holder">
                                <asp:CheckBox ID="Wednesday" runat="server" Text="Wednesday" value="Wednesday" Group="availGroup" />
                            </div>
                            <div class="Holder">
                                <asp:CheckBox ID="Thursday" runat="server" Text="Thursday" value="Thursday" Group="availGroup" />
                            </div>
                            <div class="Holder">
                                <asp:CheckBox ID="Friday" runat="server" Text="Friday" value="Friday" Group="availGroup" />
                            </div>
                            <div class="Holder">
                                <asp:CheckBox ID="Saturday" runat="server" Text="Saturday" value="Saturday" Group="availGroup" />
                            </div>
                            <div class="Holder">
                                <asp:CheckBox ID="Sunday" runat="server" Text="Sunday" value="Sunday" Group="availGroup" />
                            </div>
                        </div>
                        <div class="Location">
                            <p>Location </p>
                            <div class="Holder">
                                <asp:CheckBox ID="Home" runat="server" Text="Home" value="Home" Group="locGroup" />
                            </div>
                            <div class="Holder">
                                <asp:CheckBox ID="School" runat="server" Text="School" value="School" Group="locGroup" />
                            </div>
                            <div class="Holder">
                                <asp:CheckBox ID="Other" runat="server" Text="Public Place" value="Public Place" Group="locGroup" />
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="ButtonLoc">
                            <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" />
                            <br />
                            <asp:Button ID="Clear" runat="server" Text="Clear" OnClick="Clear_Click" />
                        </div>
                    </div>
                    <div class="CardSide">
                        <div class="BullTitle">
                            <p>TUTOR BULLETIN</p>
                        </div>
                        <div class="CardSideContainer">
                            <asp:Repeater ID="CardRepeater" runat="server">

                                <ItemTemplate>
                                    <td class="Cards">
                                        <div class="CardDiv">
                                            <div class="CardImage">
                                                <asp:Image ID="CardDPImage" runat="server" ImageUrl='<%# GetDirectLinkFromGoogleDrive(Eval("ImageName").ToString()) %>' CssClass="CardDP" />
                                                <br />
                                                <br />

                                                <asp:HiddenField ID="HiddenRid" runat="server" Value='<%# Eval("rid") %>' />
                                                <asp:Button ID="ConnectButton" runat="server" Text="Connect" class="ConnectButton"
                                                    OnClientClick='<%# "return showConnectConfirmation(" + Eval("rid") + ");" %>'
                                                    OnClick="ConnectNow_Click"
                                                    Visible='<%# IsConnectButtonVisible(Eval("uid").ToString()) %>' />
                                            </div>
                                            <div class="CardInfo">
                                                <asp:Label ID="CardName" runat="server" Text='<%# Eval("name") %>' class="label"></asp:Label>
                                                <asp:Label ID="CardContact" runat="server" Text='<%# Eval("contact") %>' class="label"></asp:Label>
                                                <asp:Label ID="CardTeaching" runat="server" Text='<%# "Looking for " + Eval("looking") %>' class="label"></asp:Label>
                                                <asp:Label ID="CardStrand" runat="server" Text='<%# Eval("strand") %>' class="label"></asp:Label>
                                                <asp:Label ID="CardAvailability" runat="server" Text='<%# Eval("availability") %>' class="label"></asp:Label>
                                                <asp:Label ID="CardLocation" runat="server" Text='<%# Eval("location") %>' class="label"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        function showConnectConfirmation(rid) {
            return confirm('Are you sure you want to connect now?');
        }
    </script>
</body>
</html>
