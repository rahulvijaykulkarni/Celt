<%@ Page MaintainScrollPositionOnPostback="true" Title="Purchase Bill" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InventoryPurchaseBill.aspx.cs" Inherits="InventoryPurchaseBill" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Purchase Bill</title>
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

    <title>Purchase Invoice</title>

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



        .tab-section {
            background-color: #fff;
        }

        .grid-view {
            height: auto;
            max-height: 250px;
            overflow: scroll;
        }
    </style>
    <style>
        .container-fluid {
            max-width: 99%;
            font-size: 10px;
            font-weight: lighter;
            font-family: Verdana;
        }

        .label_text {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 10px;
        }

        .text_box {
            margin-top: 7px;
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
            overflow: scroll;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }

        .lstyle {
            overflow: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <style>
        .table th {
            text-align: center;
            border: 2px solid #000;
        }

        .grid-view {
            height: auto;
            max-height: 250px;
            overflow: scroll;
        }



        .form-control {
            display: inline;
        }

        .row {
            margin-right: -15px;
            margin-left: -15px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            hide_show();

        });
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {
            item_type();
            $('#<%=btn_searchvendor.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_bynumber_name.ClientID%>').DataTable({
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

             $('#<%=ddl_vendortype.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_customerlist.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            dropdown_hide_show();
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

            $('.date-picker4').datepicker({
                changeMonth: true,
                changeYear: true,
                maxDate: 0,
                yearRange: "1990:+100",
                showButtonPanel: true,
                dateFormat: 'mm/yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, month, 1));
                }
            });
            $('.date-picker4').focus(function () {
                $(".ui-datepicker-calendar").hide();

            });
           // $(".date-picker").attr("readonly", "true");

            $(".date-picker1").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");
            $(".date-picker3").attr("readonly", "true");
            $(".date-picker4").attr("readonly", "true");
            cal();
            //  location _hidden();
        }

        $(document).ready(function () {
            var evt = null;
            isNumber(evt);

        });


        //function location_hidden() {
        //alert("hello");
        //var location = document.getElementById('<%=txt_particular.ClientID %>');
        //   var SelectedText = location.options[location.selectedIndex].text;

        //    var txt_description = document.getElementById('<%= txt_description.ClientID %>');
        //  var lbl_hsn = document.getElementById('<%= lbl_hsn.ClientID %>');
        //     var txt_hsn = document.getElementById('<%= txt_hsn.ClientID %>');


        //   if (SelectedText == "Select") {
        //
        //            $('.length1').hide();
        //            $('.length2').hide();
        //           $(".js-example-basic-single").select2();
        //       }

        //       else {

        //             $('.length1').show();
        //            $('.length2').show();
        //           $(".js-example-basic-single").select2();

        //       }

        //   }

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

        //  AllowAlphabet_address(event);

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
                if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 36 || charCode > 41) && (charCode < 45 || charCode > 47)) {

                    return false;

                }

            }
            return true;
        }
        function item_type() {
            var ddl_product = document.getElementById('<%=ddl_product.ClientID %>');
            var Selected_ddl_item_type = ddl_product.options[ddl_product.selectedIndex].text;
            if (Selected_ddl_item_type == "Uniform") {
                $(".uniform").show();
                $(".shoes").hide();
                $(".pantry").hide();
                $(".apron").hide();
            }
            else if (Selected_ddl_item_type == "Shoes") {
                $(".uniform").hide();
                $(".pantry").hide();
                $(".apron").hide();
                $(".shoes").show();
            }
            else if (Selected_ddl_item_type == "Pantry Jacket") {
                $(".pantry").show();
                $(".uniform").hide();
                $(".shoes").hide();
                $(".apron").hide();
            }
            else if (Selected_ddl_item_type == "Apron") {
                $(".apron").show();
                $(".uniform").hide();
                $(".shoes").hide();
                $(".pantry").hide();
            }
            else {
                $(".uniform").hide();
                $(".shoes").hide();
                $(".pantry").hide();
                $(".apron").hide();
            }
        }
        function report() {
            var txt_report_month = document.getElementById('<%=txt_report_month.ClientID %>');
            var txt_report_month1 = document.getElementById('<%=txt_report_month1.ClientID %>');
            var ddl_report_vendor = document.getElementById('<%=ddl_report_vendor.ClientID%>');
            var Selected_ddl_report_vendor = ddl_report_vendor.options[ddl_report_vendor.selectedIndex].text;


            if (txt_report_month.value == "")
            {
                alert("Please Select From Bill Month");
                txt_report_month.focus();
                return false;
            }
            if (txt_report_month1.value == "") {
                alert("Please Select To Bill Month");
                txt_report_month1.focus();
                return false;
            }
            if (Selected_ddl_report_vendor=="Select")
            {
                alert("Please Select Vendor Name");
                ddl_report_vendor.focus();
                return false;
            }
            return true;
        }

        function link_valid_btn() {

            var txt_description = document.getElementById('<%=txt_description.ClientID %>');
            var txt_desc = document.getElementById('<%=txt_desc.ClientID%>');
            var ddl_unit = document.getElementById('<%=txt_designation.ClientID%>');
            var select_unit_per = ddl_unit.options[ddl_unit.selectedIndex].text;
            var e_quantity = document.getElementById('<%=txt_quantity.ClientID%>');
            var txt_rate = document.getElementById('<%=txt_rate.ClientID%>');

            var ddl_product = document.getElementById('<%=ddl_product.ClientID%>');
            var Selected_ddl_product = ddl_product.options[ddl_product.selectedIndex].text;

            var ddl_uniformsize = document.getElementById('<%=ddl_uniformsize.ClientID%>');
            var Selected_ddl_uniformsize = ddl_uniformsize.options[ddl_uniformsize.selectedIndex].text;

            var ddl_shoosesize = document.getElementById('<%=ddl_shoosesize.ClientID%>');
            var Selected_ddl_shoosesize = ddl_shoosesize.options[ddl_shoosesize.selectedIndex].text;

            var ddl_vendortype = document.getElementById('<%=ddl_vendortype.ClientID%>');
            var Selected_ddl_vendortype = ddl_vendortype.options[ddl_vendortype.selectedIndex].text;

            if (Selected_ddl_vendortype == "Select") {
                alert("Please Select Vendor Type");
                ddl_vendortype.focus();
                return false;
            }

            if (Selected_ddl_vendortype == "Temporary") {
                var txt_vendorname = document.getElementById('<%=txt_vendorname.ClientID%>');
                if (txt_vendorname.value == "") {
                    alert("Please Enter vendor name");
                    txt_vendorname.focus();
                    return false;
                }
            }

            if (Selected_ddl_vendortype == "Regular") {
                var ddl_customerlist = document.getElementById('<%=ddl_customerlist.ClientID%>');
                var Selected_ddl_customerlist = ddl_customerlist.options[ddl_customerlist.selectedIndex].text;
                if (Selected_ddl_customerlist == "Select") {
                    alert("Please Select Vendor Name");
                    ddl_customerlist.focus();
                    return false;
                }
            }


            var txt_customer_gst = document.getElementById('<%=txt_customer_gst.ClientID %>');
            if (txt_customer_gst.value == "") {
                alert("Please Enter Gst Rate!!!");
                txt_customer_gst.focus();
                return false;
            }

            if (Selected_ddl_product == "Select") {
                alert("Please Select Item Type !!!");
                ddl_product.focus();
                return false;
            }
            var txt_particular = document.getElementById('<%=txt_particular.ClientID%>');
            var Selected_txt_particular = txt_particular.options[txt_particular.selectedIndex].text;
            if (Selected_txt_particular == "Select") {
                alert("Please Select Item Name");
                txt_particular.focus();
                return false;
            }
            if (Selected_ddl_product == "Uniform") {
                if (Selected_ddl_uniformsize == "Select") {
                    alert("Please Select Uniform Size");
                    ddl_uniformsize.focus();
                    return false;
                }
            }
            if (Selected_ddl_product == "Shoes") {
                if (Selected_ddl_shoosesize == "Select") {
                    alert("Please Select Shoes Size");
                    ddl_shoosesize.focus();
                    return false;
                }
            }


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
            if (e_quantity.value == "0" || e_quantity.value == "") {
                alert("Please Enter Quantity  !!!");
                e_quantity.focus();
                return false;
            }
            if (txt_rate.value == "0" || txt_rate.value == "") {
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
            window.open("html/Quotation.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

        $(document).ready(function () {

            $(".js-example-basic-single").select2();

        });
        function hide_show() {
            var ddl_vendortype = document.getElementById('<%=ddl_vendortype.ClientID%>');
            var Selected_ddl_vendortype = ddl_vendortype.options[ddl_vendortype.selectedIndex].text;

            if (Selected_ddl_vendortype == "Regular") {


                $(".t_show").hide();
                $(".ddl_show").show();

            }
            else if (Selected_ddl_vendortype == "Temporary") {

                $(".t_show").show();
                $(".ddl_show").hide();

            }
            else {

                $(".t_show").hide();
                $(".ddl_show").hide();

            }
            return true;
        }
        function dropdown_hide_show() {
            var ddl_product = document.getElementById('<%=ddl_product.ClientID%>');
            var Selected_ddl_product = ddl_product.options[ddl_product.selectedIndex].text;

            var ddl_uniformsize = document.getElementById('<%=ddl_uniformsize.ClientID%>');
            var Selected_ddl_uniformsize = ddl_uniformsize.options[ddl_uniformsize.selectedIndex].text;

            var ddl_shoosesize = document.getElementById('<%=ddl_shoosesize.ClientID%>');
            var Selected_ddl_shoosesize = ddl_shoosesize.options[ddl_shoosesize.selectedIndex].text;

            var ddl_pantry_size = document.getElementById('<%=ddl_pantry_size.ClientID%>');
            var Selected_ddl_pantry_size = ddl_pantry_size.options[ddl_pantry_size.selectedIndex].text;
            var ddl_apron_size = document.getElementById('<%=ddl_apron_size1.ClientID%>');
            var Selected_ddl_apron_size = ddl_apron_size.options[ddl_apron_size.selectedIndex].text;

            if (Selected_ddl_product == "Uniform") {
                ddl_uniformsize.disabled = false;
                ddl_shoosesize.disabled = true;
                ddl_pantry_size.disabled = true;
                ddl_apron_size.disabled = true;
            }
            else if (Selected_ddl_product == "Shoes") {
                ddl_uniformsize.disabled = true;
                ddl_shoosesize.disabled = false;
                ddl_pantry_size.disabled = true;
                ddl_apron_size.disabled = true;

            }
            else if (Selected_ddl_product == "Pantry Jacket") {
                ddl_uniformsize.disabled = true;
                ddl_shoosesize.disabled = true;
                ddl_pantry_size.disabled = false;
                ddl_apron_size.disabled = true;

            }
            else if (Selected_ddl_product == "Apron") {
                ddl_uniformsize.disabled = true;
                ddl_shoosesize.disabled = true;
                ddl_pantry_size.disabled = true;
                ddl_apron_size.disabled = false;

            }
            else {
                ddl_uniformsize.disabled = true;
                ddl_shoosesize.disabled = true;
                ddl_pantry_size.disabled = true;
                ddl_apron_size.disabled = true;
            }

        }
        function r_validation() {

            var ddl_vendortype = document.getElementById('<%=ddl_vendortype.ClientID%>');
            var Selected_ddl_vendortype = ddl_vendortype.options[ddl_vendortype.selectedIndex].text;

            if (Selected_ddl_vendortype == "Select") {
                alert("Please Select Vendor Type");
                ddl_vendortype.focus();
                return false;
            }

            if (Selected_ddl_vendortype == "Temporary") {
                var txt_vendorname = document.getElementById('<%=txt_vendorname.ClientID%>');
                if (txt_vendorname.value == "") {
                    alert("Please Enter vendor name");
                    txt_vendorname.focus();
                    return false;
                }
            }

            var ddl_po_num = document.getElementById('<%=ddl_po_num.ClientID%>');
            var Selected_ddl_po_num = ddl_po_num.options[ddl_po_num.selectedIndex].text;
            if (Selected_ddl_po_num == "Select") {
                swal("Please Select Purchase Order No");
                ddl_po_num.focus();
                return false;
            }
            if (Selected_ddl_vendortype == "Regular") {
                var ddl_customerlist = document.getElementById('<%=ddl_customerlist.ClientID%>');
                    var Selected_ddl_customerlist = ddl_customerlist.options[ddl_customerlist.selectedIndex].text;
                    if (Selected_ddl_customerlist == "Select") {
                        alert("Please Select Vendor Name");
                        ddl_customerlist.focus();
                        return false;
                    }
                }

                var txt_customer_gst = document.getElementById('<%=txt_customer_gst.ClientID %>');
            if (txt_customer_gst.value == "") {
                alert("Please Enter Gst Rate!!!");
                txt_customer_gst.focus();
                return false;
            }



            var txt_grossamt = document.getElementById('<%=txt_grossamt.ClientID %>');
                if (txt_grossamt.value == "0") {
                    alert(" Please Click On the Add Button(Table) to calculate Gross Amount !!!!!!");
                    txt_grossamt.focus();
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
            function R_validation1() {

                var r = confirm("Are you Sure You Want to Final This Invoice");
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
            function R_valid()
            { $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } }); }
    </script>

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="tabs" style="background: #f3f1fe; border: 1px solid #e2e2dd; margin-bottom:25px; margin-top:25px; border-radius:10px">
            <asp:HiddenField ID="hidtab" Value="0" runat="server" />
            <ul>
                <li><a href="#menu1"><b>Purchase Bill</b></a></li>
                <li><a href="#menu2"><b>Monthwise Purchase Repots</b></a></li>

            </ul>
            <div id="menu1">
                <asp:Panel ID="Panel9" runat="server" CssClass="panel panel-primary" Style="background-color: white;">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-9">
                                <div style="text-align: center; color: #fff; font-size: 16px;" class="text-center text-uppercase"><b>PURCHASE BILL</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Purchase Bill Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>

                    <div class="panel-body">
                        <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; margin-top:20px; border: 1px solid white; border: 1px solid white;">
                            <br />
                            <div class="row">
                                <div class="col-sm-1 col-xs-12 "></div>
                                <div class="col-sm-2 col-xs-12 ">
                                  <b>  Purchase Bill Number :</b>
          
                        <asp:TextBox ID="txt_docno_number" runat="server" AutoPostBack="True" class="form-control text_box"
                            OnTextChanged="txt_docno_TextChanged" MaxLength="10" onkeypress="return AllowAlphabet_Number(event,this);">P</asp:TextBox>

                                </div>
                                <div class="col-sm-3 col-xs-12 ">
                                   <b> Vendor Name :</b>
                    <asp:TextBox ID="txt_customername" runat="server" AutoPostBack="True" CssClass="form-control text_box"
                        OnTextChanged="txt_customername_TextChanged" onkeypress="return AllowAlphabet_address(event);" MaxLength="30"></asp:TextBox>

                                </div>
                                <br />
                                <div class="col-sm-3 col-xs-12" style="margin-top: 9px;">

                                    <asp:Button ID="btn_searchvendor" runat="server" CausesValidation="False" CssClass="btn btn-primary" OnClick="btn_searchvendor_Click" Text=" Search " />
                                    <%--   <asp:Button ID="btn_new" runat="server" CausesValidation="False" onclick="btn_new_Click" Text="New" CssClass="btn btn-primary" />--%>
                                    <asp:Button ID="btn_Closeup" runat="server" CausesValidation="False"
                                        CssClass="btn btn-danger" OnClick="btn_Close_Click" Text="Close" />

                                </div>
                            </div>
                            <br />
                            <asp:Panel ID="Panel5" runat="server">
                                <asp:GridView ID="gv_bynumber_name" class="table" runat="server" AutoGenerateColumns="False"
                                    ShowHeader="true" OnRowDataBound="gv_bynumber_name_RowDataBound"
                                    CellPadding="4" ForeColor="#333333" Height="36px" GridLines="Both" OnPreRender="gv_bynumber_name_PreRender">

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
                                        <asp:TemplateField HeaderText="Vendor Name">
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
                                    </Columns>
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                            <br />
                        </div>

                        <br />
                        <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
            <ContentTemplate>--%>
                        <div class="container-fluid" style="background: #f3f1fe; padding:15px 15px 15px 15px; border-radius: 10px; border: 1px solid white; border: 1px solid white">
                            <br />
                            <asp:Panel ID="Panel6" runat="server">
                                <asp:Panel ID="Panel7" runat="server">
                                    <div class="row">
                                        <div class="col-sm-1 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Purchase Bill Number :</b>
                                         <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <asp:TextBox ID="txt_docno" runat="server" CssClass="form-control text_box" MaxLength="10" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <span><b>Vendor Type:</b>
                                            <span style="color: red">*</span>
                                            </span>
                                            <span>

                                                <asp:DropDownList ID="ddl_vendortype" runat="server" MaxLength="30" AutoPostBack="true" OnSelectedIndexChanged="ddl_vendortype_SelectedIndexchange" CssClass="form-control text_box">
                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Value="Regular">Regular</asp:ListItem>
                                                    <asp:ListItem Value="Temporary">Temporary</asp:ListItem>
                                                </asp:DropDownList>

                                            </span>
                                        </div>

                                        <div class="col-sm-2 col-xs-12">
                                            <span>
                                                <asp:Label ID="lbl_vendor" runat="server" Font-Bold="true" Text="Vendor Name:"></asp:Label>
                                                <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </span>
                                            <span>
                                                <asp:DropDownList ID="ddl_customerlist" runat="server" MaxLength="30"
                                                    OnSelectedIndexChanged="customer_details" AutoPostBack="true" CssClass="form-control text_box">
                                                </asp:DropDownList>

                                                <asp:TextBox ID="txt_vendorname" runat="server" CssClass="form-control text_box" Visible="false" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>

                                            </span>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <span><b>Purchase Order No:</b>
                                            <span style="color: red">*</span>
                                            </span>
                                            <asp:DropDownList ID="ddl_po_num" runat="server" MaxLength="30"
                                                OnSelectedIndexChanged="ddl_po_num_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control text_box">
                                            </asp:DropDownList>
                                        </div>

                                        <%-- <div class="col-sm-2 col-xs-12 ddl_show" style="display:none">--%>
                                        <%--<span>Vendor Name:
                                             <asp:Label ID="Label9" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </span>
                                        --%>                                       <span>
                                            <%-- <asp:TextBox ID="ddl_customerlist" runat="server" onkeypress="return AllowAlphabet1(event,this);" MaxLength="30" CssClass="form-control text_box">
                    </asp:TextBox>--%>
                                            

                                        </span>
                                        <%--</div>--%>

                                        <div class="col-sm-2 col-xs-12">
                                            <span><b>Vendor GST Number:</b><span style="color: red">*</span>


                                                <span>
                                                    <asp:TextBox ID="txt_customer_gst" runat="server" MaxLength="30" CssClass="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)">
                                                    </asp:TextBox>
                                                </span>
                                        </div>

                                        <div class="col-sm-2 col-xs-12" style="display: none">
                                          <b>  Due Date : </b>
                                            <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <asp:TextBox ID="txt_expiry_date" runat="server" Style="margin-bottom: 0px" class="form-control date-picker2 text_box"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2" style="display: none">
                                            <asp:Label ID="lbl_referenceno2" runat="server" Text="Reference #:"></asp:Label>
                                            <asp:TextBox ID="txt_referenceno2" runat="server" MaxLength="20" CssClass="form-control text_box" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>
                                        </div>


                                        <%-- <asp:TextBox ID="ddl_transport" runat="server" MaxLength="20" class="form-control" ></asp:TextBox>--%>
                                    </div>
                                </asp:Panel>
                                <br />
                                <div class="row">
                                    <div class="col-sm-1 col-xs-12"></div>
                                    <div class="col-sm-2 col-xs-12">
                                      <b>  Invoice Date : </b>

                                        <asp:TextBox ID="txt_docdate" runat="server" Style="margin-bottom: 0px" class="form-control date-picker3 text_box"></asp:TextBox>
                                    </div>


                                    <div class="col-sm-2 col-xs-12">
                                     <b>   Mode of Transport :</b>
                                    <asp:DropDownList ID="ddl_transport" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                        <asp:ListItem Value="Road">Road</asp:ListItem>
                                        <asp:ListItem Value="Train">Train</asp:ListItem>
                                        <asp:ListItem Value="Airway">Airway</asp:ListItem>
                                        <asp:ListItem Value="Shipping">Shipping</asp:ListItem>

                                    </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                     <b>   Challan No :</b>
                                 

                                    <asp:TextBox ID="txt_p_o_no" runat="server" MaxLength="10" class="form-control" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>

                                    </div>

                                    <div class="col-sm-2 col-xs-12" hidden="hidden">
                                       <b> Freight :</b>
                                    <asp:TextBox ID="txt_freight" runat="server" MaxLength="6" class="form-control" onkeypress="return isNumber_dot(event)"></asp:TextBox>

                                    </div>

                                    <div class="col-sm-2 col-xs-12">
                                      <b>  Vehicle No :</b>
                                   <%--  <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                   --%>
                                        <asp:TextBox ID="txt_vehicle" runat="server" MaxLength="15" class="form-control" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>

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


                                    <div class="col-sm-2 col-xs-12" style="display: none;">
                                      <b>  Category :</b>
                                 <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:DropDownList ID="ddlcategory" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Original">Original</asp:ListItem>
                                            <asp:ListItem Value="Duplicate">Duplicate</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Purchase Person :"></asp:Label>
                                        <asp:TextBox ID="ddl_sales_person" runat="server" MaxLength="70" Class="form-control text_box" onkeypress="return AllowAlphabet1(event)"></asp:TextBox>

                                    </div>


                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-1"></div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Purchase Person Mobile No :"></asp:Label>
                                        <asp:TextBox ID="txtsalesmobileno" runat="server" MaxLength="10" Class="form-control text_box" onkeypress="return isNumber(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>Bill Address :</b></span>
                                        <asp:TextBox ID="txt_bill_add" runat="server" MaxLength="200" class="form-control text_box" Rows="6" onKeyPress="return AllowAlphabet_Number(event)"
                                            TextMode="MultiLine" onkeyup="textAreaAdjust(this)" Style="overflow: hidden" ReadOnly="true"></asp:TextBox>


                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>Shipping Address :</b></span>
                                        <asp:TextBox ID="txt_ship_add" runat="server" MaxLength="200" class="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)"
                                            TextMode="MultiLine" onkeyup="textAreaAdjust(this)" Style="overflow: hidden" Rows="6"></asp:TextBox>


                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>Narration :</b></span>
                                        <asp:TextBox ID="txt_narration" runat="server" MaxLength="200" class="form-control text_box" onkeypress="return AllowAlphabet1(event,this);"
                                            TextMode="MultiLine" onkeyup="textAreaAdjust(this)" Style="overflow: hidden"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>Bank A/C Name:</b></span>
                                        <asp:TextBox ID="txt_bank_ac" runat="server" MaxLength="30" CssClass="form-control text_box" onkeypress="return AllowAlphabet1(event)">
                                        </asp:TextBox>

                                    </div>




                                </div>
                                <br />
                               <div class="row">
                                    <div class="col-sm-1"></div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="Label3" runat="server" Font-Bold="true" Text=" Shiping Compay Name"></asp:Label>
                                        <asp:TextBox ID="txt_ship" runat="server"  Class="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)" ></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>Shiping company Address :</b></span>
                                        <asp:TextBox ID="txt_ship_address" runat="server" MaxLength="200" class="form-control text_box" Rows="6" onKeyPress="return AllowAlphabet_Number(event)"
                                            TextMode="MultiLine" onkeyup="textAreaAdjust(this)" Style="overflow: hidden" ></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="true" Text=" Shiping Compay GST NO"></asp:Label>
                                        <asp:TextBox ID="txt_ship_gst_no" runat="server"  Class="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)" ></asp:TextBox>
                                    </div>
                                   <div class="col-sm-2">
                                      <b> Shiping State :</b>
                                    <asp:DropDownList ID="ddl_shiping_state" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    </div>
                                   <div class="col-sm-2">
                                        <asp:Label ID="Label8" runat="server" Font-Bold="true" Text="Vendor Invoice No:"></asp:Label>
                                        <asp:TextBox ID="txt_vendor_no" runat="server"  Class="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)" ></asp:TextBox>
                                    </div>
                               </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-1 col-xs-12"></div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>Bank A/C No:</b></span>
                                        <asp:TextBox ID="txt_bank_no" runat="server" MaxLength="30" CssClass="form-control text_box" onkeypress="return isNumber(event)">
                                        </asp:TextBox>

                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>IFSC Code:</b></span>
                                        <asp:TextBox ID="txt_ifc_code" runat="server" MaxLength="30" CssClass="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)">
                                        </asp:TextBox>

                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>Credit Period:</b></span>
                                        <asp:TextBox ID="txt_credit_perod" runat="server" MaxLength="30" CssClass="form-control text_box" onkeypress="return isNumber(event)"> 0 </asp:TextBox>


                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>Balance PO Value:</b></span>
                                        <asp:TextBox ID="txt_balance_op" runat="server" MaxLength="30" CssClass="form-control text_box" ReadOnly="true" onkeypress="return isNumber(event)"> 0 </asp:TextBox>


                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span><b>Bill No Against PO:</b></span>
                                        <asp:DropDownList ID="ddl_invoice_no" runat="server" Width="100%" class="form-control" />

                                    </div>
                                    <div class="col-sm-2 col-xs-12" style="display: none;">
                                       <b> Bill Year :</b>
                                    <asp:TextBox ID="txt_year" runat="server" MaxLength="4" class="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                        <asp:TextBox ID="txt_referenceno1" runat="server" MaxLength="20" CssClass="form-control" Visible="false"></asp:TextBox>

                                    </div>
                                </div>


                            </asp:Panel>
                            <br />
                        </div>
                        <br />
                        <%-- </ContentTemplate>
                </asp:UpdatePanel>
                <hr />--%>
                        <br />
                        <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white; border: 1px solid white">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel8" runat="server" CssClass="grid-view">
                                        <table id="maintable" class="table table-striped" border="1" style="border-color: #d8d6d6">
                                            <tr style="color: #000; background-color: #D1D0CE; text-align: center">
                                                <th align="center">Item Type</th>
                                                <th align="center">Item Name</th>
                                                <th style="display: none;" class="uniform">Uniform Size</th>
                                                <th style="display: none;" class="shoes">Shoes Size</th>
                                                <th style="display: none;" class="pantry">Pantry Jacket Size</th>
                                                <th style="display: none;" class="apron">Apron Size</th>
                                                <th align="center">Description</th>
                                                <th align="center">GST(%)</th>
                                                <th>
                                                    <asp:Label ID="lbl_hsn" runat="server" Text="HSN Code"></asp:Label></th>
                                                <%--   <th align="center" class="length1" id="lbl_hsn1">HSN Code/SAC Code</th>--%>
                                                <%--  <th align="center" class="length2">SAC Code</th>--%>
                                                <th align="center">Unit</th>
                                                <th align="center">Per Unit</th>
                                                <th align="center">Quantity</th>
                                                <th align="center">Purchase Rate</th>
                                                <th align="center">Discount Rate (%)</th>
                                                <th align="center">Discount Amount</th>
                                                <th align="center">Total Amount</th>

                                                <th align="center" hidden="hidden">Customer</th>
                                                <th align="center">Table</th>
                                                <th align="center" hidden="hidden">Valid From</th>
                                                <th align="center" hidden="hidden">Valid To</th>

                                            </tr>
                                            <tr id="rows">
                                                <td>
                                                    <asp:DropDownList ID="ddl_product" runat="server" class="form-control text_box" Style="width: 70px;" OnSelectedIndexChanged="fill_txt_particular" AutoPostBack="true" onchange="item_type();">
                                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="chemicals">Chemicals</asp:ListItem>
                                                        <asp:ListItem Value="housekeeping_material">Housekeeping Materials</asp:ListItem>
                                                        <asp:ListItem Value="Shoes">Shoes</asp:ListItem>
                                                        <asp:ListItem Value="Uniform">Uniform</asp:ListItem>
                                                        <asp:ListItem Value="pantry_jacket">Pantry Jacket</asp:ListItem>
                                                        <asp:ListItem Value="Apron">Apron</asp:ListItem>
                                                        <asp:ListItem Value="ID_Card">ID Card</asp:ListItem>
                                                        <asp:ListItem Value="Other">Other</asp:ListItem>


                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="txt_particular" runat="server"
                                                        onkeypress="return AllowAlphabet1(event,this);" class="form-control text_box" Width="70px"
                                                        OnSelectedIndexChanged="particular_hsn_sac_code" AutoPostBack="true">
                                                    </asp:DropDownList>

                                                </td>
                                                <td id="unitform" runat="server" style="display: none;" class="uniform">
                                                    <asp:DropDownList ID="ddl_uniformsize" runat="server" class="form-control text_box" Style="width: 70px;">
                                                        <asp:ListItem Value="Select">Select</asp:ListItem>




                                                    </asp:DropDownList>
                                                </td>
                                                <td style="display: none;" class="shoes">
                                                    <asp:DropDownList ID="ddl_shoosesize" runat="server" class="form-control text_box" Style="width: 70px;">
                                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                                        <%--<asp:ListItem Value="chemicals">6</asp:ListItem>
                                              <asp:ListItem Value="housekeeping_material">7</asp:ListItem>
                                                <asp:ListItem Value="Other">8</asp:ListItem>
                                             <asp:ListItem Value="Other">9</asp:ListItem>
                                             <asp:ListItem Value="Other">10</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="display: none;" class="pantry">
                                                    <asp:DropDownList ID="ddl_pantry_size" runat="server" class="form-control text_box" Style="width: 70px;">
                                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="display: none;" class="apron">
                                                    <asp:DropDownList ID="ddl_apron_size1" runat="server" class="form-control text_box" Style="width: 70px;">
                                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_desc" runat="server" Width="70px" onkeypress="return AllowAlphabet1(event,this);" CssClass="form-control text_box" TextMode="MultiLine" onkeyup="textAreaAdjust(this)" Style="overflow: hidden"></asp:TextBox>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_description" runat="server" onkeypress="return isNumber_dot(event)" MaxLength="10" CssClass="form-control text_box">0</asp:TextBox>

                                                </td>
                                                <td>

                                                    <asp:TextBox ID="txt_hsn" runat="server" onkeypress="return AllowAlphabet_Number(event,this);" MaxLength="10" CssClass="form-control text_box"></asp:TextBox>

                                                </td>
                                                <%--   <td class="length2">
                                                    <asp:Label ID="lbl_sac" runat="server" Text="SAC Code :"  Visible="false"></asp:Label>
                                                     <asp:TextBox ID="txt_sac" runat="server" onkeypress="return AllowAlphabet1(event,this);"  MaxLength="10" CssClass="form-control text_box"></asp:TextBox>
                           
                                                </td>--%>
                                                <td>

                                                    <asp:DropDownList ID="txt_designation" runat="server" Width="70px" CssClass="form-control text_box" OnSelectedIndexChanged="unit_per_price_changes" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <%--  <td>
                                                   <asp:TextBox ID="txt_designation" runat="server" onkeypress="return AllowAlphabet1(event,this);"  MaxLength="10" CssClass="form-control text_box"></asp:TextBox>
                                                
                                                </td>--%>
                                                <td>
                                                    <asp:TextBox ID="txt_per_unit" runat="server" class="form-control text_box" onkeypress="return isNumber(event)" OnTextChanged="txt_quantity_TextChanged" AutoPostBack="true" MaxLength="10">0</asp:TextBox>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_quantity" runat="server" class="form-control text_box" onkeypress="return isNumber(event)" OnTextChanged="txt_quantity_TextChanged" AutoPostBack="true" MaxLength="10">0</asp:TextBox>
                                                    <asp:Label ID="lbl_qty" runat="server" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                                    <asp:Label ID="txt_quantity1" Text="In Stock" runat="server" Visible="false" ForeColor="Blue" Font-Size="Small"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_rate" runat="server" onkeypress="return isNumber_dot(event)" AutoPostBack="true" class="form-control text_box" OnTextChanged="txt_rate_TextChanged" MaxLength="10">0</asp:TextBox>
                                                    <asp:Label ID="lbl_raten" runat="server" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                                    <asp:Label ID="lbl_rete1" Text="Last Purchase Rate" runat="server" Visible="false" ForeColor="Blue" Font-Size="Small"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_discount_rate" runat="server" AutoPostBack="true" onkeypress="return isNumber_dot(event)" MaxLength="10" CssClass="form-control text_box" OnTextChanged="txt_discount_rate_TextChanged">0</asp:TextBox>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_discount_price" runat="server" AutoPostBack="true" onkeypress="return isNumber_dot(event)" MaxLength="10" CssClass="form-control text_box" OnTextChanged="txt_discount_price_TextChanged">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_amount" runat="server" onkeypress="return isNumber_dot(event)" MaxLength="10" CssClass="form-control text_box" ReadOnly="true">0</asp:TextBox>

                                                </td>

                                                <td hidden="hidden">
                                                    <asp:DropDownList ID="ddl_vendor" runat="server"
                                                        onkeypress="return AllowAlphabet1(event,this);" class="form-control text_box"
                                                        Width="70px">
                                                        <%--  <asp:ListItem Text="Vendor"></asp:ListItem>--%>
                                                    </asp:DropDownList>

                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkbtn_addmoreitem" runat="server"
                                                        OnClick="lnkbtn_addmoreitem_Click" OnClientClick="return link_valid_btn();">
                                                    <img alt="Add Item" src="Images/add_icon.png"  />
                                                    </asp:LinkButton>

                                                </td>
                                                <td hidden="hidden">
                                                    <asp:TextBox ID="txt_start_date" runat="server" Width="100px" MaxLength="10" CssClass="form-control date-picker1 text_box"></asp:TextBox>

                                                </td>
                                                <td hidden="hidden">
                                                    <asp:TextBox ID="txt_end_date" runat="server" Width="100px" MaxLength="10" CssClass="form-control date-picker2 text_box"></asp:TextBox>

                                                </td>
                                            </tr>
                                        </table>

                                    </asp:Panel>
                                    <br />
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:Panel ID="Panel4" runat="server" CssClass="grid-view" ScrollBars="Auto">

                                            <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                OnRowDataBound="gv_itemslist_RowDataBound"
                                                AutoGenerateColumns="False">

                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="White" ForeColor="#000066" />

                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemStyle Width="20px" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
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
                                                        <ItemStyle Width="85px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_item_type" runat="server" Text='<%# Eval("item_type")%>' Width="155px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Particular">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_particular" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("PARTICULAR")%>' Width="150px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Uniform Size">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="35px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_uniformsize" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("size_uniform")%>' Width="150px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Shoes Size">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="35px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_shoessize" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("size_shoes")%>' Width="150px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="pantry_jacket Size">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="35px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_pantrysize" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("size_pantry")%>' Width="150px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Apron Size">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="35px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_apronsize" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("size_apron")%>' Width="150px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Description">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="400px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_Description_final" ReadOnly="True" TextMode="MultiLine" runat="server" Style="text-align: left" class="form-control lstyle" Text='<%# Eval("Description")%>' onkeyup="textAreaAdjust(this)"></asp:TextBox>
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
                                                            <asp:TextBox ID="lbl_quantity" runat="server" ReadOnly="True" onkeypress="return isNumber_dot(event)" Style="text-align: left" class="form-control" Text='<%# Eval("QUANTITY")%>' AutoPostBack="True" Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Rate">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="90px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_rate" runat="server" ReadOnly="True" Style="text-align: left" onkeypress="return isNumber_dot(event)" class="form-control" Text='<%# Eval("RATE")%>' AutoPostBack="True" Width="90px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Discount Rate">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="90px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_discount" runat="server" ReadOnly="True" Style="text-align: left" onkeypress="return isNumber_dot(event)" class="form-control" Text='<%# Eval("DISCOUNT")%>' AutoPostBack="True" Width="90px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Discount Price">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="90px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_discount_amt" runat="server" ReadOnly="True" Style="text-align: left" onkeypress="return isNumber_dot(event)" class="form-control" Text='<%# Eval("DISCOUNT_AMT")%>' AutoPostBack="True" Width="90px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Amount">
                                                        <ItemStyle Width="90px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_amount" runat="server" Style="text-align: right" class="form-control" Text='<%# Eval("AMOUNT") %>' ReadOnly="True" Width="90px"></asp:TextBox>
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
                                                    <asp:TemplateField HeaderText="Customer" Visible="false">
                                                        <ItemStyle Width="90px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_vendor" runat="server" Style="text-align: right" class="form-control" Text='<%# Eval("VENDOR") %>' ReadOnly="True" Width="90px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </asp:Panel>

                                    <br />
                                    <hr />
                                    <div class="row">

                                        <div class="col-sm-8 col-xs-12">
                                            <div class="row">
                                                <div class="col-sm-5 col-xs-12">
                                                    <span><b>Customer Notes :</b></span>
                                                    <asp:TextBox ID="txt_customer_notes" runat="server" MaxLength="200" class="form-control text_box" onkeypress="return AllowAlphabet1(event,this);"
                                                        TextMode="MultiLine" Rows="6" onkeyup="textAreaAdjust(this)" Style="overflow: hidden;"></asp:TextBox>

                                                </div>
                                                <div class="col-sm-5 col-xs-12">
                                                    <span><b>Terms & Conditions :</b></span>
                                                    <asp:TextBox ID="txt_terms_conditions" runat="server" MaxLength="200" class="form-control text_box" onkeypress="return AllowAlphabet1(event,this);"
                                                        TextMode="MultiLine" onkeyup="textAreaAdjust(this)" Rows="6" Style="overflow: hidden"></asp:TextBox>

                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-sm-4 col-xs-12">
                                            <div class="row">
                                                <div class="col-sm-6" style="text-align: right">
                                                    <asp:Label ID="lbl_gross_amt" runat="server" Font-Bold="true" Text="Gross Amount"></asp:Label>
                                                </div>
                                                <div class="col-sm-6" style="text-align: right">
                                                    <asp:TextBox ID="txt_grossamt" runat="server" ReadOnly="True" CssClass="form-control text_box">0</asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-6" style="text-align: right">
                                                   <b> Discount (%)</b>
                                <asp:TextBox ID="txt_tot_discount_percent" onkeypress="return isNumber(event)" runat="server" AutoPostBack="true" CssClass="form-control text_box" OnTextChanged="txt_tot_discount_percent_TextChanged">0</asp:TextBox>
                                                </div>

                                                <div class="col-sm-6">
                                                  <b>  Discount (Rs.)</b>
                                <asp:TextBox ID="txt_tot_discount_amt" runat="server" onkeypress="return isNumber(event)" AutoPostBack="true" CssClass="form-control text_box" OnTextChanged="txt_tot_discount_amt_TextChanged">0</asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-6" style="text-align: right">

                                                    <asp:Label ID="lbl_taxable_amt" runat="server" Font-Bold="true" Text="Taxable Amount"></asp:Label>
                                                </div>
                                                <div class="col-sm-6" style="text-align: right">
                                                    <asp:TextBox ID="txt_taxable_amt" runat="server" Font-Bold="True" class="form-control" onkeypress="return isNumber(event)"
                                                        ForeColor="#339966">0</asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-5"></div>
                                                <div class="col-sm-7">
                                                    <asp:Panel ID="Panel1" runat="server" CssClass="table table-responsive" ScrollBars="Auto"
                                                        Width="100%">
                                                    </asp:Panel>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-sm-6" style="text-align: right">

                                                    <asp:Label ID="lbl_sub_total1" runat="server" Font-Bold="true" Text="Sub Total(A)" onkeypress="return isNumber(event)"></asp:Label>
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
                                <asp:TextBox ID="txt_extra_chrgs_amt" runat="server" CssClass="form-control extra_charg text_box" onchange="cal();" onkeypress="return isNumber(event)">0</asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-6" style="text-align: right">
                                                   <b> Other Charges GST(IN %):</b>
                                <asp:TextBox ID="txt_extra_chrgs_tax" runat="server" CssClass="form-control extra_tax text_box" onchange="cal();" Placeholder="GST(%) on Charges">0</asp:TextBox>
                                                </div>
                                                <div class="col-sm-6" style="text-align: left">
                                                   <b> Total Other Charge(IN RS.):</b>
                                <asp:TextBox ID="txt_extra_chrgs_tax_amt" runat="server" CssClass="form-control extra_amount text_box" Enabled="false">0</asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-6" style="text-align: right">

                                                    <asp:Label ID="lbl_sub_total2" runat="server" Font-Bold="true" Text="Sub Total(B)"></asp:Label>
                                                </div>
                                                <div class="col-sm-6" style="text-align: right">
                                                    <asp:TextBox ID="txt_sub_total2" runat="server" ReadOnly="True" CssClass="form-control sub_totalB text_box">0</asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-6" style="text-align: right">

                                                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Total Amount After Tax"></asp:Label>
                                                </div>
                                                <div class="col-sm-6" style="text-align: right">
                                                    <asp:TextBox ID="txt_final_total" runat="server" Enabled="False" CssClass="form-control final_total text_box">0</asp:TextBox>
                                                </div>
                                            </div>
                                            <%-- Important for Database Query.. Don't Delete or change --%>
                                            <div class="row">
                                                <div class="col-sm-6 " style="text-align: right">

                                                    <asp:TextBox ID="txt_net_total_1" runat="server" Font-Bold="True" class="form-control"
                                                        ForeColor="#339966" ReadOnly="True" Visible="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- END --%>
                                        <br />
                                    </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>
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

                                    <asp:Button ID="btn_Save" runat="server" CssClass="btn btn-primary"
                                        OnClick="btn_Save_Click" Text="Save as Draft" OnClientClick=" return r_validation();" />

                                    <asp:Button ID="btn_save_send" runat="server" CssClass="btn btn-primary"
                                        Text="Send" />
                                    <%--onclick="btn_Save_Click"--%>

                                    <asp:Button ID="btn_update" runat="server" CssClass="btn btn-primary" OnClick="btn_update_Click"
                                        Text="Update" CausesValidation="False" OnClientClick=" return r_validation();" />

                                    <asp:Button ID="btn_delete" runat="server" CausesValidation="False"
                                        CssClass="btn btn-primary" OnClick="btn_delete_Click" Text="Delete" OnClientClick="return R_validation();" />

                                    <asp:Button ID="btn_Print" runat="server" CssClass="btn btn-primary"
                                        Text="Print" CausesValidation="False"
                                        OnClick="btn_Print_Click" />

                                     <asp:Button ID="btn_final" runat="server" CausesValidation="False"  Visible ="false"
                                        CssClass="btn btn-primary" OnClick="btn_final_Click" Text="Final Invoice" OnClientClick="return R_validation1();" />

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
                            <br />
                        </div>
                        <br />

                        <%--</ContentTemplate>
             <Triggers>
                        <asp:PostBackTrigger ControlID="lnkbtn_addmoreitem" />
                 <asp:PostBackTrigger ControlID="btn_Print" />
                    </Triggers>
        </asp:UpdatePanel>--%>
                        <div class="Popup" id="divPopUp" runat="server" visible="false">
                            <asp:Panel runat="Server" ID="panelDragHandle" CssClass="drag">
                                <div class="text-center"><b>Hold here to Drag this Box</b></div>
                                <br />
                                <asp:Panel ID="Panel3" runat="server" Visible="true">
                                    <div class="row">

                                        <div class="col-sm-4 col-xs-12 ">
                                          <b>  CGST %</b>

                                                        <asp:TextBox ID="txt_ser_tax_per_pro" runat="server" Style="margin-bottom: 0px"
                                                            AutoPostBack="True" onkeypress="return isNumber_dot(event)" class="form-control"
                                                            OnTextChanged="txt_ser_tax_per_pro_TextChanged">9</asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-xs-12 ">
                                           <b> SGST %:</b>
                                                   
                                                        <asp:TextBox ID="txt_bharat_education" runat="server" onkeypress="return isNumber_dot(event)" AutoPostBack="True" class="form-control"
                                                            OnTextChanged="txt_ser_tax_per_pro_TextChanged" Style="margin-bottom: 0px">9</asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-xs-12 ">
                                          <b>  IGST & UTGST %:</b>
                                                   
                                                        <asp:TextBox ID="txt_igst" runat="server" onkeypress="return isNumber_dot(event)" AutoPostBack="True" class="form-control"
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

                    </div>
                </asp:Panel>
            </div>
            <div id="menu2">
                <br />
                <div class="row">
                    <div class="col-sm-2 col-xs-12">
                    <b>  From Bill Month:</b>
                          <asp:TextBox ID="txt_report_month" runat="server" Style="margin-bottom: 0px" class="form-control date-picker4 text_box"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                     <b> To Bill Month:</b>
                          <asp:TextBox ID="txt_report_month1" runat="server" Style="margin-bottom: 0px" class="form-control date-picker4 text_box"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12 ">
                      <b>  Vendor Name :</b>
                      <asp:DropDownList ID="ddl_report_vendor" runat="server" MaxLength="30"
                          OnSelectedIndexChanged="ddl_report_vendor_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control text_box">
                      </asp:DropDownList>

                    </div>
                    <div class="col-sm-2 col-xs-12">
                        <span><b>Purchase Order No:</b>
                                            <span style="color: red">*</span>
                        </span>
                        <asp:DropDownList ID="ddl_report_vendor_po" runat="server" MaxLength="30"
                            AutoPostBack="true" CssClass="form-control text_box" OnSelectedIndexChanged="ddl_report_vendor_po_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        <span><b>Invoice no:</b>
                                            <span style="color: red">*</span>
                        </span>
                        <asp:DropDownList ID="ddl_invoice_report" runat="server" MaxLength="30"
                            AutoPostBack="true" CssClass="form-control text_box">
                        </asp:DropDownList>
                    </div>
                      <div class="col-sm-2 col-xs-12" style="margin-top: 25px;">

                                    <asp:Button ID="btn_report" runat="server" CausesValidation="False" CssClass="btn btn-primary" OnClick="btn_report_Click" Text=" Report " OnClientClick="return report();" />
                                   

                                </div>
                    <br />
                    <br />

                </div>
                <br />
            </div>
        </div>


    </div>


</asp:Content>

