﻿<%@ Page Title="Team Home " Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MainPage.aspx.cs" Inherits="TechProFantasySoccer.MainPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <div class="row">
        <div class="col-md-8">
            <section id="mainPageDetails">
                <div class="form-horizontal">

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="LeagueName" CssClass="col-md-2 control-label">League:</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="LeagueName" CssClass="form-control" TextMode="SingleLine" Text="Brazil Fans League" Enabled="True" />
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="TeamName" CssClass="col-md-2 control-label">Team:</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="TeamName" TextMode="SingleLine" CssClass="form-control" Text="DeutscheWelle" />
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-md-offset-3 col-md-10">
                            <asp:Button id="PlayerSearchBtn" runat="server" class="btn btn-default" Width="148px" text="Player Search" />
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-offset-3 col-md-10">
                            <asp:Button id="TeamOverviewBtn" runat="server" class="btn btn-default" Width="148px" text="Team Overview" />
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-offset-3 col-md-10">
                            <asp:Button id="SetLineupBtn" runat="server" class="btn btn-default" Width="148px" text="Set Team Lineup" />
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-offset-3 col-md-10">
                            <asp:Button id="LeagueChatBtn" runat="server" class="btn btn-primary" Width="148px" text="League Chat" />
                        </div>
                    </div>

                 </div>
            </section>
        </div>
    </div>
</asp:Content>

