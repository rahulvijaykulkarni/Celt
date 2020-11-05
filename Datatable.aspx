<%@ Page Language="C#" AutoEventWireup="true" Title="Reports" MasterPageFile="~/MasterPage.master" CodeFile="Datatable.aspx.cs" Inherits="Datatable" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="cph_righrbody">
    <!DOCTYPE html>
    <link href="datatable/bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <script src="datatable/jquery-1.12.3.js"></script>
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/jszip.min.js"></script>
    <script src="datatable/pdfmake.min.js"></script>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>

    <link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <%--<script src="datatable/jquery-1.12.3.js"></script>--%>
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

    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <script type="text/javascript">

        $(document).ready(function () {
            var table = $('#<%=GradeGridView.ClientID%>').DataTable({
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
                .appendTo('#<%=GradeGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

             $.fn.dataTable.ext.errMode = 'none';

         });


    </script>
    <script type="text/jStatus Countavascript">

        $(document).ready(function () {
            var table = $('#<%=GradeGridView.ClientID%>').DataTable({
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
               .appendTo('#<%=GradeGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';

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

        .tab-section {
            background-color: #fff;
        }

        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            padding-right: 10px;
            z-index: 101;
        }
    </style>

    <script type="text/javascript" charset="utf-8">
        function pageLoad() {

        }

        $(document).ready(function () {

            var table = $('#example').DataTable({
                lengthChange: false,
                buttons: [

                    {

                        extend: 'excel',
                        exportOptions: {
                            columns: ':visible'
                        }

                    },

                    {

                        extend: 'pdf',
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
                .appendTo('#example_wrapper .col-sm-6:eq(0)');

        });


    </script>

    <style>
        .container {
            width: 100%;
            margin-right: auto;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>
    <script>
        function openWindow() {
            window.open("html/Datatable1.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container">
        <asp:Panel ID="panelmain" runat="server">

            <div class="panel panel-primary">
                <div class="panel-heading">

                    <div class="row">
                        <div class="col-sm-1"></div>
                        <div class="col-sm-9">
                            <div style="text-align: center; color: #fff; font-size: 16px;" class="text-center text-uppercase"><b>REPORTS</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Reports Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>


                <div class="panel-body">
                    <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">

                   <%-- <div id="tab1" class="tab-section">--%>
                        <div class="row">
                            <div class="col-sm-1 col-xs-12">
                               <b> From Date</b>
                        <asp:TextBox ID="txt_from_date" runat="server" class="form-control"></asp:TextBox>
                                <ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txt_from_date" Format="dd/MM/yyyy" runat="server"></ajax:CalendarExtender>
                            </div>
                            <div class="col-sm-1 col-xs-12">
                               <b> To Date</b>
                        <asp:TextBox ID="txt_to_date" runat="server" class="form-control"></asp:TextBox>
                                <ajax:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_to_date" Format="dd/MM/yyyy" runat="server"></ajax:CalendarExtender>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Technician Wise.</b>
                        <asp:DropDownList ID="ddl_technician_wise" runat="server" OnSelectedIndexChanged="ddl_technician_wise_SelectedIndexChanged" AutoPostBack="True" class="form-control" meta:resourceKey="ddl_genderResource1">
                            <asp:ListItem meta:resourceKey="ListItemResource3" Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Domain Wise.</b>
                        <asp:DropDownList ID="ddl_domain_wise" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_domain_wise_SelectedIndexChanged" class="form-control" meta:resourceKey="ddl_genderResource1">
                            <asp:ListItem meta:resourceKey="ListItemResource3" Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>

                            </div>
                            <div class="col-sm-3 col-xs-12">
                               <b> Please select the functionality to serach.</b>
                        <asp:DropDownList ID="ddl_tables" OnSelectedIndexChanged="ddl_tables_SelectedIndexChanged" runat="server" AutoPostBack="True" class="form-control" meta:resourceKey="ddl_genderResource1">
                            <asp:ListItem meta:resourceKey="ListItemResource3" Text="Select Functionality" Value="0"></asp:ListItem>
                            <asp:ListItem meta:resourceKey="ListItemResource3" Text="Daily Report" Value="daily"></asp:ListItem>
                            <asp:ListItem meta:resourceKey="ListItemResource3" Text="Auditor Report" Value="auditor"></asp:ListItem>
                            <asp:ListItem meta:resourceKey="ListItemResource3" Text="Team Report" Value="team"></asp:ListItem>
                            <%-- <asp:ListItem meta:resourceKey="ListItemResource3" Text="Holidays Details" Value="holidays_details"></asp:ListItem>
                                            <asp:ListItem meta:resourceKey="ListItemResource3" Text="AMC Details" Value="amc_details"></asp:ListItem>
                                            <asp:ListItem meta:resourceKey="ListItemResource3" Text="Service Details" Value="services_details"></asp:ListItem>--%>
                        </asp:DropDownList>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <br />
                                <asp:Button ID="btn_Close" runat="server" class="btn btn-danger" TabIndex="14" Text="CLOSE" OnClick="btn_Close_Click" />

                                &nbsp;&nbsp;<asp:LinkButton runat="server" ID="lnkView4" CssClass="details" OnClick="lnkView_Click_details">Status Count</asp:LinkButton>
                            </div>

                        </div>

                        <asp:Button ID="btn_first" runat="server" CssClass="hidden" Text="Details" />

                        <cc1:ModalPopupExtender ID="modelpopup" runat="server" PopupControlID="Panel10" TargetControlID="btn_first"
                            CancelControlID="btn_second" BackgroundCssClass="Background">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="Panel10" runat="server" CssClass="Popup" align="center" Style="display: none">
                            <iframe style="width: 300px; height: 150px; background-color: #fff;" id="Iframe1" src="p_Status_Count.aspx" runat="server"></iframe>
                            <div class="row text-center" style="width: 100%;">
                                <asp:Button ID="btn_second" CssClass="btn btn-danger" OnClientClick="callfnc()" runat="server" Text="Close" />
                            </div>

                            <br />

                        </asp:Panel>

                        <br />
                        <asp:Panel ID="Panel2" runat="server" ScrollBars="auto" CssClass="grid-view" class="panel-body">
                            <%--<asp:SqlDataSource ID="SqlDataSource" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CELTPAYConnectionString %>" ProviderName="<%$ ConnectionStrings:celtpayConnectionString.ProviderName %>"
                        SelectCommand="Select pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME  ,pay_employee_master.EMP_MOBILE_NO ,pay_employee_master.EMP_EMAIL_ID ,pay_assign_domain.Domain_Name from pay_employee_master inner join pay_assign_domain where pay_employee_master.comp_code=pay_assign_domain.comp_code ">
                        <SelectParameters>
                            <asp:SessionParameter Name="comp_code" SessionField="comp_code" />
                        </SelectParameters>
                    </asp:SqlDataSource>--%>

                            <asp:GridView ID="GradeGridView" class="table" runat="server" Font-Size="X-Small"
                                AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" OnPreRender="gv_expeness_PreRender"
                                OnRowDataBound="GradeGridView_RowDataBound">
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:TemplateField HeaderText="EMP CODE">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_EMP_CODE" runat="server" Text='<%# Eval("EMP_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Technician_Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_tech_name" runat="server" Text='<%# Eval("EMP_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_mobile_no" runat="server" Text='<%# Eval("EMP_MOBILE_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email_Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_email_id" runat="server" Text='<%# Eval("EMP_EMAIL_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Domain_Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_domain_name" runat="server" Text='<%# Eval("Domain_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>

                        </asp:Panel>


                        <br />
                        <br />
                        <div class="row">
                            <b style="font-size: medium;"></b>
                            <asp:Label ID="lbl_name" runat="server" Font-Bold="true" Font-Underline="true" Font-Size="Medium"></asp:Label>
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="300px">
                                <div class="col-sm-12">
                                    <table id="example" class="table table-striped table-bordered table-responsive">
                                        <asp:PlaceHolder ID="BodyContent1" runat="server" />
                                    </table>
                                </div>
                            </asp:Panel>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

</asp:Content>
