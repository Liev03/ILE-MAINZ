<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reset_Password.aspx.cs" Inherits="lms.Account.Reset_Password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login</title>
    <link rel="stylesheet" href="../CSS/login.css" />
    <link rel="stylesheet" href="//use.fontawesome.com/releases/v5.0.7/css/all.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js" charset="utf-8"></script>
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</head>
<body>

    <div class="login">
        <div class="login-img">
            <img src="../Resources/Novaliches High School Cover Picture.jpg" />

            <h2>Novaliches High School</h2>
        </div>
        <div class="login-page">

            <form method="post" runat="server">
                <div class="login-logo">
                    <img src="../Resources/Novaliches Senior High School.png" />
                    <h2>Learning Management System</h2>
                </div>
                <div class="container">

                    <label for="uname"><b>Enter New Password:</</b></label>
                    <asp:TextBox ID="txtnewpassword" placeholder="Enter New Password" runat="server" TextMode="Password"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="newpasswordValidator" runat="server" ErrorMessage="New Password is Required" ControlToValidate="txtnewpassword"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <label for="uname"><b>Confirm Password:</</b></label>
                    <asp:TextBox ID="txtconfirmpassword" placeholder="Confirm Password" runat="server" TextMode="Password"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="conmirmpasswordValidator" runat="server" ErrorMessage="Confirm Password is Required" ControlToValidate="txtconfirmpassword"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    <asp:Button ID="btnreset" CssClass="btn" runat="server" Text="Reset Password" OnClick="btnSent_Click" />
                   <p><a href="Login.aspx">Already Have an Account?</a></p>


                </div>

            </form>

        </div>

    </div>






</body>
</html>
