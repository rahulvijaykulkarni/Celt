<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Region_Login.aspx.cs" Inherits="Region_Login" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
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
            margin-left: -15px;
        }
    </style>
    <script type="text/javascript">
      
        function pageLoad() {
            
            $('#<%=Button13.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
             $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=GridView_employee_tracking_region.ClientID%>').DataTable({
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
               .appendTo('#<%=GridView_employee_tracking_region.ClientID%>_wrapper .col-sm-6:eq(0)');


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
                     'colvis'
                 ]

             });

             table.buttons().container()
                .appendTo('#<%=grd_work_image.ClientID%>_wrapper .col-sm-6:eq(0)');

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
    
            $(".date-picker20").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
              
            });
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
              $(".date-picker20").attr("readonly", "true");
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
        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });
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
        
        $(function () {

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
        });
        function emp_curreent_location() {
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
            background-color: #3c74a4;
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

        .grid-view {
            max-height: 450px;
            height: auto;
            overflow-x: hidden;
            overflow-y: auto;
        }
    </style>

    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <br />
            <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary" Style="background-color: #edeefe; border: gray;">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-sm-3">
                            <img src="Images/logo.png" style="width: inherit; margin-top: -6px;" />
                        </div>
                        <div class="col-sm-5">
                            <div style="text-align: center; font-size: large;"><b>Welcome To IH&MS</b></div>
                        </div>
                        <div class="col-sm-1" style="margin-top: 5px;">
                            <asp:Label ID="Label1" runat="server" Text="Login :" Font-Size="Small"> </asp:Label>
                        </div>

                        <div class="col-sm-2">
                            <span>
                                <asp:Label ID="txt_usernam1" BackColor="#337ab7" ForeColor="White" class="bg-primary" runat="server" CssClass="form-control" />
                            </span>
                        </div>
                        <div class="col-sm-20 text-right" style="margin-left: 100px;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                                <asp:Button ID="Button10" runat="server" OnClick="LinkButton1_Click" class="btn btn-primary" Text="LogOut" Style="font-weight: bold" />
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="card acik-renk-form" style="height: 19em; border-radius: 10px;">
                        <div class="row">
                            <br />
                            <div class="col-sm-2 col-xs-12"></div>
                            <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                            <div class="col-sm-2 col-xs-12">
                                <span style="font-weight: bold">Branch Name :</span>
                                <asp:DropDownList ID="ddlunitselect" runat="server" class="form-control" OnSelectedIndexChanged="UnitGridView_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>


                            <div class="col-sm-2 col-xs-12">
                                <span style="font-weight: bold">State :</span><%--<span class="text-red"> *</span>--%><%--<asp:DropDownList ID="ddl_state" runat="server" Width="100%"
                                         class="form-control text_box">
                                    </asp:DropDownList>--%>
                                <asp:TextBox ID="txt_state" runat="server" Width="100%" Rows="2" class="form-control text_box" MaxLength="50"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <span style="font-weight: bold">City :</span><%--<span class="text-red"> *</span>--%><%--<asp:DropDownList ID="txtunitcity" runat="server" class="form-control text_box" Width="100%"></asp:DropDownList>--%>
                                <asp:TextBox ID="txtunitcity" runat="server" Width="100%" Rows="2" class="form-control text_box" MaxLength="50"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <span style="font-weight: bold">Location(Place/City) :</span><%--<span class="text-red"> *</span>--%><asp:TextBox ID="txtunitaddress1" runat="server" class="form-control text_box "></asp:TextBox>
                            </div>
                            <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        </div>
                        <br />

                        <div class="row">
                            <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>--%>
                            <div class="col-sm-2 col-xs-12"></div>
                            <div class="col-sm-4 col-xs-15">
                                <span style="font-weight: bold">Address(Street/Road/Lane) :</span>
                                <asp:TextBox ID="txtunitaddress2" runat="server" TextMode="multiline" Width="100%" Rows="2" class="form-control text_box" MaxLength="50"></asp:TextBox>
                            </div>
                            <%--<div class="col-sm-2 col-xs-12">
                        GSTIN No :<span class="text-red"> *</span>
                            <asp:TextBox ID="txt_gst_no" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event,this);" Width="100%" MaxLength="20"></asp:TextBox>

                    </div>--%>
                            <div class="col-sm-2 col-xs-12">
                                <span style="font-weight: bold">Zone :</span>
                                <asp:TextBox ID="txt_zone1" runat="server" class="form-control text_box"></asp:TextBox>
                                <%--  <asp:DropDownList ID="txt_zone1" runat="server" Width="100%"
                            DataValueField="ZONE" class="form-control text_box">
                            <asp:ListItem Value="Select" Text="Select">Select</asp:ListItem>
                            <asp:ListItem Value="East" Text="East">East</asp:ListItem>
                            <asp:ListItem Value="West" Text="West">West</asp:ListItem>
                            <asp:ListItem Value="North" Text="North">North</asp:ListItem>
                            <asp:ListItem Value="South" Text="South">South</asp:ListItem>
                        </asp:DropDownList>--%>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <span style="font-weight: bold">Region Name :</span>
                                <%--<asp:DropDownList ID="txt_zone" runat="server" Width="100%" class="form-control text_box">
                            <asp:ListItem>Select</asp:ListItem>
                        </asp:DropDownList>--%>
                                <asp:TextBox ID="txt_zone" runat="server" Width="100%" Rows="2" class="form-control text_box" MaxLength="50"></asp:TextBox>
                            </div>
                            <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
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

                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2" TargetControlID="lnk_add_category"
                                CancelControlID="Button4" BackgroundCssClass="Background">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel2" runat="server" CssClass="Popup" Style="display: none">
                                <iframe style="width: 800px; height: 500px; background-color: #fff;" id="Iframe1" src="service_category.aspx" runat="server"></iframe>
                                <div class="row text-center">
                                    <asp:Button ID="Button4" CssClass="btn btn-danger" OnClientClick="callfnc()" runat="server" Text="Close" />
                                </div>

                                <br />

                            </asp:Panel>
                        </div>

                        <div class="row text-center">
                            <%--Please dont delete this 3 Buttons...Vinod --%>
                            <asp:Button ID="Button6" runat="server" CssClass="hidden" />
                            <asp:Button ID="Button7" runat="server" CssClass="hidden" />
                            <asp:Button ID="Button8" runat="server" CssClass="hidden" />


                            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel9" TargetControlID="Button7"
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
                        <br />
                    </div>


                    <br />
                    <br />
                    <asp:Panel runat="server" ID="tab_panel" CssClass="panel panel-primary" Style="border: none;">
                        <div id="tabs" style="border: none">
                            <asp:Panel runat="server" ID="Panel1" CssClass="panel panel-primary" Style="color: white; border: gray;">

                                <asp:Panel runat="server" ID="Panel13" Style="padding: 10px; background-color: #dddddd">

                                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />

                                    <ul style="border: none; background-color: #dddddd">
                                        <li id="tabactive5"><a href="#menu0">Attendance Photo</a></li>
                                          <li id="Li7"><a href="#menu12">Android Attendance</a></li>
                                          <li id="Li6"><a href="#menu11">Employee Current Location</a></li>
                                        <li id="Li4"><a href="#menu9">Work Photo</a></li>
                                        <li id="Li3"><a href="#menu1">Designation Details</a></li>
                                        <li id="tabactive11"><a href="#menu3">Heads Info</a></li>
                                        <li id="tab_attendance"><a href="#menu4">Monthly Attendance</a></li>
                                        <li id="tab_documents"><a href="#menu5">Employee Documents</a></li>
                                        <li id="tab_feedback"><a href="#menu6">Feedback</a></li>
                                        <li id="Li1" style="display: none"><a href="#menu7">R & M</a></li>
                                        <li id="Li2"><a href="#menu8">Files</a></li>
                                        <li id="Li5"><a href="#menu10">Visit Report By FO</a></li>
                                      

                                    </ul>
                                </asp:Panel>
                                

                                <div id="menu11">
<div class="row">
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
                        <asp:Button ID="btn_show_tracking_region" runat="server" Text="Show" class="btn btn-primary" OnClick="btn_show_tracking_region_Click" OnClientClick="return emp_curreent_location();" />

                                  </div>
</div>
                       <br />
                                    <asp:Panel runat="server" CssClass="grid-view">
                                      <asp:GridView ID="GridView_employee_tracking_region" class="table" runat="server" Font-Size="X-Small" Width="100%"
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" OnPreRender="GridView_employee_tracking_region_PreRender"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3"   OnSelectedIndexChanged="GridView_employee_tracking_region_SelectedIndexChanged" AutoPostBack="true"  OnRowDataBound ="GridView_employee_tracking_region_RowDataBound" >
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr No.">
                                <ItemStyle Width="20px" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                              
                             <asp:BoundField HeaderText="ID" DataField="id" SortExpression="id" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
                            <asp:BoundField HeaderText="Branch Name" DataField="unit_name" SortExpression="unit_name"  />
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
                                  <div id="menu12">
                                      <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                          <ContentTemplate>
                                        <div class="row">
                        <div class="col-sm-2 col-xs-12">
                           From Date:<span style="color: red">*</span>
                            <asp:TextBox runat="server" ID="txt_fromdate" CssClass="form-control date-picker20"></asp:TextBox>
                        </div>
                           <div class="col-sm-2 col-xs-12">
                           To Date:<span style="color: red">*</span>
                            <asp:TextBox runat="server" ID="txt_todate" CssClass="form-control date-picker20"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12" style="margin-top: 1.5em;">
                            <asp:Button ID="Button13" runat="server" CssClass="btn btn-primary" Text="Show" OnClick="tab_attendaces_details_click"></asp:Button>
                           
                        </div>
                    </div>
                          <br />
                            <div >
                            <asp:Panel ID="Panel14" runat="server" CssClass="grid-view">
                  
                    <asp:GridView ID="gv_emp_attendance" class="table" runat="server" BackColor="White" scrobar="auto"
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
                                <div id="menu0" class="tab-pane fade in active">
                                    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>--%>
                                    <div class="row">
                                        <br />
                                        <div class="col-sm-4 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12" style="margin-right: -72px;">
                                            <span style="color: black">Date :</span><span style="color: red">*</span>
                                            <asp:TextBox ID="txt_satrtdate" runat="server" class="form-control date-picker1" Style="width: 70%; margin-right: -72px;"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="margin-right: -67px;">
                                            <span style="color: black">To Date :</span><span style="color: red">*</span>
                                            <asp:TextBox ID="txt_enddate" runat="server" class="form-control date-picker2" Style="width: 70%; margin-right: -67px;"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="margin-top: 17px;">
                                            <asp:Button ID="Button11" runat="server" Text="Show" class="btn btn-primary" OnClick="Button3_Click" OnClientClick="return rq_validation();" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="container-fluid">
                                        <asp:Panel ID="Panel12" runat="server" CssClass="grid-view" Style="color: black">
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


                                                    <asp:TemplateField HeaderText="Branch Name">
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
                                        </asp:Panel>
                                    </div>
                                </div>

                                <div id="menu9" class="tab-pane" style="color: black">
                                    <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12" style="margin-right: -72px;">
                                            From Date :<span style="color: red">*</span>
                                            <asp:TextBox ID="txt_work_img_from" runat="server" class="form-control date-picker1" Style="width: 70%; margin-right: -72px;"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="margin-right: -67px;">
                                            To Date :<span style="color: red">*</span>
                                            <asp:TextBox ID="txt_work_img_to" runat="server" class="form-control date-picker2" Style="width: 70%; margin-right: -67px;"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="margin-top: 17px;">
                                            <asp:Button ID="btn_work_image" runat="server" Text="Show" class="btn btn-primary" OnClientClick="return r_validation();" OnClick="btn_work_image_Click" />
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Panel ID="Panel5" runat="server" CssClass="grid-view" Width="40%" Style="margin-left: 41em;">
                                        <asp:GridView ID="grd_work_image" class="table" runat="server" Font-Size="X-Small"
                                            AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="GradeGridView_RowDataBound" Width="100%" OnPreRender="grd_work_image_PreRender">
                                            <RowStyle ForeColor="#000066" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No.">
                                                    <ItemStyle Width="20px" />
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="BRANCH NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_UNIT_name" runat="server" Text='<%# Eval("UNIT_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_emp_name" runat="server" Text='<%# Eval("EMP_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DATE-TIME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_camera_outtime" runat="server" Text='<%# Eval("datecurrent") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IMAGE">
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
                                    </asp:Panel>
                                    <%--   </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                </div>
                                <div id="menu1" class="tab-pane" style="color: black">
                                    <%--   <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
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
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
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
                                                            <asp:BoundField DataField="UNIT_NAME" HeaderText="BRANCH NAME" SortExpression="UNIT_NAME" />
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
                                    <%--  </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                </div>

                                <div id="menu3" class="tab-pane" style="color: black">

                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12"></div>
                                        <div class="col-sm-8 col-xs-12">
                                            <table class="table table-bordered">
                                                <tr style="background-color: #337ab7; text-align: center; font-weight: bold; color: white">
                                                    <td>Title</td>
                                                    <td>Operation</td>
                                                    <td>Finance</td>
                                                    <td>Location</td>
                                                    <td>Admin Head</td>
                                                    <td>Other</td>
                                                </tr>
                                                <tr style="text-align: center; font-weight: bold;">
                                                    <td>Contact Person Name</td>
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
                                                    <td>Mobile No</td>
                                                    <td>
                                                        <asp:TextBox ID="txt_omobileno" runat="server" CausesValidation="true" onkeypress="return isNumber_dot(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_fmobileno" runat="server" class="form-control text_box" onkeypress="return isNumber_dot(event)" MaxLength="10" placeholder="Mobile No"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_lmobileno" runat="server" onkeypress="return isNumber_dot(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_itmmbl" runat="server" onkeypress="return isNumber_dot(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>


                                                    <td>
                                                        <asp:TextBox ID="txt_othermobno" runat="server" onkeypress="return isNumber_dot(event)" MaxLength="10" class="form-control text_box" placeholder="Mobile No"></asp:TextBox></td>
                                                </tr>
                                                <tr style="text-align: center; font-weight: bold;">
                                                    <td>Email Id</td>
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

                                <div id="menu4" class="tab-pane" style="color: black">
                                    <%-- <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>

                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12">
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            Select Month :<span class="text-red">*</span>
                                            <asp:TextBox ID="txt_month_year" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <br />
                                            <asp:Button ID="btn_show" runat="server" class="btn btn-primary"
                                                Text=" SHOW " OnClientClick="return re_validation();" OnClick="btn_show_Click" />
                                        </div>
                                    </div>
                                    <br />


                                    <asp:Panel ID="Panel7" runat="server" CssClass="grid-view" ScrollBars="Auto">

                                        <asp:GridView ID="gv_unit_attendance" runat="server" ForeColor="#333333" class="table" Width="100%">
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
                                    <%--   </ContentTemplate>
                                </asp:UpdatePanel> --%>
                                </div>
                                <div id="menu5" class="tab-pane" style="color: black">
                                    <%-- <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>--%>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12">
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            Select Employee :<span style="color: red;">*</span>
                                            <asp:DropDownList ID="ddl_employee" runat="server" class="form-control text_box"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <br />
                                            <asp:Button ID="btn_document_show" runat="server" class="btn btn-primary"
                                                Text="SHOW DOCUMENTS" OnClientClick="return require_validation();" Style="margin-top: 5px;" OnClick="btn_document_show_Click" />
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-1 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12" style="margin-top: 35px;">
                                            Photo(Passport Size) :
                                                <br />
                                            <asp:Label ID="photo" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                        </div>

                                        <div class="col-sm-3 col-xs-12">
                                            <asp:ImageButton ID="Image4" runat="server" Width="100px" Height="100px" meta:resourcekey="Image13Resource1" ImageUrl="~/Images/placeholder.png" />
                                        </div>
                                        <div class="col-sm-1 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12" style="margin-top: 35px;">
                                            Aadhar Card :
                                                <br />
                                            <asp:Label ID="l_bank_upload" runat="server" Text="Employee AADHAR Card Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            <asp:ImageButton ID="Image2" runat="server" Width="100px" Height="100px" meta:resourcekey="Image13Resource1" ImageUrl="~/Images/passbook.jpg" />
                                        </div>
                                    </div>
                                    <br />
                                    <br />



                                    <div class="row">
                                        <div class="col-sm-1 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12" style="margin-top: 35px;">
                                            Police Verification Document :
                                                <br />
                                            <asp:Label ID="l_Police_document" runat="server" Text="Employee Police Verification Document  Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                        </div>

                                        <div class="col-sm-3 col-xs-12">
                                            <asp:ImageButton ID="Image14" runat="server" Width="100px" Height="100px" meta:resourcekey="Image13Resource1" ImageUrl="~/Images/certificate.jpg" />
                                        </div>
                                        <div class="col-sm-1 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12" style="margin-top: 35px;">
                                            Address Proof :
                                                <br />
                                            <asp:Label ID="address_proof" runat="server" Text="Employee Address Proof Document  Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                        </div>

                                        <div class="col-sm-3 col-xs-12">
                                            <asp:ImageButton ID="image15" runat="server" Width="100px" Height="100px" meta:resourcekey="Image13Resource1" ImageUrl="~/Images/Biodata.png" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12"></div>

                                    </div>
                                    <br />

                                    <%--  </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                    <div id="dialog"></div>
                                </div>

                                <div id="menu6" class="tab-pane" style="color: black">

                                    <div class="container" style="width: 60%; margin-right: 89px;">
                                        <br />
                                        <div class="row">
                                            <p style="font-size: 13px;">
                                                &nbsp;&nbsp; &nbsp; &nbsp;We thank you for your participation for using our services
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
                                                                    <th bgcolor='#ADD8E6'>Values</th>
                                                                    <td>1-3</td>
                                                                    <td>4-5</td>
                                                                    <td>6-7</td>
                                                                    <td>8-9</td>
                                                                    <td>10</td>

                                                                </tr>
                                                                <tr>
                                                                    <th bgcolor='#ADD8E6'>Rating</th>
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
                                                <p style="font-size: 13px; margin-left: 165px">Suggestion :</p>
                                            </div>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txtfeed" runat="server" TextMode="multiline" Columns="6" Rows="2" Width="360px" class="form-control text_box"
                                                    Visible="true" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 col-xs-12">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return copy_add();" class="btn btn-primary" Style="margin-top: 14px; margin-left: -40px" OnClick="btn_submit" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-2"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                <p style="font-size: 13px; margin-left: 165px">Attach File :</p>
                                            </div>
                                            <div class="col-sm-2 col-xs-12" style="font-size: 13px; margin-top: 4px;">
                                                <asp:FileUpload ID="FileUpload1" runat="server" />

                                            </div>
                                        </div>
                                        <br />

                                    </div>


                                </div>

                                <div id="menu7" class="tab-pane" style="color: black">

                                    <div class="container" style="margin-left: 109px">
                                        <div class="row">

                                            <div class="col-sm-2 col-xs-12">
                                                Services :<span class="text-red"> *</span>

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
                                                Category :<span class="text-red"> *</span>
                                                <asp:DropDownList ID="ddl_category" runat="server" class="form-control">
                                                </asp:DropDownList>
                                                <asp:LinkButton ID="lnk_add_category" Text="Add Category" runat="server"></asp:LinkButton>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                Priority :<span class="text-red"> *</span>
                                                <asp:DropDownList ID="department_asset" runat="server" class="form-control">
                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Value="TOP">TOP</asp:ListItem>
                                                    <asp:ListItem Value="MEDIUM">MEDIUM</asp:ListItem>
                                                    <asp:ListItem Value="LOW">LOW</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                Additional Comment :<span class="text-red"> *</span>
                                                <asp:TextBox ID="txt_asset_description" TextMode="multiline" Columns="5" Rows="2" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <br />

                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <br />
                                                <asp:Button ID="btn_add" runat="server" class="btn btn-primary" Text="Send Request" OnClientClick="return validation();" OnClick="btn_add_Click" />
                                                <asp:Button ID="btncloseup" runat="server" class="btn btn-danger" Text="Close" />
                                            </div>
                                        </div>
                                        <br />

                                        <asp:Panel ID="Panel10" runat="server" BackColor="White" Visible="False">
                                            <asp:GridView ID="SearchGridView" class="table" Width="100%" AutoGenerateColumns="False" runat="server" DataKeyNames="Id"
                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                                <%--OnSelectedIndexChanged="SearchGridView_SelectedIndexChanged"--%>
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

                                <div id="menu8" class="tab-pane">
                                    <br />
                                    <div class="row" style="color: black">
                                        <div class="col-sm-4 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12">
                                            Month :
                                        <asp:TextBox runat="server" ID="txt_file_month" CssClass="form-control date-picker12"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 col-xs-12" style="margin-top: 1.5em;">
                                            <asp:Button ID="Button12" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="Button12_Click" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <asp:Panel ID="Panel11" runat="server" CssClass="grid">
                                            <asp:GridView ID="grd_company_files" class="table" Width="100%" runat="server" BackColor="White"
                                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                AutoGenerateColumns="False">
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

                                <div id="menu10" class="tab-pane" style="color: black; border: none;">
                                    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>

                                    <div class="row">

                                        <div class="col-sm-4 col-xs-12 text-left"></div>
                                        <div class="col-sm-2 col-xs-12 text-left">
                                            Supervisor Name :
                           
                                                     <asp:DropDownList ID="dd1_super" class="form-control" runat="server">
                                                     </asp:DropDownList>
                                        </div>

                                        <div class="col-sm-2 col-xs-12 text-left" style="margin-top: 17px;">
                                            <asp:Button ID="Button3" runat="server" class="btn btn-primary" OnClick="btn_show_Click1" Text=" Show " />



                                        </div>

                                    </div>


                                    <div class="container" style="width: 100%">


                                        <asp:Panel ID="Panel4" runat="server" CssClass="grid-view" Style="overflow-x: auto;">

                                            <br />

                                            <asp:GridView ID="companyGridView" class="table" HeaderStyle-CssClass="FixedHeader" runat="server"
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

                                    <%--  </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                </div>
                            </asp:Panel>
                        </div>
                        <br />
                    </asp:Panel>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
