<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="WebApplication.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="SignUpContent" class="Content">
        <table class="auto-style1">
            <tr>
                <td class="auto-style10">
                    <p>Användarnamn</p>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="txtRegUsername" placeholder="Blond_18" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="auto-style2">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRegUsername" ErrorMessage="Ange ett användarnamn" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="UserExists" ForeColor="#800000" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">
                    <p>Lösenord</p>
                </td>
                <td class="auto-style7">
                    <asp:TextBox ID="txtRegPassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
                </td>
                <td class="auto-style8">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRegPassword" ErrorMessage="Ange ett lösenord" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">
                    <p>Upprepa lösenord</p>
                </td>
                <td class="auto-style7">
                    <asp:TextBox ID="txtRegRetypePass" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
                </td>
                <td class="auto-style8">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRegRetypePass" ErrorMessage="Upprepa lösenord" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtRegPassword" ControlToValidate="txtRegRetypePass" ErrorMessage="Ditt lösenord matchar inte" ForeColor="Red"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">
                    <p>Email address</p>
                </td>
                <td class="auto-style7">
                    <asp:TextBox ID="txtRegEmail" placeholder="Something@Somewhere.com" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="auto-style8">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRegEmail" ErrorMessage="Ange din Email adress" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRegEmail" ErrorMessage="Du har inte angett en giltig Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">
                    <p>Förnamn</p>
                </td>
                <td class="auto-style7">
                    <asp:TextBox ID="txtRegFirstname" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="auto-style8">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRegFirstname" ErrorMessage="Ange ditt förnamn" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtRegFirstname" ErrorMessage="Endast bokstäver" ForeColor="Red" ValidationExpression="^[^\W\d_]+$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">
                    <p>Efternamn</p>
                </td>
                <td class="auto-style7">
                    <asp:TextBox ID="txtRegLastname" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="auto-style8">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRegLastname" ErrorMessage="Ange ditt efternamn" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtRegLastname" ErrorMessage="Endast bokstäver" ForeColor="Red" ValidationExpression="^[^\W\d_]+$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">
                    <p>Kön</p>
                </td>
                <td class="auto-style7">
                    <asp:RadioButton ID="RadioRegMale" runat="server" GroupName="GenderGroup" Text="Man" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="RadioRegFemale" runat="server" GroupName="GenderGroup" Text="Kvinna" />
                </td>
                <td class="auto-style8"></td>
            </tr>
            <tr>
                <td class="auto-style10">
                    <p>Födelsedag</p>
                </td>
                <td class="auto-style7">
                    <asp:TextBox ID="txtRegAge" placeholder="19900101" TextMode="DateTime" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="auto-style8">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtRegAge" ErrorMessage="Ange din födelsedag" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">
                    <p>Land</p>
                </td>
                <td class="auto-style7">
                    <asp:DropDownList ID="DropDownListCountry" runat="server" Width="200px">
                        <asp:ListItem Selected="True">Välj land</asp:ListItem>
                        <asp:ListItem>Sverige</asp:ListItem>
                        <asp:ListItem>Norge</asp:ListItem>
                        <asp:ListItem>Finland</asp:ListItem>
                        <asp:ListItem>Danmark</asp:ListItem>
                        <asp:ListItem>Amerika</asp:ListItem>
                        <asp:ListItem>England</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style8">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="DropDownListCountry" ErrorMessage="Välj ditt land" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">
                    <p>Sysselsättning</p>
                </td>
                <td class="auto-style7">
                    <asp:DropDownList ID="DropDownListRegEmploy" runat="server" Width="200px">
                        <asp:ListItem Selected="True">Välj något</asp:ListItem>
                        <asp:ListItem>Jobbar</asp:ListItem>
                        <asp:ListItem>Studerar</asp:ListItem>
                        <asp:ListItem>Arbetslös</asp:ListItem>
                        <asp:ListItem>Filosof</asp:ListItem>
                        <asp:ListItem>Artist</asp:ListItem>
                        <asp:ListItem>Astronaut</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style8">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="DropDownListRegEmploy" ErrorMessage="Vad är din sysselsättning" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">
                    <p>Bild</p>
                </td>
                <td class="auto-style7">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" />
                </td>
                <td class="auto-style8">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="FileUpload1" ErrorMessage="Ladda upp en bild" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">Premium</td>
                <td class="auto-style7">
                    <asp:RadioButton ID="RadioRegYes" runat="server" GroupName="Premium" Text="Ja tack" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="RadioRegNo" runat="server" GroupName="Premium" Text="Nej tack" />
                </td>
                <td class="auto-style8"></td>
            </tr>
            <tr>
                <td class="auto-style11">Om mig</td>
                <td class="auto-style12">
                    <asp:TextBox ID="txtAboutMe" runat="server" Height="63px" MaxLength="50" TextMode="MultiLine" Width="216px" ClientIDMode="Static"></asp:TextBox></td>
                <td>
                    <div id="countDisplay" style="width: 60px; left: 150px"></div>
                </td>
            </tr>
            <tr>
                <td class="auto-style10"></td>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10">&nbsp;</td>
                <td class="auto-style7">
                    <asp:Button ID="SubmitReg" runat="server" OnClick="SubmitReg_Click" Text="Registrera" />
                </td>
                <td class="auto-style8">&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>