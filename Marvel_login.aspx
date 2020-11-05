<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Marvel_login.aspx.cs" Inherits="Marvel_login" %>

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
<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>

<link href="Content/bootstrap.css" rel="stylesheet" />
<%--<link href="css/style.css" rel="stylesheet" />--%>
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
<meta charset="utf-8">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css" />
 


	<!DOCTYPE html >

<html>
<head id="Head1" runat="server">
    <title>Welcome!!!</title>
    <script type="text/javascript">
        $(document).on('click', '.toggle-password', function () {
            $(this).toggleClass("fa-eye fa-eye-slash");
            var input = $("[id*=txt_password]");
            input.attr('type') === 'password' ? input.attr('type', 'text') : input.attr('type', 'password')
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
             $(document).ready(function () {
                 $('.toggle').on('click', function () {
                     $('.container').stop().addClass('active');
                 });
                 $('.close').on('click', function () {
                     $('.container').stop().removeClass('active');
                 });

             });
    </script>
    <style>
        .btn1 {
            display: inline-block;
    padding: 10px 10px 10px 10px;
    margin-bottom: 0;
    font-size: 14px;
    font-weight: normal;
    line-height: 1.428571429;
    text-align: center;
    white-space: nowrap;
    vertical-align: middle;
    cursor: pointer;
    border: 1px solid transparent;
    border-radius: 4px;
    -webkit-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    -o-user-select: none;
    user-select: none;
    /*margin-left:190px;*/
    /*width:325px;*/
}
        body {
  background: #e9e9e9;
  color: #666666;
  font-family: 'RobotoDraft', 'Roboto', sans-serif;
  font-size: 14px;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

/* Pen Title */
.pen-title {
  padding: 20px 0;
  text-align: center;
  letter-spacing: 2px;
}
.pen-title h1 {
  margin: 0 0 20px;
  font-size: 40px;
  font-weight: 300;
}
.pen-title span {
  font-size: 12px;
}
.pen-title span .fa {
  color: #ed2553; 
}
.pen-title span a {
  color: #ed2553; 
  font-weight: 600;
  text-decoration: none;
}

/* Rerun */
.rerun {
  margin: 0 0 30px;
  text-align: center;
}
.rerun a {
  cursor: pointer;
  display: inline-block;
  /*background: #ed2553; vikas comment 28/09*/
  border-radius: 3px;
  /*box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24); vikas comment 28/09*/
  padding: 10px 20px;
  color: #ffffff;
  text-decoration: none;
  -webkit-transition: 0.3s ease;
  transition: 0.3s ease;
}
.rerun a:hover {
  /*box-shadow: 0 3px 6px rgba(0, 0, 0, 0.16), 0 3px 6px rgba(0, 0, 0, 0.23); vikas comment 28/09*/
}

/* Scroll To Bottom */
#codepen, #portfolio {
  position: fixed;
  bottom: 30px;
  right: 30px;
  /*background: #ec2652; vikas comment 28/09*/
  width: 56px;
  height: 56px;
  border-radius: 100%;
  /*box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24); vikas comment 28/09*/
  -webkit-transition: 0.3s ease;
  transition: 0.3s ease;
  color: #ffffff;
  text-align: center;
}
#codepen i, #portfolio i {
  line-height: 56px;
}
#codepen:hover, #portfolio:hover {
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.19), 0 6px 6px rgba(0, 0, 0, 0.23);
}

/* CodePen */
#portfolio {
  bottom: 96px;
  right: 36px;
  /*background: #ec2652;vikas comment 28/09*/
  width: 44px;
  height: 44px;
  -webkit-animation: buttonFadeInUp 1s ease;
  animation: buttonFadeInUp 1s ease;
}
#portfolio i {
  line-height: 44px;
}

/* Container */
/*.container {
  position: relative;
  max-width: 460px;
  width: 100%;
  margin: 0 auto 100px;
}*/
.container.active .card:first-child {
  background: #f2f2f2;
  margin: 0 15px;
}
.container.active .card:nth-child(2) {
  background: #fafafa;
  margin: 0 10px;
}
.container.active .card.alt {
  top: 20px;
  right: 0;
  width: 100%;
  min-width: 100%;
  height: auto;
  border-radius: 5px;
  padding: 60px 0 40px;
  overflow: hidden;
}
.container.active .card.alt .toggle {
  position: absolute;
  top: 40px;
  right: -70px;
  box-shadow: none;
  -webkit-transform: scale(14);
  transform: scale(15);
  -webkit-transition: -webkit-transform .5s ease;
  transition: -webkit-transform .5s ease;
  transition: transform .5s ease;
  transition: transform .5s ease, -webkit-transform .5s ease;
}
.container.active .card.alt .toggle:before {
  content: '';
}
/*.container.active .card.alt .title,
.container.active .card.alt .input-container, vikas comment 28/09/2019*/
.container.active .card.alt .button-container {
  left: 0;
  opacity: 1;
  visibility: visible;
  -webkit-transition: .3s ease;
  transition: .3s ease;
}
.container.active .card.alt .title {
  -webkit-transition-delay: .3s;
          transition-delay: .3s;
}
.container.active .card.alt .input-container {
  -webkit-transition-delay: .4s;
          transition-delay: .4s;
}
.container.active .card.alt .input-container:nth-child(2) {
  -webkit-transition-delay: .5s;
          transition-delay: .5s;
}
.container.active .card.alt .input-container:nth-child(3) {
  -webkit-transition-delay: .6s;
          transition-delay: .6s;
}
.container.active .card.alt .button-container {
  -webkit-transition-delay: .7s;
          transition-delay: .7s;
}

/* Card */
.card {
  /*position: fixed;*/
  background: #ffffff;
  /*border-radius: 5px;*/
 padding:45px 0px 20px 0px;
  /*box-sizing: border-box;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);*/
  /*-webkit-transition: .3s ease;
  transition: .3s ease;*/
  
  margin-top:80px;
  /*margin-top:-140px;*/
  /* Title */
  /* Inputs */
  /* Button */
  /* Footer */
  /* Alt Card */
}
.card:first-child {
  background: #fafafa;
  border-radius: 5px 5px 0 0;
  
  padding: 0;
}
.card .title {
  position: relative;
  z-index: 1;
  /*border-left: 5px solid #ec2652; vikas comment 28/09*/
  margin: 0 0 35px;
  padding: 10px 0 10px 50px;
  /*color:#ec2652; vikas comment 28/09*/
  font-size: 32px;
  font-weight: 600;
  text-transform: uppercase;
}
.card .input-container {
  position: relative;
  margin: 0 60px 50px;
}
.card .input-container input {
  outline: none;
  z-index:0;
  position: relative;
  background: none;
  width: 100%;
  height: 54px;
  border: 0;
  /*color: #0e0d0d;*/
  font-size: 20px;
  font-weight: 400;
}
.card .input-container input:focus ~ label {
  color: #9d9d9d;
  -webkit-transform: translate(-12%, -50%) scale(0.75);
          transform: translate(-12%, -50%) scale(0.75);
}
.card .input-container input:focus ~ .bar:before, .card .input-container input:focus ~ .bar:after {
  width: 50%;
}
.card .input-container input:valid ~ label {
  color: #9d9d9d;
  -webkit-transform: translate(-12%, -50%) scale(0.75);
          transform: translate(-12%, -50%) scale(0.75);
}
.card .input-container label {
  position: absolute;
  top: 0;
  left: 0;
  color: #757575;
  font-size: 24px;
  font-weight: 300;
  line-height: 60px;
  -webkit-transition: 0.2s ease;
  transition: 0.2s ease;
}
.card .input-container .bar {
  position: absolute;
  left: 0;
  bottom: 0;
  background: #757575;
  width: 100%;
  height: 1px;
}
.card .input-container .bar:before, .card .input-container .bar:after {
  content: '';
  position: absolute;
  /*background: #ec2652; vikas comment 28/09*/
  width: 0;
  height: 2px;
  -webkit-transition: .2s ease;
  transition: .2s ease;
}
.card .input-container .bar:before {
  left: 50%;
}
.card .input-container .bar:after {
  right: 50%;
}
.card .button-container {
  margin: 0 60px;
  text-align: center;
}

/*.card .button-container button {
  outline: 0;
  cursor: pointer;
  position: relative;
  display: inline-block;
  background: 0;
  width: 240px;
  border: 2px solid #e3e3e3;
  padding: 20px 0;
  font-size: 24px;
  font-weight: 600;
  line-height: 1;
  text-transform: uppercase;
  overflow: hidden;
  -webkit-transition: .3s ease;
  transition: .3s ease;
}
.card .button-container button span {
  position: relative;
  z-index: 1;
  color: #ddd;
  -webkit-transition: .3s ease;
  transition: .3s ease;
}*/
/*.card .button-container button:before {
  content: '';
  position: absolute;
  top: 50%;
  left: 50%;
  display: block;
  background: #ec2652;
  width: 30px;
  height: 30px;
  border-radius: 100%;
  margin: -15px 0 0 -15px;
  opacity: 0;
  -webkit-transition: .3s ease;
  transition: .3s ease;
}*/
/*.card .button-container button:hover, .card .button-container button:active, .card .button-container button:focus {
  border-color: #ec2652;
}
.card .button-container button:hover span, .card .button-container button:active span, .card .button-container button:focus span {
  color: #ec2652;
}
.card .button-container button:active span, .card .button-container button:focus span {
  color: #ffffff;
}
.card .button-container button:active:before, .card .button-container button:focus:before {
  opacity: 1;
  -webkit-transform: scale(10);
  transform: scale(10);
}*/
/*.card .footer {
  margin: 40px 0 0;
  color: #d3d3d3;
  font-size: 24px;
  font-weight: 300;
  text-align: center;
}
.card .footer a {
  color: inherit;
  text-decoration: none;
  -webkit-transition: .3s ease;
  transition: .3s ease;
}
.card .footer a:hover {
  color: #bababa;
}*/
/* Keyframes */
@-webkit-keyframes buttonFadeInUp {
  0% {
    bottom: 30px;
    opacity: 0;
  }
}
/*@keyframes buttonFadeInUp {
  0% {
    bottom: 30px;
    opacity: 0;
  }
} vikas comment 28/09*/
    </style>
</head>
<body>
    <form id="loginform" class="form" role="form" runat="server">
       <%-- <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary" Style="background-color: beige; overflow-x:hidden;height:550px; overflow-y:hidden;">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: large;" class="text-center text-uppercase"><b>IH&MS Integrated Solution India Pvt. Ltd.</b></div>
                    </div>
                </div>
            </div>
      <div class="panel-body">--%>
     
            <div class="container">
            <div class="row ">
        <%-- <div class="text-center">--%>
                 <div class="col-lg-1 col-md-3 col-sm-1">
                     </div>
                 <div class="col-lg-5 col-md-3 col-sm-3 col-xs-12" >
                     <img src="Images/logo.png" class="img-responsive logo" style="position:relative;padding:inherit;margin:185px 0px -23px;"/>  
                 </div>
            <%-- </div>--%>
                <div class="col-lg-5 col-md-3 col-sm-5 col-xs-12 card">
                     <%--<div class="card"></div>--%>
          <%--  <div class="card">--%>
                      <br/>
                <div class="input-container">
                <asp:TextBox ID="txt_login_id" runat="server" type="text" required="required" autocomplete="off" style="height:100px;"></asp:TextBox>
                <label for="Username">Login-Id</label>
                   <div class="bar"></div>
                </div>
                <div class="input-container">
                <asp:TextBox ID="txt_password" runat="server"   type="password" required="required" style="height:100px;"></asp:TextBox><span style="float: right;position: absolute;padding-top: 32px;" data-toggle="#txt_password" class="fa fa-fw fa-eye field_icon toggle-password"></span> <br />
                       <label for="Password" >Password</label>
                     <div class="bar"></div>
                   </div>
                <div class="input-container" style="background-color:#5cb85c;border-radius:4px;color:white;">
                   <asp:Button OnClientClick="return validate();" runat="server" type="password"
                         OnClick="btn_login_Click" ID="btn_login" style="height:50px;"
                        Text="Login" ></asp:Button></div>
              <%--  </div>--%>
                </div>
                <div class="col-lg-1 col-md-3 col-sm-3 col-xs-1">
               </div>
                </div>
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
                            <%--<asp:Button OnClientClick="return validate1();" runat="server" type="button" class="btn btn-success" OnClick="btn_email_Click" ID="btn_email" Text="OK"></asp:Button>--%>
                            <asp:Button data-dismiss="modal" runat="server" type="button" class="btn btn-success" ID="Button1" Text="Close"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
         <%-- </asp:Panel>
              </div>--%>
    </form>
</body>
</html>

