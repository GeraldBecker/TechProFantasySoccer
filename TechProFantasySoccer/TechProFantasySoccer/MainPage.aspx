<%@ Page Title="Team Home " Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MainPage.aspx.cs" Inherits="TechProFantasySoccer.MainPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <table id="table1">
        <tr>
            <td><strong>LEAGUE:</strong></td>
            <td>
                <asp:TextBox ID="leagueTextbox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><strong>TEAM:</strong></td>
            <td>
                <asp:TextBox ID="teamTextbox" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>

    <table id="table2">
        <tr>
            <td><asp:Button id="playerSearchBtn" runat="server" Height="36px" Width="148px" text="Player Search" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="teamOverviewBtn" runat="server" Height="36px" Width="148px" text="Team Overview" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="setLineupBtn" runat="server" Height="36px" Width="148px" text="Set Team Lineup" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="leagueChatBtn" runat="server" Height="36px" Width="148px" text="League Chat" BackColor="#ffcc00" /></td>
        </tr>
    </table>
    
</asp:Content>

