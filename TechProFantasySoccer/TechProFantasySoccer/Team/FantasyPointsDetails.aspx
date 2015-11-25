<%@ Page Title="Fantasy Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="FantasyPointsDetails.aspx.cs" Inherits="TechProFantasySoccer.FantasyPointsDetails" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3><%=UserName%>'s Team</h3>

    <asp:GridView ID="FantasyDetailsGridView" runat="server" AllowSorting="True" OnSorting="FantasyDetailsGridView_Sorting"
        AlternatingRowStyle-BackColor="#18bc9c">
    </asp:GridView>
    <br />
     <asp:Label ID="FantasyPointsHLabel" runat="server" Text="Total Fantasy Points:" CssClass="fantasy_points_label"></asp:Label>&nbsp;&nbsp;
     <asp:Label ID="FantasyPointsLabel" runat="server" Text="0 pts" CssClass="fantasy_points"></asp:Label>

</asp:Content>
