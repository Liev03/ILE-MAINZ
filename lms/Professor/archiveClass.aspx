<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/professorMasterPage.Master" AutoEventWireup="true" CodeBehind="archiveClass.aspx.cs" Inherits="lms.Professor.WebForm8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/ProfessorCSS/CreateRoom.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">


    <div class="room-page">
        <div class="room-filter">
            <div class="filters">
                <p>Archived Rooms</p>

            </div>

        </div>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <div class="room-lists">

            <asp:Repeater ID="roomRepeater" runat="server">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="room-card">
                        <div class="room-image">
                            <img class="room-banner" src='<%# "ImageHandler.ashx?roomid=" + Eval("roomid") %>' alt="" />
                        </div>

                        <div class="room-sched">
                            <a class="room-subj"><%# Eval("subjectname") %></a>
                            <span class="room-section"><%# Eval("section") + " | " + Eval("schedule") %></span>

                            <div class="room-buttons">
                                <asp:LinkButton ID="roomLink" runat="server" CssClass="room-btn"
                                    PostBackUrl='<%# "UnachiveConfirmation.aspx?roomid=" + Eval("roomid") %>'
                                    Text="Restore" />


                            </div>
                        </div>
                    </div>

                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>

        </div>

    </div>

</asp:Content>
