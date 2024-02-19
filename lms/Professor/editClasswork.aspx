<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/instructorClassRoom.Master" AutoEventWireup="true" CodeBehind="editClasswork.aspx.cs" Inherits="lms.Professor.WebForm11" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/ProfessorCSS/editClasswork.css" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

   <style>
    .hide-column {
            display: none;
        }
</style>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="Classroom" runat="server">
    <div class="classwork">
        <div class="class-head">
           <h2>Update Classwork</h2>
        </div>

        <div class="edit">
            <div class="assignment">
                <div class="assign-title">
                    <div class="title">
                        <asp:Label ID="lbltitle" runat="server" Text="Title:" CssClass="lbl-title"></asp:Label>
                        <asp:TextBox ID="txtmaterialsname" runat="server" CssClass="txt-title" placeholder="Enter Title"></asp:TextBox>
                    </div>
                    <div class="instructions">
                        <asp:Label ID="Label4" runat="server" Text="Instructions:" CssClass="lbl-instruc"></asp:Label>
                        <asp:TextBox ID="txtinstructions" runat="server" TextMode="MultiLine" Rows="5" CssClass="txt-instruc" placeholder="Write instructions about the post"></asp:TextBox>
                    </div>
                </div>
                <div class="assign-description">
                    <div class="assign">
                        <asp:Label ID="lblposttype" runat="server" Text="Create Post: " CssClass="lbl-assign"></asp:Label>
                        <asp:RadioButton ID="rbassignment" runat="server" Text="Assignment" CssClass="rdb-post" GroupName="MaterialsGroup" />
                        <asp:RadioButton ID="rbquiz" runat="server" Text="Quiz" CssClass="rdb-post" GroupName="MaterialsGroup" />
                        <asp:RadioButton ID="rbmaterials" runat="server" Text="Materials" CssClass="rdb-post" GroupName="MaterialsGroup" />

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

                        <ajaxToolkit:CalendarExtender ID="txtduedate_CalendarExtender" runat="server" BehaviorID="txtduedate_CalendarExtender" TargetControlID="txtduedate"></ajaxToolkit:CalendarExtender>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </div>
                    <div class="assign">
                        <asp:Label ID="Label8" runat="server" Text="Topic:" CssClass="lbl-assign"></asp:Label>
                        <asp:TextBox ID="txttopic" runat="server" CssClass="txt-assign txt"></asp:TextBox>

                    </div>
                     <div class="assign">

                         
<asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvFiles_SelectedIndexChanged" CssClass="gridview">
    <Columns>
        <asp:BoundField DataField="materialsId" HeaderText="File ID" SortExpression="materialsId" ReadOnly="True" HeaderStyle-CssClass="hide-column" ItemStyle-CssClass="hide-column" />
        <asp:BoundField DataField="FileName" HeaderText="File Name" SortExpression="FileName" ReadOnly="True" HeaderStyle-CssClass="hide-column" />
        <asp:CommandField ShowSelectButton="True" SelectText="Download" HeaderStyle-CssClass="hide-column" />
    </Columns>
</asp:GridView>

                  <%--  <asp:DropDownList ID="ddlFiles" runat="server" CssClass="custom-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlFiles_SelectedIndexChanged">
                    </asp:DropDownList>--%>
                           </div>

                   <%-- <asp:Button ID="btnDownload" runat="server" Text="Download File" OnClick="btnDownload_Click" />--%>
                    <div class="assign">

                        <asp:FileUpload ID="file" runat="server" />
                    </div>
                    <div class="assign-btn">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update Classwork" CssClass="buttons" OnClick="btnUpdate_Click" />
                        <a href='<%= "Classwork.aspx?roomid=" + Request.QueryString["roomid"] %>'>Back to Classwork</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
