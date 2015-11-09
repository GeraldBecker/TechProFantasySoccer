<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TechProFantasySoccer._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Fantasy Soccer</h1>
        <p class="lead">This site is going to be killer.</p>
        
    </div>

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
                            <asp:Button id="PlayerSearchBtn" runat="server" class="btn btn-default" Width="148px" text="Player Search" 
                                OnClick="MainPageBtn_Click"/>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-offset-3 col-md-10">
                            <asp:Button id="TeamOverviewBtn" runat="server" class="btn btn-default" Width="148px" text="Team Overview" 
                                OnClick="MainPageBtn_Click"/>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-offset-3 col-md-10">
                            <asp:Button id="SetLineupBtn" runat="server" class="btn btn-default" Width="148px" text="Set Team Lineup" 
                                OnClick="MainPageBtn_Click"/>
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


    <div>
        <ul>
            <li><a runat="server" href="~/About">About</a></li>
            <li><a runat="server" href="~/Contact">Contact</a></li>
        </ul>
    </div>

</asp:Content>
