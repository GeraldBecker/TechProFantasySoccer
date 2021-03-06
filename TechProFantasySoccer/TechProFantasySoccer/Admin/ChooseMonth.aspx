﻿<%@ Page Title="Edit Monthly Player Stats" Language="C#" AutoEventWireup="true" CodeBehind="ChooseMonth.aspx.cs" MasterPageFile="~/Site.Master" Inherits="TechProFantasySoccer.ChooseMonth" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Choose a month:</h3>

    <div class="row">
        <div class="col-md-8">
            <section id="mainPageDetails">
                <div class="form-horizontal">

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button id="sepBtn" runat="server" class="btn btn-default btn-lg btn-block" text="September" OnClick="MonthButton_Click"/>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button id="octBtn" runat="server" class="btn btn-default btn-lg btn-block" text="October" OnClick="MonthButton_Click"/>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button id="novBtn" runat="server" class="btn btn-default btn-lg btn-block" text="November" OnClick="MonthButton_Click"/>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button id="decBtn" runat="server" class="btn btn-default btn-lg btn-block" text="December" OnClick="MonthButton_Click"/>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button id="janBtn" runat="server" class="btn btn-default btn-lg btn-block" text="January" OnClick="MonthButton_Click"/>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button id="febBtn" runat="server" class="btn btn-default btn-lg btn-block" text="February" OnClick="MonthButton_Click"/>
                        </div>
                    </div>

                 </div>
            </section>
        </div>
    </div> 
    
</asp:Content>
