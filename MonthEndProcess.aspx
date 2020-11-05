<%@ Page Title="Month End Process" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MonthEndProcess.aspx.cs" Inherits="AttendanceRegister" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
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

    <style>
        .auto-style1 {
            color: #FFFFFF;
        }

        .text-red {
            color: #f00;
        }

        h2 {
            border-radius: 5px;
        }

        .grid-view {
            height: auto;
            max-height: 300px;
            overflow-y: auto;
            overflow-x: hidden;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }


        /*.table {
            width: 50%;
            max-width:none;
        }*/
        #modalContainer {
            background-color: rgba(0, 0, 0, 0.3);
            position: absolute;
            top: 0;
            width: 100%;
            height: 100%;
            left: 0px;
            z-index: 10000;
            background-image: url(tp.png); /* required by MSIE to prevent actions on lower z-index elements */
        }

        #alertBox {
            position: relative;
            width: 33%;
            min-height: 100px;
            max-height: 400px;
            margin-top: 50px;
            border: 1px solid #fff;
            background-color: #fff;
            background-repeat: no-repeat;
            top: 30%;
        }

        #modalContainer > #alertBox {
            position: fixed;
        }

        #alertBox h1 {
            margin: 0;
            font: bold 1em Raleway,arial;
            background-color: #337ab7;
            color: #FFF;
            border-bottom: 1px solid #14136b;
            padding: 10px 0 10px 5px;
            font-weight: bold;
            font-size: 12px;
        }

        #alertBox p {
            height: 50px;
            padding-left: 5px;
            padding-top: 30px;
            text-align: center;
            vertical-align: middle;
            font-weight: bold;
            font-size: 12px;
        }

        #alertBox #closeBtn {
            display: block;
            position: relative;
            margin: 30px auto 10px auto;
            padding: 7px;
            border: 0 none;
            font-weight: bold;
            font-size: 12px;
            width: 70px;
            text-transform: uppercase;
            text-align: center;
            color: #FFF;
            background-color: #337ab7;
            border-radius: 0px;
            text-decoration: none;
            outline: 0 !important;
        }

        /* unrelated styles */

        #mContainer {
            position: relative;
            width: 600px;
            margin: auto;
            padding: 5px;
            border-top: 2px solid #fff;
            border-bottom: 2px solid #fff;
        }

        h1, h2 {
            margin: 0;
            padding: 4px;
        }

        code {
            font-size: 1.2em;
            color: #069;
        }

        #credits {
            position: relative;
            margin: 25px auto 0px auto;
            width: 350px;
            font: 0.7em verdana;
            border-top: 1px solid #000;
            border-bottom: 1px solid #000;
            height: 90px;
            padding-top: 4px;
        }

            #credits img {
                float: left;
                margin: 5px 10px 5px 0px;
                border: 1px solid #000000;
                width: 80px;
                height: 79px;
            }

        .important {
            background-color: #14136b;
            padding: 2px;
        }

        @media (max-width: 600px) {
            #alertBox {
                position: relative;
                width: 90%;
                top: 30%;
            }
        }
    </style>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <script type="text/javascript">
        var ALERT_TITLE = "";
        var ALERT_BUTTON_TEXT = "Ok";

        if (document.getElementById) {
            window.alert = function (txt) {
                createCustomAlert(txt);
            }
        }

        function createCustomAlert(txt) {
            d = document;

            if (d.getElementById("modalContainer")) return;

            mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
            mObj.id = "modalContainer";
            mObj.style.height = d.documentElement.scrollHeight + "px";

            alertObj = mObj.appendChild(d.createElement("div"));
            alertObj.id = "alertBox";
            if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
            alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
            alertObj.style.visiblity = "visible";

            h1 = alertObj.appendChild(d.createElement("h1"));
            h1.appendChild(d.createTextNode(ALERT_TITLE));

            msg = alertObj.appendChild(d.createElement("p"));
            //msg.appendChild(d.createTextNode(txt));
            msg.innerHTML = txt;

            btn = alertObj.appendChild(d.createElement("a"));
            btn.id = "closeBtn";
            btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
            btn.href = "#";
            btn.focus();
            btn.onclick = function () { removeCustomAlert(); return false; }

            alertObj.style.display = "block";

        }

        function removeCustomAlert() {
            document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainer"));
        }
        function ful() {
            alert('Alert this pages');
        }
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {
            var table = $('#<%=gv_fullmonthot.ClientID%>').DataTable({
                       scrollY: "210px", buttons: [
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
                       ],
                       fixedHeader: {
                           header: true,
                           footer: true
                       }
                   });

                   table.buttons().container()
                      .appendTo('#<%=gv_fullmonthot.ClientID%>_wrapper .col-sm-6:eq(0)');
            $('#<%=ddl_arrears_client.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });
                   $('#<%=ddl_arrears_state.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });
                   $('#<%=ddl_arrears_unit.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });

                   $('#<%=ddl_bill_client.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });
                   $('#<%=ddl_bill_state.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });
                   $('#<%=ddlregion.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });
                   $('#<%=ddl_bill_unit.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });
                   $('#<%=ddl_pmt_client.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });
                   $('#<%=ddl_pmt_state.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });
                   $('#<%=ddl_pmt_unit.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });
                   $('#<%=ddl_client1.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });
                   $('#<%=ddl_statename.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });
                   $('#<%=ddl_bank_name.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });
                   $('#<%=ddl_minibank_client.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });
                   $('#<%=ddl_client_bank.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });
                   $('#<%=ddl_invoice_type.ClientID%>').change(function () {
                       $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                   });

                
                   vendor_bill_check();

                   var table = $('#<%=gv_arrears_gridview.ClientID%>').DataTable({
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
               .appendTo('#<%=gv_arrears_gridview.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';
            $('.date-picker1').datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "1950",
                maxDate: 0,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',

            });
            $(".date-picker1").attr("readonly", "true");


            $('.date-picker2').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });
            $(".date-picker2").focus(function () {
                $(".ui-datepicker-calendar").hide();
            })

            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                yearRange: '1950',
                maxDate: 0,
                dateFormat: 'mm/yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, month, 1));
                }
            });
            $(".date-picker").focus(function () {
                $(".ui-datepicker-calendar").hide();
            })

            $(".date-picker").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");
            disabled();
            //  conveyance_bill_check();
            support_format1();
        }
        $(document).ready(function () {
            var table = $('#<%=gv_payment.ClientID%>').DataTable({
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
               .appendTo('#<%=gv_payment.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';

            var table = $('#<%=gv_billing_details.ClientID%>').DataTable({
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
              .appendTo('#<%=gv_billing_details.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';

           
            var table = $('#<%=gv_minibank.ClientID%>').DataTable({
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
                 .appendTo('#<%=gv_minibank.ClientID%>_wrapper .col-sm-6:eq(0)');
            $.fn.dataTable.ext.errMode = 'none';
        });
        function Req_validation() {
            var ddl_client1 = document.getElementById('<%=ddl_client1.ClientID %>');
            var Selected_ddl_client1 = ddl_client1.options[ddl_client1.selectedIndex].text;
            if (Selected_ddl_client1 == "Select") {
                alert("Please Select Client Name");
                ddl_client1.focus();
                return false;

            }
            var month_year = document.getElementById('<%=txt_month.ClientID %>');
            //month year
                  if (month_year.value == "") {
                      alert("Please Select month & year.");
                      month_year.focus();
                      return false;
                  }
            // Select Status 
                  $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                  return true;
              }

              function chk_month() {
                  var month_year = document.getElementById('<%=txt_month.ClientID %>');
            var ddl_bill_client = document.getElementById('<%=ddl_bill_client.ClientID %>');
            var Selected_ddl_bill_client = ddl_bill_client.options[ddl_bill_client.selectedIndex].text;
            //month year
            if (ddl_bill_client.value != "Select") {
                alert("Please Select month & year.");
                month_year.focus();
                return false;
            }

        }

        function show() {

            alert("hello");
        }

        function openWindow() {

            window.open("html/MonthAndProcess.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');

        }

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
        function disabled() {
            var d_client = document.getElementById('<%=ddl_client.ClientID %>');
            var d_state = document.getElementById('<%=ddl_state.ClientID %>');
            var d_branch = document.getElementById('<%=ddl_branch.ClientID %>');

            d_client.disabled = true;
            d_state.disabled = true;
            d_branch.disabled = true;

        }
        function validation() {



            var txt_date = document.getElementById('<%=txt_date.ClientID%>');
            if (txt_date.value == "") {
                alert("Please Select Month");
                txt_date.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function Required_validation() {

            var txt_invoice_no = document.getElementById('<%=txt_invoice_no.ClientID %>');

            if (txt_invoice_no.value == "") {
                alert("Please Enter Invoice Number");
                txt_invoice_no.focus();
                return false;

            }

            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

            if (Selected_ddl_client == "") {
                alert("Please Select Client");
                ddl_client.focus();
                return false;

            }

            var txt_receive_amount = document.getElementById('<%=txt_receive_amount.ClientID %>');
            if (txt_receive_amount.value == "0") {
                alert("Please Enter Receiving Amount");
                txt_receive_amount.focus();
                return false;

            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        //change md 
        function Req_bill_validation() {
            var ddl_billing_type = document.getElementById('<%=ddl_billing_type.ClientID %>');
     var Selected_ddl_billing_type = ddl_billing_type.options[ddl_billing_type.selectedIndex].text;

     var ddl_conveyance_type = document.getElementById('<%=ddl_conveyance_type.ClientID %>');
     var Selected_ddl_conveyance_type = ddl_conveyance_type.options[ddl_conveyance_type.selectedIndex].text;

     if (Selected_ddl_billing_type == "Convenyance Billing") {
         if (Selected_ddl_conveyance_type == "Select") {
             alert("Please Select Convenyance Bill Type");
             ddl_conveyance_type.focus();
             return false;
         }
     }
     var month_year = document.getElementById('<%=txt_bill_date.ClientID %>');
     //month year
     if (month_year.value == "") {
         alert("Please Select month & year.");
         month_year.focus();
         return false;
     }
     var ddl_client1 = document.getElementById('<%=ddl_bill_client.ClientID %>');
     var Selected_ddl_client1 = ddl_client1.options[ddl_client1.selectedIndex].text;
     if (Selected_ddl_client1 == "Select") {
         alert("Please Select Client Name");
         ddl_client1.focus();
         return false;

     }
     var ddl_invoice_type = document.getElementById('<%=ddl_invoice_type.ClientID %>');
            var Selected_ddl_invoice_type = ddl_invoice_type.options[ddl_invoice_type.selectedIndex].text;



            if (Selected_ddl_invoice_type == "Select") {
                alert("Please Select Invoice Type");
                ddl_invoice_type.focus();
                return false;

            }
            if (Selected_ddl_invoice_type == "UNCLUB") {
                var ddl_designation = document.getElementById('<%=ddl_designation.ClientID %>');
                var Selected_ddl_designation = ddl_designation.options[ddl_designation.selectedIndex].text;
                if (Selected_ddl_designation == "Select") {
                    alert("Please Select Designation");
                    ddl_designation.focus();
                    return false;
                }
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function Req_pmt_validation() {

            var month_year = document.getElementById('<%=txt_pmt_date.ClientID %>');
            //month year
            if (month_year.value == "") {
                alert("Please Select month & year.");
                month_year.focus();
                return false;
            }
            var ddl_client1 = document.getElementById('<%=ddl_pmt_client.ClientID %>');
            var Selected_ddl_client1 = ddl_client1.options[ddl_client1.selectedIndex].text;
            if (Selected_ddl_client1 == "Select") {
                alert("Please Select Client Name");
                ddl_client1.focus();
                return false;

            }

            //$.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function Req_pmt_reject() {

            var month_year = document.getElementById('<%=txt_pmt_date.ClientID %>');
            var reject_reason = document.getElementById('<%=txt_pmt_reject_reason.ClientID %>');
            //month year
            if (month_year.value == "") {
                alert("Please Select month & year.");
                month_year.focus();
                return false;
            }
            var ddl_client1 = document.getElementById('<%=ddl_pmt_client.ClientID %>');
             var Selected_ddl_client1 = ddl_client1.options[ddl_client1.selectedIndex].text;
             if (Selected_ddl_client1 == "Select") {
                 alert("Please Select Client Name");
                 ddl_client1.focus();
                 return false;

             }
             if (reject_reason.value == "") {
                 alert("Please enter reject reason.");
                 reject_reason.focus();
                 return false;
             }

             var bool = confirm('Are you sure you want to Reject?');

             if (bool == true) {
                 $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

             }
             else {
                 return false;
             }
             return bool;
         }

         function Req_arrears_reject() {

             var reject_reason = document.getElementById('<%=txt_arrears_reason.ClientID %>');

            if (reject_reason.value == "") {
                alert("Please enter reject reason.");
                reject_reason.focus();
                return false;
            }

            var bool = confirm('Are you sure you want to Reject Arrears Bill Request?');

            if (bool == true) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }

            else {
                return false;
            }

        }
        function confirm1() {

            var r = confirm("Are you sure You want to  Approve Arrears Request?");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }
        function confirm_approve() {

            var r = confirm("Are you sure You want to  Approve?");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }

        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });
        function conveyance_bill_check() {
            var ddl_billing_type = document.getElementById('<%=ddl_billing_type.ClientID %>');
             var Selected_ddl_billing_type = ddl_billing_type.options[ddl_billing_type.selectedIndex].text;
             if (Selected_ddl_billing_type == "Convenyance Billing") {
                 $(".conveyance").show();
             }

             else { $(".conveyance").hide(); }
         }
         function vendor_bill_check() {
             var ddl_vendor_type = document.getElementById('<%=ddl_vendor_type.ClientID %>');
             var Selected_ddl_vendor_type = ddl_vendor_type.options[ddl_vendor_type.selectedIndex].text;
             if (Selected_ddl_vendor_type == "Purchase Invoice") {
                 $(".invoiceno").show();
             }
             else { $(".invoiceno").hide(); }

             if (Selected_ddl_vendor_type == "Purchase Order") {
                 $(".pono").show();
             }
             else { $(".pono").hide(); }

         }
         function support_format1() {

             var ddl_bill_client = document.getElementById('<%=ddl_bill_client.ClientID %>');

             var Selected_ddl_client = ddl_bill_client.options[ddl_bill_client.selectedIndex].text;
            if (Selected_ddl_client == "Dewan Housing Finance Corporation Limited") {
                $(".region").show();
            }
            
           else if (Selected_ddl_client == "BAJAJ ALLIANZ GENERAL INSURANCE COMPANY LTD.") {
                $(".region").show();
            }

            else { $(".region").hide(); }
        }

    </script>

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>


        <asp:Panel ID="Panel4" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Month End Process</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />


            <%--    vikas  --%>

            <div class="container-fluid">
                <div id="tabs" style="background: beige;">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li><a data-toggle="tab" href="#billing">Billing Details</a></li>
                        <li><a id="A3" data-toggle="tab" href="#payment_ap">Payment Details</a></li>
                        <li><a id="A4" data-toggle="tab" href="#home">Payment</a></li>
                        <li><a id="A1" data-toggle="tab" href="#menu1" runat="server">Month End</a></li>
                        <li><a id="A2" data-toggle="tab" href="#menu2" runat="server">MiniBank</a></li>
                        <li><a id="A5" data-toggle="tab" href="#arrears_tab" runat="server">Arrears Bill Request</a></li>
                        <li><a id="A6" data-toggle="tab" href="#vendor_tab" runat="server">Vendor Details</a></li>
                    </ul>
                    <div id="home">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="container">
                                    <div class="row" id="client_row">

                                        <%-- <div class="col-sm-2 col-xs-12">
                                        Client:<span style="color:red">*</span>
                                          
                                        <asp:DropDownList ID="ddl_client" runat="server" class="form-control">
                                                                                     
                                        </asp:DropDownList>
                                        </div> 
                           <div class="col-sm-2 col-xs-12">
                                        State:<span style="color:red">*</span>
                                         
                                        <asp:DropDownList ID="ddl_state" runat="server" 
                                            class="form-control"   onkeypress="return AllowAlphabet(event)"
                                            MaxLength="20">
                                           
                                        </asp:DropDownList>
                                    </div>--%>
                                        <div class="col-sm-4 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Date :</b>
                                    <span style="color: red">*</span>
                                            <asp:TextBox ID="txt_date" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="margin-top: 16px;">
                                            <asp:Button ID="btn_submit" runat="server" Text="Submit" class="btn btn-primary" OnClick="btn_submit_Click" OnClientClick="return validation()" />
                                        </div>
                                        <div class="col-sm-1 col-xs-22" style="margin-top: 16px;">
                                            <asp:Button ID="btn_recived_amt" runat="server" Text="Receiving Amount" Visible="false" class="btn btn-danger" OnClientClick="return validation()" />
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Panel ID="panel2" runat="server" CssClass="grid-view">
                                        <asp:GridView ID="gv_payment" class="table" DataKeyNames="Id" runat="server" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" OnSelectedIndexChanged="gv_payment_SelectedIndexChanged" OnRowDataBound="gv_payment_RowDataBound" OnPreRender="gv_payment_PreRender">
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <Columns></Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <br />
                                    <asp:Panel runat="server" ID="receving_amount" class="panel panel-primary">
                                        <br />
                                        <div class="container">
                                            <div class="row">
                                                <div class="col-sm-2 col-xs-12" style="margin-top: 16px;"></div>
                                                <div class="col-sm-2 col-xs-12">
                                                    Invoice No :
                                    
                                        <asp:TextBox ID="txt_invoice_no" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    Client:<span style="color: red">*</span>

                                                    <asp:DropDownList ID="ddl_client" runat="server" class="form-control" ReadOnly="true" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    State:<span style="color: red">*</span>

                                                    <asp:DropDownList ID="ddl_state" runat="server" class="form-control" ReadOnly="true" onkeypress="return AllowAlphabet(event)" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    Branch:<span style="color: red">*</span>

                                                    <asp:DropDownList ID="ddl_branch" runat="server" class="form-control" ReadOnly="true" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true" onkeypress="return AllowAlphabet(event)" MaxLength="10">
                                                    </asp:DropDownList>
                                                </div>



                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-2 col-xs-12" style="margin-top: 16px;"></div>
                                                <div class="col-sm-2 col-xs-12">
                                                    Billing Amount :
                                    
                                        <asp:TextBox ID="txt_bill_amount" Text="0" runat="server" CssClass="form-control" ReadOnly="true" onchange="return final_balance_amount();"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-2 col-xs-12">
                                                    Receiving Amount :<span style="color: red">*</span>

                                                    <asp:TextBox ID="txt_receive_amount" runat="server" Text="0" CssClass="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    Received Date :
                                    
                                        <asp:TextBox ID="txt_receive_date" runat="server" CssClass="form-control date-picker1 "></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    Taxable Amount :
                                        <asp:TextBox ID="txt_taxable_amount" runat="server" CssClass="form-control" onkeypress="return isNumber(event)">0</asp:TextBox>
                                                </div>

                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-2 col-xs-12" style="margin-top: 16px;"></div>
                                                <div class="col-sm-2 col-xs-12">
                                                    GST :
                                        <asp:TextBox ID="txt_gst" runat="server" CssClass="form-control" onkeypress="return isNumber(event)">0</asp:TextBox>
                                                </div>

                                                <div class="col-sm-2 col-xs-12">
                                                    Adjustment (+/-) :
                                        <asp:DropDownList ID="ddl_adjustment" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">+</asp:ListItem>
                                            <asp:ListItem Value="2">-</asp:ListItem>
                                        </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    Grand Total :
                                        <asp:TextBox ID="txt_grand_total" runat="server" CssClass="form-control" onkeypress="return isNumber(event)">0</asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <br />
                                        <div class="container" style="width: 90%">

                                            <asp:Panel ID="panel_payment_detail" runat="server" CssClass="grid">
                                                <asp:GridView ID="gv_payment_detail" class="table" runat="server" BackColor="White"
                                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%">
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <Columns>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>

                                        <br />
                                        <div class="row text-center">

                                            <asp:Button ID="btn_save" runat="server" Text="Save" class="btn btn-primary" OnClick="btn_save_Click" OnClientClick="return Required_validation();" />
                                            <asp:Button ID="btn_close" runat="server" Text="Close" OnClick="btn_close_Click" class="btn btn-danger" />

                                        </div>
                                        <br />
                                        <br />


                                    </asp:Panel>
                                    <br />


                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="menu1">
                        <br />
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">
                                    <%--       vikas--%>
                                    <div class="col-sm-1 col-xs-12">
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Month/Year :</b><span class="text-red" style="color: red">*</span></>
                    <asp:TextBox ID="txt_month" runat="server" class="form-control date-picker text_box"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Client Name :</b> <span class="text-red" style="color: red">*</span>
                                        <asp:DropDownList ID="ddl_client1" DataValueField="client_code" DataTextField="client_name" AutoPostBack="true" OnSelectedIndexChanged="ddl_client1_SelectedIndexChanged" runat="server" CssClass="form-control" onkeypresss="return chk_month()">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> State Name :</b>
                        <asp:DropDownList ID="ddl_statename" runat="server" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_statename_SelectedIndexChanged">
                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Branch Name :</b>
                        <asp:DropDownList ID="ddl_unitcode" runat="server" class="form-control text_box">
                        </asp:DropDownList>
                                    </div>
                                    <%--       vikas--%>

                                    <div class="col-sm-3 col-xs-12">
                                        <br />
                                        <asp:Button ID="btn_month_end" runat="server" class="btn btn-large" Width="30%" OnClick="btn_month_end_Click" OnClientClick="return Req_validation();" Text="Month End" />
                                        <asp:Button ID="btn_month_start" runat="server" class="btn btn-large" Width="35%" OnClick="btn_month_start_Click" OnClientClick="return Req_validation();" Text="Month Start" />
                                        <asp:Button ID="bntclose" runat="server" class="btn btn-danger" OnClick="bntclose_Click" Text="Close" />
                                    </div>
                                </div>
                                <br />
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-sm-9 col-xs-12"></div>
                                        <div class="col-sm-3 col-xs-12">
                                            <asp:Panel ID="panel10" runat="server" Visible="false" Width="90%">

                                                <table border="1" class="table table-striped" style="border: 1px solid antiquewhite; text-align: center;">
                                                    <tr>
                                                        <th><a data-toggle="modal" data-target="#monthend_count"><b style="color: red"><%=monthend_count%></b>Remaining Month End</a></th>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                        <div id="monthend_count" class="modal fade" role="dialog">
                                            <div class="modal-dialog">

                                                <!-- Modal content-->
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                        <h4 class="modal-title">Remaining Month End</h4>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="row">
                                                            <div class="col-sm-12" style="padding-left: 1%;">
                                                                <asp:Panel ID="Panel11" runat="server" CssClass="grid-view">
                                                                    <asp:GridView ID="gv_monthend_count" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1" Width="100%">
                                                                        <Columns>

                                                                            <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                                            <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name " />

                                                                        </Columns>


                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </div>

                                                        </div>

                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="container" style="width: 100%">
                                        <asp:GridView ID="gv_fullmonthot" runat="server" ForeColor="#333333" class="table" Width="100%" OnPreRender="gv_fullmonthot_PreRender">
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
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="menu2">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="container">
                                    <asp:Panel ID="panel5" runat="server" CssClass="grid-view">
                                        <asp:GridView ID="gv_minibank" class="table" runat="server" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" OnSelectedIndexChanged="gv_minibank_SelectedIndexChanged" OnRowDataBound="gv_minibank_RowDataBound" OnPreRender="gv_minibank_PreRender">
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                        </asp:GridView>
                                    </asp:Panel>
                                    <br />
                                    <asp:Panel runat="server" ID="Panel3" class="panel panel-primary">
                                        <br />
                                        <div class="container">
                                            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-sm-2 col-xs-12" style="margin-top: 16px;"></div>
                                                        <div class="col-sm-2 col-xs-12">
                                                            <b>Company Name :</b>
                                    
                                        <asp:TextBox ID="txt_comp_name" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12">
                                                           <b> Bank Name :</b>
                             <asp:DropDownList ID="ddl_bank_name" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_bank_name_SelectedIndexChanged" MaxLength="10">
                             </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12">
                                                         <b>   A/C Number :</b>
                             <asp:DropDownList ID="ddl_comp_ac_number" runat="server" class="form-control" AutoPostBack="true" MaxLength="10">
                             </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12">
                                                           <b> A/C Balanced :</b>
                           <asp:TextBox ID="txt_ac_balanced" runat="server" CssClass="form-control">0</asp:TextBox>
                                                        </div>

                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-sm-2 col-xs-12" style="margin-top: 16px;"></div>
                                                        <div class="col-sm-2 col-xs-12">
                                                           <b> Client Name:</b>

                            <asp:DropDownList ID="ddl_minibank_client" runat="server" class="form-control" OnSelectedIndexChanged="ddl_minibank_client_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12">
                                                            <b>Client Bank Name :</b>
                             <asp:DropDownList ID="ddl_client_bank" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_client_bank_SelectedIndexChanged">
                             </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12">
                                                            <b>Client A/C Number :</b>
                           <asp:DropDownList ID="ddl_client_ac_number" runat="server" class="form-control">
                           </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12">
                                                            <b>Payment Against :</b>
                            <asp:DropDownList ID="ddl_payment_type" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Advance</asp:ListItem>
                                <asp:ListItem Value="2">Invoice</asp:ListItem>
                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <br />
                                                    <div class="row">
                                                        <div class="col-sm-2 col-xs-12" style="margin-top: 16px;"></div>
                                                        <div class="col-sm-2 col-xs-12">
                                                          <b>  Select Month :</b>
                                <asp:TextBox ID="txt_minibank_month" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12">
                                                           <b> Amount :</b>
                          <asp:TextBox ID="txt_minibank_amount" runat="server" CssClass="form-control">0</asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12">
                                                           <b> Received Date :</b>
                                        <asp:TextBox ID="txt_minibank_received_date" runat="server" CssClass="form-control date-picker1 "></asp:TextBox>
                                                        </div>

                                                        <br />
                                                        <asp:Button ID="btn_minibank_submit" runat="server" class="btn btn-primary" OnClick="btn_minibank_submit_Click" Text="Submit" />

                                                    </div>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <br />
                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="billing">
                        <br />
                        <asp:Panel ID="bill_panel7" runat="server">

                            <div class="row">

                                <div class="col-sm-2 col-xs-12">
                                  <b>  Month/Year :</b><span class="text-red" style="color: red">*</span></>
                    <asp:TextBox ID="txt_bill_date" runat="server" class="form-control date-picker text_box"></asp:TextBox>
                                </div>


                                <%--       vikas--%>
                                <div class="col-sm-2 col-xs-12">
                                  <b>  Select Billing :</b> <span class="text-red" style="color: red">*</span>
                                    <asp:DropDownList ID="ddl_billing_type" AutoPostBack="true" OnSelectedIndexChanged="ddl_billing_type_SelectedIndexChanged" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">Manpower Billing</asp:ListItem>
                                        <asp:ListItem Value="1">Material Billing</asp:ListItem>
                                        <asp:ListItem Value="2">Convenyance Billing</asp:ListItem>
                                        <asp:ListItem Value="3">Deep Cleen Billing</asp:ListItem>
                                        <asp:ListItem Value="4">Pest Control Billing</asp:ListItem>
                                        <asp:ListItem Value="5">Machine Rental Billing</asp:ListItem>
                                        <asp:ListItem Value="6">From Date To Date</asp:ListItem>
                                        <asp:ListItem Value="7">Arrears Bill</asp:ListItem>
                                        <asp:ListItem Value="8">Manual</asp:ListItem>
                                         <asp:ListItem Value="9">credit debit</asp:ListItem>
                                        <asp:ListItem Value="10">R&M Service</asp:ListItem>
                                        <asp:ListItem Value="11">Administrative Expense</asp:ListItem>

                                    </asp:DropDownList>
                                </div>

                                 <%-- komal changes 12-06-2020--%>
                                    <asp:Panel ID="panel_manual_invoice_type" runat="server">

                                  <div class="col-sm-2 col-xs-12">
                                   Manual Invoice Type : <span class="text-red" style="color: red">*</span>
                                    <asp:DropDownList ID="ddl_manual_invoice_type"  runat="server" OnSelectedIndexChanged="ddl_manual_invoice_type_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                                     <asp:ListItem Value="2">Select</asp:ListItem>
                                            <asp:ListItem Value="0">Clientwise</asp:ListItem>
                                        <asp:ListItem Value="1">Other</asp:ListItem>
                                       

                                    </asp:DropDownList>
                                </div>
  </asp:Panel>
                                <div class="col-sm-2 col-xs-12 conveyance"  runat="server" id="convence">
                                    Conveyance Billing : <span class="text-red" style="color: red">*</span>
                                    <asp:DropDownList ID="ddl_conveyance_type" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Employee Conveyance Billing</asp:ListItem>
                                        <asp:ListItem Value="2">Driver Convenyance Billing</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <%-- <div class="col-sm-2 col-xs-12">
                                    Month/Year :<span class="text-red" style="color: red">*</span></>
                    <asp:TextBox ID="txt_bill_date" runat="server" class="form-control date-picker text_box" AutoPostBack="true"></asp:TextBox>
                                </div>--%>
                                <asp:Panel ID="client_state_panel" runat="server">


                                     <%-- komal changes 12-06-2020--%>
                                    <asp:Panel ID="panel_client_state" runat="server">

                                    <div class="col-sm-2 col-xs-12">
                                       <b> Client Name :</b> <span class="text-red" style="color: red">*</span>
                                        <asp:DropDownList ID="ddl_bill_client" DataValueField="client_code" DataTextField="client_name" AutoPostBack="true" OnSelectedIndexChanged="ddl_bill_client_SelectedIndexChanged" runat="server" CssClass="form-control" onchange="support_format1()">
                                            <%--<asp:ListItem Value="0">Select</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 col-xs-12 region" style="display: none">
                                       <b> Region :</b>
                            <asp:DropDownList ID="ddlregion" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_region_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                                    </div>

                                          <%-- komal changes 18-06-2020   cr-dn --%>

                                          <asp:Panel ID="panel_state_cr_dn" runat="server">
                                    <div class="col-sm-2 col-xs-12">
                                        <b>State Name :</b>
                        <asp:DropDownList ID="ddl_bill_state" runat="server" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_bill_state_SelectedIndexChanged">
                            <asp:ListItem Value="ALL">ALL</asp:ListItem>
                        </asp:DropDownList>
                                    </div>

                                                </asp:Panel>

                                         <%-- komal changes 18-06-2020  cr-dn--%>

                                         </asp:Panel>

                                   <%-- komal changes 12-06-2020--%>

                                    <asp:Panel ID="panel_manual_other_state" runat="server">
                                     <div class="col-sm-2 col-xs-12">
                           <b> State Name:</b>
                            <asp:DropDownList ID="ddl_state_name_other" runat="server" CssClass="form-control" Width="100%" OnSelectedIndexChanged="ddl_state_name_other_SelectedIndexChanged" AutoPostBack="true" >
                         </asp:DropDownList>
                        </div>

                                         <div class="col-sm-2 col-xs-12">
                           <b> Bill to Party :</b>
                                <asp:TextBox ID="txt_bill_party" runat="server"  class="form-control" onKeyPress="return "></asp:TextBox>
                        </div>


                        </asp:Panel>

                                     <%-- komal changes 12-06-2020--%>
                                       <asp:Panel ID="panel_branch_name" runat="server">

                                    <div class="col-sm-2 col-xs-12">
                                       <b> Branch Name :</b>
                        <asp:DropDownList ID="ddl_bill_unit" runat="server" class="form-control text_box" OnSelectedIndexChanged="ddl_bill_unit_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="ALL">ALL</asp:ListItem>
                        </asp:DropDownList>
                                    </div>

                                     </asp:Panel>

                                     <asp:Panel ID="panel_note_number" runat="server">
                                    <div class=" col-sm-1 col-xs-12" style="width: 10%;">
                                        <b>Note No Type :</b><span class="text-red">*</span>
                                        <asp:DropDownList ID="ddl_credit_debit_note" runat="server" OnSelectedIndexChanged="ddl_credit_debit_note_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="select">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Credit Note Number</asp:ListItem>
                                            <asp:ListItem Value="2">Debit Note Number</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                          <div class="col-sm-2 col-xs-12">
                                       Credit/Debit No : <span class="text-red" style="color: red">*</span>
                                        <asp:DropDownList ID="ddl_credit_debit_no"  runat="server" CssClass="form-control" >
                                            <%--<asp:ListItem Value="0">Select</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>

                                    </asp:Panel>

                                    <div class=" col-sm-1 col-xs-12" style="width: 10%;">
                                        <b>Invoice type :</b><span class="text-red">*</span>
                                        <asp:DropDownList ID="ddl_invoice_type" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_invoice_type_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="select">Select</asp:ListItem>
                                            <asp:ListItem Value="1">CLUB</asp:ListItem>
                                            <asp:ListItem Value="2">UNCLUB</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class=" col-sm-1 col-xs-12" style="width: 10%;">
                                        <asp:Panel ID="desigpanel" runat="server">
                                           <b> Designation :</b><span class="text-red">*</span>
                                            <asp:DropDownList ID="ddl_designation" runat="server" CssClass="form-control" />
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>
                                <%--       vikas--%>
                            </div>
                            <br />
                            <div class="row text-center">


                                <br />
                                <asp:Button ID="btn_bill_view" runat="server" class="btn btn-large" OnClientClick="return  Req_bill_validation();" OnClick="btn_bill_view_Click" Text="VIEW FINAL BILL" />
                                <asp:Button ID="btn_bill_close" runat="server" class="btn btn-danger" OnClick="bntclose_Click" Text="Close" />


                            </div>
                            <br />
                            <asp:Panel ID="Panel6" runat="server">
                                <div class="container" style="width: 100%">
                                    <asp:GridView ID="gv_billing_details" runat="server" ForeColor="#333333" class="table" Width="100%" OnPreRender="gv_billing_details_PreRender">
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
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="billing_panel" runat="server">
                                <div class="row text-center">

                                    <br />
                                    <asp:Button runat="server" ID="btn_list_bill_upload" CssClass="btn btn-primary" OnClick="btn_list_bill_upload_Click" Text="Get List Bill Upload" />
                                    <asp:Button ID="btn_attendance" runat="server" class="btn btn-primary" OnClick="btn_attendance_Click" Text="Attendance Copy" />
                                    <asp:Button ID="btn_finance" runat="server" class="btn btn-primary" OnClick="btn_finance_Click" Text="Finance Copy" />
                                    <asp:Button ID="btn_invoice" runat="server" class="btn btn-primary" OnClick="btn_invoice_Click" Text="Invoice Copy" />
                                    <asp:Button ID="btn_edit" runat="server" class="btn btn-primary" OnClick="btn_edit_Click" OnClientClick="return confirm('Are you sure You want to  Give Edit Permission?')" Text="EDIT BILL ENABLE" />
                                    <asp:Button ID="btn_approve" runat="server" class="btn btn-large" OnClick="btn_approve_Click" OnClientClick="return confirm('Are you sure You want to  Approve Final Billing?')" Text="APPROVE" />
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="Panel12" runat="server" class="grid-view" Style="overflow-x: auto;">
                                <div class="container" style="width: 100%">
                                    <asp:GridView ID="gv_bill_list_upload" runat="server" AutoGenerateColumns="false" ForeColor="#333333" class="table" Width="100%" OnRowDataBound="gv_bill_list_upload_RowDataBound">
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
                                                <ItemStyle Width="20px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                            <asp:BoundField DataField="unit_code" HeaderText="Branch Name" SortExpression="unit_code" />
                                            <asp:BoundField DataField="Month" HeaderText="Month" SortExpression="Month" />
                                            <asp:BoundField DataField="year" HeaderText="Year" SortExpression="year" />
                                            <asp:BoundField DataField="bill_type" HeaderText="Bill Type" SortExpression="bill_type" />
                                            <asp:BoundField DataField="invoice_number" HeaderText="Invoice No." SortExpression="invoice_number" />
                                            <asp:BoundField DataField="dispatch_date" HeaderText="Dispatch Date" SortExpression="dispatch_date" />
                                            <asp:BoundField DataField="receiving_date" HeaderText="Receiving Date" SortExpression="receiving_date" />
                                            <asp:BoundField DataField="mail_send_date" HeaderText="Mail Send Date" SortExpression="mail_send_date" />
                                            <asp:BoundField DataField="current_status" HeaderText="Current Status" SortExpression="current_status" />
                                            <asp:TemplateField>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="ink_edit" runat="server" ControlStyle-CssClass="btn btn-primary" OnClick="ink_edit_Click" OnClientClick="return confirm('Are you sure You want to  Give Edit Permission?')" Text="Edit" Style="color: white"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                    <div id="payment_ap">
                        <br />
                        <asp:Panel ID="payment_approve" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <%--       vikas--%>
                                        <div class="col-sm-1 col-xs-12">
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Month/Year :</b><span class="text-red" style="color: red">*</span></>
                    <asp:TextBox ID="txt_pmt_date" runat="server" class="form-control date-picker text_box" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <asp:Panel ID="Panel8" runat="server">
                                            <div class="col-sm-2 col-xs-12">
                                               <b> Client Name : </b><span class="text-red" style="color: red">*</span>
                                                <asp:DropDownList ID="ddl_pmt_client" DataValueField="client_code" DataTextField="client_name" AutoPostBack="true" OnSelectedIndexChanged="ddl_pmt_client_SelectedIndexChanged" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <b>State Name :</b>
                        <asp:DropDownList ID="ddl_pmt_state" runat="server" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_pmt_state_SelectedIndexChanged">
                        </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                               <b> Branch Name :</b>
                        <asp:DropDownList ID="ddl_pmt_unit" runat="server" class="form-control text_box" OnSelectedIndexChanged="ddl_pmt_unit_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                              <b>  Reject Reason :</b>
                    <asp:TextBox ID="txt_pmt_reject_reason" runat="server" Width="200px" Height="50px" class="form-control"></asp:TextBox>
                                            </div>
                                        </asp:Panel>


                                    </div>
                                    <br />

                                    <div class="row text-center">


                                        <br />
                                        <asp:Button ID="btn_pmt_view" runat="server" Enabled="True" class="btn btn-large" OnClientClick="return  Req_pmt_validation();" OnClick="btn_pmt_view_Click" Text="View Final Payment" />
                                        <asp:Button ID="btn_pmt_approve" runat="server" class="btn btn-primary" OnClientClick="return  confirm_approve();" OnClick="btn_pmt_approve_Click" Text="Approve" />
                                        <asp:Button ID="btn_pmt_reject" runat="server" class="btn btn-primary" OnClientClick="return   Req_pmt_reject();" OnClick="btn_pmt_reject_Click" Text="Reject" />
                                        <asp:Button ID="btn_pmt_close" runat="server" class="btn btn-danger" OnClick="bntclose_Click" Text="Close" />


                                    </div>

                                </ContentTemplate>

                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btn_pmt_view" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                    <div id="arrears_tab">
                        <br />
                        <asp:Panel ID="Panel7" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <%--       vikas--%>
                                        <div class="col-sm-1 col-xs-12">
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Month/Year :</b><span class="text-red" style="color: red">*</span></>
                    <asp:TextBox ID="txt_arrears_date" runat="server" class="form-control date-picker text_box" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <asp:Panel ID="Panel9" runat="server">
                                            <div class="col-sm-2 col-xs-12">
                                                <b>Client Name :</b> <span class="text-red" style="color: red">*</span>
                                                <asp:DropDownList ID="ddl_arrears_client" runat="server" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_arrears_Client_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <b>State Name :</b>
                        <asp:DropDownList ID="ddl_arrears_state" runat="server" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_arrears_state_SelectedIndexChanged">
                        </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <b>Branch Name :</b>
                        <asp:DropDownList ID="ddl_arrears_unit" runat="server" class="form-control text_box" OnSelectedIndexChanged="ddl_arrears_unit_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <b>Reject Reason :</b>
                    <asp:TextBox ID="txt_arrears_reason" runat="server" Width="200px" Height="50px" class="form-control"></asp:TextBox>
                                            </div>
                                        </asp:Panel>


                                    </div>
                                    <br />
                                    <div class="container">
                                        <asp:Panel runat="server" CssClass="grid-view">
                                            <asp:GridView ID="gv_arrears_gridview" class="table" AutoGenerateColumns="False" runat="server"
                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="gv_arrears_gridview_RowDataBound"
                                                CellPadding="3" meta:resourcekey="SearchGridViewResource1" Width="100%" HorizontalAlign="Center" OnPreRender="gv_arrears_gridview_PreRender">

                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                                <RowStyle ForeColor="#000066" BackColor="#ffffff" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                <Columns>

                                                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
                                                    <asp:BoundField DataField="client_name" HeaderText="CLIENT NAME" SortExpression="client_name" />
                                                    <asp:BoundField DataField="state_name" HeaderText="STATE NAME" SortExpression="state_name" />
                                                    <asp:BoundField DataField="unit_name" HeaderText="UNIT NAME" SortExpression="unit_name" />
                                                    <asp:BoundField DataField="monthyear" HeaderText="MONTH/YEAR" SortExpression="monthyear" />

                                                    <asp:BoundField DataField="status" HeaderText="STATUS" SortExpression="status" />
                                                    <asp:TemplateField HeaderText="APPROVE ">
                                                        <ItemStyle Width="20px" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnk_approve" runat="server" CausesValidation="false" Text="Approve" CommandArgument='<%# Eval("Id")%>' OnCommand="lnk_approve_Command" CssClass="btn btn-primary" Style="color: white;" OnClientClick="return confirm1();"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="REJECT ">
                                                        <ItemStyle Width="20px" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnk_reject" runat="server" CausesValidation="false" Text="Reject" CommandArgument='<%#Eval("Id") %>' OnCommand="lnk_reject_Command" CssClass="btn btn-primary" Style="color: white;" OnClientClick="return Req_arrears_reject()"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>

                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <RowStyle ForeColor="#000066" BackColor="#A1DCF2" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                    <div class="row text-center">


                                        <br />

                                        <asp:Button ID="btn_arrears_close" runat="server" class="btn btn-danger" OnClick="bntclose_Click" Text="Close" />


                                    </div>

                                </ContentTemplate>

                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btn_pmt_view" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                    <div id="vendor_tab">
                        <div class="row">

                            <div class="col-sm-2 col-xs-12">
                               <b> Month/Year :</b><span class="text-red" style="color: red">*</span></>
                                   <asp:TextBox ID="txt_month_year" runat="server" class="form-control date-picker text_box" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>Vendor Type :</b> <span class="text-red" style="color: red">*</span>
                                <asp:DropDownList ID="ddl_vendor_type" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_vendor_type_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Purchase Invoice</asp:ListItem>
                                    <asp:ListItem Value="2">Purchase Order</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>Vendor Name :</b> <span class="text-red" style="color: red">*</span>
                                <asp:DropDownList ID="ddl_vendor_name" DataValueField="cust_code" DataTextField="cust_name" AutoPostBack="true" OnSelectedIndexChanged="ddl_vendor_name_SelectedIndexChanged" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12 invoiceno" style="display: none">
                                <b>Purchase Invoice No :</b> <span class="text-red" style="color: red">*</span>
                                <asp:DropDownList ID="ddl_pi_no" AutoPostBack="true" OnSelectedIndexChanged="ddl_pi_no_SelectedIndexChanged" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12 pono" style="display: none">
                                <b>Purchase Order No: </b><span class="text-red" style="color: red">*</span>
                                <asp:DropDownList ID="ddl_po_no" AutoPostBack="true" OnSelectedIndexChanged="ddl_po_no_SelectedIndexChanged" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>



                        <br />
                        <br />
                        <br />
                        <asp:Panel ID="invoice_no_panel" runat="server">
                            <div class="container" style="width: 765px">
                                <asp:GridView ID="gv_invoice_no" runat="server" ForeColor="#333333" class="table" Width="100%" OnPreRender="gv_invoice_no_PreRender">
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
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="po_no_panel" Visible="false" runat="server">
                            <div class="container" style="width: 765px">
                                <asp:GridView ID="gv_po_no" runat="server" ForeColor="#333333" class="table" Width="100%" OnPreRender="gv_po_no_PreRender">
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
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                        <div class="row text-center">
                            <asp:Button ID="btn_edit_vendor_invoice" runat="server" Visible="false" class="btn btn-primary" OnClick="btn_edit_vendor_invoice_Click"
                                OnClientClick="return confirm('Are you sure You want to  Give Edit Permission?')" Text="EDIT BILL ENABLE" />
                        </div>
                    </div>
                    <br />
                </div>

            </div>
            <br />
        </asp:Panel>

    </div>

</asp:Content>





