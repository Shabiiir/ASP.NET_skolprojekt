<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="WebApplication.WebForm1" Culture="sv-SE" UICulture="sv-SE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="SignInContent" class="Content">
        <div id="SignInHeader1lbl">
            <br />
            <asp:Label ID="Header1lbl" meta:resourcekey="Header1lbl" runat="server" Text="Label"></asp:Label>
        </div>
        <br />
        <br />
        <div id="SignInHeader2lbl">
            <asp:Label ID="Header2lbl" meta:resourcekey="Header2lbl" runat="server" Text="Label"></asp:Label>
        </div>
        <br />
        <br />
        <br />
        <asp:Label ID="Usernamelbl" meta:resourcekey="Usernamelbl" runat="server" Text="Label"></asp:Label>
        &nbsp;<br />
        <asp:TextBox ID="UserNametxb" runat="server" Width="135px" />
        <br />
        <br />
        <asp:Label ID="Passwordlbl" runat="server" meta:resourcekey="Passwordlbl" Text="Label"></asp:Label>
        &nbsp;<br />
        <asp:TextBox ID="Passwordtxb" TextMode="password" runat="server" Width="135px" />
        <br />
        <br />
        <asp:Button runat="server" ID="Loginbtn" meta:resourcekey="Loginbtn" OnClick="Loginbtn_Click" />
        &nbsp;<asp:Button runat="server" ID="regbtn" meta:resourcekey="regbtn" OnClick="regbtn_Click" />
        <br />
        <br />
        <asp:Label ID="lblWrongPass" ForeColor="#cc0000" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblSingels" runat="server"></asp:Label>
        <br />
        <br />
        <div id="SigninPic1">
            <asp:HyperLink NavigateUrl="#" runat="server" ID="Image1Src">
                <asp:Image ImageUrl="~/images/default_profile.jpg" ID="Image1" Width="150" Height="150" runat="server" />
            </asp:HyperLink>
            &nbsp;&nbsp;
            <asp:HyperLink NavigateUrl="#" runat="server" ID="Image2Src">
                <asp:Image ImageUrl="~/images/default_profile.jpg" ID="Image2" Width="150" Height="150" runat="server" />
            </asp:HyperLink>
            &nbsp;&nbsp;
            <asp:HyperLink NavigateUrl="#" runat="server" href="#" ID="Image3Src">
                <asp:Image ImageUrl="~/images/default_profile.jpg" ID="Image3" Width="150" Height="150" runat="server" />
            </asp:HyperLink>
        </div>
        <br />
        <br />
    </div>
</asp:Content>
