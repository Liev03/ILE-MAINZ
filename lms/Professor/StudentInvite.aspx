<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/professorMasterPage.Master" AutoEventWireup="true" CodeBehind="StudentInvite.aspx.cs" Inherits="lms.Professor.StudentInvite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="../CSS/ProfessorCSS/studentInvite.css" />
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
        <div class="reports">

        <div class="report-tbl">
            <div class="invite-room">
                <div class="invite-p">
                    <div class="link-p">
                           <a href="inviteStudents.aspx" class="arrow-left"><i class="fas fa-arrow-left arrow-left"></i></a>
                             <p>List of Student</p>
                    </div>        
              
                </div>
                <div class="invite-room-list">
                    <div class="search-list">
<<<<<<< Updated upstream
                       <%-- <div class="search-student">
=======
                      <%--  <div class="search-student">
>>>>>>> Stashed changes
                               <asp:TextBox ID="TextBox1" runat="server" CssClass="text-search" placeholder="Search student" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged" ></asp:TextBox>
                               <i class="fas fa-search"></i>
                          </div>--%>
                         <div class="subj-name">
                            <span>Subject Name : </span> 
                            <asp:Label ID="Label1" runat="server" Text="Label" CssClass="subject-name"></asp:Label>
                         </div>
                    </div>

                                
                    <div class="studentlist">

                        <asp:GridView ID="roomlist" runat="server" AutoGenerateColumns="false" EmptyDataText="No Student Found">
                            <columns>
                                <asp:BoundField DataField="roomid" HeaderText="Room ID" HeaderStyle-CssClass="subj hide-column" ItemStyle-CssClass="hide-column" />

                                <asp:BoundField DataField="studentid" HeaderText="Student ID" HeaderStyle-CssClass="subj id" />
                                <asp:BoundField DataField="StudentEmail" HeaderText="Email" HeaderStyle-CssClass="subj" />
                                <asp:BoundField DataField="teacherid" HeaderText="Teacher ID" HeaderStyle-CssClass="subj hide-column" ItemStyle-CssClass="hide-column" />
                                <asp:BoundField DataField="TeacherEmail" HeaderText="Professor Email" HeaderStyle-CssClass="subj hide-column" ItemStyle-CssClass="hide-column" />
                                 <asp:BoundField DataField="subjectname" HeaderText="Subject Name" HeaderStyle-CssClass="subj hide-column" ItemStyle-CssClass="hide-column" />
                                

                                <asp:TemplateField HeaderText="" ItemStyle-Width="140px">

                                    <itemtemplate>
                                        <asp:DropDownList ID="ddlProcess" runat="server" style="display: none" CssClass="action-btn">
                                            <asp:ListItem Text="Invite" Value="Invite" ></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfTnIdPkId" runat="server" Value='<%# Eval("teacherid") %>'  />
                                        <asp:Button ID="btnUpdateStatus" runat="server" Text="Invite" CssClass="action-btn" OnClick="btnUpdateStatus_Click"   />
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
