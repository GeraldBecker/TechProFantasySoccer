<%@ Page Title="Create Team " Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AboutToJoin.aspx.cs" Inherits="TechProFantasySoccer.AboutToJoin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

        <div class="row">
        <div class="col-md-8">
            <section id="mainPageDetails">
                <div class="form-horizontal">

                    <div class="progress progress-striped active">
                        <div class="progress-bar" style="width: 98%"></div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <h4>You are about to join:</h4>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10 panel panel-default">
                            <div class="panel-body">
                                Brazil Fans League
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <h4>Name your Team:</h4>
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
                            <asp:Button id="NextBtn" runat="server" class="btn btn-primary" Width="148px" text="JOING LEAGUE" />
                        </div>
                    </div>

                 </div>
            </section>
        </div>
    </div>   
</asp:Content>
