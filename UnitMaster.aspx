<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UnitMaster.aspx.cs" Inherits="UnitDetails" Title="Branch Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Branch Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_header" runat="Server">

    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta charset="utf-8" />


    <!-- Contain the script binding the form submit event -->
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


    <script type="text/javascript">


        $(function () {
            $("#dialog").dialog({

                autoOpen: false,
                modal: true,
                height: 500,
                width: 500,
                title: "Zoomed Image",
                ForeColor: "#004C99",
               
                buttons: [{ text: "Close", click: function () { $(this).dialog("close") } }],
            });
          
            $("[id*=fire_upload]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(400));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
           
        });





        $("[id*=fire_report]").click(function () {
            $('#dialog').html('');
            $('#dialog').append($(this).clone().width(470).height(400));
            $('#dialog').dialog('open');
            //height:200;
            //width: 200;
        });



        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });
        function unblock() {
            $.unblockUI();
        }

        $(".date-picker1").datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            dateFormat: 'dd/mm/yy',
            yearRange: '1950',
            onSelect: function (selected) {
                $(".date-picker2").datepicker("option", "minDate", selected)
            }
        });

        $(document).ready(function () {
            $('#<%=UnitGridView.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddlunitclient.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=txt_zone1.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_sendemail_type.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddlunitclient1.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_designation.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            var t = false;

            $(".text_box1").focus(function () {
                var $this = $(this)

                t = setInterval(

                function () {
                    if (($this.val() < 1) && $this.val().length != 0) {
                        if ($this.val() < 1) {

                            $this.val("")
                        }

                        if ($this.val() > 24) {
                            $this.val("")
                        }

                    }
                }, 50)
            })

            $(".text_box1").blur(function () {
                if (t != false) {
                    window.clearInterval(t)
                    t = false;
                }
            })
            var t = false

            $(".text_box2").focus(function () {
                var $this = $(this)

                t = setInterval(

                function () {
                    if (($this.val() < 1) && $this.val().length != 0) {
                        if ($this.val() < 1) {

                            $this.val("")
                        }

                        if ($this.val() > 24) {
                            $this.val("")
                        }

                    }
                }, 50)
            })

            $(".text_box2").blur(function () {
                if (t != false) {
                    window.clearInterval(t)
                    t = false;
                }
            })

        });
    </script>

    <style>
        .grid-view {
            height: auto;
            max-height: 500px;
            overflow-x: hidden;
            overflow-y: auto;
        }

        .grid {
            height: auto;
            max-height: 200px;
            overflow-x: hidden;
            overflow-y: auto;
        }
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


        h5 {
            font-size: 15px;
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
            padding-top: 5px;
            padding-left: 5px;
            padding-right: 5px;
            z-index: 101;
        }

        .text-red {
            color: #f00;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <script>

        $(function () {

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_start_end_date_details.ClientID%>').DataTable({
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
               .appendTo('#<%=gv_start_end_date_details.ClientID%>_wrapper .col-sm-6:eq(0)');




        });

        function pageLoad() {
            hide_company_tab();
            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                maxDate: 0,
                yearRange: '1950',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });

            $(".date-picker").attr("readonly", "true");
            $(function () {



                $('body').on('keyup', '.maskedExt', function () {
                    var num = $(this).attr("maskedFormat").toString().split(',');
                    var regex = new RegExp("^\\d{0," + num[0] + "}(\\.\\d{0," + num[1] + "})?$");
                    if (!regex.test(this.value)) {
                        this.value = this.value.substring(0, this.value.length - 1);
                    }
                });
            });

            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                minDate: document.getElementById('<%=Hidden1.ClientID %>').value,
                dateFormat: 'dd/mm/yy',
                yearRange: '1950',
                onSelect: function (selected) {
                    $(".date-picker2").datepicker("option", "minDate", selected)
                }
            });


            $(".date-picker2").datepicker({
                changeMonth: true,
                changeYear: true,
                maxDate: document.getElementById('<%=Hidden2.ClientID %>').value,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    $(".date-picker1").datepicker("option", "maxDate", selected)
                }
            });
            $(".date-picker1").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");


            var table = $('#<%=UnitGridView.ClientID%>').DataTable({
                scrollY: "310px", buttons: [
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
               .appendTo('#<%=UnitGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

            var table = $('#<%=gv_itemslist.ClientID%>').DataTable({
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
               .appendTo('#<%=gv_itemslist.ClientID%>_wrapper .col-sm-6:eq(0)');

            /////
            var table = $('#<%=gridview_fire_extinguisher.ClientID%>').DataTable({
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
               .appendTo('#<%=gridview_fire_extinguisher.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';

            var table = $('#<%=gv_emailsend.ClientID%>').DataTable({
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
               .appendTo('#<%=gv_emailsend.ClientID%>_wrapper .col-sm-6:eq(0)');

            var table = $('#<%=grd_approval_documents.ClientID%>').DataTable({
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
               .appendTo('#<%=grd_approval_documents.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';


        }
    </script>
    <script type="text/javascript">


        $(document).ready(function () {



            $('#<%=btn_cancel.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

            var password = document.getElementById('<%= txt_unit_password.ClientID%>');
            password.value = "";

        });

        function callfnc() {

            document.getElementById('<%= Button5.ClientID %>').click();
        }

        function create() {
            var s = "";

            s += '<input type="text" name="Fname">'; //Create one textbox as HTML

            document.getElementById("screens").innerHTML = s;
        }
        function validate_designation() {
            if ($('#<%=ddl_designation.ClientID%>').val() == null) {
                alert("Please add Designation at Client level!!!");
                return false;
            }
            else {
                var designation = document.getElementById('<%=ddl_designation.ClientID %>');
                var SelectedText2 = designation.options[designation.selectedIndex].text;

                if (SelectedText2 == "Select") {
                    alert("Please Select Designation Name First!!!");
                    designation.focus();
                    return false;
                }

                var count = document.getElementById('<%=txt_count.ClientID %>');
                    if (count.value == "0" || count.value == "") {
                        alert("Designation Count should be greater than zero (0)!!!");
                        count.focus();
                        return false;
                    }
                    var wrkhrs = document.getElementById('<%=txt_working_hrs.ClientID %>');
                    if (wrkhrs.value == "" || wrkhrs.value == "0") {
                        alert("Please Add Working Hours!!!");
                        wrkhrs.focus();
                        return false;
                    }
                    var txt_start_date = document.getElementById('<%=txt_start_date.ClientID %>');
                    var txt_end_date = document.getElementById('<%=txt_end_date.ClientID %>');

                if (txt_start_date.value == "") {
                    alert("Please Select Start Date.");
                    txt_start_date.focus();
                    return false;
                }
                if (txt_end_date.value == "") {
                    alert("Please Select End Date.");
                    txt_end_date.focus();
                    return false;
                }
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function Req_validation() {

            var t_client = document.getElementById('<%=ddlunitclient.ClientID %>');
                var Selectedclient = t_client.options[t_client.selectedIndex].text;
                var t_UnitName = document.getElementById('<%=txt_unitname.ClientID %>');


                var t_state = document.getElementById('<%=ddl_state.ClientID %>');
                var SelectedText2 = t_state.options[t_state.selectedIndex].text;

                var t_City = document.getElementById('<%=txtunitcity.ClientID %>');

                var location = document.getElementById('<%= txtunitaddress1.ClientID%>');
                var t_lat = document.getElementById('<%= txt_lattitude.ClientID%>');

                var t_lon = document.getElementById('<%=txt_longitude.ClientID%>');

                var t_str = document.getElementById('<%=txtunitaddress2.ClientID%>');

                var t_area = document.getElementById('<%=txt_area.ClientID%>');
                var t_fileno = document.getElementById('<%=file_txt.ClientID%>');
                var t_zone = document.getElementById('<%= txt_zone1.ClientID%>');
                var txt_pin = document.getElementById('<%=txt_pincode.ClientID %>');

                var t_clientbranchcode = document.getElementById('<%=txt_clientbranchcode.ClientID %>');
                var txt_material_area = document.getElementById('<%=txt_material_area.ClientID %>');
                var txtemailid = document.getElementById('<%=txtemailid.ClientID %>');
                //ddl_labour_office
                //var ddl_labour_office = document.getElementById('<%=ddl_labour_office.ClientID %>');
                //var Selected_ddl_labour_office = ddl_labour_office.options[ddl_labour_office.selectedIndex].text;
                if (Selectedclient == "Select") {
                    alert("Please Select Client.");
                    t_client.focus();
                    return false;
                }
                if (SelectedText2 == "Select") {
                    alert("Please Branch State  !!!");
                    t_state.focus();
                    return false;
                }
                // City

                if (t_City.value == "") {
                    alert("Please Enter Branch City");
                    t_City.focus();
                    return false;
                }
                //Client Branch Code
                if ((Selectedclient == "BAJAJ ALLIANZ GENERAL INSURANCE CO. LTD TM") || (Selectedclient == "BAJAJ ALLIANZ GENERAL INSURANCE COMPANY LIMITED HK") || (Selectedclient == "BAJAJ ALLIANZ GENERAL INSURANCE COMPANY LIMITED SG") || (Selectedclient == "BAJAJ ALLIANZ LIFE INSURANCE COMPANY LIMITED SG") || (Selectedclient == "BAJAJ ALLIANZ LIFE INSURANCE COMPANY LIMITED HK")) {
                    if (t_clientbranchcode.value == "") {
                        alert("Please Enter Client Branch  Code");
                        t_clientbranchcode.focus();
                        return false;
                    }
                }
                var txt_opus_code = document.getElementById('<%=txt_opus_code.ClientID %>');
                if ((Selectedclient == "BAJAJ ALLIANZ GENERAL INSURANCE CO. LTD TM") || (Selectedclient == "BAJAJ ALLIANZ GENERAL INSURANCE COMPANY LIMITED HK") || (Selectedclient == "BAJAJ ALLIANZ GENERAL INSURANCE COMPANY LIMITED SG") || (Selectedclient == "BAJAJ ALLIANZ LIFE INSURANCE COMPANY LIMITED SG") || (Selectedclient == "BAJAJ ALLIANZ LIFE INSURANCE COMPANY LIMITED HK")) {
                    if (txt_opus_code.value == "") {
                        alert("Please Enter Opus Code");
                        txt_opus_code.focus();
                        return false;
                    }
                }
                // UnitName

                if (t_UnitName.value == "") {
                    alert("Please Enter Branch Name");
                    t_UnitName.focus();
                    return false;
                }
                if (location.value == "") {
                    alert("Please Enter Location");
                    location.focus();
                    return false;
                }
                // street

                if (t_str.value == "") {
                    alert("street/Road/Lane is Empty ! Please select it from Select Location");
                    t_str.focus();
                    return false;
                }


                if (txt_pin.value.length != 6) {
                    alert("Please Enter Pin Number Minimum 6 Digits");
                    txt_pin.focus();
                    return false;
                }



                //latitude

                if (t_lat.value == "") {
                    alert("Latitude is Empty ! Please select it from Select Location");
                    t_lat.focus();
                    return false;
                }

                //longitude

                if (t_lon.value == "") {
                    alert("Longitute is Empty ! Please select it from Select Location");
                    t_lon.focus();
                    return false;
                }

                //area
                if (t_area.value == "") {
                    alert("Area in Meters is Empty ! Please select it from Select Location");
                    t_area.focus();
                    return false;
                }

                if (t_fileno.value == "") {
                    alert("Please Enter File Number");
                    t_fileno.focus();
                    return false;
                }


                if (Selectedclient == "RBL Bank Ltd") {
                    if (txt_material_area.value == "0") {
                        alert("Please Enter Area in Square Ft");
                        txt_material_area.focus();
                        return false;
                    }
                }
                //if (Selected_ddl_labour_office == "Select" || Selected_ddl_labour_office == "")
                //{
                //    alert("Please Select labour office ");
                //    ddl_labour_office.focus();
                //    return false;
                //}

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }

        function upload_r_validation() {

            var txt_document_approval = document.getElementById('<%=txt_document_approval.ClientID %>');
            var document_file_approval = document.getElementById('<%=document_file_approval.ClientID %>');
            var txt_from_date_approval = document.getElementById('<%=txt_from_date_approval.ClientID %>');
            var txt_to_date_approval = document.getElementById('<%=txt_to_date_approval.ClientID %>');


            if (txt_document_approval.value == "") {
                alert("Please Enter Description");
                txt_document_approval.focus();
                return false;
            }

            if (document_file_approval.value == "") {
                alert("Please Upload File");
                document_file_approval.focus();
                return false;
            }

            if (txt_from_date_approval.value == "") {
                alert("Please Select Start Date");
                txt_from_date_approval.focus();
                return false;
            }

            if (txt_to_date_approval.value == "") {
                alert("Please Select End Date");
                txt_to_date_approval.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;


        }



            function r_val() {
                var t_client = document.getElementById('<%=ddlunitclient.ClientID %>');
                var Selectedclient = t_client.options[t_client.selectedIndex].text;
                var t_UnitName = document.getElementById('<%=txt_unitname.ClientID %>');


            var t_state = document.getElementById('<%=ddl_state.ClientID %>');
                var SelectedText2 = t_state.options[t_state.selectedIndex].text;

                var t_City = document.getElementById('<%=txtunitcity.ClientID %>');

            var location = document.getElementById('<%= txtunitaddress1.ClientID%>');
                var t_lat = document.getElementById('<%= txt_lattitude.ClientID%>');

                var t_lon = document.getElementById('<%=txt_longitude.ClientID%>');

                var t_str = document.getElementById('<%=txtunitaddress2.ClientID%>');

                var t_area = document.getElementById('<%=txt_area.ClientID%>');
                var t_fileno = document.getElementById('<%=file_txt.ClientID%>');
                var t_zone = document.getElementById('<%= txt_zone1.ClientID%>');
                var txt_pin = document.getElementById('<%=txt_pincode.ClientID %>');

                var t_clientbranchcode = document.getElementById('<%=txt_clientbranchcode.ClientID %>');
                var txt_material_area = document.getElementById('<%=txt_material_area.ClientID %>');
                var txtemailid = document.getElementById('<%=txtemailid.ClientID %>');

                if (Selectedclient == "Select") {
                    alert("Please Select Client.");
                    t_client.focus();
                    return false;
                }
                if (SelectedText2 == "Select") {
                    alert("Please Branch State  !!!");
                    t_state.focus();
                    return false;
                }
                // City
                if (t_City.value == "") {
                    alert("Please Enter Branch City");
                    t_City.focus();
                    return false;
                }
                //client branch code


                //Client Branch Code
                if ((Selectedclient == "BAJAJ ALLIANZ GENERAL INSURANCE CO. LTD TM") || (Selectedclient == "BAJAJ ALLIANZ GENERAL INSURANCE COMPANY LIMITED HK") || (Selectedclient == "BAJAJ ALLIANZ GENERAL INSURANCE COMPANY LIMITED SG") || (Selectedclient == "BAJAJ ALLIANZ LIFE INSURANCE COMPANY LIMITED SG") || (Selectedclient == "BAJAJ ALLIANZ LIFE INSURANCE COMPANY LIMITED HK")) {
                    if (t_clientbranchcode.value == "") {
                        alert("Please Enter Client Branch  Code");
                        t_clientbranchcode.focus();
                        return false;
                    }
                }
                var txt_opus_code = document.getElementById('<%=txt_opus_code.ClientID %>');
            if ((Selectedclient == "BAJAJ ALLIANZ GENERAL INSURANCE CO. LTD TM") || (Selectedclient == "BAJAJ ALLIANZ GENERAL INSURANCE COMPANY LIMITED HK") || (Selectedclient == "BAJAJ ALLIANZ GENERAL INSURANCE COMPANY LIMITED SG") || (Selectedclient == "BAJAJ ALLIANZ LIFE INSURANCE COMPANY LIMITED SG") || (Selectedclient == "BAJAJ ALLIANZ LIFE INSURANCE COMPANY LIMITED HK")) {
                if (txt_opus_code.value == "") {
                    alert("Please Enter Opus Code");
                    txt_opus_code.focus();
                    return false;
                }
            }
                // UnitName

            if (t_UnitName.value == "") {
                alert("Please Enter Branch Name");
                t_UnitName.focus();
                return false;
            }

            if (location.value == "") {
                alert("Please Enter Location");
                location.focus();
                return false;
            }
            if (txt_pin.value.length != 6) {
                alert("Please Enter Pin Number Minimum 6 Digits");
                txt_pin.focus();
                return false;
            }



                // street

            if (t_str.value == "") {
                alert("street/Road/Lane is Empty ! Please select it from Select Location");
                t_str.focus();
                return false;
            }

                //latitude

            if (t_lat.value == "") {
                alert("Latitude is Empty ! Please select it from Select Location");
                t_lat.focus();
                return false;
            }

                //longitude

            if (t_lon.value == "") {
                alert("Longitute is Empty ! Please select it from Select Location");
                t_lon.focus();
                return false;
            }

                //area
            if (t_area.value == "") {
                alert("Area in Meters is Empty ! Please select it from Select Location");
                t_area.focus();
                return false;
            }

            if (t_fileno.value == "") {
                alert("Please Enter File Number");
                t_fileno.focus();
                return false;
            }


            if (Selectedclient == "RBL Bank Ltd") {
                if (txt_material_area.value == "0") {
                    alert("Please Enter Area in Square Ft");
                    txt_material_area.focus();
                    return false;
                }
            }
//vikas add branch closing
var ddl_branchStatus = document.getElementById('<%=ddl_branchStatus.ClientID %>');
                var select_ddl_branchStatus = ddl_branchStatus.options[ddl_branchStatus.selectedIndex].text;
                if (select_ddl_branchStatus == "Closed")
                {
                    var txt_br_cose_date = document.getElementById('<%=txt_br_cose_date.ClientID %>');
                    if (txt_br_cose_date.value == "") {
                        alert("Please Enter Branch Close Date");
                        txt_br_cose_date.focus();
                        return false;
                    }

                }
                //ddl_labour_office
                // var ddl_labour_office = document.getElementById('<%=ddl_labour_office.ClientID %>');
                // var Selected_ddl_labour_office = ddl_labour_office.options[ddl_labour_office.selectedIndex].text;

                //if (Selected_ddl_labour_office == "Select" || Selected_ddl_labour_office == "") {
                //alert("Please Select labour office ");
                //ddl_labour_office.focus();
                // return false;
                //}
                var reason_update = document.getElementById('<%=txt_reason_updation.ClientID %>');
                if (!reason_update.disabled) {
                    if (reason_update.value == "") {
                        alert("Please Specify Reason For Updation !!!");
                        reason_update.focus();
                        return false;
                    }

                }

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;

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

            function email(e) {
                if (null != e) {
                    isIE = document.all ? 1 : 0
                    keyEntry = !isIE ? e.which : e.keyCode;
                    if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9') || (keyEntry == '64') || (keyEntry == '46'))
                        return true;
                    else {
                        // alert('Please Enter Only Character values.');
                        return false;
                    }
                }
            }
            function website(e) {
                if (null != e) {
                    isIE = document.all ? 1 : 0
                    keyEntry = !isIE ? e.which : e.keyCode;
                    if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9') || (keyEntry == '46') || (keyEntry == '47'))
                        return true;
                    else {
                        // alert('Please Enter Only Character values.');
                        return false;
                    }
                }
            }
            function AllowAlphabet_Number_slash(e) {
                if (null != e) {
                    isIE = document.all ? 1 : 0
                    keyEntry = !isIE ? e.which : e.keyCode;
                    if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9') || (keyEntry == '47'))
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
            function isNumber(evt) {
                if (null != evt) {
                    evt = (evt) ? evt : window.event;
                    var charCode = (evt.which) ? evt.which : evt.keyCode;
                    keyEntry = !isIE ? e.which : e.keyCode;
                    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                }
                return true;
            }
            function isNumber_plus(evt) {
                if (null != evt) {
                    evt = (evt) ? evt : window.event;
                    var charCode = (evt.which) ? evt.which : evt.keyCode;
                    if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 43 || charCode > 43)) {
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
            function openWindow() {
                window.open("html/UnitMaster.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
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

            function R_validation() {

                var r = confirm("Are you Sure You Want to Delete Record");
                if (r == true) {

                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

                }
                return r;
            }
            function validateEmail(emailField) {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

                if (reg.test(emailField.value) == false) {
                    alert('Invalid Email Address');
                    return false;
                }

                return true;

            }

    </script>
    <script>
        function pincode_validation() {
            var txt_pin = document.getElementById('<%=txt_pincode.ClientID %>');

            if (txt_pin.value.length != 6) {
                alert("Please Enter Pin Number Minimum 6 Digit");
                aField = document.getElementById('<%=txt_pincode.ClientID %>');
                setTimeout("aField.focus()", 50);
                return false;
            }
        }

        function in_out_rpt() {
            var ddl_intime = document.getElementById('<%=ddl_intime.ClientID %>');
             var Selected_ddl_intime = ddl_intime.options[ddl_intime.selectedIndex].text;

             var ddl_out_time = document.getElementById('<%=ddl_out_time.ClientID %>');
            var Selected_ddl_out_time = ddl_out_time.options[ddl_out_time.selectedIndex].text;

            var ddl_reporting_time = document.getElementById('<%=ddl_reporting_time.ClientID %>');
             var Selected_ddl_reporting_time = ddl_reporting_time.options[ddl_reporting_time.selectedIndex].text;

             //alert(ddl_intime.value);
             //alert(parseInt(Selected_ddl_reporting_time.replace(':', '')));
             //alert(parseInt(ddl_intime.value.substring(0, 1)));
             if (parseInt(Selected_ddl_reporting_time.replace(':', '')) > parseInt(Selected_ddl_intime.replace(':', ''))) {
                 alert("Reporting time should be less than Intime.");
                 ddl_intime.value = "Flexible";
                 ddl_intime.focus();
                 return false;
             }
             return true;
             //alert(str);
             //if (Selected_ddl_intime == str)
             //{
             //    var s = ddl_out_time.value >= str;
             //    return(s);
             //    return true;
             //}
             //if (str.length == 0) return true;
             //if (str.length < 4) return false;
             //var x = str.indexOf(":");
             //alert(str);
             //if (x < 0) {
             //    str = str.substr(0, 2) + ":" + str.substr(2, 2);
             //    Selected_ddl_intime = str;
             //    ddl_intime.focus();
             //    return true;
             //}
         }
         function in_out_rpt1() {
             var ddl_intime = document.getElementById('<%=ddl_intime.ClientID %>');
             var Selected_ddl_intime = ddl_intime.options[ddl_intime.selectedIndex].text;

             var ddl_out_time = document.getElementById('<%=ddl_out_time.ClientID %>');
             var Selected_ddl_out_time = ddl_out_time.options[ddl_out_time.selectedIndex].text;

             var ddl_reporting_time = document.getElementById('<%=ddl_reporting_time.ClientID %>');
             var Selected_ddl_reporting_time = ddl_reporting_time.options[ddl_reporting_time.selectedIndex].text;

             //alert(ddl_intime.value);
             //alert(parseInt(Selected_ddl_reporting_time.replace(':', '')));
             //alert(parseInt(ddl_intime.value.substring(0, 1)));
             if (parseInt(Selected_ddl_intime.replace(':', '')) > parseInt(Selected_ddl_out_time.replace(':', ''))) {
                 alert("Out time should be less than In time.");
                 ddl_out_time.value = "0:00";
                 ddl_out_time.focus();
                 return false;
             }
             return true;
             //alert(str);
             //if (Selected_ddl_intime == str)
             //{
             //    var s = ddl_out_time.value >= str;
             //    return(s);
             //    return true;
             //}
             //if (str.length == 0) return true;
             //if (str.length < 4) return false;
             //var x = str.indexOf(":");
             //alert(str);
             //if (x < 0) {
             //    str = str.substr(0, 2) + ":" + str.substr(2, 2);
             //    Selected_ddl_intime = str;
             //    ddl_intime.focus();
             //    return true;
             //}
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

         function fire_extinguisher()
         {
            
             

             var txt_end_date_fr = document.getElementById('<%=txt_end_date_fr.ClientID %>');

             if (txt_end_date_fr.value == "") {
                 alert("Please Enter Expiry Date");
                 txt_end_date_fr.focus();
                 return false;
             }

             var ddl_type_extinguisher = document.getElementById('<%=ddl_type_extinguisher.ClientID %>');
             var Selected_ddl_type_extinguisher = ddl_type_extinguisher.options[ddl_type_extinguisher.selectedIndex].text;

             if (Selected_ddl_type_extinguisher == "Select") {
                 alert("Please Select Type Of Extinguisher ");
                 ddl_type_extinguisher.focus();
                 return false;

             }

    
             var ddl_weight_kg1 = document.getElementById('<%=ddl_weight_kg.ClientID %>');
             var Selected_ddl_type_extinguisher = ddl_weight_kg1.options[ddl_weight_kg1.selectedIndex].text;

             if (Selected_ddl_type_extinguisher == "Select") {
                 alert("Please Select Weight In KG ");
                 ddl_weight_kg1.focus();
                 return false;

             }




             var txt_vender_name = document.getElementById('<%=txt_vender_name.ClientID %>');

             if (txt_vender_name.value == "") {
                 alert("Please Enter Vendor Name");
                 txt_vender_name.focus();
                 return false;
             }


             var txt_vender_no = document.getElementById('<%=txt_vender_no.ClientID %>');

             if (txt_vender_no.value == "") {
                 alert("Please Enter Vendor Contact Number");
                 txt_vender_no.focus();
                 return false;
             }


             var txt_fire_report = document.getElementById('<%=txt_fire_report.ClientID %>');
           
             if (txt_fire_report.value == "") {
                     alert("Please Upload Fire Extinguisher Report");
                     txt_fire_report.focus();
                     return false;
                 }
             
             var t_client = document.getElementById('<%=ddlunitclient.ClientID %>');
             var Selectedclient = t_client.options[t_client.selectedIndex].text;

             if (Selectedclient == "Select") {
                 alert("Please Select Client");
                 t_client.focus();
                 return false;

             }


             var ddl_state = document.getElementById('<%=ddl_state.ClientID %>');
             var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;

             if (Selected_ddl_state == "0") {
                 alert("Please Select State");
                 ddl_state.focus();
                 return false;

             }

             var txtunitcity = document.getElementById('<%=txtunitcity.ClientID %>');
             var Selected_txtunitcity = txtunitcity.options[txtunitcity.selectedIndex].text;

             if (Selected_txtunitcity == "") {
                 alert("Please Select State");
                 txtunitcity.focus();
                 return false;

             }


             var txt_unitname = document.getElementById('<%=txt_unitname.ClientID %>');

             if (txt_unitname.value == "") {
                 alert("Please Enter Branch Name ");
                 txt_unitname.focus();
                 return false;
             }

             $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             return true;

         }


         function send_mail_validation() {
             var ddl_sendemail_type = document.getElementById('<%=ddl_sendemail_type.ClientID %>');
               var Selected_ddl_sendemail_type = ddl_sendemail_type.options[ddl_sendemail_type.selectedIndex].text;

               var txt_hadename = document.getElementById('<%=txt_hadename.ClientID %>');
             var txt_head_emailid = document.getElementById('<%=txt_head_emailid.ClientID %>');
               var txt_mobileno = document.getElementById('<%=txt_mobileno.ClientID %>');

               if (Selected_ddl_sendemail_type == "Select") {
                   alert("Please Select Send Email Type");
                   ddl_sendemail_type.focus();
                   return false;

               }
               if (txt_hadename.value == "") {
                   alert("Please Enter Head Name");
                   txt_hadename.focus();
                   return false;
               }
               if (txt_head_emailid.value == "") {
                   alert("Please Enter Head Email Id");
                   txt_head_emailid.focus();
                   return false;
               }
               var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
               if (!filter.test(txt_head_emailid.value)) {
                   alert('Please provide a valid email address');
                   txt_head_emailid.focus;
                   return false;
               }
               if (txt_mobileno.value == "") {
                   alert("Please Enter Head Mobile Number");
                   txt_mobileno.focus();
                   return false;
               }
               $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
               return true;
           }
           function free_count() {
               var ddlunitclient1 = document.getElementById('<%=ddlunitclient1.ClientID %>');
            var Selected_ddlunitclient1 = ddlunitclient1.options[ddlunitclient1.selectedIndex].text;

            if (Selected_ddlunitclient1 == "Select") {
                alert("Please Select Client Name");
                ddlunitclient1.focus();
                return false;
            }
        }
        function hide_company_tab() {
            var ddlunitclient = document.getElementById('<%=ddlunitclient.ClientID %>');
            var Selected_ddlunitclient = ddlunitclient.options[ddlunitclient.selectedIndex].text;

            if (Selected_ddlunitclient == "Reliance Capital Ltd.") {
                $(".company_hide").show();
            }
            else { $(".company_hide").hide(); }
        }
    </script>
    <div class="container-fluid">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary" Style="background-color: beige">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: small;"><b>BRANCH MASTER</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <asp:Panel ID="Panel5" runat="server">
                    <asp:GridView ID="GridView1" class="table" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" ForeColor="#333333" Font-Size="X-Small" DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing">
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-primary" ShowEditButton="true" />
                            <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-primary" ShowDeleteButton="true" />
                            <asp:BoundField DataField="UNIT_CODE" HeaderText="Branch Code"
                                SortExpression="UNIT_CODE" />
                            <asp:BoundField DataField="UNIT_NAME" HeaderText="Branch Name"
                                SortExpression="UNIT_NAME" />
                            <asp:BoundField DataField="UNIT_ADD1" HeaderText="Landmark"
                                SortExpression="UNIT_ADD1" />
                            <asp:BoundField DataField="UNIT_ADD2" HeaderText="Street/Road/Lane"
                                SortExpression="UNIT_ADD2" />
                            <asp:BoundField DataField="UNIT_CITY" HeaderText="Branch City"
                                SortExpression="UNIT_CITY" />
                            <asp:BoundField DataField="STATE_NAME" HeaderText="State Name"
                                SortExpression="STATE_NAME" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />

                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                    <br />
                    <br />
                </asp:Panel>
                <asp:Panel ID="reason_panel" runat="server" Visible="false">
                    <div class="panel panel-primary" style="background-color: #f3f1fe; border-radius: 10px; border: 1px solid white">
                        <br />
                        <div class="panel-heading">
                            <div style="color: white; font-weight: bold; font-size: 15px;">Reason for Updation</div>
                        </div>
                        <div class="panel-body container">
                            <div class="row">
                                <asp:ListBox ID="lbx_reason_updation" ReadOnly="true" CssClass="form-control" runat="server" Rows="9" Width="100%"></asp:ListBox>
                                <asp:TextBox ID="txt_reason_updation" runat="server" TextMode="MultiLine" Columns="50" Rows="4" CssClass="form-control" placeholder="Enter Reason for Updation"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white">
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                            <b>Select Client :</b><span class="text-red"> *</span>

                            <asp:DropDownList ID="ddlunitclient" runat="server" DataValueField="client_Code" DataTextField="client_name" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="designstion_details1" Width="100%" onchange="hide_company_tab();">
                            </asp:DropDownList>

                        </div>

                        <%--    <asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate>--%>
                        <div class="col-sm-2 col-xs-12">
                            <b>Branch State :</b><span class="text-red"> *</span>
                            <asp:DropDownList ID="ddl_state" runat="server" DataTextField="STATE" Width="100%"
                                DataValueField="STATE" class="form-control text_box"
                                OnSelectedIndexChanged="get_city_list" AutoPostBack="true">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <span>
                                <asp:Label ID="lblstate" runat="server" Font-Size="Small"></asp:Label>
                            </span>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Branch City :</b><span class="text-red"> *</span>
                            <asp:DropDownList ID="txtunitcity" runat="server" class="form-control text_box" Width="100%"></asp:DropDownList>
                        </div>
                        <%--  </ContentTemplate></asp:UpdatePanel>--%>
                        <div class="col-sm-2 col-xs-12">
                            <b>Branch Code :</b>
                                <asp:TextBox ID="txt_unitcode" runat="server" class="form-control text_box" ReadOnly="true" MaxLength="30"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>Client Branch Code :</b><span style="color: red">*</span>

                            <asp:TextBox ID="txt_clientbranchcode" runat="server" MaxLength="30" class="form-control text_box" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>Opus Code :</b>
                           
                        <asp:TextBox ID="txt_opus_code" runat="server" MaxLength="30" class="form-control text_box" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                        </div>
                        <asp:TextBox ID="txt_empcount" runat="server" Text="0" Visible="false">0</asp:TextBox>

                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                            <b>Branch Name :</b><span class="text-red"> *</span>
                            <asp:TextBox ID="txt_unitname" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number_slash(event,this);" MaxLength="30"></asp:TextBox>
                        </div>


                        <div class="col-sm-2 col-xs-12">
                            <b>Location(Place/City) :</b><span class="text-red"> *</span>
                            <asp:TextBox ID="txtunitaddress1" runat="server" class="form-control text_box " onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <br />
                            <asp:Button ID="Button1" runat="server" CssClass="hidden" Text="Location" />
                            <asp:Button ID="Button5" runat="server" CssClass="hidden" OnClick="Button5_Click" />
                            <asp:Button ID="Button3" runat="server" CssClass="btn btn-warning" Text="Select Location" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel4" TargetControlID="Button3"
                                CancelControlID="Button4" BackgroundCssClass="Background">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel4" runat="server" CssClass="Popup" Style="display: none">
                                <iframe style="width: 1200px; height: 600px; background-color: #fff;" id="Iframe1" src="location_map.aspx" runat="server"></iframe>
                                <div class="row text-center">

                                    <asp:Button ID="Button4" CssClass="btn btn-danger" runat="server" OnClientClick="callfnc()" Text="Close" />
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>Address(Street/Road/Lane) :</b><span class="text-red"> *</span>
                            <asp:TextBox ID="txtunitaddress2" runat="server" TextMode="multiline" Width="100%" Rows="2" class="form-control text_box" MaxLength="50" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>Pincode :</b><span class="text-red"> *</span>
                            <asp:TextBox ID="txt_pincode" runat="server" class="form-control text_box" MaxLength="6" onkeypress="return isNumber(event)" onchange="return pincode_validation();"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>Lattitude :</b>
                            <span class="text-red">*</span>
                            <asp:TextBox ID="txt_lattitude" runat="server" class="form-control text_box " onkeypress="return isNumber_dot(event)"></asp:TextBox>
                        </div>



                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                            <input id="Hidden1" type="hidden" runat="server" />
                            <input id="Hidden2" type="hidden" runat="server" />
                            <b>Longitude :</b>
                           <span class="text-red">*</span>
                            <asp:TextBox ID="txt_longitude" runat="server" class="form-control text_box" onkeypress="return isNumber_dot(event)"></asp:TextBox>
                        </div>

                        <div class="col-sm-2 col-xs-12">
                            <b>Area In Meters :</b>
                            <span class="text-red">*</span>
                            <asp:TextBox ID="txt_area" runat="server" class="form-control text_box" onkeypress="return isNumber(event)">0</asp:TextBox>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="col-sm-2 col-xs-12">
                                    <b>Zone :</b>
                        <asp:DropDownList ID="txt_zone1" runat="server" DataTextField="ZONE" Width="100%"
                            DataValueField="ZONE" class="form-control text_box" OnSelectedIndexChanged="txt_zone1_region" AutoPostBack="true">
                            <%-- <asp:ListItem Value="Select" Text="Select">Select</asp:ListItem>
                             <asp:ListItem Value="East" Text="East"></asp:ListItem>
                             <asp:ListItem Value="West" Text="West"></asp:ListItem>
                             <asp:ListItem Value="North" Text="North"></asp:ListItem>
                             <asp:ListItem Value="South" Text="South"></asp:ListItem>--%>
                        </asp:DropDownList>

                                </div>

                                <div class="col-sm-2 col-xs-12">
                                    <b>Region Name :</b>
                        <asp:DropDownList ID="txt_zone" runat="server" DataTextField="ZONE" Width="100%" DataValueField="ZONE" class="form-control text_box">
                        </asp:DropDownList>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="col-sm-2 col-xs-12">
                           <b> File No. :</b><span class="text-red"> *</span>
                            <asp:TextBox ID="file_txt" runat="server" class="form-control text_box" Width="100%" MaxLength="40" meta:resourcekey="txt_presentpincodeResource1" onkeypress="return AllowAlphabet_address(event)">0</asp:TextBox>
                        </div>

                        <%--<div class="col-sm-2 col-xs-12">
                        GST IN :<span class="text-red"> *</span>
                            <asp:TextBox ID="txt_gst_no" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event,this);" Width="100%" MaxLength="20"></asp:TextBox>

                    </div>--%>


                        <asp:TextBox ID="txt_unit_login_id" runat="server" Width="100%" Visible="false" ReadOnly="true" class="form-control text_box" placeholder="User Name"></asp:TextBox>
                        <asp:TextBox ID="txt_unit_password" runat="server" Width="100%" class="form-control text_box" Visible="false" ReadOnly="true" TextMode="Password" placeholder="Password"></asp:TextBox>
                        <div class="col-sm-2 col-xs-12">
                            <b>Branch Email Id:</b> 
                        <asp:TextBox ID="txtemailid" runat="server" class="form-control text_box" Width="100%" onblur="validateEmail(this);" MaxLength="100"></asp:TextBox>
                        </div>

                    </div>

                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                            <b>Branch Cost Centre Code :</b>
                          
                        <asp:TextBox ID="txtbranch_cost_centre_code" runat="server" class="form-control text_box" MaxLength="14" onkeypress="return isNumber(event)"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>PC Code :</b>
                         
                        <asp:TextBox ID="txt_pccode" runat="server" class="form-control text_box" MaxLength="14" onkeypress="return isNumber(event)"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>Distictive Code :</b>
                         
                        <asp:TextBox ID="txt_disticitivecode" runat="server" class="form-control text_box" MaxLength="14" onkeypress="return isNumber(event)"></asp:TextBox>
                        </div>

                        <div class="col-sm-2 col-xs-12">
                            <b>Location Type :</b>
                        <asp:DropDownList ID="ddl_location_type" runat="server" Width="100%"
                            class="form-control text_box">
                            <asp:ListItem Value="Branch" Text="Branch"></asp:ListItem>
                            <asp:ListItem Value="Regional Office" Text="Regional Office"></asp:ListItem>
                            <asp:ListItem Value="Zonal Office" Text="Zonal Office"></asp:ListItem>
                            <asp:ListItem Value="Head Office" Text="Head Office"></asp:ListItem>
                            <%--<asp:ListItem Value="South" Text="South"></asp:ListItem>--%>
                        </asp:DropDownList>

                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>Branch Type :</b>
                        <asp:DropDownList ID="ddl_branch_type" runat="server" Width="100%"
                            class="form-control text_box">
                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="Urban" Text="Urban"></asp:ListItem>
                            <asp:ListItem Value="Rural" Text="Rural"></asp:ListItem>
                        </asp:DropDownList>

                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>Area In Sqr.Ft :</b>
                        <asp:TextBox ID="txt_material_area" runat="server" class="form-control text_box" Text="0" onkeypress="return isNumber(event)">0</asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <br />
                        <div class="col-sm-2 col-xs-12">
                            <b>Branch Status :</b>
                        <asp:DropDownList ID="ddl_branchStatus" runat="server" Width="100%"
                            class="form-control text_box">
                            <asp:ListItem Value="3" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Closed"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Hold"></asp:ListItem>
                        </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                                            <b>Branch Closeing Date :</b>
                        <asp:TextBox ID="txt_br_cose_date" runat="server" class="form-control date-picker" placeholder="Branch close Date :"></asp:TextBox>
                         </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>Shift Type :</b>
                        <asp:DropDownList ID="ddl_shift_type" runat="server" Width="100%"
                            class="form-control text_box">
                            <asp:ListItem Value="3" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Common"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Shift 1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Shift 2"></asp:ListItem>
                        </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>Reporting Time :</b> 
                                    <asp:DropDownList ID="ddl_reporting_time" runat="server" CssClass="form-control" Width="100%" meta:resourceKey="ddl_intimeResource1">
                                        <asp:ListItem Value="0:00" Text="0:00"></asp:ListItem>
                                        <asp:ListItem Value="0:30" Text="0:30"></asp:ListItem>
                                        <asp:ListItem Value="1:00" Text="1:00"></asp:ListItem>
                                        <asp:ListItem Value="1:30" Text="1:30"></asp:ListItem>
                                        <asp:ListItem Value="2:00" Text="2:00"></asp:ListItem>
                                        <asp:ListItem Value="2:30" Text="2:30"></asp:ListItem>
                                        <asp:ListItem Value="3:00" Text="3:00"></asp:ListItem>
                                        <asp:ListItem Value="3:30" Text="3:30"></asp:ListItem>
                                        <asp:ListItem Value="4:00" Text="4:00"></asp:ListItem>
                                        <asp:ListItem Value="4:30" Text="4:30"></asp:ListItem>
                                        <asp:ListItem Value="5:00" Text="5:00"></asp:ListItem>
                                        <asp:ListItem Value="5:30" Text="5:30"></asp:ListItem>
                                        <asp:ListItem Value="6:00" Text="6:00"></asp:ListItem>
                                        <asp:ListItem Value="6:30" Text="6:30"></asp:ListItem>
                                        <asp:ListItem Value="7:00" Text="7:00"></asp:ListItem>
                                        <asp:ListItem Value="7:30" Text="7:30"></asp:ListItem>
                                        <asp:ListItem Value="8:00" Text="8:00"></asp:ListItem>
                                        <asp:ListItem Value="8:30" Text="8:30"></asp:ListItem>
                                        <asp:ListItem Value="9:00" Text="9:00"></asp:ListItem>
                                        <asp:ListItem Value="9:30" Text="9:30"></asp:ListItem>
                                        <asp:ListItem Value="10:00" Text="10:00"></asp:ListItem>
                                        <asp:ListItem Value="10:30" Text="10:30"></asp:ListItem>
                                        <asp:ListItem Value="11:00" Text="11:00"></asp:ListItem>
                                        <asp:ListItem Value="11:30" Text="11:30"></asp:ListItem>
                                        <asp:ListItem Value="12:00" Text="12:00"></asp:ListItem>
                                        <asp:ListItem Value="12:30" Text="12:30"></asp:ListItem>
                                        <asp:ListItem Value="13:00" Text="13:00"></asp:ListItem>
                                        <asp:ListItem Value="13:30" Text="13:30"></asp:ListItem>
                                        <asp:ListItem Value="14:00" Text="14:00"></asp:ListItem>
                                        <asp:ListItem Value="14:30" Text="14:30"></asp:ListItem>
                                        <asp:ListItem Value="15:00" Text="15:00"></asp:ListItem>
                                        <asp:ListItem Value="15:30" Text="15:30"></asp:ListItem>
                                        <asp:ListItem Value="16:00" Text="16:00"></asp:ListItem>
                                        <asp:ListItem Value="16:30" Text="16:30"></asp:ListItem>
                                        <asp:ListItem Value="17:00" Text="17:00"></asp:ListItem>
                                        <asp:ListItem Value="17:30" Text="17:30"></asp:ListItem>
                                        <asp:ListItem Value="18:00" Text="18:00"></asp:ListItem>
                                        <asp:ListItem Value="18:30" Text="18:30"></asp:ListItem>
                                        <asp:ListItem Value="19:00" Text="19:00"></asp:ListItem>
                                        <asp:ListItem Value="19:30" Text="19:30"></asp:ListItem>
                                        <asp:ListItem Value="20:00" Text="20:00"></asp:ListItem>
                                        <asp:ListItem Value="20:30" Text="20:30"></asp:ListItem>
                                        <asp:ListItem Value="21:00" Text="21:00"></asp:ListItem>
                                        <asp:ListItem Value="21:30" Text="21:30"></asp:ListItem>
                                        <asp:ListItem Value="22:00" Text="22:00"></asp:ListItem>
                                        <asp:ListItem Value="22:30" Text="22:30"></asp:ListItem>
                                        <asp:ListItem Value="23:00" Text="23:00"></asp:ListItem>
                                        <asp:ListItem Value="23:30" Text="23:30"></asp:ListItem>
                                    </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>IN Time :</b> 
                                    <asp:DropDownList ID="ddl_intime" runat="server" class="form-control" Width="100%" meta:resourceKey="ddl_intimeResource1" onchange="return in_out_rpt();">
                                        <asp:ListItem Value="Flexible" Text="Flexible"></asp:ListItem>
                                        <asp:ListItem Value="0:00" Text="0:00"></asp:ListItem>
                                        <asp:ListItem Value="0:30" Text="0:30"></asp:ListItem>
                                        <asp:ListItem Value="1:00" Text="1:00"></asp:ListItem>
                                        <asp:ListItem Value="1:30" Text="1:30"></asp:ListItem>
                                        <asp:ListItem Value="2:00" Text="2:00"></asp:ListItem>
                                        <asp:ListItem Value="2:30" Text="2:30"></asp:ListItem>
                                        <asp:ListItem Value="3:00" Text="3:00"></asp:ListItem>
                                        <asp:ListItem Value="3:30" Text="3:30"></asp:ListItem>
                                        <asp:ListItem Value="4:00" Text="4:00"></asp:ListItem>
                                        <asp:ListItem Value="4:30" Text="4:30"></asp:ListItem>
                                        <asp:ListItem Value="5:00" Text="5:00"></asp:ListItem>
                                        <asp:ListItem Value="5:30" Text="5:30"></asp:ListItem>
                                        <asp:ListItem Value="6:00" Text="6:00"></asp:ListItem>
                                        <asp:ListItem Value="6:30" Text="6:30"></asp:ListItem>
                                        <asp:ListItem Value="7:00" Text="7:00"></asp:ListItem>
                                        <asp:ListItem Value="7:30" Text="7:30"></asp:ListItem>
                                        <asp:ListItem Value="8:00" Text="8:00"></asp:ListItem>
                                        <asp:ListItem Value="8:30" Text="8:30"></asp:ListItem>
                                        <asp:ListItem Value="9:00" Text="9:00"></asp:ListItem>
                                        <asp:ListItem Value="9:30" Text="9:30"></asp:ListItem>
                                        <asp:ListItem Value="10:00" Text="10:00"></asp:ListItem>
                                        <asp:ListItem Value="10:30" Text="10:30"></asp:ListItem>
                                        <asp:ListItem Value="11:00" Text="11:00"></asp:ListItem>
                                        <asp:ListItem Value="11:30" Text="11:30"></asp:ListItem>
                                        <asp:ListItem Value="12:00" Text="12:00"></asp:ListItem>
                                        <asp:ListItem Value="12:30" Text="12:30"></asp:ListItem>
                                        <asp:ListItem Value="13:00" Text="13:00"></asp:ListItem>
                                        <asp:ListItem Value="13:30" Text="13:30"></asp:ListItem>
                                        <asp:ListItem Value="14:00" Text="14:00"></asp:ListItem>
                                        <asp:ListItem Value="14:30" Text="14:30"></asp:ListItem>
                                        <asp:ListItem Value="15:00" Text="15:00"></asp:ListItem>
                                        <asp:ListItem Value="15:30" Text="15:30"></asp:ListItem>
                                        <asp:ListItem Value="16:00" Text="16:00"></asp:ListItem>
                                        <asp:ListItem Value="16:30" Text="16:30"></asp:ListItem>
                                        <asp:ListItem Value="17:00" Text="17:00"></asp:ListItem>
                                        <asp:ListItem Value="17:30" Text="17:30"></asp:ListItem>
                                        <asp:ListItem Value="18:00" Text="18:00"></asp:ListItem>
                                        <asp:ListItem Value="18:30" Text="18:30"></asp:ListItem>
                                        <asp:ListItem Value="19:00" Text="19:00"></asp:ListItem>
                                        <asp:ListItem Value="19:30" Text="19:30"></asp:ListItem>
                                        <asp:ListItem Value="20:00" Text="20:00"></asp:ListItem>
                                        <asp:ListItem Value="20:30" Text="20:30"></asp:ListItem>
                                        <asp:ListItem Value="21:00" Text="21:00"></asp:ListItem>
                                        <asp:ListItem Value="21:30" Text="21:30"></asp:ListItem>
                                        <asp:ListItem Value="22:00" Text="22:00"></asp:ListItem>
                                        <asp:ListItem Value="22:30" Text="22:30"></asp:ListItem>
                                        <asp:ListItem Value="23:00" Text="23:00"></asp:ListItem>
                                        <asp:ListItem Value="23:30" Text="23:30"></asp:ListItem>
                                    </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> OUT Time :</b> 
                                    <asp:DropDownList ID="ddl_out_time" runat="server" CssClass="form-control" Width="100%" meta:resourceKey="ddl_intimeResource1" onchange="return in_out_rpt1();">
                                        <asp:ListItem Value="0:00" Text="0:00"></asp:ListItem>
                                        <asp:ListItem Value="0:30" Text="0:30"></asp:ListItem>
                                        <asp:ListItem Value="1:00" Text="1:00"></asp:ListItem>
                                        <asp:ListItem Value="1:30" Text="1:30"></asp:ListItem>
                                        <asp:ListItem Value="2:00" Text="2:00"></asp:ListItem>
                                        <asp:ListItem Value="2:30" Text="2:30"></asp:ListItem>
                                        <asp:ListItem Value="3:00" Text="3:00"></asp:ListItem>
                                        <asp:ListItem Value="3:30" Text="3:30"></asp:ListItem>
                                        <asp:ListItem Value="4:00" Text="4:00"></asp:ListItem>
                                        <asp:ListItem Value="4:30" Text="4:30"></asp:ListItem>
                                        <asp:ListItem Value="5:00" Text="5:00"></asp:ListItem>
                                        <asp:ListItem Value="5:30" Text="5:30"></asp:ListItem>
                                        <asp:ListItem Value="6:00" Text="6:00"></asp:ListItem>
                                        <asp:ListItem Value="6:30" Text="6:30"></asp:ListItem>
                                        <asp:ListItem Value="7:00" Text="7:00"></asp:ListItem>
                                        <asp:ListItem Value="7:30" Text="7:30"></asp:ListItem>
                                        <asp:ListItem Value="8:00" Text="8:00"></asp:ListItem>
                                        <asp:ListItem Value="8:30" Text="8:30"></asp:ListItem>
                                        <asp:ListItem Value="9:00" Text="9:00"></asp:ListItem>
                                        <asp:ListItem Value="9:30" Text="9:30"></asp:ListItem>
                                        <asp:ListItem Value="10:00" Text="10:00"></asp:ListItem>
                                        <asp:ListItem Value="10:30" Text="10:30"></asp:ListItem>
                                        <asp:ListItem Value="11:00" Text="11:00"></asp:ListItem>
                                        <asp:ListItem Value="11:30" Text="11:30"></asp:ListItem>
                                        <asp:ListItem Value="12:00" Text="12:00"></asp:ListItem>
                                        <asp:ListItem Value="12:30" Text="12:30"></asp:ListItem>
                                        <asp:ListItem Value="13:00" Text="13:00"></asp:ListItem>
                                        <asp:ListItem Value="13:30" Text="13:30"></asp:ListItem>
                                        <asp:ListItem Value="14:00" Text="14:00"></asp:ListItem>
                                        <asp:ListItem Value="14:30" Text="14:30"></asp:ListItem>
                                        <asp:ListItem Value="15:00" Text="15:00"></asp:ListItem>
                                        <asp:ListItem Value="15:30" Text="15:30"></asp:ListItem>
                                        <asp:ListItem Value="16:00" Text="16:00"></asp:ListItem>
                                        <asp:ListItem Value="16:30" Text="16:30"></asp:ListItem>
                                        <asp:ListItem Value="17:00" Text="17:00"></asp:ListItem>
                                        <asp:ListItem Value="17:30" Text="17:30"></asp:ListItem>
                                        <asp:ListItem Value="18:00" Text="18:00"></asp:ListItem>
                                        <asp:ListItem Value="18:30" Text="18:30"></asp:ListItem>
                                        <asp:ListItem Value="19:00" Text="19:00"></asp:ListItem>
                                        <asp:ListItem Value="19:30" Text="19:30"></asp:ListItem>
                                        <asp:ListItem Value="20:00" Text="20:00"></asp:ListItem>
                                        <asp:ListItem Value="20:30" Text="20:30"></asp:ListItem>
                                        <asp:ListItem Value="21:00" Text="21:00"></asp:ListItem>
                                        <asp:ListItem Value="21:30" Text="21:30"></asp:ListItem>
                                        <asp:ListItem Value="22:00" Text="22:00"></asp:ListItem>
                                        <asp:ListItem Value="22:30" Text="22:30"></asp:ListItem>
                                        <asp:ListItem Value="23:00" Text="23:00"></asp:ListItem>
                                        <asp:ListItem Value="23:30" Text="23:30"></asp:ListItem>
                                    </asp:DropDownList>
                        </div>
                        
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                            <b>Labour Regional Office :</b>
                        <asp:DropDownList ID="ddl_labour_office" runat="server" Width="100%"
                            class="form-control text_box">
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                        </asp:DropDownList>

                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>Budget Materials :</b>
                         
                      <asp:TextBox ID="txt_budget_material" runat="server" class="form-control text_box" MaxLength="6" onkeypress="return isNumber(event)"></asp:TextBox>
                        </div>
                         <div id="Div2" class="col-sm-2 col-xs-12" runat="server">
                           <b>Android Attendances :</b>
                       <asp:DropDownList ID="ddl_android_attendances_flag" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="select">Select</asp:ListItem>
                                    <asp:ListItem Value="yes">Yes</asp:ListItem>
                                    <asp:ListItem Value="no">No</asp:ListItem>

                                </asp:DropDownList>
                        </div>

                        <div id="rm" class="col-sm-2 col-xs-12"  runat="server">
                            <b>R&M Service :</b>
                            <asp:DropDownList ID="ddl_service" runat="server" CssClass="form-control">

                                <asp:ListItem Value="0">Not Applicable</asp:ListItem>
                                <asp:ListItem Value="1">Applicable</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div id="ae" class="col-sm-2 col-xs-12"  runat="server">
                            <b>Administrative Expenses :</b>
                            <asp:DropDownList ID="ddl_admin_expence" runat="server" CssClass="form-control">

                                <asp:ListItem Value="0">Not Applicable</asp:ListItem>
                                <asp:ListItem Value="1">Applicable</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                    <br />
                </div>
                <br />
                <div class="col-sm-2 col-xs-12">
                    <asp:Panel ID="client_strt_end_date" runat="server">
                        <a data-toggle="modal" href="#attendance" style="color: red; font-size: 11px;">Expired Unit List</a>
                    </asp:Panel>
                </div>
                <br />
                <div class="modal fade" id="attendance" role="dialog" data-dismiss="modal">

                    <div class="modal-dialog">
                        <div class="modal-content" style="width: 627px;">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4></h4>
                            </div>

                            <div class="modal-body">

                                <asp:Panel ID="Panel6" runat="server" CssClass="grid-view">
                                    <asp:GridView ID="gv_start_end_date_details" class="table" runat="server" ForeColor="#333333" Font-Size="X-Small" OnPreRender="gv_start_end_date_details_PreRender">
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
                <br />
                <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white">
                    <br />
                    <div id="tabs" style="background: beige;">
                        <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                        <ul>
                            <li id="tabactive5" class="active"><a data-toggle="tab" href="#menu1">Designation Details</a></li>
                            <li id="tabactive11"><a data-toggle="tab" href="#menu3">Heads Info</a></li>
                            <li id="itemactive8"><a data-toggle="tab" href="#item9">Send Email</a></li>
                            <li id="Li1"><a data-toggle="tab" href="#item11">Approval Upload</a></li>
                            <li id="Li2"><a data-toggle="tab" href="#item12">Fire Extinguisher Info</a></li>
                            <li id="Li3"><a data-toggle="tab" href="#menu2">Bulk Branch Upload</a></li>

                            <li id="itemactive10" class="company_hide" style="display: none;"><a data-toggle="tab" href="#item10">Company</a></li>
                        </ul>

                        <div id="item11">

                            <div class="row" id="files_upload" runat="server">

                                <div class="col-sm-2 col-xs-12">
                                    <b>Description</b>
                                     <asp:TextBox ID="txt_document_approval" runat="server" class="form-control text_box" MaxLength="100" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>
                                </div>

                                <div class="col-sm-2 col-xs-12">
                                    <span>File to Upload :</span>
                                    <asp:FileUpload ID="document_file_approval" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                                    <b style="color: #f00; text-align: center">Note :</b> <span style="font-size: 8px; font-weight: bold;">Only JPG, PNG and PDF files will be uploaded.</span>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <b>Start Date :</b>
                             <asp:TextBox ID="txt_from_date_approval" runat="server" class="form-control date-picker1 text_box" Style="display: inline"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <b>End Date :</b>
                              <asp:TextBox ID="txt_to_date_approval" runat="server" class="form-control date-picker2 text_box" Style="display: inline"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <br />
                                    <asp:Button ID="btn_upload_approval" runat="server" class="btn btn-primary" OnClick="btn_upload_approval_Click" Text ="Upload" OnClientClick="return upload_r_validation();" />
                                </div>

                                </div>


                            
                            <div class="container WrapText">
                                <br />
                                <asp:Panel ID="Panel8" runat="server" CssClass="grd_company">
                                    <asp:GridView ID="grd_approval_documents" class="table" runat="server" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDeleting="grd_company_files_RowDeleting"
                                         AutoGenerateColumns="False" OnPreRender="grd_approval_documents_PreRender" DataKeyNames="id" Width="100%" >
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

                                             <%--<asp:TemplateField HeaderText="Delete"> 
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_remove_bank" runat="server" CausesValidation="false"  OnClientClick="return R_validation();"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDelete" runat="server" class="btn btn-primary" Text="Delete" CommandName="Delete" OnRowDataBound="grd_company_files_RowDataBound" OnClientClick="return  R_validation();" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_srnumber" runat="server"
                                                        Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Id" HeaderText="ID" Visible="false"  />
                                            <asp:BoundField DataField="description" HeaderText="Description"
                                                SortExpression="description" ItemStyle-Wrap="true" />

                                            <asp:BoundField DataField="start_Date" HeaderText="Start Date"
                                                SortExpression="start_Date" />
                                            <asp:BoundField DataField="end_Date" HeaderText="End Date"
                                                SortExpression="end_Date" />
                                            <asp:TemplateField HeaderText="Download File">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload_approval" Text="Download"  CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="lnkDownload_approval_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                           

                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>


                            </div>

                        <div id="menu1">
                          <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server">--%>
                                <%--<ContentTemplate>--%>
                                    <br />
                                    <div class="row">
                                         <div class="col-sm-1 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12">
                                            <b>Designation Name :</b>
                                        <asp:DropDownList ID="ddl_designation" runat="server" DataValueField="DESIGNATION" DataTextField="DESIGNATION" class="form-control text_box" OnSelectedIndexChanged="ddl_desid" AutoPostBack="true" Width="100%"></asp:DropDownList>
                                        </div>
                                         <div class="col-sm-2 col-xs-12">
                                            <b>Category :</b>
                                  <asp:TextBox ID="txt_category" runat="server" class="form-control text_box" ReadOnly ="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <b>Count :</b>
                                  <asp:TextBox ID="txt_count" runat="server" class="form-control text_box text_box1" MaxLength="5" onkeypress="return isNumber(event)">0</asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <b>Working Hours :</b>
                        <asp:TextBox ID="txt_working_hrs" runat="server" class="form-control text_box text_box1" MaxLength="3" onkeypress="return isNumber(event)">0</asp:TextBox>
                                        </div>
                                       
                                    </div>
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-1 col-xs-12"></div>
                                         <div class="col-sm-2 col-xs-12">
                                            <b>Start Date :</b>
                        <asp:TextBox ID="txt_start_date" runat="server" class="form-control date-picker1" placeholder="Start Date :"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-2 col-xs-12">
                                            <b>End Date :</b>
                                        <asp:TextBox ID="txt_end_date" runat="server" class="form-control " placeholder="End Date :"></asp:TextBox>
                                        </div>
                                       <div class="col-sm-1 col-xs-12" style ="margin-top:1px">
                                        <asp:LinkButton ID="lnkbtn_addmoreitem" runat="server" OnClick="lnkbtn_addmoreitem_Click" OnClientClick="return validate_designation();">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                        </asp:LinkButton>
                                           </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12"></div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:Panel ID="Panel2" runat="server" CssClass="grid">
                                                    <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                        OnRowDataBound="gv_itemslist_RowDataBound"
                                                        AutoGenerateColumns="False" OnPreRender="gv_itemslist_PreRender">

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
                                                                    <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sr No.">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="DESIGNATION" HeaderText="Designation" SortExpression="DESIGNATION" />
                                                            <asp:BoundField DataField="COUNT" HeaderText="Employee Count" SortExpression="COUNT" />
                                                            <asp:BoundField DataField="HOURS" HeaderText="Working Hours" SortExpression="HOURS" />
                                                            <asp:BoundField DataField="unit_start_date" HeaderText="START DATE" SortExpression="unit_start_date" />
                                                            <asp:BoundField DataField="unit_end_date" HeaderText="END DATE" SortExpression="unit_end_date" />
                                                            <asp:TemplateField HeaderText="Id">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Id_CODE" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                             <asp:BoundField DataField="category" HeaderText="Category" SortExpression="category" />

                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </div>
                             <%--   </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>

                    </div>
                    <div id="item3">
                        <br />
                        <div class="row">
                            <div class="col-sm-2">
                            </div>
                        </div>
                    </div>
                    <div id="menu3">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-sm-2 col-xs-12"></div>
                                    <div class="col-sm-8 col-xs-12">
                                        <table class="table table-bordered">
                                            <tr style="background-color: #d0cdcd; text-align: center; font-weight: bold;">
                                                <td>Title</td>
                                                <td>Location Head</td>
                                                <td>Operation Head</td>
                                                <td>Finance Head</td>
                                                <td>Admin Head</td>
                                                <td>Other</td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_location_heaad_title" runat="server" class="form-control text_box" AutoPostBack="true">
                                                        <asp:ListItem Value="Select" Text="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="Mr." Text="Mr."></asp:ListItem>
                                                        <asp:ListItem Value="Mrs." Text="Mrs."></asp:ListItem>
                                                        <asp:ListItem Value="Ms." Text="Ms."></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_operation_heaad_title" runat="server" class="form-control text_box" AutoPostBack="true">
                                                        <asp:ListItem Value="Select" Text="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="Mr." Text="Mr."></asp:ListItem>
                                                        <asp:ListItem Value="Mrs." Text="Mrs."></asp:ListItem>
                                                        <asp:ListItem Value="Ms." Text="Ms."></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_finance_heaad_title" runat="server" class="form-control text_box" AutoPostBack="true">
                                                        <asp:ListItem Value="Select" Text="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="Mr." Text="Mr."></asp:ListItem>
                                                        <asp:ListItem Value="Mrs." Text="Mrs."></asp:ListItem>
                                                        <asp:ListItem Value="Ms." Text="Ms."></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_admin_heaad_title" runat="server" class="form-control text_box" AutoPostBack="true">
                                                        <asp:ListItem Value="Select" Text="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="Mr." Text="Mr."></asp:ListItem>
                                                        <asp:ListItem Value="Mrs." Text="Mrs."></asp:ListItem>
                                                        <asp:ListItem Value="Ms." Text="Ms."></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_other_heaad_title" runat="server" class="form-control text_box" AutoPostBack="true">
                                                        <asp:ListItem Value="Select" Text="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="Mr." Text="Mr."></asp:ListItem>
                                                        <asp:ListItem Value="Mrs." Text="Mrs."></asp:ListItem>
                                                        <asp:ListItem Value="Ms." Text="Ms."></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>


                                            </tr>
                                            <tr style="text-align: center; font-weight: bold;">
                                                <td>Contact Person Name</td>
                                                <td>
                                                    <asp:TextBox ID="txt_locationname" onkeypress="return AllowAlphabet(event);" runat="server" class="form-control text_box" placeholder="Contact Person Name"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_operationname" onkeypress="return AllowAlphabet(event);" runat="server" class="form-control text_box" placeholder="Contact Person Name"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_financename" onkeypress="return AllowAlphabet(event);" runat="server" class="form-control text_box" placeholder="Contact Person Name"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_adminname" onkeypress="return AllowAlphabet(event);" runat="server" class="form-control text_box" placeholder="Contact Person Name"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_othername" onkeypress="return AllowAlphabet(event);" runat="server" class="form-control text_box" placeholder="Contact Person Name"></asp:TextBox></td>
                                            </tr>
                                            <tr style="text-align: center; font-weight: bold;">
                                                <td>Mobile No</td>
                                                <td>
                                                    <asp:TextBox ID="txt_lmobileno" runat="server" onkeypress="return isNumber(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_omobileno" runat="server" onkeypress="return isNumber(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_fmobileno" runat="server" class="form-control text_box" onkeypress=" return isNumber(event)" MaxLength="10" placeholder="Mobile No"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_adminmobileno" runat="server" class="form-control text_box" onkeypress=" return isNumber(event)" MaxLength="10" placeholder="Mobile No"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_othermobno" runat="server" class="form-control text_box" onkeypress="return isNumber(event)" MaxLength="10" placeholder="Mobile No"></asp:TextBox></td>
                                            </tr>
                                            <tr style="text-align: center; font-weight: bold;">
                                                <td>Email Id</td>
                                                <td>
                                                    <asp:TextBox ID="txt_lemailid" runat="server" class="form-control text_box" placeholder="Email Id" onkeypress="return email(event,this);"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_oemailid" runat="server" class="form-control text_box" placeholder="Email Id" onkeypress="return email(event,this);"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_femailid" runat="server" MaxLength="50" class="form-control text_box" placeholder="Email Id" onkeypress="return email(event,this);"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_adminemailid" runat="server" MaxLength="50" class="form-control text_box" placeholder="Email Id" onkeypress="return email(event,this);"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_otheremailid" runat="server" MaxLength="50" class="form-control text_box" placeholder="Email Id" onkeypress="return email(event,this);"></asp:TextBox></td>
                                            </tr>
                                            <tr style="text-align: center; font-weight: bold;">
                                                <td>Personal Mobile No</td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_mob1" runat="server" CausesValidation="true" onkeypress="return isNumber(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_mob2" runat="server" CausesValidation="true" onkeypress="return isNumber(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_mob3" runat="server" class="form-control text_box" CausesValidation="true" onkeypress=" return isNumber(event)" MaxLength="10" placeholder="Mobile No"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_mob4" runat="server" class="form-control text_box" CausesValidation="true" onkeypress=" return isNumber(event)" MaxLength="10" placeholder="Mobile No"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_mob5" runat="server" onkeypress="return isNumber(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>
                                            </tr>
                                            <tr style="text-align: center; font-weight: bold;">
                                                <td>Personal EmailID</td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_emailid1" runat="server" class="form-control text_box" placeholder="Email Id" onkeypress="return email(event,this);"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_emailid2" runat="server" class="form-control text_box" placeholder="Email Id" onkeypress="return email(event,this);"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_emailid3" runat="server" MaxLength="50" class="form-control text_box" placeholder="Email Id" onkeypress="return email(event,this);"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_emailid4" runat="server" MaxLength="50" class="form-control text_box" placeholder="Email Id" onkeypress="return email(event,this);"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_emailid5" runat="server" MaxLength="50" class="form-control text_box" placeholder="Email Id" onkeypress="return email(event,this);"></asp:TextBox></td>
                                            </tr>
                                            <tr style="text-align: center; font-weight: bold;">
                                                <td>BirthDate</td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_bdate1" runat="server" class="form-control date-picker" placeholder="Birthdate"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_bdate2" runat="server" class="form-control date-picker" placeholder="Birthdate"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_bdate3" runat="server" MaxLength="50" class="form-control date-picker" placeholder="Birthdate"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_bdate4" runat="server" MaxLength="50" class="form-control date-picker" placeholder="Birthdate"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_bdate5" runat="server" MaxLength="50" class="form-control date-picker" placeholder="Birthdate"></asp:TextBox></td>
                                            </tr>
                                            <tr style="text-align: center; font-weight: bold;">
                                                <td>Anniversary Date</td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_adate1" runat="server" class="form-control date-picker" placeholder="Anniversary Date"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_adate2" runat="server" class="form-control date-picker" placeholder="Anniversary Date"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_adate3" runat="server" class="form-control date-picker" placeholder="Anniversary Date"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_adate4" runat="server" class="form-control date-picker" placeholder="Anniversary Date"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_p_adate5" runat="server" class="form-control date-picker" placeholder="Anniversary Date"></asp:TextBox></td>
                                            </tr>




                                        </table>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="item9">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-sm-2 col-xs-12">
                                        <b>Types :</b>
                                <asp:DropDownList ID="ddl_sendemail_type" runat="server" class="form-control" OnSelectedIndexChanged="ddl_head_emailid" AutoPostBack="true">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Location_Head">Location_Head</asp:ListItem>
                                    <asp:ListItem Value="Operation_Head">Operation_Head</asp:ListItem>
                                    <asp:ListItem Value="Finance_Head">Finance_Head</asp:ListItem>
                                    <asp:ListItem Value="Admin_Head">Admin_Head</asp:ListItem>
                                    <asp:ListItem Value="Other_Head">Other_Head</asp:ListItem>
                                </asp:DropDownList>
                                    </div>

                                    <div class="col-sm-2 col-xs-12">
                                        <b>Head Name  :</b>
                                       <asp:TextBox ID="txt_hadename" runat="server" MaxLength="10" onkeypress="return AllowAlphabet(event)" class="form-control text_box"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <b>Head Email_Id :</b>
                                   <asp:TextBox ID="txt_head_emailid" runat="server" onkeypress="return email(event,this);" class="form-control text_box"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <b>Head Mobile No :</b>
                                   <asp:TextBox ID="txt_mobileno" runat="server" MaxLength="10" onkeypress="return isNumber(event)" class="form-control text_box"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <br />
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lnk_add_emailid_Click" OnClientClick="return send_mail_validation();" AutoPostBack="true">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-2 col-xs-12"></div>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:Panel ID="Panel13" runat="server">
                                            <asp:Panel ID="Panel14" runat="server" CssClass="grid">

                                                <asp:GridView ID="gv_emailsend" class="table" runat="server" BackColor="White"
                                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                    OnRowDataBound="gv_statewise_gst_RowDataBound"
                                                    AutoGenerateColumns="False" Width="100%" OnPreRender="gv_emailsend_PreRender">
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
                                                            <ItemStyle Width="20px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="head_type" HeaderText="Head Type"
                                                            SortExpression="head_type" />
                                                        <asp:BoundField DataField="head_name" HeaderText="Head Name"
                                                            SortExpression="head_name" />
                                                        <asp:BoundField DataField="head_email_id" HeaderText="Head Email"
                                                            SortExpression="head_email_id" />
                                                        <asp:BoundField DataField="mobileno" HeaderText="Head Mobile No"
                                                            SortExpression="mobileno" />

                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="item10" class="company_hide" style="display: none">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <br />
                                <div class="container" style="width: 70%">
                                    <asp:Panel ID="Panel10" runat="server" CssClass="grid-view">

                                        <asp:GridView ID="gv_compgroup_type" class="table" runat="server" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            OnRowDataBound="gv_head_type_RowDataBound"
                                            AutoGenerateColumns="False" OnPreRender="gv_head_type_files_PreRender" Width="100%">

                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                            <Columns>

                                                <asp:TemplateField HeaderText="Comp Name">
                                                    <ItemStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_compname" runat="server" Text='<%# Eval("comp_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gst No">
                                                    <ItemStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_cmp_gst" runat="server" Text='<%# Eval("Companyname_gst_no")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address">
                                                    <ItemStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_comp_address" runat="server" Text='<%# Eval("gst_address")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Percentage">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_gvper" Font-Size="X-Small" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("percent")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="menu2">
                            <br />
                            <div class="row" border="1">
                                <div class=" col-sm-2 col-xs-12" style="margin-top: 9px;">
                                    File :<span class="text-red">*</span>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                </div>
                                <div class=" col-sm-2 col-xs-12" style="margin-top: 14px;">
                                    <asp:Button ID="btn_upload" runat="server" CssClass="btn btn-primary convience"
                                        Text="Upload" OnClick="btn_upload_Click"  />
                                </div>
                            </div>
                        </div>
                        <div id="item12">
                            <div class="row">

                           <%--  <div class="col-sm-2 col-xs-12">
                                    <b>Fire Extinguisher Applicable :</b>
                                    <asp:DropDownList ID="ddl_fire_applicable" runat="server" OnSelectedIndexChanged="ddl_fire_applicable_SelectedIndexChanged"  class="form-control">
                                   
                                        <asp:ListItem Value="Not Applicable">Not Applicable</asp:ListItem>
                                        <asp:ListItem Value="Applicable">Applicable</asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>

                            

                            <div class="col-sm-2 col-xs-12" style="display: none;">
                                          <b> Start Date :</b>
                        <asp:TextBox ID="txt_start_date_fr" runat="server" class="form-control date-picker1" placeholder="Start Date :"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-2 col-xs-12">
                                           <b> Expiry Date : </b>
                                        <asp:TextBox ID="txt_end_date_fr" runat="server" class="form-control date-picker2" placeholder="Expiry Date :"></asp:TextBox>
                                        </div>

                              
                              <div class="col-sm-2 col-xs-12">
                                    <b>Types Of Extinguisher :</b>
                                    <asp:DropDownList ID="ddl_type_extinguisher" runat="server" class="form-control">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                        <asp:ListItem Value="Class_ABC">Class_ABC</asp:ListItem>
                                        <asp:ListItem Value="CO2">CO2</asp:ListItem>
                                          <asp:ListItem Value="Clean_Agent">Clean_Agent</asp:ListItem>
                                    </asp:DropDownList>
                                </div>


                            <div class="col-sm-2 col-xs-12">
                                    <b> Weight In KG : </b>
                                    <asp:DropDownList ID="ddl_weight_kg" runat="server" class="form-control">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="1.5">1.5</asp:ListItem>
                                          <asp:ListItem Value="2">2</asp:ListItem>
                                         <asp:ListItem Value="2.5">2.5</asp:ListItem>
                                         <asp:ListItem Value="3">3</asp:ListItem>
                                         <asp:ListItem Value="3.5">3.5</asp:ListItem>
                                         <asp:ListItem Value="4">4</asp:ListItem>
                                         <asp:ListItem Value="4.5">4.5</asp:ListItem>
                                         <asp:ListItem Value="5">5</asp:ListItem>
                                         <asp:ListItem Value="5.5">5.5</asp:ListItem>
                                         <asp:ListItem Value="6">6</asp:ListItem>
                                         <asp:ListItem Value="6.5">6.5</asp:ListItem>
                                         <asp:ListItem Value="7">7</asp:ListItem>
                                          <asp:ListItem Value="7.5">7.5</asp:ListItem>
                                         <asp:ListItem Value="8.5">8</asp:ListItem>
                                          <asp:ListItem Value="9">9</asp:ListItem>
                                          <asp:ListItem Value="9.5">9.5</asp:ListItem>
                                          <asp:ListItem Value="10">10</asp:ListItem>
                                        
                                    </asp:DropDownList>
                                </div>


                              <div class="col-sm-2 col-xs-12">
                         <b>   Vendor Name :</b> <span class="text-red"> *</span>
                            <asp:TextBox ID="txt_vender_name" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number_slash(event,this);" MaxLength="30"  placeholder="Vendor Name:"></asp:TextBox>
                        </div>


                            <div class="col-sm-2 col-xs-12">
                                           <b> Vendor Contact Number : </b>
                                        <asp:TextBox ID="txt_vender_no" runat="server" MaxLength="10" class="form-control " onkeypress="return isNumber(event)" placeholder="Vendor No:"></asp:TextBox>
                                        </div>

                             <div class="col-sm-2 col-xs-12" style="margin-top: 1.5em;">
                              <b> Fire Extiguisher Report :</b>
                               <asp:FileUpload runat="server" ID="txt_fire_report" meta:resourcekey="photo_uploadResource1" onchange="ValidateSingleInput(this);" />
                               <b> JPEG JPG GIF PNG</b>
                            </div>


                             <div class="col-sm-2 col-xs-12">
                                        <br />
                                        <asp:LinkButton ID="lnk_fire_extinguisher" runat="server" OnClick="lnk_fire_extinguisher_Click" OnClientClick="return fire_extinguisher(); " AutoPostBack="true">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                        </asp:LinkButton>
                                    </div>

                            </div>
                        <br/>

                            <div id="dialog"></div>
            <div class="panel-body">
                                <asp:Panel ID="Panel26" runat="server" CssClass="grid-view">
                                    <asp:GridView ID="gridview_fire_extinguisher" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="#CCCCCC" OnRowDataBound="gridview_fire_extinguisher_RowDataBound" OnPreRender="gridview_fire_extinguisher_PreRender" BorderStyle="None" BorderWidth="1px" CellPadding="3" class="table"  Width="100%">
                                        <FooterStyle BackColor="White" ForeColor="#004C99" />
                                        <SelectedRowStyle BackColor="#d1ddf1" Font-Bold="True" ForeColor="#333333" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#224173" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#224173" ForeColor="White" HorizontalAlign="Center" />
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
                                                    <asp:LinkButton ID="lnk_remove_fire" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure You want to  Delete ?') " OnClick="lnk_remove_fire_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemStyle Width="20px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_srnumber" runat="server" Text="<%# Container.DataItemIndex+1 %>" Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
                                            <asp:BoundField DataField="fire_ex_type" HeaderText="Types Of Extiguisher" SortExpression="fire_ex_type" />
                                           <%-- <asp:BoundField DataField="renewal_date" HeaderText="Renewal Date" SortExpression="renewal_date" />--%>
                                            <asp:BoundField DataField="expiry_date" HeaderText=" Expiry date" SortExpression="expiry_date" />
                                            <asp:BoundField DataField="weight_in_kg" HeaderText="Weight In KG " SortExpression="weight_in_kg" />
                                                <asp:BoundField DataField="vender_name" HeaderText="Vender Name" SortExpression="vender_name" />
                                                <asp:BoundField DataField="vender_no" HeaderText="Vender Contact Number" SortExpression="vender_no" />
                                           

                                             <asp:TemplateField HeaderText="Fire Equipment Report">
                                <ItemTemplate>
                                    <asp:Image ID="fire_upload" runat="server" Height="50" Width="50" />
                                </ItemTemplate>
                                                    </asp:TemplateField>
                                            
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                          </div>

                                        
                        </div>

                    <br />
                </div>
                <br />
                <br />
                <div class="row text-center">
                    <asp:Button ID="btn_approval" runat="server" class="btn btn-primary" OnClick="btn_approval_Click" Text=" Approve " />
                    <asp:Button ID="btn_add" runat="server" class="btn btn-primary"
                        OnClick="btn_add_Click" Text=" Save" OnClientClick="return Req_validation();" />
                    <asp:Button ID="btn_edit" runat="server" class="btn btn-primary"
                        OnClick="btn_edit_Click" Text=" Update " OnClientClick="return r_val();" />
                    <asp:Button ID="btn_delete" runat="server" class="btn btn-primary"
                        OnClick="btn_delete_Click" Text=" Delete " OnClientClick="return R_validation();" />

                    <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" OnClick="btn_cancel_Click" Text=" Clear " />
                    <%-- <asp:Button ID="btnexporttoexcelunit" runat="server" class="btn btn-primary"
                                OnClick="btnexporttoexcelunit_Click" Text="Export TO Excel" />--%>
                    <asp:Button ID="btncloseloewe" runat="server" class="btn btn-danger"
                        OnClick="btnclose_Click" Text="Close" />
                </div>
                <br />
                <div id="free_count" class="modal fade" role="dialog">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Free Count</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-12" style="padding-left: 1%;">
                                        <asp:Panel ID="Panel11" runat="server" CssClass="grid-view">
                                            <asp:GridView ID="gv_free_count" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1" Width="100%">
                                                <Columns>

                                                    <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                    <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name " />
                                                    <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name " />
                                                    <asp:BoundField DataField="designation" HeaderText="Designation" SortExpression="designation " />
                                                    <asp:BoundField DataField="COUNT" HeaderText="Free Count" SortExpression="COUNT " />

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
                <div class="container-fluid">
                    <asp:Panel ID="Panel7" runat="server">
    <div class="row">
                      <b>Select Client :</b>
                        <asp:DropDownList ID="ddlunitclient1" runat="server" Width="20%" DataValueField="client_Code" DataTextField="client_name" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlunitclient1_SelectedIndexChanged" />
                        <a data-toggle="modal" data-target="#free_count" style="float: right; font-size: 12px; cursor: pointer;" onclick="return free_count();"><b style="color: red; font-size: 12px;"></b>Free Count</a>

      <br />

                          <div class="col-sm-2 col-xs-12">
                      
                          <asp:Button ID="btn_excel_fire_equipment" runat="server" class="btn btn-large" Width="95%" OnClientClick="return" OnClick="btn_excel_fire_equipment_Click"
                                Text="Fire Equipment Report "  />

                              </div>

</div>
                    </asp:Panel>
                </div>
                <br />
                <asp:GridView ID="UnitGridView" class="table" runat="server" AutoGenerateColumns="False"
                    CellPadding="1" ForeColor="#333333"
                    OnRowDataBound="UnitGridView_RowDataBound" Font-Size="X-Small"
                    OnSelectedIndexChanged="UnitGridView_SelectedIndexChanged" OnPreRender="UnitGridView_PreRender">
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="UNIT_CODE" HeaderText="Branch Code" SortExpression="UNIT_CODE" />
                        <asp:BoundField DataField="client_code" HeaderText="Client Code" SortExpression="client_code" />
                        <asp:BoundField DataField="STATE_NAME" HeaderText="State Name"
                            SortExpression="STATE_NAME" />
                        <asp:BoundField DataField="UNIT_NAME" HeaderText="Branch Name"
                            SortExpression="UNIT_NAME" />
                        <asp:BoundField DataField="UNIT_ADD1" HeaderText="Landmark"
                            SortExpression="UNIT_ADD1" />
                        <asp:BoundField DataField="UNIT_ADD2" HeaderText="Street/Road/Lane"
                            SortExpression="UNIT_ADD2" />
                        <asp:BoundField DataField="UNIT_CITY" HeaderText="Branch City"
                            SortExpression="UNIT_CITY" />
                        <asp:BoundField DataField="Emp_count" HeaderText="Total Employee Count" SortExpression="Emp_count" />
                        <asp:BoundField DataField="File_No" HeaderText="Remaining Employee Count" SortExpression="File_No" />
                        <asp:BoundField DataField="branch_status" HeaderText="Branch Status" SortExpression="branch_status" />


                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />

                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                <br />
                <br />
            </div>
        </asp:Panel>
    </div>
    <br />
</asp:Content>
