<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/instructorClassRoom.Master" AutoEventWireup="true" CodeBehind="StreamClassroom.aspx.cs" Inherits="lms.Professor.WebForm10" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/ProfessorCSS/stream.css" />
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Classroom" runat="server">

   <div class="stream">
    <div class="create-post">
        <div class="post-head">
            <h2>Create Post</h2>
        </div>
        <div class="post-details">
            <div id="postDetailsLink" class="post-details-link">
                              <div class="post-div i">
               <asp:Image ID="Image1" runat="server"  CssClass="img-profile"  />                 
<%--                    <i class="fas fa-user"></i>--%>

                </div>
                <div class="post-div txt">
                    <a href="#" class="post-txt">Announce something in the class</a>
                </div>
            </div>

            <div id="postDetailsInput" class="post-details-input">
                <div class="detail-link">
                    <div class="post-div txt">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="detail-txt" placeholder="Announce something in the class" TextMode="MultiLine" Rows="7"></asp:TextBox>
                    </div>
                    <div class="post-buttons">
                        <asp:Button ID="btncreatepost" runat="server" CssClass="post-btn" Text="Create Post" OnClick="btncreatepost_Click" />
                        <asp:Button ID="Button1" runat="server" CssClass="post-btn cancel" Text="Cancel" />
                    </div>
                </div>
            </div>
<%--            <asp:Label ID="lblUserId" runat="server" Text="Label"></asp:Label>--%>


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
                                        <a href='<%# "editAnnouncement.aspx?roomid=" + Request.QueryString["roomid"] + "&announcementid=" + Eval("announcementid") %>'><i class="fas fa-edit"></i></a>
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
                                    <a href='<%# "ViewAnnouncement.aspx?roomid=" + Request.QueryString["roomid"] + "&announcementid=" + Eval("announcementid") %>' class="post-txt">Add class comment</a>

                                     </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

               
    </div>
</div>
    <
</asp:Content>