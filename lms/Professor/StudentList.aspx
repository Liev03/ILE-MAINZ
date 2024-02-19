<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/professorMasterPage.Master" AutoEventWireup="true" CodeBehind="StudentList.aspx.cs" Inherits="lms.Professor.StudentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/ProfessorCSS/inviteStudents.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="reports">

        <div class="report-tbl">
            <div class="invite-room">
                <div class="invite-p">

                    <p>List of Student</p>
                </div>
                <div class="invite-room-list">


                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>



                    <div class="studentlist">

                        <asp:GridView ID="roomlist" runat="server" AutoGenerateColumns="false" EmptyDataText="No Student Found">
                            <columns>
<%--                               <asp:BoundField DataField="subjectname" HeaderText="Professor Email" HeaderStyle-CssClass="subj" />--%>

                                <asp:BoundField DataField="studentid" HeaderText="Student ID" HeaderStyle-CssClass="subj" />
                                <asp:BoundField DataField="StudentEmail" HeaderText="Email" HeaderStyle-CssClass="subj" />
                                <asp:BoundField DataField="teacherid" HeaderText="Teacher ID" HeaderStyle-CssClass="subj" />

                                <asp:BoundField DataField="TeacherEmail" HeaderText="Professor Email" HeaderStyle-CssClass="subj" />

<%--                                     <asp:BoundField DataField="status" HeaderText="Status" HeaderStyle-CssClass="subj" />--%>

                                <asp:TemplateField HeaderText="" ItemStyle-Width="140px">

                                    <itemtemplate>
                                        <asp:DropDownList ID="ddlProcess" runat="server" CssClass="action-btn">
                                            <asp:ListItem Text="Invite" Value="Invite"></asp:ListItem>
<%--                                            <asp:ListItem Text="DELETE" Value="DELETE"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfTnIdPkId" runat="server" Value='<%# Eval("teacherid ") %>' />
                                        <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" CssClass="action-btn" OnClick="btnUpdateStatus_Click"  />
                                    </itemtemplate>
                                </asp:TemplateField>
                            </columns>
                        </asp:GridView>

                    </div>




                </div>

            </div>
        </div>
    </div>
</asp:Content>
