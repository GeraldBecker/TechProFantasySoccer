<%@ Page Title="View Team" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" 
    CodeBehind="ViewTeam.aspx.cs" Inherits="TechProFantasySoccer.Team.ViewTeam" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $(".selectedblackout").click(function () {
                window.document.location = $(this).data("href");
            });
        });
    </script>

    <h2><%=UserName%>'s Team</h2>
    <h3>Available Cap Space: <span style="font-size:16px;"><%=AvailCap%></span></h3>
    
    <h4>Players:  (YTD Stats)</h4>
    <asp:GridView ID="TeamGridView" runat="server" AllowSorting="True" OnSorting="TeamGridView_Sorting"
        AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="PlayerId" DataField="PlayerId" SortExpression="PlayerId"/>
            <asp:BoundField HeaderText="First" DataField="First" SortExpression="First"/>
            <asp:BoundField HeaderText="Last" DataField="Last" SortExpression="Last"/>
            <asp:BoundField HeaderText="Cost" DataField="Cost" SortExpression="Cost"/>
            <asp:BoundField HeaderText="Club" DataField="Club" SortExpression="Club"/>
            <asp:BoundField HeaderText="Position" DataField="Position" SortExpression="Position"/>
            <asp:BoundField HeaderText="Active" DataField="Active" SortExpression="Active"/>
            <asp:BoundField HeaderText="Goals" DataField="Goals" SortExpression="Goals"/>
            <asp:BoundField HeaderText="Shots" DataField="Shots" SortExpression="Shots"/>
            <asp:BoundField HeaderText="Assists" DataField="Assists" SortExpression="Assists"/>
            <asp:BoundField HeaderText="Min Played" DataField="Min Played" SortExpression="Min Played"/>
            <asp:BoundField HeaderText="Fouls" DataField="Fouls" SortExpression="Fouls"/>
            <asp:BoundField HeaderText="YC" DataField="YC" SortExpression="YC"/>
            <asp:BoundField HeaderText="RC" DataField="RC" SortExpression="RC"/>
            <asp:BoundField HeaderText="GA" DataField="GA" SortExpression="GA"/>
            <asp:BoundField HeaderText="CS" DataField="CS" SortExpression="CS"/>
            <asp:BoundField HeaderText="Total Fantasy Pts" DataField="Total Fantasy Pts" SortExpression="Total Fantasy Pts"/>
        </Columns>
    </asp:GridView>
    <br />

    <asp:Table ID="StatsTable" runat="server">
        <asp:TableRow>
            <asp:TableHeaderCell>
                <asp:Label ID="Label1" runat="server" Text="Goals"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="GoalsPtsLabel" runat="server" Text="0 = 0 pts"></asp:Label>
            </asp:TableCell>
            <asp:TableHeaderCell CssClass="stats_spacing">
                <asp:Label ID="Label3" runat="server" Text="Assists"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="AssistsPtsLabel" runat="server" Text="0 = 0 pts"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>
                <asp:Label ID="Label5" runat="server" Text="Shots"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="ShotsPtsLabel" runat="server" Text="0 = 0 pts"></asp:Label>
            </asp:TableCell>
            <asp:TableHeaderCell CssClass="stats_spacing">
                <asp:Label ID="Label7" runat="server" Text="Min Played"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="MinPlayedPtsLabel" runat="server" Text="0 = 0 pts"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>
                <asp:Label ID="Label9" runat="server" Text="Fouls"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="FoulsPtsLabel" runat="server" Text="0 = 0 pts"></asp:Label>
            </asp:TableCell>
            <asp:TableHeaderCell CssClass="stats_spacing">
                <asp:Label ID="Label11" runat="server" Text="Y Cards"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="YCPtsLabel" runat="server" Text="0 = 0 pts"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>
                <asp:Label ID="Label13" runat="server" Text="R Cards"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="RCPtsLabel" runat="server" Text="0 = 0 pts"></asp:Label>
            </asp:TableCell>
            <asp:TableHeaderCell CssClass="stats_spacing">
                <asp:Label ID="Label15" runat="server" Text="Goals Conceded"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="GCPtsLabel" runat="server" Text="0 = 0 pts"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>
                <asp:Label ID="Label17" runat="server" Text="Saves Made"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="SavesMadePtsLabel" runat="server" Text="0 = 0 pts"></asp:Label>
            </asp:TableCell>
            <asp:TableHeaderCell CssClass="stats_spacing">
                <asp:Label ID="Label18" runat="server" Text="Clean Sheets"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="CleanSheetsPtsLabel" runat="server" Text="0 = 0 pts"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <br />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>
                <asp:Label ID="DefendersLabel" runat="server" Text="Defenders"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="DefendersPtsLabel" runat="server" Text="0 pts"></asp:Label>
            </asp:TableCell>
            <asp:TableHeaderCell CssClass="stats_spacing">
                <asp:Label ID="MidfieldersLabel" runat="server" Text="Midfielders"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="MidfieldersPtsLabel" runat="server" Text="0 pts"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell>
                <asp:Label ID="StrikersLabel" runat="server" Text="Strikers"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="StrikersPtsLabel" runat="server" Text="0 pts"></asp:Label>
            </asp:TableCell>
            <asp:TableHeaderCell CssClass="stats_spacing">
                <asp:Label ID="GoaliesLabel" runat="server" Text="Goalies"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="GoaliesPtsLabel" runat="server" Text="0 pts"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <br />


    <asp:SqlDataSource ID="SqlFantasyDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:FantasySoccerConnectionString %>" 
        SelectCommand="
        SELECT Players.[PlayerId],
        FirstName AS First,
        LastName AS Last,
        [Cost],
        [ClubName] AS Club,
        [Positions].[PositionName] AS Position,
        SUM([PlayerStats].Goals) AS Goals,
        SUM([PlayerStats].Shots) AS Shots,
        SUM([PlayerStats].Assists) AS Assists,
        SUM([PlayerStats].MinPlayed) AS 'Min Played',
        SUM([PlayerStats].Fouls) AS Fouls,
        SUM([PlayerStats].YellowCards) AS YC,
        SUM([PlayerStats].RedCards) AS RC,
        SUM([PlayerStats].GoalsAllowed) AS GA,
        SUM([PlayerStats].SavesMade) AS Saves,
        SUM([PlayerStats].CleanSheets) AS CS,
        dbo.CalculateTotalFantasyPoints(SUM([PlayerStats].Goals),
								        SUM([PlayerStats].Shots), 
								        SUM([PlayerStats].Assists),
								        SUM([PlayerStats].MinPlayed),
								        SUM([PlayerStats].Fouls),
								        SUM([PlayerStats].YellowCards),
								        SUM([PlayerStats].RedCards),
								        SUM([PlayerStats].GoalsAllowed),
								        SUM([PlayerStats].SavesMade),
								        SUM([PlayerStats].CleanSheets),
								        Positions.PositionRef) AS 'Total Fantasy Pts'
        FROM [Players]
        INNER JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId
        INNER JOIN [Positions] ON [Positions].[PositionRef] = Players.PositionRef
        LEFT OUTER JOIN PlayerStats ON PlayerStats.PlayerId = Players.PlayerId
        INNER JOIN Clubs ON Clubs.ClubId = Players.ClubId
        WHERE LineupHistory.Month = DATEPART(MONTH, GETDATE())
        AND LineupHistory.UserId = @UserId
        GROUP BY Players.PlayerId, FirstName, LastName, Players.Cost, Clubs.ClubName, Positions.PositionName, Positions.PositionRef
        ORDER BY Last">

        <SelectParameters>
            <asp:Parameter Name="UserId" Type="string"/>
        </SelectParameters>

    </asp:SqlDataSource>


    <asp:SqlDataSource ID="SqlTeamStats" runat="server" ConnectionString="<%$ ConnectionStrings:FantasySoccerConnectionString %>" >

    </asp:SqlDataSource>


</asp:Content>