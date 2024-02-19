<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminLayout.Master" AutoEventWireup="true" CodeBehind="AddStudent.aspx.cs" Inherits="lms.Admin.AddStudent" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
        <link rel="stylesheet" href="../CSS/AdminCSS/addAccount.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
       <div class="add-account">
     <div class="acc-card">
          <div class="acc-head">
               <a href="StudentAcc.aspx"><i class="fas fa-arrow-left arrow-left"></i></a>
              <h2>Add Student</h2>
           </div>
         <div class="acc-body">
             <div class="acc-info1">
                 <div class="lbl-txt">
                        <div class="lbl">
                           <asp:Label ID="Label1" runat="server" Text="First Name:"  CssClass="acc-lbl fname"></asp:Label>
                         </div>
                     <div class="txt">
                 <asp:TextBox ID="txtfirstname" runat="server" CssClass="acc-txt" placeholder="Firstname" onkeyup="generateUsername()"></asp:TextBox>
                         </div>
                  </div>
                 <div class="lbl-txt">
                     <div class="lbl">
                      <asp:Label ID="Label2" runat="server" Text="Last Name"  CssClass="acc-lbl lname"></asp:Label>
                         </div>
                       <div class="txt">
                           <asp:TextBox ID="txtlastname" runat="server" CssClass="acc-txt" placeholder="Lastname" onkeyup="generateUsername()"></asp:TextBox>
                                </div>
                  </div>
                 <div class="lbl-txt">
                        <div class="lbl">
                       <asp:Label ID="Label3" runat="server" Text="BirthDay:"  CssClass="acc-lbl bday"></asp:Label>
                                </div>
                      <div class="txt">
                       <asp:TextBox ID="TextBox3" runat="server" CssClass="acc-txt" placeholder="Enter birthDay" AutoPostBack="True" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="TextBox3_CalendarExtender" runat="server" BehaviorID="TextBox3_CalendarExtender" TargetControlID="TextBox3">
                          </ajaxToolkit:CalendarExtender>
                             </div>
                  </div>
                 <div class="lbl-txt">
                     <div class="lbl">
                          <asp:Label ID="Label4" runat="server" Text="Age :"  CssClass="acc-lbl age"></asp:Label>
                             <asp:ScriptManager ID="ScriptManager1" runat="server">
                          </asp:ScriptManager>
                             </div>
                      <div class="txt">
                       <asp:TextBox ID="TextBox4" runat="server" CssClass="acc-txt"  placeholder="Age"></asp:TextBox>
                           </div>
                  </div>
                 <div class="lbl-txt">
                       <div class="lbl">
                         <asp:Label ID="Label5" runat="server" Text="Gender:" CssClass="acc-lbl gender"></asp:Label>
                           </div>
                      <div class="txt rdb">
                          <asp:RadioButton ID="RadioButton1" runat="server"  Text="Male" CssClass="radio-btn" GroupName="GenderGroup"/>
                           <asp:RadioButton ID="RadioButton2" runat="server"  Text="Female" CssClass="radio-btn" GroupName="GenderGroup"/>
                           </div>
                     </div>
                     <div class="lbl-txt">
                       <div class="lbl">
                           <asp:Label ID="Label6" runat="server" Text="Contact No."  CssClass="acc-lbl age"></asp:Label>
                           </div>
                          <div class="txt">
                              <asp:TextBox ID="TextBox5" runat="server" CssClass="acc-txt"  placeholder="Enter Contact Number"></asp:TextBox>
                      </div>
                  </div>
             </div>
             <div class="acc-info2">
                   <div class="acc-img">
                        <asp:Image ID="ImagePreview" runat="server" CssClass="img-preview" EnableViewState="false" Visible="true" />
                       <asp:FileUpload ID="FileUpload1" runat="server"  onchange="showImagePreview()" CssClass="upload"  />
                   </div>  


                        <div class="lbl hide">
                            <asp:Label ID="Label9" runat="server" Text="Username" CssClass="acc-lbl age"></asp:Label>
                        </div>
                        <div class="txt hide" >
                            <asp:TextBox ID="txtusername" runat="server" CssClass="acc-txt" placeholder="Username"></asp:TextBox>
                        </div>

                <div class="lbl-txt">
                        <div class="lbl">
                           <asp:Label ID="Label7" runat="server" Text="Email Account"  CssClass="acc-lbl age"></asp:Label>
                      </div>
                   <div class="txt">
                       <asp:TextBox ID="TextBox6" runat="server" CssClass="acc-txt"  placeholder="Enter Email Account"></asp:TextBox>
                 </div>
                 </div>
                   <div class="lbl-txt">
                      <div class="lbl">
                           <asp:Label ID="Label8" runat="server" Text="Password"  CssClass="acc-lbl age"></asp:Label>
                        </div>
                   <div class="txt">
                     <asp:TextBox ID="TextBox7" runat="server" CssClass="acc-txt"  placeholder="Temporary Password"></asp:TextBox>
                  </div>
                    
               </div>           
                    <div class="lbl-txt button-add">
                         <asp:Button ID="btnadd" runat="server" Text="Add" OnClientClick="return validateGender();" OnClick="btnadd_Click"  CssClass="btn-add"/>
                           <asp:Button ID="Button1" runat="server" Text="Clear"  CssClass="btn-add" OnClick="Button1_Click" />
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
     <script type="text/javascript">
         function generateUsername() {
             var firstname = document.getElementById('<%= txtfirstname.ClientID %>').value;
            var lastname = document.getElementById('<%= txtlastname.ClientID %>').value;
        var username = (firstname + "" + lastname).toLowerCase(); 
        document.getElementById('<%= txtusername.ClientID %>').value = username;
         }
     </script>
      <script>
          function showSuccessMessage() {
              Swal.fire({
                  icon: 'success',
                  text: 'The student has been added successfully, and the account details have been sent to the email   ',
                  showCancelButton: true,
                  cancelButtonText: 'Continue to Add Student',
                  confirmButtonColor: '#3085d6',
                  cancelButtonColor: '#d33',
              }).then((result) => {
                  if (result.isConfirmed) {
                      window.location.href = 'StudentAcc.aspx';
                  } else {
                      window.location.href = 'AddStudent.aspx';
                  }
              });
          }
      </script>

</asp:Content>
