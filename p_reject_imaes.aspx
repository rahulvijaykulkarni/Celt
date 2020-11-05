<%@ Page Language="C#" AutoEventWireup="true" CodeFile="p_reject_imaes.aspx.cs" Inherits="p_reject_imaes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.12.3.min.js"></script>
    <script src="Scripts/jquery-1.11.3.js"></script>
    <script src="js/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/datetimepicker.js"></script>
    <script src="Scripts/jquery-ui-1.8.20.min.js"></script>
    <script src="Scripts/jquery-ui-1.8.20.js"></script>
    <script src="Scripts/jquery-1.7.1.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.blockUI.js"></script>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <link href="Scripts/jquery-ui.css" rel="stylesheet" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>
    <link href="css/select2.min.css" rel="stylesheet" />
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <script>
        function Req_validation() {
            var txt_comment = document.getElementById('<%=txt_comment.ClientID %>');
            if (txt_comment.value == "") {
                alert("Please add Comment");
                txt_comment.focus();
                return false;
            }
        }
        function R_validation() {

            var r = confirm("Are you Sure You Want to Reject Record");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <br />
            <div class="row">
                <div class="col-md-3 col-xs-12"></div>
                <div class="col-md-6 col-xs-12">
                    <span>Comment :</span>
                    <asp:TextBox ID="txt_comment" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row text-center">
                <asp:Button ID="btn_reject" runat="server" CssClass="btn btn-primary" Text="Reject" OnClientClick="return R_validation();" OnClick="btn_reject_click" />

            </div>
        </div>
    </form>
</body>
</html>
