<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="classAssignment.aspx.cs" Inherits="lms.Professor.WebForm7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Learning Management System</title>
    <link rel="stylesheet" href="../CSS/ProfessorCSS/classAssignment.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="link">
        <div class="assignment">
               <div class="assign-title">
                   <div class="title">             
                        <asp:Label ID="Label1" runat="server" Text="Title:" CssClass="lbl-title"></asp:Label>
                          <asp:TextBox ID="TextBox1" runat="server" CssClass="txt-title"  placeholder="Enter Title"></asp:TextBox>
                     </div>
                   <div class="instructions">
                         <asp:Label ID="Label2" runat="server" Text="Instructions:" CssClass="lbl-instruc"></asp:Label>
                         <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"  Rows="5" CssClass="txt-instruc" placeholder="Write instructions about the post"></asp:TextBox>
                   </div>
               </div> 
                <div class="assign-description">
                    <div class="assign">
                        <asp:Label ID="Label3" runat="server" Text="For: " CssClass="lbl-assign"></asp:Label>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="txt-assign txt"></asp:TextBox>
                    </div>
                     <div class="assign">
                          <asp:Label ID="Label4" runat="server" Text="Points:"  CssClass="lbl-assign"></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="txt-assign list">
                             <asp:ListItem Text="100" Value="1" />
                             <asp:ListItem Text="50" Value="2" />
                             <asp:ListItem Text="40" Value="3" />
                             <asp:ListItem Text="30" Value="4" />
                         </asp:DropDownList>
                      </div>
                     <div class="assign">
                          <asp:Label ID="Label5" runat="server" Text="Due Date"  CssClass="lbl-assign"></asp:Label>
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="txt-assign txt"></asp:TextBox>
                    </div>
                     <div class="assign">
                        <asp:Label ID="Label6" runat="server" Text="Topic"  CssClass="lbl-assign"></asp:Label>
                           <asp:TextBox ID="TextBox5" runat="server" CssClass="txt-assign txt"></asp:TextBox>
                    </div>
                    <div class="assign-btn">
                        <asp:Button ID="Button1" runat="server" Text="Create Assignment" CssClass="buttons" />
                        <asp:Button ID="Button2" runat="server" Text="Cancel"  CssClass="buttons" />
                    </div>
                </div>
        </div>
      

        </div>
    </form>
</body>
</html>
