<%@ Page Title="Denied Access" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" 
    CodeBehind="AccessDenied.aspx.cs" Inherits="TechProFantasySoccer.AccessDenied" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Fantasy Soccer</h2>
    <p>
        Oops! It appears that you have tried to access a page that you do not currently have access to. <br />

        <br />If you have just registered, be sure to contact the league administrator and mention that you have created your account.

        <br /><br />Your username is: &nbsp; <%=UserName %>
    </p>
</asp:Content>