<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Friends.aspx.cs" Inherits="WebApplication.Friends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Content">
        <br />
        <br />
        <asp:Label ID="lblFriends" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="lblFriendRequests" runat="server"></asp:Label>
    </div>
</asp:Content>
