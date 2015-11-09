<%@ Page Title="Edit Player Information" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="EditPlayerInfo.aspx.cs" Inherits="TechProFantasySoccer.EditPlayerInfo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <table id="table1">
        <tr>
            <td>
                <asp:TextBox id="playerImg" runat="server" Height="200px" Width="200px">PLACE HOLDER FOR IMAGE</asp:TextBox>
            </td>
            <td>
                <strong><asp:Label id="playerNameLabel" runat="server" Text="Phillip Lahm Defender" /></strong>
            </td>
        </tr>
        <tr>
            <td>
                <strong>Club:</strong>
            </td>
            <td>
                <asp:TextBox id="cteamTextbox" runat="server">Bayern Munich</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><strong>Cost:</strong></td>
            <td>
                <asp:TextBox id="costTextBox" runat="server">$950</asp:TextBox>
            </td>
        </tr>
    </table>

    <table id="table2">
        <tr>
            <td>
                <asp:Button id="saveBtn" runat="server" Height="36px" Width="148px" text="SAVE" BackColor="#99ff99" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button id="editScoreSatsBtn" runat="server" Height="30px" Width="600px" text="Edit Scoring Stats Instead" BackColor="#eaeaea" />
            </td>
        </tr>
    </table>
    
</asp:Content>
