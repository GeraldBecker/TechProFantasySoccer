<%@ Page Title="Edit Player Information" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="EditPlayerInfo.aspx.cs" Inherits="TechProFantasySoccer.EditPlayerInfo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="banner_cheer">
        <h1 class="title_cheer">EDIT PLAYER INFORMATION</h1>
    </div>

    <div class="center_content">

        <table id="table1" align="center">
            <tr>
                <td>
                    <%--<asp:FileUpload id="fileupload" runat="server" />
                    <asp:Button ID="imageUpBtn" runat="server" Text="Upload" OnClick="ImageUpload_Click" />
                    <br />
                    <asp:Image id="image" runat="server" />--%>
                    <strong>Name:</strong>
                </td>
                <td>
                    <asp:TextBox id="PlayerFNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:TextBox id="PlayerLNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                    <br />
                </td>
            </tr>
     <!----------------------------------------------------------------------------------------------->   
            <tr>
                <td>
                    <strong>Club:</strong>
                </td>
                <td>
                    <asp:DropDownList ID="ClubDropDown" runat="server" DataSourceID="ClubsDataSource" 
                            DataTextField="ClubName" DataValueField="ClubId" CssClass="form-control">
                    </asp:DropDownList>
                    <br />
                </td>
            </tr>
     <!----------------------------------------------------------------------------------------------->
            <tr>
                <td>
                    <strong>League:</strong>
                </td>
                <td>
                    <asp:Label ID="LeagueLabel" runat="server" CssClass="form-control" disabled=""></asp:Label>
                    <br />
                </td>
            </tr>
    <!----------------------------------------------------------------------------------------------->
            <tr>
                <td>
                    <strong>Position:</strong>
                </td>
                <td>
                    <asp:DropDownList ID="PositionDropDown" runat="server" CssClass="form-control">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="1" Text="Striker"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Midfielder"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Defender"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Goalie"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                </td>
            </tr>
    <!----------------------------------------------------------------------------------------------->
            <tr>
                <td><strong>Cost:</strong></td>
                <td>
                    <div class="input-group">
                        <span class="input-group-addon">$</span>
                        <asp:TextBox id="costTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>

        <br />

        <asp:Button id="saveBtn" runat="server" text="SAVE" CssClass="btn btn-success" OnClick="editBtn_Click"/>

    </div>

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
