<%@ Page Title="" Language="C#" MasterPageFile="~/Student/classroomMasterPage.Master" AutoEventWireup="true" CodeBehind="Classworks.aspx.cs" Inherits="lms.Student.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/ProfessorCSS/classwork.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Student" runat="server">

    <div class="classwork">
        <div class="class-head">
            <div class="drop-list1">
            <%--    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="d-list">
                    <asp:ListItem Text="All Topics" Value="1" />
                    <asp:ListItem Text="Assignments" Value="2" />
                    <asp:ListItem Text="Quiz" Value="3" />
                    <asp:ListItem Text="Materials" Value="4" />
                </asp:DropDownList>--%>
            </div>
            <div class="drop-list2">
            </div>
        </div>


        <div class="class-body">

            <asp:GridView ID="materialsGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="No Learning Materials Found" CssClass="custom-materials-grid">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="materials-box">
                                <div class="title">
                                    <h2><%# Eval("materialsname") %> - <%# Eval("posttype") %> - <%# Eval("topic") %></h2>
                                </div>
                                <div class="date-points">
                                    <div class="points">
                                        <span><%# Eval("points") %>  points</span>
                                    </div>
                                    <div class="date">
                                        <span>
                                    <p>Due Date: <%# Eval("duedate") == DBNull.Value ? "No Due" : Eval("duedate", "{0:yyyy-MM-dd}") %></p>
                                        </span>
                                    </div>
                                </div>
                                <div class="instruction">
                                    <p><%# Eval("instructions") %></p>
                                </div>
                                <div class="submit-link">
                                    <%--                                 <a href="submitClasswork.aspx><i class="fa fa-eye"></i>View Details</a>--%>
                                    <a href='<%# "submitClasswork.aspx?roomid=" + Request.QueryString["roomid"] + "&materialsid=" + Eval("materialsid") %>'><i class="fa fa-eye"></i>View Details</a>

                                </div>




                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
