<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeInfo1.aspx.cs" Inherits="EmployeeInfo1" Title="Employee Master" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" EnableEventValidation="false" %>

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
    <script src="js/select2.min.js"></script>
    <link href="css/select2.min.css" rel="stylesheet" />
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <%--        <script src="datatable/jszip.min.js"></script>--%>
    <%--    <script src="datatable/pdfmake.min.js"></script>--%>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>
    <script src="datatable/pdfmake.min.js"></script>

    <script type="text/javascript">

    </script>


    <style type="text/css">
        .tab-section {
            background-color: #fff;
        }
    </style>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <script>


        $(function () {

            $('#<%=btnUpload.ClientID%>').click(function () {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });

        });
    </script>
    <style>
        .table th {
            text-align: center;
            border: 2px solid #000;
        }

        .form-control {
            display: inline;
        }

        .grid-view {
            height: auto;
            max-height: 400px;
            overflow-y: auto;
            overflow-x: hidden;
        }

        .row {
            margin: 0px;
        }

        .col-sm-2 {
            text-align: right;
        }

        .col-sm-4 {
            text-align: right;
        }
    </style>
    <script>

        function copy_add() {

            var add1 = document.getElementById("<%= txt_presentaddress.ClientID %>");
            var add2 = document.getElementById("<%= txt_permanantaddress.ClientID %>");

            add2.value = add1.value;

            var state1 = document.getElementById("<%= ddl_state.ClientID %>");
            //var text = sel.options[sel.selectedIndex].value;

            var state2 = document.getElementById("<%= ddl_permstate.ClientID %>");
            //   var text1 = sel.options[sel.selectedIndex].value;

            state2.value = state1.value;

            var city1 = document.getElementById("<%= txt_presentcity.ClientID %>");
            //var text = sel.options[sel.selectedIndex].value;

            var city2 = document.getElementById("<%= txt_permanantcity.ClientID %>");
            //   var text1 = sel.options[sel.selectedIndex].value;

            city2.value = city1.value;

            //pincode
            var pin1 = document.getElementById("<%= txt_presentpincode.ClientID %>");
            var pin2 = document.getElementById("<%= txt_permanantpincode.ClientID %>");

            pin2.value = pin1.value;

            //mobile no
            var mobile1 = document.getElementById("<%= txt_mobilenumber.ClientID %>");
            var mobile2 = document.getElementById("<%= txtref2mob.ClientID %>");

            mobile2.value = mobile1.value;





        }



    </script>
    <script type="text/javascript">
        function pageLoad() {

            var txt_left_date = document.getElementById('<%=txt_leftdate.ClientID %>');
             var txt_earning_issues = document.getElementById('<%=txt_highestqualification.ClientID %>');

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

             $(".confirm_date").datepicker({
                 changeMonth: true,
                 changeYear: true,
                 showButtonPanel: true,
                 dateFormat: 'dd/mm/yy',
                 onSelect: function (selected) {
                     $(".date_join").datepicker("option", "minDate", selected)
                 }
             });


             $(".date_join").datepicker({
                 changeMonth: true,
                 changeYear: true,
                 showButtonPanel: true,
                 dateFormat: 'dd/mm/yy',
                 onSelect: function (selected) {
                     $(".confirm_date").datepicker("option", "maxDate", selected)
                 }
             });


             $(".date_left").datepicker({
                 changeMonth: true,
                 changeYear: true,
                 showButtonPanel: true,
                 dateFormat: 'dd/mm/yy',
                 maxDate: 30,
                 yearRange: '1950',
                 onSelect: function (selected) {
                     $(".date_join").datepicker("option", "maxDate", selected)

                     if (txt_left_date != "") {
                         // Earning Issues
                         if (txt_earning_issues.value != "") {
                             alert("Please Collect your Earning Head Issues");
                             $("#home").removeClass("fade in active");
                             $("#menu1").removeClass("fade in active");
                             $("#menu6").removeClass("fade in active");
                             $("#menu7").removeClass("fade in active");
                             $("#menu3").removeClass("fade in active");
                             $("#menu2").removeClass("fade in active");
                             $("#menu5").removeClass("fade in active");
                             $("#menu8").removeClass("fade in active");
                             $("#menu9").removeClass("fade in active");
                             $("#tabactive1").removeClass("active");
                             $("#tabactive2").removeClass("active");
                             $("#tabactive3").removeClass("active");
                             $("#tabactive4").removeClass("active");
                             $("#tabactive6").removeClass("active");
                             $("#tabactive5").removeClass("active");
                             $("#tabactive8").removeClass("active");
                             $("#tabactive9").removeClass("active");
                             $("#tabactive10").removeClass("active");
                             $("#tabactive7").addClass("active");
                             $("#menu4").addClass("fade in active");

                             $("#item2").removeClass("fade in active");
                             $("#item3").removeClass("fade in active");
                             $("#item1").removeClass("fade in active");
                             $("#itemtab1").removeClass("active");
                             $("#itemtab2").removeClass("active");
                             $("#itemtab3").removeClass("active");
                             $("#itemtab4").addClass("active");
                             $("#item4").addClass("fade in active");
                             txt_earning_issues.focus();
                             return false;
                         }
                     }
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

             $(".date-EMI").attr("readonly", "true");
             $(".pass_vissa").attr("readonly", "true");
             $(".pass_vissa_passport").attr("readonly", "true");
             $(".date-picker").attr("readonly", "true");
             $(".date_join").attr("readonly", "true");
             $(".confirm_date").attr("readonly", "true");
             $(".date_left").attr("readonly", "true");
             $(".gender").attr("disabled", "true");

             $(".js-example-basic-single").select2();
             // location_hidden();
         }


         $(document).ready(function () {
             var evt = null;
             isNumber(evt);

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


         function Req_validation() {

             var e_name = document.getElementById('<%=txt_eename.ClientID %>');
            var e_fatharname = document.getElementById('<%=txt_eefatharname.ClientID %>');
            var e_birthdate = document.getElementById('<%=txt_birthdate.ClientID %>');
            var l_roleclient = document.getElementById('<%=ddl_clientname.ClientID %>');
            var SelectedTextclient = l_roleclient.options[l_roleclient.selectedIndex].text;

            var txt_unit = document.getElementById('<%=ddl_unitcode.ClientID %>');
            var Selected_unit = txt_unit.options[txt_unit.selectedIndex].text;

            var txt_dept = document.getElementById('<%=ddl_dept.ClientID %>');
            var Selected_dept = txt_dept.options[txt_dept.selectedIndex].text;

            var txt_grade = document.getElementById('<%=ddl_grade.ClientID %>');
            var Selected_grade = txt_grade.options[txt_grade.selectedIndex].text;

            var txt_report_to = document.getElementById('<%=ddl_reporting_to.ClientID %>');
            var Selected_report = txt_report_to.options[txt_report_to.selectedIndex].text;

            var l_role = document.getElementById('<%=DropDownList1.ClientID %>');
            var SelectedText11 = l_role.options[l_role.selectedIndex].text;
            <%--  var select_attendance = document.getElementById('<%= txt_attendanceid.ClientID%>'); --%>
            <%--var select_intime = document.getElementById('<%= ddl_intime.ClientID %>');--%>
            var t_email = document.getElementById('<%= txt_email.ClientID %>');


            var email = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;


            ///////*********************************************************************
            var e_permanantaddress = document.getElementById('<%=txt_permanantaddress.ClientID %>');

            var e_presentcity = document.getElementById('<%=txt_presentaddress.ClientID %>');

            var t_state = document.getElementById('<%=ddl_state.ClientID %>');
            var SelectedText2 = t_state.options[t_state.selectedIndex].text;
            var t_City = document.getElementById('<%=txt_presentcity.ClientID %>');

            var e_mobilenumber = document.getElementById('<%=txt_mobilenumber.ClientID %>');

            var t_contact_name = document.getElementById('<%=txtrefname1.ClientID %>');

            var t_mobile_num = document.getElementById('<%=txtref1mob.ClientID %>');

            var t_contact_name2 = document.getElementById('<%=txt_residencecontactnumber.ClientID %>');

            var t_mobile_num2 = document.getElementById('<%=txtrefname2.ClientID %>');


            ///////*********************************************************************
            var e_confirmationdate = document.getElementById('<%=txt_confirmationdate.ClientID %>');
            var e_joiningdate = document.getElementById('<%=txt_joiningdate.ClientID %>');
            var e_state = document.getElementById('<%=ddl_location.ClientID%>');
            var e_city = document.getElementById('<%=ddl_location_city.ClientID%>');
            var e_ptaxnumber = document.getElementById('<%=txt_ptaxnumber.ClientID %>');
            var work_state = document.getElementById('<%=ddl_location.ClientID %>');
            var Selected_work_state = work_state.options[work_state.selectedIndex].text;
            var work_city = document.getElementById('<%=ddl_location_city.ClientID %>');
            var Selected_work_city = work_city.options[work_city.selectedIndex].text;

            ///////*********************************************************************
            var t_pfbankname = document.getElementById('<%=txt_pfbankname.ClientID %>');
            var t_pfifsccode = document.getElementById('<%=txt_pfifsccode.ClientID %>');
            var t_employeeaccountnumber = document.getElementById('<%=txt_employeeaccountnumber.ClientID %>');
            var txt_bnk_transfer = document.getElementById('<%=ddl_infitcode.ClientID %>');
            var Selected_bnk_tranfer = txt_bnk_transfer.options[txt_bnk_transfer.selectedIndex].text;

            ///////*********************************************************************
            var e_Nationality = document.getElementById('<%=txt_Nationality.ClientID %>');




            ///////*********************************************************************


            ///////*********************************************************************
            var tlhead1 = document.getElementById('<%=txtlhead1.ClientID %>');

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

            // client name
            if (SelectedTextclient == "Select") {
                alert("Please Select Client !!!");
                l_roleclient.focus();
                return false;
            }

            // Unit Name
            if (Selected_unit == "Select") {
                alert("Please Select Unit !!!");
                txt_unit.focus();
                return false;
            }

            // Department Name
            if (Selected_dept == "Select") {
                alert("Please Select Department !!!");
                txt_dept.focus();
                return false;
            }

            // Grade
            if (Selected_grade == "Select") {
                alert("Please Select Grade !!!");
                txt_grade.focus();
                return false;
            }

            // Reporting To
            if (Selected_report == "Select") {
                alert("Please Select Reporting To !!!");
                txt_report_to.focus();
                return false;
            }

            // role:

            if (SelectedText11 == "--Select Role--") {
                alert("Please Select Role !!!");
                l_role.focus();
                return false;
            }
            //// Attendace Id
            //if (select_attendance.value == "") {
            //    alert("Please Put Attendace id");
            //    select_attendance.focus();
            //    return false;
            //}

            //////In Time
            //if (select_intime.value == "Select In Time") {
            //    alert("Please Select In Time");
            //    select_intime.focus();
            //    return false;

            //}

            //email



            // Present Address
            if (t_email.value == "") {
                alert("Please Enter Email Id !!! ");
                t_email.focus();
                return false;
            }

            else if (!email.test(t_email.value)) {
                alert("Please Enter Valid Email Id !!! ");
                t_email.focus();
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
            // Permanant Address

            //if (e_permanantaddress.value == "") {
            //    alert("Please Enter Permanent Address");
            //    $("#menu1").removeClass("fade in active");
            //    $("#menu6").removeClass("fade in active");
            //    $("#menu7").removeClass("fade in active");
            //    $("#menu2").removeClass("fade in active");
            //    $("#menu3").removeClass("fade in active");
            //    $("#menu4").removeClass("fade in active");
            //    $("#menu5").removeClass("fade in active");
            //    $("#menu8").removeClass("fade in active");
            //    $("#menu9").removeClass("fade in active");
            //    $("#tabactive2").removeClass("active");
            //    $("#tabactive3").removeClass("active");
            //    $("#tabactive4").removeClass("active");
            //    $("#tabactive5").removeClass("active");
            //    $("#tabactive6").removeClass("active");
            //    $("#tabactive7").removeClass("active");
            //    $("#tabactive8").removeClass("active");
            //    $("#tabactive9").removeClass("active");
            //    $("#tabactive10").removeClass("active");
            //    $("#tabactive1").addClass("active");
            //    $("#home").addClass("fade in active");
            //    e_permanantaddress.focus();
            //    return false;
            //}


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

            // Residence Contact Number

            //if (e_residencecontactnumbe.value == "") {
            //    alert("Please Enter contact No...");
            //    $("#menu1").removeClass("fade in active");
            //    $("#menu6").removeClass("fade in active");
            //    $("#menu7").removeClass("fade in active");
            //    $("#menu2").removeClass("fade in active");
            //    $("#menu3").removeClass("fade in active");
            //    $("#menu4").removeClass("fade in active");
            //    $("#menu5").removeClass("fade in active");
            //    $("#menu8").removeClass("fade in active");
            //    $("#menu9").removeClass("fade in active");
            //    $("#tabactive2").removeClass("active");
            //    $("#tabactive3").removeClass("active");
            //    $("#tabactive4").removeClass("active");
            //    $("#tabactive5").removeClass("active");
            //    $("#tabactive6").removeClass("active");
            //    $("#tabactive7").removeClass("active");
            //    $("#tabactive8").removeClass("active");
            //    $("#tabactive9").removeClass("active");
            //    $("#tabactive10").removeClass("active");
            //    $("#tabactive1").addClass("active");
            //    $("#home").addClass("fade in active");
            //    e_residencecontactnumbe.focus();
            //    return false;
            //}

            //Contact_name1
            if (t_contact_name.value == "") {
                alert("Please Contact Name");
                t_contact_name.focus();
                return false;
            }

            //Mobile Number1
            if (t_mobile_num.value == "") {
                alert("Please enter valid mobile number");
                t_mobile_num.focus();
                return false;
            }

            //Contact_name2
            if (t_contact_name2.value == "") {
                alert("Please Contact Name");
                t_contact_name2.focus();
                return false;
            }

            //Mobile Number2
            if (t_mobile_num2.value == "") {
                alert("Please enter valid mobile number");
                t_mobile_num2.focus();
                return false;
            }
            // Confirmation Date

            if (e_confirmationdate.value == "") {
                alert("Please Select Confirmation Date");
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
                e_confirmationdate.focus();
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
                e_confirmationdate.focus();
                e_joiningdate.focus();
                return false;
            }

            // location work state
            if (Selected_work_state == "Select") {
                alert("Please Select Work State");
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
            if (Selected_work_city == "Select") {
                alert("Please Select Work City");
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

            var str_adhar = e_ptaxnumber.value.length;
            if (str_adhar != "12") {

                alert("Please Enter valid Adhar Card No\Enrollment No");
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
                e_ptaxnumber.focus();
                return false;
            }
            ///////*********************************************************************

            // Bank Name

            if (t_pfbankname.value == "") {
                alert("Please Enter Bank Name");
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
                $("#tabactive10").removeClass("active");
                $("#tabactive3").addClass("active");
                $("#menu6").addClass("fade in active");
                t_pfbankname.focus();
                return false;
            }

            // IFSC CODE 

            if (t_pfifsccode.value == "") {
                alert("Please Enter IFSC CODE ");
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
                $("#tabactive10").removeClass("active");
                $("#tabactive3").addClass("active");
                $("#menu6").addClass("fade in active");
                t_pfifsccode.focus();
                return false;
            }

            // Bank Account Number

            if (t_employeeaccountnumber.value == "") {
                alert("Please Enter Bank Account Number");
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
                t_employeeaccountnumber.focus();
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
            //Identitymark

            //if (e_Identitymark.value == "") {
            //    alert("Please Enter Identitymark");
            //    $("#home").removeClass("fade in active");
            //    $("#menu1").removeClass("fade in active");
            //    $("#menu6").removeClass("fade in active");
            //    $("#menu2").removeClass("fade in active");
            //    $("#menu3").removeClass("fade in active");
            //    $("#menu4").removeClass("fade in active");
            //    $("#menu5").removeClass("fade in active");
            //    $("#menu8").removeClass("fade in active");
            //    $("#tabactive1").removeClass("active");
            //    $("#tabactive2").removeClass("active");
            //    $("#tabactive3").removeClass("active");
            //    $("#tabactive5").removeClass("active");
            //    $("#tabactive6").removeClass("active");
            //    $("#tabactive7").removeClass("active");
            //    $("#tabactive8").removeClass("active");
            //    $("#tabactive9").removeClass("active");
            //    $("#tabactive4").addClass("active");
            //    $("#menu7").addClass("fade in active");
            //    e_Identitymark.focus();
            //    return false;
            //}
            // Language Known

            //if (e_Language.value == "") {
            //    alert("Please Enter Language Known");
            //    $("#home").removeClass("fade in active");
            //    $("#menu1").removeClass("fade in active");
            //    $("#menu6").removeClass("fade in active");
            //    $("#menu2").removeClass("fade in active");
            //    $("#menu3").removeClass("fade in active");
            //    $("#menu4").removeClass("fade in active");
            //    $("#menu5").removeClass("fade in active");
            //    $("#menu8").removeClass("fade in active");
            //    $("#menu9").removeClass("fade in active");
            //    $("#tabactive1").removeClass("active");
            //    $("#tabactive2").removeClass("active");
            //    $("#tabactive3").removeClass("active");
            //    $("#tabactive5").removeClass("active");
            //    $("#tabactive6").removeClass("active");
            //    $("#tabactive7").removeClass("active");
            //    $("#tabactive8").removeClass("active");
            //    $("#tabactive9").removeClass("active");
            //    $("#tabactive10").removeClass("active");
            //    $("#tabactive4").addClass("active");
            //    $("#menu7").addClass("fade in active");
            //    e_Language.focus();
            //    return false;
            //}

            // passport 
            //if (p_passport_no.value == "") {
            //    return true;
            //} else {
            //    if (p_passport_date.value == "") {
            //        alert("Please Select Passport date...");
            //        $("#home").removeClass("fade in active");
            //        $("#menu1").removeClass("fade in active");
            //        $("#menu6").removeClass("fade in active");
            //        $("#menu2").removeClass("fade in active");
            //        $("#menu3").removeClass("fade in active");
            //        $("#menu4").removeClass("fade in active");
            //        $("#menu5").removeClass("fade in active");
            //        $("#menu8").removeClass("fade in active");
            //        $("#menu9").removeClass("fade in active");
            //        $("#tabactive1").removeClass("active");
            //        $("#tabactive2").removeClass("active");
            //        $("#tabactive3").removeClass("active");
            //        $("#tabactive5").removeClass("active");
            //        $("#tabactive6").removeClass("active");
            //        $("#tabactive7").removeClass("active");
            //        $("#tabactive8").removeClass("active");
            //        $("#tabactive9").removeClass("active");
            //        $("#tabactive10").removeClass("active");
            //        $("#tabactive4").addClass("active");
            //        $("#menu7").addClass("fade in active");
            //        p_passport_date.focus();
            //        return false;
            //    }
            //}


            ///////// visa


            //if (SelectedText_visa.value == "Select") {
            //    return true;
            //} else {
            //    if (Visa_Validity_Date.value == "") {
            //        alert("Please Select Visa Validity Date...");
            //        $("#home").removeClass("fade in active");
            //        $("#menu1").removeClass("fade in active");
            //        $("#menu6").removeClass("fade in active");
            //        $("#menu2").removeClass("fade in active");
            //        $("#menu3").removeClass("fade in active");
            //        $("#menu4").removeClass("fade in active");
            //        $("#menu5").removeClass("fade in active");
            //        $("#menu8").removeClass("fade in active");
            //        $("#menu9").removeClass("fade in active");
            //        $("#tabactive1").removeClass("active");
            //        $("#tabactive2").removeClass("active");
            //        $("#tabactive3").removeClass("active");
            //        $("#tabactive5").removeClass("active");
            //        $("#tabactive6").removeClass("active");
            //        $("#tabactive7").removeClass("active");
            //        $("#tabactive8").removeClass("active");
            //        $("#tabactive9").removeClass("active");
            //        $("#tabactive10").removeClass("active");
            //        $("#tabactive4").addClass("active");
            //        $("#menu7").addClass("fade in active");
            //        Visa_Validity_Date.focus();
            //        return false;
            //    }
            //}
            // Marital Status

            //if (e_maritalstaus.value == "") {
            //    alert("Please Enter Marital Status");
            //    $("#home").removeClass("fade in active");
            //    $("#menu1").removeClass("fade in active");
            //    $("#menu6").removeClass("fade in active");
            //    $("#menu2").removeClass("fade in active");
            //    $("#menu3").removeClass("fade in active");
            //    $("#menu4").removeClass("fade in active");
            //    $("#menu5").removeClass("fade in active");
            //    $("#menu8").removeClass("fade in active");
            //    $("#menu9").removeClass("fade in active");
            //    $("#tabactive1").removeClass("active");
            //    $("#tabactive2").removeClass("active");
            //    $("#tabactive3").removeClass("active");
            //    $("#tabactive5").removeClass("active");
            //    $("#tabactive6").removeClass("active");
            //    $("#tabactive7").removeClass("active");
            //    $("#tabactive8").removeClass("active");
            //    $("#tabactive9").removeClass("active");
            //    $("#tabactive10").removeClass("active");
            //    $("#tabactive4").addClass("active");
            //    $("#menu7").addClass("fade in active");
            //    e_maritalstaus.focus();
            //    return false;
            //}

            ///////*********************************************************************

            // Qualification

            //if (t_qualification_1.value == "") {
            //    alert("Please Enter Qualification1");
            //    $("#home").removeClass("fade in active");
            //    $("#menu1").removeClass("fade in active");
            //    $("#menu6").removeClass("fade in active");
            //    $("#menu7").removeClass("fade in active");
            //    $("#menu3").removeClass("fade in active");
            //    $("#menu4").removeClass("fade in active");
            //    $("#menu5").removeClass("fade in active");
            //    $("#menu8").removeClass("fade in active");
            //    $("#menu9").removeClass("fade in active");
            //    $("#tabactive1").removeClass("active");
            //    $("#tabactive2").removeClass("active");
            //    $("#tabactive3").removeClass("active");
            //    $("#tabactive4").removeClass("active");
            //    $("#tabactive6").removeClass("active");
            //    $("#tabactive7").removeClass("active");
            //    $("#tabactive8").removeClass("active");
            //    $("#tabactive9").removeClass("active");
            //    $("#tabactive10").removeClass("active");
            //    $("#tabactive5").addClass("active");
            //    $("#menu2").addClass("fade in active");
            //    t_qualification_1.focus();
            //    return false;
            //}

            // Year of passing

            //if (t_year_of_passing_1.value == "") {
            //    alert("Please Enter Year of passing ");
            //    $("#home").removeClass("fade in active");
            //    $("#menu1").removeClass("fade in active");
            //    $("#menu6").removeClass("fade in active");
            //    $("#menu7").removeClass("fade in active");
            //    $("#menu3").removeClass("fade in active");
            //    $("#menu4").removeClass("fade in active");
            //    $("#menu5").removeClass("fade in active");
            //    $("#menu8").removeClass("fade in active");
            //    $("#menu9").removeClass("fade in active");
            //    $("#tabactive1").removeClass("active");
            //    $("#tabactive2").removeClass("active");
            //    $("#tabactive3").removeClass("active");
            //    $("#tabactive4").removeClass("active");
            //    $("#tabactive6").removeClass("active");
            //    $("#tabactive7").removeClass("active");
            //    $("#tabactive8").removeClass("active");
            //    $("#tabactive9").removeClass("active");
            //    $("#tabactive10").removeClass("active");
            //    $("#tabactive5").addClass("active");
            //    $("#menu2").addClass("fade in active");
            //    t_year_of_passing_1.focus();
            //    return false;
            //}


            // BASIC
            if (parseFloat(tlhead1.value) == "0") {
                alert("Please Enter BASIC");
                $("#home").removeClass("fade in active");
                $("#menu1").removeClass("fade in active");
                $("#menu6").removeClass("fade in active");
                $("#menu7").removeClass("fade in active");
                $("#menu3").removeClass("fade in active");
                $("#menu2").removeClass("fade in active");
                $("#menu5").removeClass("fade in active");
                $("#menu8").removeClass("fade in active");
                $("#menu9").removeClass("fade in active");
                $("#tabactive1").removeClass("active");
                $("#tabactive2").removeClass("active");
                $("#tabactive3").removeClass("active");
                $("#tabactive4").removeClass("active");
                $("#tabactive6").removeClass("active");
                $("#tabactive5").removeClass("active");
                $("#tabactive8").removeClass("active");
                $("#tabactive9").removeClass("active");
                $("#tabactive10").removeClass("active");
                $("#tabactive7").addClass("active");
                $("#menu4").addClass("fade in active");
                $("#item2").removeClass("fade in active");
                $("#item3").removeClass("fade in active");
                $("#item4").removeClass("fade in active");
                $("#itemtab2").removeClass("active");
                $("#itemtab4").removeClass("active");
                $("#itemtab3").removeClass("active");
                $("#itemtab1").addClass("active");
                $("#item1").addClass("fade in active");
                tlhead1.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }

        window.onfocus = function () {
            $.unblockUI();

        }



        $(document).ready(function () {

            $(".js-example-basic-single").select2();

        });

        function openWindow() {
            window.open("html/EmployeeMaster.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

    </script>

    <div class="container-fluid">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />



        <asp:Panel runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff;" class="text-center text-uppercase"><b>EMPLOYEE INFORMATION</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image13" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-8 col-xs-12">
                    </div>
                    <div class="col-sm-4 col-xs-12">
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        <asp:TextBox ID="txtautoattendancecode" onkeypress="return AllowAlphabet_Number(event,this);" runat="server" class="form-control text_box" meta:resourcekey="txtautoattendancecodeResource1" Visible="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-2 col-xs-12">
                    <%-- Enter Employee Code/Name :--%>
                </div>
                <div class="col-sm-4 col-xs-12">
                    <asp:TextBox ID="txtsearchempid" Visible="false" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>
                </div>
                <br />



                <asp:Panel ID="newpanel" runat="server">
                    <div class="row">
                        <div class="col-sm-2 col-xs-12 text-left">
                            Employee Code :
                            <asp:TextBox ID="txt_eecode" runat="server" class="form-control text_box"
                                onkeypress="return Alphabet_Number(event,this);" MaxLength="20" meta:resourcekey="txt_eecodeResource1" ReadOnly="true" disabled></asp:TextBox>

                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_eecode" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1" Visible="False"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-sm-2 col-xs-12 text-left">
                            IHMS Code :
                            <asp:TextBox ID="txt_ihmscode" runat="server" class="form-control text_box" ReadOnly="true" disabled></asp:TextBox>

                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_eecode" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1" Visible="False"></asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="col-sm-2 col-xs-12 text-left">
                            Employee Name :
                            <asp:Label ID="Label10" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                            <asp:TextBox ID="txt_eename" runat="server" CausesValidation="True" class="form-control text_box"
                                MaxLength="30" meta:resourcekey="txt_eenameResource1" ReadOnly="true" onkeypress="return AllowAlphabet(event)"></asp:TextBox>

                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_eename" ErrorMessage="*" meta:resourceKey="RequiredFieldValidator2Resource1" Visible="False"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_eename" ErrorMessage="Pl enter Name" meta:resourceKey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-sm-2 col-xs-12 text-left">
                            Father / Husband Name :
                            <asp:Label ID="Label22" runat="server" ForeColor="Red" Text=" * "></asp:Label>

                            <asp:TextBox ID="txt_eefatharname" runat="server" CausesValidation="True" ReadOnly="true" class="form-control text_box"
                                MaxLength="30" meta:resourceKey="txt_eefatharnameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_eefatharname" ErrorMessage="RequiredFieldValidator" meta:resourceKey="RequiredFieldValidator4Resource1" Text="*"></asp:RequiredFieldValidator>

                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_eefatharname" ErrorMessage=" Father/Husband Name" meta:resourceKey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-sm-2 col-xs-12 text-left">
                            Relation :
                            <asp:DropDownList ID="ddl_relation" runat="server" class="form-control gender"
                                meta:resourceKey="ddl_relationResource1">
                                <asp:ListItem meta:resourceKey="ListItemResource1" Text="Father"></asp:ListItem>
                                <asp:ListItem meta:resourceKey="ListItemResource2" Text="Husband"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12 text-left">
                            Date of Birth :
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text=" * "></asp:Label>

                            <asp:TextBox ID="txt_birthdate" runat="server" ReadOnly="true" class="form-control date-picker"
                                meta:resourceKey="txt_birthdateResource1"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12 text-left">
                            Gender :
                            <asp:Label ID="Label16" runat="server" ForeColor="Red" ReadOnly="true" Text=" * "></asp:Label>
                            <asp:DropDownList ID="ddl_gender" runat="server" class="form-control gender" meta:resourceKey="ddl_genderResource1">
                                <asp:ListItem meta:resourceKey="ListItemResource3" Text="Male" Value="M"></asp:ListItem>
                                <asp:ListItem meta:resourceKey="ListItemResource4" Text="Female" Value="F"></asp:ListItem>
                                <%--<asp:ListItem meta:resourceKey="ListItemResource5" Text="Transgender" Value="T"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                        <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                        <div class="col-sm-2 col-xs-12 text-left">
                            Client Name :
                                <asp:Label ID="Label3" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                            <asp:DropDownList ID="ddl_clientname" runat="server" class="form-control gender" AutoPostBack="true" OnSelectedIndexChanged="ddl_clientname_SelectedIndexChanged"
                                DataTextField="CLIENT_NAME" DataValueField="CLIENT_NAME" meta:resourcekey="ddl_relationResource1">
                                <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                                <%-- <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>



                        <div class="col-sm-2 col-xs-12 text-left">
                            Branch Code :
                                        <asp:Label ID="Label17" runat="server" ForeColor="Red" Text=" * "></asp:Label>

                            <asp:DropDownList ID="ddl_unitcode" class="form-control gender" Width="100%" runat="server" AutoPostBack="True"
                                meta:resourcekey="ddl_unitcodeResource1" OnSelectedIndexChanged="ddl_unitcode_SelectedIndexChanged">
                            </asp:DropDownList>

                            <asp:Label ID="lblunitname" runat="server" meta:resourcekey="lblunitnameResource1" Visible="false"></asp:Label>

                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:CELTPAYConnectionString %>" ProviderName="<%$ ConnectionStrings:celtpayConnectionString.ProviderName %>" SelectCommand="SELECT UNIT_CODE, UNIT_NAME, concat(UNIT_CODE, '-', UNIT_NAME) AS CUNIT FROM pay_unit_master  WHERE comp_code=@comp_code and Client_code=Client_code ORDER BY UNIT_CODE">
                                <SelectParameters>
                                    <asp:SessionParameter Name="comp_code" SessionField="comp_code" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </div>


                        <%--   </ContentTemplate>
                            </asp:UpdatePanel>--%>

                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:CELTPAYConnectionString %>" ProviderName="<%$ ConnectionStrings:celtpayConnectionString.ProviderName %>" SelectCommand="SELECT GRADE_CODE, GRADE_DESC FROM pay_grade_master WHERE comp_code=@comp_code ORDER BY GRADE_CODE">
                            <SelectParameters>
                                <asp:SessionParameter Name="comp_code" SessionField="comp_code"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>


                        <div class="col-sm-2 col-xs-12 text-left">
                            Department :
                                    <asp:Label ID="Label30" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                            <asp:DropDownList ID="ddl_dept" ReadOnly="True" runat="server" class="js-example-basic-single gender" Width="100%" DataSourceID="SqlDataSourceDepartment"
                                DataTextField="CDEPT" DataValueField="DEPT_CODE" meta:resourcekey="ddl_deptResource1">
                                <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                            </asp:DropDownList>

                            <asp:Label ID="lbldept" runat="server" meta:resourcekey="lbldeptResource1"></asp:Label>

                            <asp:SqlDataSource ID="SqlDataSourceDepartment" runat="server" ConnectionString="<%$ 
                                        ConnectionStrings:CELTPAYConnectionString %>"
                                ProviderName="<%$ ConnectionStrings:celtpayConnectionString.ProviderName %>"
                                SelectCommand="SELECT DEPT_CODE, DEPT_NAME, concat(DEPT_CODE,'-',DEPT_NAME) AS CDEPT  FROM pay_department_master WHERE comp_code=@comp_code">
                                <SelectParameters>
                                    <asp:SessionParameter Name="comp_code" SessionField="comp_code" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </div>



                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>

                                <div class="col-sm-2 col-xs-12">
                                    Designation:<asp:Label ID="Label18" runat="server" ForeColor="Red" Text=" * "></asp:Label>

                                    <asp:DropDownList ID="ddl_grade" class="js-example-basic-single gender" Width="100%" runat="server" AutoPostBack="True"
                                        DataTextField="GRADE_DESC" DataValueField="GRADE_CODE" meta:resourcekey="ddl_gradeResource1" OnSelectedIndexChanged="ddl_grade_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    Reporting To:
                                    <asp:Label ID="Label25" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                    <asp:DropDownList ID="ddl_reporting_to" runat="server" class="form-control gender" DataTextField="emp_name"
                                        DataValueField="emp_code" meta:resourcekey="ddl_deptResource1">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <br />

                    <%--<div class="col-sm-2 col-xs-12">Department:</div>
                                    <div class="col-sm-3 col-xs-12">
                                        <asp:DropDownList ID="ddl_dept" runat="server" class="form-control" AutoPostBack="True" DataSourceID="SqlDataSourceDepartment" DataTextField="CDEPT" DataValueField="DEPT_CODE" meta:resourcekey="ddl_deptResource1" OnSelectedIndexChanged="ddl_dept_SelectedIndexChanged"></asp:DropDownList>

                                        <asp:Label ID="lbldept" runat="server" meta:resourcekey="lbldeptResource1"></asp:Label>

                                        <asp:SqlDataSource ID="SqlDataSourceDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:CELTPAYConnectionString %>" ProviderName="<%$ ConnectionStrings:celtpayConnectionString.ProviderName %>" SelectCommand="SELECT DEPT_CODE, DEPT_NAME, concat(DEPT_CODE,'-',DEPT_NAME) AS CDEPT  FROM pay_department_master WHERE comp_code=@comp_code">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="comp_code" SessionField="comp_code" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>

                                    </div>--%>


                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                            Role :
                                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                            <asp:DropDownList ID="DropDownList1" class="form-control gender" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            Attendance ID

                                    <asp:TextBox ID="txt_attendanceid" ReadOnly="true" runat="server" onkeypress="return AllowAlphabet_Number(event)"
                                        class="form-control text_box" MaxLength="10" meta:resourceKey="txt_attendacneidResource1"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            IN Time : 
                       <%--     <asp:Label ID="Label3" runat="server" ForeColor="Red" Text=" * "></asp:Label>--%>

                            <asp:DropDownList ID="ddl_intime" runat="server" class="js-example-basic-single gender " Width="100%" meta:resourceKey="ddl_intimeResource1">
                                <%--  <asp:ListItem Value="Select In Time" Text="Select In Time"></asp:ListItem>--%>
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
                            Email:<asp:Label ID="Label4" runat="server" ForeColor="Red" Text=" * "></asp:Label>

                            <asp:TextBox ID="txt_email" ReadOnly="true" runat="server" class="form-control text_box" onkeypress="return email(event)" MaxLength="30" meta:resourceKey="txt_email"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <br />
                    <ul class="nav nav-tabs">
                        <li id="tabactive1" class="active"><a data-toggle="tab" href="#home" runat="server">Contact Details</a></li>
                        <li id="tabactive2"><a data-toggle="tab" href="#menu1" runat="server">Employee Details</a></li>
                        <li id="tabactive10"><a data-toggle="tab" href="#menu9" runat="server">Family Details</a></li>
                        <li id="tabactive3"><a data-toggle="tab" href="#menu6" runat="server">Bank Account Details</a></li>
                        <li id="tabactive4"><a data-toggle="tab" href="#menu7" runat="server">Personal Details</a></li>
                        <li id="tabactive5"><a data-toggle="tab" href="#menu2" runat="server">Qualification Details</a></li>
                        <%--<li id="tabactive6"><a data-toggle="tab" href="#menu3" runat="server">Leave Details</a></li>--%>
                        <li id="tabactive7"><a data-toggle="tab" href="#menu4" id="rating" runat="server">Rating Details</a></li>
                        <li id="tabactive8"><a data-toggle="tab" href="#menu5" runat="server">Documents</a></li>
                        <li id="tabactive9"><a data-toggle="tab" href="#menu8" runat="server">Loan</a></li>
                        <li id="tabactive11"><a data-toggle="tab" href="#menu11" runat="server">KRA</a></li>

                    </ul>

                    <div class="tab-content">
                        <div id="home" class="tab-pane fade in active">

                            <br />

                            <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-12 col-xs-12 text-right">
                                            <asp:Button ID="btn_copyadd" runat="server" CssClass="btn btn-warning btn_address" OnClientClick="return copy_add();" Text="Copy Present Address" />
                                        </div>

                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12">
                                                    Present Address :
                                                     <asp:Label ID="Label5" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                                </div>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txt_presentaddress" ReadOnly="true"
                                                        runat="server" MaxLength="100" class="form-control pr_add text_box"
                                                        onkeypress="return AllowAlphabet_address(event)"
                                                        meta:resourcekey="txt_presentaddressResource1"></asp:TextBox><td class="labels">
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12">
                                                    State :
                                                       <asp:Label ID="Label23" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:DropDownList ID="ddl_state" runat="server" class="pr_state js-example-basic-single gender" Width="100%" OnSelectedIndexChanged="get_city_list" AutoPostBack="true"></asp:DropDownList>
                                                    <%-- <asp:DropDownList ID="ddl_state" runat="server" class="form-control"
                                                                    DataSourceID="SqlDataSource3" DataTextField="STATE_NAME"
                                                                    DataValueField="STATE_NAME"  AutoPostBack="true"
                                                                    meta:resourcekey="ddl_stateResource1"  >
                                                             </asp:DropDownList>--%>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12">
                                                    Present City :
                                                       <asp:Label ID="Label24" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-sm-8 col-xs-12">
                                                    <%--<asp:TextBox ID="txt_presentcity" runat="server" class="form-control text_box"
                                                                MaxLength="30" meta:resourcekey="txt_presentcityResource1"  
                                                                onkeypress="return AllowAlphabet(event)"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="txt_presentcity" runat="server" class="pr_state js-example-basic-single gender" Width="100%"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12">
                                                    Pin code :
                                                </div>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox
                                                        ID="txt_presentpincode" runat="server" ReadOnly="true" class="form-control pr_pin text_box"
                                                        onkeypress="return isNumber(event)" MaxLength="6"
                                                        meta:resourcekey="txt_presentpincodeResource1"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12">
                                                    Mobile Number :
                                                     <asp:Label ID="Label6" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                                </div>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txt_mobilenumber" ReadOnly="true" runat="server" class="form-control pr_mbno text_box"
                                                        onkeypress="return isNumber(event)" MaxLength="10"
                                                        meta:resourcekey="txt_mobilenumberResource1"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />

                                        </div>
                                        <asp:SqlDataSource
                                            ID="SqlDataSource3" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:CELTPAYConnectionString %>"
                                            ProviderName="<%$ ConnectionStrings:celtpayConnectionString.ProviderName %>"
                                            SelectCommand=" SELECT distinct STATE_NAME FROM pay_state_master   ORDER BY STATE_NAME"></asp:SqlDataSource>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12">
                                                    Permanant Address :
                                                </div>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txt_permanantaddress" ReadOnly="true" runat="server" onkeypress="return AllowAlphabet_address(event)"
                                                        MaxLength="50" class="form-control prnt_add text_box"
                                                        meta:resourcekey="txt_permanantaddressResource1"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12">
                                                    State :
                                                </div>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:DropDownList ID="ddl_permstate" runat="server" class="pr_state js-example-basic-single gender" Width="100%"
                                                        OnSelectedIndexChanged="get_city_list_shipping" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <%--   <asp:DropDownList
                                                        ID="ddl_permstate" runat="server" class="form-control"
                                                        DataSourceID="SqlDataSource3" DataTextField="STATE_NAME"
                                                        DataValueField="STATE_NAME" AutoPostBack="true"
                                                        meta:resourcekey="ddl_permstateResource1" >
                                                    </asp:DropDownList>--%>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12">
                                                    Permanant City :
                                                </div>
                                                <div class="col-sm-8 col-xs-12">
                                                    <%--<asp:TextBox ID="txt_permanantcity" runat="server" class="form-control text_box"
                                                        MaxLength="30" meta:resourcekey="txt_permanantcityResource1" 
                                                        onkeypress="return AllowAlphabet(event)"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="txt_permanantcity" runat="server" class="pr_state js-example-basic-single gender" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12">
                                                    Pin code :
                                                </div>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txt_permanantpincode" ReadOnly="true" runat="server" class="form-control prnt_pin text_box"
                                                        onkeypress="return isNumber(event)"
                                                        MaxLength="6" meta:resourcekey="txt_permanantpincodeResource1"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12">
                                                    Mobile Number  :
                                                </div>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtref2mob" runat="server" ReadOnly="true" class="form-control prnt_mbno text_box"
                                                        onkeypress="return isNumber(event)"
                                                        MaxLength="10" meta:resourcekey="txtref2mobResource1"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />


                                        </div>
                                    </div>

                                    <asp:Panel ID="newcontactpanel" runat="server" CssClass="panel panel-default">
                                        <div class="panel-heading">
                                            <b>Reference Details</b>
                                        </div>
                                        <div class="row" style="border: 1px solid #aaa; padding-top: 10px; padding-bottom: 10px;">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-sm-4 col-xs-1s2">
                                                        Contact Name 1 :
                                                   <asp:Label ID="lablec" Text="*" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtrefname1" ReadOnly="true" runat="server" class="form-control pr_c1 text_box"
                                                            MaxLength="30" meta:resourcekey="txtrefname1Resource1"
                                                            onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-sm-4 col-xs-12">
                                                        Mobile No 1 :<asp:Label ID="Label26" Text="*" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtref1mob" ReadOnly="true" runat="server" class="form-control pr_c2 text_box"
                                                            MaxLength="30" meta:resourcekey="txtrefname2Resource1"
                                                            onkeypress="return isNumber(event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-sm-4 col-xs-12">
                                                        Email Id 1 :
                                                    </div>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txt_emailid1" ReadOnly="true" runat="server" class="form-control  prnt_c2 text_box"
                                                            MaxLength="30" meta:resourcekey="txtref1mobResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-sm-4 col-xs-12">
                                                        Contact Name 2 :<asp:Label ID="Label27" Text="*" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txt_residencecontactnumber" ReadOnly="true" MaxLength="30" runat="server"
                                                            class="form-control prnt_c1 text_box" onkeypress="return AllowAlphabet(event)"
                                                            meta:resourcekey="txt_residencecontactnumberResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-sm-4 col-xs-12">
                                                        Mobile No 2 :<asp:Label ID="Label28" Text="*" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtrefname2" runat="server" ReadOnly="true" class="form-control  prnt_c2 text_box"
                                                            onkeypress="return isNumber(event)"
                                                            MaxLength="30" meta:resourcekey="txtref1mobResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-sm-4 col-xs-12">
                                                        Email Id 2 :
                                                    </div>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txt_emailid2" runat="server" ReadOnly="true" class="form-control  prnt_c2 text_box"
                                                            MaxLength="50" meta:resourcekey="txtref1mobResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                </ContentTemplate>
                            </asp:UpdatePanel>



                        </div>
                        <div id="menu1" class="tab-pane fade">
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                    Confirmation Date :
                                     <asp:Label ID="Label8" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_confirmationdate" MaxLength="100" ReadOnly="true" runat="server" Width="100%" class="form-control confirm_date"
                                        meta:resourcekey="txt_confirmationdateResource1"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    Joining Date :
                                     <asp:Label ID="Label9" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_joiningdate" runat="server" ReadOnly="true" class="form-control date_join" Width="100%"
                                        meta:resourcekey="txt_joiningdateResource1"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>

                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                            Working State :
                                         <asp:Label ID="Lbl_long" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            <asp:DropDownList ID="ddl_location" Enabled="false" CssClass="pr_state js-example-basic-single" Width="100%"
                                                runat="server" OnSelectedIndexChanged="ddl_location_SelectedIndexChanged" AutoPostBack="true"
                                                meta:resourcekey="ddl_locationResource1">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            Working City :
                                           <asp:Label ID="Label7" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            <asp:DropDownList ID="ddl_location_city" Enabled="false" CssClass="pr_state js-example-basic-single" Width="100%" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">UAN Number :</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_panno" runat="server" ReadOnly="true" class="form-control text_box"
                                        meta:resourcekey="txt_pannoResource1" MaxLength="12" onkeypress="return Alphabet_Number(event,this);"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">Police Station Name :</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_policestationname" runat="server" ReadOnly="true" class="form-control text_box"
                                        meta:resourcekey="txt_pannoResource1" MaxLength="12" onkeypress="return Alphabet_Number(event,this);"></asp:TextBox>
                                </div>

                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">PAN Number :</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_pan_new_num" runat="server" ReadOnly="true" class="form-control text_box" meta:resourceKey="txt_pan_new_numResource1" MaxLength="12" onkeypress="return Alphabet_Number(event,this);"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    Adhar Card / Enrollment No :
                                     <asp:Label ID="Label11" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_ptaxnumber" onkeypress="return isNumber(event,this);" ReadOnly="true" runat="server" class="form-control text_box" MaxLength="12"
                                        meta:resourcekey="txt_ptaxnumberResource1"></asp:TextBox>
                                </div>

                            </div>
                            <br />

                            <div class="row">
                                <div class="col-sm-2 col-xs-12">ESIC Number :</div>
                                <div class="col-sm-3 col-xs-12">

                                    <asp:TextBox ID="txt_esicnumber" runat="server" ReadOnly="true" class="form-control text_box"
                                        meta:resourcekey="txt_esicnumberResource1" Text="A"></asp:TextBox>
                                </div>

                                <div class="col-sm-2 col-xs-12">ESIC Flag :</div>
                                <div class="col-sm-3 col-xs-12">

                                    <asp:DropDownList ID="ddl_esicdeductionflag" runat="server"
                                        class="form-control gender" meta:resourcekey="ddl_esicdeductionflagResource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource12" Text="Yes"></asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource13" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <div class="row">


                                <div class="col-sm-2 col-xs-12">P.F Number :</div>
                                <div class="col-sm-3 col-xs-12">

                                    <asp:TextBox ID="txt_pfnumber" runat="server" ReadOnly="true" AutoPostBack="True"
                                        class="form-control text_box"
                                        meta:resourcekey="txt_pfnumberResource1" Text="A"
                                        onkeypress="return AllowAlphabet_Number(event,this);" MaxLength="15"></asp:TextBox>
                                </div>

                                <div class="col-sm-2 col-xs-12">PF Flag :</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:DropDownList ID="ddl_pfdeductionflag" runat="server" class="form-control gender" meta:resourcekey="ddl_pfdeductionflagResource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource8" Text="Yes" Value="Yes"></asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource9" Text="No" Value="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                    PF Employee Percentage :
                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_pfemployeepercentage" ReadOnly="true" runat="server" onkeypress="return isNumber(event,this)"
                                        class="form-control text_box" MaxLength="10">0</asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">Reason For Resign :</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txtreasonforleft" ReadOnly="true" runat="server" class="form-control text_box"
                                        meta:resourcekey="txtreasonforleftResource1"></asp:TextBox>
                                    <br />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">Post To Wages Salary :</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:DropDownList ID="ddlpfregisteremp" class="form-control gender" runat="server"
                                        meta:resourcekey="ddlpfregisterempResource1">
                                        <asp:ListItem Value="Yes"
                                            meta:resourcekey="ListItemResource10" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="No" meta:resourcekey="ListItemResource11" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12">Left Date :</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_leftdate" runat="server" ReadOnly="true" class="form-control date_left" Width="100%"
                                        meta:resourcekey="txt_leftdateResource1"></asp:TextBox>
                                </div>
                                <%-- <div class="col-sm-2 col-xs-12">ESIC Number :</div>
                                            <div class="col-sm-3 col-xs-12">

                                                <asp:TextBox ID="txt_esicnumber" runat="server" class="form-control"
                                                    meta:resourcekey="txt_esicnumberResource1" Text="A"></asp:TextBox>
                                            </div>--%>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">Medi claim No:</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_mediclaim" runat="server" ReadOnly="true" class="form-control" Width="100%"
                                        meta:resourcekey="txt_leftdateResource1"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">Acci Policy No:</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_acci" runat="server" ReadOnly="true" class="form-control" Width="100%"
                                        meta:resourcekey="txt_leftdateResource1"></asp:TextBox>
                                </div>
                            </div>
                            <br />

                            <asp:DropDownList ID="ddl_ptaxdeductionflag" Enabled="false" runat="server"
                                class="form-control" Visible="False"
                                meta:resourcekey="ddl_ptaxdeductionflagResource1">
                                <asp:ListItem meta:resourcekey="ListItemResource14" Text="Yes"></asp:ListItem>
                                <asp:ListItem meta:resourcekey="ListItemResource15" Text="No"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div id="menu6" class="tab-pane fade">
                            <br />

                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                    Bank Holder Name :
                                 
                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_bankholder" ReadOnly="true" runat="server" class="form-control text_box" MaxLength="50" meta:resourcekey="txt_pfbanknameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    Bank Name :
                                     <asp:Label ID="Label12" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_pfbankname" ReadOnly="true" runat="server" class="form-control text_box" MaxLength="50" meta:resourcekey="txt_pfbanknameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                </div>



                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">Branch Name :</div>
                                <div class="col-sm-3 col-xs-12">
                                    <%-- <asp:DropDownList ID="ddl_bankcode" runat="server"  class="form-control" DataSourceID="SqlDataSource5" DataTextField="CBANK" DataValueField="BANK_CODE" meta:resourceKey="ddl_bankcodeResource1" OnSelectedIndexChanged="ddl_bankcode_SelectedIndexChanged">
                                                </asp:DropDownList>--%>
                                    <asp:TextBox ID="ddl_bankcode" ReadOnly="true" runat="server" class="form-control text_box" MaxLength="100" meta:resourcekey="ddl_bankcodeResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    Bank A/C Number :
                                     <asp:Label ID="Label14" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_employeeaccountnumber" ReadOnly="true" runat="server" class="form-control text_box"
                                        meta:resourceKey="txt_employeeaccountnumberResource1" MaxLength="32" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                    IFSC Code :
                                     <asp:Label ID="Label13" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_pfifsccode" runat="server" ReadOnly="true" class="form-control text_box" MaxLength="15"
                                        meta:resourceKey="txt_pfifsccodeResource1" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    NFD/IFD :<asp:Label ID="Label29" Text="*" runat="server" ForeColor="Red"></asp:Label>

                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:DropDownList ID="ddl_infitcode" runat="server" class="form-control gender">
                                        <asp:ListItem Value="Select" Text="Select Transfer">Select Transfer</asp:ListItem>
                                        <asp:ListItem Value="N" Text="NEFT TRANSFER">NEFT TRANSFER</asp:ListItem>
                                        <asp:ListItem Value="I" Text="INTERNAL TRANSFER">INTERNAL TRANSFER</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">Nominee Name :</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_pfnomineename" runat="server" ReadOnly="true" class="form-control text_box"
                                        meta:resourceKey="txt_pfnomineenameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                </div>

                                <div class="col-sm-2 col-xs-12">Nominee Relation :</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_pfnomineerelation" runat="server" ReadOnly="true" class="form-control text_box" MaxLength="20" meta:resourceKey="txt_pfnomineerelationResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">Nominee Birth Date :</div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_pfbdate" runat="server" ReadOnly="true" class="form-control date-picker" meta:resourceKey="txt_pfbdateResource1" Width="250"></asp:TextBox>

                                </div>
                            </div>
                            <br />
                            <asp:Label ID="lblbankname" runat="server" meta:resourceKey="lblbanknameResource1"></asp:Label>
                            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:CELTPAYConnectionString %>" ProviderName="<%$ ConnectionStrings:celtpayConnectionString.ProviderName %>" SelectCommand="SELECT BANK_CODE, BANK_NAME,concat(BANK_CODE,'-',BANK_NAME) AS CBANK FROM pay_bank_master  WHERE comp_code=@comp_code  ORDER BY BANK_CODE">
                                <SelectParameters>
                                    <asp:SessionParameter Name="comp_code" SessionField="comp_code" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            </caption>
                        </div>
                        <div id="menu7" class="tab-pane fade">
                            <div class="row">
                                <br />
                                <div class="col-sm-6 col-xs-12">
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Nationality :
                                             <asp:Label ID="Label15" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                        </div>
                                        <div class="col-sm-6 col-xs-12" onkeypress="return lettersOnly(event,this);">
                                            <asp:TextBox ID="txt_Nationality" ReadOnly="true" runat="server" CssClass="form-control "
                                                class="text_box" onkeypress="return AllowAlphabet(event)" MaxLength="20"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Identitymark :
                                        
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_Identitymark" ReadOnly="true" runat="server" CssClass="form-control" class="text_box"
                                                onkeypress="return AllowAlphabet_number(event,this);" MaxLength="30"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Mother Tongue :
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="ddl_Mother_Tongue" ReadOnly="true" runat="server" CssClass="form-control" class="text_box"
                                                onkeypress="return AllowAlphabet_number(event,this);" MaxLength="30"></asp:TextBox>
                                            <%--<asp:DropDownList ID="ddl_Mother_Tongue" runat="server" class="form-control" meta:resourcekey="ddl_ptaxdeductionflagResource1">
                                                <asp:ListItem meta:resourcekey="ListItemResource14">Marathi</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource15">Hindi</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource15">English</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource15">Kannad</asp:ListItem>
                                                    </asp:DropDownList>--%>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">Hobbies :</div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_hobbies" ReadOnly="true" runat="server" MaxLength="100" class="form-control text_box" onkeypress="return AllowAlphabet_Number10(event)" meta:resourcekey="txt_hobbiesResource1"></asp:TextBox>
                                        </div>

                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Passport No :
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txt_Passport_No" ReadOnly="true" runat="server" MaxLength="20" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Visa(Country) :
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:DropDownList ID="ddl_Visa_Country" runat="server" class="form-control text_box gender"
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
                                            Driving License No :
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_Driving_License_No" ReadOnly="true" runat="server" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_Number(event,this);" MaxLength="20"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Mise :
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_Mise" runat="server" ReadOnly="true" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_Number(event,this);" MaxLength="30"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Weight (in Kg) :
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_weight" runat="server" ReadOnly="true" onkeypress="return isNumber_dot(event)" MaxLength="10" CssClass="form-control" class="text_box" meta:resourcekey="txt_weightResource1">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Marital Status :
                                             <%--<asp:Label ID="Label17" runat="server" ForeColor="Red" Text=" * "></asp:Label>--%>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_maritalstaus" ReadOnly="true" runat="server" class="form-control text_box" meta:resourcekey="txt_maritalstausResource1" onkeypress="return AllowAlphabet(event)" MaxLength="20"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="row">

                                        <div class="col-sm-4 col-xs-12 text-right">Religion :</div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:DropDownList
                                                ID="ddl_religion" runat="server" class="form-control gender" Width="100%"
                                                meta:resourcekey="ddl_religionResource1">
                                                <asp:ListItem meta:resourcekey="ListItemResource16">General</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource17">OBC</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource18">SC</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource19">ST</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource20">NT</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource21">Other</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Place Of Birth :

                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_Place_Of_Birth" ReadOnly="true" runat="server" CssClass="form-control"
                                                class="text_box" onkeypress="return Alphabet_Number(event,this);" MaxLength="30"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Language Known :
                                       
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_Language_Known" ReadOnly="true" runat="server" CssClass="form-control" class="text_box"
                                                meta:resourcekey="txt_LanguageResource1" onkeypress="return Alphabet_Number10(event,this);" MaxLength="100"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Area Of Expertise :
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_Area_Of_Expertise" ReadOnly="true" runat="server" CssClass="form-control" class="text_box" MaxLength="20 " onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Passport Validity Date :
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_Passport_Validity_Date" ReadOnly="true" runat="server" MaxLength="20" class="text_box" CssClass="form-control pass_vissa_passport" Width="100%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Visa Validity Date : 
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_Visa_Validity_Date" runat="server" ReadOnly="true" CssClass="form-control pass_vissa" class="text_box" Width="100%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Details Of Handicap  :
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_Details_Of_Handicap" runat="server" ReadOnly="true" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control" class="text_box" MaxLength="20"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Height (in Feets) :
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_height" runat="server" ReadOnly="true" class="form-control text_box" onkeypress="return isNumber_dot(event)" MaxLength="10" meta:resourcekey="txt_heightResource1">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                            Blood Group :
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:DropDownList ID="ddl_bloodgroup" Enabled="false" runat="server" CssClass="form-control" meta:resourcekey="ddl_bloodgroupResource1">
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
                        <div id="menu2" class="tab-pane fade">
                            <div class="row">
                                <div class="col-sm-6 col-xs-12" style="border-right: 1px solid #808080;">
                                    <div class="row text-center">
                                        <h3>Qualification</h3>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-8 col-xs-12">
                                            Qualification1 :
                                      
                                                     <asp:TextBox ID="txt_qualification_1" ReadOnly="true" runat="server" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_Number(event,this);" MaxLength="20"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-xs-12">
                                            Year of passing :
                                      
                                                     <asp:TextBox ID="txt_year_of_passing_1" ReadOnly="true" onkeypress="return isNumber(event)" MaxLength="4" class="text_box" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <br />
                                        <div class="col-sm-8 col-xs-12">
                                            Qualification2 :
                                                     <asp:TextBox ID="txt_qualification_2" ReadOnly="true" runat="server" CssClass="form-control" class="text_box" MaxLength="15" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-xs-12">
                                            <br />
                                            <asp:TextBox ID="txt_year_of_passing_2" ReadOnly="true" onkeypress="return isNumber(event)" class="text_box" MaxLength="4" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <br />
                                        <div class="col-sm-8 col-xs-12">
                                            Qualification3 :
                                                     <asp:TextBox ID="txt_qualification_3" ReadOnly="true" runat="server" CssClass="form-control" class="text_box"
                                                         onkeypress="return Alphabet_Number(event,this);" MaxLength="20"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-xs-12">
                                            <br />
                                            <asp:TextBox ID="txt_year_of_passing_3" ReadOnly="true" onkeypress="return isNumber(event)" class="text_box"
                                                runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <br />
                                        <div class="col-sm-8 col-xs-12">
                                            Qualification4 :
                                                     <asp:TextBox ID="txt_qualification_4" ReadOnly="true" runat="server" CssClass="form-control" class="text_box" onkeypress="return Alphabet_Number(event,this);" MaxLength="15"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-xs-12">
                                            <br />
                                            <asp:TextBox ID="txt_year_of_passing_4" ReadOnly="true" onkeypress="return isNumber(event)" class="text_box" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <br />
                                        <div class="col-sm-8 col-xs-12">
                                            Qualification5 :
                                                     <asp:TextBox ID="txt_qualification_5" ReadOnly="true" runat="server" CssClass="form-control" class="text_box" onkeypress="return Alphabet_Number(event,this);" MaxLength="15"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-xs-12">
                                            <br />
                                            <asp:TextBox ID="txt_year_of_passing_5" ReadOnly="true" onkeypress="return isNumber(event)" runat="server" MaxLength="4" class="text_box" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="row text-center">
                                        <h3>Skill</h3>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-8 col-xs-12">
                                            Key Skill1 : 
                                                     <asp:TextBox ID="txt_key_skill_1" ReadOnly="true" runat="server" CssClass="form-control" class="text_box" onkeypress="return Alphabet_Number(event,this);" MaxLength="50"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-xs-12">
                                            Experience in months 
                                                      <asp:TextBox ID="txt_experience_in_months_1" ReadOnly="true" runat="server" CssClass="form-control" class="text_box" onkeypress="return Alphabet_Number(event,this);" MaxLength="20">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <br />
                                        <div class="col-sm-8 col-xs-12">
                                            Key Skill2 :
                                                     <asp:TextBox ID="txt_key_skill_2" runat="server" ReadOnly="true" CssClass="form-control" class="text_box" onkeypress="return Alphabet_Number(event,this);" MaxLength="20"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-xs-12">
                                            <br />
                                            <asp:TextBox ID="txt_experience_in_months_2" runat="server" ReadOnly="true" CssClass="form-control" onkeypress="return Alphabet_Number(event,this);" MaxLength="20"> 0</asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <br />
                                        <div class="col-sm-8 col-xs-12">
                                            Key Skill3 :
                                                     <asp:TextBox ID="txt_key_skill_3" runat="server" ReadOnly="true" CssClass="form-control" class="text_box" onkeypress="return Alphabet_Number(event,this);" MaxLength="20"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-xs-12">
                                            <br />
                                            <asp:TextBox ID="txt_experience_in_months_3" runat="server" ReadOnly="true" class="text_box" CssClass="form-control" onkeypress="return Alphabet_Number(event,this);" MaxLength="20">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <br />
                                        <div class="col-sm-8 col-xs-12">
                                            Key Skill4 :
                                                     <asp:TextBox ID="txt_key_skill_4" ReadOnly="true" runat="server" class="text_box" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-xs-12">
                                            <br />
                                            <asp:TextBox ID="txt_experience_in_months_4" ReadOnly="true" runat="server" class="text_box" CssClass="form-control" onkeypress="return Alphabet_Number(event,this);" MaxLength="10">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <br />
                                        <div class="col-sm-8 col-xs-12">
                                            Key Skill5 :
                                                     <asp:TextBox ID="txt_key_skill_5" ReadOnly="true" runat="server" CssClass="form-control" class="text_box" MaxLength="10" onkeypress="return Alphabet_Number(event,this);"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-xs-12">
                                            <br />
                                            <asp:TextBox ID="txt_experience_in_months_5" ReadOnly="true" runat="server" CssClass="form-control">0</asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <%-- <div id="menu3" class="tab-pane fade">
                                        <table class="table table-bordered table-responsive">
                                            <tr>
                                                <th></th>
                                                <th>Opening Balance</th>
                                                <th>Allocated</th>
                                                <th>Taken</th>
                                                <th>Balance</th>
                                            </tr>
                                            <tr>
                                                <td>Leave</td>
                                                <td>
                                                    <asp:TextBox ID="txt_clopeningbalance" runat="server" onkeypress="return isNumber(event)" meta:resourcekey="txt_clopeningbalanceResource1" class="form-control">30</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_clallocated" runat="server" onkeypress="return isNumber(event)" meta:resourcekey="txt_clallocatedResource1" class="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_cltaken" runat="server" onkeypress="return isNumber(event)" meta:resourcekey="txt_cltakenResource1" class="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_clbalance" runat="server" onkeypress="return isNumber(event)" meta:resourcekey="txt_clbalanceResource1" class="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <div class="row">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_Sick_Leave" runat="server" Text="Sick Leave" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_slopeningbalance" runat="server" class="form-control" meta:resourceKey="txt_slopeningbalanceResource1" onkeypress="return isNumber(event)" Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_slallocated" runat="server" class="form-control" meta:resourceKey="txt_slallocatedResource1" onkeypress="return isNumber(event)" Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_sltaken" runat="server" class="form-control" meta:resourceKey="txt_sltakenResource1" onkeypress="return isNumber(event)" Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_slbalance" runat="server" class="form-control" meta:resourceKey="txt_slbalanceResource1" onkeypress="return isNumber(event)" Visible="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </div>
                                            <div class="row">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_leave" runat="server" Text="Privilaged Leave" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_plopningbalance" runat="server" class="form-control" meta:resourceKey="txt_plopningbalanceResource1" onkeypress="return isNumber(event)" Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_plallocated" runat="server" class="form-control" meta:resourceKey="txt_plallocatedResource1" onkeypress="return isNumber(event)" Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_pltaken" runat="server" class="form-control" meta:resourceKey="txt_pltakenResource1" onkeypress="return isNumber(event)" Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_plbalance" runat="server" class="form-control" meta:resourceKey="txt_plbalanceResource1" onkeypress="return isNumber(event)" Visible="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </div>
                                        </table>

                                    </div>--%>
                        <div id="menu4" class="tab-pane fade">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <ul class="nav nav-tabs">
                                        <li id="itemtab1" class="active"><a data-toggle="tab" href="#item1" runat="server">Earning Heads</a></li>
                                        <li id="itemtab2"><a data-toggle="tab" href="#item2">Lumpsum Heads</a></li>
                                        <li id="itemtab3"><a data-toggle="tab" href="#item3">Deduction Heads</a></li>
                                        <li id="itemtab4"><a data-toggle="tab" href="#item4">Earning Issues</a></li>
                                    </ul>

                                    <div class="tab-content">
                                        <div id="item1" class="tab-pane fade in active">
                                            <br />
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-sm-2 col-xs-12">
                                                            Job Type
                                                            <asp:DropDownList ID="ddl_Jobtype" runat="server" AutoPostBack="True" class="form-control gender" meta:resourceKey="ddl_employmentstatusResource1">
                                                                <asp:ListItem meta:resourceKey="ListItemResource7" Value="Permanent" Text="Permanent"></asp:ListItem>
                                                                <asp:ListItem meta:resourceKey="ListItemResource6" Value="Contract" Text="Contract"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12">
                                                            Status
                                                            <asp:DropDownList ID="ddl_employmentstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_employmentstatus_SelectedIndexChanged" class="form-control gender" meta:resourceKey="ddl_employmentstatusResource1">
                                                                <asp:ListItem meta:resourceKey="ListItemResource6" Text="Monthly"></asp:ListItem>
                                                                <asp:ListItem meta:resourceKey="ListItemResource6" Text="Daily"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12 ">
                                                            <asp:Label ID="lbl_ctc" runat="server" Text=" CTC(Annual Income)"></asp:Label>
                                                            <asp:TextBox ID="txt_amount" MaxLength="15" AutoPostBack="true" runat="server" ReadOnly="true" OnTextChanged="calculate" class="text_box" onkeypress="return isNumber(event)" CssClass="form-control" meta:resourcekey="txtlhead1Resource1" Width="100%">0</asp:TextBox></td>
                                                        </div>
                                                    </div>
                                                    <div class="row ">
                                                        <br />
                                                        <div class="col-sm-2 col-xs-12">
                                                            <asp:Label ID="lblhead1" runat="server" meta:resourcekey="lblhead1Resource1"></asp:Label>
                                                            <asp:Label ID="Label21" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                                            <asp:TextBox ID="txtlhead1" MaxLength="11" runat="server" ReadOnly="true" class="text_box" onkeypress="return isNumber_dot(event)" AutoPostBack="True" CssClass="form-control" meta:resourcekey="txtlhead1Resource1" Width="100%">0</asp:TextBox></td>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12 ">
                                                            <asp:Label ID="lblhead2" runat="server" meta:resourcekey="lblhead2Resource1"></asp:Label>
                                                            <asp:TextBox ID="txtlhead2" runat="server" MaxLength="6" ReadOnly="true" class="text_box" onkeypress="return isNumber_dot(event)" AutoPostBack="True" CssClass="form-control" meta:resourcekey="txtlhead2Resource1" Width="100%">0</asp:TextBox></td>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12 ">
                                                            <asp:Label ID="lblhead3" runat="server" meta:resourcekey="lblhead3Resource1"></asp:Label>

                                                            <asp:TextBox ID="txtlhead3" runat="server" MaxLength="6" ReadOnly="true" class="text_box" onkeypress="return isNumber_dot(event)" AutoPostBack="True" CssClass="form-control" meta:resourcekey="txtlhead3Resource1" Width="100%">0</asp:TextBox></td>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12">
                                                            <asp:Label ID="lblhead4" runat="server" meta:resourcekey="lblhead4Resource1"></asp:Label>

                                                            <asp:TextBox ID="txtlhead4" runat="server" MaxLength="6" ReadOnly="true" onkeypress="return isNumber(event)" AutoPostBack="True" class="form-control text_box" meta:resourcekey="txtlhead4Resource1" Width="100%">0</asp:TextBox></td>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12 ">
                                                            <asp:Label ID="lblhead5" runat="server" meta:resourcekey="lblhead5Resource1"></asp:Label>


                                                            <asp:TextBox ID="txtlhead5" runat="server" MaxLength="6" ReadOnly="true" onkeypress="return isNumber_dot(event)" AutoPostBack="True" class="form-control text_box" Width="100%"
                                                                meta:resourcekey="txtlhead5Resource1">0</asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12">
                                                            <asp:Label ID="lblhead6" runat="server" meta:resourcekey="lblhead6Resource1"></asp:Label>

                                                            <asp:TextBox ID="txtlhead6" runat="server" MaxLength="6" ReadOnly="true" onkeypress="return isNumber_dot(event)" AutoPostBack="True" class="form-control text_box" Width="100%"
                                                                meta:resourcekey="txtlhead6Resource1">0</asp:TextBox>
                                                        </div>

                                                    </div>

                                                    <div class="row">
                                                        <br />
                                                        <div class="col-sm-2 col-xs-12 ">
                                                            <asp:Label ID="lblhead7" runat="server" meta:resourcekey="lblhead7Resource1"></asp:Label>

                                                            <asp:TextBox ID="txtlhead7" runat="server" MaxLength="6" ReadOnly="true" onkeypress="return isNumber_dot(event)" AutoPostBack="True" class="form-control text_box" Width="100%"
                                                                meta:resourcekey="txtlhead7Resource1">0</asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12 ">
                                                            <asp:Label ID="lblhead8" runat="server" meta:resourcekey="lblhead8Resource1"></asp:Label>

                                                            <asp:TextBox ID="txtlhead8" runat="server" MaxLength="6" ReadOnly="true" onkeypress="return isNumber_dot(event)" AutoPostBack="True" class="form-control text_box" Width="100%"
                                                                meta:resourcekey="txtlhead8Resource1">0</asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12 ">
                                                            <asp:Label ID="lblhead9" runat="server" meta:resourcekey="lblhead9Resource1"></asp:Label>

                                                            <asp:TextBox ID="txtlhead9" runat="server" MaxLength="6" ReadOnly="true" onkeypress="return isNumber_dot(event)" AutoPostBack="True" class="form-control text_box" Width="100%"
                                                                meta:resourcekey="txtlhead9Resource1">0</asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12 ">
                                                            <asp:Label ID="lblhead10" runat="server" MaxLength="6" meta:resourcekey="lblhead10Resource1"></asp:Label>

                                                            <asp:TextBox ID="txtlhead10" runat="server" MaxLength="6" ReadOnly="true" onkeypress="return isNumber_dot(event)" AutoPostBack="True" class="form-control text_box"
                                                                Width="100%" meta:resourcekey="txtlhead10Resource1">0</asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12">
                                                            <asp:Label ID="lblhead11" runat="server" meta:resourcekey="lblhead11Resource1"></asp:Label>

                                                            <asp:TextBox ID="txtlhead11" runat="server" AutoPostBack="True" ReadOnly="true" MaxLength="6" class="form-control text_box"
                                                                Width="100%" onkeypress="return isNumber_dot(event)"
                                                                meta:resourcekey="txtlhead11Resource1">0</asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12 ">
                                                            <asp:Label ID="lblhead12" runat="server" meta:resourcekey="lblhead12Resource1"></asp:Label>

                                                            <asp:TextBox ID="txtlhead12" runat="server" AutoPostBack="True" MaxLength="6" ReadOnly="true" class="form-control text_box"
                                                                Width="100%" onkeypress="return isNumber_dot(event)"
                                                                meta:resourcekey="txtlhead12Resource1">0</asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <br />
                                                        <div class="col-sm-2 col-xs-12 ">
                                                            <asp:Label ID="lblhead13" runat="server" meta:resourcekey="lblhead13Resource1"></asp:Label>

                                                            <asp:TextBox ID="txtlhead13" runat="server" AutoPostBack="True" ReadOnly="true" MaxLength="6" class="form-control text_box"
                                                                Width="100%" onkeypress="return isNumber_dot(event)"
                                                                meta:resourcekey="txtlhead13Resource1">0</asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12 ">
                                                            <asp:Label ID="lblhead14" runat="server" meta:resourcekey="lblhead14Resource1"></asp:Label>

                                                            <asp:TextBox ID="txtlhead14" runat="server" AutoPostBack="True" ReadOnly="true" class="form-control text_box"
                                                                Width="100%" onkeypress="return isNumber_dot(event)"
                                                                meta:resourcekey="txtlhead14Resource1" MaxLength="6">0</asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12 ">
                                                            <asp:Label ID="lblhead15" runat="server" meta:resourcekey="lblhead15Resource1"></asp:Label>

                                                            <asp:TextBox ID="txtlhead15" runat="server" AutoPostBack="True" ReadOnly="true" MaxLength="6" class="form-control text_box"
                                                                Width="100%" onkeypress="return isNumber_dot(event)"
                                                                meta:resourcekey="txtlhead15Resource1">0</asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12 ">
                                                            Ot Rate :

                                                        <span>
                                                            <asp:TextBox ID="txt_basicpay" runat="server" AutoPostBack="True" ReadOnly="true" MaxLength="6" class="form-control text_box" onkeypress="return isNumber_dot(event)"
                                                                meta:resourcekey="txt_basicpayResource1">0</asp:TextBox></span>
                                                        </div>
                                                        <div class="col-sm-2 col-xs-12">
                                                            Total :

                                                        <asp:TextBox ID="txtetotal0" runat="server" AutoPostBack="True" MaxLength="6" ReadOnly="true" class="form-control text_box" onkeypress="return isNumber_dot(event)" meta:resourcekey="txtetotal0Resource1" Width="100%">0</asp:TextBox></td>
                                                        </div>
                                                        <div class="col-sm-3 col-xs-12 ">
                                                            <span>
                                                                <br />
                                                                <asp:CheckBox ID="chk_updaterating" Visible="False" runat="server" ReadOnly="true" AutoPostBack="True" meta:resourcekey="chk_updateratingResource1" Text="Update Rate from Unit Grade Master" /></span>
                                                        </div>

                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddl_employmentstatus" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <br />
                                        <div id="item2" class="tab-pane fade">

                                            <div class="row">
                                                <div class="col-sm-3 col-xs-12 ">
                                                    <asp:Label ID="lbllsehead1" runat="server"
                                                        meta:resourcekey="lbllsehead1Resource1"></asp:Label>

                                                    <asp:TextBox ID="txtlsehead1" runat="server" class="form-control text_box" onkeypress="return isNumber_dot(event)" ReadOnly="true"
                                                        Width="100%" meta:resourcekey="txtlsehead1Resource1">0</asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12 ">
                                                    <asp:Label ID="lbllsehead2" runat="server"
                                                        meta:resourcekey="lbllsehead2Resource1"></asp:Label>

                                                    <asp:TextBox ID="txtlsehead2" runat="server" class="form-control text_box" ReadOnly="true" onkeypress="return isNumber_dot(event)"
                                                        Width="100%" meta:resourcekey="txtlsehead2Resource1" MaxLength="6">0</asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12 ">
                                                    <asp:Label ID="lbllsehead3" runat="server"
                                                        meta:resourcekey="lbllsehead3Resource1"></asp:Label>

                                                    <asp:TextBox ID="txtlsehead3" runat="server" class="form-control text_box" ReadOnly="true" MaxLength="6" onkeypress="return isNumber_dot(event)"
                                                        Width="100%" meta:resourcekey="txtlsehead3Resource1">0</asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12 ">
                                                    <asp:Label ID="lbllsehead4" runat="server"
                                                        meta:resourcekey="lbllsehead4Resource1"></asp:Label>

                                                    <asp:TextBox ID="txtlsehead4" runat="server" class="form-control text_box" ReadOnly="true" onkeypress="return isNumber_dot(event)"
                                                        meta:resourcekey="txtlsehead4Resource1" MaxLength="6">0</asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12 ">
                                                    <asp:Label ID="lbllsehead5" runat="server"
                                                        meta:resourcekey="lbllsehead5Resource1"></asp:Label>

                                                    <asp:TextBox ID="txtlsehead5" runat="server" Width="100%" ReadOnly="true" onkeypress="return isNumber(event)"
                                                        class="form-control text_box" meta:resourcekey="txtlsehead5Resource1" MaxLength="6">0</asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div id="item3" class="tab-pane fade">
                                            <br />
                                            <div class="row">

                                                <div class="col-sm-2 col-xs-12 ">
                                                    <asp:Label ID="lbldhead2" runat="server" meta:resourcekey="lbldhead2Resource1"></asp:Label>

                                                    <asp:TextBox ID="txtdhead2" runat="server" class="form-control" ReadOnly="true" MaxLength="6" Width="100%" onkeypress="return isNumber_dot(event)"
                                                        meta:resourcekey="txtdhead2Resource1">0</asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12 text-left">
                                                    <asp:Label ID="lbldhead3" runat="server" meta:resourcekey="lbldhead3Resource1"></asp:Label>

                                                    <asp:TextBox ID="txtdhead3" runat="server" class="form-control text_box" ReadOnly="true" Width="100%"
                                                        onkeypress="return isNumber_dot(event)" meta:resourcekey="txtdhead3Resource1">0</asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    <asp:Label ID="lbldhead4" runat="server" meta:resourcekey="lbldhead4Resource1"></asp:Label>

                                                    <asp:TextBox ID="txtdhead4" runat="server" class="form-control text_box" ReadOnly="true" Width="100%"
                                                        onkeypress="return isNumber_dot(event)" meta:resourcekey="txtdhead4Resource1" MaxLength="6">0</asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12 ">
                                                    <asp:Label ID="lbldhead5" runat="server" meta:resourcekey="lbldhead5Resource1"></asp:Label>

                                                    <asp:TextBox ID="txtdhead5" runat="server" class="form-control text_box" ReadOnly="true" Width="100%"
                                                        onkeypress="return isNumber_dot(event)" meta:resourcekey="txtdhead5Resource1" MaxLength="6">0</asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12 ">
                                                    <asp:Label ID="lbldhead6" runat="server" meta:resourcekey="lbldhead6Resource1"></asp:Label>

                                                    <asp:TextBox ID="txtdhead6" runat="server" class="form-control text_box" Width="100%" ReadOnly="true"
                                                        onkeypress="return isNumber_dot(event)" meta:resourcekey="txtdhead6Resource1" MaxLength="6">0</asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <br />
                                                <div class="col-sm-2 col-xs-12">
                                                    <asp:Label ID="lbldhead7" runat="server" meta:resourcekey="lbldhead7Resource1"></asp:Label>

                                                    <asp:TextBox ID="txtdhead7" runat="server" class="form-control text_box" Width="100%" ReadOnly="true"
                                                        onkeypress="return isNumber_dot(event)" meta:resourcekey="txtdhead7Resource1" MaxLength="6">0</asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12 ">
                                                    <asp:Label ID="lbldhead8" runat="server" meta:resourcekey="lbldhead8Resource1"></asp:Label>

                                                    <asp:TextBox ID="txtdhead8" runat="server" ReadOnly="true" onkeypress="return isNumber_dot(event)" class="form-control text_box" Width="100%"
                                                        meta:resourcekey="txtdhead8Resource1" MaxLength="6">0</asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    <asp:Label ID="lbldhead9" runat="server" meta:resourcekey="lbldhead9Resource1"></asp:Label>

                                                    <asp:TextBox ID="txtdhead9" runat="server" ReadOnly="true" onkeypress="return isNumber_dot(event)" class="form-control text_box" Width="100%"
                                                        meta:resourcekey="txtdhead9Resource1" MaxLength="6">0</asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12 ">
                                                    <asp:Label ID="lbldhead10" runat="server"
                                                        meta:resourcekey="lbldhead10Resource1"></asp:Label>

                                                    <asp:TextBox ID="txtdhead10" runat="server" ReadOnly="true" onkeypress="return isNumber_dot(event)" class="form-control text_box"
                                                        Width="100%" meta:resourcekey="txtdhead10Resource1" MaxLength="6">0</asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div id="item4" class="tab-pane fade">

                                            <div class="row">
                                                <br />
                                                <div class="col-sm-6 col-xs-12">
                                                    <asp:Label ID="lbl_earning_issue" runat="server" Text="Earning Issues"
                                                        meta:resourcekey="lbldhead6Resource1"></asp:Label>

                                                    <asp:TextBox ID="txt_highestqualification" ReadOnly="true" runat="server" placeholder="Please Enter Earning Issues" class="form-control text_box" TextMode="MultiLine" Height="50px"
                                                        meta:resourcekey="txtdhead6Resource1" MaxLength="15"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    Icard Issu Date :
                                
                                                  <asp:TextBox ID="txt_icardissudate" MaxLength="100" ReadOnly="true" runat="server" Width="100%" class="form-control date-picker"
                                                      meta:resourcekey="txt_confirmationdateResource1"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    Uniform Issu Date :
                                 
                                                  <asp:TextBox ID="txt_uniformissudate" runat="server" ReadOnly="true" class="form-control date-picker"
                                                      meta:resourcekey="txt_confirmationdateResource1"></asp:TextBox>
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="menu5" class="tab-pane fade">

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                Photo(Passport Size) :
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="photo_upload" ReadOnly="true" runat="server" Visible="false" meta:resourcekey="photo_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Image ID="Image4" runat="server" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/placeholder.png" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                Adhar Card/Pan Card : 
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="adhar_pan_upload" ReadOnly="true" Visible="false" runat="server" meta:resourcekey="adhar_pan_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Image ID="Image1" runat="server" meta:resourcekey="Image1Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/pan.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                Bank Passbook :
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="bank_upload" Visible="false" ReadOnly="true" runat="server" meta:resourcekey="bank_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Image ID="Image2" runat="server" meta:resourcekey="Image2Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/passbook.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                Biodata :
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="biodata_upload" Visible="false" runat="server" meta:resourcekey="biodata_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Image ID="Image3" runat="server" meta:resourcekey="Image3Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/Biodata.png" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                Passport :
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Passport_upload" Visible="false" runat="server" meta:resourcekey="Passport_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Image ID="Image5" runat="server" meta:resourcekey="Image5Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/Passport.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                Driving Liscence :
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Driving_Liscence_upload" Visible="false" runat="server" meta:resourcekey="Driving_Liscence_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Image ID="Image6" runat="server" meta:resourcekey="Image6Resource1" onkeypress="return AllowAlphabet_Number(event,this);" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/Driving_liscence.jpg" />
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                10th Marksheet :
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Tenth_Marksheet_upload" Visible="false" runat="server" meta:resourcekey="Tenth_Marksheet_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Image ID="Image7" runat="server" meta:resourcekey="Image7Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/marksheet.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                12th Marksheet :
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Twelve_Marksheet_upload" Visible="false" runat="server" meta:resourcekey="Twelve_Marksheet_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Image ID="Image8" runat="server" meta:resourcekey="Image8Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/marksheet.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                Diploma Certificate :
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Diploma_upload" Visible="false" runat="server" meta:resourcekey="Diploma_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Image ID="Image9" runat="server" meta:resourcekey="Image9Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                Degree Certificate :
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Degree_upload" Visible="false" runat="server" meta:resourcekey="Degree_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Image ID="Image10" runat="server" meta:resourcekey="Image10Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                Post Graduation Certificate :
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Post_Graduation_upload" Visible="false" runat="server" meta:resourcekey="Post_Graduation_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Image ID="Image11" runat="server" meta:resourcekey="Image11Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                Education Certificate :
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Education_4_upload" Visible="false" runat="server" meta:resourcekey="Education_4_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Image ID="Image12" runat="server" meta:resourcekey="Image12Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>
                                        </br>
                                     <div class="row">
                                         <div class="col-sm-4 col-xs-12">
                                             Police Verification Document :
                                         </div>
                                         <div class="col-sm-5 col-xs-12">
                                             <asp:FileUpload ID="Police_document" Visible="false" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                         </div>
                                         <div class="col-sm-3 col-xs-12">
                                             <asp:Image ID="Image14" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                         </div>
                                     </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                Form No 2 :
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Formno_2" Visible="false" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Image ID="Image15" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                Form No111 :
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Formno_11" Visible="false" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Image ID="Image16" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>

                                    </div>
                                    <br />
                                    <div class="row text-center">
                                        <asp:Button ID="btnUpload" Visible="false" runat="server" CssClass="btn btn-primary" meta:resourcekey="btnUploadResource1" Text="Upload" />
                                    </div>
                                    <br />
                                    <div class="row text-center">
                                        Note : Only JPG and PNG files will be uploaded.
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                        <div id="menu8" class="tab-pane fade">
                            <br />
                            <div class="row">
                                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12 text-right">Total Loan :</div>
                                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                                    <asp:TextBox ID="txt_advance_payment" ReadOnly="true" runat="server" class="form-control" onkeypress="return isNumber(event)" meta:resourceKey="txt_advance_paymentResource1"></asp:TextBox>
                                </div>
                                <div class="col-lg-1 col-md-4 col-sm-6 col-xs-12 text-right">EMI :</div>
                                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                                    <asp:TextBox ID="txtdhead1" runat="server" ReadOnly="true" class="form-control" onkeypress="return isNumber(event)" meta:resourceKey="txtdhead1Resource1"></asp:TextBox>
                                </div>
                                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12 text-right">Start Date for EMI :</div>
                                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                                    <asp:TextBox ID="txt_loandate" runat="server" ReadOnly="true" class="form-control date-EMI" meta:resourceKey="txt_loandateResource1" Width="140"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <div id="menu9" class="tab-pane fade">
                            <br />
                            <div class="row">
                                <div class="col-lg-1 col-md-4 col-sm-6 col-xs-12 text-right">No of Child :</div>
                                <div class="col-lg-1 col-md-4 col-sm-6 col-xs-12">
                                    <asp:TextBox ID="Numberchild" runat="server" ReadOnly="true" class="form-control" onkeypress="return isNumber(event)" meta:resourceKey="txt_advance_paymentResource1"></asp:TextBox>
                                </div>

                            </div>
                            <br />
                            <table id="maintable" class="table table-responsive main_table" border="1" runat="server">
                                <tr style="background-color: #aaa;">
                                    <th align="center">Name</th>
                                    <th align="center">Relation</th>
                                    <th align="center">DOB</th>
                                    <th align="center">PAN No</th>
                                    <th align="center">Adhaar No</th>
                                    <th align="center">Mobile No</th>

                                </tr>
                                <tr id="rows">
                                    <td>
                                        <asp:TextBox ID="txt_name1" ReadOnly="true" runat="server" MaxLength="20" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_relation1" ReadOnly="true" runat="server" MaxLength="20" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control">Father</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_dob1" runat="server" ReadOnly="true" CssClass="form-control date-picker"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_pan1" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return AllowAlphabet_Number_slash(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_adhaar1" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_fmobile1" runat="server" ReadOnly="true" MaxLength="10" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="rows2">
                                    <td>
                                        <asp:TextBox ID="txt_name2" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_relation2" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control">Mother</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_dob2" runat="server" ReadOnly="true" MaxLength="20" CssClass="form-control date-picker"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_pan2" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_adhaar2" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_fmobile2" runat="server" ReadOnly="true" MaxLength="10" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="rows3">
                                    <td>
                                        <asp:TextBox ID="txt_name3" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_relation3" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control">Wife</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_dob3" runat="server" ReadOnly="true" CssClass="form-control date-picker"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_pan3" runat="server" MaxLength="20" ReadOnly="true" onkeypress="return AllowAlphabet_Number_slash(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_adhaar3" runat="server" MaxLength="20" ReadOnly="true" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_fmobile3" runat="server" MaxLength="10" ReadOnly="true" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="rows4">
                                    <td>
                                        <asp:TextBox ID="txt_name4" runat="server" MaxLength="20" ReadOnly="true" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_relation4" runat="server" MaxLength="20" ReadOnly="true" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control">Child</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_dob4" runat="server" ReadOnly="true" CssClass="form-control date-picker"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_pan4" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return AllowAlphabet_Number_slash(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_adhaar4" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_fmobile4" runat="server" ReadOnly="true" MaxLength="10" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="rows5">
                                    <td>
                                        <asp:TextBox ID="txt_name5" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_relation5" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control">Child</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_dob5" runat="server" ReadOnly="true" CssClass="form-control date-picker"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_pan5" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return AllowAlphabet_Number_slash(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_adhaar5" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_fmobile5" runat="server" ReadOnly="true" MaxLength="10" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="rows6">
                                    <td>
                                        <asp:TextBox ID="txt_name6" runat="server" MaxLength="20" ReadOnly="true" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_relation6" runat="server" MaxLength="20" ReadOnly="true" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control">Child</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_dob6" runat="server" ReadOnly="true" CssClass="form-control date-picker"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_pan6" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return AllowAlphabet_Number_slash(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_adhaar6" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_fmobile6" runat="server" ReadOnly="true" MaxLength="10" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="rows7">
                                    <td>
                                        <asp:TextBox ID="txt_name7" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_relation7" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control">Child</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_dob7" runat="server" ReadOnly="true" CssClass="form-control date-picker"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_pan7" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return AllowAlphabet_Number_slash(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_adhaar7" runat="server" ReadOnly="true" MaxLength="20" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_fmobile7" runat="server" ReadOnly="true" MaxLength="10" onkeypress="return isNumber(event,this);" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>

                        </div>
                        <div id="menu11" class="tab-pane fade">
                            <br />
                            <div class="row">
                                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12 text-right">Key Responsibility Area :</div>
                                <div class="col-lg-6 col-md-6 col-sm- col-xs-12">
                                    <asp:TextBox ID="txt_kra" runat="server" ReadOnly="true" TextMode="MultiLine" CssClass="form-control" Rows="6"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>


                    <br />

                    <div class="row text-center">

                        <asp:Button
                            ID="btncloselow" runat="server" class="btn btn-danger" OnClick="btnclose_Click"
                            Text="Close" CausesValidation="False"
                            meta:resourcekey="btncloseResource1" />

                    </div>





                </asp:Panel>
            </div>
        </asp:Panel>

    </div>

</asp:Content>

