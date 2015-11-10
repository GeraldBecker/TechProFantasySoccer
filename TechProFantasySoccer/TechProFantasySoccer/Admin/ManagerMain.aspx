<%@ Page Title="Manager Home " Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ManagerMain.aspx.cs" Inherits="TechProFantasySoccer.ManagerMain" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <div class="row">
        <div class="col-md-8">
            <section id="mainPageDetails">
                <div class="form-horizontal">

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button id="editMnthPStatBtn" runat="server" class="btn btn-default" Width="300px" 
                                text="Edit Monthly Player Stats" OnClick="ManagerButton_Click"/>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button id="editPInfoBtn" runat="server" class="btn btn-default" Width="300px" 
                                text="Edit Player Information" OnClick="ManagerButton_Click"/>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button id="addPlayerBtn" runat="server" class="btn btn-default" Width="300px" 
                                text="Add Player" OnClick="ManagerButton_Click"/>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button id="addClubBtn" runat="server" class="btn btn-default" Width="300px" 
                                text="Add Club" OnClick="ManagerButton_Click"/>
                        </div>
                    </div>

                      <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button id="addLeagueBtn" runat="server" class="btn btn-default" Width="300px" 
                                text="Add League" OnClick="ManagerButton_Click"/>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button id="createTeamBtn" runat="server" class="btn btn-info" Width="300px" 
                                text="Create Teams" OnClick="ManagerButton_Click"/>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button id="editLineupBtn" runat="server" class="btn btn-info" Width="300px" 
                                text="Edit Team Lineups" OnClick="ManagerButton_Click"/>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button id="setScoringBtn" runat="server" class="btn btn-primary" Width="300px" 
                                text="Set Scoring Values" OnClick="ManagerButton_Click"/>
                        </div>
                    </div>

                 </div>
            </section>
        </div>
    </div> 
</asp:Content>
