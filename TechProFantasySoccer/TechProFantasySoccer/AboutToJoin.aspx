<%@ Page Title="Create Team " Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AboutToJoin.aspx.cs" Inherits="TechProFantasySoccer.AboutToJoin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

        <div class="row">
        <div class="col-md-8">
            <section id="mainPageDetails">
                <div class="form-horizontal">

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Label runat="server" AssociatedControlID="LeagueNameLabel">You Are About To Join:</asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-offset-1 col-md-10">
                            <asp:Label ID="LeagueNameLabel" runat="server" ForeColor="#666666">Brazil Fans League</asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Label runat="server" AssociatedControlID="TeamNameTextBox">Team:</asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="TeamNameTextBox" TextMode="SingleLine" CssClass="form-control" Text="DeutscheWelle" />
                        </div>
                    </div>

                    <br />

                    <div class="form-group">
                        <div class="col-md-offset-1 col-md-10">
                            <asp:Button id="NextBtn" runat="server" Height="36px" Width="148px" text="JOING LEAGUE" BackColor="#00cc99" />
                        </div>
                    </div>

                 </div>
            </section>
        </div>
    </div>   
</asp:Content>
