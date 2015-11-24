<%@ Page Title="Edit Player Information" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="EditPlayerInfo.aspx.cs" Inherits="TechProFantasySoccer.EditPlayerInfo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <table id="table1">
        <tr>
            <td>
                <asp:TextBox id="playerImg" runat="server" Height="200px" Width="200px">PLACE HOLDER FOR IMAGE</asp:TextBox>
            </td>
            <td>
                <asp:TextBox id="PlayerFNameTextBox" runat="server"></asp:TextBox>
                <asp:TextBox id="PlayerLNameTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td>
                <strong>Club:</strong>
            </td>
            <td>
                <asp:DropDownList ID="ClubDropDown" runat="server" DataSourceID="ClubsDataSource" 
                        DataTextField="ClubName" DataValueField="ClubId"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <strong>League:</strong>
            </td>
            <td>
                <asp:Label ID="LeagueLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                Position:
            </th>
            <td>
                <asp:DropDownList ID="PositionDropDown" runat="server" >
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="1" Text="Striker"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Midfielder"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Defender"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Goalie"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><strong>Cost:</strong></td>
            <td>
                $<asp:TextBox id="costTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
                <asp:Button id="saveBtn" runat="server" text="SAVE" CssClass="btn btn-success btn-block" OnClick="editBtn_Click"/>
                <asp:Button id="editScoreSatsBtn" runat="server" text="Edit Scoring Stats Instead" CssClass="btn btn-default btn-block" />
            </td>
        </tr>
    </table>

    <asp:SqlDataSource ID="ClubsDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:FantasySoccerConnectionString %>" 
        SelectCommand="SELECT * FROM Clubs ORDER BY ClubName"
        UpdateCommand="UPDATE Players SET FirstName = @FirstName, LastName = @LastName, Cost = @Cost, PositionRef = @Position, ClubId = @Club WHERE PlayerId = @PlayerId">
        <UpdateParameters>
            <asp:Parameter Name="PlayerId" Type="Int32" />
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="Cost" Type="Int32" />
            <asp:Parameter Name="Position" Type="Int32" />
            <asp:Parameter Name="Club" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    
</asp:Content>
