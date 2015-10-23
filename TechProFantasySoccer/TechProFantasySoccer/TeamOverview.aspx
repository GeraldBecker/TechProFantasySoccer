<%@ Page Title="Team Overview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeamOverview.aspx.cs" 
    Inherits="TechProFantasySoccer.TeamOverview" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Available Cap Space: <span style="font-size:16px;">$50,000</span></h3>
    
    <h4>Players:  (YTD Stats)</h4>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True">
        <columns>
            <asp:BoundField DataField="Name" HeaderText="User Name" SortExpression="UserName" />
            <asp:BoundField DataField="Cost" HeaderText="Full Name" SortExpression="FullName" />
            <asp:BoundField DataField="Position" HeaderText="Full Name" SortExpression="FullName" />
        </columns>
    </asp:GridView>
    

</asp:Content>