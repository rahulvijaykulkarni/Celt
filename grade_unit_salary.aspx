<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="grade_unit_salary.aspx.cs" Inherits="Billing_rates" Title="Client / Branch Wise Salary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <style type="text/css">
         .text-red {
            color: #f00;
        }
        .HeaderFreez {
            position: relative;
            top: expression(this.offsetParent.scrollTop);
            z-index: 10;
        }

        button, input, optgroup, select, textarea {
            color: inherit;
            margin: 0 0 0 0px;
        }

        * {
            box-sizing: border-box;
        }

        .tab-section {
            background-color: #fff;
        }

        .form-control {
            display: inline;
        }

        .grid-view {
            height: auto;
            overflow: scroll;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_header" runat="Server">
    <script type="text/javascript" src="js/jquery-1.12.3.min.js"></script>
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <!-- Latest compiled JavaScript -->
    <script type="text/javascript" src="js/bootstrap.min.js"></script>

    <link href="css/new_stylesheet.css" rel="stylesheet" />

    <script type="text/javascript" src="Scripts/bootstrap-multiselect.js"></script>
    <link href="Content/bootstrap-multiselect.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
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

    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {
            $('#<%=ddl_billing_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

        }

    </script>

    <script type="text/javascript">

        function pageLoad() {

            $(".date-picker11").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                yearRange: '1951',
                onSelect: function (selected) {
                    $(".date-picker22").datepicker("option", "minDate", selected)
                }
            });

            $(".date-picker22").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
                onSelect: function (selected) {
                    $(".date-picker11").datepicker("option", "maxDate", selected)
                }
            });
            $(".date-picker11").attr("readonly", "true");
            $(".date-picker22").attr("readonly", "true");
            // end arrears

            $('.date-picker').datepicker({
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
            }).click(function () {
                $('.ui-datepicker-calendar').hide();
            });

            $(".date-picker").attr("readonly", "true");
            arrear_type();

            $(document).ready(function () {
                $('[id*=chk_selectall_header]').click(function () {
                    $("[id*='chk_client']").attr('checked', this.checked);
                });
            });


            var table = $('#<%=gv_lessEmployeeAttendances.ClientID%>').DataTable({
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
            .appendTo('#<%=gv_lessEmployeeAttendances.ClientID%>_wrapper .col-sm-6:eq(0)');
            $.fn.dataTable.ext.errMode = 'none';


        }
        function openWindow() {
            window.open("html/grade_unit_salary.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
        function arrear_type() {
            var ddl_arrears_type = document.getElementById('<%=ddl_arrears_type.ClientID %>');
             var ddl_arrears_type = ddl_arrears_type.options[ddl_arrears_type.selectedIndex].text;
             if (ddl_arrears_type == "Policy Wise") {
                 $(".arrear").show();
             }

             else { $(".arrear").hide(); }
         }

        function Req_arrears() {


            var txt_month = document.getElementById('<%=txt_arrear_month_year.ClientID %>');
            var txt_month1 = document.getElementById('<%=txt_arrear_monthend.ClientID %>');


            var ddl_arrears_type = document.getElementById('<%=ddl_arrears_type.ClientID %>');
            var Selected_arrears_type = ddl_arrears_type.options[ddl_arrears_type.selectedIndex].text;

            if (Selected_arrears_type == "Select") {
                alert("Please Select Arrear Type");
                ddl_arrears_type.focus();
                return false;

            }


            if (Selected_arrears_type == "Policy Wise") {

                if (txt_month.value == "") {
                    alert("Please Select From Month ");

                    return false;
                }

                if (txt_month1.value == "") {
                    alert("Please Select To Month ");

                    return false;
                }
            }

            return true;
        }
        function Req_validation() {

            var txt_client_code = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_client = txt_client_code.options[txt_client_code.selectedIndex].text;

            var txt_month = document.getElementById('<%=txt_month_year.ClientID %>');

            if (Selected_client == "Select") {
                alert("Please Select Client Name ");
                txt_client_code.focus();
                return false;
            }
            if (Selected_client == "ALL") {
                alert("Please Select Client Name ");
                txt_client_code.focus();
                return false;
            }
            if (txt_month.value == "") {
                alert("Please Select month & year ");
                txt_month.focus();
                return false;
            }
            return true;
        }
        function Req_validations() {

            var txt_client_code = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_client = txt_client_code.options[txt_client_code.selectedIndex].text;

            var txt_month = document.getElementById('<%=txt_month_year.ClientID %>');

              if (Selected_client == "Select") {
                  alert("Please Select Client Name ");
                  txt_client_code.focus();
                  return false;
              }

              if (txt_month.value == "") {
                  alert("Please Select month & year ");
                  txt_month.focus();
                  return false;
              }
              var ddl_con_type = document.getElementById('<%=ddl_con_type.ClientID %>');
            var Selected_ddl_con_type = ddl_con_type.options[ddl_con_type.selectedIndex].text;

            if (Selected_ddl_con_type == "Select") {
                alert("Please Select Conveyance Type");
                ddl_con_type.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function Req_validation2() {

            var txt_client_code = document.getElementById('<%=ddl_client.ClientID %>');
               var Selected_client = txt_client_code.options[txt_client_code.selectedIndex].text;

               var txt_month = document.getElementById('<%=txt_month_year.ClientID %>');

            if (Selected_client == "Select") {
                alert("Please Select Client Name ");
                txt_client_code.focus();
                return false;
            }

            if (txt_month.value == "") {
                alert("Please Select month & year ");
                txt_month.focus();
                return false;
            }

        }
        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });

        function vendor_textbox() {
            var ddl_modeofpayment = document.getElementById('<%=ddl_modeofpayment.ClientID %>');
           var Selected_ddl_modeofpayment = ddl_modeofpayment.options[ddl_modeofpayment.selectedIndex].text;

           if (Selected_ddl_modeofpayment == "CHECK") {
               $(".hide_textbox").show();
               $(".hide_text").show();
           }
           else {
               $(".hide_textbox").hide();
               $(".hide_text").hide();
           }
       }
        function vendor_validation() {
            var txt_vendor_month = document.getElementById('<%=txt_vendor_month.ClientID %>');
            if (txt_vendor_month.value == "") {
                alert("Please Enter Month");
                txt_vendor_month.focus();
                return false;
            }
           var ddl_vendorname = document.getElementById('<%=ddl_vendorname.ClientID %>');
            var Selected_ddl_vendorname = ddl_vendorname.options[ddl_vendorname.selectedIndex].text;

            if (Selected_ddl_vendorname == "Select") {
                alert("Please Select Vendor Name");
                ddl_vendorname.focus();
                return false;
            }


           
            var ddl_purchase_onum = document.getElementById('<%=ddl_purchase_onum.ClientID %>');
            var Selected_ddl_purchase_onum = ddl_purchase_onum.options[ddl_purchase_onum.selectedIndex].text;
            if (Selected_ddl_purchase_onum == "Select") {
                alert("Please Select  Purchase Orader Number");
                ddl_purchase_onum.focus();
                return false;
            }
            var ddl_modeofpayment = document.getElementById('<%=ddl_modeofpayment.ClientID %>');
            var Selected_ddl_modeofpayment = ddl_modeofpayment.options[ddl_modeofpayment.selectedIndex].text;
            if (Selected_ddl_modeofpayment == "Select") {
                alert("Please Select Mode of Payment");
                ddl_modeofpayment.focus();
                return false;
            }
            if (Selected_ddl_modeofpayment == "CHECK") {
                var txt_check_number = document.getElementById('<%=txt_check_number.ClientID %>');
                if (txt_check_number.value == "") {
                    alert("Please Enter Check Number");
                    txt_check_number.focus();
                    return false;
                }
                var txt_check_date = document.getElementById('<%=txt_check_date.ClientID %>');
                if (txt_check_date.value == "") {
                    alert("Please Select Check Date");
                    txt_check_date.focus();
                    return false;
                }
            }
        }
    </script>
    <style>
        
        .grid-view {
            max-height: 300px;
            height: auto;
            overflow-x: hidden;
            overflow-y: auto;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: 16px;" class="text-center text-uppercase"><b>Client / Branch wise Salary</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Client / Branch wise Salary Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                               <b> Select Month :</b>
                            <asp:TextBox ID="txt_month_year" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                <b> Client :</b>
                        <asp:DropDownList ID="ddl_client" DataValueField="client_code" DataTextField="client_name" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                            </div>
                            <asp:Panel ID="unit_panel" runat="server">
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                    <b> State :</b>
                            <asp:DropDownList ID="ddl_billing_state" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_billing_state_SelectedIndexChanged" AutoPostBack="true" />
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                    <b> Branch :</b>
                            <asp:DropDownList ID="ddl_unitcode" DataValueField="unit_code" DataTextField="unit_name" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                                </div>

                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-12" style="width: 157px;">
                                    <b> Process Data :</b>
                                    <asp:DropDownList ID="ddl_process_data" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">Current Policy</asp:ListItem>
                                        <asp:ListItem Value="1">Old Policy</asp:ListItem>
                                    </asp:DropDownList>


                                </div>
                            </asp:Panel>
                        </div>
                        <br />
                        <div class="row">
                            <asp:Panel ID="unit_panel1" runat="server">
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                  <b>  Bank :</b>
                            <asp:DropDownList ID="ddl_bank" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                                </div>

                                <div class="col-sm-2 col-xs-12" id="abc111">
                                  <b>  Billing Start Day: </b>
                                        <asp:DropDownList ID="ddl_start_date_common" runat="server" CssClass="form-control text_box" Width="100%">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
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
                                <div class="col-sm-2 col-xs-12" id="abc11">
                                   <b> Billing End Day:</b> 
                                        <asp:DropDownList ID="ddl_end_date_common" runat="server" CssClass="form-control text_box" Width="100%">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
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

                                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-12" style="width: 136px;">
                                   <b> Payment Type :</b>
                                    <asp:DropDownList ID="ddl_invoice_type" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_invoice_type_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1">CLUB</asp:ListItem>
                                        <asp:ListItem Value="2">UNCLUB</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-12" style="margin-left: 6px;">
                                    <asp:Panel ID="desigpanel" runat="server">
                                       <b> Designation :</b>
                                        <asp:DropDownList ID="ddl_designation" runat="server" CssClass="form-control" Style="width: 128px;" />
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                    </div>
                <div id="tabs" style="background: #f3f1fe; padding:20px 20px 20px 20px; border-radius:10px">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li><a href="#menu1"><b>Payment</b></a></li>
                        <li><a href="#menu2"><b>Arrears Payment</b></a></li>
                        <li><a href="#menu3"><b>Conveyance Payment</b></a></li>
                        <li><a href="#menu5"><b>Material Payment</b></a></li>
                        <li><a href="#menu4"><b>Vendor Payment</b></a></li>
                        <li><a href="#menu6"><b>R&M Service Payment</b></a></li>
                        <li><a href="#menu7"><b>Administrative Expense Payment</b></a></li>
                    </ul>


                    <div id="menu1">

                        <br />
                        <div class="row">
                            <div class="col-sm-1 col-xs-12">
                                <asp:Button ID="btn_save" runat="server" CssClass="btn btn-primary"
                                    Text="Process" OnClick="btn_save_Click" OnClientClick="return Req_validation();" />&nbsp;&nbsp;

                             
                                
                            </div>
                            <div class="col-sm-2 col-xs-12">

                                <asp:DropDownList ID="ddl_emp_xl_type" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Employee XL</asp:ListItem>
                                    <asp:ListItem Value="1">Without Ac/No XL</asp:ListItem>
                                    <asp:ListItem Value="2">Left Employee XL</asp:ListItem>
                                     <asp:ListItem Value="3">Reliever Employee XL</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-9 col-xs-12">
                                <asp:Button ID="btn_breakup" runat="server" CssClass="btn btn-large"
                                    Text="Get Employee XL" OnClick="btnExport_Click" OnClientClick="return Req_validation2();" />&nbsp;&nbsp;
                       
                      <asp:Button ID="btn_provisional_payment" runat="server" CssClass="btn btn-large" Width="16%"
                          Text="Provisional Payment" OnClick="btn_provisional_payment_Click" OnClientClick="return Req_validation2();" />&nbsp;&nbsp;

                       <%--<asp:Button ID="btn_bankupload" runat="server" CssClass="btn btn-primary"
                            Text="Bank Upload"   OnClick="btn_bank_Export_Click" OnClientClick="return Req_validation2();" />&nbsp;&nbsp;--%>
                                <asp:Button ID="btn_paid_salary" runat="server" CssClass="btn btn-large"
                                    Text="Paid Salary" OnClick="btn_paid_salary_Click" OnClientClick="return Req_validation2();" />&nbsp;&nbsp;
                        <asp:Button ID="btn_approve" runat="server" CssClass="btn btn-large" Width="17%"
                            Text="Get Annuxure Upload" OnClick="btn_approve_Click" OnClientClick="return   confirm('Are you sure you want to Approve?');" />&nbsp;&nbsp;
                        <asp:Button ID="btn_report" runat="server" CssClass="btn btn-primary"
                                    Text="Report" OnClick="btn_report_Click" OnClientClick="return Req_validation();" />&nbsp;&nbsp;
                        <asp:Button ID="bntclose" runat="server" CssClass="btn btn-danger" Width="10%"
                            Text="CLOSE" OnClick="bntclose_Click" />
                                <asp:HiddenField ID="hidden_month" runat="server" />
                                <asp:HiddenField ID="hidden_year" runat="server" />
                            </div>
                        </div>
                           <br /><br />
                        <div class="row text-center" > 
                             <asp:Button ID="ot_btn_breakup" runat="server" CssClass="btn btn-large"
                                    Text="OT Get Employee XL" OnClick="ot_btn_breakup_Click" OnClientClick="return Req_validation2();" />&nbsp;&nbsp;
                          <asp:Button ID="ot_btn_paid_salary" runat="server" CssClass="btn btn-large"
                                    Text="OT Paid Salary" OnClick="ot_btn_paid_salary_Click" OnClientClick="return Req_validation2();" />&nbsp;&nbsp;
                      

                        </div>
						
						 <asp:Panel ID="lessEmployeeAttendances" runat="server" CssClass="grid-view" ScrollBars="Auto" Visible="false" >
                      <div class="row">
                    
                    <div class="col-sm-9">
                        <div style="text-align: center; font-size: 16px;" class="text-center text-uppercase"><b>Hold Employee List</b></div>
                    </div>
                   
                    </div>
            
                        <%--<div class="row">--%>
                        <br />
                            
                            <asp:GridView ID="gv_lessEmployeeAttendances" runat="server"
                                 BorderColor="#CCCCCC" BorderStyle="None" CellPadding="3" Font-Size="X-Small" class="table" 
                               OnPreRender="gv_minibank_menu4"
                               > <%-- AutoGenerateColumns="false"  OnRowDataBound="gv_fullmonthot_RowDataBound"  GridLines="Both" DataKeyNames="Emp Code"--%>
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                       <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"  CssClass="text-uppercase"/>
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <%--<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />--%>
                       <%-- <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="SELECT EMPLOYEE" ItemStyle-Width="15%"  >
                                <HeaderStyle HorizontalAlign="Center" />
                                        <HeaderTemplate>
                                                <asp:CheckBox ID="chk_selectall_header" runat="server" Text="Select All" />
                                            </HeaderTemplate>
                                            <ItemStyle Width="80px" />
                                        <ItemTemplate >
                                    <asp:CheckBox ID="chk_client" runat="server" Style="text-align: center"  />
                                        </ItemTemplate>
                                <%-- <ItemStyle HorizontalAlign="Right" /> --%>  
                            </asp:TemplateField>

                                </Columns>

                    </asp:GridView>
                    
                

                       <%-- </div>--%>
                        <div class="row">
                            <div class="col-xs-12" style="text-align:center;">
                                <asp:Button ID="btn_lessAttendances" runat="server" CssClass="btn btn-primary" Width="10%" 
                                    Text="Aprrove" OnClick="lessEmployeeAttendancesFalgUpdate"  />

                        </div>
                    </div>
                    </asp:Panel>

                        
                    </div>
                    <div id="menu2">
                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                               <b> Arrear Type : </b>
                            <asp:DropDownList ID="ddl_arrears_type" runat="server" CssClass="form-control" onchange="return arrear_type();">
                                   <asp:ListItem >Select</asp:ListItem>
                                 <asp:ListItem Value="month">Month Wise</asp:ListItem>
                                   <asp:ListItem Value="policy">Policy Wise</asp:ListItem>
                               </asp:DropDownList></div>
                            <div class="col-sm-2 col-xs-12 arrear">
                          <b> From Month :</b><span class="text-red">*</span>
                                <asp:TextBox ID="txt_arrear_month_year" Class="form-control date-picker11" runat="server"></asp:TextBox>
                               </div>
                             <div class="col-sm-2 col-xs-12 arrear">
                           <b> To Month :</b><span class="text-red">*</span>
                                <asp:TextBox ID="txt_arrear_monthend" Class="form-control date-picker22" runat="server"></asp:TextBox>
                               </div>
                                 
                        </div>
                        <br /><br />

                         <div class="row">
                            <div class="col-sm-1 col-xs-12">
                                <asp:Button ID="btn_save_arrear" runat="server" CssClass="btn btn-primary"
                                    Text="Process" OnClick="btn_save_arrear_Click" OnClientClick="return Req_validation();" />&nbsp;&nbsp;
                            </div>
                            <div class="col-sm-2 col-xs-12">

                                <asp:DropDownList ID="ddl_emp_xl_type_arrear" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Employee XL</asp:ListItem>
                                    <asp:ListItem Value="1">Without Ac/No XL</asp:ListItem>
                                    <asp:ListItem Value="2">Left Employee XL</asp:ListItem>
                                     <asp:ListItem Value="3">Reliever Employee XL</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-9 col-xs-12">
                                <asp:Button ID="btn_breakup_arrear" runat="server" CssClass="btn btn-large"
                                    Text="Get Employee XL" OnClick="btn_breakup_arrear_Click" OnClientClick="return Req_validation2();" />&nbsp;&nbsp;
                                <asp:Button ID="btn_paid_salary_arrear" runat="server" CssClass="btn btn-large"
                                    Text="Paid Salary" OnClick="btn_paid_salary_arrear_Click" OnClientClick="return Req_validation2();" />&nbsp;&nbsp;
                        <asp:Button ID="btn_approve_arrear" runat="server" CssClass="btn btn-large" Width="20%"
                            Text="Get Annuexure Upload" OnClick="btn_approve_arrear_Click" OnClientClick="return confirm('Are you sure you want to Approve?');" />&nbsp;&nbsp;
                        <asp:Button ID="Button6" runat="server" CssClass="btn btn-danger"
                            Text="CLOSE" OnClick="bntclose_Click" />
                            </div>
                        </div>
                    </div>
                    <div id="menu3">
                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                               <b> Conveyance Type:</b>
                                <asp:DropDownList ID="ddl_con_type" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Conveyance</asp:ListItem>
                                    <asp:ListItem Value="2">Driver Conveyance</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-sm-2 col-xs-12" style="margin-top: 15px">

                                <asp:DropDownList ID="ddl_con_xl" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Employee XL</asp:ListItem>
                                    <asp:ListItem Value="1">Without Ac/No XL</asp:ListItem>
                                    <asp:ListItem Value="2">Left Employee XL</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-8 col-xs-12" style="margin-top: 18px">
                                <asp:Button ID="btn_con_process" runat="server" CssClass="btn btn-primary"
                                    Text="Process" OnClick="btn_con_process_Click" OnClientClick="return Req_validations();" />&nbsp;&nbsp;
                                <asp:Button ID="btn_con_xl" runat="server" CssClass="btn btn-large"
                                    Text="Get Employee XL" OnClick="btn_con_xl_Click" OnClientClick="return Req_validation2();" />&nbsp;&nbsp;
                       
                   <asp:Button ID="btn_conveyance_paid" runat="server" CssClass="btn btn-large"
                       Text="Paid Conveyance" OnClick="btn_conveyance_paid_Click" OnClientClick="return Req_validation2();" />&nbsp;&nbsp;
                       
                        <asp:Button ID="btn_con_approve" runat="server" CssClass="btn btn-large"
                            Text="Payment Approve" OnClick="btn_con_approve_Click" OnClientClick="return   confirm('Are you sure you want to Approve?');" />&nbsp;&nbsp;
                        <asp:Button ID="btn_close" runat="server" CssClass="btn btn-danger"
                            Text="CLOSE" OnClick="bntclose_Click" />
                            </div>
                        </div>
                    </div>
                    <div id="menu5">
                        <div class="row">

                            <div class="col-sm-1 col-xs-3" style="margin-top: 18px">
                                <asp:Button ID="btn_material_process" runat="server" CssClass="btn btn-primary"
                                    Text="Process" OnClick="btn_material_process_Click" OnClientClick="return Req_validation();" />
                            </div>
                            <div class="col-sm-2 col-xs-12" style="margin-top: 15px">
                                <asp:DropDownList ID="ddl_material_xl" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Employee XL</asp:ListItem>
                                    <asp:ListItem Value="1">Without Ac/No XL</asp:ListItem>
                                    <asp:ListItem Value="2">Left Employee XL</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-8 col-xs-12" style="margin-top: 18px">

                                <asp:Button ID="btn_material_xl" runat="server" CssClass="btn btn-large"
                                    Text="Get Employee XL" OnClick="btn_material_xl_Click" OnClientClick="return Req_validation();" />&nbsp;&nbsp;
                       
                   <asp:Button ID="btn_material_paid" runat="server" CssClass="btn btn-large"
                       Text="Paid Material" OnClick="btn_material_paid_Click" OnClientClick="return Req_validation();" />&nbsp;&nbsp;
                       
                        <asp:Button ID="btn_material_approve" runat="server" CssClass="btn btn-large"
                            Text="Payment Approve" OnClick="btn_material_approve_Click" OnClientClick="return   confirm('Are you sure you want to Approve?');" />&nbsp;&nbsp;
                        <asp:Button ID="btn_material_close" runat="server" CssClass="btn btn-danger"
                            Text="CLOSE" OnClick="btn_material_close_Click" />
                            </div>
                        </div>
                    </div>
                    <div id="menu4">
                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>Month :</b><span style="color: red">*</span>
                                <asp:TextBox runat="server" ID="txt_vendor_month" CssClass="form-control date-picker" />
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>Vendor Name:</b><span style="color: red">*</span>
                                <asp:DropDownList ID="ddl_vendorname" runat="server" CssClass="form-control" OnSelectedIndexChanged="vendor_paymentSelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>Purchase Order No:</b><span style="color: red">*</span>
                                <asp:DropDownList ID="ddl_purchase_onum" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_purchase_onum_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Purchase invoice No:</b>
                            <asp:DropDownList ID="ddl_purchase_inv" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                               <b> Bank :</b>
                            <asp:DropDownList ID="ddl_vendor_bank" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Mode Of Payment: </b>     
                                <asp:DropDownList ID="ddl_modeofpayment" runat="server" CssClass="form-control" onchange="vendor_textbox();">
                                    <asp:ListItem Value="1">NEFT</asp:ListItem>
                                    <asp:ListItem Value="2">CHEQUE</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12 hide_text" style="display: none">
                               <b> Cheque Number :</b><span style="color: red">*</span>
                                <asp:TextBox runat="server" ID="txt_check_number" MaxLength="6" Text="0" CssClass="form-control" onkeypress="return isNumber(event);" />
                            </div>
                            <div class="col-sm-2 col-xs-12 hide_textbox" style="display: none">
                                <b>Cheque Date :</b><span style="color: red">*</span>
                                <asp:TextBox runat="server" ID="txt_check_date" CssClass="form-control date-picker" />
                            </div>

                        </div>
                        <br />
                        <br />
                        <div class="row text-center">
                            <asp:Button ID="btn_vendor_save" runat="server" CssClass="btn btn-large" Text=" Process" OnClick="btn_vendor_save_Click" OnClientClick="return vendor_validation();" />&nbsp;&nbsp;
                           <asp:Button ID="btn_vendo" runat="server" CssClass="btn btn-large"
                               Text="Get Vendor XL" OnClick="btn_vendor_Click" />&nbsp;&nbsp;
                             <asp:Button ID="btn_paid_vendor" runat="server" CssClass="btn btn-large" Width="14%"
                                 Text="Paid Vendor Payment" OnClick="btn_paid_vendor_Click" OnClientClick="return vendor_validation();" />&nbsp;&nbsp;
                                   <asp:Button ID="btn_ven_annux" runat="server" CssClass="btn btn-large" Width="14%" OnClick="btn_ven_annux_Click"
                                       Text="Get Annuxure Upload" OnClientClick="return vendor_validation();" />&nbsp;&nbsp;
                        </div>
                    </div>
                    <div id="menu6">

                        <br />
                        <div class="row">
                            <div class="col-sm-1 col-xs-12">
                                <asp:Button ID="btn_rm_process" runat="server" CssClass="btn btn-primary"
                                    Text="Process" OnClick="btn_rm_process_Click" OnClientClick="return Req_validation();" />&nbsp;&nbsp;
                            </div>

                            <div class="col-sm-9 col-xs-12">
                                <asp:Button ID="btn_rm_get" runat="server" CssClass="btn btn-large"
                                    Text="Get Employee XL" OnClick="btn_rm_get_Click" OnClientClick="return Req_validation2();" />&nbsp;&nbsp;
                              <asp:Button ID="btn_rm_paid" runat="server" CssClass="btn btn-large"
                                  Text="Paid Salary" OnClick="btn_rm_paid_Click" OnClientClick="return Req_validation2();" />&nbsp;&nbsp;
                            </div>


                        </div>
                    </div>
                     <div id="menu7">

                        <br />
                        <div class="row">
                            <div class="col-sm-1 col-xs-12">
                                <asp:Button ID="btn_admini_process" runat="server" CssClass="btn btn-primary"
                                    Text="Process" OnClick="btn_admini_process_Click" OnClientClick="return Req_validation();" />&nbsp;&nbsp;
                            </div>

                            <div class="col-sm-9 col-xs-12">
                                <asp:Button ID="btn_admini_get" runat="server" CssClass="btn btn-large"
                                    Text="Get Employee XL" OnClick="btn_admini_get_Click" OnClientClick="return Req_validation2();" />&nbsp;&nbsp;
                              <asp:Button ID="btn_admini_paid" runat="server" CssClass="btn btn-large"
                                  Text="Paid Salary" OnClick="btn_admini_paid_Click" OnClientClick="return Req_validation2();" />&nbsp;&nbsp;
                            </div>


                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-9 col-xs-12"></div>
                        <div class="col-sm-3 col-xs-12" style="margin-top: -350px;">

                            <br />

                            <asp:Panel ID="pending_finance_panel" runat="server"><a data-toggle="modal" href="#pend_attendance"><font color="red"><b><%=pending_attendance%></b></font>Final Bill Not Approve</a></asp:Panel>
                            <asp:Panel ID="approval_finance_panel" runat="server"><a data-toggle="modal" href="#approve_attendance_finance"><font color="red"><b><%=appro_attendannce_finanace%></b></font>Final Bill Approved</a></asp:Panel>
                             <asp:Panel ID="approval_by_finance_panel" runat="server"><a data-toggle="modal" href="#approve_finace"><font color="red"><b><%=finance_approve%></b></font>Approve By Finance</a></asp:Panel>
                             <asp:Panel ID="not_approval_by_finance_panel" runat="server"><a data-toggle="modal" href="#not_approve_finance"><font color="red"><b><%=finance_not_approve%></b></font>Not Approve By Finance</a></asp:Panel>
                             <asp:Panel ID="payment_approve_panel" runat="server"><a data-toggle="modal" href="#payment_approve"><font color="red"><b><%=payment_approve%></b></font>This Branches Payment Approve</a></asp:Panel>
                          
                        </div>
                    </div>

                </div>
                <br />
                <asp:Panel ID="Panel1" runat="server" CssClass="grid-view" ScrollBars="Auto" Visible="false">

                    <asp:GridView ID="gv_fullmonthot" runat="server" ForeColor="#333333" class="table" GridLines="Both">
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

                    </asp:GridView>

                </asp:Panel>
            </div>
        </div>
        <div class="modal fade" id="approve_attendance_finance" role="dialog" data-dismiss="modal">

            <div class="modal-dialog" style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4>Branches  Approve By Finance</h4>
                    </div>

                    <div class="modal-body">
                        <asp:Panel runat="server" CssClass="grid-view">
                            <asp:GridView ID="gv_appr_att_finance" class="table" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false">
                                <Columns>
                                    <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

                                </Columns>
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
        <div class="modal fade" id="payment_approve" role="dialog" data-dismiss="modal">

            <div class="modal-dialog" style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4>This Branch Payment Approve</h4>
                    </div>

                    <div class="modal-body">
                        <asp:Panel ID="Panel2" runat="server" CssClass="grid-view">
                            <asp:GridView ID="gv_payment_approve" class="table" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false">
                                <Columns>
                                    <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

                                </Columns>
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

        <div class="modal fade" id="pend_attendance" role="dialog" data-dismiss="modal">

            <div class="modal-dialog" style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4>Branches Final Bill Not Approve</h4>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:GridView ID="gv_pend_att_finance" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                    <Columns>
                                        <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

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


        <%--// 07-05-2020 komal--%>

         <div class="modal fade" id="approve_finace" role="dialog" data-dismiss="modal">

            <div class="modal-dialog" style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4>This Record Approve By Finance</h4>
                    </div>

                    <div class="modal-body">
                        <asp:Panel ID="Panel3" runat="server" CssClass="grid-view">
                            <asp:GridView ID="gv_approve_finance" class="table" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false">
                                <Columns>
                                    <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

                                </Columns>
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


        <%--// 07-05-2020 komal--%>
        <div class="modal fade" id="not_approve_finance" role="dialog" data-dismiss="modal">

            <div class="modal-dialog" style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4>This Record Not Approve By Finance</h4>
                    </div>

                    <div class="modal-body">
                        <asp:Panel ID="Panel4" runat="server" CssClass="grid-view">
                            <asp:GridView ID="gv_not_approve_finance" class="table" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false">
                                <Columns>
                                    <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" />

                                </Columns>
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

        <%--// end 07-05-2020 komal--%>
</asp:Content>
