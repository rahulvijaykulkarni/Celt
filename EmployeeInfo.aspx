<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeInfo.aspx.cs" Inherits="EmployeeInfo" Title="Employee Master" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Employee Information</title>
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
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>



    <script type="text/javascript">

        $(document).ready(function () {
            var table = $



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
               .appendTo('#<%=gv_product_details.ClientID%>_wrapper .col-sm-12:eq(0)');
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
        }

        .form-control {
            display: inline;
        }

        .grid-view {
            height: auto;
            max-height: 400px;
            overflow-y: auto;
            overflow-x: auto;
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

        #ctl00_cph_righrbody_SearchGridView_filter {
            margin-left: 38em;
        }

        #ctl00_cph_righrbody_SearchGridView_paginate {
            margin-left: 25em;
            margin-top: -5em;
        }

        #ctl00_cph_righrbody_gv_product_details_filter {
            margin-left: 32em;
        }

        #ctl00_cph_righrbody_gv_product_details_paginate {
            margin-left: 38em;
            margin-top: -5em;
        }
    </style>
    <script>

        function copy_add() {
            //  var add1 = document.getElementById("<%= txt_presentaddress.ClientID %>");
            //  var add2 = document.getElementById("<%= txt_permanantaddress.ClientID %>");
            //  add2.value = add1.value;
            //  var state1 = document.getElementById("<%= ddl_state.ClientID %>");
            //  var state2 = document.getElementById("<%= ddl_permstate.ClientID %>");
            //state2.value = state1.value;
            //var city1 = document.getElementById("<%= txt_presentcity.ClientID %>");
            //var city2 = document.getElementById("<%= txt_permanantcity.ClientID %>");
            // city2.value = city1.value;
            //pincode
            // var pin1 = document.getElementById("<%= txt_presentpincode.ClientID %>");
            //var pin2 = document.getElementById("<%= txt_permanantpincode.ClientID %>");
            //pin2.value = pin1.value;

            //mobile no
            // var mobile1 = document.getElementById("<%= txt_mobilenumber.ClientID %>");
            //var mobile2 = document.getElementById("<%= txtref2mob.ClientID %>");

            // mobile2.value = mobile1.value;
            // newpanel.Visible = true;
        }
    </script>
    <script type="text/javascript">
        function pageLoad() {

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
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                yearRange: '1950',
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



            var txt_left_date = document.getElementById('<%=txt_leftdate.ClientID %>');
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
                yearRange: '1950',
                onSelect: function (selected) {
                    $(".date_join").datepicker("option", "minDate", selected)
                }
            });


            $(".date_join").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                yearRange: '1950',
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
                        // Earning 
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
        }

        function emp_type() {
            var t_emptype = document.getElementById('<%=ddl_employee_type.ClientID %>');
            var s_emp_type = t_emptype.options[t_emptype.selectedIndex].text;

            var t_clientname = document.getElementById('<%=ddl_unit_client.ClientID %>');

            if (s_emp_type == "Staff") {
                //t_clientname.options[0].selected = true;
                t_clientname.disabled = true;
            }
            else { t_clientname.disabled = false; }
            return true;
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
                  alert("Please Select Designation Type !!!");
                  select_type.focus();
                  return false;
              }

              if (Selected_number_of_set == "Select") {
                  alert("Please Select  Number of Sets !!!");
                  number_of_set.focus();
                  return false;
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
        function openWindow() {
            window.open("html/employee_info.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
    </script>

    <div class="container-fluid">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Panel runat="server" CssClass="panel panel-primary">

            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; text-align: center; font-size: small;"><b>EMPLOYEE INFORMATION</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image13" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
             <br />
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Employee Information Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">

                <div class="col-sm-2 col-xs-12">
                    <%-- Enter Employee Code/Name :--%>
                </div>
                <div class="col-sm-4 col-xs-12">
                    <asp:TextBox ID="txtsearchempid" Visible="false" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>
                </div>
                <br />


                <asp:Panel ID="newpanel" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-2 col-xs-12 text-left">
                                   <b> Employee Type :</b><span class="text-red"> *</span>
                                    <asp:TextBox ID="ddl_employee_type" runat="server" class="form-control text_box"></asp:TextBox>
                                    <%--<asp:DropDownList ID="ddl_employee_type" runat="server" class="form-control" >
                                         
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="Permanent">Permanent</asp:ListItem>
                                        <asp:ListItem Value="PermanentReliever">Permanent Reliever</asp:ListItem>
                                        <asp:ListItem Value="Reliever">Reliever</asp:ListItem>
                                        <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                        <asp:ListItem Value="Temporary">Temporary</asp:ListItem>
                                        <asp:ListItem Value="RM">Repair & Maintenance</asp:ListItem>
                                    </asp:DropDownList>--%>
                                </div>
                                <div class="col-sm-2 col-xs-12 text-left">
                                   <b> Client Name :</b><span class="text-red"> *</span>
                                    <asp:TextBox ID="ddl_unit_client" runat="server" class="form-control text_box"></asp:TextBox>
                                    <%--<asp:DropDownList ID="ddl_unit_client" class="form-control pr_state js-example-basic-single" Width="100%" runat="server" OnSelectedIndexChanged="ddl_clientname_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>--%>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <b>State :</b><span class="text-red"> *</span>
                                    <asp:TextBox ID="ddl_clientwisestate" runat="server" class="form-control text_box"></asp:TextBox>
                                    <%-- <asp:DropDownList ID="ddl_clientwisestate" runat="server" class="pr_state js-example-basic-single" Width="100%" OnSelectedIndexChanged="get_city_list1" AutoPostBack="true">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>--%>
                                </div>
                                <div class="col-sm-2 col-xs-12 text-left">
                                   <b> Branch Name :</b> <span class="text-red">*</span>
                                    <asp:TextBox ID="ddl_unitcode" runat="server" class="form-control text_box"></asp:TextBox>
                                    <%-- <asp:DropDownList ID="ddl_unitcode" class="form-control pr_state " Enabled="false" OnSelectedIndexChanged="designation_unitwise" AutoPostBack="true" Width="100%" runat="server">
                            </asp:DropDownList>--%>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Designation:</b> <span class="text-red">*</span>
                                    <asp:TextBox ID="ddl_grade" runat="server" class="form-control text_box"></asp:TextBox>
                                    <%-- <asp:DropDownList ID="ddl_grade" class="js-example-basic-single" Width="100%" runat="server" AutoPostBack="True" DataTextField="GRADE_DESC" DataValueField="GRADE_CODE" meta:resourcekey="ddl_gradeResource1" O>
                                        <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                                    </asp:DropDownList>--%>
                                </div>
                                <div class="col-sm-2 col-xs-12 text-left">
                                    <b>Employee Code :</b>
                            <asp:TextBox ID="txt_eecode" runat="server" class="form-control text_box"
                                onkeypress="return Alphabet_Number(event,this);" MaxLength="20" meta:resourcekey="txt_eecodeResource1" ReadOnly="true" disabled></asp:TextBox>

                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_eecode" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1" Visible="False"></asp:RequiredFieldValidator>--%>
                                </div>


                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12 text-left">
                                   <b> IHMS Code :</b>
                            <asp:TextBox ID="txt_ihmscode" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>

                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_eecode" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1" Visible="False"></asp:RequiredFieldValidator>--%>
                                </div>




                                <div class="col-sm-2 col-xs-12 text-left">
                                   <b> Employee Name :</b>    <span class="text-red">*</span>
                                    <asp:TextBox ID="txt_eename" runat="server" CausesValidation="True" class="form-control text_box"
                                        MaxLength="50" meta:resourcekey="txt_eenameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>

                                </div>
                                <div class="col-sm-2 col-xs-12 text-left">
                                   <b> Father / Husband Name :</b>    <span class="text-red">*</span>

                                    <asp:TextBox ID="txt_eefatharname" runat="server" CausesValidation="True" class="form-control text_box"
                                        MaxLength="50" meta:resourceKey="txt_eefatharnameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_eefatharname" ErrorMessage="RequiredFieldValidator" meta:resourceKey="RequiredFieldValidator4Resource1" Text="*"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-2 col-xs-12 text-left">
                                   <b> Relation :</b>
                             <asp:TextBox ID="ddl_relation" runat="server" CausesValidation="True" class="form-control text_box"></asp:TextBox>

                                    <%--<asp:DropDownList ID="ddl_relation" runat="server" class="form-control" onchange="return father_relation();">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Father">Father</asp:ListItem>
                                <asp:ListItem Value="Husband">Husband</asp:ListItem>
                            </asp:DropDownList>--%>
                                </div>
                                <div class="col-sm-2 col-xs-12 text-left">
                                   <b> Date of Birth :</b>    <span class="text-red">*</span>

                                    <asp:TextBox ID="txt_birthdate" runat="server" class="form-control date-picker"
                                        meta:resourceKey="txt_birthdateResource1"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12 text-left">
                                    <b>Gender :</b>
                           <asp:TextBox ID="ddl_gender" runat="server" CausesValidation="True" class="form-control text_box"></asp:TextBox>
                                    <%-- <asp:DropDownList ID="ddl_gender" runat="server" class="form-control" meta:resourceKey="ddl_genderResource1">
                                <asp:ListItem meta:resourceKey="ListItemResource3" Text="Male" Value="M"></asp:ListItem>
                                <asp:ListItem meta:resourceKey="ListItemResource4" Text="Female" Value="F"></asp:ListItem>
                                <%--<asp:ListItem meta:resourceKey="ListItemResource5" Text="Transgender" Value="T"></asp:ListItem>--%>
                                    <%--  </asp:DropDownList>--%>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-sm-2 col-xs-12">
                                   <b> Reporting To: </b>   
                              <asp:TextBox ID="ddl_reporting_to" runat="server" CausesValidation="True" class="form-control text_box"></asp:TextBox>
                                    <%-- <asp:DropDownList ID="ddl_reporting_to" runat="server" class="form-control js-example-basic-single" DataTextField="emp_name"
                                        DataValueField="emp_code" meta:resourcekey="ddl_deptResource1" >
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>--%>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Role : </b>   <span class="text-red">*</span>
                                    <asp:TextBox ID="DropDownList1" runat="server" CausesValidation="True" class="form-control text_box"></asp:TextBox>
                                    <%-- <asp:DropDownList ID="DropDownList1" class="form-control" runat="server"></asp:DropDownList>--%>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Working Hours: </b><span class="text-red">*</span>
                                    <asp:TextBox ID="txt_attendanceid" runat="server" CausesValidation="True" class="form-control text_box"></asp:TextBox>
                                    <%-- <asp:DropDownList ID="txt_attendanceid" runat="server" OnSelectedIndexChanged="txt_workinghours_count" AutoPostBack="true"
                                class="form-control text_box" MaxLength="2" meta:resourceKey="txt_attendacneidResource1">
                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                            </asp:DropDownList>--%>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                  <b>  IN Time : </b>
                             <asp:TextBox ID="ddl_intime" runat="server" CausesValidation="True" class="form-control text_box"></asp:TextBox>
                                    <%--   <asp:DropDownList ID="ddl_intime" runat="server" class="js-example-basic-single" Width="100%" meta:resourceKey="ddl_intimeResource1">
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
                                    </asp:DropDownList>--%>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Email-Id:</b>    

                                    <asp:TextBox ID="txt_email" runat="server" class="form-control text_box" onkeypress="return email(event)" MaxLength="70" meta:resourceKey="txt_email"></asp:TextBox>
                                </div>

                                <div class="col-sm-2 col-xs-12">
                                   <b> Police Verification Start Date :</b>
                                        <asp:TextBox ID="txt_start_date" runat="server" class="form-control date-picker11"></asp:TextBox>
                                </div>

                            </div>
                            <br />
                            <div class="row">

                                <div class="col-sm-2 col-xs-12">
                                   <b> Police Verification  End Date :</b>
                                        <asp:TextBox ID="txt_end_date" runat="server" class="form-control date-picker12"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Rent Agreement Start Date :</b>
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
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_employee_type" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </div>
                </asp:Panel>
                <br />
                <br />
                <asp:Panel ID="Panel2" runat="server">
                    <ul class="nav nav-tabs">
                        <li id="tabactive1" class="active"><a data-toggle="tab" href="#home" runat="server"><b>Contact Details</b></a></li>
                        <li id="tabactive2"><a data-toggle="tab" href="#menu1" runat="server"><b>Employee Details</b></a></li>
                        <li id="tabactive10"><a data-toggle="tab" href="#menu9" runat="server"><b>Family Details</b></a></li>
                        <li id="tabactive3"><a data-toggle="tab" href="#menu6" runat="server"><b>Bank Account Details</b></a></li>
                        <li id="tabactive4"><a data-toggle="tab" href="#menu7" runat="server"><b>Personal Details</b></a></li>
                        <li id="tabactive5"><a data-toggle="tab" href="#menu2" runat="server"><b>Qualification Details</b></a></li>
                        <%--<li id="tabactive6"><a data-toggle="tab" href="#menu3" runat="server">Leave Details</a></li>--%>
                        <li id="tabactive7"><a data-toggle="tab" href="#menu4" id="rating" runat="server"><b>Rating Details</b></a></li>

                        <li id="tabactive8"><a data-toggle="tab" href="#menu5" runat="server"><b>Documents</b></a></li>

                        <li id="tabactive11"><a data-toggle="tab" href="#menu11" runat="server"><b>KRA</b></a></li>
                        <li id="tabactive9"><a id="A2" data-toggle="tab" style="display: none" href="#menu8" runat="server"><b>Loan</b></a></li>
                        <li id="tabactive12"><a data-toggle="tab" href="#menu12"><b>Uniform Details</b></a></li>

                    </ul>

                    <div class="tab-content">
                        <div id="home" class="tab-pane fade in active" style="background: #f3f1fe; padding:20px 20px 20px 20px; border-radius:10px">
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
                                                    <asp:TextBox ID="ddl_state" runat="server" class="form-control text_box"></asp:TextBox>
                                                    <%--<asp:DropDownList ID="ddl_state" runat="server" class="pr_state js-example-basic-single" Width="100%" OnSelectedIndexChanged="get_city_list" AutoPostBack="true"></asp:DropDownList>--%>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12">
                                                    <b> Present City : </b>   <span class="text-red">*</span>
                                                </div>


                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txt_presentcity" runat="server" class="form-control text_box"></asp:TextBox>
                                                    <%--  <asp:DropDownList ID="txt_presentcity" runat="server" class="pr_state js-example-basic-single" Width="100%"></asp:DropDownList>--%>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12">
                                           <b>  Pin code :</b>
                                        </div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox
                                                ID="txt_presentpincode" ReadOnly="true" runat="server" class="form-control pr_pin text_box" onchange="return pincode_validation()"
                                                onkeypress="return isNumber(event)" MaxLength="6" meta:resourcekey="txt_presentpincodeResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12">
                                            <b> Mobile Number1 :</b>    <span class="text-red">*</span>
                                        </div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txt_mobilenumber" runat="server" ReadOnly="true" class="form-control pr_mbno text_box"
                                                onkeypress="return isNumber(event)" MaxLength="10"
                                                meta:resourcekey="txt_mobilenumberResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12">
                                            <b> Mobile Number2 :</b>  
                                        </div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="pre_mobileno_1" ReadOnly="true" runat="server" class="form-control pr_mbno text_box"
                                                onkeypress="return isNumber(event)" MaxLength="10"
                                                meta:resourcekey="txt_mobilenumberResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12">
                                            <b> Mobile Number3 :</b>   
                                        </div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="pre_mobileno_2" ReadOnly="true" runat="server" class="form-control pr_mbno text_box"
                                                onkeypress="return isNumber(event)" MaxLength="10"
                                                meta:resourcekey="txt_mobilenumberResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-6 col-xs-12">
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12">
                                            <b> Permanant Address :</b>
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
                                                   <b>  State :</b>
                                                </div>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="ddl_permstate" runat="server" class="form-control prnt_add text_box" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                                    <%--<asp:DropDownList ID="ddl_permstate" runat="server" class="pr_state js-example-basic-single" Width="100%"
                                                        OnSelectedIndexChanged="get_city_list_shipping" AutoPostBack="true">
                                                    </asp:DropDownList>--%>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-4 col-xs-12">
                                                   <b>  Permanant City :</b>
                                                </div>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txt_permanantcity" class="form-control prnt_add text_box" runat="server" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                                    <%--<asp:DropDownList ID="txt_permanantcity" runat="server" class="pr_state js-example-basic-single" Width="100%">
                                                    </asp:DropDownList>--%>
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
                                            <asp:TextBox ID="txtref2mob" runat="server" ReadOnly="true" class="form-control prnt_mbno text_box"
                                                onkeypress="return isNumber(event)"
                                                MaxLength="10" meta:resourcekey="txtref2mobResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12">
                                            <b> Mobile Number2 :  </b>  
                                        </div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txt_premanent_mob1" ReadOnly="true" runat="server" class="form-control pr_mbno text_box"
                                                onkeypress="return isNumber(event)" MaxLength="10"
                                                meta:resourcekey="txt_mobilenumberResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12">
                                            <b> Mobile Number3 :</b>    
                                        </div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txt_premanent_mob2" ReadOnly="true" runat="server" class="form-control pr_mbno text_box"
                                                onkeypress="return isNumber(event)" MaxLength="10"
                                                meta:resourcekey="txt_mobilenumberResource1"></asp:TextBox>
                                        </div>
                                    </div>


                                </div>
                            </div>
                            <br />
                            <br />
                            <br />


                            <asp:Panel ID="newcontactpanel" runat="server" CssClass="panel panel-default">
                                <div class="panel-heading">
                                    <h5><b>Reference Details</b></h5>
                                </div>

                                <div class="row" style=" margin-top:10px; margin-bottom:10px; padding-top: 10px; padding-bottom: 10px;">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-1s2">
                                                <b> Contact Name 1 : </b>   
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
                                                 <b>Mobile No 1 : </b>   
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
                                                <b> Email Id 1 :</b>
                                            </div>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txt_emailid1" runat="server" class="form-control  prnt_c2 text_box"
                                                    MaxLength="200" meta:resourcekey="txtref1mobResource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br></br>
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                               <b> Address 1 :</b>
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
                                               <b> Contact Name 2 : </b>  
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
                                                <b>Email Id 2 :</b>
                                            </div>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txt_emailid2" runat="server" class="form-control  prnt_c2 text_box"
                                                    MaxLength="50" meta:resourcekey="txtref1mobResource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br></br>
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                               <b> Address 2 :</b>
                                            </div>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txt_address2" runat="server" class="form-control  prnt_c2 text_box"
                                                    TextMode="MultiLine" Rows="6"
                                                    MaxLength="200" meta:resourcekey="txtref1mobResource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>





                        </div>
                        <div id="menu1" class="tab-pane fade">
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                   <b> Confirmation Date : </b> </b>  
                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_confirmationdate" MaxLength="100" runat="server" Width="100%" class="form-control confirm_date"
                                        meta:resourcekey="txt_confirmationdateResource1"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <b>Joining Date :  </b>  <span class="text-red">*</span>
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
                                            <b>Working State : </b>   <span class="text-red">*</span>
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            <%--<asp:DropDownList ID="ddl_location" CssClass="pr_state js-example-basic-single" Width="100%" 
                                            runat="server" OnSelectedIndexChanged="ddl_location_SelectedIndexChanged" AutoPostBack="true" 
                                            meta:resourcekey="ddl_locationResource1"></asp:DropDownList>--%>

                                            <asp:TextBox ID="ddl_location" runat="server" class="form-control text_box"
                                                MaxLength="50" meta:resourcekey="txtref1mobResource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>


                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                          <b>  Working City : </b>   <span class="text-red">*</span>
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            <asp:TextBox ID="ddl_location_city" runat="server" class="form-control text_box"
                                                MaxLength="50" meta:resourcekey="txtref1mobResource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
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
                                    <asp:TextBox ID="txt_policestationname" runat="server" class="form-control text_box" ReadOnly="true"
                                        meta:resourcekey="txt_pannoResource1" MaxLength="225" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Aadhar Card / Enrollment No :  </b>  <span class="text-red">*</span>
                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_ptaxnumber" onkeypress="return isNumber(event,this);" runat="server" class="form-control text_box" MaxLength="12"
                                        meta:resourcekey="txt_ptaxnumberResource1"></asp:TextBox>
                                </div>

                            </div>
                            <br />






                            <div class="row">
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
                        <div id="menu6" class="tab-pane fade">
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                   <b> Bank Account Holder Name :</b> <span class="text-red">*</span>

                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_bankholder" runat="server" class="form-control text_box" MaxLength="50" meta:resourcekey="txt_pfbanknameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                  <b>  Bank Name : </b>   <span class="text-red">*</span>
                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_pfbankname" runat="server" class="form-control text_box" MaxLength="50" meta:resourcekey="txt_pfbanknameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                </div>

                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12"><b>Branch Location Name :</b><span class="text-red"> *</span></div>
                                <div class="col-sm-3 col-xs-12">

                                    <asp:TextBox ID="ddl_bankcode" runat="server" class="form-control text_box" MaxLength="200" meta:resourcekey="ddl_bankcodeResource1" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                </div>

                                <div class="col-sm-2 col-xs-12">
                                   <b> Bank A/C Number :</b>
                                        <span class="text-red">*</span>
                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_employeeaccountnumber" runat="server" class="form-control text_box"
                                        meta:resourceKey="txt_employeeaccountnumberResource1" MaxLength="20" onkeypress="return isNumber(event);"></asp:TextBox>
                                </div>
                            </div>

                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                  <b>  IFSC Code :</b>
                                        <span class="text-red">*</span>
                                </div>

                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_pfifsccode" runat="server" class="form-control text_box" MaxLength="11"
                                        meta:resourceKey="txt_pfifsccodeResource1" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                  <b>  NFD/IFD : </b>   <span class="text-red">*</span>

                                </div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="ddl_infitcode" runat="server" class="form-control text_box" MaxLength="11"></asp:TextBox>
                                    <%--<asp:DropDownList ID="ddl_infitcode" runat="server" class="form-control">
                                        <asp:ListItem Value="Select" Text="Select Transfer">Select Transfer</asp:ListItem>
                                        <asp:ListItem Value="N" Text="NEFT TRANSFER">NEFT TRANSFER</asp:ListItem>
                                        <asp:ListItem Value="I" Text="INTERNAL TRANSFER">INTERNAL TRANSFER</asp:ListItem>
                                    </asp:DropDownList>--%>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12"><b>Nominee Name :</b></div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_pfnomineename" runat="server" class="form-control text_box"
                                        meta:resourceKey="txt_pfnomineenameResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                </div>

                                <div class="col-sm-2 col-xs-12"><b>Nominee Relation :</b></div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_pfnomineerelation" runat="server" class="form-control text_box" MaxLength="20" meta:resourceKey="txt_pfnomineerelationResource1" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12"><b>Nominee Birth Date :</b></div>
                                <div class="col-sm-3 col-xs-12">
                                    <asp:TextBox ID="txt_pfbdate" runat="server" class="form-control date-picker" meta:resourceKey="txt_pfbdateResource1" Width="263"></asp:TextBox>

                                </div>
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
                        <div id="menu7" class="tab-pane fade">
                            <br />
                            <br />
                            <div class="row">
                                <br />
                                <div class="col-sm-6 col-xs-12">
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                           <b> Nationality :</b>    <span class="text-red">*</span>
                                        </div>
                                        <div class="col-sm-6 col-xs-12" onkeypress="return lettersOnly(event,this);">
                                            <asp:TextBox ID="txt_Nationality" runat="server" CssClass="form-control "
                                                class="text_box" onkeypress="return AllowAlphabet(event)" MaxLength="20"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                           <b> Identitymark :</b>
                                        
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
                                          <b>  Driving Licence No :</b>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_Driving_License_No" runat="server" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_Number(event,this);" MaxLength="30"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row" style="display: none">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                          <b>  Mise :</b>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_Mise" runat="server" CssClass="form-control" class="text_box" onkeypress="return AllowAlphabet_Number(event,this);" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                          <b>  Weight (in Kg) :</b>
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
                                            <asp:TextBox ID="txt_maritalstaus" runat="server" class="form-control text_box" meta:resourcekey="txt_maritalstausResource1" onkeypress="return AllowAlphabet(event)" MaxLength="20"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="row">

                                        <div class="col-sm-4 col-xs-12 text-right"><b>Religion :</b></div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="ddl_religion" runat="server" class="form-control text_box"></asp:TextBox>
                                            <%-- <asp:DropDownList

                                                ID="ddl_religion" runat="server" class="form-control" Width="100%"
                                                meta:resourcekey="ddl_religionResource1">
                                                <asp:ListItem meta:resourcekey="ListItemResource16" Value="Select">Select</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource16" Value="General">General</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource17" Value="OBC">OBC</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource18" Value="SC">SC</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource19" Value="ST">ST</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource20" Value="NT">NT</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource21" Value="Other">Other</asp:ListItem>
                                            </asp:DropDownList>--%>
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
                                            <b>Area Of Expertise :</b>
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
                                           <b> Details Of Handicap  :</b>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:TextBox ID="txt_Details_Of_Handicap" runat="server" onkeypress="return AllowAlphabet_Number(event,this);" CssClass="form-control" class="text_box" MaxLength="20"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 text-right">
                                           <b> Height (in Feets) :</b>
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
                        <div id="menu2" class="tab-pane fade">
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-sm-6 col-xs-12" style="border-right: 1px solid #808080;">
                                    <div class="row text-center">
                                        <h3><b>Qualification</b></h3>
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
                                           <b> Qualification3 :</b>
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
                                        <h3><b>Skill</b></h3>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-8 col-xs-12">
                                            <b>Key Skill1 : </b>
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
                                           <b> Key Skill3 :</b>
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
                        <div id="menu4" class="tab-pane fade">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <%--<ul class="nav nav-tabs">
                                        <li id="itemtab5" ><a data-toggle="tab" href="#item5">Allowance</a></li>
                                    </ul>--%>
                                    <div class="tab-content">
                                        <%--<div id="item5" class="tab-pane fade in active">--%>
                                        <br />
                                        <br />
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
                                               <b> Fine :</b>
                                   
                                                     <asp:TextBox ID="txt_fine" runat="server" Text="0" class="form-control text_box  maskedExt" maskedFormat="10,2" onkeypress="return isNumber_dot(event)"
                                                         meta:resourcekey="txtdhead6Resource1" MaxLength="7"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                               <b> Advance :</b>
                                                <asp:TextBox ID="txt_advance_payment" runat="server" class="form-control" onkeypress="return isNumber(event)" meta:resourceKey="txt_advance_paymentResource1"></asp:TextBox>
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
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                               <b> Photo(Passport Size) :</b><span class="text-red"> *</span>
                                                <br />
                                                <asp:Label ID="photo" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="photo_upload" Enabled="false" runat="server" meta:resourcekey="photo_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image4" runat="server" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/placeholder.png" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                               <b> Pan Card :</b> 
                                            <br />
                                                <asp:Label ID="l_adhar_pan_upload" runat="server" Text="Employee PAN Card Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="adhar_pan_upload" Enabled="false" runat="server" meta:resourcekey="adhar_pan_uploadResource1" />
                                                <br />

                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image1" runat="server" meta:resourcekey="Image1Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/pan.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                               <b> Aadhar Card :</b><span class="text-red"> *</span>
                                                <br />
                                                <asp:Label ID="l_bank_upload" runat="server" Text="Employee AADHAR Card Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="bank_upload" Enabled="false" runat="server" meta:resourcekey="bank_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image2" runat="server" meta:resourcekey="Image2Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/passbook.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                               <b> Biodata :</b>
                                            <br />
                                                <asp:Label ID="l_biodata_upload" runat="server" Text="Employee Biodata Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="biodata_upload" Enabled="false" runat="server" meta:resourcekey="biodata_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image3" runat="server" meta:resourcekey="Image3Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/Biodata.png" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                               <b> Passport :</b>
                                            <br />
                                                <asp:Label ID="l_Passport" runat="server" Text="Employee Passport Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Passport_upload" Enabled="false" runat="server" meta:resourcekey="Passport_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image5" runat="server" meta:resourcekey="Image5Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/Passport.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                               <b> Driving Liscence :</b>
                                            <br />
                                                <asp:Label ID="l_Driving_Liscence_upload" runat="server" Text="Employee Driving Liscence Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Driving_Liscence_upload" Enabled="false" runat="server" meta:resourcekey="Driving_Liscence_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image6" runat="server" meta:resourcekey="Image6Resource1" onkeypress="return AllowAlphabet_Number(event,this);" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/Driving_liscence.jpg" />
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                               <b> 10th Marksheet :</b>
                                            <br />
                                                <asp:Label ID="l_Tenth_Marksheet_upload" runat="server" Text="Employee 10th Marksheet Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Tenth_Marksheet_upload" Enabled="false" runat="server" meta:resourcekey="Tenth_Marksheet_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image7" runat="server" meta:resourcekey="Image7Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/marksheet.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                              <b>  12th Marksheet :</b>
                                            <br />
                                                <asp:Label ID="l_Twelve_Marksheet_upload" runat="server" Text="Employee 12th Marksheet Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Twelve_Marksheet_upload" Enabled="false" runat="server" meta:resourcekey="Twelve_Marksheet_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image8" runat="server" meta:resourcekey="Image8Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/marksheet.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                               <b> Diploma Certificate :</b>
                                            <br />
                                                <asp:Label ID="l_Diploma_upload" runat="server" Text="Employee Diploma Certificate Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Diploma_upload" Enabled="false" runat="server" meta:resourcekey="Diploma_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image9" runat="server" meta:resourcekey="Image9Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                              <b>  Degree Certificate :</b>
                                            <br />
                                                <asp:Label ID="l_Degree_upload" runat="server" Text="Employee Degree Certificate Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Degree_upload" Enabled="false" runat="server" meta:resourcekey="Degree_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image10" runat="server" meta:resourcekey="Image10Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                               <b> Post Graduation Certificate :</b>
                                            <br />

                                                <asp:Label ID="l_Post_Graduation_upload" runat="server" Text="Employee Post Graduation Certificate Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Post_Graduation_upload" Enabled="false" runat="server" meta:resourcekey="Post_Graduation_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image11" runat="server" meta:resourcekey="Image11Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                              <b>  Education Certificate :</b>
                                            <br />
                                                <asp:Label ID="l_Education_4_upload" runat="server" Text="Employee Graduation Certificate Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Education_4_upload" Enabled="false" runat="server" meta:resourcekey="Education_4_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image12" runat="server" meta:resourcekey="Image12Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>
                                        <br></br>
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                              <b>  Police Verification Document :</b><span class="text-red"> *</span>
                                                <br />
                                                <asp:Label ID="l_Police_document" runat="server" Text="Employee Police Verification Document  Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Police_document" Enabled="false" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image14" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                               <b> Form No 2 :</b>
                                            <br />
                                                <asp:Label ID="l_Formno_2" runat="server" Text="Employee  Form No 2  Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Formno_2" Enabled="false" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image15" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                <b>Form No111 :</b>
                                            <br />
                                                <asp:Label ID="l_Formno_11" runat="server" Text="Employee Form No111 Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Formno_11" Enabled="false" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image16" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                               <b> Proof of Present Address :</b> <span class="text-red">*</span>
                                                <br />
                                                <asp:Label ID="l_Address_Proof" runat="server" Text="Employee Proof of Present Address Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="Address_Proof" Enabled="false" runat="server" meta:resourcekey="photo_uploadResource1" />

                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image17" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/Biodata.png" />
                                            </div>
                                            <br />


                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                              <b>  NOC Form :</b>
                                            <br />
                                                <asp:Label ID="Label1" runat="server" Text="Employee Form No111 Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="noc_form" Enabled="false" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image18" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                               <b> Medical Document :</b>
                                            <br />
                                                <asp:Label ID="Label2" runat="server" Text="Employee Form No111 Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:FileUpload ID="medical_form" Enabled="false" runat="server" meta:resourcekey="Police_document_uploadResource1" />
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:ImageButton ID="Image19" runat="server" meta:resourcekey="Image13Resource1" OnClick="image_click" Width="100px" Height="100px" ImageUrl="~/Images/certificate.jpg" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row text-center">
                                        <asp:Button ID="btnUpload" runat="server" Enabled="false" CssClass="btn btn-primary" meta:resourcekey="btnUploadResource1" Text="Upload" />
                                    </div>
                                    <br />
                                    <div class="row text-center">
                                       <b> Note : Only JPG and PNG files will be uploaded.</b>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                        <div id="menu8" class="tab-pane fade" style="display: none">
                            <br />
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

                        <div id="menu12" class="tab-pane">
                            <br />
                            <asp:Panel ID="Panel11" runat="server" CssClass="panel panel-default">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <div class="container">
                                            <br />
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-3 col-xs-12">
                                                   <b> Select Designation :</b>
                                                    <asp:TextBox ID="select_designation" runat="server" class="form-control"></asp:TextBox>
                                                    <%-- <asp:DropDownList ID="select_designation" runat="server" class="form-control text-box" OnSelectedIndexChanged="select_designation_SelectedIndexedChanged" AutoPostBack="true">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Housekeeping</asp:ListItem>
                                <asp:ListItem Value="2">Security_Guard</asp:ListItem>
                                <asp:ListItem Value="3">Common</asp:ListItem>

                            </asp:DropDownList>--%>
                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                   <b> Select Type :</b>
                                                     <asp:TextBox ID="ddl_product_type" runat="server" class="form-control"></asp:TextBox>
                                                    <%-- <asp:DropDownList ID="ddl_product_type" runat="server" class="form-control text-box" OnSelectedIndexChanged="ddl_product_type_SelectedIndexedChanged" AutoPostBack="true">
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



                            </asp:DropDownList>--%>
                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                    <span class="text-left"><b>Number Of Set :</b></span>
                                                    <asp:TextBox ID="ddl_uniformset" runat="server" class="form-control"></asp:TextBox>
                                                    <%-- <asp:DropDownList ID="ddl_uniformset" runat="server" class="form-control text-box">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">0</asp:ListItem>
                                                        <asp:ListItem Value="2">1</asp:ListItem>
                                                        <asp:ListItem Value="3">2</asp:ListItem>

                                                    </asp:DropDownList>--%>
                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                   <b> Size:</b>
                                 <asp:TextBox ID="uniform_size" runat="server" class="form-control text-box" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />



                                            <div class="row">


                                                <div class="col-sm-3 col-xs-12">
                                                   <b> Issuing Date:</b>
                                 <asp:TextBox ID="uniform_issue_date" runat="server" class="form-control date-picker1"
                                     meta:resourceKey="txt_birthdateResource1"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 col-xs-12">
                                                  <b>  Expiry Date:</b>
                                 <asp:TextBox ID="uniform_expiry_date" runat="server" class="form-control date-picker2"
                                     meta:resourceKey="txt_birthdateResource1"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    <br />
                                                    <asp:LinkButton ID="lnk_zone_add" runat="server" Visible="false" OnClick="lnk_zone_add_Click" CausesValidation="false" OnClientClick="return req_valid();">
                                      <img alt="Add Item" src="Images/add_icon.png" />
                                                    </asp:LinkButton>
                                                </div>

                                            </div>
                                            <br />
                                            <div class="row">
                                                <asp:Panel ID="Panel1" runat="server" CssClass="grid-view" ScrollBars="Auto">

                                                    <asp:GridView ID="gv_product_details" class="table" runat="server" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                        OnRowDataBound="gv_product_details_RowDataBound"
                                                        AutoGenerateColumns="False">

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
                                                            <asp:BoundField DataField="No_of_set" HeaderText="Number Of Set"
                                                                SortExpression="No_of_set" />
                                                            <asp:BoundField DataField="size" HeaderText="Size"
                                                                SortExpression="size" />
                                                            <asp:BoundField DataField="start_date" HeaderText="Issu Date"
                                                                SortExpression="start_date" />
                                                            <asp:BoundField DataField="end_date" HeaderText="Expiry Date"
                                                                SortExpression="end_date" />

                                                        </Columns>
                                                    </asp:GridView>

                                                </asp:Panel>

                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                        </div>






                        <div id="menu9" class="tab-pane fade">
                            <br />
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
                                    <th align="center"><b>Name</b></th>
                                    <th align="center"><b>Relation</b></th>
                                    <th align="center"><b>DOB</b></th>
                                    <th align="center"><b>PAN No</b></th>
                                    <th align="center"><b>Aadhar No</b></th>
                                    <th align="center"><b>Mobile No</b></th>

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
                        <div id="menu11" class="tab-pane fade">
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12 text-right"><b>Key Responsibility Area :</b></div>
                                <div class="col-lg-6 col-md-6 col-sm- col-xs-12">
                                    <asp:TextBox ID="txt_kra" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="6" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
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
                <br />

            </div>
        </asp:Panel>
    </div>
</asp:Content>

