<%@ Page Title="Edit Monthly Player Stats" Language="C#" AutoEventWireup="true" CodeBehind="ChooseMonth.aspx.cs" MasterPageFile="~/Site.Master" Inherits="TechProFantasySoccer.ChooseMonth" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="banner_cheer">
        <h1 class="title_cheer">EDIT MONTHLY PLAYERS</h1>
    </div>

    <div class="center_content" >

        <h3>Choose a month:</h3>

        <br />

        <asp:Button id="sepBtn" runat="server" CssClass="btn btn-primary" Width="50%" text="September" OnClick="MonthButton_Click"/>

        <br />
        <br />

        <asp:Button id="octBtn" runat="server" CssClass="btn btn-primary" Width="50%" text="October" OnClick="MonthButton_Click"/>

        <br />
        <br />

        <asp:Button id="novBtn" runat="server" CssClass="btn btn-primary" Width="50%" text="November" OnClick="MonthButton_Click"/>

        <br />
        <br />

        <asp:Button id="decBtn" runat="server" CssClass="btn btn-primary" Width="50%" text="December" OnClick="MonthButton_Click"/>

        <br />
        <br />

        <asp:Button id="janBtn" runat="server" CssClass="btn btn-primary" Width="50%" text="January" OnClick="MonthButton_Click"/>

        <br />
        <br />

        <asp:Button id="febBtn" runat="server" CssClass="btn btn-primary" Width="50%" text="February" OnClick="MonthButton_Click"/>

    </div> 
    
</asp:Content>
