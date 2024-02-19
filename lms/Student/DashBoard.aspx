<%@ Page Title="" Language="C#" MasterPageFile="~/Student/studentMasterPage.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="lms.Student.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="../CSS/AdminCSS/DashBoard.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
       <div class="dashboard">
      <div class="dash-list">
          <div class="text">
              <p>ROOMS </p>

          </div>
          <div class="icon-num">
              <span><%= GetActiveRoomsCount() %></span>
              <i class="fa-solid fa-users-rectangle"></i>
          </div>
      </div>
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
