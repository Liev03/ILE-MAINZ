<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/professorMasterPage.Master" AutoEventWireup="true" CodeBehind="inviteStudents.aspx.cs" Inherits="lms.Professor.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/ProfessorCSS/inviteStudents.css" />
      <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
  <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    
    <div class="reports">
      
        <div class="report-tbl">
            <div class="invite-room">
                <div class="invite-p">
                    <p>List of Created Rooms</p>
                </div>
                <div class="invite-room-list">

                    

     <div class="subjects">
     <asp:GridView ID="roomdetailsGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="No Subject Found">
        <Columns>
                <asp:BoundField DataField="subjectname" HeaderText="Subject Name" HeaderStyle-CssClass="subj"/>
                 <asp:BoundField DataField="section" HeaderText="Section" HeaderStyle-CssClass="subj"/>

                <asp:TemplateField HeaderText="" ItemStyle-Width="180px">
           <ItemTemplate>
                <asp:LinkButton ID="roomLink" runat="server" CssClass="btn-list"
                        PostBackUrl='<%# "StudentInvite.aspx?roomid=" + Eval("roomid") %>'
Text="Invite Students" />

          </ItemTemplate>
          </asp:TemplateField>
        </Columns>
     </asp:GridView>


                   
              
                </div>
            </div>
        </div>
        </div>
    </div>
</asp:Content>
