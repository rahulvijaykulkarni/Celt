<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login_Page.aspx.cs" Inherits="Login_Page" %>



<script src="Scripts/jquery-1.11.3.js"></script>
<script src="Scripts/bootstrap.js"></script>

<script src="Scripts/jquery-1.11.3.js"></script>
<script src="Scripts/bootstrap.js"></script>
<script src="Scripts/datetimepicker.js"></script>
<script src="Scripts/jquery-ui-1.8.20.min.js"></script>
<script src="Scripts/jquery-ui-1.8.20.js"></script>
<script src="Scripts/jquery-1.7.1.js"></script>
<script src="Scripts/jquery-ui.min.js"></script>
<script src="js/bootstrap.min.js"></script>
<script src="js/jquery.blockUI.js"></script>
<script src="js/hashfunction.js"></script>
<link href="Scripts/jquery-ui.css" rel="stylesheet" />

<link href="Content/bootstrap.css" rel="stylesheet" />
<link href="css/style.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
<meta charset="utf-8">

<!DOCTYPE html >

<html>
<head runat="server">
    <title>Welcome!!!</title>
    <script type="text/javascript">

        $(document).ready(function () {
            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });
        });

        $(document).ready(function () {
            document.getElementById('<%=txt_login_id.ClientID %>').focus();
             });

             function validate() {
                 var login = document.getElementById('<%=txt_login_id.ClientID %>');
                 var pass = document.getElementById('<%=txt_password.ClientID %>');

                 if (login.value == "") {
                     alert("Please Enter Login ID !!!");
                     pass.value = "";
                     login.focus();
                     return false;
                 }

                 if (pass.value == "") {
                     alert("Please Enter Password !!!");
                     pass.focus();
                     return false;
                 }
                 hash_text();
                 return true;

             }

             function validate1() {
                 var emailField = document.getElementById('<%=txt_emailid.ClientID %>');
                 var login = document.getElementById('<%=txt_login.ClientID %>');
                 var mobileno = document.getElementById('<%=txt_mobileno.ClientID %>');
                 var birthdaydate = document.getElementById('<%=txtbirthday.ClientID %>');


                 if (login.value == "") {
                     alert("Please Enter Login ID !!!");
                     login.focus();
                     return false;
                 }

                 if (emailField.value == "") {
                     alert("Please Enter Email ID !!!");
                     emailField.focus();
                     return false;
                 }

                 var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

                 if (reg.test(emailField.value) == false) {
                     alert('Invalid Email Address !!!');
                     emailField.focus();
                     return false;
                 }
                 if (mobileno.value == "") {
                     alert("Please Enter Mobile No !!!");
                     mobileno.focus();
                     return false;
                 }

                 if (birthdaydate.value == "") {
                     alert("Please Select Bitrhday Date !!!");
                     birthdaydate.focus();
                     return false;
                 }
                 return true;

             }
             // register onclick events for Encrypt button
             function hash_text() {
                 var txt_string = document.getElementById('<%=txt_password.ClientID %>').value;      // gets data from input text

                 // encrypts data and adds it in #strcrypt element
                 document.getElementById('<%=txt_password.ClientID %>').value = SHA256(txt_string);
                 return false;
             }
    </script>
    <style>
        {box-sizing: border-box;}

body { 
  margin: 0;
  font-family: Arial, Helvetica, sans-serif;
}

.header {
  overflow: hidden;
  background-color: #f1f1f1;
  padding: 20px 10px;
}

.header a {
  float: left;
  color: black;
  text-align: center;
  padding: 12px;
  text-decoration: none;
  font-size: 18px; 
  line-height: 25px;
  border-radius: 4px;
}

.header a.logo {
  font-size: 25px;
  font-weight: bold;
}

.header a:hover {
  background-color: #ddd;
  color: black;
}

.header a.active {
  background-color: blue;
  color: white;
}

.header-right {
  float: right;
}

@media screen and (max-width: 500px) {
  .header a {
    float: none;
    display: block;
    text-align: left;
  }
  
  .header-right {
    float: none;
  }
}
    </style>
</head>
<body>
    
    <form id="loginform" class="form" role="form" runat="server">
        <div class="container">
            <br />
            <img src="Images/logo.png" class="img-responsive logo" style="height: 150px;" />
        </div>
        <div class="container">
            <br />
            <br />
            <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12"></div>
            <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12">
                <div class="login_box">
                    <asp:TextBox ID="txt_login_id" runat="server" class="form-control" type="text" placeholder="Enter Login-Id"></asp:TextBox><br />
                    <asp:TextBox ID="txt_password" runat="server" class="form-control" type="password" placeholder="password"></asp:TextBox><br />
                    <asp:Button OnClientClick="return validate();" runat="server" type="button" class="btn btn-success btn_login" OnClick="btn_login_Click" ID="btn_login" Text="Login"></asp:Button>
                    <div class="text-right">
                        <br />
                        <a href="#" data-toggle="modal" data-target="#myModal">Forgot password?</a>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12"></div>
        </div>

        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="close" data-dismiss="modal">&times;</div>
                        <h4 class="modal-title">Forgot Password</h4>
                    </div>
                    <div class="modal-body">
                        Enter Login ID
                              <asp:TextBox ID="txt_login" runat="server" class="form-control" type="text" placeholder="Enter Login id"></asp:TextBox>
                    </div>
                    <div class="modal-body">
                        Enter Email id
                              <asp:TextBox ID="txt_emailid" runat="server" class="form-control" type="text" placeholder="Enter Email id"></asp:TextBox>
                    </div>
                    <div class="modal-body">
                        Enter Mobile No
                              <asp:TextBox ID="txt_mobileno" runat="server" class="form-control " type="text" placeholder="Enter Mobile No"></asp:TextBox>
                    </div>
                    <div class="modal-body">
                        Select Birthday Date :
                              <asp:TextBox ID="txtbirthday" runat="server" class="form-control  date-picker text_box" type="text" placeholder="Select Birthdays"></asp:TextBox>
                    </div>

                    <div class="modal-footer">
                        <div class="row text-right">
                            <asp:Button OnClientClick="return validate1();" runat="server" type="button" class="btn btn-success" OnClick="btn_email_Click" ID="btn_email" Text="OK"></asp:Button>
                            <asp:Button data-dismiss="modal" runat="server" type="button" class="btn btn-success" ID="Button1" Text="Close"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

