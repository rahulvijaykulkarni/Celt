<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="lwf_master.aspx.cs" Inherits="mlwf_master" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>MLWF Master</title>
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
    <style>
        .grid-view {
            height: auto;
            max-height: 400px;
            overflow-y: auto;
            overflow-x: auto;
        }

        #first_panel {
            border: 1px solid gray;
        }
    </style>
    <script>
        $(document).ready(function () {
            var table = $('#<%=gv_MLWF.ClientID%>').DataTable({
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
               .appendTo('#<%=gv_MLWF.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';

            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "2017:+100",
                showButtonPanel: true,
                dateFormat: 'mm/yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, month, 1));
                }
            });

            $(".total_cont").attr("readonly", "true");

        });

        function cal_add() {

            var txt_empe = $(".txt_empe").val();
            var txt_empr = $(".txt_empr").val();

            var total_cont = parseFloat($(".txt_empe").val()) + parseFloat($(".txt_empr").val());

            $(".total_cont").val(total_cont);

        }

        $(document).ready(function () {
            $('[id*=chk_head]').click(function () {
                $("[id*='chk_emp']").attr('checked', this.checked);
            });
        });

    </script>
    <style>
        .ui-datepicker-calendar {
            display: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; text-align: center; font-size: small;"><b>MLWF MASTER</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 15px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <asp:Panel runat="server" ID="first_panel" Visible="true" CssClass="panel panel-primary">
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-sm-2 col-xs-12">
                                State :
                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddl_state">
                                <asp:ListItem Text="Select State">Select State</asp:ListItem>
                                <asp:ListItem Text="Andhra Pradesh">Andhra Pradesh</asp:ListItem>
                                <asp:ListItem Text="Chattisgarh">Chattisgarh</asp:ListItem>
                                <asp:ListItem Text="Goa">Goa</asp:ListItem>
                                <asp:ListItem Text="Gujarat">Gujarat</asp:ListItem>
                                <asp:ListItem Text="Haryana">Haryana</asp:ListItem>
                                <asp:ListItem Text="Karnataka">Karnataka</asp:ListItem>
                                <asp:ListItem Text="Madhya Pradesh">Madhya Pradesh</asp:ListItem>
                                <asp:ListItem Text="Maharashtra">Maharashtra</asp:ListItem>
                                <asp:ListItem Text="New Delhi">New Delhi</asp:ListItem>
                                <asp:ListItem Text="Punjab">Punjab</asp:ListItem>
                                <asp:ListItem Text="Tamil Nadu">Tamil Nadu</asp:ListItem>
                                <asp:ListItem Text="West Bengal">West Bengal</asp:ListItem>
                                <asp:ListItem Text="UT of Chandigarh">UT of Chandigarh</asp:ListItem>
                            </asp:DropDownList>
                            </div>


                            <div class="col-sm-3 col-xs-12">
                                Applicablity of the LWF Act :
                            <asp:TextBox ID="txt_lwfact" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-xs-12">
                                Category of Employees to be covered:
                            <asp:TextBox ID="txt_empcategory" runat="server" class="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                Applicability Contract Labours? :
                            <asp:DropDownList ID="ddl_contractlaobou" runat="server" CssClass="form-control">
                                <asp:ListItem Text="yes">yes</asp:ListItem>
                                <asp:ListItem Text="no">no</asp:ListItem>
                            </asp:DropDownList>
                            </div>

                            <div class="col-sm-2 col-xs-12">
                                <%-- MLWF Code :--%>
                                <asp:TextBox ID="txt_id" Style="display: none" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>


                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                                Period :
                            <asp:TextBox ID="txt_city" runat="server" CssClass="form-control text-box"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                Last Day for submission :
                            <asp:TextBox ID="txt_lastday" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                Employee Contribution :
                            <asp:TextBox ID="txt_econtribution" runat="server" CssClass="form-control txt_empe" onchange="cal_add();">0</asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                Employer Contribution :
                            <asp:TextBox ID="txt_comp_contribution" runat="server" CssClass="form-control txt_empr" onchange="cal_add();">0</asp:TextBox>
                            </div>


                        </div>
                    </div>
                </asp:Panel>
                <br />
                <asp:Panel ID="pnl_Tds_Calculation" runat="server" CssClass="grid-view" class="panel-body">
                    <asp:GridView ID="gv_MLWF" runat="server" class="table" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" OnRowDataBound="gv_MLWF_RowDataBound" OnPreRender="gv_MLWF_PreRender" OnSelectedIndexChanged="gv_MLWF_SelectedIndexChanged">

                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                            <asp:CommandField HeaderText="-&gt;" SelectText="-&gt;" ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="Sr. No." Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_srnumber" Visible="false" runat="server" Text='<%# Eval("id")%>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="STATE NAME">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("state_name")%>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="CONTRACT LABOURS">
                                <ItemTemplate>
                                    <asp:Label ID="lblRATE" runat="server" Text='<%# Eval("contract_labours")%>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PERIOD">
                                <ItemTemplate>
                                    <asp:Label ID="lblpaymentdate" runat="server" Text='<%# Eval("period")%>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LAST DAY SUBMITION">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeducionDate" runat="server" Text='<%# Eval("last_day_submission")%>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMPLOYEE CONTRIBUTION">
                                <ItemTemplate>
                                    <asp:Label ID="lblemployee_contribution" runat="server" Text='<%# Eval("employee_contribution")%>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMPLOYER CONTRIBUTION">
                                <ItemTemplate>
                                    <asp:Label ID="lblemployer_contribution" runat="server" Text='<%# Eval("employer_contribution")%>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TOTAL">
                                <ItemTemplate>
                                    <asp:Label ID="lbltotal_contribution" runat="server" Text='<%# Eval("total_contribution")%>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <br />
                <div class="row text-center">
                    <asp:Button ID="btn_save" runat="server" CssClass="btn btn-primary" Text="save" OnClick="btn_save_Click" />
                    <asp:Button ID="btn_update" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="btn_update_Click" />
                    <asp:Button ID="btn_delete" runat="server" CssClass="btn btn-primary" Text="Delete" OnClick="btn_delete_Click" />
                    <asp:Button ID="btn_close" runat="server" CssClass="btn btn-danger" Text="Close" OnClick="btn_close_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

