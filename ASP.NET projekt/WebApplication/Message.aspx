<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="WebApplication.Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="MessageContent" class="Content">

        <br />
        <br />
        <asp:Label ID="lblMessagesName" runat="server"></asp:Label>
        <br />
        <br />
    </div>
</asp:Content>
