<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="employee_bank_details.aspx.cs" Inherits="employee_details" %>

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
        $(function () {
            $("#dialog").dialog({
                title: "Zoomed Image",
                autoOpen: false,
                modal: true,
                height: 500,
                width: 500,
                title: "Zoomed Image",
                buttons: [{ text: "Close", click: function () { $(this).dialog("close") } }],
            });
            $("[id*=ImageButton3]").click(function () {
//alert("giii");
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(400));
                $('#dialog').dialog('open');
               // alert("giii");
                //height:200;
                //width: 200;
            });
           

        });
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
            var val = confirm("Are you Sure You Want to Update Record")
            if (val == true) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }
            else {
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function validation_aprove() {
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

           
            var val = confirm("Are you Sure You Want to Approve Record")
            if (val == true) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }
            else {
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }
        //suraj
        function validation_reg() {

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

            var txt_reject = document.getElementById('<%=txt_reject.ClientID %>');

            if (txt_reject.value == "") {
                alert("Please Enter Reject Reason!!");
                txt_reject.focus();
                return false;
            }
            var val = confirm("Are you Sure You Want to Reject Record")
            if (val == true) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }
            else {
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        //function validation() {
        //    var val = confirm("Are you Sure You Want to Delete Record")
        //    if (val == true) {
        //        $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
        //        return true;
        //    }
        //    else {
        //        return false;
        //    }
        //}
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
        <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-primary" Style="background-color: white;">
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
            <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-left:15px; margin-right:15px; margin-bottom:20px; margin-top:20px">
                <br />
                <div class="row">
                    <div class="col-sm-2 col-xs-12 text-left">
                       <b> Client Name :   </b>
                <asp:DropDownList ID="ddl_client" class="form-control" runat="server" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> State Name : </b>  
                 <asp:DropDownList ID="ddl_state" runat="server" class="form-control" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true">
                 </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Branch Name : </b>  
                <asp:DropDownList ID="ddl_unitcode" class="form-control" runat="server" OnSelectedIndexChanged="ddl_unitcode_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Employee Type :</b> 
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
                       <b> Employee Name : </b>    
                <asp:DropDownList ID="ddl_employee" class="form-control" runat="server" OnSelectedIndexChanged="ddl_employee_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Bank Account Number : </b>   
                    
                        <asp:TextBox ID="txt_bankaccountno" runat="server" class="form-control "
                           ReadOnly ="true" onkeypress="return isNumber(event)"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">

                    <div class="col-sm-2 col-xs-12">
                      <b>  Account Holder Name :</b>    
                        <asp:TextBox ID="txt_holdaer_name" runat="server" class="form-control"
                           ReadOnly ="true" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Bank IFSC Code : </b>  
                        <asp:TextBox ID="txt_ifsccode" runat="server" class="form-control date_join" Width="100%"
                          ReadOnly="true"  onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Bank Name :</b>    
                   <asp:TextBox ID="txt_pfbankname" runat="server" class="form-control" MaxLength="50" ReadOnly="true" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                      <b>  Branch Location Name : </b>  
                   <asp:TextBox ID="ddl_bankcode" runat="server" class="form-control" MaxLength="200" ReadOnly="true" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Reject Reason :</b>
                        <asp:TextBox runat="server" ID="txt_reject" CssClass="form-control" Height="50px" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                    </div>
                    <br />
                    <br />
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-8">
                    <div class="col-sm-1 col-xs-12" runat="server" id="bank" visible="false">
                   <b> Bank Passbook:</b>
                       <br />
                    <asp:Label ID="Label1" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                </div>

                    <%--  this div we use for dialog popup window--%>
                    <div id="dialog"></div>
                <div class="col-sm-2 col-xs-12" runat="server" id="bankkk" visible="false" >
                    <asp:Image ID="ImageButton3" runat="server" Height="100" Width="100" ImageUrl="~/Images/placeholder.png" />
                    <br />
                    <asp:LinkButton ID="link_joining" Text="Download" runat="server" CommandArgument="_24" OnCommand="download_document1" Style="margin-left: 24px"></asp:LinkButton>
                </div>
                        </div>
                     <div class="col-sm-1 col-sm-12">
                        </div>
                      <div class="col-sm-3 col-sm-12">
                    <asp:Panel ID="panel_link" runat="server">
                       <br />
                       
                      
                            <table class="table table-striped" border="1" style="border-color: #c7c5c5">
                              <%--  <tr>
                                    <th><a data-toggle="modal" href="#remain_admin"><font color="red"><b><%= rem_emp_count %></b></font>Bank Details Not Approve By Admin </a></th>
                                </tr>--%>
                                <tr>
                                    <th><a data-toggle="modal" href="#approve_admin"><font color="red"><b><%= appro_emp_count %></b></font>Bank Details Approve  By Admin </a></th>
                                </tr>
                                <tr>
                                    <th><a data-toggle="modal" href="#approve_bank"><font color="red"><b><%= appro_emp_bank %></b></font>Bank Details Aprroved  By Fiance</a></th>
                                </tr>
                                <tr>
                                    <th><a data-toggle="modal" href="#rejected_bank_emp"><font color="red"><b><%= rejected_bank_emp %></b></font>Bank Details Rejected By Fiance</a></th>
                                </tr>
                            </table>
                       
                    </asp:Panel>
                           </div>
                   <%-- <div class="modal fade" id="remain_admin" role="dialog" data-dismiss="modal" href="#lost">

                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 style="text-align: center">Employee Not Approve By Admin </h4>
                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-12" style="padding-left: 1%;">
                                            <asp:GridView ID="gv_rem_emp_count" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                                <Columns>
                                                    <asp:BoundField DataField="Client_Name" HeaderText="Client Name" SortExpression="Client_Name" />
                                                    <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                                    <asp:BoundField DataField="branch_name" HeaderText="Branch Name" SortExpression="branch_name" />
                                                    <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <div class="modal-footer">
                                    <div class="row text-center">
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-4">
                                            <button type="button" class="btn btn-danger" data-dismiss="modal">CLOSE</button>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>--%>
                    <div class="modal fade" id="approve_admin" role="dialog" data-dismiss="modal" href="#lost">

                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 style="text-align: center">Employee  Approve By Admin</h4>
                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-12" style="padding-left: 1%;">
                                            <asp:GridView ID="gv_appro_emp_count" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                                <Columns>
                                                    <asp:BoundField DataField="Client_Name" HeaderText="Client Name" SortExpression="Client_Name" />
                                                    <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                                    <asp:BoundField DataField="branch_name" HeaderText="Branch Name" SortExpression="branch_name" />
                                                    <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <div class="modal-footer">
                                    <div class="row text-center">
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-4">
                                            <button type="button" class="btn btn-danger" data-dismiss="modal">CLOSE</button>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="modal fade" id="approve_bank" role="dialog" data-dismiss="modal" href="#lost">

                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 style="text-align: center">Employee BankDetails Aprroved  By Fiance</h4>
                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-12" style="padding-left: 1%;">
                                            <asp:GridView ID="gv_appro_emp_bank" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                                <Columns>
                                                    <asp:BoundField DataField="Client_Name" HeaderText="Client Name" SortExpression="Client_Name" />
                                                    <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                                    <asp:BoundField DataField="branch_name" HeaderText="Branch Name" SortExpression="branch_name" />
                                                    <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <div class="modal-footer">
                                    <div class="row text-center">
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-4">
                                            <button type="button" class="btn btn-danger" data-dismiss="modal">CLOSE</button>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="modal fade" id="rejected_bank_emp" role="dialog" data-dismiss="modal" href="#lost">

                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 style="text-align: center">Employee BankDetails Rejected By Fiance</h4>
                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-12" style="padding-left: 1%;">
                                            <asp:GridView ID="gv_rejected_bank_emp" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                                <Columns>
                                                    <asp:BoundField DataField="Client_Name" HeaderText="Client Name" SortExpression="Client_Name" />
                                                    <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                                    <asp:BoundField DataField="branch_name" HeaderText="Branch Name" SortExpression="branch_name" />
                                                    <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                                                    <asp:BoundField DataField="bankdetail_reject_reason" HeaderText="Reject Reason" SortExpression="bankdetail_reject_reason" />

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <div class="modal-footer">
                                    <div class="row text-center">
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-4">
                                            <button type="button" class="btn btn-danger" data-dismiss="modal">CLOSE</button>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <br />
            </div>
            <br />
            <div class="row text-center">
                <%--<asp:Button ID="btn_add" runat="server" class="btn btn-primary" Text=" Save "  OnClick="btn_add_Click"/>--%>
                <asp:Button ID="btn_approve" runat="server" class="btn btn-primary" Text="Aprrove" OnClick="btn_approve_Click" OnClientClick="return validation_aprove();" />
                <%--<asp:Button ID="btn_edit" runat="server" class="btn btn-primary" Text="Update" OnClick="btn_edit_Click" OnClientClick="return Req_validation();" />--%>
                <asp:Button runat="server" ID="btn_reject" CssClass="btn btn-primary" Text="Reject" OnClick="btn_reject_Click" OnClientClick="return validation_reg(); " />
                <%--                    <asp:Button ID="btn_delete" runat="server" class="btn btn-primary" Text=" Delete" OnClientClick="return R_validation();" OnClick="btn_delete_Click" />--%>
                <asp:Button ID="btnclose" runat="server" class="btn btn-danger" Text="Close" OnClick="btnclose_Click" />
            </div>
            <br />
            <br />
        </asp:Panel>
    </div>
</asp:Content>
