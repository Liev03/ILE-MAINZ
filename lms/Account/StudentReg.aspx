<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentReg.aspx.cs" Inherits="lms.Account.StudentReg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta charset="UTF-8"/>
     <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
 <title>Registration Form</title>
  <link rel="icon" href="../Resources/Novaliches Senior High School.png" type="i    mage/x-icon" />
 <link rel="stylesheet" href="../CSS/myAccountProf.css" />
    <link rel="stylesheet" href="../CSS/Announce.css" />
<link rel="stylesheet" href="//use.fontawesome.com/releases/v5.0.7/css/all.css"/>   
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js" charset="utf-8"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/2.10.2/umd/popper.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
<script src="https://rawgit.com/eKoopmans/html2pdf/master/dist/html2pdf.bundle.js"></script>
<link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'/>
<script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
     
    <style>
    .Grid {
        text-align: center;
        margin-bottom: 30px;
    }

    .gridContainer {
        width: 100%;
        height: 400px;
        overflow: scroll;
    }

    ::-webkit-scrollbar {
        width: 8px;
    }

    ::-webkit-scrollbar-track {
        background: #f1f1f1;
    }

    ::-webkit-scrollbar-thumb {
        background: #888;
        border-radius: 4px;
    }
    .modal-dialog {
        max-width: 1123px;
        max-height: 794px;
        place-content:center;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
             <header>
        <div class="header-content">
            <a href="/Professor/DashBoard.aspx"><img src="../Resources/Novaliches Senior High School.png" alt="LOGO" /></a>
           <a href="/Professor/DashBoard.aspx"> <h2> Novaliches High School</h2></a>
        </div>
        <div class="header-login">
             <div class="dropdown">
                    <button class="dropdown-toggle"><i class='bx bx-user-circle icon'></i><i class='bx bxs-down-arrow arrow'></i></button>
                     <div class="dropdown-menu">
                          <a href="#"> <i class='bx bxs-user-circle'></i>My Account</a>
                         <a href="/Account/Logout.aspx"> <i class="fas fa-sign-out-alt"></i> Logout</a>   
                 </div>
            </div>
        </div>
        </header>

    <div class="container">
      <div class="side-bar">

        <div class="menu">
            <div class="item-logo">
                <asp:Image ID="Image1" runat="server" CssClass="img-profile" />                 
                <asp:Label ID="lblUserEmail" runat="server" Text="" CssClass="admin-label"></asp:Label>
             
            </div>          
             <div class="item">
                 <a href="Announce.aspx" class="sub-btn" ><i class="fas fa-bell"></i>Reminders</a>
             </div>
             <div class="item">
                  <a href="AcademicCalendar.aspx" class="sub-btn" ><i class="fas fa-calendar"></i>Academic Calendar</a>
              </div>
              <div class="item">
                    <a href="StudentReg.aspx" class="sub-btn" ><i class="fas fa-registered"></i>Student Registration</a>
             </div>
            <div class="item">
    <a href="Grades.aspx"> <i class="fas fa-star"></i>View Grades</a>
 </div>
             <div class="item">
                 <a href="MyAccountStudent.aspx"> <i class='bx bxs-user-circle'></i>My Account</a>
            </div>
            <div class="item">
                <a href="../Student/DashBoard.aspx" class="sub-btn" ><i class="fas fa-sign-out-alt"></i>Go to DashBoard</a>
            </div>
        </div>
    </div>

     <main class="content">
      
         <div class="row">
             
          <div class="reminders-head">
              <asp:Label runat="server" ID="lbl1"></asp:Label>
       
           </div>
                           
                 <div class="filter">                   
                        
                             <div class="s-year">
                               <span> School year</span>
                                  <div>
                                 <asp:DropDownList runat="server" ID="school_year" AutoPostBack="true" CssClass="form-control">
                                     <asp:ListItem id="current" Text="current year" />
                                 </asp:DropDownList>
                                        </div>
                             </div>
                             <div class="semester">
                                <span> Semester</span>
                                 <div>
                                 <asp:DropDownList runat="server" ID="sem" AutoPostBack="true" CssClass="form-control">
                                     <asp:ListItem id="empty" Text="" Selected />
                                     <asp:ListItem id="currentSem" Text="1st sem" />
                                     <asp:ListItem id="prevSem" Text="2nd sem" />
                                 </asp:DropDownList>
                                     </div>
                             </div>

                             <div class="buttons">
                                 <asp:Button ID="display" runat="server" class="filter-btn" Text="Display" OnClick="display_Click" />
                                <a href="#" class="filter-btn" id="regform">Registration Form</a>
                             </div>
                      
                  
                 </div>
             
             <div class="information">
                 <div class="left">
                     <div class="rows">
                         <div class="txt">
                             <b>Name</b>
                         </div>

                         <div class="data">
                             <p runat="server" id="StudName"></p>
                         </div>
                     </div>

                     <div class="rows">
                         <div class="txt">
                             <b>Program</b>
                         </div>

                         <div class="data">
                             <p runat="server" id="Strand"></p>
                         </div>
                     </div>

                     <div class="rows">
                         <div class="txt">
                             <b>Status</b>
                         </div>

                         <div class="data">
                             <p runat="server" id="AdmisionStatus"></p>
                         </div>
                     </div>

                     <div class="rows">
                         <div class="txt">
                             <b>School Year</b>
                         </div>

                         <div class="data">
                             <p runat="server" id="schoolYear"></p>
                         </div>
                     </div>
                 </div>

                 <div class="right">
                     <div class="rows">
                         <div class="txt">
                             <b>Student No.</b>
                         </div>

                         <div class="data">
                             <p runat="server" id="studID"></p>
                         </div>
                     </div>

                     <div class="rows">
                         <div class="txt">
                             <b>Year Level</b>
                         </div>

                         <div class="data">
                             <p runat="server" id="year"></p>
                         </div>
                     </div>

                     <div class="rows">
                         <div class="txt">
                             <b>Section</b>
                         </div>

                         <div class="data">
                             <p runat="server" id="section"></p>
                         </div>
                     </div>

                     <div class="rows">
                         <div class="txt">
                             <b>Semester</b>
                         </div>

                         <div class="data">
                             <p runat="server" id="currentSemester"></p>
                         </div>
                     </div>
                 </div>
             </div>
               <div class="datagrid">
      <h3>Enlisted Subjects</h3>       
      <div class="gridContainer mb-4">
          <asp:GridView CssClass="Grid" ID="grid" runat="server" CellPadding="40" ForeColor="#333333" GridLines="None" Height="148px" Width="100%" SelectedRowStyle-BackColor="#6666FF"
               Font-Size="Medium" HorizontalAlign="Center">
              <AlternatingRowStyle BackColor="White" />
              <Columns>
              </Columns>
              <EditRowStyle BackColor="#2461BF" />
              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle BackColor="#EFF3FB" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <SortedAscendingCellStyle BackColor="#F5F7FB" />
              <SortedAscendingHeaderStyle BackColor="#6D95E1" />
              <SortedDescendingCellStyle BackColor="#E9EBEF" />
              <SortedDescendingHeaderStyle BackColor="#4870BE" />
          </asp:GridView>
      </div>
  </div>
         </div>
     
         </main>
</div>
  
  <div class="link" id="regFormModal">
       <div id="regFormModalContent">
        <div class="sy">
            <p>2022- 2023</p>
        </div>
      <div class="head-form">
          <div class="logo">
              <img src="../Resources/Novaliches Senior High School.png" alt="LOGO" />
          </div>
          <div class="head-txt">
              <h2>NOVALICHES SENIOR HIGHSCHOOL</h2>
              <span>Registration Form</span>
              <p>TS Cruz Subdivision, 1 Lakandula, Novaliches,<br /> Quezon City, Metro Manila</p>
          </div>
           <div class="logo">
               <img src="../Resources/Novaliches Senior High School.png" alt="LOGO" />
            </div>
      </div>
      <div class="information">
    <div class="left">
        <div class="rows">
            <div class="txt">
                <b>Name</b>
            </div>

            <div class="data">
                <p runat="server" style="text-transform:capitalize" id="RegFormName"></p>
            </div>
        </div>

        <div class="rows">
            <div class="txt">
                <b>Program</b>
            </div>

            <div class="data">
                <p style="text-transform:capitalize" runat="server" id="RegFormStrand"></p>
            </div>
        </div>

        <div class="rows">
            <div class="txt">
                <b>Status</b>
            </div>

            <div class="data">
                <p runat="server" style="text-transform:capitalize" id="RegFormAdmissionStatus"></p>
            </div>
        </div>

        <div class="rows">
            <div class="txt">
                <b>School Year</b>
            </div>

            <div class="data">
                <p runat="server" id="RegFormSchoolYear"></p>
            </div>
        </div>
    </div>

    <div class="right">
        <div class="rows">
            <div class="txt">
                <b>Student No.</b>
            </div>

            <div class="data">
                <p runat="server" id="RegFormStudID"></p>
            </div>
        </div>

        <div class="rows">
            <div class="txt">
                <b>Year Level</b>
            </div>

            <div class="data">
                <p runat="server" id="RegFormYear"></p>
            </div>
        </div>

        <div class="rows">
            <div class="txt">
                <b>Section</b>
            </div>

            <div class="data">
                <p runat="server" style="text-transform:capitalize" id="RegFormSection"></p>
            </div>
        </div>

        <div class="rows">
            <div class="txt">
                <b>Semester</b>
            </div>

            <div class="data">
                <p runat="server" id="RegFormCurrentSemester"></p>
            </div>
        </div>
    </div>
</div>
    <div class="datagrid1">
    <h3>Enlisted Subjects</h3>       
    <div class="gridContainer1 mb-4">
        <asp:GridView CssClass="Grid" ID="Regformview" runat="server" CellPadding="40" ForeColor="#333333" GridLines="None" Height="148px" Width="100%" SelectedRowStyle-BackColor="#6666FF"
             Font-Size="Medium" HorizontalAlign="Center">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>
    </div>
</div>
      <div class="form-btn">
           <button type="button" class="buttons-form" id="regFormDownloadPdfBtn">Download</button>
           <asp:Button ID="Button4" runat="server" Text="Cancel" CssClass="buttons-form"/>
           
      </div>

 </div>

 <div id="bg-blur"></div>
    </form>
     <script>
         document.getElementById('regform').addEventListener('click', function (e) {
             e.preventDefault();

             var assignmentContainer = document.getElementById('regFormModal');
             var backgroundBlur = document.getElementById('bg-blur');

             if (assignmentContainer.style.display === 'none' || assignmentContainer.style.display === '') {
                 backgroundBlur.style.display = 'block ';
                 assignmentContainer.style.display = 'block';
             } else {
                 backgroundBlur.style.display = 'none';
                 assignmentContainer.style.display = 'none';
             }
         });
     </script>
   <script>
       const regformModal = document.getElementById("regform");
       const regFormModal = document.getElementById("regFormModal");
       const close = document.getElementById("close");

       function downloadPdf() {
           const element = document.getElementById("regFormModalContent");

           html2pdf(element, {
               margin: 10,
               filename: 'Registration_Form.pdf',
               image: { type: 'jpeg', quality: 0.98 },
               html2canvas: { scale: 2 },
               jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' }
           });
       }

       $(document).ready(function () {
           $("#regFormDownloadPdfBtn").click(function () {
               downloadPdf();
           });
       });

       regformModal.addEventListener("click", () => {
           document.getElementById("RegFormName").innerText = document.getElementById("StudName").innerText;
           document.getElementById("RegFormStrand").innerText = document.getElementById("Strand").innerText;
           document.getElementById("RegFormAdmissionStatus").innerText = document.getElementById("AdmisionStatus").innerText;
           document.getElementById("RegFormSchoolYear").innerText = document.getElementById("schoolYear").innerText;
           document.getElementById("RegFormStudID").innerText = document.getElementById("studID").innerText;
           document.getElementById("RegFormYear").innerText = document.getElementById("year").innerText;
           document.getElementById("RegFormSection").innerText = document.getElementById("section").innerText;
           document.getElementById("RegFormCurrentSemester").innerText = document.getElementById("currentSemester").innerText;
           $(regFormModal).modal('show');
       });
       close.addEventListener("click", function () {
           $(regFormModal).modal('hide');
       });
   </script>
      <script>

          $(".sub-btn").click(function () {
              $(this).siblings(".sub-menu").slideToggle();
              $(this).find(".dropdown").toggleClass("rotate");
          });
      </script>
      <script>
          const dropdownToggle = document.querySelector(".dropdown-toggle");
          const dropdownMenu = document.querySelector(".dropdown-menu");

          dropdownToggle.addEventListener("click", (e) => {
              e.preventDefault();
              dropdownMenu.style.display = dropdownMenu.style.display === "block" ? "none" : "block";
          });

          window.addEventListener("click", (e) => {
              if (!dropdownToggle.contains(e.target)) {
                  dropdownMenu.style.display = "none";
              }
          });
      </script>
     <script>

         function handleLinkClick(link) {

             var menuItems = document.querySelectorAll('.menu .item');
             menuItems.forEach(function (item) {
                 item.classList.remove('active');
             });


             link.parentNode.classList.add('active');


             localStorage.setItem('activeLink', link.getAttribute('href'));
         }


         var activeLink = localStorage.getItem('activeLink');

         if (activeLink) {

             var links = document.querySelectorAll('.menu a');
             links.forEach(function (link) {
                 if (link.getAttribute('href') === activeLink) {
                     handleLinkClick(link);
                 }
             });
         }

         var links = document.querySelectorAll('.menu a');


         links.forEach(function (link) {
             link.addEventListener('click', function () {
                 handleLinkClick(this);
             });
         });
     </script>
</body>
</html>
