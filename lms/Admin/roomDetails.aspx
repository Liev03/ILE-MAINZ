<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminLayout.Master" AutoEventWireup="true" CodeBehind="roomDetails.aspx.cs" Inherits="lms.Admin.WebForm8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/AdminCSS/roomDetails.css" />
        

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <div class="view-details">
        <div class="details-card">
            <div class="room-head">
                  <a href="manageRooms.aspx"><i class="fas fa-arrow-left arrow-left"></i></a>
                <h2>Rooms Created</h2>
                   </div>
            <div class="room-prof">

                <div class="p-room">
                    <span>List of Created Rooms By:</span>
                    <asp:Label ID="Label2" runat="server" Text="" CssClass="room-lbl"></asp:Label>
             </div>

            <div class="subjects">
        <asp:GridView ID="roomdetailsGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="No Subject Found">
           <Columns>
                   <asp:BoundField DataField="subjectname" HeaderText="Subject Name" HeaderStyle-CssClass="subj"/>

                   <asp:TemplateField HeaderText="" ItemStyle-Width="140px">
              <ItemTemplate>
      <%--             <asp:LinkButton ID="roomLink" runat="server" CssClass="btn-list"
                           PostBackUrl='<%# "subDetails.aspx?roomid=" + Eval("roomid") %>'

   Text="View Subjects" />--%>


   <a href='<%# "subDetails.aspx?teacheremail=" + Request.QueryString["teacheremail"] + "&roomid=" + Eval("roomid") %>' class="btn-list">View Subjects</a>

             </ItemTemplate>
             </asp:TemplateField>
           </Columns>
        </asp:GridView>


                </div>
            </div>

        </div>


    </div>

</asp:Content>
