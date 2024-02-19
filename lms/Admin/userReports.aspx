<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminLayout.Master" AutoEventWireup="true" CodeBehind="userReports.aspx.cs" Inherits="lms.Admin.WebForm10" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/AdminCSS/userReports.css" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <div class="reports">
         <div class="search-room">
           
<%--                <div class="search-bar">          
            <asp:TextBox ID="TextBox1" runat="server" CssClass="search" placeholder="Search Email"></asp:TextBox>
                    <i class="fas fa-search"></i>
               </div>
            <div class="search-buttons">
                 <asp:DropDownList ID="DropDownList1" runat="server" CssClass="d-list">
                      <asp:ListItem Text="Filter Emails" Value="1" />
                      <asp:ListItem Text="Professor Emails" Value="2" />
                        <asp:ListItem Text="Student Emails" Value="23" />
                 </asp:DropDownList>
                              <asp:Button ID="Button1" runat="server" Text="Search" CssClass="crud"/>
                  </div>
          --%>
         </div>

        <div class="report-tbl">
            
   
                <div class="notif-p">
                 
                    <asp:GridView ID="roomdetailsGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="No Notification Found">
                        <Columns>
                            <asp:BoundField DataField="subject" HeaderText="Subject Name" HeaderStyle-CssClass="subj" />
                              <asp:BoundField DataField="Date" HeaderText="Date" HeaderStyle-CssClass="subj" />

                            <asp:TemplateField HeaderText="" ItemStyle-Width="180px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="roomLink" runat="server" CssClass="btn-list"
                                        PostBackUrl='<%# "notifications.aspx?notifid=" + Eval("notifid") %>'
                                        Text="View Message" />

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>


            </div>
    </div>

    
</asp:Content>
