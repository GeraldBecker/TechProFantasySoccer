<%@ Page Title="Create Team " Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AboutToJoin.aspx.cs" Inherits="TechProFantasySoccer.AboutToJoin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <table id="table1">
        <tr>
            <td><strong>You are about to join:</strong></td>
        </tr>
        <tr>
            <td><strong><asp:Label id="leagueLabel" runat="server" ForeColor="White" Text="Brazil Fans League" /></strong></td>
        </tr>
        <tr>
            <td><strong>Enter Team Name:</strong></td>
        </tr>
        <tr>
            <td><asp:TextBox id="teamName" runat="server">DeutscheWelle</asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Button id="joinLeagueBtn" runat="server" Height="36px" Width="300px" text="JOIN LEAGUE" BackColor="#66ff66" /></td>
        </tr>
    </table>
    
</asp:Content>
