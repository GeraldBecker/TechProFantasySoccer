<%@ Page Title="Fantasy Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="FantasyPointsDetails.aspx.cs" Inherits="TechProFantasySoccer.FantasyPointsDetails" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3><%=UserName%>'s Team</h3>

    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" OnSorting="GridView1_Sorting">
    </asp:GridView>
    <br />
     <asp:Label ID="FantasyPointsHLabel" runat="server" Font-Size="Large" Text="Total Fantasy Points:"></asp:Label>
     <asp:Label ID="FantasyPointsLabel" runat="server" Font-Size="Large" Text="0 pts"></asp:Label>

</asp:Content>
