<%@ Page Title="View Player" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="ViewPlayer.aspx.cs" Inherits="TechProFantasySoccer.Players.ViewPlayer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= PlayerName %></h2>
    
    <div class="player_info">
        <asp:Table ID="MainInfoTable" runat="server">
            <asp:TableRow>
                <asp:TableHeaderCell CssClass="playerdesc">
                    <asp:Label ID="Label3" runat="server" Text="League:"></asp:Label>
                </asp:TableHeaderCell>
                <asp:TableCell>
                    <asp:Label ID="LeagueNameLabel" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableHeaderCell CssClass="playerdesc">
                    <asp:Label ID="Label4" runat="server" Text="Club:"></asp:Label>
                </asp:TableHeaderCell>
                <asp:TableCell>
                    <asp:Label ID="ClubNameLabel" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableHeaderCell CssClass="playerdesc">
                    <asp:Label ID="Label1" runat="server" Text="Position:"></asp:Label>
                </asp:TableHeaderCell>
                <asp:TableCell>
                    <asp:Label ID="PositionLabel" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableHeaderCell CssClass="playerdesc">
                    <asp:Label ID="Label5" runat="server" Text="Cost:"></asp:Label>
                </asp:TableHeaderCell>
                <asp:TableCell>
                    <asp:Label ID="CostLabel" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableHeaderCell CssClass="playerdesc">
                    <br />
                </asp:TableHeaderCell>
                <asp:TableCell>
                    <br />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableHeaderCell CssClass="playerdesc">
                    <asp:Label ID="Label8" runat="server" Text="Owned By:"></asp:Label>
                </asp:TableHeaderCell>
                <asp:TableCell>
                    <asp:Label ID="OwnedByLabel" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <div class="player_functions">
        <asp:Button id="AddPlayerBtn" runat="server" class="btn btn-success" Text="Add Player" OnClick="PlayerFunctionBtn_Click"/>
        <asp:Button id="TradePlayerBtn" runat="server" class="btn btn-info" Text="Trade Player" OnClick="PlayerFunctionBtn_Click"
            disabled="disabled"/>
        <asp:Button id="DropPlayerBtn" runat="server" class="btn btn-danger" Text="Drop Player" OnClick="PlayerFunctionBtn_Click"/>
    </div>
    <div class="clear"></div>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Total Fantasy Points:" Font-Bold="true" CssClass="fantasy_points_label"></asp:Label>
    &nbsp;&nbsp;<asp:Label ID="FantasyPointsLabel" runat="server" Text="" CssClass="fantasy_points"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="FantasyDetailsGridView" runat="server" AlternatingRowStyle-BackColor="#18bc9c"></asp:GridView>

    


</asp:Content>