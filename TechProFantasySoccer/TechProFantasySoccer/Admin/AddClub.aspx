<%@ Page Title="Add Club" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddClub.aspx.cs" Inherits="TechProFantasySoccer.Admin.AddTeam" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $(".selectedblackout").click(function () {
                window.document.location = $(this).data("href");
            });
        });
    </script>
    <br />
    <div id="SearchBar">
        <asp:Table ID="Table1" runat="server">
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
                        DataTextField="LeagueName" DataValueField="LeagueId"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Button ID="SubmitButton" OnClick="SubmitButton_Click" runat="server" Text="Add Club" 
                        style="background-color:#99ffa9; height:50px; width:200px; margin-right:20px;"/>
                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>

    </div>
    <br />
    <asp:GridView ID="ClubGridView" runat="server" AllowSorting="False"  
        AllowPaging="true" PageSize="40">
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