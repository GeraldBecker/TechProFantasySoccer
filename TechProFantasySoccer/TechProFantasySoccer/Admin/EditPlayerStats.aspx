<%@ Page Title="Edit Player Stats" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="True" 
    CodeBehind="EditPlayerStats.aspx.cs" 
    Inherits="TechProFantasySoccer.Admin.EditPlayerStats" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="banner_cheer">
        <h1 class="title_cheer">EDIT PLAYER STATS</h1>
    </div>

    <div class="center_content">

        <script type="text/javascript">
            function fieldEdited(id) {
                document.getElementById(id).style.backgroundColor = '#ffb2b2';
            }
        
            function checkboxSelect()
            {
                var tab = document.getElementById("requests")
                var chkboxessingle = tab.getElementsByClassName("checkboxes");
                var num = chkboxessingle.length;
                var x = document.getElementById("MainContent_SelectSingle");
                for (var i = 0; i < num; i++) {
                    var value = document.getElementById("MainContent_DataList1_textRecurring_" + i).innerHTML.toString();
                    if (value.toString() == "Single") {
                        var box = document.getElementById("MainContent_DataList1_DeleteBox_" + i);
                        box.checked = x.checked;
                    }
                }
            }
        
        </script>

        <asp:DataList runat="server" 
            DataKeyField="StatId" 
            DataSourceID="SqlDataSource1" ID="DataList1"
            OnEditCommand="DataList1_EditCommand" 
            OnCancelCommand="DataList1_CancelCommand" 
            OnUpdateCommand="DataList1_UpdateCommand"
            OnDeleteCommand="DataList1_DeleteCommand"
            HorizontalAlign="Center">
            <HeaderTemplate>
                <table style="border-collapse: collapse; max-width:99%;">
                    <tr>
                        <th>Stat ID</th>
                        <th>Player ID</th>
                        <th>First</th>
                        <th>Last</th>
                        <th>Month</th>
                        <th>Goals</th>
                        <th>Shots</th>
                        <th>Assists</th>
                        <th>Min Played</th>
                        <th>Fouls</th>
                        <th>YC</th>
                        <th>RC</th>
                        <th>GA</th>
                        <th>Saves</th>
                        <th>CS</th>
                        <th>Del</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="StatIdLabel" runat="server" Text='<%# Eval("StatId") %>' Visible="true"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="RecordIDLabel" runat="server" Text='<%# Eval("PlayerId") %>' Visible="true"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="FirstName" runat="server" Text='<%# Eval("First") %>'>
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LastName" runat="server" Text='<%# Eval("Last") %>'>
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Month" runat="server" Text='<%# Eval("Month") %>'>
                        </asp:Label>
                    </td>  
                    <td>
                        <asp:TextBox ID="GoalsTextBox" runat="server" Width="40px"
                            Text='<%# Eval("Goals") %>' AutoComplete="off" 
                            OnChange="javascript:fieldEdited(this.id);">
                        </asp:TextBox>
                        <asp:Label ID="GoalsLabel" runat="server" 
                            Text='<%# Eval("Goals") %>' AutoComplete="off" style="display: none;">
                        </asp:Label>
                    </td>
                    <td >
                        <asp:TextBox ID="ShotsTextBox" runat="server" Width="40px"
                            Text='<%# Eval("Shots") %>' AutoComplete="off" 
                            OnChange="javascript:fieldEdited(this.id);">
                        </asp:TextBox>
                        <asp:Label ID="ShotsLabel" runat="server" 
                            Text='<%# Eval("Shots") %>' AutoComplete="off" style="display: none;">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="AssistsTextBox" runat="server" Width="40px"
                            Text='<%# Eval("Assists") %>' AutoComplete="off" 
                            OnChange="javascript:fieldEdited(this.id);">
                        </asp:TextBox>
                        <asp:Label ID="AssistsLabel" runat="server" 
                            Text='<%# Eval("Assists") %>' AutoComplete="off" style="display: none;">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="MinPlayedTextBox" runat="server" 
                            Text='<%# Eval("Min Played") %>' AutoComplete="off" Width="60px"
                            OnChange="javascript:fieldEdited(this.id);">
                        </asp:TextBox>
                        <asp:Label ID="MinPlayedLabel" runat="server" 
                            Text='<%# Eval("Min Played") %>' AutoComplete="off" style="display: none;">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="FoulsTextBox" runat="server" Width="40px"
                            Text='<%# Eval("Fouls") %>' AutoComplete="off" 
                            OnChange="javascript:fieldEdited(this.id);">
                        </asp:TextBox>
                        <asp:Label ID="FoulsLabel" runat="server"
                            Text='<%# Eval("Fouls") %>' AutoComplete="off" style="display: none;">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="YCTextBox" runat="server" Width="40px"
                            Text='<%# Eval("YC") %>' AutoComplete="off" 
                            OnChange="javascript:fieldEdited(this.id);">
                        </asp:TextBox>
                        <asp:Label ID="YCLabel" runat="server"
                            Text='<%# Eval("YC") %>' AutoComplete="off" style="display: none;">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="RCTextBox" runat="server" Width="40px"
                            Text='<%# Eval("RC") %>' AutoComplete="off" 
                            OnChange="javascript:fieldEdited(this.id);">
                        </asp:TextBox>
                        <asp:Label ID="RCLabel" runat="server" 
                            Text='<%# Eval("RC") %>' AutoComplete="off" 
                            OnChange="javascript:fieldEdited(this.id);" style="display: none;">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="GATextBox" runat="server" Width="40px"
                            Text='<%# Eval("GA") %>' AutoComplete="off" 
                            OnChange="javascript:fieldEdited(this.id);">
                        </asp:TextBox>
                        <asp:Label ID="GALabel" runat="server" 
                            Text='<%# Eval("GA") %>' AutoComplete="off" style="display: none;">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="SavesTextBox" runat="server" Width="40px"
                            Text='<%# Eval("Saves") %>' AutoComplete="off" 
                            OnChange="javascript:fieldEdited(this.id);">
                        </asp:TextBox>
                        <asp:Label ID="SavesLabel" runat="server" Width="40px"
                            Text='<%# Eval("Saves") %>' AutoComplete="off"  style="display: none;">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="CSTextBox" runat="server" Width="40px"
                            Text='<%# Eval("CS") %>' AutoComplete="off" 
                            OnChange="javascript:fieldEdited(this.id);">
                        </asp:TextBox>
                        <asp:Label ID="CSLabel" runat="server"
                            Text='<%# Eval("CS") %>' AutoComplete="off" style="display: none;">
                        </asp:Label>
                    </td>
                    <td style="text-align:center;">
                        <asp:CheckBox ID="DeleteBox" CssClass="checkboxes" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr>
                    <td class="outstanding" colspan="16" style="text-align:center;">
                        <br />
                        <asp:Button id="UpdateButton" Text="Update All Fields" OnClick="UpdateButton_Click" runat="server" CssClass="btn btn-success" Width="30%"/>
                        &nbsp&nbsp&nbsp
                        <asp:Button id="DeleteButton" Text="Delete Checked Fields" OnClick="DeleteButton_Click" runat="server" CssClass="btn btn-danger" Width="30%"/>
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:DataList>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:FantasySoccerConnectionString%>"
            SelectCommand="SELECT 
                            PlayerStats.StatId,
                            PlayerStats.PlayerId,
                            FirstName AS First,
                            LastName AS Last,
                            PlayerStats.Month,
                            ([PlayerStats].Goals) AS Goals,
                            ([PlayerStats].Shots) AS Shots,
                            ([PlayerStats].Assists) AS Assists,
                            ([PlayerStats].MinPlayed) AS 'Min Played',
                            ([PlayerStats].Fouls) AS Fouls,
                            ([PlayerStats].YellowCards) AS YC,
                            ([PlayerStats].RedCards) AS RC,
                            ([PlayerStats].GoalsAllowed) AS GA,
                            ([PlayerStats].SavesMade) AS Saves,
                            ([PlayerStats].CleanSheets) AS CS
                            FROM PlayerStats
                            LEFT OUTER JOIN Players ON PlayerStats.PlayerId = Players.PlayerId
                            WHERE Month = @Month
                            ORDER BY Month, LastName, FirstName
                            "
            UpdateCommand="UPDATE PlayerStats 
                            SET Goals = @GoalsParam, 
                                Shots = @ShotsParam,
                                Assists = @AssistsParam,
                                MinPlayed = @MinPlayedParam,
                                Fouls = @FoulsParam,
                                YellowCards = @YCParam,
                                RedCards = @RCParam,
                                GoalsAllowed = @GAParam,
                                SavesMade = @SavesParam,
                                CleanSheets = @CSParam
                            WHERE StatId = @StatIdParam"

            DeleteCommand=" DELETE FROM PlayerStats
                            WHERE StatId = @StatIdParam">
            <SelectParameters>
                <asp:Parameter Name="Month" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="GoalsParam" Type="Int32" />
                <asp:Parameter Name="ShotsParam" Type="Int32" />
                <asp:Parameter Name="AssistsParam" Type="Int32" />
                <asp:Parameter Name="MinPlayedParam" Type="Int32" />
                <asp:Parameter Name="FoulsParam" Type="Int32" />
                <asp:Parameter Name="YCParam" Type="Int32" />
                <asp:Parameter Name="RCParam" Type="Int32" />
                <asp:Parameter Name="GAParam" Type="Int32" />
                <asp:Parameter Name="SavesParam" Type="Int32" />
                <asp:Parameter Name="CSParam" Type="Int32" />
                <asp:Parameter Name="PlayerId" Type="Int32" />
                <asp:Parameter Name="StatIdParam" Type="Int32" />
                <asp:Parameter Name="Month" Type="Int32" />
            </UpdateParameters>
            <DeleteParameters>
                <asp:Parameter Name="StatIdParam" Type="Int32" />
            </DeleteParameters>
            
        </asp:SqlDataSource>
        <br />
        <asp:Label ID="GetPlayersLabel" runat="server"
            Text="Click the below button to create entries for players that have no data yet.">
        </asp:Label>
        <br />
        <br />
        <asp:Button id="GetPlayersButton" Text="Get Players" OnClick="GetPlayersButton_Click" 
            runat="server" CssClass="btn btn-default" Width="20%"/>
        <br />
        <asp:Label ID="GetPlayersResult" runat="server" Text='' ForeColor="Red">
        </asp:Label>

    </div>

</asp:Content>