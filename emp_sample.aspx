<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="emp_sample.aspx.cs" Inherits="emp_sample" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Employee Attendance</title>
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
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>
    <script src="datatable/pdfmake.min.js"></script>
    <script src="js/dataTables.fixedColumns.min.js"></script>
    <link href="css/fixedColumns.dataTables.min.css" rel="stylesheet" type="text/css" />

    <style>
        .grid-view {
            max-height: 600px;
            height: auto;
            overflow-x: auto;
            overflow-y: auto;
        }

        .grid-view8 {
            max-height: 300px;
            height: auto;
            overflow-x: auto;
            overflow-y: auto;
        }

        .dt-buttons.btn-group {
            display: none;
        }

        .DTFC_LeftBodyWrapper {
            margin-top: -12px;
            overflow: hidden;
        }
        #ctl00_cph_righrbody_gv_attendance_filter {
        display:none;
        }
    </style>
    <script>
        function unblock() {
            $.unblockUI();
        }

        $(document).ready(function () {

            var table = $('#<%=grid_reject_attendace.ClientID%>').DataTable({
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
              .appendTo('#<%=grid_reject_attendace.ClientID%>_wrapper .col-sm-6:eq(0)');

            var table = $('#<%=gv_attendance.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                scrollY: "310px",
                scrollX: true,
                scrollCollapse: true,
                paging: false,
                fixedColumns: {
                    leftColumns: 2,

                }


            });

            table.buttons().container()
              .appendTo('#<%=gv_attendance.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';
            $('#<%=btn_process.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_reports.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('[id*=chk_gv_header_r_m]').click(function () {
                $("[id*='chk_client']").attr('checked', this.checked);
            });
            $('[id*=chk_gv_header_admin]').click(function () {
                $("[id*='chk_client']").attr('checked', this.checked);
            });
            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "1990:+0",
                showButtonPanel: true,
                dateFormat: 'mm/yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, month, 1));
                }
            });
            $('.date-picker').focus(function () {
                $(".ui-datepicker-calendar").hide();

            });
            $(".date-picker").attr("readonly", "true");
            //vikas01/11

            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                maxDate: 0,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    //$(".date-picker").datepicker("option", "minDate", selected)
                }
            });


            $(".date-picker1").attr("readonly", "true");

        });

        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
            var st = $(this).find("input[id*='HiddenField1']").val();
            if (st == null)
                st = 0;
            $('[id$=Div1]').tabs({ selected: st });
            var st = $(this).find("input[id*='HiddenField2']").val();
            if (st == null)
                st = 0;
            $('[id$=Div2]').tabs({ selected: st });

            $('[id$=Div5]').tabs({ selected: st });

        });


        function req_validation() {

            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;
            var t_UnitCode = document.getElementById('<%=ddl_unitcode.ClientID %>');
                var SelectedText = t_UnitCode.options[t_UnitCode.selectedIndex].text;
                var val_date = document.getElementById('<%=txttodate.ClientID %>');

                if (Selected_ddl_client == "Select") {
                    alert("Please Select Client Name... ");
                    ddl_client.focus();
                    return false;
                }
                //if (SelectedText == "ALL") {
                //    alert("Please Select Branch... ");
                //    t_UnitCode.focus();
                //    return false;
                //}
            // Month/Year

                if (val_date.value == "") {
                    alert("Please Select Month/Year ");
                    val_date.focus();
                    return false;
                }

                return true;
            }
            //upload attendance


            function isNumber1(evt) {
                if (null != evt) {
                    evt = (evt) ? evt : window.event;
                    var charCode = (evt.which) ? evt.which : evt.keyCode;
                    if (charCode == 34 || charCode == 39) {
                        return false;
                    }
                    return true;
                }
            }

            function isNumberKey(evt, id) {
                try {
                    var charCode = (evt.which) ? evt.which : event.keyCode;

                    if (charCode == 46) {
                        var txt = document.getElementById(id).value;
                        if (!(txt.indexOf(".") > -1)) {

                            return true;
                        }
                    }
                    if (charCode > 31 && (charCode < 48 || charCode > 57))
                        return false;

                    return true;
                } catch (w) {
                    alert(w);
                }
            }
            function openWindow() {
                window.open("html/emp_sample.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
            }

            function r_validation1() {
                var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;
            var t_UnitCode = document.getElementById('<%=ddl_unitcode.ClientID %>');
            var SelectedText = t_UnitCode.options[t_UnitCode.selectedIndex].text;
            var val_date = document.getElementById('<%=txttodate.ClientID %>');

                if (Selected_ddl_client == "Select") {
                    alert("Please Select Client Name... ");
                    ddl_client.focus();
                    return false;
                }

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

                return true;

            }
            function validate() {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

                return true;


            }

            function valid_grid() {
                var left_date_date = document.getElementById('left_date_date');
                var txt_emp_sample_left_reson = document.getElementById('txt_emp_sample_left_reson');
                if (left_date_date.value != "") {
                    alert("Please Enter Reason");
                    txt_emp_sample_left_reson.focus();
                    return false;
                }


            }
            function valid_upload1() {

                var document1_upload = document.getElementById('<%=FileUpload1.ClientID %>');
                if (document1_upload.value == "") {
                    alert("Please Upload File");
                    document1_upload.focus();
                    return false;
                }



            }


            function valid_upload() {
                var document1_file = document.getElementById('<%=document1_file.ClientID %>');
            if (document1_file.value == "") {
                alert("Please Upload File");
                document1_file.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function R_validation() {

            var r = confirm("Are you Sure You Want to Approve Record");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }
        function Req_validation() {

            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;


            if (Selected_ddl_client == "Select") {
                alert("Please Select Client");
                ddl_client.focus();
                return false;
            }
            var ddl_state = document.getElementById('<%=ddl_state.ClientID %>');
            var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;

            if (Selected_ddl_state == "Select") {
                alert("Please Select State");
                ddl_state.focus();
                return false;
            }
            var txttodate = document.getElementById('<%=txttodate.ClientID %>');
            if (txttodate.value == "") {
                alert("Please Select Month/Year");
                txttodate.focus();
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
        function driver_conveyneance() {
            var txt_date_conveyance = document.getElementById('<%=txt_date_conveyance.ClientID %>');
            if (txt_date_conveyance.value == "") {
                alert("Please Select Month");
                txt_date_conveyance.focus();
                return false;
            }
            var ddl_client = document.getElementById('<%=con_ddl_client.ClientID %>');
             var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;
             if (Selected_ddl_client == "Select") {
                 alert("Please Select Client ");
                 ddl_client.focus();
                 return false;
             }
             var ddl_state = document.getElementById('<%=con_ddl_state.ClientID %>');
            var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;
            if (Selected_ddl_state == "Select") {
                alert("Please Select State ");
                ddl_state.focus();
                return false;
            }
            var ddl_unitcode = document.getElementById('<%=con_ddl_unitcode.ClientID %>');
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

            var ddl_employee = document.getElementById('<%=ddl_employee.ClientID %>');
            var Selected_ddl_employee = ddl_employee.options[ddl_employee.selectedIndex].text;
            if (Selected_ddl_employee == "ALL") {
                alert("Please Select Employee Name ");
                ddl_employee.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function driver_conveyneance1() {
            var txt_date_conveyance = document.getElementById('<%=txt_date_conveyance.ClientID %>');
             if (txt_date_conveyance.value == "") {
                 alert("Please Select Month");
                 txt_date_conveyance.focus();
                 return false;
             }
             var ddl_client = document.getElementById('<%=con_ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;
            if (Selected_ddl_client == "Select") {
                alert("Please Select Client ");
                ddl_client.focus();
                return false;
            }
            var ddl_state = document.getElementById('<%=con_ddl_state.ClientID %>');
             var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;
             if (Selected_ddl_state == "Select") {
                 alert("Please Select State ");
                 ddl_state.focus();
                 return false;
             }
             var ddl_unitcode = document.getElementById('<%=con_ddl_unitcode.ClientID %>');
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
            var btn_upload = document.getElementById('<%=btn_upload.ClientID %>');
            if (btn_upload.value == "") {
                alert("Please Enter Employee Conveyance Bill Upload");
                btn_upload.focus();
                return false;
            }
          
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function driver_upload()
        {
            var con_bill_upload = document.getElementById('<%=con_bill_upload.ClientID %>');
            if (con_bill_upload.value == "")
            {
                alert("Please Enter Driver Conveyance Bill Upload");
                con_bill_upload.focus();
                return false;
            }
        }
        function conv_validation() {
            var bill_upload = document.getElementById('<%=bill_upload.ClientID %>');
            if (bill_upload.value == "")
            {
                alert("Please Upload Employee Conveyance");
                bill_upload.focus();
                return false;
            }
        }
        function r_validation1() {
            var txt_month_material = document.getElementById('<%=txt_month_material .ClientID %>');
            if (txt_month_material.value == "") {
                alert("Please Select Month");
                txt_date_conveyance.focus();
                return false;
            }
            var ddl_client_material = document.getElementById('<%=ddl_client_material.ClientID %>');
            var Selected_ddl_client_material = ddl_client_material.options[ddl_client_material.selectedIndex].text;


            if (Selected_ddl_client_material == "Select") {
                alert("Please Select Client");
                ddl_client_material.focus();
                return false;
            }
            var ddl_state_material = document.getElementById('<%=ddl_state_material.ClientID %>');
            var Selected_ddl_state_material = ddl_state_material.options[ddl_state_material.selectedIndex].text;

            if (Selected_ddl_state_material == "Select") {
                alert("Please Select State");
                ddl_state_material.focus();
                return false;
            }
            var ddl_branch_material = document.getElementById('<%=ddl_branch_material.ClientID %>');
            var Selected_ddl_branch_material = ddl_branch_material.options[ddl_branch_material.selectedIndex].text;
            if (Selected_ddl_branch_material == "Select") {
                alert("Please Select Branch Name ");
                ddl_branch_material.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }
        function r_validation3() {

            var r = confirm("Are you Sure You Want to Approve Record");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }
        function isNumber_acc(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            return true;
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

        function Req_validation_service() {

            var ddl_client_service = document.getElementById('<%=ddl_client_service.ClientID %>');
            var Selected_ddl_client_service = ddl_client_service.options[ddl_client_service.selectedIndex].text;


            if (Selected_ddl_client_service == "Select") {
                 alert("Please Select Client");
                 ddl_client_service.focus();
                 return false;
             }
            var ddl_state_service = document.getElementById('<%=ddl_state_service.ClientID %>');
            var Selected_ddl_state_service = ddl_state_service.options[ddl_state_service.selectedIndex].text;

            if (Selected_ddl_state_service == "Select") {
                alert("Please Select State");
                ddl_state_service.focus();
                return false;
            }
            var ddl_branch_service = document.getElementById('<%=ddl_branch_service.ClientID %>');
            var Selected_ddl_branch_service = ddl_branch_service.options[ddl_branch_service.selectedIndex].text;

            if (Selected_ddl_branch_service == "Select") {
                alert("Please Select Branch");
                ddl_branch_service.focus();
                return false;
            }

            var txt_date = document.getElementById('<%=txt_date.ClientID %>');
            if (txt_date.value == "") {
                alert("Please Select Month/Year");
                txt_date.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }

        function Req_validation_adm() {

            var ddl_client_adm = document.getElementById('<%=ddl_client_adm.ClientID %>');
            var Selected_ddl_client_adm = ddl_client_adm.options[ddl_client_adm.selectedIndex].text;


            if (Selected_ddl_client_adm == "Select") {
                 alert("Please Select Client");
                 ddl_client_adm.focus();
                 return false;
             }
            var ddl_state_adm = document.getElementById('<%=ddl_state_adm.ClientID %>');
            var Selected_ddl_state_adm = ddl_state_adm.options[ddl_state_adm.selectedIndex].text;

            if (Selected_ddl_state_adm == "Select") {
                alert("Please Select State");
                ddl_state_adm.focus();
                return false;
            }
            var ddl_branch_adm = document.getElementById('<%=ddl_branch_adm.ClientID %>');
            var Selected_ddl_branch_adm = ddl_branch_adm.options[ddl_branch_adm.selectedIndex].text;

            if (Selected_ddl_branch_adm == "Select") {
                alert("Please Select Branch");
                ddl_branch_adm.focus();
                return false;
            }

            var txt_date_adm = document.getElementById('<%=txt_date_adm.ClientID %>');
            if (txt_date_adm.value == "") {
                alert("Please Select Month/Year");
                txt_date_adm.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }
       
        function Req_save_val_service() {

            var txt_party_name = document.getElementById('<%=txt_party_name.ClientID %>');
            if (txt_party_name.value == "") {
                alert("Please Enter Party Name");
                txt_party_name.focus();
                return false;
            }
            var txt_help_req_no = document.getElementById('<%=txt_help_req_no.ClientID %>');
            if (txt_help_req_no.value == "") {
                alert("Please Enter Help Desk Request Number");
                txt_help_req_no.focus();
                return false;
            }
            var txt_amount = document.getElementById('<%=txt_amount.ClientID %>');
            if (txt_amount.value == "") {
                alert("Please Enter amount");
                txt_amount.focus();
                return false;
            }
            var txt_bank_acc_no = document.getElementById('<%=txt_bank_acc_no.ClientID %>');
            if (txt_bank_acc_no.value == "") {
                alert("Please Enter Bank Account Number");
                txt_bank_acc_no.focus();
                return false;
            }
            var txt_ifsc_code = document.getElementById('<%=txt_ifsc_code.ClientID %>');
            if (txt_ifsc_code.value == "") {
                alert("Please Enter IFSC Code");
                txt_ifsc_code.focus();
                return false;
            }
            var r_m_upload = document.getElementById('<%=r_m_upload.ClientID %>');
            if (r_m_upload.value == "") {
                alert("Please Upload File");
                r_m_upload.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }
        function Req_save_val_administartive() {

            var txt_party_adm = document.getElementById('<%=txt_party_adm.ClientID %>');
            if (txt_party_adm.value == "") {
                 alert("Please Enter Party Name");
                 txt_party_adm.focus();
                 return false;
             }
            var txt_req_no_adm = document.getElementById('<%=txt_req_no_adm.ClientID %>');
            if (txt_req_no_adm.value == "") {
                alert("Please Enter Days");
                txt_req_no_adm.focus();
                return false;
            }
            var txt_amount_req = document.getElementById('<%=txt_amount_req.ClientID %>');
            if (txt_amount_req.value == "") {
                alert("Please Enter amount");
                txt_amount_req.focus();
                return false;
            }
            var txt_bank_account_adm = document.getElementById('<%=txt_bank_account_adm.ClientID %>');
            if (txt_bank_account_adm.value == "") {
                alert("Please Enter Bank Account Number");
                txt_bank_account_adm.focus();
                return false;
            }
            var txt_ifsc_adm = document.getElementById('<%=txt_ifsc_adm.ClientID %>');
            if (txt_ifsc_adm.value == "") {
                alert("Please Enter IFSC Code");
                txt_ifsc_adm.focus();
                return false;
            }
            var administrative_upload = document.getElementById('<%=administrative_upload.ClientID %>');
            if (administrative_upload.value == "") {
                alert("Please Upload File");
                administrative_upload.focus();
                return false;
            }
         
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }
        function isNumber_dot_amt_r_m(evt) {
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

    <style>
        .row {
            margin: 0px;
        }

        .nt_style {
            color: red;
            font: bold;
            text-align: center;
        }

        .text-red {
            color: red;
        }

        .wid {
            width: 120px;
        }

        .DTFC_LeftBodyLiner {
            overflow-x: hidden;
            top: -13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: small;"><b>EMPLOYEE ATTENDANCE</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>

            <br />

             <div id="tabs" style="background: beige;">
                <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                <ul>
                    <li><a id="A2" href="#menu0" runat="server">Employee Attendance</a></li>

                    <li><a href="#menu5" id="A1" runat="server">Conveyance</a></li>

                    <li><a href="#menu6" runat="server">Material Amount</a></li>
                      <li><a id="A5" href="#menu7" runat="server">R&M Service</a></li>
                      <li><a id="A8" href="#menu8" runat="server">Administrative Expenses</a></li>
                    
                </ul>
                <div id="menu6">
                    <div class="row">

                        <div class="col-sm-2 col-xs-12">
                            <b>Select Month :</b><span class="text-red">*</span>
                            <asp:TextBox ID="txt_month_material" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                        </div>


                        <div class="col-sm-2 col-xs-12">
                                   <b>Material Amount Type :</b> <span class="text-red" style="color: red">*</span>
                                    <asp:DropDownList ID="ddl_material_amount_type"  runat="server"  CssClass="form-control">
                                        <asp:ListItem Value="0">Employeewise Material</asp:ListItem>
                                        <asp:ListItem Value="1">Locationwise Material</asp:ListItem>
                                       

                                    </asp:DropDownList>
                                </div>




                        <div class="col-sm-2 col-xs-12">
                            <b>Client Name :</b> <span class="text-red">*</span>
                            <asp:DropDownList ID="ddl_client_material" onchange="validate();" DataValueField="client_code" DataTextField="client_name" AutoPostBack="true" OnSelectedIndexChanged="ddl_client_material_SelectedIndexChanged" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>State :</b><span class="text-red">*</span>
                            <asp:DropDownList ID="ddl_state_material" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_state_material_SelectedIndexChanged" AutoPostBack="true" />
                        </div>

                        <div class="col-sm-2 col-xs-12">
                                    <b>Employee Type :</b> <span class="text-red" style="color: red">*</span>
                                    <asp:DropDownList ID="ddl_employee_type1"  runat="server"  CssClass="form-control">
                                        <asp:ListItem Value="0">Permanent</asp:ListItem>
                                        <asp:ListItem Value="1">Left</asp:ListItem>
                                       

                                    </asp:DropDownList>
                                </div>

                        <div class="col-sm-2 col-xs-12">
                            <b>Branch Name :</b><span class="text-red">*</span>
                            <asp:DropDownList ID="ddl_branch_material" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_branch_material_SelectedIndexChanged" AutoPostBack="true" />
                        </div>

                        <br />
                    </div>

                     <br />
                     <br />
                       <%--    for employee,branchwise material komal 14-05-2020--%>
                    <div id="Div5">

                         <div id="Div8" style="background: beige;">
                <asp:HiddenField ID="HiddenField1" Value="0" runat="server" />
                <ul>
                    <li><a id="A6"  href="#e3" runat="server">Employeewise Material</a></li>

                    <li><a id="A7" href="#e4" runat="server">Locationwise Material</a></li>
                </ul>

                               <%--    for employee material komal 14-05-2020--%>
                               <div id="e3">
                    <div class="container-fluid">

                        <div class="row">
                            <div class="col-sm-8 col-xs-12"></div>
                            <div class="col-sm-4 col-xs-12">
                                <div style="max-height: 200px; overflow-y: auto; overflow-x: hidden;">
                                    <asp:Panel ID="Panel_notification_material" runat="server">

                                        <table border="1" class="table table-striped">
                                            <tr style="background-color: #eeeaea">
                                                <th>Branch Count</th>
                                                <th style="text-align: center">Notification</th>

                                            </tr>
                                            <%--   <tr>
                                                    <asp:Panel ID="panel_deployment_material" runat="server">
                                                        <td class="nt_style"><%=Material_deployment %></td>
                                                        <td style="font: bold;"><a data-toggle="modal" href="#branches_deployment_conveyance"><b>Branches Having  No  Deployment</b></a></td>
                                                        <td class="nt_style">0</td>
                                                    </asp:Panel>
                                                </tr>--%>
                                            <%--   <tr>
                                                    <asp:Panel ID="panel_close_material" runat="server">
                                                        <td class="nt_style"><%=Material_closed_branch %></td>
                                                        <td style="font: bold;"><a data-toggle="modal" href="#branches_close_conveyance"><b>Branches Have  Closed </b></a></td>
                                                        <td class="nt_style"><%=Material_closed_branch_emp %></td>
                                                    </asp:Panel>
                                                </tr>--%>

                                            <tr>
                                                <asp:Panel ID="Panel_not_appr_material" runat="server">
                                                    <td class="nt_style"><%=Material_Message%></td>
                                                    <td style="font: bold;"><a data-toggle="modal" href="#branches_no_material"><b>Branches Not Approved By Admin</b></a></td>

                                                </asp:Panel>
                                            </tr>


                                            <tr>
                                                <asp:Panel ID="Panel_appro_atte_material" runat="server">
                                                    <td class="nt_style"><%=Material_appro_attendannce%></td>
                                                    <td style="font: bold;"><a data-toggle="modal" href="#approve_by_admin_material"><b>Branches Approved By Admin</b></a></td>

                                                </asp:Panel>
                                            </tr>

                                            <tr>
                                                <asp:Panel ID="Panel_approv_finance_material" runat="server">
                                                    <td class="nt_style"><%=Material_reject_attendance%></td>
                                                    <td style="font: bold;"><a data-toggle="modal" href="#branches_reject_material"><b>Branches Approved By Finance</b></a></td>

                                                </asp:Panel>
                                            </tr>

                                            <tr>
                                                <asp:Panel ID="Panel_reject_Material" runat="server">
                                                    <td class="nt_style"><%=Material_appro_attendannce_finanace %></td>
                                                    <td style="font: bold;"><a data-toggle="modal" href="#branches_approve_by_finance_material"><b>Branches Reject By Finance</b></a></td>

                                                </asp:Panel>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="row">
                        <%-- for material_location 02-01-19--%>
                        <asp:Panel ID="Panel_materil" runat="server">

                            <div class="container" style="width: 75%">
                                <asp:GridView ID="gv_material_location" class="table" runat="server" BackColor="White" OnRowDataBound="grd_convayance_RowDataBound"
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
                                                <asp:LinkButton ID="lnk_material" runat="server" CausesValidation="false" Text="Material" Style="color: white" OnCommand="lnk_material_Command1" CommandArgument='<%# Eval("material_upload") +","+ Eval("unit_name")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                        <%-- /////////////////////////////////--%>


                        <br />
                        <asp:Panel ID="Panel14" runat="server">
                            <asp:GridView ID="grid_material_amount" runat="server" AutoGenerateColumns="false" OnRowDataBound="grid_material_amount_RowDataBound" ForeColor="#333333" class="table" GridLines="Both">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="50" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />--%>
                                    <asp:BoundField DataField="unit_name" HeaderText="Branch name" SortExpression="unit_name" />
                                    <asp:BoundField DataField="unit_code" HeaderText="Branch code" SortExpression="unit_code" />
                                    <asp:BoundField DataField="EMP_NAME" HeaderText="Employee Name" SortExpression="EMP_NAME" />
                                    <asp:BoundField DataField="EMP_code" HeaderText="Employee Code" SortExpression="EMP_code" />

                                    <asp:TemplateField HeaderText="Material Amount">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txt_material_amount" Width="100px" Text='<%# Eval("LEFT_DATE") %>' CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Material Deduction Amount">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txt_material_deduction" Width="100px" Text='<%# Eval("LEFT_DATE1") %>' CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                        <br />
                        <br />

                       <%-- <asp:Panel ID="Panel13" runat="server">
                            <div align="center">
                                <br />
                                <asp:Button ID="btn_save_material" runat="server" Text="Save" class="btn btn-primary" OnClick="btn_save_material_Click" OnClientClick="return r_validation1();" />

                                <%--  <asp:Button ID="btn_update_material" runat="server" Text="Update" class="btn btn-primary" OnClick="btn_update_material_Click" OnClientClick="return R_validation();" />--%>

                            <%--    <asp:Button ID="btn_approve_material" runat="server" Text="Approve" class="btn btn-primary" OnClick="btn_approve_material_Click" OnClientClick="return r_validation3();" />
                                <asp:Button ID="btn_material_link" runat="server" Text="Reports" class="btn btn-primary" OnClick="btn_material_link_Click" OnClientClick="return r_validation1();" />

                                
                            </div>
                        </asp:Panel>

                        <br />

                        <br />
                        <asp:Panel ID="Panel11" runat="server">

                            <div class="row" id="Div4" runat="server">

                                <div class="col-sm-2 col-xs-12"></div>
                                <div class="col-sm-2 col-xs-12"></div>
                                <div class="col-sm-2 col-xs-12">
                                    Description :
                                      <asp:TextBox ID="des_material" runat="server" class="form-control text_box" onkeypress="return  isNumber1(event)" MaxLength="200"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <span class="text_margin">File to Upload :</span>

                                    <asp:FileUpload ID="document_file_material" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                                </div>
                                <div class="col-sm-3 col-xs-12 text-left" style="padding-top: 1%">
                                    <asp:Button ID="btn_material_upload" runat="server" class="btn btn-primary" Text=" Upload " OnClientClick="return valid_upload();" OnClick="btn_material_upload_Click" />
                                </div>
                                <div class="col-sm-2 col-xs-12"><b style="color: #f00; text-align: center">Note :</b> Only JPG, PNG,JPEG and PDF files will be uploaded.</div>

                                <br />
                            </div>
                        </asp:Panel>--%>

                        <br />

                        <%--  // Material Amount 01-10-2019--%>

                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <div class="modal fade" id="Div7" role="dialog">
                                    <div class="modal-dialog">
                                        <div class="modal-content" style="width: 400px; height: auto">
                                            <div class="modal-header">
                                                <div class="modal-body">
                                                    <div class="row">
                                                        <div class="col-sm-12" style="padding-left: 1%;">
                                                            <asp:GridView ID="GridView1" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="modal fade" id="branch_no_deployment_material" role="dialog" data-dismiss="modal" href="#lost">

                                    <div class="modal-dialog">
                                        <div class="modal-content" style="width: 458px;">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4>Branches Having  No Deployment</h4>
                                            </div>

                                            <div class="modal-body">
                                                <asp:Panel ID="Panel_no_deployment_material" runat="server" CssClass="grid-view8">
                                                    <asp:GridView ID="gv_no_deployment_material" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div7">
                                                        <Columns>
                                                            <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
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




                                <div class="modal fade" id="branches_no_material" role="dialog" data-dismiss="modal" href="#lost">

                                    <div class="modal-dialog">
                                        <div class="modal-content" style="width: 458px;">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4>Not Approve By Admin</h4>
                                            </div>

                                            <div class="modal-body">
                                                <asp:Panel ID="Panel_material" runat="server" CssClass="grid-view8">
                                                    <asp:GridView ID="gv_branches_no_material" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div7">
                                                        <Columns>
                                                            <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                            <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                            <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />

                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
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



                                <div class="modal fade" id="approve_by_admin_material" role="dialog" data-dismiss="modal" href="#lost">

                                    <div class="modal-dialog">
                                        <div class="modal-content" style="width: 458px;">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4>Approve By Admin</h4>
                                            </div>

                                            <div class="modal-body">
                                                <asp:Panel ID="Panel_approve_by_admin_material" runat="server" CssClass="grid-view8">
                                                    <asp:GridView ID="gv_approve_by_admin_material" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div7">
                                                        <Columns>
                                                            <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                            <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                            <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />

                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
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



                                <div class="modal fade" id="branches_reject_material" role="dialog" data-dismiss="modal" href="#lost">

                                    <div class="modal-dialog">
                                        <div class="modal-content" style="width: 622px;">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4>Approve By Finance</h4>
                                            </div>

                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-sm-12" style="padding-left: 1%;">
                                                        <asp:Panel ID="Panel_branches_reject_material" runat="server" CssClass="grid-view8">
                                                            <asp:GridView ID="gv_branches_reject_material" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div7">
                                                                <Columns>
                                                                    <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                                    <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                                    <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />

                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
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


                                <div class="modal fade" id="branches_approve_by_finance_material" role="dialog" data-dismiss="modal" href="#lost">

                                    <div class="modal-dialog">
                                        <div class="modal-content" style="width: 458px;">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4>Branches Rejected By Finance</h4>
                                            </div>

                                            <div class="modal-body">
                                                <asp:Panel ID="Panel_branches_approve_by_finance_material" runat="server" CssClass="grid-view8">
                                                    <asp:GridView ID="gv_approve_by_finance_material" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div7">
                                                        <Columns>
                                                            <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                            <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                            <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />

                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
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
                                    <br />
                                </div>



                                <div class="modal fade" id="branch_close_material" role="dialog" data-dismiss="modal" href="#lost">

                                    <div class="modal-dialog">
                                        <div class="modal-content" style="width: 458px;">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4>Branches Have Closed</h4>
                                            </div>

                                            <div class="modal-body">
                                                <asp:Panel ID="Panel_branch_close_material" runat="server" CssClass="grid-view">
                                                    <asp:GridView ID="gv_branch_close_material" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div7">
                                                        <Columns>
                                                            <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
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

                            </ContentTemplate>

                        </asp:UpdatePanel>

                        <%--////////////////////////////////////--%>
                    </div>

                                   </div>


                         <%--    for branchwise material komal 14-05-2020--%>
                             <div id="e4">


                                  <asp:Panel ID="Panel_upload_branchwise" runat="server">

                            <div class="container" style="width: 75%">
                                <asp:GridView ID="gv_locationwise_material_upload" class="table" runat="server" BackColor="White" OnRowDataBound="grd_convayance_RowDataBound"
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
                                                <asp:LinkButton ID="lnk_material" runat="server" CausesValidation="false" Text="Material" Style="color: white" OnCommand="lnk_material_Command1" CommandArgument='<%# Eval("material_upload") +","+ Eval("unit_name")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>


                                        <asp:Panel ID="Panel19" runat="server">
                            <asp:GridView ID="gv_branchwise_material" runat="server" AutoGenerateColumns="false" OnRowDataBound="gv_branchwise_material_RowDataBound" ForeColor="#333333" class="table" GridLines="Both">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="50" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />--%>
                                    <asp:BoundField DataField="unit_name" HeaderText="Branch name" SortExpression="unit_name" />
                                    <asp:BoundField DataField="unit_code" HeaderText="Branch code" SortExpression="unit_code" />
                                    

                                    <asp:TemplateField HeaderText="Material Amount">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txt_material_amount" Width="100px" Text='<%# Eval("LEFT_DATE") %>' CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </asp:Panel>


                                   </div>



                              <asp:Panel ID="Panel13" runat="server">
                            <div align="center">
                                <br />
                                <asp:Button ID="btn_save_material" runat="server" Text="Save" class="btn btn-primary" OnClick="btn_save_material_Click" OnClientClick="return r_validation1();" />

                                <%--  <asp:Button ID="btn_update_material" runat="server" Text="Update" class="btn btn-primary" OnClick="btn_update_material_Click" OnClientClick="return R_validation();" />--%>

                                <asp:Button ID="btn_approve_material" runat="server" Text="Approve" class="btn btn-primary" OnClick="btn_approve_material_Click" OnClientClick="return r_validation3();" />
                                <asp:Button ID="btn_material_link" runat="server" Text="Reports" class="btn btn-primary" OnClick="btn_material_link_Click" OnClientClick="return r_validation1();" />

                                
                            </div>
                        </asp:Panel>

                        <br />

                        <br />
                        <asp:Panel ID="Panel11" runat="server">

                            <div class="row" id="Div4" runat="server" >
                                <div class="col-sm-2 col-xs-12"></div>
                                <div class="col-sm-2 col-xs-12">
                                  <b>  Description :</b>
                                      <asp:TextBox ID="des_material" runat="server" class="form-control text_box" onkeypress="return  isNumber1(event)" MaxLength="200"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <span class="text_margin">File to Upload :</span>

                                    <asp:FileUpload ID="document_file_material" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                                        <b style="color: #f00; text-align: center">Note :</b> Only JPG, PNG,JPEG and PDF files will be uploaded.
                                     </div>
                                <div class="col-sm-2 col-xs-12 text-left" style="padding-top: 1%">
                                    <asp:Button ID="btn_material_upload" runat="server" class="btn btn-primary" Text=" Upload " OnClientClick="return valid_upload();" OnClick="btn_material_upload_Click" />
                                </div>
                               
                                <br />
                            </div>
                        </asp:Panel>



                        </div>
                         </div>
                       <%--    end komal 14-05-2020--%>

                </div>
                <div id="menu0">

                      <div class="container-fluid">
               
                <div class="row">
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
 <div class="col-sm-2 col-xs-12">
                       <b> Month / Year :</b><span class="text-red">*</span>
                        <asp:TextBox ID="txttodate" runat="server" Visible="true" class="form-control date-picker text_box"></asp:TextBox>

                    </div>

                    <div class="col-sm-2 col-xs-12">
                       <b> Client Name :</b> <span class="text-red">*</span>
                        <asp:DropDownList ID="ddl_client" onchange="validate();" DataValueField="client_code" DataTextField="client_name" AutoPostBack="true" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        <b>State :</b><span class="text-red">*</span>
                        <asp:DropDownList ID="ddl_state" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        <b>Branch Name :</b> <span class="text-red">*</span>
                        <asp:DropDownList ID="ddl_unitcode" runat="server" class="form-control text_box">
                        </asp:DropDownList>
                    </div>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
                    
                    <div class="col-sm-4 col-xs-12">
                        <br />
                        <asp:Button ID="emp_btn_process" runat="server" Text="Process" class="btn btn-primary" OnClientClick="return Req_validation();" OnClick="emp_btn_process_Click" />
                        <asp:Button ID="btn_attendance" runat="server" Text="Attendance" class="btn btn-primary" OnClientClick="return req_validation();" OnClick="btn_attendance_Click" />


                        <asp:Button ID="btn_add_emp" runat="server" Text="Add Employee" class="btn btn-primary" Visible="false"/>
                        <asp:Button ID="btn_reports" runat="server" Text="Reports" class="btn btn-primary" OnClick="btn_reports_Click" />
                        <asp:Button ID="BtnClose" runat="server" Text="Close" class="btn btn-danger" OnClick="BtnClose_Click" />
                    </div>



                    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel9" TargetControlID="btn_add_emp"
                        CancelControlID="Button9" BackgroundCssClass="Background">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panel9" runat="server" CssClass="Popup" Style="display: none">
                        <iframe style="width: 800px; height: 350px; background-color: #fff;" id="Iframe2" src="add_emp.aspx" runat="server"></iframe>
                        <div class="row text-center">
                            <asp:Button ID="Button9" CssClass="btn btn-danger" OnClientClick="callfnc2()" runat="server" Text="Close" />
                        </div>

                        <br />

                    </asp:Panel>


                </div>
            </div>
            <br />
            <div class="container-fluid">

                <div class="row">
                    <div class="col-sm-8 col-xs-12"></div>
                    <div class="col-sm-4 col-xs-12">
                        <div style="max-height: 200px; overflow-y: auto; overflow-x: hidden;">
                            <asp:Panel ID="Notification_panel" runat="server">

                                <table border="1" class="table table-striped">
                                    <tr style="background-color: #eeeaea">
                                        <th>Branch Count</th>
                                        <th style="text-align: center">Notification</th>
                                        <th>Emp Count</th>
                                    </tr>
                                    <tr>
                                        <asp:Panel ID="panel_deployment" runat="server">
                                            <td class="nt_style"><%=deployment %></td>
                                            <td style="font: bold;"><a data-toggle="modal" href="#branch_deployment"><b>Branches Having  No  Deployment</b></a></td>
                                            <td class="nt_style">0</td>
                                        </asp:Panel>
                                    </tr>
                                    <tr>
                                        <asp:Panel ID="panel_clo_branch" runat="server">
                                            <td class="nt_style"><%=closed_branch %></td>
                                            <td style="font: bold;"><a data-toggle="modal" href="#branch_close"><b>Branches Have  Closed </b></a></td>
                                            <td class="nt_style"><%=Emp_closed_branch %></td>
                                        </asp:Panel>
                                    </tr>
                                    <tr>
                                        <asp:Panel ID="pnl_branch" runat="server">
                                            <td class="nt_style"><%=Message%></td>
                                            <td style="font: bold;"><a data-toggle="modal" href="#attendance"><b>Branches Not Approved By Admin</b></a></td>
                                            <td class="nt_style"><%=Emp_Message %></td>
                                        </asp:Panel>
                                    </tr>

                                    <tr>
                                        <asp:Panel ID="approval_panel" runat="server">
                                            <td class="nt_style"><%=appro_attendannce%></td>
                                            <td style="font: bold;"><a data-toggle="modal" href="#approve_attendance"><b>Branches Approved By Admin</b></a></td>
                                            <td class="nt_style"><%=Emp_appro_attendannce %></td>
                                        </asp:Panel>
                                    </tr>

                                    <tr>
                                        <asp:Panel ID="approval_finance_panel" runat="server">
                                            <td class="nt_style"><%=appro_attendannce_finanace%></td>
                                            <td style="font: bold;"><a data-toggle="modal" href="#approve_attendance_finance"><b>Branches Approved By Finance</b></a></td>
                                            <td class="nt_style"><%=Emp_appro_attendannce_finanace %></td>
                                        </asp:Panel>
                                    </tr>

                                    <tr>
                                        <asp:Panel ID="reject_panel" runat="server">
                                            <td class="nt_style"><%=reject_attendance %></td>
                                            <td style="font: bold;"><a data-toggle="modal" href="#reject_attendance"><b>Branches Reject By Finance</b></a></td>
                                            <td class="nt_style"><%=Emp_reject_attendance %></td>
                                        </asp:Panel>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="container" style="width: 80%">
                <asp:Panel ID="Panel6" runat="server" Style="overflow-y: auto; max-height: 250px; overflow-x: hidden">
                    <asp:GridView ID="gv_fullmonthot" runat="server" AutoGenerateColumns="false" ForeColor="#333333" class="table" GridLines="Both" OnRowDataBound="gv_fullmonthot_RowDataBound" DataKeyNames="emp_code">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="50" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SELECT EMPLOYEE" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_client" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- vikas 01/11/2018--%>
                            <asp:BoundField DataField="emp_code" HeaderText="EMPLOYEE CODE" SortExpression="emp_code" />

                            <%--<asp:TemplateField HeaderText="Emp Code" >
                            <ItemTemplate>
                               <asp:TextBox runat="server" ID="emp_code" Width="100px" ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                            <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                            <asp:BoundField DataField="unit_code" HeaderText="Branch Name" SortExpression="unit_code" Visible="false" />
                            <asp:BoundField DataField="emp_name" HeaderText="EMPLOYEE NAME" SortExpression="emp_name" />
                            <asp:BoundField DataField="designation" HeaderText="Designation" SortExpression="designation" />
                            <asp:BoundField DataField="Employee_type" HeaderText="TYPE" SortExpression="Employee_type" />
                            <asp:TemplateField HeaderText="LEFT DATE">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="left_date_date" Width="100px" CssClass="form-control date-picker1" Text='<%# Eval("LEFT_DATE") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="LEFT REASON">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txt_emp_sample_left_reson" Width="100px" CssClass="form-control" Text='<%# Eval("LEFT_REASON") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="emp_code1" HeaderText="EMPLOYEE CODE1" SortExpression="emp_code1"/>




                        </Columns>
                    </asp:GridView>


                </asp:Panel>
                <br />
                <asp:Panel ID="Panel10" runat="server">
                    <div class="row text-center">
                        <asp:Button ID="btn_process" runat="server" class="btn btn-primary" OnClick="btn_process_Click" Text="Submit" />
                    </div>
                </asp:Panel>
            </div>
            <br />
            <div class="container-fluid">
                <asp:Panel ID="Panel1" runat="server" Width="100%" CssClass="panel panel-primary" Style="border-color: #d0cdcd; background: beige;">

                    <asp:HiddenField ID="hidden_month" runat="server" />
                    <asp:HiddenField ID="hidden_year" runat="server" />
                    <div>
                        <div class="col-sm-3 col-xs-12">

                            <h5 class="text-left"><a data-toggle="modal" href="#myModal">Color Desc</a></h5>
                        </div>
                        <div class="col-sm-6 col-xs-12">
                            <h4 class="text-center">
                                <asp:LinkButton ID="LinkButton2" Text=" < " OnClientClick="validate();" OnClick="LinkButton2_Click" runat="server" />&nbsp;&nbsp;
                                                <asp:Label ID="lbl_month_year" runat="server" Text="" />
                                &nbsp;&nbsp;
                                                    <asp:LinkButton ID="LinkButton3" Text=" > "
                                                        OnClick="LinkButton3_Click" OnClientClick="validate();" runat="server" />
                            </h4>
                        </div>
                    </div>
                    <div class="container-fluid">
                        <asp:GridView ID="gv_attendance" runat="server" class="table" AutoGenerateColumns="False" CellPadding="1"
                             ForeColor="#333333" OnPreRender="gv_attendance_PreRender" OnRowDataBound="shiftcalendar_RowDataBound" 
                            DataKeyNames="emp_code" Width="100%" Height="50%" Style="border-collapse: collapse;" >
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="True" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />

                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            <Columns>
                                <asp:TemplateField ControlStyle-Width="45px" HeaderText="Sr. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_srnumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="45px" HeaderText="Employee Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblempcode" runat="server" Text='<%# Eval("EMP_CODE")%>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:BoundField DataField="EMP_NAME" HeaderText="Employee Name" SortExpression="EMP_NAME"  ItemStyle-Width="45%" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="TOT_DAYS_PRESENT" HeaderText="Present Days" SortExpression="TOT_DAYS_PRESENT" ItemStyle-Width="45%"/>
                                <asp:BoundField DataField="TOT_DAYS_ABSENT" HeaderText="Absent Days" SortExpression="TOT_DAYS_ABSENT" />
                                <asp:BoundField DataField="TOT_LEAVES" HeaderText="Leaves" SortExpression="TOT_LEAVES" />
                                <asp:BoundField DataField="WEEKLY_OFF" HeaderText="Weeks Off" SortExpression="WEEKLY_OFF" />
                                <asp:BoundField DataField="HOLIDAYS" HeaderText="Holidays" SortExpression="HOLIDAYS" />
                                <asp:BoundField DataField="TOT_WORKING_DAYS" HeaderText="Month Days" SortExpression="TOT_WORKING_DAYS" />

                                <asp:TemplateField HeaderStyle-BackColor="#0066FF" HeaderText="OT Hours"  ControlStyle-Width="45px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_ot_hours" runat="server" Text='<%# Eval("ot_hours")%>' Width="50" onkeypress="return isNumberKey(event,this.id)"></asp:TextBox>
                                    <br />
                                         <asp:TextBox ID="txt_total_ot_hours" runat="server" Width="49px" Text='<%# Eval("TOTAL_OT") %>' Style="margin-top: 1em;visibility:hidden;" ></asp:TextBox>

                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#0066FF" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid1" runat="server" Text='<%# Eval("DAY01") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid2" runat="server" Text='<%# Eval("DAY02") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id3">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid3" runat="server" Text='<%# Eval("DAY03") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id4">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid4" runat="server" Text='<%# Eval("DAY04") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id5">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid5" runat="server" Text='<%# Eval("DAY05") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid6" runat="server" Text='<%# Eval("DAY06") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id7">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid7" runat="server" Text='<%# Eval("DAY07") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id8">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid8" runat="server" Text='<%# Eval("DAY08") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id9">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid9" runat="server" Text='<%# Eval("DAY09") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id10">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid10" runat="server" Text='<%# Eval("DAY10") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id11">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid11" runat="server" Text='<%# Eval("DAY11") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id12">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid12" runat="server" Text='<%# Eval("DAY12") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id13">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid13" runat="server" Text='<%# Eval("DAY13") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id14">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid14" runat="server" Text='<%# Eval("DAY14") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id15">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid15" runat="server" Text='<%# Eval("DAY15") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id16">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid16" runat="server" Text='<%# Eval("DAY16") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id17">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid17" runat="server" Text='<%# Eval("DAY17") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id18">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid18" runat="server" Text='<%# Eval("DAY18") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id19">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid19" runat="server" Text='<%# Eval("DAY19") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id20">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid20" runat="server" Text='<%# Eval("DAY20") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id21">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid21" runat="server" Text='<%# Eval("DAY21") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id22">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid22" runat="server" Text='<%# Eval("DAY22") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id23">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid23" runat="server" Text='<%# Eval("DAY23") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id24">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid24" runat="server" Text='<%# Eval("DAY24") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id25">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid25" runat="server" Text='<%# Eval("DAY25") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id26">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid26" runat="server" Text='<%# Eval("DAY26") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id27">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid27" runat="server" Text='<%# Eval("DAY27") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id28">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid28" runat="server" Text='<%# Eval("DAY28") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id29">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid29" runat="server" Text='<%# Eval("DAY29") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id30">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid30" runat="server" Text='<%# Eval("DAY30") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="id31">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid31" runat="server" Text='<%# Eval("DAY31") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="1">
                                    <ItemTemplate>
                                        <asp:DropDownList AppendDataBoundItems="true" ID="DropDownList1" runat="server" SelectedValue='<%# Bind("DAY01") %>'>
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txt_ot_1" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY01") %>' Style="margin-top: 1em;"></asp:TextBox>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList1" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY01") %>' Width="100%">
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="2">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList2" runat="server" SelectedValue='<%# Bind("DAY02") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox ID="txt_ot_2" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY02") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList2" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY02") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="3">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList3" runat="server" SelectedValue='<%# Bind("DAY03") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                        <asp:TextBox ID="txt_ot_3" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY03") %>' Style="margin-top: 1em;"></asp:TextBox><%-- --%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList3" Width="100%" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY03") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="4">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList4" runat="server" SelectedValue='<%# Bind("DAY04") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_4" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY04") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList4" Width="100%" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY04") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="5">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList5" runat="server" SelectedValue='<%# Bind("DAY05") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_5" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY05") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList5" Width="100%" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY05") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="6">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList6" runat="server" SelectedValue='<%# Bind("DAY06") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_6" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY06") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList6" Width="100%" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY06") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="7">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList7" runat="server" SelectedValue='<%# Bind("DAY07") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_7" runat="server" Text='<%# Eval("OT_DAILY_DAY07") %>' Style="margin-top: 1em;" Width="49px"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList7" Width="100%" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY07") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="8">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList8" runat="server" SelectedValue='<%# Bind("DAY08") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_8" runat="server" Text='<%# Eval("OT_DAILY_DAY08") %>' Width="49px" Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList8" Width="100%" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY08") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="9">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList9" runat="server" SelectedValue='<%# Bind("DAY09") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_9" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY09") %>' Style="margin-top: 1em;"></asp:TextBox><%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList9" Width="100%" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY09") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="10">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList10" runat="server" SelectedValue='<%# Bind("DAY10") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_10" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY10") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList10" Width="100%" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY10") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="11">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList11" runat="server" SelectedValue='<%# Bind("DAY11") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_11" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY11") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>
                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList11" Width="100%" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY11") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="12">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList12" runat="server" SelectedValue='<%# Bind("DAY12") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_12" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY12") %>' Style="margin-top: 1em;"></asp:TextBox><%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList12" Width="100%" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY12") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="13">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList13" runat="server" SelectedValue='<%# Bind("DAY13") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_13" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY13") %>' Style="margin-top: 1em;"></asp:TextBox><%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList13" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY13") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="14">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList14" runat="server" SelectedValue='<%# Bind("DAY14") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_14" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY14") %>' Style="margin-top: 1em;"></asp:TextBox><%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList14" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY14") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="15">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList15" runat="server" SelectedValue='<%# Bind("DAY15") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_15" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY15") %>' Style="margin-top: 1em;"></asp:TextBox><%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList15" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY15") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="16">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList16" runat="server" SelectedValue='<%# Bind("DAY16") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_16" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY16") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList16" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY16") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="17">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList17" runat="server" SelectedValue='<%# Bind("DAY17") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_17" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY17") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList17" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY17") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="18">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList18" runat="server" SelectedValue='<%# Bind("DAY18") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_18" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY18") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList18" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY18") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="19">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList19" runat="server" SelectedValue='<%# Bind("DAY19") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_19" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY19") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList19" Width="100%" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY19") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="20">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList20" runat="server" SelectedValue='<%# Bind("DAY20") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_20" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY20") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList20" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY20") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="21">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList21" runat="server" SelectedValue='<%# Bind("DAY21") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_21" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY21") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList21" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY21") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="22">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList22" runat="server" SelectedValue='<%# Bind("DAY22") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_22" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY22") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList22" Width="100%" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY22") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="23">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList23" runat="server" SelectedValue='<%# Bind("DAY23") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_23" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY23") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList23" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY23") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="24">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList24" runat="server" SelectedValue='<%# Bind("DAY24") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_24" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY24") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>
                                        <br />
                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList24" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY24") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="25">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList25" runat="server" SelectedValue='<%# Bind("DAY25") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                        <asp:TextBox ID="txt_ot_25" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY25") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList25" Width="100%" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY25") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="26">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList26" runat="server" SelectedValue='<%# Bind("DAY26") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                        <asp:TextBox ID="txt_ot_26" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY26") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>


                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList26" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY26") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="27">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList27" runat="server" SelectedValue='<%# Bind("DAY27") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_27" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY27") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList27" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY27") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="28">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList28" runat="server" SelectedValue='<%# Bind("DAY28") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_28" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY28") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%--Text='<%# Eval("OT_DAILY_DAY28") %>'--%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList28" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY28") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="29">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList29" runat="server" SelectedValue='<%# Bind("DAY29") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_29" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY29") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%--Text='<%# Eval("OT_DAILY_DAY29") %>'--%>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList29" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY29") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="30">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList30" runat="server" SelectedValue='<%# Bind("DAY30") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>
                                        <br />

                                        <asp:TextBox ID="txt_ot_30" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY30") %>' Style="margin-top: 1em;"></asp:TextBox>
                                        <%----%>
                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList30" Width="100%" Style="margin-top: 1em;" runat="server" SelectedValue='<%# Bind("OT_DAY30") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="31">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList31" runat="server" SelectedValue='<%# Bind("DAY31") %>' AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Set" />
                                            <asp:ListItem Value="P" Text="P" />
                                            <asp:ListItem Value="A" Text="A" />
                                            <asp:ListItem Value="HD" Text="HD" />
                                            <asp:ListItem Value="CL" Text="CL" />
                                            <asp:ListItem Value="PH" Text="PH" />
                                            <asp:ListItem Value="ML" Text="ML" />
                                            <asp:ListItem Value="PL" Text="PL" />
                                            <asp:ListItem Value="W" Text="W" />
                                            <asp:ListItem Value="H" Text="H" />
                                        </asp:DropDownList>
                                        <br />
                                        <br />

                                        <asp:TextBox ID="txt_ot_31" runat="server" Width="49px" Text='<%# Eval("OT_DAILY_DAY31") %>' Style="margin-top: 1em;"></asp:TextBox>

                                        <asp:DropDownList AppendDataBoundItems="true" ID="OT_DropDownList31" Style="margin-top: 1em;" Width="100%" runat="server" SelectedValue='<%# Bind("OT_DAY31") %>'>
                                            <asp:ListItem Value="0" Text="0" />
                                            <asp:ListItem Value="2" Text="2" />
                                            <asp:ListItem Value="8" Text="8" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>


                    <%--</ContentTemplate></asp:UpdatePanel>--%>
                    <br />
                    <div align="center">
                        <br />
                        <asp:Button ID="btn_save" runat="server" Text="Save" class="btn btn-primary" OnClick="btn_save_Click" OnClientClick="return req_validation();" />
                        <asp:Button ID="btn_approve" runat="server" Text="Approve" class="btn btn-primary" OnClick="btn_approve_Click" OnClientClick="return R_validation();" />
                    </div>

                    <br />
                    <br />

                    <asp:Panel ID="att_upload_panel" runat="server">
                        <div class="row" id="files_upload" runat="server">
                            <div class="col-sm-2 col-xs-12"></div>
                            <div class="col-sm-2 col-xs-12"></div>
                            <div class="col-sm-2 col-xs-12">
                                Description :
                                      <asp:TextBox ID="txt_document1" runat="server" class="form-control text_box" onkeypress="return  isNumber1(event)" MaxLength="200"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <span class="text_margin">File to Upload :</span>

                                <asp:FileUpload ID="document1_file" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                            </div>
                            <div class="col-sm-3 col-xs-12 text-left" style="padding-top: 1%">
                                <asp:Button ID="upload" runat="server" class="btn btn-primary" Text=" Upload " OnClientClick="return valid_upload();" OnClick="upload_Click" />
                            </div>
                            <div class="col-sm-2 col-xs-12"><b style="color: #f00; text-align: center">Note :</b> Only JPG, PNG,JPEG and PDF files will be uploaded.</div>

                            <br />

                        </div>
                        <br />
                    </asp:Panel>


                 
                </asp:Panel>


                   <asp:Panel runat="server" ID="Panel21">
                         
                     
                        <asp:Button ID="btn_attendance_upload" runat="server" Text="Download Report"  OnClick="btn_attendance_upload_Click" class="btn btn-primary" />
                         
                        <div class=" col-sm-4 col-xs-12" style="margin-top: 9px;">
                           File :<span class="text-red">*</span>
                           <asp:FileUpload ID="FileUpload1" runat="server" />
                        </div>

                        <asp:Button ID="btn_report_update" runat="server" Text="Update Report" OnClick="btn_report_update_Click" class="btn btn-primary " OnClientClick="return valid_upload1();" />

                         </asp:Panel>
            </div>
            <br />
            <div class="container-fluid">
                <asp:Panel ID="gv_reject_panel" runat="server" CssClass="grid-view panel panel-primary" Style="overflow-x: hidden; background-color: beige; border-color: #d0cdcd">

                    <div class="row text-center">
                        <h3>Reject Attendance By Finance</h3>
                    </div>
                    <div class="container">
                        <asp:GridView ID="grid_reject_attendace" runat="server" ForeColor="#333333" AutoGenerateColumns="false" class="table" OnRowDataBound="grid_reject_attendace_RowDataBound" DataKeyNames="" OnPreRender="grid_reject_attendace_PreRender">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />

                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="50" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            <Columns>


                                <asp:BoundField DataField="client_name" HeaderText="CLIENT NAME"
                                    SortExpression="client_name" />
                                <asp:BoundField DataField="state_name" HeaderText="STATE NAME"
                                    SortExpression="state_name" />
                                <asp:BoundField DataField="branch_name" HeaderText="BRANCH NAME"
                                    SortExpression="branch_name" />
                                <asp:BoundField DataField="month_year" HeaderText="MONTH YEAR"
                                    SortExpression="month_year" />
                                <asp:BoundField DataField="file_name" HeaderText="Attendance_file"
                                    SortExpression="file_name" />
                                <asp:TemplateField HeaderText="DOWNLOAD">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_attendance" runat="server" CausesValidation="false" Text="Attendance Sheet" CommandArgument='<%# Eval("file_name") +","+ Eval("branch_name")%>' OnCommand="lnkDownload_Command" CssClass="btn btn-primary"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="rejected_reason" HeaderText="REJECTED REASON"
                                    SortExpression="rejected_reason" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:Panel>
            </div>
            <br />
            <asp:Panel ID="gv_approve_panel" runat="server" CssClass="grid-view">

                <div class="row text-center">
                    <h3>Approve Attendance By Finance</h3>
                </div>
                <div class="container">
                    <asp:GridView ID="grid_approve_attendace" runat="server" ForeColor="#333333" AutoGenerateColumns="false" class="table" OnRowDataBound="grid_reject_attendace_RowDataBound" DataKeyNames="" OnPreRender="grid_reject_attendace_PreRender">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />

                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="50" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        <Columns>


                            <asp:BoundField DataField="client_name" HeaderText="CLIENT NAME"
                                SortExpression="client_name" />
                            <asp:BoundField DataField="state_name" HeaderText="STATE NAME"
                                SortExpression="state_name" />
                            <asp:BoundField DataField="branch_name" HeaderText="BRANCH NAME"
                                SortExpression="branch_name" />
                            <asp:BoundField DataField="month_year" HeaderText="MONTH YEAR"
                                SortExpression="month_year" />
                            <asp:BoundField DataField="file_name" HeaderText="Attendance_file"
                                SortExpression="file_name" />
                            <asp:TemplateField HeaderText="DOWNLOAD">
                                <ItemStyle Width="20px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_attendance" runat="server" CausesValidation="false" Text="Attendance Sheet" CommandArgument='<%# Eval("file_name") +","+ Eval("branch_name")%>' OnCommand="lnkDownload_Command" CssClass="btn btn-primary"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <asp:Panel ID="gv_deployment_panel" runat="server" CssClass="grid-view">

                <div class="row text-center">
                    <h3>Branch Having No Deployment</h3>
                </div>
                <asp:GridView ID="gv_deployment" runat="server" ForeColor="#333333" AutoGenerateColumns="false" class="table">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />

                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    <Columns>


                        <asp:BoundField DataField="client_name" HeaderText="CLIENT NAME"
                            SortExpression="client_name" />
                        <asp:BoundField DataField="STATE_NAME" HeaderText="STATE NAME"
                            SortExpression="STATE_NAME" />
                        <asp:BoundField DataField="UNIT_NAME" HeaderText="BRANCH NAME"
                            SortExpression="UNIT_NAME" />
                        <asp:TemplateField HeaderText="APPROVE BRANCH">
                            <ItemStyle Width="20px" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_approve" runat="server" CausesValidation="false" Text="APPROVE" CommandArgument='<%# Eval("unit_code")%>' OnCommand="lnk_deployment_Command" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure You want to  Approve Branch?')"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HOLD BRANCH">
                            <ItemStyle Width="20px" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_reject" runat="server" CausesValidation="false" Text="HOLD" CommandArgument='<%#Eval("unit_code") %>' OnCommand="lnk_hold_Command" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure You want to  Hold Branch?')"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </asp:Panel>
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content" style="width: 680px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4>Color Description</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12" style="padding-left: 1%;">
                                    <table class="table table-bordered">
                                        <tr style="background-color: #d0cdcd; text-align: center; font-weight: bold;">
                                            <td style="background-color: #fff;">Status</td>
                                            <td>Present </td>
                                            <td>Absent  </td>
                                            <td>Half Day  </td>
                                            <td>Casual Leave  </td>
                                            <td>Paternity Leaves </td>
                                            <td>Maternity Leave  </td>
                                            <td>Privilege Leave  </td>
                                            <td>Weekly Off  </td>
                                            <td>Holiday </td>
                                            <%--<td>Company Off    </td>--%>
                                        </tr>
                                        <tr style="text-align: center; font-weight: bold;">
                                            <td style="background-color: #fff;">Symbol</td>
                                            <td style="background-color: LimeGreen">P</td>
                                            <td style="background-color: red;">A</td>
                                            <td style="background-color: orange;">HD</td>
                                            <td style="background-color: yellow;">CL</td>
                                            <td style="background-color: Aqua;">PH</td>
                                            <td style="background-color: Wheat;">ML</td>
                                            <td style="background-color: YellowGreen;">PL</td>
                                            <td style="background-color: Violet;">W</td>
                                            <td style="background-color: Pink;">H</td>
                                            <%--<td style="background-color: Peru;">CO</td>--%>
                                        </tr>

                                    </table>
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
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal fade" id="Div1" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content" style="width: 400px; height: auto">
                                <div class="modal-header">
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-sm-12" style="padding-left: 1%;">
                                                <asp:GridView ID="gv_emp_details" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade" id="branch_deployment" role="dialog" data-dismiss="modal" href="#lost">

                        <div class="modal-dialog">
                            <div class="modal-content" style="width: 458px;">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4>Branches Having  No Deployment</h4>
                                </div>

                                <div class="modal-body">
                                    <asp:Panel ID="Panel2" runat="server" CssClass="grid-view8">
                                        <asp:GridView ID="gv_branch_deployment" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                            <Columns>
                                                <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
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
                    <div class="modal fade" id="attendance" role="dialog" data-dismiss="modal" href="#lost">

                        <div class="modal-dialog">
                            <div class="modal-content" style="width: 458px;">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4>Branches With No Attendance</h4>
                                </div>

                                <div class="modal-body">
                                    <asp:Panel ID="Panel4" runat="server" CssClass="grid-view8">
                                        <asp:GridView ID="gridService" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                            <Columns>
                                                <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
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
                    <div class="modal fade" id="branch_close" role="dialog" data-dismiss="modal" href="#lost">

                        <div class="modal-dialog">
                            <div class="modal-content" style="width: 458px;">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4>Branches Have Closed</h4>
                                </div>

                                <div class="modal-body">
                                    <asp:Panel ID="Panel7" runat="server" CssClass="grid-view">
                                        <asp:GridView ID="gv_branch_close" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                            <Columns>
                                                <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
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
                    <div class="modal fade" id="approve_attendance" role="dialog" data-dismiss="modal" href="#lost">

                        <div class="modal-dialog">
                            <div class="modal-content" style="width: 458px;">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4>Branches  Approve By Admin</h4>
                                </div>

                                <div class="modal-body">
                                    <asp:Panel ID="Panel5" runat="server" CssClass="grid-view8">
                                        <asp:GridView ID="gv_approved_attendance" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                            <Columns>
                                                <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
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


                    <div class="modal fade" id="approve_attendance_finance" role="dialog" data-dismiss="modal" href="#lost">

                        <div class="modal-dialog">
                            <div class="modal-content" style="width: 458px;">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4>Branches Approve By Finance</h4>
                                </div>

                                <div class="modal-body">
                                    <asp:Panel ID="Panel8" runat="server" CssClass="grid-view8">
                                        <asp:GridView ID="gv_appr_att_finance" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                            <Columns>
                                                <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
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
                        <br />
                    </div>



                            <div class="modal fade" id="Div6" role="dialog" data-dismiss="modal" href="#lost">

                                <div class="modal-dialog">
                                    <div class="modal-content" style="width: 458px;">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4>Branches  Approve By Admin</h4>
                                        </div>

                                        <div class="modal-body">
                                            <asp:Panel ID="Panel16" runat="server" CssClass="grid-view8">
                                                <asp:GridView ID="gv_approve_admin_conv" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                                    <Columns>
                                                        <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
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

                        </ContentTemplate>

            </asp:UpdatePanel>
                </div>

                    <div id="menu7">
                    <%-- <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                           <ContentTemplate>
                     --%>
               
                <div class="row">
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>


                    <div class="col-sm-2 col-xs-12">
                       <b> Client Name : <span class="text-red">*</span></b>
                        <asp:DropDownList ID="ddl_client_service" DataValueField="client_code" DataTextField="client_name" AutoPostBack="true"  runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_client_service_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        <b>State :<span class="text-red">*</span></b>
                        <asp:DropDownList ID="ddl_state_service" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_state_service_SelectedIndexChanged"/>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Branch Name : <span class="text-red">*</span></b>
                        <asp:DropDownList ID="ddl_branch_service" runat="server" class="form-control text_box">
                        </asp:DropDownList>
                    </div>
                     <div class="col-sm-2 col-xs-12">
                        <b>Month / Year :<span class="text-red">*</span></b>
                        <asp:TextBox ID="txt_date" runat="server" Visible="true" class="form-control date-picker text_box"></asp:TextBox>

                    </div>

                     <div class="col-sm-4 col-xs-12">
                        <br />
                        <asp:Button ID="btn_process_service" runat="server" Text="Process" class="btn btn-primary" OnClick="btn_process_service_Click" OnClientClick="return Req_validation_service();" />
                        <asp:Button ID="btn_close_service" runat="server" Text="Close" class="btn btn-danger" />
                    </div>

                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
                    
                </div>
                               <br />
                               <br />
                               <div class="row" id="RandM_panel" runat="server">
             <div class="container-fluid">
<div class="col-sm-2 col-xs-12">
                        Party Name : <span class="text-red">*</span>
                        <asp:TextBox ID="txt_party_name" runat="server" Visible="true" class="form-control text_box"></asp:TextBox>
                    </div>
                                    <div class="col-sm-2 col-xs-12">
                       HelpDesk Request No :<span class="text-red">*</span>
                        <asp:TextBox ID="txt_help_req_no" runat="server" Visible="true" class="form-control text_box"></asp:TextBox>

                    </div>
                                    <div class="col-sm-2 col-xs-12">
                       Amount :<span class="text-red">*</span>
                        <asp:TextBox ID="txt_amount" runat="server" Visible="true" class="form-control text_box" onKeyPress="return isNumber_dot_amt_r_m(event)" ></asp:TextBox>

                    </div>
                                    <div class="col-sm-2 col-xs-12">
                       Bank Account No :<span class="text-red">*</span>
                        <asp:TextBox ID="txt_bank_acc_no" runat="server" Visible="true"  MaxLength="30" class="form-control text_box"  onKeyPress="return isNumber_acc(event)"></asp:TextBox>

                    </div>
 <div class="col-sm-2 col-xs-12">
                       IFSC :<span class="text-red">*</span>
                        <asp:TextBox ID="txt_ifsc_code" runat="server" Visible="true" MaxLength="10" class="form-control text_box" AutoPostBack="true" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>

                    </div>
                                   <br />
               <br />
               <div class="row">
                                      <br />
                                       <br />
                                        <div class="col-sm-2 col-xs-12">
                                     <table class="table table-striped" style="width: 20%">
                            <tr>
                                <td>File to Upload :
                                                <asp:FileUpload ID="r_m_upload" runat="server" meta:resourcekey="photo_uploadResource1" onchange="ValidateSingleInput(this);" />
                                                <span style="color: red; font-size: 8px; font-weight: bold;">Only JPG,JPEG,GIF,PDF</span></td>
                                            <td>
                                              <%--  <asp:Button ID="btn_upload_r_m" runat="server" class="btn btn-primary" Style="margin-top: 1em" OnClick="btn_upload_r_m_Click" Text=" Upload " OnClientClick="return r_m_service_upload();"/>--%>
                                            </td>
                                        </tr>
                                    </table>
                                            </div>
                   </div>
                 </div>
                       <div class="row text-center">
                        <br />
                        <asp:Button ID="btn_save_service" runat="server" Text="Save" class="btn btn-primary" AutoPostBack="true" OnClick="btn_save_service_Click" OnClientClick="return Req_save_val_service();" />
                        <asp:Button ID="btn_approve_service" runat="server" Text="Approve" class="btn btn-primary" Visible="false" AutoPostBack="true" OnClick="btn_approve_service_Click" />
                      <asp:Button ID="btn_close_service_rm" runat="server" Text="Close" class="btn btn-danger" />
                           </div>
                    
                                    <br />
                             
                               </div>
                               <%--</ContentTemplate>

            </asp:UpdatePanel>--%>
                        <br />
                       
                             <asp:Panel ID="gv_r_m" runat="server" CssClass="grid-view" Style="overflow-x:hidden;">

                <div class="row text-center">
                    <h3>R&M Approve By Admin</h3>
                </div>
                <asp:GridView ID="gv_rm" runat="server" ForeColor="#333333" AutoGenerateColumns="false" class="table" OnRowDataBound="gv_rm_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />

                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    <Columns>
                         <asp:BoundField DataField="ID" HeaderText="ID"
                            SortExpression="ID" />
                        <asp:TemplateField HeaderText="Sr No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                        <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chk_gv_header_r_m" runat="server" Text=" SELECT " />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_client" runat="server" CssClass="center-block" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        <asp:BoundField DataField="party_name" HeaderText="PARTY NAME"
                            SortExpression="party_name" />
                        <asp:BoundField DataField="help_req_number" HeaderText="HELP REQ NUMBER"
                            SortExpression="help_req_number" />
                        <asp:BoundField DataField="amount" HeaderText="AMOUNT"
                            SortExpression="amount" />
                         <asp:BoundField DataField="bank_acc_no" HeaderText="BANK ACC NUMBER "
                            SortExpression="bank_acc_no" />
                        <asp:BoundField DataField="ifsc_code" HeaderText="IFSC CODE "
                            SortExpression="ifsc_code" />
                         <asp:BoundField DataField="r_m_status" HeaderText="STATUS "
                            SortExpression="r_m_status" />
                         <asp:TemplateField HeaderText="DOWNLOAD">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_R_M_download_gv" runat="server" CausesValidation="false" Text="Download" CommandArgument='<%#Eval("image_name") %>' OnCommand="lnk_R_M_download_gv_Command" CssClass="btn btn-primary" Style="color: white"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="DELETE">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_R_M" runat="server" CausesValidation="false" Text="Delete" CommandArgument='<%#Eval("ID") %>'  CssClass="btn btn-primary" Style="color: white" OnCommand="lnk_R_M_Command" OnClientClick="return confirm('Are you sure You want Delete Record?')"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </asp:Panel>
                       <div class="row text-center" id="btn_r_m" runat="server">
                                <asp:Button ID="btn_r_m_aprrove" runat="server" class="btn btn-primary" Text="Approve" OnClick="btn_r_m_aprrove_Click"  />
                            </div>
                         <asp:Panel ID="process_gv" runat="server" CssClass="grid-view" Style="overflow-x:hidden;" >
               
                <asp:GridView ID="process_gv_r_m" runat="server" ForeColor="#333333" AutoGenerateColumns="false" class="table" OnRowDataBound="process_gv_r_m_RowDataBound" >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />

                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    <Columns>
                         <asp:BoundField DataField="ID" HeaderText="ID"
                            SortExpression="ID" />
                        <asp:TemplateField HeaderText="Sr No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                        <asp:BoundField DataField="party_name" HeaderText="PARTY NAME"
                            SortExpression="party_name" />
                        <asp:BoundField DataField="help_req_number" HeaderText="HELP REQ NUMBER"
                            SortExpression="help_req_number" />
                        <asp:BoundField DataField="amount" HeaderText="AMOUNT"
                            SortExpression="amount" />
                         <asp:BoundField DataField="bank_acc_no" HeaderText="BANK ACC NUMBER "
                            SortExpression="bank_acc_no" />
                        <asp:BoundField DataField="ifsc_code" HeaderText="IFSC CODE "
                            SortExpression="ifsc_code" />
                         <asp:BoundField DataField="r_m_status" HeaderText="STATUS "
                            SortExpression="r_m_status" />
                         <asp:TemplateField HeaderText="DOWNLOAD">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_R_M_download_gv" runat="server" CausesValidation="false" Text="R&M Sheet Download" CommandArgument='<%#Eval("image_name") %>' CssClass="btn btn-primary" Style="color: white" OnCommand="lnk_R_M_download_gv_Command"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </asp:Panel>
                </div>

                   <div id="menu8">
                   <%--  <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                           <ContentTemplate>--%>
                     
               
                <div class="row">
                   
  <div class="col-sm-2 col-xs-12">
                      <b>  Client Name : <span class="text-red">*</span></b>
                        <asp:DropDownList ID="ddl_client_adm"  DataValueField="client_code" DataTextField="client_name" AutoPostBack="true"  runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_client_adm_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> State :<span class="text-red">*</span></b>
                        <asp:DropDownList ID="ddl_state_adm" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_state_adm_SelectedIndexChanged" />
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Branch Name : <span class="text-red">*</span></b>
                        <asp:DropDownList ID="ddl_branch_adm" runat="server" class="form-control text_box">
                        </asp:DropDownList>
                    </div>
                     <div class="col-sm-2 col-xs-12">
                       <b> Month / Year :<span class="text-red">*</span></b>
                        <asp:TextBox ID="txt_date_adm" runat="server" Visible="true" class="form-control date-picker text_box"></asp:TextBox>

                    </div>
                       <div class="col-sm-4 col-xs-12">
                        <br />
                        <asp:Button ID="btn_process_administrative" runat="server" Text="Process" class="btn btn-primary" AutoPostBack="true" OnClick="btn_process_administrative_Click" OnClientClick="return Req_validation_adm();" />
                        <asp:Button ID="btn_close_administrative" runat="server" Text="Close" class="btn btn-danger" />
                    </div>
                    
                </div>
                               <br />
                               <br />
                               <div class="row" id="administrative_panel" runat="server">
                               <div class="container-fluid">
<div class="col-sm-2 col-xs-12">
                        Party Name : <span class="text-red">*</span>
                        <asp:TextBox ID="txt_party_adm" runat="server" Visible="true" class="form-control text_box"></asp:TextBox>
                    </div>
                                    <div class="col-sm-2 col-xs-12">
                      Days :<span class="text-red">*</span>
                        <asp:TextBox ID="txt_req_no_adm" runat="server" Visible="true" class="form-control text_box" onKeyPress="return isNumber_acc(event)"></asp:TextBox>

                    </div>
                                    <div class="col-sm-2 col-xs-12">
                       Amount :<span class="text-red">*</span>
                        <asp:TextBox ID="txt_amount_req" runat="server" Visible="true" class="form-control text_box" onKeyPress="return isNumber_dot_amt_r_m(event)"></asp:TextBox>

                    </div>
                                    <div class="col-sm-2 col-xs-12">
                       Bank Account No :<span class="text-red">*</span>
                        <asp:TextBox ID="txt_bank_account_adm" runat="server" Visible="true" MaxLength="30" class="form-control text_box"  onKeyPress="return isNumber_acc(event)"></asp:TextBox>

                    </div>
 <div class="col-sm-2 col-xs-12">
                       IFSC :<span class="text-red">*</span>
                        <asp:TextBox ID="txt_ifsc_adm" runat="server" Visible="true" class="form-control text_box" MaxLength="10" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>

                    </div>
                                   <br />
                                   <br />
                                   <div class="row" >
                                      <br />
                                       <br />
                                        <div class="col-sm-2 col-xs-12">
                                     <table class="table table-striped" style="width: 20%">
                            <tr>
                                <td>File to Upload :
                                                <asp:FileUpload ID="administrative_upload" runat="server" meta:resourcekey="photo_uploadResource1" onchange="ValidateSingleInput(this);" />
                                                <span style="color: red; font-size: 8px; font-weight: bold;">Only JPG,JPEG,GIF,PDF</span></td>
                                            <td>
                                                <%--<asp:Button ID="btn_upload_admini" runat="server" class="btn btn-primary" Style="margin-top: 1em" OnClick="btn_upload_admini_Click" Text=" Upload " OnClientClick="return adminitrative_upload();"/>--%>
                                            </td>
                                        </tr>
                                    </table>
                                         </div>
                                       </div>
                                    <br />
                               </div>
                                    <div class="row text-center">
                        <br />
                        <asp:Button ID="btn_save_administrative" runat="server" Text="Save" class="btn btn-primary" AutoPostBack="true" OnClick="btn_save_administrative_Click" OnClientClick="return Req_save_val_administartive();" />
                        <asp:Button ID="btn_approve_administrative" runat="server" Text="Approve" class="btn btn-primary" AutoPostBack="true" OnClick="btn_approve_administrative_Click" Visible="false"/>
                      <asp:Button ID="btn_close_administrati" runat="server" Text="Close" class="btn btn-danger" />
                           </div>
                                   </div>
                               <%--</ContentTemplate>

            </asp:UpdatePanel>--%>
                        <asp:Panel ID="admin_gv_ap" runat="server" CssClass="grid-view" Style="overflow-x:hidden;">

                <div class="row text-center">
                    <h3>Administrative Approve By Admin</h3>
                </div>
                <asp:GridView ID="gv_admin" runat="server" ForeColor="#333333" AutoGenerateColumns="false" class="table" OnRowDataBound="gv_admin_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />

                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    <Columns>
                         <asp:BoundField DataField="ID" HeaderText="id"
                            SortExpression="ID" />
                         <asp:TemplateField HeaderText="Sr No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                        <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chk_gv_header_admin" runat="server" Text=" SELECT " />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_client" runat="server" CssClass="center-block" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        <asp:BoundField DataField="party_name" HeaderText="PARTY NAME"
                            SortExpression="party_name" />
                        <asp:BoundField DataField="days" HeaderText="HELP REQ NUMBER"
                            SortExpression="days" />
                        <asp:BoundField DataField="amount" HeaderText="AMOUNT"
                            SortExpression="amount" />
                         <asp:BoundField DataField="bank_acc_no" HeaderText="BANK ACC NUMBER "
                            SortExpression="bank_acc_no" />
                        <asp:BoundField DataField="ifsc_code" HeaderText="IFSC CODE "
                            SortExpression="ifsc_code" />
                         <asp:BoundField DataField="admini_status" HeaderText="STATUS "
                            SortExpression="admini_status" />
                         <asp:TemplateField HeaderText="DOWNLOAD">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_admin_download_gv" runat="server" CausesValidation="false" Text="Download" CommandArgument='<%#Eval("image_name") %>' OnCommand="lnk_admin_download_gv_Command" CssClass="btn btn-primary" Style="color: white"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="DELETE">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_admin" runat="server" CausesValidation="false" Text="Delete" CommandArgument='<%#Eval("ID") %>'  CssClass="btn btn-primary" Style="color: white" OnCommand="lnk_admin_Command"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </asp:Panel>
                        <div class="row text-center" id="btn_admin_ap" runat="server">
                                <asp:Button ID="btn_admin_aprrove" runat="server" class="btn btn-primary" Text="Approve" OnClick="btn_admin_aprrove_Click"  />
                            </div>
                       <asp:Panel ID="process_admin_panel" runat="server" CssClass="grid-view" Style="overflow-x:hidden;" >
               
                <asp:GridView ID="process_admin" runat="server" ForeColor="#333333" AutoGenerateColumns="false" class="table" OnRowDataBound="process_admin_RowDataBound" >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />

                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    <Columns>
                         <asp:BoundField DataField="ID" HeaderText="ID"
                            SortExpression="ID" />
                        <asp:TemplateField HeaderText="Sr No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                        <asp:BoundField DataField="party_name" HeaderText="PARTY NAME"
                            SortExpression="party_name" />
                        <asp:BoundField DataField="days" HeaderText="DAYS"
                            SortExpression="days" />
                        <asp:BoundField DataField="amount" HeaderText="AMOUNT"
                            SortExpression="amount" />
                         <asp:BoundField DataField="bank_acc_no" HeaderText="BANK ACC NUMBER "
                            SortExpression="bank_acc_no" />
                        <asp:BoundField DataField="ifsc_code" HeaderText="IFSC CODE "
                            SortExpression="ifsc_code" />
                         <asp:BoundField DataField="admini_status" HeaderText="STATUS "
                            SortExpression="admini_status" />
                         <asp:TemplateField HeaderText="DOWNLOAD">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_admin_download_gv" runat="server" CausesValidation="false" Text="Download" CommandArgument='<%#Eval("image_name") %>' CssClass="btn btn-primary" Style="color: white" OnCommand="lnk_admin_download_gv_Command1"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </asp:Panel>
                </div>




                 <div id="menu5">
                    
                      <div class="row">

                    <div class="col-sm-2 col-xs-12">
                        <b>Select Month :</b><span class="text-red">*</span>
                        <asp:TextBox ID="txt_date_conveyance" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-sm-2 col-xs-12 text-left">
                        <b>Client Name :</b>   
                <asp:DropDownList ID="con_ddl_client" class="form-control pr_state js-example-basic-single" runat="server" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged1" AutoPostBack="true">
                </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                        <b>State Name :</b>   
                 <asp:DropDownList ID="con_ddl_state" runat="server" DataTextField="STATE" DataValueField="STATE" class="form-control text_box" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged1" AutoPostBack="true">
                 </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                        <b>Branch Name :</b>   
                <asp:DropDownList ID="con_ddl_unitcode" class="form-control pr_state js-example-basic-single" runat="server" OnSelectedIndexChanged="ddlunitselect_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                        <b>Employee Type :</b>
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
                        <b>Employee Name :</b>    
                <asp:DropDownList ID="ddl_employee" class="form-control pr_state js-example-basic-single" runat="server" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                    </div>

                           </div>
                     <br />
                     <br />
                            <div id="Div2">
                 
                     <div id="Div3" style="background: beige;">
                <asp:HiddenField ID="HiddenField2" Value="0" runat="server" />
                <ul>
                    <li><a id="A3"  href="#e1" runat="server">Employee Conveyance</a></li>

                    <li><a id="A4" href="#e2" runat="server">Driver Conveyance</a></li>
                </ul>
                         <div id="e1">
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
                                    <div class="row">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>


                                                <div class="modal fade" id="emp_con_not_approve_admin" role="dialog" data-dismiss="modal" href="#lost">

                                                    <div class="modal-dialog">
                                                        <div class="modal-content" style="width: 458px;">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                <h4>Branches Not Approve By Admin</h4>
                                                            </div>

                                                            <div class="modal-body">
                                                                <asp:Panel ID="Panel15" runat="server" CssClass="grid-view8">
                                                                    <asp:GridView ID="gridview_conv_1" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div5">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                                            <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                                            <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
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

                                                <div class="modal fade" id="emp_con_approve_admin" role="dialog" data-dismiss="modal" href="#lost">

                                                    <div class="modal-dialog">
                                                        <div class="modal-content" style="width: 458px;">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                <h4>Branches  Approve By Admin</h4>
                                                            </div>

                                                            <div class="modal-body">
                                                                <asp:Panel ID="Panel17" runat="server" CssClass="grid-view8">
                                                                    <asp:GridView ID="gv_approved_conveyance" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div5">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                                            <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                                            <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
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

                                                <div class="modal fade" id="emp_con_approve_finance" role="dialog" data-dismiss="modal" href="#lost">

                                                    <div class="modal-dialog">
                                                        <div class="modal-content" style="width: 622px;">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                <h4>Branches  Approve By Finance</h4>
                                                            </div>

                                                            <div class="modal-body">
                                                                <div class="row">
                                                                    <div class="col-sm-12" style="padding-left: 1%;">
                                                                        <asp:GridView ID="gv_reject_conveyance" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div5">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                                                <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                                                <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />

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

                                                <div class="modal fade" id="emp_con_reject_fiance" role="dialog" data-dismiss="modal" href="#lost">

                                                    <div class="modal-dialog">
                                                        <div class="modal-content" style="width: 458px;">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                <h4>Reject By Finance</h4>
                                                            </div>

                                                            <div class="modal-body">
                                                                <asp:Panel ID="Panel18" runat="server" CssClass="grid-view8">
                                                                    <asp:GridView ID="gv_app_att_finance_conv" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div5">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                                            <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                                            <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
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
                                                    <br />
                                                </div>


                                                </div>

                                            </ContentTemplate>

                                        </asp:UpdatePanel>

                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="col-sm-8 col-xs-12"></div>
                                                <div class="col-sm-4 col-xs-12">
                                                    <div style="max-height: 200px; overflow-y: auto; overflow-x: hidden;">
                                                        <asp:Panel ID="Panel_notification_conv" runat="server">

                                                            <table border="1" class="table table-striped">
                                                                <tr style="background-color: #eeeaea">
                                                                    <th>Branch Count</th>
                                                                    <th style="text-align: center">Notification</th>

                                                                </tr>


                                                                <tr>
                                                                    <asp:Panel ID="Panel_not_approve_conv" runat="server">
                                                                        <td class="nt_style"><%=emp_con_remaing%></td>
                                                                        <td style="font: bold;"><a data-toggle="modal" href="#emp_con_not_approve_admin"><b>Branches Not Approved By Admin</b></a></td>

                                                                    </asp:Panel>
                                                                </tr>


                                                                <tr>
                                                                    <asp:Panel ID="Panel_appro_con" runat="server">
                                                                        <td class="nt_style"><%=emp_con_approve_admin%></td>
                                                                        <td style="font: bold;"><a data-toggle="modal" href="#emp_con_approve_admin"><b>Branches Approved By Admin</b></a></td>

                                                                    </asp:Panel>
                                                                </tr>


                                                                <tr>
                                                                    <asp:Panel ID="Panel_reject_con" runat="server">
                                                                        <td class="nt_style"><%=emp_con_approve_finance %></td>
                                                                        <td style="font: bold;"><a data-toggle="modal" href="#emp_con_approve_finance"><b>Branches Approve By Finance</b></a></td>

                                                                    </asp:Panel>
                                                                </tr>
                                                                <tr>
                                                                    <asp:Panel ID="Panel_appro_finance_con" runat="server">
                                                                        <td class="nt_style"><%=emp_con_reject_finance%></td>
                                                                        <td style="font: bold;"><a data-toggle="modal" href="#emp_con_reject_fiance"><b>Branches Reject By Finance</b></a></td>

                                                                    </asp:Panel>
                                                                </tr>

                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container">
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

                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="lnk_attendance" runat="server" CausesValidation="false" Text="Conveyance" CommandArgument='<%# Eval("file_name") +","+ Eval("branch_name")%>' OnCommand="lnkDownload_Command" CssClass="btn btn-primary"></asp:LinkButton>--%>
                                                        <asp:LinkButton ID="lnk_conveyance" runat="server" CausesValidation="false" Text="Conveyance" Style="color: white" OnCommand="lnk_conveyance_Command" CommandArgument='<%# Eval("conveyance_images") +","+ Eval("unit_name")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <br />

                                    <div class="container" style="width: 75%; overflow-y: auto; height: 300px;">

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
                                        <ItemTemplate>
                                            <%-- <asp:TextBox ID="txt_conveyance" runat="server"   Width="80px" Text='<%# Eval("quantity") %>'></asp:TextBox>--%>

                                            <asp:TextBox ID="txt_conveyance_amount" runat="server" Text='<%# Eval("conveyance_rate") %>' class="form-control" onkeypress="return isNumber_dot(event)" meta:resourceKey="txt_advance_paymentResource1"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									
									 <asp:TemplateField HeaderText="Conveyance Deduction :">
                                        <ItemStyle Width="70px" />
                                        <ItemTemplate>
                                            <%-- <asp:TextBox ID="txt_conveyance" runat="server"   Width="80px" Text='<%# Eval("quantity") %>'></asp:TextBox>--%>

                                            <asp:TextBox ID="txt_conveyance_deduction" runat="server" Text='<%# Eval("emp_con_deduction") %>' class="form-control" onkeypress="return isNumber_dot(event)" meta:resourceKey="txt_advance_paymentResource1"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- <div class="col-sm-2 col-xs-12">
                                                Conveyance Amount :
                                                <asp:TextBox ID="txt_conveyance_amount" runat="server" Text="0" class="form-control" onkeypress="return isNumber_dot(event)" meta:resourceKey="txt_advance_paymentResource1"></asp:TextBox>
                                            </div> --%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <br />
                                    <div class="row text-center">
                                        <asp:Button ID="btn_conv_save" OnClick="btn_conv_save_Click" runat="server" CssClass="btn btn-primary" Text="Save" OnClientClick="return driver_conveyneance1()" Visible="false" />
                                        <asp:Button ID="btn_approve_conveyance" runat="server" OnClick="btn_approve_conveyance_Click" class="btn btn-primary" Text="Approve" CausesValidation="False" Visible="false" />
                                        <asp:Button ID="btn_conv_link" runat="server" Text="Reports" class="btn btn-primary" OnClick="btn_conv_link_Click" OnClientClick="return driver_conveyneance1()" Visible="false" />
                                        <asp:Button ID="Button1" runat="server" class="btn btn-danger" OnClick="btnclose_Click" Text="Close" CausesValidation="False" Visible="false" />

                                    </div>
                                </div>

                                <%-- <div class="container" style="width: 75%">
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
                                      <%--  <%--<asp:LinkButton ID="lnk_attendance" runat="server" CausesValidation="false" Text="Conveyance" CommandArgument='<%# Eval("file_name") +","+ Eval("branch_name")%>' OnCommand="lnkDownload_Command" CssClass="btn btn-primary"></asp:LinkButton>--%>
                                    <%--    <asp:LinkButton ID="lnk_conveyance" runat="server" CausesValidation="false" Text="Conveyance" Style="color: white" OnCommand="lnk_conveyance_Command" CommandArgument='<%# Eval("conveyance_images") +","+ Eval("unit_name")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>--%>
                                <%--   </div>--%>
                            </div>


                            <div id="e2">
                                <asp:Panel runat="server" ID="driver" Visible="false">
                                    <div class="container-fluid">
                                        <div class="row">
                                            <table class="table table-striped" style="width: 20%">
                                                <tr>
                                                    <td>File to Upload :
                                                <asp:FileUpload ID="con_bill_upload" runat="server" meta:resourcekey="photo_uploadResource1" onchange="ValidateSingleInput(this);" />
                                                        <span style="color: red; font-size: 8px; font-weight: bold;">Only JPG,JPEG,GIF,PDF</span></td>
                                                    <td>
                                                        <asp:Button ID="btn_driver_convence" runat="server" class="btn btn-primary" Style="margin-top: 1em" Text=" Upload " OnClick="btn_driver_convence_Click" OnClientClick="return driver_upload();" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <div class="row">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>


                                                        <div class="modal fade" id="driver_con_not_approve_admin" role="dialog" data-dismiss="modal" href="#lost">

                                                            <div class="modal-dialog">
                                                                <div class="modal-content" style="width: 458px;">
                                                                    <div class="modal-header">
                                                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                        <h4>Branches Not Approve By Admin</h4>
                                                                    </div>

                                                                    <div class="modal-body">
                                                                        <asp:Panel ID="Panel26" runat="server" CssClass="grid-view8">
                                                                            <asp:GridView ID="driver_con_notapprove_admin" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div5">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                                                    <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                                                    <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                                                                    <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
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

                                                        <div class="modal fade" id="driver_con_approve_admin" role="dialog" data-dismiss="modal" href="#lost">

                                                            <div class="modal-dialog">
                                                                <div class="modal-content" style="width: 458px;">
                                                                    <div class="modal-header">
                                                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                        <h4>Employees Approve By Admin</h4>
                                                                    </div>

                                                                    <div class="modal-body">
                                                                        <asp:Panel ID="Panel22" runat="server" CssClass="grid-view8">
                                                                            <asp:GridView ID="gv_approved_driver_conveyance" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div5">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                                                    <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                                                    <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                                                                    <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
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

                                                        <div class="modal fade" id="driver_con_approve_finance" role="dialog" data-dismiss="modal" href="#lost">

                                                            <div class="modal-dialog">
                                                                <div class="modal-content" style="width: 622px;">
                                                                    <div class="modal-header">
                                                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                        <h4>Employees Approve By Finance</h4>
                                                                    </div>

                                                                    <div class="modal-body">
                                                                        <div class="row">
                                                                            <div class="col-sm-12" style="padding-left: 1%;">
                                                                                <asp:GridView ID="driver_con_appro_finance" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div5">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                                                        <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                                                        <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
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

                                                        <div class="modal fade" id="driver_con_reject_fiance" role="dialog" data-dismiss="modal" href="#lost">

                                                            <div class="modal-dialog">
                                                                <div class="modal-content" style="width: 458px;">
                                                                    <div class="modal-header">
                                                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                        <h4>Employees Reject By Finance</h4>
                                                                    </div>

                                                                    <div class="modal-body">
                                                                        <asp:Panel ID="Panel_not_approve_conv_dri" runat="server" CssClass="grid-view8">
                                                                            <asp:GridView ID="driver_con_notapprove_fiance" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div5">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                                                    <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                                                    <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                                                                    <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
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
                                                            <br />
                                                        </div>

                                                        <div class="modal fade" id="branches_close_conveyance" role="dialog" data-dismiss="modal" href="#lost">

                                                            <div class="modal-dialog">
                                                                <div class="modal-content" style="width: 458px;">
                                                                    <div class="modal-header">
                                                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                        <h4>Branches Have Closed</h4>
                                                                    </div>

                                                                    <div class="modal-body">
                                                                        <asp:Panel ID="Panel20" runat="server" CssClass="grid-view">
                                                                            <asp:GridView ID="gv_branch_close_conv" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div5">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
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

                                                    </ContentTemplate>

                                                </asp:UpdatePanel>

                                                <div class="container-fluid">

                                                    <div class="row">
                                                        <div class="col-sm-8 col-xs-12"></div>
                                                        <div class="col-sm-4 col-xs-12">
                                                            <div style="max-height: 200px; overflow-y: auto; overflow-x: hidden;">
                                                                <asp:Panel ID="Panel_notification_conv_driver" runat="server">

                                                                    <table border="1" class="table table-striped">
                                                                        <tr style="background-color: #eeeaea">
                                                                            <th>Branch Count</th>
                                                                            <th style="text-align: center">Notification</th>

                                                                        </tr>


                                                                        <tr>
                                                                            <%--      <asp:Panel ID="Panel_not_approve_conv_driver" runat="server">
                                                                                <td class="nt_style"><%=driver_con_remaing%></td>
                                                                                <td style="font: bold;"><a data-toggle="modal" href="#driver_con_not_approve_admin"><b>Branches Not Approved By Admin</b></a></td>

                                                                            </asp:Panel>
                                                                        </tr>--%>

                                                                            <tr>
                                                                                <asp:Panel ID="Panel_appro_con_driver" runat="server">
                                                                                    <td class="nt_style"><%=driver_con_approve_admin%></td>
                                                                                    <td style="font: bold;"><a data-toggle="modal" href="#driver_con_approve_admin"><b>Employees Approved By Admin</b></a></td>

                                                                                </asp:Panel>
                                                                            </tr>


                                                                            <tr>
                                                                                <asp:Panel ID="Panel_reject_con_driver" runat="server">
                                                                                    <td class="nt_style"><%=driver_con_approve_finance %></td>
                                                                                    <td style="font: bold;"><a data-toggle="modal" href="#driver_con_approve_finance"><b>Employees Approve By Finance</b></a></td>

                                                                                </asp:Panel>
                                                                            </tr>
                                                                            <tr>
                                                                                <asp:Panel ID="Panel_appro_finance_con_driver" runat="server">
                                                                                    <td class="nt_style"><%=driver_con_reject_finance%></td>
                                                                                    <td style="font: bold;"><a data-toggle="modal" href="#driver_con_reject_fiance"><b>Employees Reject By Finance</b></a></td>

                                                                                </asp:Panel>
                                                                            </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <asp:Panel ID="Panel12" runat="server" class="grid-view" Style="overflow-x: auto;">
                                            <div class="container" style="width: 100%">
                                                <asp:GridView ID="gv_bill_list_upload" runat="server" AutoGenerateColumns="false" ForeColor="#333333" class="table" Width="100%">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="50" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
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
                                                                <asp:LinkButton ID="lnk_driver_conveyance" runat="server" CausesValidation="false" Text="conveyance" Style="color: white" OnCommand="lnk_driver_conveyance_Command" CommandArgument='<%# Eval("conveyance_images") +","+ Eval("unit_name")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <asp:Panel ID="Panel_driver_conv" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12" style="margin-top: 25px; text-align: left; font-weight: bold;">
                                                <asp:Label ID="Label2" runat="server" Text="FOOD ALLOWANCE :"></asp:Label>
                                            </div>

                                            <%-- <div class="col-sm-2 col-xs-12">
                                      Rate :
                                      <asp:TextBox runat="server" ID="txt_food_rate" CssClass="form-control" onkeypress="return isNumber_dot(event)"></asp:TextBox>
                                  </div>--%>
                                   <div class="col-sm-1 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                        Days :
                                      <asp:TextBox runat="server" ID="txt_food_days" CssClass="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                  </div>
                             </div>
                             <br />
                               <div class="row">
                                     <div class="col-sm-2 col-xs-12"></div>
                                 <div class="col-sm-3 col-xs-12" style="margin-top:25px;text-align:left;font-weight: bold;">
                                     <asp:Label ID="Label3" runat="server" Text="OUTSTATION ALLOWANCE/CONVEYANCE :"></asp:Label>
                                 </div>
                                    
                                <%--  <div class="col-sm-2 col-xs-12">
                                      Rate :
                                      <asp:TextBox runat="server" ID="txt_oc_rate" CssClass="form-control" onkeypress="return isNumber_dot(event)"></asp:TextBox>
                                  </div>--%>
                                     <div class="col-sm-1 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                        Days :
                                      <asp:TextBox runat="server" ID="txt_oc_days" CssClass="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                  </div>
                             </div>
                             <br />
                               <div class="row">
                                     <div class="col-sm-2 col-xs-12"></div>
                                 <div class="col-sm-3 col-xs-12" style="margin-top:25px;text-align:left;font-weight: bold;">
                                     <asp:Label ID="Label4" runat="server" Text="OUTSTATION FOOD ALLOWANCE :"></asp:Label>
                                 </div>
                                   
                                 <%-- <div class="col-sm-2 col-xs-12">
                                      Rate :
                                      <asp:TextBox runat="server" ID="txt_os_rate" CssClass="form-control" onkeypress="return isNumber_dot(event)"></asp:TextBox>
                                  </div>--%>
                                     <div class="col-sm-1 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                        Days :
                                      <asp:TextBox runat="server" ID="txt_os_days" CssClass="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                  </div>
                             </div>
                             <br />
                               <div class="row">
                                     <div class="col-sm-2 col-xs-12"></div>
                                 <div class="col-sm-3 col-xs-12" style="margin-top:25px;text-align:left;font-weight: bold;">
                                     <asp:Label ID="Label5" runat="server" Text="NIGHT HALT :"></asp:Label>
                                 </div>
                                   
                                 <%-- <div class="col-sm-2 col-xs-12">
                                      Rate :
                                      <asp:TextBox runat="server" ID="txt_nh_rate" CssClass="form-control" onkeypress="return isNumber_dot(event)"></asp:TextBox>
                                  </div>--%>
                                     <div class="col-sm-1 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                        Days :
                                      <asp:TextBox runat="server" ID="txt_nh_days" CssClass="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                  </div>
                             </div>
                             <br />
                               <div class="row">
                                     <div class="col-sm-2 col-xs-12"></div>
                                 <div class="col-sm-3 col-xs-12"  style="margin-top:25px;text-align:left;font-weight: bold;">
                                     <asp:Label ID="Label6" runat="server" Text="TOTAL KM AMOUNT :"></asp:Label>
                                 </div>
                                   
                                <%--  <div class="col-sm-2 col-xs-12">
                                      Rate :
                                      <asp:TextBox runat="server" ID="txt_km_rate" CssClass="form-control" onkeypress="return isNumber_dot(event)"></asp:TextBox>
                                  </div>--%>
                                     <div class="col-sm-1 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                        Total  :
                                      <asp:TextBox runat="server" ID="txt_total_km" CssClass="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                  </div>


                             </div>
                                      <br />
                                       <div class="row">
                                     <div class="col-sm-2 col-xs-12"></div>
                                 <div class="col-sm-3 col-xs-12"  style="margin-top:25px;text-align:left;font-weight: bold;">
                                     <asp:Label ID="Label7" runat="server" Text="DEDUCTION :"></asp:Label>
                                 </div>
                                   
                                <%--  <div class="col-sm-2 col-xs-12">
                                      Rate :
                                      <asp:TextBox runat="server" ID="txt_km_rate" CssClass="form-control" onkeypress="return isNumber_dot(event)"></asp:TextBox>
                                  </div>--%>
                                     <div class="col-sm-1 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                        Amount  :
                                      <asp:TextBox runat="server" ID="txt_deduction_amount" CssClass="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                  </div>


                                        </div>
                                    </asp:Panel>
                                    <br />
                                    <div class="row text-center">
                                        <asp:Button ID="btn_drive_save" OnClick="btn_drive_save_Click" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="return driver_conveyneance()" />
                                        <asp:Button ID="btn_drive_approve" runat="server" Text="Approve" class="btn btn-primary" OnClick="btn_drive_approve_Click" OnClientClick="return driver_conveyneance()" />
                                        <asp:Button ID="btn_report" runat="server" Text="Reports" class="btn btn-primary" OnClick="btn_report_Click" OnClientClick="return driver_conveyneance()" />

                                    </div>
                                    <%-- </ContentTemplate>
                             </asp:UpdatePanel>--%>
                            </asp:Panel>

                        </div>
                        <br />
                    </div>
                </div>







                <%--// for links 28-09-19--%>



          <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>


                        <div class="modal fade" id="emp_con_not_approve_admin" role="dialog" data-dismiss="modal" href="#lost">

                            <div class="modal-dialog">
                                <div class="modal-content" style="width: 458px;">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4>Branches Not Approve By Admin</h4>
                                    </div>

                                    <div class="modal-body">
                                        <asp:Panel ID="Panel15" runat="server" CssClass="grid-view8">
                                            <asp:GridView ID="gridview_conv_1" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div5">
                                                <Columns>
                                                    <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                    <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                    <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
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

                        <div class="modal fade" id="emp_con_approve_admin" role="dialog" data-dismiss="modal" href="#lost">

                            <div class="modal-dialog">
                                <div class="modal-content" style="width: 458px;">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4>Branches  Approve By Admin</h4>
                                    </div>

                                    <div class="modal-body">
                                        <asp:Panel ID="Panel17" runat="server" CssClass="grid-view8">
                                            <asp:GridView ID="gv_approved_conveyance" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div5">
                                                <Columns>
                                                    <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                    <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                    <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
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

                        <div class="modal fade" id="emp_con_approve_finance" role="dialog" data-dismiss="modal" href="#lost">

                            <div class="modal-dialog">
                                <div class="modal-content" style="width: 622px;">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4>Branches  Approve By Finance</h4>
                                    </div>

                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-sm-12" style="padding-left: 1%;">
                                                <asp:GridView ID="gv_reject_conveyance" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div5">
                                                    <Columns>
                                                     <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                    <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                    <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                                   
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

                        <div class="modal fade" id="emp_con_reject_fiance" role="dialog" data-dismiss="modal" href="#lost">

                            <div class="modal-dialog">
                                <div class="modal-content" style="width: 458px;">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4>Branches Reject By Finance</h4>
                                    </div>

                                    <div class="modal-body">
                                        <asp:Panel ID="Panel18" runat="server" CssClass="grid-view8">
                                            <asp:GridView ID="gv_app_att_finance_conv" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div5">
                                                <Columns>
                                                     <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                    <asp:BoundField DataField="state_name" HeaderText="State Namae" SortExpression="state_name" />
                                                    <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
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
                            <br />
                        </div>



                        <div class="modal fade" id="branches_close_conveyance" role="dialog" data-dismiss="modal" href="#lost">

                            <div class="modal-dialog">
                                <div class="modal-content" style="width: 458px;">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4>Branches Have Closed</h4>
                                    </div>

                                    <div class="modal-body">
                                        <asp:Panel ID="Panel20" runat="server" CssClass="grid-view">
                                            <asp:GridView ID="gv_branch_close_conv" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridview_conv_1_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div5">
                                                <Columns>
                                                    <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
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

                    </ContentTemplate>

                </asp:UpdatePanel>


                <div class="container-fluid">

                    <div class="row">
                        <div class="col-sm-8 col-xs-12"></div>
                        <div class="col-sm-4 col-xs-12">
                            <div style="max-height: 200px; overflow-y: auto; overflow-x: hidden;">
                                <asp:Panel ID="Panel_notification_conv" runat="server">

                                    <table border="1" class="table table-striped">
                                        <tr style="background-color: #eeeaea">
                                            <th>Branch Count</th>
                                            <th style="text-align: center">Notification</th>
                                            
                                        </tr>
                                       

                                        <tr>
                                            <asp:Panel ID="Panel_not_approve_conv" runat="server">
                                                <td class="nt_style"><%=emp_con_remaing%></td>
                                                <td style="font: bold;"><a data-toggle="modal" href="#emp_con_not_approve_admin"><b>Branches Not Approved By Admin</b></a></td>
                                                
                                            </asp:Panel>
                                        </tr>


                                        <tr>
                                            <asp:Panel ID="Panel_appro_con" runat="server">
                                                <td class="nt_style"><%=emp_con_approve_admin%></td>
                                                <td style="font: bold;"><a data-toggle="modal" href="#emp_con_approve_admin"><b>Branches Approved By Admin</b></a></td>
                                                
                                            </asp:Panel>
                                        </tr>

                                        <tr>
                                            <asp:Panel ID="Panel_reject_con" runat="server">
                                                <td class="nt_style"><%=emp_con_approve_finance %></td>
                                                <td style="font: bold;"><a data-toggle="modal" href="#emp_con_approve_finance"><b>Branches Approve By Finance</b></a></td>
                                                
                                            </asp:Panel>
                                        </tr>
                                         <tr>
                                            <asp:Panel ID="Panel_appro_finance_con" runat="server">
                                                <td class="nt_style"><%=emp_con_reject_finance%></td>
                                                <td style="font: bold;"><a data-toggle="modal" href="#emp_con_reject_fiance"><b>Branches Reject By Finance</b></a></td>
                                                
                                            </asp:Panel>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>--%>
                </div>



            </div>
   

    </asp:Panel>
    </div>

    <br />


    <div class="modal fade" id="reject_attendance" role="dialog" data-dismiss="modal" href="#lost">

        <div class="modal-dialog">
            <div class="modal-content" style="width: 622px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4>Branches Reject Attendance</h4>
                </div>

                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12" style="padding-left: 1%;">
                            <asp:GridView ID="gv_reject_attendance" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                <Columns>
                                    <asp:BoundField DataField="CUNIT" HeaderText="Branch Name" SortExpression="CUNIT" />
                                    <asp:BoundField DataField="Rejected_Reason" HeaderText="Rejected Reason" SortExpression="Rejected_Reason" />
                                    <asp:BoundField DataField="Rejected_Date" HeaderText="Rejected Date" SortExpression="Rejected_Date" />
                                    <asp:BoundField DataField="Rejected_By" HeaderText="Rejected By" SortExpression="Rejected_By" />
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
    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>--%>
</asp:Content>

