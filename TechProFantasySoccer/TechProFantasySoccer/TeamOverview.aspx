<%@ Page Title="Team Overview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeamOverview.aspx.cs" 
    Inherits="TechProFantasySoccer.TeamOverview" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Available Cap Space: <span style="font-size:16px;">$50,000</span></h3>
    
    <h4>Players:  (YTD Stats)</h4>
    <asp:GridView ID="GridView1" runat="server">
        <Columns>
            <asp:DynamicField DataField="Name" />
            <asp:DynamicField DataField="Cost" />
            <asp:DynamicField DataField="Position" />
            <asp:DynamicField DataField="Goals" />
            <asp:DynamicField DataField="Shots" />
            <asp:DynamicField DataField="Assists" />
            <asp:DynamicField DataField="Minutes Played" />
            <asp:DynamicField DataField="Fouls" />
            <asp:DynamicField DataField="YC" />
            <asp:DynamicField DataField="RC" />
            <asp:TemplateField HeaderText="Total Credits">
            <ItemTemplate>
                <asp:Label Text="haha"
                runat="server" />
            </ItemTemplate>
            </asp:TemplateField>        
        </Columns>

    </asp:GridView>
    

</asp:Content>