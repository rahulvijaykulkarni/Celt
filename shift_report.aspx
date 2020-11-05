<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="shift_report.aspx.cs" Inherits="Datatable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Shift Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_header" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta charset="utf-8" />
    <script src="datatable/jquery-1.12.4.js"></script>
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
    <script src="js/select2.min.js"></script>

    <link href="css/new_stylesheet.css" rel="stylesheet" />

    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />

    <link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <%--<script src="datatable/jquery-1.12.3.js"></script>--%>
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
    <style>
        .container {
            max-width: 99%;
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
            max-height: 300px;
            overflow: scroll;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }
    </style>
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
    </style>
    <style type="text/css">
        button, input, optgroup, select, textarea {
            color: inherit;
            margin: 0 0 0 0px;
        }

        .grid-view {
            height: auto;
            max-height: 250px;
            overflow: scroll;
        }

        .tab-section {
            background-color: #fff;
        }

        .row-highlight {
            background-color: Yellow;
        }
    </style>
    <script>

        $(function () {

            $('#<%=btn_search.ClientID%>').click(function () {
                 if (req_validate()) {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 }
             });
         });

         $(document).ready(function () {

             $(".js-example-basic-single").select2();

         });
    </script>
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

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
                onSelect: function (selected) {
                    $(".date-picker1").datepicker("option", "maxDate", selected)
                }
            });

            $(".date-picker1").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");

        });
        $(document).ready(function () {
            var table = $('#example').DataTable({
                destroy: true,
                lengthChange: false,
                buttons: ['copy', 'excel', 'pdf', 'colvis']


            });
            $('#example tbody').on('click', 'tr', function () {
                $(this).toggleClass('selected');
            });
            table.buttons().container()
                .appendTo('#example_wrapper .col-sm-6:eq(0)');

        });



        $(document).ready(function () {

            var table = $('#example').DataTable({
                destroy: true,
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
        function openWindow() {

            window.open("html/Datatable.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');

        }
        function req_validate() {
            var from_date = document.getElementById('<%=txt_from_date.ClientID %>');
           var to_date = document.getElementById('<%=txt_to_date.ClientID %>');

           if (from_date.value == "") {
               alert("Please select from date.");
               from_date.focus();
               return false;
           }
           if (to_date.value == "") {
               alert("Please select to date.");
               to_date.focus();
               return false;
           }
           if (to_date.value.substr(3, 2) != from_date.value.substr(3, 2)) {
               alert("Please select dates of same month only.");
               to_date.focus();
               return false;
           }
           return true;
       }
    </script>

    <style>
        .container {
            width: 100%;
            margin-right: auto;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>



</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <div class="container">

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1">
                    </div>
                    <div class="col-sm-9">
                        <div class="text-center text-uppercase" style="color: #fff; font-size: small">
                            <b>Shift Report</b>
                        </div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Shift Report Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">
                <div class="row">
                    <div class="col-sm-2 col-xs-12">
                       <b> From Date (mm/dd/yyyy):</b>
                        <asp:TextBox ID="txt_from_date" runat="server" Font-Size="X-Small" class="form-control date-picker1"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> To Date (mm/dd/yyyy):</b>
                        <asp:TextBox ID="txt_to_date" runat="server" Font-Size="X-Small" class="form-control date-picker2"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Unit :</b>
                    <asp:DropDownList ID="ddl_tables" AppendDataBoundItems="true" Font-Size="X-Small" runat="server" class="form-control">
                        <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                      <b>  Department :</b>
                    <br />
                        <asp:DropDownList ID="ddl_department" AppendDataBoundItems="true" Font-Size="X-Small" runat="server" class="js-example-basic-single text_box">
                            <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Shift :</b>
                    <asp:DropDownList ID="ddl_shift" AppendDataBoundItems="true" Font-Size="X-Small" runat="server" class="form-control ">
                        <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    </div>
                    <div class="col-sm-2">
                        <br />
                        <asp:Button ID="btn_search" runat="server" OnClientClick="return req_validate();" class="btn btn-primary" Text="Search" OnClick="btn_search_Click" />
                        <asp:Button ID="btn_close" runat="server" CssClass="btn btn-danger" Text="Close" OnClick="btn_close_Click" />
                    </div>
                </div>
            </div>
                </div>
        </asp:Panel>

        <div class="row">
            <div class="col-sm-12">
                <table id="example" class="table table-striped table-bordered table-responsive">
                    <asp:PlaceHolder ID="BodyContent1" runat="server" />
                </table>
            </div>
        </div>

    </div>
</asp:Content>
