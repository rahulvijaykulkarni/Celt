<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="HolidayMaster.aspx.cs" Inherits="HolidayMaster" Title="Holiday Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_header" runat="server">
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
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>
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

        $(document).ready(function () {
            var table = $('#<%=gvholiday.ClientID%>').DataTable({
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
                   .appendTo('#<%=gvholiday.ClientID%>_wrapper .col-sm-6:eq(0)');

                $.fn.dataTable.ext.errMode = 'none';

            });
    </script>




    <style>
        .grid-view {
            height: auto;
            max-height: 500px;
            overflow: scroll;
        }
    </style>
    <style>
        .auto-style1 {
            color: #FFFFFF;
        }

        h2 {
            border-radius: 5px;
        }

        .grid-view {
            height: auto;
            max-height: 300px;
            overflow: scroll;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }

        .text-red {
            color: #f00;
        }
    </style>

    <script type="text/javascript">

        function pageLoad() {
            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: -15,
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });

            $('.date-picker').attr("readonly", "true");

        }

        function save_validate() {

            var ocassion = document.getElementById('<%=txt_occassion.ClientID %>');
            var holiday_date = document.getElementById('<%=txt_date.ClientID %>');

            if (ocassion.value == "") {
                alert("Please select occassion");
                ocassion.focus();
                return false;
            }

            if (holiday_date.value == "") {
                alert("Please select Date");
                holiday_date.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function Req_Validation() {

            var view_data = document.getElementById('<%=ddl_department.ClientID %>');
            var SelectedText1 = view_data.options[view_data.selectedIndex].text;

            // select Gender
            if (SelectedText1 == "Select Department") {
                alert(" Select Department ");
                view_data.focus();
                return false;
            }


            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }

        window.onfocus = function () {

            $.unblockUI();

        }

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

        $(function () {

            $('#<%=btn_add.ClientID%>').click(function () {
                if (Req_validation()) {

                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

                }
            });

            $('#<%=btn_update.ClientID%>').click(function () {
                if (Req_validation()) {
                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                }
            });



            $('#<%=btn_delete.ClientID%>').click(function () {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });



            $('#<%=gvholiday.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

        });


        $(document).ready(function () {

            $(".js-example-basic-single").select2();

        });

        function openWindow() {

            window.open("html/holiday.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');

        }

    </script>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <div class="container-fluid">

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small" class="text-center text-uppercase"><b>Holiday Master</b></div>

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
                    <div class=" col-sm-2 col-xs-12">
                        Client :
                        <asp:DropDownList ID="ddl_client" DataValueField="client_code" DataTextField="client_name" AutoPostBack="true" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class=" col-sm-2 col-xs-12">
                        Branch :
                        <asp:DropDownList ID="ddl_unitcode" DataValueField="unit_code" DataTextField="unit_name" On AutoPostBack="true" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Department :
                        <asp:DropDownList ID="ddl_department" class="js-example-basic-single text_box" DataValueField="DEPT_CODE" DataTextField="dept" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Branch Name:
                                <asp:DropDownList ID="ddl_unit" class="form-control text_box" DataValueField="unit_code" DataTextField="unit_name" runat="server">
                                </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Occassion :
                                <span class="text-red">*</span>
                        <asp:TextBox ID="txt_occassion" runat="server" onKeyPress="return AllowAlphabet_Number(event)"
                            CssClass="form-control text_box" MaxLength="50"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        Date :
                                <span class="text-red">*</span>
                        <asp:TextBox ID="txt_date" runat="server" CssClass="form-control date-picker text_box"></asp:TextBox>
                    </div>
                </div>
                <br />
                <br />
                <br />
                <br />

                <asp:Panel ID="pnl_edit" runat="server" ScrollBars="Auto" CssClass="grid-view" class="panel-body">
                    <asp:GridView ID="gvholiday" class="table" AutoGenerateColumns="true" runat="server"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        meta:resourcekey="SearchGridViewResource1" OnSelectedIndexChanged="gvholiday_SelectedIndexChanged"
                        OnRowDataBound="OnRowDataBound" HeaderStyle-BackColor="#337AB7"
                        HeaderStyle-ForeColor="WhiteSmoke" OnPreRender="gvholiday_PreRender">
                        <AlternatingRowStyle BackColor="WhiteSmoke" ForeColor="Black" />
                        <FooterStyle BackColor="White" ForeColor="#999999" />
                        <PagerStyle BackColor="White" ForeColor="#999999" HorizontalAlign="center" />
                        <SelectedRowStyle BackColor="#add8e6" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="#ffffff" CssClass="text-uppercase" />
                    </asp:GridView>
                </asp:Panel>
                <br />
                <div class="row text-center">
                    <asp:Button ID="btn_add" runat="server" class="btn btn-primary" Text="Save" OnClick="save_update" OnClientClick="return save_validate();" BackColor="#428BCA" />
                    <asp:Button ID="btn_update" runat="server" class="btn btn-primary" Text="Update" OnClick="btnupdate_Click" OnClientClick="return save_validate();" BackColor="#428BCA" />
                    <asp:Button ID="btn_delete" runat="server" class="btn btn-primary" Text="Delete" OnClick="btndelete_Click" BackColor="#428BCA" />
                    <asp:Button ID="btn_clear" runat="server" class="btn btn-primary" Text="Clear" OnClick="btn_cancel_Click" BackColor="#428BCA" />
                    <asp:Button ID="btn_close" runat="server" CssClass="btn btn-danger" OnClick="btn_Close_Click" Text="Close" />
                </div>
                <br />
            </div>
        </asp:Panel>
    </div>

</asp:Content>
