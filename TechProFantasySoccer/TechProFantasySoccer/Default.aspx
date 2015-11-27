﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TechProFantasySoccer._Default" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="banner"></div>

    <div class="container">

        <div class="row">

                <div class="center_content">

                    <label ID="teamLabel">Team:&nbsp;&nbsp;</label>
                    <asp:Label runat="server" ID="TeamName" Text=""></asp:Label>

                    <br />

                    <asp:Button id="SetLineupBtn" runat="server" CssClass="btn btn-primary" Width="40%" text="Set Team Lineup" OnClick="MainPageBtn_Click"/>  

                    <br />
                    <br />

                    <asp:Button id="TeamOverviewBtn" runat="server" CssClass="btn btn-primary" Width="40%" text="My Team" OnClick="MainPageBtn_Click"/>

                    <br />
                    <br />

                    <asp:Button id="PlayerSearchBtn" runat="server" CssClass="btn btn-primary" Width="40%" text="Player Search" OnClick="MainPageBtn_Click"/>

                    <br />
                    <br />

                    <asp:Button id="StandingsBtn" runat="server" CssClass="btn btn-primary" Width="40%" text="Standings" OnClick="MainPageBtn_Click"/>

                 </div>
        </div>

    </div>

</asp:Content>
