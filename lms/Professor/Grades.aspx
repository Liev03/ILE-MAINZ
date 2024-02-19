<%@ Page Title="" Language="C#" MasterPageFile="~/Professor/instructorClassRoom.Master" AutoEventWireup="true" CodeBehind="Grades.aspx.cs" Inherits="lms.Professor.Grades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <link rel="stylesheet" href="../CSS/ProfessorCSS/grades.css" />
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
    <div class="class-submit">

    <div class="submitted">
    <div class="works">
        <h2>Grade Records</h2>
        <asp:Button ID="btnprint" runat="server" CssClass="btn-print" Text="Print" OnClientClick="printGridview();" OnClick="btnprint_Click" />
    </div>


        <div class="grades">
        <asp:GridView ID="gvgraded" runat="server" AutoGenerateColumns="False" CssClass="gridview" OnSelectedIndexChanged="gvgraded_SelectedIndexChanged"  >
            <Columns>
                <asp:BoundField DataField="studentworkid" HeaderText="File ID" SortExpression="materialsId" ReadOnly="True" HeaderStyle-CssClass="hide-column" ItemStyle-CssClass="hide-column" />
                <asp:BoundField DataField="materialsId" HeaderText="File ID" SortExpression="materialsId" ReadOnly="True" HeaderStyle-CssClass="hide-column" ItemStyle-CssClass="hide-column" />
                <asp:BoundField DataField="studentname" HeaderText="Student Name" SortExpression="StudentName" ReadOnly="True"  />
                <asp:BoundField DataField="FileName" HeaderText="File Name" SortExpression="FileName" ReadOnly="True" />
                <asp:BoundField DataField="points" HeaderText="Score" SortExpression="FileName" ReadOnly="True"  />
            </Columns>
        </asp:GridView>

        </div>


</div>
    </div>
      <script type="text/javascript">
      window.onload = function () {
          var grid = document.getElementById('<%= gvgraded.ClientID %>');
          var columnsToHide = grid.querySelectorAll('.hide-column');

          columnsToHide.forEach(function (column) {
              column.style.display = 'none';
          });
      };
  </script>
 <script type="text/javascript">
     function printGridview() {
         var grid = document.getElementById('<%= gvgraded.ClientID %>');

         var printWindow = window.open('', '', 'height=400,width=800');
         printWindow.document.write('<html><head><title>.</title>');
         printWindow.document.write('<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />');
         printWindow.document.write('<style>table {border-collapse: collapse; width: 100%;} th, td {padding: 8px; text-align: center; border-bottom: 1px solid #ddd;} th {background-color: maroon; color: white;}</style>');
         printWindow.document.write('</head><body>');
         printWindow.document.write('<h1 style="text-align: center; color: maroon; font-size:80px;">Novaliches Senior High School LMS     </h1>');
         printWindow.document.write('<div style="display:flex; justify-content:space-between;">');
         printWindow.document.write('</div>');
         printWindow.document.write('<div style="display:flex; justify-content:space-between;">');
         printWindow.document.write('</div>');
         var cleanedHTML = grid.outerHTML.replace(/<th.*?File ID.*?<\/th>|<td.*?File ID.*?<\/td>/g, '');
         printWindow.document.write(cleanedHTML);
         printWindow.document.write('</body></html>');
         printWindow.document.close();
         printWindow.print();
     }
 </script>
</asp:Content>
