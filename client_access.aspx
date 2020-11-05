<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="client_access.aspx.cs" Inherits="client_access" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Client Assign</title>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="cph_header" runat="server">
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
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>

    <link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <%--<script src="datatable/jquery-1.12.3.js"></script>--%>
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/jszip.min.js"></script>
    <%--    <script src="datatable/pdfmake.min.js"></script>--%>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {
            $('#<%=ddl_department.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_employee_type.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_client_name.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state_name.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_clear.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_employee.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_client_access.ClientID%>').DataTable({
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
               .appendTo('#<%=gv_client_access.ClientID%>_wrapper .col-sm-6:eq(0)');
        }

        function openWindow() {
            window.open("html/Assign_client.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }


        function Req_validation() {
            var ddl_department = document.getElementById('<%=ddl_department.ClientID %>');
            var Selected_ddl_department = ddl_department.options[ddl_department.selectedIndex].text;

            if (Selected_ddl_department == "Select") {
                alert("Please Select Department Type");
                ddl_department.focus();
                return false;
            }
            var ddl_employee_type = document.getElementById('<%=ddl_employee_type.ClientID %>');
            var Selected_ddl_employee_type = ddl_employee_type.options[ddl_employee_type.selectedIndex].text;
            if (Selected_ddl_employee_type == "Select Staff") {
                alert("Please Select Employee Name");
                ddl_employee_type.focus();
                return false;
            }
            var ddl_client_name = document.getElementById('<%=ddl_client_name.ClientID %>');
    var Selected_ddl_client_name = ddl_client_name.options[ddl_client_name.selectedIndex].text;
    if (Selected_ddl_client_name == "Select") {
        alert("Please Select Client Name");
        ddl_client_name.focus();
        return false;
    }
    var ddl_state_name = document.getElementById('<%=ddl_state_name.ClientID %>');
            var Selected_ddl_state_name = ddl_state_name.options[ddl_state_name.selectedIndex].text;
            if (Selected_ddl_state_name == "Select") {
                alert("Please Select State");
                ddl_state_name.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
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
        function R_remove_validation() {
            var ddl_unitcode1 = document.getElementById('<%=ddl_unitcode1.ClientID %>');

            if (ddl_unitcode1.value == "") {
                alert("Please Select Branch Having Staff");
                ddl_unitcode1.focus();
                return false;
            }
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
    <style>
        .grid-view {
            max-height: 300px;
            height: auto;
            overflow-x: auto;
            overflow-y: hidden;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="cph_righrbody" ID="content3" runat="server">
    <div class="container-fluid">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>CLIENT ASSIGN</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Client Assign Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white; margin-bottom:15px;margin-top:15px">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                   <b> Department Type :</b><span class="text-red"> </span>
                                    <asp:DropDownList ID="ddl_department" runat="server" class="form-control" OnSelectedIndexChanged="ddl_department_OnSelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="Select">Select</asp:ListItem>

                                        <asp:ListItem Value="HR Department">HR Department</asp:ListItem>
                                        <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                        <asp:ListItem Value="Finance">Finance</asp:ListItem>
                                        <asp:ListItem Value="Operation">Operation</asp:ListItem>
                                        <asp:ListItem Value="Sales">Sales</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Employee Name :</b>
                                  
                        <asp:DropDownList ID="ddl_employee_type" class="form-control" Width="100%" runat="server" OnSelectedIndexChanged="emp_onSelectedValue" AutoPostBack="true">
                        </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12 ">
                                   <b> Employee ID :</b>
                                  
                        <asp:TextBox ID="txt_employee_code" class="form-control" Width="100%" runat="server" ReadOnly="true">
                        </asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Client Name : </b>
                                        <asp:DropDownList ID="ddl_client_name" runat="server" Font-Size="X-Small" class="form-control" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged1" AutoPostBack="true">
                                        </asp:DropDownList>

                                </div>
                                <div class="col-sm-2 col-xs-12">
                                  <b>  State : </b>
                                        <asp:DropDownList ID="ddl_state_name" runat="server" Font-Size="X-Small" class="form-control" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged1" AutoPostBack="true">
                                        </asp:DropDownList>

                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12"></div>
                                <div class="col-sm-4 col-xs-12 text-left">
                                   <b> Branch Having Staff :</b>
                                        <asp:ListBox ID="ddl_unitcode1" runat="server" class="form-control" SelectionMode="Multiple" Height="150"></asp:ListBox>
                                </div>
                                <div class="col-sm-4 col-xs-12 text-left">
                                   <b> Branch Not Having Staff :</b>
                                        <asp:ListBox ID="ddl_unitcode_without1" runat="server" class="form-control" SelectionMode="Multiple" Height="150"></asp:ListBox>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <br />
                    </div>
                    <br />
                    <div class="row text-center">
                        <asp:Button ID="btn_save" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btn_submit_click" OnClientClick="return Req_validation();" />
                        <asp:Button ID="btn_delete" runat="server" CssClass="btn btn-primary" Text="Remove" OnClick="btn_delete_click" OnClientClick="return R_remove_validation()" />
                        <asp:Button ID="btn_clear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="btn_clear_click" />
                        <asp:Button ID="btn_close" runat="server" CssClass="btn btn-danger" Text="Close" OnClick="btn_close_click" />
                    </div>
                    <br />
                
                <br />
                 <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white; padding:10px 10px 10px 10px ; margin-bottom:15px;margin-top:15px">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                  <b>  Employee Wise :</b>
                        <asp:DropDownList ID="ddl_employee" class="form-control" Width="100%" runat="server" OnSelectedIndexChanged="emp_gv_onSelectedValue" AutoPostBack="true">
                        </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Client Wise : </b>
                                        <asp:DropDownList ID="ddl_client" runat="server" Font-Size="X-Small" class="form-control" OnSelectedIndexChanged="ddl_client_gv_SelectedIndexChanged1" AutoPostBack="true">
                                        </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <b>State Wise : </b>
                                        <asp:DropDownList ID="ddl_state" runat="server" Font-Size="X-Small" class="form-control" OnSelectedIndexChanged="ddl_state_gv_SelectedIndexChanged1" AutoPostBack="true">
                                        </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            </div>
                            <div class="container">
                                <div class="container-fluid" style="background: white; border-radius: 10px; border: 1px solid white; padding:20px 20px 20px 20px ; margin-bottom:15px;margin-top:15px">
                                <asp:Panel ID="Panel1" runat="server" Style="height: auto; max-height: 300px; overflow-x: hidden; overflow-y: auto">
                                    <asp:GridView ID="gv_client_access" class="table" runat="server" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnPreRender="gv_client_access_PreRender" Width="100%">
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="White" ForeColor="#000066" />


                                    </asp:GridView>

                                </asp:Panel>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
