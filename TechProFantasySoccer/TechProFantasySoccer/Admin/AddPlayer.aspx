<%@ Page Title="Add Player" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="AddPlayer.aspx.cs" Inherits="TechProFantasySoccer.Admin.CreatePlayer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $(".selectedblackout").click(function () {
                window.document.location = $(this).data("href");
            });
        });
    </script>
    <h2>Add or Edit Players</h2>
    <br />
    <div id="SearchBar">
        <asp:Table ID="Table1" runat="server">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>
                    First Name
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Last Name
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Club
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Position
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Cost
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="FirstNameTextBox" runat="server" Width="150px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="LastNameTextBox" runat="server" Width="150px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ClubDropDown" runat="server" DataSourceID="ClubsDataSource" 
                        DataTextField="ClubName" DataValueField="ClubId"></asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="PositionDropDown" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="1" Text="Striker"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Midfielder"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Defender"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Goalie"></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="CostTextBox" runat="server" Width="50px"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow>
                <asp:TableCell ColumnSpan="5">
                    <asp:Button ID="SubmitButton" OnClick="SubmitButton_Click" runat="server" Text="Add Player" 
                        CssClass="btn btn-success btn-block"/>
                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>

    </div>
    <br />
    <asp:GridView ID="PlayerGridView" runat="server" AllowSorting="False"  
        AllowPaging="true" PageSize="40" OnPageIndexChanging="PlayerGridView_PageIndexChanging">
    </asp:GridView>

    <asp:SqlDataSource ID="ClubsDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:FantasySoccerConnectionString %>" 
        SelectCommand="SELECT * FROM Clubs ORDER BY ClubName"
        InsertCommand="INSERT INTO Players (FirstName, LastName, Cost, PositionRef, ClubId) 
                        VALUES (@FirstName, @LastName, @Cost, @Position, @Club)"
        >
        <InsertParameters>
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="Cost" Type="Int32" />
            <asp:Parameter Name="Position" Type="Int32" />
            <asp:Parameter Name="Club" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>

</asp:Content>