<%@ Page Title="Team Home "Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MainPage.aspx.cs" Inherits="TechProFantasySoccer.MainPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <asp:Table ID="Table1" runat="server" CellPadding="10">
        <asp:TableRow>
            <asp:TableHeaderCell>LEAGUE:</asp:TableHeaderCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>  </asp:TableHeaderCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>TEAM:</asp:TableHeaderCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

