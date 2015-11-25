<%@ Page Title="Edit Scoring Values" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="True" 
    CodeBehind="EditScoringValues.aspx.cs" 
    Inherits="TechProFantasySoccer.Admin.EditScoringValues" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function fieldEdited(id) {
            document.getElementById(id).style.backgroundColor = '#83F52C';
        } 
    </script>
    <br />
    <asp:DataList runat="server" 
        DataKeyField="ScoringId" 
        DataSourceID="ScoringValuesDataSource" ID="DataList1"
        OnUpdateCommand="DataList1_UpdateCommand">
        <HeaderTemplate>
            <table style="border-collapse: collapse; max-width:99%;">
                <tr>
                    <th>ScoringId</th>
                    <th>Name</th>
                    <th>Value</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr> 
                <td>
                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("ScoringId") %>' Visible="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' Visible="true"></asp:Label>
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
                <td class="outstanding" colspan="16" style="text-align:center;">
                    <br />
                    <asp:Button id="UpdateButton" Text="Update All Fields" OnClick="UpdateButton_Click" runat="server"
                        style="background-color:#99ffa9; height:50px; width:200px; margin-right:20px;"/>
                </td>
            </tr>
            </table>
        </FooterTemplate>
    </asp:DataList>

    <asp:SqlDataSource ID="ScoringValuesDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:FantasySoccerConnectionString%>"
        SelectCommand="SELECT 
                        ScoringValues.ScoringId,
                        ScoringValues.Name,
                        ScoringValues.Value
                        FROM ScoringValues
                        ORDER BY Name"
        UpdateCommand="UPDATE ScoringValues 
                        SET Value = @ValueParam 
                        WHERE ScoringId = @ScoringIdParam">
        <UpdateParameters>
            <asp:Parameter Name="ValueParam" Type="Decimal" />
            <asp:Parameter Name="ScoringIdParam" Type="Int32" />
        </UpdateParameters>
            
    </asp:SqlDataSource>
</asp:Content>