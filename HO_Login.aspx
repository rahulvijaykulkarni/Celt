<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HO_Login.aspx.cs" Inherits="HO_Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Welcome</title>
</head>
<body>
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
    <script src="datatable/pdfmake.min.js"></script>
  
    <style>
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
            margin-left: 15px;
        }

        .grid-view {
            height: auto;
            max-height: 450px;
            width: auto;
            overflow-y: auto;
            overflow-x: hidden;
        }

        .nav > li > a:focus, .nav > li > a:hover {
            text-decoration: none;
            background-color: #3c74a4;
            color: white;
        }
    </style>
     <script type="text/javascript">
         function pageLoad() {
             month_check();
             $.fn.dataTable.ext.errMode = 'none';
             var table = $('#<%=GridView_employee_tracking.ClientID%>').DataTable({
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
                .appendTo('#<%=GridView_employee_tracking.ClientID%>_wrapper .col-sm-6:eq(0)');
             $.fn.dataTable.ext.errMode = 'none';
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
             var table = $('#<%=companyGridView.ClientID%>').DataTable({
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

                 ]

             });

             table.buttons().container()
                .appendTo('#<%=companyGridView.ClientID%>_wrapper .col-sm-6:eq(0)');
             $.fn.dataTable.ext.errMode = 'none';

             var table = $('#<%=grd_work_image.ClientID%>').DataTable({
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

                  ]

              });

              table.buttons().container()
                 .appendTo('#<%=grd_work_image.ClientID%>_wrapper .col-sm-6:eq(0)');

             // chaitali 

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

                 ]

             });

             table.buttons().container()
                .appendTo('#<%=gv_unit_attendance.ClientID%>_wrapper .col-sm-6:eq(0)');

             $.fn.dataTable.ext.errMode = 'none';

             var table = $('#<%=unitcomplaintGridView1.ClientID%>').DataTable({
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

                  ]

              });

              table.buttons().container()
                 .appendTo('#<%=unitcomplaintGridView1.ClientID%>_wrapper .col-sm-6:eq(0)');

              $.fn.dataTable.ext.errMode = 'none';

              var table = $('#<%=gv_summary_report.ClientID%>').DataTable({
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

                 ]

             });

             table.buttons().container()
                .appendTo('#<%=gv_summary_report.ClientID%>_wrapper .col-sm-6:eq(0)');


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





             $(".date-picker13").datepicker({
                 changeMonth: true,
                 changeYear: true,
                 showButtonPanel: true,
                 dateFormat: 'dd/mm/yy',

             });
             $(".date-picker13").attr("readonly", "true");
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

             $('.date-picker12').datepicker({
                 beforeShow: function (input, inst) {
                     setTimeout(function () {
                         inst.dpDiv.css({
                             top: $(".date-picker12").offset().top + 30,
                             left: $(".date-picker12").offset().left,
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
             $(".date-picker12").attr("readonly", "true");
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
              $("#dialog").dialog({

                  autoOpen: false,
                  modal: true,
                  height: 500,
                  width: 500,
                  title: "Zoomed Image",
                  buttons: [{ text: "Close", click: function () { $(this).dialog("close") } }],
              });
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
              $("[id*=Camera_Image1]").click(function () {
                  $('#dialog').html('');
                  $('#dialog').append($(this).clone().width(470).height(397));
                  $('#dialog').dialog('open');
                  //height:200;
                  //width: 200;
              });
              $("[id*=Camera_Image2]").click(function () {
                  $('#dialog').html('');
                  $('#dialog').append($(this).clone().width(470).height(397));
                  $('#dialog').dialog('open');
                  //height:200;
                  //width: 200;
              });
              $("[id*=image_name]").click(function () {
                  $('#dialog').html('');
                  $('#dialog').append($(this).clone().width(470).height(397));
                  $('#dialog').dialog('open');
                  //height:200;
                  //width: 200;
              });
          }
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
         }

         function re_validation() {
             var txt_month_year = document.getElementById('<%=txt_month_year.ClientID %>');
             if (txt_month_year.value == "") {
                 alert("Please Select Month");
                 txt_month_year.focus();
                 return false;
             }
         }
         function require_validation() {
             var ddl_employee = document.getElementById('<%=ddl_employee.ClientID %>');
             var Selected_ddl_employee = ddl_employee.options[ddl_employee.selectedIndex].text;
             if (Selected_ddl_employee == "Select") {

                 alert("Please Select Employee");
                 ddl_employee.focus();
                 return false;
             }
         }
             function attendance_validation() {
                 var ddl_emp_attendance = document.getElementById('<%=ddl_emp_attendance.ClientID %>');
                  var Selected_ddl_emp_attendance = ddl_emp_attendance.options[ddl_emp_attendance.selectedIndex].text;
                 
                  
                  if (Selected_ddl_emp_attendance == "Select") {

                      alert(" Please Select Attendance Type ");
                      ddl_emp_attendance.focus();
                      return false;
                  }

                  var ddl_cur_month = document.getElementById('<%=ddl_cur_month.ClientID %>');
                  var Selected_ddl_cur_month = ddl_cur_month.options[ddl_cur_month.selectedIndex].text;
                 
                  if (Selected_ddl_cur_month == "Select") {

                      alert(" Please Select Month Type ");
                      ddl_cur_month.focus();
                      return false;
                  }
                  if (Selected_ddl_cur_month == "Monthwise") {

                      var txt_satrtdate1 = document.getElementById('<%=txt_satrtdate1.ClientID%>');
                      if (txt_satrtdate1.value == "") {
                          alert(" Please Select From Date ");
                          txt_satrtdate1.focus();
                          return false;
                      }

                      var txt_enddate1 = document.getElementById('<%=txt_enddate1.ClientID%>');
                      if (txt_enddate1.value == "") {
                          alert(" Please Select To Date ");
                          txt_enddate1.focus();
                          return false;
                      }
                  }
                 var ddl_get_attendace = document.getElementById('<%=ddl_get_attendace.ClientID %>');
                 var Selected_ddl_get_attendace = ddl_get_attendace.options[ddl_get_attendace.selectedIndex].text;

                 if (Selected_ddl_get_attendace == "Select") {

                     alert(" Please Select Attendance");
                     ddl_get_attendace.focus();
                     return false;
                 }

                 if (Selected_ddl_get_attendace == "Absent") {

                     var ddlunitselect = document.getElementById('<%=ddlunitselect.ClientID %>');
                     var Selected_ddlunitselect= ddlunitselect.options[ddlunitselect.selectedIndex].text;

                     if (Selected_ddlunitselect == "ALL") {

                         alert(" Please Select Branch Name");
                         ddlunitselect.focus();
                         return false;
                     }
                  }
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
                  return true;
              }

          }
          function validateCheckBoxes() {

              var ddl_region = document.getElementById('<%=ddl_region.ClientID %>');
             var Selected_ddl_region = ddl_region.options[ddl_region.selectedIndex].text;
             if (Selected_ddl_region == "Select") {
                 alert("Please Select Region");
                 ddl_region.focus();
                 return false;
             }

             var ddl_priority = document.getElementById('<%=ddl_priority.ClientID %>');
               var Selected_ddl_priority = ddl_priority.options[ddl_priority.selectedIndex].text;
               if (Selected_ddl_priority == "Select") {
                   alert("Please Select Priority");
                   ddl_priority.focus();
                   return false;
               }
               var ddl_add_category = document.getElementById('<%=ddl_add_category.ClientID %>');
             var Selected_ddl_add_category = ddl_add_category.options[ddl_add_category.selectedIndex].text;
             if (Selected_ddl_add_category == "Select") {
                 alert("Please Select Category");
                 ddl_add_category.focus();
                 return false;
             }

             var isValid = false;
             var gridView = document.getElementById('<%= gv_complaint.ClientID %>');
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
             alert("Please select atleast One Complaint Type");
             return false;
         }
      </script>
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
         $(document).ready(function () {
             var st = $(this).find("input[id*='hidtab']").val();
             if (st == null)
                 st = 0;
             $('[id$=tabs]').tabs({ selected: st });
         });
         function emp_curreent_location() {
            // var txt_emp_from = document.getElementById
             if (txt_emp_from.value == "") {
                 alert("Please Select From Date");
                 txt_emp_from.focus();
                 return false;
             }
           //  var txt_emp_to = document.getElementById
             if (txt_emp_to.value == "") {
                 alert("Please Select To Date");
                 txt_emp_to.focus();
                 return false;
             }
             $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             return true;
         }

         function month_check() {
             var ddl_cur_month = document.getElementById('<%=ddl_cur_month.ClientID %>');
             var Selected_ddl_cur_month = ddl_cur_month.options[ddl_cur_month.selectedIndex].text;
             if (Selected_ddl_cur_month == "Monthwise") {
                 $(".monthwise").show();
             }

             else { $(".monthwise").hide(); }
         }
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
             filter: alpha(opacity=90);
             opacity: 0.8;
         }

         /*.Popup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 5px;
            padding-left: 5px;
            padding-right: 5px;
            z-index: 101;
        }*/

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
             background-color: #3c74a4;
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
         /* Pen Title */
         .pen-title {
             padding: 20px 0;
             text-align: center;
             letter-spacing: 2px;
         }

             .pen-title h1 {
                 margin: 0 0 20px;
                 font-size: 40px;
                 font-weight: 300;
             }

             .pen-title span {
                 font-size: 12px;
             }

                 .pen-title span .fa {
                     color: #ed2553;
                 }

                 .pen-title span a {
                     color: #ed2553;
                     font-weight: 600;
                     text-decoration: none;
                 }

         /* Rerun */
         .rerun {
             margin: 0 0 30px;
             text-align: center;
         }

             .rerun a {
                 cursor: pointer;
                 display: inline-block;
                 background: #ed2553;
                 border-radius: 3px;
                 box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
                 padding: 10px 20px;
                 color: #ffffff;
                 text-decoration: none;
                 -webkit-transition: 0.3s ease;
                 transition: 0.3s ease;
             }

                 .rerun a:hover {
                     box-shadow: 0 3px 6px rgba(0, 0, 0, 0.16), 0 3px 6px rgba(0, 0, 0, 0.23);
                 }

         /* Scroll To Bottom */
         #codepen, #portfolio {
             position: fixed;
             bottom: 30px;
             right: 30px;
             background: #ec2652;
             width: 56px;
             height: 56px;
             border-radius: 100%;
             box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
             -webkit-transition: 0.3s ease;
             transition: 0.3s ease;
             color: #ffffff;
             text-align: center;
         }

             #codepen i, #portfolio i {
                 line-height: 56px;
             }

             #codepen:hover, #portfolio:hover {
                 box-shadow: 0 10px 20px rgba(0, 0, 0, 0.19), 0 6px 6px rgba(0, 0, 0, 0.23);
             }

         /* CodePen */
         #portfolio {
             bottom: 96px;
             right: 36px;
             background: #ec2652;
             width: 44px;
             height: 44px;
             -webkit-animation: buttonFadeInUp 1s ease;
             animation: buttonFadeInUp 1s ease;
         }

             #portfolio i {
                 line-height: 44px;
             }

         /* Container */
         /*.container {
  position: relative;
  max-width: 460px;
  width: 100%;
  margin: 0 auto 100px;
}*/
         .container.active .card:first-child {
             background: #f2f2f2;
             margin: 0 15px;
         }

         .container.active .card:nth-child(2) {
             background: #fafafa;
             margin: 0 10px;
         }

         .container.active .card.alt {
             top: 20px;
             right: 0;
             width: 100%;
             min-width: 100%;
             height: auto;
             border-radius: 5px;
             padding: 60px 0 40px;
             overflow: hidden;
         }

             .container.active .card.alt .toggle {
                 position: absolute;
                 top: 40px;
                 right: -70px;
                 box-shadow: none;
                 -webkit-transform: scale(14);
                 transform: scale(15);
                 -webkit-transition: -webkit-transform .5s ease;
                 transition: -webkit-transform .5s ease;
                 transition: transform .5s ease;
                 transition: transform .5s ease, -webkit-transform .5s ease;
             }

                 .container.active .card.alt .toggle:before {
                     content: '';
                 }

             .container.active .card.alt .title,
             .container.active .card.alt .input-container,
             .container.active .card.alt .button-container {
                 left: 0;
                 opacity: 1;
                 visibility: visible;
                 -webkit-transition: .3s ease;
                 transition: .3s ease;
             }

             .container.active .card.alt .title {
                 -webkit-transition-delay: .3s;
                 transition-delay: .3s;
             }

             .container.active .card.alt .input-container {
                 -webkit-transition-delay: .4s;
                 transition-delay: .4s;
             }

                 .container.active .card.alt .input-container:nth-child(2) {
                     -webkit-transition-delay: .5s;
                     transition-delay: .5s;
                 }

                 .container.active .card.alt .input-container:nth-child(3) {
                     -webkit-transition-delay: .6s;
                     transition-delay: .6s;
                 }

             .container.active .card.alt .button-container {
                 -webkit-transition-delay: .7s;
                 transition-delay: .7s;
             }

         /* Card */
         .card {
             /*position: fixed;*/
             background: #ffffff;
             /*border-radius: 5px;*/
             padding: 45px 0px 20px 0px;
             /*box-sizing: border-box;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);*/
             /*-webkit-transition: .3s ease;
  transition: .3s ease;*/
             /*margin-top:-140px;*/
             /* Title */
             /* Inputs */
             /* Button */
             /* Footer */
             /* Alt Card */
         }

             .card:first-child {
                 background: #fff;
                 border-radius: 5px 5px 0 0;
                 padding: 0;
             }

             .card .title {
                 position: relative;
                 z-index: 1;
                 border-left: 5px solid #ec2652;
                 margin: 0 0 35px;
                 padding: 10px 0 10px 50px;
                 color: #ec2652;
                 font-size: 32px;
                 font-weight: 600;
                 text-transform: uppercase;
             }

             .card .input-container {
                 position: relative;
                 margin: 0 0px 40px;
             }

                 .card .input-container input {
                     outline: none;
                     z-index: 0;
                     position: relative;
                     background: none;
                     height: 54px;
                     border: 0;
                     /*color: #0e0d0d;*/
                     font-size: 11px;
                     font-weight: 400;
                     margin-bottom: -25px;
                 }

                     .card .input-container input:focus ~ label {
                         color: #9d9d9d;
                         -webkit-transform: translate(-12%, -50%) scale(0.75);
                         transform: translate(-12%, -50%) scale(0.75);
                     }

                     .card .input-container input:focus ~ .bar:before, .card .input-container input:focus ~ .bar:after {
                         width: 50%;
                     }

                 .card .input-container label {
                     position: absolute;
                     top: -8px;
                     left: 0;
                     color: black;
                     font-size: 11px;
                     /*font-weight: normal;*/
                     line-height: 25px;
                     -webkit-transition: 0.2s ease;
                     transition: 0.2s ease;
                 }

                 .card .input-container .bar {
                     position: absolute;
                     left: 0;
                     bottom: 0;
                     background: #757575;
                     width: 100%;
                     height: 1px;
                 }

                     .card .input-container .bar:before, .card .input-container .bar:after {
                         content: '';
                         position: absolute;
                         background: #ec2652;
                         width: 0;
                         height: 2px;
                         -webkit-transition: .2s ease;
                         transition: .2s ease;
                     }

                     .card .input-container .bar:before {
                         left: 50%;
                     }

                     .card .input-container .bar:after {
                         right: 50%;
                     }

             .card .button-container {
                 margin: 0 60px;
                 text-align: center;
             }
         /*.card .button-container button {
  outline: 0;
  cursor: pointer;
  position: relative;
  display: inline-block;
  background: 0;
  width: 240px;
  border: 2px solid #e3e3e3;
  padding: 20px 0;
  font-size: 24px;
  font-weight: 600;
  line-height: 1;
  text-transform: uppercase;
  overflow: hidden;
  -webkit-transition: .3s ease;
  transition: .3s ease;
}
.card .button-container button span {
  position: relative;
  z-index: 1;
  color: #ddd;
  -webkit-transition: .3s ease;
  transition: .3s ease;
}*/
         /*.card .button-container button:before {
  content: '';
  position: absolute;
  top: 50%;
  left: 50%;
  display: block;
  background: #ec2652;
  width: 30px;
  height: 30px;
  border-radius: 100%;
  margin: -15px 0 0 -15px;
  opacity: 0;
  -webkit-transition: .3s ease;
  transition: .3s ease;
}*/
         /*.card .button-container button:hover, .card .button-container button:active, .card .button-container button:focus {
  border-color: #ec2652;
}
.card .button-container button:hover span, .card .button-container button:active span, .card .button-container button:focus span {
  color: #ec2652;
}
.card .button-container button:active span, .card .button-container button:focus span {
  color: #ffffff;
}
.card .button-container button:active:before, .card .button-container button:focus:before {
  opacity: 1;
  -webkit-transform: scale(10);
  transform: scale(10);
}*/
         /*.card .footer {
  margin: 40px 0 0;
  color: #d3d3d3;
  font-size: 24px;
  font-weight: 300;
  text-align: center;
}
.card .footer a {
  color: inherit;
  text-decoration: none;
  -webkit-transition: .3s ease;
  transition: .3s ease;
}
.card .footer a:hover {
  color: #bababa;
}*/
         /* Keyframes */
         @-webkit-keyframes buttonFadeInUp {
             0% {
                 bottom: 30px;
                 opacity: 0;
             }
         }

         @keyframes buttonFadeInUp {
             0% {
                 bottom: 30px;
                 opacity: 0;
             }
         }
     </style>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <br />
            <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary" Style="background-color: #f3f1fe; border: gray;">
                <div class="panel-heading">
                    
                    <div class="row">
                        <div class="col-sm-3">
                               <img src="Images/logo.png" style="width:150px; margin-left:-20px;" />
                        </div>
                        <div class="col-sm-5">
                            <div style="text-align: center; margin-top:20px; font-size: large;"><b>WELCOME TO IH&MS</b></div>
                        </div>
                          <div class="col-sm-1" style="margin-top: 5px;">
                            <div class="col-sm-2" ></div>
                            
                          <asp:label ID="Label1" runat="server" Text="Login :" Font-Size="Small"> </asp:label>
                            </div>

                        <div class="col-sm-3" >
                            
                             
                        <span><asp:Label ID="txt_usernam1" BackColor="#337ab7"  ForeColor="White" runat="server" CssClass="form-control" /> </span>
                        </div>
                         <div class="row">
                        <div class="col-sm-3 text-right" style="margin-left:100px; margin-top:20px;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                           <asp:Button ID="Button10" runat="server" OnClick="LinkButton1_Click" class="btn btn-primary" Text="LogOut" style="font-weight:bold;margin-right: 30px;" />
                            </asp:LinkButton>
                        </div>
                             </div>
                    </div>
                </div>
                <br />
                 <div class="panel-body" style="padding:10px;">
                     <div class="container-fluid">
                       <asp:Panel ID="Panel15" runat="server" CssClass="panel panel-primary" Style="border-color: gray; background: #fff">
                       <div class="card" style=" border-radius: 10px;">
                           <br />
                                      <div class="row">
                        <br />
                         <div class="col-md-2 col-xs-12"></div>
                            <div class="col-md-2 col-xs-12">
                             <span style="font-weight:bold">Zone Name :</span><br /><br />
                            <asp:DropDownList ID="ddl_zone" runat="server" class="form-control" OnSelectedIndexChanged="ddl_zone_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            </div>
                        <div class="col-sm-2 col-xs-12">
                             <span style="font-weight:bold">Region Name :</span><br /><br />
                            <asp:DropDownList ID="ddl_region" runat="server" class="form-control" OnSelectedIndexChanged="ddl_region_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12"margin-top: 7px;">
                            <span style="font-weight:bold"> Branch Name :</span><br /><br />
                            <asp:DropDownList ID="ddlunitselect" runat="server" class="form-control" OnSelectedIndexChanged="ddlunitselect_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                               
                            </ContentTemplate>
                        </asp:UpdatePanel>--%>
                          </div>
                   
                            <br />
                            <br />
                           <br />
                    <div class="row" style="display:none">
                      <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server">--%>
                            <%--<ContentTemplate>--%>
                                  <div class="col-sm-2 col-xs-12"></div>
                                <div class="col-sm-2 col-xs-12" visible ="false">
                                     <label for="state">State :</label><br />
                                     <asp:Label ID="ddl_state" runat="server" CssClass="form-control"></asp:Label>
                                      </div>
                                    <%-- <span style="font-weight:bold">City :</span>--%><%--<span class="text-red"> *</span>--%>
                                    <%--<asp:DropDownList ID="txtunitcity" runat="server" class="form-control text_box" Width="100%"></asp:DropDownList>--%>
                                      <%--<asp:Label ID="txtunitcity" runat="server" ></asp:Label>--%>
                                 <%--<asp:TextBox ID="txtunitcity" runat="server"  Width="100%" class="form-control text_box" ></asp:TextBox>--%>
                                
                                   <%-- <div class="input-container">
                <asp:TextBox ID="ddl_state" runat="server" type="text" required="required"  ></asp:TextBox>
                <label for="State">State :</label>
                <%--   <div class="bar"></div>--%>
                          
                                     <%--span style="font-weight:bold">State :</span>
                                    <%--<span class="text-red"> *</span>--%>
                                   <%-- <asp:DropDownList ID="ddl_state" runat="server" Width="100%"
                                         class="form-control text_box">
                                    </asp:DropDownList>--%>
                                   <%-- <br />
                                    <br />
                                    <asp:Label ID="ddl_state" runat="server" ></asp:Label>--%>
                                   <%-- <asp:TextBox ID="ddl_state" runat="server"  Width="100%" class="form-control text_box" ></asp:TextBox>--%>
                                 <div class="col-sm-2 col-xs-12" visible ="false">
                                      <label for="City:">City :</label>
                                    <asp:Label ID="txtunitcity" runat="server" CssClass="form-control"></asp:Label>
                                      </div>
                                  <div class="col-sm-1 col-xs-12" style="width:auto;" visible ="false">
                                        <label >Location(Place/City) :</label>
                                      <asp:Label ID="txtunitaddress1" runat="server" CssClass="form-control"></asp:Label>
                                      </div>
                                     <%-- <div class="input-container">
                <asp:TextBox ID="txtunitcity" runat="server" type="text" required="required"  ></asp:TextBox>
               </div>--%>
                  <%-- <div class="bar"></div>--%>
                
                                        
                                  <%-- <div class="col-sm-1 col-xs-12" style="width:auto;">
                                       <div class="input-container">
                <asp:TextBox ID="txtunitaddress1" runat="server" type="text" required="required" ></asp:TextBox>
                <label >Location(Place/City):</label>--%>
                   <%--<div class="bar"></div>--%>
              <%--  </div>
                                  </div>--%>
                                  <%-- <span style="font-weight:bold">  Location(Place/City) :</span>--%><%--<span class="text-red"> *</span>--%>
                                      <%-- <asp:TextBox ID="txtunitaddress1" runat="server" class="form-control text_box "></asp:TextBox>--%>
                                <div class="col-sm-2 col-xs-12" visible ="false">
                                    <label >Address(Street/Road/Lane):</label>
                                      <asp:Label ID="txtunitaddress2" runat="server" style="height:65px;width: 250px;" CssClass="form-control" TextMode="MultiLine"  ></asp:Label>
                                    </div>

                               <%-- <div class="col-sm-4 col-xs-12">
                                     <div class="input-container">
                                         <div class="text" style="padding: 10px;">
                <asp:TextBox ID="txtunitaddress2" runat="server" type="text" required="required" TextMode="MultiLine" BorderStyle="None" Width="400px" ></asp:TextBox>
                <label for="Address(Street/Road/Lane):">Address(Street/Road/Lane):</label>
                                              </div>
                                         </div>
                </div>--%>
                   <%--<div class="bar"></div>--%>
                                            
                                    <%-- <span style="font-weight:bold">Address(Street/Road/Lane) :</span>
                        <asp:TextBox ID="txtunitaddress2" runat="server" TextMode="multiline" Width="100%" Rows="2" class="form-control text_box" MaxLength="50"></asp:TextBox>
                               --%> 
                                <%--<div class="col-sm-2 col-xs-12">
                        GSTIN No :<span class="text-red"> *</span>
                            <asp:TextBox ID="txt_gst_no" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event,this);" Width="100%" MaxLength="20"></asp:TextBox>

                    </div>--%>
                                <div class="col-sm-2 col-xs-12" style="display:none">
                                     <span style="font-weight:bold">Zone :</span>
                        <asp:DropDownList ID="txt_zone1" runat="server" Width="100%"
                            DataValueField="ZONE" class="form-control text_box">
                            <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                            <asp:ListItem Value="1" Text="East">East</asp:ListItem>
                            <asp:ListItem Value="2" Text="West">West</asp:ListItem>
                            <asp:ListItem Value="3" Text="North">North</asp:ListItem>
                            <asp:ListItem Value="4" Text="South">South</asp:ListItem>
                        </asp:DropDownList>

                                </div>
                                <div class="col-sm-2 col-xs-12" style="display:none">
                                     <span style="font-weight:bold">Region Name :</span>
                        <asp:DropDownList ID="txt_zone" runat="server" Width="100%" class="form-control text_box">
                            <asp:ListItem>Select</asp:ListItem>
                        </asp:DropDownList>
                                </div>
                           <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>

                    </div>
                        <br />
                           <br />
                                  </div>
                           </asp:Panel>
                <%--</asp:Panel>--%>
                  </div>
                    <br />
                    <br />
                     <div class="row text-center">
                            <%--Please dont delete this 3 Buttons...Vinod --%>
                            <asp:Button ID="Button1" runat="server" CssClass="hidden" Text="Claim Expense" />
                            <asp:Button ID="Button2" runat="server" CssClass="hidden" Text="Add New Travelling Plan" />
                            <asp:Button ID="Button5" runat="server" CssClass="hidden" />

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

                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2" TargetControlID="Button2"
                                CancelControlID="Button4" BackgroundCssClass="Background">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel2" runat="server" CssClass="Popup" Style="display: none">
                                <iframe style="width: 800px; height: 500px; background-color: #fff;" id="Iframe1" src="" runat="server"></iframe>
                                <div class="row text-center">
                                    <asp:Button ID="Button4" CssClass="btn btn-danger" OnClientClick="callfnc()" runat="server" Text="Close" />
                                </div>

                                <br />

                            </asp:Panel>
                        </div>

                    <div class="row text-center">
                            <%--Please dont delete this 3 Buttons...Vinod --%>
                            <asp:Button ID="Button6" runat="server" CssClass="hidden" />
                            <asp:Button ID="Button7" runat="server" CssClass="hidden"  />
                            <asp:Button ID="Button8" runat="server" CssClass="hidden" />


                            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel9" TargetControlID="lnk_add_category"
                                CancelControlID="Button9" BackgroundCssClass="Background">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel9" runat="server" CssClass="Popup" Style="display: none">
                                <iframe style="width: 800px; height: 500px; background-color: #fff;" id="Iframe2" src="service_category.aspx" runat="server"></iframe>
                                <div class="row text-center">
                                    <asp:Button ID="Button9" CssClass="btn btn-danger" OnClientClick="callfnc2()" runat="server" Text="Close" />
                                </div>

                                <br />

                            </asp:Panel>
                        </div>
                     
                      <div class="container-fluid"> 
                     <div id="tabs" style="background: white;" runat="server">
                      
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                  
                      <ul class="nav nav-tabs" style="border: none; background-color: white">
                          <li id="Li10"><a href="#menu16"><b>Employee Attendance</b></a></li>
                         <%--  <li id="tabactive5"><a href="#menu0">Attendance Photo</a></li>--%>
                            <%-- <li id="Li7"><a href="#menu12">Android Attendance</a></li>--%>
                              <%--<li id="Li6"><a href="#menu11">Employee Current Location</a></li>--%>
                            <li id="Li4"><a href="#menu9"><b>Work Photo</b></a></li>
                            <li id="Li3"><a  href="#menu1"><b>Deployment Details</b></a></li>
                            <li id="tabactive11" style="display:none;" ><a  href="#menu3"><b>Heads Info</b></a></li>
                            <li id="tab_attendance"   style="display:none;" ><a href="#menu4"><b>Monthly Attendance</b></a></li>
                            <li id="tab_documents"><a  href="#menu5"><b>Employee KYC</b></a></li>
                            <li id="tab_feedback" style="display:none;"><a  href="#menu6"><b>Feedback</b></a></li>
                            
                            <li id="tab_attendance1"  style="display:none;"><a href="#menu8"><b>Files</b></a></li>
                            <li id="Li5"><a href="#menu10"><b>Visit Report By FO</b></a></li>
                            <li id="Li1" style="display:none;"><a  href="#menu7"><b> & M</b></a></li>
                           <li id="Li8" style="display:none;"><a href="#menu13"><b>Current Attendance</b></a></li>
                           <li id="Li9"><a href="#menu14"><b>Summary Report</b></a></li>
                            <li id="Li2" runat="server"><a href="#menu15"><b>Raise Complaint</b></a></li>
                           <li id="Li15"><a data-toggle="tab" href="#item16">Fire Extinguisher Info</a></li>
                        </ul>
                           
                           <div id="menu16" >
                               <div class="row">
                                      <div class="col-sm-2 col-xs-12"></div>
                                   <br /><br />
                                <div class="col-sm-2 col-xs-12">
                                     <span ><b>Attendance Type :</b></span>
                        <asp:DropDownList ID="ddl_emp_attendance" runat="server" Width="100%"
                           class="form-control text_box" OnSelectedIndexChanged="ddl_emp_attendance_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                            <asp:ListItem Value="1" >Mobile App Attendance</asp:ListItem>
                            <asp:ListItem Value="2" >Tab Attendance</asp:ListItem>
                            <asp:ListItem Value="3" >Desk Attendance</asp:ListItem>
                            <asp:ListItem Value="4" >Employee Tracking</asp:ListItem>
                        </asp:DropDownList>

                                </div>
                                <div class="col-sm-2 col-xs-12">
                                     <span > <b>Month Type :</b></span>
                        <asp:DropDownList ID="ddl_cur_month" runat="server" Width="100%"
                             class="form-control text_box" OnSelectedIndexChanged="ddl_emp_attendance_SelectedIndexChanged" onchange="return month_check()">
                            <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                            <asp:ListItem Value="1" >Current Month</asp:ListItem>
                            <asp:ListItem Value="2" >Monthwise</asp:ListItem>
                           
                        </asp:DropDownList>


                                </div>
                                <div class="col-sm-2 col-xs-12 monthwise" style ="display:none">
                           <b> Form Date:</b><span style="color: red">*</span>
                            <asp:TextBox runat="server" ID="txt_satrtdate1" CssClass="form-control date-picker13"></asp:TextBox>
                         </div>

                                <div class="col-sm-2 col-xs-12 monthwise" style ="display:none">
                            <b>To Date:</b><span style="color: red">*</span>
                            <asp:TextBox runat="server" ID="txt_enddate1" CssClass="form-control date-picker13"></asp:TextBox>
                        </div>

                              <div class="col-sm-2 col-xs-12">
                                     <span ><b>Attendance:</b></span>
                        <asp:DropDownList ID="ddl_get_attendace" runat="server" Width="100%"
                           class="form-control text_box" OnSelectedIndexChanged="ddl_emp_attendance_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                            <asp:ListItem Value="1" >Present</asp:ListItem>
                            <asp:ListItem Value="2" >Absent</asp:ListItem>
                            
                        </asp:DropDownList>

                                </div>

                        <div class="col-sm-2 col-xs-12" style="margin-top: 1.5em;">
                            <asp:Button ID="btn_show_emp" runat="server" CssClass="btn btn-primary" Text="Show"  OnClick="btn_show_emp_Click" OnClientClick="return attendance_validation();"></asp:Button>
                           <%-- <asp:Button ID="Button14" runat="server"  CssClass="btn btn-danger" Text="Close"></asp:Button>--%>
                        </div>
                            </div>
                               <br />
                                 <div class="container-fluid">
                                <asp:panel ID="Panel12" runat="server" CssClass="grid-view" Style="color:black">
                                            <asp:GridView ID="GradeGridView" class="table" runat="server" Font-Size="X-Small" Width="100%"
                                                AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="GradeGridView_RowDataBound1" OnPreRender="GradeGridView_PreRender">
                                                <RowStyle ForeColor="#000066" />
                                                <Columns>
                                                   <asp:TemplateField HeaderText="Sr No.">
                                                        <ItemStyle Width="20px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="BRANCH NAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_unit_name" runat="server" Text='<%# Eval("UNIT_NAME") %>'></asp:Label>
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
                                </asp:panel>
                                  </div>

                                 <asp:Panel ID="Panel13" runat="server" CssClass="grid-view" ScrollBars="Auto">
                  
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
                            <asp:BoundField DataField="grade_desc" HeaderText="Designation"
                                SortExpression="grade_desc" />
                            <asp:BoundField DataField="shifttime" HeaderText="Shift Time"
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

                                 <asp:Panel ID="Panel4" runat="server" CssClass="grid-view">
                             <asp:GridView ID="GridView_employee_tracking" class="table" runat="server" Font-Size="X-Small" Width="100%"
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" OnPreRender="GridView_employee_tracking_PreRender" AutoPostBack="true"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="GridView_employee_tracking_SelectedIndexChanged"  OnRowDataBound="GridView_employee_tracking_RowDataBound">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr No.">
                                <ItemStyle Width="20px" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                              
                             <asp:BoundField HeaderText="ID" DataField="id" SortExpression="id" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
                             <asp:BoundField HeaderText="Branch Name" DataField="unit_name" SortExpression="unit_name" />
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

                         </div>


                            <div id="menu11">
                 <%--  <div class="row">
                       <div class="col-sm-4 col-xs-12"></div>
                          <div class="col-sm-2 col-xs-12">
                        From Date :
                                        <asp:TextBox ID="txt_emp_from" runat="server" class="form-control date-picker1"></asp:TextBox>
                    </div>

                    <div class="col-sm-2 col-xs-12">
                        To Date :
                                        <asp:TextBox ID="txt_emp_to" runat="server" class="form-control date-picker2"></asp:TextBox>
                    </div>
                             <div class="col-sm-2 col-xs-12">
                                 <br />
                        <asp:Button ID="btn_show_tracking" runat="server" Text="Show" class="btn btn-primary" OnClick="btn_show_tracking_Click" OnClientClick="return emp_curreent_location();" />

                                  </div>
                             </div>--%>
                     <%--  <br />--%>
                      <%--  <asp:Panel ID="Panel4" runat="server" CssClass="grid-view">
                             <asp:GridView ID="GridView_employee_tracking" class="table" runat="server" Font-Size="X-Small" Width="100%"
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" OnPreRender="GridView_employee_tracking_PreRender" AutoPostBack="true"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="GridView_employee_tracking_SelectedIndexChanged"  OnRowDataBound="GridView_employee_tracking_RowDataBound">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr No.">
                                <ItemStyle Width="20px" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                              
                             <asp:BoundField HeaderText="ID" DataField="id" SortExpression="id" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
                             <asp:BoundField HeaderText="Branch Name" DataField="unit_name" SortExpression="unit_name" />
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
                             </asp:Panel>--%>
                      </div>

                            <div id="menu12">
                          <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                              <ContentTemplate>
                                     <%--   <div class="row">
                        <div id="Div1"></div>
                       
          


           <%-- <div class="col-sm-2 col-xs-12">
                        Client Name :
   <asp:DropDownList ID="ddl_client" DataValueField="client_code" DataTextField="client_name"  AutoPostBack="true" runat="server" CssClass="form-control">
   </asp:DropDownList>
                    </div>--%>

     <%-- <div class="col-sm-2 col-xs-12 ">
                        Branch Name :
   <asp:DropDownList ID="" runat="server" class="form-control">
   </asp:DropDownList>
          </div>--%>
                                <%--<div class="col-sm-4 col-xs-12"></div>--%>
<%-- <div class="col-sm-2 col-xs-12">
                            Form Date:<span style="color: red">*</span>
                            <asp:TextBox runat="server" ID="txt_fromdate" CssClass="form-control date-picker13"></asp:TextBox>
 </div>--%>

                      <%--  <div class="col-sm-2 col-xs-12">
                            To Date:<span style="color: red">*</span>
                            <asp:TextBox runat="server" ID="txt_todate" CssClass="form-control date-picker13"></asp:TextBox>
                        </div>--%>
                  <%--      <div class="col-sm-2 col-xs-12" style="margin-top: 1.5em;">
                            <asp:Button ID="Button13" runat="server" CssClass="btn btn-primary" Text="Show" OnClientClick="return Req_validation();" OnClick="tab_attendaces_details_click"></asp:Button>
                           <%-- <asp:Button ID="Button14" runat="server"  CssClass="btn btn-danger" Text="Close"></asp:Button>--%>
                    <%--    </div>
                    </div>
                          <br />
                            <div >--%>
                         <%--   <asp:Panel ID="Panel13" runat="server" CssClass="grid-view" ScrollBars="Auto">
                  
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
                            <asp:BoundField DataField="grade_desc" HeaderText="Designation"
                                SortExpression="grade_desc" />
                            <asp:BoundField DataField="shifttime" HeaderText="Shift Time"
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
                </asp:Panel>--%>
                                      <%-- </div>--%>
                              <%--</ContentTemplate>


                          </asp:UpdatePanel>--%>
                                  </div>
                      
                            <div id="menu0">
                                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>--%>
                             <%--   <div class="row">
                                    <br />
                                    <div class="col-sm-4 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12">
                                               <span style="color:black"> Date :</span><span style="color:red">*</span> 
                                        <asp:TextBox ID="txt_satrtdate" runat="server" class="form-control date-picker1"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                             <span style="color:black">   To Date :</span><span style="color:red">*</span> 
                                        <asp:TextBox ID="txt_enddate" runat="server" class="form-control date-picker2"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 col-xs-12" style="margin-top: 17px;">
                                                <asp:Button ID="Button11" runat="server" Text="Show" class="btn btn-primary" OnClick="Button3_Click" OnClientClick="return rq_validation();" />
                                            </div>
                                    </div>
                                <br />--%>
                            <%--  <div class="container-fluid">
                                <asp:panel ID="Panel12" runat="server" CssClass="grid-view" Style="color:black">
                                            <asp:GridView ID="GradeGridView" class="table" runat="server" Font-Size="X-Small" Width="100%"
                                                AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="GradeGridView_RowDataBound1" OnPreRender="GradeGridView_PreRender">
                                                <RowStyle ForeColor="#000066" />
                                                <Columns>
                                                   <asp:TemplateField HeaderText="Sr No.">
                                                        <ItemStyle Width="20px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="BRANCH NAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_unit_name" runat="server" Text='<%# Eval("UNIT_NAME") %>'></asp:Label>
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
                              <%--  </asp:panel>
                                  </div>--%>
                            </div>
                               
                            <div id="menu9"  style="color:black">
                              <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>--%>
                                        <br />
                                        <div class="row">
                                             <div class="col-sm-4 col-xs-12"></div>
                                            <div class="col-sm-2 col-xs-12">
                                                <br />
                                             <b>   From Date :</b><span style="color:red">*</span> 
                                        <asp:TextBox ID="txt_work_img_from" runat="server" class="form-control date-picker1"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <br />
                                               <b> To Date :</b><span style="color:red">*</span> 
                                        <asp:TextBox ID="txt_work_img_to" runat="server" class="form-control date-picker2"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 col-xs-12" style="margin-top:17px;">
                                                <br />
                                                <asp:Button ID="btn_work_image" runat="server" Text="Show" class="btn btn-primary" OnClientClick="return r_validation();"  OnClick="btn_work_image_Click"/>
                                            </div>
                                            </div>
                                        <br />
                                        <asp:Panel ID="Panel5" runat="server" CssClass="grid-view">
                                            <div class="container">
                                                <div class="row">
                                                     <div class="col-sm-3 col-xs-12"></div>
                                                    <div class="col-sm-6 col-xs-12">
                                                        <div class="grid-view">
                                             <asp:GridView ID="grd_work_image" class="table" runat="server" Font-Size="X-Small"
                                                AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" OnRowDataBound="GradeGridView_RowDataBound" OnPreRender="grd_work_image_PreRender1">
                                                <RowStyle ForeColor="#000066" />
                                                <Columns>
                                                      <asp:TemplateField HeaderText="Sr No.">
                                                        <ItemStyle />
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="BRANCH NAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_unit_name" runat="server" Text='<%# Eval("UNIT_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="NAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_emp_name" runat="server" Text='<%# Eval("EMP_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="WORK TIME ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_camera_outtime" runat="server" Text='<%# Eval("datecurrent") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WORK TIME IMAGE">
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
                                                        </div>
                                                </div>
                                                </div>
                                        </asp:Panel>
                                  <%--  </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </div>

                            <div id="menu1"  style="color:black">
                               <%-- <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:Panel ID="Panel6" runat="server">
                                                    <asp:Panel ID="Panel8" runat="server" CssClass="grid">
                                                        <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                            
                                                            AutoGenerateColumns="False" Width="100%" OnPreRender="gv_itemslist_PreRender">
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EFF3FB" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                                            <Columns>
                                                                 <asp:TemplateField HeaderText="Sr No.">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                                <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                                            <asp:BoundField DataField="DESIGNATION" HeaderText="Designation" SortExpression="DESIGNATION" />
                                                            <asp:BoundField DataField="COUNT" HeaderText="Employee Count" SortExpression="COUNT" />
                                                            <asp:BoundField DataField="Hours" HeaderText="Working Hours" SortExpression="Hours" />
                                                              <asp:BoundField DataField="unit_start_date" HeaderText="START DATE" SortExpression="unit_start_date" />
                                                            <asp:BoundField DataField="unit_end_date" HeaderText="END DATE" SortExpression="unit_end_date" />
                                                        
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                   <%-- </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </div>

                            <div id="menu3"  style="color:black">
                                <br />

                                <div class="row">
                                    <div class="col-sm-2 col-xs-12"></div>
                                    <div class="col-sm-8 col-xs-12">
                                        <table class="table table-bordered">
                                            <tr style="background-color: #337ab7; text-align: center; font-weight: bold;color:white">
                                                <td></td>
                                                <td>Operation</td>
                                                <td>Finance</td>
                                                <td>Location</td>
                                                <td>Admin Head</td>
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
                                                    <asp:TextBox ID="txt_itemhead" onkeypress="return AllowAlphabet_Number_slash(event);" runat="server" class="form-control text_box" placeholder="Contact Person Name"></asp:TextBox></td>
                                           
                                                 <td>
                                                   
                                                     <asp:TextBox ID="txt_othername" onkeypress="return AllowAlphabet_Number_slash(event);" runat="server" class="form-control text_box" placeholder="Contact Person Name"></asp:TextBox></td>
                                            </tr>
                                            <tr style="text-align: center; font-weight: bold;">
                                                <td><b>Mobile No</b></td>
                                                <td>
                                                    <asp:TextBox ID="txt_omobileno" runat="server" CausesValidation="true" onkeypress="return isNumber_dot(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_fmobileno" runat="server" class="form-control text_box" onkeypress="return isNumber_dot(event)" MaxLength="10" placeholder="Mobile No"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_lmobileno" runat="server"  onkeypress="return isNumber_dot(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>
                                                 <td>
                                                    <asp:TextBox ID="txt_itmmbl" runat="server"  onkeypress="return isNumber_dot(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>
                                               
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
                                                    <asp:TextBox ID="txt_itmemail" runat="server" class="form-control text_box" placeholder="Email Id"></asp:TextBox></td>
                                                
                                                  <td>
                                                    <asp:TextBox ID="txt_otheremailid" runat="server" MaxLength="50" class="form-control text_box" placeholder="Email Id"></asp:TextBox></td>
                                            </tr>

                                        </table>

                                    </div>
                                </div>
                            </div>

                            <div id="menu4"  style="color:black">
                               <%-- <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>--%>
                                        <br />
                                <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <b>Select Month :</b><span class="text-red">*</span>
                                                <asp:TextBox ID="txt_month_year" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <br />
                                                <asp:Button ID="btn_show" runat="server" class="btn btn-primary"
                                                    Text=" SHOW " OnClientClick="return re_validation();" OnClick="btn_show_Click" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="container" style="width:100%">
                                          <asp:Panel ID="Panel7" runat="server" CssClass="grid-view" ScrollBars="Auto">
                                            
                                               <asp:GridView ID="gv_unit_attendance" runat="server" ForeColor="#333333" class="table" Width="100%" OnPreRender="gv_unit_attendance_PreRender1">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
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
                                 <%--   </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </div>

                            <div id="menu5"  style="color:black">
                              <%--  <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <br />
                                              <b>  Select Employee :</b><span style="color:red;">*</span>
                                  <asp:DropDownList ID="ddl_employee" runat="server" class="form-control text_box"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <br />
                                                <br />
                                                <asp:Button ID="btn_document_show" runat="server" class="btn btn-primary"
                                                   Text="SHOW DOCUMENTS" OnClientClick="return require_validation();" Style="margin-top: 5px;" OnClick="btn_document_show_Click"/>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-1 col-xs-12"></div>
                                            <div class="col-sm-2 col-xs-12" style="margin-top: 35px;">
                                              <b>  Photo(Passport Size) :</b>
                                                <br />
                                                <asp:Label ID="photo" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>

                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image4" runat="server" Width="100px" Height="100px" meta:resourcekey="Image13Resource1" ImageUrl="~/Images/placeholder.png" />
                                            </div>
                                             <div class="col-sm-1 col-xs-12"></div>
                                               <div class="col-sm-2 col-xs-12" style="margin-top: 35px;">
                                            <b>    Aadhar Card :</b>
                                                <br />
                                                <asp:Label ID="l_bank_upload" runat="server" Text="Employee AADHAR Card Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image2" runat="server" Width="100px" Height="100px" meta:resourcekey="Image13Resource1"  ImageUrl="~/Images/passbook.jpg" />
                                            </div>
                                        </div>
                                      <br />
                                        <br />
                                    
                                       
                                   
                                        <div class="row">
                                            <div class="col-sm-1 col-xs-12"></div>
                                            <div class="col-sm-2 col-xs-12" style="margin-top: 35px;">
                                             <b>   Police Verification Document :</b>
                                                <br />
                                                <asp:Label ID="l_Police_document" runat="server" Text="Employee Police Verification Document  Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>

                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image14" runat="server" Width="100px" Height="100px" meta:resourcekey="Image13Resource1"  ImageUrl="~/Images/certificate.jpg"  />
                                            </div>
                                              <div class="col-sm-1 col-xs-12"></div>
                                             <div class="col-sm-2 col-xs-12" style="margin-top: 35px;">
                                                <b>Address Proof :</b>
                                                <br />
                                                <asp:Label ID="address_proof" runat="server" Text="Employee Address Proof Document  Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>

                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="image15" runat="server" Width="100px" Height="100px" meta:resourcekey="Image13Resource1"  ImageUrl="~/Images/Biodata.png"  />
                                            </div>
                                        </div>
                                        <br />
                                         <div class="row">
                                            <div class="col-sm-4 col-xs-12"></div>
                                           
                                        </div>
                                        <br />
                                       
                                 <%--   </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                <div id="dialog"></div>
                            </div>

                            <div id="menu6"  style="color:black">
                                

                                                                                        
                                <div class="container" style="width: 60%; margin-right: 89px;">
                                    <br />
                                    <div class="row">
                                        <p style="font-size: 13px;">
                                            &nbsp;&nbsp; &nbsp; &nbsp;<b>We thank you for your participation for using our services</b>
                                        </p>

                                    </div>
                                </div>
                                <br />
                                   
                                <div class="row">
                                    <div>

                                        <p class="text-center" style="font-size: 14px;">
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
                                        <br />
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
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-2"></div>
                                        <div class="col-sm-3 col-xs-12">
                                            <p style="font-size: 13px;margin-left:165px"><b>Suggestion :</b></p>
                                        </div>
                                        <div class="col-sm-4 col-xs-12">
                                            <asp:TextBox ID="txtfeed" runat="server" TextMode="multiline" Columns="6" Rows="2" Width="360px" class="form-control text_box" onkeypress="return AllowAlphabet_address(event)"
                                                Visible="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 col-xs-12">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return copy_add();" class="btn btn-primary" Style="margin-top: 14px;margin-left:-40px" OnClick="btn_submit" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-2"></div>
                                        <div class="col-sm-3 col-xs-12">
                                            <p style="font-size: 13px;margin-left:165px"><b>Attach File :</b></p>
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="font-size: 13px; margin-top: 4px;">
                                            <asp:FileUpload ID="FileUpload1" runat="server" />

                                        </div>
                                    </div>
                                    <br />

                                </div>
                               
                            </div>

                            <div id="menu7"  style="color:black">

                                <div class="container" style="margin-left: 109px">
                                    <div class="row">
                                        <br />
                            <div class="col-sm-2 col-xs-12">
                               <b> Services :</b><span class="text-red"> *</span>
                                       
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
                                <b>Category :</b><span class="text-red"> *</span>
                                <asp:DropDownList ID="ddl_category" runat="server" class="form-control">
                                </asp:DropDownList>
                                             <asp:LinkButton ID="lnk_add_category" Text="Add Category" runat="server" ></asp:LinkButton>
                            </div>
                                        <div class="col-sm-2 col-xs-12">
                               <b> Priority :</b><span class="text-red"> *</span>
                                <asp:DropDownList ID="department_asset" runat="server" class="form-control">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="TOP">TOP</asp:ListItem>
                                    <asp:ListItem Value="MEDIUM">MEDIUM</asp:ListItem>
                                    <asp:ListItem Value="LOW">LOW</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3 col-xs-12">
                               <b> Additional Comment :</b><span class="text-red"> *</span>
                                    <asp:TextBox ID="txt_asset_description" TextMode="multiline" Columns="5" Rows="2" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                   <div class="col-sm-3 col-xs-12"> <br />
                                            
                                       </div>
                                   <div class="col-sm-3 col-xs-12"> <br />
                                       <asp:Button ID="btn_add" runat="server" class="btn btn-primary" Text="Send Request" OnClientClick="return validation();" />
                                            <asp:Button ID="btncloseup" runat="server" class="btn btn-danger" Text="Close" />
                                        </div>
                                    </div>
                                    <br />
                                      
                                    <asp:Panel ID="Panel10" runat="server" BackColor="White" Visible="False">
                                        <asp:GridView ID="SearchGridView" class="table" Width="100%" AutoGenerateColumns="False" runat="server" DataKeyNames="Id"
                                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" ><%--OnSelectedIndexChanged="SearchGridView_SelectedIndexChanged"--%>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                            <Columns>
                                               
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div id="menu8"  style="color:black">
                                <br />
                                  <div class="row">
                                     <div class="col-sm-4 col-xs-12"></div>
                                    <div class="col-sm-2 col-xs-12">
                                      <b>  Month :</b>
                                        <%--<asp:TextBox runat="server" ID="txt_file_month" CssClass="form-control date-picker"></asp:TextBox>--%>
                                        <asp:TextBox ID="txt_file_month" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                    </div> 
                                     <div class="col-sm-1 col-xs-12" style="margin-top:1.5em;">
                                         <asp:Button ID="Button12" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="Button12_Click" />
                                     </div>
                                </div>
                                <br />
                                <div class="row">
                                    <br />
                                    <asp:Panel ID="Panel11" runat="server" CssClass="grid">
                                        <asp:GridView ID="grd_company_files" class="table"  Width="100%" runat="server" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            AutoGenerateColumns="False" >
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
                                               
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div id="menu10"  style="color:black">
                                <br />
                               <%-- <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>

                                 

                                 <div class="row">
                                    
                          <div class="col-sm-2 col-xs-12 text-left">
                              </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                              </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                        <br />
                       <b> Supervisor Name :</b>
                            <span class="text-red" style="color: red">*</span>
                        <asp:DropDownList ID="dd1_super" class="form-control" OnSelectedIndexChanged="dd1_super_SelectedIndexChanged" runat="server" AutoPostBack="true" >
                        </asp:DropDownList>
                    </div>

                                              <div class="col-sm-2 col-xs-12 text-left">
                                                   <br />
                                <b>   Name  </b> 
                                  <span class="text-red" style="color: red">*</span>
                        <asp:DropDownList ID="ddl_feedback_type" class="form-control" Width="100%"  runat="server" >
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                     <asp:ListItem Value="1">Site Audit</asp:ListItem>
                                     <asp:ListItem Value="2">Supervisor Through Feedback</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                                      

                    <div class="col-sm-2 col-xs-12 text-left" style="margin-top: 17px;">
                        <br />
                        <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text=" Show " OnClick="btn_show_Click1" />
                       


                    </div>
                </div>
                     <br />
                     <br />             
                                 
             
                  <div class="container" style="width: 100%">

   <asp:Panel ID="panelc" runat="server">
                <asp:GridView ID="companyGridView1" class="table" HeaderStyle-CssClass="FixedHeader" runat="server"
                    AutoGenerateColumns="False" CellPadding="1" Font-Size="X-Small" ForeColor="#333333" DataKeyNames="id" OnRowDataBound="companyGridView1_RowDataBound" OnPreRender="companyGridView1_PreRender">

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
                        <asp:BoundField DataField="ID" HeaderText="ID"
                            SortExpression="ID" />
                        <asp:BoundField DataField="unit_name" HeaderText="Branch Name"
                            SortExpression="unit_name" />

                        <asp:BoundField DataField="que_1_ans" HeaderText="Question_1"
                            SortExpression="que_1_ans" />
                        <asp:BoundField DataField="que_2_ans" HeaderText="Question_2"
                            SortExpression="que_2_ans" />
                        <asp:BoundField DataField="que_3_ans" HeaderText="Question_3"
                            SortExpression="que_3_ans" />
                        <asp:BoundField DataField="que_4_ans" HeaderText="Question_4"
                            SortExpression="que_4_ans" />
                        <asp:BoundField DataField="que_5_ans" HeaderText="Question_5"
                            SortExpression="que_5_ans" />
                        <asp:BoundField DataField="que_6_ans" HeaderText="Question_6"
                            SortExpression="que_6_ans" />
                        <asp:BoundField DataField="que_7_ans" HeaderText="Question_7"
                            SortExpression="que_7_ans" />
                        <asp:BoundField DataField="que_8_ans" HeaderText="Question_8"
                            SortExpression="que_8_ans" />
                        <asp:BoundField DataField="que_9_ans" HeaderText="Question_9"
                            SortExpression="que_9_ans" />
                        <asp:BoundField DataField="que_10_ans" HeaderText="Question_10"
                            SortExpression="que_10_ans" />
                          <asp:BoundField DataField="remark" HeaderText="Remark"
                            SortExpression="remark" />
                           <asp:BoundField DataField="Status" HeaderText="Status"
                            SortExpression="Status" />
                         <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>

                                <asp:LinkButton ID="lnkbtn_edititem" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Approved" OnClientClick="return R_validation();" ></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Reject"  OnClientClick="return R_validation1();"></asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtn_complete1" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Complete"  OnClientClick="return R_validation2();"></asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </asp:Panel>


                <asp:Panel ID="Panel14" runat="server" CssClass="grid-view" Style="overflow-x:auto;">

                    <%-- <div class="row">
                     <div class="col-sm-10 col-xs-12"></div>
                       <div class="col-sm-2 col-xs-12">
                        Search :
                        <asp:TextBox runat="server" ID="txt_search" CssClass=" form-control" onkeyup="Search_Gridview(this)" />
                           </div>
                        </div> 
                             <br />--%>

                    <asp:GridView ID="companyGridView" class="table" HeaderStyle-CssClass="FixedHeader"   runat="server"
                        AutoGenerateColumns="False" CellPadding="1" Font-Size="X-Small" ForeColor="#333333" DataKeyNames="id"
                        OnRowDataBound="companyGridView_RowDataBound" OnPreRender="companyGridView_PreRender1">
                      

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
                            <asp:BoundField DataField="unit_name" HeaderText="Branch Name"
                            SortExpression="unit_name" />

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

                     <%-- 10-08-19 komal--%>
                     
            </div>

 <%--  </ContentTemplate>

                                </asp:UpdatePanel>--%>
                             </div>

                            <div id="menu13"  style="color:black">
                              <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <br />
                                                <br />
                                               <b> Select Month :</b><span class="text-red">*</span>
                                                <asp:TextBox ID="txt_month" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <br />
                                                <asp:Button ID="btn_show_summary" runat="server" class="btn btn-primary"
                                                    Text=" SHOW " OnClick="btn_show_summary_Click" />
                                                <%-- OnClientClick="return re_validation();"--%>
                                            </div>
                                        </div>
                                        <br />
                              <div class="container" style="width:100%">
                                          <asp:Panel ID="Panel1" runat="server" CssClass="grid-view" ScrollBars="Auto">
                                            
                                               <asp:GridView ID="gv_summary_report" runat="server" ForeColor="#333333" class="table" Width="100%" OnPreRender="gv_summary_report_PreRender">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
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

                            <div id="menu14"  style="color:black">
                          <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <br />
                                                <br />
                                                <b>Select Month :</b><span class="text-red">*</span>
                                                <asp:TextBox ID="txt_month_report" CssClass="form-control date-picker13" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <br />
                                                <br />
                                                <br />
                                                <asp:Button ID="btn_show_report" runat="server" class="btn btn-primary"
                                                    Text=" SHOW " OnClick="btn_show_report_Click" />
                                                <%-- OnClientClick="return re_validation();"--%>
                                            </div>
                                        </div>

                                        <br />
                        <%--  <div class="container" style="width:100%">
                                          <asp:Panel ID="Panel16" runat="server" CssClass="grid-view" ScrollBars="Auto">
                                            
                                               <asp:GridView ID="gv_report" runat="server" ForeColor="#333333" class="table" Width="100%" OnPreRender="gv_report_PreRender">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                                                    </asp:GridView>
                                        </asp:Panel>
                                             </div>--%>
                         </div>

                            <div id="menu15" >
                              <div class="container-fluid">
                                                <asp:Panel runat="server" ID="panel212" CssClass="panel panel-primary" Style="border: gray; background: rgba(255, 255, 255, 0.58); border-radius: 10px">
                                                    <div class="container-fluid">
                                                        <div class="row">
                                                            <br />

                                                            <div class="col-sm-2 col-xs-12 text-left">
                                                                <span style="color: black"><b>Priority Level:</b></span>

                                                                <asp:DropDownList ID="ddl_priority" class="form-control" runat="server" OnSelectedIndexChanged="ddl_priority_SelectedIndexChanged" AutoPostBack="true">
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
                                                                <div class="col-sm-3 col-xs-12">
                                                                    <input id="Hidden" type="hidden" runat="server" />
                                                                    <span style="color: black">Remark :</span>

                                                                    <asp:TextBox ID="txt_remark" class="form-control" TextMode="MultiLine" runat="server" onkeypress="return AllowAlphabet_address(event);"> </asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 col-xs-12" style="margin-top: 3em;">
                                                                    <asp:Button ID="btn_send" runat="server" CssClass="btn btn-primary" Text="Register Complaint" OnClick="btn_send_Click" OnClientClick="return validateCheckBoxes();" />

                                                                </div>
                                                            </asp:Panel>
                                                            <%--<div class="col-sm-3 col-xs-12 block">
                                                <asp:CheckBoxList ID="ChecComplaints" runat="server" Font-Size="X-Small">
                                                </asp:CheckBoxList>
                                            </div>--%>
                                                            <br />

                                                        </div>
                                                    
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-sm-4 col-xs-12"></div>
                                                              <div class="col-sm-4 col-xs-12">
                                                                    <asp:Panel runat="server" CssClass="grid-view" ID="pan">
                                                                        <asp:GridView ID="gv_complaint" runat="server" AutoGenerateColumns="false" ForeColor="#333333" class="table" GridLines="Both" >
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
                                                                                      <%--  <asp:CheckBox ID="ChecComplaints" runat="server" />--%>
                                                                                        <%--<asp:Label ID="Label1" runat="server" Text="SR.NO"></asp:Label>--%>
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
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                             <div class="container-fluid">
                                                <asp:Panel ID="Panel16" runat="server" Visible="False" meta:resourcekey="Panel5Resource1" Style="color: black; overflow-x:auto; overflow-y:auto" CssClass="grid-view">

                                                    <asp:GridView ID="unitcomplaintGridView1" class="table" Width="100%" AutoGenerateColumns="False" DataKeyNames="id" runat="server"
                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="unitcomplaintGridView1_RowDataBound"
                                                        CellPadding="3" meta:resourcekey="SearchGridViewResource1" OnPreRender="unitcomplaintGridView1_PreRender">
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
                                                             <asp:BoundField DataField="client_name" HeaderText="client name" SortExpression="client_name" />
                                                             <asp:BoundField DataField="ZONE" HeaderText="REGION" SortExpression="ZONE" />
                                                             <asp:BoundField DataField="state_name" HeaderText="state" SortExpression="state_name" />
                                                             <asp:BoundField DataField="unit_name" HeaderText="Branch name" SortExpression="unit_name" />
                                                            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                                             <asp:BoundField DataField="priority" HeaderText="Priority" SortExpression="priority" />
                                                            <asp:BoundField DataField="complaint_name" HeaderText="Type Of Complaint" SortExpression="complaint_name" />
                                                            <asp:BoundField DataField="date" HeaderText="Complaint Raised Date" SortExpression="date" />
                                                            <asp:BoundField DataField="resole_date" HeaderText="Complaint Resolved Date" SortExpression="resole_date" />
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbl_status" runat="server" ReadOnly="true" Text='<%# Eval("status") %>'></asp:TextBox>
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
                         </div> 
                        <br />


                           <div id="item16" class="tab-pane">
                                             <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                 <ContentTemplate>
                                                      
                                                     <div class="row" >

                                <asp:Panel ID="Panel26" runat="server" CssClass="grid-view">
                                    <asp:GridView ID="gridview_fire_extinguisher" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="#CCCCCC" OnPreRender="gridview_fire_extinguisher_PreRender" OnRowDataBound="gridview_fire_extinguisher_RowDataBound" BorderStyle="None" BorderWidth="1px" CellPadding="3" class="table"  Width="100%">
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
                                                    <asp:LinkButton ID="lnk_remove_fire" runat="server" CausesValidation="false"  OnClientClick="return confirm('Are you sure You want to  Delete ?') " ><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemStyle Width="20px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_srnumber" runat="server" Text="<%# Container.DataItemIndex+1 %>" Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
                                             <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
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

                                            <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                            <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                            
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                         

                                                         </div>
                                                        
                                                           </ContentTemplate>
                                            </asp:UpdatePanel>
                                      </div>
                   
                     </div>
                           </div>
                     
                </div>
                <br />
            </asp:Panel>
            <br />
            </div>
    </form>
</body>
</html>