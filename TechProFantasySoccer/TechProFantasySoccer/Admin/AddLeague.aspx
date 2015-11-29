<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddLeague.aspx.cs" Inherits="TechProFantasySoccer.Admin.AddLeague" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="banner_cheer">
        <h1 class="title_cheer">ADD A LEAGUE</h1>
    </div>

    <div id="SearchBar">
        <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>
                    League Name
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="LeagueNameTextBox" runat="server" Width="150px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableHeaderCell >
                    <asp:Label ID="ConfirmationLabel" runat="server" Text=""></asp:Label>
                </asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableFooterRow HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="2">

                    <br />

                    <asp:Button ID="SubmitButton" OnClick="SubmitButton_Click" runat="server" Text="Add League" CssClass="btn btn-default"/>

                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>

    </div> 
    <br />
    <asp:GridView ID="LeagueGridView" runat="server" AllowSorting="False"  
        AllowPaging="true" PageSize="40" HorizontalAlign="Center" PagerStyle-HorizontalAlign="Center" PagerStyle-Font-Bold="true">
    </asp:GridView>

    <asp:SqlDataSource ID="FantasyDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:FantasySoccerConnectionString %>" 
        SelectCommand="SELECT * FROM Leagues ORDER BY LeagueName"
        InsertCommand="INSERT INTO Leagues (LeagueName)
                        VALUES (@LeagueName)"
        >
        <InsertParameters>
            <asp:Parameter Name="LeagueName" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>

</asp:Content>
