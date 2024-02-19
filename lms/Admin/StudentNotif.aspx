<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminLayout.Master" AutoEventWireup="true" CodeBehind="StudentNotif.aspx.cs" Inherits="lms.Admin.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
<script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="../CSS/AdminCSS/Prof_StudNotif.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="notif">
        <div class="search-nav">
              <div class="search-bar">   
                     <asp:TextBox ID="txtsearch" runat="server" CssClass="search" placeholder="Search Student" AutoPostBack="True" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                   <i class="fas fa-search"></i>
                  </div>
           <div class="search-buttons">
                 <asp:Button ID="btnSendToAll" runat="server" Text="Send To All" CssClass="crud" OnClick="btnSendToAll_Click" />
              </div>
        </div>

          <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

     <div class="notif-body">
           <asp:GridView ID="studentGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="No Student Found">
     <Columns>
         <asp:BoundField DataField="studentid" HeaderText="Student ID" />
         <asp:BoundField DataField="Fullname" HeaderText="Name" />
         <asp:BoundField DataField="email" HeaderText="Email" />
         <asp:TemplateField HeaderText="" ItemStyle-Width="140px">
             <ItemTemplate>
                 <asp:HyperLink ID="studentLink" runat="server"
                     NavigateUrl='<%# "WriteNotif.aspx?studentid=" + Eval("studentid") %>'
                     Text="Send Message" CssClass="view" />
             </ItemTemplate>
         </asp:TemplateField>
     </Columns>
 </asp:GridView>
            </div>
    </div>
</asp:Content>
