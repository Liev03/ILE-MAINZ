<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/professorMasterPage.Master" AutoEventWireup="true" CodeBehind="NotificationDetails.aspx.cs" Inherits="lms.Professor.NotificationDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="../CSS/ProfessorCSS/notificationDetails.css"/>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
  
    <div class="notifications">
    <div class="message">
        <div class="message-from">
            <div class="from">
                <h3> FROM :</h3>
            </div>
            <div class="from-label">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="lbl-from" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="message-from">
               <div class="from">
                      <h3> SUBJECT :</h3>
                  </div>
          <div class="from-label">
              <asp:TextBox ID="TextBox2" runat="server" CssClass="lbl-from" Enabled="False"></asp:TextBox>
           </div>
        </div>
        <div class="message-content">
            <div class="from">
                <h3>MESSAGE : </h3>
            </div>
               <div class="from-label">
                 <asp:TextBox ID="TextBox3" runat="server" CssClass="text-area" Enabled="False" TextMode="MultiLine" Rows="14"></asp:TextBox>
              </div>
        </div>
       <div class="message-button">
           <asp:Button ID="Button1" runat="server" Text="Go Back" CssClass="btn-msg" OnClick="Button1_Click"    />
       </div>
    </div>
        </div>
</asp:Content>
