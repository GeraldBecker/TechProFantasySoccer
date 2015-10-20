<%@ Page Title="Team Home "Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MainPage.aspx.cs" Inherits="TechProFantasySoccer.MainPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <style type="text/css">
            .auto-style2 {
                width: 100%;
            }
            .auto-style3 {
                width: 486px;
            }
            .auto-style4 {
                width: 1003px;
            }
    </style>

    <table id="tbale1" class="auto-style2">
        <tr>
            <td class="auto-style3">LEAGUE:</td>
            <td class="auto-style3">
                <asp:TextBox ID="leagueTextbox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">TEAM:</td>
            <td class="auto-style3">
                <asp:TextBox ID="teamTextbox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><button id="playerSearchBtn">Player Search</button></td>
        </tr>
        <tr>
            <td><button id="teamOverviewBtn">Team Overview</button></td>
        </tr>
        <tr>
            <td><button id="setLineupBtn">Set Team Lineup</button></td>
        </tr>
        <tr>
            <td><button id="leagueChatBtn">League Chat</button></td>
        </tr>
    </table>
    
</asp:Content>

