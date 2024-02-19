<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAccountStudent.aspx.cs" Inherits="lms.Account.MyAccountStudent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta charset="UTF-8"/>
     <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
 <title>My Account</title>
  <link rel="icon" href="../Resources/Novaliches Senior High School.png" type="i    mage/x-icon" />
 <link rel="stylesheet" href="../CSS/myAccountProf.css" />
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
    <a href="Grades.aspx"><i class="fas fa-star"></i>View Grades</a>
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
             <div class="account">
                 <div class="account-img">   
                     <div class="profile">
                         <h2>Profile Picture</h2>
                          <asp:Image ID="Image2" runat="server" CssClass="img-preview" EnableViewState="true" Visible="true"  />
                         <div class="img-btn">                      
                         <asp:FileUpload ID="FileUpload1" runat="server"  onchange="showImagePreview()"  CssClass="upload"    />
                          <asp:Button ID="btnchangeimage" runat="server" Text="Update Image" CssClass="changge-image" OnClick="btnchangeimage_Click" />
                               </div>
                          <asp:TextBox ID="txtusername" runat="server" CssClass="username" Enabled="False"></asp:TextBox>

                         </div>
                     <div class="password">

                         <h2> Change Password</h2>
                           <div class="info-pass">
                                 <asp:TextBox ID="TextBox5" runat="server" CssClass="info-text" placeholder="Type Current Password" ></asp:TextBox>
                             </div>
                         <div class="info-pass">
                                 <asp:TextBox ID="TextBox6" runat="server" CssClass="info-text" placeholder="Type New Password"></asp:TextBox>
                               </div>
                          <div class="info-pass">
                            <asp:TextBox ID="TextBox7" runat="server" CssClass="info-text" placeholder="Re-type New Password"></asp:TextBox>
                          </div>
                          <div class="info-pass pass-btn">
                              <asp:Button ID="Button1" runat="server" Text="Change Password" CssClass="pass-button" OnClick="Button1_Click" />
                                 <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="pass-button" OnClick="Button2_Click" />
                          </div>
                     </div>                   
                 </div>
                 <div class="account-info">
                     <h2> Personal Information </h2>
                     <div class="information">
                         
                         <div class="info-lbl">
                             <asp:Label ID="Label1" runat="server" Text="Full Name" CssClass="info-label"></asp:Label>
                         </div>
                         <div class="info-txt">
                             <asp:TextBox ID="TextBox1" runat="server" CssClass="info-text" Enabled="False"></asp:TextBox>
                        </div>
                     </div>
                      <div class="information">
                          <div class="info-lbl">
                                <asp:Label ID="Label2" runat="server" Text="Citzenship" CssClass="info-label"></asp:Label>
                          </div>
                           <div class="info-txt">
                                   <asp:TextBox ID="TextBox2" runat="server" CssClass="info-text" Enabled="False"></asp:TextBox>
                                  </div>
                         </div>
                      <div class="information">
                             <div class="info-lbl">
                                  <asp:Label ID="Label3" runat="server" Text="Student No." CssClass="info-label"></asp:Label>
                              </div>
                          <div class="info-txt">
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="info-text" Enabled="False"></asp:TextBox>
                            </div>
                    </div>
                      <div class="information">
                             <div class="info-lbl">
                                  <asp:Label ID="Label4" runat="server" Text="Address" CssClass="info-label"></asp:Label>
                              </div>
                       <div class="info-txt">
                          <asp:TextBox ID="TextBox4" runat="server" CssClass="info-text" Enabled="False"></asp:TextBox>
                        </div>
                    </div>
                         <div class="information">
                              <div class="info-lbl">
                                     <asp:Label ID="Label5" runat="server" Text="Birthday" CssClass="info-label"></asp:Label>
                               </div>
                          <div class="info-txt">
                               <asp:TextBox ID="TextBox8" runat="server" CssClass="info-text" Enabled="False"></asp:TextBox>
                          </div>
                     </div>
                         <div class="information">
                               <div class="info-lbl">
                                    <asp:Label ID="Label6" runat="server" Text="Email" CssClass="info-label"></asp:Label>
                              </div>
                          
                             <div class="info-txt">
                                  <asp:TextBox ID="TextBox9" runat="server" CssClass="info-text" Enabled="False"></asp:TextBox>
                          </div>
                     </div>
                       <div class="app-pass">
                           <h2> SMTP Password </h2>
                            <div class="info-txt">
                               <asp:TextBox ID="TextBox10" runat="server" CssClass="info-text" Placeholder="Enter your SMTP pass from Email"></asp:TextBox>
                            </div>
                            <div class="info-btn">
                                 <asp:Button ID="Button4" runat="server" Text="Submit SMTP" CssClass="smtp-btn" OnClick="Button3_Click" />
                                <asp:Button ID="Button3" runat="server" Text="Update SMTP" CssClass="smtp-btn" OnClick="Button4_Click" />
                              
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

           <script type="text/javascript">
               function showImagePreview() {
                   var imgPreview = document.getElementById('<%= Image2.ClientID %>');
           var fileUpload = document.getElementById('<%= FileUpload1.ClientID %>');

                   if (fileUpload.files && fileUpload.files[0]) {
                       var reader = new FileReader();
                       reader.onload = function (e) {
                           imgPreview.src = e.target.result;
                           imgPreview.style.display = 'block';
                       };
                       reader.readAsDataURL(fileUpload.files[0]);
                   } else {
                       imgPreview.style.display = 'none';
                   }
               }
           </script>
          <script>
              function showSuccessMessage() {
                  Swal.fire({
                      icon: 'success',
                      text: 'The Teacher Profile Picture has been updated successfully.  ',
                      ShowConfitmButton: true,
                      comfirmButtonText: 'Confirm',
                      confirmButtonColor: '#3085d6',
                      cancelButtonColor: '#d33',
                  }).then((result) => {
                      if (result.isConfirmed) {
                          window.location.href = 'MyAccountProf.aspx';
                   
                      }
                  });
              }
          </script>
    </form>
</body>
</html>
