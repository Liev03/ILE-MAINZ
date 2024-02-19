<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentRegistration.aspx.cs" Inherits="lms.sis.SISPAGES.StudentRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Registration</title>
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
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous"/>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
    <script src="https://rawgit.com/eKoopmans/html2pdf/master/dist/html2pdf.bundle.js"></script>

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
                        <p class="inter-700 text-uppercase">Navigation > Student Registration</p>
                    </div>
                   <div class="col-lg-6" align="right">
                        <div class="dropdown">
                            <a class="border-0 border-none bg-white text-dark dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-bs-toggle="dropdown" aria-expanded="false">
                                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="28" height="28">
                                    <path stroke-linecap="round" stroke-linejoin="round" d="M17.982 18.725A7.488 7.488 0 0012 15.75a7.488 7.488 0 00-5.982 2.975m11.963 0a9 9 0 10-11.963 0m11.963 0A8.966 8.966 0 0112 21a8.966 8.966 0 01-5.982-2.275M15 9.75a3 3 0 11-6 0 3 3 0 016 0z" />
                                </svg>
                            </a>
                            <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                <li class="p-1 text-uppercase"><a id="A1" runat="server" class="dropdown-item bg-danger text-white" onserverclick="Logout_Click">Log-out</a></li>
                            </ul>
                        </div>
                   </div>
                    <br />
                                 <div class="card card-body border-danger bg-danger text-white text-uppercase josefin-sans-700">
                     <asp:Label runat="server" ID="lbl1"></asp:Label>
                    <%--Attention! : Today's class is suspended due to typhoon.--%>
                </div>
                        <br />
                        <br />
                    <form runat="server">
                        
                        <div class="d-block mt-4">
                            <div class="card card-body border-secondary text-uppercase">
                                <div class="row">
                                    <div class="col-lg-4">
                                        School Year<br />
                                        <asp:DropDownList runat="server" ID="school_year" AutoPostBack="true" CssClass="form-control">
                                            <asp:ListItem id="current" Text="current year" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        Semester<br />
                                        <asp:DropDownList runat="server" ID="sem" AutoPostBack="true" CssClass="form-control">
                                            <asp:ListItem id="empty" Text="" Selected />
                                            <asp:ListItem id="currentSem" Text="1st sem" />
                                            <asp:ListItem id="prevSem" Text="2nd sem" />
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-4">
                                        <br />
                                        <asp:Button ID="display" runat="server" class="btn btn-danger" Text="Display" OnClick="display_Click" />
                                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal" id="regform">Registration Form</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
                <br />
                <div class="card card-body bg-gray border border-gray container d-block mt-4" style="background-color: var(--bs-gray-300)">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-6">
                                    <b>Name</b>
                                </div>

                                <div class="col-6">
                                    <p runat="server" id="StudName"></p>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-6">
                                    <b>Program</b>
                                </div>

                                <div class="col-6">
                                    <p runat="server" id="Strand"></p>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-6">
                                    <b>Status</b>
                                </div>

                                <div class="col-6">
                                    <p runat="server" id="AdmisionStatus"></p>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-6">
                                    <b>School Year</b>
                                </div>

                                <div class="col-6">
                                    <p runat="server" id="schoolYear"></p>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-6">
                                    <b>Student No.</b>
                                </div>

                                <div class="col-6">
                                    <p runat="server" id="studID"></p>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-6">
                                    <b>Year Level</b>
                                </div>

                                <div class="col-6">
                                    <p runat="server" id="year"></p>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-6">
                                    <b>Section</b>
                                </div>

                                <div class="col-6">
                                    <p runat="server" id="section"></p>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-6">
                                    <b>Semester</b>
                                </div>

                                <div class="col-6">
                                    <p runat="server" id="currentSemester"></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <br />
                <div class="d-block mt-4">
                    <h3>Enlisted Subjects</h3>
                    <br />
                    <div class="gridContainer mb-4">
                        <asp:GridView CssClass="Grid" ID="grid" runat="server" CellPadding="30" ForeColor="#333333" GridLines="None" Height="148px" Width="100%" SelectedRowStyle-BackColor="#6666FF"
                            CellSpacing="10" Font-Size="Medium" HorizontalAlign="Center">
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
        </div>

    </div>
 

<div class="modal fade" id="regFormModal" tabindex="-1" role="dialog" aria-labelledby="regFormModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
<div class="modal-body" id="regFormModalContent">
     <p id="RegFormSchoolYear" class="text-right"></p>
    <div class="d-flex align-content-center justify-content-center">
        <img src="Resources/NH-LOGO.png" width="100px"/>
        <h2 class="modal-title mt-4 mx-3 text-lg-center lh-sm" id="regFormModalLabel">
            Novaliches High School<br />
             <b style="font-size:20px;">
                Student Registration<br/>
                <span class="text-lg-center" style="font-size:15px;font-weight:500">
                    TS Cruz Subdivision, 1 Lakandula, Novaliches,<br /> Quezon City, Metro Manila
                </span>
            </b>
            <br/>
         
        </h2>
        <img src="Resources/NH-LOGO.png" width="100px" style="margin-right:30px" />
    </div>
       <hr/>
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-5">
                <div class="border border-dashed p-0 mb-1">
                    <p class="mb-0 opacity-75"><b>Name</b></p>
                    <p class="text-center" id="RegFormName"></p>
                </div>
            </div>
            <div class="col-md-3 mb-1">
                <div class="border border-dashed p-0">
                    <p class="mb-0 opacity-75"><b>Student No.</b></p>
                    <p class="text-center"  id="RegFormStudID"></p>
                </div>
            </div>
            <div class="col-md-4 mb-1">
                <div class="border border-dashed p-0">
                    <p class="mb-0 opacity-75"><b>Program</b></p>
                    <p class="text-center"  id="RegFormStrand"></p>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 mb-3">
                <div class="border border-dashed p-0">
                    <p class="mb-0 opacity-75"><b>Status</b></p>
                    <p class="text-center"  id="RegFormAdmissionStatus"></p>
                </div>
            </div>
            <div class="col-md-3 mb-1">
                <div class="border border-dashed p-0">
                    <p class="mb-0 opacity-75"><b>Grade</b></p>
                    <p class="text-center" id="RegFormYear"></p>
                </div>
            </div>
            <div class="col-md-3 mb-3">
                <div class="border border-dashed p-0">
                    <p class="mb-0 opacity-75"><b>Semester</b></p>
                    <p class="text-center"  id="RegFormCurrentSemester"></p>
                </div>
            </div>
            <div class="col-md-3 mb-3">
                <div class="border border-dashed p-0">
                    <p class="mb-0 opacity-75"><b>Section</b></p>
                    <p class="text-center"  id="RegFormSection"></p>
                </div>
            </div>
        </div>
    </div>
        <div class="d-block mt-4">
                    <h3>Enlisted Subjects</h3>
                    <br />
                    <div class="gridContainer mb-4">
                        <asp:GridView CssClass="form" ID="Regformview" runat="server" CellPadding="8" ForeColor="#333333" GridLines="None" Height="148px" Width="100%" SelectedRowStyle-BackColor="#6666FF"
                            CellSpacing="5" Font-Size="small" HorizontalAlign="Center">
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
    <span>@Nova SIS</span>
    </div>
           <div class="modal-footer">
                <button type="button" class="btn btn-secondary" id="close" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary" id="regFormDownloadPdfBtn">Download</button>
            </div>
        </div>
    </div>
</div>
</form>
</body>
<script>
    const regformModal = document.getElementById("regform");
    const regFormModal = document.getElementById("regFormModal");
    const close = document.getElementById("close");

    function downloadPdf() {
        const element = document.getElementById("regFormModalContent");

        html2pdf(element, {
            margin: 10,
            filename:'Registration_Form.pdf',
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
</html>
