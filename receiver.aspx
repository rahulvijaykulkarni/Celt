<%@ Page Language="C#" AutoEventWireup="true" CodeFile="receiver.aspx.cs" Inherits="receiver" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <style>
        .row {
            margin: 0px;
        }
    </style>
      <script type="text/javascript">
          function R_val() {
              var txt_comment = document.getElementById('<%=TextBox1.ClientID %>');

              if (txt_comment.value == "") {
                  alert("Please Enter the next avaibility");
                  return false;
              }
              return true;
          }
          </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row text-center">
            <br />
            <div class="col-sm-2 col-xs-12"></div>
            <div id="div_accept" runat="server" class="col-sm-8 col-xs-12 alert alert-success">
                <asp:Label ID="Accept" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <div id="div_reject" runat="server" class="col-sm-8 col-xs-12 alert alert-danger">
                <asp:Label ID="Reject" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <div class="col-sm-2 col-xs-12"></div>


        </div>
        <br />
        <div class="row">
            <div class="col-sm-2 col-xs-12"></div>
            <div class="text-center col-sm-8 col-xs-12">
                <asp:TextBox ID="TextBox1" TextMode="multiline" CssClass="form-control" Columns="50" Rows="5" runat="server" />
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="Button1_Click" Visible="false" OnClientClick="return R_val();" />
                <br />
                <asp:LinkButton runat="server" ID="lnkapp" OnClick="lnkapp_Click">Go To Application</asp:LinkButton>
            </div>
            <div class="col-sm-2 col-xs-12">
            </div>
        </div>
    </form>
</body>
</html>
