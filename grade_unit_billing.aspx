<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="grade_unit_billing.aspx.cs" Inherits="Billing_rates" Title="Client Billing" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Billing Rates</title>
    <style type="text/css">
        .text-red {
            color: #f00;
        }

        .nt_style {
            color: red;
            font: bold;
            text-align: center;
        }

        .HeaderFreez {
            position: relative;
            top: expression(this.offsetParent.scrollTop);
            z-index: 10;
        }

        button, input, optgroup, select, textarea {
            color: inherit;
            margin: 0 0 0 0px;
        }

        * {
            box-sizing: border-box;
        }

        .tab-section {
            background-color: #fff;
        }

        .form-control {
            display: inline;
        }

        .grid-view {
            height: auto;
            overflow-x: hidden;
            overflow-y: auto;
            max-height: 300px;
            width: 100%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_header" runat="Server">
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
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/hashfunction.js"></script>
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />

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
    <%-- <script src="datatable/pdfmake.min.js"></script>--%>
    <script type="text/javascript">

        $(document).ready(function () {
            var table = $('#<%=gv_material_bill.ClientID%>').DataTable({
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
              .appendTo('#<%=gv_material_bill.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';


        });

        $(document).ready(function () {
            $(".flip1").click(function () {
                $(".panel-disp1").toggle("slow");
            });
            $(".flip2").click(function () {
                $(".panel-disp2").toggle("slow");
            });
            $(".flip3").click(function () {
                $(".panel-disp3").toggle("slow");
            });
            $(".flip4").click(function () {
                $(".panel-disp4").toggle("slow");
            });

            $("#btn_show").click(function () {
                $("#panel1").slideUp();
                $("#panel14").slideUp();
                $("#panel15").slideUp();
                $("#panel16").slideUp();
            });
        });


    </script>

    <script type="text/javascript">

        function unblock() { $.unblockUI(); }

        function pageLoad() {
            $.fn.dataTable.ext.errMode = 'none';

            var table = $('#<%=grd_material_billing.ClientID%>').DataTable({
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
              .appendTo('#<%=grd_material_billing.ClientID%>_wrapper .col-sm-6:eq(0)');
            $('[id*=chk_gv_header]').click(function () {
                $("[id*='chk_client']").attr('checked', this.checked);
            });
            $('#<%=ddl_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_billing_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_report.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

            $('[id*=chk_state_header]').click(function () {
                $("[id*='chk_state']").attr('checked', this.checked);
            });

            $('[id*=chk_bill_state]').click(function () {
                $("[id*='chk_bill']").attr('checked', this.checked);
            });
            $('#<%=btn_show.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            //R&M
            $('[id*=chk_gv_header_r_m]').click(function () {
                $("[id*='chk_client_rm']").attr('checked', this.checked);
            });
            //administrative
            $('[id*=chk_gv_header_admini]').click(function () {
                $("[id*='chk_client_admin']").attr('checked', this.checked);
            });
            //arears
            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                yearRange: '1951',
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
            $(".date-picker1").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");
            // end arrears
            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                maxDate: 0,
                yearRange: "1990:+100",
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

            $(".datepicker").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                // yearRange: '1950',
                yearRange: "1990:+1",
                //maxDate: 40,
                onSelect: function (value, ui) {


                }
            }).click(function () {
                $('.ui-datepicker-calendar').show();
            });
            $(".datepicker").attr("readonly", "true");
            arrear_type();
            billing_rates();
            support_format1();
            bill_check();
            // datehis();
        }
        function openWindow() {
            window.open("html/grade_unit_billing.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

        function Req_validation() {

            var txt_client_code = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_client = txt_client_code.options[txt_client_code.selectedIndex].text;

            var txt_month = document.getElementById('<%=txt_month_year.ClientID %>');

            if (Selected_client == "Select") {
                alert("Please Select Client Name ");
                txt_client_code.focus();
                return false;
            }
            if (Selected_client == "ALL") {
                alert("Please Select Client Name ");
                txt_client_code.focus();
                return false;
            }
            if (txt_month.value == "") {
                alert("Please Select month & year ");
                txt_month.focus();
                return false;
            }
            return true;
        }
        function Req_arrears_final() {

            var ddl_arrears_type = document.getElementById('<%=ddl_arrears_type.ClientID %>');
            var Selected_arrears_type = ddl_arrears_type.options[ddl_arrears_type.selectedIndex].text;

            if (Selected_arrears_type == "Select") {
                alert("Please Select Arrear Type");
                ddl_arrears_type.focus();
                return false;

            }
            var t_invoice_no = document.getElementById('<%=txt_invoice_no.ClientID %>');
            var t_bill_date = document.getElementById('<%=txt_bill_date.ClientID %>');

            if (t_invoice_no.value == "") {
                alert("Please Enter Invoice No ");
                t_invoice_no.focus();
                return false;
            }
            if (t_bill_date.value == "") {
                alert("Please Enter Bill Date ");
                t_bill_date.focus();
                return false;
            }

            return true;

        }
        function Req_arrears() {


            var txt_month = document.getElementById('<%=txt_arrear_month_year.ClientID %>');
            var txt_month1 = document.getElementById('<%=txt_arrear_monthend.ClientID %>');


            var ddl_arrears_type = document.getElementById('<%=ddl_arrears_type.ClientID %>');
            var Selected_arrears_type = ddl_arrears_type.options[ddl_arrears_type.selectedIndex].text;

            if (Selected_arrears_type == "Select") {
                alert("Please Select Arrear Type");
                ddl_arrears_type.focus();
                return false;

            }


            if (Selected_arrears_type == "Policy Wise") {

                if (txt_month.value == "") {
                    alert("Please Select From Month ");

                    return false;
                }

                if (txt_month1.value == "") {
                    alert("Please Select To Month ");

                    return false;
                }
            }

            return true;
        }
        function Req_validationAll() {

            var txt_client_code = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_client = txt_client_code.options[txt_client_code.selectedIndex].text;

            var txt_month = document.getElementById('<%=txt_month_year.ClientID %>');

            if (Selected_client == "Select") {
                alert("Please Select Client Name ");
                txt_client_code.focus();
                return false;
            }

            if (txt_month.value == "") {
                alert("Please Select month & year ");
                txt_month.focus();
                return false;
            }
            return true;
        }
        function Req_validation1() {

            if (Req_validation()) {
                var t_invoice_no = document.getElementById('<%=txt_invoice_no.ClientID %>');
                 var t_bill_date = document.getElementById('<%=txt_bill_date.ClientID %>');

                 if (t_invoice_no.value == "") {
                     alert("Please Enter Invoice No ");
                     t_invoice_no.focus();
                     return false;
                 }
                 if (t_bill_date.value == "") {
                     alert("Please Enter Bill Date ");
                     t_bill_date.focus();
                     return false;
                 }

                 return true;
             }

             else { return false; }
         }
         function driver_report_validation() {



             var ddl_unitcode = document.getElementById('<%=ddl_unitcode.ClientID %>');
            var Selected_ddl_unitcode = ddl_unitcode.options[ddl_unitcode.selectedIndex].text;

            var ddl_billing_state = document.getElementById('<%=ddl_billing_state.ClientID %>');
            var Selected_ddl_billing_state = ddl_billing_state.options[ddl_billing_state.selectedIndex].text;

            if (Selected_ddl_billing_state == "ALL") {
                alert("Please Select State Name ");
                ddl_billing_state.focus();
                return false;
            }
            //if (Selected_ddl_unitcode == "ALL") {
            //        alert("Please Select Unit Name");
            //        ddl_unitcode.focus();
            //        return false;
            //    }
            return true;

        }
        function report_link_validation() {

            var ddl_billing_state = document.getElementById('<%=ddl_billing_state.ClientID %>');
            var Selected_ddl_billing_state = ddl_billing_state.options[ddl_billing_state.selectedIndex].text;

            if (Selected_ddl_billing_state == "ALL") {
                alert("Please Select State Name ");
                ddl_billing_state.focus();
                return false;
            }

            return true;

        }
        function Req_invoice_validation1() {

            if (Req_validation()) {
                var t_invoice_no = document.getElementById('<%=txt_invoice_no.ClientID %>');
                var t_bill_date = document.getElementById('<%=txt_bill_date.ClientID %>');
                var t_bill_state = document.getElementById('<%=ddl_billing_state.ClientID %>');

                if (t_bill_state.value == "ALL") {
                    alert("Please Select State Name ");
                    t_bill_date.focus();
                    return false;
                }
                if (t_invoice_no.value == "") {
                    alert("Please Enter Invoice No ");
                    t_invoice_no.focus();
                    return false;
                }
                if (t_bill_date.value == "") {
                    alert("Please Enter Bill Date ");
                    t_bill_date.focus();
                    return false;
                }

                return true;
            }

            else { return false; }
        }
        function Req_final_invoice() {
            if (Req_validation1()) {

                var t_invoice_no = document.getElementById('<%=txt_invoice_no.ClientID %>');
                var t_bill_date = document.getElementById('<%=txt_bill_date.ClientID %>');


                if (t_invoice_no.value == "") {
                    alert("Please Enter Invoice No ");
                    t_invoice_no.focus();
                    return false;
                }
                if (t_bill_date.value == "") {
                    alert("Please Enter Bill Date ");
                    t_bill_date.focus();
                    return false;
                }

                //return true;

                var Ok = confirm(' Invoice No : ' + t_invoice_no.value + ' And Billing Date : ' + t_bill_date.value + '\n\n Are you sure want to Final Invoice Print?');
                if (Ok)
                    return true;
                else
                    return false;
            }
            else {

                return false;
            }
        }
        function ConfirmSave() {

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
        function ConfirmDelete() {
            var x = confirm("Are you sure you want to Reject Timesheet?");
            if (x)
                return true;
            else
                return false;
        }


        function check() {
            if (Req_validation()) {
                return ConfirmDelete();
            }
            else { return false; }
        }
        var _validFileExtensions = [".jpg", ".jpeg", ".gif", ".pdf", ".zip"];
        function ValidateSingleInput(oInput) {
            if (oInput.type == "file") {
                var sFileName = oInput.value;
                if (sFileName.length > 0) {
                    var blnValid = false;
                    for (var j = 0; j < _validFileExtensions.length; j++) {
                        var sCurExtension = _validFileExtensions[j];
                        if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                            blnValid = true;
                            break;
                        }
                    }

                    if (!blnValid) {
                        alert("Sorry, " + sFileName + " is invalid, allowed extensions are: " + _validFileExtensions.join(", "));
                        oInput.value = "";
                        return false;
                    }
                }
            }
            return true;
        }
        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });
        function datehis() {
            var txt_month1 = document.getElementById('<%=txt_month_year.ClientID %>').value;
            var res = txt_month1.substring(0, 2);
            if (res == "03" || res == "04") {
                $("#abc11").show();
                $("#abc111").show();
            }
            else {
                $("#abc11").hide();
                $("#abc111").hide();
            }
        }
        function req_bill_upload() {

            // datehis();

            var txt_client_code = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_client = txt_client_code.options[txt_client_code.selectedIndex].text;



            if (Selected_client == "Select") {
                alert("Please Select Client Name ");
                txt_client_code.focus();
                return false;
            }
            var ddl_billing_state = document.getElementById('<%=ddl_billing_state.ClientID %>');


		    var txt_month = document.getElementById('<%=txt_month_year.ClientID %>');
            if (txt_month.value == "") {
                alert("Please Select month & year ");
                txt_month.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }

        function pf_upload() {
            if (req_bill_upload) {
                var bill_upload = document.getElementById('<%=up_pf_challan.ClientID %>');
		        if (bill_upload.value == "") {
		            alert("Please Select Document ");
		            bill_upload.focus();
		            return false;
		        }
		        return true;
		    }
            return false;
        }

        function ecr_upload() {
            if (req_bill_upload) {
                var bill_upload = document.getElementById('<%=up_ecr_file.ClientID %>');
                if (bill_upload.value == "") {
                    alert("Please Select Document ");
                    bill_upload.focus();
                    return false;
                }
                return true;
            }
            return false;
        }

        function req_validation_process() {

            //  datehis();

            var txt_client_code = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_client = txt_client_code.options[txt_client_code.selectedIndex].text;
            var ddl_billing_state = document.getElementById('<%=ddl_billing_state.ClientID %>');
            var Selected_state = ddl_billing_state.options[ddl_billing_state.selectedIndex].text;
            var txt_unitcode = document.getElementById('<%=ddl_unitcode.ClientID %>');
            var Selected_unit = txt_unitcode.options[txt_unitcode.selectedIndex].text;


            if (Selected_client == "Select") {
                alert("Please Select Client Name ");
                txt_client_code.focus();
                return false;
            }
            if (Selected_client == "ALL") {
                alert("Please Select Client Name ");
                txt_client_code.focus();
                return false;
            }
            if (Selected_state == "Select") {
                alert("Please Select State Name ");
                txt_client_code.focus();
                return false;
            }
            if (Selected_client == "Reliance Capital Ltd." && Selected_unit == "ALL") {
                alert("Please Select Branch Name ");
                txt_unitcode.focus();
                return false;
            }
            var txt_month = document.getElementById('<%=txt_month_year.ClientID %>');
            if (txt_month.value == "") {
                alert("Please Select month & year ");
                txt_month.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }

        function bill_upload() {
            var txt_client_code = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_client = txt_client_code.options[txt_client_code.selectedIndex].text;

            if (Selected_client == "Select") {
                alert("Please Select Client Name");
                txt_client_code.focus();
                return false;
            }

            var txt_month_year = document.getElementById('<%=txt_month_year.ClientID %>');

            if (txt_month_year.value == "") {
                alert("Please Select Month");
                txt_month_year.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function send_email() {

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function R_validation1() {

            var r = confirm("Are you Sure You Want to Approve Branch");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }
        function R_validation2() {

            var r = confirm("Are you Sure You Want to Reject Branch");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }

        function areas_bill_validation() {
            var txt_client_code = document.getElementById('<%=ddl_client.ClientID %>');
           var Selected_client = txt_client_code.options[txt_client_code.selectedIndex].text;

           var txt_month = document.getElementById('<%=txt_month_year.ClientID %>');

               if (Selected_client == "Select") {
                   alert("Please Select Client Name ");
                   txt_client_code.focus();
                   return false;
               }

               if (txt_month.value == "") {
                   alert("Please Select month & year ");
                   txt_month.focus();
                   return false;
               }
               $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
               return true;
           }

           function billing_rates() {
               var billing_type = document.getElementById('<%=billing_type.ClientID %>');
            var Selected_billing_type = billing_type.options[billing_type.selectedIndex].text;

            if (Selected_billing_type == "Manpower Billing") {
                $(".manpower_billing").show();
                $(".material_billing").hide();
                $(".conveyance_billing").hide();
                $(".deep_clean_billing").hide();
                $(".pest_control_billing").hide();
                $(".Machine_Rent_Bill").hide();
                $(".R_M_billing").hide();
                $(".ad_ex__billing").hide();
            }
            else if (Selected_billing_type == "R And M Service") {
                $(".R_M_billing").show();
                $(".manpower_billing").hide();
                $(".material_billing").hide();
                $(".conveyance_billing").hide();
                $(".deep_clean_billing").hide();
                $(".pest_control_billing").hide();
                $(".Machine_Rent_Bill").hide();
                $(".ad_ex__billing").hide();

            }
            else if (Selected_billing_type == "Administrative Expenses") {
                $(".ad_ex__billing").show();
                $(".manpower_billing").hide();
                $(".material_billing").hide();
                $(".conveyance_billing").hide();
                $(".deep_clean_billing").hide();
                $(".pest_control_billing").hide();
                $(".Machine_Rent_Bill").hide();
                $(".R_M_billing").hide();

            }
            else if (Selected_billing_type == "Material Billing") {
                $(".material_billing").show();
                $(".manpower_billing").hide();
                $(".conveyance_billing").hide();
                $(".deep_clean_billing").hide();
                $(".pest_control_billing").hide();
                $(".Machine_Rent_Bill").hide();
                $(".R_M_billing").hide();
                $(".ad_ex__billing").hide();

            }
            else if (Selected_billing_type == "Conveyance Billing") {
                $(".conveyance_billing").show();
                $(".manpower_billing").hide();
                $(".material_billing").hide();
                $(".deep_clean_billing").hide();
                $(".pest_control_billing").hide();
                $(".Machine_Rent_Bill").hide();
                $(".R_M_billing").hide();
                $(".ad_ex__billing").hide();
                conveyance_bill_check();
            }
            else if (Selected_billing_type == "Deep Clean Billing") {
                $(".deep_clean_billing").show();
                $(".manpower_billing").hide();
                $(".material_billing").hide();
                $(".conveyance_billing").hide();
                $(".pest_control_billing").hide();
                $(".Machine_Rent_Bill").hide();
                $(".R_M_billing").hide();
                $(".ad_ex__billing").hide();
            }
            else if (Selected_billing_type == "Pest Control Billing") {
                $(".pest_control_billing").show();
                $(".manpower_billing").hide();
                $(".material_billing").hide();
                $(".conveyance_billing").hide();
                $(".deep_clean_billing").hide();
                $(".Machine_Rent_Bill").hide();
                $(".R_M_billing").hide();
                $(".ad_ex__billing").hide();
            }
                //vikas add 20-06-2019
            else if (Selected_billing_type == "Machine Rental") {
                $(".Machine_Rent_Bill").show();
                $(".pest_control_billing").hide();
                $(".manpower_billing").hide();
                $(".material_billing").hide();
                $(".conveyance_billing").hide();
                $(".deep_clean_billing").hide();
                $(".R_M_billing").hide();
                $(".ad_ex__billing").hide();

            }

            else {
                $(".Machine_Rent_Bill").hide();
                $(".manpower_billing").hide();
                $(".material_billing").hide();
                $(".conveyance_billing").hide();
                $(".deep_clean_billing").hide();
                $(".pest_control_billing").hide();
                $(".R_M_billing").hide();
                $(".ad_ex__billing").hide();

            }

            return true;
        }
        function arrear_type() {
            var ddl_arrears_type = document.getElementById('<%=ddl_arrears_type.ClientID %>');
            var ddl_arrears_type = ddl_arrears_type.options[ddl_arrears_type.selectedIndex].text;
            if (ddl_arrears_type == "Policy Wise") {
                $(".arrear").show();
            }

            else { $(".arrear").hide(); }
        }
        function support_format1() {

            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].value;
            var btn_support_format123 = document.getElementById('<%= btn_support_format.ClientID %>');

            if (Selected_ddl_client == "ALL") {
                $(".manpower_billing").show();
                $(".material_billing").show();
                $(".conveyance_billing").show();
                $(".deep_clean_billing").show();
                $(".pest_control_billing").show();
                $(".Machine_Rent_Bill").show();
            }

            if (Selected_ddl_client == "MAX") {

                $(".support12").show();
            }
            else {

                $(".support12").hide();
            }
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;
            if (Selected_ddl_client == "Dewan Housing Finance Corporation Limited") {
                $(".region").show();
            }

            else { $(".region").hide(); }

            if (Selected_ddl_client == "BAJAJ ALLIANZ GENERAL INSURANCE COMPANY LTD.") {
                $(".region").show();
            }
        }
        function validateCheckBoxes() {
            var isValid = false;
            var gridView = document.getElementById('<%= gv_approve_attendace.ClientID %>');
       for (var i = 1; i < gridView.rows.length; i++) {
           var inputs = gridView.rows[i].getElementsByTagName('input');
           if (inputs != null) {
               if (inputs[0].type == "checkbox") {
                   if (inputs[0].checked) {
                       isValid = true;
                       if (R_validation()) {
                           return true;
                       }
                       return false;
                   }
               }
           }
       }
       alert("Please select atleast one Branch");
       return false;
   }


        function validateCheckBoxes_reject() {
            var isValid = false;
            var gridView = document.getElementById('<%= gv_approve_attendace.ClientID %>');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;
                            if (R_validation_reject()) {
                                return true;
                            }
                            return false;
                        }
                    }
                }
            }
            alert("Please select atleast one Branch");
            return false;
        }



   function validateCheckBoxesMaterial() {
       var isValid = false;
       var gridView = document.getElementById('<%= gv_approve_material.ClientID %>');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;
                            if (R_validation()) {
                                return true;
                            }
                            return false;
                        }
                    }
                }
            }
            alert("Please select atleast one Branch");
            return false;
        }
        function validateCheckBoxesEmpConveyance() {
            var isValid = false;
            var gridView = document.getElementById('<%= gv_con_emp_approve.ClientID %>');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;
                            if (R_validation()) {
                                return true;
                            }
                            return false;
                        }
                    }
                }
            }
            alert("Please select atleast one Branch");
            return false;
        }

        function validateCheckBoxesDriverConveyance() {
            var isValid = false;
            var gridView = document.getElementById('<%= gv_con_driver_approve.ClientID %>');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;
                            if (R_validation()) {
                                return true;
                            }
                            return false;
                        }
                    }
                }
            }
            alert("Please select atleast one Branch");
            return false;
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

        function R_validation_reject() {

            var r = confirm("Are you Sure You Want to Reject Record");
            if (r == true) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }

        function R_valid() {

            var r = confirm("Are you Sure You Want to Delete Record");
            if (r == true) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            return r;
        }

        function bill_check() {
            var ddl_report = document.getElementById('<%=ddl_report.ClientID %>');
            var Selected_ddl_report = ddl_report.options[ddl_report.selectedIndex].text;
            if (Selected_ddl_report == "Monthwise Billing Details") {
                $(".bill").show();
            }

            else { $(".bill").hide(); }
        }

        function report_validation() {
            var ddl_report = document.getElementById('<%=ddl_report.ClientID %>');
            var Selected_ddl_report = ddl_report.options[ddl_report.selectedIndex].text;

            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

            var ddl_state = document.getElementById('<%=ddl_billing_state.ClientID %>');
             var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;

             var txt_date = document.getElementById('<%=txt_month_year.ClientID %>');

            var ddl_bill_type = document.getElementById('<%=ddl_bill_type.ClientID %>');
            var Selected_ddl_bill_type = ddl_bill_type.options[ddl_bill_type.selectedIndex].text;

            if ((Selected_ddl_report == "Salary Slip Sending Details") || (Selected_ddl_report == "Monthwise Billing Details")) {
                if ((Selected_ddl_client == "ALL") && (Selected_ddl_bill_type != "Clientwise Bill")) {
                    alert("Please Select Client Name");
                    ddl_client.focus();
                    return false;
                }
                if (txt_date.value == "") {
                    alert("Please Select Month");
                    txt_date.focus();
                    return false;
                }
                if (Selected_ddl_report == "Monthwise Billing Details") {
                    if (Selected_ddl_bill_type == "Select") {
                        alert("Please Select Billing Type");
                        ddl_bill_type.focus();
                        return false;
                    }
                }
                if (Selected_ddl_bill_type == "Clientwise Bill") {
                    if (Selected_ddl_client != "ALL") {
                        alert("Please Select Client Name  ALL");
                        ddl_client.focus();
                        return false;
                    }
                }
            }


        }

        function conveyance_bill_check() {

            var ddl_conveyance_type = document.getElementById('<%=ddl_conveyance_type.ClientID %>');
    var Selected_ddl_conveyance_type = ddl_conveyance_type.options[ddl_conveyance_type.selectedIndex].text;
    if (Selected_ddl_conveyance_type == "Driver Convenyance Billing") {
        $(".conveyance2").show();
        $(".conveyance1").hide();
        //("show");
    }

    else {
        $(".conveyance2").hide();
        $(".conveyance1").show();
        //alert("hide"); 
    }

}

    </script>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="panel panel-primary" style="background: beige;">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: 16px;" class="text-center text-uppercase"><b>Billing Rates</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                <asp:Panel runat="server" CssClass="panel panel-primary" Style="border-color: gray; background: #f6f3ff">
                    <br />
                    <div class="container-fluid">
                        <div class="row">
                            <div class=" col-md-2 col-xs-12">
                                <b>Select Month :</b><span class="text-red">*</span>
                                <asp:TextBox ID="txt_month_year" Class="form-control date-picker" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                <b>Client Name :</b><span class="text-red">*</span>
                                <asp:DropDownList ID="ddl_client" DataValueField="client_code" DataTextField="client_name" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control" onchange="support_format1()">
                                </asp:DropDownList>
                            </div>
                            <asp:Panel ID="unit_panel" runat="server">
                                <%-- suraj--%>
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                    <b>Billing Type :</b>
                            <asp:DropDownList ID="billing_type" runat="server" CssClass="form-control" onchange="billing_rates()">
                            </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12 region" style="display: none">
                                    <b>Region :</b>
                            <asp:DropDownList ID="ddlregion" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_region_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                                </div>

                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12" runat="server" id="ddcomp_group">
                                    <b>Company Groups :</b><span class="text-red">*</span>
                                    <asp:DropDownList ID="ddl_company_group" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                    <b>State Name:</b><span class="text-red">*</span>
                                    <asp:DropDownList ID="ddl_billing_state" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true" />
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                    <b> Branch Name :</b>
                            <asp:DropDownList ID="ddl_unitcode" DataValueField="unit_code" DataTextField="unit_name" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_unitcode_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                                </div>




                                <div class="modal fade" id="client_billing" role="dialog" data-dismiss="modal">

                                    <div class="modal-dialog">
                                        <div class="modal-content" style="width: 627px;">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="modal-title">Check Client Billing </h4>

                                            </div>

                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-sm-12" style="padding-left: 1%;">
                                                        <asp:Panel ID="Panel123" runat="server" CssClass="grid-view">
                                                            <asp:GridView ID="gv_client_billing" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                <EditRowStyle BackColor="#999999" />
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" Width="50" />
                                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                <Columns>

                                                                    <asp:BoundField DataField="CLIENT_NAME" HeaderText="Client Name" SortExpression="CLIENT_NAME" />
                                                                    <asp:BoundField DataField="billing_name" HeaderText="Billing Type" SortExpression="billing_name" />
                                                                    <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                                                    <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                                                    <asp:BoundField DataField="designation" HeaderText="Designation" SortExpression="designation" />
                                                                    <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />

                                                                </Columns>


                                                            </asp:GridView>
                                                        </asp:Panel>
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
                            </asp:Panel>
                        </div>
                        <br />
                        <asp:Panel ID="unit_panel2" runat="server">
                            <div class="row">

                                <div class=" col-md-2  col-xs-12">
                                    <b>Process Data :</b><span class="text-red">*</span>
                                    <asp:DropDownList ID="ddl_process_data" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">Current Policy</asp:ListItem>
                                        <asp:ListItem Value="1">Old Policy</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Invoice No :</b>
                            <asp:TextBox ID="txt_invoice_no" CssClass="form-control" runat="server" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                    <b>Bill Date :</b><span class="text-red">*</span>
                                    <asp:TextBox ID="txt_bill_date" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Invoice type :</b><span class="text-red">*</span>
                                    <asp:DropDownList ID="ddl_invoice_type" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_invoice_type_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1">CLUB</asp:ListItem>
                                        <asp:ListItem Value="2">UNCLUB</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class=" col-md-2 col-xs-12">
                                    <asp:Panel ID="desigpanel" runat="server">
                                        <b>Designation :</b><span class="text-red">*</span>
                                        <asp:DropDownList ID="ddl_designation" runat="server" CssClass="form-control" />
                                    </asp:Panel>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12" id="abc111">
                                    <b>Billing Start Day:</b> 
                                        <asp:DropDownList ID="ddl_start_date_common" runat="server" CssClass="form-control text_box" Width="100%">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                            <asp:ListItem Value="13">13</asp:ListItem>
                                            <asp:ListItem Value="14">14</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="16">16</asp:ListItem>
                                            <asp:ListItem Value="17">17</asp:ListItem>
                                            <asp:ListItem Value="18">18</asp:ListItem>
                                            <asp:ListItem Value="19">19</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="21">21</asp:ListItem>
                                            <asp:ListItem Value="22">22</asp:ListItem>
                                            <asp:ListItem Value="23">23</asp:ListItem>
                                            <asp:ListItem Value="24">24</asp:ListItem>
                                            <asp:ListItem Value="25">25</asp:ListItem>
                                            <asp:ListItem Value="26">26</asp:ListItem>
                                            <asp:ListItem Value="27">27</asp:ListItem>
                                            <asp:ListItem Value="28">28</asp:ListItem>
                                            <asp:ListItem Value="29">29</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="31">31</asp:ListItem>
                                        </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12" id="abc11">
                                    <b>Billing End Day:</b> 
                                        <asp:DropDownList ID="ddl_end_date_common" runat="server" CssClass="form-control text_box" Width="100%">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                            <asp:ListItem Value="13">13</asp:ListItem>
                                            <asp:ListItem Value="14">14</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="16">16</asp:ListItem>
                                            <asp:ListItem Value="17">17</asp:ListItem>
                                            <asp:ListItem Value="18">18</asp:ListItem>
                                            <asp:ListItem Value="19">19</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="21">21</asp:ListItem>
                                            <asp:ListItem Value="22">22</asp:ListItem>
                                            <asp:ListItem Value="23">23</asp:ListItem>
                                            <asp:ListItem Value="24">24</asp:ListItem>
                                            <asp:ListItem Value="25">25</asp:ListItem>
                                            <asp:ListItem Value="26">26</asp:ListItem>
                                            <asp:ListItem Value="27">27</asp:ListItem>
                                            <asp:ListItem Value="28">28</asp:ListItem>
                                            <asp:ListItem Value="29">29</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="31">31</asp:ListItem>
                                        </asp:DropDownList>
                                </div>
                                <div class="col-sm-6 col-xs-12"></div>
                                <div class="col-sm-2 col-xs-12" style="margin-top: 2em;">
                                    <asp:Panel ID="billing_rate_modal" runat="server" Visible="false">
                                        <a data-toggle="modal" href="#client_billing" style="color: blue; font-weight: bold">Check Client Billing</a>
                                    </asp:Panel>
                                </div>
                            </div>
                            <br />
                        </asp:Panel>
                        <br />
                        <div class="row">
                            <div class="col-sm-9 col-xs-12">
                                <asp:TextBox ID="Auto_invoice_no" CssClass="form-control" runat="server" Visible="false" Text="Auto Invoice No  :"></asp:TextBox>
                            </div>
                            <div class="col-sm-6 col-xs-12">

                                <%--  <br />
                                <asp:Panel ID="panel_deployment" runat="server"><a data-toggle="modal" href="#branch_deployment"><font color="red"><b><%=deployment %></b></font>Branches Having  No  Deployment</a></asp:Panel>
                                <asp:Panel ID="remaining_panel" runat="server"><a data-toggle="modal" href="#attendance"><font color="red"><b><%=Message%></b></font>Branches Not Approved By Admin</a><br />
                                </asp:Panel>


                                <asp:Panel ID="policy_panel" runat="server"><a data-toggle="modal" href="#div_policy"><font color="red"><b><%=policy%></b></font>Branches Policy Remaining</a></asp:Panel>
                                <asp:Panel ID="approval_panel" runat="server"><a data-toggle="modal" href="#approve_attendance"><font color="red"><b><%=appro_attendannce%></b></font>Branches Approved By Admin</a></asp:Panel>
                                <asp:Panel ID="approval_finance_panel" runat="server"><a data-toggle="modal" href="#approve_attendance_finance"><font color="red"><b><%=appro_attendannce_finanace%></b></font>Branches Approved By Finance</a></asp:Panel>
                                <asp:Panel ID="reject_panel" runat="server"><a data-toggle="modal" href="#reject_attendance"><font color="red"><b><%=reject_attendance%></b></font>Branches  Reject By Finance</a></asp:Panel>--%>

                                <div style="max-height: 150px; overflow-y: auto; width: 70%; margin-left: 80em">
                                    <asp:Panel ID="Notification_panel" runat="server">

                                        <table border="1" class="table table-striped">

                                            <tr style="background-color: #bfbcbc; font-size: 12px; font-weight: bold;">

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
                                                <asp:Panel ID="remaining_panel" runat="server">
                                                    <td class="nt_style"><%=Message%></td>
                                                    <td style="font: bold;"><a data-toggle="modal" href="#attendance"><b>Branches Not Approved By Admin</b></a></td>
                                                    <td class="nt_style"><%=Emp_Message %></td>
                                                </asp:Panel>
                                            </tr>
                                            <tr>
                                                <asp:Panel ID="policy_panel" runat="server">
                                                    <td class="nt_style"><%=policy%></td>
                                                    <td style="font: bold;"><a data-toggle="modal" href="#div_policy"><b>Branches Policy Remaining</b></a></td>
                                                    <td class="nt_style"></td>
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
                    </div>
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
                    <br />
                </asp:Panel>
                <div class="modal fade" id="reject_attendance" role="dialog" data-dismiss="modal" href="#lost">

                    <div class="modal-dialog">
                        <div class="modal-content" style="width: 720px;">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4>Branches Reject Attendance</h4>
                            </div>

                            <div class="modal-body">
                                <asp:Panel runat="server" CssClass="grid-view">
                                    <asp:GridView ID="gv_reject_attendance" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                        <Columns>
                                            <asp:BoundField DataField="CUNIT" HeaderText="Branch Name" SortExpression="CUNIT" />
                                            <asp:BoundField DataField="Rejected_Reason" HeaderText="Rejected Reason" SortExpression="Rejected_Reason" />
                                            <asp:BoundField DataField="Rejected_Date" HeaderText="Rejected Date" SortExpression="Rejected_Date" />
                                            <asp:BoundField DataField="Rejected_By" HeaderText="Rejected By" SortExpression="Rejected_By" />
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
                <div class="modal fade" id="branch_deployment" role="dialog" data-dismiss="modal" href="#lost">

                    <div class="modal-dialog">
                        <div class="modal-content" style="width: 458px;">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4>Branches Having  No Deployment</h4>
                            </div>

                            <div class="modal-body">
                                <asp:Panel ID="Panel3" runat="server" CssClass="grid-view">
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
                                              <asp:BoundField DataField="branch_close_date" HeaderText="Branche Closing Date" SortExpression="branch_close_date" />
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
                                <h4>Branches With Approve Attendance</h4>
                            </div>

                            <div class="modal-body">
                                <asp:Panel ID="Panel4" runat="server" CssClass="grid-view">
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
            </div>

            <div class="modal fade" id="attendance" role="dialog" data-dismiss="modal" href="#lost">

                <div class="modal-dialog">
                    <div class="modal-content" style="width: 458px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4>Branches Having No Attendance</h4>
                        </div>

                        <div class="modal-body">
                            <asp:Panel runat="server" CssClass="grid-view">
                                <asp:GridView ID="gv_remain_attendance" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
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
            <div class="modal fade" id="div_policy" role="dialog" data-dismiss="modal" href="#lost">

                <div class="modal-dialog">
                    <div class="modal-content" style="width: 458px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4>Branches With No Billing Policy.</h4>
                        </div>

                        <div class="modal-body">
                            <asp:Panel ID="Panel5" runat="server" CssClass="grid-view">
                                <asp:GridView ID="grd_policy" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
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
                            <h4>Branches  Approve By Finance</h4>
                        </div>

                        <div class="modal-body">
                            <asp:Panel ID="Panel6" runat="server" CssClass="grid-view">
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

            </div>

            <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
            <br />

            <div class="modal fade" id="reject_attendance_billing" role="dialog" style="position: absolute; height: 343px; overflow-y: hidden; top: 50%; left: 30%; transform: translate(-50%, -50%);">
                <div class="modal-dialog" style="width: 100%">
                    <div class="modal-content" style="width: 427px; margin-left: 62em;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4>Reject Attendance </h4>
                        </div>
                        <div class="modal-body">
                            <div class="container">
                                <br />
                                <div class="row">
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row text-center">
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-4">
                                    <button type="button" class="btn btn-primary" data-dismiss="modal">Save</button>
                                </div>
                                <div class="col-sm-4">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%-- <div class="modal fade" id="div_policy" role="dialog" style=" position: absolute; top: 50%; left: 30%;transform: translate(-50%, -50%);">
            <div class="modal-dialog" style="width:100%">
                <div class="modal-content" style="width:400px;">
                    <div class="modal-header">
<button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4>Branches With No Billing Policy.</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12" style="padding-left:1%;">
                            <asp:GridView ID="grd_policy" runat="server" AutoGenerateColumns="false" ShowFooter="false">
                    <Columns>
                        <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />
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

            <%--material--%>
            <asp:Panel ID="Panel11" runat="server">
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


            </asp:Panel>
            <%--Empconveyance--%>
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
                                    <asp:Panel ID="Panel8" runat="server" CssClass="grid-view8">
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
            <%--Driverconveyance--%>
            <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <div class="modal fade" id="driver_con_not_approve_admin" role="dialog" data-dismiss="modal" href="#lost">

                        <div class="modal-dialog">
                            <div class="modal-content" style="width: 458px;">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4>Branches Not Approve By Admin</h4>
                                </div>

                                <div class="modal-body">
                                    <asp:Panel ID="Panel21" runat="server" CssClass="grid-view8">
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
            <br />
            <div class="container-fluid">
                <div id="tabs" style="background: beige;" runat="server">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>

                        <li class="manpower_billing" runat="server"><a href="#menu1">Manpower Billing</a></li>
                        <li class="material_billing"><a href="#menu2">Material Billing</a></li>
                        <li class="conveyance_billing"><a href="#menu3">Conveyance Billing</a></li>
                        <li class="deep_clean_billing"><a href="#menu4">Deep Clean Billing</a></li>
                        <li class="pest_control_billing"><a href="#menu5">Pest Control Billing</a></li>
                        <li class="Machine_Rent_Bill"><a href="#menu9">Machine Rent Bill</a></li>
                        <li class="R_M_billing"><a href="#menu11">R&M Service Billing </a></li>
                         <li class="ad_ex__billing"><a href="#menu12">Administrative Expense Billing </a></li>
                        <%--//vikas 19/06/2019--%>
                        <li class="arrears_billing"><a href="#menu8">Arrears Billing</a></li>
                        <li><a href="#menu6">Bill Upload</a></li>
                        <li><a href="#menu7">Bill Dispatch</a></li>
                        <li><a href="#menu10">Billing Summary Report</a></li>
                    </ul>

                    <div id="menu1" class="manpower_billing">
                        <br />
                        <div class="row">
                            <br />

                            <asp:Button ID="btn_save" runat="server" CssClass="btn btn-primary"
                                Text="Process" OnClick="btn_save_Click" OnClientClick="return req_validation_process();" />&nbsp;&nbsp;

                       <asp:Button ID="btn_attendance" runat="server" CssClass="btn btn-primary"  Width="15%"
                           Text="Client Attendance" OnClick="btn_attendance_Click" OnClientClick="return Req_validation();" />&nbsp;&nbsp;

                     <%-- <asp:Button ID="btn_location_attendance" runat="server" CssClass="btn btn-primary"
                            Text="Location Attendance" OnClick="btn_location_attendance_Click" OnClientClick="return Req_validation();" />&nbsp;&nbsp;--%>

                            <asp:Button ID="btn_breakup" runat="server" CssClass="btn btn-primary"  Width="10%"
                                Text="Break Up" OnClick="btnExport_Click" OnClientClick="return Req_validationAll();" />&nbsp;&nbsp;
                            <asp:Button ID="btn_statewise_breakup" runat="server" CssClass="btn btn-primary"  Width="15%"
                                Text="Statewise Break Up" OnClick="btn_statewise_breakup_Click" OnClientClick="return Req_validation();" />&nbsp;&nbsp;

                      <asp:Button ID="btn_finance_rpt" runat="server" CssClass="btn btn-primary"  Width="15%"
                          Text="Finance Copy" OnClick="btn_finance_rpt_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;

                        <asp:Button ID="btn_invoice" runat="server" CssClass="btn btn-primary"  Width="15%"
                            Text="Invoice Copy" OnClick="btn_invoice_rpt_Click" OnClientClick="return Req_invoice_validation1();" />&nbsp;&nbsp;
                       <asp:Button ID="btn_final_invoice" runat="server" CssClass="btn btn-primary"  Width="15%"
                           Text="Final Invoice Print" OnClick="btn_final_invoice_Click" OnClientClick="return Req_final_invoice();" />&nbsp;&nbsp;
                      
                     <span style="display: none" class="support12">
                         <asp:Button ID="btn_support_format" runat="server" CssClass="btn btn-primary" Width="15%"
                             Text="Support Format" OnClick="btn_support_format_Click" /></span>&nbsp;&nbsp;
                     <%-- <asp:Button ID="btn_attendace_chk" runat="server" CssClass="btn btn-primary"
                          Text="Attendance Details" OnClick="attendance_info" Visible="false" OnClientClick="return Req_validation();" />&nbsp;&nbsp;     --%>
                            </div>
                        <br />
                        <div class="row">
                            <asp:Button ID="btn_client_sheet" runat="server" CssClass="btn btn-primary" Width="15%"
                                Text="Client Sheet" OnClick="btn_client_sheet_Click" Visible="true" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;  
                            <asp:Button ID="btn_report" runat="server" CssClass="btn btn-primary" Text="Report" OnClick="btn_report_Click" OnClientClick="return Req_validation();" />&nbsp;&nbsp;                                  
                         
                          <asp:Button ID="btn_breakup_ot" runat="server" CssClass="btn btn-primary" Width="15%"
                                Text="OT Break Up" OnClick="btn_breakup_ot_Click" OnClientClick="return Req_validationAll();" />&nbsp;&nbsp;
                             <asp:Button ID="btn_finance_rpt_ot" runat="server" CssClass="btn btn-primary" Width="15%"
                                 Text=" OT Finance Copy" OnClick="btn_finance_rpt_ot_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                               <asp:Button ID="btn_invoice_ot" runat="server" CssClass="btn btn-primary" Width="15%"
                                   Text="OT Invoice Copy" OnClick="btn_invoice_ot_Click" OnClientClick="return Req_invoice_validation1();" />&nbsp;&nbsp;
                      <asp:Button ID="btn_ot_final_invoice" runat="server" CssClass="btn btn-primary" Width="15%"
                          Text="Final OT Invoice Print" OnClick="btn_ot_final_invoice_Click" OnClientClick="return Req_final_invoice();" />&nbsp;&nbsp;
                      
                        </div>
                        <br />
                        <div class="row text-center"> 
                         
                             <asp:Button ID="bntclose" runat="server" CssClass="btn btn-danger"
                            Text="CLOSE" OnClick="bntclose_Click" />
                        </div>
                        <br />
                        <div class="container-fluid">

                            <br />


                            <asp:Panel ID="gv_attendance_panel" runat="server" CssClass="grid-view">

                                <div class="row text-center">
                                    <h3>Approve Attendance By Admin</h3>
                                </div>
                                <asp:GridView ID="gv_approve_attendace" runat="server" ForeColor="#333333" AutoGenerateColumns="false" class="table" OnRowDataBound="gv_approve_attendace_RowDataBound" DataKeyNames="unit_code">
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
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chk_gv_header" runat="server" Text=" SELECT " />
                                                <asp:ImageButton ID="zip_download" Width="25" OnClick="zip_download_Click" runat="Server" ImageUrl="~/Images/winzip.png" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_client" runat="server" CssClass="center-block" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="client_name" HeaderText="CLIENT NAME"
                                            SortExpression="client_name" />
                                        <asp:BoundField DataField="state_name" HeaderText="STATE NAME"
                                            SortExpression="state_name" />
                                        <asp:BoundField DataField="branch_name" HeaderText="BRANCH NAME"
                                            SortExpression="branch_name" />
                                        <asp:BoundField DataField="month_year" HeaderText="MONTH YEAR"
                                            SortExpression="month_year" />
                                        <asp:BoundField DataField="status" HeaderText="CURRENT STATUS"
                                            SortExpression="status" />
                                        <asp:BoundField DataField="attendance_file" HeaderText="Attendance_file"
                                            SortExpression="attendance_file" />
                                        <asp:TemplateField HeaderText="DOWNLOAD">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_attendance" runat="server" CausesValidation="false" Text="Attendance Sheet" CommandArgument='<%#Eval("attendance_file")+","+Eval("branch_name") %>' OnCommand="lnkDownload_Command" CssClass="btn btn-primary" Style="color: white"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="APPROVE ">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_approve" runat="server" Style="color: white" CausesValidation="false" Text="Approve" CommandArgument='<%# Eval("unit_code")%>' OnCommand="lnk_approve_Command" CssClass="btn btn-primary" OnClientClick="return R_validation1()"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="REJECT ">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_reject" runat="server" Style="color: white" CausesValidation="false" Text="Reject" CommandArgument='<%#Eval("unit_code") %>' CommandName="reject" OnCommand="lnk_reject_Command" CssClass="btn btn-primary" OnClientClick="return R_validation2()"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="REJECT REASON">
                                            <ItemStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="reject_reason" Text='<%# Eval("rejected_reason") %>' Width="180px" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="id" HeaderText="ID"
                                            SortExpression="id" />

                                         <asp:BoundField DataField="unit_code" HeaderText="unit_code"
                                            SortExpression="unit_code" />

                                    </Columns>
                                </asp:GridView>
                                <div class="row text-center">
                                    <asp:Button ID="btn_approve" runat="server" class="btn btn-primary" OnClick="btn_approve_click" Text="Approve" OnClientClick="return validateCheckBoxes();" />
                              
                                       <asp:Button ID="btn_reject_gv" runat="server" OnClick="btn_reject_gv_Click"  class="btn btn-primary"  Text="Reject" OnClientClick="return validateCheckBoxes_reject();" />
                                      </div>

                                 <div class="row text-center">
                                 
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
                        </div>
                    </div>
                    <div id="menu2" class="material_billing">
                        <br />
                        <%--<asp:UpdatePanel runat="server" UpdateMode="Conditional">
                               <ContentTemplate>--%>
                        <div class="row text-center">

                            <br />
                            <asp:Button ID="btn_material_process" runat="server" CssClass="btn btn-primary"
                                Text="Process" OnClick="btn_material_process_Click" OnClientClick="return req_validation_process();" />&nbsp;&nbsp;
                     
                                   <asp:Button ID="btn_material_fc" runat="server" CssClass="btn btn-primary"
                                       Text="Material Finance Copy" OnClick="btn_material_fc_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;

                                   <asp:Button ID="btn_material_fixbill" runat="server" CssClass="btn btn-primary"
                                       Text="Material Invoice Copy " OnClick="btn_material_fixbill_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                             <asp:Button ID="btn_material_final_bill" runat="server" CssClass="btn btn-primary"
                                 Text="Final Invoice Print " OnClick="btn_material_fbill_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                          
                                <asp:Button ID="btn_material_report" runat="server" CssClass="btn btn-primary"
                                    Text="Report " OnClick="btn_material_report_Click" OnClientClick="return report_link_validation();" />&nbsp;&nbsp;
                          

                           <asp:Button ID="btn_material_bill" runat="server" CssClass="btn btn-primary"
                               Text="Material Actual Bill" OnClick="btn_material_bill_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                             
                           <asp:Button ID="btn_material_invoice" runat="server" CssClass="btn btn-primary"
                               Text="Material Actual Invoice" OnClick="btn_material_invoice_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;

                                 <asp:Button ID="btn_material_Export" runat="server" CssClass="btn btn-primary"
                                     Text="Export Material Details" OnClick="btn_material_Export_Click" OnClientClick="return Req_validation();" />&nbsp;&nbsp;
                              

                        </div>
                        <br />


                        <asp:Panel ID="gv_material_panel" runat="server" CssClass="grid-view">

                            <div class="row text-center">
                                <h3>Approve Material By Admin</h3>
                            </div>
                            <asp:GridView ID="gv_approve_material" runat="server" ForeColor="#333333" AutoGenerateColumns="false" class="table" OnRowDataBound="gv_approve_material_RowDataBound" DataKeyNames="unit_code">
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
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chk_gv_header" runat="server" Text=" SELECT " />
                                            <asp:ImageButton ID="zip_download" Width="25" OnClick="zip_download_Click" runat="Server" ImageUrl="~/Images/winzip.png" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_client" runat="server" CssClass="center-block" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="client_name" HeaderText="CLIENT NAME"
                                        SortExpression="client_name" />
                                    <asp:BoundField DataField="state_name" HeaderText="STATE NAME"
                                        SortExpression="state_name" />
                                    <asp:BoundField DataField="branch_name" HeaderText="BRANCH NAME"
                                        SortExpression="branch_name" />
                                    <asp:BoundField DataField="month_year" HeaderText="MONTH YEAR"
                                        SortExpression="month_year" />
                                    <asp:BoundField DataField="material_status" HeaderText="CURRENT STATUS"
                                        SortExpression="material_status" />
                                    <asp:BoundField DataField="attendance_file" HeaderText="Material_file"
                                        SortExpression="attendance_file" />
                                    <asp:TemplateField HeaderText="DOWNLOAD">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_attendance" runat="server" CausesValidation="false" Text="Material Sheet" CommandArgument='<%#Eval("attendance_file")+","+Eval("branch_name") %>' OnCommand="lnkDownloadMaterial_Command" CssClass="btn btn-primary" Style="color: white"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="APPROVE ">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_approve" runat="server" Style="color: white" CausesValidation="false" Text="Approve" CommandArgument='<%# Eval("unit_code")%>' OnCommand="lnk_approve_CommandMaterial" CssClass="btn btn-primary" OnClientClick="return R_validation1()"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REJECT ">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_reject_material" runat="server" Style="color: white" CausesValidation="false" Text="Reject" CommandArgument='<%#Eval("unit_code") %>' CommandName="reject" OnCommand="lnk_reject_material_Command" CssClass="btn btn-primary" OnClientClick="return R_validation2()"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REJECT REASON">
                                        <ItemStyle Width="50px" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="material_reject_reason" Text='<%# Eval("material_reject_reason") %>' Width="180px" CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="row text-center">
                                <asp:Button ID="btn_material_approve" runat="server" class="btn btn-primary" OnClick="btn_material_approve_Click" Text="Approve" OnClientClick="return validateCheckBoxesMaterial();" />
                            </div>
                        </asp:Panel>

                        <%--7-05-19--%>
                        <div class="container" style="width: 75%">
                            <asp:Panel runat="server" CssClass="grid-view">
                                <asp:GridView ID="grd_material_billing" class="table" runat="server" BackColor="White" OnRowDataBound="grd_material_billing_RowDataBound"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    AutoGenerateColumns="False" OnPreRender="grd_material_billing_PreRender" Width="100%">

                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <Columns>

                                        <asp:BoundField DataField="unit_code" HeaderText="Branch Name" SortExpression="unit_code" />

                                        <asp:BoundField DataField="material_name" HeaderText="Material Name" SortExpression="material_name" />

                                        <asp:BoundField DataField="rate" HeaderText="Rate" SortExpression="rate" />

                                        <asp:BoundField DataField="handling_charges_amount" HeaderText="Handling Charges amount" SortExpression="handling_charges_amount" />

                                        <asp:BoundField DataField="handling_percent" HeaderText="Handling Charges Percent" SortExpression="handling_percent" />

                                        <asp:BoundField DataField="client_code" HeaderText="client_code" SortExpression="client_code" />
                                        <asp:BoundField DataField="state_name" HeaderText="state" SortExpression="state_name" />
                                        <asp:BoundField DataField="new_policy_name" HeaderText="new_policy_name" SortExpression="new_policy_name" />
                                        <asp:BoundField DataField="start_date" HeaderText="start_date" SortExpression="start_date" />
                                        <asp:BoundField DataField="end_date" HeaderText="end_date" SortExpression="end_date" />
                                        <asp:BoundField DataField="designation" HeaderText="designation" SortExpression="designation" />

                                        <asp:BoundField DataField="unit_code1" HeaderText="unit_code1" SortExpression="unit_code1" />
                                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                                        <asp:BoundField DataField="handling_applicable" HeaderText="handling_applicable" SortExpression="handling_applicable" />
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemStyle Width="70px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_quantity" runat="server" Width="80px" Text='<%# Eval("quantity") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>

                        <br />
                        <br />
                        <br />
                        <div class="container">
                            <asp:Panel ID="Panel2" runat="server" CssClass="grid-view" ScrollBars="Auto">

                                <asp:GridView ID="gv_material_bill" runat="server" ForeColor="#333333" class="table" GridLines="Both" Width="100%" OnPreRender="gv_material_bill_PreRender">
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

                                </asp:GridView>

                            </asp:Panel>

                        </div>
                    </div>
                    <div id="menu3" class="conveyance_billing">
                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-13 ">
                                Conveyance Billing : <span class="text-red" style="color: red">*</span>
                                <asp:DropDownList ID="ddl_conveyance_type" runat="server" CssClass="form-control" onchange="return conveyance_bill_check();">
                                    <asp:ListItem Value="1">Employee Conveyance Billing</asp:ListItem>
                                    <asp:ListItem Value="2">Driver Convenyance Billing</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="row text-center conveyance1" style="margin-top: 20px; margin-right: 326px;">

                                <asp:Button ID="btn_conveyance_process" runat="server" CssClass="btn btn-primary"
                                    Text="Process" OnClick="btn_conveyance_process_Click" OnClientClick="return req_validation_process();" />&nbsp;&nbsp;
                              
                            <asp:Button runat="server" ID="btn_conveyance_fc" CssClass="btn btn-primary" OnClick="btn_conveyance_fc_Click" Text="Conveyance Finance Copy" OnClientClick="return Req_validationAll();" />
                                <asp:Button ID="btn_conveyance" runat="server" CssClass="btn btn-primary"
                                    Text="Conveyance Bill" OnClick="btn_conveyance_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                            <asp:Button ID="btn_final_conveyance" runat="server" CssClass="btn btn-primary"
                                Text="Final Conveyance Bill" OnClick="btn_final_conveyance_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                             <asp:Button ID="btn_emp_report" runat="server" CssClass="btn btn-primary"
                                 Text="Report" OnClick="btn_emp_report_Click" OnClientClick="return report_link_validation();" />&nbsp;&nbsp;
                           
                            </div>
                            <div class="row text-center conveyance2" style="margin-top: 20px; margin-right: 326px;">

                                <asp:Button ID="btn_conveyance_process1" runat="server" CssClass="btn btn-primary"
                                    Text="Process" OnClick="btn_conveyance_process_Click" OnClientClick="return req_validation_process();" />&nbsp;&nbsp;
                              
                                <asp:Button runat="server" ID="btn_driver_conveyance" CssClass="btn btn-primary " OnClick="btn_driver_conveyance_Click" Text="Driver Finance Copy" OnClientClick="return Req_validation1();" />
                                <asp:Button ID="btn_driver" runat="server" CssClass="btn btn-primary "
                                    Text="Driver Bill" OnClick="btn_driver_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                             <asp:Button ID="btn_final_driver" runat="server" CssClass="btn btn-primary "
                                 Text="Final Driver Bill" OnClick="btn_final_driver_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                            <asp:Button ID="btn_driver_report" runat="server" CssClass="btn btn-primary"
                                Text="Report" OnClick="btn_driver_report_Click" OnClientClick="return driver_report_validation();" />&nbsp;&nbsp;
                          
                            
                            </div>
                        </div>
                        <asp:Panel ID="gv_con_emp_panel" runat="server" CssClass="grid-view">

                            <div class="row text-center">
                                <h3>Approve Employee Conveyance By Admin</h3>
                            </div>
                            <asp:GridView ID="gv_con_emp_approve" runat="server" ForeColor="#333333" AutoGenerateColumns="false" class="table" OnRowDataBound="gv_con_emp_approve_RowDataBound" DataKeyNames="unit_code">
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
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chk_gv_header" runat="server" Text=" SELECT " />
                                            <asp:ImageButton ID="zip_download" Width="25" OnClick="zip_download_Click" runat="Server" ImageUrl="~/Images/winzip.png" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_client" runat="server" CssClass="center-block" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="client_name" HeaderText="CLIENT NAME"
                                        SortExpression="client_name" />
                                    <asp:BoundField DataField="state" HeaderText="STATE NAME"
                                        SortExpression="state" />
                                    <asp:BoundField DataField="branch_name" HeaderText="BRANCH NAME"
                                        SortExpression="branch_name" />
                                    <asp:BoundField DataField="month_year" HeaderText="MONTH YEAR"
                                        SortExpression="month_year" />
                                    <asp:BoundField DataField="con_emp_status" HeaderText="CURRENT STATUS"
                                        SortExpression="con_emp_status" />
                                    <asp:BoundField DataField="attendance_file" HeaderText="Conveyance_file"
                                        SortExpression="attendance_file" />
                                    <asp:TemplateField HeaderText="DOWNLOAD">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_con_emp" runat="server" CausesValidation="false" Text="Conveyance Sheet" CommandArgument='<%#Eval("attendance_file")+","+Eval("branch_name") %>' OnCommand="lnkDownloadCon_Emp_Command" CssClass="btn btn-primary" Style="color: white"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="APPROVE ">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_approve" runat="server" Style="color: white" CausesValidation="false" Text="Approve" CommandArgument='<%# Eval("unit_code")%>' OnCommand="lnk_approve_CommandCon_Emp" CssClass="btn btn-primary" OnClientClick="return R_validation1()"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REJECT ">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_reject_Con_Emp" runat="server" Style="color: white" CausesValidation="false" Text="Reject" CommandArgument='<%#Eval("unit_code") %>' CommandName="reject" OnCommand="lnk_reject_Con_Emp_Command" CssClass="btn btn-primary" OnClientClick="return R_validation2()"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REJECT REASON">
                                        <ItemStyle Width="50px" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="con_emp_reject_reason" Text='<%# Eval("con_emp_reject_reason") %>' Width="180px" CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="row text-center">
                                <asp:Button ID="btn_con_emp_approve" runat="server" class="btn btn-primary" OnClick="btn_con_emp_approve_Click" Text="Approve" OnClientClick="return validateCheckBoxesEmpConveyance();" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="gv_con_driver_panel" runat="server" CssClass="grid-view">

                            <div class="row text-center">
                                <h3>Approve Driver Conveyance By Admin</h3>
                            </div>
                            <asp:GridView ID="gv_con_driver_approve" runat="server" ForeColor="#333333" AutoGenerateColumns="false" class="table" OnRowDataBound="gv_con_driver_approve_RowDataBound" DataKeyNames="unit_code,emp_code">
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
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chk_gv_header" runat="server" Text=" SELECT " />
                                            <asp:ImageButton ID="zip_download" Width="25" OnClick="zip_download_Click" runat="Server" ImageUrl="~/Images/winzip.png" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_client" runat="server" CssClass="center-block" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="client_name" HeaderText="CLIENT NAME"
                                        SortExpression="client_name" />
                                    <asp:BoundField DataField="state" HeaderText="STATE NAME"
                                        SortExpression="state" />
                                    <asp:BoundField DataField="branch_name" HeaderText="BRANCH NAME"
                                        SortExpression="branch_name" />
                                    <asp:BoundField DataField="emp_name" HeaderText="EMPLOYEE NAME"
                                        SortExpression="emp_name" />
                                    <asp:BoundField DataField="month_year" HeaderText="MONTH YEAR"
                                        SortExpression="month_year" />
                                    <asp:BoundField DataField="con_driver_status" HeaderText="CURRENT STATUS"
                                        SortExpression="con_driver_status" />
                                    <asp:BoundField DataField="attendance_file" HeaderText="Conveyance_file"
                                        SortExpression="attendance_file" />
                                    <asp:TemplateField HeaderText="DOWNLOAD">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_con_driver" runat="server" CausesValidation="false" Text="Conveyance Sheet" CommandArgument='<%#Eval("attendance_file")+","+Eval("branch_name") %>' OnCommand="lnkDownloadCon_Driver_Command" CssClass="btn btn-primary" Style="color: white"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="APPROVE ">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_approve" runat="server" Style="color: white" CausesValidation="false" Text="Approve" CommandArgument='<%# Eval("unit_code")%>' OnCommand="lnk_approve_CommandCon_Driver" CssClass="btn btn-primary" OnClientClick="return R_validation1()"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REJECT ">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_reject_Driver_Emp" runat="server" Style="color: white" CausesValidation="false" Text="Reject" CommandArgument="<%# Container.DataItemIndex%>" CommandName="reject" OnCommand="lnk_reject_Con_Driver_Command" CssClass="btn btn-primary" OnClientClick="return R_validation2()"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REJECT REASON">
                                        <ItemStyle Width="50px" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="con_driver_rejected_reason" Text='<%# Eval("con_driver_rejected_reason") %>' Width="180px" CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="emp_code" HeaderText="EMPLOYEE NAME"
                                            SortExpression="emp_code"/>--%>
                                </Columns>
                            </asp:GridView>
                            <div class="row text-center">
                                <asp:Button ID="btn_con_driver_approve" runat="server" class="btn btn-primary" OnClick="btn_con_driver_approve_Click" Text="Approve" OnClientClick="return validateCheckBoxesDriverConveyance();" />
                            </div>
                        </asp:Panel>
                        <br />
                        <br />
                        <br />
                    </div>
                    <div id="menu4" class="deep_clean_billing">
                        <br />
                        <div class="row text-center">
                            <asp:Button ID="btn_dc_process" runat="server" CssClass="btn btn-primary"
                                Text="Process" OnClick="btn_dc_process_Click" OnClientClick="return req_validation_process();" />&nbsp;&nbsp;
                              <asp:Button runat="server" ID="btn_dc_fc" CssClass="btn btn-primary" OnClick="btn_dc_fc_Click" Text="Deep Clean Finance Copy" OnClientClick="return Req_validationAll();" />
                            <asp:Button ID="btn_dc" runat="server" CssClass="btn btn-primary"
                                Text="Deep Cleaning Bill" OnClick="btn_dc_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btn_dc_fb" CssClass="btn btn-primary" OnClick="btn_dc_fb_Click" Text="Final Deep Cleaning Bill" OnClientClick="return Req_validation1();" />
                        </div>
                    </div>
                    <div id="menu5" class="pest_control_billing">
                        <br />
                        <div class="row text-center">
                            <asp:Button runat="server" ID="btn_pc_process" CssClass="btn btn-primary" OnClick="btn_pc_process_Click" Text="Process" OnClientClick="return req_validation_process();" />&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btn_pestcontrol_fc" CssClass="btn btn-primary" OnClick="btn_pestcontrol_fc_Click" Text="Pest Control Finance Copy" OnClientClick="return Req_validation1();" />
                        </div>
                    </div>
                    <div id="menu8">
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                               <b> Arrear Type : </b>
                            <asp:DropDownList ID="ddl_arrears_type" runat="server" CssClass="form-control" onchange="return arrear_type();">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem Value="month">Month Wise</asp:ListItem>
                                <asp:ListItem Value="policy">Policy Wise</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12 arrear">
                                <b>From Month :</b><span class="text-red">*</span>
                                <asp:TextBox ID="txt_arrear_month_year" Class="form-control date-picker1" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12 arrear">
                                <b>To Month :</b><span class="text-red">*</span>
                                <asp:TextBox ID="txt_arrear_monthend" Class="form-control date-picker2" runat="server"></asp:TextBox>
                            </div>

                        </div>
                        <br />
                        <br />
                        <div class="row-centered">
                            <asp:Button runat="server" ID="Req_arrears_bill" CssClass="btn btn-large" Width="60%" OnClick="Req_arrears_bill_Click" Text="Request for arrears bill" OnClientClick="return Req_arrears();" Visible="false" />&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btn_arrears_process" CssClass="btn btn-large" OnClick="btn_arrears_process_Click" Text="Process" OnClientClick="return Req_arrears();" />&nbsp;&nbsp;
                             <asp:Button runat="server" ID="btn_arrears_attendance" CssClass="btn btn-large" Width="20%" OnClick="btn_arrears_attendance_Click" Text="Arrears Client Attendances" OnClientClick="return Req_arrears();" />&nbsp;&nbsp;
                             <asp:Button runat="server" ID="btn_arrears_rate_breakup" CssClass="btn btn-large" Width="15%" OnClick="btn_arrears_rate_breakup_Click" Text="Arrears Rate Breakup" OnClientClick="return Req_arrears();" />&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btn_arrears_finance" CssClass="btn btn-large" Width="15%" OnClick="btn_arrears_finance_Click" Text="Arrears Finance Copy" OnClientClick="return Req_arrears();" />&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btn_arrears_invoice" CssClass="btn btn-large" Width="15%" OnClick="btn_arrears_invoice_Click" Text="Arrears Invoice Copy" OnClientClick="return Req_arrears_final();" />&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btn_final_arrears_invoice" CssClass="btn btn-large" OnClick="btn_final_arrears_invoice_Click" Text="Final Invoice copy" OnClientClick="return Req_arrears_final();" />&nbsp;&nbsp;
							                             <asp:Button ID="btn_final_arrears_client" runat="server" CssClass="btn btn-primary" Text="Client Sheet" OnClick="btn_final_arrears_client_Click" Visible="true" OnClientClick="return Req_validation1();" />&nbsp;&nbsp; 
                           
                        </div>
                    </div>
                    <div id="menu6">
                        <br />
                        <asp:Panel ID="pnl_show_btn" runat="server" CssClass="panel panel-primary" Style="border-color: #bfbcbc;">
                            <div class="container-fluid">

                                <div class="row">
                                    <br />
                                    <div class="col-sm-3 col-xs-12">
                                        <asp:Button ID="btn_show" runat="server" CssClass="btn btn-primary" Text="Show" OnClick="btn_show_Click" OnClientClick="return Req_validation();" />
                                    </div>
                                </div>
                                <br />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnl_send_email" runat="server" CssClass="panel panel-primary" Style="border-color: #bfbcbc;">

                            <div class="container-fluid">
                                <br />
                                <div class="row">
                                    <br />

                                    <div class="col-sm-3 col-xs-12">
                                        <asp:Button runat="server" ID="btn_send_mail" OnClick="btn_send_mail_Click" OnClientClick="return send_email();" Text="Send Mail" CssClass="btn btn-primary" />
                                    </div>

                                </div>
                                <br />
                            </div>

                            <div class="panel-body">
                                <div class=" panel panel-primary flip1" style="margin-bottom: -5px;">
                                    <div class="panel-heading  text-center" style="padding: -20px">
                                        <div class="head-space" style="font-size: small;">
                                            <div class="row">

                                                <b>All State Bill</b>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <asp:Panel ID="Panel1" HorizontalAlign="Center" runat="server" CssClass="panel panel-primary panel-disp1" ViewStateMode="Disabled" Style="border-color: #bfbcbc;">
                                    <div class="row">
                                        <br />
                                        <div class="col-sm-3"></div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:CheckBox ID="chk_xls_bill_all" runat="server" Text=" Add to Email" />
                                            <asp:Label runat="server" ID="lbl_all_state" Text="All State bill Not Finalized" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            Excel Bill for All State
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:Button runat="server" ID="btn_state_download" OnClick="btn_state_download_Click" Text="Download" CssClass="btn btn-primary" />
                                        </div>

                                    </div>
                                    <br />
                                </asp:Panel>
                                <div class=" panel panel-primary flip2" style="margin-bottom: -5px;">
                                    <div class="panel-heading text-center">
                                        <div class="head-space" style="font-size: small;">
                                            <div class="row">
                                                <%-- <asp:CheckBox ID="chk_pf_challan_ecr" style="margin-left: 10px;" Text=" Add to Email" runat="server" />--%>
                                                <b>PF Challan and ECR</b>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <asp:Panel ID="Panel14" HorizontalAlign="Center" runat="server" CssClass="panel panel-primary panel-disp2" Style="border-color: #bfbcbc;">
                                    <div class="row">
                                        <br />
                                        <div class="col-sm-3"></div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:CheckBox ID="chk_pf_challan" Text=" Add to Email" runat="server" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            PF CHALLAN
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:FileUpload ID="up_pf_challan" runat="server" meta:resourcekey="photo_uploadResource1" onchange="ValidateSingleInput(this);" />
                                            <asp:Button runat="server" ID="btn_pf_challan_do" OnClick="btn_pf_challan_do_Click" Text="Download" CssClass="btn btn-primary" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:Button runat="server" ID="btn_upload_pf" OnClick="btn_upload_pf_Click" OnClientClick="return pf_upload();" Text="Upload" CssClass="btn btn-primary" />
                                            <asp:Button runat="server" ID="btn_pf_challan_de" OnClick="btn_pf_challan_de_Click" OnClientClick="return confirm('Are you sure you want to delete?');" Text="Delete" CssClass="btn btn-primary" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <br />
                                        <div class="col-sm-3"></div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:CheckBox ID="chk_ecr_file" Text=" Add to Email" runat="server" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            ECR File
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:FileUpload ID="up_ecr_file" runat="server" meta:resourcekey="photo_uploadResource1" onchange="ValidateSingleInput(this);" />
                                            <asp:Button runat="server" ID="btn_ecr_file_do" OnClick="btn_ecr_file_do_Click" Text="Download" CssClass="btn btn-primary" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:Button runat="server" ID="btn_ecr_upload" OnClick="btn_ecr_upload_Click" OnClientClick="return ecr_upload();" Text="Upload" CssClass="btn btn-primary" />
                                            <br />
                                            <asp:Button runat="server" ID="btn_ecr_file_de" OnClick="btn_ecr_file_de_Click" OnClientClick="return confirm('Are you sure you want to delete?');" Text="Delete" CssClass="btn btn-primary" />
                                        </div>
                                        <br />
                                    </div>
                                    <br />
                                </asp:Panel>
                                <div class=" panel panel-primary flip3" style="margin-bottom: -5px;">
                                    <div class="panel-heading text-center">
                                        <div class="head-space" style="font-size: small">
                                            <div class="row">
                                                <%--  <asp:CheckBox ID="chk_esic_upload" style="margin-left: 10px;" Text=" Add to Email" runat="server" />--%>
                                                <b>ESIC Upload</b>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <asp:Panel ID="Panel15" HorizontalAlign="Center" runat="server" CssClass="panel panel-primary panel-disp3" Style="border-color: #bfbcbc;">
                                    <asp:GridView ID="grd_esic_upload" class="table" runat="server" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        OnRowDataBound="grd_esic_upload_RowDataBound" OnRowCommand="grd_esic_upload_RowCommand" DataKeyNames="state_name" AutoGenerateColumns="False" Width="100%">
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
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chk_state_header" runat="server" Text=" SELECT " />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_state" runat="server" CssClass="center-block" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                            <asp:BoundField DataField="type" HeaderText="State Name" SortExpression="type" />
                                            <asp:TemplateField HeaderText="File">
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fu_esic" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload">
                                                <ItemTemplate>
                                                    <asp:Button ID="saveBtn" runat="server" CommandArgument="<%# Container.DataItemIndex%>" CommandName="save" Text="Upload" />
                                                    <asp:Button ID="btn_delete" runat="server" OnClientClick="return confirm('Are you sure you want to delete?');" CommandArgument="<%# Container.DataItemIndex%>" CommandName="delete_record" Text="Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Download">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                                <div class=" panel panel-primary flip4" style="margin-bottom: -5px;">
                                    <div class="panel-heading text-center">
                                        <div class="head-space" style="font-size: small"><b>Scanned Bill Upload</b></div>
                                    </div>
                                </div>
                                <br />

                                <asp:Panel ID="Panel16" HorizontalAlign="Center" runat="server" CssClass="panel panel-primary panel-disp4" Style="border-color: #bfbcbc;">
                                    <asp:GridView ID="grd_bill_upload" class="table" runat="server" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        OnRowDataBound="grd_bill_upload_RowDataBound" OnRowCommand="grd_bill_upload_RowCommand" DataKeyNames="state_name" AutoGenerateColumns="False" Width="100%">
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
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chk_bill_state" runat="server" Text=" SELECT " />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_bill" runat="server" CssClass="center-block" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                            <asp:BoundField DataField="type" HeaderText="State Name" SortExpression="type" />
                                            <asp:TemplateField HeaderText="File">
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fu_scan_bill" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload">
                                                <ItemTemplate>
                                                    <asp:Button ID="saveBtn" runat="server" CommandArgument="<%# Container.DataItemIndex%>" CommandName="save" Text="Upload" />
                                                    <asp:Button ID="btn_delete" runat="server" OnClientClick="return confirm('Are you sure you want to delete?');" CommandArgument="<%# Container.DataItemIndex%>" CommandName="delete_record" Text="Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Download">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                            <br />
                        </asp:Panel>
                    </div>
                    <div id="menu7">
                        <br />
                        <div class="row">
                            <div class="col-sm-4 col-xs-12"></div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Month :</b>
                                <asp:TextBox runat="server" ID="txt_dispatch_month" CssClass="form-control date-picker" />
                            </div>
                            <div class="col-sm-2 col-xs-12" style="margin-top: 1.5em;">
                                <asp:Button runat="server" CssClass="btn btn-primary" Text="Submit" />
                            </div>
                        </div>
                        <br />
                        <div class="container-fluid">
                            <asp:Panel ID="Panel9" runat="server" CssClass="grid-view">
                                <asp:GridView ID="gv_bill_dispatch" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" ShowFooter="false">
                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>

                    <div id="menu10">
                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                                <b>Report type:</b> <span class="text-red">*</span>
                                <asp:DropDownList ID="ddl_report" runat="server" onchange="return bill_check();" class="form-control">
                                    <asp:ListItem Value="Salary Slip Sending Details">Salary Slip Sending Details</asp:ListItem>
                                    <asp:ListItem Value="Monthwise Billing Details">Monthwise Billing Details</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12 bill" style="display: none">
                                Billing type: <span class="text-red" style="color: red">*</span>
                                <asp:DropDownList ID="ddl_bill_type" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Clientwise Bill</asp:ListItem>
                                    <asp:ListItem Value="2">Statewise Bill</asp:ListItem>
                                    <asp:ListItem Value="3">Branchwise Bill</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <asp:Button ID="btn_getxl_report" Style="margin-top: 18px;" runat="server" class="btn btn-large" OnClick="btn_getxl_report_Click" Text="Get Report" OnClientClick="return report_validation()" />

                            </div>
                        </div>
                    </div>
                    <div id="menu9" class="Machine_Rent_Bill">
                        <br />
                        <div class="row">
                            <div class="col-sm-6 col-xs-12" style="margin-top: 1.5em;">
                                <asp:Button ID="btn_process" runat="server" class="btn btn-primary" Text="Process" OnClick="btn_process_Click" />
                                <asp:Button ID="btn_rent_finance_copy" runat="server" class="btn btn-primary" Text="Rent Finance Copy" OnClick="btn_rent_invoice_Click" OnClientClick="return Req_validation1();" />
                                <asp:Button ID="btn_rent_invoice" runat="server" class="btn btn-primary" Text="Rent Invoice" OnClick="btn_rent_finance_copy_Click" OnClientClick="return Req_validation1();" />
                                <asp:Button ID="btn_final_inv" runat="server" class="btn btn-primary" Text="Final Invoice" OnClick="btn_final_inv_Click" OnClientClick="return Req_validation1();" />
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="container-fluid">
                            <asp:Panel ID="Panel13" runat="server" CssClass="grid-view" Style="overflow-x: hidden;">

                                <asp:GridView ID="gv_machine_rent" class="table" runat="server" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    AutoGenerateColumns="False" Width="100%" OnRowDataBound="gv_machine_rent_RowDataBound" OnPreRender="gv_machine_rent_PreRender">

                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="White" ForeColor="#000066" />

                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="Id"
                                            SortExpression="id" />
                                        <asp:BoundField DataField="policy_id" HeaderText="policy_id"
                                            SortExpression="policy_id" />
                                        <asp:BoundField DataField="billing_unit_code" HeaderText="Branch Name"
                                            SortExpression="billing_unit_code" />
                                        <asp:BoundField DataField="policy_machine_nane" HeaderText="Machine Name"
                                            SortExpression="policy_machine_nane" />
                                        <asp:BoundField DataField="policy_rate_type" HeaderText="Rate Type"
                                            SortExpression="policy_rate_type" />
                                        <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_rate" runat="server" Width="65px" Height="28px" Text='<%# Eval("policy_m_rate") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="policy_m_rate1" HeaderText="policy_m_rate1"
                                            SortExpression="policy_m_rate1" />
                                        <asp:BoundField DataField="policy_m_h_charges" HeaderText="policy_m_h_charges"
                                            SortExpression="policy_m_h_charges" />
                                        <asp:BoundField DataField="policy_in_pre" HeaderText="policy_in_pre"
                                            SortExpression="policy_in_pre" />
                                        <asp:BoundField DataField="policy_m_amount" HeaderText="policy_m_amount"
                                            SortExpression="policy_m_amount" />
                                        <asp:BoundField DataField="machine_code" HeaderText="machine_code"
                                            SortExpression="machine_code" />
                                        <%-- policy_m_rate--%>
                                    </Columns>
                                </asp:GridView>

                            </asp:Panel>

                        </div>
                    </div>
                    <div id="menu11" class="R_M_billing">
                        <br />
                        <div class="row text-center">
                            <asp:Button ID="btn_r_m_proccess" runat="server" class="btn btn-primary" Text="Process" OnClick="btn_r_m_proccess_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btn_r_m_finance" runat="server" class="btn btn-primary" Text="Finance Copy" OnClick="btn_r_m_finance_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                                <asp:Button ID="btn_r_m_invoice" runat="server" class="btn btn-primary" Text="Invoice" OnClick="btn_r_m_invoice_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                                <asp:Button ID="btn_r_m_final_invoice" runat="server" class="btn btn-primary" Text="Final Invoice" OnClick="btn_r_m_final_invoice_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                        
                        </div>
                             <asp:Panel ID="gv_r_m_load_panel" runat="server" CssClass="grid-view">

                            <div class="row text-center">
                                <h3>Approve R&M By Admin</h3>
                            </div>
                            <asp:GridView ID="gv_r_m_load" runat="server" ForeColor="#333333" AutoGenerateColumns="false" class="table" OnRowDataBound="gv_r_m_load_RowDataBound" DataKeyNames="unit_code">
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
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chk_gv_header_r_m" runat="server" Text=" SELECT " />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_client_rm" runat="server" CssClass="center-block" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="client_name" HeaderText="CLIENT NAME"
                                        SortExpression="client_name" />
                                    <asp:BoundField DataField="state_name" HeaderText="STATE NAME"
                                        SortExpression="state_name" />
                                    <asp:BoundField DataField="branch_name" HeaderText="BRANCH NAME"
                                        SortExpression="unit_name" />
                                    <asp:BoundField DataField="unit_code" HeaderText="BRANCH NAME"
                                        SortExpression="unit_code" />
                                    <asp:TemplateField HeaderText="DOWNLOAD">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_R_M" runat="server" CausesValidation="false" Text="Download" CommandArgument='<%#Eval("attendance_file")+","+Eval("branch_name") %>' OnCommand="lnk_R_M_Command" CssClass="btn btn-primary" Style="color: white"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REJECT ">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_reject_r_m" runat="server" Style="color: white" CausesValidation="false" Text="Reject" CommandArgument='<%#Eval("unit_code") %>' CommandName="reject" OnCommand="lnk_reject_r_m_Command" CssClass="btn btn-primary" OnClientClick="return R_validation2()"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REJECT REASON">
                                        <ItemStyle Width="50px" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="reject_reason" Text='<%# Eval("reject_reason") %>' Width="180px" CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:BoundField DataField="r_m_status" HeaderText="STATUS"
                                        SortExpression="r_m_status" />
                                </Columns>
                            </asp:GridView>
                            <div class="row text-center">
                                <asp:Button ID="btn_r_m_aprrove" runat="server" class="btn btn-primary" Text="Approve" OnClick="btn_r_m_aprrove_Click" OnClientClick="return confirm('Are you sure You want Approve Record?')" />
                                 <asp:Button ID="btn_reject" runat="server" class="btn btn-primary" Text="Reject" OnClick="btn_reject_Click" OnClientClick="return confirm('Are you sure You want Reject Record?')" />
                            </div>
                        </asp:Panel>
                        <br />
                        <br />

                    </div>
                       <div id="menu12" class="ad_ex__billing">
                        <br />
                        <div class="row text-center">
                            <asp:Button ID="btn_adex_process" runat="server" class="btn btn-primary" Text="Process" OnClick="btn_adex_process_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btn_adex_finance" runat="server" class="btn btn-primary" Text="Finance Copy" OnClick="btn_adex_finance_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                                <asp:Button ID="btn_adex_invoice" runat="server" class="btn btn-primary" Text="Invoice" OnClick="btn_adex_invoice_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                                <asp:Button ID="btn_adex_final_invoice" runat="server" class="btn btn-primary" Text="Final Invoice" OnClick="btn_adex_final_invoice_Click" OnClientClick="return Req_validation1();" />&nbsp;&nbsp;
                        
                        </div>
                            <asp:Panel ID="gv_admini_load_panel" runat="server" CssClass="grid-view">

                            <div class="row text-center">
                                <h3>Approve  By Administrative Expense Admin</h3>
                            </div>
                            <asp:GridView ID="GridView_admini" runat="server" ForeColor="#333333" AutoGenerateColumns="false" class="table" OnRowDataBound="GridView_admini_RowDataBound" DataKeyNames="unit_code">
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
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chk_gv_header_admini" runat="server" Text=" SELECT " />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_client_admin" runat="server" CssClass="center-block" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="client_name" HeaderText="CLIENT NAME"
                                        SortExpression="client_name" />
                                    <asp:BoundField DataField="state_name" HeaderText="STATE NAME"
                                        SortExpression="state_name" />
                                    <asp:BoundField DataField="branch_name" HeaderText="BRANCH NAME"
                                        SortExpression="unit_name" />
                                      <asp:BoundField DataField="unit_code" HeaderText="BRANCH CODE"
                                        SortExpression="unit_code" />
                                   
                                    <asp:TemplateField HeaderText="DOWNLOAD">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_admini_expense" runat="server" CausesValidation="false" Text="Download" CommandArgument='<%#Eval("attendance_file")+","+Eval("branch_name") %>' OnCommand="lnk_admini_expense_Command" CssClass="btn btn-primary" Style="color: white"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REJECT ">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_reject_admini" runat="server" Style="color: white" CausesValidation="false" Text="Reject" CommandArgument='<%#Eval("unit_code") %>' CommandName="reject" OnCommand="lnk_reject_admini_Command" CssClass="btn btn-primary" OnClientClick="return R_validation2()"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REJECT REASON">
                                        <ItemStyle Width="50px" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="reject_reason" Text='<%# Eval("reject_reason") %>' Width="180px" CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="admini_status" HeaderText="STATUS"
                                        SortExpression="admini_status" />
                                </Columns>
                            </asp:GridView>
                            <div class="row text-center">
                                <asp:Button ID="btn_admini_approve" runat="server" class="btn btn-primary" Text="Approve" OnClick="btn_admini_approve_Click" />
                           <asp:Button ID="btn_reject_admin" runat="server" class="btn btn-primary" Text="Reject" OnClick="btn_reject_admin_Click" OnClientClick="return confirm('Are you sure You want Reject Record?')" />
                                 </div>
                        </asp:Panel>
                        <br />
                        <br />

                    </div>
                </div>
            </div>
            <br />
        </div>
    </div>

</asp:Content>
