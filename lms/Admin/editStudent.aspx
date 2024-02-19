<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminLayout.Master" AutoEventWireup="true" CodeBehind="editStudent.aspx.cs" Inherits="lms.Admin.WebForm12" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/AdminCSS/editProf.css" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="add-account">
        <div class="acc-card">
            <div class="acc-head">
                <a href="StudentAcc.aspx"><i class="fas fa-arrow-left arrow-left"></i></a>
                <h2>Professor Details</h2>
            </div>
            <div class="acc-body">
                <div class="acc-info1">
                    <div class="lbl-txt">
                        <div class="lbl">
                            <asp:Label ID="Label1" runat="server" Text="First Name:" CssClass="acc-lbl fname"></asp:Label>
                        </div>
                        <div class="txt">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="acc-txt" placeholder="Enter First Name"></asp:TextBox>
                        </div>
                    </div>
                    <div class="lbl-txt">
                        <div class="lbl">
                            <asp:Label ID="Label2" runat="server" Text="Last Name" CssClass="acc-lbl lname"></asp:Label>
                        </div>
                        <div class="txt">
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="acc-txt" placeholder="Enter Last Name"></asp:TextBox>
                        </div>
                    </div>
                    <div class="lbl-txt">
                        <div class="lbl">
                            <asp:Label ID="Label3" runat="server" Text="BirthDay:" CssClass="acc-lbl bday"></asp:Label>
                        </div>
                        <div class="txt">
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="acc-txt" placeholder="Enter birthDay" AutoPostBack="True" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="TextBox3_CalendarExtender" runat="server" BehaviorID="TextBox3_CalendarExtender" TargetControlID="TextBox3"></ajaxToolkit:CalendarExtender>
                        </div>
                    </div>
                    <div class="lbl-txt">
                        <div class="lbl">
                            <asp:Label ID="Label4" runat="server" Text="Age :" CssClass="acc-lbl age"></asp:Label>
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                        </div>
                        <div class="txt">
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="acc-txt" placeholder="Age"></asp:TextBox>
                        </div>
                    </div>
                    <div class="lbl-txt">
                        <div class="lbl">
                            <asp:Label ID="Label5" runat="server" Text="Gender:" CssClass="acc-lbl gender"></asp:Label>
                        </div>
                        <div class="txt rdb">
                            <asp:RadioButton ID="RadioButton1" runat="server" Text="Male" CssClass="radio-btn" GroupName="GenderGroup" />
                            <asp:RadioButton ID="RadioButton2" runat="server" Text="Female" CssClass="radio-btn" GroupName="GenderGroup" />
                        </div>
                    </div>
                    <div class="lbl-txt">
                        <div class="lbl">
                            <asp:Label ID="Label6" runat="server" Text="Contact No." CssClass="acc-lbl age"></asp:Label>
                        </div>
                        <div class="txt">
                            <asp:TextBox ID="TextBox5" runat="server" CssClass="acc-txt" placeholder="Enter Contact Number"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="acc-info2">
                    <div class="acc-img">
                        <asp:Image ID="ImagePreview" runat="server" CssClass="img-preview" EnableViewState="true" Visible="true" />
                        <asp:FileUpload ID="FileUpload1" runat="server" text="" onchange="showImagePreview()" CssClass="upload" />
                    </div>

                 
                        <div class="lbl hide">
                            <asp:Label ID="Label9" runat="server" Text="Username" CssClass="acc-lbl age"></asp:Label>

                        </div>
                        <div class="txt hide">
                            <asp:TextBox ID="txtusername" runat="server" CssClass="acc-txt" placeholder="Username"></asp:TextBox>
                        </div>
                 
                    <div class="lbl-txt">
                        <div class="lbl">
                            <asp:Label ID="Label7" runat="server" Text="Email Account" CssClass="acc-lbl age"></asp:Label>
                        </div>
                        <div class="txt">
                            <asp:TextBox ID="TextBox6" runat="server" CssClass="acc-txt" placeholder="Enter Email Account"></asp:TextBox>


                        </div>
                    </div>


                    <div class="lbl-txt">
                        <div class="lbl">
                            <asp:Label ID="Label10" runat="server" Text="Status:" CssClass="acc-lbl gender"></asp:Label>
                        </div>
                        <div class="txt rdb">
                            <asp:RadioButton ID="RadioButton3" runat="server" Text="Activated" CssClass="radio-btn" GroupName="StatusGroup" />
                            <asp:RadioButton ID="RadioButton4" runat="server" Text="Deactivated" CssClass="radio-btn" GroupName="StatusGroup" />
                        </div>
                    </div>


                    
                    <div class="lbl-txt button-add">
                           <div class="box">
                                   <asp:CheckBox ID="CheckBox1" runat="server" CssClass="check-bx" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged1"  />
                                   <span>Enable Editing?</span>
                             </div>
                             <div class="box-btn">
                                       <asp:Button ID="btnedit" runat="server" Text="Edit Details" OnClientClick="return validateGender();" CssClass="btn-add" OnClick="btnedit_Click"  />
                                     </div>
                      
                    </div>
                </div>
            </div>

        </div>


    </div>
    <script type="text/javascript">
        function showImagePreview() {
            var imgPreview = document.getElementById('<%= ImagePreview.ClientID %>');
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
</asp:Content>
