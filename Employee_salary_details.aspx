<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Employee_salary_details.aspx.cs" Inherits="Employee_salary_details" Title="Employee salary details" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Employee Salary Details</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_header" runat="Server">
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

    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>

    <link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <%--<script src="datatable/jquery-1.12.3.js"></script>--%>
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/jszip.min.js"></script>
    <%--    <script src="datatable/pdfmake.min.js"></script>--%>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>
    <style>
        .text-red {
            color: red;
        }
    </style>
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {
            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "1990:+100",
                maxDate: 0,
                showButtonPanel: true,
                dateFormat: 'mm/yy',
                onClose: function (dateText, inst) {
                    $('.ui-datepicker-calendar').detach();
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, month, 1));
                }
            }).click(function () {
                $('.ui-datepicker-calendar').hide();
            });

            $(".date-picker").attr("readonly", "true");

            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
                onSelect: function (selected) {
                    $(".date-picker2").datepicker("option", "minDate", selected)
                }
            });


            $(".date-picker2").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
                onSelect: function (selected) {
                    $(".date-picker1").datepicker("option", "maxDate", selected)
                }
            });
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

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=grd_convayance.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                buttons: [
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ]

            });

            table.buttons().container()
               .appendTo('#<%=grd_convayance.ClientID%>_wrapper .col-sm-6:eq(0)');

              $.fn.dataTable.ext.errMode = 'none';
              var table = $('#<%=grd_emp_file.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                buttons: [
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ]

            });

            table.buttons().container()
               .appendTo('#<%=grd_emp_file.ClientID%>_wrapper .col-sm-6:eq(0)');
            //$(".date-picker1").val("");
            // $(".date-picker2").val("");

            display_panel();
        }
        function Req_validation() {
            var t_client = document.getElementById('<%=ddl_client.ClientID %>');
             var Selectedclient = t_client.options[t_client.selectedIndex].text;

             if (Selectedclient == "Select") {
                 alert("Please Select Client.");
                 t_client.focus();
                 return false;
             }
         }
         function display_panel() {
             var t_client = document.getElementById('<%=ddl_esicdeductionflag.ClientID %>');
            var SelectedDropdown = t_client.options[t_client.selectedIndex].text;
            if (SelectedDropdown == "No") {
                $('.hide_panel').show();
            }
            else {
                $('.hide_panel').hide();
            }
        }
        function AllowAlphabet_Number10(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || (keyEntry == '32') || (keyEntry == '9') || (keyEntry == '46') || (keyEntry == '44'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
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

        function chk_month() {
            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

            if (Selected_ddl_client == "Select") {
                alert("Please Select Client ");
                ddl_client.focus();
                return false;
            }
            var ddl_state = document.getElementById('<%=ddl_state.ClientID %>');
            var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;

            if (Selected_ddl_state == "Select") {
                alert("Please Select State ");
                ddl_state.focus();
                return false;
            }
            var ddl_unitcode = document.getElementById('<%=ddl_unitcode.ClientID %>');
            var Selected_ddl_unitcode = ddl_state.options[ddl_unitcode.selectedIndex].text;

            if (Selected_ddl_unitcode == "Select") {
                alert("Please Select Branch ");
                ddl_unitcode.focus();
                return false;
            }
            var ddl_employee_type = document.getElementById('<%=ddl_employee_type.ClientID %>');
            var Selected_ddl_employee_type = ddl_employee_type.options[ddl_employee_type.selectedIndex].text;
            if (Selected_ddl_employee_type == "Select") {
                alert("Please Select Employee Type ");
                ddl_employee_type.focus();
                return false;
            }
            var txt_date_conveyance = document.getElementById('<%=txt_date_conveyance.ClientID%>');
		    if (txt_date_conveyance.value == "") {
		        alert("Please Select month & year ");
		        txt_date_conveyance.focus();
		        return false;
		    }
		    var txt_bankaccountno = document.getElementById('<%=txt_bankaccountno.ClientID%>');
            var txt_holdaer_name = document.getElementById('<%=txt_holdaer_name.ClientID%>');
            var txt_ifsccode = document.getElementById('<%=txt_ifsccode.ClientID%>');
            var txt_pfbankname = document.getElementById('<%=txt_pfbankname.ClientID%>');
            var ddl_bankcode = document.getElementById('<%=ddl_bankcode.ClientID%>');


            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
            var st = $(this).find("input[id*='HiddenField1']").val();
            if (st == null)
                st = 0;
            $('[id$=Div1]').tabs({ selected: st });

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
        function deduction() {
            var txt_fine = document.getElementById('<%=txt_fine.ClientID %>');
            if (txt_fine.value == "0" || txt_fine.value == "") {
                alert("Please Enter Fine");
                txt_fine.focus();
                return false;
            }
            var txt_finedesc = document.getElementById('<%=txt_finedesc.ClientID %>');
		     if (txt_finedesc.value == "0" || txt_finedesc.value == "") {
		         alert("Please Enter Fine Description");
		         txt_finedesc.focus();
		         return false;
		     }
		     var txt_date = document.getElementById('<%=txt_date.ClientID %>');
            if (txt_date.value == "") {
                alert("Please Enter Date");
                txt_date.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function add_lnk_file() {
            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;
            if (Selected_ddl_client == "Select") {
                alert("Please Select Client ");
                ddl_client.focus();
                return false;
            }
            var ddl_employee_type = document.getElementById('<%=ddl_employee_type.ClientID %>');
            var Selected_ddl_employee_type = ddl_employee_type.options[ddl_employee_type.selectedIndex].text;
            if (Selected_ddl_employee_type == "Select") {
                alert("Please Select Employee Type ");
                ddl_employee_type.focus();
                return false;
            }

            var txt_fine = document.getElementById('<%=txt_fine.ClientID%>');
            if (txt_fine.value == "0") {
                alert("Please Enter Fine");
                txt_fine.focus();
                return false;
            }
            var txt_finedesc = document.getElementById('<%=txt_finedesc.ClientID %>');
            if (txt_finedesc.value == "0" || txt_finedesc.value == "") {
                alert("Please Enter Fine Description");
                txt_finedesc.focus();
                return false;
            }
            var txt_date = document.getElementById('<%=txt_date.ClientID %>');
            if (txt_date.value == "") {
                alert("Please Enter Date");
                txt_date.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function R_v_validation() {

            var r = confirm("Are you Sure You Want to Delete Record");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }
        function conv_validation() {
            var bill_upload = document.getElementById('<%=bill_upload.ClientID%>');
            if (bill_upload.value == "") {
                alert("Please Select File to Uoload");
                bill_upload.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function conv_req_validation() {

            var txt_date_conveyance = document.getElementById('<%=txt_date_conveyance.ClientID %>');
            if (txt_date_conveyance.value == "") {
                alert("Please Select Month");
                txt_date_conveyance.focus();
                return false;
            }
            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;
            if (Selected_ddl_client == "Select") {
                alert("Please Select Client ");
                ddl_client.focus();
                return false;
            }
            var ddl_state = document.getElementById('<%=ddl_state.ClientID %>');
            var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;
            if (Selected_ddl_state == "Select") {
                alert("Please Select State ");
                ddl_state.focus();
                return false;
            }
            var ddl_unitcode = document.getElementById('<%=ddl_unitcode.ClientID %>');
            var Selected_ddl_unitcode = ddl_unitcode.options[ddl_unitcode.selectedIndex].text;
            if (Selected_ddl_unitcode == "Select") {
                alert("Please Select Branch Name ");
                ddl_unitcode.focus();
                return false;
            }
            var ddl_employee_type = document.getElementById('<%=ddl_employee_type.ClientID %>');
            var Selected_ddl_employee_type = ddl_employee_type.options[ddl_employee_type.selectedIndex].text;
            if (Selected_ddl_employee_type == "Select") {
                alert("Please Select Employee Type ");
                ddl_employee_type.focus();
                return false;
            }
        }
        function AllowAlpha_Numeric(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '49') || ((keyEntry >= '57')))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }
        function AllowAlphabet_Number(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9') || (keyEntry == '46'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }
        function openWindow() {

            window.open("html/Employee_salary_details.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');

        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <div class="container-fluid">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Empolyee Salary Details</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Empolyee Salary Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">
                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <div class="row">

                    <div class="col-sm-2 col-xs-12">
                       <b> Select Month :</b><span class="text-red">*</span>
                        <asp:TextBox ID="txt_date_conveyance" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-sm-2 col-xs-12 text-left">
                       <b> Client Name : </b>  
                <asp:DropDownList ID="ddl_client" class="form-control pr_state js-example-basic-single" runat="server" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                       <b> State Name :</b>   
                 <asp:DropDownList ID="ddl_state" runat="server" DataTextField="STATE" DataValueField="STATE" class="form-control text_box" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true">
                 </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                       <b> Branch Name : </b>  
                <asp:DropDownList ID="ddl_unitcode" class="form-control pr_state js-example-basic-single" runat="server" OnSelectedIndexChanged="ddlunitselect_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                       <b> Employee Type :</b>
                                    <asp:DropDownList ID="ddl_employee_type" runat="server" class="form-control" OnSelectedIndexChanged="ddlunitselect_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                        <asp:ListItem Value="Permanent">Permanent</asp:ListItem>
                                        <%--<asp:ListItem Value="PermanentReliever">Permanent Reliever</asp:ListItem>--%>
                                        <asp:ListItem Value="Reliever">Reliever</asp:ListItem>
                                        <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                        <asp:ListItem Value="Temporary">Temporary</asp:ListItem>
                                        <%--<asp:ListItem Value="RM">Repair & Maintenance</asp:ListItem>--%>
                                        <asp:ListItem Value="Left">Left</asp:ListItem>
                                    </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                       <b> Employee Name :</b>    
                <asp:DropDownList ID="ddl_employee" class="form-control pr_state js-example-basic-single" runat="server" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                    </div>

                </div>
                    <br />
                    </div>

            </div>
            <%--   </ContentTemplate>
        </asp:UpdatePanel>--%>
            <br />
            <div id="tabs" style="background: #f3f1fe; padding:20px 20px 20px 20px; border: 1px solid #e2e2dd; margin:15px 15px 15px 15px; border-radius:10px">
                <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                <ul>
                    <li><a id="A2" href="#menu0" runat="server"><b>Compliances</b></a></li>

                    <li><a href="#menu5" id="A1" runat="server" style="display:none">Conveyance</a></li>
                </ul>
                <div id="menu0">
                    <div id="Div1" style="background: white; padding:20px 20px 20px 20px; border: 1px solid #e2e2dd; margin:15px 15px 15px 15px; border-radius:10px">
                        <asp:HiddenField ID="HiddenField1" Value="0" runat="server" />
                        <ul>
                            <li><a id="A3" href="#menu1" runat="server"><b>Employee Details</b></a></li>
                            <li><a href="#menu2" id="rating" runat="server"><b>Allowance</b></a></li>
                            <li><a href="#menu3" id="salary" runat="server"><b>Payment Hold/Unhold</b></a></li>
                            <li><a href="#menu4" id="file" runat="server"><b>Deduction/Fine</b> </a></li>
                        </ul>

                        <div id="menu1">
                            <br />
                            <br />
                            <%--   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>--%>
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                   <b> UAN Number :</b>  
                    
                        <asp:TextBox ID="txt_uanno" MaxLength="100" runat="server" Width="100%" class="form-control confirm_date"
                            meta:resourcekey="txt_confirmationdateResource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <b>PAN Number : </b> 
                    
                        <asp:TextBox ID="txt_pan_new_num" runat="server" class="form-control date_join" Width="100%"
                            meta:resourcekey="txt_joiningdateResource1" onkeypress="return  AllowAlphabet_address(event)" MaxLength="10"></asp:TextBox>
                                </div>

                                <div class="col-sm-2 col-xs-12">
                                   <b> ESIC Number : </b>  
                    
                        <asp:TextBox ID="txt_esicnumber" MaxLength="17" runat="server" Width="100%" class="form-control confirm_date"
                            meta:resourcekey="txt_confirmationdateResource1" onkeypress="return isNumber(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12 text-left">
                                   <b> ESIC Flag :</b>    
                         <asp:DropDownList ID="ddl_esicdeductionflag" runat="server"
                             class="form-control" meta:resourcekey="ddl_esicdeductionflagResource1" onchange="display_panel();">

                             <asp:ListItem meta:resourcekey="ListItemResource12" Text="Select"></asp:ListItem>
                             <asp:ListItem meta:resourcekey="ListItemResource12" Text="Yes"></asp:ListItem>
                             <asp:ListItem meta:resourcekey="ListItemResource13" Text="No"></asp:ListItem>
                         </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> PF Number :  </b> 
                    
                        <asp:TextBox ID="txt_pfnumber" runat="server" class="form-control date_join" Width="100%"
                            meta:resourcekey="txt_joiningdateResource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12 text-left">
                                   <b> PF Flag :</b>   
                        <asp:DropDownList ID="ddl_pfdeductionflag" runat="server" class="form-control" meta:resourcekey="ddl_pfdeductionflagResource1">
                            <asp:ListItem meta:resourcekey="ListItemResource12" Text="Select"></asp:ListItem>
                            <asp:ListItem meta:resourcekey="ListItemResource8" Text="Yes" Value="Yes"></asp:ListItem>
                            <asp:ListItem meta:resourcekey="ListItemResource9" Text="No" Value="No"></asp:ListItem>
                        </asp:DropDownList>
                                </div>

                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                   <b> Bank Account Number :</b>   
                    
                        <asp:TextBox ID="txt_bankaccountno" runat="server" class="form-control date_join" Width="100%"
                            meta:resourcekey="txt_joiningdateResource1" onkeypress="return isNumber_dot(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Account Holder Name : </b>  
                    
                        <asp:TextBox ID="txt_holdaer_name" runat="server" class="form-control date_join" Width="100%"
                            meta:resourcekey="txt_joiningdateResource1" onkeypress="return AllowAlphabet_Number10(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                  <b>  Bank IFSC Code : </b>  
                    
                        <asp:TextBox ID="txt_ifsccode" runat="server" class="form-control date_join" Width="100%"
                            meta:resourcekey="txt_joiningdateResource1" onkeypress="return AllowAlphabet_Number(event);" MaxLength="11"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Bank Name :</b>    
                                  <asp:TextBox ID="txt_pfbankname" runat="server" class="form-control text_box" MaxLength="50" meta:resourcekey="txt_pfbanknameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Branch Location Name :</b>   
                                 <asp:TextBox ID="ddl_bankcode" runat="server" class="form-control text_box" MaxLength="200" meta:resourcekey="ddl_bankcodeResource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                </div>

                            </div>
                            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
                            <br />
                            <asp:Panel ID="psnel_esic" runat="server">
                                <div class="row">
                                    <br />
                                    <div class="col-sm-2 col-xs-12 hide_panel">
                                       <b> Group Insurance :</b>
                    <asp:TextBox ID="txt_greoupinsuranc" runat="server" Text="0" class="form-control text_box maskedExt" maskedFormat="10,2" onkeypress="return isNumber_dot(event)"
                        meta:resourcekey="txtdhead6Resource1" MaxLength="5"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12 hide_panel">
                                       <b> Policy Id:</b>
                     <asp:TextBox ID="txt_policy_id" runat="server" Text="0" class="form-control text_box  maskedExt" maskedFormat="10,2" onkeypress="return isNumber_dot(event)"
                         meta:resourcekey="txtdhead6Resource1" MaxLength="5"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12 hide_panel">
                                       <b> Start Date</b>
                            <asp:TextBox ID="txt_start_date" runat="server" class="form-control date-picker1" placeholder="Start Date :"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-2 col-xs-12 hide_panel">
                                       <b> End Date</b>
                            <asp:TextBox ID="txt_end_date" runat="server" class="form-control date-picker2" placeholder="End Date :"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div id="menu2">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <br />
                                        <div class="col-sm-2 col-xs-12">
                                           <b> CCA :</b>
                    <asp:TextBox ID="Txt_cca" runat="server" Text="0" class="form-control text_box maskedExt" maskedFormat="10,2" onkeypress="return isNumber_dot(event)"
                        meta:resourcekey="txtdhead6Resource1" MaxLength="5"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> CCA Description :</b>
                                   
                                                     <asp:TextBox ID="txt_ccadesc" runat="server" Text="0" class="form-control text_box "
                                                         meta:resourcekey="txtdhead6Resource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-2 col-xs-12">
                                           <b> Advance :</b>
                                                <asp:TextBox ID="txt_advance_payment" runat="server" Text="0" class="form-control" onkeypress="return isNumber_dot(event)" meta:resourceKey="txt_advance_paymentResource1"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Advance Description :</b>
                                   
                                                     <asp:TextBox ID="txt_advancedesc" runat="server" Text="0" class="form-control text_box"
                                                         meta:resourcekey="txtdhead6Resource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>

                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">

                                        <div class="col-sm-2 col-xs-12">
                                          <b>  Gratuity:</b>
                     <asp:TextBox ID="Txt_gra" runat="server" Text="0" class="form-control text_box  maskedExt" maskedFormat="10,2" onkeypress="return isNumber_dot(event)"
                         meta:resourcekey="txtdhead6Resource1" MaxLength="5"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Special Allowance :</b>
                    <asp:TextBox ID="Txt_allow" runat="server" Text="0" class="form-control text_box  maskedExt" maskedFormat="10,2" onkeypress="return isNumber_dot(event)"
                        meta:resourcekey="txtdhead6Resource1" MaxLength="5"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Conveyance Amount :</b>
                                                <asp:TextBox ID="txt_conveyance_amount" runat="server" Text="0" class="form-control" onkeypress="return isNumber_dot(event)" meta:resourceKey="txt_advance_paymentResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="menu3">
                            <br />
                            <div class="row">
                                <%-- <div class="col-sm-2 col-xs-12">
                        Select Month :<span class="text-red">*</span>
                        <asp:TextBox ID="txt_payment_date" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                    </div>
                                <div class="col-sm-2 col-xs-12 text-left">
                                    Payment Status:    
                         <asp:DropDownList ID="ddl_payment_status" runat="server"
                             class="form-control">

                             <asp:ListItem Value="0">Select</asp:ListItem>
                             <asp:ListItem Value="1">Hold</asp:ListItem>
                             <asp:ListItem Value="2">UnHold</asp:ListItem>
                         </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    Reason:  
                                    <asp:TextBox ID="txt_desc" MaxLength="100" runat="server" TextMode="MultiLine" Width="100%" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    File to Upload :
                                    <asp:FileUpload ID="salary_attachment" runat="server" />
                                </div>
                                <br />
                                 <asp:LinkButton ID="add_link_payment_hold" runat="server" OnClick="add_link_payment_hold_Click" >
                        <img alt="Add Item" src="Images/add_icon.png"  style="margin-top:1.5em;" />
                            </asp:LinkButton>--%>
                                 <div class="col-sm-2 col-xs-12">
                                 <asp:Button ID="btn_show" runat="server" class="btn btn-primary" Text=" Show " OnClick="btn_show_Click" />
                            </div>
                                </div>
                             <asp:Panel ID="gv_panel" runat="server" CssClass="grid-view">
                             <div class="container" style="width: 100%">
                                <br />
                                <asp:GridView ID="gv_payment_hold" class="table" runat="server" BackColor="White" OnRowDataBound="gv_payment_hold_RowDataBound"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                    AutoGenerateColumns="false" Width="100%" OnPreRender="gv_payment_hold_PreRender">

                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <Columns>

                                       <%-- <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_remove_payment" runat="server" CausesValidation="false" OnClick="lnk_remove_payment_Click"  OnClientClick="return R_validation();"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                          <asp:TemplateField HeaderText="Sr No.">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                           <asp:BoundField DataField="EMP_CODE" HeaderText="Employee CODE" SortExpression="EMP_CODE" />
                                        <asp:BoundField DataField="salary_status" HeaderText="Emp status" SortExpression="salary_status" />
                                        <asp:BoundField DataField="EMP_NAME" HeaderText="Employee NAME" SortExpression="EMP_NAME" />
                                          <asp:TemplateField HeaderText="SALARY STATUS">
                                            <ItemStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddl_payment_status"  Width="180px" CssClass="form-control" OnSelectedIndexChanged="ddl_payment_status_SelectedIndexChanged">
                                                     <asp:ListItem Value="0">UnHold</asp:ListItem>
                                                     <asp:ListItem Value="1">Hold</asp:ListItem>
                                                     <%--<asp:ListItem Value="2">UnHold</asp:ListItem>--%>
                                               </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="REASON">
                                            <ItemStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txt_salary_desc" Text='<%# Eval("salary_desc") %>' Width="180px" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                      

                                    </Columns>
                                </asp:GridView>


                            </div>
                                 </asp:Panel>
                        </div>

                        <%--///komal 16-05-19--%>

                        <div id="menu4">

                            <br />

                            <div class="col-sm-2 col-xs-12">
                               <b> Fine :</b>
                                   
                                                     <asp:TextBox ID="txt_fine" runat="server" Text="0" class="form-control text_box  maskedExt" maskedFormat="10,2" onkeypress="return isNumber_dot(event)"
                                                         meta:resourcekey="txtdhead6Resource1" MaxLength="7"></asp:TextBox>
                            </div>

                            <div class="col-sm-2 col-xs-12">
                               <b> Fine Description :</b>
                                   
                                                     <asp:TextBox ID="txt_finedesc" runat="server" Text=" " class="form-control text_box"
                                                         meta:resourcekey="txtdhead6Resource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                            </div>


                            <div class="col-sm-2 col-xs-12">
                               <b> Month :</b>
                                   
                                                     <asp:TextBox ID="txt_date" runat="server" CssClass="form-control date-picker"
                                                         meta:resourcekey="txtdhead6Resource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                            </div>


                            <asp:LinkButton ID="add_lnk_file" runat="server" OnClick="add_lnk_file_Click" OnClientClick="return deduction();">
                        <img alt="Add Item" src="Images/add_icon.png"  style="margin-top:1.5em;" />
                            </asp:LinkButton>



                            <%-- komal 17-05-19--%>
                            <br />
                            <div class="container" style="width: 100%">
                                <br />
                                <asp:GridView ID="grd_emp_file" class="table" runat="server" BackColor="White" OnRowDataBound="grd_emp_file_RowDataBound"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    AutoGenerateColumns="false" Width="100%">

                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <Columns>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_remove_bank" runat="server" CausesValidation="false" OnClick="lnk_remove_bank_Click" OnClientClick="return R_validation();"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sr No.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
                                        <asp:BoundField DataField="fine" HeaderText="Fine" SortExpression="fine" />
                                        <asp:BoundField DataField="fine_description" HeaderText="Fine Description" SortExpression="fine_description" />
                                        <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" />

                                    </Columns>
                                </asp:GridView>


                            </div>
                        </div>
                        <br />
                    </div>
                    <br />
                    <div class="row text-center">

                        <asp:Button ID="btn_add" runat="server" class="btn btn-primary" OnClick="btn_add_Click" OnClientClick="return chk_month();"
                            Text=" Save " />
                        <asp:Button ID="btn_Export" runat="server" class="btn btn-primary" OnClick="btn_export_Click"
                            Text=" Export " />


                        <asp:Button ID="btn_Import" runat="server" class="btn btn-primary" OnClick="btn_import_Click"
                            Text=" Import " />


                        <asp:Button ID="btnclose" runat="server" class="btn btn-danger"
                            OnClick="btnclose_Click" Text="Close" CausesValidation="False" />
                    </div>
                </div>

                <%-- komal 12-06-19--%>

                <div id="menu5" style="display:none">
                    <div id="wssc">
                        <table class="table table-striped" style="width: 20%">
                            <tr>
                                <td>File to Upload :
                                                <asp:FileUpload ID="bill_upload" runat="server" meta:resourcekey="photo_uploadResource1" onchange="ValidateSingleInput(this);" />
                                    <span style="color: red; font-size: 8px; font-weight: bold;">Only JPG,JPEG,GIF,PDF</span></td>
                                <td>
                                    <asp:Button ID="btn_upload" runat="server" class="btn btn-primary" Style="margin-top: 1em" OnClick="btn_upload_Click1" Text=" Upload " OnClientClick="return conv_validation();" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div class="container" style="width: 75%;overflow-y:auto;height:300px;">
                           
                            <asp:GridView ID="grd_convayance" class="table" runat="server" BackColor="White" OnRowDataBound="grd_convayance_RowDataBound"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                AutoGenerateColumns="False" Width="100%">

                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No.">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EMP_CODE" HeaderText="Employee Code" SortExpression="EMP_CODE" />
                                    <asp:BoundField DataField="EMP_NAME" HeaderText="Employee Name" SortExpression="EMP_NAME" />
                                    <%--<asp:BoundField DataField="unit_code1" HeaderText="unit_code1" SortExpression="unit_code1" />
                                            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />--%>

                                    <asp:TemplateField HeaderText="Conveyance Amount :">
                                        <ItemStyle Width="70px" />
                                        <ItemTemplate>
                                            <%-- <asp:TextBox ID="txt_conveyance" runat="server"   Width="80px" Text='<%# Eval("quantity") %>'></asp:TextBox>--%>

                                            <asp:TextBox ID="txt_conveyance_amount" runat="server" Text='<%# Eval("conveyance_rate") %>' class="form-control" onkeypress="return isNumber_dot(event)" meta:resourceKey="txt_advance_paymentResource1"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- <div class="col-sm-2 col-xs-12">
                                                Conveyance Amount :
                                                <asp:TextBox ID="txt_conveyance_amount" runat="server" Text="0" class="form-control" onkeypress="return isNumber_dot(event)" meta:resourceKey="txt_advance_paymentResource1"></asp:TextBox>
                                            </div> --%>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <br />
                    <div class="container" style="width: 75%">
                        <asp:GridView ID="grd_convayance_location" class="table" runat="server" BackColor="White" OnRowDataBound="grd_convayance_RowDataBound"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            AutoGenerateColumns="False" Width="100%">

                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sr No.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                <asp:BoundField DataField="state_name" HeaderText="State" SortExpression="state_name" />
                                <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="UNIT_name" />
                                <asp:BoundField DataField="month" HeaderText="Month" SortExpression="month" />
                                <asp:TemplateField HeaderText="DOWNLOAD">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <%--<asp:LinkButton ID="lnk_attendance" runat="server" CausesValidation="false" Text="Conveyance" CommandArgument='<%# Eval("file_name") +","+ Eval("branch_name")%>' OnCommand="lnkDownload_Command" CssClass="btn btn-primary"></asp:LinkButton>--%>
                                        <asp:LinkButton ID="lnk_conveyance" runat="server" CausesValidation="false" Text="Conveyance" Style="color: white" OnCommand="lnk_conveyance_Command" CommandArgument='<%# Eval("conveyance_images") +","+ Eval("unit_name")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                    <div class="row text-center">
                        <asp:Button ID="btn_conv_save" OnClick="btn_conv_save_Click" runat="server" CssClass="btn btn-primary" Text="Save" OnClientClick="return conv_req_validation()" />
                        <asp:Button ID="Button1" runat="server" class="btn btn-danger"
                            OnClick="btnclose_Click" Text="Close" CausesValidation="False" />
                    </div>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-sm-5 col-xs-12"></div>
                <div class="col-sm-2 col-xs-12">

                    <asp:FileUpload ID="FileUpload1" runat="server" Visible="false" />
                </div>
            </div>

            <br />

            <br />
    </div>
    <br />

    </div>
                    <br />
    <asp:Panel ID="Panel1" class="table" runat="server" ScrollBars="Both" Height="250px"
        BorderColor="#43729F" BorderStyle="Solid" Visible="false">
        <asp:GridView ID="UpdtEmpAttendanceGridView" class="table" runat="server" BackColor="White"
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <RowStyle ForeColor="#000066" />
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </asp:Panel>
    </asp:Panel>
          
    </div>
</asp:Content>
