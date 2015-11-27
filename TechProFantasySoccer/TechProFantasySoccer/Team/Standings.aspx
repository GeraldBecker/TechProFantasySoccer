<%@ Page Title="Standings" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Standings.aspx.cs" 
    Inherits="TechProFantasySoccer.LeagueStandings" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $(".selectedblackout").click(function () {
                window.document.location = $(this).data("href");
            });
        });
    </script>

    <div class="center_content">

        <div class="banner_reg">
            <h1 class="title_reg">TEAM STANDINGS</h1>
        </div>

        <asp:GridView ID="StandingsGridView" runat="server" AllowSorting="True" AllowPaging="true" PageSize="40" HorizontalAlign="Center">
        </asp:GridView>

        </div>
    

</asp:Content>
