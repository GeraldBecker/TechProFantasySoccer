<%@ Page Title="Edit Monthly Player Stats" Language="C#" AutoEventWireup="true" CodeBehind="ChooseMonth.aspx.cs" MasterPageFile="~/Site.Master" Inherits="TechProFantasySoccer.ChooseMonth" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Choose a month:</h3>

    <table id="table1">
        <tr>
            <td><asp:Button id="janBtn" runat="server" Height="36px" Width="300px" text="January" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="febBtn" runat="server" Height="36px" Width="300px" text="February" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="marBtn" runat="server" Height="36px" Width="300px" text="March" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="aprBtn" runat="server" Height="36px" Width="300px" text="April" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="mayBtn" runat="server" Height="36px" Width="300px" text="May" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="junBtn" runat="server" Height="36px" Width="300px" text="June" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="julBtn" runat="server" Height="36px" Width="300px" text="July" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="augBtn" runat="server" Height="36px" Width="300px" text="August" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="sepBtn" runat="server" Height="36px" Width="300px" text="September" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="octBtn" runat="server" Height="36px" Width="300px" text="October" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="novBtn" runat="server" Height="36px" Width="300px" text="November" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="decBtn" runat="server" Height="36px" Width="300px" text="December" BackColor="#eaeaea" /></td>
        </tr>
    </table>
    
</asp:Content>
