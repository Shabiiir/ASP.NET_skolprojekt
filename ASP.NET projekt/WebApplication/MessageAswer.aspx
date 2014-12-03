<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MessageAswer.aspx.cs" Inherits="WebApplication.MessageAswer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="MessageContent" class="Content">
        <br />
        <br />
        <br />
        <asp:Label ID="lblMessageAnswer" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="tbxMessageAnswer" runat="server" Height="150px" TextMode="MultiLine" CssClass="auto-style2" Width="300px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnMessageAnswer" runat="server" Height="24px" Text="Svara" Width="77px" OnClick="btnMessageAnswer_Click" />
    </div>
</asp:Content>
