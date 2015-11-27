﻿<%@ Page Title="Set Lineup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetLineup.aspx.cs" Inherits="TechProFantasySoccer.SetLineup" EnableViewState="true"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<script>
 
</script>
    <div class="centered">

        <div class="banner_reg">
            <h1 class="title_reg">SET LINEUP</h1>
        </div>

        <table class="nav-justified">
            <tr>
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
                            <div class="player" draggable="true">
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
    </div>

    <div id="buttonDiv">
        <asp:Button ID="CancelButton" runat="server" Text="Cancel" CssClass="btn btn-danger" Width="20%" Enabled="False" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="SubmitButton" runat="server" Text="Submit" CssClass="btn btn-success" Width="20%" Enabled="False" />
    </div>
    </asp:Content>
