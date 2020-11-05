<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shift_calender.aspx.cs" Inherits="Default2" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="Server" ContentPlaceHolderID="cph_title">
    <title>Shift Calender</title>
</asp:Content>
<asp:Content ID="Content2" runat="Server" ContentPlaceHolderID="cph_header">
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
    .


    
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
            var table = $('#<%=shiftcalendar.ClientID%>').DataTable({
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
                   .appendTo('#<%=shiftcalendar.ClientID%>_wrapper .col-sm-6:eq(0)');

                $.fn.dataTable.ext.errMode = 'none';

            });
    </script>



    <style>
        .container {
            max-width: 99%;
        }

        .row {
            margin: 0px;
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

        .grid-view {
            height: auto;
            max-height: 400px;
            overflow: scroll;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }
    </style>

    <style type="text/css">
        .hidden {
            display: none;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding: 10px;
            /*width: 500px;
        height: 210px;*/
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
        }

        .Popupshift {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding: 10px;
            /*width: 1000px;
        height: 800px;*/
        }
    </style>


    <script type="text/javascript">
        function pageLoad() {

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

            $(".date-picker").attr("readonly", "true");

        }
        function validate() {

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            return true;


        }

        $(function () {

            $('#<%=lnkaddtravelplan.ClientID%>').click(function () {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });

            $('#<%=link_excel_download.ClientID%>').click(function () {


                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });

            $('#<%=link_reporting_excel.ClientID%>').click(function () {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });

            $('#<%=LinkButton4.ClientID%>').click(function () {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });


        });

        $(function () {
            $('#<%=LinkButton2.ClientID%>').click(function () {
                if (validate()) {
                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                }
                else {
                    return false;
                }
            });

            $('#<%=LinkButton3.ClientID%>').click(function () {
                if (validate()) {
                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                }
                else {
                    return false;
                }
            });
        });

        function Req_validation() {

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            return true;
        }
        window.onfocus = function () {

            $.unblockUI();

        }

        function openWindow() {
            window.open("html/shiftcalendor.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }


    </script>
    <script type="text/javascript">

        function ShowPopup() {
            $("#btnShowPopup").click();
        }
        function callfnc() {
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            document.getElementById('<%= Button5.ClientID %>').click();
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" runat="Server" ContentPlaceHolderID="cph_righrbody">
    <div class="container-fluid">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1">
                    </div>
                    <div class="col-sm-9">
                        <div class="text-center text-uppercase" style="color: #fff; font-size: small">
                            <b>Shift Calender</b>
                        </div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                        Client Name :
                        <asp:DropDownList ID="ddl_client" DataValueField="client_code" DataTextField="client_name" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                        Branch Name :
                            <asp:DropDownList ID="ddl_unitcode" DataValueField="unit_code" DataTextField="unit_name" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                    </div>
                    <div class="col-lg-1 col-md-2 col-sm-3 col-xs-12">
                        Select Month :
                            <asp:TextBox ID="txt_month_year" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                        <asp:Button ID="btn_process" runat="server" class="btn btn-primary" OnClick="btn_process_Click"
                            OnClientClick="return Req_validation();" Text="Process" />
                        <asp:Button ID="bntclose" runat="server" class="btn btn-danger" OnClick="bntclose_Click" Text="Close" />
                    </div>
                </div>
            </div>

            <div id="main_div" runat="server" class="panel-body">
                <div class="row text-center">
                    <%--Please dont delete this 3 Buttons...Vinod --%>
                    <%--  <asp:Button ID="Button1" runat="server" CssClass="hidden" Text="Claim Expense" />--%>
                    <asp:Button ID="Button3" runat="server" CssClass="hidden" Text="Assign Shift" />
                    <asp:Button ID="Button5" OnClick="Button5_Click" runat="server" CssClass="hidden" />

                    <asp:HiddenField ID="hidden_month" runat="server" />
                    <asp:HiddenField ID="hidden_year" runat="server" />


                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                        BackgroundCssClass="Background" CancelControlID="Button10" PopupControlID="Panel4" TargetControlID="Button3">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panel4" runat="server" CssClass="Popup" Style="display: none">
                        <iframe id="Iframe1" runat="server" src="p_employee_shift_mapping.aspx" style="width: 700px; height: 400px; background-color: #fff;"></iframe>
                        <div class="row text-center">
                            <asp:Button ID="Button10" runat="server" CssClass="btn btn-danger" Text="Close" />
                        </div>

                        <br />

                    </asp:Panel>
                </div>
                <div class="col-sm-12" style="border: 1px solid #000; border-bottom: none; background-color: #eee;">
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                            <h6 class="text-left">
                                <asp:LinkButton ID="lnkaddtravelplan" runat="server" OnClick="lnkaddtravelplan_Click"
                                    Text="Assign Shift"></asp:LinkButton>
                            </h6>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <h6 class="text-left">
                                <asp:LinkButton ID="link_excel_download" runat="server" OnClick="all_employee_excel"
                                    Text="All Employee Excel">
                                </asp:LinkButton>
                            </h6>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <h6 class="text-left">
                                <asp:LinkButton ID="link_reporting_excel" runat="server" OnClick="reporting_excel"
                                    Text="Reporting Employee Excel">
                                </asp:LinkButton>
                            </h6>
                        </div>
                        <div class="col-sm-3 col-xs-12">
                            <h6 class="text-left">
                                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButtonshift_Click"
                                    Text="Shift Wise Employee Calculation">
                                </asp:LinkButton>
                            </h6>
                        </div>

                        <div class="col-sm-2 col-xs-12">
                            <h6 class="text-right">
                                <asp:LinkButton align="left" ID="btnShow" Text="Available Shifts" runat="server" />

                            </h6>
                        </div>
                    </div>
                    <div class="row text-center">
                        <div class="col-sm-12 col-xs-12 text-center">
                            <h4 class="text-center">
                                <asp:LinkButton ID="LinkButton2" Text=" < " OnClientClick="validate();" OnClick="LinkButton2_Click" runat="server" />&nbsp;&nbsp;
                                                <asp:Label ID="lbl_month_year" runat="server" Text="" />
                                &nbsp;&nbsp;
                                                    <asp:LinkButton ID="LinkButton3" Text=" > "
                                                        OnClick="LinkButton3_Click" OnClientClick="validate();" runat="server" />
                            </h4>
                        </div>
                    </div>
                    <div class="row text-center">
                        <%--Please dont delete this 3 Buttons...Vinod --%>
                        <%--  <asp:Button ID="Button1" runat="server" CssClass="hidden" Text="Claim Expense" />--%>
                        <asp:Button ID="btn_shift" runat="server" CssClass="hidden" Text="Shift Wise Employee Calculation" />
                        <asp:Button ID="Button6" OnClick="Button5_Click" runat="server" CssClass="hidden" />

                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
                            BackgroundCssClass="Background" CancelControlID="Button4" PopupControlID="Panelshift" TargetControlID="btn_shift">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="Panelshift" runat="server" CssClass="Popupshift" Style="display: none">
                            <asp:UpdatePanel ID="update" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <asp:Panel ID="Panel2" runat="server" CssClass="grid-view" ScrollBars="Auto">
                                            <asp:Label ID="lbl_header" runat="server" Text=""></asp:Label><br />
                                            <asp:GridView ID="UnitGrid_PF" class="table" runat="server" Width="100%"
                                                ForeColor="#333333" Visible="false" ShowHeaderWhenEmpty="True">
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                            </asp:GridView>
                                            <div class="row text-center">
                                                <asp:Button ID="Button4" CssClass="btn btn-danger" runat="server" Text="Close" />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>

                    </div>
                    <asp:Panel ID="Panel5" runat="server" BackColor="White" CssClass="grid-view" ScrollBars="Auto" meta:resourcekey="Panel5Resource1">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="shiftcalendar" class="table" runat="server" OnPreRender="shiftcalendar_PreRender"
                                    AutoGenerateColumns="false" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="shiftcalendar_RowDataBound" DataKeyNames="emp_code">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Employee">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_emp_name" runat="server" Text='<%# Eval("emp_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id1">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid1" runat="server" Text='<%# Eval("DAY01") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid2" runat="server" Text='<%# Eval("DAY02") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id3">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid3" runat="server" Text='<%# Eval("DAY03") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id4">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid4" runat="server" Text='<%# Eval("DAY04") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id5">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid5" runat="server" Text='<%# Eval("DAY05") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id6">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid6" runat="server" Text='<%# Eval("DAY06") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id7">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid7" runat="server" Text='<%# Eval("DAY07") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id8">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid8" runat="server" Text='<%# Eval("DAY08") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id9">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid9" runat="server" Text='<%# Eval("DAY09") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid10" runat="server" Text='<%# Eval("DAY10") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id11">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid11" runat="server" Text='<%# Eval("DAY11") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id12">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid12" runat="server" Text='<%# Eval("DAY12") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id13">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid13" runat="server" Text='<%# Eval("DAY13") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id14">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid14" runat="server" Text='<%# Eval("DAY14") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id15">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid15" runat="server" Text='<%# Eval("DAY15") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id16">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid16" runat="server" Text='<%# Eval("DAY16") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id17">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid17" runat="server" Text='<%# Eval("DAY17") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id18">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid18" runat="server" Text='<%# Eval("DAY18") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id19">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid19" runat="server" Text='<%# Eval("DAY19") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id20">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid20" runat="server" Text='<%# Eval("DAY20") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id21">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid21" runat="server" Text='<%# Eval("DAY21") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id22">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid22" runat="server" Text='<%# Eval("DAY22") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id23">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid23" runat="server" Text='<%# Eval("DAY23") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id24">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid24" runat="server" Text='<%# Eval("DAY24") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id25">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid25" runat="server" Text='<%# Eval("DAY25") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id26">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid26" runat="server" Text='<%# Eval("DAY26") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id27">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid27" runat="server" Text='<%# Eval("DAY27") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id28">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid28" runat="server" Text='<%# Eval("DAY28") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id29">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid29" runat="server" Text='<%# Eval("DAY29") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id30">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid30" runat="server" Text='<%# Eval("DAY30") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="id31">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid31" runat="server" Text='<%# Eval("DAY31") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="1">
                                            <ItemTemplate>
                                                <asp:DropDownList AppendDataBoundItems="true" DataSourceID="SqlDataSource4" ID="DropDownList1" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY01") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>

                                                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:CELTPAYConnectionString %>"
                                                    ProviderName="<%$ ConnectionStrings:celtpayConnectionString.ProviderName %>"
                                                    SelectCommand="SELECT id, shift_name FROM pay_shift_master WHERE comp_code=@comp_code and UNIT_CODE=@UNIT_CODE">
                                                    <SelectParameters>
                                                        <asp:SessionParameter Name="comp_code" SessionField="comp_code" />
                                                        <asp:SessionParameter Name="UNIT_CODE" SessionField="UNIT_CODE" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="2">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList2" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY02") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="3">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList3" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY03") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="4">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList4" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY04") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="5">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList5" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY05") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="6">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList6" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY06") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="7">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList7" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY07") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="8">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList8" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY08") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="9">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList9" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY09") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="10">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList10" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY10") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="11">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList11" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY11") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="12">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList12" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY12") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="13">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList13" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY13") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="14">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList14" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY14") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="15">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList15" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY15") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="16">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList16" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY16") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="17">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList17" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY17") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="18">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList18" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY18") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="19">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList19" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY19") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="20">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList20" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY20") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="21">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList21" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY21") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="22">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList22" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY22") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="23">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList23" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY23") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="24">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList24" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY24") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="25">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList25" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY25") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="26">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList26" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY26") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="27">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList27" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY27") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="28">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList28" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY28") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="29">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList29" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY29") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="30">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList30" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY30") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="31">
                                            <ItemTemplate>
                                                <asp:DropDownList DataSourceID="SqlDataSource4" ID="DropDownList31" DataTextField="shift_name" DataValueField="id" runat="server" SelectedValue='<%# Bind("DAY31") %>' OnSelectedIndexChanged="shiftcalendar_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Set Shift" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="emp_code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_emp_code" runat="server" Text='<%# Eval("emp_code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>




                    <!-- ModalPopupExtender -->
                    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
                        CancelControlID="btnClose_mp" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel runat="server" CssClass="">
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup grid-view" ScrollBars="Auto" align="center" Style="display: none">
                            <asp:GridView ID="gv_currency" DataSourceID="SqlDataSource6" runat="server" class="table" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3">

                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="True" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Shift">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcat" runat="server" Text='<%# Eval("shift_name")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFROMDESIG" runat="server" Text='<%# Eval("shift_from")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFROMDESIG" runat="server" Text='<%# Eval("shift_to")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:CELTPAYConnectionString %>"
                                ProviderName="<%$ ConnectionStrings:celtpayConnectionString.ProviderName %>"
                                SelectCommand="SELECT shift_name, DATE_FORMAT(shift_from,'%h:%i %p')As shift_from, DATE_FORMAT(shift_to,'%h:%i %p')As shift_to FROM pay_shift_master  WHERE comp_code=@comp_code">
                                <SelectParameters>
                                    <asp:SessionParameter Name="comp_code" SessionField="comp_code" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:Button ID="btnClose_mp" CssClass="btn btn-danger" runat="server" Text="Close" />
                        </asp:Panel>
                        <!-- ModalPopupExtender -->
                    </asp:Panel>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>


