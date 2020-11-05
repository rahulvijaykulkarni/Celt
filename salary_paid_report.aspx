<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="salary_paid_report.aspx.cs"  Inherits="salary_paid_report" Title="salary paid report" EnableEventValidation="false" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Salary Paid Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_header" runat="Server">
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
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {

           
            $('#<%=ddl_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_billing_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_unitcode.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_client_name.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            
            

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
                    // alert(month);
                    //if (month == "2" || month == "3") {

                    //    $("#abc11").show();
                    //    $("#abc111").show();
                    //} else {

                    //    $("#abc11").hide();
                    //    $("#abc111").hide();
                    //}
                }
            });
            $(document).ready(function () {
                var st = $(this).find("input[id*='hidtab']").val();
                if (st == null)
                    st = 0;
                $('[id$=tabs]').tabs({ selected: st });
            });
            //vikas add for frige
            var table = $('#<%=salary_gridview.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                scrollY: "310px",
                scrollX: true,
                scrollCollapse: true,
                paging: false,
                //fixedColumns: {
                //    leftColumns: 3,

                //}


            });
            $('.date-picker12').datepicker({
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
            });
            $('.date-picker12').focus(function () {
                $(".ui-datepicker-calendar").hide();

            });
            $(".date-picker12").attr("readonly", "true");
            $(".date-picker").attr("readonly", "true");


            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=salary_gridview.ClientID%>').DataTable({
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
               .appendTo('#<%=salary_gridview.ClientID%>_wrapper .col-sm-6:eq(0)');
        }
        function req_validation()
        {
            
            var txt_month_year = document.getElementById('<%=txt_month_year.ClientID %>');
            if (txt_month_year.value == "") {
                alert("Please Select Month/Year");
                txt_month_year.focus();
                return false;
            }
            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

            if (Selected_ddl_client == "Select")
            {
                alert("Please Select Client");
                ddl_client.focus();
                return false;
            }
        }

        function openWindow() {
            window.open("html/salary_paid_report.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

        function report_validation() {

            var ddl_report = document.getElementById('<%=ddl_report.ClientID %>');
            var Selected_ddl_report = ddl_report.options[ddl_report.selectedIndex].text;
            var ddl_client_name = document.getElementById('<%=ddl_client_name.ClientID %>');
            var Selected_ddl_client = ddl_client_name.options[ddl_client_name.selectedIndex].text;


            if ((Selected_ddl_report == "Branch Head Contact Details") || (Selected_ddl_report == "Joining Letter Sending Details")) {
                if (Selected_ddl_client == "ALL") {
                    alert("Please Select Client Name");
                    ddl_client_name.focus();
                    return false;
                }
            }
            

            var txt_date = document.getElementById('<%=txt_date.ClientID %>');
            if (txt_date.value == "") {
                alert("Please Select Month");
                txt_date.focus();
                return false;
            }

        }
    </script>
    <style></style>
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
                        <div style="text-align: center; color: #fff; font-size: small;" class="text-center text-uppercase"><b> Salary Paid Report</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Salary Paid Report Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                 <div id="tabs" style="background: #f3f1fe; border-radius: 10px; padding:20px 20px 20px 20px; border: 1px solid white; margin-bottom:15px; margin-top:15px">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li><a href="#menu1"><b>Salary Reports</b></a></li>
                        <li><a href="#menu2"><b>Report</b></a></li>
                      
                    </ul>
                     <div id="menu1">
                         <div class="panel-body">
                         <br />
                         <div class="row">
                             <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12" style="width: 9.667%;">
                                   <b> Select Month:</b><asp:Label ID="Label2" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                            <asp:TextBox ID="txt_month_year" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                </div>
                            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                              <b>  Client :</b><asp:Label ID="Label1" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                        <asp:DropDownList ID="ddl_client" DataValueField="client_code" DataTextField="client_name"  AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged">
                        </asp:DropDownList>
                            </div>
                            <asp:Panel ID="unit_panel" runat="server">
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                   <b> State :</b><asp:Label ID="Label3" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                            <asp:DropDownList ID="ddl_billing_state" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_billing_state_SelectedIndexChanged"  AutoPostBack="true" />
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                   <b> Branch :</b><asp:Label ID="Label4" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                            <asp:DropDownList ID="ddl_unitcode" DataValueField="unit_code" DataTextField="unit_name" runat="server" OnSelectedIndexChanged="ddl_unitcode_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                                </div>
                                <div class="row">
                                    <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12" style="margin-top:17px">
                                 <asp:Button ID="btn_attendance" runat="server" CssClass="btn btn-large"
                           Text="Client Attendance" OnClick="btn_attendance_Click" OnClientClick="return req_validation();" />&nbsp;&nbsp;
                              </div>
                                    </div>
                                </div> 
                            </asp:Panel>
                        </div>
                                        
                   
                  <asp:Panel ID="panel2" runat="server" Style="height:500px">
                          
                      <asp:GridView ID="salary_gridview" class="table" runat="server" BackColor="White" 
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        AutoGenerateColumns="False" Width="100%" OnPreRender="salary_gridview_PreRender">
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <Columns>
                                            <asp:BoundField DataField="state_name" HeaderText="state name" SortExpression="state_name" />
                                            <asp:BoundField DataField="unit_name" HeaderText="Branch name" SortExpression="unit_name" />
                                            <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                                            <asp:BoundField DataField="grade" HeaderText="Designation" SortExpression="grade" />
                                            <asp:BoundField DataField="employee_type" HeaderText="Employee Type" SortExpression="employee_type" />
                                            <asp:BoundField DataField="month" HeaderText="Month" SortExpression="month" />
                                            <asp:BoundField DataField="year" HeaderText="Year" SortExpression="year" />
                                            <asp:BoundField DataField="EMP_ADVANCE_PAYMENT" HeaderText="EMP ADVANCE PAYMENT" SortExpression="EMP_ADVANCE_PAYMENT" />
                                            <asp:BoundField DataField="fine" HeaderText="fine" SortExpression="fine" />
                                            <asp:BoundField DataField="DEDUCTION" HeaderText="DEDUCTION" SortExpression="DEDUCTION" />
                                            <asp:BoundField DataField="Total_Days_Present" HeaderText="No Of Paid Days" SortExpression="Total_Days_Present" />
                                            <asp:BoundField DataField="payment" HeaderText="Take Home" SortExpression="payment" />
                                            <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" />
                                            <asp:BoundField DataField="Bank_holder_name" HeaderText="Bank holder name" SortExpression="Bank_holder_name" />
                                            <asp:BoundField DataField="BANK_EMP_AC_CODE" HeaderText="Bank Account no" SortExpression="BANK_EMP_AC_CODE" />
                                            <asp:BoundField DataField="PF_IFSC_CODE" HeaderText="IFSC CODE" SortExpression="PF_IFSC_CODE" />
                                            
                                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                            <asp:BoundField DataField="salary_date" HeaderText="Salary Date" SortExpression="salary_date" />
                                        </Columns>
                      </asp:GridView>
                  </asp:Panel>

                         </div>
                     <div id="menu2">
                <div class="panel-body">
                    <br />
                 <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">

                <ContentTemplate>--%>
                <div class="row">

                    <div class="col-sm-2 col-xs-12 text-left">
                       <b> Client Name : </b>  <asp:Label ID="Label5" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                <asp:DropDownList ID="ddl_client_name" class="form-control pr_state js-example-basic-single" runat="server" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged1" AutoPostBack="true">
                </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                       <b> State Name : </b>  <asp:Label ID="Label6" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                 <asp:DropDownList ID="ddl_state" runat="server" DataTextField="STATE" DataValueField="STATE" class="form-control text_box" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged1" AutoPostBack="true">
                 </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                       <b> Branch Name : </b>  <asp:Label ID="Label7" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                <asp:DropDownList ID="ddl_unit" class="form-control pr_state js-example-basic-single" runat="server">
                </asp:DropDownList>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12 ">
                           <b> Select Month :</b><asp:Label ID="Label8" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                            <asp:TextBox ID="txt_date" CssClass="form-control date-picker12" runat="server" ></asp:TextBox>
                                </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                                <b> Report type: </b><asp:Label ID="Label9" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                    <asp:DropDownList ID="ddl_report" runat="server" onchange="return bill_check();" class="form-control">
                                          <asp:ListItem Value="Branch Head Contact Details">Branch Head Contact Details</asp:ListItem>
                                          <asp:ListItem Value="Joining Letter Sending Details">Joining Letter Sending Details</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                    <div class="col-sm-2 col-xs-12 text-left" >
                                    <asp:Button ID="btn_getxl_report" style="margin-top: 18px;" runat="server" class="btn btn-large" OnClick="btn_getxl_report_Click" Text="Get Report" OnClientClick="report_validation();"  />
                                 
                                </div>

                </div>
                   <%--</ContentTemplate>
                      </asp:UpdatePanel>--%>
                     </div>
                     
               
                        
                </div>
                <br />
        
                </div>
        </div>
           
</asp:Content>