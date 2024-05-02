<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="studentClassroom.aspx.cs" Inherits="lms.Student.studentClassroom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Learning Management System</title>
    <link rel="icon" href="../Resources/Novaliches Senior High School.png" type="image/x-icon" />
    <link rel="stylesheet" href="../CSS/ProfessorCSS/instructorClassroom.css" />
    <link rel="stylesheet" href="//use.fontawesome.com/releases/v5.0.7/css/all.css" />
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet' />
    <link rel="stylesheet" href="../CSS/ProfessorCSS/pedingInvite.css" />
    <link href="../CSS/ProfessorCSS/announcement.css" rel="stylesheet" />


</head>
<body>
    <form id="form1" runat="server">
        <header>
            <div class="header-content">
                <div class="icon-img-h2">
                    <%--       <i class="fa fa-bars" aria-hidden="true" id="btn"></i>--%>
                    <img src="../Resources/Novaliches Senior High School.png" alt="LOGO" />
                    <h2>Novaliches High School</h2>
                </div>
                <i class="fas fa-angle-right dropdown"></i>
                <div class="sched-subj">
                    <asp:Label ID="lblsubjectname" runat="server" Text="" CssClass="sub"></asp:Label>
                    <asp:Label ID="lblschedule" runat="server" Text="" CssClass="sched-room"></asp:Label>
                </div>
            </div>
            <div class="header-login">
                <div class="dropdown">
                    <button class="dropdown-toggle"><i class='bx bx-user-circle icon'></i><i class='bx bxs-down-arrow arrow'></i></button>
                    <div class="dropdown-menu">
                        <a href="/Account/Logout.aspx"><i class="fas fa-sign-out-alt"></i>Logout</a>

                    </div>
                <div id="noti_Container">
    <div id="noti_Counter">7</div>
    <button id="noti_Button"><i class="fas fa-bell"></i></button>
    <div id="notifications">
      <h3>Notifications</h3>
      <ul>
        <!-- Example notification items -->
        <li>Notification 1</li>
        <li>Notification 2</li>
        <li>Notification 3</li>
      </ul>
      <div class="seeAll"><a href="#">See All</a></div>
    </div>
  </div>
            </div>
                </div>
        </header>
        <div class="container">
            <div class="side-bar">
                <div class="menu">

                    <div class="item">

                        <a href="instructorClassroom.aspx" class="sub-btn-active"><i class='bx bxs-dashboard'></i>Home</a>
                    </div>

                    <div class="item">

                        <a href="classSubjects.aspx" class="sub-btn-active"><i class="fas fa-sign-out-alt"></i>Exit</a>
                    </div>
                </div>
            </div>
            <main class="content">

                <div class="invite-room-list">
                    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
                        StaticSelectedStyle-CssClass="selected-tab" StaticMenuItemStyle-HorizontalPadding="50px" StaticMenuItemStyle-VerticalPadding="15px"
                        StaticSelectedStyle-BackColor="#eb4d4d" CssClass="tabs" OnMenuItemClick="Menu1_MenuItemClick1">
                        <Items>
                            <asp:MenuItem Text="Stream" Value="0" Selected="true"></asp:MenuItem>
                            <asp:MenuItem Text="Classwork" Value="1"></asp:MenuItem>
                            <asp:MenuItem Text="People" Value="2"></asp:MenuItem>
                            <asp:MenuItem Text="Grades" Value="3"></asp:MenuItem>
                        </Items>
                    </asp:Menu>
                    <div class="tabContents">
                        <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">

                            <asp:View ID="View1" runat="server">
                                <div class="stream">
                                    <div class="create-post">
                                        <div class="post-head">
                                            <h2>Create Post</h2>
                                        </div>
                                        <div class="post-details">
                                            <div id="postDetailsLink" class="post-details-link">
                                                <div class="post-div i">
                                                    <i class="fas fa-user"></i>
                                                </div>
                                                <div class="post-div txt">
                                                    <a href="#" class="post-txt">Announce something in the class</a>
                                                </div>
                                            </div>

                                            <div id="postDetailsInput" class="post-details-input">
                                                <div class="detail-link">
                                                    <div class="post-div txt">
                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="detail-txt" placeholder="Announce something in the class" TextMode="MultiLine" Rows="7"></asp:TextBox>
                                                    </div>
                                                    <div class="post-buttons">
                                                        <asp:Button ID="Button2" runat="server" CssClass="post-btn" Text="Create Post" />
                                                        <asp:Button ID="Button1" runat="server" CssClass="post-btn cancel" Text="Cancel" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <asp:GridView ID="postGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="No Announcements Found" CssClass="announcement-grid">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="announcement-box">
                                                            <div class="announcement-title">Announcement:</div>
                                                            <div class="announcement-teacher"><%# Eval("teacheremail") %></div>
                                                            <div class="announcement-date"><%# Eval("date") %></div>
                                                            <div class="announcement-content"><%# Eval("postcontent") %></div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:View>

                            <asp:View ID="View2" runat="server">
                                <div class="classwork">
                                    <div class="class-head">
                                        <div class="drop-list1">
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="d-list">
                                                <asp:ListItem Text="All Topics" Value="1" />
                                                <asp:ListItem Text="Assignments" Value="2" />
                                                <asp:ListItem Text="Quiz" Value="3" />
                                                <asp:ListItem Text="Materials" Value="4" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="drop-list2">
                                        </div>
                                    </div>
                                    <div class="class-body">
                                        <%-- <iframe src="classAssignment.aspx"></iframe>--%>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View ID="View3" runat="server">
                                <div class="instructors">
                                    <div class="people-view">
                                        <h2>ROOM INSTRUCTORS </h2>

                                        <div class="instruc-creator">

                                            <div class="icon">
                                                <i class="fas fa-user"></i>
                                            </div>
                                            <div class="lbl-name">
                                                <asp:Label ID="lblinstructormain" runat="server" Text="Label" CssClass="instructor-name"></asp:Label>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="student-list">
                                        <h2>Student Lists</h2>
                                        <div class="student-table">
                                            <asp:GridView ID="studentlist" runat="server" AutoGenerateColumns="false" EmptyDataText="No Student Found">
                                                <Columns>
                                                    <asp:BoundField DataField="StudentEmail" HeaderText="Email" HeaderStyle-CssClass="subj" />

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View ID="View4" runat="server">
                                GRADES
                            </asp:View>
                        </asp:MultiView>

                    </div>

                </div>


            </main>
        </div>

    </form>

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
        //const btn = document.querySelector("#btn");
        //const sidebar = document.querySelector(".side-bar");

        //btn.onclick = function () {
        //    sidebar.classList.toggle("active");
        //}
        document.addEventListener('DOMContentLoaded', function () {

            document.getElementById('postDetailsInput').style.display = 'none';


            var link = document.querySelector('#postDetailsLink .post-txt');
            var link2 = document.querySelector('#postDetailsInput .cancel');
            var createPost = document.querySelector('.post-details');

            link.addEventListener('click', function (event) {
                event.preventDefault();


                document.getElementById('postDetailsLink').style.display = 'none';
                document.getElementById('postDetailsInput').style.display = 'block';

                createPost.style.height = '230px';
            });
            link2.addEventListener('click', function (event) {
                event.preventDefault();


                document.getElementById('postDetailsLink').style.display = 'flex';
                document.getElementById('postDetailsInput').style.display = 'none';
                createPost.style.height = '60px';
            });
        });

    </script>
    <script>

        document.addEventListener('DOMContentLoaded', function () {
            var link = document.querySelector('#postDetailsLink .post-txt');
            var link2 = document.querySelector('#postDetailsInput .cancel');
            var linkContainer = document.getElementById('postDetailsLink');
            var inputContainer = document.getElementById('postDetailsInput');


            linkContainer.style.opacity = '1';
            inputContainer.style.opacity = '0';

            link.addEventListener('click', function (event) {
                event.preventDefault();


                linkContainer.style.opacity = '0';


                setTimeout(function () {
                    inputContainer.style.opacity = '1';
                }, 300);
            });

            link2.addEventListener('click', function (event) {
                event.preventDefault();


                inputContainer.style.opacity = '0';


                setTimeout(function () {
                    linkContainer.style.opacity = '1';
                }, 300);
            });
        });
    </script>
    <script>
        document.getElementById('createRoomLink').addEventListener('click', function (e) {
            e.preventDefault();

            var assignmentContainer = document.getElementById('assignmentContainer');
            var backgroundBlur = document.getElementById('bg-blur');

            if (assignmentContainer.style.display === 'none' || assignmentContainer.style.display === '') {

                backgroundBlur.style.display = 'block';
                assignmentContainer.style.display = 'block';
            } else {

                backgroundBlur.style.display = 'none';
                assignmentContainer.style.display = 'none';
            }
        });
    </script>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
  <script>
      $(document).ready(function () {
          $('#noti_Button').click(function () {
              $('#notifications').fadeToggle('fast', 'linear');
              $('#noti_Counter').fadeOut('slow');
              return false;
          });

          $(document).click(function () {
              $('#notifications').hide();
          });

          $('#notifications').click(function (e) {
              e.stopPropagation();
          });
      });
  </script>
</body>
</html>
