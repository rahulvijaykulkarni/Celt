<%@ Page Title="Employee Leave Apply Form" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Leave_form.aspx.cs" Inherits="Leave_form" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Employee Leave Apply Form</title>
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
    <script src="datatable/pdfmake.min.js"></script>



    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }

        $(document).ready(function () {
            var table = $('#<%=LeaveTypeGridView.ClientID%>').DataTable({
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
               .appendTo('#<%=LeaveTypeGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';

        });
    </script>



    <style>
        .container {
            max-width: 99%;
        }

        .grid-view {
            height: auto;
            max-height: 500px;
            overflow-x: hidden;
            overflow-y: auto;
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



    <script type="text/javascript">

        function pageLoad() {

            //$(".date-picker1").datepicker({
            //    changeMonth: true,
            //    changeYear: true,
            //    showButtonPanel: true,
            //    dateFormat: 'dd/mm/yy',
            //    minDate: 0,
            //    onSelect: function (selected) {
            //        $(".date-picker2").datepicker("option", "minDate", selected);
            //        var fromDate = $(".date-picker1").datepicker('getDate');
            //        var toDate = $(".date-picker2").datepicker('getDate');
            //        // date difference in millisec
            //        var diff = new Date(toDate - fromDate);
            //        // date difference in days
            //        var days = diff / 1000 / 60 / 60 / 24;
            //        days = days + 1;

            //        if ($(".date-picker2").val() == "") {

            //            return false;

            //        } else {

            //            $('.no_of_days').val(days);
            //            var no_days = $('.no_of_days').val();
            //            //alert(no_days);
            //            var bal_leave = $('.bal_leave').val();
            //            //alert(bal_leave);
            //            var lab_leave_result = bal_leave - no_days;
            //            //alert(lab_leave_result);
            //            $('.bal_leave').val(lab_leave_result);

            //        }
            //    }
            //});
            $('.date-picker2').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
                //yearRange: '1950',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });
            $('.date-picker1').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
                // yearRange: '1950',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });

            //$(".date-picker2").datepicker({
            //    changeMonth: true,
            //    changeYear: true,
            //    showButtonPanel: true,
            //    dateFormat: 'dd/mm/yy',
            //    minDate: 0,
            //    onSelect: function (selected) {
            //        $(".date-picker1").datepicker("option", "maxDate", selected);
            //        var fromDate = $(".date-picker1").datepicker('getDate');
            //        var toDate = $(".date-picker2").datepicker('getDate');
            //        // date difference in millisec
            //        var diff = new Date(toDate - fromDate);
            //        // date difference in days
            //        var days = diff / 1000 / 60 / 60 / 24;
            //        days = days + 1;


            //        if ($(".date-picker1").val() == "") {

            //            return false;
            //        } else {
            //            $('.no_of_days').val(days);
            //            var no_days = $('.no_of_days').val();
            //            //alert(no_days);
            //            var bal_leave = $('.bal_leave').val();
            //            //alert(bal_leave);
            //            var lab_leave_result = bal_leave - no_days;
            //            //alert(lab_leave_result);
            //            $('.bal_leave').val(lab_leave_result);
            //        }
            //    }
            //});

            $(".date-picker1").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");


        }

        //$(document).ready(function () {


        //    var $datepicker1 = $(".date-picker1");
        //    var $datepicker2 = $(".date-picker2");
        //    var $no_of_days = $(".no_of_days");

        //    $datepicker1.datepicker();
        //    $datepicker2.datepicker({
        //        onClose: function () {
        //            var fromDate = $datepicker1.datepicker('getDate');
        //            var toDate = $datepicker2.datepicker('getDate');
        //            // date difference in millisec
        //            var diff = new Date(toDate - fromDate);
        //            // date difference in days
        //            var days = diff / 1000 / 60 / 60 / 24;

        //            alert(days);
        //        }
        //    });

        //});
        $(function () {
            $('#<%=btn_cancel.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });


            $('#<%=btn_leave_cancel.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });


            $('#<%=LeaveTypeGridView.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

        });


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


        function openWindow() {
            window.open("html/Leave_form.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
        function confirmation() {
            var answer = confirm("Are you sure you want to Cancel Leave? This action cannot be undone.")
        }
        function Req_validation() {


            var l_leave_type = document.getElementById('<%=ddl_leave_type.ClientID %>');
            var SelectedText1 = l_leave_type.options[l_leave_type.selectedIndex].text;

            var t_reason = document.getElementById('<%=txt_reason.ClientID %>');
             var t_manager = document.getElementById('<%=txt_manager.ClientID %>');
            var t_balance_leave = document.getElementById('<%=txt_balance_leave.ClientID %>');
            var t_fromdate = document.getElementById('<%=txt_fromdate.ClientID %>');
            var t_todate = document.getElementById('<%=txt_todate.ClientID %>');
            var t_no_of_days = document.getElementById('<%=txt_no_of_days.ClientID %>');



            //Leave Type: 

            if (SelectedText1 == "--Select Leave Type--") {
                alert("Please Select Type Leave !!!");
                l_leave_type.focus();
                return false;
            }
            // Reason For Leave

            if (t_reason.value == "") {
                alert("Please Enter Reason For Leave");
                t_reason.focus();
                return false;
            }


            // Reporting To

            //if (t_manager.value == "") {
            //    alert("Please Enter Reporting To");
            //    t_manager.focus();
            //    return false;
            //}

            //Balance Leave

            if (t_balance_leave.value == "") {
                alert("Please Enter Balance Leave");
                t_balance_leave.focus();
                return false;
            }


            //Start Date

            if (t_fromdate.value == "") {
                alert("Please Select Start Date");
                t_fromdate.focus();
                return false;
            }

            // End Date

            if (t_todate.value == "") {
                alert("Please Select End Date");
                t_todate.focus();
                return false;
            }


            //Number Of Days

            if (t_no_of_days.value == "") {
                alert("Please Enter Number Of Days");
                t_no_of_days.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <div class="container-fluid">


        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Employee Leave Apply Form</b></div>
                    </div>
                    <div class="col-sm-2 text-right">

                        <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image2" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
             <br />
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Employee Leave Apply Form Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>

            <div class="panel-body">
                <%--               <div class="row">
                    <asp:Panel ID="Panel3" runat="server" CssClass="grid-view" ScrollBars="Auto">
                    <asp:GridView ID="GridView1" class="table" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White"
                         BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"  OnRowDataBound="OnRowDataBound" 
                        OnPreRender="GridView1_PreRender" >
                           <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:CommandField HeaderText="-&gt;" SelectText="-&gt;"
                                ShowSelectButton="True" />
 
                            
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="COMP_CODE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_comp_code" runat="server" Text='<%# Eval("comp_code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="POLICY NAME">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_policy_name" runat="server" Text='<%# Eval("policy_name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="START DATE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_start" runat="server" Text='<%# Eval("Start_date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="END DATE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_end" runat="server" Text='<%# Eval("end_date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </asp:Panel>

               </div>--%>


                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                               <b> Leave Type :</b>
                    <asp:DropDownList ID="ddl_leave_type" runat="server" Width="100%" class="form-control text_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_leave_type_TextChanged">
                        <%--<asp:ListItem Value="select">Select Leave Type</asp:ListItem>
                        <asp:ListItem Value="CL">Casual Leave</asp:ListItem>
                        <asp:ListItem Value="PL">Privilege Leave</asp:ListItem>
                        <asp:ListItem Value="F">HalfDay Leave</asp:ListItem>
                        <asp:ListItem Value="ML">Maternity Leave</asp:ListItem>
                        <asp:ListItem Value="PT">Paternity Leave</asp:ListItem>
                        <asp:ListItem Value="LW">Leave Without Pay</asp:ListItem>--%>
                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Reporting To :</b>
                
                    <asp:TextBox ID="txt_manager" runat="server" class="form-control text_box"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-xs-12">
                               <b> Reason For Leave :</b>

                    <asp:TextBox ID="txt_reason" runat="server" class="form-control text_box" TextMode="MultiLine" MaxLength="200" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <asp:Label class="form-control" ID="half" runat="server" BorderStyle="None" />
                                <asp:DropDownList ID="ddl_halfday" runat="server" Width="100%" class="form-control text_box">
                                    <asp:ListItem Value="NA">Select Half</asp:ListItem>
                                    <asp:ListItem Value="FH">First Half</asp:ListItem>
                                    <asp:ListItem Value="SH">Second Half</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txt_del_date" runat="server" class="form-control datepicker text_box" Width="85%" Style="display: inline"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <asp:Label class="form-control" ID="lbl_pl_cl" runat="server" BorderStyle="None" Text="Leave Type :" />
                                <asp:DropDownList ID="ddl_pl_cl" runat="server" Width="100%" class="form-control text_box">
                                    <asp:ListItem Value="PL">PL</asp:ListItem>
                                    <asp:ListItem Value="CL">CL</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row">

                            <div class="col-sm-2 col-xs-12">
                               <b> Balance Leave :</b>
                
                    <asp:TextBox ID="txt_balance_leave" runat="server" onkeypress="return isNumber(event)" class="form-control bal_leave text_box"></asp:TextBox>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Carry Forword :</b>
                
                    <asp:TextBox ID="txt_carry_forword" runat="server" onkeypress="return isNumber(event)" class="form-control bal_leave text_box" ReadOnly="true"></asp:TextBox>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Start Date :</b>
                        <%--<asp:TextBox ID="txt_fromdate1" runat="server" class="form-control date-picker1 text_box"   width="100%" ></asp:TextBox>
                        --%>
                                <asp:TextBox ID="txt_fromdate" ReadOnly="false" runat="server" class="form-control date-picker1 text_box" Width="100%" Style="display: inline"></asp:TextBox>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> End Date :</b>
                                      <asp:TextBox ID="txt_todate" runat="server" class="form-control date-picker2 text_box" Width="100%"></asp:TextBox>


                            </div>

                            <div class="col-sm-2 col-xs-12">
                                <br />
                                <br />
                                <asp:Button ID="txt_calc" runat="server" Text="Calculate" OnClick="txt_no_of_days_TextChanged" CssClass="btn btn-primary" Width="50%"></asp:Button>


                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Number Of Days :</b>
                
                                        <asp:TextBox ID="txt_no_of_days" ReadOnly="true" runat="server" class="form-control no_of_days text_box" MaxLength="3" AutoPostBack="true" onkeypress="return isNumber(event)"></asp:TextBox>
                            </div>


                        </div>
                        <br />

                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                                <asp:Label ID="lbl_Leave_Approved_Date" runat="server" Visible="false"> </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                                <asp:Label ID="lbl_status" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                                <asp:Label ID="lbl_Leave_Status_Comment" runat="server" Visible="false"></asp:Label>
                            </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    <br />
                <div class="row text-center">
                    <asp:Button ID="btn_leave_cancel" runat="server" CssClass="btn btn-primary" Text=" Cancel Leave "
                        OnClick="btn_leave_cancel_Click" OnClientClick="confirmation();" />
                    <asp:Button ID="btn_add" runat="server" class="btn btn-primary" OnClick="btn_add_Click"
                        OnClientClick="return Req_validation();" Text=" Save " />
                    <asp:Button ID="btn_edit" runat="server" class="btn btn-primary"
                        OnClick="btn_edit_Click" Text=" Update " OnClientClick="return Req_validation();" />
                    <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" Text=" Clear "
                        OnClick="btn_cancel_Click" />
                    <asp:Button ID="btnclose" runat="server" class="btn btn-danger"
                        OnClick="btnclose_Click" Text="Close" />
                </div>
                <br />
                <asp:Panel ID="Panel2" runat="server" CssClass="grid-view">
                    <asp:GridView ID="LeaveTypeGridView" class="table" runat="server" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        OnSelectedIndexChanged="LeaveTypeGridView_SelectedIndexChanged"
                        AutoGenerateSelectButton="True"
                        OnRowDataBound="OnRowDataBound" OnPreRender="LeaveTypeGridView_PreRender">
                        <RowStyle ForeColor="#000066" />
                        <Columns>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ID">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_leaveid" runat="server" Text='<%# Eval("leave_id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="LEAVE APPLY DATE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Leave_Apply_Date" runat="server" Text='<%# Eval("Leave_Apply_Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="LEAVE TYPE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_LEAVE_TYPE" runat="server" Text='<%# Eval("LEAVE_TYPE")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="LEAVE REASON">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_LEAVE_REASON" runat="server" Text='<%# Eval("LEAVE_REASON")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="REPORTING TO">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_MANAGER" runat="server" Text='<%# Eval("MANAGER")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="START DATE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_FROM_DATE" runat="server" Text='<%# Eval("FROM_DATE")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="END DATE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_TO_DATE" runat="server" Text='<%# Eval("TO_DATE")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="NO OF DAYS">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_NO_OF_DAYS" runat="server" Text='<%# Eval("NO_OF_DAYS")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="LEAVE STATUS">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_LEAVE_STATUS" runat="server" Text='<%# Eval("LEAVE_STATUS")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="STATUS COMMENT">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_STATUS_COMMENT" runat="server" Text='<%# Eval("STATUS_COMMENT")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="LEAVE APPROVED DATE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Leave_Approved_Date" runat="server" Text='<%# Eval("Leave_Approved_Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="HALF DAY TYPE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_half_day_type" runat="server" Text='<%# Eval("half_day_type")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </asp:Panel>
            </div>


        </asp:Panel>


    </div>



</asp:Content>
