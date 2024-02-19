<%@ Page Title="" Language="C#" MasterPageFile="~/Student/studentMasterPage.Master" AutoEventWireup="true" CodeBehind="ArchivedClasses.aspx.cs" Inherits="lms.Student.ArchivedClasses" %>

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
                        <span class="room-subj"><%# Eval("invitation_subjectname") %></span>
                        <span class="room-section"><%# Eval("section") + " | " + Eval("schedule") %></span>

                      <%--  <div class="room-buttons">
                            <asp:LinkButton ID="enterroom" runat="server" CssClass="room-btn"
                                PostBackUrl='<%# "studentClassroom.aspx?roomid=" + Eval("roomid") %>'
                                Text="Enter Room" />



                        </div>--%>
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
