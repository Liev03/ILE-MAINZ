<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/professorMasterPage.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="lms.Professor.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <link rel="stylesheet" href="../CSS/ProfessorCSS/Dashboard.css"/>--%>
    <link rel="stylesheet" href="../CSS/AdminCSS/DashBoard.css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="dashboard">
        <div class="dash-list">
            <div class="text">
                <p>TOTAL ROOMS </p>

            </div>
            <div class="icon-num">
                <span><%= GetActiveRoomsCount() %></span>
                <i class="fa-solid fa-users-rectangle"></i>
            </div>
        </div>
        <%--  <div class="dash-list">
         <div class="text">
          <p>TOTAL INSTRUCTORS </p>
    
        </div>--%>
        <%--<div class="icon-num">                  
           <span><%= GetTotalTeacherCount() %></span>
          <i class="fas fa-users"></i>  
    </div>
</div>--%>
        <div class="dash-list">
            <div class="text">
                <p>NOTIFICATIONS</p>

            </div>
            <div class="icon-num">
                <span><%= GetTotalNotificationCount() %></span>
                <i class="fas fa-bell"></i>
            </div>
        </div>


    </div>
</asp:Content>
