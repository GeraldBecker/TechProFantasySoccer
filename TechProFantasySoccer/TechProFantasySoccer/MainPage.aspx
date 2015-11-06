<%@ Page Title="Team Home " Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MainPage.aspx.cs" Inherits="TechProFantasySoccer.MainPage" %>

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
            width: 32px;
        }
    </style>

    <table id="table1" class="auto-style2">
        <tr>
            <td class="auto-style4"><strong>LEAGUE:</strong></td>
            <td class="auto-style3">
                <asp:TextBox ID="leagueTextbox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style4"><strong>TEAM:</strong></td>
            <td class="auto-style3">
                <asp:TextBox ID="teamTextbox" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>

    <table id="table2" class="auto-style2">
        <tr>
            <td class="auto-style4"><asp:Button id="playerSearchBtn" runat="server" Height="36px" Width="148px" text="Player Search" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td class="auto-style4"><asp:Button id="teamOverviewBtn" runat="server" Height="36px" Width="148px" text="Team Overview" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td class="auto-style4"><asp:Button id="setLineupBtn" runat="server" Height="36px" Width="148px" text="Set Team Lineup" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td class="auto-style4"><asp:Button id="leagueChatBtn" runat="server" Height="36px" Width="148px" text="League Chat" BackColor="#ffcc00" /></td>
        </tr>
    </table>
    
</asp:Content>

