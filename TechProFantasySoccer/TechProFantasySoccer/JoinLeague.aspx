<%@ Page  Title="Join A League "Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="JoinLeague.aspx.cs" Inherits="TechProFantasySoccer.JoinLeague" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <table id="table1">
        <tr>
            <td class="auto-style4"><strong>Leagues Joined:</strong></td>
        </tr>
        <tr>
            <td class="auto-style4"><asp:TextBox id="leaguesJoined1" runat="server">Example League 1</asp:TextBox></td>
        </tr>
        <tr>
            <td class="auto-style4"><asp:TextBox id="leaguesJoined2" runat="server">Example League 2</asp:TextBox></td>
        </tr>
        <tr>
            <td class="auto-style4"><strong>Leagues Available:</strong></td>
        </tr>
        <tr>
            <td class="auto-style4"><asp:TextBox id="leaguesAvail1" runat="server">Brazil Fans League</asp:TextBox></td>
        </tr>
    </table>
    
</asp:Content>
