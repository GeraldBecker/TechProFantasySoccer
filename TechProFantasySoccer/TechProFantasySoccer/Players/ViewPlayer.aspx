<%@ Page Title="View Player" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="ViewPlayer.aspx.cs" Inherits="TechProFantasySoccer.Players.ViewPlayer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= PlayerName %></h2>

    
    <asp:Table ID="MainInfoTable" runat="server">
        <asp:TableRow>
            <asp:TableHeaderCell>
                <asp:Label ID="Label3" runat="server" Text="League:"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="LeagueNameLabel" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>
                <asp:Label ID="Label4" runat="server" Text="Club:"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="ClubNameLabel" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>
                <asp:Label ID="Label1" runat="server" Text="Position:"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="PositionLabel" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>
                <asp:Label ID="Label5" runat="server" Text="Cost:"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="CostLabel" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>
                <asp:Label ID="Label2" runat="server" Text="Total Fantasy Points Earned:"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="FantasyPointsLabel" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <br />
    <asp:GridView ID="FantasyDetailsGridView" runat="server" AlternatingRowStyle-BackColor="#18bc9c"></asp:GridView>

    


</asp:Content>