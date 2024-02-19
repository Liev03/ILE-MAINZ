<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vviewfile.aspx.cs" Inherits="lms.Vviewfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Files</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvFiles_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="FileID" HeaderText="File ID" SortExpression="FileID" />
                    <asp:BoundField DataField="FileName" HeaderText="File Name" SortExpression="FileName" />
                    <asp:ButtonField ButtonType="Button" CommandName="Download" Text="Download" HeaderText="Action" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:Label ID="lblFileContent" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>