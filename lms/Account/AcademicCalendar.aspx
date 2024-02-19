<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcademicCalendar.aspx.cs" Inherits="lms.Account.AcademicCalendar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="UTF-8"/>
     <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
 <title>Academic Calendar</title>
  <link rel="icon" href="../Resources/Novaliches Senior High School.png" type="i    mage/x-icon" />
 <link rel="stylesheet" href="../CSS/myAccountProf.css" />
<link rel="stylesheet" href="//use.fontawesome.com/releases/v5.0.7/css/all.css"/>   
 <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.min.js"></script>
 <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js" charset="utf-8"></script>
 <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'/>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
        <link rel="stylesheet" type="text/css" href="../css/site.css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap.css" />

    <link rel="stylesheet" type="text/css" href="../css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap.rtl.css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap.rtl.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap-grid.css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap-grid.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap-grid.rtl.css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap-grid.rtl.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap-reboot.css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap-reboot.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap-reboot.rtl.css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap-reboot.rtl.min.css" />
<%--    <link rel="stylesheet" type="text/css" href="/css/bootstrap-reboot.rtl.min.css" />--%>
    <link rel="stylesheet" type="text/css" href="../css/bootstrap-utilities.css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap-utilities.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap-utilities.rtl.css" />
    <link rel="stylesheet" type="text/css" href="../css/bootstrap-utilities.rtl.min.css" />  
    <style>
    .grid-container {
        display:grid;
        place-content:center;

        color:black;
        width:100%;
    }
    .custom-calendar {
        width: 65vw;
        font-family: Arial, sans-serif;
        border: 1px solid #ccc;
        padding: 10px;
        text-align: center;
        
    }

    .calendar-day-header {
        background-color: #ff6666;
        color: #fff;
        font-weight: bold;
        padding: 10px;
    }

    .calendar-selected-day {
        background-color: gray;
        color: #fff;
        font-weight: bold;
        padding:0;
    }

    .custom-calendar td {
        text-align: center;
        border: 1px solid #ccc;
        background-color: #fff;
        transition: background-color 0.3s;
        width: 10px;
        height: 85px;
        
    }

    .custom-calendar a {
        text-decoration: none;
        color: gray;
        display: block;
        padding: 5px;
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
     <div class="col-lg-9 bg-white text-dark">
        <div class="container-fluid">
             <div class="row">
                  <div class="grid-container">
                      <asp:Calendar BackColor="White" ID="ReadOnlyCalendar" OnDayRender="ReadOnlyCalendar_DayRender" runat="server" CssClass="auto-style1 custom-calendar" Height="499px" NextPrevStyle-BackColor="#CC3399"
                          NextPrevStyle-ForeColor="#CC0000" OtherMonthDayStyle-BackColor="#999999" SelectedDayStyle-BackColor="Silver" BorderColor="Black"
                          BorderStyle="Solid" BorderWidth="1px" AutoPostBack="true" TodayDayStyle-BackColor="#CCCCFF" WeekendDayStyle-BackColor="#CC6699">
                          <DayHeaderStyle CssClass="calendar-day-header" />
                          <DayStyle BorderColor="#CC6699" BorderStyle="Solid" BorderWidth="1px" />
                          <NextPrevStyle BackColor="#669999" ForeColor="#CC0000" />
                          <OtherMonthDayStyle BackColor="#CCCCCC" BorderColor="#CC6699" BorderStyle="Solid" BorderWidth="1px" ForeColor="Gray" />
                          <SelectedDayStyle CssClass="calendar-selected-day" BackColor="#999999" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                      </asp:Calendar>
                      <asp:TextBox ID="TextBox1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Enabled="False" Text="Red == important , orangeRed == close to today , neutral == lime green" CssClass="text-center mt-2"></asp:TextBox>
                  </div>
             </div>
          </div>
    </div>
    </main>
</div>

    <script>
      
        $(".sub-btn").click(function() {
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
            </form>
</body>
</html>
