<%@ Page Title="Join a League " Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="JoinLeague.aspx.cs" Inherits="TechProFantasySoccer.JoinLeague" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="banner"></div>

    <h2><%: Title %></h2>

    <div class="row">
        <div class="col-md-8">
            <section id="mainPageDetails">
                <div class="form-horizontal">

                    <div class="progress progress-striped active">
                        <div class="progress-bar" style="width: 48%"></div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <h4>Leagues Joined:</h4>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <table id="leaguesJoinedTable" class="table table-striped table-hover">
                                <thead>
                                    <tr class="success">
                                        <th>#</th>
                                        <th>League Name</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>1</td>
                                        <td>Test League</td>
                                    </tr>
                                    <tr class="active">
                                        <td>2</td>
                                        <td>Germans Only League</td>
                                    </tr>
                                    <tr>
                                        <td>3</td>
                                        <td>Hello League</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <br />

                    <div class="form-group">
                        <div class="col-md-10">
                            <h4>Leagues Available:</h4>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <table id="leaguesAvailTable" class="table table-striped table-hover">
                                <thead>
                                    <tr class="success">
                                        <th>#</th>
                                        <th>League Name</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>1</td>
                                        <td>Brazil Fans League</td>
                                    </tr>
                                    <tr class="active">
                                        <td>2</td>
                                        <td>Protugal League</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-offset-3 col-md-10">
                            <asp:Button id="NextBtn" runat="server" class="btn btn-primary" Width="148px" text="Next" />
                        </div>
                    </div>

                 </div>
            </section>
        </div>
    </div>
</asp:Content>
