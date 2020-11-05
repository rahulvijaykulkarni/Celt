<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="employee_details.aspx.cs" Inherits="employee_details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="cph_title">
    <title>employee bank details</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cph_header">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta charset="utf-8" />
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
    <script src="Scripts/sweetalert.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" type="text/css" />
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>

    <script type="text/javascript">
        function pageLoad() {
            $('#<%=ddl_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_unitcode.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_employee_type.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_employee.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
        }
        function unblock() {
            $.unblockUI();
        }
        function isNumber(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            return true;
        }
        function AllowAlphabet_address(e) {
            if (null != e) {
                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) ||
                    (keyEntry == '32') || (keyEntry == '38') || ((keyEntry == '39') && (keyEntry == '34')) || (keyEntry == '44') || ((keyEntry >= '45') && (keyEntry <= '47')) ||
                    (keyEntry == '58') || (keyEntry == '59') || (keyEntry == '61') || (keyEntry == '92'))
                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }

        function allowAlphaNumericSpace(e) {
            var code = ('charCode' in e) ? e.charCode : e.keyCode;
            if (!(code == 32) && // space
              !(code > 47 && code < 58) && // numeric (0-9)
              !(code > 64 && code < 91) && // upper alpha (A-Z)
              !(code > 96 && code < 123)) { // lower alpha (a-z)
                e.preventDefault();
            }
        }
        function AllowAlphabet_Number(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9') || (keyEntry == '46') || (keyEntry == '44'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }
        function AllowAlphabet(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45' || (keyEntry == '8'))
                return true;
            else {
                // alert('Please Enter Only Character values.');
                return false;
            }
        }
        function email(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9') || (keyEntry == '64') || (keyEntry == '46'))

                    return true;
                else {
                    return false;
                }
            }
        }
        function Req_validation() {

            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

            if (Selected_ddl_client == "Select") {
                alert("Please Select Client Name");
                ddl_client.focus();
                return false;
            }

            var ddl_state = document.getElementById('<%=ddl_state.ClientID %>');
            var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;

            if (Selected_ddl_state == "Select") {
                alert("Please Select State Name");
                ddl_state.focus();
                return false;
            }
            var ddl_unitcode = document.getElementById('<%=ddl_unitcode.ClientID %>');
            var Selected_ddl_unitcode = ddl_unitcode.options[ddl_unitcode.selectedIndex].text;

            if (Selected_ddl_unitcode == "Select") {
                alert("Please Select Branch Name");
                ddl_unitcode.focus();
                return false;
            }

            var ddl_employee_type = document.getElementById('<%=ddl_employee_type.ClientID %>');
            var Selected_ddl_employee_type = ddl_employee_type.options[ddl_employee_type.selectedIndex].text;

            if (Selected_ddl_employee_type == "Select") {
                alert("Please Select Employee Type");
                ddl_employee_type.focus();
                return false;
            }

            var ddl_employee = document.getElementById('<%=ddl_employee.ClientID %>');
            var Selected_ddl_employee = ddl_employee.options[ddl_employee.selectedIndex].text;

            if (Selected_ddl_employee == "ALL") {
                alert("Please Select Employee Name");
                ddl_employee.focus();
                return false;
            }

            var txt_bankaccountno = document.getElementById('<%=txt_bankaccountno.ClientID %>');

            if (txt_bankaccountno.value == "") {
                alert("Please Enter Bank Account Number");
                txt_bankaccountno.focus();
                return false;
            }
            var txt_holdaer_name = document.getElementById('<%=txt_holdaer_name.ClientID %>');

            if (txt_holdaer_name.value == "") {
                alert("Please Enter Account Holder Name");
                txt_holdaer_name.focus();
                return false;
            }
            var txt_ifsccode = document.getElementById('<%=txt_ifsccode.ClientID %>');

            if (txt_ifsccode.value == "") {
                alert("Please Enter IFSC Code");
                txt_ifsccode.focus();
                return false;
            }
            var txt_pfbankname = document.getElementById('<%=txt_pfbankname.ClientID %>');

            if (txt_pfbankname.value == "") {
                alert("Please Enter Bank Name");
                txt_pfbankname.focus();
                return false;
            }
            var ddl_bankcode = document.getElementById('<%=ddl_bankcode.ClientID %>');

            if (ddl_bankcode.value == "") {
                alert("Please Enter Branch Location Name");
                ddl_bankcode.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function R_validation() {
            var val = confirm("Are you Sure You Want to Delete Record")
            if (val == true) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }
            else {
                return false;
            }
        }
        function isNumber_dot(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 46 || charCode > 46)) {
                    return false;
                }
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cph_righrbody">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-primary" Style="background-color: beige;">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Employee Bank Details</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
            <div class="container-fluid" runat="server" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white; width: 99%">
                <br />
                <div class="row">
                    <div class="col-sm-2 col-xs-12 text-left">
                        Client Name :   
                <asp:DropDownList ID="ddl_client" class="form-control" runat="server" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        State Name :   
                 <asp:DropDownList ID="ddl_state" runat="server" class="form-control" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true">
                 </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Branch Name :   
                <asp:DropDownList ID="ddl_unitcode" class="form-control" runat="server" OnSelectedIndexChanged="ddl_unitcode_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Employee Type :
                                    <asp:DropDownList ID="ddl_employee_type" runat="server" class="form-control" OnSelectedIndexChanged="ddl_employee_type_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                        <asp:ListItem Value="Permanent">Permanent</asp:ListItem>
                                        <asp:ListItem Value="Reliever">Reliever</asp:ListItem>
                                        <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                        <asp:ListItem Value="Temporary">Temporary</asp:ListItem>
                                        <asp:ListItem Value="Left">Left</asp:ListItem>
                                    </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Employee Name :    
                <asp:DropDownList ID="ddl_employee" class="form-control" runat="server" OnSelectedIndexChanged="ddl_employee_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Bank Account Number :   
                    
                        <asp:TextBox ID="txt_bankaccountno" runat="server" class="form-control "
                            onkeypress="return isNumber(event)"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">

                    <div class="col-sm-2 col-xs-12">
                        Account Holder Name :   
                        <asp:TextBox ID="txt_holdaer_name" runat="server" class="form-control"
                            onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Bank IFSC Code :   
                        <asp:TextBox ID="txt_ifsccode" runat="server" class="form-control date_join" Width="100%"
                            onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Bank Name :    
                   <asp:TextBox ID="txt_pfbankname" runat="server" class="form-control" MaxLength="50" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Branch Location Name :   
                   <asp:TextBox ID="ddl_bankcode" runat="server" class="form-control" MaxLength="200" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                    </div>

                </div>
                <br />
            </div>
            <br />
            <div class="row text-center">
                <%--<asp:Button ID="btn_add" runat="server" class="btn btn-primary" Text=" Save "  OnClick="btn_add_Click"/>--%>
                <asp:Button ID="btn_edit" runat="server" class="btn btn-primary" Text="Update" OnClick="btn_edit_Click" OnClientClick="return Req_validation();" />
                <%--                    <asp:Button ID="btn_delete" runat="server" class="btn btn-primary" Text=" Delete" OnClientClick="return R_validation();" OnClick="btn_delete_Click" />--%>
                <asp:Button ID="btnclose" runat="server" class="btn btn-danger" Text="Close" OnClick="btnclose_Click" />
            </div>
            <br />
        </asp:Panel>
    </div>
</asp:Content>
