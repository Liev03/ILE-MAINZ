<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/professorMasterPage.Master" AutoEventWireup="true" CodeBehind="notifications.aspx.cs" Inherits="lms.Professor.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/ProfessorCSS/notifications.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="notifications">
        <div class="notif-data">
            <div class="notif-head">
                <i class="fas fa-bell"></i>
                <span>Notifications</span>
                <asp:Label ID="Label2" runat="server"></asp:Label>
            </div>
            <div class="notif-list">
                <div class="notif-p">
                 
                    <asp:GridView ID="roomdetailsGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="No Notification Found">
                        <Columns>
                            <asp:BoundField DataField="subject" HeaderText="Subject Name" HeaderStyle-CssClass="subj" />
                              <asp:BoundField DataField="Date" HeaderText="Date" HeaderStyle-CssClass="subj" />

                            <asp:TemplateField HeaderText="" ItemStyle-Width="180px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="roomLink" runat="server" CssClass="btn-list"
                                        PostBackUrl='<%# "NotificationDetails.aspx?notifid=" + Eval("notifid") %>'
                                        Text="View Message" />

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>


            </div>
        </div>
    </div>

</asp:Content>
