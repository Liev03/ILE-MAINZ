<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/instructorClassRoom.Master" AutoEventWireup="true" CodeBehind="ViewAnnouncement.aspx.cs" Inherits="lms.Professor.ViewAnnouncement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/ProfessorCSS/ProfessorViewAnnouncement.css" rel="stylesheet" />
    <style>
        .emailname {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Classroom" runat="server">


    <div class="submit">
        <div class="class-content">
            <div class="details">
                <div class="title">
<%--                    <h2>Date Posted</h2>--%>
                    <asp:Label ID="lbldate" runat="server" Text="" CssClass="h2"></asp:Label>

                </div>
                <div class="name">
                    <asp:Label ID="lblteachername" runat="server" Text="" CssClass="h3"></asp:Label>
                    <asp:Label ID="lblteacheremail" runat="server" CssClass="emailname" Text=""></asp:Label>

                </div>

            </div>
            <div class="modules">

                <%--                 <p>please answer and submit within the day.  do not forget to turn in.</p>--%>
                <asp:Label ID="lblpostcontent" runat="server" Text="" CssClass="p"></asp:Label>


            </div>
            <div class="comments">
                <div class="class-comments">
                      <i class="fas fa-users"></i> 
                 
                    <asp:Label ID="classCommentCountLabel" runat="server" Text='<%# Eval("CommentCount") %>' CssClass="lbl-comment"></asp:Label>
                      <span> Class comments </span>
                </div>
                <div class="comment-list">
                    <asp:GridView ID="commentGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="" CssClass="announcement-grid">
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
                                                    <h3><%# Eval("name") %></h3>
                                               
                                                    <span><%# Eval("datepost") %></span>
                                                </div>
                                                 <div class="announcement-body">
                                                      <p><%# Eval("commentpost") %></p>
                                                 </div>
                                            </div>
                                        </div>
                                      
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>



                </div>
                <div class="announcement-comment">
                    <div class="post-div i">
                        <asp:Image ID="profileimagedis" runat="server" CssClass="img-profile" />
                    </div>
                    <div class="post-div txt">
                        <asp:TextBox ID="txtcomment" runat="server" placeholder="Add comment" CssClass="txt-comment"></asp:TextBox>
                    </div>
                    <div class="comment-button">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Resources/submit2.png" CssClass="btn-comment" OnClick="ImageButton1_Click"  />
                     
                    </div>

                </div>
            </div>
        </div>

    </div>

</asp:Content>
