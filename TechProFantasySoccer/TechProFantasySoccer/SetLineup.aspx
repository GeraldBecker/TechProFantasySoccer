<%@ Page Title="Set Lineup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetLineup.aspx.cs" Inherits="TechProFantasySoccer.SetLineup" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        //Add listeners to the player boxes and position drop boxes
        $("div .player").each(function () {
            this.addEventListener('dragstart', OnDragStart, false);
            this.addEventListener('dragend', OnDragEnd, false);
        });

        $("div .teamSpots").each(function () {
            this.addEventListener('dragenter', OnDragEnter, false);
            this.addEventListener('dragleave', OnDragLeave, false);
            this.addEventListener('dragover', OnDragOver, false);
            this.addEventListener('drop', OnDrop, false);
            this.addEventListener('dragend', OnDragEnd, false);
        });

        function OnDragStart(e) {
            this.style.opacity = '0.3';
            srcElement = this;
            e.dataTransfer.effectAllowed = 'move';
            e.dataTransfer.setData('text/html', $(this).find("div")[0].innerHTML);
        }

        function OnDragOver(e) {
            if (e.preventDefault) {
                e.preventDefault();
            }
            $(this).addClass('highlight');
            e.dataTransfer.dropEffect = 'move';
            return false;
        }

        function OnDragEnter(e) {
            $(this).addClass('highlight');
        }

        function OnDragLeave(e) {
            $(this).removeClass('highlight');
        }

        function OnDrop(e) {
            if (e.stopPropagation) {
                e.stopPropagation();
            }

            srcElement.style.opacity = '1';
            $(this).removeClass('highlight');
            var count = $(this).find("div[data-player-name='" + e.dataTransfer.getData('text/html') + "']").length;
            if (count <= 0) {
                $(this).append("<div class='player' data-player-name='" + e.dataTransfer.getData('text/html') + "'>" + e.dataTransfer.getData('text/html') + "</div>");
                $(this).find('div').attr('draggable', 'true');
                $(this).find('div').bind('dragstart', OnDragStart);
            }
            else {
                
            }
            return false;
        }

        document.addEventListener("drop", function (event) {
            event.preventDefault();
            if (event.target.className == "droptarget") {
                document.getElementById("demo").style.color = "";
                event.target.style.border = "";
                var data = event.dataTransfer.getData("Text");
                event.target.appendChild(document.getElementById(data));
            }
        });

        function OnDragEnd(e) {
            $("div .bag").removeClass('highlight');
            this.style.opacity = '1';
        }
    })
</script>
    <h2><%: Title %></h2>
    <table class="nav-justified">
        <tr>
            <td rowspan="5" class="lineupColumn1">
                <h4>Stats</h4>
                <asp:ListBox ID="ListBox1" runat="server" Height="547px" Width="300px"></asp:ListBox>
            </td>
            <td class="lineupColumn1">
                <h4>Defenders</h4>
                <div class="teamSpots" id="defDiv">
                <asp:DataList ID="tbDefenders" runat="server" Width="300px" Height="120px">
                    <ItemTemplate>
                        <div class="player" draggable="true">
                            <header><%# Eval("First") %> <%# Eval("Last") %></header>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                </div>
            </td>
            <td class="lineupColumn1" rowspan="2">
                <h4>Bench</h4>
                <div class="teamSpots" id="benchDiv">
                <asp:DataList ID="tbBench" runat="server" Width="300px" Height="150px">
                    <ItemTemplate>
                        <div class="player" draggable="true">
                            <div class="playerName"><%# Eval("First") %> <%# Eval("Last") %></div>
                                <div class="playerPosition"><%# Eval("Position") %></div>
                            
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                </div>
            </td>
        </tr>
        <tr>
            <td class="lineupColumn1">
                <h4>Midfielders</h4>
                <div class="teamSpots" id="midDiv">
                <asp:DataList ID="tbMidfielders" runat="server" Width="300px" Height="120px">
                    <ItemTemplate>
                        <div class="player" draggable="true">   
                            <header><%# Eval("First") %> <%# Eval("Last") %></header>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                </div>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="lineupColumn1">
            <h4>Atackers</h4>
            <div class="teamSpots" id="strDiv">
            <asp:DataList ID="tbStrikers" runat="server" Width="300px" Height="60px">
                <ItemTemplate>
                    <div class="player" draggable="true">
                        <header><%# Eval("First") %> <%# Eval("Last") %></header>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            </div>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
            <h4>Goalie</h4>
            <asp:ListBox ID="lbGoalie" runat="server" Width="300px" Height="32px"></asp:ListBox></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    </asp:Content>
