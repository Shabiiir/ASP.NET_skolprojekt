<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplication.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="HomeWelcome" class="Content">
        <br />
        <br />
        <asp:Label ID="lblHome1" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblUserName" ClientIDMode="Static" Visible="false" runat="server"></asp:Label>
        <br />
        <script type="text/javascript">

            $(document).ready(function () {

                var username = '<%:HttpContext.Current.User.Identity.Name%>';
                var route = 'http://localhost:50828/api/Message/GetMessages?user=' + username;

                jQuery.support.cors = true;
                $.ajax(
                    {
                        url: route,
                        type: 'GET',
                        dataType: 'json',
                        success: function (data) {
                            $('#getLast5').append("Dina " + data.length + " senaste meddelanden!"+"<br />");

                            for (var i = 0; i < data.length; i++) {
                                var content = data[i].Content;
                                $('#getLast5').append("<a class='Links' href = 'Message.aspx'> Meddelande: " + data[i].Meddelande + "</a> <br />");
                            }
                        },
                        error: function (error) {
                            alert(error);
                        }
                    });
            });
        </script>
        <br />
        <asp:Label ID="lblHome2" runat="server" Text="Andra inloggade singlar:"></asp:Label>
        <br />
        <div id="HomePics">
            <asp:HyperLink NavigateUrl="#" runat="server" ID="Image1Src">
                <asp:Image ImageUrl="~/images/default_profile.jpg" ID="Image1" Width="150" Height="150" runat="server" />
            </asp:HyperLink>
            &nbsp;&nbsp;
            <asp:HyperLink NavigateUrl="#" runat="server" ID="Image2Src">
                <asp:Image ImageUrl="~/images/default_profile.jpg" ID="Image2" Width="150" Height="150" runat="server" />
            </asp:HyperLink>
            &nbsp;&nbsp;
            <asp:HyperLink NavigateUrl="#" runat="server" ID="Image3Src">
                <asp:Image ImageUrl="~/images/default_profile.jpg" ID="Image3" Width="150" Height="150" runat="server" />
            </asp:HyperLink>
        </div>
        <br />
        <br />
        <asp:Label ID="getLast5" runat="server" Text="" ClientIDMode="Static"></asp:Label>
    </div>
</asp:Content>
