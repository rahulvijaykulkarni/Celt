<%@ Page MaintainScrollPositionOnPostback="true" Title="Tax Invoice" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Transaction.aspx.cs" Inherits="Transaction" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Sales Invoice</title>
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
    <script src="Scripts/sweetalert.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" type="text/css" />
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
    <script src="datatable/jszip.min.js"></script>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>

    <style type="text/css">
        button, input, optgroup, select, textarea {
            color: inherit;
            margin: 0 0 0 0px;
        }

        * {
            box-sizing: border-box;
        }


        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding: 10px;
            height: 200px;
            width: 600px;
            z-index: 101;
        }

        .container-fluid {
            font-family: Verdana;
            font-size: 10px;
            font-weight: lighter;
        }

        .tab-section {
            background-color: #fff;
        }

        .grid-view {
            height: auto;
            max-height: 250px;
            overflow-x: auto;
            overflow-y: auto;
        }

        .grid-v {
            height: auto;
            max-height: 250px;
            overflow-x: hidden;
            overflow-y: auto;
        }

        table {
            height: auto;
            width: 80%;
        }

        td {
            text-align: center;
            height: 30px;
            width: 0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <script>
        var textarea = null;
        window.addEventListener("load", function () {
            textarea = window.document.querySelector("textarea");
            textarea.addEventListener("keypress", function () {
                if (textarea.scrollTop != 0) {
                    textarea.style.height = textarea.scrollHeight + "px";
                }
            }, false);
        }, false);
    </script>
    <script type="text/javascript">
        AllowAlphabet_address(event);
        function pageLoad() {

            var table = $('#<%=gv_bynumber_name.ClientID%>').DataTable(
               {
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
               .appendTo('#<%=gv_bynumber_name.ClientID%>_wrapper .col-sm-6:eq(0)');

            var table = $('#<%=gv_idcard.ClientID%>').DataTable(
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
               .appendTo('#<%=gv_idcard.ClientID%>_wrapper .col-sm-6:eq(0)');

            var table = $('#<%=gv_details.ClientID%>').DataTable(
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

            table.buttons().container().appendTo('#<%=gv_details.ClientID%>_wrapper .col-sm-6:eq(0)');
            $.fn.dataTable.ext.errMode = 'none';
            dropdown_hide_show();

            location_hidden();
            cal();
        }


        $(document).ready(function () {


            var evt = null;
            isNumber(evt);

        });

        function location_hidden() {
            //alert("hello");
            var location = document.getElementById('<%=txt_particular.ClientID %>');
            var SelectedText = location.options[location.selectedIndex].text;

            var txt_description = document.getElementById('<%= txt_description.ClientID %>');
            var lbl_hsn = document.getElementById('<%= lbl_hsn.ClientID %>');
            var txt_hsn = document.getElementById('<%= txt_hsn.ClientID %>');


            if (SelectedText == "Select") {

                $('.length1').hide();
                $('.length2').hide();
                $(".js-example-basic-single").select2();
            }

            else {

                $('.length1').show();
                $('.length2').show();
                $(".js-example-basic-single").select2();

            }

        }

        function AllowAlphabet1(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || (keyEntry == '32') || (keyEntry == '9'))

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
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9'))

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

                    (keyEntry == '32') || (keyEntry == '39') || (keyEntry == '38') || (keyEntry == '39') || (keyEntry == '44') || ((keyEntry >= '45') && (keyEntry <= '47')) ||

                    (keyEntry == '58') || (keyEntry == '59') || (keyEntry == '61') || (keyEntry == '34') || (keyEntry == '92'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }

        function isNumber(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 36 || charCode > 41)) {

                    return false;

                }

            }
            return true;
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

        function isNumber1(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) || (charCode == 46)) {

                    return false;
                }

            }
            return true;
        }



        function Req_validation() {

            var custcode = document.getElementById('<%=ddl_customerlist.ClientID %>');
            var custgstno = document.getElementById('<%=txt_customer_gst.ClientID %>');

            var ddl_sales_person = document.getElementById('<%=ddl_sales_person.ClientID%>');

            var mobileno = document.getElementById('<%=txt_sales_mobile_no.ClientID %>');

            var txt_grossamt = document.getElementById('<%=txt_grossamt.ClientID %>');

            if (custcode.value == "Select") {
                alert("Please Enter Client Name!!!");
                custcode.focus();
                return false;
            }
            var statecode = document.getElementById('<%=ddl_state.ClientID %>');

            if (statecode.value == "Select") {
                alert("Please Select State Name!!!");
                statecode.focus();
                return false;
            }
            var Branch = document.getElementById('<%=ddlunitselect.ClientID %>');

            if (Branch.value == "Select") {
                alert("Please Select Branch Name!!!");
                Branch.focus();
                return false;
            }

            if (custgstno.value == "") {
                alert("Please Enter Client GST Number!!!");
                custgstno.focus();
                return false;
            }
            //$.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            //return true;
            // Gross Amount

            if (txt_grossamt.value == "0" || txt_grossamt.value == "") {
                alert("Please Click On the Add Button(Table) to calculate Gross Amount !!!");
                txt_grossamt.focus();
                return false;
            }


            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function Req_validation1() {


            var text_doc_comment = document.getElementById('<%=txt_comment.ClientID %>');


            if (text_doc_comment.value == "") {

                alert("Please Enter Reason !!");
                text_doc_comment.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function btn_validation() {


            var ddl_product = document.getElementById('<%=ddl_product.ClientID%>');
            var Selected_ddl_product = ddl_product.options[ddl_product.selectedIndex].text;


            // Particular txt_designation
            if (Selected_ddl_product == "Select") {
                alert("Please Select Item Type !!!");
                ddl_product.focus();
                return false;
            }
            var e_particular = document.getElementById('<%=txt_particular.ClientID%>');
            var SelectedText = e_particular.options[e_particular.selectedIndex].text;
            if (SelectedText == "Select") {
                alert("Please Select Item Name !!!");
                e_particular.focus();
                return false;
            }
            var ddl_emp = document.getElementById('<%=ddl_emp.ClientID%>');
            var Selected_ddl_emp = ddl_emp.options[ddl_emp.selectedIndex].text;
            if (Selected_ddl_emp == "Select") {
                sweetAlert("Select Employee Name");
                ddl_emp.focus();
                return false;
            }
            var ddl_uniformsize = document.getElementById('<%=ddl_uniformsize.ClientID%>');
            if (Selected_ddl_product == "Uniform") {
                if (ddl_uniformsize.value == "0") {
                    alert("Please Select Uniform Size");
                    ddl_uniformsize.focus();
                    return false;
                }
            }
            var ddl_shoosesize = document.getElementById('<%=ddl_shoosesize.ClientID%>');
            if (Selected_ddl_product == "Shoes") {
                if (ddl_shoosesize.value == "0") {
                    alert("Please Select Shoes Size");
                    ddl_shoosesize.focus();
                    return false;
                }
            }
            var txt_description = document.getElementById('<%=txt_description.ClientID %>');
            var txt_desc = document.getElementById('<%=txt_desc.ClientID%>');
            var ddl_unit = document.getElementById('<%=txt_designation.ClientID%>');
            var select_unit_per = ddl_unit.options[ddl_unit.selectedIndex].text;
            var e_quantity = document.getElementById('<%=txt_quantity.ClientID%>');
            var txt_rate = document.getElementById('<%=txt_rate.ClientID%>');






            if (txt_desc.value == "") {
                alert("Please Enter Description !!!");
                txt_desc.focus();
                return false;
            }

            if (txt_description.value == "") {
                alert("Please Enter GST% Rate !!!");
                txt_description.focus();
                return false;
            }

            // unit
            if (select_unit_per == "Select Unit") {
                alert("Please Select unit !!!");
                ddl_unit.focus();
                return false;
            }
            if (parseFloat(e_quantity.value) == "0" || e_quantity.value == "") {
                alert("Please Enter Quantity  !!!");
                e_quantity.focus();
                return false;
            }
            if (parseFloat(txt_rate.value) == "0" || txt_rate.value == "") {
                alert("Please Enter Rate  !!!");
                txt_rate.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }


        function cal() {

            var sub_totalA = $(".sub_totalA").val();

            var exta_charg = $(".extra_charg").val();
            var exta_tax = $(".extra_tax").val();
            var charg_amont = $(".extra_amount").val();
            var sub_totalB = $(".sub_totalB").val();
            var Total = $(".final_total").val();

            var charg_amont2 = (exta_charg * exta_tax) / 100;
            $(".extra_amount").val(charg_amont2);

            var sub_totalB2 = parseInt($(".extra_charg").val()) + charg_amont2;
            $(".sub_totalB").val(sub_totalB2);

            var Total2 = parseInt($(".sub_totalA").val()) + sub_totalB2;

            $(".final_total").val(Total2);

        }

        function textAreaAdjust(o) {
            o.style.height = "1px";
            o.style.height = (25 + o.scrollHeight) + "px";
        }

        function openWindow() {
            window.open("html/Transaction.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

        $(document).ready(function () {

            $(".js-example-basic-single").select2();

        });
        function dropdown_hide_show() {
            var ddl_product = document.getElementById('<%=ddl_product.ClientID%>');
            var Selected_ddl_product = ddl_product.options[ddl_product.selectedIndex].text;

            var ddl_uniformsize = document.getElementById('<%=ddl_uniformsize.ClientID%>');
            //var Selected_ddl_uniformsize = ddl_uniformsize.options[ddl_uniformsize.selectedIndex].text;

            var ddl_shoosesize = document.getElementById('<%=ddl_shoosesize.ClientID%>');
            // var Selected_ddl_shoosesize = ddl_shoosesize.options[ddl_shoosesize.selectedIndex].text;

            var ddl_pantry_size = document.getElementById('<%=ddl_pantry_size.ClientID%>');

            var ddl_apron = document.getElementById('<%=ddl_apron.ClientID%>');

            var ddl_emp = document.getElementById('<%=ddl_emp.ClientID%>');

            if (Selected_ddl_product == "Uniform") {
                ddl_uniformsize.disabled = false;
                ddl_shoosesize.disabled = true;
                ddl_pantry_size.disabled = true;
                ddl_emp.disabled = false;
                ddl_apron.disabled = true;
            }
            else if (Selected_ddl_product == "Shoes") {
                ddl_uniformsize.disabled = true;
                ddl_shoosesize.disabled = false;
                ddl_pantry_size.disabled = true;
                ddl_emp.disabled = false;
                ddl_apron.disabled = true;
            }
            else if (Selected_ddl_product == "Pantry Jacket") {
                ddl_uniformsize.disabled = true;
                ddl_shoosesize.disabled = true;
                ddl_pantry_size.disabled = false;
                ddl_emp.disabled = false;
                ddl_apron.disabled = true;
            }
            else if (Selected_ddl_product == "Apron") {
                ddl_uniformsize.disabled = true;
                ddl_shoosesize.disabled = true;
                ddl_pantry_size.disabled = true;
                ddl_apron.disabled = false;
                ddl_emp.disabled = false;

            }
            else {
                ddl_uniformsize.disabled = true;
                ddl_shoosesize.disabled = true;
                ddl_emp.disabled = true;
                ddl_pantry_size.disabled = true;
                ddl_apron.disabled = true;
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
        $(function () {
            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: -180,
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


            $('.date-picker3').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });



            $(".date-picker1").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");
            $(".date-picker3").attr("readonly", "true");
        });
        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });

        $(function () {
            $('#<%=ddl_client_name.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state1.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_branch_name.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

            $('#<%=ddl_customerlist.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddlunitselect.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=txt_docno_number.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_searchvendor.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=txt_designation.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_emp.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_product.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=txt_particular.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=txt_quantity.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=txt_discount_price.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=txt_discount_rate.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=txt_rate.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
        });
        function R_confirm_valid() {
            var r = confirm("Please Enter Hold Reason and Click on Hold Button");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }

        function R_valid() {
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function delete_popup() {
            var r = confirm("Are you sure, You want to Delete Record ?");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }
    </script>
    <style>
        hr {
            display: block;
            margin-top: 1.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 1px;
        }
    </style>

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel9" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>DISPATCH MATERIAL</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
             <br />
            <%-- <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Dispatch Material Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>

            <div class="panel-body">
                <div id="tabs" style="background: #f3f1fe; border: 1px solid #e2e2dd; padding:15px 15px 15px 15px; margin-bottom:25px; margin-top:20px; border-radius:10px">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li id="tabactive2"><a href="#menu2" runat="server"><b>Uniform_Id_Shoes Dispatch</b> </a></li>
                        <li id="tabactive1"><a href="#menu1" runat="server"><b>Other Material </b></a></li>

                    </ul>

                    <div id="menu2" class="tab-pane fade in active">
                        <br />
                        <%-- vikas add 05/04/2019--%>
                        <div class="container-fluid" style="background: white; margin-bottom:25px; margin-top:20px; border-radius: 10px; border: 1px solid white">
                            <br />
                            <div class="row">
                                <div class="col-sm-1 col-xs-12 "></div>
                                <%-- <asp:UpdatePanel runat="server" UpdateMode="Conditional"><ContentTemplate>--%>
                                <div class="col-sm-2 col-xs-12 ">
                                   <b> Client Name :</b>
          
                                    <asp:DropDownList ID="ddl_client_name" runat="server" MaxLength="30" OnSelectedIndexChanged="ddl_client_name_SelectedIndexChanged"
                                        AutoPostBack="true" CssClass="form-control text_box">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12 ">
                                  <b>  State Name :</b>
          
                                    <asp:DropDownList ID="ddl_state1" runat="server" MaxLength="30" OnSelectedIndexChanged="ddl_state1_SelectedIndexChanged"
                                        AutoPostBack="true" CssClass="form-control text_box">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12 ">
                                 <b>   Branch Name :</b>
          
                                    <asp:DropDownList ID="ddl_branch_name" runat="server" MaxLength="30"
                                        OnSelectedIndexChanged="ddl_branch_name_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control text_box">
                                    </asp:DropDownList>
                                </div>


                                <div class="col-sm-2 col-xs-12 " runat="server" id="reson">
                                  <b>  Reson :</b>
                                     <asp:TextBox ID="txt_comment" runat="server" Rows="4" MaxLength="200" class="form-control text_box"
                                         TextMode="MultiLine" Style="overflow: hidden"></asp:TextBox>


                                </div>
                                <div class="col-sm-1 col-xs-12 " runat="server" id="hold_materl" style="margin-top: 1.5em">

                                    <asp:Button ID="hold_material" runat="server" CssClass="btn btn-primary" Text="Hold" OnClick="hold_material_Click" OnClientClick="return Req_validation1();" />
                                </div>
                                <div class="col-sm-1 col-xs-12 ">
                                    <asp:TextBox ID="txt_emp_id" runat="server" Rows="4" MaxLength="200" class="form-control text_box" Visible="false"></asp:TextBox>

                                </div>
                                <div class="col-sm-1 col-xs-12 ">
                                    <asp:TextBox ID="txt_particul" runat="server" Rows="4" MaxLength="200" class="form-control text_box" Visible="false"></asp:TextBox>

                                </div>
                                <%-- </ContentTemplate></asp:UpdatePanel>--%>
                            </div>


                            <br />
                            <br />
                            <div class="container" style="width: 85%">
                                <asp:Panel ID="Panel7" runat="server" CssClass="grid-view" Style="overflow-x: hidden;">
                                    <asp:GridView ID="gv_idcard" class="table" runat="server" AutoGenerateColumns="False"
                                        ShowHeader="true"
                                        CellPadding="4" ForeColor="#333333" GridLines="Both" OnPreRender="gv_idcard_PreRender" Width="100%">

                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="11px" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="White" ForeColor="#000066" />


                                        <Columns>
                                            <%--<asp:BoundField DataField="" HeaderText="SR NO"
                                SortExpression="" />--%>
                                            <asp:BoundField DataField="client_code" HeaderText="Client Name"
                                                SortExpression="client_code" />
                                            <asp:BoundField DataField="STATE_NAME" HeaderText="State Name"
                                                SortExpression="STATE_NAME" />
                                            <asp:BoundField DataField="unit_name" HeaderText="Unit Name"
                                                SortExpression="unit_name" />
                                            <%-- <asp:BoundField DataField="STATE_NAME" HeaderText="State Name"
                                SortExpression="STATE_NAME" />--%>
                                        </Columns>
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                            <%--//--%>

                            <%--<div class="container" style="width:30%">--%>
                            <asp:Panel ID="Panel10" runat="server" CssClass="grid-v">
                                <asp:GridView ID="gv_details" class="table" runat="server" AutoGenerateColumns="False" ShowHeader="true" OnPreRender="gv_details_PreRender"
                                    CellPadding="4" ForeColor="#333333" GridLines="Both" OnRowDataBound="gv_details_RowDataBound" DataKeyNames="emp_code1" Width="100%">

                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="White" ForeColor="#000066" />


                                    <Columns>

                                        <asp:BoundField DataField="emp_code1" HeaderText="emp_code1"
                                            SortExpression="emp_code1" />
                                        <asp:BoundField DataField="unit_code" HeaderText="Unit Name"
                                            SortExpression="unit_code" />
                                        <asp:BoundField DataField="emp_code" HeaderText="Employee Name"
                                            SortExpression="emp_code" />
                                        <asp:TemplateField HeaderText="ITEM TYPE" HeaderStyle-Width="25%" ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <span style="margin-left: 0.5em;">ID CARD :  </span>
                                                <asp:TextBox ID="txt_id" runat="server" ReadOnly="true" Width="100px" Height="28px" Text='<%# Eval("ID_CARD") %>'></asp:TextBox><br />
                                                <hr />
                                                UNIFORM      : 
                                            <asp:TextBox ID="txt_uniform" runat="server" ReadOnly="true" Width="100px" Height="28px" Text='<%# Eval("UNIFORM") %>'></asp:TextBox><br />
                                                <hr />
                                                <span style="margin-left: 1.5em;">SHOES :</span>
                                                <asp:TextBox ID="txt_Shoes" runat="server" ReadOnly="true" Width="100px" Height="28px" Text='<%# Eval("Shoes") %>'></asp:TextBox><br />
                                                <hr />
                                                <span>PANTRY JACKET:</span>
                                                <asp:TextBox ID="txt_Pantry_Jacket" runat="server" ReadOnly="true" Width="100px" Height="28px" Style="margin-right: 3em;" Text='<%# Eval("Pantry_Jacket") %>'></asp:TextBox><br />
                                                <hr />
                                                <span style="margin-left: 1.5em;">APRON : </span>
                                                <asp:TextBox ID="txt_Apron" runat="server" ReadOnly="true" Width="100px" Height="28px" Text='<%# Eval("Apron") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ITEM SIZE" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                SIZE :  
                                            <asp:TextBox ID="txt_id_size" runat="server" ReadOnly="true" Width="44px" Height="28px" Text='<%# Eval("ID_Card_size") %>'></asp:TextBox><br />
                                                <hr />
                                                SIZE :  
                                           <asp:TextBox ID="txt_uniform_size" runat="server" ReadOnly="true" Width="44px" Height="28px" Text='<%# Eval("UNIFORM_size") %>'></asp:TextBox><br />
                                                <hr />
                                                SIZE :  
                                            <asp:TextBox ID="txt_Shoes_size" runat="server" ReadOnly="true" Width="44px" Height="28px" Text='<%# Eval("Shoes_size") %>'></asp:TextBox><br />
                                                <hr />
                                                SIZE :  
                                            <asp:TextBox ID="txt_Pantry_Jacket_size" runat="server" ReadOnly="true" Width="44px" Height="28px" Text='<%# Eval("Pantry_Jacket_size") %>'></asp:TextBox><br />
                                                <hr />
                                                SIZE :  
                                            <asp:TextBox ID="txt_Apron_size" runat="server" ReadOnly="true" Width="44px" Height="28px" Text='<%# Eval("Apron_size") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ITEM SET" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                SET  :
                                            <asp:TextBox ID="txt_id_set" runat="server" ReadOnly="true" Width="44px" Height="28px" Text='<%# Eval("ID_CARD_set") %>'></asp:TextBox><br />
                                                <hr />
                                                SET  :
                                            <asp:TextBox ID="txt_uniform_set" runat="server" ReadOnly="true" Width="44px" Height="28px" Text='<%# Eval("UNIFORM_set") %>'></asp:TextBox><br />
                                                <hr />
                                                SET  :
                                            <asp:TextBox ID="txt_Shoes_set" runat="server" ReadOnly="true" Width="44px" Height="28px" Text='<%# Eval("Shoes_set") %>'></asp:TextBox><br />
                                                <hr />
                                                SET  :
                                            <asp:TextBox ID="txt_Pantry_Jacket_set" runat="server" ReadOnly="true" Width="44px" Height="28px" Text='<%# Eval("Pantry_Jacket_set") %>'></asp:TextBox><br />
                                                <hr />
                                                SET  :
                                            <asp:TextBox ID="txt_Apron_set" runat="server" ReadOnly="true" Width="44px" Height="28px" Text='<%# Eval("Apron_set") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:BoundField DataField="emp_code1" HeaderText="Employee Name"
                                            SortExpression="emp_code1" Visible="false" />

                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lnk_idcard" runat="server" ControlStyle-CssClass="btn btn-primary" Text="ID Process" OnClick="lnk_idcard_Click" Style="color: white"></asp:LinkButton><br />
                                                <hr />
                                                <asp:LinkButton ID="lnk_uniform" runat="server" ControlStyle-CssClass="btn btn-primary" Text="UNIFORM Process" OnClick="lnk_uniform_Click" Style="color: white"></asp:LinkButton><br />
                                                <hr />
                                                <asp:LinkButton ID="lnk_shoes" runat="server" ControlStyle-CssClass="btn btn-primary" Text="SHOES Process" OnClick="lnk_shoes_Click" Style="color: white"></asp:LinkButton><br />
                                                <hr />
                                                <asp:LinkButton ID="lnk_pantry_j" runat="server" ControlStyle-CssClass="btn btn-primary" Text="PANTY_J Process" OnClick="lnk_pantry_j_Click" Style="color: white"></asp:LinkButton><br />
                                                <hr />
                                                <asp:LinkButton ID="lnk_apron" runat="server" ControlStyle-CssClass="btn btn-primary" Text="APRON  Process" OnClick="lnk_apron_Click" Style="color: white"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lnk_idcard_hold" runat="server" ControlStyle-CssClass="btn btn-primary" Text="ID Hold" OnClick="lnk_idcard_hold_Click" Style="color: white" OnClientClick="return R_confirm_valid();"></asp:LinkButton><br />
                                                <hr />
                                                <asp:LinkButton ID="lnk_uniform_hold" runat="server" ControlStyle-CssClass="btn btn-primary" Text="UNIFORM Hold" OnClick="lnk_uniform_hold_Click" Style="color: white" OnClientClick="return R_confirm_valid();"></asp:LinkButton><br />
                                                <hr />
                                                <asp:LinkButton ID="lnk_shoes_hold" runat="server" ControlStyle-CssClass="btn btn-primary" Text="SHOES Hold" OnClick="lnk_shoes_hold_Click" Style="color: white" OnClientClick="return R_confirm_valid();"></asp:LinkButton><br />
                                                <hr />
                                                <asp:LinkButton ID="lnk_pantry_j_hold" runat="server" ControlStyle-CssClass="btn btn-primary" Text="PANTY_J Hold" OnClick="lnk_pantry_j_hold_Click" Style="color: white" OnClientClick="return R_confirm_valid();"></asp:LinkButton><br />
                                                <hr />
                                                <asp:LinkButton ID="lnk_apron_hold" runat="server" ControlStyle-CssClass="btn btn-primary" Text="APRON Hold" OnClick="lnk_apron_hold_Click" Style="color: white" OnClientClick="return R_confirm_valid();"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>

                                                <asp:Label ID="lbl_id_status" runat="server" Text='<%# Eval("dispatch_flag1")%>' Width="30px" Height="28px"></asp:Label><br />
                                                <hr />

                                                <asp:Label ID="lbl_uniform_status" runat="server" Text='<%# Eval("dispatch_flag2")%>' Width="50px" Height="28px"></asp:Label><br />
                                                <hr />

                                                <asp:Label ID="lbl_Shoes_status" runat="server" Text='<%# Eval("dispatch_flag3")%>' Width="30px" Height="28px"></asp:Label><br />
                                                <hr />

                                                <asp:Label ID="lbl_Pantry_status" runat="server" Text='<%# Eval("dispatch_flag5")%>' Width="70px" Height="28px"></asp:Label><br />
                                                <hr />

                                                <asp:Label ID="lbl_Apron_status" runat="server" Text='<%# Eval("dispatch_flag4")%>' Width="30px" Height="28px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%-- comment--%>
                                        <asp:TemplateField HeaderText="Comment">
                                            <ItemTemplate>

                                                <asp:Label ID="lbl_id_comment" runat="server" Text='<%# Eval("comment1")%>' Width="30px" Height="28px"></asp:Label><br />
                                                <hr />

                                                <asp:Label ID="lbl_uniform_comment" runat="server" Text='<%# Eval("comment2")%>' Width="50px" Height="28px"></asp:Label><br />
                                                <hr />

                                                <asp:Label ID="lbl_Shoes_comment" runat="server" Text='<%# Eval("comment3")%>' Width="30px" Height="28px"></asp:Label><br />
                                                <hr />

                                                <asp:Label ID="lbl_Pantry_comment" runat="server" Text='<%# Eval("comment4")%>' Width="70px" Height="28px"></asp:Label><br />
                                                <hr />

                                                <asp:Label ID="lbl_Apron_comment" runat="server" Text='<%# Eval("comment5")%>' Width="30px" Height="28px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                            <%--  </div>--%>
                            <br />
                        </div>
                    </div>

                    <div id="menu1">
                        <br />
                        <div class="container-fluid" style="background: white; border-radius: 10px; border: 1px solid white">
                            <br />
                            <div class="row">
                                <div class="col-sm-1 col-xs-12 "></div>
                                <div class="col-sm-2 col-xs-12 ">
                                   <b> Invoice Number :</b>
          
                                    <asp:TextBox ID="txt_docno_number" runat="server" AutoPostBack="True" class="form-control text_box"
                                        OnTextChanged="txt_docno_TextChanged" MaxLength="10" onkeypress="return AllowAlphabet_Number(event,this);">I</asp:TextBox>
                                </div>
                                <div class="col-sm-3 col-xs-12 ">
                                   <b> Client Name :</b>
                                    <asp:TextBox ID="txt_customername" runat="server" AutoPostBack="True" CssClass="form-control text_box"
                                        OnTextChanged="txt_customername_TextChanged" onkeypress="return AllowAlphabet1(event,this);" MaxLength="30"></asp:TextBox>

                                </div>
                                <br />

                                <div class="col-sm-3 col-xs-12">

                                    <asp:Button ID="btn_searchvendor" runat="server" CausesValidation="False" CssClass="btn btn-primary" OnClick="btn_searchvendor_Click" Text=" Search " />
                                    <%--   <asp:Button ID="btn_new" runat="server" CausesValidation="False" onclick="btn_new_Click" Text="New" CssClass="btn btn-primary" />--%>
                                    <asp:Button ID="btn_Closeup" runat="server" CausesValidation="False"
                                        CssClass="btn btn-danger" OnClick="btn_Close_Click" Text="Close" />

                                </div>
                            </div>

                            <br />
                            <asp:Panel ID="Panel5" runat="server">
                                <asp:GridView ID="gv_bynumber_name" class="table" runat="server" AutoGenerateColumns="False" DataKeyNames="DOC_NO" OnRowDataBound="gv_bynumber_name_RowDataBound"
                                    ShowHeader="true" CellPadding="4" ForeColor="#333333" GridLines="Both" OnRowDeleting="gv_bynumber_name_RowDeleting" OnPreRender="gv_bynumber_name_PreRender">

                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="11px" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="White" ForeColor="#000066" />


                                    <Columns>
                                        <asp:BoundField DataField="DOC_NO" HeaderText="DOC_NO"
                                            SortExpression="DOC_NO" />
                                        <asp:TemplateField HeaderText="Estimate No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_docnumber" runat="server" Text='<%# Eval("DOC_NO")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estimate Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_docdate" runat="server" Text='<%# Eval("DOC_DATE")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_customername" runat="server"
                                                    Text='<%# Eval("CUST_NAME")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_amount" runat="server"
                                                    Text='<%# Eval("FINAL_PRICE")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Get Details">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lnk_button" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Get Details" OnClick="lnk_button_Click" Style="color: white" OnClientClick="return R_valid();"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:CommandField ShowSelectButton="True" SelectText="Get Details"/>--%>
                                    </Columns>
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>

                            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                            <asp:Panel ID="Panel6" runat="server">

                                <div class="row">
                                    <div class="col-sm-1 col-xs-12"></div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Invoice Number :</b>
                                        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:TextBox ID="txt_docno" runat="server" CssClass="form-control text_box" MaxLength="10" onkeypress="return AllowAlphabet1(event,this);"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Invoice Date :</b> 
                                        <asp:TextBox ID="txt_docdate" runat="server" Style="margin-bottom: 0px" class="form-control date-picker1 text_box"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Category :</b>

                                        <asp:DropDownList ID="ddlcategory" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                            <asp:ListItem Value="Regular">Regular</asp:ListItem>
                                            <asp:ListItem Value="On Demand">On Demand</asp:ListItem>
                                        </asp:DropDownList>


                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>Client Name:</b>
                                              <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </span>

                                        <%-- <asp:TextBox ID="ddl_customerlist" runat="server" onkeypress="return AllowAlphabet1(event,this);" MaxLength="30" CssClass="form-control text_box">
                                                </asp:TextBox>--%>
                                        <asp:DropDownList ID="ddl_customerlist" runat="server" MaxLength="30"
                                            OnSelectedIndexChanged="customer_details" AutoPostBack="true" CssClass="form-control text_box">
                                        </asp:DropDownList>


                                    </div>
                                    <div class="col-sm-2 col-xs-12 ">
                                      <b>  Select State :</b>
                                         <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:DropDownList ID="ddl_state" runat="server" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>






                                </div>


                                <br />
                                <div class="row">
                                    <div class="col-sm-1 col-xs-12"></div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Branch Name :</b>
                                     <asp:Label ID="Label9" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <%-- <asp:Label ID="Label5" runat="server" Text="Branch Name :"></asp:Label>--%>
                                        <asp:DropDownList runat="server" ID="ddlunitselect" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlbeabchwise1_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>Client GST Number:</b>
                                                                <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*" onkeypress="return isNumber(event)"></asp:Label>
                                        </span>
                                        <span>
                                            <asp:TextBox ID="txt_customer_gst" runat="server" MaxLength="15" CssClass="form-control text_box" onkeypress="return isNumber(event)">
                                            </asp:TextBox>
                                        </span>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                      <b>  Due Date : </b>
                                         <%--  <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                        <asp:TextBox ID="txt_expiry_date" runat="server" Style="margin-bottom: 0px" class="form-control date-picker2 text_box"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Sales Person :"></asp:Label>
                                        <%--   <asp:DropDownList runat="server" ID="ddl_sales_person" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_sales_person_SelectedIndexChanged">
                                        </asp:DropDownList>--%>
                                        <asp:TextBox ID="ddl_sales_person" runat="server" MaxLength="60" class="form-control" onkeypress="return AllowAlphabet1(event)"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-2 col-xs-12">
                                       <b> Mobile No :</b>
                                    <asp:TextBox ID="txt_sales_mobile_no" runat="server" MaxLength="10" class="form-control" onKeyPress="return isNumber(event)"></asp:TextBox>

                                    </div>


                                </div>
                                <br />
                                <div class="row">


                                    <div class="col-sm-1 col-xs-12"></div>
                                    <div class="col-sm-2 col-xs-12">
                                      <b>  Challan No :</b>
                          <%--  <asp:Label ID="Label11" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>

                                        <asp:TextBox ID="txt_p_o_no" runat="server" MaxLength="10" class="form-control" onkeypress="return isNumber(event)"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Mode of Transport :</b>

                                    <asp:DropDownList ID="ddl_transport" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                        <asp:ListItem Value="Road">Road</asp:ListItem>
                                        <asp:ListItem Value="Train">Train</asp:ListItem>
                                        <asp:ListItem Value="Airway">Airway</asp:ListItem>
                                        <asp:ListItem Value="Shipping">Shipping</asp:ListItem>

                                    </asp:DropDownList>
                                        <%-- <asp:TextBox ID="ddl_transport" runat="server" MaxLength="20" class="form-control" ></asp:TextBox>--%>
                                    </div>
                                    <div class="col-sm-2 col-xs-12" hidden="hidden">
                                       <b> Freight :</b>
                                    <asp:TextBox ID="txt_freight" runat="server" MaxLength="6" class="form-control" onkeypress="return isNumber(event)">0</asp:TextBox>

                                    </div>

                                    <div class="col-sm-2 col-xs-12">
                                       <b> Vehicle No :</b>
                           <%-- <asp:Label ID="Label13" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>

                                        <asp:TextBox ID="txt_vehicle" runat="server" MaxLength="15" class="form-control" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-2 col-xs-12" style="display: none;">
                                        <span><b>Bill Month :</b> <span style="color: #FF0000">*</span></span>


                                        <asp:DropDownList ID="ddlcalmonth" runat="server" Width="100%" class="form-control">
                                            <asp:ListItem>JAN</asp:ListItem>
                                            <asp:ListItem>FEB</asp:ListItem>
                                            <asp:ListItem>MAR</asp:ListItem>
                                            <asp:ListItem>APR</asp:ListItem>
                                            <asp:ListItem>MAY</asp:ListItem>
                                            <asp:ListItem>JUN</asp:ListItem>
                                            <asp:ListItem>JUL</asp:ListItem>
                                            <asp:ListItem>AUG</asp:ListItem>
                                            <asp:ListItem>SEP</asp:ListItem>
                                            <asp:ListItem>OCT</asp:ListItem>
                                            <asp:ListItem>NOV</asp:ListItem>
                                            <asp:ListItem>DEC</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2" style="display: none;">
                                        <asp:TextBox ID="txt_referenceno2" runat="server" MaxLength="20" CssClass="form-control text_box" onkeypress="return AllowAlphabet1(event,this);"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>Register Office Address :</b>
                                        </span>
                                        <asp:TextBox ID="txt_bill_add" runat="server" Rows="4" MaxLength="200" class="form-control text_box"
                                            TextMode="MultiLine" onkeyup="textAreaAdjust(this)" onkeypress="return AllowAlphabet_address(event,this);" Style="overflow: hidden" ReadOnly="true"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>Branch Address :</b>
                                        </span>
                                        <asp:TextBox ID="txt_ship_add" runat="server" Rows="4" MaxLength="200" class="form-control text_box"
                                            TextMode="MultiLine" onkeyup="textAreaAdjust(this)" onkeypress="return AllowAlphabet1(event,this);" Style="overflow: hidden"></asp:TextBox>

                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-1 col-xs-12"></div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>Narration :</b></span>
                                        <asp:TextBox ID="txt_narration" runat="server" MaxLength="200" class="form-control text_box" onkeypress="return AllowAlphabet1(event,this);"
                                            TextMode="MultiLine" onkeyup="textAreaAdjust(this)" Style="overflow: hidden"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-2 col-xs-12" style="display: none;">
                                       <b> Bill Year :</b>
                <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:TextBox ID="txt_year" runat="server" MaxLength="4" class="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                        <asp:TextBox ID="txt_referenceno1" runat="server" MaxLength="20" CssClass="form-control" Visible="false"></asp:TextBox>

                                    </div>




                                    <%--  <div class="col-sm-2" style="visibility:hidden">
                                <asp:Label ID="lbl_referenceno1" runat="server" Text="Bill Type:"></asp:Label>
                                <asp:TextBox ID="txt_referenceno1" runat="server" MaxLength="20" CssClass="form-control text_box" onkeypress="return AllowAlphabet1(event,this);"></asp:TextBox>
                        </div>--%>
                                </div>


                            </asp:Panel>
                            <%--    </ContentTemplate>
                </asp:UpdatePanel>--%>
                            <br />
                        </div>
                        <br />
                        <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate> vv 22/11--%>

                        <asp:Panel ID="Panel8" runat="server" CssClass="grid-view">
                            <table id="maintable" class="table table-responsive" border="1">
                                <tr style="color: #000; background-color: #D1D0CE; text-align: center">
                                    <th align="center">Item Type</th>

                                    <th align="center">Item Name</th>
                                    <th align="center">POD Number</th>
                                    <th align="center">Employee Name</th>
                                    <th align="center">Uniform Size</th>
                                    <th align="center">Pantry Jacket Size</th>
                                    <th align="center">Apron Size</th>
                                    <th align="center">Shoes Size</th>
                                    <th align="center">Description</th>
                                    <th align="center">GST(%)</th>
                                    <th>
                                        <asp:Label ID="lbl_hsn" runat="server" Text="HSN Code"></asp:Label></th>
                                    <%--   <th align="center" class="length1" id="lbl_hsn1">HSN Code/SAC Code</th>--%>
                                    <%--  <th align="center" class="length2">SAC Code</th>--%>
                                    <th align="center">Unit</th>
                                    <th align="center">Per Unit</th>
                                    <th align="center">Quantity</th>
                                    <th align="center">Sales Rate</th>
                                    <th align="center">Discount Rate (%)</th>
                                    <th align="center">Discount Amount</th>
                                    <th align="center">Total Amount</th>
                                    <th align="center" hidden="hidden">VenDor</th>
                                    <th align="center">Table</th>
                                    <th align="center" hidden="hidden">Valid To</th>
                                    <th align="center" hidden="hidden">Vendor</th>
                                </tr>
                                <tr id="rows">
                                    <td>
                                        <asp:DropDownList ID="ddl_product" runat="server" class="form-control" AutoPostBack="true" Width="70px" OnSelectedIndexChanged="fill_txt_particular">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                            <asp:ListItem Value="chemicals">Chemicals</asp:ListItem>
                                            <asp:ListItem Value="housekeeping_material">Housekeeping Materials</asp:ListItem>
                                            <asp:ListItem Value="Uniform">Uniform</asp:ListItem>
                                            <asp:ListItem Value="Pantry_Jacket">Pantry Jacket</asp:ListItem>
                                            <asp:ListItem Value="Apron">Apron</asp:ListItem>
                                            <asp:ListItem Value="Shoes">Shoes</asp:ListItem>
                                            <asp:ListItem Value="ID_Card">ID_Card</asp:ListItem>
                                            <asp:ListItem Value="Other">Other</asp:ListItem>


                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="txt_particular" runat="server" class="form-control"
                                            OnSelectedIndexChanged="particular_hsn_sac_code" AutoPostBack="true" Width="70px">
                                        </asp:DropDownList>

                                    </td>
                                    <%--vikas13/12--%>
                                    <td>
                                        <asp:TextBox ID="txt_pod_num" ReadOnly="true" runat="server" onkeypress="return AllowAlphabet1(event,this);" CssClass="form-control text_box" Width="10px">0</asp:TextBox>

                                    </td>

                                    <td>
                                        <asp:DropDownList ID="ddl_emp" runat="server" CssClass="form-control" Width="70px" OnSelectedIndexChanged="ddl_emp_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>

                                        <asp:TextBox ID="ddl_uniformsize" runat="server" MaxLength="10" Width="40px" CssClass="form-control text_box"></asp:TextBox>
                                    </td>
                                    <td>

                                        <asp:TextBox ID="ddl_pantry_size" runat="server" MaxLength="10" Width="40px" CssClass="form-control text_box"></asp:TextBox>
                                    </td>
                                    <td>

                                        <asp:TextBox ID="ddl_apron" runat="server" MaxLength="10" Width="40px" CssClass="form-control text_box"></asp:TextBox>
                                    </td>
                                    <td>

                                        <asp:TextBox ID="ddl_shoosesize" runat="server" MaxLength="10" Width="40px" CssClass="form-control text_box"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_desc" runat="server" onkeypress="return AllowAlphabet1(event,this);" TextMode="MultiLine" CssClass="form-control text_box" onkeyup="textAreaAdjust(this)" Style="overflow: hidden"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_description" runat="server" onkeypress="return isNumber_dot(event)" MaxLength="10" CssClass="form-control text_box">0</asp:TextBox>

                                    </td>
                                    <td>

                                        <asp:TextBox ID="txt_hsn" runat="server" onkeypress="return AllowAlphabet1(event,this);" MaxLength="20" CssClass="form-control text_box"></asp:TextBox>

                                    </td>
                                    <%--   <td class="length2">
                                                    <asp:Label ID="lbl_sac" runat="server" Text="SAC Code :"  Visible="false"></asp:Label>
                                                     <asp:TextBox ID="txt_sac" runat="server" onkeypress="return AllowAlphabet1(event,this);"  MaxLength="10" CssClass="form-control text_box"></asp:TextBox>
                           
                                                </td>--%>
                                    <td>
                                        <asp:DropDownList ID="txt_designation" runat="server" CssClass="form-control" OnSelectedIndexChanged="unit_per_price_changes" AutoPostBack="true" Width="70px"></asp:DropDownList>

                                    </td>
                                    <%--  <td>
                                                   <asp:TextBox ID="txt_designation" runat="server" onkeypress="return AllowAlphabet1(event,this);"  MaxLength="10" CssClass="form-control text_box"></asp:TextBox>
                                                
                                                </td>--%>
                                    <td>
                                        <asp:TextBox ID="txt_per_unit" runat="server" class="form-control text_box" onkeypress="return isNumber(event)" OnTextChanged="txt_quantity_TextChanged" AutoPostBack="true" Width="50px" MaxLength="10">0</asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_quantity" runat="server" class="form-control text_box" Width="55px" onkeypress="return isNumber(event)" OnTextChanged="txt_quantity_TextChanged" AutoPostBack="true" MaxLength="10">0</asp:TextBox>
                                        <asp:Label ID="lbl_qty" runat="server" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                        <asp:Label ID="txt_quantity1" Text="In Stock" runat="server" Visible="false" ForeColor="Blue" Font-Size="Small"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_rate" runat="server" AutoPostBack="true" class="form-control text_box" OnTextChanged="txt_rate_TextChanged" onkeypress="return AllowAlphabet_Number(event,this);" MaxLength="10" Width="50px">0</asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_discount_rate" runat="server" onkeypress="return isNumber(event)" AutoPostBack="true" MaxLength="10" CssClass="form-control text_box" OnTextChanged="txt_discount_rate_TextChanged">0</asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_discount_price" runat="server" onkeypress="return isNumber(event)" AutoPostBack="true" MaxLength="10" CssClass="form-control text_box" OnTextChanged="txt_discount_price_TextChanged">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_amount" runat="server" onkeypress="return isNumber_dot(event)" MaxLength="10" CssClass="form-control text_box" ReadOnly="true">0</asp:TextBox>

                                    </td>

                                    <td hidden="hidden">
                                        <asp:DropDownList ID="ddl_vendor" runat="server" onchange="location_hidden();"
                                            onkeypress="return AllowAlphabet1(event,this);" class="form-control text_box"
                                            AutoPostBack="true">
                                            <%--  <asp:ListItem Text="Vendor"></asp:ListItem>--%>
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkbtn_addmoreitem" runat="server" CausesValidation="False"
                                            OnClick="lnkbtn_addmoreitem_Click" OnClientClick="return btn_validation();"><img alt="Add Item"  
                                                        src="Images/add_icon.png"  /></asp:LinkButton>

                                    </td>
                                    <td hidden="hidden">
                                        <asp:TextBox ID="txt_start_date" runat="server" MaxLength="10" CssClass="form-control date-picker1 text_box"></asp:TextBox>

                                    </td>
                                    <td hidden="hidden">
                                        <asp:TextBox ID="txt_end_date" runat="server" MaxLength="10" CssClass="form-control date-picker2 text_box"></asp:TextBox>

                                    </td>
                                </tr>
                            </table>

                        </asp:Panel>
                        <br />
                        <br />
                        <asp:Panel ID="Panel2" runat="server">
                            <asp:Panel ID="Panel4" runat="server" CssClass="grid-view" ScrollBars="Auto">

                                <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    OnRowDataBound="gv_itemslist_RowDataBound"
                                    AutoGenerateColumns="False">

                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />

                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="White" ForeColor="#000066" />

                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" onclick="return R_valid();" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sr No.">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CODE">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_item_code" runat="server" Text='<%# Eval("ITEM_CODE")%>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Type">
                                            <ItemStyle Width="35px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_item_type" runat="server" Text='<%# Eval("item_type")%>' Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Item Name">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_particular" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("PARTICULAR")%>' Width="150px"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--  vikas13/12--%>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="POD Number">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_pod_num" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("POD_NUM")%>' Width="150px"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemStyle Width="35px" />
                                            <ItemTemplate>
                                                <%-- <asp:Label ID="lbl_emp_name" runat="server" Text='<%# Eval("emp_name")%>' Width="35px"></asp:Label>--%>
                                                <asp:TextBox ID="lbl_emp_name" runat="server" ReadOnly="true" Style="text-align: left" class="form-control" Text='<%# Eval("emp_name")%>' Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Uniform Size">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="35px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_uniformsize" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("size_uniform")%>' Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Pantry Size">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="35px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_pantrysize" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("size_pantry")%>' Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Apron Size">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="35px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_apronsize" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("size_apron")%>' Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Shoes Size">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="35px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_shoessize" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("size_shoes")%>' Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Description">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="400px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_Description_final" ReadOnly="True" runat="server" Style="text-align: left" class="form-control grid_desc" onkeyup="textAreaAdjust(this)" TextMode="MultiLine" Text='<%# Eval("Description")%>' Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="GST">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_vat" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("VAT")%>' Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="HSN_Code">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_hsn_code" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("HSN_Code")%>' Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Unit">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="550px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_designation" runat="server" ReadOnly="True" Style="text-align: left" class="form-control" Text='<%# Eval("Designation")%>' Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemStyle Width="70px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_quantity" runat="server" ReadOnly="True" onkeypress="return isNumber(event)" Style="text-align: left" class="form-control" Text='<%# Eval("QUANTITY")%>' AutoPostBack="True" Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Rate">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_rate" runat="server" ReadOnly="True" Style="text-align: left" onkeypress="return isNumber(event)" class="form-control" Text='<%# Eval("RATE")%>' AutoPostBack="True" Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Discount Rate">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_discount" runat="server" ReadOnly="True" Style="text-align: left" onkeypress="return isNumber(event)" class="form-control" Text='<%# Eval("DISCOUNT")%>' AutoPostBack="True" Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Discount Price">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_discount_amt" runat="server" ReadOnly="True" Style="text-align: left" onkeypress="return AllowAlphabet_Number(event,this);" class="form-control" Text='<%# Eval("DISCOUNT_AMT")%>' AutoPostBack="True" Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amount">
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_amount" runat="server" Style="text-align: right" onkeypress="return isNumber(event)" class="form-control" Text='<%# Eval("AMOUNT") %>' ReadOnly="True" Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Date" Visible="false">
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_start_date" runat="server" Style="text-align: right" class="form-control" Text='<%# Eval("START_DATE") %>' ReadOnly="True" Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Date" Visible="false">
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_end_date" runat="server" Style="text-align: right" class="form-control" Text='<%# Eval("END_DATE") %>' ReadOnly="True" Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vendor" Visible="false">
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_vendor" runat="server" Style="text-align: right" class="form-control" Text='<%# Eval("VENDOR") %>' ReadOnly="True" Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Emp Code" Visible="false">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_empcode" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("emp_code")%>' Width="50px"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </asp:Panel>
                        <br />
                        <div class="container-fluid" style="background: white; border-radius: 10px; border: 1px solid white">
                            <br />
                            <div class="row">
                                <div class="col-sm-8 col-xs-12">
                                    <br />

                                    <div class="row">
                                        <div class="col-sm-5 col-xs-12">
                                            <span><b>Customer Notes :</b></span>
                                            <asp:TextBox ID="txt_customer_notes" runat="server" MaxLength="200" Rows="5" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event,this);"
                                                TextMode="MultiLine" onkeyup="textAreaAdjust(this)" Style="overflow: hidden"></asp:TextBox>

                                        </div>
                                        <div class="col-sm-5 col-xs-12">
                                            <span><b>Terms & Conditions :</b></span>
                                            <asp:TextBox ID="txt_terms_conditions" runat="server" MaxLength="200" Rows="5" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event,this);"
                                                TextMode="MultiLine" onkeyup="textAreaAdjust(this)" Style="overflow: hidden"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4 col-xs-12">
                                    <div class="row">

                                        <div class="col-sm-6" style="text-align: right">
                                            <asp:Label ID="lbl_gross_amt" runat="server" Font-Bold="true" Text="Gross Total"></asp:Label>
                                        </div>
                                        <div class="col-sm-6" style="text-align: right">
                                            <asp:TextBox ID="txt_grossamt" runat="server" CssClass="form-control text_box" ReadOnly="true">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-6" style="text-align: right">
                                          <b>  Discount (%)</b>
                                        <asp:TextBox ID="txt_tot_discount_percent" onkeypress="return isNumber_dot(event)" runat="server" AutoPostBack="true" CssClass="form-control text_box" OnTextChanged="txt_tot_discount_percent_TextChanged" MaxLength="5">0</asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                          <b>  Discount (Rs.)</b>
                                        <asp:TextBox ID="txt_tot_discount_amt" onkeypress="return isNumber_dot(event)" runat="server" AutoPostBack="true" CssClass="form-control text_box" OnTextChanged="txt_tot_discount_amt_TextChanged">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-6" style="text-align: right">
                                            <asp:Label ID="lbl_taxable_amt" runat="server" Font-Bold="true" Text="Taxable Amount"></asp:Label>
                                        </div>
                                        <div class="col-sm-6" style="text-align: right">
                                            <asp:TextBox ID="txt_taxable_amt" runat="server" ReadOnly="true" Font-Bold="True" class="form-control"
                                                ForeColor="#339966">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <br />


                                    <div class="row">
                                        <div class="col-sm-5"></div>
                                        <div class="col-sm-7">
                                            <asp:Panel ID="Panel1" runat="server" CssClass="table table-responsive" ScrollBars="Auto" Width="100%">
                                            </asp:Panel>
                                        </div>

                                    </div>







                                    <br />
                                    <div class="row">
                                        <div class="col-sm-6" style="text-align: right">
                                            <asp:Label ID="lbl_sub_total1" runat="server" Font-Bold="true" Text="Sub Total(A)"></asp:Label>
                                        </div>
                                        <div class="col-sm-6" style="text-align: right">
                                            <asp:TextBox ID="txt_sub_total1" runat="server" ReadOnly="true" CssClass="form-control sub_totalA text_box">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-6" style="text-align: right">
                                           <b> Other Charges:</b>
                                <asp:TextBox ID="txt_extra_chrgs" runat="server" CssClass="form-control text_box" onkeypress="return AllowAlphabet1(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6" style="text-align: left">
                                           <b> Other Charges(IN Rs):</b>
                                <asp:TextBox ID="txt_extra_chrgs_amt" runat="server" CssClass="form-control extra_charg text_box" onchange="cal();" onkeypress="return isNumber_dot(event)">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-6" style="text-align: right">
                                           <b> GST(%) On Charges:</b> 
                                <asp:TextBox ID="txt_extra_chrgs_tax" runat="server" CssClass="form-control extra_tax text_box" onchange="cal();" Placeholder="GST(%) on Charges" onkeypress="return isNumber_dot(event)" MaxLength="4">0</asp:TextBox>
                                        </div>
                                        <div class="col-sm-6" style="text-align: left">
                                          <b>  GST(%) On Charges: </b>
                                <asp:TextBox ID="txt_extra_chrgs_tax_amt" ReadOnly="true" runat="server" CssClass="form-control extra_amount text_box">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-6" style="text-align: right">
                                            <asp:Label ID="lbl_sub_total2" runat="server" Font-Bold="true" Text="Sub Total(B)"></asp:Label>
                                        </div>
                                        <div class="col-sm-6" style="text-align: right">
                                            <asp:TextBox ID="txt_sub_total2" runat="server" ReadOnly="true" CssClass="form-control sub_totalB text_box">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-6" style="text-align: right">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Total Amount After Tax(A+B)"></asp:Label>
                                        </div>
                                        <div class="col-sm-6" style="text-align: right">
                                            <asp:TextBox ID="txt_final_total" Enabled="false" runat="server" CssClass="form-control final_total text_box">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <%-- Important for Database Query.. Don't Delete or change --%>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txt_net_total_1" runat="server" Font-Bold="True" class="form-control"
                                                ForeColor="#339966" ReadOnly="True" Visible="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%-- END --%>
                                </div>
                            </div>
                            <%--</ContentTemplate> vv 22/11--%>

                            <%--</asp:UpdatePanel> vv 22/11--%>
                            <br />



                            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btn_save_send"
                                CancelControlID="Button2" BackgroundCssClass="Background">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
                                <iframe style="width: 600px; height: 350px; background-color: #fff;" id="irm1" src="p_uploadfile.aspx" runat="server"></iframe>
                                <div class="row text-center" style="width: 100%;">
                                    <asp:Button ID="Button2" CssClass="btn btn-danger" runat="server" Text="Close" />
                                </div>

                                <br />

                            </asp:Panel>
                            <div class="row text-center">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-7 ">

                                    <asp:Button ID="btn_Save" runat="server" CssClass="btn btn-primary" Width="20%"
                                        OnClick="btn_Save_Click" Text="Save as Draft" OnClientClick=" return Req_validation();" />

                                    <asp:Button ID="btn_save_send" runat="server" CssClass="btn btn-primary"
                                        Text="Send" />
                                    <%--onclick="btn_Save_Click"--%>

                                    <asp:Button ID="btn_update" runat="server" CssClass="btn btn-primary" OnClick="btn_update_Click"
                                        Text="Update" CausesValidation="False" OnClientClick=" return Req_validation();" />

                                    <asp:Button ID="btn_clear" runat="server"
                                        CssClass="btn btn-primary" OnClick="btn_btn_clear" Text="Clear" />

                                    <asp:Button ID="btn_delete" runat="server" CausesValidation="False"
                                        CssClass="btn btn-primary" OnClick="btn_delete_Click" Text="Delete" OnClientClick="return delete_popup();" />

                                    <asp:Button ID="btn_Print" runat="server" CssClass="btn btn-primary"
                                        Text="Print" CausesValidation="False"
                                        OnClick="btn_Print_Click" />

                                    <%-- <asp:Button ID="btn_ShowST" runat="server" CausesValidation="False"
                                OnClick="btn_ShowST_Click" Text="GST Tax Details"
                                CssClass="btn btn-primary" />--%>
                                    <%--    <cc1:ModalPopupExtender ID="btn_ShowST_ModalPopupExtender" runat="server"
                                BackgroundCssClass="Background" DropShadow="true"
                                Enabled="True" PopupControlID="divPopUp"
                                PopupDragHandleControlID="panelDragHandle" TargetControlID="btn_ShowST">
                            </cc1:ModalPopupExtender>--%>
                                    <asp:Button ID="btn_Closelow" runat="server" CausesValidation="False"
                                        CssClass="btn btn-danger" OnClick="btn_Close_Click" Text="Close" />
                                    <asp:Label ID="lbl_print_quote" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                            </div>

                            <br />
                            <%--</div>--%>

                            <%-- </asp:Panel>--%>

                            <div class="Popup" id="divPopUp" runat="server" visible="false">
                                <asp:Panel runat="Server" ID="panelDragHandle" CssClass="drag">
                                    <div class="text-center"><b>Hold here to Drag this Box</b></div>
                                    <br />
                                    <asp:Panel ID="Panel3" runat="server" Visible="true">
                                        <div class="row">

                                            <div class="col-sm-4 col-xs-12 ">
                                              <b>  CGST %</b>

                                                        <asp:TextBox ID="txt_ser_tax_per_pro" runat="server" Style="margin-bottom: 0px"
                                                            AutoPostBack="True" onkeypress="return isNumber_dot(event,this)" class="form-control"
                                                            OnTextChanged="txt_ser_tax_per_pro_TextChanged">9</asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12 ">
                                              <b>  SGST %:</b>
                                                   
                                                        <asp:TextBox ID="txt_bharat_education" runat="server" onkeypress="return isNumber_dot(event,this)" AutoPostBack="True" class="form-control"
                                                            OnTextChanged="txt_ser_tax_per_pro_TextChanged" Style="margin-bottom: 0px">9</asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12 ">
                                              <b>  IGST & UTGST %:</b>
                                                   
                                                        <asp:TextBox ID="txt_igst" runat="server" onkeypress="return isNumber_dot(event,this)" AutoPostBack="True" class="form-control"
                                                            OnTextChanged="txt_ser_tax_per_pro_TextChanged" Style="margin-bottom: 0px">18</asp:TextBox>
                                            </div>
                                            <br />


                                        </div>
                                        <br />
                                        <div class="row text-center">

                                            <asp:Button ID="btnClose" runat="server" Text="OK" class="btn btn-primary" CausesValidation="False" />

                                        </div>
                                    </asp:Panel>


                                </asp:Panel>
                            </div>

                            <table class="style1">

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                    HeaderText="Please Fill Necessary Fields" ShowMessageBox="True"
                                    ShowSummary="False" />

                            </table>
                            <br />
                        </div>
                        <br />
                    </div>

                </div>
            </div>
            <br />

        </asp:Panel>
    </div>


</asp:Content>

