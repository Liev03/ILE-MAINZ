<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/instructorClassRoom.Master" AutoEventWireup="true" CodeBehind="Classwork.aspx.cs" Inherits="lms.Professor.WebForm13" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/ProfessorCSS/classwork.css" />
    <style>
        .random {
            display: none;
            }
        </style>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="Classroom" runat="server">

    <div class="classwork">
        <div class="class-head">
            <div class="drop-list1">
                <%--<asp:DropDownList ID="DropDownList1" runat="server" CssClass="d-list">
                    <asp:ListItem Text="All Topics" Value="1" />
                    <asp:ListItem Text="Assignments" Value="2" />
                    <asp:ListItem Text="Quiz" Value="3" />
                    <asp:ListItem Text="Materials" Value="4" />
                </asp:DropDownList>--%>
            </div>
            <div class="drop-list2">
                <a href="#" class="d-link" id="createRoomLink">Create Task</a>
            </div>
        </div>

        <asp:Label ID="lblsubjectname" runat="server" Text="" CssClass="random"></asp:Label>
        <div class="class-body">

            <asp:GridView ID="materialsGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="No Learning Materials Found" CssClass="custom-materials-grid">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="materials-box">
                                <div class="title">
                                    <h2><%# Eval("materialsname") %> - <%# Eval("posttype") %> - <%# Eval("topic") %></h2>

                                    <a href='<%# "editClasswork.aspx?roomid=" + Request.QueryString["roomid"] + "&materialsid=" + Eval("materialsid") %>'><i class="fas fa-edit"></i></a>
                                </div>
                                <div class="date-points">
                                    <div class="points">
                                        <span><%# Eval("points") %>  points</span>
                                    </div>
                                    <div class="date">
                                        <span>
<%--                                            <p>Due Date: <%# Eval("duedate", "{0:yyyy-MM-dd}") %></p>--%>
                                            <p>Due Date: <%# Eval("duedate") == DBNull.Value ? "No Due" : Eval("duedate", "{0:yyyy-MM-dd}") %></p>

                                        </span>
                                    </div>
                                </div>
                                <div class="instruction">
                                    <p><%# Eval("instructions") %></p>
                                </div>
                                <div class="submit-link">

                                    <a href='<%# "viewClasswork.aspx?roomid=" + Request.QueryString["roomid"] + "&materialsid=" + Eval("materialsid") %>'><i class="fa fa-eye"></i>View Details</a>

                                </div>


                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="link" id="assignmentContainer">
        <div class="post-materials">
            <h2>CREATE MATERIALS</h2>
        </div>
        <div class="assignment">
            <div class="assign-title">
                <div class="title">
                    <asp:Label ID="lbltitle" runat="server" Text="Module:" CssClass="lbl-title"></asp:Label>
                    <asp:TextBox ID="txtmaterialsname" runat="server" CssClass="txt-title" placeholder="Enter Module Name"></asp:TextBox>
                </div>
                <div class="instructions">
                    <asp:Label ID="Label4" runat="server" Text="Instructions or links:" CssClass="lbl-instruc"></asp:Label>
                    <asp:TextBox ID="txtinstructions" runat="server" TextMode="MultiLine" Rows="5" CssClass="txt-instruc" placeholder="Write instructions about the post"></asp:TextBox>
                </div>
            </div>
            <div class="assign-description">
                <div class="assign">
                    <asp:Label ID="lblposttype" runat="server" Text="Create Post: " CssClass="lbl-assign"></asp:Label>
                    <asp:RadioButton ID="rbassignment" runat="server" Text="Assignment" CssClass="rdb-post" GroupName="MaterialsGroup" />
                    <asp:RadioButton ID="rbquiz" runat="server" Text="Quiz" CssClass="rdb-post" GroupName="MaterialsGroup" />
                    <asp:RadioButton ID="rbmaterials" runat="server" Text="Materials" CssClass="rdb-post" GroupName="MaterialsGroup" />
                   <asp:RadioButton ID="rbexam" runat="server" Text="Exam" CssClass="rdb-post" GroupName="MaterialsGroup" />

                </div>
                <div class="assign">
                    <asp:Label ID="lblpoints" runat="server" Text="Points:" CssClass="lbl-assign"></asp:Label>
                    <asp:DropDownList ID="drdpoints" runat="server" CssClass="txt-assign list">
                        <asp:ListItem Text="0" Value="0" />
                        <asp:ListItem Text="100" Value="100" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="40" Value="40" />
                        <asp:ListItem Text="30" Value="30" />
                    </asp:DropDownList>
                </div>
                <div class="assign">
                    <asp:Label ID="Label7" runat="server" Text="Due Date:" CssClass="lbl-assign"></asp:Label>
                    <asp:TextBox ID="txtduedate" runat="server" CssClass="txt-assign txt"></asp:TextBox>

                    <ajaxToolkit:CalendarExtender ID="txtduedate_CalendarExtender" runat="server" BehaviorID="txtduedate_CalendarExtender" TargetControlID="txtduedate" Format="yyyy-MM-dd"></ajaxToolkit:CalendarExtender>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>
                <div class="assign">
                    <asp:Label ID="Label8" runat="server" Text="Topic:" CssClass="lbl-assign"></asp:Label>
                    <asp:TextBox ID="txttopic" runat="server" CssClass="txt-assign txt"></asp:TextBox>

                </div>
                <div class="assign">
                    <asp:FileUpload ID="file" runat="server" />

                </div>
                <div class="assign-btn">
                    <asp:Button ID="btncreate" runat="server" Text="Create Task" OnClick="btncreate_Click" CssClass="buttons" />
                    <asp:Button ID="Button4" runat="server" Text="Cancel" CssClass="buttons" />
                </div>
            </div>
        </div>




    </div>

    <div id="bg-blur"></div>

    <script>
        document.getElementById('createRoomLink').addEventListener('click', function (e) {
            e.preventDefault();

            var assignmentContainer = document.getElementById('assignmentContainer');
            var backgroundBlur = document.getElementById('bg-blur');

            if (assignmentContainer.style.display === 'none' || assignmentContainer.style.display === '') {
                backgroundBlur.style.display = 'block !important';
                assignmentContainer.style.display = 'block !impotant';
            } else {
                backgroundBlur.style.display = 'none';
                assignmentContainer.style.display = 'none';
            }
        });
    </script>

</asp:Content>
