<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminLayout.Master" AutoEventWireup="true" CodeBehind="manageRooms.aspx.cs" Inherits="lms.Admin.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/AdminCSS/manageRoom.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
            
    <div class="classroom">
        <div class="search-room">
            <div class="search-bar">         
                    <asp:TextBox ID="txtsearch" runat="server" CssClass="search" placeholder="Search Rooms" AutoPostBack="True" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>        
                    <i class="fas fa-search"></i>
                </div>
            <div class="search-buttons">

            </div>
        </div>
        
        <div class="room-tbl">

        <asp:GridView ID="roomGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="No Rooms Found">
              <Columns>

        <asp:BoundField DataField="teachername" HeaderText="Teacher Name" />
        <asp:BoundField DataField="teacheremail" HeaderText="Teacher Email" />

                  <asp:TemplateField HeaderText="" ItemStyle-Width="140px">
                 <ItemTemplate>
                   <asp:LinkButton ID="roomLink" runat="server" 
                    PostBackUrl='<%# "roomDetails.aspx?teacheremail=" + Eval("teacheremail") %>'
                  Text="View Rooms" CssClass="view" />




                  </ItemTemplate>
             </asp:TemplateField>
          </Columns>
   </asp:GridView>
        </div>
    </div>

</asp:Content>
