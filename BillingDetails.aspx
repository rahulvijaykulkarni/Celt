<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Title="Billing Details" CodeFile="BillingDetails.aspx.cs" Inherits="BillingDeails" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Billing & Salary Master</title>
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
    <%-- <script src="datatable/pdfmake.min.js"></script>--%>
    <style>
        .text-red {
            color: red;
        }

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
            max-height: 500px;
            height: auto;
            overflow-y: auto;
            overflow-x: hidden;
        }

        .grid {
            max-height: 250px;
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
    </style>

    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }

        function pageLoad() {
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
            var table = $('#<%=grd_policy.ClientID%>').DataTable({
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
               .appendTo('#<%=grd_policy.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';


            // $('.ddl_cotract_type').attr("disabled", true);

            $('#<%=ddl_unit_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_clientwisestate.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_unitcode.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_unitcode_without.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_designation.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_add_machine.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            uniform_dropdown();
            deep_clean_applicable();
            material_policy();
            deep_clean_type();
            ddl_Conveyance_Applicable();
            change_drop();
            //leave_taxable();
            //bonus_taxable();
            // gratuaty_taxable();
            b_taxable();
            g_taxable();
            l_taxable();
            Rental_cost();
            consume_applicable();
            linear_applicable();
            bin_applicable();
            machine_applicable();
            tackle_applicable();
            common_bonus_applicable();
            common_leave_applicable();
            contract_type();
            machine_rental_applicable();
            Machine_rent_in();
            Machine_rent_amount();
            deep_h_charge();
            hidde_in();
            policy_check();
            change_dropdown_r_m();
            change_dropdown_administrative();

           
        }


        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
            function EndRequestHandler(sender, args) {

                $('body').on('keyup', '.maskedExt', function () {
                    var num = $(this).attr("maskedFormat").toString().split(',');
                    var regex = new RegExp("^\\d{0," + num[0] + "}(\\.\\d{0," + num[1] + "})?$");
                    if (!regex.test(this.value)) {
                        this.value = this.value.substring(0, this.value.length - 1);
                    }
                });

                var table = $('#<%=grd_material_detail.ClientID%>').DataTable(
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
                   .appendTo('#<%=grd_material_detail.ClientID%>_wrapper .col-sm-6:eq(0)');


                change_txt_box();
                change_txt_box1();
                // billing_start_date();
                ot_allowed();
                add_number1();
                $(<%=txt_end_date_common.ClientID%>).attr("readonly", "true");
            }
        });



        function Number10(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if ((charCode >= 48 || charCode <= 58)) {
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

        function common_bonus_applicable() {
            var ddl_bonus_app = document.getElementById('<%=ddl_bonus_app.ClientID %>');
            var Selected_ddl_bonus_app = ddl_bonus_app.options[ddl_bonus_app.selectedIndex].text;

            var ddl_bonus_policy_aap_billing = document.getElementById('<%=ddl_bonus_policy_aap_billing.ClientID %>');
            var Selected_ddl_bonus_policy_aap_billing = ddl_bonus_policy_aap_billing.options[ddl_bonus_policy_aap_billing.selectedIndex].text;

            var ddl_bonus_taxable_billing = document.getElementById('<%=ddl_bonus_taxable_billing.ClientID %>');
             var Selected_ddl_bonus_taxable_billing = ddl_bonus_taxable_billing.options[ddl_bonus_taxable_billing.selectedIndex].text;

             var txt_bonus_amount_billing = document.getElementById('<%=txt_bonus_amount_billing.ClientID %>');
            var txt_bill_bonus_percent = document.getElementById('<%=txt_bill_bonus_percent.ClientID %>');

            var ddl_bonus_policy_aap_salary = document.getElementById('<%=ddl_bonus_policy_aap_salary.ClientID %>');
            var Selected_ddl_bonus_policy_aap_salary = ddl_bonus_policy_aap_salary.options[ddl_bonus_policy_aap_salary.selectedIndex].text;

            var ddl_bonus_taxable_salary = document.getElementById('<%=ddl_bonus_taxable_salary.ClientID %>');
            var Selected_ddl_bonus_taxable_salary = ddl_bonus_taxable_salary.options[ddl_bonus_taxable_salary.selectedIndex].text;

            var txt_bonus_amount_salary = document.getElementById('<%=txt_bonus_amount_salary.ClientID %>');
             var txt_sal_bonus = document.getElementById('<%=txt_sal_bonus.ClientID %>');


            if (Selected_ddl_bonus_app == "No") {
                //ddl_bonus_policy_aap_billing.options[0].selected = true;
                //ddl_bonus_taxable_billing.options[0].selected = true;
                //ddl_bonus_policy_aap_salary.options[0].selected = true;
                //ddl_bonus_taxable_salary.options[0].selected = true;
                ddl_bonus_policy_aap_billing.disabled = true;
                ddl_bonus_taxable_billing.disabled = true;
                ddl_bonus_policy_aap_salary.disabled = true;
                ddl_bonus_taxable_salary.disabled = true;
                txt_bonus_amount_billing.disabled = true;
                txt_bill_bonus_percent.disabled = true;
                txt_bonus_amount_salary.disabled = true;
                txt_sal_bonus.disabled = true;

            }
            else if (Selected_ddl_bonus_app == "Yes") {
                //ddl_bonus_policy_aap_billing.options[1].selected = true;
                //ddl_bonus_taxable_billing.options[1].selected = true;
                //ddl_bonus_policy_aap_salary.options[1].selected = true;
                //ddl_bonus_taxable_salary.options[1].selected = true;
                ddl_bonus_policy_aap_billing.disabled = false;
                ddl_bonus_taxable_billing.disabled = false;
                ddl_bonus_policy_aap_salary.disabled = false;
                ddl_bonus_taxable_salary.disabled = false;
                txt_bonus_amount_billing.disabled = false;
                txt_bill_bonus_percent.disabled = false;
                txt_bonus_amount_salary.disabled = false;
                txt_sal_bonus.disabled = false;

            }
            change_txt_box();
            change_txt_box1();
            return true;
        }
        function common_leave_applicable() {
            var ddl_leave_app = document.getElementById('<%=ddl_leave_app.ClientID %>');
            var Selected_ddl_leave_app = ddl_leave_app.options[ddl_leave_app.selectedIndex].text;

            var txt_leave_days_percent = document.getElementById('<%=txt_leave_days_percent.ClientID %>');
             var txt_leave_days = document.getElementById('<%=txt_leave_days.ClientID %>');
            var ddl_leave_taxable_billing = document.getElementById('<%=ddl_leave_taxable_billing.ClientID %>');
            var Selected_ddl_leave_taxable_billing = ddl_leave_taxable_billing.options[ddl_leave_taxable_billing.selectedIndex].text;

            var ddl_leave_taxable_salary = document.getElementById('<%=ddl_leave_taxable_salary.ClientID %>');
             var Selected_ddl_leave_taxable_salary = ddl_leave_taxable_salary.options[ddl_leave_taxable_salary.selectedIndex].text;
             var txt_leave_days_salary = document.getElementById('<%=txt_leave_days_salary.ClientID %>');
            var txt_leave_days_percent_salary = document.getElementById('<%=txt_leave_days_percent_salary.ClientID %>');

            if (Selected_ddl_leave_app == "No") {
                //ddl_leave_taxable_billing.options[0].selected = true;
                ddl_leave_taxable_billing.disabled = true;
                txt_leave_days.disabled = true;
                txt_leave_days_percent.disabled = true;

                //ddl_leave_taxable_salary.options[0].selected = true;
                ddl_leave_taxable_salary.disabled = true;
                txt_leave_days_salary.disabled = true;
                txt_leave_days_percent_salary.disabled = true;
            }
            else if (Selected_ddl_leave_app == "Yes") {
                //ddl_leave_taxable_billing.options[1].selected = true;
                ddl_leave_taxable_billing.disabled = false;
                txt_leave_days.disabled = false;
                txt_leave_days_percent.disabled = false;

                //ddl_leave_taxable_salary.options[1].selected = true;
                ddl_leave_taxable_salary.disabled = false;
                txt_leave_days_salary.disabled = false;
                txt_leave_days_percent_salary.disabled = false;
            }


        }

		 function Req_validation1() {
            var txt_clientwisestate = document.getElementById('<%=ddl_clientwisestate.ClientID %>');
            var Selected_clientwiswstate = txt_clientwisestate.options[txt_clientwisestate.selectedIndex].text;
            var txt_client = document.getElementById('<%=ddl_unit_client.ClientID %>');
            var Selected_client = txt_client.options[txt_client.selectedIndex].text;
            var txt_policy = document.getElementById('<%=txt_policy_name1.ClientID %>');
           
            var ddl_policy = document.getElementById('<%=ddl_policy.ClientID %>');
            var Selected_ddl_policy = ddl_policy.options[ddl_policy.selectedIndex].text;

             
            if (Selected_client == "Select") {
                alert("Please Select Client Name !!!");
                txt_client.focus();
                return false;
            }

            // Client Name
            if (Selected_clientwiswstate == "Select") {
                alert("Please Select State !!!");
                txt_clientwisestate.focus();
                return false;
            }

            //Unit Name

            if (Selected_ddl_policy == "Branchwise Policy") {
                var values_unit = "";
                var listBox_1 = document.getElementById("<%= ddl_unitcode.ClientID%>");
                for (var i = 0; i < listBox_1.options.length; i++) {
                    if (listBox_1.options[i].selected) {
                        values_unit = 1;
                    }
                }
                var listBox_2 = document.getElementById("<%= ddl_unitcode_without.ClientID%>");
                 for (var i = 0; i < listBox_2.options.length; i++) {
                     if (listBox_2.options[i].selected) {
                         values_unit = 1;
                     }
                 }
                 if (values_unit == "") {
                     alert("Please Select atleast One Branch Name !!!");
                     listBox_1.focus();
                     return false;
                 }
            }

            var values = "";
            var listBox = document.getElementById("<%= ddl_designation.ClientID%>");
              for (var i = 0; i < listBox.options.length; i++) {
                  if (listBox.options[i].selected) {
                      values = 1;
                  }
              }

            if (values == "") {
                alert("Please Select Atleast One Designation.");
                listBox.focus();
                return false;

            }

        }


        function Req_validation() {
            var txt_clientwisestate = document.getElementById('<%=ddl_clientwisestate.ClientID %>');
             var Selected_clientwiswstate = txt_clientwisestate.options[txt_clientwisestate.selectedIndex].text;
             var txt_client = document.getElementById('<%=ddl_unit_client.ClientID %>');
            var Selected_client = txt_client.options[txt_client.selectedIndex].text;
            var txt_policy = document.getElementById('<%=txt_policy_name1.ClientID %>');
            var start_date = document.getElementById('<%=txt_start_date.ClientID %>');
             var end_date = document.getElementById('<%=txt_end_date.ClientID %>');
             if (Selected_client == "Select") {
                 alert("Please Select Client Name !!!");
                 txt_client.focus();
                 return false;
             }

             // Client Name
             if (Selected_clientwiswstate == "Select") {
                 alert("Please Select State !!!");
                 txt_clientwisestate.focus();
                 return false;
             }

            //Unit Name

            var ddl_policy = document.getElementById('<%=ddl_policy.ClientID %>');
            var Selected_ddl_policy = ddl_policy.options[ddl_policy.selectedIndex].text;

             
            var values_unit = "";
            var listBox_1 = document.getElementById("<%= ddl_unitcode.ClientID%>");
            for (var i = 0; i < listBox_1.options.length; i++) {
                if (listBox_1.options[i].selected) {
                    values_unit = 1;
                }
            }
            var listBox_2 = document.getElementById("<%= ddl_unitcode_without.ClientID%>");
            for (var i = 0; i < listBox_2.options.length; i++) {
                if (listBox_2.options[i].selected) {
                    values_unit = 1;
                }
            }

            var ddl_policy = document.getElementById('<%=ddl_policy.ClientID %>');
            var Selected_ddl_policy = ddl_policy.options[ddl_policy.selectedIndex].text;

             

            if (Selected_ddl_policy == "Branchwise Policy") {
                if (values_unit == "") {
                    alert("Please Select atleast One Branch Name !!!");
                    listBox_1.focus();
                    return false;
                }
            }
            if (txt_policy.value == "") {
                alert("Please Enter Policy Name");
                txt_policy.focus();
                return false;
            }

            //if (start_date.value == "") {
            //    alert("Please Enter Start Date");
            //    start_date.focus();
            //    return false;
            //}

            //if (end_date.value == "") {
            //    alert("Please Enter End Date");
            //    end_date.focus();
            //    return false;
            //}

            var values = "";
            var listBox = document.getElementById("<%= ddl_designation.ClientID%>");
            for (var i = 0; i < listBox.options.length; i++) {
                if (listBox.options[i].selected) {
                    values = 1;
                }
            }

            if (values == "") {
                alert("Please Select Atleast One Designation.");
                listBox.focus();
                return false;

            }


            var txt_client1 = document.getElementById('<%=ddl_hours.ClientID %>');
            var Selected_client1 = txt_client1.options[txt_client1.selectedIndex].text;

            if (Selected_client1 == "") {
                alert("Please Select Duty Hours");
                txt_client1.focus();
                return false;
            }

            var txt_client2 = document.getElementById('<%=ddl_month_calc.ClientID %>');
            var Selected_client2 = txt_client2.options[txt_client2.selectedIndex].text;

            if (Selected_client2 == "Select") {
                alert("Please Select Month Calculation");
                txt_client2.focus();
                return false;
            }
            var bonus_taxable = document.getElementById('<%=ddl_bonus_taxable_billing.ClientID %>');
            var Selected_bonus_taxable = bonus_taxable.options[bonus_taxable.selectedIndex].text;
            var bonus_percent = document.getElementById('<%=txt_bill_bonus_percent.ClientID %>');
             //Salary





            if (Selected_bonus_taxable == "No") {
                if (bonus_percent.value == 0 || bonus_percent.value == "") {
                    alert("Billing Bonus Percent should be Greater than zero");
                    bonus_percent.focus();
                    return false;
                }

            }
            if (Selected_bonus_taxable == "Yes") {
                if (bonus_percent.value == 0 || bonus_percent.value == "") {
                    alert("Billing Bonus Percent should be Greater than zero");
                    bonus_percent.focus();
                    return false;
                }
            }
            var gratuaty_taxable = document.getElementById('<%=ddl_gratuity_taxable_billing.ClientID %>');
            var Selected_gratuaty_taxable = gratuaty_taxable.options[gratuaty_taxable.selectedIndex].text;
            var gratuaty_percent = document.getElementById('<%=txt_gratuity_percent_billing.ClientID %>');
             //For Salalry
            var gratuaty_taxable_sal = document.getElementById('<%=ddl_gratuity_taxable_salary.ClientID %>');



             if (Selected_gratuaty_taxable == "No") {
                 if (gratuaty_percent.value == 0 || gratuaty_percent.value == "") {
                     alert("Billing Gratuity Percent value should be greater than zero");
                     gratuaty_percent.focus();
                     return false;
                 }


             }
             if (Selected_gratuaty_taxable == "Yes") {
                 if (gratuaty_percent.value == 0 || gratuaty_percent.value == "") {
                     alert("Billing Gratuity Percent value should be greater than zero");
                     gratuaty_percent.focus();
                     return false;
                 }

             }
             var leave_taxable = document.getElementById('<%=ddl_leave_taxable_billing.ClientID %>');
            var Selected_leave_taxable = leave_taxable.options[leave_taxable.selectedIndex].text;
            var leave_percent = document.getElementById('<%=txt_leave_days_percent.ClientID %>');
            var leave_day = document.getElementById('<%=txt_leave_days.ClientID %>');




             if (Selected_leave_taxable == "No") {
                 if (leave_percent.value == 0 && leave_day.value == 0 || leave_percent.value == "" && leave_day.value == "") {
                     alert("Please add Billing leave percent/leave days");
                     leave_percent.focus();
                     return false;
                 }

             }
             if (Selected_leave_taxable == "Yes") {

                 if (leave_percent.value == 0 && leave_day.value == 0 || leave_percent.value == "" && leave_day.value == "") {
                     alert("Please add Billing leave percent/leave days");
                     leave_percent.focus();
                     return false;
                 }
             }
             var ddl_bonus_taxable_salary = document.getElementById('<%=ddl_bonus_taxable_salary.ClientID %>');
            var Selected_ddl_bonus_taxable_salary = ddl_bonus_taxable_salary.options[ddl_bonus_taxable_salary.selectedIndex].text;
            var txt_sal_bonus = document.getElementById('<%=txt_sal_bonus.ClientID %>');

            if (Selected_ddl_bonus_taxable_salary == "No") {
                if (txt_sal_bonus.value == 0 || txt_sal_bonus.value == "") {
                    alert("Salary Bonus percent should be greater than zero");
                    txt_sal_bonus.focus();
                    return false;
                }
            }
            if (Selected_ddl_bonus_taxable_salary == "Yes") {
                if (txt_sal_bonus.value == 0 || txt_sal_bonus.value == "") {
                    alert("Salary Bonus percent should be greater than zero");
                    txt_sal_bonus.focus();
                    return false;
                }
            }
            var ddl_gratuity_taxable_salary = document.getElementById('<%=ddl_gratuity_taxable_salary.ClientID %>');
            var Selected_ddl_gratuity_taxable_salary = ddl_gratuity_taxable_salary.options[ddl_gratuity_taxable_salary.selectedIndex].text;
            var txt_sal_graguity_per = document.getElementById('<%=txt_sal_graguity_per.ClientID %>');

            if (Selected_ddl_gratuity_taxable_salary == "No") {
                if (txt_sal_graguity_per.value == 0 || txt_sal_graguity_per.value == "") {
                    alert("Salary gratuity percent should be greater than zero");
                    txt_sal_graguity_per.focus();
                    return false;
                }
            }
            if (Selected_ddl_gratuity_taxable_salary == "Yes") {
                if (txt_sal_graguity_per.value == 0 || txt_sal_graguity_per.value == "") {
                    alert("Salary gratuity percent should be greater than zero");
                    txt_sal_graguity_per.focus();
                    return false;
                }
            }
            var ddl_leave_taxable_salary = document.getElementById('<%=ddl_leave_taxable_salary.ClientID %>');
            var Selected_ddl_leave_taxable_salary = ddl_leave_taxable_salary.options[ddl_leave_taxable_salary.selectedIndex].text;
            var txt_leave_days_percent_salary = document.getElementById('<%=txt_leave_days_percent_salary.ClientID %>');
            var txt_leave_days_salary = document.getElementById('<%=txt_leave_days_salary.ClientID %>');

             if (Selected_ddl_leave_taxable_salary == "No") {
                 if ((txt_leave_days_percent_salary.value == 0 && txt_leave_days_salary.value == 0) || (txt_leave_days_percent_salary.value == "" && txt_leave_days_salary.value == "")) {
                     alert("Please add Salary leave percent/leave days");
                     txt_leave_days_percent_salary.focus();
                     return false;
                 }

             }
             if (Selected_ddl_leave_taxable_salary == "Yes") {
                 if ((txt_leave_days_percent_salary.value == 0 && txt_leave_days_salary.value == 0) || (txt_leave_days_percent_salary.value == "" && txt_leave_days_salary.value == "")) {
                     alert("Please add Salary leave percent/leave days");
                     txt_leave_days_percent_salary.focus();
                     return false;
                 }

             }

             $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             var gratuaty_taxable_sal = document.getElementById('<%=ddl_gratuity_taxable_salary.ClientID %>');
            gratuaty_taxable_sal.disabled = false;
            var ddl_bonus_policy_aap_billing = document.getElementById('<%=ddl_bonus_policy_aap_billing.ClientID %>');
            var txt_bonus_amount_billing = document.getElementById('<%=txt_bonus_amount_billing.ClientID %>');
             var ddl_bonus_policy_aap_salary = document.getElementById('<%=ddl_bonus_policy_aap_salary.ClientID %>');
             var txt_bonus_amount_salary = document.getElementById('<%=txt_bonus_amount_salary.ClientID %>');
             ddl_bonus_policy_aap_billing.disabled = false;
             txt_bonus_amount_billing.disabled = false;
             ddl_bonus_policy_aap_salary.disabled = false;
             txt_bonus_amount_salary.disabled = false;
             var ddl_leave_taxable_salary = document.getElementById('<%=ddl_leave_taxable_salary.ClientID %>');
            ddl_leave_taxable_salary.disabled = false;
            var txt_leave_days = document.getElementById('<%=txt_leave_days.ClientID %>');
            txt_leave_days.disabled = false;
            var txt_leave_days_salary = document.getElementById('<%=txt_leave_days_salary.ClientID %>');
            txt_leave_days_salary.disabled = false;
            var ddl_leave_taxable_billing = document.getElementById('<%=ddl_leave_taxable_billing.ClientID %>');
            ddl_leave_taxable_billing.disabled = false;
            var ddl_bonus_taxable_billing = document.getElementById('<%= ddl_bonus_taxable_billing.ClientID %>');
            var ddl_bonus_taxable_salary = document.getElementById('<%=ddl_bonus_taxable_salary.ClientID %>');
             ddl_bonus_taxable_salary.disabled = false;
             ddl_bonus_taxable_billing.disabled = false;
             var txt_handling_percent = document.getElementById('<%=txt_handling_percent.ClientID %>');
             var txt_handling_amount = document.getElementById('<%=txt_handling_amount.ClientID %>');
                txt_handling_percent.disabled = false;
                txt_handling_amount.disabled = false;
                var ddl_machine_rental_applicable = document.getElementById('<%=ddl_machine_rental_applicable.ClientID %>');
            var txt_machine_rental_amount = document.getElementById('<%=txt_machine_rental_amount.ClientID %>');
            ddl_machine_rental_applicable.disabled = false;
            txt_machine_rental_amount.disabled = false;
             return true;

         }
         function hra_amount() {
             var txt1 = document.getElementById('<%=txt_hra_amount.ClientID %>');
            var txt2 = document.getElementById('<%=txt_hra_percent.ClientID %>');
            if (txt1.value != "0") {
                txt2.value = "0";
                return;
            }

        }
        function hra_amount_per() {
            var txt1 = document.getElementById('<%=txt_hra_amount.ClientID %>');
            var txt2 = document.getElementById('<%=txt_hra_percent.ClientID %>');
            if (txt2.value != "0") {
                txt1.value = "0";
                return;
            }



        }
        function hr_amount_per() {
            var txt2 = document.getElementById('<%=txt_hra_percent.ClientID %>');
            var t_hra_percent_salary = document.getElementById('<%=txt_hra_percent_salary.ClientID %>');
            if (txt2.value < 0 || txt2.value > 100) {
                alert("Please Enter Less Than Hundred");
                txt2.value = "0";
                t_hra_percent_salary.value = "0";
                txt2.focus();
                return false;
            }
            if (t_hra_percent_salary.value < 0 || t_hra_percent_salary.value > 100) {
                alert("Please Enter Less Than Hundred");
                t_hra_percent_salary.value = "0";
                t_hra_percent_salary.focus();
                return false;
            }
        }
        function leave_days_billing() {
            var txt1 = document.getElementById('<%=txt_leave_days.ClientID %>');
            var txt2 = document.getElementById('<%=txt_leave_days_percent.ClientID %>');
            if (txt1.value != "0") {
                txt2.value = "0";

                return;
            }

        }
        function leave_days_per_billing() {
            var txt1 = document.getElementById('<%=txt_leave_days.ClientID %>');
            var txt2 = document.getElementById('<%=txt_leave_days_percent.ClientID %>');
            if (txt2.value != "0") {
                txt1.value = "0";
                return;
            }

        }
        function leav_days_per_billing() {
            var txt2 = document.getElementById('<%=txt_leave_days_percent.ClientID %>');
            if (txt2.value < 0 || txt2.value > 100) {
                alert("Please Enter Less Than Hundred");
                txt2.value = "0";
                txt2.focus();
                return false;
            }
        }
        function leave_days_salary() {
            var txt1 = document.getElementById('<%=txt_leave_days_salary.ClientID %>');
            var txt2 = document.getElementById('<%=txt_leave_days_percent_salary.ClientID %>');
            if (txt1.value != "0") {
                txt2.value = "0";

                return;
            }

        }
        function leave_days_per_salary() {
            var txt1 = document.getElementById('<%=txt_leave_days_salary.ClientID %>');
                var txt2 = document.getElementById('<%=txt_leave_days_percent_salary.ClientID %>');
                if (txt2.value != "0") {
                    txt1.value = "0";
                    return;
                }

            }
            function leav_days_per_salary() {
                var txt2 = document.getElementById('<%=txt_leave_days_percent_salary.ClientID %>');
            if (txt2.value < 0 || txt2.value > 100) {
                alert("Please Enter Less Than Hundred");
                txt2.value = "0";
                txt2.focus();
                return false;
            }
        }
        function uniform_rate() {
            var txt1 = document.getElementById('<%=txt_bill_uniform_rate.ClientID %>');
            var txt2 = document.getElementById('<%=txt_bill_uniform_percent.ClientID %>');
            if (txt1.value != "0") {
                txt2.value = "0";
                return;
            }

        }
        function uniform_rate_per() {
            var txt1 = document.getElementById('<%=txt_bill_uniform_rate.ClientID %>');
            var txt2 = document.getElementById('<%=txt_bill_uniform_percent.ClientID %>');
            if (txt2.value != "0") {
                txt1.value = "0";
                return;
            }

        }
        function unifor_rate_per() {
            var txt2 = document.getElementById('<%=txt_bill_uniform_percent.ClientID %>');
            if (txt2.value < 0 || txt2.value > 100) {
                alert("Please Enter Less Than Hundred");
                txt2.value = "0";
                txt2.focus();
                return false;
            }
        }
        function sal_uniform_rate() {
            var txt1 = document.getElementById('<%=txt_sal_uniform_rate.ClientID %>');
            var txt2 = document.getElementById('<%=txt_sal_uniform_percent.ClientID %>');
            if (txt1.value != "0") {
                txt2.value = "0";
                return;
            }

        }
        function sal_uniform_rate_per() {
            var txt1 = document.getElementById('<%=txt_sal_uniform_rate.ClientID %>');
             var txt2 = document.getElementById('<%=txt_sal_uniform_percent.ClientID %>');
             if (txt2.value != "0") {
                 txt1.value = "0";
                 return;
             }

         }
         function sa_uniform_rate_per() {
             var txt2 = document.getElementById('<%=txt_sal_uniform_percent.ClientID %>');
            if (txt2.value < 0 || txt2.value > 100) {
                alert("Please Enter Less Than Hundred");
                txt2.value = "0";
                txt2.focus();
                return false;
            }
        }
        function oper_cost() {
            var txt1 = document.getElementById('<%=txt_bill_oper_cost_amt.ClientID %>');
            var txt2 = document.getElementById('<%=txt_bill_oper_cost_percent.ClientID %>');
            if (txt1.value != "0") {
                txt2.value = "0";
                return;
            }

        }
        function oper_cost_per() {
            var txt1 = document.getElementById('<%=txt_bill_oper_cost_amt.ClientID %>');
             var txt2 = document.getElementById('<%=txt_bill_oper_cost_percent.ClientID %>');
             if (txt2.value != "0") {
                 txt1.value = "0";
                 return;
             }

         }
         function ope_cost_per() {
             var txt2 = document.getElementById('<%=txt_bill_oper_cost_percent.ClientID %>');
            if (txt2.value < 0 || txt2.value > 100) {
                alert("Please Enter Less Than Hundred");
                txt2.value = "0";
                txt2.focus();
                return false;
            }
        }
        function bill_bonus_percent() {
            var txt2 = document.getElementById('<%=txt_bill_bonus_percent.ClientID %>');
            if (txt2.value < 0 || txt2.value > 100) {
                alert("Please Enter Less Than Hundred");
                txt2.value = "8.33";
                txt2.focus();
                return false;
            }
            b_taxable();
            return true;
        }
        function bill_pf_percent() {
            var txt2 = document.getElementById('<%=txt_bill_pf_percent.ClientID %>');
            if (txt2.value < 0 || txt2.value > 100) {
                alert("Please Enter Less Than Hundred");
                txt2.value = "0";
                txt2.focus();
                return false;
            }
        }
        function bill_esic_percent() {
            var txt2 = document.getElementById('<%=txt_bill_esic_percent.ClientID %>');
            if (txt2.value < 0 || txt2.value > 100) {
                alert("Please Enter Less Than Hundred");
                txt2.value = "0";
                txt2.focus();
                return false;
            }
        }
        function bill_relieving() {
            var txt2 = document.getElementById('<%=txt_bill_relieving.ClientID %>');
            if (txt2.value < 0 || txt2.value > 100) {
                alert("Please Enter Less Than Hundred");
                txt2.value = "0";
                txt2.focus();
                return false;
            }
        }
        function bill_service_charge() {
            var txt2 = document.getElementById('<%=txt_bill_service_charge.ClientID %>');
             if (txt2.value < 0 || txt2.value > 100) {
                 alert("Please Enter Less Than Hundred");
                 txt2.value = "0";
                 txt2.focus();
                 return false;
             }
         }
         function sal_bonus() {
             var txt2 = document.getElementById('<%=txt_sal_bonus.ClientID %>');
            if (txt2.value < 0 || txt2.value > 100) {
                alert("Please Enter Less Than Hundred");
                txt2.value = "0";
                txt2.focus();
                return false;
            }
        }
        function sal_pf() {
            var txt2 = document.getElementById('<%=txt_sal_pf.ClientID %>');
            if (txt2.value < 0 || txt2.value > 100) {
                alert("Please Enter Less Than Hundred");
                txt2.value = "0";
                txt2.focus();
                return false;
            }
        }

        function sal_esic() {
            var txt2 = document.getElementById('<%=txt_sal_esic.ClientID %>');
            if (txt2.value < 0 || txt2.value > 100) {
                alert("Please Enter Less Than Hundred");
                txt2.value = "0";
                txt2.focus();
                return false;
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
        function isNumber0(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            hra_amount();
            return true;
        }
        function isNumber0_dot(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 46 || charCode > 46)) {
                    return false;
                }
            }
            hra_amount_per()

            return true;
        }
        function isNumber1(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            leave_days_billing();
            leave_days_salary();
            Conveyance_Service_Charge_Amount();

            return true;

        }
        function isNumber1_dot(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 46 || charCode > 46)) {
                    return false;
                }
            }
            leave_days_per_billing();
            leave_days_per_salary();
            Conveyance_Service_Charge();
            Conveyance_Service_Charge_per();

            return true;

        }
        function isNumber2(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            uniform_rate();

            return true;

        }
        function isNumber2_dot(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 46 || charCode > 46)) {
                    return false;
                }
            }
            uniform_rate_per();

            return true;

        }
        function isNumber3(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            oper_cost();

            return true;

        }
        function isNumber3_dot(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 46 || charCode > 46)) {
                    return false;
                }
            }
            oper_cost_per();

            return true;

        }
        function isNumber4(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            sal_uniform_rate();

            return true;

        }
        function isNumber4_dot(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 46 || charCode > 46)) {
                    return false;
                }
            }
            sal_uniform_rate_per();

            return true;

        }

        function add_number() {
            var t_basic = document.getElementById('<%=txt_basic_billing.ClientID %>');
            if (t_basic.value == "") { t_basic.value = "0"; }
            var t_vda = document.getElementById('<%=txt_vda_billing.ClientID %>');
            if (t_vda.value == "") { t_vda.value = "0"; }
            var t_basic_vda = document.getElementById('<%=txt_basic_vda_billing.ClientID %>');
            var result = parseFloat(t_basic.value) + parseFloat(t_vda.value);
            var t_basic_sal = document.getElementById('<%=txt_basic_salary.ClientID %>');
            var t_vda_salary = document.getElementById('<%=txt_vda_salary.ClientID %>');

            var t_hra_amount = document.getElementById('<%=txt_hra_amount.ClientID %>');
            var t_hra_percent = document.getElementById('<%=txt_hra_percent.ClientID %>');
            var t_hra_amount_salary = document.getElementById('<%=txt_hra_amount_salary.ClientID %>');
            var t_hra_percent_salary = document.getElementById('<%=txt_hra_percent_salary.ClientID %>');

            if (!isNaN(result)) {
                t_basic_sal.value = t_basic.value;
                t_vda_salary.value = t_vda.value;
                t_basic_vda.value = result;

                t_hra_amount_salary.value = t_hra_amount.value;
                t_hra_percent_salary.value = t_hra_percent.value;

                ot_policy_billing();
                add_number1();
                hr_amount_per();
                return true;
            }
            return false;

        }
        function add_number1() {
            var t_basic_bill = document.getElementById('<%=txt_basic_billing.ClientID %>');
            if (t_basic_bill.value == "") { t_basic_bill.value = "0"; }
            var t_vda_bill = document.getElementById('<%=txt_vda_billing.ClientID %>');
            if (t_vda_bill.value == "") { t_vda_bill.value = "0"; }

            var t_basic_vda_bill = document.getElementById('<%=txt_basic_vda_billing.ClientID %>');
            var result_bill = parseFloat(t_basic_bill.value) + parseFloat(t_vda_bill.value);

            t_basic_vda_bill.value = result_bill;

            var t_basic = document.getElementById('<%=txt_basic_salary.ClientID %>');
            if (t_basic.value == "") { t_basic.value = "0"; }
            var t_vda = document.getElementById('<%=txt_vda_salary.ClientID %>');
            if (t_vda.value == "") { t_vda.value = "0"; }
            var t_basic_vda = document.getElementById('<%=txt_basic_vda_salary.ClientID %>');
            var result = parseFloat(t_basic.value) + parseFloat(t_vda.value);
            if (!isNaN(result)) {
                t_basic_vda.value = result;
                ot_policy();
                return true;
            }
            return false;
        }
        function change_txt_box() {
            var bonus_a = document.getElementById('<%=ddl_bonus_policy_aap_billing.ClientID %>');
            var Selected_bonus_a = bonus_a.options[bonus_a.selectedIndex].text;
            var bonus_amount = document.getElementById('<%=txt_bonus_amount_billing.ClientID %>');
            if (Selected_bonus_a == "No") {
                bonus_amount.disabled = true;
                bonus_amount.value = "0";
            }
            else {
                bonus_amount.value = "7000";
                bonus_amount.disabled = false;
            }
        }
        function change_txt_box1() {
            var bonus_a = document.getElementById('<%=ddl_bonus_policy_aap_salary.ClientID %>');
            var Selected_bonus_a = bonus_a.options[bonus_a.selectedIndex].text;
            var bonus_amount = document.getElementById('<%=txt_bonus_amount_salary.ClientID %>');

             if (Selected_bonus_a == "Yes") {
                 bonus_amount.value = "7000";
                 bonus_amount.disabled = false;
             }
             else {
                 bonus_amount.disabled = true;
                 bonus_amount.value = "0";

             }
         }
         function change_txt_box11() {
             var bonus_a = document.getElementById('<%=ddl_bonus_policy_aap_salary.ClientID %>');
            var Selected_bonus_a = bonus_a.options[bonus_a.selectedIndex].text;
            var bonus_t = document.getElementById('<%=ddl_bonus_taxable_salary.ClientID %>');
            var Selected_bonus_t = bonus_t.options[bonus_t.selectedIndex].text;
            var bonus_percent = document.getElementById('<%=txt_sal_bonus.ClientID %>');
            var bonus_amount = document.getElementById('<%=txt_bonus_amount_salary.ClientID %>');
            if (Selected_bonus_a == "No") {
                bonus_amount.value = "0";
                bonus_amount.readOnly = true;
                bonus_t.value = 0;
                bonus_t.disabled = true;
                return true;
            }
            else if (Selected_bonus_a == "Advance Policy") {
                bonus_amount.value = "0";
                bonus_amount.readOnly = true;
                bonus_t.disabled = true;
                bonus_percent.value = "8.33";
                return true;
            }
            else if (Selected_bonus_a == "Not Applicable") {
                bonus_amount.value = "0";
                bonus_amount.readOnly = true;
                return true;
            }
            else {
                bonus_t.disabled = false;
                bonus_amount.readOnly = false;
                bonus_percent.readOnly = false;
            }
            if (Selected_bonus_a == "Yes") {
                if (bonus_amount.value == "" || bonus_amount.value == "0") {
                    bonus_amount.value == "7000";
                    bonus_amount.focus();
                    return true;
                }
            }

        }

        function bonus_taxable() {
            var bonus_taxable = document.getElementById('<%=ddl_bonus_taxable_billing.ClientID %>');
            var Selected_bonus_taxable = bonus_taxable.options[bonus_taxable.selectedIndex].text;
            var bonus_percent = document.getElementById('<%=txt_bill_bonus_percent.ClientID %>');
            //Salary
            var bonus_taxable_sal = document.getElementById('<%=ddl_bonus_taxable_salary.ClientID %>');

            //bonus_percent.value = "8.33";
            bonus_percent.readOnly = false;

            if (Selected_bonus_taxable == "Not Applicable") {
                bonus_percent.value = "0";
                bonus_percent.readOnly = true;
                bonus_taxable_sal.disabled = true;
                bonus_taxable_sal.options[2].selected = true;

                bonus_taxable_salary();
                return true;
            }
            else if (Selected_bonus_taxable == "No") {
                //for salary
                bonus_taxable_sal.options[0].selected = true;
                bonus_taxable_sal.disabled = false;
                bonus_taxable_salary();
                return true;
            }
            else if (Selected_bonus_taxable == "Yes") {
                //for salary
                bonus_taxable_sal.options[1].selected = true;
                bonus_taxable_sal.disabled = false;
                bonus_taxable_salary();
                return true;
            }
        }

        function bonus_taxable_salary() {
            var bonus_taxable = document.getElementById('<%=ddl_bonus_taxable_salary.ClientID %>');
            var Selected_bonus_taxable = bonus_taxable.options[bonus_taxable.selectedIndex].text;
            var bonus_percent = document.getElementById('<%=txt_sal_bonus.ClientID %>');
            //billing
            var bonus_taxable_bill = document.getElementById('<%=ddl_bonus_taxable_billing.ClientID %>');
            var Selected_bonus_taxable_bill = bonus_taxable_bill.options[bonus_taxable_bill.selectedIndex].text;

            bonus_percent.value = "8.33";
            bonus_percent.readOnly = false;
            if (Selected_bonus_taxable == "Not Applicable") {
                bonus_percent.value = "0";
                bonus_percent.readOnly = true;
                return true;
            }
            else if (Selected_bonus_taxable == "Advance Policy") {
                alert("In Advance Policy Bonus Amount will not be Taxable.");
                return true;
            }
            else if (Selected_bonus_taxable == "No") {
                if (Selected_bonus_taxable_bill == "Yes") {
                    alert("In Billing Bonus Amount is Taxable.");
                }
                if (Selected_bonus_taxable_bill == "Not Applicable") {
                    alert("In Billing Bonus Policy is Not Applicable, Select Not Applicable / Advance Policy.");
                    bonus_taxable.options[2].selected = true;
                    bonus_percent.value = "0";
                    bonus_percent.readOnly = true;
                }
                return true;
            }
            else if (Selected_bonus_taxable == "Yes") {
                if (Selected_bonus_taxable_bill == "No") {
                    alert("In Billing Bonus Amount is Not Taxable.");
                }
                if (Selected_bonus_taxable_bill == "Not Applicable") {
                    alert("In Billing Bonus Policy is Not Applicable, Select Not Applicable / Advance Policy.");
                    bonus_taxable.options[2].selected = true;
                    bonus_percent.value = "0";
                    bonus_percent.readOnly = true;
                }
                return true;
            }

        }

        function ot_allowed() {
            var ddl_hrs1 = document.getElementById('<%=ddl_ot_applicable.ClientID %>');
            var ddl_hrs_value = ddl_hrs1.options[ddl_hrs1.selectedIndex].text;
            var ddl_ot = document.getElementById('<%=ddl_esic_ot.ClientID %>');

            if (ddl_hrs_value == "No") {
                ddl_ot.options[0].selected = true;
                ddl_ot.disabled = true;
            }
            else {
                ddl_ot.disabled = false;
            }
            return true;
        }

        function billing_hours() {
            alert("Inside billing");
            var ddl_hrs = document.getElementById('<%=ddl_hours.ClientID %>');
            var ddl_hrs_value = ddl_hrs.options[ddl_hrs.selectedIndex].text;
            var ddl_ot = document.getElementById('<%=ddl_ot_policy.ClientID %>');
              var txt_ot_amt = document.getElementById('<%=txt_ot_amount.ClientID %>');
            var ddl_ot_bill = document.getElementById('<%=ddl_ot_policy_billing.ClientID %>');
            var txt_ot_amt_bill = document.getElementById('<%=txt_ot_amount_billing.ClientID %>');

            alert("Inside billing hrs " + ddl_hrs_value);
            if (ddl_hrs_value.value == "") { ddl_hrs_value.value = "0"; }
            if (parseInt(ddl_hrs_value) == 12) {
                ddl_ot.disabled = false;
                txt_ot_amt.readOnly = false;
                ddl_ot_bill.disabled = false;
                txt_ot_amt_bill.readOnly = false;
            }
            else {
                ddl_ot.options[0].selected = true;
                ddl_ot.disabled = true;
                txt_ot_amt.value = "0";
                txt_ot_amt.readOnly = true;
                ddl_ot_bill.options[0].selected = true;
                ddl_ot_bill.disabled = true;
                txt_ot_amt_bill.value = "0";
                txt_ot_amt_bill.readOnly = true;
            }
            return true;
        }
        function ot_policy() {
            var ot_app = document.getElementById('<%=ddl_ot_policy.ClientID %>');
              var ot_app_value = ot_app.options[ot_app.selectedIndex].text;
              var ot_amount = document.getElementById('<%=txt_ot_amount.ClientID %>');
            var ot_basic_vda = document.getElementById('<%=txt_basic_vda_salary.ClientID %>');

              if (ot_app_value == "Yes") {
                  ot_amount.value = (parseInt(ot_basic_vda.value) / 2);
              }
              else { ot_amount.value = 0; }
              return true;
          }

          function ot_policy_billing() {
              var ot_app = document.getElementById('<%=ddl_ot_policy_billing.ClientID %>');
            var ot_app_value = ot_app.options[ot_app.selectedIndex].text;
            var ot_amount = document.getElementById('<%=txt_ot_amount_billing.ClientID %>');
            var ot_basic_vda = document.getElementById('<%=txt_basic_vda_billing.ClientID %>');

            if (ot_app_value == "Yes") {
                ot_amount.value = (parseInt(ot_basic_vda.value) / 2);
            }
            else { ot_amount.value = 0; }
            return true;
        }


        function leave_taxable() {
            var leave_taxable = document.getElementById('<%=ddl_leave_taxable_billing.ClientID %>');
            var Selected_leave_taxable = leave_taxable.options[leave_taxable.selectedIndex].text;
            var leave_percent = document.getElementById('<%=txt_leave_days_percent.ClientID %>');
            var leave_day = document.getElementById('<%=txt_leave_days.ClientID %>');

            //Salary
            var leave_taxable_sal = document.getElementById('<%=ddl_leave_taxable_salary.ClientID %>');

            leave_percent.readOnly = false;
            leave_day.readOnly = false;
            if (Selected_leave_taxable == "Not Applicable") {
                leave_percent.value = "0";
                leave_percent.readOnly = true;
                leave_day.value = "0";
                leave_day.disabled = true;
                leave_taxable_sal.disabled = true;
                leave_taxable_sal.options[2].selected = true;

                leave_taxable_salary();
                return true;
            }
            else if (Selected_leave_taxable == "No") {

                //for salary
                leave_taxable_sal.options[0].selected = true;
                leave_taxable_sal.disabled = false;
                leave_taxable_salary();
                return true;
            }
            else if (Selected_leave_taxable == "Yes") {

                //for salary
                leave_taxable_sal.options[1].selected = true;
                leave_taxable_sal.disabled = false;
                leave_taxable_salary();
                return true;
            }
            l_taxable();
            return true;
        }

        function leave_taxable_salary() {

            var leave_taxable = document.getElementById('<%=ddl_leave_taxable_salary.ClientID %>');
            var Selected_leave_taxable = leave_taxable.options[leave_taxable.selectedIndex].text;
            var leave_percent = document.getElementById('<%=txt_leave_days_percent_salary.ClientID %>');
            var leave_day = document.getElementById('<%=txt_leave_days_salary.ClientID %>');
            //billing
            var leave_taxable_bill = document.getElementById('<%=ddl_leave_taxable_billing.ClientID %>');
            var Selected_leave_taxable_bill = leave_taxable_bill.options[leave_taxable_bill.selectedIndex].text;

            leave_percent.readOnly = false;
            leave_day.readOnly = false;

            if (Selected_leave_taxable == "Not Applicable") {
                leave_percent.value = "0";
                leave_percent.readOnly = true;
                leave_day.value = "0";
                leave_day.disabled = true;
                return true;
            }
            else if (Selected_leave_taxable == "Advance Policy") {
                alert("In Advance Policy Leave Amount will not be Taxable.");
                return true;
            }
            else if (Selected_leave_taxable == "No") {
                if (Selected_leave_taxable_bill == "Yes") {
                    alert("In Billing Leave Amount is Taxable.");
                }
                if (Selected_leave_taxable_bill == "Not Applicable") {
                    alert("In Billing Leave Policy is Not Applicable, Select Not Applicable / Advance Policy.");
                    leave_taxable.options[2].selected = true
                    leave_percent.value = "0";
                    leave_percent.readOnly = true;
                    leave_day.value = "0";
                    leave_day.disabled = true;
                }
                return true;
            }
            else if (Selected_leave_taxable == "Yes") {
                if (Selected_leave_taxable_bill == "No") {
                    alert("In Billing Leave Amount is Not Taxable.");
                }
                if (Selected_leave_taxable_bill == "Not Applicable") {
                    alert("In Billing Leave Policy is Not Applicable, Select Not Applicable / Advance Policy.");
                    leave_taxable.options[2].selected = true;
                    leave_percent.value = "0";
                    leave_percent.readOnly = true;
                    leave_day.value = "0";
                    leave_day.disabled = true;
                }

                return true;
            }
        }

        function gratuaty_taxable() {
            var gratuaty_taxable = document.getElementById('<%=ddl_gratuity_taxable_billing.ClientID %>');
            var Selected_gratuaty_taxable = gratuaty_taxable.options[gratuaty_taxable.selectedIndex].text;
            var gratuaty_percent = document.getElementById('<%=txt_gratuity_percent_billing.ClientID %>');
            //For Salalry
            var gratuaty_taxable_sal = document.getElementById('<%=ddl_gratuity_taxable_salary.ClientID %>');

            // alert("1");
            // alert(Selected_gratuaty_taxable);
            if (Selected_gratuaty_taxable == "Not Applicable") {
                gratuaty_percent.value = "0";
                gratuaty_percent.readOnly = true;
                //for salary
                // alert("4");
                gratuaty_taxable_sal.options[2].selected = true;
                gratuaty_taxable_sal.disabled = true;
                gratuaty_taxable_salary();
                return true;
            }
            else if (Selected_gratuaty_taxable == "No") {
                gratuaty_percent.value = "4.81";
                gratuaty_percent.readOnly = false;
                //for salary
                gratuaty_taxable_sal.options[0].selected = true;
                gratuaty_taxable_sal.disabled = false;
                gratuaty_taxable_salary();
                return true;
            }
            else if (Selected_gratuaty_taxable == "Yes") {
                gratuaty_percent.value = "4.81";
                gratuaty_percent.readOnly = false;
                //for salary
                gratuaty_taxable_sal.options[1].selected = true;
                gratuaty_taxable_sal.disabled = false;
                gratuaty_taxable_salary();
                return true;
            }
            //else {
            //    gratuaty_percent.value = "4.81";
            //    gratuaty_percent.readOnly = false;
            //    //for salary
            //    gratuaty_taxable_sal.disabled = false;
            //    gratuaty_taxable_sal.options[0].selected = true;
            //    gratuaty_taxable_salary();
            //}
        }
        function gratuaty_taxable_salary() {
            var gratuaty_taxable = document.getElementById('<%=ddl_gratuity_taxable_salary.ClientID %>');
            var Selected_gratuaty_taxable = gratuaty_taxable.options[gratuaty_taxable.selectedIndex].text;
            var gratuaty_percent = document.getElementById('<%=txt_sal_graguity_per.ClientID %>');

            //For Billing
            var gratuaty_taxable_bill = document.getElementById('<%=ddl_gratuity_taxable_billing.ClientID %>');
            var Selected_gratuaty_taxable_bill = gratuaty_taxable_bill.options[gratuaty_taxable_bill.selectedIndex].text;

            gratuaty_percent.value = "4.81";
            gratuaty_percent.readOnly = false;
            // alert("2");
            // alert(Selected_gratuaty_taxable);
            if (Selected_gratuaty_taxable == "Not Applicable") {
                gratuaty_percent.value = "0";
                gratuaty_percent.readOnly = true;
                return true;
            }
            else if (Selected_gratuaty_taxable == "No") {
                if (Selected_gratuaty_taxable_bill == "Yes") {
                    alert("In Billing Gratuity is Taxable.");
                }
                return true;
            }
            else if (Selected_gratuaty_taxable == "Yes") {
                if (Selected_gratuaty_taxable_bill == "No") {
                    alert("In Billing Gratuity is Not Taxable.");
                }
                g_taxable();
                return true;
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
        function uniform_dropdown() {
            var ddl_bill_ser_uniform = document.getElementById('<%=ddl_bill_ser_uniform.ClientID %>');
            var Selected_ddl_bill_ser_uniform = ddl_bill_ser_uniform.options[ddl_bill_ser_uniform.selectedIndex].text;
            var ddl_relieving_uniform = document.getElementById('<%=ddl_relieving_uniform.ClientID %>');
            var Selected_ddl_relieving_uniform = ddl_relieving_uniform.options[ddl_relieving_uniform.selectedIndex].text;

            if (Selected_ddl_bill_ser_uniform == "No") {
                ddl_relieving_uniform.options[0].selected = true;
                ddl_relieving_uniform.disabled = true;

            }
            else {
                ddl_relieving_uniform.disabled = false;
            }
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

        function material_policy() {
            var d_material_contract = document.getElementById('<%=ddl_material_contract.ClientID %>');
            var Selected_material_contract = d_material_contract.options[d_material_contract.selectedIndex].text;

            var ddl_equmental_applicable = document.getElementById('<%=ddl_equmental_applicable.ClientID %>');
            var Selected_ddl_equmental_applicable = ddl_equmental_applicable.options[ddl_equmental_applicable.selectedIndex].text;

            var ddl_chemical_applicable = document.getElementById('<%=ddl_chemical_applicable.ClientID %>');
            var Selected_ddl_chemical_applicable = ddl_chemical_applicable.options[ddl_chemical_applicable.selectedIndex].text;

            var ddl_dustin_applicable = document.getElementById('<%=ddl_dustin_applicable.ClientID %>');
            var Selected_ddl_dustin_applicable = ddl_dustin_applicable.options[ddl_dustin_applicable.selectedIndex].text;

            var ddl_femina_applicable = document.getElementById('<%=ddl_femina_applicable.ClientID %>');
            var Selected_ddl_femina_applicable = ddl_femina_applicable.options[ddl_femina_applicable.selectedIndex].text;

            var ddl_aerosol_applicable = document.getElementById('<%=ddl_aerosol_applicable.ClientID %>');
            var Selected_ddl_aerosol_applicable = ddl_aerosol_applicable.options[ddl_aerosol_applicable.selectedIndex].text;

            var ddl_tool_applicable = document.getElementById('<%=ddl_tool_applicable.ClientID %>');
            var Selected_ddl_tool_applicable = ddl_tool_applicable.options[ddl_tool_applicable.selectedIndex].text;

            //ar d_cotract_type = document.getElementById('<%=ddl_cotract_type.ClientID %>');
            var t_contract_rate = document.getElementById('<%=txt_contract_rate.ClientID %>');
            var d_handling_charge = document.getElementById('<%=ddl_handling_charge.ClientID %>');

            var t_handling_percent = document.getElementById('<%=txt_handling_percent.ClientID %>');

            var ddl_machine_rental_applicable = document.getElementById('<%=ddl_machine_rental_applicable.ClientID %>');
            var Selected_ddl_machine_rental_applicable = ddl_machine_rental_applicable.options[ddl_machine_rental_applicable.selectedIndex].text;

            var txt_machine_rental_amount = document.getElementById('<%=txt_machine_rental_amount.ClientID %>');

            if (Selected_material_contract == "No") {

                //d_cotract_type.options[0].selected = true;
                //d_cotract_type.disabled = true;
                t_contract_rate.value = "0";
                t_contract_rate.readOnly = true;
                ddl_equmental_applicable.disabled = true;
                ddl_equmental_applicable.options[0].selected = true;
                ddl_chemical_applicable.disabled = true;
                ddl_chemical_applicable.options[0].selected = true;
                ddl_dustin_applicable.disabled = true;
                ddl_dustin_applicable.options[0].selected = true;
                ddl_femina_applicable.disabled = true;
                ddl_femina_applicable.options[0].selected = true;
                ddl_aerosol_applicable.disabled = true;
                ddl_aerosol_applicable.options[0].selected = true;
                ddl_tool_applicable.disabled = true;
                ddl_tool_applicable.options[0].selected = true;
                ddl_machine_rental_applicable.disabled = true;
                ddl_machine_rental_applicable.options[0].selected = true;
                document.getElementById('<%=ddl_handling_charge.ClientID %>').value = 0;
                //d_handling_charge.options[0].selected = true;
                d_handling_charge.disabled = true;
                t_handling_percent.value = "0";
                t_handling_percent.readOnly = true;
                txt_machine_rental_amount.value = "0";
                txt_machine_rental_amount.readOnly = true;
                Rental_cost();
                consume_applicable();
                linear_applicable();
                bin_applicable();
                machine_applicable();
                tackle_applicable();
            }
            if (ddl_machine_rental_applicable == "Yes")
            {
                txt_machine_rental_amount.value = "0";
                txt_machine_rental_amount.disabled = false;
            }
            else {
                //  d_cotract_type.disabled = false;
                t_contract_rate.readOnly = false;
                d_handling_charge.disabled = false;
                t_handling_percent.readOnly = false;
                ddl_equmental_applicable.disabled = false;
                ddl_chemical_applicable.disabled = false;
                ddl_dustin_applicable.disabled = false;
                ddl_femina_applicable.disabled = false;
                ddl_aerosol_applicable.disabled = false;
                ddl_tool_applicable.disabled = false;
                ddl_machine_rental_applicable.disabled = false;
            }

        }
        function change_drop() {
            //vikas add 22/06/2019
            var ddl_handling_charge = document.getElementById('<%=ddl_handling_charge.ClientID %>');
            var Selected_ddl_handling_charge = ddl_handling_charge.options[ddl_handling_charge.selectedIndex].text;

            var txt_handling_percent = document.getElementById('<%=txt_handling_percent.ClientID %>');
            var txt_handling_amount = document.getElementById('<%=txt_handling_amount.ClientID %>');
            if (Selected_ddl_handling_charge == "Yes") {
                txt_handling_percent.disabled = false;
                txt_handling_amount.disabled = false;
            }
            if (Selected_ddl_handling_charge == "No") {
                txt_handling_percent.disabled = true;
                txt_handling_amount.disabled = true;
                txt_handling_percent.value = "0";
                txt_handling_amount.value = "0";
            }
        }
        function deep_clean_applicable() {
            var ddl_deepclean_contract = document.getElementById('<%=ddl_dc_contract.ClientID %>');
            var Selected_ddl_deepclean_contract = ddl_deepclean_contract.options[ddl_deepclean_contract.selectedIndex].text;

            var ddl_dc_type = document.getElementById('<%=ddl_dc_type.ClientID %>');
            var txt_dc_rate = document.getElementById('<%=txt_dc_rate.ClientID %>');
            var txt_per_sq_ft = document.getElementById('<%=txt_dc_area.ClientID %>');
            var ddl_dc_handling_charge = document.getElementById('<%=ddl_dc_handling_charge.ClientID %>');
            var txt_dc_handling_percent = document.getElementById('<%=txt_dc_handling_percent.ClientID %>');

            var ddl_pc_contract = document.getElementById('<%=ddl_pc_contract.ClientID %>');
            var Selected_ddl_pc_contract = ddl_pc_contract.options[ddl_pc_contract.selectedIndex].text;

            var ddl_pc_type = document.getElementById('<%=ddl_pc_type.ClientID %>');
            var txt_pc_rate = document.getElementById('<%=txt_pc_rate.ClientID %>');
            var txt_pc_area = document.getElementById('<%=txt_pc_area.ClientID %>');
            var ddl_pc_handling_charge = document.getElementById('<%=ddl_pc_handling_charge.ClientID %>');
            var txt_pc_handling_percent = document.getElementById('<%=txt_pc_handling_percent.ClientID %>');

            if (Selected_ddl_pc_contract == "No") {
                ddl_pc_type.disabled = true;
                txt_pc_rate.disabled = true;
                txt_pc_area.disabled = true;
                ddl_pc_handling_charge.disabled = true;
                txt_pc_handling_percent.disabled = true;
            }
            else {
                ddl_pc_type.disabled = false;
                txt_pc_rate.disabled = false;
                txt_pc_area.disabled = false;
                ddl_pc_handling_charge.disabled = false;
                txt_pc_handling_percent.disabled = false;
            }
            if (Selected_ddl_deepclean_contract == "No") {
                ddl_dc_type.disabled = true;
                txt_dc_rate.disabled = true;
                txt_per_sq_ft.disabled = true;
                ddl_dc_handling_charge.disabled = true;
                txt_dc_handling_percent.disabled = true;

            }
            else {
                ddl_dc_type.disabled = false;
                txt_dc_rate.disabled = false;
                txt_per_sq_ft.disabled = false;
                ddl_dc_handling_charge.disabled = false;
                txt_dc_handling_percent.disabled = false;

            }

        }
        function deep_clean_type() {
            var ddl_dc_type = document.getElementById('<%=ddl_dc_type.ClientID %>');
            var Selected_ddl_dc_type = ddl_dc_type.options[ddl_dc_type.selectedIndex].text;

            var ddl_pc_type = document.getElementById('<%=ddl_pc_type.ClientID %>');
            var Selected_ddl_pc_type = ddl_pc_type.options[ddl_pc_type.selectedIndex].text;

            var txt_pc_area = document.getElementById('<%=txt_pc_area.ClientID %>');

            if (Selected_ddl_pc_type == "Fix") {
                txt_pc_area.disabled = true;
                txt_pc_area.value = "0";
            }
            else if (Selected_ddl_pc_type == "Select") {
                txt_pc_area.disabled = true;
                txt_pc_area.value = "0";
            }
            else { txt_pc_area.disabled = false; }

            var txt_per_sq_ft = document.getElementById('<%=txt_dc_area.ClientID %>');

            if (Selected_ddl_dc_type == "Fix") {
                txt_per_sq_ft.disabled = true;
                txt_per_sq_ft.value = "0";
            }
            else if (Selected_ddl_dc_type == "Select") {
                txt_per_sq_ft.disabled = true;
                txt_per_sq_ft.value = "0";
            }
            else {
                txt_per_sq_ft.disabled = false;
            }

        }
        //vikas 19/06/2019
        function hidde_in() {

            var ddl_h_c_applicable = document.getElementById('<%=ddl_h_c_applicable.ClientID %>');
            var Selected_ddl_h_c_applicable = ddl_h_c_applicable.options[ddl_h_c_applicable.selectedIndex].text;
            var txt_in_per = document.getElementById('<%=txt_in_per.ClientID %>');
		    var txt_in_amt = document.getElementById('<%=txt_in_amt.ClientID %>');
            if (Selected_ddl_h_c_applicable == "No") {
                txt_in_per.disabled = true;
                txt_in_amt.disabled = true;
                txt_in_per.value = "0";
                txt_in_amt.value = "0";
            }
            else {
                txt_in_per.disabled = false;
                txt_in_amt.disabled = false;
            }
        }
        function ddl_Conveyance_Applicable() {

            var ddl_conveyance_applicable = document.getElementById('<%=ddl_conveyance_applicable.ClientID %>');
		    var Selected_ddl_conveyance_applicable = ddl_conveyance_applicable.options[ddl_conveyance_applicable.selectedIndex].text;

		    var ddl_conveyance_type = document.getElementById('<%=ddl_conveyance_type.ClientID %>');
            var txt_conveyance_rate = document.getElementById('<%=txt_conveyance_rate.ClientID %>');
		    var txt_conveyance_km = document.getElementById('<%=txt_conveyance_km.ClientID %>');
		    var ddl_conveyance_service_charge = document.getElementById('<%=ddl_conveyance_service_charge.ClientID %>');
		    var txt_conveyance_service_charge = document.getElementById('<%=txt_conveyance_service_charge.ClientID %>');
		    var txt_conveyance_service_amount = document.getElementById('<%=txt_conveyance_service_amount.ClientID %>');

		    if (Selected_ddl_conveyance_applicable == "No") {
		        ddl_conveyance_type.disabled = true;
		        txt_conveyance_rate.disabled = true;
		        txt_conveyance_km.disabled = true;
		        ddl_conveyance_service_charge.disabled = true;
		        txt_conveyance_service_charge.disabled = true;
		        txt_conveyance_service_amount.disabled = true;
		    }
		    else {
		        ddl_conveyance_type.disabled = false;
		        txt_conveyance_rate.disabled = false;
		        txt_conveyance_km.disabled = false;
		        ddl_conveyance_service_charge.disabled = false;
		        txt_conveyance_service_charge.disabled = false;
		        txt_conveyance_service_amount.disabled = false;
		    }
		    Service_Charge_Applicable();
		    Conveyance_type();
		    return true;
		}
		function Service_Charge_Applicable() {
		    var ddl_conveyance_service_charge = document.getElementById('<%=ddl_conveyance_service_charge.ClientID %>');
            var Selected_ddl_conveyance_service_charge = ddl_conveyance_service_charge.options[ddl_conveyance_service_charge.selectedIndex].text;

            var txt_conveyance_service_charge = document.getElementById('<%=txt_conveyance_service_charge.ClientID %>');
            var txt_conveyance_service_amount = document.getElementById('<%=txt_conveyance_service_amount.ClientID %>');

            if (Selected_ddl_conveyance_service_charge == "No") {
                txt_conveyance_service_charge.disabled = true;
                txt_conveyance_service_amount.disabled = true;
                txt_conveyance_service_charge.value = "0";
                txt_conveyance_service_amount.value = "0";

            }
            else {
                txt_conveyance_service_charge.disabled = false;
                txt_conveyance_service_amount.disabled = false;
            }
        }
        function machine_rental() {

            var ddl_machine_rental_applicable = document.getElementById('<%=ddl_machine_rental_applicable.ClientID %>');
            var Selected_ddl_machine_rental_applicable = ddl_machine_rental_applicable.options[ddl_machine_rental_applicable.selectedIndex].text;

            var txt_machine_rental_amount = document.getElementById('<%=txt_machine_rental_amount.ClientID %>');
            if (Selected_ddl_machine_rental_applicable == "No") {
               
                txt_machine_rental_amount.value = "0";
                txt_machine_rental_amount.disabled = true;
            }
            else {
                txt_machine_rental_amount.disabled = false;
            }

        }
        function Conveyance_type()
        {
            var ddl_conveyance_type = document.getElementById('<%=ddl_conveyance_type.ClientID %>');
            var Selected_ddl_conveyance_type = ddl_conveyance_type.options[ddl_conveyance_type.selectedIndex].text;

            var txt_conveyance_km = document.getElementById('<%=txt_conveyance_km.ClientID %>');
            var txt_conveyance_rate = document.getElementById('<%=txt_conveyance_rate.ClientID %>');

            if (Selected_ddl_conveyance_type == "Select") {
                txt_conveyance_km.disabled = true;
                txt_conveyance_rate.disabled = true;
                txt_conveyance_rate.value = "0";
                txt_conveyance_km.value = "0";
            }
            else if (Selected_ddl_conveyance_type == "Fix") {
                txt_conveyance_km.disabled = true;
                txt_conveyance_rate.disabled = false;

                txt_conveyance_km.value = "0";
            }
            else if (Selected_ddl_conveyance_type == "EmployeeWise") {
                txt_conveyance_km.disabled = true;
                txt_conveyance_rate.disabled = true;
                txt_conveyance_rate.value = "0";
                txt_conveyance_km.value = "0";
            }
            else if (Selected_ddl_conveyance_type == "Km") {
                txt_conveyance_km.disabled = false;
                txt_conveyance_rate.disabled = false;
                //txt_conveyance_rate.value = "0";
                //txt_conveyance_km.value = "0";
            }

            else {
                txt_conveyance_rate.disabled = false;
                txt_conveyance_km.disabled = false;
            }
        }
        function Conveyance_Service_Charge() {
            var txt1 = document.getElementById('<%=txt_conveyance_service_charge.ClientID %>');
            var txt2 = document.getElementById('<%=txt_conveyance_service_amount.ClientID %>');

            if (txt1.value != "0") {
                txt2.value = "0";
                return;
            }
            Conveyance_Service_Charge_per();
        }
        function Conveyance_Service_Charge_Amount() {
            var txt1 = document.getElementById('<%=txt_conveyance_service_charge.ClientID %>');
            var txt2 = document.getElementById('<%=txt_conveyance_service_amount.ClientID %>');

            if (txt2.value != "0") {
                txt1.value = "0";
                return;
            }



        }
        function Conveyance_Service_Charge_per() {
            var txt1 = document.getElementById('<%=txt_conveyance_service_charge.ClientID %>');
            if (txt1.value < 0 || txt1.value > 100) {
                alert("Please Enter Less Than Hundred");
                txt1.value = "0";
                txt1.focus();
                return false;
            }

        }
        function b_taxable() {

            var ddl_bonus_taxable_billing = document.getElementById('<%=ddl_bonus_taxable_billing.ClientID %>');
            var Selected_ddl_bonus_taxable_billing = ddl_bonus_taxable_billing.options[ddl_bonus_taxable_billing.selectedIndex].text;

            var ddl_bonus_taxable_salary = document.getElementById('<%=ddl_bonus_taxable_salary.ClientID %>');
            var Selected_ddl_bonus_taxable_salary = ddl_bonus_taxable_salary.options[ddl_bonus_taxable_salary.selectedIndex].text;

            if (Selected_ddl_bonus_taxable_billing == "Not Applicable") {
                ddl_bonus_taxable_salary.disabled = true;
                Selected_ddl_bonus_taxable_salary = "Not Applicable";
            }

        }
        function g_taxable() {
            var ddl_gratuity_taxable_billing = document.getElementById('<%=ddl_gratuity_taxable_billing.ClientID %>');
            var Selected_ddl_gratuity_taxable_billing = ddl_gratuity_taxable_billing.options[ddl_gratuity_taxable_billing.selectedIndex].text;

            var ddl_gratuity_taxable_salary = document.getElementById('<%=ddl_gratuity_taxable_salary.ClientID %>');
             var Selected_ddl_gratuity_taxable_salary = ddl_gratuity_taxable_salary.options[ddl_gratuity_taxable_salary.selectedIndex].text;

            // alert("3");
            // alert(Selected_ddl_gratuity_taxable_billing);
             if (Selected_ddl_gratuity_taxable_billing == "Not Applicable") {
                 ddl_gratuity_taxable_salary.disabled = true;
                 Selected_ddl_gratuity_taxable_salary = "Not Applicable";
             }
         }
         function l_taxable() {
             var ddl_leave_taxable_billing = document.getElementById('<%=ddl_leave_taxable_billing.ClientID %>');
            var Selected_ddl_leave_taxable_billing = ddl_leave_taxable_billing.options[ddl_leave_taxable_billing.selectedIndex].text;

            var ddl_leave_taxable_salary = document.getElementById('<%=ddl_leave_taxable_salary.ClientID %>');
            var Selected_ddl_leave_taxable_salary = ddl_leave_taxable_salary.options[ddl_leave_taxable_salary.selectedIndex].text;

            if (Selected_ddl_leave_taxable_billing == "Not Applicable") {
                ddl_leave_taxable_salary.disabled = true;
                Selected_ddl_leave_taxable_salary = "Not Applicable";
            }
        }
        function Rental_cost() {
            var ddl_equmental_applicable = document.getElementById('<%=ddl_equmental_applicable.ClientID %>');
            var Selected_ddl_equmental_applicable = ddl_equmental_applicable.options[ddl_equmental_applicable.selectedIndex].text;

            var txt_equment_rate = document.getElementById('<%=txt_equment_rate.ClientID %>');
            var txt_equment_rental = document.getElementById('<%=txt_equment_rental.ClientID %>');
            var ddl_equmental_charges = document.getElementById('<%=ddl_equmental_charges.ClientID %>');
            var txt_equmental_percent = document.getElementById('<%=txt_equmental_percent.ClientID %>');

            if (Selected_ddl_equmental_applicable == "No") {
                txt_equment_rate.disabled = true;
                txt_equment_rental.disabled = true;
                ddl_equmental_charges.disabled = true;
                txt_equmental_percent.disabled = true;
            }
            else {
                txt_equment_rate.disabled = false;
                txt_equment_rental.disabled = false;
                ddl_equmental_charges.disabled = false;
                txt_equmental_percent.disabled = false;
            }
        }
        function consume_applicable() {
            var ddl_chemical_applicable = document.getElementById('<%=ddl_chemical_applicable.ClientID %>');
            var Selected_ddl_chemical_applicable = ddl_chemical_applicable.options[ddl_chemical_applicable.selectedIndex].text;

            var txt_chemical_unit = document.getElementById('<%=txt_chemical_unit.ClientID %>');
            var txt_chemical = document.getElementById('<%=txt_chemical.ClientID %>');
            var ddl_chemical_charges = document.getElementById('<%=ddl_chemical_charges.ClientID %>');
            var txt_chemical_percent = document.getElementById('<%=txt_chemical_percent.ClientID %>');

            if (Selected_ddl_chemical_applicable == "No") {
                txt_chemical_unit.disabled = true;
                txt_chemical.disabled = true;
                ddl_chemical_charges.disabled = true;
                txt_chemical_percent.disabled = true;
            }
            else {
                txt_chemical_unit.disabled = false;
                txt_chemical.disabled = false;
                ddl_chemical_charges.disabled = false;
                txt_chemical_percent.disabled = false;
            }
        }
        function linear_applicable() {
            var ddl_dustin_applicable = document.getElementById('<%=ddl_dustin_applicable.ClientID %>');
            var Selected_ddl_dustin_applicable = ddl_dustin_applicable.options[ddl_dustin_applicable.selectedIndex].text;

            var txt_dustin_rate = document.getElementById('<%=txt_dustin_rate.ClientID %>');
            var txt_dustin = document.getElementById('<%=txt_dustin.ClientID %>');
            var ddl_dustin_charges = document.getElementById('<%=ddl_dustin_charges.ClientID %>');
            var txt_dustin_percent = document.getElementById('<%=txt_dustin_percent.ClientID %>');

            if (Selected_ddl_dustin_applicable == "No") {
                txt_dustin_rate.disabled = true;
                txt_dustin.disabled = true;
                ddl_dustin_charges.disabled = true;
                txt_dustin_percent.disabled = true;
            }
            else {
                txt_dustin_rate.disabled = false;
                txt_dustin.disabled = false;
                ddl_dustin_charges.disabled = false;
                txt_dustin_percent.disabled = false;
            }
        }
        function bin_applicable() {
            var ddl_femina_applicable = document.getElementById('<%=ddl_femina_applicable.ClientID %>');
            var Selected_ddl_femina_applicable = ddl_femina_applicable.options[ddl_femina_applicable.selectedIndex].text;

            var txt_femina_unit = document.getElementById('<%=txt_femina_unit.ClientID %>');
            var txt_femina = document.getElementById('<%=txt_femina.ClientID %>');
            var ddl_femina_charges = document.getElementById('<%=ddl_femina_charges.ClientID %>');
            var txt_femina_percent = document.getElementById('<%=txt_femina_percent.ClientID %>');

            if (Selected_ddl_femina_applicable == "No") {
                txt_femina_unit.disabled = true;
                txt_femina.disabled = true;
                ddl_femina_charges.disabled = true;
                txt_femina_percent.disabled = true;
            }
            else {
                txt_femina_unit.disabled = false;
                txt_femina.disabled = false;
                ddl_femina_charges.disabled = false;
                txt_femina_percent.disabled = false;
            }
        }
        function machine_applicable() {


            var ddl_aerosol_applicable = document.getElementById('<%=ddl_aerosol_applicable.ClientID %>');
            var Selected_ddl_aerosol_applicable = ddl_aerosol_applicable.options[ddl_aerosol_applicable.selectedIndex].text;

            var txt_aerosol_rate = document.getElementById('<%=txt_aerosol_rate.ClientID %>');
            var txt_aerosol = document.getElementById('<%=txt_aerosol.ClientID %>');
            var ddl_aerosol_charges = document.getElementById('<%=ddl_aerosol_charges.ClientID %>');
            var txt_aerosol_percent = document.getElementById('<%=txt_aerosol_percent.ClientID %>');

            if (Selected_ddl_aerosol_applicable == "No") {
                txt_aerosol_rate.disabled = true;
                txt_aerosol.disabled = true;
                ddl_aerosol_charges.disabled = true;
                txt_aerosol_percent.disabled = true;
            }
            else {
                txt_aerosol_rate.disabled = false;
                txt_aerosol.disabled = false;
                ddl_aerosol_charges.disabled = false;
                txt_aerosol_percent.disabled = false;
            }
        }
        function tackle_applicable() {


            var ddl_tool_applicable = document.getElementById('<%=ddl_tool_applicable.ClientID %>');
            var Selected_ddl_tool_applicable = ddl_tool_applicable.options[ddl_tool_applicable.selectedIndex].text;

            var txt_tool_unit = document.getElementById('<%=txt_tool_unit.ClientID %>');
            var txt_tool = document.getElementById('<%=txt_tool.ClientID %>');
            var ddl_tool_charges = document.getElementById('<%=ddl_tool_charges.ClientID %>');
            var txt_tool_percent = document.getElementById('<%=txt_tool_percent.ClientID %>');

            if (Selected_ddl_tool_applicable == "No") {
                txt_tool_unit.disabled = true;
                txt_tool.disabled = true;
                ddl_tool_charges.disabled = true;
                txt_tool_percent.disabled = true;
            }
            else {
                txt_tool_unit.disabled = false;
                txt_tool.disabled = false;
                ddl_tool_charges.disabled = false;
                txt_tool_percent.disabled = false;
            }


        }

        function common_leave_applicable() {
            var ddl_leave_app = document.getElementById('<%=ddl_leave_app.ClientID %>');
            var Selected_ddl_leave_app = ddl_leave_app.options[ddl_leave_app.selectedIndex].text;

            var txt_leave_days_percent = document.getElementById('<%=txt_leave_days_percent.ClientID %>');
            var txt_leave_days = document.getElementById('<%=txt_leave_days.ClientID %>');
            var ddl_leave_taxable_billing = document.getElementById('<%=ddl_leave_taxable_billing.ClientID %>');
            var Selected_ddl_leave_taxable_billing = ddl_leave_taxable_billing.options[ddl_leave_taxable_billing.selectedIndex].text;

            var ddl_leave_taxable_salary = document.getElementById('<%=ddl_leave_taxable_salary.ClientID %>');
            var Selected_ddl_leave_taxable_salary = ddl_leave_taxable_salary.options[ddl_leave_taxable_salary.selectedIndex].text;
            var txt_leave_days_salary = document.getElementById('<%=txt_leave_days_salary.ClientID %>');
            var txt_leave_days_percent_salary = document.getElementById('<%=txt_leave_days_percent_salary.ClientID %>');

            if (Selected_ddl_leave_app == "No") {
                //ddl_leave_taxable_billing.options[0].selected = true;
                ddl_leave_taxable_billing.disabled = true;
                txt_leave_days.disabled = true;
                txt_leave_days_percent.disabled = true;

                //ddl_leave_taxable_salary.options[0].selected = true;
                ddl_leave_taxable_salary.disabled = true;
                txt_leave_days_salary.disabled = true;
                txt_leave_days_percent_salary.disabled = true;
            }
            else if (Selected_ddl_leave_app == "Yes") {
                //ddl_leave_taxable_billing.options[1].selected = true;
                ddl_leave_taxable_billing.disabled = false;
                txt_leave_days.disabled = false;
                txt_leave_days_percent.disabled = false;

                //ddl_leave_taxable_salary.options[1].selected = true;
                ddl_leave_taxable_salary.disabled = false;
                txt_leave_days_salary.disabled = false;
                txt_leave_days_percent_salary.disabled = false;
            }


        }

        function save_validate2() {

            var txt_material_name = document.getElementById('<%=txt_material_name.ClientID %>');

            if (txt_material_name.value == "") {
                alert("Please Enter Material name");
                txt_material_name.focus();
                return false;
            }


            var txt_contract_rate = document.getElementById('<%=txt_contract_rate.ClientID %>');
            if (txt_contract_rate.value == "0" || txt_contract_rate.value == "") {
                alert("Please Enter Material Contract Rate");
                txt_contract_rate.focus();
                return false;
            }

            var ddl_handling_charge = document.getElementById('<%=ddl_handling_charge.ClientID %>');
            var Selected_ddl_handling_charge = ddl_handling_charge.options[ddl_handling_charge.selectedIndex].text;
            var txt_handling_amount = document.getElementById('<%=txt_handling_amount.ClientID %>');
                var txt_handling_percent = document.getElementById('<%=txt_handling_percent.ClientID %>');
            if (Selected_ddl_handling_charge == "Yes") {
                if ((txt_handling_percent.value == "0" || txt_handling_percent.value == "") && (txt_handling_amount.value == "0" || txt_handling_amount.value == "")) {
                    alert("Please Enter Material Contract Handling Charges Either Percentage");
                    txt_handling_percent.focus();
                    return false;
                }
            }

        }

        function contract_type() {
            var ddl_material_contract = document.getElementById('<%=ddl_material_contract.ClientID %>');
            var Selected_ddl_material_contract = ddl_material_contract.options[ddl_material_contract.selectedIndex].text;

            var ddl_cotract_type = document.getElementById('<%=ddl_cotract_type.ClientID %>');

            if (Selected_ddl_material_contract == "No") {
                ddl_cotract_type.innerHTML = "No";
                $(".material_tab").show();
            }

            if (Selected_ddl_material_contract == "Fix") {
                ddl_cotract_type.innerHTML = "Fix";
                $(".hide_textbox").show();
                $(".material_tab").show();
              
            }
            if (Selected_ddl_material_contract == "Sqr.Ft") {
                ddl_cotract_type.innerHTML = "Sqr.Ft";
                $(".material_tab").show();
            }
            if (Selected_ddl_material_contract == "Employeewise") {
                ddl_cotract_type.innerHTML = "Employeewise";
                $(".material_tab").show();
            }
            if (Selected_ddl_material_contract == "Fix material") {
                ddl_cotract_type.innerHTML = "Fix material";
                $(".material_tab").hide();
                $(".material_name").show();
                $(".bill_details_add").show();
                $(".add_material").show();
                $(".gv_material").show();
                $(".hide_textbox").hide();
                
            }
        
            else {
                $(".material_name").hide();
                $(".bill_details_add").hide();
                $(".add_material").hide();
                $(".gv_material").hide();
            }

            var ddl_material_contract = document.getElementById('<%=ddl_material_contract.ClientID %>');
            var Selected_ddl_material_contract = ddl_material_contract.options[ddl_material_contract.selectedIndex].text;

            var txt_contract_rate = document.getElementById('<%=txt_contract_rate.ClientID %>');
            if (Selected_ddl_material_contract == "Employeewise") {
                txt_contract_rate.value = "0";

                txt_contract_rate.disabled = true;
               
            }
            else {
                txt_contract_rate.disabled = false;
                
            }
            material_policy();
            return true;
        }
        function h_charges_p() {
            var ddl_handling_charge = document.getElementById('<%=ddl_handling_charge.ClientID %>');
            var Selected_ddl_handling_charge = ddl_handling_charge.options[ddl_handling_charge.selectedIndex].text;

            var txt_handling_percent = document.getElementById('<%=txt_handling_percent.ClientID %>');
            var txt_handling_amount = document.getElementById('<%=txt_handling_amount.ClientID %>');

            if (Selected_ddl_handling_charge == "Yes") {
                if (txt_handling_percent.value != "0" || txt_handling_percent.value != "") {
                    txt_handling_amount.value = "0";
                }
            }

        }
        function h_charges_a() {
            var ddl_handling_charge = document.getElementById('<%=ddl_handling_charge.ClientID %>');
            var Selected_ddl_handling_charge = ddl_handling_charge.options[ddl_handling_charge.selectedIndex].text;

            var txt_handling_percent = document.getElementById('<%=txt_handling_percent.ClientID %>');
            var txt_handling_amount = document.getElementById('<%=txt_handling_amount.ClientID %>');

            if (Selected_ddl_handling_charge == "Yes") {

                if (txt_handling_amount.value != "0" || txt_handling_amount.value != "") {
                    txt_handling_percent.value = "0";
                }
            }

        }
        function isNumber120_dot(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 46 || charCode > 46)) {
                    return false;
                }
            }
            h_charges_p();
            return true;

        }
        function isNumber121_dot(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 46 || charCode > 46)) {
                    return false;
                }
            }
            h_charges_a();
            return true;

        }
        function machine_rental_applicable() {
            var ddl_machine_rental_app = document.getElementById('<%=ddl_machine_rental_app.ClientID %>');
            var Selected_ddl_machine_rental_app = ddl_machine_rental_app.options[ddl_machine_rental_app.selectedIndex].text;

            var ddl_machine = document.getElementById('<%=ddl_machine.ClientID %>');
            var Selected_ddl_machine = ddl_machine.options[ddl_machine.selectedIndex].text;

            var ddl_rate_type = document.getElementById('<%=ddl_rate_type.ClientID %>');
            var Selected_ddl_rate_type = ddl_rate_type.options[ddl_rate_type.selectedIndex].text;

            var txt_machine_amount = document.getElementById('<%=txt_machine_amount.ClientID %>');

            var ddl_h_c_applicable = document.getElementById('<%=ddl_h_c_applicable.ClientID %>');
            var Selected_ddl_h_c_applicable = ddl_h_c_applicable.options[ddl_h_c_applicable.selectedIndex].text;

            if (Selected_ddl_machine_rental_app == "No") {
                ddl_machine.disabled = true;
                Selected_ddl_machine = "Select";
                ddl_rate_type.disabled = true;
                Selected_ddl_rate_type = "Select";
                txt_machine_amount.disabled = true;
                txt_machine_amount.value = "0";
                ddl_h_c_applicable.disabled = true;
                Selected_ddl_h_c_applicable = "No";
                $(".add_rental").hide();
            }
            else {
                ddl_machine.disabled = false;
                ddl_rate_type.disabled = false;
                txt_machine_amount.disabled = false;
                ddl_h_c_applicable.disabled = false;
                $(".add_rental").show();
            }
        }
        function Machine_rent_in() {
            var txt_in_per = document.getElementById('<%=txt_in_per.ClientID %>');
            var txt_in_amt = document.getElementById('<%=txt_in_amt.ClientID %>');
            if (txt_in_per != "0") {
                txt_in_amt.value = "0";
            }
        }
        function Machine_rent_amount() {
            var txt_in_per = document.getElementById('<%=txt_in_per.ClientID %>');
            var txt_in_amt = document.getElementById('<%=txt_in_amt.ClientID %>');
            if (txt_in_amt != "0") {
                txt_in_per.value = "0";
            }
        }
        function machine_click_val() {
            var ddl_machine = document.getElementById('<%=ddl_machine.ClientID %>');
            var Selected_ddl_machine = ddl_machine.options[ddl_machine.selectedIndex].text;

            if (Selected_ddl_machine == "Select") {
                alert("Please Select Machine Name");
                ddl_machine.focus();
                return false;
            }

            var ddl_rate_type = document.getElementById('<%=ddl_rate_type.ClientID %>');
            var Selected_ddl_rate_type = ddl_rate_type.options[ddl_rate_type.selectedIndex].text;

            if (Selected_ddl_rate_type == "Select") {
                alert("Please Select Rate Type");
                ddl_rate_type.focus();
                return false;
            }

            var txt_machine_amount = document.getElementById('<%=txt_machine_amount.ClientID %>');

            if (txt_machine_amount.value == "" || txt_machine_amount.value == "0") {
                alert("Please Enter Amount");
                txt_machine_amount.focus();
                txt_machine_amount.value = "0";
                return false;
            }

            var ddl_h_c_applicable = document.getElementById('<%=ddl_h_c_applicable.ClientID %>');
            var Selected_ddl_h_c_applicable = ddl_h_c_applicable.options[ddl_h_c_applicable.selectedIndex].text;
            var txt_in_per = document.getElementById('<%=txt_in_per.ClientID %>');
            var txt_in_amt = document.getElementById('<%=txt_in_amt.ClientID %>');

            if (Selected_ddl_h_c_applicable == "Yes") {
                if (txt_in_per.value == "0" && txt_in_amt.value == "0") {
                    alert("Please Enter Percentage / Amount");
                    txt_in_per.focus();
                    return false;
                }
            }
            //$.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            //return true;
        }
        $(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            //if (st == null)
            //    st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });
        function deep_h_charge() {
            var ddl_dc_handling_charge = document.getElementById('<%=ddl_dc_handling_charge.ClientID %>');
            var Selected_ddl_dc_handling_charge = ddl_dc_handling_charge.options[ddl_dc_handling_charge.selectedIndex].text;

            var txt_dc_handling_percent = document.getElementById('<%=txt_dc_handling_percent.ClientID %>');

            if (Selected_ddl_dc_handling_charge == "No") {
                txt_dc_handling_percent.value = "0";
                txt_dc_handling_percent.disabled = true;
            }
            else { txt_dc_handling_percent.disabled = false; }

            var ddl_pc_handling_charge = document.getElementById('<%=ddl_pc_handling_charge.ClientID %>');
            var Selected_ddl_pc_handling_charge = ddl_pc_handling_charge.options[ddl_pc_handling_charge.selectedIndex].text;

            var txt_pc_handling_percent = document.getElementById('<%=txt_pc_handling_percent.ClientID %>');

            if (Selected_ddl_pc_handling_charge == "No") {
                txt_pc_handling_percent.value = "0";
                txt_pc_handling_percent.disabled = true;
            }
            else { txt_pc_handling_percent.disabled = false; }
        }
        function policy_check() {
            var ddl_policy = document.getElementById('<%=ddl_policy.ClientID %>');
            var Selected_ddl_policy = ddl_policy.options[ddl_policy.selectedIndex].text;
            if (Selected_ddl_policy == "Branchwise Policy") {
                $(".policy").show();

            }

            else { $(".policy").hide(); }

        }
        function openWindow() {

            window.open("html/Policymaster.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');

        }


        function change_dropdown_r_m() {

            var ddl_service_charge = document.getElementById('<%=ddl_service_charge.ClientID %>');
            var Selected_ddl_service_charge = ddl_service_charge.options[ddl_service_charge.selectedIndex].text;

            var txt_service_charge_rate = document.getElementById('<%=txt_service_charge_rate.ClientID %>');
            var txt_service_charge = document.getElementById('<%=txt_service_charge.ClientID %>');
            if (Selected_ddl_service_charge == "Not Applicable") {
                txt_service_charge_rate.disabled = true;
                txt_service_charge.disabled = true;
            }
            if (Selected_ddl_service_charge == "Applicable") {
                txt_service_charge_rate.disabled = false;
                txt_service_charge.disabled = false;
               // txt_service_charge_rate.value = "0";
                //txt_service_charge.value = "0";
            }
        }
        function change_dropdown_administrative() {

            var ddl_service_charge_adm = document.getElementById('<%=ddl_service_charge_adm.ClientID %>');
            var Selected_ddl_service_charge_adm = ddl_service_charge_adm.options[ddl_service_charge_adm.selectedIndex].text;

            var txt_rate_adm = document.getElementById('<%=txt_rate_adm.ClientID %>');
            var txt_ser_rate_adm = document.getElementById('<%=txt_ser_rate_adm.ClientID %>');
            if (Selected_ddl_service_charge_adm == "Not Applicable") {
                txt_rate_adm.disabled = true;
                txt_ser_rate_adm.disabled = true;
            }
            if (Selected_ddl_service_charge_adm == "Applicable") {
                txt_rate_adm.disabled = false;
                txt_ser_rate_adm.disabled = false;
              //  txt_rate_adm.value = "0";
              //  txt_ser_rate_adm.value = "0";
            }
        }
        function ser_amount_r_m() {
            var txt1 = document.getElementById('<%=txt_service_charge_rate.ClientID %>');
            var txt2 = document.getElementById('<%=txt_service_charge.ClientID %>');
            if (txt1.value != "0") {
                txt2.value = "0";
                return;
            }

        }
        function ser_amount_per_r_m() {
            var txt1 = document.getElementById('<%=txt_service_charge_rate.ClientID %>');
             var txt2 = document.getElementById('<%=txt_service_charge.ClientID %>');
             if (txt2.value != "0") {
                 txt1.value = "0";
                 return;
             }
         }
         function isNumber_r_m(evt) {
             if (null != evt) {
                 evt = (evt) ? evt : window.event;
                 var charCode = (evt.which) ? evt.which : evt.keyCode;
                 if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                     return false;
                 }
             }
             ser_amount_r_m();
             return true;
         }
         function isNumber_dot_r_m(evt) {
             if (null != evt) {
                 evt = (evt) ? evt : window.event;
                 var charCode = (evt.which) ? evt.which : evt.keyCode;
                 if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 46 || charCode > 46)) {
                     return false;
                 }
             }
             ser_amount_per_r_m()

             return true;
         }
         function amount_adm() {
             var txt1 = document.getElementById('<%=txt_rate_adm.ClientID %>');
            var txt2 = document.getElementById('<%=txt_ser_rate_adm.ClientID %>');
            if (txt1.value != "0") {
                txt2.value = "0";
                return;
            }

        }
        function amount_per_adm() {
            var txt1 = document.getElementById('<%=txt_rate_adm.ClientID %>');
            var txt2 = document.getElementById('<%=txt_ser_rate_adm.ClientID %>');
            if (txt2.value != "0") {
                txt1.value = "0";
                return;
            }
        }
        function isNumber_adm(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            amount_adm();
            return true;
        }
        function isNumber_dot_adm(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 46 || charCode > 46)) {
                    return false;
                }
            }
            amount_per_adm()

            return true;
        }

    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Billing & Salary Master</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="panel-body">

                <asp:Panel ID="panel1" runat="server" CssClass="panel panel-primary" Style="background: beige; border-color: #c4c0c0">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-2 col-xs-12 text-left">
                               <b> Client Name :<span class="text-red"> *</span></b>

                                <asp:DropDownList ID="ddl_unit_client" class="form-control  pr_state js-example-basic-single text_box" Width="100%" runat="server" OnSelectedIndexChanged="ddl_clientname_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                               <div class="col-sm-2 col-xs-12">
                                <b>Policy:</b><span class="text-red"> *</span>

                                    <asp:DropDownList ID="ddl_policy" runat="server"   class="form-control pr_state js-example-basic-single text_box" onchange="return policy_check()">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Statewise Policy</asp:ListItem>
                                        <asp:ListItem Value="2">Branchwise Policy</asp:ListItem>
                                              
                                    </asp:DropDownList>
                            </div>
                         
						   
						    <div class="col-sm-2 col-xs-12">
                                <b>State :</b><span class="text-red"> *</span>

                                <asp:DropDownList ID="ddl_clientwisestate" runat="server" class="form-control pr_state js-example-basic-single text_box" OnSelectedIndexChanged="ddl_clientwisestate_SelectedIndexChanged" AutoPostBack="true" Width="100%">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-sm-3 col-xs-12  text-left policy"  style="display:none">
                                <b>Branch Having Policy :</b> <span class="text-red">*</span>
                                <asp:ListBox ID="ddl_unitcode" runat="server" class="form-control pr_state js-example-basic-single text_box" SelectionMode="Multiple" OnSelectedIndexChanged="ddl_unitcode_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                            </div>
                            <div class="col-sm-3 col-xs-12 text-left policy"  style="display:none">
                                <b>Branch Not Having Policy :</b> <span class="text-red">*</span>
                                <asp:ListBox ID="ddl_unitcode_without" runat="server" class="form-control pr_state js-example-basic-single text_box" SelectionMode="Multiple" OnSelectedIndexChanged="ddl_unitcode_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                                <b>New Policy Name:</b> <span class="text-red">*</span>
                                <asp:TextBox ID="txt_policy_name1" runat="server" class="form-control text_box"
                                    placeholder=" New Policy Name : " onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>Start Date:</b> <span class="text-red">*</span>
                                <asp:TextBox ID="txt_start_date" runat="server" class="form-control  text_box" placeholder="Start Date :" Width="100%" Style="display: inline"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>End Date:</b> <span class="text-red">*</span>
                                <asp:TextBox ID="txt_end_date" runat="server" placeholder="End Date" class="form-control  text_box" Width="100%" Style="display: inline"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                    </div>
                </asp:Panel>

                <br />
                <div id="tabs" style="background: beige;">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li><a href="#home">Common</a></li>
                        <li><a href="#menu1">Billing</a></li>
                        <li><a href="#menu2">Salary</a></li>
                        <li><a href="#menu3">Material</a></li>
                        <li><a href="#menu4">Deep Cleaning</a></li>
                        <li><a href="#menu5">Conveyance</a></li>
                        <li><a href="#menu6">Pest Control</a></li>
                        <li><a href="#menu7">Machine Rent Policy</a></li>
                         <li><a href="#menu8">R&M Service</a></li>
                           <li><a href="#menu9">Administrative Expenses</a></li>
                        <%--//vikas 19/06/2019--%>
                    </ul>
                    <div id="home">
                        <br />
                        <div class="row">
                              <div class="col-sm-2 col-xs-12">
                                <b>Categories :</b><span class="text-red"> *</span>

                                <asp:DropDownList ID="ddl_category"  OnSelectedIndexChanged="ddl_category_SelectedIndexChanged"  runat="server" class="form-control text_box " AutoPostBack="true" Width="100%">
                                   
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Designation :</b><span class="text-red"> *</span>
                                <asp:ListBox ID="ddl_designation" runat="server" DataTextField="txt_policy_name" DataValueField="id" class="form-control pr_state js-example-basic-single text_box" SelectionMode="Multiple" OnSelectedIndexChanged="ddl_designation_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                <%--<asp:DropDownList ID="ddl_designation" runat="server" class="form-control text_box" DataTextField="txt_policy_name" DataValueField="id">
                                </asp:DropDownList>--%>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>Duty Hours :</b><span class="text-red"> *</span>
                                <asp:DropDownList ID="ddl_hours" runat="server" CssClass="form-control text_box" Width="100%" onchange="return billing_hours();">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>Month Calculation :</b><span class="text-red"> *</span>
                                <asp:DropDownList ID="ddl_month_calc" runat="server" CssClass="form-control text_box" Width="100%">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">CALENDER DAYS</asp:ListItem>
                                    <asp:ListItem Value="2">WORKING DAYS</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>Billing Start Day:</b> <span class="text-red">*</span>
                                <asp:DropDownList ID="txt_start_date_common" Enabled="false" runat="server" CssClass="form-control text_box " Width="100%">
                                    <asp:ListItem Value="0">0</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>
                                    <asp:ListItem Value="8">8</asp:ListItem>
                                    <asp:ListItem Value="9">9</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                    <asp:ListItem Value="13">13</asp:ListItem>
                                    <asp:ListItem Value="14">14</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="16">16</asp:ListItem>
                                    <asp:ListItem Value="17">17</asp:ListItem>
                                    <asp:ListItem Value="18">18</asp:ListItem>
                                    <asp:ListItem Value="19">19</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="21">21</asp:ListItem>
                                    <asp:ListItem Value="22">22</asp:ListItem>
                                    <asp:ListItem Value="23">23</asp:ListItem>
                                    <asp:ListItem Value="24">24</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="26">26</asp:ListItem>
                                    <asp:ListItem Value="27">27</asp:ListItem>
                                    <asp:ListItem Value="28">28</asp:ListItem>
                                    <asp:ListItem Value="29">29</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="31">31</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>Billing End Day:</b> <span class="text-red">*</span>
                                <asp:TextBox ID="txt_end_date_common" runat="server" ReadOnly="true" placeholder="End Date" class="form-control text_box" Width="100%"></asp:TextBox>
                            </div>
                            

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Allowance :</b>
                                 <asp:TextBox ID="txt_other_allow" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>ESIC on Allowance Applicable:</b>     
                                  <asp:DropDownList ID="ddl_esic_allow" runat="server" CssClass="form-control text_box" Width="100%">
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>OverTime (OT) Allowed:</b>     
                                 <asp:DropDownList ID="ddl_ot_applicable" runat="server" CssClass="form-control text_box" Width="100%" onchange="return ot_allowed();">
                                     <asp:ListItem Value="0">No</asp:ListItem>
                                     <asp:ListItem Value="1">Yes</asp:ListItem>
                                 </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>ESIC on OT Applicable:</b>     
                                  <asp:DropDownList ID="ddl_esic_ot" runat="server" CssClass="form-control text_box" Width="100%">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                  </asp:DropDownList>
                            </div>

                            <div class="col-md-2 col-xs-12">
                                <b>LWF Applicable:</b>     
                                  <asp:DropDownList ID="ddl_lwf_applicable" runat="server" CssClass="form-control text_box" Width="100%">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>GST Applicable:</b>     
                                  <asp:DropDownList ID="ddl_gst_applicable" runat="server" CssClass="form-control text_box" Width="100%">
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                          
                        </div>
                        <br />
                        <div class="row">

                              <div class="col-md-2 col-xs-12">
                                <b>PF ON :</b>     
                                  <asp:DropDownList ID="ddl_cmn_pf_app" runat="server" CssClass="form-control text_box" Width="100%">
                                      <asp:ListItem Value="0">Basic / VDA</asp:ListItem>
                                      <asp:ListItem Value="1">Gross</asp:ListItem>
                                      <asp:ListItem Value="2">Gross-HRA</asp:ListItem>
                                      <asp:ListItem Value="3">Basic+CCA+Allowance</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Material Contract :</b>     
                                  <asp:DropDownList ID="ddl_material_contract" runat="server" CssClass="form-control text_box" onchange="return contract_type();" Width="100%">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Fix</asp:ListItem>
                                      <asp:ListItem Value="2">Sqr.Ft</asp:ListItem>
                                      <asp:ListItem Value="3">Fix material</asp:ListItem>
                                       <asp:ListItem Value="4">Employeewise</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Deep Cleaning Applicable :</b>     
                                  <asp:DropDownList ID="ddl_dc_contract" runat="server" CssClass="form-control text_box" Width="100%" onchange="deep_clean_applicable();">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>

                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Conveyance Applicable :</b>     
                                  <asp:DropDownList ID="ddl_conveyance_applicable" runat="server" CssClass="form-control text_box" Width="100%" onchange="return ddl_Conveyance_Applicable();">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Pest Control Applicable :</b>     
                                  <asp:DropDownList ID="ddl_pc_contract" runat="server" CssClass="form-control text_box" Width="100%" onchange="deep_clean_applicable();">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>

                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Machine Rental Applicable :</b>     
                                  <asp:DropDownList ID="ddl_machine_rental_app" runat="server" CssClass="form-control text_box" Width="100%" onchange="machine_rental_applicable();">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>

                                  </asp:DropDownList>
                            </div>
                           

                        </div>
                        <br />
                        <div class="row">
                             <div class="col-md-2 col-xs-12">
                                <b>Bonus Applicable :</b> <span style="color: red">*</span>
                                <asp:DropDownList ID="ddl_bonus_app" runat="server" CssClass="form-control text_box" Width="100%" onchange="common_bonus_applicable();">
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Leave Applicable :</b> <span style="color: red">*</span>
                                <asp:DropDownList ID="ddl_leave_app" runat="server" CssClass="form-control text_box" Width="100%" onchange="common_leave_applicable();">
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                             <div class="col-md-2 col-xs-12">
                                <b>Travel Allowance :</b> <span style="color: red">*</span>
                                 <asp:TextBox ID="txt_conveyance_amount" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            
                        </div>
                    </div>
                    <div id="menu1">
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Per Day Rate :</b>
                                     <asp:TextBox ID="txt_per_rate_billing" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Basic :</b>
                                      <asp:TextBox ID="txt_basic_billing" runat="server" MaxLength="15" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);" onchange="add_number();"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>VDA :</b>
                                 <asp:TextBox ID="txt_vda_billing" runat="server" MaxLength="15" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);" onchange="add_number();"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Basic + VDA :</b>
                                 <asp:TextBox ID="txt_basic_vda_billing" runat="server" ReadOnly="true" class="form-control maskedExt text_box" maskedFormat="10,2" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                               <b> HRA Amount :</b>
                                 <asp:TextBox ID="txt_hra_amount" runat="server" Width="100%" class="form-control pr_add text_box" Text="0" onchange="add_number();" onkeypress="return isNumber0(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                               <b> HRA Percent (%):</b> 
                                 <asp:TextBox ID="txt_hra_percent" runat="server" MaxLength="5" Width="100%" class="form-control pr_add text_box" Text="0" onchange="add_number();" onkeypress="return isNumber0_dot(event);"></asp:TextBox>
                            </div>


                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-xs-12">
                                <b>Bonus Policy Applicable:</b>     
                                  <asp:DropDownList ID="ddl_bonus_policy_aap_billing" runat="server" CssClass="form-control text_box" Width="100%" onchange="return change_txt_box();">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>

                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Bonus Amount : </b>    
                                 <asp:TextBox ID="txt_bonus_amount_billing" runat="server" onfocus="return onfocus();" Text="0" MaxLength="5" class="form-control pr_add text_box" onchange="bill_service_charge();" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Bonus Percent (%) :</b>    
                                 <asp:TextBox ID="txt_bill_bonus_percent" Text="8.33" runat="server" MaxLength="5" class="form-control pr_add text_box" onchange="bill_bonus_percent();" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Bonus Taxable:</b>     
                                  <asp:DropDownList ID="ddl_bonus_taxable_billing" runat="server" CssClass="form-control text_box" Width="100%" onchange="return bonus_taxable();">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                      <asp:ListItem Value="2">Not Applicable</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Gratuity Percent (%):</b>
                                 <asp:TextBox ID="txt_gratuity_percent_billing" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="4.81" onchange="bill_pf_percent();" onkeypress="return isNumber_dot(event);">4.81</asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Gratuity for ESIC Taxable:</b>     
                                  <asp:DropDownList ID="ddl_gratuity_taxable_billing" runat="server" CssClass="form-control text_box" Width="100%" onchange="return gratuaty_taxable();">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                      <asp:ListItem Value="2">Not Applicable</asp:ListItem>
                                  </asp:DropDownList>
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Washing :</b>
                                 <asp:TextBox ID="txt_washing_billing" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>

                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Travelling :</b>
                                 <asp:TextBox ID="txt_travelling_billing" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Education :</b>
                                 <asp:TextBox ID="txt_education_billing" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>CCA :</b>
                                 <asp:TextBox ID="txt_cca_billing" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Other Allowances :</b>
                                 <asp:TextBox ID="txt_allowances_billing" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>

                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>National Holidays Count:</b>
                                 <asp:TextBox ID="txt_national_holidays_billing" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="4" onkeypress="return isNumber1_dot(event);" onchange="leav_days_per();">4</asp:TextBox>
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Leave Days Percent(%) :</b>
                                 <asp:TextBox ID="txt_leave_days_percent" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber1_dot(event);" onchange="leav_days_per_billing();"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                               <b> Leave Days :</b>
                                 <asp:TextBox ID="txt_leave_days" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber1_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Leave Taxable: </b>    
                                  <asp:DropDownList ID="ddl_leave_taxable_billing" runat="server" CssClass="form-control text_box" Width="100%" onchange="return leave_taxable();">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                      <asp:ListItem Value="2">Not Applicable</asp:ListItem>
                                  </asp:DropDownList>
                            </div>

                            <div class="col-md-2 col-xs-12">
                                <b>PF Percent (%) :  </b>   
                                 <asp:TextBox ID="txt_bill_pf_percent" runat="server" MaxLength="5" class="form-control pr_add text_box" onchange="bill_pf_percent();" onkeypress="return isNumber_dot(event);">0</asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                               <b> ESIC Percent (%) :</b>     
                                 <asp:TextBox ID="txt_bill_esic_percent" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="3.25" onchange="bill_esic_percent();" onkeypress="return isNumber_dot(event);">3.25</asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                               <b> Relieving Charges Percent (%): </b>     
                                 <asp:TextBox ID="txt_bill_relieving" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="0" onchange="bill_relieving();" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>


                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-xs-12">
                                <b>Uniform Rate : </b>    
                                 <asp:TextBox ID="txt_bill_uniform_rate" runat="server" class="form-control pr_add text_box" Text="250" onkeypress="return isNumber2(event);">250</asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                               <b> Uniform Percent (%) :  </b>   
                                 <asp:TextBox ID="txt_bill_uniform_percent" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="0" onchange=" unifor_rate_per();" onkeypress="return isNumber2_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Operational Cost Amount :</b>     
                                 <asp:TextBox ID="txt_bill_oper_cost_amt" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber3(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Operational Cost Percent (%) :</b>     
                                 <asp:TextBox ID="txt_bill_oper_cost_percent" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="0" onchange="ope_cost_per();" onkeypress="return isNumber3_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Relieving Charge on Uniform:</b>     
                                  <asp:DropDownList ID="ddl_relieving_uniform" runat="server" CssClass="form-control text_box" Width="100%" onkeypress="return isNumber(event);">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Service Charge on Uniform: </b>    
                                  <asp:DropDownList ID="ddl_bill_ser_uniform" runat="server" CssClass="form-control text_box" Width="100%" onkeypress="return isNumber(event);" onchange="return uniform_dropdown()">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                  </asp:DropDownList>
                            </div>



                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-xs-12">
                                <b>Service Charge on Operational Cost:</b>     
                                  <asp:DropDownList ID="ddl_bill_ser_operations" runat="server" CssClass="form-control text_box" Width="100%" onkeypress="return isNumber(event);">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Service Charge (%) :</b>     
                                 <asp:TextBox ID="txt_bill_service_charge" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="7" onchange="service_charge();" onkeypress="return isNumber30(event);">7</asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Service Charge Amount : </b>    
                                 <asp:TextBox ID="txt_bill_service_charge_amount" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="7" onchange="bill_service_charge();" onkeypress="return isNumber30_dot(event);">0</asp:TextBox>
                            </div>

                            <div class="col-md-2 col-xs-12">
                                <b>Special Allowances Applicable(Only 12Hrs):</b>     
                                  <asp:DropDownList ID="ddl_ot_policy_billing" runat="server" CssClass="form-control text_box" Width="100%" onchange="return ot_policy_billing();">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Special Allowances :</b>      
                                 <asp:TextBox ID="txt_ot_amount_billing" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber4_dot(event);">0</asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>ESIC On Other Allowances:</b>     
                                  <asp:DropDownList ID="ddl_esic_oa_billing" runat="server" CssClass="form-control text_box" Width="100%" onkeypress="return isNumber(event);">
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                      <asp:ListItem Value="0">No</asp:ListItem>

                                  </asp:DropDownList>
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-xs-12">
                                <b>Group Insurance : </b>     
                                 <asp:TextBox ID="txt_group_insurance" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber4_dot(event);">0</asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12" style="width: 20%">
                                <b>Service Charge On Group Insurance: </b>    
                                  <asp:DropDownList ID="ddl_service_group_insurance" runat="server" CssClass="form-control text_box" Width="100%">
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                      <asp:ListItem Value="0">No</asp:ListItem>

                                  </asp:DropDownList>
                            </div>

                            <div class="col-md-2 col-xs-12">
                               <b> LWF Actual / Monthly:</b>     
                                  <asp:DropDownList ID="ddl_lwf_act_man" runat="server" CssClass="form-control text_box" Width="100%">
                                      <asp:ListItem Value="0">Monthly</asp:ListItem>
                                      <asp:ListItem Value="1">Actual</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                        </div>
                        <br />

                        <br />
                    </div>
                    <div id="menu2">
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-xs-12">
                               <b> Per Day Rate :</b>
                                     <asp:TextBox ID="txt_per_rate_salary" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                               <b> Basic :</b>
                                      <asp:TextBox ID="txt_basic_salary" runat="server" MaxLength="15" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);" onchange="add_number1();"></asp:TextBox>
                            </div>

                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>VDA :</b>
                                 <asp:TextBox ID="txt_vda_salary" runat="server" MaxLength="15" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);" onchange="add_number1();"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Basic + VDA :</b>
                                 <asp:TextBox ID="txt_basic_vda_salary" runat="server" ReadOnly="true" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>HRA Amount :</b>
                                 <asp:TextBox ID="txt_hra_amount_salary" runat="server" Width="100%" class="form-control pr_add text_box" Text="0" onchange="add_number();" onkeypress="return isNumber0(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                               <b> HRA Percent (%): </b>
                                 <asp:TextBox ID="txt_hra_percent_salary" runat="server" MaxLength="5" Width="100%" class="form-control pr_add text_box" Text="0" onchange="hr_amount_per();" onkeypress="return isNumber0_dot(event);"></asp:TextBox>
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-xs-12">
                                <b>Bonus Policy Applicable:</b>     
                                  <asp:DropDownList ID="ddl_bonus_policy_aap_salary" runat="server" CssClass="form-control text_box" Width="100%" onchange="change_txt_box1()">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                               <b> Bonus Amount :</b>     
                                 <asp:TextBox ID="txt_bonus_amount_salary" runat="server" Text="0" MaxLength="5" class="form-control pr_add text_box" onchange="bill_service_charge();" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Bonus Percent (%) : </b>..    
                                 <asp:TextBox ID="txt_sal_bonus" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="8.33" onchange="sal_bonus();" onkeypress="return isNumber_dot(event);">8.33</asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Bonus Taxable:</b>     
                                  <asp:DropDownList ID="ddl_bonus_taxable_salary" runat="server" CssClass="form-control text_box" Width="100%" onchange="return bonus_taxable_salary();">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                      <asp:ListItem Value="2">Not Applicable</asp:ListItem>
                                      <asp:ListItem Value="3">Advance Policy</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Gratuity Percent (%):</b>
                                 <asp:TextBox ID="txt_sal_graguity_per" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="4.81" onkeypress="return isNumber_dot(event);">4.81</asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Gratuity for ESIC Taxable:</b>     
                                  <asp:DropDownList ID="ddl_gratuity_taxable_salary" runat="server" CssClass="form-control text_box" Width="100%" onchange="return gratuaty_taxable_salary();">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                      <asp:ListItem Value="2">Not Applicable</asp:ListItem>
                                  </asp:DropDownList>
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Washing :</b>
                                 <asp:TextBox ID="txt_washing_salary" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Travelling :</b>
                                 <asp:TextBox ID="txt_travelling_salary" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Education :</b>
                                 <asp:TextBox ID="txt_education_salary" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>CCA :</b>
                                 <asp:TextBox ID="txt_cca_salary" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                               <b> Other Allowances :</b>
                                 <asp:TextBox ID="txt_allowances_salary" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>

                            <div class="col-md-2 col-xs-12">
                               <b> Leave Taxable:</b>     
                                  <asp:DropDownList ID="ddl_leave_taxable_salary" runat="server" CssClass="form-control text_box" Width="100%" onchange="leave_taxable_salary();">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                      <asp:ListItem Value="2">Not Applicable</asp:ListItem>
                                      <asp:ListItem Value="2">Advance Policy</asp:ListItem>
                                  </asp:DropDownList>
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Leave Days :</b>
                                 <asp:TextBox ID="txt_leave_days_salary" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Leave Days Percent(%) :</b>
                                 <asp:TextBox ID="txt_leave_days_percent_salary" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber1_dot(event);" onchange="leav_days_per_salary();"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                               <b> PF Percent (%) : </b>    
                                 <asp:TextBox ID="txt_sal_pf" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="0" onchange="sal_pf();" onkeypress="return isNumber_dot(event);">0</asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>ESIC Percent (%) :</b>     
                                 <asp:TextBox ID="txt_sal_esic" runat="server" class="form-control pr_add text_box" Text="0.75" onchange="sal_esic();" onkeypress="return isNumber_dot(event);">0.75</asp:TextBox>
                            </div>


                            <div class="col-md-2 col-xs-12">
                                <b>Uniform Percent (%) : </b>     
                                 <asp:TextBox ID="txt_sal_uniform_percent" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="0" onchange="sa_uniform_rate_per();" onkeypress="return isNumber4_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                               <b> Uniform Rate :    </b> 
                                 <asp:TextBox ID="txt_sal_uniform_rate" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber4(event);"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-xs-12">
                               <b> PT Applicable:</b>     
                                  <asp:DropDownList ID="ddl_pt_applicable" runat="server" CssClass="form-control text_box" Width="100%">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                  </asp:DropDownList>
                            </div>

                            <div class="col-md-2 col-xs-12">
                               <b> Special Allowances Applicable(Only 12Hrs):</b>     
                                  <asp:DropDownList ID="ddl_ot_policy" runat="server" CssClass="form-control text_box" Width="100%" onchange="return ot_policy();">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12">
                                <b>Special Allowances :</b>      
                                 <asp:TextBox ID="txt_ot_amount" runat="server" MaxLength="5" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber4_dot(event);">0</asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12">
                               <b> ESIC On Other Allowances:</b>     
                                  <asp:DropDownList ID="ddl_esic_oa_salary" runat="server" CssClass="form-control text_box" Width="100%" onkeypress="return isNumber(event);">
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                      <asp:ListItem Value="0">No</asp:ListItem>

                                  </asp:DropDownList>
                            </div>

                        </div>

                        <br />

                    </div>
                    <div id="menu3">
                        <br />
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table class="table table-striped" style="width: 25%; border-color: #f5f3f3; color: #f85d5d" border="1">
                                    <tr>
                                        <th style="width: 50%; border-color: beige">
                                            <span>Contract Type :</span>
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="ddl_cotract_type" Width="100%" Style="font-weight: bold;"></asp:Label>
                                        </th>
                                    </tr>
                                </table>
                                <br />
                                <div class="row">
                                    <div class="col-md-2 col-sm-2 col-xs-12 material_name" runat="server" style="display: none">
                                       <b> Material Name :</b>
                                 <asp:TextBox ID="txt_material_name" runat="server" class="form-control pr_add text_box" onKeyPress="return AllowAlphabet(event);"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-xs-12">
                                        <b>Rate :</b>
                                 <asp:TextBox ID="txt_contract_rate" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                                    </div>
									<div class="col-sm-2">
                            <b>Machine Rental Applicable :</b>
                            <asp:DropDownList ID="ddl_machine_rental_applicable" runat="server" CssClass="form-control text_box"  onchange ="return machine_rental();" >
                                <asp:ListItem Value="0">No</asp:ListItem>
                                 <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                                     <div class="col-md-2 col-sm-3 col-xs-12 hide_textbox">
                            <b> Machine Rental Amount :</b>
                            <asp:TextBox ID="txt_machine_rental_amount" CssClass="form-control text_box" runat="server">0</asp:TextBox>
                        </div>
                                    <div class="col-sm-2" style="width: 18%">
                                        <b>Handling Charges Applicable :</b>
                            <asp:DropDownList ID="ddl_handling_charge" runat="server" CssClass="form-control text_box" onchange="change_drop();">
                                <asp:ListItem Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-3 col-xs-12" >
                                        <b>Handling Charges(percentage):</b>
                            <asp:TextBox ID="txt_handling_percent" CssClass="form-control text_box" runat="server" Text="0" onkeypress="return isNumber120_dot(event);"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 col-sm-3 col-xs-12">
                                        <b>Handling Charges(amount) :</b>
                            <asp:TextBox ID="txt_handling_amount" CssClass="form-control text_box" runat="server" Text="0" onkeypress=" return isNumber121_dot(event);"></asp:TextBox>
                                    </div>

                                    <span style="display: none;" class="add_material">
                                        <asp:LinkButton ID="lnk_btn" runat="server" OnClick="lnk_btn_Click" OnClientClick="return save_validate2();">
                        <img alt="Add Item" src="Images/add_icon.png"  style="margin-top:2.2em;" />
                                        </asp:LinkButton></span>

                                </div>
                                <br />
                                <br />

                                <%-- komal 3-05-19--%>

                                <div class="container" style="width: 100%">
                                    <span style="display: none;" class="gv_material">
                                        <asp:Panel ID="Panel17" runat="server" CssClass="grid">
                                            <asp:GridView ID="grd_material_detail" class="table" runat="server" BackColor="White" OnRowDataBound="grd_material_detail_RowDataBound"
                                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                AutoGenerateColumns="False" OnPreRender="grd_material_detail_PreRender" Width="100%" DataKeyNames="id">

                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sr No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
                                                    <asp:BoundField DataField="Field1" HeaderText="Material Name" SortExpression="Field1" />
                                                    <asp:BoundField DataField="Field2" HeaderText="Rate" SortExpression="Field2" />
                                                    <asp:BoundField DataField="Field4" HeaderText="Handling Charges(Percentage)" SortExpression="Field4" />
                                                    <asp:BoundField DataField="Field3" HeaderText="Handling Charges(amount)" SortExpression="Field3" />
                                                    <asp:TemplateField>
                                                        <ItemStyle Width="20px" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnk_remove_bank" runat="server" CausesValidation="false" OnClick="lnk_remove_bank_Click1"><img alt="" height="10"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </span>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <br />
                        <%--  vikas 09-01--%>
                        <br />
                        <div class="material_tab" style="display: none">
                            <div class="row">
                                <div class="col-md-3 col-xs-12">
                                   <b> EQUMENTAL RENTAL COST Applicable :</b>
                                    <asp:DropDownList ID="ddl_equmental_applicable" runat="server" CssClass="form-control text_box" onchange="Rental_cost();">
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                   <b> No. Of unit :</b>
                                         <asp:TextBox ID="txt_equment_rate" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Rate Per Unit :</b>
                                 <asp:TextBox ID="txt_equment_rental" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Handling Charges Applicable :</b>
                            <asp:DropDownList ID="ddl_equmental_charges" runat="server" CssClass="form-control text_box">
                                <asp:ListItem Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Handling % :</b>
                            <asp:TextBox ID="txt_equmental_percent" CssClass="form-control text_box" runat="server">0</asp:TextBox>
                                </div>

                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3 col-xs-12">
                                    <b>Chemical and Consumables Applicable :</b>
                                    <asp:DropDownList ID="ddl_chemical_applicable" runat="server" CssClass="form-control text_box" onchange="consume_applicable();">
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                   <b> No. Of unit :</b>
                                         <asp:TextBox ID="txt_chemical_unit" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Rate Per Unit   :</b>
                                 <asp:TextBox ID="txt_chemical" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Handling Charges Applicable :</b>
                            <asp:DropDownList ID="ddl_chemical_charges" runat="server" CssClass="form-control text_box">
                                <asp:ListItem Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Handling % :</b>
                            <asp:TextBox ID="txt_chemical_percent" CssClass="form-control text_box" runat="server">0</asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3 col-xs-12">
                                    <b>Dustbin Liners Applicable :</b>
                                    <asp:DropDownList ID="ddl_dustin_applicable" runat="server" CssClass="form-control text_box" onchange="linear_applicable();">
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                   <b> No. Of unit :</b>
                                         <asp:TextBox ID="txt_dustin_rate" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                   <b> Rate Per Unit  :</b>
                                 <asp:TextBox ID="txt_dustin" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Handling Charges Applicable :</b>
                            <asp:DropDownList ID="ddl_dustin_charges" runat="server" CssClass="form-control text_box">
                                <asp:ListItem Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Handling % :</b>
                            <asp:TextBox ID="txt_dustin_percent" CssClass="form-control text_box" runat="server">0</asp:TextBox>
                                </div>

                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3 col-xs-12">
                                    <b>Femina Hygiene bin Applicable :</b>
                                    <asp:DropDownList ID="ddl_femina_applicable" runat="server" CssClass="form-control text_box" onchange="bin_applicable();">
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                   <b> No. Of unit :</b>
                                         <asp:TextBox ID="txt_femina_unit" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Rate Per Unit  :</b>
                                 <asp:TextBox ID="txt_femina" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Handling Charges Applicable :</b>
                            <asp:DropDownList ID="ddl_femina_charges" runat="server" CssClass="form-control text_box">
                                <asp:ListItem Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Handling % :</b>
                            <asp:TextBox ID="txt_femina_percent" CssClass="form-control text_box" runat="server">0</asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3 col-xs-12">
                                    <b>Aerosol Dispenser Machines Applicable :</b>
                                    <asp:DropDownList ID="ddl_aerosol_applicable" runat="server" CssClass="form-control text_box" onchange="machine_applicable();">
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>No. Of unit :</b>
                                         <asp:TextBox ID="txt_aerosol_rate" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                   <b> Rate Per Unit  :</b>
                                 <asp:TextBox ID="txt_aerosol" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Handling Charges Applicable :</b>
                            <asp:DropDownList ID="ddl_aerosol_charges" runat="server" CssClass="form-control text_box">
                                <asp:ListItem Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                   <b> Handling % :</b>
                            <asp:TextBox ID="txt_aerosol_percent" CssClass="form-control text_box" runat="server">0</asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3 col-xs-12">
                                   <b> Tool and Tackles Applicable :</b>
                                    <asp:DropDownList ID="ddl_tool_applicable" runat="server" CssClass="form-control text_box" onchange="tackle_applicable();">
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>No. Of unit :</b>
                                         <asp:TextBox ID="txt_tool_unit" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Rate Per Unit  :</b>
                                 <asp:TextBox ID="txt_tool" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Handling Charges Applicable :</b>
                                    <asp:DropDownList ID="ddl_tool_charges" runat="server" CssClass="form-control text_box">
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <b>Handling % :</b>
                            <asp:TextBox ID="txt_tool_percent" CssClass="form-control text_box" runat="server">0</asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div id="menu4">
                        <br />
                        <div class="row">

                            <div class="col-md-2 col-xs-12">
                                Type :     
                                 <asp:DropDownList ID="ddl_dc_type" runat="server" CssClass="form-control text_box" Width="100%" onchange="deep_clean_type();">
                                     <asp:ListItem Value="0">Select</asp:ListItem>
                                     <asp:ListItem Value="1">Fix</asp:ListItem>
                                     <asp:ListItem Value="2">Sqr.Feet</asp:ListItem>
                                 </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                              <b>  Rate :</b>
                                 <asp:TextBox ID="txt_dc_rate" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Sq.Feet Area :</b>
                                 <asp:TextBox ID="txt_dc_area" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <b>Handling Charges Applicable :</b>
                            <asp:DropDownList ID="ddl_dc_handling_charge" runat="server" CssClass="form-control text_box" onchange="deep_h_charge();">
                                <asp:ListItem Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-3 col-xs-12">
                                <b>Handling % :</b>
                            <asp:TextBox ID="txt_dc_handling_percent" CssClass="form-control text_box" runat="server">0</asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div id="menu5">
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-xs-12">
                                <b>Type :</b>     
                                 <asp:DropDownList ID="ddl_conveyance_type" runat="server" CssClass="form-control text_box" Width="100%" onchange="Conveyance_type()">
                                     <asp:ListItem Value="0">Select</asp:ListItem>
                                     <asp:ListItem Value="1">Fix</asp:ListItem>
                                     <asp:ListItem Value="2">Km</asp:ListItem>
                                     <asp:ListItem Value="3">EmployeeWise</asp:ListItem>
                                 </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                               <b> Rate/Km :</b>
                                 <asp:TextBox ID="txt_conveyance_rate" runat="server" class="form-control text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                               <b> Total Km :</b>
                                 <asp:TextBox ID="txt_conveyance_km" runat="server" class="form-control text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-xs-12" style="width: 21%">
                               <b> Conveyance Service Charge Applicable:</b>     
                                  <asp:DropDownList ID="ddl_conveyance_service_charge" runat="server" CssClass="form-control text_box" Width="100%" onkeypress="return isNumber(event);" onchange="Service_Charge_Applicable();">
                                      <asp:ListItem Value="0">No</asp:ListItem>
                                      <asp:ListItem Value="1">Yes</asp:ListItem>
                                  </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-12" style="width: 18%">
                                <b>In (%) :</b>     
                                 <asp:TextBox ID="txt_conveyance_service_charge" runat="server" MaxLength="5" class="form-control text_box" onkeypress="return isNumber1_dot(event);" onchange="return Conveyance_Service_Charge_per();">0</asp:TextBox>
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2 col-xs-12" style="width: 20%">
                               <b> In Amount :</b>     
                                 <asp:TextBox ID="txt_conveyance_service_amount" runat="server" MaxLength="5" class="form-control text_box" onkeypress="return isNumber1(event);">0</asp:TextBox>
                            </div>
                        </div>
                        <hr style="border-color:black" />
                        <div class="row">
                         <div class="col-sm-3 col-xs-12" style="margin-top:25px;text-align:left;font-weight: bold;">
                                     <asp:Label ID="Label2" runat="server" Text="FOOD ALLOWANCE :"></asp:Label>
                                 </div>
                                 
                              <div class="col-sm-2 col-xs-12">
                                     <b> Rate :</b>
                                      <asp:TextBox runat="server" ID="txt_food_rate" CssClass="form-control" onkeypress="return isNumber_dot(event)">0</asp:TextBox>
                                  </div>

                           <div class="col-sm-3 col-xs-12" style="margin-top:25px;text-align:left;font-weight: bold;">
                                     <asp:Label ID="Label3" runat="server" Text="OUTSTATION ALLOWANCE/CONVEYANCE :"></asp:Label>
                                 </div>
                                    
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Rate :</b>
                                      <asp:TextBox runat="server" ID="txt_oc_rate" CssClass="form-control" onkeypress="return isNumber_dot(event)">0</asp:TextBox>
                                  </div>
                        </div>
                        <br />
                          <div class="row">
                           <div class="col-sm-3 col-xs-12" style="margin-top:25px;text-align:left;font-weight: bold;">
                                     <asp:Label ID="Label4" runat="server" Text="OUTSTATION FOOD ALLOWANCE :"></asp:Label>
                                 </div>
                                   
                                  <div class="col-sm-2 col-xs-12">
                                     <b> Rate :</b>
                                      <asp:TextBox runat="server" ID="txt_os_rate" CssClass="form-control" onkeypress="return isNumber_dot(event)">0</asp:TextBox>
                                  </div>

                         <div class="col-sm-3 col-xs-12" style="margin-top:25px;text-align:left;font-weight: bold;">
                                     <asp:Label ID="Label5" runat="server" Text="NIGHT HALT :"></asp:Label>
                                 </div>
                                   
                                  <div class="col-sm-2 col-xs-12">
                                      <b>Rate :</b>
                                      <asp:TextBox runat="server" ID="txt_nh_rate" CssClass="form-control" onkeypress="return isNumber_dot(event)">0</asp:TextBox>
                                  </div>
                              </div>
                        <br />
                                <div class="row">
                        <div class="col-sm-3 col-xs-12"  style="margin-top:25px;text-align:left;font-weight: bold;">
                                     <asp:Label ID="Label6" runat="server" Text=" KM RATE :"></asp:Label>
                                 </div>
                                   
                                  <div class="col-sm-2 col-xs-12">
                                      <b>Rate :</b>
                                      <asp:TextBox runat="server" ID="txt_km_rate" CssClass="form-control" onkeypress="return isNumber_dot(event)">0</asp:TextBox>
                                  </div>
                                    </div>

                    </div>
                    <div id="menu6">
                        <br />
                        <div class="row">

                            <div class="col-md-2 col-xs-12">
                                <b>Type : </b>    
                                 <asp:DropDownList ID="ddl_pc_type" runat="server" CssClass="form-control text_box" Width="100%" onchange="deep_clean_type();">
                                     <asp:ListItem Value="0">Select</asp:ListItem>
                                     <asp:ListItem Value="1">Fix</asp:ListItem>
                                     <asp:ListItem Value="2">Sqr.Feet</asp:ListItem>
                                 </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Rate :</b>
                                 <asp:TextBox ID="txt_pc_rate" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <b>Sq.Feet Area :</b>
                                 <asp:TextBox ID="txt_pc_area" runat="server" class="form-control pr_add text_box" Text="0" onkeypress="return isNumber_dot(event);"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                               <b> Handling Charges Applicable :</b>
                            <asp:DropDownList ID="ddl_pc_handling_charge" runat="server" CssClass="form-control text_box" onchange="deep_h_charge()">
                                <asp:ListItem Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-3 col-xs-12">
                                <b>Handling % :</b>
                            <asp:TextBox ID="txt_pc_handling_percent" CssClass="form-control text_box" runat="server">0</asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div id="menu7">
                        <br />
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-sm-2 col-xs-12">
                                        <b>Select Machine:</b>
                                         <asp:DropDownList runat="server" CssClass="form-control" ID="ddl_machine">
                                             <asp:ListItem>Select</asp:ListItem>
                                         </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Select Rate Type:</b>
                                         <asp:DropDownList runat="server" CssClass="form-control" ID="ddl_rate_type">
                                             <asp:ListItem Value="Select">Select</asp:ListItem>
                                             <asp:ListItem Value="Fix Rate">Fix Rate</asp:ListItem>
                                             <asp:ListItem Value="Per Hr. Rate">Per Hr. Rate</asp:ListItem>
                                             <asp:ListItem Value="Per Day Rate">Per Day Rate</asp:ListItem>
                                             <asp:ListItem Value="Per Sqft. Rate">Per Sqft. Rate</asp:ListItem>

                                         </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <b>Amount:</b>
                                         <asp:TextBox runat="server" CssClass="form-control" ID="txt_machine_amount" Text="0" onkeypress="return isNumber_dot(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <b>Handling Charges Applicable:</b>
                                         <asp:DropDownList runat="server" CssClass="form-control" ID="ddl_h_c_applicable" onchange="hidde_in();">
                                             <asp:ListItem Value="No">No</asp:ListItem>
                                             <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                         </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-1 col-xs-12">
                                        <b>In(%):</b>
                                         <asp:TextBox runat="server" CssClass="form-control" ID="txt_in_per" onkeypress="Machine_rent_in()" Text="0"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <b>In(Amt):</b>
                                         <asp:TextBox runat="server" CssClass="form-control" ID="txt_in_amt" onkeypress="Machine_rent_amount()" Text="0"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 col-xs-12 add_rental" style="margin-top: 1.5em; display: none">
                                        <asp:LinkButton ID="btn_add_machine" OnClick="btn_add_machine_Click" runat="server" OnClientClick="return machine_click_val();">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="container-fluid">
                                    <asp:Panel ID="Panel4" runat="server" CssClass="grid-view">
                                        <asp:GridView ID="gv_product_details" class="table" runat="server" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            OnRowDataBound="gv_product_details_RowDataBound"
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
                                                        <asp:LinkButton ID="lnk_remove_product" runat="server" OnClick="lnk_remove_product_Click" CausesValidation="false"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sr No.">
                                                    <ItemStyle Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:BoundField DataField="policy_machine_nane" HeaderText="Machine Name"
                                                    SortExpression="policy_machine_nane" />
                                                <asp:BoundField DataField="policy_rate_type" HeaderText="Rent Type"
                                                    SortExpression="policy_rate_type" />
                                                <asp:BoundField DataField="policy_m_rate" HeaderText="Rent"
                                                    SortExpression="policy_m_rate" />
                                                <asp:BoundField DataField="policy_m_h_charges" HeaderText="Handling Charges"
                                                    SortExpression="policy_m_h_charges" />
                                                <asp:BoundField DataField="policy_in_pre" HeaderText="Handling In %"
                                                    SortExpression="policy_in_pre" />
                                                <asp:BoundField DataField="policy_m_amount" HeaderText="Handling In Amount"
                                                    SortExpression="policy_m_amount" />
                                                <asp:BoundField DataField="machine_code" HeaderText="machine_code"
                                                    SortExpression="machine_code" />
                                            </Columns>
                                        </asp:GridView>

                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_add_machine" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div id="menu8">
                        <div class="row">
                            <br />
                            <div class="col-sm-2 col-xs-12">
                          Service Charge:
                                <br />
                       <asp:DropDownList ID="ddl_service_charge" runat="server" CssClass="form-control" Onchange="return change_dropdown_r_m();">
                                    <asp:ListItem Value="0">Not Applicable</asp:ListItem>
                                    <asp:ListItem Value="1">Applicable</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                             <div class="col-sm-2 col-xs-12">
                            Service Charge Rate(Rs) :
                        <asp:TextBox ID="txt_service_charge_rate" runat="server" class="form-control"  onkeypress="return isNumber_r_m(event);">0</asp:TextBox>
                        </div>
                            <div class="col-sm-2 col-xs-12">
                            Service Charge Rate(%) :
                        <asp:TextBox ID="txt_service_charge" runat="server" class="form-control" onkeypress="return isNumber_dot_r_m(event);">0</asp:TextBox>
                        </div>

                        </div>
                    </div>
                    <div id="menu9">
                        <div class="row">
                            <br />
                            <div class="col-sm-2 col-xs-12">
                          Service Charge:
                                <br />
                       <asp:DropDownList ID="ddl_service_charge_adm" runat="server" CssClass="form-control" Onchange="return change_dropdown_administrative();">
                                    <asp:ListItem Value="0">Not Applicable</asp:ListItem>
                                    <asp:ListItem Value="1">Applicable</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                             <div class="col-sm-2 col-xs-12">
                            Service Charge Rate(Rs) :
                        <asp:TextBox ID="txt_rate_adm" runat="server" class="form-control"  onkeypress="return isNumber_adm(event);">0</asp:TextBox>
                        </div>
                            <div class="col-sm-2 col-xs-12">
                            Service Charge Rate(%) :
                        <asp:TextBox ID="txt_ser_rate_adm" runat="server" class="form-control"  onkeypress="return isNumber_dot_adm(event);">0</asp:TextBox>
                        </div>

                        </div>
                    </div>
                    <br />
                    <div class="row text-center">
                        <asp:Button ID="btnadd" runat="server" class="btn btn-primary"
                            Text=" Save " OnClick="btnadd_Click" OnClientClick="return Req_validation();" />
                      
                          <asp:Button ID="btn_assign" runat="server" class="btn btn-primary  policy" style="display:none"
                            Text=" Assign Policy " OnClick="btn_assign_Click" OnClientClick="return Req_validation1();"/>
                        <%-- <asp:Button ID="btn_UPDATE" runat="server" class="btn btn-primary"
                        Text=" Update " OnClick="btn_UPDATE_Click" Visible="false" />--%>

                    &nbsp;&nbsp;<asp:Button ID="btndelete" runat="server" class="btn btn-primary" OnClick="btndelete_Click" Text=" Delete " OnClientClick="return R_validation();" />
                        &nbsp;&nbsp;<asp:Button ID="btn_close" runat="server" class="btn btn-danger"
                            Text=" Close " OnClick="btn_close_Click" />
                    </div>
                    <br />
                </div>


                <div class="container-fluid">
                    <br />
                    <asp:Panel ID="Panel2" runat="server" CssClass="grid-view">
                        <asp:GridView ID="grd_policy" class="table" runat="server" CellPadding="1" Font-Size="X-Small" OnPreRender="grd_policy_PreRender"
                            ForeColor="#333333" OnRowDataBound="grd_policy_RowDataBound"
                            OnSelectedIndexChanged="grd_policy_SelectedIndexChanged">
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
                <br />
            </div>
        </asp:Panel>

    </div>
</asp:Content>


