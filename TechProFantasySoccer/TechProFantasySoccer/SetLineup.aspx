<%@ Page Title="Set Lineup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetLineup.aspx.cs" Inherits="TechProFantasySoccer.SetLineup" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <table class="nav-justified">
        <tr>
            <td rowspan="5" class="lineupColumn1">
                <h4>Stats</h4>
                <asp:ListBox ID="ListBox1" runat="server" Height="547px" Width="343px"></asp:ListBox>
            </td>
            <td class="lineupColumn1">
                <h4>Defenders</h4>
                <asp:ListBox ID="tbDefenders" runat="server" Width="343px" Height="150px"></asp:ListBox>
            </td>
            <td>
                <h4>Bench</h4>
                <asp:ListBox ID="tbBench" runat="server" Width="343px" Height="150px"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td>
                <h4>Midfielders</h4>
                <asp:ListBox ID="tbMidfielders" runat="server" Width="343px" Height="150px"></asp:ListBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
            <h4>Atackers</h4>
            <asp:ListBox ID="tbAttackers" runat="server" Width="343px" Height="75px"></asp:ListBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
            <h4>Goalie</h4>
            <asp:ListBox ID="lbGoalie" runat="server" Width="343px" Height="32px"></asp:ListBox></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    </asp:Content>
