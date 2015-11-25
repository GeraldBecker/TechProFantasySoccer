<%@ Page Title="Edit Player Stats" Language="C#" MasterPageFile="~/Site.Master"   AutoEventWireup="true" CodeBehind="Settings.aspx.cs" 
    Inherits="TechProFantasySoccer.Admin.ToggleTransactions" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
    <h2>Fantasy Settings</h2>
    <br />
    
    <asp:DataList runat="server" 
        DataKeyField="KeyId" 
        DataSourceID="SqlDataSource1" ID="DataList1">
        <HeaderTemplate>
            <table style="border-collapse: collapse; max-width:99%;">
                <tr>
                    <th>Key ID</th>
                    <th>Description</th>
                    <th>Value</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="KeyIdLabel" runat="server" Text='<%# Eval("KeyId") %>'></asp:Label>
                </td>
                <td>
                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>'>
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ValueTextBox" runat="server" Width="40px"
                        Text='<%# Eval("Value") %>' AutoComplete="off" 
                        OnChange="javascript:fieldEdited(this.id);">
                    </asp:TextBox>
                    <asp:Label ID="ValueLabel" runat="server" 
                        Text='<%# Eval("Value") %>' AutoComplete="off" style="display: none;">
                    </asp:Label>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td class="outstanding" colspan="3" style="text-align:center;">
                    <br />
                    <asp:Button id="UpdateButton" Text="Update All Fields" OnClick="UpdateButton_Click" runat="server"
                        CssClass="btn btn-success btn-block"/>
                    
                </td>
            </tr>
            </table>
        </FooterTemplate>
    </asp:DataList>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:FantasySoccerConnectionString %>"
        SelectCommand="SELECT   KeyId,
                                Description,
		                        Value
                        FROM Settings"
        UpdateCommand="UPDATE Settings 
                        SET Value = @ValueParam
                        WHERE KeyId = @KeyIdParam">

        
        <SelectParameters>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ValueParam" Type="Int32" />
            <asp:Parameter Name="KeyIdParam" Type="Int32" />
        </UpdateParameters>
            
    </asp:SqlDataSource>
    <br />
    
    <br />
    
    <br />
    <asp:Label ID="GetPlayersResult" runat="server" Text='' ForeColor="Red">
    </asp:Label>
</asp:Content>