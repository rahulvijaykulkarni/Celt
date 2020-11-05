<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Change_password.aspx.cs" Inherits="Change_password" Title="Change password" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="cph_title"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_header" runat="Server">

    <script src="Scripts/jquery-1.11.3.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <script src="js/hashfunction.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta charset="utf-8">
    <script>

        $(function () {

            $('#<%=Button1.ClientID%>').click(function () {
                 $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

             });

         });
    </script>
    <script type="text/javascript">
        function Validate() {
            var password = document.getElementById('<%=txt_pass.ClientID%>');
            var oldpassword = document.getElementById('<%=txt_oldpass.ClientID%>');
            var confirmPassword = document.getElementById('<%=txt_pass1.ClientID%>');
            if (password.value != confirmPassword.value) {
                alert("New Password & Confirm New Password do not match.");
                return false;
            }
            if (password.value == oldpassword.value) {
                alert("Old Password & New Password cannot be same.");
                return false;
            }
            if (oldpassword.value.length == 0) {
                alert("Please enter Old Password.");
                oldpassword.focus();
                return false;
            }
            if (password.value.length <= 8 || confirmPassword.value.length <= 8) {
                alert("Please enter new password more than 8 bytes.");
                password.focus();
                return false;
            }
            hash_text();
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        window.onfocus = function () {
            $.unblockUI();
        }
        function hash_text() {

            var txt_string1 = document.getElementById('<%=txt_oldpass.ClientID %>').value;      // gets data from input text
            // encrypts data and adds it in #strcrypt element
            document.getElementById('<%=txt_oldpass.ClientID %>').value = SHA256(txt_string1);

            var txt_string = document.getElementById('<%=txt_pass1.ClientID %>').value;      // gets data from input text

            // encrypts data and adds it in #strcrypt element
            document.getElementById('<%=txt_pass1.ClientID %>').value = SHA256(txt_string);

            var txt_string_pass = document.getElementById('<%=txt_pass.ClientID %>').value;      // gets data from input text

            // encrypts data and adds it in #strcrypt element
            document.getElementById('<%=txt_pass.ClientID %>').value = SHA256(txt_string_pass);

            return false;
        }

        function Clear() {
            document.getElementById('<%=txt_pass.ClientID%>').value = "";
            document.getElementById('<%=txt_oldpass.ClientID%>').value = "";
            document.getElementById('<%=txt_pass1.ClientID%>').value = "";
        }
        function openWindow() {
            window.open("html/Change_password.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

    </script>

    <style>
        .container {
            max-width: 99%;
        }

        .label_text {
            font-size: 14px;
        }

        .auto-style1 {
            color: #FFFFFF;
        }

        h2 {
            border-radius: 5px;
        }

        .grid-view {
            height: auto;
            max-height: 300px;
            overflow: scroll;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <div class="container-fluid">

        <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase" font-size="25px"><b>CHANGE PASSWORD</b></div>
                    </div>
                    <div class="col-sm-2 text-right">

                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
            <div class="panel-heading">

                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Change Password Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-bottom:20px; margin-top:20px">
                    <div class="row">
                        <div class="col-sm-2"></div>
                        <div class="col-sm-3" style="text-align: right;">
                            <br />
                             <b>Old Password :</b> 
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_oldpass" runat="server" Font-Bold="True" TextMode="Password" CssClass="form-control text_box"></asp:TextBox>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2"></div>
                        <div class="col-sm-3" style="text-align: right;">
                            <br />
                           <b> New Password :</b>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_pass" runat="server" Font-Bold="True" TextMode="Password" CssClass="form-control text_box"></asp:TextBox>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <div class="col-sm-4" style="text-align: right;">
                            <br />
                            <b> Confirm New Password :</b>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_pass1" runat="server" Font-Bold="True" TextMode="Password" CssClass="form-control text_box"></asp:TextBox>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    </div>
                    <br />
                    <div class="row text-center">

                        <asp:Button ID="Button1" runat="server" Text="Save" OnClick="btn_login_Click" OnClientClick="return Validate();" meta:resourcekey="btn_addResource1" class="btn btn-primary" BackColor="#428BCA" />

                        <asp:Button ID="Button3" runat="server" Text="Clear" OnClientClick="Clear();" class="btn btn-primary" BackColor="#428BCA" />

                        <asp:Button ID="Button4" runat="server" class="btn btn-danger" OnClick="close" Text="Close" />

                    </div>
                <br />

                
            </div>

        </asp:Panel>
    </div>
</asp:Content>

