<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/instructorClassRoom.Master" AutoEventWireup="true" CodeBehind="People1.aspx.cs" Inherits="lms.Professor.WebForm14" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" href="../CSS/ProfessorCSS/People1.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Classroom" runat="server">

 <div class="instructors">
     <div class="people1">
    <div class="people-view">
        <h2>ROOM INSTRUCTORS </h2>

        <div class="instruc-creator">

            <div class="icon">
                <i class="fas fa-user"></i>
            </div>
            <div class="lbl-name">
                <asp:Label ID="lblinstructormain" runat="server" Text="Label" CssClass="instructor-name"></asp:Label>
            </div>
        </div>


    </div>
    <div class="student-list">
        <h2>Student Lists</h2>
        <div class="student-table">
            <asp:GridView ID="studentlist" runat="server" AutoGenerateColumns="false" EmptyDataText="No Student Found">
                <Columns>
                    <asp:BoundField DataField="StudentEmail" HeaderText="Email" HeaderStyle-CssClass="subj" />

                </Columns>
            </asp:GridView>
        </div>
    </div>
</div>
     </div>

</asp:Content>
