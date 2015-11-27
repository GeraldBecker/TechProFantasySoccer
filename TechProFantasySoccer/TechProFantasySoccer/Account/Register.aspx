<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TechProFantasySoccer.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="banner_reg">
        <h1 class="title_reg">REGISTER</h1>
    </div>

    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal center_content">
        <h4>Create a new account.</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="center_content">Email</asp:Label>

            <asp:TextBox runat="server" ID="Email" CssClass="form-control center_content" TextMode="Email" />

            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                CssClass="text-danger center_content" ErrorMessage="The email field is required." />
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Username" CssClass="center_content">Username</asp:Label>

            <asp:TextBox runat="server" ID="Username" CssClass="form-control center_content" TextMode="SingleLine" />

            <asp:RequiredFieldValidator runat="server" ControlToValidate="Username"
                CssClass="text-danger center_content" ErrorMessage="The username field is required." />

        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="center_content">Password</asp:Label>

            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control center_content" />

            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                CssClass="text-danger center_content" ErrorMessage="The password field is required." />
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="center_content">Confirm password</asp:Label>

            <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control center_content" />

            <br />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                CssClass="text-danger center_content" Display="Dynamic" ErrorMessage="The confirm password field is required." />

            <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                CssClass="text-danger center_content" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />

        </div>

        <div class="form-group" style="padding-top:15px;">
            <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
        </div>

    </div>
</asp:Content>
