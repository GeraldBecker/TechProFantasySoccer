<%@ Page Title="Set Lineup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetLineup.aspx.cs" Inherits="TechProFantasySoccer.SetLineup" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <table class="nav-justified">
        <tr>
            <td rowspan="5" class="lineupColumn1">
                <h4>Stats</h4>
                <asp:ListBox ID="ListBox1" runat="server" Height="547px" Width="343px"></asp:ListBox>
            </td>
            <td>
                <h4>Defenders</h4>
                <div class="teamSpots">
                <asp:DataList ID="tbDefenders" runat="server" Width="300px" Height="150px">
                    <ItemTemplate>
                        <div class="player" draggable="true">
                            <div><%# Eval("First") %> <%# Eval("Last") %></div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                </div>
            </td>
            <td>
                <h4>Bench</h4>
                <div class="teamSpots">
                <asp:DataList ID="tbBench" runat="server" Width="343px" Height="150px">
                    <ItemTemplate>
                        <div class="player" draggable="true">
                            <div><%# Eval("First") %> <%# Eval("Last") %></div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <h4>Midfielders</h4>
                <div class="teamSpots">
                <asp:DataList ID="tbMidfielders" runat="server" Width="343px" Height="150px">
                    <ItemTemplate>
                        <div class="player" draggable="true">
                            <div><%# Eval("First") %> <%# Eval("Last") %></div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                </div>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
            <h4>Atackers</h4>
            <div class="teamSpots">
            <asp:DataList ID="tbStrikers" runat="server" Width="343px" Height="75px">
                <ItemTemplate>
                    <div class="player" draggable="true">
                        <div><%# Eval("First") %> <%# Eval("Last") %></div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            </div>
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
