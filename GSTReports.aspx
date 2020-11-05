<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GSTReports.aspx.cs" Inherits="GSTReports" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>GSTR Reports</title>
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

    <link href="datatableexcel/bootstrap.min.css" rel="stylesheet" />
    <link href="datatableexcel/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <%--<script src="datatable/jquery-1.12.3.js"></script>--%>
    <script src="datatableexcel/jquery.dataTables.min.js"></script>
    <script src="datatableexcel/dataTables.bootstrap.min.js"></script>
    <script src="datatableexcel/dataTables.buttons.min.js"></script>
    <script src="datatableexcel/buttons.bootstrap.min.js"></script>
    <script src="datatableexcel/jszip.min.js"></script>
    <script src="datatableexcel/pdfmake.min.js"></script>
    <script src="datatableexcel/vfs_fonts.js"></script>
    <script src="datatableexcel/buttons.html5.min.js"></script>
    <script src="datatableexcel/buttons.print.min.js"></script>
    <script src="datatableexcel/buttons.colVis.min.js"></script>

    <%--<link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>--%>
    <%--  <script src="datatable/pdfmake.min.js"></script> --%>
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {

            //$(document).ready(function () {
            //    var table = $('#example').DataTable({
            //        lengthChange: false,
            //        buttons: ['copy', 'excel', 'csv', 'pdf', 'colvis']
            //    });

            //    table.buttons().container()
            //        .appendTo('#example_wrapper .col-sm-6:eq(0)');
            //});

            // 27AAECI3733R1Z1

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_gstr1_b2b_csv.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                lengthMenu: [
                        [10, 25, 50,100, -1],
                        ['10', '25', '50','100', 'Show all']
                    ],
                buttons: [
                    {
                        extend: 'excelHtml5',
                       // text: '<a>Excel<i class="fa fa-files-o"></i></a>',
                        exportOptions: {
                            columns: ':visible',
                            //columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                        },
                        filename: 'b2b',
                        sheetName: 'b2b',
                        title: 'b2b',
                        className: 'btn-primary',
                        extension: '.xls',
                        
                    },
                    
                    {
                        extend: 'csvHtml5',
                        exportOptions: {
                            columns: ':visible',
                        },
                        filename: 'b2b',
                        sheetName: 'b2b',
                        title: 'b2b'
                       // extension: '.csv',
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        },
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        download: 'open'

                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },

                    'colvis'
              ]
        //        "bAutoWidth": false,
        //        "aoColumns": [
        //{ "sWidth": "30%" }, // 1st column width 
        //{ "sWidth": "10%" }, // 2nd column width 
        //{ "sWidth": "20%" }, // 3rd column width
        //{ "sWidth": "30%" }, // 4th column width 
        //{ "sWidth": "40%" }, // 5th column width 
        //{ "sWidth": "50%" }, // 6th column width
        //{ "sWidth": "60%" }, // 7th column width 
        //{ "sWidth": "70%" }, // 8th column width 
        //{ "sWidth": "10%" }, // 9th column width
        //{ "sWidth": "10%" }, // 10th column width
        //{ "sWidth": "20%" }, // 11th column width
        //{ "sWidth": "30%" }, // 12th column width
        //{ "sWidth": "50%" } // 13th column width
        //]
        });

        table.buttons().container()
           .appendTo('#<%=gv_gstr1_b2b_csv.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_hsncodecsv.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                buttons: [
                    {
                        extend: 'excelHtml5',
                        exportOptions: {
                            columns: ':visible',
                            //columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                        },
                        filename: 'hsn',
                        sheetName: 'hsn',
                        title: 'hsn'



                    },
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible',
                        },
                        filename: 'hsn',
                        sheetName: 'hsn',
                        title: 'hsn'
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
               .appendTo('#<%=gv_hsncodecsv.ClientID%>_wrapper .col-sm-6:eq(0)');


             $(".date-picker1").datepicker({
                 changeMonth: true,
                 changeYear: true,
                 showButtonPanel: true,
                 dateFormat: 'mm/yy',

             });
             }

    </script>
    <style>
        .label_text {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 10px;
        }

        .auto-style1 {
            color: #FFFFFF;
        }

        .grid-view {
            height: auto;
            max-height: 300px;
            width: auto;
            overflow-y: auto;
            overflow-x: hidden;
        }

        h2 {
            border-radius: 5px;
        }



        h5 {
            font-weight: bold;
            font-size: 15px;
        }

        .text-red {
            color: #f00;
        }

        .panel-body {
            overflow-x: hidden;
        }
    </style>

    <script type="text/javascript">

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

        function date_Val()
        {
            var txt_month = document.getElementById('<%=txt_salarymonth.ClientID %>');

            if (txt_month.value == "") {
                alert("Please Select month & year ");
                txt_salarymonth.focus();
                return false;
            }
            return true;
        }
        function openWindow() {
            window.open("html/GSTReports.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }


    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">

            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>GST REPORTS</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>

            <div class="panel-body">
                <div class="container-fluid" style="background: beige; border: 1px solid #e2e2dd; border-radius: 10px;">
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                           <b> Select Month/Year :</b>
                    <span class="text-red">*</span>
                            <asp:TextBox ID="txt_salarymonth" runat="server" class="form-control date-picker1"></asp:TextBox>
                        </div>
                         
                        <div class="col-xs-2">

                            <asp:Button ID="btn_gstb2b_excel" runat="server" class="btn btn-large" Text="GST Grid View" OnClick="btn_gstb2bcsvfile" OnClientClick="return date_Val();"/>
                        </div>
                         <div class="col-xs-2">
                            <asp:Button ID="btn_gstrcsv" runat="server" class="btn btn-large" Text="GST B2B CSV FILE" OnClick="btn_gstr1b2bcsvfile" OnClientClick="return date_Val();"/>
                        </div>

                        <div class="col-xs-2">
                            <asp:Button ID="btn_hsncodecsv" runat="server" class="btn btn-large" Text="GST HSN Grid View" OnClick="btn_hsncodecsvclick" OnClientClick="return date_Val();"/>
                        </div>

                        <div class="col-xs-2">
                            <asp:Button ID="btn_hsncodecsvfile" runat="server" class="btn btn-large" Text="GST HSN CSV File" OnClick="btn_gstr1hsncodecsvfile" OnClientClick="return date_Val();"/>
                        </div>
                        <div class="col-xs-2">
                            <asp:Button ID="btn_gstr3b" runat="server" class="btn btn-large" Text="GSTR3B Excel" OnClick="btn_gstr3bexcel" OnClientClick="return date_Val();"/>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="container-fluid" runat="server" id="divgv_gstr1_b2b_csv">
                            <br />
                            <asp:Panel ID="panel_gstr1_b2b_csv" runat="server" ScrollBars="Auto" CssClass="grid-view">
                                <asp:GridView ID="gv_gstr1_b2b_csv" class="table" runat="server" DataKeyNames="GSTIN/UIN of Recipient"
                                     ForeColor="#333333"
                                    Font-Size="X-Small" OnPreRender="gv_salary_deuction_PreRender">
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
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                        <br />

                    </div>

                    </br>

                    <div class="row">
                        
                        <div class="container-fluid" runat="server" id="div_hsn_codecsv">
                            <br />
                            <asp:Panel ID="panel_hsncode" runat="server" ScrollBars="Auto" CssClass="grid-view">
                                <asp:GridView ID="gv_hsncodecsv" class="table" runat="server"  ForeColor="#333333"
                                    Font-Size="X-Small" OnPreRender="gv_hsncodecsv_PreRender">
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
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>


                    </div>

                    <%--<div class="panel-body table-responsive" style="background-color: white; font-size: x-small;">
                        <table class="table table-bordered">
                            <table id="example" class="table table-striped table-bordered">
                                <asp:PlaceHolder ID="BodyContent1" runat="server" />
                            </table>
                        </table>
                    </div>--%>
                </div>
                <br />
            </div>
            <br />
    </div>

    </asp:Panel>

    </div>

</asp:Content>
