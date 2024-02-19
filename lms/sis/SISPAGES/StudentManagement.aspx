<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentManagement.aspx.cs" Inherits="lms.sis.SISPAGES.StudentManagement" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Management</title>
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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.js" integrity="sha512-AA1Bzp5Q0K1KanKKmvN/4d3IRKVlv9PYgwFPvm32nPO6QS8yH1HO7LbgB1pgiOxPtfeg5zEn2ba64MUcqJx6CA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>

            <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</head>
<body>
  <div class="row m-0 p-0 bg-danger">
 <div class="col-lg-3 m-0 bg-danger text-white text-uppercase" style="height: 100vh">
     <div class="container-fluid">
         <div class="bg-danger p-2 w-100 shadow-none" align="center">
             <center>
                   <img src="../../Resources/Novaliches Senior High School.png"200" style="margin-left: -7px" />
             </center>
         </div>

         <a href="Admin.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
             <span class="d-flex">
                 <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                     <path stroke-linecap="round" stroke-linejoin="round" d="M2.25 12l8.954-8.955c.44-.439 1.152-.439 1.591 0L21.75 12M4.5 9.75v10.125c0 .621.504 1.125 1.125 1.125H9.75v-4.875c0-.621.504-1.125 1.125-1.125h2.25c.621 0 1.125.504 1.125 1.125V21h4.125c.621 0 1.125-.504 1.125-1.125V9.75M8.25 21h8.25" />
                 </svg>
                 Home
             </span>
         </a>

         <a href="StudentManagement.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
             <span class="d-flex">
                 <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                     <path stroke-linecap="round" stroke-linejoin="round" d="M4.26 10.147a60.436 60.436 0 00-.491 6.347A48.627 48.627 0 0112 20.904a48.627 48.627 0 018.232-4.41 60.46 60.46 0 00-.491-6.347m-15.482 0a50.57 50.57 0 00-2.658-.813A59.905 59.905 0 0112 3.493a59.902 59.902 0 0110.399 5.84c-.896.248-1.783.52-2.658.814m-15.482 0A50.697 50.697 0 0112 13.489a50.702 50.702 0 017.74-3.342M6.75 15a.75.75 0 100-1.5.75.75 0 000 1.5zm0 0v-3.675A55.378 55.378 0 0112 8.443m-7.007 11.55A5.981 5.981 0 006.75 15.75v-1.5" />
                 </svg>
                 Student Management
             </span>
         </a>

          <a href="ClassRegistration.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
  <span class="d-flex">
      <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="18" height="18" class="sidebar-icon">
          <path stroke-linecap="round" stroke-linejoin="round" d="M3.75 12h16.5m-16.5 3.75h16.5M3.75 19.5h16.5M5.625 4.5h12.75a1.875 1.875 0 010 3.75H5.625a1.875 1.875 0 010-3.75z" />
                 </svg>
                Class Registration
             </span>
         </a>

         <a href="CourseManagement.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
             <span class="d-flex">
                 <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                     <path stroke-linecap="round" stroke-linejoin="round" d="M17.982 18.725A7.488 7.488 0 0012 15.75a7.488 7.488 0 00-5.982 2.975m11.963 0a9 9 0 10-11.963 0m11.963 0A8.966 8.966 0 0112 21a8.966 8.966 0 01-5.982-2.275M15 9.75a3 3 0 11-6 0 3 3 0 016 0z" />
                 </svg>
                 Strand Management
             </span>
         </a>

         <a href="SectionManagement.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
             <span class="d-flex">
                 <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                     <path stroke-linecap="round" stroke-linejoin="round" d="M15 19.128a9.38 9.38 0 002.625.372 9.337 9.337 0 004.121-.952 4.125 4.125 0 00-7.533-2.493M15 19.128v-.003c0-1.113-.285-2.16-.786-3.07M15 19.128v.106A12.318 12.318 0 018.624 21c-2.331 0-4.512-.645-6.374-1.766l-.001-.109a6.375 6.375 0 0111.964-3.07M12 6.375a3.375 3.375 0 11-6.75 0 3.375 3.375 0 016.75 0zm8.25 2.25a2.625 2.625 0 11-5.25 0 2.625 2.625 0 015.25 0z" />
                 </svg>
                 Section Management
             </span>
         </a>

                 <a href="SubjectModule.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
             <span class="d-flex">
                 <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                     <path stroke-linecap="round" stroke-linejoin="round" d="M3.75 12h16.5m-16.5 3.75h16.5M3.75 19.5h16.5M5.625 4.5h12.75a1.875 1.875 0 010 3.75H5.625a1.875 1.875 0 010-3.75z" />
                </svg>
                 Subject Module
             </span>
         </a>

         <a href="AddingGrades.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
             <span class="d-flex">
                 <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                     <path stroke-linecap="round" stroke-linejoin="round" d="M12 9v6m3-3H9m12 0a9 9 0 11-18 0 9 9 0 0118 0z" />
                 </svg>
                 Adding Grades
             </span>
         </a>

         <a href="ManageUser.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
             <span class="d-flex">
                 <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.0" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                     <path stroke-linecap="round" stroke-linejoin="round" d="M17.982 18.725A7.488 7.488 0 0012 15.75a7.488 7.488 0 00-5.982 2.975m11.963 0a9 9 0 10-11.963 0m11.963 0A8.966 8.966 0 0112 21a8.966 8.966 0 01-5.982-2.275M15 9.75a3 3 0 11-6 0 3 3 0 016 0z" />
                 </svg>
                 Manage Users
             </span>
         </a>
               <a href="adminCalendar.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
                <span class="d-flex">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M19 21H5a2 2 0 01-2-2V6a2 2 0 012-2h2V3a3 3 0 116 0v1h2a2 2 0 012 2v13a2 2 0 01-2 2zM16 10a4 4 0 00-8 0"></path>
                    </svg>
                    Calendar
                </span>
            </a>
       <%--  <a href="Maintenance.aspx" class="card card-body bg-pink p-2 w-100 mt-2 text-decoration-none text-black">
             <span class="d-flex">
                 <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" width="18" height="18" class="sidebar-icon">
                     <path stroke-linecap="round" stroke-linejoin="round" d="M11.42 15.17L17.25 21A2.652 2.652 0 0021 17.25l-5.877-5.877M11.42 15.17l2.496-3.03c.317-.384.74-.626 1.208-.766M11.42 15.17l-4.655 5.653a2.548 2.548 0 11-3.586-3.586l6.837-5.63m5.108-.233c.55-.164 1.163-.188 1.743-.14a4.5 4.5 0 004.486-6.336l-3.276 3.277a3.004 3.004 0 01-2.25-2.25l3.276-3.276a4.5 4.5 0 00-6.336 4.486c.091 1.076-.071 2.264-.904 2.95l-.102.085m-1.745 1.437L5.909 7.5H4.5L2.25 3.75l1.5-1.5L7.5 4.5v1.409l4.26 4.26m-1.745 1.437l1.745-1.437m6.615 8.206L15.75 15.75M4.867 19.125h.008v.008h-.008v-.008z" />
                 </svg>
                 Maintenance
             </span>
         </a>--%>
        </div>
    </div>

      <div class="col-lg-9 bg-white">
          <br />
          <div class="container-fluid">
              <div class="row">
                  <div class="col-lg-6">
                      <p class="inter-700 text-uppercase">Navigation > Student Management</p>
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
                  <div class="row">
                <div class="col-lg-3"></div>


                <div class="col-lg-3"></div>
            </div>

            <br />
            <div class="row">
                <div class="col-lg-3">
                           <asp:Image  CssClass="card card-body border-gray m-lg-4 p-2 w-75" align="center" ID="Image1" runat="server" style="background-color: var(--bs-gray-300); height: 200px;"/>           
                </div>
                <div class="col-lg-9">
                    <form id="form1" runat="server" class="card card-body border-gray p-2 w-100 text-uppercase inter-700" style="background-color: var(--bs-gray-300)">
                        <div class="container">
                            <br />
                            <p>Name</p>
                            <div class="row">
                                <div class="col-lg-4">
                                 <%--   <input type="text" class="form-control w-100" name="first-name" placeholder="First Name" />--%>
                                    <asp:TextBox ID="fname" runat="server" CssClass="form-control w-100 capitalize-first" placeholder="First Name" oninput="replaceSpacesAndCapitalize('fname')"></asp:TextBox>
                                </div>

                                <div class="col-lg-4">
                                   <%-- <input type="text" class="form-control w-100" name="middle-name" placeholder="First Name" />--%>
                                     <asp:TextBox ID="mname" runat="server" CssClass="form-control w-100 capitalize-first" placeholder="Middle Name" oninput="replaceSpacesAndCapitalize('mname')"></asp:TextBox>
                                </div>

                                <div class="col-lg-4">
                                    <%--<input type="text" class="form-control w-100" name="surname" placeholder="Surname" />--%>
                                    <asp:TextBox ID="Surname" runat="server" CssClass="form-control w-100 capitalize-first" placeholder="Surname" oninput="replaceSpacesAndCapitalize('Surname')"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mt-2">
                                <div class="col-lg-6 d-flex">
                                    <span class="justify-self-center align-self-center" style="margin-right: 4px">Email</span>
                                   <%-- <input type="email" class="form-control w-100" name="email" placeholder="Email" />--%>
                                    <asp:TextBox ID="Email" runat="server" CssClass="form-control w-100" placeholder="Email" TextMode="Email"></asp:TextBox>
                                </div>

                                <div class="col-lg-6 d-flex">
                                    <span class="justify-self-center align-self-center" style="margin-right: 4px">Citizenship</span>
                                  <%--  <input type="text" class="form-control w-100" name="citizenship" placeholder="Citizenship" />--%>
                                      <asp:TextBox ID="Citizenship" runat="server" CssClass="form-control w-100" placeholder="Citizenship"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mt-2">
                                <div class="col-lg-6 d-flex">
                                    <span class="justify-self-center align-self-center" style="margin-right: 4px">Birthday</span>
                                  <%--  <input type="date" class="form-control w-100" name="birthday" placeholder="Birthday" />--%>
                                     <asp:TextBox ID="Birthday" runat="server" CssClass="form-control w-100" placeholder="Birthday" TextMode="Date"></asp:TextBox>
                                </div>

                                <div class="col-lg-6 d-flex">
                                    <span class="justify-self-center align-self-center" style="margin-right: 8px">Gender</span>

                                    <div class="m-2">
                                        <asp:RadioButtonList ID="genderRadioButtonList" runat="server" RepeatDirection="Horizontal" CellPadding="3" CellSpacing="10">
                                            <asp:ListItem Text="Male" Value="male"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="female"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="m-2">  
                                    </div>
                                </div>
                            </div>

                            <div class="row mt-2">
                                <div class="col-lg-6 d-flex">
                                    <span class="justify-self-center align-self-center" style="margin-right: 4px">Address</span>
                                   <%-- <input type="text" class="form-control w-100" name="address" placeholder="Address" />--%>
                                    <asp:TextBox ID="Address" runat="server" CssClass="form-control w-100" placeholder="Address"></asp:TextBox>
                                </div>

                                <div class="col-lg-6 d-flex">
                                    <span class="justify-self-center align-self-center" style="margin-right: 4px">Contact</span>
                                 <%--   <input type="text" class="form-control w-100" name="contact" placeholder="Contact" />--%>
                                    <asp:TextBox ID="Contact" MaxLength="11" runat="server" CssClass="form-control w-100" placeholder="Contact" oninput="limitTo11Digits(this);"></asp:TextBox>
                                </div>
                            </div>

                            <br /><br />
                            
                                  <p>In-case of Emergency</p>
                            <div class="row">
                                <div class="col-lg-6 d-flex">
                                    <span class="justify-self-center align-self-center" style="margin-right: 4px">Name</span>
                                    <%--<input type="text" class="form-control w-100" name="relative-name" />--%>
                                    <asp:TextBox ID="IEname" runat="server" CssClass="form-control w-100"></asp:TextBox>
                                </div>

                                <div class="col-lg-6 d-flex">
                                    <span class="justify-self-center align-self-center" style="margin-right: 4px">Relationship</span>
                                   <%-- <input type="date" class="form-control w-100" name="relationship" />--%>
                                     <asp:TextBox ID="IErelationship" runat="server" CssClass="form-control w-100"></asp:TextBox>
                                </div>
                            </div>

                                <div class="col-lg-6 d-flex mt-2">
                                    <span class="justify-self-center align-self-center" style="margin-right: 4px">Contact</span>
                                    <%--<input type="text" class="form-control w-100" name="contact" />--%>
                                    <asp:TextBox ID="IEcontact" MaxLength="11" runat="server" CssClass="form-control w-100" oninput="limitTo11Digits(this);"></asp:TextBox>
                                </div>
                            </div>
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:Label ID="Label3" runat="server" Text="GOOD MORAL"></asp:Label>
                                <asp:FileUpload ID="FileUpload1" runat="server"  />
                                <asp:Label ID="Label2" runat="server" Text="FORM 137"></asp:Label>
                                <asp:FileUpload ID="FileUpload2" runat="server"  />

                                <br />
                            </div>
                            <div align="right">
                                <%--<button type="submit" class="btn btn-success mt-2 px-4">Add</button>--%>
                                <asp:Button ID="SMadd" runat="server" Text="Add" CssClass="btn btn-success mt-2 px-4" OnClick="SMadd_Click" />
                                <%--<button type="submit" class="btn btn-success mt-2 px-4">Update</button>--%>
                                <asp:Button ID="SMupdate" runat="server" Text="Update" CssClass="btn btn-success mt-2 px-4" OnClick="SMupdate_Click" />
                                <%-- <button type="submit" class="btn btn-danger mt-2 px-4">Cancel</button>--%>
                                <asp:Button ID="SMcancel" runat="server" Text="Cancel" CssClass="btn btn-danger mt-2 px-4" OnClick="SMcancel_Click" />

                                <dialog id="dialog" class="dialog border-0 w-25 overflow-hidden p-4 rounded-0" >
                                    <asp:Button ID="close" runat="server" Text="X" CssClass="btn d-grid align-content-end " Width ="2.5em" />
                                    <div class="text-center mb-3 mt-5 w-100">Input the Student ID</div>
                                    <asp:TextBox ID="targetId" runat="server" CssClass="form-control border-0 border-bottom border-dark  text-center  mb-5" Width="100%"></asp:TextBox>
                                    <asp:Button ID="Button1" runat="server" Text="Find" OnClick="Button1_Click" CssClass="btn btn-primary mb-2 w-100" />
                                    
                                    <asp:Button ID="ActualUpdate" runat="server" Text="Update" OnClick="ActualUpdate_Click" CssClass="btn btn-info w-100" />
                                </dialog>
                    </form>
                </div>
            </div>
            <br />
            </div>
            <br/>
        </div>
    </div>
</div>
</body>
    <script type="text/javascript">
    function limitTo11Digits(input) {
     
        input.value = input.value.replace(/\D/g, '').substring(0, 11);
        }

        document.addEventListener("DOMContentLoaded", function () {
            const dialog = document.getElementById("dialog");
            const showButton = document.getElementById("SMupdate");
            const closeButton = document.getElementById("close");

            showButton.addEventListener("click", (event) => {
                event.preventDefault();
                dialog.showModal();
            });

            closeButton.addEventListener("click", () => {
                dialog.close();
            });
        });
        function replaceSpacesAndCapitalize(elementId) {
            var inputElement = document.getElementById(elementId);
            var inputValue = inputElement.value;
            var camelCaseValue = inputValue.replace(/\s(.)/g, function (match, group) {
                return group.toUpperCase();
            });
            inputElement.value = camelCaseValue;
        }
    </script>
<style>
    .dialog::backdrop {
        background: rgba(0 0 0 / 0.4);
    }

    .capitalize-first {
        text-transform: capitalize;
    }
</style>
</html>
