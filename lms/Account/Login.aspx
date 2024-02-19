<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="lms.LOGIN.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Learning Management System</title>
    <link rel="icon" href="../Resources/Novaliches Senior High School.png" type="image/x-icon" />
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
            <h2>Novaliches Senior High School</h2>
        </div>
        <div class="login-page">
            <form method="post" runat="server">
                <div class="login-logo">
                    <img src="../Resources/Novaliches Senior High School.png" />
                    <h2>Learning Management System</h2>
                </div>
                <div class="container">
                    <label for="uname"><b>Email</b></label>
                    <asp:TextBox ID="txtemail" placeholder="Enter Email" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="emailValidator" runat="server" ErrorMessage=" * Email is Required" ControlToValidate="txtemail" CssClass="required"></asp:RequiredFieldValidator>
                    <br />
                    <label for="psw"><b>Password</b></label>
                    <asp:TextBox ID="txtpassword" placeholder="Enter Password" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="passwordValidator" runat="server" ErrorMessage="* Password is Required" ControlToValidate="txtpassword" CssClass="required"></asp:RequiredFieldValidator>
                    <br />  
                    <div class="show-pass">
                        <span>
                            <input type="checkbox" onclick="myFunction()" />
                            Show Password</span>
                        <a href="Forgot_Password.aspx">Forgot Password?</a>
                    </div>
                    <asp:Button ID="btnlogin" CssClass="btn" runat="server" Text="LOGIN" OnClick="btnlogin_Click" />
                </div>
            </form>
        </div>
    </div>
    <script>
        function myFunction() {
            var x = document.getElementById("txtpassword");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }
    </script>
</body>
</html>
