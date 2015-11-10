<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddLeague.aspx.cs" Inherits="TechProFantasySoccer.Admin.AddLeague" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <br />
    <div id="SearchBar">
        <asp:Table ID="Table1" runat="server">
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
            <asp:TableFooterRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Button ID="SubmitButton" OnClick="SubmitButton_Click" runat="server" Text="Add League" 
                        style="background-color:#99ffa9; height:50px; width:200px; margin-right:20px;"/>
                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>

    </div>
    <br />
    <asp:GridView ID="LeagueGridView" runat="server" AllowSorting="False"  
        AllowPaging="true" PageSize="40">
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
