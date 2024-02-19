<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="lms.sis.SISPAGES.EditProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EditProfile</title>
<link rel="stylesheet" type="text/css" href="/css/site.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />

    <link rel="stylesheet" type="text/css" href="/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.rtl.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.rtl.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap-grid.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap-grid.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap-grid.rtl.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap-grid.rtl.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap-reboot.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap-reboot.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap-reboot.rtl.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap-reboot.rtl.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap-reboot.rtl.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap-utilities.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap-utilities.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap-utilities.rtl.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap-utilities.rtl.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.js" integrity="sha512-AA1Bzp5Q0K1KanKKmvN/4d3IRKVlv9PYgwFPvm32nPO6QS8yH1HO7LbgB1pgiOxPtfeg5zEn2ba64MUcqJx6CA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>

</head>
<body>
<div class="row m-0 p-0 bg-danger">
    <div class="col-lg-3 m-0 bg-danger text-white text-uppercase" style="height: 100vh">
        <div class="container-fluid">
            <div class="bg-danger p-2 w-100 shadow-none" align="center">
                 <center>
                      <img src="../../../Resources/Novaliches%20Senior%20High%20School.png" />
                  </center>
            </div>

            <br />
            <a href="../../../Student/DashBoard.aspx">
                <span class="d-flex">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M2.25 12l8.954-8.955c.44-.439 1.152-.439 1.591 0L21.75 12M4.5 9.75v10.125c0 .621.504 1.125 1.125 1.125H9.75v-4.875c0-.621.504-1.125 1.125-1.125h2.25c.621 0 1.125.504 1.125 1.125V21h4.125c.621 0 1.125-.504 1.125-1.125V9.75M8.25 21h8.25" />
                    </svg>
                    Home
                </span>
            </a>

            <a href="AcademicCalendar.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
                <span class="d-flex">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M6.75 3v2.25M17.25 3v2.25M3 18.75V7.5a2.25 2.25 0 012.25-2.25h13.5A2.25 2.25 0 0121 7.5v11.25m-18 0A2.25 2.25 0 005.25 21h13.5A2.25 2.25 0 0021 18.75m-18 0v-7.5A2.25 2.25 0 015.25 9h13.5A2.25 2.25 0 0121 11.25v7.5m-9-6h.008v.008H12v-.008zM12 15h.008v.008H12V15zm0 2.25h.008v.008H12v-.008zM9.75 15h.008v.008H9.75V15zm0 2.25h.008v.008H9.75v-.008zM7.5 15h.008v.008H7.5V15zm0 2.25h.008v.008H7.5v-.008zm6.75-4.5h.008v.008h-.008v-.008zm0 2.25h.008v.008h-.008V15zm0 2.25h.008v.008h-.008v-.008zm2.25-4.5h.008v.008H16.5v-.008zm0 2.25h.008v.008H16.5V15z" />
                    </svg>
                    Academic Calendar
                </span>
            </a>

            <a href="StudentRegistration.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
                <span class="d-flex">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M3.75 12h16.5m-16.5 3.75h16.5M3.75 19.5h16.5M5.625 4.5h12.75a1.875 1.875 0 010 3.75H5.625a1.875 1.875 0 010-3.75z" />
                    </svg>
                    Student Registration
                </span>
            </a>

            <a href="ViewGrades.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
                <span class="d-flex">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M16.862 4.487l1.687-1.688a1.875 1.875 0 112.652 2.652L6.832 19.82a4.5 4.5 0 01-1.897 1.13l-2.685.8.8-2.685a4.5 4.5 0 011.13-1.897L16.863 4.487zm0 0L19.5 7.125" />
                    </svg>
                    View Grades
                </span>
            </a>

           <a href="EditProfile.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
                <span class="d-flex">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M16.862 4.487l1.687-1.688a1.875 1.875 0 112.652 2.652L6.832 19.82a4.5 4.5 0 01-1.897 1.13l-2.685.8.8-2.685a4.5 4.5 0 011.13-1.897L16.863 4.487zm0 0L19.5 7.125" />
                    </svg>
                    Edit profile
                </span>
            </a>

            <a href="ChangePassword.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
                <span class="d-flex">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M16.862 4.487l1.687-1.688a1.875 1.875 0 112.652 2.652L6.832 19.82a4.5 4.5 0 01-1.897 1.13l-2.685.8.8-2.685a4.5 4.5 0 011.13-1.897L16.863 4.487zm0 0L19.5 7.125" />
                    </svg>
                    Change Password
                </span>
            </a>
        </div>
    </div>

    <div class="col-lg-9 bg-white text-dark">
        <br />
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-6">
                    <p class="inter-700 text-uppercase">Navigation > Edit Profile</p>
                </div>

                <div class="col-lg-6" align="right">
                    <div class="dropdown">
                        <a class="border-0 border-none bg-white text-dark dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-bs-toggle="dropdown" aria-expanded="false">
                            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="28" height="28" ">
                                <path stroke-linecap="round" stroke-linejoin="round" d="M17.982 18.725A7.488 7.488 0 0012 15.75a7.488 7.488 0 00-5.982 2.975m11.963 0a9 9 0 10-11.963 0m11.963 0A8.966 8.966 0 0112 21a8.966 8.966 0 01-5.982-2.275M15 9.75a3 3 0 11-6 0 3 3 0 016 0z" />
                            </svg>
                        </a>
                        <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                             <li class="p-1 text-uppercase"><a id="A1" runat="server" class="dropdown-item bg-danger text-white" onserverclick="Logout_Click">Log-out</a></li>
                        </ul>
                    </div>
                </div>
                <br />
            

                <div class="row">
                    <div class="col-lg-4">
                         <form id="form1" runat="server">
                        <div class="card border-secondary border">
                                <asp:Image  CssClass="card card-body bg-white w-100" align="center" ID="Image1" runat="server" style="background-color: var(--bs-gray-300); height: 100%;"/>       
                            <div class="border-bottom border-secondary w-100"></div>
                            <div class="px-4 mt-2 text-uppercase">
                                <p runat="server" id = "name" align="center" class="m-0"></p><p runat="server" id ="id" align="center" class="m-0"></p>
                            </div>
                       
                        </div>
                 
                        <br />
                        <%--<button type="button" class="btn btn-danger text-uppercase w-100 josefin-sans-700">Upload Photo</button>--%>       
                        
                           <asp:FileUpload ID="btnepupload" runat="server" CssClass="btn btn-danger text-uppercase w-100 josefin-sans-700" onchange="previewImage()"/>
                               
                             <%--<asp:Button ID="btnepupload" runat="server" Text="Upload Photo" CssClass="btn btn-danger text-uppercase w-100 josefin-sans-700" OnClick="btnepupload_Click" />--%>
                    </div>
                    <div class="col-lg-8">
                        <div class="card card-body border border-secondary">
                            <div class="row">
                                <div class="col-5">
                                    <b>Birthday</b>
                                </div>
                                <div class="col-7">
                                    <%--<textarea class="form-control w-100" style="resize: none" rows="1">September 18, 2002</textarea>--%>
                                    <asp:TextBox ID="editprobday" runat="server" CssClass="form-control w-100" style="resize: none" rows="1" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <br />
                            <div class="row">
                                <div class="col-5">
                                    <b>Address</b>
                                </div>

                                <div class="col-7">
                                    <%--<textarea class="form-control w-100" style="resize: none; word-wrap: anywhere" rows="3">Road 5 Area C Brgy. Bagong Silangan Quezon City</textarea>--%>
                                    <asp:TextBox ID="Address" runat="server" CssClass="form-control w-100" style="resize: none; word-wrap: anywhere" rows="3"></asp:TextBox>
                                </div>
                            </div>

                            <br />
                            <div class="row">
                                <div class="col-5">
                                    <b>Contact Number</b>
                                </div>

                                <div class="col-7">
                                    <%--<textarea class="form-control w-100" style="resize: none" rows="1">09937601093</textarea>--%>
                                    <asp:TextBox ID="Contact" runat="server" TextMode="Number" CssClass="form-control w-100"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <br />
                        <div align="right">
                            <%--<button class="btn btn-danger text-uppercase" data-bs-toggle="modal" data-bs-target="#confirm">Save Changes</button>--%>
                            <asp:Button ID="btnepsave" runat="server" Text="Save Changes" CssClass="btn btn-danger text-uppercase" OnClick="btnepsave_Click" />
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</div>
    </form>
</body>
    <script>
        function previewImage() {
            var fileUpload = document.getElementById('<%= btnepupload.ClientID %>');
              var imgPreview = document.getElementById('<%= Image1.ClientID %>');

              if (fileUpload.files && fileUpload.files[0]) {
                  var reader = new FileReader();

                  reader.onload = function (e) {
                      imgPreview.src = e.target.result;
                      imgPreview.style.display = 'block';
                  };
                  reader.readAsDataURL(fileUpload.files[0]);
              }
          }
    </script>
</html>
