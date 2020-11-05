<%@ Page Title="User Dashboard" Language="C#" AutoEventWireup="true" CodeFile="userDashboard.aspx.cs" Inherits="userDashboard" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>User Dashboard</title>
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
    <style>
        .grid-view {
            height: auto;
            max-height: 600px;
            overflow-x: hidden;
            overflow-y: auto;
            font-family: Verdana;
        }

        #ctl00_cph_righrbody_gv_dash_board_filter {
            text-align: right;
        }

        #ctl00_cph_righrbody_gv_dash_board_paginate {
            text-align: right;
        }
    </style>
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        $(document).ready(function () {
            $('#<%=btn_show.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_dash_board.ClientID%>').DataTable({
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
               .appendTo('#<%=gv_dash_board.ClientID%>_wrapper .col-sm-6:eq(0)');

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
        });


    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <div class="container-fluid">
        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: small;"><b>USER DASHBOARD</b></div>
                    </div>
                </div>
            </div>
            <div class="panel-body">


                <div class="row">
                    <div class="col-sm-2 col-xs-12">
                        Select Client :
                                        
                                        <asp:DropDownList ID="ddl_client" runat="server" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control text-box">
                                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Select State :
                                        
                                        <asp:DropDownList ID="ddl_state" runat="server" CssClass="form-control text-box">
                                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Select Type :
                                        
                                        <asp:DropDownList ID="ddl_type" runat="server" CssClass="form-control text-box">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Attendance</asp:ListItem>
                                            <asp:ListItem Value="2">Police Verification</asp:ListItem>
                                            <asp:ListItem Value="3">Rent Agreement</asp:ListItem>

                                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-2 col-xs-12">
                        From Date :
                                        <asp:TextBox ID="txt_satrtdate" runat="server" class="form-control date-picker1"></asp:TextBox>
                    </div>

                    <div class="col-sm-2 col-xs-12">
                        To Date :
                                        <asp:TextBox ID="txt_enddate" runat="server" class="form-control date-picker2"></asp:TextBox>
                    </div>

                    <div class="col-sm-2 col-xs-12">
                        <br />
                        <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click" class="btn btn-primary" Text="SHOW" />
                        &nbsp;&nbsp;
                                <asp:Button ID="btn_close" runat="server" OnClick="btn_close_Click" class="btn btn-danger" Text="Close" />
                    </div>
                </div>
                <br />

            </div>
            <br />
            <div class="row">
                <div class="container">
                    <asp:Panel ID="Panel2" runat="server" CssClass="grid-view">
                        <asp:GridView ID="gv_dash_board" OnRowDataBound="gv_dash_board_RowDataBound" runat="server" ForeColor="#333333" class="table" GridLines="Both" OnPreRender="gv_dash_board_PreRender">
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
                            <Columns>
                                <asp:TemplateField HeaderText="Details">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click">Closed</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
