<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeMaster.aspx.cs" Inherits="AddNewEmployee" Title="Employee Master" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Employee Master</title>
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
    <%--  <script src="datatable/jszip.min.js"></script>--%>
    <%--    <script src="datatable/pdfmake.min.js"></script>--%>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>



    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        $(document).ready(function () {

            $("[id*=Address_Proof],[id*=original_Address_Proof],[id*=originalphoto],[id*=emp_signature],[id*=medical_form],[id*=photo_upload],[id*=bank_upload],[id*=original_adharcard_upload]," +
                "[id*=Police_document],[id*=original_police_document],[id*=Formno_2],[id*=Education_4_upload],[id*=Post_Graduation_upload]," +
                "[id*=Degree_upload],[id*=Driving_Liscence_upload],[id*=Passport_upload],[id*=biodata_upload],[id*=adhar_pan_upload],[id*=Tenth_Marksheet_upload]," +
                "[id*=Twelve_Marksheet_upload],[id*=Diploma_upload],[id*=Formno_11],[id*=noc_form],[id*=bank_passbook]").on('change', function () {
                    var numb = $(this)[0].files[0].size / 1024;
                    //numb = numb.toFixed(2);
                    if (!(numb > 0.0 && numb < 5000.0)) {
                        alert('Maximum allowed size is 5 MB. Your File Size is ' + numb);
                        $(this).replaceWith($(this).val('').clone(true));
                    }
                });



            // ddl_show_hide();
        });
    </script>
    <style type="text/css">
        .tab-section {
            background-color: #fff;
        }


        .text-red {
            color: #f00;
        }
    </style>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <style>
        .form-control {
            display: inline;
        }

        .grid-view {
            height: auto;
            max-height: 400px;
            overflow-y: auto;
            overflow-x: auto;
        }

        .grid {
            height: auto;
            max-height: 250px;
            overflow-y: auto;
            overflow-x: auto;
        }

        a label {
            cursor: pointer;
        }

        .text-right {
            text-align: left;
        }
    </style>
    <style type="text/css">
        element {
            position: fixed;
            z-index: 8001;
            left: -60.5px;
            top: 34.5px;
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
            padding-top: 10px;
            padding-left: 10px;
            z-index: 101;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }

        .row {
            margin: 0px;
        }
    </style>
    <script type="text/javascript">

        function ShowPopup() {
            $("#btnShowPopup").click();
        }

        $(function () {

            $('#<%=emp_upload.ClientID%>').click(function () {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });


        });

            function pageLoad() {

                ////////////////////
                $.fn.dataTable.ext.errMode = 'none';
                var table = $('#<%=SearchGridView.ClientID%>').DataTable(
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
               .appendTo('#<%=SearchGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';


            var table = $('#<%=gv_product_details.ClientID%>').DataTable(
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
               .appendTo('#<%=gv_product_details.ClientID%>_wrapper .col-sm-6:eq(0)');

            var table = $('#<%=gv_app_gridview.ClientID%>').DataTable(
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
               .appendTo('#<%=gv_app_gridview.ClientID%>_wrapper .col-sm-6:eq(0)');
            $('#<%=btnUpload.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=SearchGridView.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddlunitclient1.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_gv_statewise.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_gv_branchwise.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_unit_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_clientwisestate.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_unitcode.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_grade.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_reporting_to.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=txt_attendanceid.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_permstate.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=select_designation.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_product_type.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=uniform_size.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_app_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_app_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_app_unit.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_app_emp.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });


            $(document).ready(function () {
                $('body').on('keyup', '.maskedExt', function () {
                    var num = $(this).attr("maskedFormat").toString().split(',');
                    var regex = new RegExp("^\\d{0," + num[0] + "}(\\.\\d{0," + num[1] + "})?$");
                    if (!regex.test(this.value)) {
                        this.value = this.value.substring(0, this.value.length - 1);
                    }
                });
            });

            $(".date-picker1").datepicker({

                dateFormat: "dd/mm/yy",
                onSelect: function (dateText, instance) {
                    date = $.datepicker.parseDate(instance.settings.dateFormat, dateText, instance.settings);
                    date.setMonth(date.getMonth() + 12);
                    $(".date-picker2").datepicker("setDate", date);
                }
            });


            $(".date-picker2").datepicker({
                dateFormat: "dd/mm/yy"
            });



            $(".date-picker_du1").datepicker({

                dateFormat: "dd/mm/yy",
                onSelect: function (dateText, instance) {
                    date = $.datepicker.parseDate(instance.settings.dateFormat, dateText, instance.settings);
                    date.setMonth(date.getMonth() + 12);
                    $(".date-picker_du2").datepicker("setDate", date);
                }
            });


            $(".date-picker_du2").datepicker({
                dateFormat: "dd/mm/yy"
            });




            var txt_left_date = document.getElementById('<%=txt_leftdate.ClientID %>');
            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                maxDate: '-18Y',
                yearRange: '-110:-18',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });

            $(".confirm_date").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                //yearRange: '1950',
                onSelect: function (selected) {
                    $(".date_join").datepicker("option", "minDate", selected)
                }
            });


            $(".date_join").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                yearRange: '1950:2050',
                onSelect: function (selected) {
                    $(".confirm_date").datepicker("option", "maxDate", selected)
                    $(".date_left").datepicker("option", "minDate", selected)
                }
            });


            $(".date_left").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                // maxDate: 0,
                yearRange: '1950',
                onSelect: function (selected) {
                    $(".date_join").datepicker("option", "maxDate", selected)


                }
            });

            $('.pass_vissa').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
                maxDate: '+20Y',

                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });

            $('.pass_vissa_passport').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
                maxDate: '+10Y',

                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });

            $('.date-EMI').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                yearRange: '1950',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });


            $(".date-picker11").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    $(".date-picker12").datepicker("option", "minDate", selected)
                }
            });


            $(".date-picker12").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    $(".date-picker11").datepicker("option", "maxDate", selected)
                }
            });
            $(".txt_Nationality").attr("readonly", "true");
            $(".date-EMI").attr("readonly", "true");
            $(".pass_vissa").attr("readonly", "true");
            $(".pass_vissa_passport").attr("readonly", "true");
            $(".date-picker").attr("readonly", "false");
            $(".date_join").attr("readonly", "true");
            $(".confirm_date").attr("readonly", "true");
            $(".date_left").attr("readonly", "true");
            $(".date-picker11").attr("readonly", "true");
            $(".date-picker12").attr("readonly", "true");
            $(".date-picker1").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");

            $(".js-example-basic-single").select2();
            // location_hidden();
            $('#<%=btnhelp.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btndelete.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_add_employee.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

            $('#<%=ddl_adhar_search.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_adhar_add_emp.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_emp_approve.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_history.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

            var table = $('#<%=gv_search_adharcardno.ClientID%>').DataTable(
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
               .appendTo('#<%=gv_search_adharcardno.ClientID%>_wrapper .col-sm-6:eq(0)');
            ddl_show_hide();
        }



        $(document).ready(function () {
            var evt = null;
            isNumber(evt);
            isNumber_bank(evt);

        });
        function AllowAlphabet_Number10(e) {
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

        function callfnc() {

            document.getElementById('<%= Button5.ClientID %>').click();

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
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            return true;
        }

        //vikas for bank
        function isNumber_bank(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 47 || charCode > 57)) {
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
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '32') || keyEntry == '45' || (keyEntry == '8') || (keyEntry == '9') || (keyEntry == '46'))
                return true;
            else {
                // alert('Please Enter Only Character values.');
                return false;
            }
        }

        function AllowAlphabet10(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '32') || keyEntry == '45' || (keyEntry == '8') || (keyEntry == '9') || (keyEntry == '46') || (keyEntry == '44'))
                return true;
            else {
                // alert('Please Enter Only Character values.');
                return false;
            }
        }





        function update_validation() {

            var reason_update = document.getElementById('<%=txt_reason_updation.ClientID %>');

            var txt_emptype = document.getElementById('<%=ddl_employee_type.ClientID %>');
            var Selected_type = txt_emptype.options[txt_emptype.selectedIndex].text;


            var txt_department = document.getElementById('<%=ddl_department.ClientID %>');
            var Selected_department = txt_emptype.options[txt_department.selectedIndex].text;


            var e_name = document.getElementById('<%=txt_eename.ClientID %>');
            var e_fatharname = document.getElementById('<%=txt_eefatharname.ClientID %>');
            var e_birthdate = document.getElementById('<%=txt_birthdate.ClientID %>');

            var txt_clientwisestate = document.getElementById('<%=ddl_clientwisestate.ClientID %>');
            var Selected_clientwiswstate = txt_clientwisestate.options[txt_clientwisestate.selectedIndex].text;

            var txt_client = document.getElementById('<%=ddl_unit_client.ClientID %>');
            var Selected_client = txt_client.options[txt_client.selectedIndex].text;
            var txt_unit = document.getElementById('<%=ddl_unitcode.ClientID %>');
            var Selected_unit = txt_unit.options[txt_unit.selectedIndex].text;
            var txt_grade = document.getElementById('<%=ddl_grade.ClientID %>');
            var Selected_grade = txt_grade.options[txt_grade.selectedIndex].text;
            var l_role = document.getElementById('<%=DropDownList1.ClientID %>');
            var SelectedText11 = l_role.options[l_role.selectedIndex].text;
            var t_email = document.getElementById('<%= txt_email.ClientID %>');
            var t_workinghrs = document.getElementById('<%= txt_attendanceid.ClientID %>');
            var email = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;

            ///////*********************************************************************
            var e_permanantaddress = document.getElementById('<%=txt_permanantaddress.ClientID %>');

            var e_presentcity = document.getElementById('<%=txt_presentaddress.ClientID %>');

            var t_state = document.getElementById('<%=ddl_state.ClientID %>');
            var SelectedText2 = t_state.options[t_state.selectedIndex].text;
            var t_City = document.getElementById('<%=txt_presentcity.ClientID %>');

            var e_mobilenumber = document.getElementById('<%=txt_mobilenumber.ClientID %>');


            ///////*********************************************************************

            var e_joiningdate = document.getElementById('<%=txt_joiningdate.ClientID %>');
            var e_state = document.getElementById('<%=ddl_location.ClientID%>');
            var e_city = document.getElementById('<%=ddl_location_city.ClientID%>');
            var e_ptaxnumber = document.getElementById('<%=txt_ptaxnumber.ClientID %>');
            var work_state = document.getElementById('<%=ddl_location.ClientID %>');

            var work_city = document.getElementById('<%=ddl_location_city.ClientID %>');

            var txt_left_date = document.getElementById('<%=txt_leftdate.ClientID %>');
            var txt_left_reason = document.getElementById('<%=txtreasonforleft.ClientID %>');




            ///////*********************************************************************
            var t_pfbankname = document.getElementById('<%=txt_pfbankname.ClientID %>');
            var t_pfifsccode = document.getElementById('<%=txt_pfifsccode.ClientID %>');
            var t_employeeaccountnumber = document.getElementById('<%=txt_employeeaccountnumber.ClientID %>');
            var txt_originalbankaccountno = document.getElementById('<%=txt_originalbankaccountno.ClientID %>');
            var t_ddl_bankcode = document.getElementById('<%=ddl_bankcode .ClientID %>');
            var txt_bnk_transfer = document.getElementById('<%=ddl_infitcode.ClientID %>');
            var Selected_bnk_tranfer = txt_bnk_transfer.options[txt_bnk_transfer.selectedIndex].text;

            ///////*********************************************************************
            var e_Nationality = document.getElementById('<%=txt_Nationality.ClientID %>');




            ///////*********************************************************************

            var t_txt_bankholder = document.getElementById('<%=txt_bankholder.ClientID %>');



            ///////*********************************************************************

            if (Selected_type == "Select") {
                alert("Please Select Employee Type !!!");
                txt_emptype.focus();
                return false;
            }

            //Department type

            if (Selected_department == "Select") {
                alert("Please Select Department type !!!");
                txt_department.focus();
                return false;
            }
            // client Name
            if (Selected_client == "Select") {
                alert("Please Select Client Name !!!");
                txt_client.focus();
                return false;
            }

            // Unit Name
            if (Selected_clientwiswstate == "Select") {
                alert("Please Select State !!!");
                txt_clientwisestate.focus();
                return false;
            }

            // Unit Name
            if (Selected_unit == "Select") {
                alert("Please Select Branch Name !!!");
                txt_unit.focus();
                return false;
            }


            // Grade
            if (Selected_grade == "Select") {
                alert("Please Select Designation !!!");
                txt_grade.focus();
                return false;
            }


            // project name

            if (e_name.value == "") {
                alert("Please Enter Employee Name");
                e_name.focus();
                return false;
            }
            // Father/ Husband Name

            if (e_fatharname.value == "") {
                alert("Please Enter Father/ Husband Name");
                e_fatharname.focus();
                return false;
            }


            // Date of Birth

            if (e_birthdate.value == "") {
                alert("Please Select Date of Birth");
                e_birthdate.focus();
                return false;
            }
            var ddl_gender = document.getElementById('<%=ddl_gender.ClientID %>');
            var Selected_ddl_gender = ddl_gender.options[ddl_gender.selectedIndex].text;

            if (Selected_ddl_gender == "Select") {
                alert("Please Select Gender");
                ddl_gender.focus();
                return false;
            }
            // role:

            if (SelectedText11 == "--Select Role--") {
                alert("Please Select Role !!!");
                l_role.focus();
                return false;
            }

            if (t_workinghrs.value == "") {
                alert("Please Enter Working Hours.");
                t_workinghrs.focus();
                return false;
            }






            if ((Selected_type == "Permanent") || (Selected_type == "Permanent Reliever") || (Selected_type == "Staff") || (Selected_type == "Temporary") || (Selected_type == "Repair & Maintenance")) {

                if (e_presentcity.value == "") {
                    alert("Please Enter Present Address");
                    $("#menu1").removeClass("fade in active");
                    $("#menu6").removeClass("fade in active");
                    $("#menu7").removeClass("fade in active");
                    $("#menu2").removeClass("fade in active");
                    $("#menu3").removeClass("fade in active");
                    $("#menu4").removeClass("fade in active");
                    $("#menu5").removeClass("fade in active");
                    $("#menu8").removeClass("fade in active");
                    $("#menu9").removeClass("fade in active");
                    $("#tabactive2").removeClass("active");
                    $("#tabactive3").removeClass("active");
                    $("#tabactive4").removeClass("active");
                    $("#tabactive5").removeClass("active");
                    $("#tabactive6").removeClass("active");
                    $("#tabactive7").removeClass("active");
                    $("#tabactive8").removeClass("active");
                    $("#tabactive9").removeClass("active");
                    $("#tabactive10").removeClass("active");
                    $("#tabactive1").addClass("active");
                    $("#home").addClass("fade in active");
                    e_presentcity.focus();
                    return false;
                }
                if (SelectedText2 == "Select") {
                    alert("Please Select State  !!!");
                    t_state.focus();
                    return false;
                }
                // City

                if (t_City.value == "Select") {
                    alert("Please Enter Present  City");
                    t_City.focus();
                    return false;
                }


                //Mobile Number

                if (e_mobilenumber.value == "") {
                    alert("Please Enter Mobile Number");
                    $("#menu1").removeClass("fade in active");
                    $("#menu6").removeClass("fade in active");
                    $("#menu7").removeClass("fade in active");
                    $("#menu2").removeClass("fade in active");
                    $("#menu3").removeClass("fade in active");
                    $("#menu4").removeClass("fade in active");
                    $("#menu5").removeClass("fade in active");
                    $("#menu8").removeClass("fade in active");
                    $("#menu9").removeClass("fade in active");
                    $("#tabactive2").removeClass("active");
                    $("#tabactive3").removeClass("active");
                    $("#tabactive4").removeClass("active");
                    $("#tabactive5").removeClass("active");
                    $("#tabactive6").removeClass("active");
                    $("#tabactive7").removeClass("active");
                    $("#tabactive8").removeClass("active");
                    $("#tabactive9").removeClass("active");
                    $("#tabactive10").removeClass("active");
                    $("#tabactive1").addClass("active");
                    $("#home").addClass("fade in active");
                    e_mobilenumber.focus();
                    return false;
                }
            }







            // Joining Date

            if (e_joiningdate.value == "") {
                alert("Please Select Joining Date");
                $("#home").removeClass("fade in active");
                $("#menu6").removeClass("fade in active");
                $("#menu7").removeClass("fade in active");
                $("#menu2").removeClass("fade in active");
                $("#menu3").removeClass("fade in active");
                $("#menu4").removeClass("fade in active");
                $("#menu5").removeClass("fade in active");
                $("#menu8").removeClass("fade in active");
                $("#menu9").removeClass("fade in active");
                $("#tabactive1").removeClass("active");
                $("#tabactive3").removeClass("active");
                $("#tabactive4").removeClass("active");
                $("#tabactive5").removeClass("active");
                $("#tabactive6").removeClass("active");
                $("#tabactive7").removeClass("active");
                $("#tabactive8").removeClass("active");
                $("#tabactive9").removeClass("active");
                $("#tabactive10").removeClass("active");
                $("#tabactive2").addClass("active");
                $("#menu1").addClass("fade in active");

                e_joiningdate.focus();
                return false;
            }

            // location work state
            if (work_state.value == "") {
                alert("Please Enter Work State");
                $("#home").removeClass("fade in active");
                $("#menu6").removeClass("fade in active");
                $("#menu7").removeClass("fade in active");
                $("#menu2").removeClass("fade in active");
                $("#menu3").removeClass("fade in active");
                $("#menu4").removeClass("fade in active");
                $("#menu5").removeClass("fade in active");
                $("#menu8").removeClass("fade in active");
                $("#menu9").removeClass("fade in active");
                $("#tabactive1").removeClass("active");
                $("#tabactive3").removeClass("active");
                $("#tabactive4").removeClass("active");
                $("#tabactive5").removeClass("active");
                $("#tabactive6").removeClass("active");
                $("#tabactive7").removeClass("active");
                $("#tabactive8").removeClass("active");
                $("#tabactive9").removeClass("active");
                $("#tabactive10").removeClass("active");
                $("#tabactive2").addClass("active");
                $("#menu1").addClass("fade in active");
                work_state.focus();
                return false;
            }

            // location work state
            if (work_city.value == "") {
                alert("Please Enter Work City");
                $("#home").removeClass("fade in active");
                $("#menu6").removeClass("fade in active");
                $("#menu7").removeClass("fade in active");
                $("#menu2").removeClass("fade in active");
                $("#menu3").removeClass("fade in active");
                $("#menu4").removeClass("fade in active");
                $("#menu5").removeClass("fade in active");
                $("#menu8").removeClass("fade in active");
                $("#menu9").removeClass("fade in active");
                $("#tabactive1").removeClass("active");
                $("#tabactive3").removeClass("active");
                $("#tabactive4").removeClass("active");
                $("#tabactive5").removeClass("active");
                $("#tabactive6").removeClass("active");
                $("#tabactive7").removeClass("active");
                $("#tabactive8").removeClass("active");
                $("#tabactive9").removeClass("active");
                $("#tabactive10").removeClass("active");
                $("#tabactive2").addClass("active");
                $("#menu1").addClass("fade in active");
                work_city.focus();
                return false;
            }

            if (Selected_clientwiswstate == "Arunachal Pradesh" || Selected_clientwiswstate == "Assam" || Selected_clientwiswstate == "Manipur" || Selected_clientwiswstate == "Meghalaya" || Selected_clientwiswstate == "Mizoram " || Selected_clientwiswstate == "Nagaland" || Selected_clientwiswstate == "Sikkim" || Selected_clientwiswstate == "Tripura") {
                if (e_ptaxnumber.value == "") {
                    return true;
                }
            }
            // Adhar Card No\Enrollment No
            var chk_state = document.getElementById('<%=ddl_clientwisestate.ClientID %>');
            var Selected_clientwiswstate = chk_state.options[chk_state.selectedIndex].text;
            var e_ptaxnumber = document.getElementById('<%=txt_ptaxnumber.ClientID %>');
            if ((Selected_type == "Permanent Reliever") || (Selected_type == "Permanent") || (Selected_type == "Staff") || (Selected_type == "Temporary") || (Selected_type == "Repair & Maintenance")) {

                if (e_ptaxnumber.value == "") {
                    alert("Please Enter Adhar Card Number");
                    e_ptaxnumber.focus();
                    return false;
                }
                var str_adhar = e_ptaxnumber.value.length;
                //rahul




                if (str_adhar != "12") {


                    $("#home").removeClass("fade in active");
                    $("#menu6").removeClass("fade in active");
                    $("#menu7").removeClass("fade in active");
                    $("#menu2").removeClass("fade in active");
                    $("#menu3").removeClass("fade in active");
                    $("#menu4").removeClass("fade in active");
                    $("#menu5").removeClass("fade in active");
                    $("#menu8").removeClass("fade in active");
                    $("#menu9").removeClass("fade in active");
                    $("#tabactive1").removeClass("active");
                    $("#tabactive3").removeClass("active");
                    $("#tabactive4").removeClass("active");
                    $("#tabactive5").removeClass("active");
                    $("#tabactive6").removeClass("active");
                    $("#tabactive7").removeClass("active");
                    $("#tabactive8").removeClass("active");
                    $("#tabactive9").removeClass("active");
                    $("#tabactive10").removeClass("active");
                    $("#tabactive2").addClass("active");
                    $("#menu1").addClass("fade in active");
                    alert("Please Enter Valid Adhar Card Number");
                    e_ptaxnumber.focus();
                    return false;
                }
            }
            if (txt_left_date.value != "") {
                if (txt_left_reason.value == "") {
                    alert("Please enter reason for resign");
                    $("#home").removeClass("fade in active");
                    $("#menu6").removeClass("fade in active");
                    $("#menu7").removeClass("fade in active");
                    $("#menu2").removeClass("fade in active");
                    $("#menu3").removeClass("fade in active");
                    $("#menu4").removeClass("fade in active");
                    $("#menu5").removeClass("fade in active");
                    $("#menu8").removeClass("fade in active");
                    $("#menu9").removeClass("fade in active");
                    $("#tabactive1").removeClass("active");
                    $("#tabactive3").removeClass("active");
                    $("#tabactive4").removeClass("active");
                    $("#tabactive5").removeClass("active");
                    $("#tabactive6").removeClass("active");
                    $("#tabactive7").removeClass("active");
                    $("#tabactive8").removeClass("active");
                    $("#tabactive9").removeClass("active");
                    $("#tabactive10").removeClass("active");
                    $("#tabactive2").addClass("active");
                    $("#menu1").addClass("fade in active");
                    txt_left_reason.focus();
                    return false;
                }
            }

            if (txt_left_reason.value != "") {
                if (txt_left_date.value == "") {
                    alert("Please select Left Date");
                    $("#home").removeClass("fade in active");
                    $("#menu6").removeClass("fade in active");
                    $("#menu7").removeClass("fade in active");
                    $("#menu2").removeClass("fade in active");
                    $("#menu3").removeClass("fade in active");
                    $("#menu4").removeClass("fade in active");
                    $("#menu5").removeClass("fade in active");
                    $("#menu8").removeClass("fade in active");
                    $("#menu9").removeClass("fade in active");
                    $("#tabactive1").removeClass("active");
                    $("#tabactive3").removeClass("active");
                    $("#tabactive4").removeClass("active");
                    $("#tabactive5").removeClass("active");
                    $("#tabactive6").removeClass("active");
                    $("#tabactive7").removeClass("active");
                    $("#tabactive8").removeClass("active");
                    $("#tabactive9").removeClass("active");
                    $("#tabactive10").removeClass("active");
                    $("#tabactive2").addClass("active");
                    $("#menu1").addClass("fade in active");
                    txt_left_date.focus();
                    return false;
                }
            }

            var txt_bankholder = document.getElementById('<%=txt_bankholder.ClientID %>');
            if (txt_bankholder.value == "") {
                alert("Please Enter Bank Account Holder Name");
                txt_bankholder.focus();
                return false;
            }
            var txt_originalbankaccountno = document.getElementById('<%=txt_originalbankaccountno.ClientID %>');
            if (txt_originalbankaccountno.value == "") {
                alert("Please Enter Original Bank Account Number");
                txt_originalbankaccountno.focus();
                return false;
            }
            var txt_pfifsccode = document.getElementById('<%=txt_pfifsccode.ClientID %>');
            if (txt_pfifsccode.value == "") {
                alert("Please Enter Bank IFSC Code");
                txt_pfifsccode.focus();
                return false;
            }
            if (txt_pfifsccode.value.length != 11) {
                alert("Please Enter Valid IFSC Code");
                txt_pfifsccode.focus();
                return false;
            }
            // Bank Transfer

            if (Selected_bnk_tranfer == "Select Transfer") {
                alert("Please Select Transfer");
                $("#home").removeClass("fade in active");
                $("#menu1").removeClass("fade in active");
                $("#menu7").removeClass("fade in active");
                $("#menu2").removeClass("fade in active");
                $("#menu3").removeClass("fade in active");
                $("#menu4").removeClass("fade in active");
                $("#menu5").removeClass("fade in active");
                $("#menu8").removeClass("fade in active");
                $("#menu9").removeClass("fade in active");
                $("#tabactive1").removeClass("active");
                $("#tabactive2").removeClass("active");
                $("#tabactive4").removeClass("active");
                $("#tabactive5").removeClass("active");
                $("#tabactive6").removeClass("active");
                $("#tabactive7").removeClass("active");
                $("#tabactive8").removeClass("active");
                $("#tabactive9").removeClass("active");
                $("#tabactive3").addClass("active");
                $("#menu6").addClass("fade in active");
                txt_bnk_transfer.focus();
                return false;
            }
            ///////*********************************************************************

            // Nationality

            if (e_Nationality.value == "") {
                alert("Please Enter Nationality");
                $("#home").removeClass("fade in active");
                $("#menu1").removeClass("fade in active");
                $("#menu6").removeClass("fade in active");
                $("#menu2").removeClass("fade in active");
                $("#menu3").removeClass("fade in active");
                $("#menu4").removeClass("fade in active");
                $("#menu5").removeClass("fade in active");
                $("#menu8").removeClass("fade in active");
                $("#tabactive1").removeClass("active");
                $("#tabactive2").removeClass("active");
                $("#tabactive3").removeClass("active");
                $("#tabactive5").removeClass("active");
                $("#tabactive6").removeClass("active");
                $("#tabactive7").removeClass("active");
                $("#tabactive8").removeClass("active");
                $("#tabactive9").removeClass("active");
                $("#tabactive4").addClass("active");
                $("#menu7").addClass("fade in active");
                e_Nationality.focus();
                return false;
            }
            //var itc_upload_form = document.getElementById('<%=itc_upload_form.ClientID %>');
            //if (itc_upload_form.value == "") {
            //    alert("Please Select ITC Document in Document Tab");
            //    itc_upload_form.focus();
            //    return false;
            //}

            var reason_update = document.getElementById('<%=txt_reason_updation.ClientID %>');
            if (!reason_update.disabled) {
                if (reason_update.value == "") {
                    alert("Please Specify Reason For Updation !!!");
                    reason_update.focus();
                    return false;
                }
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }


        }
        function update_validation2() {

            var reason_update = document.getElementById('<%=txt_reason_updation.ClientID %>');

            var txt_emptype = document.getElementById('<%=ddl_employee_type.ClientID %>');
            var Selected_type = txt_emptype.options[txt_emptype.selectedIndex].text;


            var txt_department = document.getElementById('<%=ddl_department.ClientID %>');
            var Selected_department = txt_emptype.options[txt_department.selectedIndex].text;


            var e_name = document.getElementById('<%=txt_eename.ClientID %>');
             var e_fatharname = document.getElementById('<%=txt_eefatharname.ClientID %>');
            var e_birthdate = document.getElementById('<%=txt_birthdate.ClientID %>');

            var txt_clientwisestate = document.getElementById('<%=ddl_clientwisestate.ClientID %>');
            var Selected_clientwiswstate = txt_clientwisestate.options[txt_clientwisestate.selectedIndex].text;

            var txt_client = document.getElementById('<%=ddl_unit_client.ClientID %>');
            var Selected_client = txt_client.options[txt_client.selectedIndex].text;
            var txt_unit = document.getElementById('<%=ddl_unitcode.ClientID %>');
             var Selected_unit = txt_unit.options[txt_unit.selectedIndex].text;
             var txt_grade = document.getElementById('<%=ddl_grade.ClientID %>');
            var Selected_grade = txt_grade.options[txt_grade.selectedIndex].text;
            var l_role = document.getElementById('<%=DropDownList1.ClientID %>');
            var SelectedText11 = l_role.options[l_role.selectedIndex].text;
            var t_email = document.getElementById('<%= txt_email.ClientID %>');
            var t_workinghrs = document.getElementById('<%= txt_attendanceid.ClientID %>');
            var email = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;

            ///////*********************************************************************
            var e_permanantaddress = document.getElementById('<%=txt_permanantaddress.ClientID %>');

            var e_presentcity = document.getElementById('<%=txt_presentaddress.ClientID %>');

            var t_state = document.getElementById('<%=ddl_state.ClientID %>');
            var SelectedText2 = t_state.options[t_state.selectedIndex].text;
            var t_City = document.getElementById('<%=txt_presentcity.ClientID %>');

            var e_mobilenumber = document.getElementById('<%=txt_mobilenumber.ClientID %>');


            ///////*********************************************************************

            var e_joiningdate = document.getElementById('<%=txt_joiningdate.ClientID %>');
            var e_state = document.getElementById('<%=ddl_location.ClientID%>');
            var e_city = document.getElementById('<%=ddl_location_city.ClientID%>');
            var e_ptaxnumber = document.getElementById('<%=txt_ptaxnumber.ClientID %>');
            var work_state = document.getElementById('<%=ddl_location.ClientID %>');

            var work_city = document.getElementById('<%=ddl_location_city.ClientID %>');

            var txt_left_date = document.getElementById('<%=txt_leftdate.ClientID %>');
            var txt_left_reason = document.getElementById('<%=txtreasonforleft.ClientID %>');




            ///////*********************************************************************
            var t_pfbankname = document.getElementById('<%=txt_pfbankname.ClientID %>');
            var t_pfifsccode = document.getElementById('<%=txt_pfifsccode.ClientID %>');
            var t_employeeaccountnumber = document.getElementById('<%=txt_employeeaccountnumber.ClientID %>');
            var txt_originalbankaccountno = document.getElementById('<%=txt_originalbankaccountno.ClientID %>');
            var t_ddl_bankcode = document.getElementById('<%=ddl_bankcode .ClientID %>');
            var txt_bnk_transfer = document.getElementById('<%=ddl_infitcode.ClientID %>');
            var Selected_bnk_tranfer = txt_bnk_transfer.options[txt_bnk_transfer.selectedIndex].text;

            ///////*********************************************************************
            var e_Nationality = document.getElementById('<%=txt_Nationality.ClientID %>');




            ///////*********************************************************************

            var t_txt_bankholder = document.getElementById('<%=txt_bankholder.ClientID %>');



            ///////*********************************************************************

            if (Selected_type == "Select") {
                alert("Please Select Employee Type !!!");
                txt_emptype.focus();
                return false;
            }

            //Department type

            if (Selected_department == "Select") {
                alert("Please Select Department type !!!");
                txt_department.focus();
                return false;
            }
            // client Name
            if (Selected_client == "Select") {
                alert("Please Select Client Name !!!");
                txt_client.focus();
                return false;
            }

            // Unit Name
            if (Selected_clientwiswstate == "Select") {
                alert("Please Select State !!!");
                txt_clientwisestate.focus();
                return false;
            }

            // Unit Name
            if (Selected_unit == "Select") {
                alert("Please Select Branch Name !!!");
                txt_unit.focus();
                return false;
            }


            // Grade
            if (Selected_grade == "Select") {
                alert("Please Select Designation !!!");
                txt_grade.focus();
                return false;
            }


            // project name

            if (e_name.value == "") {
                alert("Please Enter Employee Name");
                e_name.focus();
                return false;
            }
            // Father/ Husband Name

            if (e_fatharname.value == "") {
                alert("Please Enter Father/ Husband Name");
                e_fatharname.focus();
                return false;
            }


            // Date of Birth

            if (e_birthdate.value == "") {
                alert("Please Select Date of Birth");
                e_birthdate.focus();
                return false;
            }
            var ddl_gender = document.getElementById('<%=ddl_gender.ClientID %>');
            var Selected_ddl_gender = ddl_gender.options[ddl_gender.selectedIndex].text;

            if (Selected_ddl_gender == "Select") {
                alert("Please Select Gender");
                ddl_gender.focus();
                return false;
            }
            // role:

            if (SelectedText11 == "--Select Role--") {
                alert("Please Select Role !!!");
                l_role.focus();
                return false;
            }

            if (t_workinghrs.value == "") {
                alert("Please Enter Working Hours.");
                t_workinghrs.focus();
                return false;
            }








            if (e_presentcity.value == "") {
                alert("Please Enter Present Address");
                $("#menu1").removeClass("fade in active");
                $("#menu6").removeClass("fade in active");
                $("#menu7").removeClass("fade in active");
                $("#menu2").removeClass("fade in active");
                $("#menu3").removeClass("fade in active");
                $("#menu4").removeClass("fade in active");
                $("#menu5").removeClass("fade in active");
                $("#menu8").removeClass("fade in active");
                $("#menu9").removeClass("fade in active");
                $("#tabactive2").removeClass("active");
                $("#tabactive3").removeClass("active");
                $("#tabactive4").removeClass("active");
                $("#tabactive5").removeClass("active");
                $("#tabactive6").removeClass("active");
                $("#tabactive7").removeClass("active");
                $("#tabactive8").removeClass("active");
                $("#tabactive9").removeClass("active");
                $("#tabactive10").removeClass("active");
                $("#tabactive1").addClass("active");
                $("#home").addClass("fade in active");
                e_presentcity.focus();
                return false;
            }
            if (SelectedText2 == "Select") {
                alert("Please Select State  !!!");
                t_state.focus();
                return false;
            }
            // City

            if (t_City.value == "Select") {
                alert("Please Enter Present  City");
                t_City.focus();
                return false;
            }


            //Mobile Number

            if (e_mobilenumber.value == "") {
                alert("Please Enter Mobile Number");
                $("#menu1").removeClass("fade in active");
                $("#menu6").removeClass("fade in active");
                $("#menu7").removeClass("fade in active");
                $("#menu2").removeClass("fade in active");
                $("#menu3").removeClass("fade in active");
                $("#menu4").removeClass("fade in active");
                $("#menu5").removeClass("fade in active");
                $("#menu8").removeClass("fade in active");
                $("#menu9").removeClass("fade in active");
                $("#tabactive2").removeClass("active");
                $("#tabactive3").removeClass("active");
                $("#tabactive4").removeClass("active");
                $("#tabactive5").removeClass("active");
                $("#tabactive6").removeClass("active");
                $("#tabactive7").removeClass("active");
                $("#tabactive8").removeClass("active");
                $("#tabactive9").removeClass("active");
                $("#tabactive10").removeClass("active");
                $("#tabactive1").addClass("active");
                $("#home").addClass("fade in active");
                e_mobilenumber.focus();
                return false;
            }








            // Joining Date

            if (e_joiningdate.value == "") {
                alert("Please Select Joining Date");
                $("#home").removeClass("fade in active");
                $("#menu6").removeClass("fade in active");
                $("#menu7").removeClass("fade in active");
                $("#menu2").removeClass("fade in active");
                $("#menu3").removeClass("fade in active");
                $("#menu4").removeClass("fade in active");
                $("#menu5").removeClass("fade in active");
                $("#menu8").removeClass("fade in active");
                $("#menu9").removeClass("fade in active");
                $("#tabactive1").removeClass("active");
                $("#tabactive3").removeClass("active");
                $("#tabactive4").removeClass("active");
                $("#tabactive5").removeClass("active");
                $("#tabactive6").removeClass("active");
                $("#tabactive7").removeClass("active");
                $("#tabactive8").removeClass("active");
                $("#tabactive9").removeClass("active");
                $("#tabactive10").removeClass("active");
                $("#tabactive2").addClass("active");
                $("#menu1").addClass("fade in active");

                e_joiningdate.focus();
                return false;
            }

            // location work state
            if (work_state.value == "") {
                alert("Please Enter Work State");
                $("#home").removeClass("fade in active");
                $("#menu6").removeClass("fade in active");
                $("#menu7").removeClass("fade in active");
                $("#menu2").removeClass("fade in active");
                $("#menu3").removeClass("fade in active");
                $("#menu4").removeClass("fade in active");
                $("#menu5").removeClass("fade in active");
                $("#menu8").removeClass("fade in active");
                $("#menu9").removeClass("fade in active");
                $("#tabactive1").removeClass("active");
                $("#tabactive3").removeClass("active");
                $("#tabactive4").removeClass("active");
                $("#tabactive5").removeClass("active");
                $("#tabactive6").removeClass("active");
                $("#tabactive7").removeClass("active");
                $("#tabactive8").removeClass("active");
                $("#tabactive9").removeClass("active");
                $("#tabactive10").removeClass("active");
                $("#tabactive2").addClass("active");
                $("#menu1").addClass("fade in active");
                work_state.focus();
                return false;
            }

            // location work state
            if (work_city.value == "") {
                alert("Please Enter Work City");
                $("#home").removeClass("fade in active");
                $("#menu6").removeClass("fade in active");
                $("#menu7").removeClass("fade in active");
                $("#menu2").removeClass("fade in active");
                $("#menu3").removeClass("fade in active");
                $("#menu4").removeClass("fade in active");
                $("#menu5").removeClass("fade in active");
                $("#menu8").removeClass("fade in active");
                $("#menu9").removeClass("fade in active");
                $("#tabactive1").removeClass("active");
                $("#tabactive3").removeClass("active");
                $("#tabactive4").removeClass("active");
                $("#tabactive5").removeClass("active");
                $("#tabactive6").removeClass("active");
                $("#tabactive7").removeClass("active");
                $("#tabactive8").removeClass("active");
                $("#tabactive9").removeClass("active");
                $("#tabactive10").removeClass("active");
                $("#tabactive2").addClass("active");
                $("#menu1").addClass("fade in active");
                work_city.focus();
                return false;
            }


            // Adhar Card No\Enrollment No
            var chk_state = document.getElementById('<%=ddl_clientwisestate.ClientID %>');
             var Selected_clientwiswstate = chk_state.options[chk_state.selectedIndex].text;
             var e_ptaxnumber = document.getElementById('<%=txt_ptaxnumber.ClientID %>');
             var str_adhar = e_ptaxnumber.value.length;
            //rahul

             if (Selected_clientwiswstate == "Arunachal Pradesh" || Selected_clientwiswstate == "Assam" || Selected_clientwiswstate == "Manipur" || Selected_clientwiswstate == "Meghalaya" || Selected_clientwiswstate == "Mizoram" || Selected_clientwiswstate == "Nagaland" || Selected_clientwiswstate == "Sikkim" || Selected_clientwiswstate == "Tripura") {

             }

             else {

                 if (str_adhar != "12") {


                     $("#home").removeClass("fade in active");
                     $("#menu6").removeClass("fade in active");
                     $("#menu7").removeClass("fade in active");
                     $("#menu2").removeClass("fade in active");
                     $("#menu3").removeClass("fade in active");
                     $("#menu4").removeClass("fade in active");
                     $("#menu5").removeClass("fade in active");
                     $("#menu8").removeClass("fade in active");
                     $("#menu9").removeClass("fade in active");
                     $("#tabactive1").removeClass("active");
                     $("#tabactive3").removeClass("active");
                     $("#tabactive4").removeClass("active");
                     $("#tabactive5").removeClass("active");
                     $("#tabactive6").removeClass("active");
                     $("#tabactive7").removeClass("active");
                     $("#tabactive8").removeClass("active");
                     $("#tabactive9").removeClass("active");
                     $("#tabactive10").removeClass("active");
                     $("#tabactive2").addClass("active");
                     $("#menu1").addClass("fade in active");
                     alert("Please Enter Valid Adharcard Number");
                     e_ptaxnumber.focus();
                     return false;
                 }



             }




             if (txt_left_date.value != "") {
                 if (txt_left_reason.value == "") {
                     alert("Please enter reason for resign");
                     $("#home").removeClass("fade in active");
                     $("#menu6").removeClass("fade in active");
                     $("#menu7").removeClass("fade in active");
                     $("#menu2").removeClass("fade in active");
                     $("#menu3").removeClass("fade in active");
                     $("#menu4").removeClass("fade in active");
                     $("#menu5").removeClass("fade in active");
                     $("#menu8").removeClass("fade in active");
                     $("#menu9").removeClass("fade in active");
                     $("#tabactive1").removeClass("active");
                     $("#tabactive3").removeClass("active");
                     $("#tabactive4").removeClass("active");
                     $("#tabactive5").removeClass("active");
                     $("#tabactive6").removeClass("active");
                     $("#tabactive7").removeClass("active");
                     $("#tabactive8").removeClass("active");
                     $("#tabactive9").removeClass("active");
                     $("#tabactive10").removeClass("active");
                     $("#tabactive2").addClass("active");
                     $("#menu1").addClass("fade in active");
                     txt_left_reason.focus();
                     return false;
                 }
             }

             if (txt_left_reason.value != "") {
                 if (txt_left_date.value == "") {
                     alert("Please select Left Date");
                     $("#home").removeClass("fade in active");
                     $("#menu6").removeClass("fade in active");
                     $("#menu7").removeClass("fade in active");
                     $("#menu2").removeClass("fade in active");
                     $("#menu3").removeClass("fade in active");
                     $("#menu4").removeClass("fade in active");
                     $("#menu5").removeClass("fade in active");
                     $("#menu8").removeClass("fade in active");
                     $("#menu9").removeClass("fade in active");
                     $("#tabactive1").removeClass("active");
                     $("#tabactive3").removeClass("active");
                     $("#tabactive4").removeClass("active");
                     $("#tabactive5").removeClass("active");
                     $("#tabactive6").removeClass("active");
                     $("#tabactive7").removeClass("active");
                     $("#tabactive8").removeClass("active");
                     $("#tabactive9").removeClass("active");
                     $("#tabactive10").removeClass("active");
                     $("#tabactive2").addClass("active");
                     $("#menu1").addClass("fade in active");
                     txt_left_date.focus();
                     return false;
                 }
             }

             var txt_bankholder = document.getElementById('<%=txt_bankholder.ClientID %>');
            if (txt_bankholder.value == "") {
                alert("Please Enter Bank Account Holder Name");
                txt_bankholder.focus();
                return false;
            }
            var txt_originalbankaccountno = document.getElementById('<%=txt_originalbankaccountno.ClientID %>');
            if (txt_originalbankaccountno.value == "") {
                alert("Please Enter Original Bank Account Number");
                txt_originalbankaccountno.focus();
                return false;
            }
            var txt_pfifsccode = document.getElementById('<%=txt_pfifsccode.ClientID %>');
            if (txt_pfifsccode.value == "") {
                alert("Please Enter Bank IFSC Code");
                txt_pfifsccode.focus();
                return false;
            }
            if (txt_pfifsccode.value.length != 11) {
                alert("Please Enter Valid IFSC Code");
                txt_pfifsccode.focus();
                return false;
            }
            // Bank Transfer

            if (Selected_bnk_tranfer == "Select Transfer") {
                alert("Please Select Transfer");
                $("#home").removeClass("fade in active");
                $("#menu1").removeClass("fade in active");
                $("#menu7").removeClass("fade in active");
                $("#menu2").removeClass("fade in active");
                $("#menu3").removeClass("fade in active");
                $("#menu4").removeClass("fade in active");
                $("#menu5").removeClass("fade in active");
                $("#menu8").removeClass("fade in active");
                $("#menu9").removeClass("fade in active");
                $("#tabactive1").removeClass("active");
                $("#tabactive2").removeClass("active");
                $("#tabactive4").removeClass("active");
                $("#tabactive5").removeClass("active");
                $("#tabactive6").removeClass("active");
                $("#tabactive7").removeClass("active");
                $("#tabactive8").removeClass("active");
                $("#tabactive9").removeClass("active");
                $("#tabactive3").addClass("active");
                $("#menu6").addClass("fade in active");
                txt_bnk_transfer.focus();
                return false;
            }
            ///////*********************************************************************

            // Nationality

            if (e_Nationality.value == "") {
                alert("Please Enter Nationality");
                $("#home").removeClass("fade in active");
                $("#menu1").removeClass("fade in active");
                $("#menu6").removeClass("fade in active");
                $("#menu2").removeClass("fade in active");
                $("#menu3").removeClass("fade in active");
                $("#menu4").removeClass("fade in active");
                $("#menu5").removeClass("fade in active");
                $("#menu8").removeClass("fade in active");
                $("#tabactive1").removeClass("active");
                $("#tabactive2").removeClass("active");
                $("#tabactive3").removeClass("active");
                $("#tabactive5").removeClass("active");
                $("#tabactive6").removeClass("active");
                $("#tabactive7").removeClass("active");
                $("#tabactive8").removeClass("active");
                $("#tabactive9").removeClass("active");
                $("#tabactive4").addClass("active");
                $("#menu7").addClass("fade in active");
                e_Nationality.focus();
                return false;
            }
            //var itc_upload_form = document.getElementById('<%=itc_upload_form.ClientID %>');
            //if (itc_upload_form.value == "") {
            //    alert("Please Select ITC Document in Document Tab");
            //    itc_upload_form.focus();
            //    return false;
            //}

            if (R_validation() == false)
            { return false }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;



        }
        function update_validation1() {

            var txt_emptype = document.getElementById('<%=ddl_employee_type.ClientID %>');
            var Selected_type = txt_emptype.options[txt_emptype.selectedIndex].text;

            if (Selected_type == "Select") {
                alert("Please Select Employee Type !!!");
                txt_emptype.focus();
                return false;
            }

            //Department type
            var txt_department = document.getElementById('<%=ddl_department.ClientID %>');
            var Selected_department = txt_emptype.options[txt_department.selectedIndex].text;

            if (Selected_department == "Select") {
                alert("Please Select Department type !!!");
                txt_department.focus();
                return false;
            }

            // client Name
            var txt_client = document.getElementById('<%=ddl_unit_client.ClientID %>');
              var Selected_client = txt_client.options[txt_client.selectedIndex].text;

              if (Selected_client == "Select") {
                  alert("Please Select Client Name !!!");
                  txt_client.focus();
                  return false;
              }

            // Unit Name
              var txt_clientwisestate = document.getElementById('<%=ddl_clientwisestate.ClientID %>');
              var Selected_clientwiswstate = txt_clientwisestate.options[txt_clientwisestate.selectedIndex].text;

              if (Selected_clientwiswstate == "Select") {
                  alert("Please Select State !!!");
                  txt_clientwisestate.focus();
                  return false;
              }

            // Unit Name
              var txt_unit = document.getElementById('<%=ddl_unitcode.ClientID %>');
              var Selected_unit = txt_unit.options[txt_unit.selectedIndex].text;

              if (Selected_unit == "Select") {
                  alert("Please Select Branch Name !!!");
                  txt_unit.focus();
                  return false;
              }


            // Grade
              var txt_grade = document.getElementById('<%=ddl_grade.ClientID %>');
              var Selected_grade = txt_grade.options[txt_grade.selectedIndex].text;

              if (Selected_grade == "Select") {
                  alert("Please Select Designation !!!");
                  txt_grade.focus();
                  return false;
              }


            // project name
              var e_name = document.getElementById('<%=txt_eename.ClientID %>');

              if (e_name.value == "") {
                  alert("Please Enter Employee Name");
                  e_name.focus();
                  return false;
              }
            // Father/ Husband Name
              var e_fatharname = document.getElementById('<%=txt_eefatharname.ClientID %>');

              if (e_fatharname.value == "") {
                  alert("Please Enter Father/ Husband Name");
                  e_fatharname.focus();
                  return false;
              }


            // Date of Birth
              var e_birthdate = document.getElementById('<%=txt_birthdate.ClientID %>');

              if (e_birthdate.value == "") {
                  alert("Please Select Date of Birth");
                  e_birthdate.focus();
                  return false;
              }
              var ddl_gender = document.getElementById('<%=ddl_gender.ClientID %>');
              var Selected_ddl_gender = ddl_gender.options[ddl_gender.selectedIndex].text;

              if (Selected_ddl_gender == "Select") {
                  alert("Please Select Gender");
                  ddl_gender.focus();
                  return false;
              }
            // role:
              var l_role = document.getElementById('<%=DropDownList1.ClientID %>');
              var SelectedText11 = l_role.options[l_role.selectedIndex].text;

              if (SelectedText11 == "--Select Role--") {
                  alert("Please Select Role !!!");
                  l_role.focus();
                  return false;
              }

              var t_workinghrs = document.getElementById('<%= txt_attendanceid.ClientID %>');
              if (t_workinghrs.value == "") {
                  alert("Please Enter Working Hours.");
                  t_workinghrs.focus();
                  return false;
              }

              if ((Selected_type == "Permanent") || (Selected_type == "Permanent Reliever") || (Selected_type == "Staff") || (Selected_type == "Temporary") || (Selected_type == "Repair & Maintenance")) {
                  var e_presentcity = document.getElementById('<%=txt_presentaddress.ClientID %>');
                  if (e_presentcity.value == "") {
                      alert("Please Enter Present Address");
                      $("#menu1").removeClass("fade in active");
                      $("#menu6").removeClass("fade in active");
                      $("#menu7").removeClass("fade in active");
                      $("#menu2").removeClass("fade in active");
                      $("#menu3").removeClass("fade in active");
                      $("#menu4").removeClass("fade in active");
                      $("#menu5").removeClass("fade in active");
                      $("#menu8").removeClass("fade in active");
                      $("#menu9").removeClass("fade in active");
                      $("#tabactive2").removeClass("active");
                      $("#tabactive3").removeClass("active");
                      $("#tabactive4").removeClass("active");
                      $("#tabactive5").removeClass("active");
                      $("#tabactive6").removeClass("active");
                      $("#tabactive7").removeClass("active");
                      $("#tabactive8").removeClass("active");
                      $("#tabactive9").removeClass("active");
                      $("#tabactive10").removeClass("active");
                      $("#tabactive1").addClass("active");
                      $("#home").addClass("fade in active");
                      e_presentcity.focus();
                      return false;
                  }

                  var t_state = document.getElementById('<%=ddl_state.ClientID %>');
                  var SelectedText2 = t_state.options[t_state.selectedIndex].text;

                  if (SelectedText2 == "Select") {
                      alert("Please Select State  !!!");
                      t_state.focus();
                      return false;
                  }
                  // City
                  var t_City = document.getElementById('<%=txt_presentcity.ClientID %>');

                  if (t_City.value == "Select") {
                      alert("Please Enter Present  City");
                      t_City.focus();
                      return false;
                  }


                  //Mobile Number
                  var e_mobilenumber = document.getElementById('<%=txt_mobilenumber.ClientID %>');

                  if (e_mobilenumber.value == "") {
                      alert("Please Enter Mobile Number");
                      $("#menu1").removeClass("fade in active");
                      $("#menu6").removeClass("fade in active");
                      $("#menu7").removeClass("fade in active");
                      $("#menu2").removeClass("fade in active");
                      $("#menu3").removeClass("fade in active");
                      $("#menu4").removeClass("fade in active");
                      $("#menu5").removeClass("fade in active");
                      $("#menu8").removeClass("fade in active");
                      $("#menu9").removeClass("fade in active");
                      $("#tabactive2").removeClass("active");
                      $("#tabactive3").removeClass("active");
                      $("#tabactive4").removeClass("active");
                      $("#tabactive5").removeClass("active");
                      $("#tabactive6").removeClass("active");
                      $("#tabactive7").removeClass("active");
                      $("#tabactive8").removeClass("active");
                      $("#tabactive9").removeClass("active");
                      $("#tabactive10").removeClass("active");
                      $("#tabactive1").addClass("active");
                      $("#home").addClass("fade in active");
                      e_mobilenumber.focus();
                      return false;
                  }
              }
            // Joining Date
              var e_joiningdate = document.getElementById('<%=txt_joiningdate.ClientID %>');

            if (e_joiningdate.value == "") {
                alert("Please Select Joining Date");
                $("#home").removeClass("fade in active");
                $("#menu6").removeClass("fade in active");
                $("#menu7").removeClass("fade in active");
                $("#menu2").removeClass("fade in active");
                $("#menu3").removeClass("fade in active");
                $("#menu4").removeClass("fade in active");
                $("#menu5").removeClass("fade in active");
                $("#menu8").removeClass("fade in active");
                $("#menu9").removeClass("fade in active");
                $("#tabactive1").removeClass("active");
                $("#tabactive3").removeClass("active");
                $("#tabactive4").removeClass("active");
                $("#tabactive5").removeClass("active");
                $("#tabactive6").removeClass("active");
                $("#tabactive7").removeClass("active");
                $("#tabactive8").removeClass("active");
                $("#tabactive9").removeClass("active");
                $("#tabactive10").removeClass("active");
                $("#tabactive2").addClass("active");
                $("#menu1").addClass("fade in active");

                e_joiningdate.focus();
                return false;
            }

            // location work state
            var work_state = document.getElementById('<%=ddl_location.ClientID %>');

              if (work_state.value == "") {
                  alert("Please Enter Work State");
                  $("#home").removeClass("fade in active");
                  $("#menu6").removeClass("fade in active");
                  $("#menu7").removeClass("fade in active");
                  $("#menu2").removeClass("fade in active");
                  $("#menu3").removeClass("fade in active");
                  $("#menu4").removeClass("fade in active");
                  $("#menu5").removeClass("fade in active");
                  $("#menu8").removeClass("fade in active");
                  $("#menu9").removeClass("fade in active");
                  $("#tabactive1").removeClass("active");
                  $("#tabactive3").removeClass("active");
                  $("#tabactive4").removeClass("active");
                  $("#tabactive5").removeClass("active");
                  $("#tabactive6").removeClass("active");
                  $("#tabactive7").removeClass("active");
                  $("#tabactive8").removeClass("active");
                  $("#tabactive9").removeClass("active");
                  $("#tabactive10").removeClass("active");
                  $("#tabactive2").addClass("active");
                  $("#menu1").addClass("fade in active");
                  work_state.focus();
                  return false;
              }

            // location work state
              var work_city = document.getElementById('<%=ddl_location_city.ClientID %>');

              if (work_city.value == "") {
                  alert("Please Enter Work City");
                  $("#home").removeClass("fade in active");
                  $("#menu6").removeClass("fade in active");
                  $("#menu7").removeClass("fade in active");
                  $("#menu2").removeClass("fade in active");
                  $("#menu3").removeClass("fade in active");
                  $("#menu4").removeClass("fade in active");
                  $("#menu5").removeClass("fade in active");
                  $("#menu8").removeClass("fade in active");
                  $("#menu9").removeClass("fade in active");
                  $("#tabactive1").removeClass("active");
                  $("#tabactive3").removeClass("active");
                  $("#tabactive4").removeClass("active");
                  $("#tabactive5").removeClass("active");
                  $("#tabactive6").removeClass("active");
                  $("#tabactive7").removeClass("active");
                  $("#tabactive8").removeClass("active");
                  $("#tabactive9").removeClass("active");
                  $("#tabactive10").removeClass("active");
                  $("#tabactive2").addClass("active");
                  $("#menu1").addClass("fade in active");
                  work_city.focus();
                  return false;
              }
              var txt_bankholder = document.getElementById('<%=txt_bankholder.ClientID %>');
              if (txt_bankholder.value == "") {
                  alert("Please Enter Bank Account Holder Name");
                  txt_bankholder.focus();
                  return false;
              }
              var txt_originalbankaccountno = document.getElementById('<%=txt_originalbankaccountno.ClientID %>');
              if (txt_originalbankaccountno.value == "") {
                  alert("Please Enter Original Bank Account Number");
                  txt_originalbankaccountno.focus();
                  return false;
              }
              var txt_pfifsccode = document.getElementById('<%=txt_pfifsccode.ClientID %>');
            if (txt_pfifsccode.value == "") {
                alert("Please Enter Bank IFSC Code");
                txt_pfifsccode.focus();
                return false;
            }
            if (txt_pfifsccode.value.length != 11) {
                alert("Please Enter Valid IFSC Code");
                txt_pfifsccode.focus();
                return false;
            }
            var ddl_infitcode = document.getElementById('<%=ddl_infitcode.ClientID %>');
            var Selected_ddl_infitcode = ddl_infitcode.options[ddl_infitcode.selectedIndex].text;

            if (Selected_ddl_infitcode == "Select Transfer") {
                alert("Please Select NFD/IFD");
                ddl_infitcode.focus();
                return false;
            }
            var txt_Nationality = document.getElementById('<%=txt_Nationality.ClientID %>');
            if (txt_Nationality.value == "") {
                alert("Please Enter Nationality");
                txt_Nationality.focus();
                return false;
            }


            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        $(document).ready(function () {
            $(".js-example-basic-single").select2();
        });

        function openWindow() {
            window.open("html/EmployeeMaster.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

    </script>
    <script>
        function pincode_validation() {
            var txt_pin = document.getElementById('<%=txt_presentpincode.ClientID %>');

            if (txt_pin.value.length != 6) {
                alert("Please Enter Pin Number Minimum 6 Digit");
                aField = document.getElementById('<%=txt_presentpincode.ClientID %>');
                setTimeout("aField.focus()", 50);
                return false;
            }
        }
        function pincode_validation1() {
            var txt_pin = document.getElementById('<%=txt_permanantpincode.ClientID %>');

            if (txt_pin.value.length != 6) {
                alert("Please Enter Pin Number Minimum 6 Digit");
                aField = document.getElementById('<%=txt_permanantpincode.ClientID %>');
                setTimeout("aField.focus()", 50);
                return false;
            }
        }

        function validation_dublicate_id() {


            var select_type = document.getElementById('<%=ddl_id_set_dublicate.ClientID %>');
            var Selected_select_type = select_type.options[select_type.selectedIndex].text;


            if (Selected_select_type == "Select") {
                alert("Please Select Dublicate Id Card Set!!!");
                select_type.focus();
                return false;
            }


            var txt_from_date = document.getElementById('<%=txt_from_date.ClientID %>');

            if (txt_from_date.value == "") {
                alert("Please Select Issue Date");
                txt_from_date.focus();
                return false;
            }



        }


        function req_valid() {
            var select_designation = document.getElementById('<%=select_designation.ClientID %>');
            var Selected_select_designation = select_designation.options[select_designation.selectedIndex].text;

            var select_type = document.getElementById('<%=ddl_product_type.ClientID %>');
              var Selected_select_type = select_type.options[select_type.selectedIndex].text;

              var number_of_set = document.getElementById('<%=ddl_uniformset.ClientID %>');
              var Selected_number_of_set = number_of_set.options[number_of_set.selectedIndex].text;

              var issue_date = document.getElementById('<%=uniform_issue_date.ClientID %>');

            var expiry_date = document.getElementById('<%=uniform_expiry_date.ClientID %>');

            if (Selected_select_designation == "Select") {
                alert("Please Select Designation !!!");
                select_designation.focus();
                return false;
            }

            if (Selected_select_type == "Select") {
                alert("Please Select product Type !!!");
                select_type.focus();
                return false;
            }

            if (Selected_number_of_set == "Select") {
                alert("Please Select  Number of Sets !!!");
                number_of_set.focus();
                return false;
            }
            var select_uniform_size = document.getElementById('<%=uniform_size.ClientID %>');
              var Selected_uniform_size = select_uniform_size.options[select_uniform_size.selectedIndex].text;

              if (!(Selected_select_type == "ID_Card" || Selected_select_type == "Torch" || Selected_select_type == "Baton" || Selected_select_type == "Belt")) {
                  if (Selected_uniform_size == "Select") {
                      alert("Please Select  Product Size !!!");
                      select_uniform_size.focus();
                      return false;
                  }
              }
              if (issue_date.value == "") {
                  alert("Please Select Issue Date");
                  issue_date.focus();
                  return false;
              }

              if (expiry_date.value == "") {
                  alert("Please Select Expiry Date");
                  expiry_date.focus();
                  return false;
              }
              $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
              return true;
          }

          function father_relation() {
              var ddl_relation = document.getElementById('<%=ddl_relation.ClientID %>');
              var Selected_ddl_relation = ddl_relation.options[ddl_relation.selectedIndex].text;

              var txt_eefatharname = document.getElementById('<%=txt_eefatharname.ClientID %>');
              var txt_name1 = document.getElementById('<%=txt_name1.ClientID %>');
              var txt_name4 = document.getElementById('<%=txt_name4.ClientID %>');

              if (Selected_ddl_relation == "Father") {
                  txt_name1.value = txt_eefatharname.value;
                  txt_name4.value = "";

                  return true;
              }
              else if (Selected_ddl_relation == "Husband") {
                  txt_name4.value = txt_eefatharname.value;
                  txt_name1.value = "";
                  return true;
              }


          }

          function RT_validation() {

              var ddl_emp_type = document.getElementById('<%=ddl_emp_type.ClientID %>');
            var Selected_ddl_emp_type = ddl_emp_type.options[ddl_emp_type.selectedIndex].text;

            if (Selected_ddl_emp_type == "Select") {
                alert("Please Select Receiver Type");
                ddl_emp_type.focus();
                return false;
            }
        }
        function ddl_show_hide() {
            var ddl_employee_type = document.getElementById('<%=ddl_employee_type.ClientID %>');
            var Selected_ddl_employee_type = ddl_employee_type.options[ddl_employee_type.selectedIndex].text;

            var ddl_department = document.getElementById('<%=ddl_department.ClientID %>');
    var Selected_ddl_department = ddl_department.options[ddl_department.selectedIndex].text;
    if (Selected_ddl_employee_type == "Select") {
        ddl_department.value = "Select";
    }
    if (Selected_ddl_employee_type == "Permanent") {
        ddl_department.value = "HR Department";
        ddl_department.disabled = true;
    }
    if (Selected_ddl_employee_type == "Permanent Reliever") {
        ddl_department.value = "HR Department";
        ddl_department.disabled = true;
    }
    if (Selected_ddl_employee_type == "Reliever") {
        ddl_department.value = "HR Department";
        ddl_department.disabled = true;
    }
    if (Selected_ddl_employee_type == "Temporary") {
        ddl_department.value = "HR Department";
        ddl_department.disabled = true;
    }
}


function adhar_val() {
    var txt_search_adhar = document.getElementById('<%=txt_search_adhar.ClientID %>');
            if (txt_search_adhar.value.length != 12) {
                alert("Adhar Card Number is not Valid !");
                txt_search_adhar.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function uniform_detail_validation() {
            var select_designation = document.getElementById('<%=select_designation.ClientID %>');
            var Selected_select_designation = select_designation.options[select_designation.selectedIndex].text;
            if (Selected_select_designation == "Select") {
                alert("Please Select Designation");
                select_designation.focus();
                return false;
            }

            var ddl_product_type = document.getElementById('<%=ddl_product_type.ClientID %>');
            var Selected_ddl_product_type = ddl_product_type.options[ddl_product_type.selectedIndex].text;
            if (Selected_ddl_product_type == "Select") {
                alert("Please Select Product Type");
                ddl_product_type.focus();
                return false;
            }
            var ddl_uniformset = document.getElementById('<%=ddl_uniformset.ClientID %>');
            var Selected_ddl_uniformset = ddl_uniformset.options[ddl_uniformset.selectedIndex].text;
            if (Selected_ddl_uniformset == "Select") {
                alert("Please Select Number of Set");
                ddl_uniformset.focus();
                return false;
            }
            var uniform_size = document.getElementById('<%=uniform_size.ClientID %>');
            var Selected_uniform_size = uniform_size.options[uniform_size.selectedIndex].text;
            if (Selected_uniform_size == "Select") {
                alert("Please Select Size");
                uniform_size.focus();
                return false;
            }
            var uniform_issue_date = document.getElementById('<%=uniform_issue_date.ClientID %>');
            if (uniform_issue_date.value == "") {
                alert("Please Select Issue Date");
                uniform_issue_date.focus();
                return false;
            }
            var uniform_expiry_date = document.getElementById('<%=uniform_expiry_date.ClientID %>');
            if (uniform_expiry_date.value == "") {
                alert("Please Select Expiry Date");
                uniform_expiry_date.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function Rqvd_validation() {

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

            if (confirm("Are you Sure You Want to Approve Record")) {

                if (update_validation1()) {
                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                    return true;
                }
            }
            else {
                alert("Record not Available");
                return false;
            }
            return false;
        }


        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });

        $(document).ready(function () {
            $('#<%=ddl_gender.ClientID%>').change(function () {
                $('#<%=ddl_product_type.ClientID%>').children("option").show();
                $("#<%=ddl_product_type.ClientID%> option[value='" + $('#<%=ddl_gender.ClientID%>').val() + "']").hide();
            });
        });
        function validateEmail(emailField) {
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

            if (reg.test(emailField.value) == false) {
                alert('Invalid Email Address');
                return false;
            }
            return true;

        }
        function valid_ifcs() {
            var txt_pfifsccode = document.getElementById('<%=txt_pfifsccode.ClientID %>');
            if (txt_pfifsccode.value.length != 11) {
                alert("Please Enter Valid IFSC Code");
                txt_pfifsccode.focus();
                return false;
            }
        }
        function Left_button() {

            var txt_leftdate = document.getElementById('<%=txt_leftdate.ClientID %>');
            if (txt_leftdate.value.length == "") {
                alert("please Select Left Date");
                txt_leftdate.focus();
                return false;
            }


            var txtreasonforleft = document.getElementById('<%=txtreasonforleft.ClientID %>');
            if (txtreasonforleft.value.length == "") {
                alert("please Enter Reason For Resign");
                txtreasonforleft.focus();
                return false;
            }



        }
    </script>

    <div class="container-fluid">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <asp:Panel ID="panel_all" runat="server" CssClass="panel panel-primary" Style="background-color: white;">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; text-align: center; font-size: small;"><b>EMPLOYEE INFORMATION</b></div>
                    </div>
                    <div class="col-sm-2 text-right" style="text-align:right">
                        <asp:LinkButton ID="link_rejoin" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image13" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
           <%-- <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align:left; color: #fff; font-size: small;"><b>Employee Master Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                </div>
            </div>
            </div>--%>
            <br />
             <div class="container-fluid">

            <div class="panel-body">
                <div id="employee_count" class="modal fade" role="dialog">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Employee Count</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-12" style="padding-left: 1%;">
                                        <asp:Panel runat="server" CssClass="grid-view">
                                            <asp:GridView ID="gv_employee_list" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gv_employee_list_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                                <Columns>
                                                    <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                                    <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                                    <asp:BoundField DataField="permanent" HeaderText="Permanent Employee count" SortExpression="permanent" />



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
                <div id="left_emp_count" class="modal fade" role="dialog">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Expired ID Uniform Shoes Of Employee</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-12" style="padding-left: 1%;">
                                        <asp:Panel ID="Panel6" runat="server" CssClass="grid-view">
                                            <asp:GridView ID="gv_left_emp_count" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                                <Columns>
                                                    <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                    <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                                    <asp:BoundField DataField="emp_code" HeaderText="Employee Code" SortExpression="emp_code" />
                                                    <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                                                    <asp:BoundField DataField="ID_card" HeaderText="ID Card End Date" SortExpression="ID_card" />
                                                    <asp:BoundField DataField="Shoes" HeaderText="Shoes End Date" SortExpression="Shoes" />
                                                    <asp:BoundField DataField="uniform" HeaderText="Uniform End Date" SortExpression="uniform" />
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
                <div id="unifom_hold_count" class="modal fade" role="dialog">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Uniform Hold  Count</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-12" style="padding-left: 1%;">
                                        <asp:Panel ID="Panel7" runat="server" CssClass="grid-view">
                                            <asp:GridView ID="gv_unifom_hold_count" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                                <Columns>

                                                    <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                                    <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                                    <asp:BoundField DataField="emp_code" HeaderText="Employee Code" SortExpression="emp_code" />
                                                    <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                                                    <asp:BoundField DataField="ID_card" HeaderText="ID Card " SortExpression="ID_card" />
                                                    <asp:BoundField DataField="Shoes" HeaderText="Shoes End " SortExpression="Shoes" />
                                                    <asp:BoundField DataField="uniform" HeaderText="Uniform " SortExpression="uniform" />

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

                <asp:Panel ID="reason_panel" runat="server" Visible="false">
                    <div class="panel panel-primary">
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

                <div class="row text-right">

                    <asp:Panel ID="panel_adhar_card" runat="server">


                        <div class="row">
                            <div class="col-sm-3 col-xs-12"></div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Select State :</b><span class="text-red"> *</span>
                                <asp:DropDownList ID="ddl_adhar_search" runat="server" class="form-control" OnSelectedIndexChanged="ddl_adhar_search_SelectedIndexChanged" AutoPostBack="true">

                                    <asp:ListItem Value="other_state">Other State</asp:ListItem>
                                    <asp:ListItem Value="northeast">North East</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12" style="text-align: left">
                                <asp:Label runat="server" ID="lable_search" Font-Bold="true" Text="Search Adhar Card Number :"></asp:Label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txt_search_adhar" MaxLength="12" onkeypress="return isNumber(event)"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 col-xs-12" style="margin-top: 1.5em;">

                                <asp:Button ID="btn_search" runat="server" class="btn btn-primary" Text="Search" OnClick="searchadgarcard_Click" OnClientClick="return adhar_val();" />

                            </div>
                            <div class="col-sm-1 col-xs-12" style="margin-top: 1.5em;">
                                <asp:Button ID="btn_adhar_add_emp" runat="server" OnClick="add_new_emp" class="btn btn-large" Text="Add Employee" />
                            </div>
                        </div>
                        <br />
                        <div class="container-fluid">
                            <asp:Panel ID="Panel3" runat="server" BackColor="White" Visible="false" Width="100%">
                                <asp:GridView ID="gv_search_adharcardno" class="table" AutoGenerateColumns="False" runat="server"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                    OnRowDataBound="gv_search_adharcardno_RowDataBound"
                                    CellPadding="2" meta:resourcekey="SearchGridViewResource1"
                                    OnPreRender="gv_search_adharcardno_PreRender" Width="100%" DataKeyNames="EMP_CODE">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000066" BackColor="#ffffff" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                        <asp:BoundField DataField="left_date" HeaderText="LEFT DATE" SortExpression="left_date" />
                                        <asp:BoundField DataField="EMP_CODE" HeaderText="EMP CODE" SortExpression="EMP_CODE" />
                                        <asp:BoundField DataField="CLIENT_NAME" HeaderText="CLIENT NAME" SortExpression="CLIENT_NAME" />
                                        <asp:BoundField DataField="location" HeaderText="STATE NAME" SortExpression="location" />
                                        <asp:BoundField DataField="UNIT_NAME" HeaderText="UNIT NAME" SortExpression="UNIT_NAME" />
                                        <asp:BoundField DataField="EMP_NAME" HeaderText="NAME" SortExpression="EMP_NAME" />
                                        <asp:BoundField DataField="employee_type" HeaderText="TYPE" SortExpression="employee_type" />
                                        <asp:BoundField DataField="JOINING_DATE" HeaderText="JOINING DATE." SortExpression="JOINING_DATE" />
                                        <asp:BoundField DataField="original_bank_account_no" HeaderText="Bank Account No" SortExpression="original_bank_account_no" />
                                        <asp:BoundField DataField="PF_BANK_NAME" HeaderText="Bank Name" SortExpression="PF_BANK_NAME" />
                                        <asp:TemplateField HeaderText="Rejoin Date" HeaderStyle-Width="124.7px">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txt_rejoin_date" CssClass="form-control date-picker11"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txt_rejoin_date" ValidationGroup="gv_search_adharcardno" ErrorMessage="Please Select Rejoin Date" Style="color: red"></asp:RequiredFieldValidator>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Link" runat="server" Text="Rejoin" CssClass="btn btn-primary" OnClick="Link_Click" ValidationGroup="gv_search_adharcardno" CausesValidation="true"></asp:LinkButton>
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
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="pnl_buttons" runat="server">
                        <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px;
                         border: 1px solid white; height: 180px; text-align: center; padding-top:30px ;
                          margin-left:-2px; margin-right:-2px" >
                           
                            <div class="row"> 
                            <div class="col-md-2 col-xs-12" >
                                <asp:Panel ID="Panel_dispatch1" runat="server" style="margin-top: -16px;">
                                <b>Receiver Type :</b> <span style="color:red">*</span> 
                                    <br /> 
                                  <asp:DropDownList ID="ddl_emp_type" runat="server" CssClass="form-control text_box"  Width="100%" onchange="dispatch_bill_validation1();" AutoPostBack ="true">
                                      <asp:ListItem Value="0">Select</asp:ListItem>
                                      <asp:ListItem Value="1">Continue</asp:ListItem>
                                      <asp:ListItem Value="2">Left</asp:ListItem>
                                      </asp:DropDownList>
                                     </asp:Panel>
                            </div>
                                </div>
                            </br>
                            <div class="row">
                               <div class="col-md-12 col-xs-12" > 

                             <asp:Button ID="emp_upload" runat="server" OnClick="emp_upload_Click" class="btn btn-large" Text="Employee Upload"/>
                             <asp:Button ID="btn_dispatch_details" runat="server" OnClick="btn_dispatch_details_Click" class="btn btn-large" Text="Dispatch Details" OnClientClick="return RT_validation();"/>
                            <asp:Button ID="btn_add_employee" runat="server" OnClick="btn_add_employee_Click" class="btn btn-large" Text="Add Employee" />
                            <asp:Button ID="btnexcelexport" runat="server" class="btn btn-large"
                                OnClick="btn_emp_Export_Click" Text="Export To Excel"
                                CausesValidation="False" meta:resourcekey="btnexcelexportResource1" OnClientClick="return RT_validation();" />
                            <asp:Button ID="Button1" runat="server" OnClick="btn_add_employeeIcard_Click" class="btn btn-large" Text="Give ICard" Style="display:none;" />
                            <asp:Button ID="btnhelp" runat="server" CausesValidation="False" type="button" class="btn btn-large" meta:resourcekey="btnhelpResource1" OnClick="btnhelp_Click" Text="Search" onkeypress="return isNumber(event)" Visible="false" />
                            <asp:Button ID="btn_history" runat="server" CausesValidation="False" type="button" class="btn btn-large" meta:resourcekey="btnhelpResource1" OnClick="btn_history_click" Text="Left Employee" onkeypress="return isNumber(event)" />
                            <asp:Button ID="btn_Leftemp_Export" runat="server" class="btn btn-large"
                                OnClick="btn_Leftemp_Export_Click" Text="Left Emoloyee Export To Excel"
                                CausesValidation="False" meta:resourcekey="btnexcelexportResource1" Visible="false" OnClientClick="return RT_validation();" />

                            <asp:Button ID="btn_rem_documents" runat="server" CausesValidation="False" type="button" class="btn btn-large" meta:resourcekey="btnhelpResource1" OnClick="btn_rem_documents_Click" Text="Remaining Documents" Width="20%" onkeypress="return isNumber(event)" />
                           
                                
                                  <%-- <div class="row">--%>
                                    <asp:Button ID="btn_emp_approve" runat="server" CausesValidation="False" type="button" class="btn btn-large" meta:resourcekey="btnhelpResource1" OnClick="btn_emp_approve_Click" Text="Approval/Rejection" onkeypress="return isNumber(event)" />
                           
                                      <%-- </div>--%>
                                   </div>
                                </div>
                                 <br />
                            <div class="row">
                                <div class="col-md-12 col-xs-12" > 

                                     <asp:Button ID="btncloseup" runat="server" class="btn btn-danger" OnClick="btnclose_Click" Text="Close" CausesValidation="False" meta:resourcekey="btncloseResource1" />

                                    </div>
                            </div>
                            <br />
                           
                             </div>
                          </asp:Panel> 
                        </div>
                       
                   
                    <div class="row">
                        <asp:TextBox ID="txtautoattendancecode" onkeypress="return AllowAlphabet_Number(event,this);" runat="server" class="form-control text_box" meta:resourcekey="txtautoattendancecodeResource1" Visible="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-2 col-xs-12">
                    <%-- Enter Employee Code/Name :--%>
                </div>
                <div class="col-sm-4 col-xs-12">
                    <asp:TextBox ID="txtsearchempid" Visible="false" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>
                </div>


                <%--  suraj --%>

                  &nbsp;<asp:Panel ID="newpanel" runat="server">
                      <asp:Panel ID="employee_data" runat="server" CssClass="panel panel-primary" Style="background: beige; border-color: #e6dede">
                          <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white">
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12 text-left">
                                     <b> Employee Code :</b>
                            <asp:TextBox ID="txt_eecode" runat="server" class="form-control text_box"
                                onkeypress="return Alphabet_Number(event,this);" MaxLength="20" meta:resourcekey="txt_eecodeResource1" ReadOnly="true"></asp:TextBox>

                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_eecode" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1" Visible="False"></asp:RequiredFieldValidator>--%>
                                  </div>

                                  <div class="col-sm-2 col-xs-12 text-left">
                                    <b>  Employee Type :</b><span class="text-red"> *</span>
                                      <asp:DropDownList ID="ddl_employee_type" runat="server" class="form-control" onchange="ddl_show_hide()">
                                          <asp:ListItem Value="">Select</asp:ListItem>
                                          <asp:ListItem Value="Permanent">Permanent</asp:ListItem>
                                          <asp:ListItem Value="PermanentReliever">Permanent Reliever</asp:ListItem>
                                          <asp:ListItem Value="Reliever">Reliever</asp:ListItem>
                                          <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                          <asp:ListItem Value="Temporary">Temporary</asp:ListItem>
                                          <asp:ListItem Value="RM">Repair & Maintenance</asp:ListItem>
                                      </asp:DropDownList>
                                  </div>

                                  <div class="col-sm-2 col-xs-12 text-left">
                                     <b> Department Type :</b><span class="text-red"> *</span>
                                      <asp:DropDownList ID="ddl_department" runat="server" class="form-control">
                                          <asp:ListItem Value="Select">Select</asp:ListItem>
                                          <asp:ListItem Value="HR Department">HR Department</asp:ListItem>
                                          <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                          <asp:ListItem Value="Finance">Finance</asp:ListItem>
                                          <asp:ListItem Value="Operation">Operation</asp:ListItem>
                                          <asp:ListItem Value="Sales">Sales</asp:ListItem>
                                      </asp:DropDownList>
                                  </div>

                                  <div class="col-sm-2 col-xs-12 text-left">
                                     <b> Client Name :</b><span class="text-red"> *</span>
                                      <asp:DropDownList ID="ddl_unit_client" class="form-control" Width="100%" runat="server" OnSelectedIndexChanged="ddl_clientname_SelectedIndexChanged" AutoPostBack="true">
                                      </asp:DropDownList>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                     <b> State :</b><span class="text-red"> *</span>
                                      <asp:DropDownList ID="ddl_clientwisestate" runat="server" class="form-control" Width="100%" OnSelectedIndexChanged="get_city_list1" AutoPostBack="true">
                                          <asp:ListItem Value="0">Select</asp:ListItem>
                                      </asp:DropDownList>

                                  </div>
                                  <div class="col-sm-2 col-xs-12 text-left">
                                     <b> Branch Name :</b> <span class="text-red">*</span>
                                      <asp:DropDownList ID="ddl_unitcode" class="form-control" OnSelectedIndexChanged="designation_unitwise" AutoPostBack="true" Width="100%" runat="server">
                                      </asp:DropDownList>
                                  </div>



                              </div>
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Designation:</b> <span class="text-red">*</span>
                                      <asp:DropDownList ID="ddl_grade" class="form-control" Width="100%" runat="server" AutoPostBack="True" DataTextField="GRADE_DESC" DataValueField="GRADE_CODE" meta:resourcekey="ddl_gradeResource1" OnSelectedIndexChanged="ddl_grade_SelectedIndexChanged">
                                          <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                                      </asp:DropDownList>

                                  </div>
                                  <div class="col-sm-2 col-xs-12 text-left">
                                    <b>  Employee ID As Per DOJ :</b>
                            <asp:TextBox ID="txt_id_as_per_dob" runat="server" class="form-control text_box"
                                onkeypress="return Alphabet_Number(event,this);" MaxLength="20" meta:resourcekey="txt_eecodeResource1" ReadOnly="true" disabled></asp:TextBox>

                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_eecode" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1" Visible="False"></asp:RequiredFieldValidator>--%>
                                  </div>
                                  <div class="col-sm-2 col-xs-12 text-left">
                                     <b> IHMS Code :</b>
                            <asp:TextBox ID="txt_ihmscode" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>

                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_eecode" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1" Visible="False"></asp:RequiredFieldValidator>--%>
                                  </div>




                                  <div class="col-sm-2 col-xs-12 text-left">
                                    <b>  Employee Name :</b>    <span class="text-red">*</span>
                                      <asp:TextBox ID="txt_eename" runat="server" CausesValidation="True" class="form-control text_box"
                                          MaxLength="50" meta:resourcekey="txt_eenameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>

                                  </div>
                                  <div class="col-sm-2 col-xs-12 text-left">
                                     <b> Father / Husband Name : </b>   <span class="text-red">*</span>

                                      <asp:TextBox ID="txt_eefatharname" runat="server" CausesValidation="True" class="form-control text_box"
                                          MaxLength="50" meta:resourceKey="txt_eefatharnameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_eefatharname" ErrorMessage="RequiredFieldValidator" meta:resourceKey="RequiredFieldValidator4Resource1" Text="*"></asp:RequiredFieldValidator>
                                  </div>
                                  <div class="col-sm-2 col-xs-12 text-left">
                                     <b> Relation :</b>
                            <asp:DropDownList ID="ddl_relation" runat="server" class="form-control" onchange="return father_relation();">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Father">Father</asp:ListItem>
                                <asp:ListItem Value="Husband">Husband</asp:ListItem>
                            </asp:DropDownList>
                                  </div>

                              </div>
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12 text-left">
                                    <b>  Date of Birth :</b>    <span class="text-red">*</span>

                                      <asp:TextBox ID="txt_birthdate" runat="server" class="form-control date-picker"
                                          meta:resourceKey="txt_birthdateResource1"></asp:TextBox>
                                  </div>

                                  <div class="col-sm-2 col-xs-12 text-left">
                                     <b> Gender :</b><span class="text-red">*</span>

                                      <asp:DropDownList ID="ddl_gender" runat="server" class="form-control">
                                          <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                          <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                          <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                      </asp:DropDownList>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Reporting To: </b>   
                                    <asp:DropDownList ID="ddl_reporting_to" runat="server" class="form-control" DataTextField="emp_name"
                                        DataValueField="emp_code" meta:resourcekey="ddl_deptResource1" OnSelectedIndexChanged="ddl_reporting_to_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>

                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Role : </b>   <span class="text-red">*</span>
                                      <asp:DropDownList ID="DropDownList1" class="form-control" runat="server"></asp:DropDownList>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Working Hours:</b> <span class="text-red">*</span>
                                      <asp:DropDownList ID="txt_attendanceid" runat="server" OnSelectedIndexChanged="txt_workinghours_count" AutoPostBack="true"
                                          class="form-control text_box" MaxLength="2" meta:resourceKey="txt_attendacneidResource1">
                                          <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                      </asp:DropDownList>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                     <b> IN Time : </b>
                                    <asp:DropDownList ID="ddl_intime" runat="server" class="form-control" Width="100%" meta:resourceKey="ddl_intimeResource1">
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


                              </div>
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Email-Id:  </b>  

                                    <asp:TextBox ID="txt_email" runat="server" class="form-control text_box" onblur="validateEmail(this);" MaxLength="70" meta:resourceKey="txt_email"></asp:TextBox>
                                  </div>


                                  <div class="col-sm-2 col-xs-12">
                                     <b> Police Verification Start Date :</b>
                                        <asp:TextBox ID="txt_start_date" runat="server" class="form-control date-picker11"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Police Verification  End Date :</b>
                                        <asp:TextBox ID="txt_end_date" runat="server" class="form-control date-picker12"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Rent Agreement Start Date :</b>
                                        <asp:TextBox ID="txt_ranteagrement_satrtdate" runat="server" class="form-control date-picker11"></asp:TextBox>
                                  </div>

                                  <div class="col-sm-2 col-xs-12">
                                     <b> Rent Agreement End Date :</b>
                                        <asp:TextBox ID="txt_ranteagrement_enddate" runat="server" class="form-control date-picker12"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-2 col-xs-12 ">

                                      <asp:DropDownList ID="ddl_clientname" runat="server" class="form-control " AutoPostBack="true" Style="display: none" OnSelectedIndexChanged="ddl_clientname_SelectedIndexChanged"
                                          DataTextField="CLIENT_NAME" DataValueField="CLIENT_NAME" meta:resourcekey="ddl_relationResource1">
                                          <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                                      </asp:DropDownList>
                                  </div>

                              </div>
                              <br />
                          </div>
                      </asp:Panel>
                      <br />
                      <br />
                      <div id="tabs" style="background: beige;">
                          <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                          <ul>
                              <li id="tabactive1" class="active"><a data-toggle="tab" href="#home" runat="server">Contact Details</a></li>
                              <li id="tabactive2"><a data-toggle="tab" href="#menu1" runat="server">Employee Details</a></li>
                              <li id="tabactive10"><a data-toggle="tab" href="#menu9" runat="server">Family Details</a></li>
                              <li id="tabactive3"><a data-toggle="tab" href="#menu6" runat="server">Bank Account Details</a></li>
                              <li id="tabactive4"><a data-toggle="tab" href="#menu7" runat="server">Personal Details</a></li>
                              <li id="tabactive5"><a data-toggle="tab" href="#menu2" runat="server">Qualification Details</a></li>
                              <%--<li id="tabactive6"><a data-toggle="tab" href="#menu3" runat="server">Leave Details</a></li>--%>
                              <li id="tabactive7"><a data-toggle="tab" href="#menu4" id="rating" runat="server">Rating Details</a></li>

                              <li id="tabactive8"><a data-toggle="tab" href="#menu5" runat="server">Documents</a></li>

                              <li id="tabactive11"><a data-toggle="tab" href="#menu11" runat="server">KRA</a></li>
                              <li id="tabactive9"><a id="A2" data-toggle="tab" style="display: none" href="#menu8" runat="server">Loan</a></li>
                              <li id="tabactive12"><a data-toggle="tab" href="#menu12">Uniform Details</a></li>
                              <li id="Li1"><a data-toggle="tab" href="#menu13"> Dublicate Id Card</a></li>

                          </ul>


                          <div id="home">
                              <br />

                              <div class="row">
                                  <div class="col-sm-12 col-xs-12 text-right" style="width: 98.5%;text-align: right;">
                                      <asp:Button ID="btn_copyadd" runat="server" CssClass="btn btn-warning btn_address" OnClick="btn_copyadd_Click" OnClientClick="return copy_add();" Text="Copy Present Address" />
                                  </div>

                              </div>
                              <br />

                              <div class="row">
                                  <div class="col-sm-6 col-xs-12">
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12">
                                             <b> Present Address :</b>    <span class="text-red">*</span>
                                          </div>
                                          <div class="col-sm-8 col-xs-12">
                                              <asp:TextBox ID="txt_presentaddress"
                                                  runat="server" MaxLength="225" class="form-control pr_add text_box"
                                                  onkeypress="return AllowAlphabet_address(event)"
                                                  meta:resourcekey="txt_presentaddressResource1"></asp:TextBox><td class="labels">
                                          </div>
                                      </div>
                                      <br />
                                      <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                                          <ContentTemplate>
                                              <div class="row">
                                                  <div class="col-sm-4 col-xs-12">
                                                     <b> State : </b>   <span class="text-red">*</span>
                                                  </div>
                                                  <div class="col-sm-8 col-xs-12">
                                                      <asp:DropDownList ID="ddl_state" runat="server" class="form-control" Width="100%" OnSelectedIndexChanged="get_city_list" AutoPostBack="true"></asp:DropDownList>

                                                  </div>
                                              </div>
                                              <br />
                                              <div class="row">
                                                  <div class="col-sm-4 col-xs-12">
                                                      <b>Present City :</b>    <span class="text-red">*</span>
                                                  </div>


                                                  <div class="col-sm-8 col-xs-12">

                                                      <asp:DropDownList ID="txt_presentcity" runat="server" class="form-control" Width="100%"></asp:DropDownList>
                                                  </div>
                                              </div>
                                          </ContentTemplate>
                                      </asp:UpdatePanel>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12">
                                             <b> Pin code :</b>
                                          </div>
                                          <div class="col-sm-8 col-xs-12">
                                              <asp:TextBox
                                                  ID="txt_presentpincode" runat="server" class="form-control pr_pin text_box" onchange="return pincode_validation()"
                                                  onkeypress="return isNumber(event)" MaxLength="6" meta:resourcekey="txt_presentpincodeResource1"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12">
                                             <b> Mobile Number1 : </b>   <span class="text-red">*</span>
                                          </div>
                                          <div class="col-sm-8 col-xs-12">
                                              <asp:TextBox ID="txt_mobilenumber" runat="server" class="form-control pr_mbno text_box"
                                                  onkeypress="return isNumber(event)" MaxLength="10"
                                                  meta:resourcekey="txt_mobilenumberResource1"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12">
                                             <b> Mobile Number2 : </b> 
                                          </div>
                                          <div class="col-sm-8 col-xs-12">
                                              <asp:TextBox ID="pre_mobileno_1" runat="server" class="form-control pr_mbno text_box"
                                                  onkeypress="return isNumber(event)" MaxLength="10"
                                                  meta:resourcekey="txt_mobilenumberResource1"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />

                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12">
                                            <b>  Mobile Number3 : </b>  
                                          </div>
                                          <div class="col-sm-8 col-xs-12">
                                              <asp:TextBox ID="pre_mobileno_2" runat="server" class="form-control pr_mbno text_box"
                                                  onkeypress="return isNumber(event)" MaxLength="10"
                                                  meta:resourcekey="txt_mobilenumberResource1"></asp:TextBox>
                                          </div>
                                      </div>
                                  </div>
                                  <div class="col-sm-6 col-xs-12">
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12">
                                            <b>  Permanant Address :</b>
                                          </div>
                                          <div class="col-sm-8 col-xs-12">
                                              <asp:TextBox ID="txt_permanantaddress" runat="server" onkeypress="return AllowAlphabet_address(event)"
                                                  MaxLength="225" class="form-control prnt_add text_box"
                                                  meta:resourcekey="txt_permanantaddressResource1"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />
                                      <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                          <ContentTemplate>

                                              <div class="row">
                                                  <div class="col-sm-4 col-xs-12">
                                                     <b> State :</b>
                                                  </div>
                                                  <div class="col-sm-8 col-xs-12">
                                                      <asp:DropDownList ID="ddl_permstate" runat="server" class="form-control" Width="100%"
                                                          OnSelectedIndexChanged="get_city_list_shipping" AutoPostBack="true">
                                                      </asp:DropDownList>
                                                  </div>
                                              </div>
                                              <br />
                                              <div class="row">
                                                  <div class="col-sm-4 col-xs-12">
                                                     <b> Permanant City :</b>
                                                  </div>
                                                  <div class="col-sm-8 col-xs-12">
                                                      <asp:DropDownList ID="txt_permanantcity" runat="server" class="form-control" Width="100%">
                                                      </asp:DropDownList>
                                                  </div>
                                              </div>
                                              <br />
                                          </ContentTemplate>
                                      </asp:UpdatePanel>
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12">
                                             <b> Pincode :</b>
                                          </div>
                                          <div class="col-sm-8 col-xs-12">
                                              <asp:TextBox ID="txt_permanantpincode" runat="server" class="form-control prnt_pin text_box"
                                                  onkeypress="return isNumber(event)" onchange="return pincode_validation1()"
                                                  MaxLength="6" meta:resourcekey="txt_permanantpincodeResource1"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12">
                                             <b> Mobile Number1  :</b>
                                          </div>
                                          <div class="col-sm-8 col-xs-12">
                                              <asp:TextBox ID="txtref2mob" runat="server" class="form-control prnt_mbno text_box"
                                                  onkeypress="return isNumber(event)"
                                                  MaxLength="10" meta:resourcekey="txtref2mobResource1"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12">
                                             <b> Mobile Number2 :</b>    
                                          </div>
                                          <div class="col-sm-8 col-xs-12">
                                              <asp:TextBox ID="txt_premanent_mob1" runat="server" class="form-control pr_mbno text_box"
                                                  onkeypress="return isNumber(event)" MaxLength="10"
                                                  meta:resourcekey="txt_mobilenumberResource1"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />

                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12">
                                            <b>  Mobile Number3 : </b>   
                                          </div>
                                          <div class="col-sm-8 col-xs-12">
                                              <asp:TextBox ID="txt_premanent_mob2" runat="server" class="form-control pr_mbno text_box"
                                                  onkeypress="return isNumber(event)" MaxLength="10"
                                                  meta:resourcekey="txt_mobilenumberResource1"></asp:TextBox>
                                          </div>
                                      </div>


                                  </div>
                              </div>
                              <br />
                              <asp:Panel ID="newcontactpanel" runat="server" CssClass="panel panel-default">
                                  <div class="panel-heading">
                                      <b>Reference Details</b>
                                  </div>
                                  <div class="container-fluid">
                                      <br />
                                  <div class="row"  padding-top: 10px; padding-bottom: 10px;">
                                      <div class="col-sm-6 col-xs-12">
                                          <div class="row">
                                              <div class="col-sm-4 col-xs-1s2">
                                                <b>  Contact Name 1 :  </b>  
                                              </div>
                                              <div class="col-sm-8 col-xs-12">
                                                  <asp:TextBox ID="txtrefname1" runat="server" class="form-control pr_c1 text_box"
                                                      MaxLength="30" meta:resourcekey="txtrefname1Resource1"
                                                      onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                              </div>
                                          </div>
                                          <br />
                                          <div class="row">
                                              <div class="col-sm-4 col-xs-12">
                                                <b>  Mobile No 1 :</b>    
                                              </div>
                                              <div class="col-sm-8 col-xs-12">
                                                  <asp:TextBox ID="txtref1mob" runat="server" class="form-control pr_c2 text_box"
                                                      MaxLength="10" meta:resourcekey="txtrefname2Resource1"
                                                      onkeypress="return isNumber(event)"></asp:TextBox>
                                              </div>
                                          </div>
                                          <br />
                                          <div class="row">
                                              <div class="col-sm-4 col-xs-12">
                                                <b>  Email Id 1 :</b>
                                              </div>
                                              <div class="col-sm-8 col-xs-12">
                                                  <asp:TextBox ID="txt_emailid1" runat="server" class="form-control  prnt_c2 text_box"
                                                      MaxLength="200" meta:resourcekey="txtref1mobResource1" onkeypress="return email(event)"></asp:TextBox>
                                              </div>
                                          </div>
                                          <br></br>
                                          <div class="row">
                                              <div class="col-sm-4 col-xs-12">
                                                <b>  Address 1 :</b>
                                              </div>
                                              <div class="col-sm-8 col-xs-12">
                                                  <asp:TextBox ID="txt_address1" runat="server" class="form-control  prnt_c2 text_box"
                                                      TextMode="MultiLine" Rows="6"
                                                      MaxLength="200" meta:resourcekey="txtref1mobResource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                              </div>
                                          </div>
                                      </div>
                                      <div class="col-sm-6 col-xs-12">
                                          <div class="row">
                                              <div class="col-sm-4 col-xs-12">
                                                <b>  Contact Name 2 : </b>  
                                              </div>
                                              <div class="col-sm-8 col-xs-12">
                                                  <asp:TextBox ID="txtrefname2" MaxLength="50" runat="server"
                                                      class="form-control prnt_c1 text_box" onkeypress="return AllowAlphabet(event)"
                                                      meta:resourcekey="txt_residencecontactnumberResource1"></asp:TextBox>
                                              </div>
                                          </div>
                                          <br />
                                          <div class="row">
                                              <div class="col-sm-4 col-xs-12">
                                                 <b> Mobile No 2 : </b>  
                                              </div>
                                              <div class="col-sm-8 col-xs-12">
                                                  <asp:TextBox ID="txt_residencecontactnumber" runat="server" class="form-control  prnt_c2 text_box"
                                                      onkeypress="return isNumber(event)"
                                                      MaxLength="10" meta:resourcekey="txtref1mobResource1"></asp:TextBox>
                                              </div>
                                          </div>
                                          <br />
                                          <div class="row">
                                              <div class="col-sm-4 col-xs-12">
                                                 <b> Email Id 2 :</b>
                                              </div>
                                              <div class="col-sm-8 col-xs-12">
                                                  <asp:TextBox ID="txt_emailid2" runat="server" class="form-control  prnt_c2 text_box"
                                                      MaxLength="50" meta:resourcekey="txtref1mobResource1" onkeypress="return email(event)"></asp:TextBox>
                                              </div>
                                          </div>
                                          <br></br>
                                          <div class="row">
                                              <div class="col-sm-4 col-xs-12">
                                                <b>  Address 2 :</b>
                                              </div>
                                              <div class="col-sm-8 col-xs-12">
                                                  <asp:TextBox ID="txt_address2" runat="server" class="form-control  prnt_c2 text_box"
                                                      TextMode="MultiLine" Rows="6"
                                                      MaxLength="200" meta:resourcekey="txtref1mobResource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                              </div>
                                          </div>
                                      </div>
                                  </div>
                                      <br />
                                      </div>
                              </asp:Panel>

                          </div>
                          <div id="menu1">
                              <br />

                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                      <b>Confirmation Date : </b>   
                                  </div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txt_confirmationdate" MaxLength="100" runat="server" Width="100%" class="form-control confirm_date"
                                          meta:resourcekey="txt_confirmationdateResource1"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Joining Date :  </b>  <span class="text-red">*</span>
                                  </div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txt_joiningdate" runat="server" class="form-control date_join" Width="100%"
                                          meta:resourcekey="txt_joiningdateResource1"></asp:TextBox>
                                  </div>
                              </div>
                              <br />
                              <asp:UpdatePanel runat="server">
                                  <ContentTemplate>

                                      <div class="row">
                                          <div class="col-sm-2 col-xs-12">
                                            <b>  Working State :</b>    <span class="text-red">*</span>
                                          </div>
                                          <div class="col-sm-3 col-xs-12">
                                              <%--<asp:DropDownList ID="ddl_location" CssClass="pr_state js-example-basic-single" Width="100%" 
                                            runat="server" OnSelectedIndexChanged="ddl_location_SelectedIndexChanged" AutoPostBack="true" 
                                            meta:resourcekey="ddl_locationResource1"></asp:DropDownList>--%>

                                              <asp:TextBox ID="ddl_location" runat="server" class="form-control text_box"
                                                  MaxLength="50" meta:resourcekey="txtref1mobResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>


                                          </div>
                                          <div class="col-sm-2 col-xs-12">
                                            <b>  Working City : </b>   <span class="text-red">*</span>
                                          </div>
                                          <div class="col-sm-3 col-xs-12">
                                              <asp:TextBox ID="ddl_location_city" runat="server" class="form-control text_box"
                                                  MaxLength="50" meta:resourcekey="txtref1mobResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                              <%--<asp:DropDownList ID="ddl_location_city" CssClass="pr_state js-example-basic-single" Width="100%" runat="server"></asp:DropDownList>--%>
                                          </div>
                                      </div>
                                  </ContentTemplate>
                              </asp:UpdatePanel>
                              <br />
                              <div class="row">
                                  <%--<div class="col-sm-2 col-xs-12">UAN Number :</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_panno" runat="server" class="form-control text_box"
                                        meta:resourcekey="txt_pannoResource1" MaxLength="12" onkeypress="return Alpha_Numeric(event,this);"></asp:TextBox>
                                </div>--%>
                                  <div class="col-sm-2 col-xs-12"><b>Police Station Name :</b></div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txt_policestationname" runat="server" class="form-control text_box"
                                          meta:resourcekey="txt_pannoResource1" MaxLength="225" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Aadhar Card / Enrollment No :</b> <span class="text-red">*</span>
                                  </div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txt_ptaxnumber" onkeypress="return isNumber(event,this);" ReadOnly="true" runat="server" class="form-control text_box" MaxLength="12"
                                          meta:resourcekey="txt_ptaxnumberResource1"></asp:TextBox>
                                  </div>

                              </div>
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  PAN Number : </b>
                                  </div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txt_pan_new_num" runat="server" class="form-control" Width="100%"
                                          meta:resourcekey="txt_joiningdateResource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                  </div>
                                  <%--<div class="col-sm-2 col-xs-12">
                                    PF Employee Percentage :
                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_pfemployeepercentage" runat="server" onkeypress="return isNumber(event,this)"
                                        class="form-control text_box" MaxLength="10">0</asp:TextBox>
                                </div>--%>
                                  <div class="col-sm-2 col-xs-12"><b>Reason For Resign :</b></div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txtreasonforleft" runat="server" class="form-control text_box"
                                          meta:resourcekey="txtreasonforleftResource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>

                                      <br />
                                  </div>

                              </div>
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12"><b>Left Date :</b></div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txt_leftdate" runat="server" class="form-control date_left" Width="100%"
                                          meta:resourcekey="txt_leftdateResource1"></asp:TextBox>
                                  </div>
                              </div>

                              <div class="row" style="display: none">


                                  <%-- <div class="col-sm-2 col-xs-12">ESIC Number :</div>
                                            <div class="col-sm-3 col-xs-12">

                                                <asp:TextBox ID="txt_esicnumber" runat="server" class="form-control"
                                                    meta:resourcekey="txt_esicnumberResource1" Text="A"></asp:TextBox>
                                            </div>--%>
                                  <div class="col-sm-2 col-xs-12"><b>Post To Wages Salary :</b></div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:DropDownList ID="ddlpfregisteremp" class="form-control" runat="server"
                                          meta:resourcekey="ddlpfregisterempResource1">
                                          <asp:ListItem Value="Yes"
                                              meta:resourcekey="ListItemResource10" Text="Yes"></asp:ListItem>
                                          <asp:ListItem Value="No" meta:resourcekey="ListItemResource11" Text="No"></asp:ListItem>
                                      </asp:DropDownList>
                                  </div>
                              </div>
                              <%-- <div class="row">


                                <div class="col-sm-2 col-xs-12">P.F Number : </div>
                                <div class="col-sm-3 col-xs-12">

                                    <asp:TextBox ID="txt_pfnumber" runat="server" AutoPostBack="True"
                                        class="form-control text_box" OnTextChanged="txt_pfnumber_TextChanged"
                                        meta:resourcekey="txt_pfnumberResource1" Text="A" 
                                        onkeypress="return isNumber(event)" MaxLength="40"></asp:TextBox>
                                </div>

                                <div class="col-sm-2 col-xs-12">PF Flag :</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:DropDownList ID="ddl_pfdeductionflag" runat="server" class="form-control" meta:resourcekey="ddl_pfdeductionflagResource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource8" Text="Yes" Value="Yes"></asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource9" Text="No" Value="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                 <%--<div class="row">
                                <%--<div class="col-sm-2 col-xs-12">ESIC Number : </div>
                                <div class="col-sm-3 col-xs-12">

                                    <asp:TextBox ID="txt_esicnumber" runat="server" class="form-control text_box"
                                        meta:resourcekey="txt_esicnumberResource1"    Text="A"></asp:TextBox>
                                </div>

                                <%--<div class="col-sm-2 col-xs-12">ESIC Flag :</div>
                                <div class="col-sm-3 col-xs-12">

                                    <asp:DropDownList ID="ddl_esicdeductionflag" runat="server"
                                        class="form-control" meta:resourcekey="ddl_esicdeductionflagResource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource12" Text="Yes"></asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource13" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>
                              <%-- </div>--%>
                              <br />

                              <br />
                              <br />
                              <%--  <div class ="row">
                                 <div class="col-sm-2 col-xs-12">Medi claim No:</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_mediclaim" runat="server"  class="form-control" Width="100%"
                                        meta:resourcekey="txt_leftdateResource1"></asp:TextBox>
                                </div>
                                 <div class="col-sm-2 col-xs-12">Acci Policy No:</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_acci" runat="server"  class="form-control" Width="100%"
                                        meta:resourcekey="txt_leftdateResource1"></asp:TextBox>
                                </div>
                            </div>
                            <br />--%>

                              <asp:DropDownList ID="ddl_ptaxdeductionflag" runat="server"
                                  class="form-control" Visible="False"
                                  meta:resourcekey="ddl_ptaxdeductionflagResource1">
                                  <asp:ListItem meta:resourcekey="ListItemResource14" Text="Yes"></asp:ListItem>
                                  <asp:ListItem meta:resourcekey="ListItemResource15" Text="No"></asp:ListItem>
                              </asp:DropDownList>
                          </div>
                          <div id="menu6">
                              <br />

                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Bank Account Holder Name :</b>  <span style="color: red">*</span>

                                  </div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txt_bankholder" runat="server" class="form-control text_box" MaxLength="50" meta:resourcekey="txt_pfbanknameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Bank Name : </b> 
                                  </div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txt_pfbankname" runat="server" class="form-control text_box" MaxLength="50" meta:resourcekey="txt_pfbanknameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                  </div>

                              </div>
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12"><b>Branch Location Name :</b></div>
                                  <div class="col-sm-3 col-xs-12">

                                      <asp:TextBox ID="ddl_bankcode" runat="server" class="form-control text_box" MaxLength="200" meta:resourcekey="ddl_bankcodeResource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                  </div>

                                  <div class="col-sm-2 col-xs-12">
                                     <b> Other Bank A/C Number :</b>
                                        
                                  </div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txt_employeeaccountnumber" runat="server" class="form-control text_box"
                                          meta:resourceKey="txt_employeeaccountnumberResource1" MaxLength="20" onkeypress="return isNumber(event);"></asp:TextBox>
                                  </div>
                              </div>

                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                      <b>Original Bank A/C Number :</b><span style="color: red">*</span>

                                  </div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txt_originalbankaccountno" runat="server" class="form-control text_box"
                                          meta:resourceKey="txt_employeeaccountnumberResource1" MaxLength="20" onkeypress="return isNumber_bank(event);"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                     <b> IFSC Code :</b><span style="color: red">*</span>
                                  </div>

                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txt_pfifsccode" runat="server" class="form-control text_box" MaxLength="11"
                                          meta:resourceKey="txt_pfifsccodeResource1" onkeypress="return AllowAlphabet_Number(event,this);" onchange="return valid_ifcs();"></asp:TextBox>
                                  </div>

                              </div>
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                     <b> NFD/IFD : </b>   <span class="text-red">*</span>

                                  </div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:DropDownList ID="ddl_infitcode" runat="server" class="form-control">
                                          <asp:ListItem Value="Select" Text="Select Transfer">Select Transfer</asp:ListItem>
                                          <asp:ListItem Value="N" Text="NEFT TRANSFER">NEFT TRANSFER</asp:ListItem>
                                          <asp:ListItem Value="I" Text="INTERNAL TRANSFER">INTERNAL TRANSFER</asp:ListItem>
                                      </asp:DropDownList>
                                  </div>
                                  <div class="col-sm-2 col-xs-12"><b>Nominee Name :</b></div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txt_pfnomineename" runat="server" class="form-control text_box"
                                          meta:resourceKey="txt_pfnomineenameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                  </div>


                              </div>
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12"><b>Nominee Relation :</b></div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txt_pfnomineerelation" runat="server" class="form-control text_box" MaxLength="20" meta:resourceKey="txt_pfnomineerelationResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-2 col-xs-12"><b>Nominee Birth Date :</b></div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="txt_pfbdate" runat="server" class="form-control date-picker" meta:resourceKey="txt_pfbdateResource1" Width="294"></asp:TextBox>

                                  </div>

                              </div>
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12"><b>Nominee Address :</b></div>
                                  <div class="col-sm-3 col-xs-12">
                                      <asp:TextBox ID="nominee_address" runat="server" class="form-control text-box" meta:resourceKey="txt_pfbdateResource1" MaxLength="300" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>

                                  </div>
                              </div>
                              <br />
                              <%--<asp:Label ID="lblbankname" runat="server" meta:resourceKey="lblbanknameResource1"></asp:Label>--%>
                              <%-- <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:CELTPAYConnectionString %>" ProviderName="<%$ ConnectionStrings:celtpayConnectionString.ProviderName %>" SelectCommand="SELECT BANK_CODE, BANK_NAME,concat(BANK_CODE,'-',BANK_NAME) AS CBANK FROM pay_bank_master  WHERE comp_code=@comp_code  ORDER BY BANK_CODE">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="comp_code" SessionField="comp_code" />
                                    </SelectParameters>
                                </asp:SqlDataSource>--%>
                          </div>
                          <div id="menu9">
                              <br />
                              <div class="row">
                                  <div class="col-lg-1 col-md-4 col-sm-6 col-xs-12 text-right"><b>Number of Child :</b></div>
                                  <div class="col-lg-1 col-md-4 col-sm-6 col-xs-12">
                                      <asp:TextBox ID="Numberchild" runat="server" class="form-control" onkeypress="return isNumber(event)" meta:resourceKey="txt_advance_paymentResource1"></asp:TextBox>
                                  </div>

                              </div>
                              <br />
                              <table id="maintable" class="table table-responsive main_table" border="1" runat="server">
                                  <tr style="background-color: #aaa;">
                                      <th align="center">Name</th>
                                      <th align="center">Relation</th>
                                      <th align="center">DOB</th>
                                      <th align="center">PAN No</th>
                                      <th align="center">Aadhar No</th>
                                      <th align="center">Mobile No</th>

                                  </tr>
                                  <tr id="rows">
                                      <td>
                                          <asp:TextBox ID="txt_name1" ReadOnly="true" runat="server" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_relation1" ReadOnly="true" runat="server" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control">Father</asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_dob1" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_pan1" runat="server" MaxLength="20" onkeypress="return AllowAlphabet_Number_slash(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_adhaar1" runat="server" MaxLength="20" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_fmobile1" runat="server" MaxLength="10" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr id="rows2">
                                      <td>
                                          <asp:TextBox ID="txt_name2" runat="server" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_relation2" ReadOnly="true" runat="server" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control">Mother</asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_dob2" runat="server" MaxLength="20" CssClass="form-control date-picker"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_pan2" runat="server" MaxLength="20" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_adhaar2" runat="server" MaxLength="20" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_fmobile2" runat="server" MaxLength="10" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr id="rows3">
                                      <td>
                                          <asp:TextBox ID="txt_name3" runat="server" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_relation3" ReadOnly="true" runat="server" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control">Wife</asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_dob3" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_pan3" runat="server" MaxLength="20" onkeypress="return AllowAlphabet_Number_slash(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_adhaar3" runat="server" MaxLength="20" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_fmobile3" runat="server" MaxLength="10" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr id="rows4">
                                      <td>
                                          <asp:TextBox ID="txt_name4" ReadOnly="true" runat="server" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_relation4" ReadOnly="true" runat="server" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control">Husband</asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_dob4" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_pan4" runat="server" MaxLength="20" onkeypress="return AllowAlphabet_Number_slash(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_adhaar4" runat="server" MaxLength="20" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_fmobile4" runat="server" MaxLength="10" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr id="rows5">
                                      <td>
                                          <asp:TextBox ID="txt_name5" runat="server" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_relation5" runat="server" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control">Child</asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_dob5" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_pan5" runat="server" MaxLength="20" onkeypress="return AllowAlphabet_Number_slash(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_adhaar5" runat="server" MaxLength="20" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_fmobile5" runat="server" MaxLength="10" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr id="rows6">
                                      <td>
                                          <asp:TextBox ID="txt_name6" runat="server" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_relation6" runat="server" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control">Child</asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_dob6" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_pan6" runat="server" MaxLength="20" onkeypress="return AllowAlphabet_Number_slash(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_adhaar6" runat="server" MaxLength="20" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_fmobile6" runat="server" MaxLength="10" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr id="rows7">
                                      <td>
                                          <asp:TextBox ID="txt_name7" runat="server" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_relation7" runat="server" MaxLength="50" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control">Child</asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_dob7" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_pan7" runat="server" MaxLength="20" onkeypress="return AllowAlphabet_Number_slash(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_adhaar7" runat="server" MaxLength="20" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="txt_fmobile7" runat="server" MaxLength="10" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                      </td>
                                  </tr>
                              </table>

                          </div>
                          <div id="menu7">
                              <div class="row">
                                  <br />
                                  <div class="col-sm-6 col-xs-12">
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                            <b>  Nationality :  </b>  <span class="text-red">*</span>
                                          </div>
                                          <div class="col-sm-6 col-xs-12" onkeypress="return lettersOnly(event,this);">
                                              <asp:TextBox ID="txt_Nationality" runat="server" CssClass="form-control txt_Nationality"
                                                  class="text_box" Text="INDIAN" onkeypress="return AllowAlphabet(event)"  MaxLength="20"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                            <b>  Identitymark :</b>
                                        
                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:TextBox ID="txt_Identitymark" runat="server" CssClass="form-control" class="text_box"
                                                  MaxLength="30" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                            <b>  Mother Tongue :</b>
                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:TextBox ID="ddl_Mother_Tongue" runat="server" CssClass="form-control" class="text_box"
                                                  onkeypress="return AllowAlphabet_address(event)" MaxLength="30"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right"><b>Hobbies :</b></div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:TextBox ID="txt_hobbies" runat="server" MaxLength="100" class="form-control text_box" onkeypress="return AllowAlphabet_Number10(event)" meta:resourcekey="txt_hobbiesResource1"></asp:TextBox>
                                          </div>

                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                             <b> Passport No :</b>
                                          </div>
                                          <div class="col-sm-6">
                                              <asp:TextBox ID="txt_Passport_No" runat="server" MaxLength="20" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>
                                          </div>
                                      </div>

                                      <div class="row" style="display: none">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                             <b> Visa(Country) :</b>
                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:DropDownList ID="ddl_Visa_Country" runat="server" class="form-control text_box"
                                                  meta:resourcekey="ddl_ptaxdeductionflagResource1">
                                                  <asp:ListItem meta:resourcekey="ListItemResource14">Select</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource14">Indian</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource15">USA</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource15">LANDON</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource15">CANADA</asp:ListItem>
                                              </asp:DropDownList>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                             <b> Driving Licence No :</b>
                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:TextBox ID="txt_Driving_License_No" runat="server" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_Number(event,this);" MaxLength="30"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row" style="display: none">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                             <b> Mise :</b>
                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:TextBox ID="txt_Mise" runat="server" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_Number(event,this);" MaxLength="50"></asp:TextBox>
                                          </div>
                                      </div>

                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                             <b> Weight (in Kg) :</b>
                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:TextBox ID="txt_weight" runat="server" onkeypress="return isNumber_dot(event)" MaxLength="30" CssClass="form-control" class="text_box maskedExt" maskedFormat="10,2" meta:resourcekey="txt_weightResource1">0</asp:TextBox>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                            <b>  Marital Status :</b>
                                             <%--<asp:Label ID="Label17" runat="server" ForeColor="Red" Text=" * "></asp:Label>--%>
                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                               <asp:DropDownList ID="ddl_MaritalStatus" runat="server" class="form-control" Width="100%">
                                                   <asp:ListItem  Value="">Select</asp:ListItem>
                                                  <asp:ListItem Text="Married" Value="M">Married</asp:ListItem>
                                                  <asp:ListItem Text="UnMarried" Value="U">UnMarried</asp:ListItem>
                                                  
                                               </asp:DropDownList>
                                          </div>
                                      </div>
                                  </div>
                                  <div class="col-sm-6 col-xs-12">
                                      <div class="row">

                                          <div class="col-sm-4 col-xs-12 text-right"><b>Religion :</b></div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:DropDownList
                                                  ID="ddl_religion" runat="server" class="form-control" Width="100%"
                                                  meta:resourcekey="ddl_religionResource1">
                                                  <asp:ListItem meta:resourcekey="ListItemResource16" Value="Select">Select</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource16" Value="General">General</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource17" Value="OBC">OBC</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource18" Value="SC">SC</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource19" Value="ST">ST</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource20" Value="NT">NT</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource21" Value="Other">Other</asp:ListItem>
                                              </asp:DropDownList>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                             <b> Place Of Birth :</b>

                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:TextBox ID="txt_Place_Of_Birth" runat="server" CssClass="form-control"
                                                  class="text_box" onkeypress="return AllowAlphabet_address(event)" MaxLength="50"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                             <b> Language Known :</b>
                                       
                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:TextBox ID="txt_Language_Known" runat="server" CssClass="form-control" class="text_box"
                                                  meta:resourcekey="txt_LanguageResource1" onkeypress="return AllowAlphabet_address(event)" MaxLength="100"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                             <b> Area Of Expertise :</b>
                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:TextBox ID="txt_Area_Of_Expertise" runat="server" CssClass="form-control" class="text_box" MaxLength="20 " onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>
                                          </div>
                                      </div>

                                      <div class="row" style="display: none">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                             <b> Passport Validity Date :</b>
                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:TextBox ID="txt_Passport_Validity_Date" runat="server" MaxLength="20" class="text_box" CssClass="form-control pass_vissa_passport" Width="100%"></asp:TextBox>
                                          </div>
                                      </div>

                                      <div class="row" style="display: none">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                             <b> Visa Validity Date : </b>
                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:TextBox ID="txt_Visa_Validity_Date" runat="server" CssClass="form-control pass_vissa" class="text_box" Width="100%"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                            <b>  Details Of Handicap  :</b>
                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:TextBox ID="txt_Details_Of_Handicap" runat="server" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control" class="text_box" MaxLength="20"></asp:TextBox>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                            <b>  Height (in Feets) :</b>
                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:TextBox ID="txt_height" runat="server" class="form-control text_box maskedExt" maskedFormat="10,2" onkeypress="return isNumber_dot(event)" MaxLength="10" meta:resourcekey="txt_heightResource1">0</asp:TextBox>
                                          </div>
                                      </div>
                                      <br />

                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right">
                                             <b> Blood Group :</b>
                                          </div>
                                          <div class="col-sm-6 col-xs-12">
                                              <asp:DropDownList ID="ddl_bloodgroup" runat="server" CssClass="form-control" meta:resourcekey="ddl_bloodgroupResource1">
                                                  <asp:ListItem meta:resourcekey="ListItemResource22">Select</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource22">A+</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource23">A-</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource24">B+</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource25">B-</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource26">O+</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource27">O-</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource28">AB+</asp:ListItem>
                                                  <asp:ListItem meta:resourcekey="ListItemResource29">AB-</asp:ListItem>
                                              </asp:DropDownList>
                                          </div>
                                      </div>
                                      <br />
                                      <div class="row">
                                          <div class="col-sm-4 col-xs-12 text-right">

                                              <asp:Label ID="lblQuali" Text=" Qualification:" Visible="False" runat="server"></asp:Label>
                                          </div>

                                      </div>
                                      <br />

                                  </div>
                              </div>
                          </div>
                          <div id="menu2">
                              <div class="row">
                                  <div class="col-sm-6 col-xs-12" style="border-right: 1px solid #808080;">
                                      <div class="row text-center">
                                          <h3>Qualification</h3>
                                      </div>
                                      <div class="row">
                                          <div class="col-sm-8 col-xs-12">
                                              <b>Qualification1 :</b>
                                      
                                                     <asp:TextBox ID="txt_qualification_1" runat="server" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_address(event)" MaxLength="20"></asp:TextBox>
                                          </div>
                                          <div class="col-sm-4 col-xs-12">
                                             <b> Year of passing :</b>
                                      
                                                     <asp:TextBox ID="txt_year_of_passing_1" onkeypress="return isNumber(event)" MaxLength="4" class="text_box" runat="server" CssClass="form-control"></asp:TextBox>
                                          </div>
                                      </div>
                                      <div class="row">
                                          <br />
                                          <div class="col-sm-8 col-xs-12">
                                             <b> Qualification2 :</b>
                                                     <asp:TextBox ID="txt_qualification_2" runat="server" CssClass="form-control" class="text_box" MaxLength="15" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                          </div>
                                          <div class="col-sm-4 col-xs-12">
                                              <br />
                                              <asp:TextBox ID="txt_year_of_passing_2" onkeypress="return isNumber(event)" class="text_box" MaxLength="4" runat="server" CssClass="form-control"></asp:TextBox>
                                          </div>
                                      </div>
                                      <div class="row">
                                          <br />
                                          <div class="col-sm-8 col-xs-12">
                                            <b>  Qualification3 :</b>
                                                     <asp:TextBox ID="txt_qualification_3" runat="server" CssClass="form-control" class="text_box"
                                                         onkeypress="return AllowAlphabet_address(event)" MaxLength="20"></asp:TextBox>
                                          </div>
                                          <div class="col-sm-4 col-xs-12">
                                              <br />
                                              <asp:TextBox ID="txt_year_of_passing_3" onkeypress="return isNumber(event)" class="text_box"
                                                  runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                          </div>
                                      </div>
                                      <div class="row">
                                          <br />
                                          <div class="col-sm-8 col-xs-12">
                                             <b> Qualification4 :</b>
                                                     <asp:TextBox ID="txt_qualification_4" runat="server" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_address(event)" MaxLength="15"></asp:TextBox>
                                          </div>
                                          <div class="col-sm-4 col-xs-12">
                                              <br />
                                              <asp:TextBox ID="txt_year_of_passing_4" onkeypress="return isNumber(event)" class="text_box" runat="server" MaxLength="4" CssClass="form-control"></asp:TextBox>
                                          </div>
                                      </div>
                                      <div class="row">
                                          <br />
                                          <div class="col-sm-8 col-xs-12">
                                             <b> Qualification5 :</b>
                                                     <asp:TextBox ID="txt_qualification_5" runat="server" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_address(event)" MaxLength="15"></asp:TextBox>
                                          </div>
                                          <div class="col-sm-4 col-xs-12">
                                              <br />
                                              <asp:TextBox ID="txt_year_of_passing_5" onkeypress="return isNumber(event)" runat="server" MaxLength="4" class="text_box" CssClass="form-control"></asp:TextBox>
                                          </div>
                                      </div>
                                  </div>
                                  <div class="col-sm-6 col-xs-12">
                                      <div class="row text-center">
                                          <h3>Skill</h3>
                                      </div>
                                      <div class="row">
                                          <div class="col-sm-8 col-xs-12">
                                             <b> Key Skill1 :</b> 
                                                     <asp:TextBox ID="txt_key_skill_1" runat="server" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_address(event)" MaxLength="50"></asp:TextBox>
                                          </div>
                                          <div class="col-sm-4 col-xs-12">
                                             <b> Experience in months </b>
                                                      <asp:TextBox ID="txt_experience_in_months_1" runat="server" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_address(event)" MaxLength="20">0</asp:TextBox>
                                          </div>
                                      </div>
                                      <div class="row">
                                          <br />
                                          <div class="col-sm-8 col-xs-12">
                                             <b> Key Skill2 :</b>
                                                     <asp:TextBox ID="txt_key_skill_2" runat="server" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_address(event)" MaxLength="20"></asp:TextBox>
                                          </div>
                                          <div class="col-sm-4 col-xs-12">
                                              <br />
                                              <asp:TextBox ID="txt_experience_in_months_2" runat="server" CssClass="form-control" onkeypress="return AllowAlphabet_address(event)" MaxLength="20"> 0</asp:TextBox>
                                          </div>
                                      </div>
                                      <div class="row">
                                          <br />
                                          <div class="col-sm-8 col-xs-12">
                                              <b>Key Skill3 :</b>
                                                     <asp:TextBox ID="txt_key_skill_3" runat="server" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_address(event)" MaxLength="20"></asp:TextBox>
                                          </div>
                                          <div class="col-sm-4 col-xs-12">
                                              <br />
                                              <asp:TextBox ID="txt_experience_in_months_3" runat="server" class="text_box" CssClass="form-control" onkeypress="return AllowAlphabet_address(event)" MaxLength="20">0</asp:TextBox>
                                          </div>
                                      </div>
                                      <div class="row">
                                          <br />
                                          <div class="col-sm-8 col-xs-12">
                                             <b> Key Skill4 :</b>
                                                     <asp:TextBox ID="txt_key_skill_4" runat="server" class="text_box" CssClass="form-control" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                          </div>
                                          <div class="col-sm-4 col-xs-12">
                                              <br />
                                              <asp:TextBox ID="txt_experience_in_months_4" runat="server" class="text_box" CssClass="form-control" onkeypress="return AllowAlphabet_address(event)" MaxLength="10">0</asp:TextBox>
                                          </div>
                                      </div>
                                      <div class="row">
                                          <br />
                                          <div class="col-sm-8 col-xs-12">
                                             <b> Key Skill5 :</b>
                                                     <asp:TextBox ID="txt_key_skill_5" runat="server" CssClass="form-control" class="text_box" MaxLength="10" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                          </div>
                                          <div class="col-sm-4 col-xs-12">
                                              <br />
                                              <asp:TextBox ID="txt_experience_in_months_5" runat="server" CssClass="form-control" onkeypress="return AllowAlphabet_address(event)">0</asp:TextBox>
                                          </div>
                                      </div>
                                  </div>
                              </div>
                          </div>
                          <div id="menu4">
                              <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                  <ContentTemplate>
                                      <%--<ul class="nav nav-tabs">
                                        <li id="itemtab5" ><a data-toggle="tab" href="#item5">Allowance</a></li>
                                    </ul>--%>
                                      <div class="tab-content">
                                          <%--<div id="item5" class="tab-pane fade in active">--%>
                                          <div class="row">
                                              <br />
                                              <%-- <div class="col-sm-2 col-xs-12">
                                                    CCA :
                                                    <asp:TextBox ID="Txt_cca" runat="server" Text="0" class="form-control text_box maskedExt" maskedFormat="10,2" onkeypress="return isNumber_dot(event)"   
                                                        meta:resourcekey="txtdhead6Resource1"  MaxLength="7"  ></asp:TextBox>
                                                </div>
                                                 <div class="col-sm-2 col-xs-12">Gratuity:
                                                     <asp:TextBox ID="Txt_gra" runat="server"  text="0" class="form-control text_box  maskedExt"  maskedFormat="10,2"  onkeypress="return isNumber_dot(event)"
                                                        meta:resourcekey="txtdhead6Resource1" MaxLength="7"  ></asp:TextBox>
                                                </div>
                                                                 <div class="col-sm-2 col-xs-12">Special Allowance :
                                   
                                                     <asp:TextBox ID="Txt_allow" runat="server" Text="0" class="form-control text_box  maskedExt"  maskedFormat="10,2"  onkeypress="return isNumber_dot(event)"
                                                        meta:resourcekey="txtdhead6Resource1" MaxLength="7" ></asp:TextBox>
                                                </div>--%>
                                              <div class="col-sm-2 col-xs-12">
                                                <b>  Fine :</b>
                                   
                                                     <asp:TextBox ID="txt_fine" runat="server" Text="0" class="form-control text_box  maskedExt" maskedFormat="10,2" onkeypress="return isNumber_dot(event)"
                                                         meta:resourcekey="txtdhead6Resource1" MaxLength="7"></asp:TextBox>
                                              </div>
                                              <div class="col-sm-2 col-xs-12">
                                               <b>   Advance :</b>
                                                <asp:TextBox ID="txt_advance_payment" runat="server" class="form-control" onkeypress="return isNumber(event)" meta:resourceKey="txt_advance_paymentResource1"></asp:TextBox>
                                              </div>
                                          </div>

                                      </div>
                                  </ContentTemplate>
                              </asp:UpdatePanel>
                          </div>
                          <div id="menu5">


                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Duplicate  Photo(Passport Size) :</b>
                                                <br />
                                      <asp:Label ID="photo" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:Panel ID="Panel111" runat="server">
                                          <asp:FileUpload ID="photo_upload" runat="server" meta:resourcekey="photo_uploadResource1" />
                                      </asp:Panel>
                                      <asp:LinkButton ID="link_photo" Text="Download" runat="server" CommandArgument="_1" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image4" runat="server" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/placeholder.png" Style="margin-left: 10em;" />
                                  </div>
                                  <div class="col-sm-2 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Original Photo(Passport Size) :</b>
                                                <br />
                                      <asp:Label ID="lbl_originalphoto" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="originalphoto" runat="server" meta:resourcekey="photo_uploadResource1" />
                                      <asp:LinkButton ID="link_originalphoto" Text="Download" runat="server" CommandArgument="_20" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image21" runat="server" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/placeholder.png" Style="margin-left: 10em;" />
                                  </div>
                              </div>
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Duplicate Aadhar Card :</b>
                                                <br />
                                      <asp:Label ID="l_bank_upload" runat="server" Text="Employee AADHAR Card Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:Panel ID="Panel112" runat="server">
                                          <asp:FileUpload ID="bank_upload" runat="server" meta:resourcekey="bank_uploadResource1" />
                                      </asp:Panel>
                                      <asp:LinkButton ID="link_bank_upload" Text="Download" runat="server" CommandArgument="_3" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image2" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image2Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/passbook.jpg" />
                                  </div>
                                  <div class="col-sm-2 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Original Aadhar Card :</b>
                                                <br />
                                      <asp:Label ID="lbl_originaladharcard" runat="server" Text="Employee AADHAR Card Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="original_adharcard_upload" runat="server" meta:resourcekey="bank_uploadResource1" />
                                      <asp:LinkButton ID="link_original_adharcard_upload" Text="Download" runat="server" CommandArgument="_21" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image22" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image2Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/passbook.jpg" />
                                  </div>


                              </div>
                              <br />

                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Duplicate Police Verification Document :</b>
                                                <br />
                                      <asp:Label ID="l_Police_document" runat="server" Text="Employee Police Verification Document  Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:Panel ID="Panel113" runat="server">
                                          <asp:FileUpload ID="Police_document" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                      </asp:Panel>
                                      <asp:LinkButton ID="link_Police_document" Text="Download" runat="server" CommandArgument="_13" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image14" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                  </div>


                                  <div class="col-sm-2 col-xs-12"></div>

                                  <div class="col-sm-2 col-xs-12">
                                     <b> Original Police Verification Document :</b>
                                                <br />
                                      <asp:Label ID="o_policy_document" runat="server" Text="Employee Police Verification Document  Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="original_police_document" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                      <asp:LinkButton ID="link_original_police_document" Text="Download" runat="server" CommandArgument="_22" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image23" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                  </div>
                              </div>
                              <br />

                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Duplicate Proof of Present Address :</b>
                                                <br />
                                      <asp:Label ID="l_Address_Proof" runat="server" Text="Employee Proof of Present Address Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:Panel ID="Panel114" runat="server">
                                          <asp:FileUpload ID="Address_Proof" runat="server" meta:resourcekey="photo_uploadResource1" />
                                      </asp:Panel>
                                      <asp:LinkButton ID="link_Address_Proof" Text="Download" runat="server" CommandArgument="_15" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image17" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/Biodata.png" />
                                  </div>


                                  <div class="col-sm-2 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Original Proof of Present Address :</b> 
                                                <br />
                                      <asp:Label ID="o_Address_Proof" runat="server" Text="Employee Proof of Present Address Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="original_Address_Proof" runat="server" meta:resourcekey="photo_uploadResource1" />
                                      <asp:LinkButton ID="link_original_Address_Proof" Text="Download" runat="server" CommandArgument="_23" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image24" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/Biodata.png" />
                                  </div>

                              </div>
                              <br />

                              <div class="row">

                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Passport :</b>
                                            <br />
                                      <asp:Label ID="l_Passport" runat="server" Text="Employee Passport Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="Passport_upload" runat="server" meta:resourcekey="Passport_uploadResource1" />
                                      <asp:LinkButton ID="link_Passport_upload" Text="Download" runat="server" CommandArgument="_5" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image5" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image5Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/Passport.jpg" />
                                  </div>
                                  <div class="col-sm-2 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                      <b>Joining Kit:</b>
                                            <br />
                                      <asp:Label ID="Label3" runat="server" Text="Employee Form No111 Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="emp_signature" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                      <asp:LinkButton ID="link_emp_signature" Text="Download" runat="server" CommandArgument="_19" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image20" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                  </div>



                              </div>
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  10th Marksheet :</b>
                                            <br />
                                      <asp:Label ID="l_Tenth_Marksheet_upload" runat="server" Text="Employee 10th Marksheet Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="Tenth_Marksheet_upload" runat="server" meta:resourcekey="Tenth_Marksheet_uploadResource1" />
                                      <asp:LinkButton ID="link_Tenth_Marksheet_upload" Text="Download" runat="server" CommandArgument="_7" OnCommand="download_document1"></asp:LinkButton>
                                  </div>

                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image7" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image7Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/marksheet.jpg" />
                                  </div>
                                  <div class="col-sm-2 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Form No 11 :</b>
                                            <br />
                                      <asp:Label ID="l_Formno_11" runat="server" Text="Employee Form No111 Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="Formno_11" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                      <asp:LinkButton ID="link_Formno_11" Text="Download" runat="server" CommandArgument="_16" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image16" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                  </div>



                              </div>
                              <br />

                              <div class="row">

                                  <div class="col-sm-2 col-xs-12">
                                     <b> Diploma Certificate :</b>
                                            <br />
                                      <asp:Label ID="l_Diploma_upload" runat="server" Text="Employee Diploma Certificate Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="Diploma_upload" runat="server" meta:resourcekey="Diploma_uploadResource1" />
                                      <asp:LinkButton ID="link_Diploma_upload" Text="Download" runat="server" CommandArgument="_9" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image9" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image9Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                  </div>
                                  <div class="col-sm-2 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Form No 2 :</b>
                                            <br />
                                      <asp:Label ID="l_Formno_2" runat="server" Text="Employee  Form No 2  Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="Formno_2" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                      <asp:LinkButton ID="link_Formno_2" Text="Download" runat="server" CommandArgument="_14" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image15" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                  </div>


                              </div>
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Post Graduation Certificate :</b>
                                            <br />

                                      <asp:Label ID="l_Post_Graduation_upload" runat="server" Text="Employee Post Graduation Certificate Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="Post_Graduation_upload" runat="server" meta:resourcekey="Post_Graduation_uploadResource1" />
                                      <asp:LinkButton ID="link_Post_Graduation_upload" Text="Download" runat="server" CommandArgument="_11" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image11" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image11Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                  </div>
                                  <div class="col-sm-2 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Bank Passbook  :</b>
                                            <br />
                                      <asp:Label ID="lbl_bank_passbook" runat="server" Text="Employee Form No111 Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="bank_passbook" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                      <asp:LinkButton ID="link_bank_passbook" Text="Download" runat="server" CommandArgument="_24" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image25" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                  </div>



                              </div>
                              <br />

                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Degree Certificate :</b>
                                            <br />
                                      <asp:Label ID="l_Degree_upload" runat="server" Text="Employee Degree Certificate Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="Degree_upload" runat="server" meta:resourcekey="Degree_uploadResource1" />
                                      <asp:LinkButton ID="link_Degree_upload" Text="Download" runat="server" CommandArgument="_10" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image10" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image10Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                  </div>
                                  <div class="col-sm-2 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Driving Liscence :</b>
                                            <br />
                                      <asp:Label ID="l_Driving_Liscence_upload" runat="server" Text="Employee Driving Liscence Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="Driving_Liscence_upload" runat="server" meta:resourcekey="Driving_Liscence_uploadResource1" />
                                      <asp:LinkButton ID="link_Driving_Liscence_upload" Text="Download" runat="server" CommandArgument="_6" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image6" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image6Resource1" onkeypress="return AllowAlphabet_Number(event,this);" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/Driving_liscence.jpg" />
                                  </div>

                              </div>
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                     <b> NOC Form :</b>
                                            <br />
                                      <asp:Label ID="Label1" runat="server" Text="Employee Form No111 Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="noc_form" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                      <asp:LinkButton ID="link_noc_form" Text="Download" runat="server" CommandArgument="_17" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image18" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                  </div>
                                  <div class="col-sm-2 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Education Certificate :</b>
                                            <br />
                                      <asp:Label ID="l_Education_4_upload" runat="server" Text="Employee Graduation Certificate Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="Education_4_upload" runat="server" meta:resourcekey="Education_4_uploadResource1" />
                                      <asp:LinkButton ID="link_Education_4_upload" Text="Download" runat="server" CommandArgument="_12" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image12" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image12Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                  </div>
                              </div>
                              <br />
                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                    <b>  Pan Card :</b> 
                                            <br />
                                      <asp:Label ID="l_adhar_pan_upload" runat="server" Text="Employee PAN Card Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="adhar_pan_upload" runat="server" meta:resourcekey="adhar_pan_uploadResource1" />

                                      <asp:LinkButton ID="link_adhar_pan_upload" Text="Download" runat="server" CommandArgument="_2" OnCommand="download_document1"></asp:LinkButton>
                                      <br />

                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image1" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image1Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/pan.jpg" />
                                  </div>
                                  <div class="col-sm-2 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Biodata :</b>
                                            <br />
                                      <asp:Label ID="l_biodata_upload" runat="server" Text="Employee Biodata Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="biodata_upload" runat="server" meta:resourcekey="biodata_uploadResource1" />
                                      <asp:LinkButton ID="link_biodata_upload" Text="Download" runat="server" CommandArgument="_4" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image3" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image3Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/Biodata.png" />
                                  </div>

                                  <br />

                              </div>
                              <br />

                              <div class="row">
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Medical Document :</b>
                                            <br />
                                      <asp:Label ID="Label2" runat="server" Text="Employee Form No111 Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">

                                      <asp:FileUpload ID="medical_form" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                      <asp:LinkButton ID="link_medical_form" Text="Download" runat="server" CommandArgument="_18" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image19" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                  </div>
                                  <div class="col-sm-2 col-xs-12"></div>
                                  <div class="col-sm-2 col-xs-12">
                                     <b> 12th Marksheet :</b>
                                            <br />
                                      <asp:Label ID="l_Twelve_Marksheet_upload" runat="server" Text="Employee 12th Marksheet Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 col-xs-12">
                                      <asp:FileUpload ID="Twelve_Marksheet_upload" runat="server" meta:resourcekey="Twelve_Marksheet_uploadResource1" />
                                      <asp:LinkButton ID="link_Twelve_Marksheet_upload" Text="Download" runat="server" CommandArgument="_8" OnCommand="download_document1"></asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="Image8" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image8Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/marksheet.jpg" />
                                  </div>
                              </div>
                              <br />


                              <div class="row">

                                  <div class="col-sm-2 col-xs-12">
                                      <b>TIC Document :</b>
                                            <br />
                                      <asp:Label ID="ITC_document" runat="server" Text="Employee Form No111 Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                  </div>

                                  <div class="col-sm-1 col-xs-12">

                                      <asp:FileUpload ID="itc_upload_form" runat="server" meta:resourcekey="itc_upload_documents" />
                                      <asp:LinkButton ID="itc_link_btn" runat="server" Text="Download" CommandArgument="_25" OnCommand="download_document1"></asp:LinkButton>
                                  </div>

                                  <div class="col-sm-2 col-xs-12">
                                      <asp:ImageButton ID="itc_img_btn" Style="margin-left: 10em;" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                  </div>

                              </div>
                              <br />



                              <div class="row text-center">
                                  <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" OnClick="Upload_File" Text="Upload" />
                              </div>
                              <br />
                              <div class="row text-center">
                                  Note : Only JPG , PNG , PDF files will be uploaded.
                              </div>

                          </div>
                          <div id="menu8" style="display: none">
                              <br />
                              <div class="row">

                                  <%--<div class="col-lg-1 col-md-4 col-sm-6 col-xs-12 text-right">EMI :</div>--%>
                                  <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                                      <asp:TextBox ID="txtdhead1" runat="server" class="form-control" Visible="false" onkeypress="return isNumber(event)" meta:resourceKey="txtdhead1Resource1"></asp:TextBox>
                                  </div>
                                  <%--<div class="col-lg-2 col-md-4 col-sm-6 col-xs-12 text-right">Start Date for EMI :</div>--%>
                                  <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                                      <asp:TextBox ID="txt_loandate" runat="server" class="form-control date-EMI" Visible="false" meta:resourceKey="txt_loandateResource1" Width="140"></asp:TextBox>
                                  </div>
                              </div>

                          </div>
                          <div id="menu11">
                              <br />
                              <div class="row">
                                  <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12 text-right"><b>Key Responsibility Area :</b></div>
                                  <div class="col-lg-6 col-md-6 col-sm- col-xs-12">
                                      <asp:TextBox ID="txt_kra" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="6" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                  </div>
                              </div>
                          </div>
                          <div id="menu12">
                              <br />
                              <asp:Panel ID="Panel11" runat="server" CssClass="panel panel-default">
                                  <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                      <ContentTemplate>
                                          <div class="container">
                                              <br />

                                              <div class="row">
                                                  <div class="col-sm-2 col-xs-12">
                                                     <b> Select Designation :</b>
                            <asp:DropDownList ID="select_designation" runat="server" class="form-control text-box" OnSelectedIndexChanged="select_designation_SelectedIndexedChanged" AutoPostBack="true">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Housekeeping</asp:ListItem>
                                <asp:ListItem Value="2">Security_Guard</asp:ListItem>
                                <asp:ListItem Value="3">Common</asp:ListItem>

                            </asp:DropDownList>
                                                  </div>
                                                  <div class="col-sm-2 col-xs-12">
                                                    <b>  Select Type :</b>
                            <asp:DropDownList ID="ddl_product_type" runat="server" class="form-control text-box" OnSelectedIndexChanged="ddl_product_type_SelectedIndexedChanged" AutoPostBack="true">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Uniform</asp:ListItem>
                                <asp:ListItem Value="2">Shoes</asp:ListItem>
                                <asp:ListItem Value="3">Sweater</asp:ListItem>
                                <asp:ListItem Value="4">ID_Card</asp:ListItem>
                                <asp:ListItem Value="5">Raincoat </asp:ListItem>
                                <asp:ListItem Value="6">Torch</asp:ListItem>
                                <asp:ListItem Value="7">Whistle</asp:ListItem>
                                <asp:ListItem Value="8">Baton</asp:ListItem>
                                <asp:ListItem Value="9">Belt</asp:ListItem>
                                <asp:ListItem Value="10">Pantry_Jacket</asp:ListItem>
                                <asp:ListItem Value="11">Apron</asp:ListItem>



                            </asp:DropDownList>
                                                  </div>
                                                  <div class="col-sm-2 col-xs-12">
                                                      <span class="text-left"><b>Number Of Set :</b></span>
                                                      <asp:DropDownList ID="ddl_uniformset" runat="server" class="form-control text-box">
                                                          <asp:ListItem Value="0">Select</asp:ListItem>
                                                          <asp:ListItem Value="1">0</asp:ListItem>
                                                          <asp:ListItem Value="2">1</asp:ListItem>
                                                          <asp:ListItem Value="3">2</asp:ListItem>

                                                      </asp:DropDownList>
                                                  </div>
                                                  <div class="col-sm-1 col-xs-12">
                                                     <b> Size:</b>
                                 
                                                    <%--<asp:TextBox ID="uniform_size" runat="server" class="form-control text-box" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>--%>
                                                      <%--<asp:Label ID="lb_uniform_size" runat="server" Text="Unifrom Size :"></asp:Label><asp:Label ID="lb_pantry_jacket_size" runat="server" Text="Pantry Jacket Size :"></asp:Label> <asp:Label ID="lb_shoes_size" runat="server" Text="Shoes Size :"></asp:Label>--%>
                                                      <asp:DropDownList ID="uniform_size" runat="server" class="form-control" OnSelectedIndexChanged="uniform_size_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                      <%-- vikas add 03/04/2019--%>
                                                      <asp:Label ID="lbl_qty" runat="server" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                                      <asp:Label ID="txt_quantity1" Text="In Stock" runat="server" Visible="false" ForeColor="Blue" Font-Size="Small"></asp:Label>
                                                  </div>

                                                  <div class="col-sm-2 col-xs-1">
                                                    <b>  Issuing Date:</b>
                                 <asp:TextBox ID="uniform_issue_date" runat="server" class="form-control date-picker1"
                                     meta:resourceKey="txt_birthdateResource1"></asp:TextBox>
                                                  </div>
                                                  <div class="col-sm-2 col-xs-12">
                                                     <b> Expiry Date:</b>
                                 <asp:TextBox ID="uniform_expiry_date" runat="server" class="form-control date-picker2"
                                     meta:resourceKey="txt_birthdateResource1"></asp:TextBox>
                                                  </div>
                                                  <div class="col-sm-1 col-xs-12">
                                                      <br />
                                                      <asp:Panel ID="Panel2" runat="server" CssClass="panel panel-default" Style="width: 36px">
                                                          <asp:LinkButton ID="lnk_zone_add" runat="server" OnClick="lnk_zone_add_Click" CausesValidation="false" OnClientClick="return req_valid();">
                                      <img alt="Add Item" src="Images/add_icon.png" />
                                                          </asp:LinkButton>
                                                      </asp:Panel>
                                                  </div>

                                              </div>
                                              <br />
                                              <div class="row">
                                                  <asp:Panel ID="Panel1" runat="server" CssClass="grid" Style="overflow-x: hidden;">

                                                      <asp:GridView ID="gv_product_details" class="table" runat="server" BackColor="White"
                                                          BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                          OnRowDataBound="gv_product_details_RowDataBound"
                                                          AutoGenerateColumns="False" OnPreRender="gv_product_details_PreRender" Width="100%">

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
                                                                      <asp:LinkButton ID="lnk_remove_product" runat="server" CausesValidation="false" OnClick="lnk_remove_product_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Sr No.">
                                                                  <ItemStyle Width="20px" />
                                                                  <ItemTemplate>
                                                                      <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>


                                                              <asp:BoundField DataField="document_type" HeaderText="Type"
                                                                  SortExpression="document_type" />
                                                              <asp:BoundField DataField="admin_no_of_set" HeaderText="Number Of Set"
                                                                  SortExpression="admin_no_of_set" />
                                                              <asp:BoundField DataField="size" HeaderText="Size"
                                                                  SortExpression="size" />
                                                              <asp:BoundField DataField="start_date" HeaderText="Issue Date"
                                                                  SortExpression="start_date" />
                                                              <asp:BoundField DataField="end_date" HeaderText="Expiry Date"
                                                                  SortExpression="end_date" />
                                                              <asp:BoundField DataField="remaining_no_set" HeaderText="Remainig Number Of Set"
                                                                  SortExpression="remaining_no_set" />

                                                          </Columns>
                                                      </asp:GridView>

                                                  </asp:Panel>

                                              </div>
                                          </div>
                                      </ContentTemplate>
                                  </asp:UpdatePanel>
                              </asp:Panel>

                          </div>


                            <div id="menu13">
                               
                                <asp:Panel ID="Panel_dublicate" runat="server" CssClass="panel panel-default">

                                          
                                          <div class="container">
                                              <br />

                                              <div class="row">

                            <div class="col-sm-2 col-xs-12">
                                                      <span class="text-left"><b>Number Of Set :</b></span>
                                                      <asp:DropDownList ID="ddl_id_set_dublicate" runat="server" class="form-control text-box">
                                                          <asp:ListItem Value="0">Select</asp:ListItem>
                                                          <asp:ListItem Value="1">1</asp:ListItem>
                                                        

                                                      </asp:DropDownList>
                                                  </div>

                                                   

                                    <div class="col-sm-2 col-xs-1">
                                                     <b> Issuing Date:</b>
                                 <asp:TextBox ID="txt_from_date" runat="server" class="form-control date-picker_du1"
                                     meta:resourceKey="txt_birthdateResource1"></asp:TextBox>
                                                  </div>

                                                  <div class="col-sm-2 col-xs-12">
                                                    <b>  Expiry Date:</b>
                                 <asp:TextBox ID="txt_to_date" runat="server" class="form-control date-picker_du2"
                                     meta:resourceKey="txt_birthdateResource1"></asp:TextBox>
                                                  </div>

                                                    <div class="col-sm-1 col-xs-12">
                                                      <br />
                                                      <asp:Panel ID="Panel_du" runat="server" CssClass="panel panel-default" Style="width: 36px">
                                                          <asp:LinkButton ID="lnk_dublicate_id" runat="server" OnClick="lnk_dublicate_id_Click" CausesValidation="false" OnClientClick="return validation_dublicate_id();">
                                      <img alt="Add Item" src="Images/add_icon.png" />
                                                          </asp:LinkButton>
                                                      </asp:Panel>
                                                  </div>



                                                  <%--  <div class="col-sm-1 col-xs-12">
                                                      <br />
                                                      <asp:Panel ID="Panel_du" runat="server" CssClass="panel panel-default" Style="width: 36px">
                                                          <asp:LinkButton ID="lnk_dublicate_id" runat="server"  OnClick="lnk_dublicate_id_Click"  CausesValidation="false" OnClientClick="return ">
                                      <img alt="Add Item" src="Images/add_icon.png" />
                                                          </asp:LinkButton>
                                                      </asp:Panel>
                                                  </div>--%>

                                                  </div>

                                                   <div class="row">
                                                  <asp:Panel ID="Panel12" runat="server" CssClass="grid" Style="overflow-x: hidden;">

                                                      <asp:GridView ID="gv_dublicate_id_card" class="table" runat="server" BackColor="White"
                                                          BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                          OnRowDataBound="gv_dublicate_id_card_RowDataBound"
                                                          AutoGenerateColumns="False" OnPreRender="gv_dublicate_id_card_PreRender" Width="100%">

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
                                                                      <asp:LinkButton ID="lnk_remove_product" runat="server" CausesValidation="false" OnClick="lnk_remove_product_Click1"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Sr No.">
                                                                  <ItemStyle Width="20px" />
                                                                  <ItemTemplate>
                                                                      <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>

                                                              <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
                                                              <asp:BoundField DataField="admin_id_set" HeaderText="Number Of Set"
                                                                  SortExpression="admin_id_set" />

                                                              <asp:BoundField DataField="from_date" HeaderText="ISSUE DATE"
                                                                  SortExpression="from_date" />

                                                              <asp:BoundField DataField="to_date" HeaderText="EXPIRY DATE"
                                                                  SortExpression="to_date" />
                                                            

                                                          </Columns>
                                                      </asp:GridView>

                                                  </asp:Panel>

                                              </div>
                           

                                                   
                                              <br />
                                                    </div>
                                              
                                      </asp:Panel>
                           </div>




                          
                      </div>


                      <br />

                      <div class="row text-center">
                          <asp:Button ID="btn_add" runat="server" Text="Save" OnClientClick="return update_validation1();" class="btn btn-primary"
                              OnClick="btn_add_Click" />
                          <asp:Button ID="btn_approve" runat="server" Text="Approve" class="btn btn-primary"
                              OnClick="btn_approve_Click" OnClientClick="return update_validation2();" />

                          <asp:Button ID="btnupdate"
                              runat="server" class="btn btn-primary"
                              Text="Update" OnClientClick="return update_validation();" OnClick="btnupdate_Click" />

                          <asp:Button ID="btn_left" runat="server" CssClass="btn btn-primary" Text="Left" OnClick="btn_left_Click"  OnClientClick="return Left_button();"/>

                          <asp:Button ID="btndelete"
                              runat="server" class="btn btn-primary"
                              OnClick="btndelete_Click" Text="Delete" CausesValidation="False" Visible="false" Style="display: none;"
                              meta:resourcekey="btndeleteResource1" OnClientClick="return Rqvd_validation()" />

                          <%-- <asp:Button ID="btnexcelexport" runat="server" class="btn btn-primary"
                            OnClick="btnexcelexport_Click" Text="Export To Excel"
                            CausesValidation="False" meta:resourcekey="btnexcelexportResource1" />--%>

                           <asp:Button ID="btn_releiving_letter"
                              runat="server" class="btn btn-primary"
                              Text="Send Relieving Letter" OnClick="btn_releiving_letter_Click" />

                          <asp:Button
                              ID="btncloselow" runat="server" class="btn btn-danger" OnClick="btnclose_Click"
                              Text="Close" CausesValidation="False"
                              meta:resourcekey="btncloseResource1" />

                          <%--   <asp:LinkButton ID="lnkDownload" Text="Download Employee Document" runat="server" OnClick="download_document"></asp:LinkButton>--%>
                      </div>
                  </asp:Panel>

                <asp:Panel ID="pln_searchemp" runat="server" meta:resourcekey="Panel8Resource1" for="ex2">
                    <asp:Panel ID="panel4" runat="server" Visible="false" Width="100%">

                        <table border="1" class="table table-striped text-left" style="width: 20%; float: right; border: 1px solid antiquewhite;">
                            <tr>
                                <th style="text-align: left"><a data-toggle="modal" data-target="#employee_count"><b style="color: red"><li><%=employee_list%></b>Employee Count</li></a></th>
                            </tr>
                            <tr>
                                <th style="text-align: left"><a data-toggle="modal" data-target="#left_emp_count"><b style="color: red"><li><%=left_emp_count%></b>Expired ID Uniform Shoes of Employee</li></a></th>
                            </tr>
                            <tr>
                                <th style="text-align: left"><a data-toggle="modal" data-target="#unifom_hold_count"><b style="color: red"><li><%=unifom_hold_count%></b>ID Unifom Shoes Hold Employee</li></a> </th>
                            </tr>
                            <tr>
                                <th style="text-align: left"><a data-toggle="modal" href="#vakant_branch"><font color="red"><b><li><%= branch_list %></b></font>Vacant Branch </li></a></th>
                            </tr>
                        </table>

                    </asp:Panel>
                    <br />
                     <asp:Panel ID="panel9" runat="server">
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                          <b>  Select Client :</b>
                        <asp:DropDownList ID="ddlunitclient1" runat="server" DataValueField="client_Code" DataTextField="client_name" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlunitclient1_SelectedIndexChanged" />
                        </div>

                        <div class="col-sm-2 col-xs-12">
                           <b> Select State :</b>
                        <asp:DropDownList ID="ddl_gv_statewise" runat="server" DataValueField="STATE_CODE" DataTextField="STATE_NAME" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlsatewises_SelectedIndexChanged" />
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Select Branch :</b>
                        <asp:DropDownList ID="ddl_gv_branchwise" runat="server" DataValueField="UNIT_CODE" DataTextField="UNIT_NAME" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlbeabchwise1_SelectedIndexChanged" />
                        </div>
                    </div>
                          </asp:Panel>

                    <br />

                    <asp:Panel ID="panel_dispatch" runat="server">
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                           <b> Select Client Dispatch :</b>
                        <asp:DropDownList ID="ddl_client_name_dispatch" runat="server" DataValueField="client_Code" DataTextField="client_name" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_client_name_dispatch_SelectedIndexChanged" />
                        </div>

                        <div class="col-sm-2 col-xs-12">
                           <b> Select State Dispatch :</b>
                        <asp:DropDownList ID="ddl_state_name_dispatch" runat="server" DataValueField="STATE_CODE" DataTextField="STATE_NAME" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_state_name_dispatch_SelectedIndexChanged" />
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Select Branch Dispatch :</b>
                        <asp:DropDownList ID="ddl_branch_name_dispatch" runat="server" DataValueField="UNIT_CODE" DataTextField="UNIT_NAME" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_branch_name_dispatch_SelectedIndexChanged" />
                        </div>
                    </div>
                         </asp:Panel>
                    <%--///////////////////////////////////////////--%>
                 <br />
                    <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white; padding:20px 20px 20px 20px; margin-left:-10px; margin-right:-10px">
                    <asp:Panel ID="Panel5" runat="server" BackColor="#f3f1fe" Visible="False" meta:resourcekey="Panel5Resource1" CssClass="grid-view" Style="overflow-x:hidden">
                        <asp:GridView ID="SearchGridView" class="table" AutoGenerateColumns="False" runat="server"
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="SearchGridView_RowDataBound"
                            CellPadding="3" meta:resourcekey="SearchGridViewResource1"
                            OnSelectedIndexChanged="SearchGridView_SelectedIndexChanged" OnPreRender="gv_expeness_PreRender">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                           <%-- <RowStyle ForeColor="#000066" BackColor="#ffffff" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />--%>
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                            <Columns>
                                <asp:BoundField DataField="EMP_CODE" HeaderText="EMP CODE" SortExpression="EMP_CODE" />
                                <asp:BoundField DataField="ihmscode" HeaderText="IHMS ID" SortExpression="ihmscode" />
                                <asp:BoundField DataField="EMP_NAME" HeaderText="NAME" SortExpression="EMP_NAME" />
                                <asp:BoundField DataField="GRADE_DESC" HeaderText="DESIGNATION" SortExpression="GRADE_DESC" />
                                <asp:BoundField DataField="CLIENT_NAME" HeaderText="CLIENT NAME" SortExpression="CLIENT_NAME" />
                                <asp:BoundField DataField="client_wise_state" HeaderText="State" SortExpression="client_wise_state" />
                                <asp:BoundField DataField="UNIT_NAME" HeaderText="UNIT NAME" SortExpression="UNIT_NAME" />
                                <asp:BoundField DataField="employee_type" HeaderText="TYPE" SortExpression="employee_type" />
                                <asp:BoundField DataField="EMP_MOBILE_NO" HeaderText="MOBILE NO." SortExpression="EMP_MOBILE_NO" />
                                <asp:BoundField DataField="BIRTH_DATE" HeaderText="BIRTH DATE" SortExpression="BIRTH_DATE" />
                                <asp:BoundField DataField="JOINING_DATE" HeaderText="JOINING DATE" SortExpression="JOINING_DATE" />
                            </Columns>

                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <%--<RowStyle ForeColor="#000066" BackColor="#A1DCF2" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />--%>
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                    </asp:Panel>
                       <%-- </div>--%>

                </asp:Panel>


                <%--   /////////09-10-19--%>

                    <%--<div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white; padding:20px 20px 20px 20px; margin-left:-10px; margin-right:-10px">--%>
                    <asp:Panel ID="Panel8" runat="server" BackColor="#f3f1fe" Visible="true" meta:resourcekey="Panel5Resource1" CssClass="grid-view" Style="overflow-x:hidden">
                        <asp:GridView ID="gv_dispatch_details" class="table" AutoGenerateColumns="False" runat="server" OnPreRender="gv_dispatch_details_PreRender" OnRowDataBound="gv_dispatch_details_RowDataBound"
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                            CellPadding="3" meta:resourcekey="SearchGridViewResource1"
                        >
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

                                <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_srnumber" runat="server"
                                                Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" />
                                <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                <asp:BoundField DataField="state_dispatch" HeaderText="State Name" SortExpression="state_dispatch" />
                                <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                                <asp:BoundField DataField="designation" HeaderText="Designation" SortExpression="designation" />
                                <asp:BoundField DataField="dispatch_date" HeaderText="Dispatch Date" SortExpression="dispatch_date" />
                                <asp:BoundField DataField="receiving_date" HeaderText="Receiving Date" SortExpression="receiving_date" />
                                <asp:BoundField DataField="shipping_address" HeaderText="Shipping Address" SortExpression="shipping_address" />
                                <asp:BoundField DataField="Uniform" HeaderText="Uniform Size" SortExpression="Uniform" />
                                <asp:BoundField DataField="Set_Uniform" HeaderText="Uniform Set" SortExpression="Set_Uniform" />
                                 <asp:BoundField DataField="Shoes" HeaderText="Shoes Size" SortExpression="Shoes" />
                                 <asp:BoundField DataField="ID_Card" HeaderText="ID Card" SortExpression="ID_Card" />
                                  <asp:BoundField DataField="pod_no" HeaderText="POD NO" SortExpression="pod_no" />
                                
                                
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
                       <%-- </div>--%>




                   <%-- /////////////////////////--%>


            </div>
                 </div>

            <div class="panel-body">


                <asp:Panel ID="panel_approval" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                               <b> Select Client :</b>
                        <asp:DropDownList ID="ddl_app_client" runat="server" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_app_client_SelectedIndexChanged" />
                            </div>

                            <div class="col-sm-2 col-xs-12">
                                <b>Select State :</b>
                        <asp:DropDownList ID="ddl_app_state" runat="server" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_app_state_SelectedIndexChanged" />
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>Select Branch :</b>
                        <asp:DropDownList ID="ddl_app_unit" runat="server" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_app_unit_SelectedIndexChanged" />
                            </div>

                            <div class="col-sm-2 col-xs-12">
                                <b>Select Employee :</b>
                        <asp:DropDownList ID="ddl_app_emp" runat="server" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_app_emp_SelectedIndexChanged" />
                            </div>
                            <div class="col-sm-1 col-xs-12"></div>
                            <asp:Panel ID="panel_link" runat="server">
                                <div class="col-sm-3 col-xs-12 text-left" style="margin-top: -43px; margin-left: 104px;">

                                    <table border="1" class="table table-striped" style="border-color: #e6dede">
                                        <tr>
                                            <th><a data-toggle="modal" href="#remain_admin"><font color="red"><b><%= rem_emp_count %></b></font>Employee Not Approve By Admin </a></th>
                                        </tr>
                                        <tr>
                                            <th><a data-toggle="modal" href="#approve_admin"><font color="red"><b><%= appro_emp_count %></b></font>Employee Approve  By Admin </a></th>
                                        </tr>
                                        <tr>
                                            <th><a data-toggle="modal" href="#approve_legal"><font color="red"><b><%= appro_emp_legal %></b></font>Employee  Approve By Legal </a></th>
                                        </tr>
                                        <tr>
                                            <th><a data-toggle="modal" href="#reject_leagal"><font color="red"><b><%= reject_emp_legal %></b></font>Employee Reject  By Legal </a></th>
                                        </tr>
                                          <tr>
                                            <th><a data-toggle="modal" href="#approve_bank"><font color="red"><b><%= appro_emp_bank %></b></font>Bank Details Approve By Fiance </a></th>
                                        </tr>
                                          <tr>
                                            <th><a data-toggle="modal" href="#rejected_bank_emp"><font color="red"><b><%= rejected_bank_emp %></b></font>Bank Details Reject By Fiance </a></th>
                                        </tr>

                                    </table>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <br />





            <div class="modal fade" id="remain_admin" role="dialog" data-dismiss="modal" href="#lost">

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

            </div>
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
            <div class="modal fade" id="approve_legal" role="dialog" data-dismiss="modal" href="#lost">

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 style="text-align: center">Employee  Approve By Legal</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12" style="padding-left: 1%;">
                                    <asp:GridView ID="gv_appro_emp_legal" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
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
            <div class="modal fade" id="reject_leagal" role="dialog" data-dismiss="modal" href="#lost">

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 style="text-align: center">Employee Reject  By Legal</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12" style="padding-left: 1%;">
                                    <asp:GridView ID="gv_reject_emp_legal" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                        <Columns>
                                            <asp:BoundField DataField="Client_Name" HeaderText="Client Name" SortExpression="Client_Name" />
                                            <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                            <asp:BoundField DataField="branch_name" HeaderText="Branch Name" SortExpression="branch_name" />
                                            <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                                            <asp:BoundField DataField="reject_reason" HeaderText="Reject Reason" SortExpression="reject_reason" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
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
                
            <div class="modal fade" id="vakant_branch" role="dialog" data-dismiss="modal" href="#lost">

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 style="text-align: center">Branch List</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12" style="padding-left: 1%;">
                                    <asp:GridView ID="gv_branch_list" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                        <Columns>
                                            <asp:BoundField DataField="Client_Name" HeaderText="Client Name" SortExpression="Client_Name" />
                                            <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                            <asp:BoundField DataField="branch_name" HeaderText="Branch Name" SortExpression="branch_name" />
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
        </asp:Panel>
        
        <asp:Panel ID="Panel_app_gv" runat="server" BackColor="White" Visible="False" meta:resourcekey="Panel5Resource1">
            <asp:GridView ID="gv_app_gridview" class="table" AutoGenerateColumns="False" runat="server"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="gv_app_gridview_RowDataBound"
                CellPadding="3" meta:resourcekey="SearchGridViewResource1"
                OnSelectedIndexChanged="gv_app_gridview_SelectedIndexChanged" OnPreRender="gv_app_gridview_PreRender">
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

                    <asp:BoundField DataField="emp_code" HeaderText="EMP CODE" SortExpression="emp_code" />
                    <asp:BoundField DataField="client_name" HeaderText="CLIENT NAME" SortExpression="client_name" />
                    <asp:BoundField DataField="state_name" HeaderText="STATE NAME" SortExpression="state_name" />
                    <asp:BoundField DataField="branch_name" HeaderText="UNIT NAME" SortExpression="branch_name" />
                    <asp:BoundField DataField="emp_name" HeaderText="EMP NAME" SortExpression="emp_name" />
                     <asp:BoundField DataField="Grade_code" HeaderText="DESIGNATION" SortExpression="Grade_code" />
                    <asp:BoundField DataField="joining_date" HeaderText="DOJ" SortExpression="joining_date" />
                    <asp:BoundField DataField="status" HeaderText="STATUS" SortExpression="status" />

                    <asp:BoundField DataField="Reason" HeaderText="REJECTED REASON" SortExpression="JOINING_DATE" />
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
    </div>
       
        <asp:Button ID="Button2" runat="server" CssClass="hidden" Text="Claim Expense" />
                    <asp:Button ID="Button3" runat="server" CssClass="hidden" Text="Add New Travelling Plan" />
                    <asp:Button ID="Button5" runat="server" CssClass="hidden" OnClick="Button4_Click" />

                        <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel10" TargetControlID="Button3"
                        CancelControlID="Button4" BackgroundCssClass="Background">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panel10" runat="server" CssClass="Popup" Style="display: none">
                        <iframe style="width: 1000px; height: 500px; background-color: #fff;" id="Iframe1" src="p_upload_employee.aspx" runat="server"></iframe>
                        <div class="row text-center">
                            <asp:Button ID="Button4" CssClass="btn btn-danger" OnClientClick="callfnc()" runat="server" Text="Close" />
                        </div>

                        <br />
                       
                    </asp:Panel>
    </div>
    
    
</asp:Content>

