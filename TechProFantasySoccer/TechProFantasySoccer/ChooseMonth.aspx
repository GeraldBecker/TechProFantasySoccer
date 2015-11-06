<%@ Page Title="Edit Monthly Player Stats" Language="C#" AutoEventWireup="true" CodeBehind="ChooseMonth.aspx.cs" MasterPageFile="~/Site.Master" Inherits="TechProFantasySoccer.ChooseMonth" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Choose a month:</h3>

    <table id="table1">
        <tr>
            <td><asp:Button id="sepBtn" runat="server" Height="36px" Width="300px" text="September" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="octBtn" runat="server" Height="36px" Width="300px" text="October" BackColor="#e7e7e7" /></td>
        </tr>
        <tr>
            <td><asp:Button id="novBtn" runat="server" Height="36px" Width="300px" text="November" BackColor="#e4e4e4" /></td>
        </tr>
        <tr>
            <td><asp:Button id="decBtn" runat="server" Height="36px" Width="300px" text="December" BackColor="#cccccc" /></td>
        </tr>
        <tr>
            <td><asp:Button id="janBtn" runat="server" Height="36px" Width="300px" text="January" BackColor="#b4b4b4" /></td>
        </tr>
        <tr>
            <td><asp:Button id="febBtn" runat="server" Height="36px" Width="300px" text="February" BackColor="#9c9c9c" /></td>
        </tr>
    </table>
    
</asp:Content>
