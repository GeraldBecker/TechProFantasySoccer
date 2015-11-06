<%@ Page Title="Join A League " Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="JoinLeague.aspx.cs" Inherits="TechProFantasySoccer.JoinLeague" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <div class="row">
        <div class="col-md-8">
            <section id="mainPageDetails">
                <div class="form-horizontal">

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Label runat="server" AssociatedControlID="LeaguesJoinedListBox">Leagues Joined:</asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:ListBox ID="LeaguesJoinedListBox"
                                runat="server"
                                Rows="3"
                                Width="350px" >

                                <asp:ListItem>Example League</asp:ListItem>
                                <asp:ListItem>Test League</asp:ListItem>
                                <asp:ListItem>Germans Only League</asp:ListItem>

                            </asp:ListBox>
                        </div>
                    </div>

                    <br />

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Label runat="server" AssociatedControlID="LeaguesAvailListBox">Team:</asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:ListBox ID="LeaguesAvailListBox"
                                runat="server"
                                Rows="2"
                                Width="350px" >

                                <asp:ListItem>Brazil Fans League</asp:ListItem>
                                <asp:ListItem>Portugal League</asp:ListItem>

                            </asp:ListBox>
                        </div>
                    </div>

                    <br />

                    <div class="form-group">
                        <div class="col-md-offset-1 col-md-10">
                            <asp:Button id="NextBtn" runat="server" Height="36px" Width="148px" text="Next" BackColor="#ffcc00" />
                        </div>
                    </div>

                 </div>
            </section>
        </div>
    </div>
</asp:Content>
