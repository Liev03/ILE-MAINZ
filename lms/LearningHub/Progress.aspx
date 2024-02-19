<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Progress.aspx.cs" Inherits="lms.LearningHub.Progress" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Progress</title>
    <link href="CSS/Dashboard.css" rel="stylesheet" />
    <link href="CSS/ProgressTracker.css" rel="stylesheet" />
    <link rel="icon" href="../Resources/Novaliches Senior High School.png" type="image/x-icon" />
    <style>
        #additionalContent {
            width: 50%;
            height: 80%;
        }

        .progress-table {
            text-align: center;
            margin: 0 auto;
            color: black;
        }

        .Content {
            overflow: auto;
        }

        .additional-content {
            width: 80%;
        }

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
                <asp:Button ID="btnGeneratePDF" runat="server" Text="Generate PDF" OnClick="GeneratePDF_Click" />
                <asp:GridView ID="progressGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="progressGridView_RowCommand" DataKeyNames="TransactionID">
                    <Columns>
                        <asp:BoundField DataField="TransactionID" HeaderText="Transaction ID" SortExpression="TransactionID" />
                        <asp:BoundField DataField="TuteeName" HeaderText="Tutee Name" SortExpression="TuteeName" />
                        <asp:BoundField DataField="TuteeStudentID" HeaderText="Tutee Student ID" SortExpression="TuteeStudentID" />
                        <asp:BoundField DataField="TutorName" HeaderText="Tutor Name" SortExpression="TutorName" />
                        <asp:BoundField DataField="TutorStudentID" HeaderText="Tutor Student ID" SortExpression="TutorStudentID" />
                        <asp:BoundField DataField="TuteeYearLevel" HeaderText="Year Level" SortExpression="TuteeYearLevel" />
                        <asp:BoundField DataField="TuteeStrand" HeaderText="Strand" SortExpression="TuteeStrand" />
                        <asp:BoundField DataField="TutorAvailability" HeaderText="Availability" SortExpression="TutorAvailability" />
                        <asp:BoundField DataField="TutorLocation" HeaderText="Location" SortExpression="TutorLocation" />
                        <asp:BoundField DataField="days" HeaderText="Days" SortExpression="days" />
                        <asp:BoundField DataField="Progress" HeaderText="Progress" SortExpression="Progress" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button ID="btnMore" runat="server" Text="More" CssClass="more-button" CommandName="MoreCommand" CommandArgument='<%# Eval("TransactionID") %>' />
                                <asp:HiddenField ID="hfRowIndex" runat="server" Value='<%# Container.DataItemIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <div id="additionalContent" class="additional-content" runat="server" style="display: none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); background-color: white; padding: 20px; border: 1px solid #ccc;">
                    <div style="height: 70%; overflow: auto; border: 1px solid #000;">
                        <asp:Button ID="closeButton" runat="server" Text="X" CssClass="close-button" OnClick="Close_Click" Style="position: absolute; top: 5px; right: 5px;" />
                        <table class="progress-table" style="width: 40%; float: left;">
                            <tr>
                                <th colspan="2">First Half</th>
                            </tr>
                            <tr>
                                <td>Day 1</td>
                                <td>
                                    <asp:Button ID="btnDetails1" runat="server" Text="Details" CssClass="details-button" OnClick="Details_Click" CommandArgument="Day 1" /></td>
                            </tr>
                            <tr>
                                <td>Day 2</td>
                                <td>
                                    <asp:Button ID="btnDetails2" runat="server" Text="Details" CssClass="details-button" OnClick="Details_Click" CommandArgument="Day 2" /></td>
                            </tr>
                            <tr>
                                <td>Day 3</td>
                                <td>
                                    <asp:Button ID="btnDetails3" runat="server" Text="Details" CssClass="details-button" OnClick="Details_Click" CommandArgument="Day 3" /></td>
                            </tr>
                            <tr>
                                <td>Day 4</td>
                                <td>
                                    <asp:Button ID="btnDetails4" runat="server" Text="Details" CssClass="details-button" OnClick="Details_Click" CommandArgument="Day 4" /></td>
                            </tr>
                            <tr>
                                <td>Day 5</td>
                                <td>
                                    <asp:Button ID="btnDetails5" runat="server" Text="Details" CssClass="details-button" OnClick="Details_Click" CommandArgument="Day 5" /></td>
                            </tr>
                            <tr>
                                <td>Day 6</td>
                                <td>
                                    <asp:Button ID="btnDetails6" runat="server" Text="Details" CssClass="details-button" OnClick="Details_Click" CommandArgument="Day 6" /></td>
                            </tr>
                            <tr>
                                <td>Day 7</td>
                                <td>
                                    <asp:Button ID="btnDetails7" runat="server" Text="Details" CssClass="details-button" OnClick="Details_Click" CommandArgument="Day 7" /></td>
                            </tr>
                        </table>

                        <table class="progress-table" style="width: 40%; float: right;">
                            <tr>
                                <th colspan="2">Second Half</th>
                            </tr>
                            <tr>
                                <td>Day 8</td>
                                <td>
                                    <asp:Button ID="btnDetails8" runat="server" Text="Details" CssClass="details-button" OnClick="Details_Click" CommandArgument="Day 8" /></td>
                            </tr>
                            <tr>
                                <td>Day 9</td>
                                <td>
                                    <asp:Button ID="btnDetails9" runat="server" Text="Details" CssClass="details-button" OnClick="Details_Click" CommandArgument="Day 9" /></td>
                            </tr>
                            <tr>
                                <td>Day 10</td>
                                <td>
                                    <asp:Button ID="btnDetails10" runat="server" Text="Details" CssClass="details-button" OnClick="Details_Click" CommandArgument="Day 10" /></td>
                            </tr>
                            <tr>
                                <td>Day 11</td>
                                <td>
                                    <asp:Button ID="btnDetails11" runat="server" Text="Details" CssClass="details-button" OnClick="Details_Click" CommandArgument="Day 11" /></td>
                            </tr>
                            <tr>
                                <td>Day 12</td>
                                <td>
                                    <asp:Button ID="btnDetails12" runat="server" Text="Details" CssClass="details-button" OnClick="Details_Click" CommandArgument="Day 12" /></td>
                            </tr>
                            <tr>
                                <td>Day 13</td>
                                <td>
                                    <asp:Button ID="btnDetails13" runat="server" Text="Details" CssClass="details-button" OnClick="Details_Click" CommandArgument="Day 13" /></td>
                            </tr>
                            <tr>
                                <td>Day 14</td>
                                <td>
                                    <asp:Button ID="btnDetails14" runat="server" Text="Details" CssClass="details-button" OnClick="Details_Click" CommandArgument="Day 14" /></td>
                            </tr>
                        </table>
                    </div>

                    <div id="hidediv" class="hidedivclass" runat="server" style="display: none; border-style: solid; height: 30%; border-width: 5px; border-color: orange; z-index: 1; position: relative;">
                        <p></p>


                        <asp:Label ID="lblTopMiddle" runat="server" Style="position: absolute; top: 0; left: 50%; transform: translateX(-50%); color: black;">Top Middle Label</asp:Label>


                        <asp:Label ID="lblCenter" runat="server" Text="Center Label" Style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); color: black;"></asp:Label>
                        <div id="editCenterForm" class="hidden-form" runat="server">
                            <asp:TextBox ID="CenterTextarea" runat="server" TextMode="MultiLine" Rows="5" Columns="40"></asp:TextBox>
                            <br />
                            <asp:Button ID="saveButton" runat="server" Text="Save" OnClick="Save_Click" OnClientClick="hideEditCenterForm();" />
                        </div>


                        <div style="position: absolute; bottom: 10px; left: 50%; transform: translateX(-50%);">
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="edit-button" Visible="true" OnClick="Edit_Click" Style="color: black;" OnClientClick="showEditCenterForm(); return false;" />
                            <asp:Button ID="btnComplete" runat="server" Text="Complete" CommandName="CompleteCommand" CssClass="complete-button" OnClick="Complete_Click" Visible="true" Style="color: black;" />
                        </div>

                    </div>
                </div>

            </div>
        </div>

    </form>
    <script>



        function showConfirmationModal(button, rowIndex) {
            return confirm('Are you sure you want to complete this? (Irreversible)');
        }

        function showMoreContent() {
            var additionalContent = document.getElementById("additionalContent");
            additionalContent.style.display = (additionalContent.style.display === "none") ? "block" : "none";
            return false;
        }

        function showEditCenterForm() {
            var editCenterForm = document.getElementById('<%= editCenterForm.ClientID %>');
            if (editCenterForm) {
                editCenterForm.style.display = 'block';
            }
        }

        function hideEditCenterForm() {
            var editCenterForm = document.getElementById('<%= editCenterForm.ClientID %>');
            if (editCenterForm) {
                editCenterForm.style.display = 'none';
            }
        }

        function showMoreContent(rowIndex) {

            var tid = document.getElementById('<%= progressGridView.ClientID %>').rows[rowIndex].cells[8].innerText;
        }
    </script>
</body>
</html>
