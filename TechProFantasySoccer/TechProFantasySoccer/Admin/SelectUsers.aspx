<%@ Page Title="User Access Levels" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="SelectUsers.aspx.cs" 
    Inherits="TechProFantasySoccer.Admin.SelectUsers" %>

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

    <div class="banner_cheer">
        <h1 class="title_cheer">USER ACCESS LEVELS</h1>
    </div>

    <div class="center_content">

        <p>
            0 = No Access<br />1 = Admin User<br />2 = Fantasy User
        </p>

        <br />

        <asp:DataList runat="server" 
            DataKeyField="AccessId" 
            DataSourceID="SqlDataSource1" ID="DataList1"
            OnUpdateCommand="DataList1_UpdateCommand"
            HorizontalAlign="Center">
            <HeaderTemplate>
                <table style="border-collapse: collapse; max-width:99%;">
                    <tr>
                        <th>Access ID</th>
                        <th>Username</th>
                        <th>Email</th>
                        <th>Access Level</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="AccessIdLabel" runat="server" Text='<%# Eval("AccessId") %>'></asp:Label>
                        <asp:Label ID="UserIDLabel" runat="server" Text='<%# Eval("Id") %>' style="display: none;"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="UsernameLabel" runat="server" Text='<%# Eval("Username") %>'>
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>'>
                        </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="AccessTextBox" runat="server" Width="40px"
                            Text='<%# Eval("Access") %>' AutoComplete="off" 
                            OnChange="javascript:fieldEdited(this.id);">
                        </asp:TextBox>
                        <asp:Label ID="AccessLabel" runat="server" 
                            Text='<%# Eval("Access") %>' AutoComplete="off" style="display: none;">
                        </asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr>
                    <td class="outstanding" colspan="4" style="text-align:center;">
                        <br />
                        <asp:Button id="UpdateButton" Text="UPDATE" OnClick="UpdateButton_Click" runat="server" CssClass="btn btn-success"/>
                    
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:DataList>
    

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:FantasySoccerConnectionString %>"
            SelectCommand="SELECT   AccessId,
                                    Id,
		                            Username,
		                            Email,
		                            Access
                            FROM AspNetUsers
                            LEFT OUTER JOIN AccessLevel ON AspNetUsers.Id = AccessLevel.UserId"
            UpdateCommand="UPDATE AccessLevel 
                            SET Access = @AccessParam
                            WHERE AccessId = @AccessIdParam" 
            InsertCommand="INSERT INTO AccessLevel (UserId, Access)
                           VALUES (@UserIdParam, @AccessParam)">

        
            <SelectParameters>
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="AccessParam" Type="Int32" />
                <asp:Parameter Name="AccessIdParam" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="UserIdParam" Type="String" />
                <asp:Parameter Name="AccessParam" Type="Int32" />
            </InsertParameters>
            
        </asp:SqlDataSource>
        <br />
    
        <br />
    
        <br />
        <asp:Label ID="GetPlayersResult" runat="server" Text='' ForeColor="Red">
        </asp:Label>

    </div>
</asp:Content>