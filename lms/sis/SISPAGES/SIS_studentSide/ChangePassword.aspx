<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="lms.sis.SISPAGES.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
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
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>

</head>
<body>
   
    <div class="modal fade" id="confirm" tabindex="-1" aria-labelledby="confirmLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content bg-danger text-white">
            <div class="modal-body">
                <div align="right">
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close" align="right"></button>
                </div>

                <br /><br />
                <p class="text-uppercase" align="center">Are you sure you want to change your password?</p>

                <br/><br/>
                <div align="center">
                    <div class="w-100 d-flex text-uppercase">
                        <button type="button" class="btn btn-danger border-white w-50 mx-1">Cancel</button>
                        <button type="button" class="btn btn-success border-white w-50 mx-1" onclick="$('#confirm').modal('hide'); $('#success').modal('show')">Confirm</button>
                    </div>
                </div>
                <br /><br />
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="success" tabindex="-1" aria-labelledby="successLabel" aria-hidden="true" data-bs-backdrop="static">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content bg-success text-white">
            <div class="modal-body">
                <div align="right">
                    <button type="button" class="btn-close btn-close-white" onclick="location.reload()" aria-label="Close" align="right"></button>
                </div>

                <br /><br />
                <p class="text-uppercase" align="center">Password was successfully changed!</p>
                <br /><br />
            </div>
        </div>
    </div>
</div>

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
                    <p class="inter-700 text-uppercase">Navigation > Change Password</p>
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
                    <div class="col-lg-7">
                        <form id="form1" runat="server">
                        <div class="card card-body bg-danger container text-white">
                            <label for="current-password" class="form-control-label">Current Password</label>
                           <%-- <input id="current-password" name="current-password" class="form-control" />--%>
                            <asp:TextBox ID="cpass" runat="server" CssClass="form-control"></asp:TextBox>

                            <label for="new-password" class="form-control-label mt-2">New Password</label>
                            <%--<input id="new-password" name="new-password" class="form-control" />--%>
                             <asp:TextBox ID="npass" runat="server" CssClass="form-control"></asp:TextBox>

                            <label for="confirm-password" class="form-control-label mt-2">Confirm Password</label>
                            <%--<input id="confirm-password" name="confirm-password" class="form-control" />--%>
                             <asp:TextBox ID="confirmpass" runat="server" CssClass="form-control"></asp:TextBox>

                            <br/>
                            <div class="row">
                                <div class="col-2"></div>

                                <div class="col-4">
                                   <%-- <button type="button" class="btn btn-danger border-white">Clear Entries</button>--%>
                                    <asp:Button ID="btnclear" runat="server" Text="Clear Entries" CssClass="btn btn-danger border-white" OnClick="btnclear_Click" />
                                </div>

                                <div class="col-6">
                                    <%--<button type="button" class="btn btn-success border-white" data-bs-toggle="modal" data-bs-target="#confirm">Change Password</button>--%>
                                    <asp:Button ID="btnchangepass" runat="server" Text="Change Password" CssClass="btn btn-success border-white" data-bs-toggle="modal" data-bs-target="#confirm"  OnClick="btnchangepass_Click"/>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-5">
                        <div class="card card-body bg-danger container text-white">
                            <br/>

                            <p>Things to keep in mind!</p>

                            <ul>
                                <li>Make it long and complex.</li>
                                <li>Don't re-use passwords.</li>
                                <li>Don't use easily guessed passwords.</li>
                                <li>Change your password regularly.</li>
                                <li>Use password manager.</li>
                            </ul>

                            <br/>
                        </div>

                        <br/>
                        <p>Need help? Contact Us.</p>
                        <p>Email us at <b>novahigh@deped.gov.ph</b><br/>Call us at <b>+63 2 417 0378</b></p>
                    </div>
                </div>

            </div>
        </div>
    </div>
</div>
    </form>
</body>
</html>
