<%@ Page MaintainScrollPositionOnPostback="true" Title="Vendor Master" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VendorMaster.aspx.cs" Inherits="VendorMaster" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Vendor Master</title>
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
    <script src="Scripts/sweetalert.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" type="text/css" />
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

    <script type="text/javascript">
        $(function () {
            var table = $('#<%=VendorGridView.ClientID%>').DataTable(
                {
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
               .appendTo('#<%=VendorGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';
            $('#<%=btncancel.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=VendorGridView.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=txtbillstate.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_typevendor.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=txt_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=txt_city.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                //minDate: 0,
                yearRange: '2019',
                onSelect: function (selected) {
                    $(".date-picker2").datepicker("option", "minDate", selected)
                }
            });


            $(".date-picker2").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',

                onSelect: function (selected) {
                    $(".date-picker1").datepicker("option", "maxDate", selected)
                }
            });
            $(".date-picker1").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");



        });


    </script>

    <script>

        $(document).ready(function () {
            $("#add_new").click(function () {

                $(".main_table").each(function () {

                    var tds = '<tr>';
                    jQuery.each($('tr:last td', this), function () {
                        tds += '<td>' + $(this).html() + '</td>';
                    });
                    tds += '</tr>';
                    if ($('tbody', this).length > 0) {
                        $('tbody', this).append(tds);
                    } else {
                        $(this).append(tds);
                    }
                });
            });

            $('.main_table').delegate('.delete', 'click', function () {
                $(this).closest('tr').remove();
            })

        });


    </script>
    <style type="text/css">
        .tab-section {
            background-color: #fff;
        }

        .row {
            margin-right: -15px;
            margin-left: -15px;
        }


        .grid-view {
            height: auto;
            max-height: 400px;
            overflow-y: auto;
            overflow-x: hidden;
        }


        button, input, optgroup, select, textarea {
            margin: 0 0 0 0px;
            color: inherit;
        }

        body {
            font-size: 14px;
            font-weight: lighter;
        }

        .shadow {
            -moz-box-shadow: 3px 3px 5px 6px #ccc;
            -webkit-box-shadow: 3px 3px 5px 6px #ccc;
            box-shadow: 3px 3px 5px 6px #ccc;
            padding: 20px;
            padding-right: 25px;
            font-size: 10px;
            font-weight: lighter;
            font-family: Verdana;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {

            $('#<%=ddl_payment_bank.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

            // for wait gridview 03-04-2020 komal

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gridview_wait_master.ClientID%>').DataTable(
                              {
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
                                  ],
                                  fixedHeader: {
                                      header: true,
                                      footer: true
                                  }

                              });

             table.buttons().container()
                .appendTo('#<%=gridview_wait_master.ClientID%>_wrapper .col-sm-6:eq(0)');


          


            ////////////////////////
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_rate_master.ClientID%>').DataTable(
                       {
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
                           ],
                           fixedHeader: {
                               header: true,
                               footer: true
                           }

                       });

                table.buttons().container()
                  .appendTo('#<%=gv_rate_master.ClientID%>_wrapper .col-sm-6:eq(0)');

            /////// for rate master komal 03-04-2020

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_company_bank.ClientID%>').DataTable(
                       {
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
                           ],
                           fixedHeader: {
                               header: true,
                               footer: true
                           }

                       });

            table.buttons().container()
              .appendTo('#<%=gv_company_bank.ClientID%>_wrapper .col-sm-6:eq(0)');

           

            $(".js-example-basic-single").select2();

        }
        $(document).ready(function () {


            var event = null;
            isNumber(event);
            isNumber(event)
            var e = null;
            AllowAlphabet1(e);
            AllowAlphabet11(e);
        });

        function AllowAlphabet11(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry < '31'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }
        function isNumber(event) {
            if (null != event) {
                event = (event) ? event : window.event;

                var charCode = (event.which) ? event.which : event.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {

                    return false;
                }

            }
            return true;
        }

        function isNumbertotal(event) {
            if (null != event) {
                event = (event) ? event : window.event;

                var charCode = (event.which) ? event.which : event.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    if (charCode == 46) {
                        return true;
                    }
                    return false;
                }

            }
            return true;
        }

        function AllowAlphabet3(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || (keyEntry < '31') || (keyEntry < '8'))
                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }

        function isNumberfax(event) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 32 && (charCode < 48 || charCode > 57)) {
                    if (charCode == 46 || charCode == 45 || charCode == 40 || charCode == 41) {
                        return true;
                    }
                    return false;
                }

            }
            return true;
        }

        function AllowAlphabet(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || (keyEntry < '31'))
                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }


        function AllowAlphabet1(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry < '31'))

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

        function plus_validation() {
            var ddl_payment_bank = document.getElementById('<%=ddl_payment_bank.ClientID %>');
            var Selected_ddl_payment_bank = ddl_payment_bank.options[ddl_payment_bank.selectedIndex].text;

            if (Selected_ddl_payment_bank == "Select") {
                alert("Please Select Bank..!!");
                ddl_payment_bank.focus();
                return false;
            }
            var txtvendorname = document.getElementById('<%=txtvendorname.ClientID %>');
            if (txtvendorname.value == "") {
                alert("Please Enter Vendor Name");
                txtvendorname.focus();
                return false;
            }
            var administrative_upload = document.getElementById('<%=upload_sheet.ClientID %>');
            if (administrative_upload.value == "") {
                alert("Please Upload File");
                administrative_upload.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function Req_validation1() {
            var lst_simu = document.getElementById('<%=lst_simu.ClientID%>');
            var SelectedTextsalu = lst_simu.options[lst_simu.selectedIndex].text;
            var txtfirstname = document.getElementById('<%=txtfirstname.ClientID %>');
            var txtmobileno = document.getElementById('<%=txtmobileno.ClientID %>');

            
               

            if (SelectedTextsalu == "Select Salutation") {
                alert("Please Select salutation !!!");
                lst_simu.focus();
                return false;
            }
            if (txtfirstname.value == "") {
                alert("Please Enter First Name   !!!");
                txtfirstname.focus();
                return false;
            }
            //if (txtlastname.value == "") {
            //    alert("Please Enter Last Name   !!!");
            //    txtlastname.focus();
            //    return false;
            //}

            //if (txteaddress.value == "") {
            //    errmsg += "Please Enter Person Email Address 1\n";
            //    //alert("Please Enter Project Contact Person Name 2");
            //    txteaddress.focus();
            //    //return false;
            //}

            //else if (!email.test(txteaddress.value)) {
            //    errmsg1 += "Please Enter  valid email address\n";
            //    // alert('Please provide a valid email address');
            //    txteaddress.focus();
            //    //    return false;
            //}


            //if (errmsg != "") {
            //    alert(errmsg);
            //    return false;
            //}
            //if (errmsg1 != "") {
            //    alert(errmsg1);
            //    return false;
            //}
            //if (txtworkphonno.value == "") {
            //    alert("Please Enter Phone Number   !!!");
            //    txtworkphonno.focus();
            //    return false;
            //}
            if (txtmobileno.value == "") {
                alert("Please Enter Mobile Number   !!!");
                txtmobileno.focus();
                return false;
            }
            //if (txtdesignation1.value == "") {
            //    alert("Please Enter Designation  !!!");
            //    txtdesignation1.focus();
            //    return false;
            //}
            //if (txtdept.value == "") {
            //    alert("Please Enter Department  !!!");
            //    txtdept.focus();
            //    return false;
            //}


            return true;

        }

        function rate_tab() {


            var ddl_item_type = document.getElementById('<%=ddl_item_type.ClientID %>');
            var Selected_ddl_item_type = ddl_item_type.options[ddl_item_type.selectedIndex].text;
            if (Selected_ddl_item_type == "Select") {
                alert("Please Select Item Type... ");
                ddl_item_type.focus();
                return false;
            }


            var ddl_item_code = document.getElementById('<%=ddl_item_code.ClientID %>');
            var Selected_ddl_item_code = ddl_item_code.options[ddl_item_code.selectedIndex].text;
            if (Selected_ddl_item_code == "Select") {
                alert("Please Select Item Code... ");
                ddl_item_code.focus();
                return false;
            }

            var txt_itemsize = document.getElementById('<%=txt_itemsize.ClientID %>');

            if (txt_itemsize.value == "") {
                alert("Please Select Item Size ");
                txt_itemsize.focus();
                return false;
            }

            var txt_rate_mas = document.getElementById('<%=txt_rate_mas.ClientID %>');

            if (txt_rate_mas.value == "") {
                alert("Please Select Rate ");
                txt_rate_mas.focus();
                return false;
            }


            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function wait_tab() {

            var ddl_courier_type = document.getElementById('<%=ddl_courier_type.ClientID %>');
            var Selected_ddl_client = ddl_courier_type.options[ddl_courier_type.selectedIndex].text;
            if (Selected_ddl_client == "Select") {
                alert("Please Select Courier Type... ");
                ddl_courier_type.focus();
                return false;
            }

           



               var ddl_wait_type = document.getElementById('<%=ddl_wait_type.ClientID %>');
            var Selected_ddl_wait_type = ddl_wait_type.options[ddl_wait_type.selectedIndex].text;
            if (Selected_ddl_wait_type == "Select") {
                   alert("Please Select Weight... ");
                   ddl_wait_type.focus();
                   return false;
               }


            var ddl_from = document.getElementById('<%=ddl_from.ClientID %>');
            var Selected_ddl_from = ddl_from.options[ddl_from.selectedIndex].text;
            if (Selected_ddl_from == "Select") {
                alert("Please Select From Weight... ");
                ddl_from.focus();
                return false;
            }

            
            var ddl_to = document.getElementById('<%=ddl_to.ClientID %>');
            var Selected_ddl_to = ddl_to.options[ddl_to.selectedIndex].text;
            if (Selected_ddl_to == "Select") {
                alert("Please Select To Weight... ");
                ddl_to.focus();
                return false;
            }



            var ddl_from_km = document.getElementById('<%=ddl_from_km.ClientID %>');
            var Selected_ddl_from_km = ddl_from_km.options[ddl_from_km.selectedIndex].text;
            if (Selected_ddl_from_km == "Select") {
                alert("Please Select From Km... ");
                ddl_from_km.focus();
                return false;
            }


            var ddl_to_km = document.getElementById('<%=ddl_to_km.ClientID %>');
            var Selected_ddl_to_km = ddl_to_km.options[ddl_to_km.selectedIndex].text;
            if (Selected_ddl_to_km == "Select") {
                alert("Please Select To Km... ");
                ddl_to_km.focus();
                return false;
            }

            var txt_rate = document.getElementById('<%=txt_rate.ClientID %>');

            if (txt_rate.value == "") {
                alert("Please Select Rate ");
                txt_rate.focus();
                return false;
            }
              


            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }


        function copy_add() {


            var txtbillattention1 = document.getElementById('<%=txtbillattention.ClientID %>');
            var txtsattention1 = document.getElementById('<%=txtsattention.ClientID %>');
            // alert("hello");
            txtsattention1.value = txtbillattention1.value;

            var txtbilladdress1 = document.getElementById('<%=txtbilladdress.ClientID %>');
            var txtsaddress1 = document.getElementById('<%=txtsaddress.ClientID %>');

            txtsaddress1.value = txtbilladdress1.value;

            var txtbillzipcode1 = document.getElementById('<%=txtbillzipcode.ClientID %>');
            var txtszipcode1 = document.getElementById('<%=txtszipcode.ClientID %>');
            // alert("hello");
            txtszipcode1.value = txtbillzipcode1.value;

            var txtbillcountry1 = document.getElementById('<%=txtbillcountry.ClientID %>');
            var txtscountry1 = document.getElementById('<%=txtscountry.ClientID %>');
            // alert("hello");
            txtscountry1.value = txtbillcountry1.value;

            var txtbillfax1 = document.getElementById('<%=txtbillfax.ClientID %>');
            var txtsfax1 = document.getElementById('<%=txtsfax.ClientID %>');
            // alert("hello");
            txtsfax1.value = txtbillfax1.value;


            var txtbillstate1 = document.getElementById('<%=txtbillstate.ClientID %>');
            var SelectedText_copy = txtbillstate1.options[txtbillstate1.selectedIndex].text;
            var txtssstate1 = document.getElementById('<%=txtssstate.ClientID %>');
           var SelectedText_copy1 = txtssstate1.options[txtssstate1.selectedIndex].text;

           txtssstate1.value = txtbillstate1.value;
            // alert("hello");
           var txtbillcity = document.getElementById('<%=txtbillcity.ClientID %>');
           var SelectedText_copy3 = txtbillcity.options[txtbillcity.selectedIndex].text;
            // alert("heloocity");
           var txtscitycity = document.getElementById('<%=txtscity.ClientID %>');
            // alert("heloocityQ");
           var SelectedText_copy4 = txtscitycity.options[txtscitycity.selectedIndex].text;

           txtscitycity.value = txtbillcity.value;

           return true;
       }

       function openWindow() {
           window.open("html/VendorMaster.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
       }

       function R_validation() {
           var r = confirm("Are you Sure You Want to Delete Record");
           if (r == true) {
              // ($.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } }))
               return true;
           }
           else {
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
           var txtvendorname = document.getElementById('<%=txtvendorname.ClientID %>');
           var txt_v_type = document.getElementById('<%=txt_v_type.ClientID %>');
           var SelectedText_txt_v_type = txt_v_type.options[txt_v_type.selectedIndex].text;
           var txtvendoradd1 = document.getElementById('<%=txtvendoradd1.ClientID %>');
           var txt_c_1 = document.getElementById('<%=txt_c_1.ClientID %>');
           var txtvendorphone1 = document.getElementById('<%=txtvendorphone1.ClientID %>');
           var txt_email = document.getElementById('<%=txt_email.ClientID %>');
           var txt_start = document.getElementById('<%=txt_start.ClientID %>');
           var txt_end = document.getElementById('<%=txt_end.ClientID %>');
           var txt_gst = document.getElementById('<%=txt_gst.ClientID %>');
           var txtvendoradd2 = document.getElementById('<%=txtvendoradd2.ClientID %>');
           var txtbilladdress = document.getElementById('<%=txtbilladdress.ClientID %>');
           var txtbillstate = document.getElementById('<%=txtbillstate.ClientID %>');
           var txtbillcity = document.getElementById('<%=txtbillcity.ClientID %>');
           var txtbillcountry = document.getElementById('<%=txtbillcountry.ClientID %>');
           var txt_vendor_bank_acc_name = document.getElementById('<%=txt_vendor_bank_acc_name.ClientID %>');
           var txt_acc_no = document.getElementById('<%=txt_acc_no.ClientID %>');
           var txt_ifsc_code = document.getElementById('<%=txt_ifsc_code.ClientID %>');
           var txt_bank_name = document.getElementById('<%=txt_bank_name.ClientID %>');
           var txtvendortin = document.getElementById('<%=txtvendortin.ClientID %>');

           if (txtvendorname.value == "") {
               alert("Please Enter Vendor Name");
               txtvendorname.focus();
               return false;

           }
           var ddl_vendor_nation = document.getElementById('<%=ddl_vendor_nation.ClientID %>');
           var SelectedText_ddl_vendor_nation = ddl_vendor_nation.options[ddl_vendor_nation.selectedIndex].text;
           if (SelectedText_ddl_vendor_nation == "Select") {
               alert("Please Select Vendor Type (As per Nation)");
               ddl_vendor_nation.focus();
               return false;
           }

           if (SelectedText_txt_v_type == "Select") {
               alert("Please Select Vendor Type");
               txt_v_type.focus();
               return false;
           }
           if (txtvendoradd1.value == "") {
               alert("Please Select Vendor Address");
               txtvendoradd1.focus();
               return false;

           }
           if (txt_c_1.value == "") {
               alert("Please Select Atleast One Contact Person");
               txt_c_1.focus();
               return false;

           }
           if (txtvendorphone1.value == "") {
               alert("Please Select Atleast One Contact Number");
               txtvendorphone1.focus();
               return false;

           }
           if (txt_email.value == "") {
               alert("Please Enter Email Address");
               txt_email.focus();
               return false;

           }
           var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
           if (!filter.test(txt_email.value)) {
               alert('Please Enter valid email address');
               txt_email.focus;
               return false;
           }
           var ddl_vendor_type = document.getElementById('<%=ddl_vendor_type.ClientID %>');
           var SelectedText_ddl_vendor_type = ddl_vendor_type.options[ddl_vendor_type.selectedIndex].text;
           if (SelectedText_ddl_vendor_type == "Select") {
               alert("Please Select Vendor Categories");
               ddl_vendor_type.focus();
               return false;

           }
           var txt_credit_period = document.getElementById('<%=txt_credit_period.ClientID %>');

           if (txt_credit_period.value == "") {
               alert("Please Enter Credit Period (In Day) ");
               txt_credit_period.focus();
               return false;

           }

           if (txt_start.value == "") {
               alert("Please Select Aggrement Start Date");
               txt_start.focus();
               return false;

           }
           if (txt_end.value == "") {
               alert("Please Select Aggrement End Date");
               txt_end.focus();
               return false;

           }

           if (txt_gst.value == "") {
               alert("Please Enter GST");
               txt_gst.focus();
               return false;

           }

           if (txtvendoradd2.value == "") {
               alert("Please Enter Service Type");
               txtvendoradd2.focus();
               return false;

           }

           if (txtbillcountry.value == "") {
               alert("Please Enter Billing Address Country");
               txtbillcountry.focus();
               return false;

           }
           var SelectedText_txtbillstate = txtbillstate.options[txtbillstate.selectedIndex].text;

           if (SelectedText_txtbillstate == "Select") {
               alert("Please Select Billing Address State");
               txtbillstate.focus();
               return false;

           }
           var SelectedText_txtbillcity = txtbillcity.options[txtbillcity.selectedIndex].text;
           if (SelectedText_txtbillcity == "Select") {
               alert("Please Select Billing Address City");
               txtbillcity.focus();
               return false;

           }
           if (txtbilladdress.value == "") {
               alert("Please Enter Billing Address");
               txtbilladdress.focus();
               return false;

           }
           if (txt_bank_name.value == "") {
               alert("Please Enter Bank Name");
               txt_bank_name.focus();
               return false;

           }
           if (txt_vendor_bank_acc_name.value == "") {
               alert("Please Enter Vendor Bank Account Name");
               txt_vendor_bank_acc_name.focus();
               return false;

           }
           if (txt_acc_no.value == "") {
               alert("Please Enter Vendor Bank Account Number");
               txt_acc_no.focus();
               return false;

           }
           if (txt_ifsc_code.value == "") {
               alert("Please Enter Vendor Bank IFSC Code");
               txt_ifsc_code.focus();
               return false;

           }

           $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
           return true;

       }
       function R_validation() {

           var r = confirm("Are you Sure You Want to Delete Record");
           if (r == true) {

               $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

           }
           else {
               alert("Record not Available");
           }
           return r;
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
       function AllowAlphabet(e) {
           isIE = document.all ? 1 : 0
           keyEntry = !isIE ? e.which : event.keyCode;
           if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '32') || keyEntry == '45' || (keyEntry == '8') || (keyEntry == '9') || (keyEntry == '46'))
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
       $(function () {
           ChnagePlaces();
       });
       function ChnagePlaces() {
           var country = $("[id*=ddl_vendor_nation] option:selected").val();
           $('[id*=txt_v_type] option').each(function () {
               if (country != $(this).val() && $(this).val() != 0) {
                   $(this).hide();
               }
               else {
                   $(this).show();
               }
           });
           return false;
       }
       $(document).ready(function () {
           var st = $(this).find("input[id*='hidtab']").val();
           if (st == null)
               st = 0;
           $('[id$=tabs]').tabs({ selected: st });
       });

    </script>
    <style>
        .row {
            margin-right: -15px;
            margin-left: -15px;
        }

        .text-red {
            color: red;
        }
    </style>
    <div class="container-fluid">


        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-lg-11 col-md-11 col-sm-11 col-xs-12">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Vendor Master</b> </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-12 text-right">

                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>


            </div>
             <br />
             <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Vendor Master Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>

            <div class="panel-body">
                <div id="tabs" style="background: #f3f1fe; border: 1px solid #e2e2dd; padding:10px 10px 10px 10px; margin-bottom:25px; margin-top:20px; border-radius:10px">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li><a href="#menu3"><b>Vendor Info</b></a></li>
                        <li><a href="#home"><b>Tax Details</b></a></li>
                        <li><a href="#menu1"><b>Address Details</b></a></li>
                        <li><a href="#menu4"><b>Bank Details</b></a></li>
                        <li><a href="#menu2"><b>Contact Details</b></a></li>
                        <li><a href="#menu5"><b>State Details</b></a></li>
                        <li><a href="#home1"><b>Weight Master</b></a></li>
                        <li><a href="#home2"><b>Rate Master</b></a></li>
                          <li><a href="#home3"><b>Client Assign to Vendor</b></a></li>
                        <li><a href="#menu6"><b>Company Bank Details</b></a></li>
                    </ul>
                    <div id="menu3">
                        <br />
                        <div class="container-fluid" style="background: white; border-radius: 10px; border: 1px solid white">
                            <br />
                            <div class="row text-center ">
                                <div class="col-md-12">
                                    <b><u><h4>Vendor Information</h4></u></b>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                            </div>
                            
                            <div class="container">
                                <div class="row">
                                    <div class="row">
                                        <div class="col-sm-1 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Vendor Id :</b>
                                           <asp:TextBox ID="txtvendorid" runat="server" class="form-control text_box" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class=" col-sm-2 col-xs-12">
                                           <b> Vendor Name :</b><span class="text-red">*</span>
                                            <asp:TextBox ID="txtvendorname" runat="server" class="form-control" MaxLength="70"
                                                onKeyPress="return AllowAlphabet(event)"></asp:TextBox>
                                        </div>
                                        <div class=" col-sm-2 col-xs-12">
                                           <b> Vendor Type(As per Nation):</b><span class="text-red">*</span>


                                            <asp:DropDownList ID="ddl_vendor_nation" runat="server" class="form-control" onchange="return ChnagePlaces()">
                                                <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Indian</asp:ListItem>
                                                <asp:ListItem Value="2">International</asp:ListItem>



                                            </asp:DropDownList>
                                        </div>
                                        <div class=" col-sm-2 col-xs-12">
                                          <b>  Vendor Type :</b><span class="text-red">*</span>


                                            <asp:DropDownList ID="txt_v_type" runat="server" class="form-control">
                                                <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Private</asp:ListItem>
                                                <asp:ListItem Value="2">Public</asp:ListItem>
                                                <asp:ListItem Value="3">Company</asp:ListItem>
                                                <asp:ListItem Value="4">Firm</asp:ListItem>
                                                <asp:ListItem Value="5">Proprietorship</asp:ListItem>


                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-sm-2 col-xs-12">
                                           <b> Vendor Address :</b><span class="text-red">*</span>
                                            <asp:TextBox ID="txtvendoradd1" runat="server" class="form-control"
                                                MaxLength="100" onKeyPress="return  AllowAlphabet_address(event)"></asp:TextBox>
                                        </div>


                                        <%--  <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">

                                            <asp:Button ID="btnnew" runat="server" class="btn btn-primary" OnClick="btnnew_Click" Text="New" />
                                            <asp:Button ID="btnnewclose" runat="server" class="btn btn-danger" OnClick="btnnewclose_Click" Text="Close" />
                                        </div>--%>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-1 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Contact Person 1 :</b><span class="text-red">*</span>
                                            <asp:TextBox ID="txt_c_1" runat="server" class="form-control" MaxLength="100" onKeyPress="return  AllowAlphabet(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Contact Person Mob No.1 :</b><span class="text-red">*</span>
                                            <asp:TextBox ID="txtvendorphone1" runat="server" MaxLength="10" onkeypress="return isNumber(event)" class="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                          <b>  Contact Person 2 :</b>
                                <asp:TextBox ID="txt_c_2" runat="server" class="form-control"
                                    MaxLength="100" onKeyPress="return  AllowAlphabet(event)"></asp:TextBox>
                                        </div>

                                        <%--  <div class="col-sm-1"></div>--%>
                                        <div class=" col-sm-2 col-xs-12">
                                           <b> Contact Person  Mob No.2 :</b>
                              <asp:TextBox ID="txtvendorphone2" runat="server" MaxLength="10" onkeypress="return isNumber(event)" class="form-control"></asp:TextBox><br />
                                        </div>
                                        <div class=" col-sm-2 col-xs-12">
                                           <b> Contact Person Email ID :</b><span class="text-red">*</span>
                                            <asp:TextBox ID="txt_email" runat="server" MaxLength="70" class="form-control" onkeypress="return email(event)"></asp:TextBox><br />
                                        </div>


                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-1 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Vendor Categories :</b><span class="text-red">*</span>
                                            <asp:DropDownList ID="ddl_vendor_type" runat="server" class="form-control">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="Electrical">Electrical</asp:ListItem>
                                                <asp:ListItem Value="Plumbing">Plumbing</asp:ListItem>
                                                <asp:ListItem Value="Carpentry">Carpentry</asp:ListItem>
                                                <asp:ListItem Value="Civil">Civil</asp:ListItem>
                                                <asp:ListItem Value="Pest_Control">Pest Control</asp:ListItem>
                                                <asp:ListItem Value="HVAC">HVAC</asp:ListItem>
                                                <asp:ListItem Value="Stationery">Stationery</asp:ListItem>
                                                <asp:ListItem Value="Chemical_Supplier">Chemical Supplier</asp:ListItem>
                                                <asp:ListItem Value="Shoes">Shoes</asp:ListItem>
                                                <asp:ListItem Value="Uniform">Uniform</asp:ListItem>
                                                <asp:ListItem Value="Housekeeping_services">Housekeeping services</asp:ListItem>
                                                <asp:ListItem Value="Other">Other</asp:ListItem>


                                            </asp:DropDownList>
                                        </div>
                                        <div class=" col-sm-2 col-xs-12">
                                          <b>  Credit Period (In Day) :</b><span class="text-red">*</span>
                                            <asp:TextBox ID="txt_credit_period" runat="server" class="form-control" MaxLength="70" onKeyPress="return  isNumber(event)"></asp:TextBox>
                                        </div>
                                        <div class=" col-sm-2 col-xs-12">
                                           <b> Area to be Served :</b>
                              <asp:TextBox ID="txt_area" runat="server" MaxLength="10" class="form-control" onKeyPress="return  AllowAlphabet_address(event)"></asp:TextBox><br />
                                        </div>
                                        <div class=" col-sm-2 col-xs-12">
                                          <b>  Agreement Start Date:</b><span class="text-red">*</span>
                                            <asp:TextBox ID="txt_start" runat="server" MaxLength="10" onkeypress="return isNumber(event)" class="form-control date-picker1"></asp:TextBox><br />
                                        </div>
                                        <div class=" col-sm-2 col-xs-12">
                                          <b>  Agreement End Date :</b><span class="text-red">*</span>
                                            <asp:TextBox ID="txt_end" runat="server" MaxLength="10" onkeypress="return isNumber(event)" class="form-control date-picker2"></asp:TextBox><br />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                                            <asp:Label ID="Label1" runat="server" Text=" Designation :" Visible="false"></asp:Label>

                                        </div>
                                        <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txtvendordesignation" runat="server" Visible="false" Font-Size="X-Small" class="form-control" MaxLength="30" onKeyPress="return AllowAlphabet(event)"></asp:TextBox><br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>


                    <div id="menu6">
                             <div class="row">

                                    <div class="col-sm-2 col-xs-12">
                                        <br /><br />
                          <b>  Payment Against Bank :</b>
                                      
                                <asp:DropDownList ID="ddl_payment_bank" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_payment_bank_SelectedIndexChanged">
                                </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12" runat="server" id="Div1">
                            <br /><br />
                          <b>  Company A/C No:</b>
                        <asp:TextBox ID="txt_comp_ac_no" runat="server" class="form-control text_box" ReadOnly="true"></asp:TextBox>

                        </div>
                                 
                                      <br />
                                       <br />
                                        <div class="col-sm-2 col-xs-12">
                                     <table class="table table-striped" >
                            <tr>
                                <td>File to Upload :
                                                <asp:FileUpload ID="upload_sheet" runat="server" meta:resourcekey="photo_uploadResource1" onchange="ValidateSingleInput(this);" />
                                                <span style="color: red; font-size: 8px; font-weight: bold;">Only JPG,JPEG,GIF,PDF</span></td>
                                            <td>
                                                <%--<asp:Button ID="btn_upload" runat="server" class="btn btn-primary" Style="margin-top: 1em" Text=" Upload " OnClick="btn_upload_Click" OnClientClick="return Req_upload();"/>--%>
                                            </td>
                                        </tr>
                                    </table>
                                            </div>
                                 <br />
                                
                                    <div class="col-sm-2 col-xs-12"></div>
                                 <div class="col-sm-2 col-xs-12">
                                     
                                            <asp:LinkButton ID="lnk_account_no" runat="server" OnClientClick="return plus_validation();" AutoPostBack="true" OnClick="lnk_account_no_Click" >
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                            </asp:LinkButton>

                                        </div>
                                 <br/>
                                   <br/>

                                </div>
                                <br /><br />
                        <br />

                              <div class="row">

                                   <div class="container-fluid" style="width: 60%">
                                     <asp:Panel ID="Panel_comp_bank" runat="server" BackColor="#f3f1fe"  meta:resourcekey="Panel_comp_bankResource1" CssClass="grid-view" >
                                <asp:GridView ID="gv_company_bank" class="table" runat="server" BackColor="White" OnRowDataBound="gv_company_bank_RowDataBound" 
                                    OnPreRender="gv_company_bank_PreRender"
                                    BorderColor="#CCCCCC" BorderStyle="None" meta:resourcekey="gv_company_bankResource1" BorderWidth="1px" 
                                    AutoGenerateColumns="false" Width="100%">
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

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_remove_com_bank" runat="server" CausesValidation="false" OnClientClick="R_validation();" OnClick="lnk_remove_com_bank_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sr No.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
                                        <asp:BoundField DataField="payment_ag_bank" HeaderText="Payment Against Bank " SortExpression="payment_ag_bank" />
                                        <asp:BoundField DataField="company_ac_no" HeaderText="Company A/C No" SortExpression="company_ac_no" />
                                       <asp:TemplateField HeaderText="DOWNLOAD">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_download" runat="server" CausesValidation="false" Text="Download" CommandArgument='<%#Eval("file_upload") %>' CssClass="btn btn-primary" OnCommand="lnk_download_Command" Style="color: white"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                         

                                    </Columns>
                                </asp:GridView>
                                          </asp:Panel>

                           </div>
                              </div>
                                  
                              </div>



                    <div id="menu4">
                        <div class="container-fluid" style="background: white; border-radius: 10px; padding:15px 15px 15px 15px; margin:15px 15px 15px 15px; border: 1px solid white">
                            <br />
                            <div class="row text-center ">
                                <div class="col-md-12">
                                    <u><h4>Vendor Information</h4></u>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                            </div>
                            
                            <div class="container-fluid">
                                <div class="row">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-3 col-xs-12">
                                                   <b> Bank Name :</b><span class="text-red">*</span>
                                                    <asp:TextBox ID="txt_bank_name" runat="server" MaxLength="30" class="form-control" onKeyPress="return  AllowAlphabet(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                   <b> Vendor Bank Account Name :</b><span class="text-red">*</span>
                                                    <asp:TextBox ID="txt_vendor_bank_acc_name" runat="server" MaxLength="60" class="form-control" onKeyPress="return  AllowAlphabet(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                   <b> Account Number :</b><span class="text-red">*</span>
                                                    <asp:TextBox ID="txt_acc_no" runat="server" MaxLength="30" onkeypress="return isNumber(event)" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                    <b>IFSC Code :</b><span class="text-red">*</span>
                                                    <asp:TextBox ID="txt_ifsc_code" runat="server" MaxLength="15" class="form-control" onKeyPress="return  AllowAlphabet_address(event)"></asp:TextBox>
                                                </div>



                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                    <div id="home">
                        <br />
                        <div class="container-fluid" style="background: white; border-radius: 10px; border: 1px solid white">
                            <br />
                            <div class="row text-center ">
                                <div class="col-md-12">
                                    <h4><u>All Taxes</u></h4>
                                </div>
                                <br /><br />
                                <br />
                                <br />
                                <br />
                            </div>

                            <div class="container-fluid">
                                <div class="row">
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                            <span style="margin-left: 8em"><b>GST IN :</b> </span>
                                            <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:TextBox ID="txt_gst" runat="server" class="form-control" MaxLength="15" onKeyPress="return AllowAlphabet1(event)"></asp:TextBox><br />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <span style="margin-left: 8em"><b>PAN No :</b></span>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:TextBox ID="txtvendorpan" runat="server" class="form-control" MaxLength="10" onKeyPress="return AllowAlphabet1(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <span style="margin-left: 5em"><b>Service Type :</b></span>
                                            <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:TextBox ID="txtvendoradd2" runat="server" class="form-control" MaxLength="15" onKeyPress="return AllowAlphabet3(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:TextBox ID="txtvendorlst" runat="server" class="form-control" MaxLength="15" onKeyPress="return AllowAlphabet11(event)" Visible="false"></asp:TextBox>
                                        </div>


                                    </div>
                                    <div class="row">


                                        <div class="col-sm-2 col-xs-12">
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:TextBox ID="txtvendorcst" Visible="false" runat="server" class="form-control" MaxLength="15" onKeyPress="return AllowAlphabet11(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:Label ID="Label2" Visible="false" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:TextBox ID="txtvendortin" Visible="false" runat="server" class="form-control" MaxLength="15" onKeyPress="return AllowAlphabet11(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:TextBox ID="txtvendorlbt" runat="server" Visible="false" class="form-control" MaxLength="15" onKeyPress="return AllowAlphabet11(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">


                                        <div class="col-sm-2 col-xs-12">
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:TextBox ID="txtvendorservictax" Visible="false" runat="server" class="form-control" MaxLength="15" onKeyPress="return AllowAlphabet11(event)"></asp:TextBox>
                                        </div>



                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                            <span style="margin-left: 5em"><b>Active Status :</b></span>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:DropDownList ID="ddlstatus" runat="server"
                                                class="form-control">
                                                <asp:ListItem Value="A">Active</asp:ListItem>
                                                <asp:ListItem Value="D">Deactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-sm-2 col-xs-12">
                                            <span style="margin-left: 6em"><b>Total Dues :</b></span>

                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:TextBox ID="txtvendortotaldues" runat="server" class="form-control" MaxLength="10" onkeypress="return isNumbertotal(event)"></asp:TextBox><br />
                                        </div>
                                        <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                                            <span style="margin-left: 3em"><b>Opening Balance :</b></span>

                                        </div>
                                        <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txtopeningbalance" runat="server" onkeypress="return isNumbertotal(event)" class="form-control" MaxLength="10"></asp:TextBox><br />
                                        </div>
                                        <%-- <div class="col-sm-2 col-xs-12">
                                    SAC Code :
                                </div>--%>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:TextBox ID="txt_saccode1" runat="server" class="form-control" MaxLength="12" onKeyPress="return AllowAlphabet1(event)" Visible="false"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                            <span style="margin-left: 1em"><b>Registration Number :</b>  </span>

                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:TextBox ID="txt_regi_no" runat="server" class="form-control"></asp:TextBox><br />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <span style="margin-left: 6em"><b>SAC Code : </b>  </span>

                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:TextBox ID="txt_saccode" runat="server" class="form-control"></asp:TextBox><br />
                                        </div>
                                        <%-- <div class="col-sm-2 col-xs-12">
                                    HSN Code :
                                </div>--%>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:TextBox ID="txt_hsmcode" runat="server" class="form-control" MaxLength="12" onKeyPress="return AllowAlphabet1(event)" Visible="false"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-2 col-xs-12">
                                            <asp:DropDownList ID="ddltype" Visible="false" runat="server" class="form-control" DataTextField="VEND_TYPE" DataValueField="VEND_TYPE">
                                            </asp:DropDownList><br />
                                            <%--<asp:SqlDataSource ID="SqlDataSourceVendType" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:celtpayConnectionString %>" ProviderName="<%$ ConnectionStrings:celtpayConnectionString.ProviderName %>"
                                        SelectCommand="SELECT VEND_TYPE FROM PAY_VENDOR_TYPE"></asp:SqlDataSource>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                    <div id="menu1">
                        <br />
                        <div class="container-fluid" style="background: white; border-radius: 10px; border: 1px solid white">
                            <br />
                            <div class="row text-center ">
                                <div class="col-md-12">
                                    <h4><u>Vendor Information</u></h4>
                                </div>
                                <br /><br /><br /><br /><br />
                            </div>
                            
                            <div class="container" style="width: 85%; margin-left: 12em;">
                                <div class="row">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="row text-right" style="display: none;">
                                                <asp:Button ID="btn_shipping_address" runat="server" CssClass="btn btn-warning" Text="Copy Billing Address"
                                                    OnClientClick="return copy_add();"
                                                    OnClick="btn_shipping_address_Click" />
                                            </div>
                                            <%--<div class="row">
                                        
                                            <h5 class="text-center">Billing Address</h5>
                                        </div>--%>
                                            <br />
                                            <div class="row">

                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Attention : </b>
                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                    <asp:TextBox ID="txtbillattention" runat="server" onkeypress="return AllowAlphabet(event)" class="form-control" MaxLength="70"></asp:TextBox>
                                                    <div class="col-sm-1 col-xs-12"></div>
                                                </div>
                                                <div class="col-sm-1 col-xs-12"></div>
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Country :</b>
                                      <asp:Label ID="Label10" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                    <asp:TextBox ID="txtbillcountry" runat="server" class="form-control" MaxLength="50" onkeypress="return AllowAlphabet(event)" Rows="4"></asp:TextBox>
                                                </div>

                                            </div>

                                            <br />
                                            <div class="row">

                                                <div class="col-sm-2 col-xs-12">
                                                   <b> State :</b>
                                     <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                    <%--<asp:TextBox ID="txtbillstate" runat="server" class="form-control" onkeypress="return AllowAlphabet(event)" MaxLength="25" rows="4"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="txtbillstate" runat="server" class="form-control" Width="100%" OnSelectedIndexChanged="get_city_list" AutoPostBack="true">
                                                        <asp:ListItem Text="Select" Value="Select" />
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1 col-xs-12"></div>
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> City :</b>
                                       <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                    <%--<asp:TextBox ID="txtbillcity" runat="server" class="form-control" onkeypress="return AllowAlphabet(event)" MaxLength="25" rows="4"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="txtbillcity" runat="server" class="form-control" Width="100%"></asp:DropDownList>
                                                </div>
                                            </div>


                                            <br />
                                            <div class="row">

                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Zip code :</b>
                                   <%--   <asp:Label ID="Label9" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                    <asp:TextBox ID="txtbillzipcode" onkeypress="return isNumber(event)" MaxLength="6" runat="server" class="form-control" Rows="4"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 col-xs-12"></div>
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Address :</b>
                                         <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>

                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                    <asp:TextBox ID="txtbilladdress" runat="server" class="form-control" placeholder="Street1" MaxLength="100" TextMode="MultiLine" Rows="4" onKeyPress="return  AllowAlphabet_address(event)"></asp:TextBox>


                                                </div>
                                            </div>

                                            <br />
                                            <div class="row">

                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Fax :</b>
                                         
                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                    <asp:TextBox ID="txtbillfax" onkeypress="return isNumber(event)" MaxLength="20" runat="server" class="form-control" Rows="4"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-6 col-xs-12" style="display: none">
                                                <h5 class="text-center">Shipping Address</h5>
                                                <div class="row">
                                                    <div class="col-sm-2 col-xs-12">
                                                       <b> Attention :</b>
                                                    </div>
                                                    <div class="col-sm-9 col-xs-12">
                                                        <asp:TextBox ID="txtsattention" runat="server" onkeypress="return AllowAlphabet(event)" MaxLength="70" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-sm-2 col-xs-12">
                                                       <b> Address :</b>
                                 
                                                    </div>
                                                    <div class="col-sm-9 col-xs-12">
                                                        <asp:TextBox ID="txtsaddress" runat="server" MaxLength="100" class="form-control" placeholder="Street1" TextMode="MultiLine" Rows="4" onKeyPress="return  AllowAlphabet_address(event)"></asp:TextBox>

                                                    </div>
                                                </div>

                                                <br />
                                                <div class="row">
                                                    <div class="col-sm-2 col-xs-12">
                                                       <b> State :</b>
                                        
                                                    </div>
                                                    <div class="col-sm-9 col-xs-12">
                                                        <%--<asp:TextBox ID="txtssstate" runat="server" class="form-control" onkeypress="return AllowAlphabet(event)" MaxLength="25" rows="4"></asp:TextBox>--%>
                                                        <asp:DropDownList ID="txtssstate" runat="server" class="js-example-basic-single" Width="100%" AppendDataBoundItems="true" OnSelectedIndexChanged="get_city_list_shipping" AutoPostBack="true">
                                                            <asp:ListItem Text="Select" Value="Select" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-sm-2 col-xs-12">
                                                       <b> City :</b>
                                       
                                                    </div>
                                                    <div class="col-sm-9 col-xs-12">
                                                        <%--<asp:TextBox ID="txtscity" runat="server" class="form-control" onkeypress="return AllowAlphabet(event)" MaxLength="25" rows="4"></asp:TextBox>--%>
                                                        <asp:DropDownList ID="txtscity" runat="server" class="js-example-basic-single" Width="100%"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-sm-2 col-xs-12">
                                                      <b>  Zip code :</b>
                                      
                                                    </div>
                                                    <div class="col-sm-9 col-xs-12">
                                                        <asp:TextBox ID="txtszipcode" runat="server" class="form-control" MaxLength="7" onkeypress="return isNumber(event)" Rows="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-sm-2 col-xs-12">
                                                       <b> Country :</b>
                                                    </div>
                                                    <div class="col-sm-9 col-xs-12">
                                                        <asp:TextBox ID="txtscountry" runat="server" class="form-control" MaxLength="50" onkeypress="return AllowAlphabet(event)" Rows="4"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <br />
                                                <div class="row">
                                                    <div class="col-sm-2 col-xs-12">
                                                       <b> Fax :</b>
                                                    </div>
                                                    <div class="col-sm-9 col-xs-12">
                                                        <asp:TextBox ID="txtsfax" runat="server" class="form-control" MaxLength="20" onkeypress="return isNumber(event)" Rows="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                    <div id="menu2">
                        <br />
                        <div class="container-fluid" style="background:white; border-radius: 10px; border: 1px solid white">
                            <br />
                            <div class="row text-center ">
                                <div class="col-md-12">
                                    <h4><u>Vendor Information</u></h4>
                                </div>
                                <br /><br /><br /><br /><br />
                            </div>
                            
                            <div class="container-fluid">
                                <div class="row">
                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table id="maintable" class="table table-responsive main_table" border="1" runat="server">
                                                <tr style="background-color: #aaa;">
                                                    <th align="center">Salutation</th>
                                                    <th align="center">First Name</th>
                                                    <th align="center">Last Name</th>
                                                    <th align="center">Email Address</th>
                                                    <th align="center">Work Phone</th>
                                                    <th align="center">Contact Number</th>
                                                    <th align="center">Designation</th>
                                                    <th align="center">Department</th>
                                                    <th align="center">Add Row</th>
                                                </tr>
                                                <tr id="rows">

                                                    <td>
                                                        <asp:DropDownList ID="lst_simu" runat="server" Font-Size="X-Small" class="form-control" name="salutation<? $i ?>">
                                                            <asp:ListItem Value="Select Salutation">Select Salutation</asp:ListItem>
                                                            <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
                                                            <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
                                                            <asp:ListItem Value="Ms.">Ms.</asp:ListItem>
                                                            <asp:ListItem Value="Miss.">Miss.</asp:ListItem>
                                                            <asp:ListItem Value="Dr.">Dr.</asp:ListItem>

                                                        </asp:DropDownList>



                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtfirstname" onkeypress="return AllowAlphabet(event)" MaxLength="50" runat="server" class="form-control" name="firstname<? $i ?>"></asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtlastname" onkeypress="return AllowAlphabet(event)" MaxLength="50" runat="server" class="form-control" name="lastname<? $i ?>"></asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txteaddress" onkeypress="return email(event)" MaxLength="100" runat="server" class="form-control" name="emailaddress<? $i ?>"></asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtworkphonno" MaxLength="10" runat="server" class="form-control" onkeypress="return isNumber(event)" name="workphone<? $i ?>"></asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtmobileno" MaxLength="10" runat="server" class="form-control" onkeypress="return isNumber(event)" name="mobile<? $i ?>"></asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtdesignation1" onkeypress="return AllowAlphabet(event)" MaxLength="50" runat="server" class="form-control" name="Designation<? $i ?>"></asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtdept" onkeypress="return AllowAlphabet(event)" MaxLength="50" runat="server" class="form-control" name="Department<? $i ?>"></asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <%--   <a style="cursor:pointer;"><div class="delete">- Remove</div></a>--%>
                                                        <asp:LinkButton ID="lnkbtn_addmoreitem" runat="server" CausesValidation="False" Width="85%"
                                                            OnClick="lnkbtn_addmoreitem_Click" OnClientClick=" return Req_validation1();"><img alt="Add Item"  
                                                        src="Images/add_icon.png"  /></asp:LinkButton>

                                                    </td>

                                                </tr>
                                            </table>
                                            <br>
                                            <asp:Panel ID="Panel3" runat="server">
                                                <asp:Panel ID="Panel4" runat="server" CssClass="grid-view">

                                                    <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                        OnRowDataBound="gv_itemslist_RowDataBound"
                                                        AutoGenerateColumns="False" Font-Size="X-Small">

                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />

                                                        <Columns>
                                                            <%--<Columns>--%>
                                                            <asp:TemplateField>
                                                                <ItemStyle />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sr No.">
                                                                <ItemStyle />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_srnumber" runat="server" Font-Size="X-Small" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Salutation">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="lbl_samulation" runat="server" Style="text-align: left" class="form-control" Font-Size="X-Small" Text='<%# Eval("Salutation")%>'>
                                                                        <asp:ListItem Value="Select salutation">Select salutation</asp:ListItem>
                                                                        <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
                                                                        <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
                                                                        <asp:ListItem Value="Ms.">Ms.</asp:ListItem>
                                                                        <asp:ListItem Value="Miss.">Miss.</asp:ListItem>
                                                                        <asp:ListItem Value="Dr.">Dr.</asp:ListItem>

                                                                    </asp:DropDownList>
                                                                    <%-- <asp:TextBox ID="" ReadOnly="True" Font-Size="X-Small" runat="server" style="text-align: left" class="form-control"  Text='<%# Eval("Salutation")%>'></asp:TextBox>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="First Name">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbltxtfirstname" Font-Size="X-Small" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("txtfirstname")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Last Name">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbltxtlastname" Font-Size="X-Small" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("txtlastname")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Email Address">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbltxteaddress" Font-Size="X-Small" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("txteaddress")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Work Phone">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbltxtworkphonno" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("txtworkphonno")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Mobile">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbltxtmobileno" Font-Size="X-Small" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("txtmobileno")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation">
                                                                <ItemStyle />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbltxtdesignation1" Font-Size="X-Small" runat="server" onkeypress="return isNumber(event)" Style="text-align: left" class="form-control" Text='<%# Eval("txtdesignation1")%>' AutoPostBack="True"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Department">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbldepartment" Font-Size="X-Small" runat="server" Style="text-align: left" onkeypress="return isNumber(event)" class="form-control" Text='<%# Eval("txtdept")%>' AutoPostBack="True"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                   <%--for wait tab komal--%>
                     <div id="home1">
                        <br />
                        <div class="container-fluid" style="background: white; margin:15px 15px 15px 15px; padding:20px 20px 20px 20px; border-radius: 10px; border: 1px solid white">

                             

                     <div class="col-md-2 col-xs-12">
                               <b> Courier Type :</b> <span style="color:red">*</span>    
                                  <asp:DropDownList ID="ddl_courier_type" runat="server" CssClass="form-control text_box"   Width="100%" >
                                      <asp:ListItem Value="Select">Select</asp:ListItem>
                                      <asp:ListItem Value="By Road">By Road</asp:ListItem>
                                      <asp:ListItem Value="By Air">By Air</asp:ListItem>
                                       <asp:ListItem Value="By Train">By Train</asp:ListItem>
                                      
                                      
                                  </asp:DropDownList>
                            </div>


                                 <div class="col-md-2 col-xs-12">
                              <b>  Weight :</b> <span style="color:red">*</span>    
                                  <asp:DropDownList ID="ddl_wait_type" runat="server" CssClass="form-control text_box" Width="100%">
                                      <asp:ListItem Value="Select">Select</asp:ListItem>
                                      <asp:ListItem Value="Grm">Gram</asp:ListItem>
                                      <asp:ListItem Value="Kg">Kg</asp:ListItem>
                                  </asp:DropDownList>
                            </div>


                                   <div class="col-md-2 col-xs-12">
                               <b> From Weight : </b><span style="color:red">*</span>    
                                  <asp:DropDownList ID="ddl_from" runat="server" CssClass="form-control text_box" Width="100%">
                                      <asp:ListItem Value="Select">Select</asp:ListItem>
                                      <asp:ListItem Value="5">5</asp:ListItem>
                                      <asp:ListItem Value="10">10</asp:ListItem>
                                       <asp:ListItem Value="15">15</asp:ListItem>
                                       <asp:ListItem Value="20">20</asp:ListItem>
                                       <asp:ListItem Value="25">25</asp:ListItem>
                                       <asp:ListItem Value="30">30</asp:ListItem>
                                       <asp:ListItem Value="35">35</asp:ListItem>
                                       <asp:ListItem Value="40">40</asp:ListItem>
                                  </asp:DropDownList>
                            </div>


                                   <div class="col-md-2 col-xs-12">
                             <b>  To Weight: </b><span style="color:red">*</span>    
                                  <asp:DropDownList ID="ddl_to" runat="server" CssClass="form-control text_box" Width="100%" >
                                      <asp:ListItem Value="Select">Select</asp:ListItem>
                                       <asp:ListItem Value="100">100</asp:ListItem>
                                       <asp:ListItem Value="150">150</asp:ListItem>
                                       <asp:ListItem Value="200">200</asp:ListItem>
                                       <asp:ListItem Value="250">250</asp:ListItem>
                                       <asp:ListItem Value="300">300</asp:ListItem>
                                       <asp:ListItem Value="350">350</asp:ListItem>
                                       <asp:ListItem Value="400">400</asp:ListItem>
                                  </asp:DropDownList>
                            </div>


                                   <div class="col-md-2 col-xs-12">
                              <b> From Km:</b> <span style="color:red">*</span>    
                                  <asp:DropDownList ID="ddl_from_km" runat="server" CssClass="form-control text_box" Width="100%" >
                                     <asp:ListItem Value="Select">Select</asp:ListItem>
                                      <asp:ListItem Value="5">5</asp:ListItem>
                                      <asp:ListItem Value="10">10</asp:ListItem>
                                       <asp:ListItem Value="15">15</asp:ListItem>
                                       <asp:ListItem Value="20">20</asp:ListItem>
                                       <asp:ListItem Value="25">25</asp:ListItem>
                                       <asp:ListItem Value="30">30</asp:ListItem>
                                       <asp:ListItem Value="35">35</asp:ListItem>
                                       <asp:ListItem Value="40">40</asp:ListItem>
                                  </asp:DropDownList>
                            </div>


                                   <div class="col-md-2 col-xs-12">
                              <b> To Km:</b> <span style="color:red">*</span>    
                                  <asp:DropDownList ID="ddl_to_km" runat="server" CssClass="form-control text_box" Width="100%" >
                                      <asp:ListItem Value="Select">Select</asp:ListItem>
                                       <asp:ListItem Value="100">100</asp:ListItem>
                                       <asp:ListItem Value="150">150</asp:ListItem>
                                       <asp:ListItem Value="200">200</asp:ListItem>
                                       <asp:ListItem Value="250">250</asp:ListItem>
                                       <asp:ListItem Value="300">300</asp:ListItem>
                                       <asp:ListItem Value="350">350</asp:ListItem>
                                       <asp:ListItem Value="400">400</asp:ListItem>
                                  </asp:DropDownList>
                            </div>

                                  <div class="col-sm-2 col-xs-12 text-left" ">
                      <b> Rate :</b> <span style="color:red">*</span>  
                <asp:Textbox ID="txt_rate" class="form-control" onkeypress="return isNumber(event)"  runat="server"></asp:Textbox>
                    </div>

                             <div class="col-sm-2 col-xs-12">
                                            <br />
                                            <asp:LinkButton ID="link_wait_master" runat="server" OnClick="link_wait_master_Click" OnClientClick="return  wait_tab();" AutoPostBack="true" >
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                            </asp:LinkButton>

                                        </div>



                               

                            </div>

                         <br/>
                              <div class="container" style="width:60%">
                             <asp:Panel ID="Panel24" runat="server" CssClass="grid-view" >


                                                        <asp:GridView ID="gridview_wait_master" class="table" runat="server" BackColor="White" OnRowDataBound="gridview_wait_master_RowDataBound"
                                                            BorderColor="#CCCCCC" BorderStyle="None" OnPreRender="gridview_wait_master_PreRender" BorderWidth="1px" CellPadding="3"
                                                            AutoGenerateColumns="False" Width="100%">
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
                                                                <asp:TemplateField>
                                                                    <ItemStyle Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnk_remove_wait" runat="server" OnClick="lnk_remove_wait_Click" CausesValidation="false" ><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Sr No.">
                                                                    <ItemStyle Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_srnumber_de" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>  
                                                                                                                                
                                                                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id"/>
                                                              <asp:BoundField DataField="courier_type" HeaderText="Courier Type" SortExpression="courier_type"/>
                                                                <asp:BoundField DataField="wait" HeaderText="Weight" SortExpression="wait"/>
                                                                 <asp:BoundField DataField="from_wait" HeaderText="From Weight" SortExpression="from_wait" />
                                                                <asp:BoundField DataField="to_wait" HeaderText="To Weight" SortExpression="to_wait" />
                                                                <asp:BoundField DataField="from_km" HeaderText="From Km" SortExpression="from_km" />
                                                               <asp:BoundField DataField="to_km" HeaderText="To Km" SortExpression="to_km" />
                                                                <asp:BoundField DataField="rate" HeaderText="Rate" SortExpression="rate" />
                                                              

                                                            
                                                               

                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                </div>
                               



                         </div>
                   <%-- for rate tab komal--%>
                     <div id="home2">
                        <br />
                        <div class="container-fluid" style="background: white; margin-top:15px; padding:25px 25px 25px 25px; border-radius: 10px; border: 1px solid white">

                             

                    <div class="col-sm-2 col-xs-12">
                           <b> Item Type :</b>
                                  <asp:Label ID="Label_itme_type" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <asp:DropDownList ID="ddl_item_type" runat="server" class="form-control" Width="170px" OnSelectedIndexChanged="ddl_item_type_SelectedIndexChanged" MaxLength="30"  AutoPostBack="true">
                                <%--  OnSelectedIndexChanged="hide_Click" AutoPostBack="true"--%>
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="chemicals">Chemicals</asp:ListItem>
                                <asp:ListItem Value="housekeeping_material">Housekeeping Materials</asp:ListItem>
                                <asp:ListItem Value="Uniform">Uniform</asp:ListItem>
                                <asp:ListItem Value="pantry_jacket">Pantry Jacket</asp:ListItem>
                                <asp:ListItem Value="Apron">Apron</asp:ListItem>
                                <asp:ListItem Value="Shoes">Shoes</asp:ListItem>
                                <asp:ListItem Value="ID_Card">ID Card</asp:ListItem>
                                <asp:ListItem Value="Machine">Machine</asp:ListItem>
                                <asp:ListItem Value="Other">Other</asp:ListItem>

                            </asp:DropDownList>
                        </div>

                              <div class="col-sm-2 col-xs-12 text-left">
                       <b> Item Code:</b> <span style="color:red">*</span>
                 <asp:DropDownList ID="ddl_item_code" runat="server" DataTextField="STATE" DataValueField="STATE" class="form-control text_box" OnSelectedIndexChanged="ddl_item_code_SelectedIndexChanged" AutoPostBack="true">
                 
                 </asp:DropDownList>
                    </div>


                                 <div class="col-md-2  col-xs-12">
                           <b> Item Size :</b>
                                <asp:Label ID="Label_item_size" runat="server" ForeColor="Red" Text="*"></asp:Label>

                            <asp:TextBox ID="txt_itemsize" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                        </div>

                                
                                  <div class="col-sm-2 col-xs-12 text-left" ">
                      <b> Rate :</b> <span style="color:red">*</span>  
                <asp:Textbox ID="txt_rate_mas" class="form-control" onkeypress="return isNumber(event)"  runat="server"></asp:Textbox>
                    </div>

                             <div class="col-sm-2 col-xs-12">
                                            <br />
                                            <asp:LinkButton ID="lnk_rate_master" runat="server" OnClick="lnk_rate_master_Click" OnClientClick="return rate_tab();" AutoPostBack="true" >
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                            </asp:LinkButton>

                                        </div>

                            </div>

                         <br/>

                              <div class="container" style="width:60%">
                             <asp:Panel ID="Panel5" runat="server" CssClass="grid-view" >

                                                        <asp:GridView ID="gv_rate_master" class="table" runat="server" BackColor="White" OnPreRender="gv_rate_master_PreRender" OnRowDataBound="gv_rate_master_RowDataBound"
                                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                            AutoGenerateColumns="False" Width="100%">
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
                                                                <asp:TemplateField>
                                                                    <ItemStyle Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnk_remove_rate" runat="server" OnClick="lnk_remove_rate_Click" CausesValidation="false" ><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Sr No.">
                                                                    <ItemStyle Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_srnumber_rate" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>  
                                                                                    
                                                                <asp:BoundField DataField="id" HeaderText="Id" SortExpression="id"/>                                            
                                                              <asp:BoundField DataField="item_type" HeaderText="Item Type" SortExpression="item_type"/>
                                                                <asp:BoundField DataField="item_code" HeaderText="Item Code" SortExpression="item_type"/>
                                                                 <asp:BoundField DataField="item_size" HeaderText="Item Size" SortExpression="item_size" />
                                                                <asp:BoundField DataField="rate" HeaderText="Rate" SortExpression="rate" />
                                                              
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                </div>
                               



                         </div>

<div id="home3">

   <%-- <div class="col-sm-2 col-xs-12">
                                                    Vendor Name :
                                          <asp:DropDownList runat="server" ID="ddl_vendor_name" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_vendor_name_SelectedIndexChanged">
                                              <asp:ListItem Value="Select">Select</asp:ListItem>
                                             
                                          </asp:DropDownList>
                                                </div>--%>

    <br/>
     <br/>
                                               <div class="row">

                                               
                                                <div class="col-sm-4 col-xs-12 text-left">
                                                    <b>Vendor Having Client :</b>
                                        <asp:ListBox ID="list_client_assign" runat="server" class="form-control" SelectionMode="Multiple" Height="150"></asp:ListBox>
                                                </div>
                                                   
                                                    <div class="col-sm-4 col-xs-12 text-center " style="margin-top:60px">
                                               <asp:Button ID="btn_remove_client_assign" runat="server" OnClick="btn_remove_client_assign_Click" class="btn btn-primary" Text="Remove Client Assign" TabIndex="1"  />
                                                     </div>

                                                <div class="col-sm-4 col-xs-12 text-left">
                                                   <b> Vendor Not Having Client :</b>
                                        <asp:ListBox ID="list_client_not_assign" runat="server" class="form-control" SelectionMode="Multiple" Height="150"></asp:ListBox>
                                                </div>

                                            </div>

    </div>

                  <%--  //////////////////--%>






                    <div id="menu5">
                        <br />
                        <div class="container-fluid" style="background: white; border-radius: 10px; border: 1px solid white">
                            <br />
                            <div class="row text-center ">
                                <div class="col-md-12">
                                    <h4><u>State</u></h4>
                                </div>
                                <br /><br /><br /><br /><br />
                            </div>
                           
                            <div class="container-fluid">
                                <div class="row">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>

                                            <div class="row">
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> State :</b>
                                          <asp:DropDownList runat="server" ID="ddl_state" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_state_click">
                                              <asp:ListItem Value="Select">Select</asp:ListItem>
                                              <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                          </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 col-xs-12"></div>
                                                <div class="col-sm-4 col-xs-12 text-left">
                                                   <b> Vendor Having City :</b>
                                        <asp:ListBox ID="ddl_assign_city" runat="server" class="form-control" SelectionMode="Multiple" Height="150"></asp:ListBox>
                                                </div>
                                                <div class="col-sm-4 col-xs-12 text-left">
                                                  <b>  Vendor Not Having City :</b>
                                        <asp:ListBox ID="ddl_notassign_city" runat="server" class="form-control" SelectionMode="Multiple" Height="150"></asp:ListBox>
                                                </div>



                                            </div>

                                            <div class="col-sm-2 col-xs-12" style="display: none">
                                              <b>  City :</b>
                                          <asp:DropDownList runat="server" ID="ddl_city" CssClass="form-control">
                                              <asp:ListItem Value="Select">Select</asp:ListItem>
                                          </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2 col-xs-12" style="display: none">
                                                <br />
                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lnk_add_state_Click" AutoPostBack="true">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                                </asp:LinkButton>
                                            </div>

                                            <br />

                                            <div class="container" style="width: 50%">
                                                <asp:Panel ID="Panel13" runat="server">
                                                    <asp:Panel ID="Panel14" runat="server" CssClass="grid-view">

                                                        <asp:GridView ID="gv_client_state" class="table" runat="server" BackColor="White"
                                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                            OnRowDataBound="gv_statewise_gst_RowDataBound"
                                                            AutoGenerateColumns="False" Width="100%">
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EFF3FB" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemStyle Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnk_remove_head" runat="server" CausesValidation="false" OnClick="lnk_remove_mail_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sr No.">
                                                                    <ItemStyle />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="state" HeaderText="State"
                                                                    SortExpression="state" />
                                                                <asp:BoundField DataField="city" HeaderText="City"
                                                                    SortExpression="city" />
                                                                <%--<asp:TemplateField HeaderText="State">
                                                          
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_state" runat="server" Text='<%# Eval("state")%>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="City">
                                                          
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_city" runat="server" Text='<%# Eval("city")%>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                --%>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </asp:Panel>

                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                    <br />
                    <div class="row text-center">
                        <asp:Button ID="btnadd" runat="server" class="btn btn-primary" OnClick="btnadd_Click" Text="Save" OnClientClick=" return Req_validation();" />
                        <asp:Button ID="btnupdate" runat="server" class="btn btn-primary" OnClick="btnupdate_Click" Text="Update" OnClientClick=" return Req_validation();" TabIndex="2" />
                        <asp:Button ID="btndelete" runat="server" class="btn btn-primary" OnClick="btndelete_Click" Text="Delete" TabIndex="3" OnClientClick=" return R_validation();" />
                        <asp:Button ID="btncancel" runat="server" class="btn btn-primary" OnClick="btncancel_Click" Text="Clear" TabIndex="4" />
                        <asp:Button ID="btnexporttoexcel" runat="server" class="btn btn-primary" OnClick="btnexporttoexcel_Click" Text="Export To Excel" TabIndex="5" />
                        <asp:Button ID="btnclose" runat="server" class="btn btn-danger" OnClick="btnclose_Click" Text="Close" TabIndex="6" />
                    </div>
                    <br />
                </div>
                <br />
                <div>
                   
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                           <b> Vendor Type :</b>
                                <asp:DropDownList ID="ddl_typevendor" runat="server" OnSelectedIndexChanged="ddl_typevendor_SelectedIndex" AutoPostBack="true" class="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Electrical</asp:ListItem>
                                    <asp:ListItem Value="2">Plumbing</asp:ListItem>
                                    <asp:ListItem Value="3">Carpentry</asp:ListItem>
                                    <asp:ListItem Value="4">Civil</asp:ListItem>
                                    <asp:ListItem Value="5">Pest Control</asp:ListItem>
                                    <asp:ListItem Value="6">HVAC</asp:ListItem>

                                </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> State: </b>
                                   

                                <%--<asp:TextBox ID="txtbillstate" runat="server" class="form-control" onkeypress="return AllowAlphabet(event)" MaxLength="25" rows="4"></asp:TextBox>--%>
                            <asp:DropDownList ID="txt_state" runat="server" class="form-control" Width="100%" OnSelectedIndexChanged="get_city" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> City: </b>
                                       

                                <%--<asp:TextBox ID="txtbillcity" runat="server" class="form-control" onkeypress="return AllowAlphabet(event)" MaxLength="25" rows="4"></asp:TextBox>--%>
                            <asp:DropDownList ID="txt_city" AutoPostBack="true" runat="server" class="form-control" Width="100%" OnSelectedIndexChanged="txt_city_Selected_Index"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-left:5px; margin-right:5px; margin-bottom:20px; margin-top:20px">
                    <asp:Panel ID="Panel2" CssClass="grid-view" runat="server">

                        <asp:GridView ID="VendorGridView" class="table" runat="server" BackColor="White" OnPreRender="VendorGridView_PreRender"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="VendorGridView_RowDataBound"
                            OnSelectedIndexChanged="VendorGridView_SelectedIndexChanged" Font-Size="X-Small"
                            AutoGenerateColumns="False" Width="100%">
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="White" ForeColor="#000066" />


                            <Columns>
                                <asp:CommandField SelectText="&gt;"
                                    ShowSelectButton="True" />
                                <asp:BoundField DataField="COMP_CODE" HeaderText="COMPANY CODE"
                                    SortExpression="COMP_CODE" />
                                <asp:TemplateField HeaderText="VENDOR ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_vender_id" runat="server" Text='<%# Eval("VEND_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="VEND_ID" HeaderText="VENDOR ID"
                                            SortExpression="VEND_ID" />--%>
                                <asp:BoundField DataField="VEND_NAME" HeaderText="VENDOR NAME"
                                    SortExpression="VEND_NAME" />
                                <asp:BoundField DataField="PHONE1" HeaderText="PHONE NO1" SortExpression="PHONE1" />
                                <asp:BoundField DataField="txtbillstate" HeaderText="STATE"
                                    SortExpression="txtbillstate" />
                                <asp:BoundField DataField="txtbillcity" HeaderText="CITY"
                                    SortExpression="txtbillcity" />

                                <%-- <asp:BoundField DataField="PHONE2" HeaderText="PHONE NO2" SortExpression="PHONE2" />--%>
                                <asp:BoundField DataField="sac_code" HeaderText="SAC CODE" SortExpression="sac_code" Visible="false" />
                                <asp:BoundField DataField="GST" HeaderText="GST"
                                    SortExpression="GST" />

                                <%--   <asp:BoundField DataField="LST_NO" HeaderText="LST NO"
                                            SortExpression="LST_NO" />
                                        <asp:BoundField DataField="CST_NO" HeaderText="CST NO"
                                            SortExpression="CST_NO" />
                                        <asp:BoundField DataField="TIN_NO" HeaderText="TAN NO"
                                            SortExpression="TIN_NO" />
                                        <asp:BoundField DataField="LBT_NO" HeaderText="LBT NO"
                                            SortExpression="LBT_NO" />
                                        <asp:BoundField DataField="SERVICE_TAX_NO" HeaderText="SERVICE TAX NO"
                                            SortExpression="SERVICE_TAX_NO" />
                                          <asp:BoundField DataField="VEND_ADD2" HeaderText="SERVICE TYPE"
                                            SortExpression="VEND_ADD2" />
                                        <asp:BoundField DataField="ACTIVE_STATUS" HeaderText="STATUS"
                                            SortExpression="ACTIVE_STATUS" />
                                        <asp:BoundField DataField="TYPE" HeaderText="TYPE"
                                            SortExpression="TYPE" />
                                        <asp:BoundField DataField="TOTAL_DUES" HeaderText="TOTAL DUES"
                                            SortExpression="TOTAL_DUES" />
                                        <asp:BoundField DataField="OPENING_BALANCE" HeaderText="OPENING BALANCE"
                                            SortExpression="OPENING_BALANCE" />
                                        <asp:BoundField DataField="txtbillattention" HeaderText="ATTENTION BILL"
                                            SortExpression="txtbillattention" />
                                        <asp:BoundField DataField="txtbilladdress" HeaderText="ADDRESS BILL"
                                            SortExpression="txtbilladdress" />
                                        <asp:BoundField DataField="txtbillcity" HeaderText="txtbillcity" SortExpression="CITY BILL" />
                                        <asp:BoundField DataField="txtbillstate" HeaderText="STATE BILL"
                                            SortExpression="txtbillstate" />
                                        <asp:BoundField DataField="txtbillzipcode" HeaderText="ZIP CODE BILL"
                                            SortExpression="txtbillzipcode" />
                                         <asp:BoundField DataField="txtbillcountry" HeaderText="BILL COUNTRY"
                                            SortExpression="txtbillcountry" />
                                         <asp:BoundField DataField="txtbillfax" HeaderText="BILL FAX"
                                            SortExpression="txtbillfax" />
                                         <asp:BoundField DataField="txtsattention" HeaderText="SHIP ATTENTION"
                                            SortExpression="txtsattention" />
                                         <asp:BoundField DataField="txtsaddress" HeaderText="SHIP ADDRESS"
                                            SortExpression="txtsaddress" />
                                         <asp:BoundField DataField="txtscity" HeaderText="SHIP CITY"
                                            SortExpression="txtscity" />
                                         <asp:BoundField DataField="txtssstate" HeaderText="SHIP STATE"
                                            SortExpression="txtssstate" />
                                         <asp:BoundField DataField="txtszipcode" HeaderText="SHIP ZIP CODE"
                                            SortExpression="txtszipcode" />
                                         <asp:BoundField DataField="txtscountry" HeaderText="SHIP COUNTRY"
                                            SortExpression="txtscountry" />
                                         <asp:BoundField DataField="txtsfax" HeaderText="SHIP FAX"
                                            SortExpression="txtsfax" />--%>
                            </Columns>

                        </asp:GridView>
                        <%--<asp:SqlDataSource ID="SqlDataSourceVendor" runat="server"
                                    ConnectionString="<%$ ConnectionStrings:celtpayConnectionString %>" ProviderName="<%$ ConnectionStrings:celtpayConnectionString.ProviderName %>"
                                    SelectCommand="SELECT COMP_CODE,VEND_ID,VEND_NAME,VEND_ADD1,PHONE1,PHONE2,DESIGNATION,GST,PAN_NO,LST_NO,CST_NO,TIN_NO,LBT_NO,SERVICE_TAX_NO,VEND_ADD2,ACTIVE_STATUS,TYPE,TOTAL_DUES,OPENING_BALANCE,txtbillattention,txtbilladdress,txtbillcity,txtbillstate,txtbillzipcode,txtbillcountry,txtbillfax,txtsattention,txtsaddress,txtscity,txtssstate,txtszipcode,txtscountry,txtsfax FROM PAY_VENDOR_MASTER WHERE (COMP_CODE = @COMP_CODE) ORDER BY VEND_ID">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="COMP_CODE" SessionField="COMP_CODE" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>--%>
                    </asp:Panel>
                    <br />
                </div>
            </div>
                </div>

             <%-- company bank details jyotsna--%>

                   
        </asp:Panel>
    </div>

    <br />
</asp:Content>

