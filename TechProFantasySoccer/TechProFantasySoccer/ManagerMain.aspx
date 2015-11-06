<%@ Page Title="Manager Home " Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ManagerMain.aspx.cs" Inherits="TechProFantasySoccer.ManagerMain" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <table id="table1">
        <tr>
            <td><asp:Button id="editMnthPStatBtn" runat="server" Height="36px" Width="300px" text="Edit Monthly Player Stats" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="editPInfoBtn" runat="server" Height="36px" Width="300px" text="Edit Player Information" BackColor="#eaeaea" /></td>
        </tr>
        <tr>
            <td><asp:Button id="createTeamBtn" runat="server" Height="36px" Width="300px" text="Create Teams" BackColor="#ccffcc" /></td>
        </tr>
        <tr>
            <td><asp:Button id="editLineupBtn" runat="server" Height="36px" Width="300px" text="Edit Team Lineups" BackColor="#ccffcc" /></td>
        </tr>
        <tr>
            <td><asp:Button id="setScoringBtn" runat="server" Height="36px" Width="300px" text="Set Scoring Values" BackColor="#ffcc00" /></td>
        </tr>
    </table>
    
</asp:Content>
