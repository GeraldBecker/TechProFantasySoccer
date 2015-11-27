<%@ Page Title="Admin Home" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ManagerMain.aspx.cs" Inherits="TechProFantasySoccer.ManagerMain" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="banner_cheer">
        <h1 class="title_cheer">ADMINISTRATION</h1>
    </div>

    <div class="center_content">

        <asp:Button id="editMnthPStatBtn" runat="server" class="btn btn-default" Width="50%" 
            text="Edit Monthly Player Stats" OnClick="ManagerButton_Click"/>

        <br />
        <br />

        <!--<div class="form-group">
            <div class="col-md-10">
                <asp:Button id="editPInfoBtn" runat="server" class="btn btn-default" Width="300px" 
                    text="Edit Player Information" OnClick="ManagerButton_Click"/>
            </div>
        </div>-->

        <asp:Button id="addPlayerBtn" runat="server" CssClass="btn btn-default" Width="50%" 
            text="Add or Edit Players" OnClick="ManagerButton_Click"/>

        <br />
        <br />

        <asp:Button id="addClubBtn" runat="server" CssClass="btn btn-default" Width="50%" 
            text="Add Club" OnClick="ManagerButton_Click"/>

        <br />
        <br />

        <asp:Button id="addLeagueBtn" runat="server" CssClass="btn btn-default" Width="50%" 
            text="Add League" OnClick="ManagerButton_Click"/>

        <br />
        <br />

        <!--<asp:Button id="createTeamBtn" runat="server" CssClass="btn btn-info" Width="50%" 
            text="Create Teams" OnClick="ManagerButton_Click"/>

        <br />
        <br />

        <asp:Button id="editLineupBtn" runat="server" CssClass="btn btn-info" Width="50%" 
            text="Edit Team Lineups" OnClick="ManagerButton_Click"/>

        <br />
        <br /> -->

        <asp:Button id="setScoringBtn" runat="server" CssClass="btn btn-default" Width="50%" 
            text="Set Scoring Values" OnClick="ManagerButton_Click"/>

        <br />
        <br />

        <asp:Button id="selectUsersBtn" runat="server" CssClass="btn btn-default" Width="50%" 
            text="Set User Access Levels" OnClick="ManagerButton_Click"/>

        <br />
        <br />

        <asp:Button id="settingsBtn" runat="server" CssClass="btn btn-primary" Width="50%" 
            text="Settings" OnClick="ManagerButton_Click"/>

    </div> 

</asp:Content>
