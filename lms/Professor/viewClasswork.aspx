<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/instructorClassRoom.Master" AutoEventWireup="true" CodeBehind="viewClasswork.aspx.cs" Inherits="lms.Professor.WebForm16" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/ProfessorCSS/viewClasswork.css" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <style>
        .hide-column {
            display: none;
        }

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
                    <asp:Label ID="lblpost" runat="server" Text="" CssClass="h2"></asp:Label>

                </div>
                <div class="name">
                    <asp:Label ID="lblteacher" runat="server" Text="" CssClass="h3"></asp:Label>
                    <asp:Label ID="lblteacheremail" runat="server" CssClass="emailname" Text=""></asp:Label>
                    <asp:Label ID="lbldateposted" runat="server" Text="" CssClass="span"></asp:Label>

                </div>
                <div class="points-date">
                    <div class="points">
                        <asp:Label ID="lblpoints" runat="server" Text="" CssClass="span"> </asp:Label>
                        <span>points</span>
                    </div>
                    <div class="dates">
                        <span>Due Date:</span>
                        <asp:Label ID="lbldue" runat="server" Text="" CssClass="span"></asp:Label>
                    </div>

                </div>
            </div>
            <div class="modules">
                <div class="instructions">

                    <asp:Label ID="lblinstructions" runat="server" Text="" CssClass="p"></asp:Label>

                </div>
                <div class="file">
                    <div class="files">



                        <asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                            <Columns>
                                <asp:BoundField DataField="materialsId" HeaderText="File ID" SortExpression="materialsId" ReadOnly="True" HeaderStyle-CssClass="hide-column" ItemStyle-CssClass="hide-column" />
                                <asp:BoundField DataField="FileName" HeaderText="File Name" SortExpression="FileName" ReadOnly="True" HeaderStyle-CssClass="hide-column" />
<%--                                <asp:CommandField ShowSelectButton="True" SelectText="Download" HeaderStyle-CssClass="hide-column" />--%>
                                <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="hide-column">
                                    <ItemTemplate>
                                        <a href='<%# $"FileHandler.ashx?materialsId={Eval("materialsId")}" %>' target="_blank">View</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>



                    </div>
                </div>
            </div>
            <div class="comments">
                <div class="class-comments">
                    <i class="fas fa-users"></i>

                    <asp:Label ID="classCommentCountLabel" runat="server" Text='<%# Eval("CommentCount") %>' CssClass="lbl-comment"></asp:Label>
                    <span>Class comments </span>
                </div>
                <div class="comment-list">


                    <asp:GridView ID="commentGridView" runat="server" AutoGenerateColumns="false" CssClass="announcement-grid">
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
                        <asp:Image ID="Image1" runat="server" CssClass="img-profile" />

                    </div>
                    <div class="post-div txt">
                        <asp:TextBox ID="txtcomment" runat="server" placeholder="Add comment" CssClass="txt-comment"></asp:TextBox>
                    </div>
                    <div class="comment-button">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Resources/submit2.png" CssClass="btn-comment" OnClick="ImageButton1_Click" />

                    </div>

                </div>
            </div>

        </div>
        <div class="class-submit">

            <div class="submitted">
                <div class="works">
                    <h2>Turned In</h2>
                    <span></span>
                </div>
                <div class="work-list">

                    <asp:GridView ID="gvwork" runat="server" AutoGenerateColumns="False" CssClass="gridview1">
                        <Columns>

                            <asp:BoundField DataField="materialsId" HeaderText="File ID" SortExpression="materialsId" ReadOnly="True" HeaderStyle-CssClass="hide-column" ItemStyle-CssClass="hide-column" />
                            <asp:BoundField DataField="FileName" HeaderText="File Name" SortExpression="FileName" ReadOnly="True" HeaderStyle-CssClass="hide-column" />
                            <asp:BoundField DataField="studentname" HeaderText="Student Name" SortExpression="StudentName" ReadOnly="True" HeaderStyle-CssClass="hide-column" />
                            <asp:BoundField DataField="studentworkid" HeaderText="File ID" SortExpression="materialsId" ReadOnly="True" HeaderStyle-CssClass="hide-column" ItemStyle-CssClass="hide-column" />

                            <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="hide-column">
                                <ItemTemplate>
                                    <a href='<%# $"FileHandler.ashx?materialsId={Eval("materialsId")}" %>' target="_blank">View</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" SelectText="Download" HeaderStyle-CssClass="hide-column" />

                            <asp:TemplateField HeaderText="Grade" HeaderStyle-CssClass="hide-column">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgrade" runat="server" CssClass="grade-txt" placeholder="Input Score"></asp:TextBox>
                                    <asp:HiddenField ID="hfTnIdPkId" runat="server" Value='<%# Eval("studentworkid") %>' />
                                    <asp:Button ID="btngrade" runat="server" Text="Submit Grade" OnClick="btngrade_Click1" CssClass="grade-btn" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                </div>

            </div>
            <div class="not-submitted">
                <div class="works">
                    <h2>Assigned</h2>
                    <span></span>
                </div>
                <div class="work-list">


                    <asp:GridView ID="gvpeople" runat="server" AutoGenerateColumns="False" CssClass="gridview" OnSelectedIndexChanged="gvpeople_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="studentemail" HeaderText="Student Name" SortExpression="StudentName" ReadOnly="True" HeaderStyle-CssClass="hide-column" />

                        </Columns>
                    </asp:GridView>


                </div>

            </div>

        </div>
    </div>
</asp:Content>
