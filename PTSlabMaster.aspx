<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PTSlabMaster.aspx.cs" Inherits="PTSlabMaster" Title="PT Slab Master Page" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>PT Slab Master</title>
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

    <link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" 
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
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {
            $('#<%=btn_cancel.ClientID%>').click(function () {
                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
             $('#<%=ddl_uan_client_list.ClientID%>').change(function () {
                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             });
            $('#<%=ddl_uan_state_name.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=PTSlabGridView.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=gv_pfdetails.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=gv_lwfdetails.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_pt_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_pt_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_upload_pf.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

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
            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    $(".date-picker2").datepicker("option", "minDate", selected)
                }
            });

            $(".date-picker").attr("readonly", "true");

            $(document).ready(function () {
                addition_all_textbox();
                var table = $('#<%=PTSlabGridView.ClientID%>').DataTable({
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
                   .appendTo('#<%=PTSlabGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

                $.fn.dataTable.ext.errMode = 'none';

                var table = $('#<%=gv_pfdetails.ClientID%>').DataTable({
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
                   .appendTo('#<%=gv_pfdetails.ClientID%>_wrapper .col-sm-6:eq(0)');

                $.fn.dataTable.ext.errMode = 'none';

                var table = $('#<%=gv_labour_office.ClientID%>').DataTable({
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
                   .appendTo('#<%=gv_labour_office.ClientID%>_wrapper .col-sm-6:eq(0)');

                var table = $('#<%=gv_lwfdetails.ClientID%>').DataTable({
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
                   .appendTo('#<%=gv_lwfdetails.ClientID%>_wrapper .col-sm-6:eq(0)');

                $.fn.dataTable.ext.errMode = 'none';

            });
        }
    </script>

    <style>
       
    </style>
    <style>
        .label_text {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 10px;
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
            overflow-x: hidden;
            overflow-y: auto;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }

        .text-red {
            color: #f00;
        }
    </style>

    <script type="text/javascript">

        function Req_validation() {

            var state = document.getElementById('<%=ddl_state.ClientID%>');
            var Selected_state = state.options[state.selectedIndex].text;
            var t_FromAmount = document.getElementById('<%=txt_fromamount.ClientID %>');
            var t_ToAmount = document.getElementById('<%=txt_toamount.ClientID %>');
            var t_SlabAmount = document.getElementById('<%=txt_slabamount.ClientID %>');


            if (Selected_state == "Select") {
                alert("Please Select state  !!!");
                state.focus();
                return false;

            }

            //if (state.value == "-") {
            //    alert("Please Select State !!!!!!");
            //    state.focus();
            //    return false;
            //}
            // Amount

            if (t_FromAmount.value == "") {
                alert("Please Enter Form Amount");
                t_FromAmount.focus();
                return false;
            }

            // To Amount

            if (t_ToAmount.value == "") {
                alert("Please Enter To Amount");
                t_ToAmount.focus();
                return false;
            }

            // Slab Amount

            if (t_SlabAmount.value == "") {
                alert("Please Enter the Slab Amount");
                t_SlabAmount.focus();
                return false;
            }
            var ddl_from_month = document.getElementById('<%=ddl_from_month.ClientID%>');
            var Selected_ddl_from_month = ddl_from_month.options[ddl_from_month.selectedIndex].text;

            if (Selected_ddl_from_month == "Select Month") {
                alert("Please Select From Month");
                ddl_from_month.focus();
                return false;
            }
            var ddl_to_month = document.getElementById('<%=ddl_to_month.ClientID%>');
            var Selected_ddl_to_month = ddl_to_month.options[ddl_to_month.selectedIndex].text;

            if (Selected_ddl_to_month == "Select Month") {
                alert("Please Select To Month");
                ddl_to_month.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }


        function validate() {
            var t_FromAmount = document.getElementById('<%=txt_fromamount.ClientID %>');
            var t_ToAmount = document.getElementById('<%=txt_toamount.ClientID %>');
            if (!t_FromAmount.value == "" && !t_ToAmount.value == "") {
                if (parseFloat(t_FromAmount.value) >= parseFloat(t_ToAmount.value)) {
                    alert("To Amount should be greater than From Amount.");
                    t_ToAmount.value = "";
                    t_ToAmount.focus();
                    return false;
                }
                else { return true; }
            }
        }

        function addition_all_textbox() {
            var txt_pf_account = document.getElementById('<%=txt_pf_account.ClientID %>');
            if (txt_pf_account.value == "") { txt_pf_account.value = "0"; }
            var txt_pension_account = document.getElementById('<%=txt_pension_account.ClientID %>');
             if (txt_pension_account.value == "") { txt_pension_account.value = "0"; }
             var txt_admin_charge = document.getElementById('<%=txt_admin_charge.ClientID %>');
            if (txt_admin_charge.value == "") { txt_admin_charge.value = "0"; }
            var txt_edli = document.getElementById('<%=txt_edli.ClientID %>');
            if (txt_edli.value == "") { txt_edli.value = "0"; }
            var txt_total = document.getElementById('<%=txt_total.ClientID %>');
            txt_total.readOnly = true;
            var result = parseFloat(txt_pf_account.value) + parseFloat(txt_pension_account.value) + parseFloat(txt_admin_charge.value) + parseFloat(txt_edli.value);
            if (!isNaN(result)) {
                txt_total.value = result;

                return true;
            }
            return false;
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

        $(function () {

            $('#<%=btnexporttoexcelptslab.ClientID%>').click(function () {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });




        });

            $(document).ready(function () {

                $(".js-example-basic-single").select2();

            });

            function openWindow() {
                window.open("html/PTSlabMaster.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
            }
            function validation() {
                var Select_employee_type = document.getElementById('<%=Select_employee_type.ClientID%>');
            var Selected_Select_employee_type = Select_employee_type.options[Select_employee_type.selectedIndex].text;

            var txt_pf_account = document.getElementById('<%=txt_pf_account.ClientID%>');
            var txt_pension_account = document.getElementById('<%=txt_pension_account.ClientID%>');
            var txt_admin_charge = document.getElementById('<%=txt_admin_charge.ClientID%>');
            var txt_edli = document.getElementById('<%=txt_edli.ClientID%>');

            if (Selected_Select_employee_type == "Select") {
                alert("Please Select Employer/Employee !!!")
                Select_employee_type.focus();
                return false;
            }
            if (txt_pf_account.value == "0") {
                alert("Please Select PF Amount !!!")
                txt_pf_account.focus();
                return false;
            }
            if (txt_pension_account.value == "0") {
                alert("Please Select Pension Amount !!!")
                txt_pension_account.focus();
                return false;
            }

            if (txt_admin_charge.value == "0") {
                alert("Please Select Admin Charges !!!")
                txt_admin_charge.focus();
                return false;
            }

            if (txt_edli.value == "0") {
                alert("Please Select EDLI Charges !!!")
                txt_edli.focus();
                return false;
            }
        }
        function Req_validation1() {

            var r = confirm("Are you Sure You Want to Delete Record");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }

        function R_validation() {
            var Select_lwf_state = document.getElementById('<%=Select_lwf_state.ClientID%>');
            var Selected_Select_lwf_state = Select_lwf_state.options[Select_lwf_state.selectedIndex].text;

            var txt_lwf_act = document.getElementById('<%=txt_lwf_act.ClientID%>');
            var txt_emp_category = document.getElementById('<%=txt_emp_category.ClientID%>');

            var ddl_contract_laobour = document.getElementById('<%=ddl_contract_laobour.ClientID%>');
            var Selected_ddl_contract_laobour = ddl_contract_laobour.options[ddl_contract_laobour.selectedIndex].text;

            var txt_period = document.getElementById('<%=txt_period.ClientID%>');
            var txt_last_day = document.getElementById('<%=txt_last_day.ClientID%>');
            var txt_e_contribution = document.getElementById('<%=txt_e_contribution.ClientID%>');
            var txt_c_contribution = document.getElementById('<%=txt_c_contribution.ClientID%>');

            if (Selected_Select_lwf_state == "Select") {

                alert("Please Select State !!");
                Select_lwf_state.focus();
                return false;
            }
            if (txt_lwf_act.value == "") {

                alert("Please Select Applicablity of the LWF Act !!");
                txt_lwf_act.focus();
                return false;
            }
            if (txt_emp_category.value == "") {

                alert("Please Select Category of Employees covered !!");
                txt_emp_category.focus();
                return false;
            }
            if (txt_period.value == "") {

                alert("Please Select Period !!");
                txt_period.focus();
                return false;
            }
            if (txt_last_day.value == "") {

                alert("Please Select Last Day for submission !!");
                txt_last_day.focus();
                return false;
            }
            if (txt_e_contribution.value == "") {

                alert("Please Select Employee Contribution !!");
                txt_e_contribution.focus();
                return false;
            }
            if (txt_c_contribution.value == "") {

                alert("Please Select Employer Contribution !!");
                txt_c_contribution.focus();
                return false;
            }
        }
        
        function pf_upload_sys() {
            var d_client = document.getElementById('<%=ddl_pf_client.ClientID%>');
            var Selected_client = d_client.options[d_client.selectedIndex].text;
            var t_month_year = document.getElementById('<%=txt_pf_date.ClientID%>');

            if (Selected_client == "Select") {

                alert("Please Select Client !!");
                d_client.focus();
                return false;
            }

            if (t_month_year.value == "") {
                alert("Please Select month & year ");
                t_month_year.focus();
                return false;
            }
           
            return true;
        }
        function pf_upload() {
            var d_client = document.getElementById('<%=ddl_pf_client.ClientID%>');
            var Selected_client = d_client.options[d_client.selectedIndex].text;
            var t_month_year = document.getElementById('<%=txt_pf_date.ClientID%>');

            if (t_month_year.value == "") {
                alert("Please Select month & year ");
                t_month_year.focus();
                return false;
            }

            if (Selected_client == "Select") {

                alert("Please Select Client !!");
                d_client.focus();
                return false;
            }

           
        }
        function pt_upload() {
            var d_client = document.getElementById('<%=ddl_pt_client.ClientID%>');
            var Selected_client = d_client.options[d_client.selectedIndex].text;
            var t_month_year = document.getElementById('<%=txt_pt_date.ClientID%>');

            if (Selected_client == "Select") {

                alert("Please Select Client !!");
                d_client.focus();
                return false;
            }

            if (t_month_year.value == "") {
                alert("Please Select month & year ");
                t_month_year.focus();
                return false;
            }

        }
        function Require_valid() {
            var Select_employee_type = document.getElementById('<%=Select_employee_type.ClientID%>');
            var Selected_Select_employee_type = Select_employee_type.options[Select_employee_type.selectedIndex].text;

            if (Selected_Select_employee_type == "Select") {
                alert("Select Type !!!");
                Select_employee_type.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function r_validation() {

            var r = confirm("Are you Sure You Want to Delete Record");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }
        function lwf_validation() {
            var Select_lwf_state = document.getElementById('<%=Select_lwf_state.ClientID%>');
            var Selected_Select_lwf_state = Select_lwf_state.options[Select_lwf_state.selectedIndex].text;
            if (Selected_Select_lwf_state == "Select") {
                alert("Please Select State");
                Select_lwf_state.focus();
                return false;
            }
            var txt_lwf_act = document.getElementById('<%=txt_lwf_act.ClientID%>');
            if (txt_lwf_act.value == "") {
                alert("Please Enter Applicablity of the LWF Act");
                txt_lwf_act.focus();
                return false;
            }
            var txt_emp_category = document.getElementById('<%=txt_emp_category.ClientID%>');
            if (txt_emp_category.value == "") {
                alert("Please Enter Category of Employees covered");
                txt_emp_category.focus();
                return false;
            }
            var ddl_contract_laobour = document.getElementById('<%=ddl_contract_laobour.ClientID%>');
            var Selected_ddl_contract_laobour = ddl_contract_laobour.options[ddl_contract_laobour.selectedIndex].text;
            if (Selected_ddl_contract_laobour == "Select") {
                alert("Please Select Applicability Contract Labours?");
                ddl_contract_laobour.focus();
                return false;
            }
            var txt_period = document.getElementById('<%=txt_period.ClientID%>');
            if (txt_period.value == "") {
                alert("Please Enter Period");
                txt_period.focus();
                return false;
            }
            var txt_last_day = document.getElementById('<%=txt_last_day.ClientID%>');
            if (txt_last_day.value == "") {
                alert("Please Enter Last Day for submission");
                txt_last_day.focus();
                return false;
            }
            var txt_e_contribution = document.getElementById('<%=txt_e_contribution.ClientID%>');
            if (txt_e_contribution.value == "") {
                alert("Please Enter Employee Contribution");
                txt_e_contribution.focus();
                return false;
            }
            var txt_c_contribution = document.getElementById('<%=txt_c_contribution.ClientID%>');
            if (txt_c_contribution.value == "") {
                alert("Please Enter Employee Contribution");
                txt_c_contribution.focus();
                return false;
            }
            var txt_monthly_charges = document.getElementById('<%=txt_monthly_charges.ClientID%>');
            if (txt_monthly_charges.value == "") {
                alert("Please Enter Monthly Charges");
                txt_monthly_charges.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            GetValues();
            return true;
        }
        function labour_ofc_validate() {
            var ddl_labour_state = document.getElementById('<%=ddl_labour_state.ClientID%>');
            var Selected_ddl_labour_state = ddl_labour_state.options[ddl_labour_state.selectedIndex].text;

            if (Selected_ddl_labour_state == "Select") {
                alert("Please Select Labour office State");
                ddl_labour_state.focus();
                return false;
            }

            var txt_labour_location = document.getElementById('<%=txt_labour_location.ClientID%>');

            if (txt_labour_location.value == "") {
                alert("Please Enter Labour Region Office");
                txt_labour_location.focus();
                return false;
            }

            var txt_labour_address = document.getElementById('<%=txt_labour_address.ClientID%>');

            if (txt_labour_address.value == "") {
                alert("Please Enter Address");
                txt_labour_address.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });
        function esic_uploading() {
            var txt_esic_amount = document.getElementById('<%=txt_esic_amount.ClientID%>');

            if (txt_esic_amount.value == "") {
                alert("Please ESIC Uploading Amount");
                txt_esic_amount.focus();
                return false;
            }
            var upd_esic_ack = document.getElementById('<%=upd_esic_ack.ClientID%>');
            if (upd_esic_ack.value == "") {
                alert("Please Ack Document Upload");
                upd_esic_ack.focus();
                return false;
            }
            var upd_esic_trrn = document.getElementById('<%=upd_esic_trrn.ClientID%>');
            if (upd_esic_trrn.value == "") {
                alert("Please TRRN Slip Document Upload");
                upd_esic_trrn.focus();
                return false;
            }
            var upd_esic_challan = document.getElementById('<%=upd_esic_challan.ClientID%>');
            if (upd_esic_challan.value == "") {
                alert("Please Challan Document Upload");
                upd_esic_challan.focus();
                return false;
            }
            var upd_esic_ecr = document.getElementById('<%=upd_esic_ecr.ClientID%>');
            if (upd_esic_ecr.value == "") {
                alert("Please ECR Document Upload");
                upd_esic_ecr.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function pf_uploading() {
            var txt_pf_amount = document.getElementById('<%=txt_pf_amount.ClientID%>');
            if (txt_pf_amount.value == "") {
                alert("Please Enter PF Amount");
                txt_pf_amount.focus();
                return false;
            }
            var upd_ack = document.getElementById('<%=upd_ack.ClientID%>');
            if (upd_ack.value == "") {
                alert("Please ack Document Upload");
                upd_ack.focus();
                return false;
            }
            var upd_trrn = document.getElementById('<%=upd_trrn.ClientID%>');
            if (upd_trrn.value == "") {
                alert("Please TRRN Document Upload");
                upd_trrn.focus();
                return false;
            }
            var upd_challan = document.getElementById('<%=upd_challan.ClientID%>');
            if (upd_challan.value == "") {
                alert("Please CHALLAN Document Upload");
                upd_challan.focus();
                return false;
            }
            var upd_ecr = document.getElementById('<%=upd_ecr.ClientID%>');
            if (upd_ecr.value == "") {
                alert("Please ECR Document Upload");
                upd_ecr.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function uan_detail_validation()
        {
            var ddl_uan_client_list = document.getElementById('<%=ddl_uan_client_list.ClientID%>');
            var Selected_ddl_uan_client_list = ddl_uan_client_list.options[ddl_uan_client_list.selectedIndex].text;

            if (Selected_ddl_uan_client_list == "ALL")
            {
                alert("Please Select Client Name");
                ddl_uan_client_list.focus();
                return false;
            }

            var ddl_uan_state_name = document.getElementById('<%=ddl_uan_state_name.ClientID%>');
            var Selected_ddl_uan_state_name = ddl_uan_state_name.options[ddl_uan_state_name.selectedIndex].text;

            if (Selected_ddl_uan_state_name == "ALL") {
                alert("Please Select State Name");
                ddl_uan_state_name.focus();
                return false;
            }
            var ddl_uan_unit_name = document.getElementById('<%=ddl_uan_unit_name.ClientID%>');
            var Selected_ddl_uan_unit_name = ddl_uan_unit_name.options[ddl_uan_unit_name.selectedIndex].text;

            if (Selected_ddl_uan_unit_name == "Select Unit") {
                alert("Please Select Unit Name");
                ddl_uan_unit_name.focus();
                return false;
            }

        }
		function GetValues() {
           
            var values = "";
            var listBox = document.getElementById("<%=lbx_month.ClientID%>");
            var hf_lwf = document.getElementById("<%=hf_lwf.ClientID%>");
            for (var i = 0; i < listBox.options.length; i++)
            {
               
            if (listBox.options[i].selected) {
                values += listBox.options[i].value+",";
              
            }
            }
            //alert(values);
            values = values.substring(0, values.length - 1);
           // alert(values);
            hf_lwf.value = values;
            //alert(hf_lwf);
        return true;
        }
        function lwf_report_validation()
        {
            var txt_lwf_month = document.getElementById('<%= txt_lwf_month.ClientID%>');
            if (txt_lwf_month.value == "")
            {
                alert("Please Select Month");
                txt_lwf_month.focus();
                return false;
            }

            var ddl_lwf_state = document.getElementById('<%=ddl_lwf_state.ClientID%>');
            var Selected_ddl_lwf_state = ddl_lwf_state.options[ddl_lwf_state.selectedIndex].text;

            if (Selected_ddl_lwf_state == "Select") {
                alert("Please Select State");
                ddl_lwf_state.focus();
                return false;
            }
        }
        function Bonus_val() {
            var ddl_bclient = document.getElementById('<%=ddl_bclient.ClientID%>');
            var Selected_ddl_bclient = ddl_bclient.options[ddl_bclient.selectedIndex].text;
            

            if (Selected_ddl_bclient == "Select") {

                alert("Please Select Client !!");
                ddl_bclient.focus();
                return false;
            }
            var txt_work_img_from = document.getElementById('<%= txt_work_img_from.ClientID%>');
            if (txt_work_img_from.value == "") {
                alert("Please Select From Month");
                txt_work_img_from.focus();
                return false;
            }
            var txt_work_img_to = document.getElementById('<%= txt_work_img_to.ClientID%>');
            if (txt_work_img_to.value == "") {
                alert("Please Select To Month");
                txt_work_img_to.focus();
                return false;
            }
            return true;
        }
        function esic_upload() {
          
           
            var txt_esic_date = document.getElementById('<%=txt_esic_date.ClientID%>');

            if (txt_esic_date.value == "") {
                alert("Please Select month & year ");
                txt_esic_date.focus();
                return false;
            }
            var state = document.getElementById('<%=ddl_esic_state.ClientID%>');
            var Selected_state = state.options[state.selectedIndex].text;
           

            if (Selected_state == "Select") {
                alert("Please Select state  !!!");
                state.focus();
                return false;

            }

           
            return true;
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
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Taxes</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20"
                                ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />

            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Taxes Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>

    <br />
            <div class="panel-body">
                  <div id="tabs" style="background: #f3f1fe; border-radius: 10px; padding:20px 20px 20px 20px; border: 1px solid white">
                       <asp:HiddenField ID="hf_lwf" runat="server" />

                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                    <li><a href="#home"><b>PT-Slab Master</b></a></li>
                    <li><a href="#menu1" runat="server"><b>PF</b></a></li>
                    <li><a href="#menu2" runat="server"><b>ESIC</b></a></li>
                    <li><a href="#menu3" runat="server"><b>LWF</b></a></li>
                    <li><a href="#menu4" runat="server"><b>ESIC Uploading</b></a></li>
                    <li><a href="#menu5" runat="server"><b>PF Uploading</b></a></li>
                    <li><a href="#menu6" runat="server"><b>PT Uploading</b></a></li>
                    <li><a  href="#menu7" runat="server"><b>Labour Office</b></a></li>
                    <li><a  href="#menu8" runat="server"><b>UAN Number Details</b></a></li>
                    <li><a id="A1"  href="#menu9" runat="server"><b>Clientwise PF-ESIC</b></a></li>
                    <li><a  href="#menu10" runat="server"><b>Bonus/Leave/Gratuity Reports</b></a></li>
                </ul>
                    <div id="home">
                        <br />
                        <asp:UpdatePanel id="up1" runat="server">
                            <ContentTemplate>
                                <br />
                                
                            
                <div class="row">

                    <div class="col-sm-2">
                        <b>State :</b><span style="color:red">*</span>

                <asp:DropDownList ID="ddl_state" runat="server"  CssClass="form-control text_box">
                    <asp:ListItem Value="0">Select State</asp:ListItem>
                </asp:DropDownList>
                       

                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> From Amount :</b>
                    <span class="text-red"> *</span>
                        <asp:TextBox ID="txt_fromamount" runat="server" onkeypress="return isNumber_dot(event)" CssClass="form-control text_box " MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> To Amount :</b><span class="text-red"> *</span>
                        <asp:TextBox ID="txt_toamount" runat="server" onkeypress="return isNumber_dot(event)" CssClass="form-control text_box " MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Slab Amount :</b><span class="text-red"> *</span>
                        <asp:TextBox ID="txt_slabamount" runat="server" onkeypress="return isNumber_dot(event)" CssClass="form-control text_box " MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-sm-2">
                     <b> From Month :</b><span class="text-red"> *</span>

                        <div>
                <asp:DropDownList ID="ddl_from_month" runat="server" CssClass="form-control text_box" Width="155px" >
                    <asp:ListItem Value="0">Select Month</asp:ListItem>
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
                            </div>
                    </div>
                    <div class="col-sm-2">
                     <b> To Month :</b><span class="text-red"> *</span>

                       <div>
                <asp:DropDownList ID="ddl_to_month" runat="server" CssClass="form-control text_box" Width="155px">
                    <asp:ListItem Value="0">Select Month</asp:ListItem>
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
                            </div>
                    </div>
                    </div>
                                
                                <br />
                                 <br />
                                <br />
                                <div class="row text-center">
                                    <asp:Button ID="btn_add" runat="server" class="btn btn-primary" OnClick="btn_add_Click"
                                        Text=" Save " OnClientClick="return Req_validation();" />
                                    <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" Text=" Clear " OnClick="btn_clear_data" />
                                    <asp:Button ID="btn_edit" runat="server" class="btn btn-primary"
                                        OnClick="btn_edit_Click" Text=" Update " OnClientClick="return Req_validation();" />
                                    <asp:Button ID="btn_delete" runat="server" class="btn btn-primary" Text=" Delete "
                                        OnClick="btn_delete_Click" OnClientClick="return Req_validation1();" />
                                    <asp:Button ID="btnexporttoexcelptslab" runat="server" class="btn btn-primary"
                                        Text="Export TO Excel" OnClick="btnexporttoexcelptslab_Click" />
                                    <asp:Button ID="btnclose" runat="server" class="btn btn-danger"
                                        OnClick="btnclose_Click" Text="Close" />

                </div>
                        
                                <br />
                                
                                <div class="container-fluid" style="background: white; border-radius: 10px; margin-top:20px; border: 1px solid white; padding:20px 20px 20px 20px">
                                <asp:Panel ID="Panel2" runat="server" class="grid-view">
                                    <asp:GridView ID="PTSlabGridView" class="table" runat="server" OnPreRender="PTSlabGridView_PreRender"
                                        AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" Font-Size="X-Small"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                        OnSelectedIndexChanged="PTSlabGridView_SelectedIndexChanged"
                                        OnRowDataBound="PTSlabGridView_RowDataBound" DataKeyNames="ID" Width="100%">
                                        <RowStyle ForeColor="#000066" />
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID"
                                                SortExpression="ID" />
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="State Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_STATE_NAME" runat="server" Text='<%# Eval("STATE_NAME")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="From Amount">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_FROM_AMOUNT" runat="server" Text='<%# Eval("FROM_AMOUNT")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="To Amount">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_TO_AMOUNT" runat="server" Text='<%# Eval("TO_AMOUNT")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Slab Amount">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_SLAB_AMOUNT" runat="server" Text='<%# Eval("SLAB_AMOUNT")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="From Month">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <%# Eval("FROM_MONTH")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="To Month">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <%# Eval("TO_MONTH")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                               </div>
                                </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                    <div id="menu1">
                        <br />
                       <asp:UpdatePanel id="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                  <div class="row">
                                       <div class="col-sm-2 col-xs-12">
                                           <b>Select Type :</b><span style="color:red">*</span>
                                       <asp:DropDownList ID="Select_employee_type" runat="server" Width="100%"  CssClass="form-control text_box">
                                            <asp:ListItem Value="Select" Text="Select">Select</asp:ListItem>
                                            <asp:ListItem Value="Employer" Text="Employer">Employer</asp:ListItem>
                                            <asp:ListItem Value="Employee" Text="Employee">Employee</asp:ListItem>
                </asp:DropDownList>
                                           </div>
                                       <div class="col-sm-2 col-xs-12">
                       <b> PF Account :</b>
                  <asp:TextBox ID="txt_pf_account" runat="server" onKeypress="return isNumber_dot(event)" CssClass="form-control text_box "  MaxLength="10" onchange="return addition_all_textbox()"></asp:TextBox>
                    </div>
                                       <div class="col-sm-2 col-xs-12">
                      <b> Pension Account :</b>
                  <asp:TextBox ID="txt_pension_account" runat="server" onKeypress="return isNumber_dot(event)" CssClass="form-control text_box "  MaxLength="10" onchange="return addition_all_textbox()"></asp:TextBox>
                    </div>
                                       <div class="col-sm-2 col-xs-12">
                      <b> Admin Charge :</b>
                    <asp:TextBox ID="txt_admin_charge" runat="server" onKeypress="return isNumber_dot(event)" CssClass="form-control text_box " MaxLength="10" onchange="return addition_all_textbox()"></asp:TextBox>
                    </div>
                                       <div class="col-sm-2 col-xs-12">
                       <b> EDLI :</b>
                   <asp:TextBox ID="txt_edli" runat="server" onKeypress="return isNumber_dot(event)" CssClass="form-control text_box "  MaxLength="10" onchange="return addition_all_textbox()"></asp:TextBox>
                    </div>
                                 
                                  </div><br />
                                <div class="row">
                                   
                                      <div class="col-sm-2 col-xs-12">
                        <b>Total :</b>
                  <asp:TextBox ID="txt_total" runat="server"  onKeypress="return isNumber_dot(event)" CssClass="form-control text_box " MaxLength="10"></asp:TextBox>
                    </div>   
                                    <div class="col-sm-2 col-xs-12" style="display:none"> 
                                     <asp:TextBox ID="txt_srno_pf" runat="server"   CssClass="form-control text_box " MaxLength="10"></asp:TextBox>
                                        </div>
                                     <div class="col-sm-2 col-xs-12">
                                     <asp:LinkButton ID="add_employee_type" runat="server" OnClick="lnkbtn_addmoreitem_Click" Visible="false">
                                      <img alt="Add Item" src="Images/add_icon.png" style="margin-top:16px;"  />
                                        </asp:LinkButton>
                                        </div>
                                    </div>
                                <br />
                                 
                         <div class="row text-center">
                    <asp:Button ID="pf_save" runat="server" class="btn btn-primary" OnClick="btn_pf_save_Click"
                        Text=" Save " OnClientClick="return Require_valid();" />
                   
                    <asp:Button ID="pf_update" runat="server" class="btn btn-primary"
                        OnClick="btn_pf_update_Click" Text=" Update " OnClientClick="return Require_valid();" />
                    <asp:Button ID="pf_delete" runat="server" class="btn btn-primary" Text=" Delete "
                        OnClick="btn_deletepf_Click" OnClientClick="return r_validation();" />
                    <asp:Button ID="pf_close" runat="server" class="btn btn-danger"
                        OnClick="btnclose_Click" Text="Close" />

                </div>
                        <br /><br />  
                       <asp:Panel ID="Panel1" runat="server">
                           <div class="container-fluid" style="background: white; border-radius: 10px; margin-top:20px; border: 1px solid white; padding:20px 20px 20px 20px">
                                            <asp:Panel ID="Panel4" runat="server" CssClass="grid-view">

                                                <asp:GridView ID="gv_pfdetails" class="table" runat="server"
                                                        AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" Font-Size="X-Small"
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="1" ScrollBars="both"
                                                        OnSelectedIndexChanged="gv_pfdetails_selected_Index"
                                                        OnRowDataBound="gv_pfdetails_RowDataBound" OnPreRender="gv_pfdetails_PreRender" Width="100%">

                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />

                                                    
                      

                                                    <Columns>
                                                       
                                                        <asp:TemplateField HeaderText="Sr No.">
                                                            <ItemStyle  />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Eval("SR_NO")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
                                                        <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
                                                        <asp:BoundField DataField="pf_account" HeaderText="PF_ACCOUNT" SortExpression="pf_account" />
                                                        <asp:BoundField DataField="pension_account" HeaderText="PENSION_ACCOUNT" SortExpression="pension_account" />
                                                        <asp:BoundField DataField="admin_charge" HeaderText="ADMIN_CHARGES" SortExpression="admin_charge" />
                                                        <asp:BoundField DataField="edil_chares" HeaderText="EDIL_CHARGES" SortExpression="edil_chares" />
                                                        <asp:BoundField DataField="Total" HeaderText="TOTAL" SortExpression="Total" />

                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                               </div>
                                        </asp:Panel>
                                 
                              
                               
                            
                   
                                </ContentTemplate>
                           </asp:UpdatePanel>
                     </div>
                    <div id="menu2">
                        <br />
                       <asp:UpdatePanel id="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                  <div class="row">
                              </div>
                           
                      <br />
                      <br />
                         <div class="row text-center">
                    <asp:Button ID="esic_save" runat="server" class="btn btn-primary" OnClick="btn_add_Click"
                        Text=" Save " />
                    <asp:Button ID="esic_clear" runat="server" class="btn btn-primary" Text=" Clear " OnClick="btn_clear_data" />
                    <asp:Button ID="esic_update" runat="server" class="btn btn-primary"
                        OnClick="btn_edit_Click" Text=" Update " />
                    <asp:Button ID="esic_delete" runat="server" class="btn btn-primary" Text=" Delete "
                        OnClick="btn_delete_Click" />
                    <asp:Button ID="esic_close" runat="server" class="btn btn-danger"
                        OnClick="btnclose_Click" Text="Close" />

                </div>
                       
                                </ContentTemplate>
                           </asp:UpdatePanel>
                       </div>
                    <div id="menu3">
                        <br />
                      
                                  <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                           <b>State :</b>
                                       <asp:DropDownList ID="Select_lwf_state" runat="server" Width="100%" CssClass="form-control text_box">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            
                </asp:DropDownList>
                                           </div>
                                        <div class="col-sm-2 col-xs-12">
                          <b>  Applicablity of the LWF Act :</b>
                            <asp:TextBox ID="txt_lwf_act" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"  onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                        </div> 
                                       <div class="col-sm-2 col-xs-12" style="width:18%">
                           <b> Category of Employees covered:</b>
                            <asp:TextBox ID="txt_emp_category" runat="server" class="form-control"  TextMode="MultiLine" Rows="3"  onkeypress="return AllowAlphabet_address(event)" ></asp:TextBox>
                        </div> 
                                        <div class="col-sm-2 col-xs-12" style="width:17%">
                          <b>  Applicability Contract Labours? :</b>
                            <asp:DropDownList ID="ddl_contract_laobour" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0" Text="No">No</asp:ListItem>
                                <asp:ListItem Value="1" Text="Yes">Yes</asp:ListItem>
                            </asp:DropDownList>
                        </div> 
                                          <div class="col-sm-2 col-xs-12">
                            <b> Period :</b>
                            <asp:TextBox ID="txt_period" runat="server" CssClass="form-control text-box" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                        </div>
                                    
                                  </div>
                                <br />
                                <div class="row">
                                       <div class="col-sm-2 col-xs-12">
                            <b> Last Day for submission :</b>
                            <asp:TextBox ID="txt_last_day" runat="server" CssClass="form-control"  onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                        </div>
                                      <div class="col-sm-2 col-xs-12">
                            <b> Employee Contribution :</b>
                            <asp:TextBox  ID="txt_e_contribution" runat="server" CssClass="form-control txt_empe" onKeypress="return isNumber_dot(event)"></asp:TextBox>
                        </div>
                                    <div class="col-sm-2 col-xs-12">
                             <b>Employer Contribution :</b>
                            <asp:TextBox ID="txt_c_contribution"  runat="server" CssClass="form-control txt_empr" onKeypress="return isNumber_dot(event)"></asp:TextBox>
                                         </div>
                                     <div class="col-sm-2 col-xs-12">
                                         <b>Monthly Charges : </b>
                                           <asp:TextBox ID="txt_monthly_charges" runat="server" onKeypress="return isNumber_dot(event)" CssClass="form-control text_box" MaxLength="10"></asp:TextBox>
                                     </div>
                                     <div class="col-sm-3 col-xs-12" >
                                       <b>  submission Month:</b>
                                        <asp:ListBox ID="lbx_month" runat="server" CssClass="form-control" SelectionMode="Multiple" OnClientClick="return GetValues()">
                                            
                                            <asp:ListItem Value="JAN"></asp:ListItem>
                                            <asp:ListItem Value="FEB"></asp:ListItem>
                                            <asp:ListItem Value="MAR"></asp:ListItem>
                                            <asp:ListItem Value="APR"></asp:ListItem>
                                            <asp:ListItem Value="MAY"></asp:ListItem>
                                            <asp:ListItem Value="JUN"></asp:ListItem>
                                            <asp:ListItem Value="JULY"></asp:ListItem>
                                            <asp:ListItem Value="AUG"></asp:ListItem>
                                            <asp:ListItem Value="SEP"></asp:ListItem>
                                            <asp:ListItem Value="OCT"></asp:ListItem>
                                            <asp:ListItem Value="NOV"></asp:ListItem>
                                            <asp:ListItem Value="DEC"></asp:ListItem>
                                        </asp:ListBox>
                                    </div>
                                  <div class="col-sm-2 col-xs-12">
                                    <asp:LinkButton ID="add_lwf" runat="server" OnClick="lnkbtn_add_lwfdetails_Click" Visible="false" >
                                      <img alt="Add Item" src="Images/add_icon.png" style="margin-left:10px;margin-top:16px"/>
                                        </asp:LinkButton>
                                        </div>
                               
                                </div>
                        <br />
                        <br />
                                <div class="row">
                                     <div class="col-sm-2 col-xs-12">
                                       <b> Select Month :</b><span class="text-red">*</span>
                                        <asp:TextBox ID="txt_lwf_month" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                    </div>
                                     <div class="col-sm-2 col-xs-12">
                                          <b> State :</b>
                                       <asp:DropDownList ID="ddl_lwf_state" runat="server" Width="100%" CssClass="form-control text_box">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                         </div>
                                   <div class="col-sm-1 col-xs-12">
                                        <asp:Button ID="btn_lwf_excel" runat="server" class="btn btn-large" Width="100%" style="margin-top: 19px;"  Text="Get Excel" OnClick="btn_lwf_excel_Click" OnClientClick="return lwf_report_validation();"/>
                                    </div>

                                     <div class="col-sm-1 col-xs-12">
                                        <asp:Button ID="btn_report" runat="server" class="btn btn-primary" style="margin-top: 19px;" OnClick="btn_report_Click" Text="Report" OnClientClick="return lwf_report_validation();"/>
                                    </div>
                                </div>  
                                                       <br /><br />
                             <div class="row text-center">
                    <asp:Button ID="lwf_save" runat="server" class="btn btn-primary" OnClick="btn_lwfsave_Click"
                        Text=" Save " OnClientClick="return lwf_validation();" />
                   
                    <asp:Button ID="lwf_update" runat="server" class="btn btn-primary"
                        OnClick="btn_lwfedit_Click" Text=" Update" OnClientClick="return lwf_validation();" />
                    <asp:Button ID="lwf_delete" runat="server" class="btn btn-primary" Text=" Delete "
                        OnClick="btn_deletelwf_Click" OnClientClick="return r_validation();"/>
                  
                    <asp:Button ID="lwf_close" runat="server" class="btn btn-danger"
                        OnClick="btnclose_Click" Text="Close" />

                                </div>

                                <br />
                                 
                                    <asp:Panel ID="Panel5" runat="server" >
                                        <div class="" style="overflow-y:auto">
                                            <div class="container-fluid" style="background: white; border-radius: 10px; margin-top:20px; border: 1px solid white; padding:20px 20px 20px 20px">
                                            <asp:Panel ID="Panel6" runat="server" CssClass="grid-view" >

                                                <asp:GridView ID="gv_lwfdetails" class="table" runat="server" BackColor="White"
                                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                    OnRowDataBound="gv_lwfdetails_RowDataBound" OnSelectedIndexChanged="gv_lwfdetails_selected_Index_Change"
                                                    AutoGenerateColumns="False" OnPreRender="gv_lwfdetails_PreRender" >

                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />

                                                    <Columns>
                                                        <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" />
                                                        <asp:BoundField DataField="state_name" HeaderText="STATE" SortExpression="state_name" />
                                                        <asp:BoundField DataField="app_LWF_act" HeaderText="LWF_ACT" SortExpression="app_LWF_act" />
                                                        <asp:BoundField DataField="category_employees" HeaderText="CATEGORY" SortExpression="category_employees" />
                                                        <asp:BoundField DataField="contract_labours" HeaderText="CONTRACT_LABOURS" SortExpression="contract_labours" />
                                                        <asp:BoundField DataField="period" HeaderText="PERIOD" SortExpression="period" />
                                                        <asp:BoundField DataField="last_day_submission" HeaderText="LAST_DAY_SUBMISSION" SortExpression="last_day_submission" />
                                                        <asp:BoundField DataField="catagory" HeaderText=" MONTH_SUBMISSION" SortExpression="catagory" />
                                                        <asp:BoundField DataField="employee_contribution" HeaderText="EMPLOYEE_CONTRIBUTION" SortExpression="employee_contribution" />
                                                        <asp:BoundField DataField="employer_contribution" HeaderText="EMPLOYER_CONTRIBUTION" SortExpression="employer_contribution" />
                                                       

                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                                </div>
                                            </div>
                                        </asp:Panel>
                         
                            
                    </div>
                    <div id="menu4">
                        <div class="row">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <%-- <div class="col-sm-2 col-xs-12">
                                           Client Name :<span class="text-red">*</span>
                        <asp:DropDownList ID="ddl_esic_client"  runat="server" CssClass="form-control"  OnSelectedIndexChanged="ddl_esic_client_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                                           </div>--%>
                                    <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                       <b> State :</b><span class="text-red">*</span>
                                        <asp:DropDownList ID="ddl_esic_state" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <%--<div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                            Branch Name :
                            <asp:DropDownList ID="ddl_esic_unit" DataValueField="unit_code"  DataTextField="unit_name" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>--%>
                                    <div class="col-sm-2 col-xs-12">
                                      <b>  Select Month :</b><span class="text-red">*</span>
                                        <asp:TextBox ID="txt_esic_date" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                      <b>  Report Name :</b><span class="text-red">*</span>
                                        <asp:DropDownList ID="ddl_report_esic" runat="server" CssClass="form-control" AutoPostBack="true" >
                                               <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                                           <asp:ListItem Text="ESIC_PAID_REPORT" Value="1"></asp:ListItem>
                                           <asp:ListItem Text="ESIC_UNPAID_REPORT" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
<asp:Button ID="btn_esic_statement" runat="server" CssClass="btn btn-large"
                            Text="ESIC STATEMENT" OnClick="btn_esic_statement_Click" OnClientClick="return esic_upload();" />&nbsp;&nbsp;
                            <asp:Button ID="btn_esic_upload" runat="server" CssClass="btn btn-large"
                                Text="ESIC UPLOAD" OnClick="btn_esic_upload_Click" OnClientClick="return esic_upload();" />&nbsp;&nbsp;
                                 
                               <asp:Button ID="btn_esic_uload" runat="server" CssClass="btn btn-large"
                                   Text="SYS UPLOAD" OnClick="btn_esic_uload_Click" OnClientClick="return esic_upload();" />

                            <asp:Button ID="btn_esic_excel" runat="server" CssClass="btn btn-large"
                                   Text="ESIC Excel" OnClick="btn_esic_excel_Click" OnClientClick="return esic_upload();" Visible="false"/>

                             <asp:Button ID="btn_report_esic" runat="server" CssClass="btn btn-primary"
                                   Text="Report" OnClick="btn_report_esic_Click" OnClientClick="return esic_upload();" />
                        </div>
                        <br />
                        <asp:Panel ID="pnl_esic" runat="server" CssClass="grd_company panel panel-primary">
                            <div class="container-fluid" style="background:#d5d4fb;border-bottom:1px solid gray;">
                            <div class="row text-center">
                                        <h3>Upload Files</h3>
                                    </div>
                                </div>
                            <br />
                            <div class="row" style="margin-left:1em;">
                                
                                <div class="col-sm-2 col-xs-12">
                                  <b>  Amount :</b>
                                          <asp:TextBox ID="txt_esic_amount" CssClass="form-control" runat="server" onkeypress="return isNumber_dot(event)" />
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Ack :</b>
                                          <asp:FileUpload ID="upd_esic_ack" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                  <b>  TRRN Slip :</b>
                                          <asp:FileUpload ID="upd_esic_trrn" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Challan :</b>
                                          <asp:FileUpload ID="upd_esic_challan" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> ECR :</b>
                                          <asp:FileUpload ID="upd_esic_ecr" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <asp:Button ID="btn_esic_sys_upload" runat="server" CssClass="btn btn-primary" Text="Upload" OnClick="btn_esic_sys_upload_Click" OnClientClick="return esic_uploading();" />
                                </div>
                            </div>
                            <br />
                            <div class="container-fluid">
                            <asp:Panel ID="Panel9" runat="server" CssClass="grd_company">
                                <asp:GridView ID="grd_esic_upload" class="table" runat="server" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    OnRowDeleting="grd_esic_upload_RowDeleting" AutoGenerateColumns="False" DataKeyNames="id" Width="100%" OnPreRender="grd_esic_upload_PreRender">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_srnumber" runat="server"
                                                    Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id" HeaderText="ID" />
                                        <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                        <asp:TemplateField HeaderText="Download ACK">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload1" Text="Download" CommandArgument='<%# Eval("Value1") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Download TRRN">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload2" Text="Download" CommandArgument='<%# Eval("Value2") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Download Challan">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload3" Text="Download" CommandArgument='<%# Eval("Value3") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Download ECR">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload4" Text="Download" CommandArgument='<%# Eval("Value4") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="month" HeaderText="Month"
                                            SortExpression="month" />
                                        <asp:BoundField DataField="year" HeaderText="Year"
                                            SortExpression="year" />
                                          <asp:TemplateField HeaderText="Download Challan">
                                            <ItemTemplate>
                                                <asp:Button Text="Delete" runat="server" CssClass="btn btn-primary" CommandName="Delete" OnRowDataBound="grd_esic_upload_RowDataBound" OnClientClick="return r_validation();"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                                </div>
                            <br />
                        </asp:Panel>
                    </div>
                    <div id="menu5">
                        <div class="row">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-2 col-xs-12">
                                        <b>Client Name :</b><span class="text-red">*</span>
                                        <asp:DropDownList ID="ddl_pf_client" runat="server" CssClass="form-control" />
                                    </div>
                                    <%-- <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                            State :<span class="text-red">*</span>
                            <asp:DropDownList ID="ddl_pf_state" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_pf_state_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                            Branch Name :
                            <asp:DropDownList ID="ddl_pf_unit" DataValueField="unit_code"  DataTextField="unit_name" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                                    --%>
                                    <div class="col-sm-2 col-xs-12">
                                        <b>Select Month :</b><span class="text-red">*</span>
                                        <asp:TextBox ID="txt_pf_date" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                    </div>
                                     <div class="col-sm-2 col-xs-12">
                                       <b> Report Name :</b><span class="text-red">*</span>
                                        <asp:DropDownList ID="ddl_report" runat="server" CssClass="form-control" AutoPostBack="true" >
                                               <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                                           <asp:ListItem Text="PF_PAID_REPORT" Value="1"></asp:ListItem>
                                           <asp:ListItem Text="PF_UNPAID_REPORT" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                                   </asp:UpdatePanel>
                                   <br />
                                   <asp:Button ID="btn_pf_upload" runat="server" CssClass="btn btn-large"
                            Text="PF STATEMENT" OnClick="btn_pf_upload_Click" OnClientClick="return pf_upload();" />&nbsp;&nbsp;
                                   <asp:Button ID="btn_pf_upload_count" runat="server" CssClass="btn btn-large"
                                       Text="PF UPLOAD" OnClick="btn_pf_upload_count_Click" OnClientClick="return pf_upload();" />&nbsp;&nbsp;
                                   <asp:Button ID="btn_sys_upload" runat="server" CssClass="btn btn-large"
                                       Text="SYS UPLOAD" OnClick="btn_sys_upload_Click" OnClientClick="return pf_upload_sys();" />
                            <asp:Button ID="btn_pf_excel" runat="server" CssClass="btn btn-large"
                                       Text="PF Excel " OnClick="btn_pf_excel_sheet" OnClientClick="return pf_upload_sys();" Visible="false"/>
                              <asp:Button ID="Report" runat="server" CssClass="btn btn-large"
                                       Text="Report " OnClick="Report_Click" OnClientClick="return pf_upload();"  />
                        </div>
                        <br />
                                  
                        <asp:Panel ID="pnl_sys_upload" runat="server" CssClass="grd_company panel panel-primary">
                            <div class="container-fluid"  style="background:#d5d4fb;border-bottom:1px solid gray;">
                              <div class="row text-center">
                                        <h3>Upload Files</h3>
                                    </div>
                                </div>
                           <br />
                            <div class="row" style="margin-left:1em;">
                                <div class="col-sm-2 col-xs-12">
                                   <b> Amount :</b>
                                          <asp:TextBox ID="txt_pf_amount" CssClass="form-control" runat="server" onkeypress="return isNumber_dot(event)" />
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                  <b>  Ack :</b>
                                          <asp:FileUpload ID="upd_ack" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                  <b>  TRRN Slip :</b>
                                          <asp:FileUpload ID="upd_trrn" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Challan :</b>
                                          <asp:FileUpload ID="upd_challan" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                 <b>   ECR :</b>
                                          <asp:FileUpload ID="upd_ecr" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <asp:Button ID="btn_upload_pf" runat="server" CssClass="btn btn-primary" Text="Upload" OnClick="btn_upload_pf_Click" OnClientClick="return pf_uploading();" />
                                </div>
                            </div>
                              <br />
                  
                              <div class="container-fluid">
                            <asp:Panel ID="Panel7" runat="server" CssClass="grd_company">
                                <asp:GridView ID="grd_company_files" class="table" runat="server" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    OnRowDataBound="grd_company_files_RowDataBound" OnRowDeleting="grd_company_files_RowDeleting" AutoGenerateColumns="False" DataKeyNames="id" Width="100%" OnPreRender="grd_company_files_files_PreRender">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_srnumber" runat="server"
                                                    Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id" HeaderText="ID" />
                                        <asp:BoundField DataField="Client_name" HeaderText="Client Name" SortExpression="Client_name" />
                                        <asp:TemplateField HeaderText="Download ACK">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload1" Text="Download" CommandArgument='<%# Eval("Value1") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Download TRRN">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload2" Text="Download" CommandArgument='<%# Eval("Value2") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Download Challan">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload3" Text="Download" CommandArgument='<%# Eval("Value3") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Download ECR">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload4" Text="Download" CommandArgument='<%# Eval("Value4") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="month" HeaderText="Month"
                                            SortExpression="month" />
                                        <asp:BoundField DataField="year" HeaderText="Year"
                                            SortExpression="year" />
                                          <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btndelete" Text="Delete" runat="server" class="btn btn-primary" CommandName="Delete" OnClientClick="return r_validation();"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                            </asp:Panel>
                                  </div>
                             </asp:Panel>
                             <%--  </ContentTemplate>
                            </asp:UpdatePanel>
                      --%>
                    </div>
                    <div id="menu6">
                        <div class="row">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                      <div class="col-sm-2 col-xs-12">
                            <b>Select Month :</b><span class="text-red">*</span>
                            <asp:TextBox ID="txt_pt_date" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                        </div>
                                    <div class="col-sm-2 col-xs-12">
                                      <b>  Client Name :</b><span class="text-red">*</span>
                                        <asp:DropDownList ID="ddl_pt_client" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_pt_client_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                       <b> State :</b><span class="text-red">*</span>
                                        <asp:DropDownList ID="ddl_pt_state" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_pt_state_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                       <b> Branch Name :</b>
                            <asp:DropDownList ID="ddl_pt_unit" DataValueField="unit_code" DataTextField="unit_name" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                                 
                                </ContentTemplate>
                                   </asp:UpdatePanel>
                                   <br />
                                   <asp:Button ID="btn_pt" runat="server" CssClass="btn btn-large"
                            Text="PT UPLOAD" OnClick="btn_pt_Click" OnClientClick="return pt_upload();" />&nbsp;&nbsp;
                               </div>
                     </div>
                    <div id="menu7" >
                        <br />
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                               <b> State:</b> 
                                <asp:DropDownList runat="server" ID="ddl_labour_state" CssClass="form-control">
                                   
                                </asp:DropDownList>
                            </div> 
                            <div class="col-sm-2 col-xs-12">
                                <b>Labour Regional Office:</b>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txt_labour_location" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                            </div> 
                              <div class="col-sm-2 col-xs-12">
                              <b>  Address:</b>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txt_labour_address" TextMode="MultiLine" Rows="3" Columns="3" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                            </div> 
                              <div class="col-sm-4 col-xs-12" style="margin-top:4em">
                                  <asp:Button runat="server" ID="btn_labour_save" CssClass="btn btn-primary" Text="Save" OnClientClick="return labour_ofc_validate()" OnClick="btn_labour_save_Click"  /> 
                                  <asp:Button runat="server" ID="btn_update" CssClass="btn btn-primary" Text="Update"  OnClick="btn_update_Click" OnClientClick="return labour_ofc_validate()"  /> 
                                   <asp:Button runat="server" ID="btn_delete1" CssClass="btn btn-primary" Text="Delete"  OnClick="lnkbtn_removeitem_Click1" OnClientClick="return r_validation();" /> 
                                  <asp:Button runat="server" ID="btn_labour_close" CssClass="btn btn-danger" Text="Close"  OnClick="btn_labour_close_Click"/> 
                               </div> 
                        </div>
                        <br />
                        <asp:Panel runat="server" CssClass="grid-view" ID="panel11">
                             <asp:GridView ID="gv_labour_office" class="table" runat="server" BackColor="White"  AutoGenerateColumns="false"  DataKeyNames="id" OnRowDataBound="gv_labour_office_RowDataBound"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" OnPreRender="gv_labour_office_PreRender" OnSelectedIndexChanged="gv_labour_office_SelectedIndexChanged">
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <Columns>
                                   
                                    <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" />
                                     <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                     <asp:BoundField DataField="city" HeaderText="Labour Regional Office" SortExpression="city" />
                                     <asp:BoundField DataField="address" HeaderText="Address" SortExpression="address" />
                                </Columns>
                                </asp:GridView>
                        </asp:Panel>
                                </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                       <div id="menu8" >
                        <br />
                       
                        <div class="row">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <div class="col-sm-2 col-xs-12">
                               <b> Client Name </b>
                                <asp:DropDownList runat="server" ID="ddl_uan_client_list" CssClass="form-control"
                                    OnSelectedIndexChanged="ddl_uan_client_SelectedIndexChanged" AutoPostBack="true">
                                   
                                </asp:DropDownList>
                            </div> 
                            <div class="col-sm-2 col-xs-12">
                               <b> State Name </b>
                                <asp:DropDownList runat="server" ID="ddl_uan_state_name" CssClass="form-control"
                                    OnSelectedIndexChanged="ddl_uan_state_SelectedIndexChanged" AutoPostBack="true">
                                   
                                </asp:DropDownList>'
                            </div> 
                             <div class="col-sm-2 col-xs-12">
                               <b> Unit Name</b>
                                <asp:DropDownList runat="server" ID="ddl_uan_unit_name" CssClass="form-control">
                                   
                                </asp:DropDownList>'
                            </div>
                            </ContentTemplate>
                                </asp:UpdatePanel>
                              <div class="col-sm-4 col-xs-12" style="margin-top:1.5em">
                                  <asp:Button runat="server" ID="btn_uan_exceldownload" CssClass="btn btn-large" Text="UAN Excel Download" Width="40%"  OnClick="btn_uan_exceldownload_click" OnClientClick="return uan_detail_validation();" /> 
                                  <asp:Button runat="server" ID="btn_uan_cvcdownload" CssClass="btn btn-large" Text="UAN CVC Download"  OnClick="btn_uan_cvcdownload_click" OnClientClick="return uan_detail_validation();"  /> 
                                  <asp:Button runat="server" ID="btn_close" CssClass="btn btn-danger" Text="Close"  OnClick="btn_labour_close_Click"/> 
                               </div> 
                        </div>
                        <br />
                       
                                
                    </div>

                      <div id="menu9">
                          <br />
                       <div class="row">

                            <div class="col-sm-2 col-xs-12">
                                           <b>Select Type :</b><span style="color:red">*</span>
                                       <asp:DropDownList ID="ddl_type_pf_esic" runat="server" Width="100%" OnSelectedIndexChanged="ddl_type_pf_esic_SelectedIndexChanged" CssClass="form-control text_box" AutoPostBack="true">
                                            <asp:ListItem Value="Select" Text="Select">Select</asp:ListItem>
                                            <asp:ListItem Value="PF" Text="PF">PF</asp:ListItem>
                                            <asp:ListItem Value="ESIC" Text="ESIC">ESIC</asp:ListItem>
                </asp:DropDownList>
                                           </div>

                           <div class="col-sm-2 col-xs-12">
                                       <b> Client Name :</b><span class="text-red">*</span>
                                        <asp:DropDownList ID="ddl_clientname_pf_esic" runat="server" CssClass="form-control" />
                                    </div>

                            <div class="col-sm-2 col-xs-12">
                                      <b>  Select Month :</b><span class="text-red">*</span>
                                        <asp:TextBox ID="txt_month_pf_esic" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                    </div>
                            <div class="col-sm-4 col-xs-12">
                           <asp:Button ID="btn_pf_sheet" runat="server" CssClass="btn btn-primary"
                                       Text="PF Excel " OnClick="btn_pf_sheet_Click" OnClientClick="return"/>
                               </div>
                             <div class="col-sm-4 col-xs-12">
                            <asp:Button ID="btn_esic_sheet" runat="server" CssClass="btn btn-primary"
                                   Text="ESIC Excel" OnClick="btn_esic_sheet_Click" OnClientClick="return"/>
                             </div>
                           </div>
                          </div>


                       <div id="menu10" >
                        <br />
                       
                        <div class="row">
                             <div class="col-sm-2 col-xs-12">
                                <b>Type : </b>
                                <asp:DropDownList runat="server" ID="ddl_type" CssClass="form-control">
                                    <asp:ListItem Value="Bonus">Bonus</asp:ListItem>
                                    <asp:ListItem Value="Leave">Leave</asp:ListItem>
                                    <asp:ListItem Value="Gratuity">Gratuity</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Client Name :</b>
                                <asp:DropDownList runat="server" ID="ddl_bclient" CssClass="form-control">
                                </asp:DropDownList>
                            </div> 
                                   <div class="col-sm-2 col-xs-12">
                                                <b>From Month :</b><span style="color:red">*</span> 
                                        <asp:TextBox ID="txt_work_img_from" runat="server" class="form-control date-picker"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                               <b> To Month :</b><span style="color:red">*</span> 
                                        <asp:TextBox ID="txt_work_img_to" runat="server" class="form-control date-picker"></asp:TextBox>
                                            </div>
                              <div class="col-sm-2 col-xs-12"style="margin-top: 17px;">
                                           <asp:Button runat="server" ID="btn_bonus_report" CssClass="btn btn-large" Text="Get Report"  OnClick="btn_bonus_report_Click" OnClientClick="return Bonus_val()" />  
                                      </div>
                        </div>
                        <br />
                    </div>

                     </div>
                <br />
               
            </div>
            </asp:Panel>
                </div>

                      
      
   
    
</asp:Content>

