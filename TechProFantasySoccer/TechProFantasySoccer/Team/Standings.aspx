<%@ Page Title="Standings." Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Standings.aspx.cs" Inherits="TechProFantasySoccer.LeagueStandings" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $(".selectedblackout").click(function () {
                window.document.location = $(this).data("href");
            });
        });
    </script>

    <div class="row">
        <div class="col-md-8">
            <section id="mainPageDetails">
                <div class="form-horizontal">

                    <%--<div class="form-group">
                        <div class="col-md-10">
                            <table id="standingsTable" class="table table-striped table-hover">
                                <thead>
                                    <tr class="info">
                                        <th>Team Name</th>
                                        <th>Salary Cap</th>
                                        <th>Points Earned</th>
                                        <th>Postion</th>
                                        <th>Striker Rank</th>
                                        <th>Midfielder Rank</th>
                                        <th>Defensive Rank</th>
                                        <th>Goalie Rank</th>
                                        <th>Current Month Points</th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>--%>

                    <asp:GridView ID="StandingsGridView" runat="server" AllowSorting="True"
                        AllowPaging="true" PageSize="40" >
                    </asp:GridView>

                    <br />

                    <div class="form-group">
                        <div class="col-md-offset-3 col-md-10">
                            <asp:Button id="BackBtn" runat="server" class="btn btn-primary" Width="148px" text="BACK" />
                        </div>
                    </div>

                 </div>
            </section>
        </div>
    </div>
    

</asp:Content>
