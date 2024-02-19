<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/professorMasterPage.Master" AutoEventWireup="true" CodeBehind="pendingInvite.aspx.cs" Inherits="lms.Professor.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="../CSS/ProfessorCSS/pedingInvite.css" />
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

       <div class="invitation-tbl">
             <asp:GridView ID="pendinggrv" runat="server" AutoGenerateColumns="false" EmptyDataText="No Pending Invitations">
        <Columns>
            <asp:BoundField DataField="invitationid" HeaderText="Invitation ID" ItemStyle-CssClass="hide-column" HeaderStyle-CssClass="hide-column"/>
            <asp:BoundField DataField="roomid" HeaderText="Room ID" ItemStyle-CssClass="hide-column" HeaderStyle-CssClass="hide-column"/>
            <asp:BoundField DataField="studentid" HeaderText="Student ID"  ItemStyle-CssClass="hide-column" HeaderStyle-CssClass="hide-column"/>
            <asp:BoundField DataField="teacherid" HeaderText="Teacher ID"   ItemStyle-CssClass="hide-column" HeaderStyle-CssClass="hide-column"/>
            <asp:BoundField DataField="teacheremail" HeaderText="Teacher Email" ItemStyle-CssClass="hide-column" HeaderStyle-CssClass="hide-column"/>
            <asp:BoundField DataField="studentemail" HeaderText="Student Email" ItemStyle-CssClass="hide" HeaderStyle-CssClass="hide" />
            <asp:BoundField DataField="subjectname" HeaderText="Subject Name" ItemStyle-CssClass="hide" HeaderStyle-CssClass="hide"/>

            <asp:BoundField DataField="status" HeaderText="Status" />

           <asp:TemplateField HeaderText="" ItemStyle-Width="140px">
     <itemtemplate>
         <asp:DropDownList ID="ddlProcess" runat="server" style="display: none" CssClass="action-btn">
             <asp:ListItem Text="Invite" Value="Invite" ></asp:ListItem>
         </asp:DropDownList>
         <asp:HiddenField ID="hfTnIdPkId" runat="server" Value='<%# Eval("invitationid") %>' />
         <asp:Button ID="btnUpdateStatus" runat="server" Text="Cancel" CssClass="action-btn" OnClick="btnUpdateStatus_Click"   />
     </itemtemplate>
 </asp:TemplateField>
        </Columns>
    </asp:GridView>


       </div>
   </div>
</asp:Content>
