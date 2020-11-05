<%@ Page Title="Letters" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Offerletter.aspx.cs" Inherits="Offerletter" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Letters</title>
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

        $(document).ready(function () {

            var table = $('#<%=SearchGridView.ClientID%>,#<%=gv_warring.ClientID%>, #<%=incrementsearch.ClientID%>, #<%=updateincrementgv.ClientID%>, #<%=AppointmentSearch.ClientID%>, #<%=gv_realiving.ClientID%>, #<%=gv_experianceletter.ClientID%> ,#<%=gv_terminal.ClientID%>,#<%=gv_terminal.ClientID%>').DataTable({
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
               .appendTo('#<%=SearchGridView.ClientID%>_wrapper .col-sm-6:eq(0), #<%=incrementsearch.ClientID%>__wrapper .col-sm-6:eq(0), #<%=updateincrementgv.ClientID%>.ClientID%>__wrapper .col-sm-6:eq(0), #<%=AppointmentSearch.ClientID%>.ClientID%>__wrapper .col-sm-6:eq(0), #<%=gv_realiving.ClientID%>.ClientID%>__wrapper .col-sm-6:eq(0), #<%=gv_experianceletter.ClientID%>.ClientID%>__wrapper .col-sm-6:eq(0),#<%=gv_terminal.ClientID%>.ClientID%>__wrapper .col-sm-6:eq(0),#<%=gv_terminal.ClientID%>.ClientID%>__wrapper .col-sm-6:eq(0)');


            $.fn.dataTable.ext.errMode = 'none';

        });
    </script>
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <style>
        .container {
            max-width: 99%;
        }

        .grid-view {
            height: auto;
            max-height: 500px;
            overflow-y: auto;
            overflow-x: hidden;
        }
    </style>
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


        h5 {
            font-weight: bold;
            font-size: 15px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <script>

      
    </script>
    <script type="text/javascript">
        function pageLoad() {
            $('#<%=gv_terminal.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=gv_warring.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=SearchGridView.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=OfferLetter.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=AppoinymentLetter.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_Reliving_Letter.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_Experiance_Letter.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_warning.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_terminate.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_uniform.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                maxDate: 60,
                dateFormat: 'dd/mm/yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });


            $('.date-picker1').datepicker({

                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'yy',
                onClose: function (dateText) {

                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();

                }
            });



            $('.confirm_date').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                maxDate: '+10Y',

                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });

            $(".date-picker").attr("readonly", "true");
            $(".date-picker1").attr("readonly", "true");
        }
        $(document).ready(function () {


            var evt = null;
            isNumber(evt);
            var e = null;
            AllowAlphabet1(e);
        });
        function AllowAlphabet1(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '48') && (keyEntry <= '57')) || ((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '32') || keyEntry == '45' || (keyEntry == '8'))
                return true;
            else {
                // alert('Please Enter Only Character values.');
                return false;
            }
        }

        function isNumber(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    if (charCode == 46) {
                        return true;
                    }
                    return false;
                }


            }
            return true;
        }

        function validateFloatKeyPress(el) {
            var v = parseFloat(el.value);
            el.value = (isNaN(v)) ? '' : v.toFixed(2);
        }

        function validate_Calculation(evt) {
            var txt_1_head = document.getElementById('<%=txtlhead1.ClientID %>');
            var txt_2_head = document.getElementById('<%=txtlhead2.ClientID %>');
            var txt_3_head = document.getElementById('<%=txtlhead3.ClientID %>');
            var txt_4_head = document.getElementById('<%=txtlhead4.ClientID %>');

            var txt_5_head = document.getElementById('<%=txtlhead5.ClientID %>');
            var txt_6_head = document.getElementById('<%=txtlhead6.ClientID %>');
            var txt_7_head = document.getElementById('<%=txtlhead7.ClientID %>');
            var txt_8_head = document.getElementById('<%=txtlhead8.ClientID %>');
            var txt_9_head = document.getElementById('<%=txtlhead9.ClientID %>');

            var txt_10_head = document.getElementById('<%=txtlhead10.ClientID %>');
            var txt_11_head = document.getElementById('<%=txtlhead11.ClientID %>');
            var txt_12_head = document.getElementById('<%=txtlhead12.ClientID %>');
            var txt_13_head = document.getElementById('<%=txtlhead13.ClientID %>');
            var txt_14_head = document.getElementById('<%=txtlhead14.ClientID %>');
            var txt_15_head = document.getElementById('<%=txtlhead15.ClientID %>');


            var txt_quantity = document.getElementById('<%=txtincrementamount.ClientID %>');

            var total = (parseFloat(txt_1_head.value)) + (parseFloat(txt_2_head.value)) + (parseFloat(txt_3_head.value)) + (parseFloat(txt_4_head.value)) + (parseFloat(txt_5_head.value)) + (parseFloat(txt_6_head.value)) + (parseFloat(txt_7_head.value)) + (parseFloat(txt_8_head.value)) + (parseFloat(txt_9_head.value)) + (parseFloat(txt_10_head.value)) + (parseFloat(txt_11_head.value)) + (parseFloat(txt_12_head.value)) + (parseFloat(txt_13_head.value)) + (parseFloat(txt_14_head.value)) + (parseFloat(txt_15_head.value)) + (parseFloat(txt_otrate.value));

            if (parseFloat(total) > parseFloat(txt_quantity.value)) {
                alert("Please Enter Amount Quantity Less Than Increment Amount.");

                txt_2_head.focus();

                return false;
            }

            return true;
        }




        function Req_validation() {
            var txt_Emp_Name = document.getElementById('<%=txt_Emp_Name.ClientID %>');
            if (txt_Emp_Name.value == "") {
                alert("Please Enter Employee Name");
                txt_Emp_Name.focus();
                return false;
            }
            var txt_joining_Date = document.getElementById('<%=txt_joining_Date.ClientID %>');
            if (txt_joining_Date.value == "") {
                alert("Please Enter Joining Date");
                txt_joining_Date.focus();
                return false;
            }
            var txt_address = document.getElementById('<%=txt_address.ClientID %>');
            if (txt_address.value == "") {
                alert("Please Enter Address");
                txt_address.focus();
                return false;
            }
            var txt_grade_code = document.getElementById('<%=txt_grade_code.ClientID %>');
            if (txt_grade_code.value == "") {
                alert("Please Enter Designation");
                txt_grade_code.focus();
                return false;
            }
            var txt_per_month_salary = document.getElementById('<%=txt_per_month_salary.ClientID %>');
            if (txt_per_month_salary.value == "") {
                alert("Please Enter Sallary Per Month");
                txt_per_month_salary.focus();
                return false;
            }
            var ddlstate = document.getElementById('<%=ddlstate.ClientID %>');
            var Selected_ddlstate = ddlstate.options[ddlstate.selectedIndex].text;
            if (Selected_ddlstate == "Select") {
                alert("Please Select State");
                ddlstate.focus();
                return false;
            }
            var ddlcity = document.getElementById('<%=ddlcity.ClientID %>');
            var Selected_ddlcity = ddlcity.options[ddlcity.selectedIndex].text;
            if (Selected_ddlcity == "Select City") {
                alert("Please Select City");
                ddlcity.focus();
                return false;
            }
        }






        window.onfocus = function () {

            $.unblockUI();
        }

        function openWindow() {
            window.open("html/OfferLetter.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
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
    </script>

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Letters</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
            <%--<div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Letters Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd;
                 border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">
                
                    <div class="row">
                    <asp:Button ID="OfferLetter" runat="server" class="btn btn-primary" OnClick="btn_OfferLetter_Click" Text=" Offer Letter" Width="15%" />
                    &nbsp; &nbsp;
                    <asp:Button ID="AppoinymentLetter" runat="server" class="btn btn-primary" OnClick="btn_AppoinymentLetter_Click" Text=" Appointment Letter" Width="15%"/>
                    &nbsp;&nbsp;<asp:Button ID="joiningletter" runat="server" class="btn btn-primary" OnClick="btn_joiningletter_Click" Text=" Joining Letter" Width="15%" />
                    &nbsp;&nbsp;<asp:Button ID="btn_incrementLetter" runat="server" class="btn btn-primary" OnClick="btn_incrementLetter_Click" Text=" Increment Letter" Visible="false" Width="15%"/>
                    &nbsp;&nbsp;<asp:Button ID="btn_Reliving_Letter" runat="server" class="btn btn-primary" OnClick="btn_Reliving_Letter_Click" Text=" Relieving Letter" Width="15%"/>
                    &nbsp;&nbsp;<asp:Button ID="btn_Experiance_Letter" runat="server" class="btn btn-primary" OnClick="btn_Experiance_Letter_Click" Text=" Experience Letter" Width="15%"/>
                    &nbsp;&nbsp;<asp:Button ID="btn_warning" runat="server" class="btn btn-primary" Text="Warning Letter" OnClick="btn_warning_Letter_Click" Width="15%"/>
                    </div>
                    <br />
                    <div class="row">
                    &nbsp;&nbsp;<asp:Button ID="btn_terminate" runat="server" class="btn btn-primary" Text="Termination Letter" OnClick="btn_terminate_click" Width="15%"/>
                    &nbsp;&nbsp;<asp:Button ID="btn_uniform" runat="server" class="btn btn-primary" Text="Uniform Letter" OnClick="unoform_click" Width="15%"/>
                    &nbsp;&nbsp;<asp:Button ID="btnclose1" runat="server" class="btn btn-danger" OnClick="btnclose1_Click" Text="Close" />
                </div>
                    </div>
               <br />
                    <br />

                <asp:Panel runat="server" ID="panelnew" CssClass="panel panel-default">
                    <div class="panel-heading text-center">
                        <div style="font-size: small"><b>OFFER LETTER</b></div>

                    </div>
                    <div class="panel-body">
                        <br />
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                                <%-- <label for="ex2">--%>
                                <b> Employee Code :</b>
                                 <%--</label>--%>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_emp_code" runat="server" class="form-control" ReadOnly="true" onkeypress="return AllowAlphabet1(event)" MaxLength="10"></asp:TextBox>
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                                <%-- <label for="ex2">--%>
                                <b> Employee Name :</b>
                                 <%--</label>--%>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_Emp_Name" runat="server" class="form-control" onkeypress="return AllowAlphabet1(event)" MaxLength="30"></asp:TextBox>
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                               <b> Joining Date :</b>
                            </div>
                            <div class="col-sm-3 ">
                                <asp:TextBox ID="txt_joining_Date" runat="server" onkeypress="return isNumber(event)" class="form-control confirm_date" Style="display: inline"></asp:TextBox>



                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                              <b>  Address :</b>
                            </div>

                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_address" runat="server" class="form-control" onkeypress="return AllowAlphabet_address(event);"></asp:TextBox>
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                               <b> Designation :</b>
                            </div>

                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_grade_code" runat="server" class="form-control " onKeyPress="return AllowAlphabet1(event)" MaxLength="30"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                               <b> Salary Per Month :</b>
                            </div>

                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_per_month_salary" runat="server" Width="95%" class="form-control " onkeypress="return isNumber(event)" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                              <b>  State :</b>
                            </div>

                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="True" class="form-control" DataTextField="STATE_NAME" DataValueField="STATE_NAME" OnSelectedIndexChanged="get_city_list">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>

                                <div class="row">
                                    <div class="col-sm-3"></div>
                                    <div class="col-sm-2 text-right">
                                       <b> City :</b>
                                    </div>

                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlcity" runat="server" class="form-control" Width="100%"></asp:DropDownList>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <br />
                        <div class="row text-center">
                            <%--    <asp:Button ID="btn_new" runat="server" class="btn btn-primary" OnClick="btn_new_Click"  Text=" New " />--%>
                            <asp:Button ID="btn_add" runat="server" class="btn btn-primary" OnClick="btn_add_Click" OnClientClick="return Req_validation();" Text=" Save " />
                            <asp:Button ID="btn_print" runat="server" class="btn btn-primary" OnClick="btn_print_Click" Text=" Print " />
                            <asp:Button ID="btn_delete" runat="server" class="btn btn-primary" OnClick="btn_delete_Click" Text=" Delete " />
                            <asp:Button ID="btn_move_to_app" runat="server" class="btn btn-primary" OnClick="btn_move_to_appointment" Text=" Move to Appoinment " Visible="false" />
                            <cc1:ConfirmButtonExtender ID="btn_delete_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure you want to delete record?"
                                Enabled="True" TargetControlID="btn_delete"></cc1:ConfirmButtonExtender>
                            <%--<asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" onclick="btn_cancel_Click" Text=" Clear "   />
                                   <asp:Button ID="btn_export_exel" runat="server" class="btn btn-primary"   Text="Export To Excel" OnClick="btnexporttoexceldesignation_Click"  /> --%>

                            <asp:Button ID="btnclose" runat="server" class="btn btn-danger" OnClick="btnclose_Click" Text="Close" />
                        </div>
                        <br />

                        <asp:Panel ID="Panel2" runat="server" CssClass="grid-view" class="panel-body">
                            <asp:SqlDataSource ID="SqlDataSource" runat="server"
                                ConnectionString="<%$ ConnectionStrings:CELTPAYConnectionString %>" ProviderName="<%$ ConnectionStrings:celtpayConnectionString.ProviderName %>"
                                SelectCommand="SELECT comp_code, EMP_CODE, EMP_NAME, JOINING_DATE,ADDRESS,GRADE_CODE,SALARY_PER_MONTH, STATE, CITY FROM offer_letter WHERE (comp_code = @comp_code) ">
                                <SelectParameters>
                                    <asp:SessionParameter Name="comp_code" SessionField="comp_code" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <asp:GridView ID="SearchGridView" class="table" AutoGenerateColumns="False" runat="server"
                                OnRowDataBound="SearchGridView_RowDataBound" OnPreRender="SearchGridView_PreRender"
                                Width="100%" OnSelectedIndexChanged="SearchGridView_SelectedIndexChanged">

                                <%--OnRowEditing="RowEditing",OnRowDeleting="GridView1_RowDeleting"-    --%>
                                <Columns>
                                    <asp:CommandField HeaderText="-&gt;" SelectText="-&gt;" ShowSelectButton="True" />

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ID" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("EMP_CODE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Employee Name">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Emp_Name" runat="server" Text='<%# Eval("EMP_NAME")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Joining Date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Joining_Date" runat="server" Text='<%# Eval("JOINING_DATE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Address">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_address" runat="server" Text='<%# Eval("ADDRESS")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Designation">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_designation" runat="server" Text='<%# Eval("GRADE_CODE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salary Per Month">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_salary" runat="server" Text='<%# Eval("SALARY_PER_MONTH")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_state" runat="server" Text='<%# Eval("STATE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_city" runat="server" Text='<%# Eval("CITY")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                </Columns>

                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
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
                <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-default" Visible="false">
                    <div class="panel-heading text-center">
                        <div style="font-size: small"><b>JOINING LETTER</b></div>

                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-2"></div>

                            <div class="col-sm-2">
                               <b> Client Name:</b>
                                <asp:DropDownList ID="ddl_joining_client" runat="server" CssClass="form-control" OnSelectedIndexChanged="btn_joining_client_list" AutoPostBack="true"></asp:DropDownList>

                            </div>
                            <div class="col-sm-2 ">
                               <b> State Name:</b>
                                 <asp:DropDownList ID="ddl_joining_state" runat="server" CssClass="form-control" OnSelectedIndexChanged="btn_joining_state_list" AutoPostBack="true"></asp:DropDownList>


                            </div>
                            <div class="col-sm-2 ">
                               <b> Branch Name:</b>
                                 <asp:DropDownList ID="ddl_joining_unit" runat="server" CssClass="form-control" OnSelectedIndexChanged="btn_joining_unit_list" AutoPostBack="true"></asp:DropDownList>


                            </div>
                            <div class="col-sm-2 ">
                               <b> Employee Name:</b>
                                 <asp:DropDownList ID="ddl_joining_employee" runat="server" CssClass="form-control"></asp:DropDownList>


                            </div>
                        </div>
                        <br />
                        <br />
                        <br />
                        <div class="row text-center">
                            <asp:Button runat="server" ID="btn_save" Text="Print" CssClass="btn btn-primary" OnClick="btn_joiningletter_print_click" />
                            <asp:Button runat="server" ID="btn_close" Text="Close" CssClass="btn btn-danger" OnClick="btn_close_click" />
                        </div>
                    </div>
                    <br />

                </asp:Panel>
                <asp:Panel ID="IncrementAmountpanel" runat="server" CssClass="panel panel-default">
                    <div class="panel-heading text-center">
                        <div style="font-size: small"><b>INCREMENT LETTER</b></div>

                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-4 col-xs-12"></div>

                            <div class="col-sm-2 col-xs-12">
                              <b>  Client Name :</b>
                                <asp:DropDownList ID="ddl_client_increment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_client_name_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Branch Name :</b>
                                <asp:DropDownList ID="ddl_branch_increment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_branch_name_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtincrementempcode" runat="server" class="form-control" onkeypress="return AllowAlphabet1(event)" MaxLength="10" Style="display: none;"></asp:TextBox>
                            </div>





                            <%--<div class="col-sm-2 text-right">
                            <%-- <label for="ex2">--%>
                            <%--   Employee Name :--%>
                            <%--</label>--%>
                            <%--</div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtIncrementname" runat="server" class="form-control" onkeypress="return AllowAlphabet1(event)" MaxLength="30"></asp:TextBox>
                        </div>--%>

                            <asp:Button ID="btn_Increment" runat="server" class="btn btn-primary" OnClick="btn_Increment_Click" Text="Search" Visible="false" />
                        </div>

                        <br />
                        <asp:Panel ID="incrementgvpanel" runat="server" CssClass="grid-view" class="panel-body">
                            <asp:GridView ID="incrementsearch" class="table" AutoGenerateColumns="False" runat="server"
                                OnRowDataBound="Increment_OnRowDataBound" Width="100%" OnPreRender="incrementsearch_PreRender"
                                OnSelectedIndexChanged="incrementsearch_SelectedIndexChanged">
                                <%--OnRowEditing="RowEditing",OnRowDeleting="GridView1_RowDeleting"-    --%>
                                <Columns>




                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ID" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_increment_id" runat="server" Text='<%# Eval("EMP_CODE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Employee Name">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Emp_Name2" runat="server" Text='<%# Eval("EMP_NAME")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Designation">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_designation2" runat="server" Text='<%# Eval("GRADE_CODE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_amount" runat="server" Text='<%# Eval("INCREMETN_AMOUNT")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
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

                        <br />

                        <asp:Panel ID="updateincrement" runat="server">
                            <div class="row">

                                <div class="col-sm-2">
                                    <%-- <label for="ex2">--%>
                                <b> Employee Code :</b>
                                 <%--</label>--%>

                                    <asp:TextBox ID="IEmp_code" runat="server" class="form-control" ReadOnly="true" onkeypress="return AllowAlphabet1(event)" MaxLength="10"></asp:TextBox>
                                </div>



                                <div class="col-sm-3">
                                    <%-- <label for="ex2">--%>
                                <b> Employee Name :</b>
                                 <%--</label>--%>
                                    <asp:TextBox ID="IEmp_Name" runat="server" class="form-control" onkeypress="return AllowAlphabet1(event)" MaxLength="30" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-sm-3 ">
                                    <%-- <label for="ex2">--%>
                                <b> Designation :</b>
                                 <%--</label>--%>
                                    <%--<asp:TextBox ID="txtdesination" runat="server" class="form-control"  onkeypress="return AllowAlphabet1(event)" MaxLength="30" ReadOnly="true"></asp:TextBox>--%>

                                    <asp:DropDownList ID="txtdesination" runat="server" onkeypress="return AllowAlphabet1(event,this);" CssClass="form-control text_box">
                                    </asp:DropDownList>
                                </div>


                                <div class="col-sm-2">
                                    <%-- <label for="ex2">--%>
                               <b>  Year :</b>
                                 <%--</label>--%>
                                    <asp:TextBox ID="txtyear" runat="server" class="form-control date-picker1" onkeypress="return AllowAlphabet1(event)" MaxLength="30"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 ">
                                    <%-- <label for="ex2">--%>
                               <b> Increment Amount :</b>
                                 <%--</label>--%>
                                    <asp:TextBox ID="txtincrementamount" runat="server" class="form-control" onkeypress="return AllowAlphabet1(event)" OnTextChanged="txtincrementamount_TextChanged" AutoPostBack="true" MaxLength="30"></asp:TextBox>
                                </div>
                            </div>




                            <br />

                            <div class="row ">
                                <div class="col-sm-2 col-xs-12 ">
                                    <asp:Label ID="lblbasic" runat="server" Text="BASIC"></asp:Label>

                                    <asp:TextBox ID="txtlhead1" MaxLength="15" runat="server" class="text_box" onkeypress="return isNumber(event)" AutoPostBack="True" CssClass="form-control" meta:resourcekey="txtlhead1Resource1" Width="100%" ReadOnly="true" onkeyup="return validate_Calculation(event)">0</asp:TextBox></td>
                                </div>
                                <div class="col-sm-2 col-xs-12 ">
                                    <asp:Label ID="lbllhead2" runat="server" meta:resourcekey="lblhead2Resource1" Text="HRA"></asp:Label>

                                    <asp:TextBox ID="txtlhead2" runat="server" MaxLength="15" class="text_box" onkeypress="return isNumber(event)" AutoPostBack="True" CssClass="form-control" meta:resourcekey="txtlhead2Resource1" Width="100%" ReadOnly="true" onkeyup="return validate_Calculation(event)">0</asp:TextBox></td>
                                </div>
                                <div class="col-sm-2 col-xs-12 ">
                                    <asp:Label ID="lblhead3" runat="server" meta:resourcekey="lblhead3Resource1" Text="CHILDREN EDU.ALLOW"></asp:Label>

                                    <asp:TextBox ID="txtlhead3" runat="server" MaxLength="15" class="text_box" onkeypress="return isNumber(event)" AutoPostBack="True" CssClass="form-control" meta:resourcekey="txtlhead3Resource1" Width="100%" onkeyup="return validate_Calculation(event)">0</asp:TextBox></td>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <asp:Label ID="lblhead4" runat="server" meta:resourcekey="lblhead4Resource1" Text="CONVEY.ALLOW"></asp:Label>

                                    <asp:TextBox ID="txtlhead4" runat="server" MaxLength="15" onkeypress="return isNumber(event)" AutoPostBack="True" class="form-control text_box" meta:resourcekey="txtlhead4Resource1" Width="100%" ReadOnly="true" onkeyup="return validate_Calculation(event)">0</asp:TextBox></td>
                                </div>
                                <div class="col-sm-2 col-xs-12 ">
                                    <asp:Label ID="lblhead5" runat="server" meta:resourcekey="lblhead5Resource1" Text="MEDICAL ALLOW"></asp:Label>


                                    <asp:TextBox ID="txtlhead5" runat="server" MaxLength="15" onkeypress="return isNumber(event)" AutoPostBack="True" class="form-control text_box" Width="100%"
                                        meta:resourcekey="txtlhead5Resource1" onkeyup="return validate_Calculation(event)">0</asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <asp:Label ID="lblhead6" runat="server" meta:resourcekey="lblhead6Resource1" Text=" OTHER ALLOW"></asp:Label>

                                    <asp:TextBox ID="txtlhead6" runat="server" MaxLength="15" onkeypress="return isNumber(event)" AutoPostBack="True" class="form-control text_box" Width="100%"
                                        meta:resourcekey="txtlhead6Resource1" onkeyup="return validate_Calculation(event)">0</asp:TextBox>
                                </div>

                            </div>

                            <div class="row">
                                <br />
                                <div class="col-sm-2 col-xs-12 ">
                                    <asp:Label ID="lblhead7" runat="server" meta:resourcekey="lblhead7Resource1" Text=" BONOUS"></asp:Label>

                                    <asp:TextBox ID="txtlhead7" runat="server" MaxLength="15" onkeypress="return isNumber(event)" AutoPostBack="True" class="form-control text_box" Width="100%"
                                        meta:resourcekey="txtlhead7Resource1" onkeyup="return validate_Calculation(event)">0</asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12 ">
                                    <asp:Label ID="lblhead8" runat="server" meta:resourcekey="lblhead8Resource1" Text="EHEAD8"></asp:Label>

                                    <asp:TextBox ID="txtlhead8" runat="server" MaxLength="15" onkeypress="return isNumber(event)" AutoPostBack="True" class="form-control text_box" Width="100%"
                                        meta:resourcekey="txtlhead8Resource1" onkeyup="return validate_Calculation(event)">0</asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12 ">
                                    <asp:Label ID="lblhead9" runat="server" meta:resourcekey="lblhead9Resource1" Text="EHEAD9"></asp:Label>

                                    <asp:TextBox ID="txtlhead9" runat="server" MaxLength="15" onkeypress="return isNumber(event)" AutoPostBack="True" class="form-control text_box" Width="100%"
                                        meta:resourcekey="txtlhead9Resource1" onkeyup="return validate_Calculation(event)">0</asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12 ">
                                    <asp:Label ID="lblhead10" runat="server" MaxLength="15" meta:resourcekey="lblhead10Resource1" Text="EHEAD10"></asp:Label>

                                    <asp:TextBox ID="txtlhead10" runat="server" MaxLength="15" onkeypress="return isNumber(event)" AutoPostBack="True" class="form-control text_box"
                                        Width="100%" meta:resourcekey="txtlhead10Resource1" onkeyup="return validate_Calculation(event)">0</asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12 ">
                                    <asp:Label ID="lblhead11" runat="server" meta:resourcekey="lblhead11Resource1" Text="EHEAD11"></asp:Label>

                                    <asp:TextBox ID="txtlhead11" runat="server" AutoPostBack="True" MaxLength="15" class="form-control text_box"
                                        Width="100%" onkeypress="return isNumber(event)"
                                        meta:resourcekey="txtlhead11Resource1" onkeyup="return validate_Calculation(event)">0</asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12 ">
                                    <asp:Label ID="lblhead12" runat="server" meta:resourcekey="lblhead12Resource1" Text="EHEAD12"></asp:Label>

                                    <asp:TextBox ID="txtlhead12" runat="server" AutoPostBack="True" MaxLength="15" class="form-control text_box"
                                        Width="100%" onkeypress="return isNumber(event)"
                                        meta:resourcekey="txtlhead12Resource1" onkeyup="return validate_Calculation(event)">0</asp:TextBox>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-sm-2 col-xs-12 ">
                                    <asp:Label ID="lblhead13" runat="server" meta:resourcekey="lblhead13Resource1" Text="EHEAD13"></asp:Label>

                                    <asp:TextBox ID="txtlhead13" runat="server" AutoPostBack="True" MaxLength="15" class="form-control text_box"
                                        Width="100%" onkeypress="return isNumber(event)"
                                        meta:resourcekey="txtlhead13Resource1" onkeyup="return validate_Calculation(event)">0</asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12 ">
                                    <asp:Label ID="lblhead14" runat="server" meta:resourcekey="lblhead14Resource1" Text="EHEAD814"></asp:Label>

                                    <asp:TextBox ID="txtlhead14" runat="server" AutoPostBack="True" class="form-control text_box"
                                        Width="100%" onkeypress="return isNumber(event)"
                                        meta:resourcekey="txtlhead14Resource1" MaxLength="15" onkeyup="return validate_Calculation(event)">0</asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12 ">
                                    <asp:Label ID="lblhead15" runat="server" meta:resourcekey="lblhead15Resource1" Text="EHEAD15"></asp:Label>

                                    <asp:TextBox ID="txtlhead15" runat="server" AutoPostBack="True" MaxLength="15" class="form-control text_box"
                                        Width="100%" onkeypress="return isNumber(event)"
                                        meta:resourcekey="txtlhead15Resource1" onkeyup="return validate_Calculation(event)">0</asp:TextBox>
                                </div>
                                <%-- <div class="col-sm-2 col-xs-12 ">
                                                    <asp:Label ID="lblotrate" runat="server" meta:resourcekey="lblhead15Resource1" Text="OTRATE"></asp:Label>

                                                    <asp:TextBox ID="txtotrate" runat="server" AutoPostBack="True" MaxLength="15" class="form-control text_box"
                                                        Width="100%" onkeypress="return isNumber(event)" onkeyup="return validate_Calculation(event)" 
                                                        meta:resourcekey="txtlhead15Resource1">0</asp:TextBox>
                                                </div>--%>
                            </div>

                            <br />

                            <div class="row">
                                <div class="col-lg-5"></div>
                                <asp:Button ID="btnincrementsave" runat="server" class="btn btn-primary" OnClick="btnincrementsave_Click" Text=" Save " />
                                <asp:Button ID="brnclear2" runat="server" Class="btn btn-primary" OnClick="brnclear2_Click" Text="Clear" />
                                <asp:Button ID="incrementclose" runat="server" Class="btn btn-danger" OnClick="incrementclose_Click" Text="Close" />

                            </div>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="panelupdaeincrementamt" runat="server" CssClass="grid-view" class="panel-body">
                            <asp:GridView ID="updateincrementgv" class="table" AutoGenerateColumns="False" runat="server" OnPreRender="updateincrementgv_PreRender" OnRowDataBound="panelupdaeincrementamt_OnRowDataBound" Width="100%">
                                <%--OnRowEditing="RowEditing",OnRowDeleting="GridView1_RowDeleting"-    --%>
                                <Columns>




                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ID" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_updateincrement_id" runat="server" Text='<%# Eval("EMP_CODE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Employee Name">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_updateEmp_Name2" runat="server" Text='<%# Eval("EMP_NAME")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Designation">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_updatedesignation2" runat="server" Text='<%# Eval("GRADE_CODE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_updateyear" runat="server" Text='<%# Eval("YEAR")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IncrementAmount">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_updateincrementamt" runat="server" Text='<%# Eval("INCREMETN_AMOUNT")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
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

                <asp:Panel ID="appoimentpanel" runat="server" CssClass="panel panel-default">
                    <div class="panel-heading text-center">
                        <div style="font-size: small"><b>APPOINTMENT LETTER</b></div>

                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-2"></div>
                            <div class="col-sm-3 text-right">
                                <%--<label for="ex2">
                                 Employee Code/Name :
                                 </label>--%>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="appoi_emp_id" runat="server" class="form-control" onkeypress="return AllowAlphabet1(event)" MaxLength="10" Style="display: none;"></asp:TextBox>
                            </div>





                            <asp:Button ID="btn_APPOINMENTSEARCH" runat="server" class="btn btn-primary" OnClick="btn_APPOINMENTSEARCH_Click" Text="Search" Visible="false" />
                        </div>

                        <br />
                        <asp:Panel ID="panelapposearch" runat="server" CssClass="grid-view" class="panel-body">
                            <asp:GridView ID="AppointmentSearch" class="table" AutoGenerateColumns="False" runat="server"
                                OnRowDataBound="Appoint_OnRowDataBound" Width="100%" OnPreRender="AppointmentSearch_PreRender"
                                OnSelectedIndexChanged="AppointmentSearch_SelectedIndexChanged">
                                <%--OnRowEditing="RowEditing",OnRowDeleting="GridView1_RowDeleting"-    --%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit">

                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:LinkButton ID="lnkPrint" runat="server" Text="PRINT" OnClick="AppointmentSearch_SelectedIndexChanged" CommandName="Print" ToolTip="Print"
                                                    CommandArgument=''> </asp:LinkButton>
                                                <%--<asp:ImageButton ID="ButtonDelete" Visible="false" runat="server"   ImageUrl="~/Images/edit.png" Width="15px" Height="15px" CommandName="Edit" ToolTip="Edit"  OnClick="DepartmentGridView_SelectedIndexChanged"/>--%>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ID" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_appointment_id" runat="server" Text='<%# Eval("EMP_CODE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Employee Name">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Emp_Name1" runat="server" Text='<%# Eval("EMP_NAME")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Joining Date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Joining_Date1" runat="server" Text='<%# Eval("JOINING_DATE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Designation">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_designation1" runat="server" Text='<%# Eval("GRADE_CODE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WORKING STATE">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_location" runat="server" Text='<%# Eval("WORKING_STATE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="WORKING CITY">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_location_city" runat="server" Text='<%# Eval("WORKING_CITY")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="salary_per_month">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_birthdaydate" runat="server" Text='<%# Eval("SALARY_PER_MONTH")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
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



                <asp:Panel ID="Panel_Realiving" runat="server" CssClass="panel panel-default">
                    <div class="panel-heading text-center">
                        <div style="font-size: small"><b>RELIEVING LETTER</b></div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-4 col-xs-12"></div>

                            <div class="col-sm-2 col-xs-12">
                                <b>Client Name :</b>
                                <asp:DropDownList ID="ddl_client_reliev" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_client_reliev_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Branch Name :</b>
                                <asp:DropDownList ID="ddl_branch_reliev" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_branch_reliev_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txt_reliving_Empid" runat="server" class="form-control" onkeypress="return AllowAlphabet1(event)" MaxLength="10" Style="display: none"></asp:TextBox>
                            </div>





                            <%-- <div class="col-sm-2 text-right">
                            <%-- <label for="ex2">--%>
                            <%-- Employee Name :--%>
                            <%--</label>--%>
                            <%--   </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="appoi_emp_name" runat="server" class="form-control" onkeypress="return AllowAlphabet1(event)" MaxLength="30"></asp:TextBox>
                        </div>--%>

                            <asp:Button ID="btn_realivingSearch" runat="server" class="btn btn-primary" OnClick="btn_realivingSearch_Click" Text="Search" Visible="false" />
                        </div>

                        <br />
                        <asp:Panel ID="panel_relivingSearch" runat="server" CssClass="grid-view" class="panel-body">
                            <asp:GridView ID="gv_realiving" class="table" AutoGenerateColumns="False"
                                runat="server" OnRowDataBound="realiving_OnRowDataBound" Width="100%"
                                OnSelectedIndexChanged="gv_realiving_SelectedIndexChanged" OnPreRender="gv_realiving_PreRender">
                                <%--OnRowEditing="RowEditing",OnRowDeleting="GridView1_RowDeleting"-    --%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit">

                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:LinkButton ID="lnkPrint" runat="server" Text="PRINT" OnClick="gv_realiving_SelectedIndexChanged" CommandName="Print" ToolTip="Print"
                                                    CommandArgument=''> </asp:LinkButton>
                                                <%--<asp:ImageButton ID="ButtonDelete" Visible="false" runat="server"   ImageUrl="~/Images/edit.png" Width="15px" Height="15px" CommandName="Edit" ToolTip="Edit"  OnClick="DepartmentGridView_SelectedIndexChanged"/>--%>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ID" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_realiving_id" runat="server" Text='<%# Eval("EMP_CODE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Employee Name">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Emp_Name1" runat="server" Text='<%# Eval("EMP_NAME")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Joining Date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Joining_Date1" runat="server" Text='<%# Eval("JOINING_DATE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Designation">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_designation1" runat="server" Text='<%# Eval("GRADE_CODE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LOCATION">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_location" runat="server" Text='<%# Eval("LOCATION")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Birth_Date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_birthdaydate" runat="server" Text='<%# Eval("BIRTH_DATE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
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

                <asp:Panel ID="panel_Experiance_Letter" runat="server" CssClass="panel panel-default">
                    <div class="panel-heading text-center">
                        <div style="font-size: small"><b>EXPERIENCE LETTER</b></div>
                    </div>
                    <div class="panel-body">
                        <%--<asp:UpdatePanel runat="server">
                            <ContentTemplate>--%>
                        <div class="row">
                            <div class="col-sm-4 col-xs-12"></div>

                            <div class="col-sm-2 col-xs-12">
                               <b> Client Name :</b>
                                        <asp:DropDownList ID="ddl_client_experiance" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_client_experiance_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Branch Name :</b>
                                        <asp:DropDownList ID="ddl_branch_experiance" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_branch_experiance_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="TextBox1" runat="server" class="form-control" onkeypress="return AllowAlphabet1(event)" MaxLength="10" Style="display: none;"></asp:TextBox>
                            </div>





                            <%-- <div class="col-sm-2 text-right">
                                    <%-- <label for="ex2">--%>
                            <%-- Employee Name :--%>
                            <%--</label>--%>
                            <%--   </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="appoi_emp_name" runat="server" class="form-control" onkeypress="return AllowAlphabet1(event)" MaxLength="30"></asp:TextBox>
                                </div>--%>

                            <%-- <asp:Button ID="btn_Experiancesearch_Letter" runat="server" class="btn btn-primary" onclick="btn_Experiancesearch_Letter_Click"   Text="Search" /> --%>
                        </div>
                        <br />
                        <asp:Panel ID="panem_exp_letter" runat="server" CssClass="grid-view" class="panel-body">
                            <asp:GridView ID="gv_experianceletter" class="table" AutoGenerateColumns="False" runat="server"
                                OnRowDataBound="expering_OnRowDataBound" OnPreRender="gv_experianceletter_PreRender"
                                Width="100%" OnSelectedIndexChanged="gv_Experiance_SelectedIndexChanged">
                                <%--OnRowEditing="RowEditing",OnRowDeleting="GridView1_RowDeleting"-    --%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit">

                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:LinkButton ID="lnkPrint" runat="server" Text="PRINT" OnClick="gv_Experiance_SelectedIndexChanged" CommandName="Print" ToolTip="Print"
                                                    CommandArgument=''> </asp:LinkButton>
                                                <%--<asp:ImageButton ID="ButtonDelete" Visible="false" runat="server"   ImageUrl="~/Images/edit.png" Width="15px" Height="15px" CommandName="Edit" ToolTip="Edit"  OnClick="DepartmentGridView_SelectedIndexChanged"/>--%>
                                            </div>
                                        </ItemTemplate>

                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ID" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_realiving_id" runat="server" Text='<%# Eval("EMP_CODE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Employee Name">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Emp_Name1" runat="server" Text='<%# Eval("EMP_NAME")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Joining Date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Joining_Date1" runat="server" Text='<%# Eval("JOINING_DATE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Designation">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_designation1" runat="server" Text='<%# Eval("GRADE_CODE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LOCATION">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_location" runat="server" Text='<%# Eval("LOCATION")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Birth_Date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_birthdaydate" runat="server" Text='<%# Eval("BIRTH_DATE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
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

                        <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="panel_warring" CssClass="panel panel-default">
                    <div class="panel-heading text-center">
                        <div style="font-size: small"><b>WARNING LETTER</b></div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-2 col-xs-12"></div>

                            <asp:TextBox ID="txt_emp_id_warn" runat="server" Style="display: none" CssClass="form-control"></asp:TextBox>

                            <div class="col-sm-2 col-xs-12">
                              <b>  Client Name :</b>
                                <asp:DropDownList ID="ddl_client_warn" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Branch Name :</b>
                                <asp:DropDownList ID="ddl_branch_warn" runat="server" OnSelectedIndexChanged="ddl_unit_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Employee Name :</b>
                                <asp:DropDownList ID="ddl_emp_warn" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="select_count">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-12"></div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Warning Date :</b>
                               <asp:TextBox ID="txt_warn_date" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                            </div>

                            <div class="col-sm-2 col-xs-12">
                               <b> Count :</b>
                                   <asp:TextBox ID="txt_count" runat="server" CssClass="form-control" onkeypress="return AllowAlphabet_address(event);"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Reason :</b>
                               <asp:TextBox ID="txt_resion_warn" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" onkeypress="return AllowAlphabet_address(event);"></asp:TextBox>
                            </div>
                        </div>

                        <br />
                        <div class="row text-center">
                            <asp:Button ID="btn_save_warn" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btn_save_warn_Click" />
                            <asp:Button ID="btn_close_warn" runat="server" CssClass="btn btn-danger" Text="Close" OnClick="btn_close_warn_click" />
                            <asp:Button ID="btn_move_to_terminate" runat="server" CssClass="btn btn-primary" Text="Move To Terminate" Visible="false" OnClick="btn_move_to_terminate_Click" />
                        </div>
                        <br />
                        <asp:Panel ID="panel_gv_earring" runat="server">


                            <asp:GridView ID="gv_warring" runat="server" class="table" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" OnRowDataBound="gv_warring_RowDataBound" OnSelectedIndexChanged="gv_warring_selectedIndexChange" OnPreRender="updatewarring_PreRender">
                                <%--OnSelectedIndexChanged="gv_warring_SelectedIndexChanged"--%>

                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="True" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />

                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                <Columns>
                                    <asp:CommandField HeaderText="-&gt;" SelectText="-&gt;" />
                                    <asp:TemplateField HeaderText="Sr. No." Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_wsrnumber" Visible="false" runat="server" Text='<%# Eval("Id")%>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNamewar" runat="server" Text='<%# Eval("Emp_Code")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WARRING_DATE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblwdate" runat="server" Text='<%# Eval("warring_date")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REASON">
                                        <ItemTemplate>
                                            <asp:Label ID="lblreason" runat="server" Text='<%# Eval("Reason")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="COUNT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcount" runat="server" Text='<%# Eval("count")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel_terminate" runat="server" CssClass="panel panel-default">
                    <div class="panel-heading text-center">
                        <div style="font-size: small"><b>TERMINATION LETTER</b></div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-2 col-xs-12"></div>

                            <div class="col-sm-2 col-xs-12">
                               <b> Client Name :</b>
                                <asp:DropDownList ID="ddl_client_term" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_client1_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Branch Name :</b>
                                <asp:DropDownList ID="ddl_unit_term" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_unit1_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Employee Name :</b>
                                <asp:DropDownList ID="ddl_emp_term" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <br />
                                <asp:TextBox ID="txt_id_term" runat="server" CssClass="form-control" Style="display: none"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-12"></div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Terminate Date :</b>
                               <asp:TextBox ID="txt_date_term" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                            </div>


                            <div class="col-sm-2 col-xs-12">
                               <b> Reason :</b>
                               <asp:TextBox ID="txt_resion_term" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" onkeypress="return AllowAlphabet_address(event);"></asp:TextBox>
                            </div>
                        </div>

                        <br />
                        <div class="row text-center">
                            <asp:Button ID="btn_save_term" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btn_save_term_Click" />

                        </div>
                        <asp:Panel ID="Panel_terminate_gv" runat="server">

                            <asp:GridView ID="gv_terminal" runat="server" class="table" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" OnRowDataBound="gv_terminal_RowDataBound" OnPreRender="updatterminate_PreRender" OnSelectedIndexChanged="gv_terminal_SelectedIndexChanged">

                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#000066" BackColor="#A1DCF2" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                <Columns>
                                    <asp:CommandField HeaderText="-&gt;" SelectText="-&gt;" />
                                    <asp:TemplateField HeaderText="Sr. No." Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_wsrnumber1" Visible="false" runat="server" Text='<%# Eval("Id")%>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNamewar1" runat="server" Text='<%# Eval("Emp_Name")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TERMINATION_DATE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblwdate1" runat="server" Text='<%# Eval("terminate_date")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REASON">
                                        <ItemTemplate>
                                            <asp:Label ID="lblreason1" runat="server" Text='<%# Eval("Reason")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel_Uniform" runat="server" CssClass="panel panel-default">
                    <div class="panel-heading text-center">
                        <div style="font-size: small"><b>UNIFORM LETTER</b></div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-3 col-xs-12"></div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Client Name :</b>
                                    <asp:DropDownList ID="ddl_client_uniform" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_client_uniform_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Branch Name :</b>
                                    <asp:DropDownList ID="ddl_branch_uniform" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_branch_uniform_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Employee Name :</b>
                                <asp:DropDownList ID="ddl_emp_uniform" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                            </div>
                            <div class="col-sm-2 col-xs-12"><b>Uniform Recieve Status :-</b></div>
                            <div class="col-sm-1 col-xs-12">


                                <asp:RadioButton ID="rb_uni_yes" runat="server" GroupName="uniform" />
                               <b> Yes :</b>

                            </div>
                            <div class="col-sm-1 col-xs-12">


                                <asp:RadioButton ID="rb_uni_no" runat="server" GroupName="uniform" />
                              <b>  No :</b>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Received Date :</b>
                            
                                           <asp:TextBox ID="txt_uniform_rcv_date" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                            </div>

                        </div>
                        <br />

                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                            </div>
                            <div class="col-sm-2 col-xs-12"><b>ID Card Recieve Status :-</b></div>
                            <div class="col-sm-1 col-xs-12">


                                <asp:RadioButton ID="rb_id_yes" runat="server" GroupName="icard" />
                              <b>  Yes :</b>

                            </div>
                            <div class="col-sm-1 col-xs-12">

                                <asp:RadioButton ID="rb_id_no" runat="server" GroupName="icard" />
                               <b> No :</b>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Received Date :</b>
                                   
                                               <asp:TextBox ID="txt_icard_rcv_date" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row text-center">


                            <asp:Button ID="btn_uniform_submit" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btn_uniform_submit_Click" />

                        </div>
                    </div>
                    </div>
                </asp:Panel>

            </div>

        </asp:Panel>
    </div>


</asp:Content>
