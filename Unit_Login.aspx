<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Unit_Login.aspx.cs" Inherits="Unit_Login" Title="CUSTOMER LOGIN" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>

    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta charset="utf-8" />

    <!-- Contain the script binding the form submit event -->


    <link href="css/style.css" rel="stylesheet" />

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
    <%--<script src="datatable/pdfmake.min.js"></script>--%>

    <style>
        .nav-pills > li.active > a, .nav-pills > li.active > a:focus, .nav-pills > li.active > a:hover {
            color: none;
        }

        .circle {
            width: 40px;
            height: 40px;
            border-radius: 50px;
            background-color: #45619D;
            margin-top: 5px;
            cursor: pointer;
        }

        .img_circle {
            width: 40px;
            height: 40px;
            border-radius: 50px;
            cursor: pointer;
        }

        #notification_li {
            position: relative;
        }

        #notificationContainer {
            background-color: #fff;
            border: 1px solid rgba(100, 100, 100, .4);
            -webkit-box-shadow: 0 3px 8px rgba(0, 0, 0, .25);
            overflow: visible;
            position: absolute;
            top: 30px;
            z-index: 1100;
            margin-left: -170px;
            width: 400px;
            display: none;
        }


        #notificationTitle {
            font-weight: bold;
            padding: 8px;
            font-size: 13px;
            background-color: #ffffff;
            position: fixed;
            z-index: 1000;
            width: 384px;
            border-bottom: 1px solid #dddddd;
            
        }

        #notificationsBody {
            padding: 33px 0px 0px 0px !important;
            min-height: 300px;
        }

        #notificationFooter {
            background-color: #e9eaed;
            text-align: center;
            font-weight: bold;
            padding: 8px;
            font-size: 12px;
            border-top: 1px solid #dddddd;
        }
        /*notification count*/
        #notification_count {
            padding: 3px 7px 3px 7px;
            background: #cc0000;
            color: #ffffff;
            font-weight: bold;
            border-radius: 9px;
            -moz-border-radius: 9px;
            -webkit-border-radius: 9px;
            position: absolute;
            margin-top: -4px;
            font-size: 11px;
        }

        body {
            font-family: Verdana;
            font-size: 10px;
        }

        .row {
            margin-right: -15px;
            margin-left: -15px;
        }
    </style>
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        $(function () {

            $("#dialog").dialog({

                autoOpen: false,
                modal: true,
                height: 500,
                width: 500,
                title: "Zoomed Image",
                buttons: [{ text: "Close", click: function () { $(this).dialog("close") } }],
            });
            $("[id*=Camera_Image1]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(316).height(252));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=Camera_Image2]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(316).height(252));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
           
        });
        $(function () {


            var table = $('#<%=GradeGridView.ClientID%>').DataTable({
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
            .appendTo('#<%=GradeGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

                    

            $.fn.dataTable.ext.errMode = 'none';

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=unitcomplaintGridView.ClientID%>').DataTable({
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
               .appendTo('#<%=unitcomplaintGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

        });


    </script>


    <style>
        .container {
            max-width: 99%;
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
            max-height: 450px;
            overflow-y: auto;
            overflow-x: hidden;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }

        .row {
            margin: 0px;
        }



        .modal {
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            background-color: black;
            z-index: 100;
            opacity: 0.8;
            filter: alpha(opacity=60);
            -moz-opacity: 0.8;
            min-height: 100%;
        }

        #divImage {
            display: none;
            z-index: 1000;
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
            height: 550px;
            width: 600px;
            padding: 3px;
            border: solid 1px black;
        }
    </style>
    <script>

        $(document).ready(function () {

            $("#notificationLink").click(function () {
                $("#notificationContainer").fadeToggle(300);
                $("#notification_count").fadeOut("slow");
                return false;
            });

            //Document Click hiding the popup 
            $(document).click(function () {
                $("#notificationContainer").hide();
            });

            //Popup on click
            $("#notificationContainer").click(function () {
                return false;
            });


            $("#notificationLink2").click(function () {
                $("#notificationContainer2").fadeToggle(300);
                $("#notification_count2").fadeOut("slow");
                return false;
            });

            //Document Click hiding the popup 
            $(document).click(function () {
                $("#notificationContainer2").hide();
            });

            //Popup on click
            $("#notificationContainer2").click(function () {
                return false;
            });

        });

    </script>

    <script type="text/javascript">


        $(function () {
          
            $("[id*=Image4]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(316).height(252));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=Image2]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(316).height(252));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=Image14]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(316).height(252));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
        });



        $(document).ready(function () {

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_unit_attendance.ClientID%>').DataTable({
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
                   .appendTo('#<%=gv_unit_attendance.ClientID%>_wrapper .col-sm-6:eq(0)');


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

                $.fn.dataTable.ext.errMode = 'none';

            });





    </script>

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

        .lable {
            margin-right:20px;
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

        li {
            font-size: 11px;
        }

        .table {
            font-size: 11px;
        }

        .grid {
            overflow-x: hidden;
            overflow-y: auto;
            height: auto;
            max-height: 300px;
        }

        .nav > li > a:focus, .nav > li > a:hover {
            text-decoration: none;
            background-color: #337ab7;
        }

        .block {
            height: 100px;
            width: 300px;
            overflow: auto;
        }

        .firma-ara {
            padding-bottom: 100px;
            padding-top: 100px;
        }

        .form-arka-plan {
            background-image: url("https://cdn.filepicker.io/api/file/1WxRtkAQG5h70aoPQdGA/convert?format=jpeg&quality=50");
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
        }

        .acik-renk-form {
            background: rgba(255, 255, 255, 0.58);
        }

        .siyah-cerceve {
            -webkit-text-fill-color: white;
            -webkit-text-stroke-width: 1px;
            -webkit-text-stroke-color: black;
        }
    </style>

    <script>


        $(function () {
            $('body').on('keyup', '.maskedExt', function () {
                var num = $(this).attr("maskedFormat").toString().split(',');
                var regex = new RegExp("^\\d{0," + num[0] + "}(\\.\\d{0," + num[1] + "})?$");
                if (!regex.test(this.value)) {
                    this.value = this.value.substring(0, this.value.length - 1);
                }
            });
        });

    </script>
    <script type="text/javascript">
        function create() {
            var s = "";

            s += '<input type="text" name="Fname">'; //Create one textbox as HTML

            document.getElementById("screens").innerHTML = s;
        }


        window.onfocus = function () {
            $.unblockUI();

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
        window.onfocus = function () {
            $.unblockUI();
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
        function readonly_ddl() {
            var ddlunitselect = document.getElementById('<%=ddlunitselect.ClientID %>');
            ddlunitselect.disabled = true;

        }

        function validation() {
            var txt_services = document.getElementById('<%=ddl_asset_type.ClientID %>');
                var Selected_txt_services = txt_services.options[txt_services.selectedIndex].text;
                var txt_priority = document.getElementById('<%=department_asset.ClientID %>');
                var Selected_txt_priorty = txt_priority.options[txt_priority.selectedIndex].text;
                var assitional_comment = document.getElementById('<%=txt_asset_description.ClientID%>');

                if (Selected_txt_services == "Select") {
                    alert("Please Select Services !!!");
                    txt_services.focus();
                    return false;
                }

                if (Selected_txt_priorty == "Select") {
                    alert("Please Select Priority  !!!");
                    txt_priority.focus();
                    return false;
                }
                if (assitional_comment.value == "") {
                    alert("Please Enter Additional Comment !!!");
                    assitional_comment.focus();
                    return false;
                }
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
        }
        
        function feedback_validations()
        {

            var txt_feedback_date = document.getElementById('<%=txt_feedback_date.ClientID %>');
            if (txt_feedback_date.value == "") {
                alert("Please Select Feedback Month");
                txt_feedback_date.focus();
                return false;
            }
        }



            function r_validation() {
                var txt_work_img_from = document.getElementById('<%=txt_work_img_from.ClientID %>');
            var txt_work_img_to = document.getElementById('<%=txt_work_img_to.ClientID %>');
            if (txt_work_img_from.value == "") {
                alert("Please Enter From Date");
                txt_work_img_from.focus();
                return false;
            }
            if (txt_work_img_to.value == "") {
                alert("Please Enter To Date");
                txt_work_img_to.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function rq_validation() {
            var txt_satrtdate = document.getElementById('<%=txt_satrtdate.ClientID %>');
            var txt_enddate = document.getElementById('<%=txt_enddate.ClientID %>');
            if (txt_satrtdate.value == "") {
                alert("Please Enter From Date");
                txt_satrtdate.focus();
                return false;
            }
            if (txt_enddate.value == "") {
                alert("Please Enter To Date");
                txt_enddate.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function re_validation() {
            var txt_month_year = document.getElementById('<%=txt_month_year.ClientID %>');
            if (txt_month_year.value == "") {
                alert("Please Select Month");
                txt_month_year.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function require_validation() {
            var ddl_employee = document.getElementById('<%=ddl_employee.ClientID %>');
            var Selected_ddl_employee = ddl_employee.options[ddl_employee.selectedIndex].text;
            if (Selected_ddl_employee == "Select") {

                alert("Please Select Employee");
                ddl_employee.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function copy_add() {
            var r1 = document.getElementById("<%= RadioButton1.ClientID %>");
            var r2 = document.getElementById("<%= RadioButton2.ClientID %>");
            var r3 = document.getElementById("<%= RadioButton3.ClientID %>");
            var r4 = document.getElementById("<%= RadioButton4.ClientID %>");
            var r5 = document.getElementById("<%= RadioButton5.ClientID %>");
            var r6 = document.getElementById("<%= RadioButton6.ClientID %>");
            var r7 = document.getElementById("<%= RadioButton7.ClientID %>");
            var r8 = document.getElementById("<%= RadioButton8.ClientID %>");
            var r9 = document.getElementById("<%= RadioButton9.ClientID %>");
            var r10 = document.getElementById("<%= RadioButton10.ClientID %>");

            var txtfeed = document.getElementById('<%=txtfeed.ClientID %>');
            if (r1.checked == true || r2.checked == true || r3.checked == true || r4.checked == true || r5.checked == true) {
                if (txtfeed.value == "") {
                    alert("Please Enter Sugession");
                    txtfeed.focus();
                    return false;
                }
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }
        }
        function R_validation() {

            var r = confirm("Are you Sure You Want to Approve Record");
            if (r == true) {



            }
            else {
                alert("Record not Available");
            }
            return r;
        }
        function fun() {
            var txt_remark = document.getElementById('<%=txt_remark.ClientID %>');
            if (txt_remark.value == "") {
                alert("Please Enter Remark");
                txt_remark.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function Confirm() {
            var r = confirm("Are you sure,You want to complete ?");
            if (r == true) {
                return false;


            }
            else {
                return true;
            }

        }
        function pageLoad() {

            employee_groom_percent();

            $('#<%=btn_tabattendances.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=GridView_emloyee_tracking_unit.ClientID%>').DataTable({
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
                .appendTo('#<%=GridView_emloyee_tracking_unit.ClientID%>_wrapper .col-sm-6:eq(0)');

            ///// fire extinguisher
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
            var table = $('#<%=gv_emp_attendance.ClientID%>').DataTable({
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
                  .appendTo('#<%=gv_emp_attendance.ClientID%>_wrapper .col-sm-6:eq(0)');
            $("[id*=intime_imgpath]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(397));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=outtime_imgpath]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(397));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $(".date-picker3").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    $(".date-picker4").datepicker("option", "minDate", selected)
                }
            });

            $(".date-picker4").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
                onSelect: function (selected) {
                    $(".date-picker3").datepicker("option", "maxDate", selected)
                }
            });


            $(".date-picker3").attr("readonly", "true");
            $(".date-picker4").attr("readonly", "true");

            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
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
            $(".date-picker11").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
              
            });

            $(".date-picker12").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
               
               
            });


            $(".date-picker11").attr("readonly", "true");
            $(".date-picker12").attr("readonly", "true");
            $('.date-picker').datepicker({
                beforeShow: function (input, inst) {
                    setTimeout(function () {
                        inst.dpDiv.css({
                            top: $(".date-picker").offset().top + 30,
                            left: $(".date-picker").offset().left,
                        });
                    }, 0);
                },
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'mm/yy',
                onClose: function (dateText, inst) {
                    $(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, 1));
                }
            }).focus(function () {
                $(".ui-datepicker-calendar").hide();

            });
            $(".date-picker").attr("readonly", "true");

            $('.date-picker13').datepicker({
                beforeShow: function (input, inst) {
                    setTimeout(function () {
                        inst.dpDiv.css({
                            top: $(".date-picker13").offset().top + 30,
                            left: $(".date-picker13").offset().left,
                        });
                    }, 0);
                },
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'mm yy',
                onClose: function (dateText, inst) {
                    $(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, 1));
                }
            }).focus(function () {
                $(".ui-datepicker-calendar").hide();

            });
            $(".date-picker13").attr("readonly", "true");

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=SearchGridView.ClientID%>').DataTable({
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
               .appendTo('#<%=SearchGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

              $.fn.dataTable.ext.errMode = 'none';
              var table = $('#<%=unitcomplaintGridView.ClientID%>').DataTable({
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
               .appendTo('#<%=unitcomplaintGridView.ClientID%>_wrapper .col-sm-6:eq(0)');
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
        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });
        function visit_rpt_val() {
            var dd1_super = document.getElementById('<%=dd1_super.ClientID %>');
            var Selected_dd1_super = dd1_super.options[dd1_super.selectedIndex].text;

            if (Selected_dd1_super == "Select") {
                alert("Please Select Supervisor Name");
                dd1_super.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function emp_curreent_location()
        {
            var txt_emp_from = document.getElementById('<%=txt_emp_from.ClientID %>');
            if (txt_emp_from.value == "") {
                alert("Please Select From Date");
                txt_emp_from.focus();
                return false;
            }
            var txt_emp_to = document.getElementById('<%=txt_emp_to.ClientID %>');
            if (txt_emp_to.value == "") {
                alert("Please Select To Date");
                txt_emp_to.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
		
		  function employee_groom_percent() {
            var ddl_employee_groom = document.getElementById('<%=ddl_employee_groom.ClientID %>');
            var selected_ddl_employee_groom = ddl_employee_groom.options[ddl_employee_groom.selectedIndex].text;
            var text1 = ddl_employee_groom.options[ddl_employee_groom.selectedIndex].value;

            var ddl_employee_hygiene = document.getElementById('<%=ddl_employee_hygiene.ClientID %>');
            var selected_ddl_employee_hygiene = ddl_employee_hygiene.options[ddl_employee_hygiene.selectedIndex].text;
            var text2 = ddl_employee_hygiene.options[ddl_employee_hygiene.selectedIndex].value;

            var ddl_employee_duty = document.getElementById('<%=ddl_employee_duty.ClientID %>');
            var selected_ddl_employee_duty = ddl_employee_duty.options[ddl_employee_duty.selectedIndex].text;
            var text3 = ddl_employee_duty.options[ddl_employee_duty.selectedIndex].value;

            var ddl_employee_behaviour = document.getElementById('<%=ddl_employee_behaviour.ClientID %>');
            var selected_ddl_employee_behaviour = ddl_employee_behaviour.options[ddl_employee_behaviour.selectedIndex].text;
            var text4 = ddl_employee_behaviour.options[ddl_employee_behaviour.selectedIndex].value;

            var ddl_employee_support = document.getElementById('<%=ddl_employee_support.ClientID %>');
            var selected_ddl_employee_support = ddl_employee_support.options[ddl_employee_support.selectedIndex].text;
            var text5 = ddl_employee_support.options[ddl_employee_support.selectedIndex].value;

            var lbl_percentage = document.getElementById('<%=lbl_percentage.ClientID %>');

            lbl_percentage.innerHTML = ((parseInt(text1) + parseInt(text2) + parseInt(text3) + parseInt(text4) + parseInt(text5)) / 5) + "%";
        }
		
        function openWindow() {

            window.open("html/Unit_Login.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');

        }
    </script>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid">

            <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary" Style="background-color: #f3f1fe; border: gray;">
                <div class="panel-heading">
                    
                    <div class="row">
                        <div class="col-sm-3">
                            <img src="Images/logo.png" style="width:150px; margin-left:-20px;" />
                        </div>
                        <div class="col-sm-5">
                            <div style="text-align: center; margin-top:20px; font-size: large;"><b>WELCOME TO IH&MS</b></div>
                        </div>
                           <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>


                        <div class="col-sm-3" style="margin-top: 5px;">
                            <div class="col-sm-2"></div>
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2"></div>
                            <div class="col-sm-2"></div>
                            <div class="col-sm-1"></div>

                            <asp:Label runat="server"  Text="Login :" Font-Bold="true" Font-Size="Small"> </asp:Label>
                        </div>
                        <div class="col-sm-1">
                            <span>
                                <asp:Label ID="txt_usernam1" BackColor="#337ab7" ForeColor="White" class="bg-primary" runat="server" CssClass="form-control" />
                            </span>
                        </div>
                        <div class="row">
                        <div class="col-sm-3 text-right" style="margin-left:100px; margin-top:20px;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                           <asp:Button runat="server" OnClick="LinkButton1_Click"  class="btn btn-primary" Text="LogOut" style="font-weight:bold" />
                            </asp:LinkButton>
                        </div>
                        </div>
                    </div>
                </div>
                <div class="panel-body">

                    <div class="card acik-renk-form" style="height: 19em; border-radius: 10px;">

                        <div class="row">
                            <br /><br />
                            <div class="col-sm-2 col-xs-12"></div>
                            <div class="col-sm-2 col-xs-12">
                                <span style="font-weight: bold">Branch Name :</span>
                                <asp:DropDownList ID="ddlunitselect" runat="server" class="form-control text_box" OnSelectedIndexChanged="ddlunitselect_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>

                                    <div class="col-sm-2 col-xs-12">
                                        <span style="font-weight: bold">State :</span><%--<span class="text-red"> *</span>--%><%--<asp:DropDownList ID="ddl_state" runat="server" DataTextField="STATE_NAME" Width="100%"
                                        DataValueField="STATE_NAME" class="form-control text_box">
                                    </asp:DropDownList>--%>

                                        <asp:TextBox ID="ddl_state" runat="server" class="form-control text_box"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span style="font-weight: bold">City :</span><%--<span class="text-red"> *</span>--%><%--<asp:DropDownList ID="txtunitcity" runat="server" class="form-control text_box" Width="100%"></asp:DropDownList>--%>
                                        <asp:TextBox ID="txtunitcity" runat="server" class="form-control text_box"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span style="font-weight: bold">Location(Place/City) :</span><%--<span class="text-red"> *</span>--%><asp:TextBox ID="txtunitaddress1" runat="server" class="form-control text_box "></asp:TextBox>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <br />
                        <br />
                        <div class="row">
                            
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-2 col-xs-12"></div>
                                    <div class="col-sm-4 col-xs-12">
                                        <span style="font-weight: bold">Address(Street/Road/Lane) :</span>
                                        <asp:TextBox ID="txtunitaddress2" runat="server" TextMode="multiline" Width="100%" Rows="2" class="form-control text_box" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <%--<div class="col-sm-2 col-xs-12">
                        GSTIN No :<span class="text-red"> *</span>
                            <asp:TextBox ID="txt_gst_no" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event,this);" Width="100%" MaxLength="20"></asp:TextBox>

                    </div>--%>
                                    <div class="col-sm-2 col-xs-12">
                                        <span style="font-weight: bold">Zone :</span>
                                        <%--  <asp:DropDownList ID="txt_zone1" runat="server" DataTextField="ZONE" Width="100%"
                            DataValueField="ZONE" class="form-control text_box">
                            <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                            <asp:ListItem Value="1" Text="East">East</asp:ListItem>
                            <asp:ListItem Value="2" Text="West">West</asp:ListItem>
                            <asp:ListItem Value="3" Text="North">North</asp:ListItem>
                            <asp:ListItem Value="4" Text="South">South</asp:ListItem>
                        </asp:DropDownList>--%>
                                        <asp:TextBox ID="txt_zone1" runat="server" class="form-control text_box"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span style="font-weight: bold">Region Name :</span>
                                        <%--   <asp:DropDownList ID="txt_zone" runat="server" DataTextField="ZONE" Width="100%" DataValueField="ZONE" class="form-control text_box">
                            <asp:ListItem>Select</asp:ListItem>
                        </asp:DropDownList>--%>
                                        <asp:TextBox ID="txt_zone" runat="server" class="form-control text_box"></asp:TextBox>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <br />
                    </div>


                    <br />
                    <br />
                    <div class="row text-center">
                        <%--Please dont delete this 3 Buttons...Vinod --%>
                        <asp:Button ID="Button1" runat="server" CssClass="hidden" Text="Claim Expense" />
                        <asp:Button ID="Button2" runat="server" CssClass="hidden" Text="Add New Travelling Plan" />
                        <asp:Button ID="Button5" runat="server" CssClass="hidden" OnClick="Button4_Click" />

                        <%-- <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="Button1"
                                CancelControlID="Button2" BackgroundCssClass="Background">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
                                <iframe style="width: 800px; height: 450px; background-color: #fff;" id="irm1" src="p_add_expencess1.aspx" runat="server"></iframe>
                                <div class="row text-center" style="width: 100%;">
                                    <asp:Button ID="Button2" CssClass="btn btn-danger" OnClientClick="callfnc()" runat="server" Text="Close" />
                                </div>

                                <br />

                            </asp:Panel>--%>

                        <%--  <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2" TargetControlID="Button2"
                                CancelControlID="Button4" BackgroundCssClass="Background">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel2" runat="server" CssClass="Popup" Style="display: none">
                                <iframe style="width: 800px; height: 500px; background-color: #fff;" id="Iframe1" src="Unit_reject_reason.aspx" runat="server"></iframe>
                                <div class="row text-center">
                                    <asp:Button ID="Button4" CssClass="btn btn-danger" runat="server" Text="Close" />
                                </div>

                                <br />

                            </asp:Panel>--%>
                    </div>
                    <br />
                    <br />
                    <%--<asp:Panel runat="server" ID="tab_panel" CssClass="panel panel-primary" Style="border: none;">--%>

                        <div id="tabs" style="background: white;">
                            <%--<asp:Panel runat="server" ID="Panel1" CssClass="panel panel-primary" Style="color: white; border: gray;">--%>

                                <asp:Panel runat="server" ID="Panel13" Style=" background-color: white">

                                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />

                                    <ul style="border: none; background-color: #dddddd">
                                        <li id="tabactive5"><a href="#menu0">Attendance Photo</a></li>
                                         <li id="Li7"><a href="#menu12">Android Attendance</a></li>
                                         <li id="Li8"><a href="#menu13">Employee Current Location</a></li>
                                        <li id="Li4"><a href="#menu9">Work Photo</a></li>
                                        <li id="Li3"><a href="#menu1">Designation</a></li>
                                        <li id="tabactive11"><a href="#menu3">Heads Info</a></li>
                                        <li id="tab_attendance"><a href="#menu4">Monthly Attendance</a></li>
                                        <li id="tab_documents"><a href="#menu5">Employee Documents</a></li>
                                        <li style="display: none;" ><a href="#menu6">Feedback</a></li>
                                        <li id="Li1"><a href="#menu7">R & M</a></li>
                                        <li id="Li2"><a href="#menu8">Files</a></li>
                                        <li id="Li5"><a href="#menu10">Visit Report By FO</a></li>
                                        <li id="Li6"><a href="#menu11">Raise Complaint</a></li>
                                         <li id="Li9"><a href="#menu14">FeedBack</a></li>
                                          <li id="Li15"><a data-toggle="tab" href="#item15">Fire Extinguisher Info</a></li>
                                    </ul>
                                </asp:Panel>

                                 <div id="menu13">
                           <%--<div class="col-sm-2 col-xs-12">
                        Type :
   <asp:DropDownList ID="ddl_att_work" runat="server" CssClass="form-control">
       <asp:ListItem Text="Employee Current Location"></asp:ListItem>
   </asp:DropDownList>
                    </div>--%>
<div class="row">
                          <div class="col-sm-2 col-xs-12">
                       <b> From Date :</b>
                                        <asp:TextBox ID="txt_emp_from" runat="server" class="form-control date-picker1"></asp:TextBox>
                    </div>

                    <div class="col-sm-2 col-xs-12">
                      <b>  To Date :</b>
                                        <asp:TextBox ID="txt_emp_to" runat="server" class="form-control date-picker2"></asp:TextBox>
                    </div>
                             <div class="col-sm-2 col-xs-12">
                                 <br />
                        <asp:Button ID="btn_show_tracking" runat="server" Text="Show" OnClick="btn_show_tracking_Click" class="btn btn-primary" OnClientClick="return emp_curreent_location();"  />

                                  </div>
</div>
                                      <br />
                                     
                                     <asp:Panel runat="server" CssClass="grid-view" Width="100%">
                             <asp:GridView ID="GridView_emloyee_tracking_unit" class="table" runat="server" Font-Size="X-Small" Width="100%"
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" OnRowDataBound="GridView_emloyee_tracking_unit_RowDataBound"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="GridView_emloyee_tracking_unit_SelectedIndexChanged" AutoPostBack="true"  OnPreRender="GridView_emloyee_tracking_unit_PreRender">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr No.">
                                <ItemStyle Width="20px" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                              
                             <asp:BoundField HeaderText="ID" DataField="id" SortExpression="id" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
                            <asp:BoundField HeaderText="Emp-Name" DataField="emp_code" SortExpression="emp_code" />
                            <asp:BoundField HeaderText="Current-Latitude" DataField="cur_latitude" SortExpression="cur_latitude" />
                            <asp:BoundField HeaderText="Current-Longitude" DataField="cur_longtitude" SortExpression="cur_longtitude" />
                           <asp:BoundField HeaderText="Current-Date" DataField="cur_date" SortExpression="cur_date" />
                            <asp:BoundField HeaderText="Address" DataField="cur_address" SortExpression="cur_address" /> 
                           
                            
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                                         </asp:Panel>
                                     <br />
                                      </div>
                                     
                                   <div id="menu12">
                                       <asp:UpdatePanel runat="server" UpdateMode="Conditional"><ContentTemplate>
                                        <div class="row">
                        <div id="Div1"></div>
                        <div class="col-sm-2 col-xs-12">
                           <b> From Date:</b><span style="color: red">*</span>
                            <asp:TextBox runat="server" ID="txt_fromdate" CssClass="form-control date-picker11"></asp:TextBox>
                        </div>
                                            <div class="col-sm-2 col-xs-12">
                         <b>  To Date:</b><span style="color: red">*</span>
                            <asp:TextBox runat="server" ID="txt_todate" CssClass="form-control date-picker12"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12" style="margin-top: 1.5em;">
                            <asp:Button ID="btn_tabattendances" runat="server" CssClass="btn btn-primary" Text="Show" OnClientClick="return Req_validation();" OnClick="tab_attendances_click"></asp:Button>
                           <%-- <asp:Button ID="Button14" runat="server"  CssClass="btn btn-danger" Text="Close"></asp:Button>--%>
                        </div>
                    </div>
                          <br />
                                       <div >
                            <asp:Panel ID="Panel2" runat="server" CssClass="grid-view" ScrollBars="Auto">
                  
                    <asp:GridView ID="gv_emp_attendance" class="table" runat="server" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        AutoGenerateColumns="False" Width="100%" OnRowDataBound="gv_emp_attendance_RowDataBound" OnPreRender="gv_emp_attendance_PreRender">
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
                            <asp:BoundField DataField="unit_code" HeaderText="Branch"
                                SortExpression="unit_code" />
                            <asp:BoundField DataField="emp_name" HeaderText="Employee Name"
                                SortExpression="emp_name" />
                            <asp:BoundField DataField="grade_desc" HeaderText="Grade"
                                SortExpression="grade_desc" />
                            <asp:BoundField DataField="shifttime" HeaderText="Shipt Time"
                                SortExpression="shifttime" />
                            <asp:BoundField DataField="punctuality" HeaderText="Punctuality"
                                SortExpression="punctuality" />
                            <asp:BoundField DataField="uniforms" HeaderText="Uniforms"
                                SortExpression="uniforms" />
                            <asp:BoundField DataField="cap" HeaderText="Cap"
                                SortExpression="cap" />
                            <asp:BoundField DataField="shoes" HeaderText="Shoes"
                                SortExpression="shoes" />
                            <asp:BoundField DataField="belt" HeaderText="Belt"
                                SortExpression="belt" />
                            <asp:BoundField DataField="id_card" HeaderText="Id Card"
                                SortExpression="id_card" />
                            <asp:BoundField DataField="shaving" HeaderText="Shaving"
                                SortExpression="shaving" />
                            <asp:BoundField DataField="hairs" HeaderText="Hairs"
                                SortExpression="hairs" />
                            <asp:BoundField DataField="nails" HeaderText="Nails"
                                SortExpression="nails" />
                            <asp:BoundField DataField="briefing" HeaderText="Briefing"
                                SortExpression="briefing" />

                            <asp:TemplateField HeaderText="InTime Image">
                                <ItemTemplate>
                                    <asp:Image ID="intime_imgpath" runat="server" Height="50" Width="50" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OutTime Image">
                                <ItemTemplate>
                                    <asp:Image ID="outtime_imgpath" runat="server" Height="50" Width="50" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="remarks" HeaderText="Remarks"
                                SortExpression="remarks" />
                            <asp:BoundField DataField="location_add" HeaderText="Location Address"
                                SortExpression="location_add" />

                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                                       </div>
                                           </ContentTemplate></asp:UpdatePanel>
                                  </div>
                                <div id="menu0">
                                    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>--%>
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12" style="margin-right: -72px;">
                                            <span style="color: black"><b>Date :</b></span><span style="color: red">*</span>
                                            <asp:TextBox ID="txt_satrtdate" runat="server" class="form-control date-picker1" Style="width: 70%; margin-right: -72px;"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="margin-right: -67px;">
                                            <span style="color: black"><b>To Date :</b></span><span style="color: red">*</span>
                                            <asp:TextBox ID="txt_enddate" runat="server" class="form-control date-picker2" Style="width: 70%; margin-right: -67px;"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="margin-top: 17px;">
                                            <asp:Button ID="Button3" runat="server" Text="Show" class="btn btn-primary" OnClick="Button3_Click" OnClientClick="return rq_validation();" />
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Panel runat="server" CssClass="grid-view">
                                        <asp:GridView ID="GradeGridView" class="table" runat="server" Font-Size="X-Small" Width=""
                                            AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" OnPreRender="GradeGridView_PreRender"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="GradeGridView_RowDataBound1">
                                            <RowStyle ForeColor="#000066" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No.">
                                                    <ItemStyle Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_emp_name" runat="server" Text='<%# Eval("EMP_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ADDRESS">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_address" runat="server" Text='<%# Eval("ADDRESS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BRANCH IN-TIME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_attendances_intime" runat="server" Text='<%# Eval("attendances_intime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BRANCH OUT_TIME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_attendances_outtime" runat="server" Text='<%# Eval("attendances_outtime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OUTSIDE IN-TIME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_camera_intime" runat="server" Text='<%# Eval("camera_intime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OUTSIDE OUT-TIME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_camera_outtime" runat="server" Text='<%# Eval("camera_outtime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IN">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Camera_Image1" runat="server" Height="50" Width="50" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OUT">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Camera_Image2" runat="server" Height="50" Width="50" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>


                                        <%-- </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                    </asp:Panel>
                                </div>

                                <div id="menu9" class="tab-pane">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12"></div>
                                                <div class="col-sm-2 col-xs-12" style="margin-right: -72px;">
                                                    <span style="color: black"><b>From Date :</b></span><span style="color: red">*</span>
                                                    <asp:TextBox ID="txt_work_img_from" runat="server" class="form-control date-picker3" Style="width: 70%; margin-right: -72px;"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12" style="margin-right: -67px;">
                                                    <span style="color: black"><b>To Date :</b></span><span style="color: red">*</span>
                                                    <asp:TextBox ID="txt_work_img_to" runat="server" class="form-control date-picker4" Style="width: 70%; margin-right: -67px;"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12" style="margin-top: 17px;">
                                                    <asp:Button ID="btn_work_image" runat="server" Text="Show" class="btn btn-primary" OnClick="btn_work_image_Click" OnClientClick="return r_validation();" />
                                                </div>
                                            </div>
                                            <br />
                                            <asp:Panel runat="server" CssClass="grid-view">
                                                <div class="container">
                                                    <asp:GridView ID="grd_work_image" class="table" runat="server" Font-Size="X-Small"
                                                        AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" OnPreRender="grd_work_image_PreRender"
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="GradeGridView_RowDataBound">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr No.">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="EMP NAME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_emp_name" runat="server" Text='<%# Eval("EMP_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DATE-TIME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_camera_outtime" runat="server" Text='<%# Eval("datecurrent") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="IN">
                                                                <ItemTemplate>
                                                                    <asp:Image ID="image_name" runat="server" Height="50" Width="50" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="menu1" class="tab-pane">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>

                                            <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; margin-top:20px; border: 1px solid white; padding:20px 20px 20px 20px">


                                                <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                    OnRowDataBound="gv_itemslist_RowDataBound"
                                                    AutoGenerateColumns="False" Width="100%" OnPreRender="gv_itemslist_PreRender">
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr No.">
                                                            <ItemStyle />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation">
                                                            <ItemStyle />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_designation" runat="server" Text='<%# Eval("DESIGNATION")%>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Employee Count">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_count" runat="server" ReadOnly="True" Style="text-align: left" class="form-control" Text='<%# Eval("COUNT")%>' Width="150px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                


                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div id="menu3" class="tab-pane">

                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12"></div>
                                        <div class="col-sm-8 col-xs-12">
                                            <table class="table table-bordered">
                                                <tr style="background-color: #337ab7; text-align: center; font-weight: bold; color: white">
                                                    <td></td>
                                                    <td>Operation</td>
                                                    <td>Finance</td>
                                                    <td>Location</td>
                                                    <td>Other</td>
                                                </tr>
                                                <tr style="text-align: center; font-weight: bold;">
                                                    <td><b>Contact Person Name</b></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_operationname" onkeypress="return AllowAlphabet_Number_slash(event);" runat="server" class="form-control text_box" placeholder="Contact Person Name"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_financename" onkeypress="return AllowAlphabet_Number_slash(event);" runat="server" class="form-control text_box" placeholder="Contact Person Name"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_locationname" onkeypress="return AllowAlphabet_Number_slash(event);" runat="server" class="form-control text_box" placeholder="Contact Person Name"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_othername" onkeypress="return AllowAlphabet_Number_slash(event);" runat="server" class="form-control text_box" placeholder="Contact Person Name"></asp:TextBox></td>
                                                </tr>
                                                <tr style="text-align: center; font-weight: bold;">
                                                    <td><b>Mobile No</b></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_omobileno" runat="server" CausesValidation="true" onkeypress="return isNumber_dot(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_fmobileno" runat="server" class="form-control text_box" CausesValidation="true" onkeypress="return isNumber_dot(event)" MaxLength="10" placeholder="Mobile No"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_lmobileno" runat="server" CausesValidation="true" onkeypress="return isNumber_dot(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_othermobno" runat="server" onkeypress="return isNumber_dot(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>
                                                </tr>
                                                <tr style="text-align: center; font-weight: bold;">
                                                    <td><b>Email Id</b></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_oemailid" runat="server" class="form-control text_box" placeholder="Email Id"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_femailid" runat="server" MaxLength="50" class="form-control text_box" placeholder="Email Id"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_lemailid" runat="server" class="form-control text_box" placeholder="Email Id"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_otheremailid" runat="server" MaxLength="50" class="form-control text_box" placeholder="Email Id"></asp:TextBox></td>
                                                </tr>

                                            </table>

                                        </div>
                                    </div>
                                </div>

                                <div id="menu4" class="tab-pane">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>

                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12">
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    <span style="color: black"><b>Select Month :</b></span><span class="text-red">*</span>
                                                    <asp:TextBox ID="txt_month_year" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    <br />
                                                    <asp:Button ID="btn_show" runat="server" class="btn btn-primary"
                                                        OnClick="btn_show_Click" Text=" SHOW " OnClientClick="return re_validation();" />
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="container" style="margin-right: 67px;">
                                                    <asp:Panel ID="Panel4" runat="server" CssClass="grid-view" ScrollBars="Auto">

                                                        <asp:GridView ID="gv_unit_attendance" runat="server" ForeColor="#333333" class="table" GridLines="Both" OnPreRender="gv_unit_attendance_PreRender">
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

                                                        </asp:GridView>

                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="menu5" class="tab-pane">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>

                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12">
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    <span style="color: black"><b>Select Employee :</b></span><span style="color: red;">*</span>
                                                    <asp:DropDownList ID="ddl_employee" runat="server" class="form-control text_box"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    <br />
                                                    <asp:Button ID="btn_document_show" runat="server" class="btn btn-primary"
                                                        OnClick="btn_document_show_Click" Text="SHOW DOCUMENTS" OnClientClick="return require_validation();" Style="margin-top: 11px;" />
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-1 col-xs-12"></div>
                                                <div class="col-sm-2 col-xs-12" style="margin-top: 35px;">
                                                    <span style="color: black"><b>Photo(Passport Size) :</b></span>
                                                    <br />
                                                    <asp:Label ID="photo" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                                </div>

                                                <div class="col-sm-3 col-xs-12">
                                                    <asp:ImageButton ID="Image4" runat="server" Width="100px" OnClick="image_click" Height="100px" ImageUrl="~/Images/placeholder.png" />
                                                </div>
                                                <div class="col-sm-1 col-xs-12"></div>
                                                <div class="col-sm-2 col-xs-12" style="margin-top: 35px;">
                                                    <span style="color: black"><b>Aadhar Card :</b></span>
                                                    <br />
                                                    <asp:Label ID="l_bank_upload" runat="server" Text="Employee AADHAR Card Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                    <asp:ImageButton ID="Image2" runat="server" meta:resourcekey="Image2Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/passbook.jpg" />
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-1 col-xs-12"></div>
                                                <div class="col-sm-2 col-xs-12" style="margin-top: 35px;">
                                                    <span style="color: black"><b>Police Verification Document :</b></span>
                                                    <br />
                                                    <asp:Label ID="l_Police_document" runat="server" Text="Employee Police Verification Document  Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                                </div>

                                                <div class="col-sm-3 col-xs-12">
                                                    <asp:ImageButton ID="Image14" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                                </div>

                                                <div class="col-sm-1 col-xs-12"></div>
                                                <div class="col-sm-2 col-xs-12" style="margin-top: 35px;">
                                                    <span style="color: black"><b>Address Proof :</b></span>
                                                    <br />
                                                    <asp:Label ID="address_proof" runat="server" Text="Employee Address Proof Document  Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                                </div>

                                                <div class="col-sm-3 col-xs-12">
                                                    <asp:ImageButton ID="image15" runat="server" Width="100px" Height="100px" meta:resourcekey="Image13Resource1" ImageUrl="~/Images/Biodata.png" />
                                                </div>
                                            </div>
                                            <br />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div id="dialog"></div>
                                </div>

                                <div id="menu6" class="tab-pane">
                                    <div class="container" style="width: 60%; margin-right: 392px;">
                                        <div class="row">
                                            <p style="font-size: 13px; color: black">
                                                &nbsp;&nbsp; &nbsp; &nbsp;<b>We thank you for your participation for using our services</b>
                                            </p>

                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div>

                                            <p class="text-center" style="font-size: 14px; color: black">
                                                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                             <asp:RadioButton ID="RadioButton1" runat="server" GroupName="radioA1" />
                                                &nbsp;1 &nbsp;
                             <asp:RadioButton ID="RadioButton2" runat="server" GroupName="radioA1" />
                                                &nbsp; 2&nbsp;
                             <asp:RadioButton ID="RadioButton3" runat="server" GroupName="radioA1" />
                                                &nbsp;3 &nbsp;
                             <asp:RadioButton ID="RadioButton4" runat="server" GroupName="radioA1" />
                                                &nbsp;4 &nbsp;
                             <asp:RadioButton ID="RadioButton5" runat="server" GroupName="radioA1" />
                                                &nbsp;5 &nbsp;
                             <asp:RadioButton ID="RadioButton6" runat="server" GroupName="radioA1" />
                                                &nbsp; 6&nbsp;
                             <asp:RadioButton ID="RadioButton7" runat="server" GroupName="radioA1" />
                                                &nbsp;7 &nbsp;
                             <asp:RadioButton ID="RadioButton8" runat="server" GroupName="radioA1" />
                                                &nbsp;8 &nbsp;
                             <asp:RadioButton ID="RadioButton9" runat="server" GroupName="radioA1" />
                                                &nbsp;9 &nbsp;
                             <asp:RadioButton ID="RadioButton10" runat="server" GroupName="radioA1" />
                                                &nbsp;10 &nbsp;
                                            </p>

                                            <div class="row">

                                                <div class="container">
                                                    <div class="col-sm-3"></div>
                                                    <div class="col-sm-6">
                                                        <div class="text-center">

                                                            <table border="2" class="table">
                                                                <tr>
                                                                    <th bgcolor='#ADD8E6'><b>Values</b></th>
                                                                    <td>1-3</td>
                                                                    <td>4-5</td>
                                                                    <td>6-7</td>
                                                                    <td>8-9</td>
                                                                    <td>10</td>

                                                                </tr>
                                                                <tr>
                                                                    <th bgcolor='#ADD8E6'><b>Rating</b></th>
                                                                    <td>Fair</td>
                                                                    <td>Average</td>
                                                                    <td>Good</td>
                                                                    <td>Best</td>
                                                                    <td>Excellent</td>
                                                                </tr>

                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2"></div>
                                            <div class="col-sm-2 col-xs-12">
                                                <p style="font-size: 13px; color: black;"><span style="margin-left: 7em;"><b>Suggestion :</b></span></p>
                                            </div>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txtfeed" runat="server" TextMode="multiline" Columns="6" Rows="2" Width="400px" class="form-control text_box"
                                                    Visible="true" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 col-xs-12">
                                                <asp:Button ID="btnSubmit" runat="server" OnClick="btn_submit" Text="Submit" OnClientClick="return copy_add();" class="btn btn-primary" Style="margin-top: 22px;" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-2"></div>
                                            <div class="col-sm-2 col-xs-12">
                                                <p style="font-size: 13px; color: black"><span style="margin-left: 7em;"><b>Attach File :</b></span></p>
                                            </div>
                                            <div class="col-sm-2 col-xs-12" style="font-size: 13px; margin-top: 4px; color: black">
                                                <asp:FileUpload ID="FileUpload1" runat="server" meta:resourcekey="photo_uploadResource1" />
                                                <span>
                                                    <p style="color: black; font-size: 10px"><b>JPEG PNG PDF</b></p>
                                                </span>

                                            </div>
                                        </div>
                                        <br />

                                    </div>

                                </div>

                                <div id="menu7" class="tab-pane">
                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row text-center">
                                                <%--Please dont delete this 3 Buttons...Vinod --%>
                                                <asp:Button ID="Button6" runat="server" CssClass="hidden" />
                                                <asp:Button ID="Button7" runat="server" CssClass="hidden" />
                                                <asp:Button ID="Button8" runat="server" CssClass="hidden" OnClick="Button8_Click" />


                                                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel9" TargetControlID="lnk_add_category"
                                                    BackgroundCssClass="Background">
                                                </cc1:ModalPopupExtender>
                                                <asp:Panel ID="Panel9" runat="server" CssClass="Popup" Style="display: none">
                                                    <iframe style="width: 800px; height: 500px; background-color: #fff;" id="Iframe2" src="service_category.aspx" runat="server"></iframe>
                                                    <div class="row text-center">
                                                        <asp:Button ID="Button9" CssClass="btn btn-danger" OnClientClick="callfnc()" runat="server" Text="Close" OnClick="Button9_Click" />
                                                    </div>

                                                    <br />

                                                </asp:Panel>
                                            </div>
                                            <div class="container" style="margin-left: 109px">
                                                <div class="row">

                                                    <div class="col-sm-2 col-xs-12">
                                                        <span style="color: black"><b>Services :</b></span><span class="text-red"> *</span>

                                                        <asp:DropDownList ID="ddl_asset_type" runat="server" class="form-control" OnSelectedIndexChanged="ddl_asset_type_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                                            <asp:ListItem Value="Electrical">Electrical</asp:ListItem>
                                                            <asp:ListItem Value="Plumbing">Plumbing</asp:ListItem>
                                                            <asp:ListItem Value="Carpentry">Carpentry</asp:ListItem>
                                                            <asp:ListItem Value="Civil">Civil</asp:ListItem>
                                                            <asp:ListItem Value="Pest_Control">Pest_Control</asp:ListItem>
                                                            <asp:ListItem Value="HVAC">HVAC</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2 col-xs-12">
                                                        <span style="color: black"><b>Category :</b></span><span class="text-red"> *</span>
                                                        <asp:DropDownList ID="ddl_category" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                        <asp:LinkButton ID="lnk_add_category" Font-Bold="true" Text="Add Category" OnClick="lnk_add_category_Click" runat="server"></asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-2 col-xs-12">
                                                        <span style="color: black"><b>Priority :</b></span><span class="text-red"> *</span>
                                                        <asp:DropDownList ID="department_asset" runat="server" class="form-control">
                                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                                            <asp:ListItem Value="TOP">TOP</asp:ListItem>
                                                            <asp:ListItem Value="MEDIUM">MEDIUM</asp:ListItem>
                                                            <asp:ListItem Value="LOW">LOW</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3 col-xs-12">
                                                        <span style="color: black"><b>Additional Comment :</b></span><span class="text-red"> *</span>
                                                        <asp:TextBox ID="txt_asset_description" TextMode="multiline" Columns="5" Rows="2" runat="server" class="form-control" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3 col-xs-12">
                                                        <br />

                                                    </div>
                                                    <div class="col-sm-3 col-xs-12">
                                                        <br />
                                                        <asp:Button ID="btn_add" runat="server" class="btn btn-primary" Text="Send Request" OnClick="btn_send_request_click" OnClientClick="return validation();" />
                                                        <asp:Button ID="btncloseup" runat="server" class="btn btn-danger" Text="Close" />
                                                    </div>
                                                </div>
                                                <br />

                                                <asp:Panel ID="Panel6" runat="server" BackColor="White" Visible="False" meta:resourcekey="Panel5Resource1">
                                                    <div class="container" style="width: 100%;">
                                                        <asp:GridView ID="SearchGridView" class="table" Width="100%" AutoGenerateColumns="False" DataKeyNames="id" runat="server"
                                                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="SearchGridView_RowDataBound"
                                                            CellPadding="3" meta:resourcekey="SearchGridViewResource1" OnPreRender="SearchGridView_PreRender">
                                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                            <Columns>
                                                                <asp:BoundField DataField="Id" HeaderText="No." SortExpression="Id" />
                                                                <asp:BoundField DataField="services" HeaderText="Services" SortExpression="services" />
                                                                <asp:BoundField DataField="priority" HeaderText="Priority" SortExpression="priority" />
                                                                <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" />
                                                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                                                <asp:BoundField DataField="additional_comment" HeaderText="Comments" ItemStyle-Width="200px" SortExpression="additional_comment" />
                                                                <asp:TemplateField ItemStyle-Width="100">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>

                                                                        <%-- <asp:LinkButton  ID="lnkbtn_edititem" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Approved" OnClientClick="return R_validation();" OnClick="gv_emp_d_varification_RowEditing"></asp:LinkButton>   --%>
                                                                        <asp:LinkButton ID="lnkbtn_edititem" runat="server" Text="Approved" OnClientClick="return R_validation();" OnClick="lnkbtn_edititem_Click"></asp:LinkButton><br />
                                                                        <asp:LinkButton ID="lnk_reject" runat="server" Text="Reject" OnClick="lnk_rejet_Click"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Download File">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="lnkDownload_Click"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </asp:Panel>

                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="SearchGridView" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                                <div id="menu8" class="tab-pane">

                                    <div class="row" style="color: black">
                                        <div class="col-sm-4 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12">
                                          <b>  Month :</b>
                                        <asp:TextBox runat="server" ID="txt_file_month" CssClass="form-control date-picker13"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 col-xs-12" style="margin-top: 1.5em;">
                                            <asp:Button runat="server" Text="Show" CssClass="btn btn-primary" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <asp:Panel ID="Panel7" runat="server" CssClass="grid">
                                            <asp:GridView ID="grd_company_files" class="table" Width="100%" runat="server" BackColor="White"
                                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                OnRowDataBound="grd_company_files_RowDataBound" OnRowDeleting="grd_company_files_RowDeleting" AutoGenerateColumns="False" DataKeyNames="id">
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

                                                    <asp:TemplateField HeaderText="Download File">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="created_by" HeaderText="Quotation By"
                                                        SortExpression="created_by" />
                                                    <asp:BoundField DataField="create_date" HeaderText="Date"
                                                        SortExpression="create_date" />
                                                    <asp:CommandField ButtonType="Button"
                                                        ControlStyle-CssClass="btn btn-primary"
                                                        ShowDeleteButton="true" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>

                                <div id="menu10" class="tab-pane">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>

                                            <div class="row">

                                                <div class="col-sm-5 col-xs-12 text-left"></div>
                                                <div class="col-sm-2 col-xs-12 text-left">
                                                    <span style="color: black"><b>Supervisor Name :</b></span>

                                                    <asp:DropDownList ID="dd1_super" class="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-sm-2 col-xs-12 text-left" style="margin-top: 17px;">
                                                    <asp:Button ID="Button10" runat="server" class="btn btn-primary" OnClick="btn_show_Click1" Text=" Show " OnClientClick="return visit_rpt_val();" />



                                                </div>

                                            </div>
                                            <div class="container" style="width: 100%">


                                                <asp:Panel ID="Panel10" runat="server" CssClass="grid-view" Style="overflow-x: auto;">

                                                    <br />

                                                    <asp:GridView ID="companyGridView" class="table" HeaderStyle-CssClass="FixedHeader" runat="server"
                                                        AutoGenerateColumns="False" CellPadding="1" Font-Size="X-Small" ForeColor="#333333" DataKeyNames="id"
                                                        OnRowDataBound="companyGridView_RowDataBound">


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

                                                            <asp:BoundField DataField="Id" HeaderText="ID"
                                                                SortExpression="Id" Visible="false" />


                                                            <asp:BoundField DataField="emp_code" HeaderText="Employee_Name"
                                                                SortExpression="emp_code" />
                                                            <asp:BoundField DataField="grade_name" HeaderText="Grade_name"
                                                                SortExpression="grade_name" />
                                                            <asp:BoundField DataField="que_1_ans" HeaderText="Question_1"
                                                                SortExpression="que_1_ans" />

                                                            <asp:TemplateField HeaderText="IMAGE">
                                                                <ItemTemplate>
                                                                    <asp:Image ID="que_1_path" runat="server" Height="50" Width="50" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="que_2_ans" HeaderText="Question_2"
                                                                SortExpression="que_2_ans" />

                                                            <asp:TemplateField HeaderText="IMAGE">
                                                                <ItemTemplate>
                                                                    <asp:Image ID="que_2_path" runat="server" Height="50" Width="50" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:BoundField DataField="que_3_ans" HeaderText="Question_3"
                                                                SortExpression="que_3_ans" />

                                                            <asp:TemplateField HeaderText="IMAGE">
                                                                <ItemTemplate>
                                                                    <asp:Image ID="que_3_path" runat="server" Height="50" Width="50" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:BoundField DataField="que_4_ans" HeaderText="Question_4"
                                                                SortExpression="que_4_ans" />


                                                            <asp:TemplateField HeaderText="IMAGE">
                                                                <ItemTemplate>
                                                                    <asp:Image ID="que_4_path" runat="server" Height="50" Width="50" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:BoundField DataField="que_5_ans" HeaderText="Question_5"
                                                                SortExpression="que_5_ans" />


                                                            <asp:TemplateField HeaderText="IMAGE">
                                                                <ItemTemplate>
                                                                    <asp:Image ID="que_5_path" runat="server" Height="50" Width="50" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField DataField="que_6_ans" HeaderText="Question_6"
                                                                SortExpression="que_6_ans" />


                                                            <asp:TemplateField HeaderText="IMAGE">
                                                                <ItemTemplate>
                                                                    <asp:Image ID="que_6_path" runat="server" Height="50" Width="50" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                            <asp:BoundField DataField="remark" HeaderText="Remark"
                                                                SortExpression="remark" />
                                                            <asp:BoundField DataField="location" HeaderText="location"
                                                                SortExpression="location" />
                                                            <asp:BoundField DataField="comment" HeaderText="Comment"
                                                                SortExpression="comment" />
                                                            <%--  <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-primary"  
                                                        ShowDeleteButton="true" EditText="Approved" DeleteText="Reject" ShowEditButton="true"/>
                                                            --%>
                                                            <asp:BoundField DataField="Status" HeaderText="Status"
                                                                SortExpression="Status" />





                                                        </Columns>




                                                    </asp:GridView>

                                                </asp:Panel>
                                            </div>


                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>

                                  <div id="menu14" class="tab-pane">
                                          <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>

                                                     <div class="row" >
                                                       <div class="col-sm-2 col-xs-12">
                                                          <b>  Feedback Date :</b>
                                        <asp:TextBox ID="txt_feedback_date" runat="server" CssClass="form-control date-picker "></asp:TextBox>
                                                        </div>

                                                         <%--   <div class="col-sm-2 col-xs-12" style="margin-top: 17px;">
                                            <asp:Button ID="btn_show_feedback" runat="server" Text="Show" class="btn btn-primary" OnClick="Button3_Click" OnClientClick="return rq_validation();" />
                                        </div>--%>
                                                         </div>

                                                    <div class="container-fluid" style="font-family:Verdana;font-weight:bold; border-radius: 10px; border: 1px solid white;width:99%;font-size:12px;">
                <br />
                                     
                        <div class="row" style="text-align:right;">
                 <div class="col-sm-2 col-xs-12"></div>
                    <div class="col-sm-4 col-xs-12">
                ARE THE EMPLOYEES GROOMED PROPERLY
                        </div>
                <div class="col-sm-2 col-xs-12">
                                <asp:DropDownList ID="ddl_employee_groom" runat="server"  class="form-control" onchange="employee_groom_percent();">
                                    <asp:ListItem Value="100">Excellent</asp:ListItem>
                                      <asp:ListItem Value="95">Very Good</asp:ListItem>
                                      <asp:ListItem Value="90">Good</asp:ListItem>
                                      <asp:ListItem Value="80">Satisfactory</asp:ListItem>
                                      <asp:ListItem Value="70">Poor</asp:ListItem>
                                </asp:DropDownList>
                </div>
                </div>
                <br />
            <div class="row" style="text-align:right;">
                  <div class="col-sm-2 col-xs-12"></div>
                     <div class="col-sm-4 col-xs-12">
                         CLEANING AND HYGIENE STANDARDS MAINTAINED AT SITE
                     </div>
                  <div class="col-sm-2 col-xs-12">
                        <asp:DropDownList ID="ddl_employee_hygiene" runat="server"  class="form-control" onchange="employee_groom_percent();">
                            <asp:ListItem Value="100">Excellent</asp:ListItem>
                                      <asp:ListItem Value="95">Very Good</asp:ListItem>
                                      <asp:ListItem Value="90">Good</asp:ListItem>
                                      <asp:ListItem Value="80">Satisfactory</asp:ListItem>
                                      <asp:ListItem Value="70">Poor</asp:ListItem>
                                </asp:DropDownList>
                  </div>
                    </div>
                <br />
            <div class="row" style="text-align:right;">
                  <div class="col-sm-2 col-xs-12"></div>
                     <div class="col-sm-4 col-xs-12">
                         DO THE EMPLOYEES KNOW THEIR DUTIES
                     </div>
                  <div class="col-sm-2 col-xs-12">
                        <asp:DropDownList ID="ddl_employee_duty" runat="server"  class="form-control" onchange="employee_groom_percent();">
                             <asp:ListItem Value="100">Excellent</asp:ListItem>
                                      <asp:ListItem Value="95">Very Good</asp:ListItem>
                                      <asp:ListItem Value="90">Good</asp:ListItem>
                                      <asp:ListItem Value="80">Satisfactory</asp:ListItem>
                                      <asp:ListItem Value="70">Poor</asp:ListItem>
                                </asp:DropDownList>
                  </div>
                    </div>
                <br />
            <div class="row" style="text-align:right;">
                  <div class="col-sm-2 col-xs-12"></div>
                     <div class="col-sm-4 col-xs-12">
                         BEHAVIOR AND ATTITUDE OF EMPLOYEE
                     </div>
                  <div class="col-sm-2 col-xs-12">
                        <asp:DropDownList ID="ddl_employee_behaviour" runat="server"  class="form-control" onchange="employee_groom_percent();">
                             <asp:ListItem Value="100">Excellent</asp:ListItem>
                                      <asp:ListItem Value="95">Very Good</asp:ListItem>
                                      <asp:ListItem Value="90">Good</asp:ListItem>
                                      <asp:ListItem Value="80">Satisfactory</asp:ListItem>
                                      <asp:ListItem Value="70">Poor</asp:ListItem>
                                </asp:DropDownList>
                  </div>
                    </div>
                <br />
            <div class="row" style="text-align:right;">
                  <div class="col-sm-2 col-xs-12"></div>
                       <div class="col-sm-4 col-xs-12">
                           SUPPORT FROM TEAM (FO/ADMIN)
                       </div>
                  <div class="col-sm-2 col-xs-12">
                        <asp:DropDownList ID="ddl_employee_support" runat="server"  class="form-control"  onchange="employee_groom_percent();">
                             <asp:ListItem Value="100">Excellent</asp:ListItem>
                                      <asp:ListItem Value="95">Very Good</asp:ListItem>
                                      <asp:ListItem Value="90">Good</asp:ListItem>
                                      <asp:ListItem Value="80">Satisfactory</asp:ListItem>
                                      <asp:ListItem Value="70">Poor</asp:ListItem>
                                </asp:DropDownList>
                  </div> 
                    </div>
                <br />
            <div class="row">
                  <div class="col-sm-2 col-xs-12"></div>
                      <div class="col-sm-4 col-xs-12" style="text-align:right;">
                          <span style="font-family:Verdana;font-weight:bold;font-size:14px;">Percentage : </span>
                      </div>
                 <div class="col-sm-2 col-xs-12">
                     <table><tr><th>  <asp:Label runat="server" ID="lbl_percentage" style="font-family:Arial;font-weight:bold;font-size:12px;">100%</asp:Label></th></tr></table>
                    
                  </div> 
            </div>

                                                         <br />
                                                         <div class="container">
                <div class="row text-center">
                    <asp:Button ID="btn_submit_feedback" runat="server" CssClass="btn btn-primary" OnClientClick="return feedback_validations();" OnClick="btn_submit_feedback_Click" Text="Submit" /> 
                     <asp:Button ID="btn_close" runat="server" CssClass="btn btn-danger" Text="Close" /> 
                </div> 
            </div>


               <br />    
               </div>

                                                     </ContentTemplate>
                                               </asp:UpdatePanel>
                                      </div>

                                  <div id="item15" class="tab-pane">
                                             <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                 <ContentTemplate>
                                                      
                                                     <div class="row" >

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
                                                    <asp:LinkButton ID="lnk_remove_fire" runat="server" CausesValidation="false" OnClick="lnk_remove_fire_Click" OnClientClick="return confirm('Are you sure You want to  Delete ?') " ><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
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
                                                        
                                                           </ContentTemplate>
                                            </asp:UpdatePanel>
                                      </div>


                                <div id="menu11" class="tab-pane">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="container-fluid">
                                                <asp:Panel runat="server" ID="panel212" CssClass="panel panel-primary" Style="border: gray; background: rgba(255, 255, 255, 0.58); border-radius: 10px">
                                                    <div class="container-fluid">
                                                        <div class="row">
                                                            <br />

                                                            <div class="col-sm-2 col-xs-12 text-left">
                                                                <span style="color: black"><b>Priority Level:</b></span>

                                                                <asp:DropDownList ID="ddl_priority" class="form-control" runat="server" OnSelectedIndexChanged="fillcopmlaint_ckh" AutoPostBack="true">
                                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                    <asp:ListItem Value="TOP">TOP</asp:ListItem>
                                                                    <asp:ListItem Value="MEDIUM">MEDIUM</asp:ListItem>
                                                                    <asp:ListItem Value="LOW">LOW</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-2 col-xs-12 text-left">
                                                                <span style="color: black"><b>Complaint Category:</b></span>

                                                                <asp:DropDownList ID="ddl_add_category" class="form-control" runat="server" OnSelectedIndexChanged="ddl_add_category_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <asp:Panel runat="server" Visible="false" ID="panl">
                                                                <br />
                                                                <div class="col-sm-4 col-xs-12">
                                                                    <asp:Panel runat="server" CssClass="grid-view" ID="pan">
                                                                        <asp:GridView ID="gv_complaint" runat="server" AutoGenerateColumns="false" ForeColor="#333333" class="table" GridLines="Both" OnRowDataBound="gv_complaint_RowDataBound">
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
                                                                                        <asp:CheckBox ID="ChecComplaints" runat="server" />
                                                                                        <asp:Label runat="server" Text="SR.NO"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chk_client" runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="name" HeaderText="COMPLAINT TYPE" SortExpression="name" />

                                                                            </Columns>
                                                                        </asp:GridView>

                                                                    </asp:Panel>
                                                                </div>
                                                                <div class="col-sm-3 col-xs-12">
                                                                    <input id="Hidden" type="hidden" runat="server" />
                                                                    <span style="color: black"><b>Remark :</b></span>

                                                                    <asp:TextBox ID="txt_remark" class="form-control" TextMode="MultiLine" runat="server" onkeypress="return AllowAlphabet_address(event);"> </asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 col-xs-12" style="margin-top: 3em;">
                                                                    <asp:Button ID="btn_send" runat="server" CssClass="btn btn-primary" Text="SEND" OnClick="btn_send_unit_feerback_click" OnClientClick="return fun();" />

                                                                </div>
                                                            </asp:Panel>
                                                            <%--<div class="col-sm-3 col-xs-12 block">
                                                <asp:CheckBoxList ID="ChecComplaints" runat="server" Font-Size="X-Small">
                                                </asp:CheckBoxList>
                                            </div>--%>
                                                            <br />

                                                        </div>
                                                        <br />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <br />
                                            <div class="container-fluid">
                                                <asp:Panel ID="Panel11" runat="server" Visible="False" meta:resourcekey="Panel5Resource1" Style="color: black" CssClass="grid-view">

                                                    <asp:GridView ID="unitcomplaintGridView" class="table" Width="100%" AutoGenerateColumns="False" DataKeyNames="id" runat="server"
                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="unitcomplaintGridView_RowDataBound"
                                                        CellPadding="3" meta:resourcekey="SearchGridViewResource1" OnPreRender="unitcomplaintGridView_PreRender">
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Id" HeaderText="No." SortExpression="Id" />
                                                            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                                            <asp:BoundField DataField="complaint_name" HeaderText="Type Of Complaint" SortExpression="complaint_name" />
                                                            <asp:BoundField DataField="priority" HeaderText="Priority" SortExpression="priority" />
                                                            <asp:BoundField DataField="date" HeaderText="Complaint Raised Date" SortExpression="date" />
                                                            <asp:BoundField DataField="resole_date" HeaderText="Complaint Resolved Date" SortExpression="resole_date" />
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbl_status" runat="server" Text='<%# Eval("status") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Remark" HeaderText="Remark" ItemStyle-Width="200px" SortExpression="Remark" />
                                                            <asp:BoundField DataField="comment" HeaderText="Reply From IHMS" ItemStyle-Width="200px" SortExpression="Remark" />
                                                            <asp:TemplateField>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>


                                                                    <asp:LinkButton ID="lnkbtn_edititemcomplaince" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Complete" OnClick="lnkbtn_edititemcomplaince_click" OnClientClick="return confirm('Are you sure want complete?')"></asp:LinkButton>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>

                                                </asp:Panel>
                                            </div>
                                            <br />
                                            <div class="row text-center">
                                            </div>


                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <br />
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>

        </div>
    </form>
</body>
</html>



