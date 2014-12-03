﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WebApplication.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="ProfileContent" class="Content">
        <div id="ProfilePicture">
            <asp:Image ImageUrl="~/images/default_profile.jpg" ID="PPicture" Width="150" Height="150" runat="server" />
        </div>
        <div id="lblNamn">
            <asp:Literal ID="ltrUserName" runat="server" />
            <asp:Label ID="lblPNamn" runat="server" Text="Namn: "></asp:Label>
            <asp:Label ID="lblPNamn2" runat="server" Text=""></asp:Label>
        </div>
        <div id="lblAlder">
            <asp:Label ID="lblPAlder" runat="server" Text="Ålder: "></asp:Label>
            <asp:Label ID="lblPAlder2" runat="server" Text="Ålder här!"></asp:Label>
        </div>
        <div id="lblBor">
            <asp:Label ID="lblPBor" runat="server" Text="Bor: "></asp:Label>
            <asp:Label ID="lblPBor2" runat="server" Text="Bor här!" CssClass="auto-style2"></asp:Label>
        </div>
        <div id="lblKon">
            <asp:Label ID="lblPKon" runat="server" Text="Kön: "></asp:Label>
            <asp:Label ID="lblPKon2" runat="server" Text="Kön här!"></asp:Label>
        </div>
        <div id="lblPermium">
            <asp:Label ID="lblPPermium" runat="server" Text="Kontotyp: "></asp:Label>
            <asp:Label ID="lblPPermium2" runat="server" Text=""></asp:Label>
        </div>
        <div id="lblEmail">
            <asp:Label ID="lblPEmail" runat="server" Text="Email: "></asp:Label>
            <asp:Label ID="lblPEmail2" runat="server" Text="Email här!"></asp:Label>
        </div>
        <div id="lblSysselsattning">
            <asp:Label ID="lblPSysselsattning" runat="server" Text="Sysselsättning: "></asp:Label>
            <asp:Label ID="lblPSysselsattning2" runat="server" Text="Sysselsättning här!"></asp:Label>
        </div>
        <div id="lblOmmig">
            <asp:Label ID="lblPOmmig" runat="server" Text="Om mig: "></asp:Label>
            <asp:Label ID="lblPOmmig2" runat="server"></asp:Label>
        </div>
        <div id="PRedigera" class="auto-style4">
            <asp:Button ID="btnPRedigera" runat="server" Text="Redigera profil" Width="150px" OnClick="btnPRedigera_Click" />
            <br />
        </div>
        <div id="Wall">
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="WalltxtMeddelande" TextMode="MultiLine" placeholder="Skriv något..." Style="resize: none" runat="server" Width="316px" Height="58px"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="WallBtnSkicka" runat="server" Text="Skicka" Width="83px" OnClick="btnSaveWallMsg_Click" />
                    <br />
                    <br />
                    <asp:TextBox ID="WalltxtBox" runat="server" Height="290px" ReadOnly="true" TextMode="MultiLine" Style="resize: none" Width="370px" overflow="scroll" Text=""></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>