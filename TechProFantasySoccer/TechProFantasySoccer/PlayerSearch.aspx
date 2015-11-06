<%@ Page Title="Player Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="PlayerSearch.aspx.cs" Inherits="TechProFantasySoccer.PlayerSearch" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function myFunction() {
            document.getElementById("#MainContent_PlayerSearchGridView").backgroundColor = "red";
        }


        jQuery(document).ready(function ($) {
            $(".selectedblackout").click(function () {
                window.document.location = $(this).data("href");
            });
        });
    </script>

    


    <h2><%: Title %></h2>

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
                    League Name
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Club Name
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Position
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="FirstNameTextBox" runat="server" Width="100px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="LastNameTextBox" runat="server" Width="100px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="LeagueTextBox" runat="server" Width="100px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="ClubTextBox" runat="server" Width="100px"></asp:TextBox>
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
            </asp:TableRow>
            <asp:TableFooterRow>
                <asp:TableCell ColumnSpan="5">
                    <asp:Button ID="ClearButton" CommandName="ClearButton" runat="server" Text="Search" /><asp:Button runat="server" 
                        Text="Clear" OnClick="ClearEntries"/>
                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>

    </div>
    <br />
    <asp:GridView ID="PlayerSearchGridView" runat="server" AllowSorting="True" OnSorting="PlayerSearchGridView_Sorting" 
        AllowPaging="true" PageSize="40">
    </asp:GridView>

</asp:Content>