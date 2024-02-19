<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Grades.aspx.cs" Inherits="lms.Account.Grades" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta charset="UTF-8"/>
     <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
 <title>View Grades</title>
  <link rel="icon" href="../Resources/Novaliches Senior High School.png" type="i    mage/x-icon" />
 <link rel="stylesheet" href="../CSS/myAccountProf.css" />
<link rel="stylesheet" href="//use.fontawesome.com/releases/v5.0.7/css/all.css"/>   
 <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.min.js"></script>
 <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js" charset="utf-8"></script>
 <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'/>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
            <link rel="stylesheet" href="../CSS/Announce.css" />
  
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
            
            <div class="school-year">        
                                     
                        <div class="status">
                            <div class="status-child">
                               <h3> School Year: </h3>
                                <asp:Label ID="sy" runat="server" Text="" CssClass="lbl-status"></asp:Label>
                            </div>

                            <div class="status-child">
                               <h3>  Admission Status:</h3><asp:Label ID="Admission_Status" runat="server" Text=""  CssClass="lbl-status"></asp:Label>
                            </div>
                        </div>
                        <div class="status">
                             <div class="status-child">
                               <h3>  Semester:</h3> 
                               <asp:Label ID="Sem" runat="server" Text="" CssClass="lbl-status"></asp:Label>
                            </div>
                             <div class="status-child">
                              <h3>   Strand:</h3> 
                                 <asp:Label ID="Strand" runat="server" Text="" CssClass="lbl-status"></asp:Label>
                            </div>
                        </div>
                
                
            </div>      
            <div class="ddlist">
                <asp:DropDownList ID="btnviewgrades" runat="server" CssClass="form-select" OnSelectedIndexChanged ="btnviewgrades_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                </asp:DropDownList>
          
            </div>

                <div class="gridContainer">
                    <asp:GridView CssClass="Grid" ID="grid" runat="server" CellPadding="30" ForeColor="#333333" GridLines="None"  Width="100%" SelectedRowStyle-BackColor="#6666FF"
                         Font-Size="Medium" HorizontalAlign="Center">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990f02" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#bc544b" Font-Bold="True" ForeColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
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
