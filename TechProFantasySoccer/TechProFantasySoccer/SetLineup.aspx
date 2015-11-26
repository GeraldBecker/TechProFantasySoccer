<%@ Page Title="Set Lineup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetLineup.aspx.cs" Inherits="TechProFantasySoccer.SetLineup" EnableViewState="true"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<script>
 
</script>
    <h2><%: Title %></h2>
    <asp:Label ID="NotifyLabel" runat="server"></asp:Label>
    <table class="nav-justified">
        <tr>
            <td rowspan="5" class="lineupColumn1">
                <h4>Stats</h4>
                <asp:ListBox ID="ListBox1" runat="server" Height="547px" Width="300px"></asp:ListBox>
            </td>
            <td class="lineupColumn1">
                <h4>Defenders</h4>
                <asp:Panel ID="DefenderPanel" runat="server" Height="120px">
                </asp:Panel>
            </td>
            <td class="lineupColumn1" rowspan="3">
                <h4>Bench</h4>
                <div class="teamSpots" id="benchDiv">
                <asp:DataList ID="tbBench" runat="server" Width="300px" Height="150px">
                    <ItemTemplate>
                        <div class="player draggable="true">
                            <%--<div class="playerName"><%# Eval("First") %> <%# Eval("Last") %></div>
                            <div class="playerPosition"><%# Eval("Position") %></div>--%>
                            <%# Container.DataItem %>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                </div>
            </td>
        </tr>
        <tr>
            <td class="lineupColumn1">
                <h4>Midfielders</h4>
                <asp:Panel ID="MidfielderPanel" runat="server"></asp:Panel>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="lineupColumn1">
            <h4>Strikers</h4>
            <asp:Panel ID="StrikerPanel" runat="server"></asp:Panel>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
            <h4>Goalie</h4>
                <asp:Panel ID="GoaliePanel" runat="server"></asp:Panel>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <div id="buttonDiv">
        <asp:Button ID="CancelButton" runat="server" Text="Cancel" BackColor="#CC0000" BorderColor="White" ForeColor="White" Height="50px" Width="300px" OnClick="CancelButton_Click" />
        <asp:Button ID="SubmitButton" runat="server" Text="Submit" BackColor="#269926" BorderColor="White" ForeColor="White" Height="50px" Width="300px" OnClick="SubmitButton_Click" />
    </div>
    </asp:Content>
