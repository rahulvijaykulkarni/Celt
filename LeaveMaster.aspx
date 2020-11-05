<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="LeaveMaster.aspx.cs" Inherits="LeaveMaster" EnableEventValidation="false" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Leave Master</title>
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
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/hashfunction.js"></script>
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
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
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }

        $(document).ready(function () {

            $('#<%=ddl_stateholi.ClientID%> ').change(function () {
                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                });





                var table = $('#<%=GridView1.ClientID%>').DataTable({
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
                   .appendTo('#<%=GridView1.ClientID%>_wrapper .col-sm-6:eq(0)');

                $.fn.dataTable.ext.errMode = 'none';

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

            $(document).ready(function () {

            });


    </script>



    <style>
        .container {
            max-width: 99%;
        }

        .grid-view {
            height: auto;
            max-height: 250px;
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

            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',

                onSelect: function (selected) {
                    $(".date-picker2").datepicker("option", "minDate", selected);
                    var fromDate = $(".date-picker1").datepicker('getDate');
                    var toDate = $(".date-picker2").datepicker('getDate');
                    // date difference in millisec
                    var diff = new Date(toDate - fromDate);
                    // date difference in days
                    var days = diff / 1000 / 60 / 60 / 24;
                    days = days + 1;

                    if ($(".date-picker2").val() == "") {

                        return false;

                    } else {

                        $('.no_of_days').val(days);
                        var no_days = $('.no_of_days').val();
                        //alert(no_days);
                        var bal_leave = $('.bal_leave').val();
                        //alert(bal_leave);
                        var lab_leave_result = bal_leave - no_days;
                        //alert(lab_leave_result);
                        $('.bal_leave').val(lab_leave_result);

                    }
                }
            });

            $(".date-picker2").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',

                onSelect: function (selected) {
                    $(".date-picker1").datepicker("option", "maxDate", selected);
                    var fromDate = $(".date-picker1").datepicker('getDate');
                    var toDate = $(".date-picker2").datepicker('getDate');
                    // date difference in millisec
                    var diff = new Date(toDate - fromDate);
                    // date difference in days
                    var days = diff / 1000 / 60 / 60 / 24;
                    days = days + 1;


                    if ($(".date-picker1").val() == "") {

                        return false;
                    } else {
                        $('.no_of_days').val(days);
                        var no_days = $('.no_of_days').val();
                        //alert(no_days);
                        var bal_leave = $('.bal_leave').val();
                        //alert(bal_leave);
                        var lab_leave_result = bal_leave - no_days;
                        //alert(lab_leave_result);
                        $('.bal_leave').val(lab_leave_result);
                    }
                }
            });

            $(".date-picker1").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");

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
        }

        function Req_validation() {

            var policy_name = document.getElementById('<%=txt_policy_name.ClientID %>');
            var txt_Name = document.getElementById('<%=txt_leave_name.ClientID %>');
            var txt_abrivaiton = document.getElementById('<%=txt_abbreviation.ClientID %>');
            var ddl_gender_status = document.getElementById('<%=ddl_gender.ClientID %>');

            var txt_max_leave = document.getElementById('<%=txt_max_no_of_leave.ClientID %>');
            var txt_carry_forword = document.getElementById('<%=txt_carry_forward.ClientID %>');
            var txt_from_date = document.getElementById('<%=txt_fromdate.ClientID %>');
            var txt_to_date = document.getElementById('<%=txt_todate.ClientID %>');

            if (policy_name.value == "") {
                alert("Please Enter policy name");
                policy_name.focus();
                return false;
            }
            if (txt_from_date.value == "") {
                alert("Please Select Start Date");
                txt_from_date.focus();
                return false;
            }
            if (txt_to_date.value == "") {
                alert("Please Select End Date");
                txt_to_date.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function holiday_validation() {

            var ddl_stateholi = document.getElementById('<%=ddl_stateholi.ClientID %>');
            var Selected_ddl_stateholi = ddl_stateholi.options[ddl_stateholi.selectedIndex].text;



            if (Selected_ddl_stateholi == "Select") {
                alert("Please Select Holiday Name");
                ddl_stateholi.focus();
                return false;
            }
            var dd_state = document.getElementById('<%=dd_state.ClientID %>');
            var Selected_dd_state = dd_state.options[dd_state.selectedIndex].text;
            if (Selected_ddl_stateholi == "State Holiday") {
                if (Selected_dd_state == "Select") {
                    alert("Please Select State");
                    dd_state.focus();
                    return false;
                }
            }

            var txt_stateholi_name = document.getElementById('<%=txt_stateholi_name.ClientID %>');

            if (txt_stateholi_name.value == "") {
                alert("Please Enter Holiday Description");
                txt_stateholi_name.focus();
                return false;
            }
            var txt_date = document.getElementById('<%=txt_date.ClientID %>');
            if (txt_date.value == "") {
                alert("Please Enter Start Date");
                txt_date.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        $(function () {


            $('#<%=btn_cancel.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=LeaveTypeGridView.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

        });


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
                    if (charCode == 46) {
                        return true;
                    }
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




        function openWindow() {
            window.open("html/Leave_master.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

        //function confirmation() {
        //    var answer = confirm("Are you sure you want to Cancel Leave? This action cannot be undone.")
        //}


        //window.onfocus = function () {

        //    $.unblockUI();
        //}

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
        function Req_val() {
            var txt_leave_name = document.getElementById('<%=txt_leave_name.ClientID %>');
             var Selected_txt_leave_name = txt_leave_name.options[txt_leave_name.selectedIndex].text;

             var ddl_gender = document.getElementById('<%=ddl_gender.ClientID %>');
             var Selected_ddl_gender = ddl_gender.options[ddl_gender.selectedIndex].text;

             var txt_leave_accural = document.getElementById('<%=txt_leave_accural.ClientID %>');

             var ddl_adv_req = document.getElementById('<%=ddl_adv_req.ClientID %>');
             var Selected_ddl_adv_req = ddl_adv_req.options[ddl_adv_req.selectedIndex].text;

             var ddl_bck_date = document.getElementById('<%=ddl_bck_date.ClientID %>');
             var Selected_ddl_bck_date = ddl_bck_date.options[ddl_bck_date.selectedIndex].text;

             if (Selected_txt_leave_name == "Select Leave") {
                 alert("Please Select Leave Name");
                 txt_leave_name.focus();
                 return false;
             }
             var txt_abbreviation = document.getElementById('<%=txt_abbreviation.ClientID %>');
             if (txt_abbreviation.value == "") {
                 alert("Please Enter Leave Abbreviation");
                 txt_abbreviation.focus();
                 return false;
             }

             if (Selected_ddl_gender == "Select Gender") {
                 alert("Please Select Gender");
                 ddl_gender.focus();
                 return false;
             }

             var txt_max_no_of_leave = document.getElementById('<%=txt_max_no_of_leave.ClientID %>');
             if (txt_max_no_of_leave.value == "") {
                 alert("Please Enter MAX No. of Leave");
                 txt_max_no_of_leave.focus();
                 return false;
             }
             var txt_carry_forward = document.getElementById('<%=txt_carry_forward.ClientID %>');
             if (txt_carry_forward.value == "") {
                 alert("Please Enter Carry forward");
                 txt_carry_forward.focus();
                 return false;
             }


             if (txt_leave_accural.value == "") {
                 alert("Please Enter Leave Accural");
                 txt_leave_accural.focus();
                 return false;
             }

             if (Selected_ddl_adv_req == "Select") {
                 alert("Please Select Advance Request");
                 ddl_adv_req.focus();
                 return false;
             }
             if (Selected_ddl_bck_date == "Select") {
                 alert("Please Select Backdated Request");
                 ddl_bck_date.focus();
                 return false;
             }
             $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             return true;
         }
         $(document).ready(function () {
             var st = $(this).find("input[id*='hidtab']").val();
             if (st == null)
                 st = 0;
             $('[id$=tabs]').tabs({ selected: st });
         });
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
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Leave Master</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Leave Master Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>

            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">

                <div class="row">
                    <div class=" col-sm-2 col-xs-12">
                       <b> Policy Name :</b>
                        <asp:TextBox ID="txt_policy_name" runat="server" onKeyPress="return AllowAlphabet_Number(event)" MaxLength="15" class="form-control text_box"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Start Date :</b>
                
                                                    <asp:TextBox ID="txt_fromdate" runat="server" class="form-control date-picker1 text_box" Width="100%" Style="display: inline"></asp:TextBox>

                    </div>
                    <div class="col-sm-2 col-xs-12">
                        <b>End Date :</b>
                                                    <asp:TextBox ID="txt_todate" runat="server" class="form-control date-picker2 text_box" Width="100%" Style="display: inline"></asp:TextBox>

                    </div>
                </div>
                    </div>
                <br />
                <br />

                <div id="tabs" style="background: #f3f1fe; padding:20px 20px 20px 20px; border-radius:10px">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li id="tabactive1" class="active"><a id="A1" data-toggle="tab" href="#home" runat="server"><b>Leave</b></a></li>
                        <li id="tabactive2"><a id="A2" data-toggle="tab" href="#menu1" runat="server"><b>Holidays</b></a></li>

                    </ul>

                    <br />

                    <div id="home">

                        <div class="row">


                            <asp:TextBox ID="TextBox1" runat="server" onKeyPress="return AllowAlphabet_Number(event)" MaxLength="15" class="form-control text_box" Visible="false"></asp:TextBox>

                            <div class="col-sm-2 col-xs-12">
                               <b> Leave Name :</b>
                
                                                    <%--<asp:TextBox ID="txt_leave_name" runat="server" onKeyPress="return AllowAlphabet_Number(event)" MaxLength="15" class="form-control text_box"></asp:TextBox>--%>
                                <asp:DropDownList ID="txt_leave_name" runat="server" Width="100%" class="form-control text_box">
                                    <%--OnSelectedIndexChanged="ddl_leave_type_TextChanged"--%>
                                    <asp:ListItem Value="select">Select Leave</asp:ListItem>
                                    <asp:ListItem Value="Casual Leave">Casual Leave</asp:ListItem>
                                    <asp:ListItem Value="Sick Leave">Sick Leave</asp:ListItem>


                                </asp:DropDownList>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Abbreviation :</b>

                                                    <asp:TextBox ID="txt_abbreviation" runat="server" onKeyPress="return AllowAlphabet_Number(event)" MaxLength="3" class="form-control text_box"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>Gender :</b>
                                                    <asp:DropDownList ID="ddl_gender" runat="server" Width="100%" class="form-control text_box">
                                                        <%--OnSelectedIndexChanged="ddl_leave_type_TextChanged"--%>
                                                        <asp:ListItem Value="select">Select Gender</asp:ListItem>
                                                        <asp:ListItem Value="M">Male</asp:ListItem>
                                                        <asp:ListItem Value="F">Female</asp:ListItem>
                                                        <asp:ListItem Value="B">Both</asp:ListItem>

                                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> MAX No. of Leave :</b>

                                                    <asp:TextBox ID="txt_max_no_of_leave" runat="server" onKeyPress="return isNumber_dot(event)" MaxLength="5" class="form-control text_box"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Carry forward :</b>

                                                    <asp:TextBox ID="txt_carry_forward" runat="server" onKeyPress="return isNumber_dot(event)" MaxLength="5" class="form-control text_box"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">

                                <asp:Label ID="lbl_id" runat="server" CssClass="hidden" Width="100%" Style="display: inline" Text="Hello"></asp:Label>

                            </div>
                        </div>

                        <br />
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                              <b>  Leave Accural :</b>

                                                   <asp:TextBox ID="txt_leave_accural" runat="server" MaxLength="50" Text="0" class="form-control text_box" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Advance request :</b>
                                                            <asp:DropDownList ID="ddl_adv_req" runat="server" Width="100%" class="form-control text_box">
                                                                <asp:ListItem Value="select">Select</asp:ListItem>
                                                                <asp:ListItem Value="M">Yes</asp:ListItem>
                                                                <asp:ListItem Value="F">No</asp:ListItem>
                                                            </asp:DropDownList>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Backdated request :</b>

                                                        <asp:DropDownList ID="ddl_bck_date" runat="server" Width="100%" class="form-control text_box">
                                                            <asp:ListItem Value="select">Select</asp:ListItem>
                                                            <asp:ListItem Value="M">Yes</asp:ListItem>
                                                            <asp:ListItem Value="F">No</asp:ListItem>
                                                        </asp:DropDownList>

                            </div>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"
                                OnClick="LinkButton1_Click" OnClientClick="return Req_val();">
                                                                    <img alt="Add Item" src="Images/add_icon.png" style="margin-top:1.5em"  />
                            </asp:LinkButton>

                        </div>


                        <br />

                        <div>
                            <%--<asp:GridView ID="GridView1" class="table" runat="server" BackColor="White" 
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="OnRowDataBound"
                                                        Width="500px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
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
                                                                       <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click1"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sr No.">
                                                                    <ItemStyle Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Leave_Name ">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Leave_Name" runat="server" Text='<%# Eval("leave_name")%>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
 					                  
                                                            <asp:TemplateField HeaderText="Abbreviation">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Abb" runat="server" Text='<%# Eval("abbreviation")%>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
 					                                                <asp:TemplateField HeaderText="Gender">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Gender" runat="server" Text='<%# Eval("gender")%>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Max_no_leave">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_max_no_leave" runat="server" Text='<%# Eval("max_no_of_leave")%>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Carry_Forward">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_carry_forward" runat="server" Text='<%# Eval("carry_forward")%>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Leave_Accural">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_leave_accural" runat="server" Text='<%# Eval("Leave_Accural")%>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                       
                                                        </Columns>
                                                    </asp:GridView>--%>
                            <asp:Panel ID="Panel4" runat="server" CssClass="grid-view">
                                <asp:GridView ID="GridView1" class="table" runat="server" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                    OnRowDataBound="OnRowDataBound"
                                    AutoGenerateColumns="False" OnPreRender="GridView1_PreRender">

                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="White" ForeColor="#000066" />

                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click_remove"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Leave_Name ">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Leave_Name" runat="server" Text='<%# Eval("leave_name")%>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Abbreviation">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Abb" runat="server" Text='<%# Eval("abbreviation")%>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gender">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Gender" runat="server" Text='<%# Eval("gender")%>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Max_no_leave">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_max_no_leave" runat="server" Text='<%# Eval("max_no_of_leave")%>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Carry_Forward">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_carry_forward" runat="server" Text='<%# Eval("carry_forward")%>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave_Accural">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_leave_accural" runat="server" Text='<%# Eval("Leave_Accural")%>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Advance_request">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Advance_request" runat="server" Text='<%# Eval("Advance_request")%>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Backdated_request">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Backdated_request" runat="server" Text='<%# Eval("Backdated_request")%>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>



                        </div>
                    </div>
                    <br />


                    <div id="menu1">

                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">


                                    <asp:TextBox ID="holi_id" runat="server" onKeyPress="return AllowAlphabet_Number(event)" MaxLength="15" class="form-control text_box" Visible="false"></asp:TextBox>



                                    <div class="col-sm-2 col-xs-12" style="margin-left: 20px;">
                                      <b>  Holiday :</b>
                                                <asp:DropDownList ID="ddl_stateholi" runat="server" CssClass="form-control" Width="200px" OnSelectedIndexChanged="ddl_state_click" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Text="State Holiday">Select</asp:ListItem>
                                                    <asp:ListItem Value="1" Text="State Holiday">State Holiday</asp:ListItem>
                                                    <asp:ListItem Value="2" Text="National Holiday">National Holiday</asp:ListItem>
                                                </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 col-xs-12 state" id="abc" runat="server">
                                        <asp:Label ID="state" runat="server" Text="State" Font-Bold="true"></asp:Label>


                                        <asp:DropDownList ID="dd_state" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Holiday Descriptiopn:</b>
                                                            <asp:TextBox ID="txt_stateholi_name" runat="server" CssClass="form-control" Visible="true" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Start Date :</b>
                                                         <asp:TextBox ID="txt_date" name="txt_date" runat="server" CssClass="form-control date-picker2 text_box"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-2 col-xs-12" style="margin-top: 1.5em">
                                        <asp:LinkButton ID="lnkbtn_addmoreitem" runat="server" CausesValidation="False"
                                            OnClick="lnkbtn_addmoreitem_Click" OnClientClick=" return holiday_validation();">
                                                        <img alt="Add Item" src="Images/add_icon.png"  />
                                        </asp:LinkButton>
                                    </div>



                                </div>


                                <br />
                                <div class="container">
                                    <asp:Panel runat="server" ID="gv_itemlist" CssClass="grid-view" Width="100%">
                                        <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            Width="100%" OnRowDataBound="gv_itemslist_RowDataBound"
                                            AutoGenerateColumns="False" OnPreRender="gv_itemslist_PreRender">

                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                            <Columns>

                                                <asp:TemplateField HeaderText="State">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_state" runat="server" Text='<%# Eval("HOLIDAY_STATE")%>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Holiday ">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_holi_name" runat="server" Text='<%# Eval("holiday")%>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Holiday_Discription">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_state_holi" runat="server" Text='<%# Eval("holiday_name")%>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_state_date" runat="server" Text='<%# Eval("date")%>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>

                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click" CssClass="btn btn-primary" Text="Delete" Style="color: white"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

                </div>


                <br />
                <br />

                <div class="row">
                    <div class="col-sm-2 col-xs-12">
                        <asp:Label ID="lbl_Leave_Approved_Date" runat="server" Visible="false"></asp:Label>
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
                <div class="row text-center">
                    <asp:Button ID="btn_save_draft" runat="server" class="btn btn-primary"
                        OnClientClick=" return Req_validation();" Text=" Submit " OnClick="btn_save_as_draft" />



                    <%-- <cc1:ConfirmButtonExtender ID="btn_add_ConfirmButtonExtender" 
                            runat="server" ConfirmText="After Click on Submit Button You cannot Make Any Changes!!" 
                            Enabled="True" TargetControlID="btn_add">
                        </cc1:ConfirmButtonExtender>--%>

                    <asp:Button ID="btn_edit" runat="server" class="btn btn-primary"
                        Text=" Update " OnClientClick="return Req_validation();" OnClick="btn_edit_Click" />
                    <asp:Button ID="btn_delete" runat="server" class="btn btn-primary"
                        Text="Delete" OnClick="btn_delete_Click" OnClientClick="return R_validation();" />
                    <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" Text=" Clear " OnClick="btn_cancel_Click" />
                    <asp:Button ID="btnclose" runat="server" class="btn btn-danger"
                        Text="Close" OnClick="btnclose_Click" />
                </div>
                <br />
                <asp:Panel ID="Panel2" runat="server" CssClass="grid-view">
                    <asp:GridView ID="LeaveTypeGridView" class="table" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="LeaveTypeGridView_SelectedIndexChanged" OnRowDataBound="OnRowDataBound" OnPreRender="LeaveTypeGridView_PreRender">
                        <%--onselectedindexchanged="LeaveTypeGridView_SelectedIndexChanged"  OnRowDataBound="OnRowDataBound"--%>
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <%-- <asp:CommandField HeaderText="-&gt;" SelectText="-&gt;"
                                ShowSelectButton="True" />--%>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click1"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

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


                            <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="MAX NO OF LEAVE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_max_no_of_leave" runat="server" Text='<%# Eval("max_no_of_leave")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="CARRY FORWARD">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_carry_forward" runat="server" Text='<%# Eval("carry_forward")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <%-- <asp:TemplateField  HeaderStyle-HorizontalAlign="Left" HeaderText="BALANCE LEAVE">
                                     <HeaderStyle HorizontalAlign="Left" /> 
                                      <ItemStyle HorizontalAlign="Left" /> 
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BALANCE_LEAVE" runat="server" Text='<%# Eval("BALANCE_LEAVE")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  --%>
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
