<%@ Page Title="" Language="C#" MasterPageFile="~/Student/classroomMasterPage.Master" AutoEventWireup="true" CodeBehind="Stream.aspx.cs" Inherits="lms.Student.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="../CSS/Student/studentStream.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Student" runat="server">

    <div class="stream">
         <div class="create-post">
        <div class="post-head">
            <i class="fas fa-bullhorn"></i>
             <h2>Announcements</h2>
             </div>
       
                              
        <asp:GridView ID="postGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="" CssClass="announcement-grid">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                      
                        <div class="announcement-box">
                            <div class="announcement-head">
                                <div class="profile">
                               <asp:Image ID="Image1" runat="server" CssClass="img-profile" ImageUrl='<%# GetProfileImageUrl(Eval("profileimage")) %>' />
                                </div>
                                <div class="info">
                                    <div class="name">
                                        <h3><%# Eval("teachername") %></h3>
                                    </div>
                                    <div class="date">
                                        <span><%# Eval("datepost") %></span>
                                    </div>
                                </div>
                            </div>
                            <div class="announcement-body">
                                <p><%# Eval("postcontent") %></p>
                            </div>
                            <div class="announcement-comment">
                           
                                     <div class="post-div i">                                                  
                                          <i class="fas fa-user"></i>

                                      </div>
                                     <div class="post-div txt">
<%--                                          <a href='<%# "viewAnnouncement.aspx?roomid=" + Request.QueryString["roomid"]  + "&announcementid =" + Eval("announcementid")  %>' class="post-txt">Add class comment</a>--%>
                                    <a href='<%# "viewAnnouncement.aspx?roomid=" + Request.QueryString["roomid"] + "&announcementid=" + Eval("announcementid") %>' class="post-txt">Add class comment</a>

                                     </div>
                              
                            </div>

                        </div>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

               
    </div>
</div>
    

</asp:Content>
