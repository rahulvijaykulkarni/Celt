<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Title="Tab Employee Attendance Details" CodeFile="tab_employee_attendance_details.aspx.cs" Inherits="tab_employee_attendance_details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Tab Employee Attendance Details</title>
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
    <%--<script src="datatable/jquery-1.12.3.js"></script>--%>
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <%-- <script src="datatable/jszip.min.js"></script>--%>
    <%-- <script src="datatable/pdfmake.min.js"></script>--%>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>
    <script src="datatable/pdfmake.min.js"></script>
    <script src="js/dataTables.fixedColumns.min.js"></script>
    <link href="css/fixedColumns.dataTables.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {
            $('#<%=ddl_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_emp_attendance.ClientID%>').DataTable(
                    {
                        "responsive": true,
                        "sPaginationType": "full_numbers",
                        scrollY: "310px",
                        scrollX: true,
                        scrollCollapse: true,
                        paging: true,
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
                        ],
                    });

            table.buttons().container()
               .appendTo('#<%=gv_emp_attendance.ClientID%>_wrapper .col-sm-6:eq(0)');

               $(".date-picker").datepicker({
                   changeMonth: true,
                   changeYear: true,
                   showButtonPanel: true,
                   dateFormat: 'dd/mm/yy',
                   yearRange: '1951',
               });
               $(".date-picker").attr("readonly", "true");

               $("#dialog").dialog({
                   autoOpen: false,
                   modal: true,
                   height: 500,
                   width: 500,
                   title: "Zoomed Image",
                   buttons: [{ text: "Close", click: function () { $(this).dialog("close") } }],
               });
               $("[id*=intime_imgpath]").click(function () {
                   //alert("hello");
                   $('#dialog').html('');
                   $('#dialog').append($(this).clone().width(470).height(400));
                   $('#dialog').dialog('open');
                   //height:200;
                   //width: 200;
               });
               $("[id*=outtime_imgpath]").click(function () {
                   //alert("hello");
                   $('#dialog').html('');
                   $('#dialog').append($(this).clone().width(470).height(400));
                   $('#dialog').dialog('open');
                   //height:200;
                   //width: 200;
               });
           }
           function Req_validation() {
               var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;
            if (Selected_ddl_client == "Select") {
                alert("Please Select Client");
                ddl_client.focus();
                return false;
            }
            var ddl_state = document.getElementById('<%=ddl_state.ClientID %>');
            var Selected_ddl_state = ddl_client.options[ddl_state.selectedIndex].text;

            if (Selected_ddl_state == "Select") {
                alert("Please Select State");
                ddl_state.focus();
                return false;
            }
            var ddl_unit = document.getElementById('<%=ddl_unit.ClientID %>');
            var Selected_ddl_unit = ddl_unit.options[ddl_unit.selectedIndex].text;

            if (Selected_ddl_unit == "Select") {
                alert("Please Select Client");
                ddl_unit.focus();
                return false;
            }
            var ddl_unit = document.getElementById('<%=txt_date.ClientID %>');

            if (ddl_unit.value == "") {
                alert("Please Select Date");
                ddl_unit.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
           }

        function openWindow() {
            window.open("html/tab_employee_attendance_details.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }


    </script>
    <style>
        .grid-view {
            height: auto;
            max-height: 300px;
            width: auto;
            overflow-y: auto;
            overflow-x: hidden;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <div class="container-fluid">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary" Style="background-color: white;">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Tab Employee Attendance</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;">
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Tab Employee Attendance Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>

            <div class="panel-body">
                
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">
                    <br />
                    <div class="row">
                        <div id="dialog"></div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Client Name:</b><span style="color: red">*</span>
                            <asp:DropDownList runat="server" ID="ddl_client" CssClass="form-control" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> State Name:</b><span style="color: red">*</span>
                            <asp:DropDownList runat="server" ID="ddl_state" CssClass="form-control" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Branch Name:</b><span style="color: red">*</span>
                            <asp:DropDownList runat="server" ID="ddl_unit" CssClass="form-control">
                                <asp:ListItem>Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Date:</b><span style="color: red">*</span>
                            <asp:TextBox runat="server" ID="txt_date" CssClass="form-control date-picker"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12" style="margin-top: 1.5em;">
                            <asp:Button runat="server" ID="btn_submit" CssClass="btn btn-primary" Text="Show" OnClick="btn_submit_Click" OnClientClick="return Req_validation();"></asp:Button>
                            <asp:Button runat="server" ID="btn_close" CssClass="btn btn-danger" Text="Close"></asp:Button>
                        </div>
                    </div>
                    <br />
                </div>
                <br />
                <asp:Panel ID="Panel1" runat="server">
                    <%--<asp:GridView ID="gv_emp_attendance" class="table" runat="server"  AutoGenerateColumns="False"  ForeColor="#333333" Font-Size="X-Small" OnRowDataBound="gv_emp_attendance_RowDataBound">
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
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>


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
                              <asp:BoundField DataField="currdate" HeaderText="Date & Time"
                                SortExpression="currdate" />
                            <asp:BoundField DataField="location_add" HeaderText="Location Address"
                                SortExpression="location_add" />

                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
            <br />
        </asp:Panel>
    </div>
</asp:Content>
