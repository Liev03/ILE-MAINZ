<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="file.aspx.cs" Inherits="lms.file" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Upload and Download</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="fileUpload" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload File" OnClick="btnUpload_Click" />
            <br />
            <asp:DropDownList ID="ddlFiles" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFiles_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Button ID="btnDownload" runat="server" Text="Download File" OnClick="btnDownload_Click" />
        </div>
    </form>
</body>
</html>