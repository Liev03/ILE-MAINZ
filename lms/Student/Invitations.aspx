<%@ Page Title="" Language="C#" MasterPageFile="~/Student/studentMasterPage.Master" AutoEventWireup="true" CodeBehind="Invitations.aspx.cs" Inherits="lms.Student.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <link rel="stylesheet" href="../CSS/Student/classInvitation.css" />

     <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
     <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

      <div class="reports">
      <div class="search-room">
             <div class="search-bar">         
                <asp:TextBox ID="txtsearch" runat="server" CssClass="search" placeholder="Search Rooms" AutoPostBack="True" OnTextChanged="txtsearch_TextChanged" ></asp:TextBox>        
                 <i class="fas fa-search"></i>
               </div>
         <div class="search-buttons">

         </div>


                   



      </div>

                <div class="studentlist">

    <asp:GridView ID="subjectinvite" runat="server" AutoGenerateColumns="false" EmptyDataText="No Invitations  Found">
        <columns>
            <asp:BoundField DataField="Invitationid" HeaderText="Invitation ID" HeaderStyle-CssClass="subj hide-column" ItemStyle-CssClass="hide-column" />
            <asp:BoundField DataField="roomid" HeaderText="Room ID" HeaderStyle-CssClass="subj hide-column" ItemStyle-CssClass="hide-column" />
            <asp:BoundField DataField="studentid" HeaderText="Student ID" HeaderStyle-CssClass="subj hide-column" ItemStyle-CssClass="hide-column" />
            <asp:BoundField DataField="StudentEmail" HeaderText="Email" HeaderStyle-CssClass="subj hide-column" ItemStyle-CssClass="hide-column" />
            <asp:BoundField DataField="teacherid" HeaderText="Teacher ID" HeaderStyle-CssClass="subj hide-column" ItemStyle-CssClass="hide-column"  />
            <asp:BoundField DataField="TeacherEmail" HeaderText="Teacher Email" HeaderStyle-CssClass="subj" />
             <asp:BoundField DataField="subjectname" HeaderText="Subject Name" HeaderStyle-CssClass="subj" />
            <asp:TemplateField HeaderText="" ItemStyle-Width="140px">

                <itemtemplate>
                    <asp:DropDownList ID="ddlProcess" runat="server" style="display: none" CssClass="action-btn">
                        <asp:ListItem Text="Accepted" Value="Accepted" ></asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField ID="hfTnIdPkId" runat="server" Value='<%# Eval("invitationid") %>'  />
                    <asp:Button ID="btnUpdateStatus" runat="server" Text="Accept" CssClass="action-btn" OnClick="btnUpdateStatus_Click"   />
                </itemtemplate>
            </asp:TemplateField>

                        <asp:TemplateField HeaderText="" ItemStyle-Width="140px">

    <itemtemplate>
        <asp:DropDownList ID="ddlProcess1" runat="server" style="display: none" CssClass="action-btn">
            <asp:ListItem Text="Decline" Value="Decline" ></asp:ListItem>
        </asp:DropDownList>
        <asp:HiddenField ID="hfTnIdPkId1" runat="server" Value='<%# Eval("invitationid") %>'  />
        <asp:Button ID="btnUpdateStatus1" runat="server" Text="Decline" CssClass="action-btn" OnClick="btnUpdateStatus1_Click"   />
    </itemtemplate>
</asp:TemplateField>

        </columns>
    </asp:GridView>
</div>
                    </div>


</asp:Content>
