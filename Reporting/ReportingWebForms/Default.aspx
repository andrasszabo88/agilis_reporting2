<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ReportingWebForms.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>
            <asp:Label Text="Customer name: " runat="server" />
            <asp:TextBox ID="txtCustomerName" runat="server" />
            <asp:Button ID="btnCustomerInput" runat="server" Text="Show my campaigns!" OnClick="btnCustomerInput_Click" />
        </p>
        <p runat="server" visible="false" id="campaignParagraph">
            <asp:DropDownList ID="ddlCampaigns" runat="server" />
            <asp:Button ID="btnQueryResponse" runat="server" Text="Show statistics!" OnClick="btnQueryResponse_Click" />
            <asp:Button ID="btnQueryAdvanced" runat="server" Text="Show advanced statistics!" OnClick="btnQueryAdvanced_Click" />
        </p>
    </div>
    <div id="results" runat="server">
        <asp:Label ID="lblEmailClickRatio" runat="server" Text="Opened/all email clicks ratio: " visible="false" />
        <asp:Label ID="lblRatio" runat="server" Visible="false" />
        <asp:Table ID="tblOpenedEmailsByDevice" runat="server" Visible="false" />
    </div>
        
    </form>
</body>
</html>
