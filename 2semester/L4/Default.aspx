<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" style="height: 26px" Text="Button" />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="180px" OnTextChanged="TextBox1_TextChanged" TextMode="MultiLine" Width="661px"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" Height="160px" TextMode="MultiLine" Width="655px"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="TextBox3" runat="server" Height="147px" TextMode="MultiLine" Width="662px"></asp:TextBox>
    </form>
</body>
</html>
