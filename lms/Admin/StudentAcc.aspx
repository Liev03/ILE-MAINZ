<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminLayout.Master" AutoEventWireup="true" CodeBehind="StudentAcc.aspx.cs" Inherits="lms.Admin.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
<script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="Management">
        <div class="search-nav">
             <div class="search-bar">    
                    <asp:TextBox ID="txtsearch" runat="server" CssClass="search" placeholder="Search Student" AutoPostBack="True" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                       <i class="fas fa-search"></i>
                  </div>
              <div class="search-buttons">
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="d-list" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">


            </asp:DropDownList>

            <a href="AddStudent.aspx" class="crud">Add Account </a>
             </div>
        </div>
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
          <div class="search-tbl">
        <asp:GridView ID="studentGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="No Student Found">
            <Columns>
                <asp:BoundField DataField="studentid" HeaderText="Student ID" />
                <asp:BoundField DataField="Fullname" HeaderText="Name" />
                <asp:BoundField DataField="email" HeaderText="Email" />
                <asp:TemplateField HeaderText="" ItemStyle-Width="140px">
                    <ItemTemplate>
                        <asp:HyperLink ID="studentLink" runat="server"
                            NavigateUrl='<%# "editStudent.aspx?studentid=" + Eval("studentid") %>'
                            Text="View Details" CssClass="view" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
              </div>
    </div>
   

</asp:Content>
