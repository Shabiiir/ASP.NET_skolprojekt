<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="WebApplication.EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Content">
        <br />
        <asp:Label ID="lblFNamn" runat="server" Text="Förnamn"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblENamn" runat="server" Text="Efternamn"></asp:Label>
        <br />
        <asp:TextBox ID="tbxFNamn" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="tbxEnamn" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LblEmail" runat="server" Text="Email"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblBirthday" runat="server" Text="Födelsedag"></asp:Label>
        <br />
        <asp:TextBox ID="tbxEmail" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="tbxBirthday" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblResidency" runat="server" Text="Land"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblOccupation" runat="server" Text="Sysselsättning"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownListCountry" runat="server" Width="160px" Height="25px">
            <asp:ListItem Selected="True">Neverland</asp:ListItem>
            <asp:ListItem>Sverige</asp:ListItem>
            <asp:ListItem>Norge</asp:ListItem>
            <asp:ListItem>Finland</asp:ListItem>
            <asp:ListItem>Danmark</asp:ListItem>
            <asp:ListItem>Amerika</asp:ListItem>
            <asp:ListItem>England</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:DropDownList ID="DropDownListRegEmploy" runat="server" Width="160px" Height="25px">
           <asp:ListItem Selected="True">Astronaut</asp:ListItem>
           <asp:ListItem>Jobbar</asp:ListItem>
           <asp:ListItem>Studerar</asp:ListItem>
           <asp:ListItem>Arbetslös</asp:ListItem>
           <asp:ListItem>Filosof</asp:ListItem>
           <asp:ListItem>Artist</asp:ListItem>
           <asp:ListItem>Stjärna</asp:ListItem>
       </asp:DropDownList>

        <br />

        <br />
        <asp:Label ID="lblAboutMe" runat="server" Text="Om mig"></asp:Label>
        <br />
        <asp:TextBox ID="tbxAboutMe" TextMode="MultiLine" runat="server" Height="131px" Width="253px"></asp:TextBox>

        <br />

        <br />
        <asp:Button ID="btnSave" runat="server" OnClick="btn_saveClick" Text="Spara min information" />

        <br />

        <br />
        <div id="EPPicture">
            <asp:Image ImageUrl="~/images/default_profile.jpg" ID="PPicture" Width="150" Height="150" runat="server" />
            <br />
            <br />
            <asp:Label ID="lblPicture" runat="server" Text="Byt bild"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:FileUpload ID="FileUpload1" runat="server" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSavePic" runat="server" OnClick="btn_SavePic_Click" Text="Spara bild" />
        </div>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblGammlalösen" runat="server" Text="Aktuellt lösenord"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
        <asp:Label ID="lblNewPassword2" runat="server" Text="Nytt lösenord"></asp:Label>
        &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblNewPassword1" runat="server" Text="Upprepa nytt lösenord"></asp:Label>
        <br />
        <asp:Label ID="lblPassword" runat="server" Text="Ändra lösenord"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="tbxOld" TextMode="Password" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="tbxNew1" TextMode="Password" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="tbxNew2" TextMode="Password" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSavePass" runat="server" OnClick="btnSavePass_Click" Text="Ändra" />
        <br />
    </div>
</asp:Content>
