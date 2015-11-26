<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TechProFantasySoccer.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="container">
        <div class="row">

            <section id="loginForm">

                <div class="center_content">

                    <h2>Welcome to Fantasy Soccer</h2>
                    <hr />

                      <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>

                    <div class="form-group">
                        <asp:TextBox runat="server" ID="Username" CssClass="form-control center_content" TextMode="SingleLine" placeholder="Username"/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Username"
                                CssClass="text-danger center_content" ErrorMessage="The username field is required." />
                    </div>

                    <div class="form-group">
                        <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control center_content" placeholder="Password"/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger center_content" ErrorMessage="The password field is required." />
                    </div>

                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10 center_content">
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="RememberMe" />
                                <asp:Label runat="server" AssociatedControlID="RememberMe">Remember me?</asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10 center_content">
                            <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-default" />
                        </div>
                    </div>


                <br/>
                <br/>

                <p>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled" CssClass="center_content">Register as a New User</asp:HyperLink>
                </p>

                <p>
                    <%-- Enable this once you have account confirmation enabled for password reset functionality
                    <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">Forgot your password?</asp:HyperLink>
                    --%>
                </p>
                </div>

            </section>
        </div>

    </div>
</asp:Content>
