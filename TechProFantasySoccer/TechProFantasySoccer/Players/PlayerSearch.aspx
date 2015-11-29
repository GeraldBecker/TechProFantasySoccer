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

    
    <div class="banner_reg">
       <h1 class="title_reg">PLAYER SEARCH</h1>
    </div>

    <div id="SearchBar">
        <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
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
                    <asp:TextBox ID="FirstNameTextBox" runat="server" Width="120px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="LastNameTextBox" runat="server" Width="120px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="LeagueTextBox" runat="server" Width="120px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="ClubTextBox" runat="server" Width="120px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="PositionDropDown" runat="server" Height="27px" Width="120px">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="1" Text="Striker"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Midfielder"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Defender"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Goalie"></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="5">
                    <br />
                    <asp:Button ID="SubmitButton" CommandName="SubmitButton" runat="server" Text="Search" CssClass="btn btn-success"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" Text="Clear" OnClick="ClearEntries" CssClass="btn btn-default"/>
                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>

    </div>
    <br />
    <asp:GridView ID="PlayerSearchGridView" runat="server" AllowSorting="True" OnSorting="PlayerSearchGridView_Sorting" AllowPaging="true" PageSize="40" OnPageIndexChanging="PlayerSearchGridView_PageIndexChanging">
    </asp:GridView>

</asp:Content>