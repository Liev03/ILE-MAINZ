<%@ Page Title="" Language="C#" MasterPageFile="~/Student/classroomMasterPage.Master" AutoEventWireup="true" CodeBehind="submitClasswork.aspx.cs" Inherits="lms.Student.WebForm8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/Student/submitClasswork.css" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <style>
        .hide-column {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Student" runat="server">

    <div class="submit">
        <div class="class-content">
            <div class="details">
                <div class="title">
                    <asp:Label ID="lblpost" runat="server" Text="" CssClass="h2"></asp:Label>

                </div>
                <div class="name">
                    <asp:Label ID="lblteacher" runat="server" Text="" CssClass="h3"></asp:Label>


                    <%--hidden--%>
                    <asp:Label ID="lblmaterialsid" runat="server" CssClass="hidelbl" Text=""></asp:Label>
                    <asp:Label ID="lblteacherid" runat="server" CssClass="hidelbl" Text=""></asp:Label>
                    <asp:Label ID="lblsubjectname" runat="server" CssClass="hidelbl" Text=""></asp:Label>
                    <asp:Label ID="lblmaterialsname" runat="server" CssClass="hidelbl" Text=""></asp:Label>
                    <asp:Label ID="lblteacheremail" runat="server" CssClass="hidelbl" Text=""></asp:Label>
                    <%--hidden--%>


                    <asp:Label ID="lbldateposted" runat="server" Text="" CssClass="span"></asp:Label>

                </div>
                <div class="points-date">
                    <asp:Label ID="lblpoints" runat="server" Text="" CssClass="span"></asp:Label>'

               

              <asp:Label ID="lbldue" runat="server" Text="" CssClass="span"></asp:Label>


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
                        <%--<i class="fas fa-arrow-right"></i>--%>
                        <%--  it should be imagebutton(arrow image button)--%>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Resources/submit2.png" CssClass="btn-comment" OnClick="ImageButton1_Click" />

                    </div>

                </div>
            </div>

        </div>
        <div class="class-submit">
            <div class="my-works">
                <div class="works">
                    <h2>Your Work</h2>
                       <asp:Label ID="lblstatus" runat="server" Text="" CssClass="span"></asp:Label>'
     <asp:Label ID="lblgradepoints" runat="server" Text="" CssClass="span"></asp:Label>'
                    <%--<span>Turned in</span>--%>
                </div>
                <div class="work-list">
                    <asp:GridView ID="gvwork" runat="server" AutoGenerateColumns="False" CssClass="gridview" OnRowCommand="gvwork_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="materialsId" HeaderText="File ID" SortExpression="studentworkid" ReadOnly="True" HeaderStyle-CssClass="hide-column" ItemStyle-CssClass="hide-column" />

                            <asp:BoundField DataField="studentworkid" HeaderText="File ID" SortExpression="studentworkid" ReadOnly="True" HeaderStyle-CssClass="hide-column" ItemStyle-CssClass="hide-column" />
                            <asp:BoundField DataField="FileName" HeaderText="File Name" SortExpression="FileName" ReadOnly="True" HeaderStyle-CssClass="hide-column" />
                            <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="hide-column">
                                <ItemTemplate>
                                    <asp:Button runat="server" CommandName="RemoveFile" CommandArgument='<%# Eval("studentworkid") %>' Text="X" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
                <div class="submit-btn">
                    <asp:FileUpload ID="file" runat="server" CssClass="work-btn" />

                    <asp:Button ID="Button1" runat="server" Text="Add Classwork" CssClass="work-btn" OnClick="Button1_Click" />
<%--                    <asp:Button ID="btnmarkasdone" runat="server" Text="Mark as done" CssClass="work-btn" OnClientClick="toggleDivs(); return false;" />--%>
                </div>
                <div class="submit-btn2">
                    <asp:Button ID="Button2" runat="server" Text="Unsubmit" CssClass="work-btn" OnClientClick="toggleDivs(); return false;" />
                </div>
            </div>
        </div>

    </div>

    <script>
        function toggleDivs() {
            var div1 = document.querySelector('.submit-btn');
            var div2 = document.querySelector('.submit-btn2');
            var downloadButton = document.getElementById('<%= gvwork.ClientID %>_ctl02');

            if (downloadButton) {
                downloadButton.disabled = true;
            }


            if (div1.style.display !== 'none') {
                fadeOut(div1);
                setTimeout(function () {
                    div1.style.display = 'none';
                    fadeIn(div2);
                }, 500);
            } else {
                fadeOut(div2);
                setTimeout(function () {
                    div2.style.display = 'none';
                    fadeIn(div1);
                }, 500);
            }
        }

        function fadeIn(element) {
            element.style.display = 'block';
            setTimeout(function () {
                element.style.opacity = 1;
            }, 10);
        }

        function fadeOut(element) {
            element.style.opacity = 0;
            setTimeout(function () {
                element.style.display = 'none';
            }, 500);
        }
    </script>

</asp:Content>
