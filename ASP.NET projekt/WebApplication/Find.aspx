<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Find.aspx.cs" Inherits="WebApplication.Find" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Content">
        <br />
        <br />
        <asp:TextBox ID="tbxFindSearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnFindSearch" runat="server" Text="Sök" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="ShowProfile" runat="server" CssClass="Links" postbackurl='<%#"~/ProfileByID.aspx?AnvID=" + Eval("Id") %>'>Visa profil</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Användarnamn" HeaderText="Användarnamn" SortExpression="Användarnamn" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnectionString %>" SelectCommand="SELECT [ID], [Användarnamn] FROM [Användare] WHERE ([Användarnamn] LIKE '%' + @Användarnamn + '%')">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbxFindSearch" Name="Användarnamn" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
