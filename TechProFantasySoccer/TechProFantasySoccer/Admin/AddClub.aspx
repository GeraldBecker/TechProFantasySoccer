<%@ Page Title="Add Club" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddClub.aspx.cs" Inherits="TechProFantasySoccer.Admin.AddTeam" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="banner_cheer">
       <h1 class="title_cheer">ADD A CLUB</h1>
    </div>

    <div id="SearchBar center_content">

        <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">

            <asp:TableHeaderRow>
                <asp:TableHeaderCell>
                    Team Name
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    League
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="TeamNameTextBox" runat="server" Width="150px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>

                    <asp:DropDownList ID="LeagueDropDown" runat="server" DataSourceID="FantasyDataSource"
                      DataTextField="LeagueName"
                      DataValueField="LeagueId"
                      Height="27px">
                    </asp:DropDownList>

                </asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="2">

                    <br />
                    <asp:Button ID="SubmitButton" OnClick="SubmitButton_Click" runat="server" Text="Add Club" CssClass="btn btn-default"/>

                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>

    </div>

    <br />

    <asp:GridView ID="ClubGridView" runat="server" AllowSorting="False" AllowPaging="true" PageSize="40" HorizontalAlign="Center" PagerStyle-HorizontalAlign="Center" PagerStyle-Font-Bold="true">
    </asp:GridView>

    <asp:SqlDataSource ID="FantasyDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:FantasySoccerConnectionString %>" 
        SelectCommand="SELECT * FROM Leagues ORDER BY LeagueName"
        InsertCommand="INSERT INTO Clubs (ClubName, LeagueId) 
                        VALUES (@ClubName, @League)"
        >
        <InsertParameters>
            <asp:Parameter Name="ClubName" Type="String" />
            <asp:Parameter Name="League" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>

</asp:Content>