<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TechProFantasySoccer._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Fantasy Soccer</h1>
        <p class="lead">This site is going to be killer.</p>
        
    </div>

    <div class="row">
        <div class="col-md-8">
            <section id="defaultPageDetails">
                <div class="form-horizontal">

                    <div class="form-group" style="margin-left:auto; margin-right:auto;">
                        <asp:Label runat="server" AssociatedControlID="TeamName" CssClass="col-md-2 control-label">Team:</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="TeamName" TextMode="SingleLine" CssClass="form-control" Text="DeutscheWelle" />
                        </div>
                    </div>

                    <div class="form-group" style="margin-left:auto; margin-right:auto;">
                        <div class="col-md-10">
                            <asp:Button id="PlayerSearchBtn" runat="server" class="btn btn-primary" Width="300px" text="Player Search" 
                                OnClick="MainPageBtn_Click"/>
                        </div>
                    </div>

                    <div class="form-group" style="margin-left:auto; margin-right:auto;">
                        <div class="col-md-10">
                            <asp:Button id="TeamOverviewBtn" runat="server" class="btn btn-primary" Width="300px" text="Team Overview" 
                                OnClick="MainPageBtn_Click"/>
                        </div>
                    </div>

                    <div class="form-group" style="margin-left:auto; margin-right:auto;">
                        <div class="col-md-10">
                            <asp:Button id="SetLineupBtn" runat="server" class="btn btn-primary" Width="300px" text="Set Team Lineup" 
                                OnClick="MainPageBtn_Click"/>
                        </div>
                    </div>


                 </div>
            </section>
        </div>
    </div>

</asp:Content>
