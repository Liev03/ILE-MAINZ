<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Announce.aspx.cs" Inherits="lms.Account.Announce" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="UTF-8"/>
     <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
 <title>Announcements</title>
  <link rel="icon" href="../Resources/Novaliches Senior High School.png" type="i    mage/x-icon" />
 <link rel="stylesheet" href="../CSS/myAccountProf.css" />
     <link rel="stylesheet" href="../CSS/Announce.css" />
<link rel="stylesheet" href="//use.fontawesome.com/releases/v5.0.7/css/all.css"/>   
 <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.min.js"></script>
 <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js" charset="utf-8"></script>
 <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'/>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
     

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
                <div class="reminders-body">
                    <div class="reminder">
                         <p><b>Nova High Student Information System</b> empowers students with a user-friendly and accessible platform that enhances their academic experience and personal development. This innovative system provides students with easy access to their academic records, class schedules, and grades, allowing them to stay informed and engaged in their educational journey. </p>
                     </div>  
                    <div class="reminder">
                         <b>Dos for Students Using Nova High Student Information System:</b>

                     </div>  
                    <div class="rem">
                         <p>Regularly Check Your Portal:</p>
                    </div>
                 <div class="reminder">                  
                    <p>Do log in to the Nova High Student Information System regularly to stay updated on your academic records, class schedules, and grades. This will help you keep track of your progress and identify areas for improvement.</p>
                 </div>  
                    <div class="rem">
                         <p>Stay Engaged in Your Educational Journey:</p>
                    </div>
               <div class="reminder">                  
                    <p>Do use the platform to actively engage with your educational journey. Reviewing your academic records and understanding your grades can provide valuable insights into your strengths and areas that may need additional attention.</p>
                </div>  
                <div class="reminder">
                    <b>Don'ts for Students Using Nova High Student Information System:</b>
                 </div>  
                    <div class="rem">
                        <p>Don’t Share Your Login Credentials:</p>
                    </div>
                <div class="reminder">              
                    <p>Don't share your login credentials with others. Your student information is private, and sharing login details compromises the security and confidentiality of your academic records.</p>
                 </div>  
                    <div class="rem">
                         <p>Avoid Misuse of the System:</p>
                    </div>
                 <div class="reminder">                  
                    <p>Don't misuse the system by attempting to alter grades or access information that is not relevant to you. Such actions violate school policies and can lead to serious consequences.</p>
                </div>
                </div>
            </div>

             </main>
    </div>
</form>



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
</body>
</html>
