<%@ Page Language="C#" AutoEventWireup="true" CodeFile="L2.aspx.cs" Inherits="L2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            height: 816px;
        }
    </style>
</head>
<body style="background-color:#ADDFFF;">
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="START" BackColor="Blue" BorderColor="White" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        &nbsp;
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        &nbsp;
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
        <br />
            <asp:TextBox ID="TextBox3" runat="server" Height="231px" Width="556px" BackColor="#FFCCFF" TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="START" BackColor="Blue" />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
        &nbsp;<p>
            <asp:TextBox ID="TextBox1" runat="server" Height="30px" Width="187px" BorderColor="#FFCCFF" TextMode="MultiLine"></asp:TextBox>
        </p>
        <p>
            <asp:TextBox ID="TextBox2" runat="server" Height="144px" Width="563px" BackColor="#FFCCFF" TextMode="MultiLine"></asp:TextBox>
        </p>
    </form>
</body>
</html>
