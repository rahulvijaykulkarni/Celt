<%@ Page Language="C#" AutoEventWireup="true" CodeFile="query.aspx.cs" Inherits="AddNewEmployee_query" EnableEventValidation="false" %>

<html>
<head>
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
    <script src="datatable/pdfmake.min.js"></script>
    <style type="text/css">
        .tab-section {
            background-color: #fff;
        }

        .text-red {
            color: #f00;
        }

        .Panel-body {
            padding-top: 0px;
            padding-left: 49px;
            padding-right: 49px;
            padding-bottom: 0px;
        }

        #SearchGridView {
            font-size: 10px;
        }

        .grid-view {
            overflow-x: hidden;
            overflow-y: hidden;
        }

        #ctl00_cph_righrbody_irm1 {
            border: 1px solid gray;
        }
    </style>
</head>

<body>
    <br />
    <br />
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="Panel5" runat="server" CssClass="grid-view panel-body">
            <asp:GridView ID="SearchGridView" class="table" runat="server" Width="100%" ForeColor="#333333" ShowHeaderWhenEmpty="True">
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
            </asp:GridView>
            <br />
            <asp:Label runat="server" ID="header_1" Style="margin-left: 20em; font-size: 17px;"></asp:Label>
            <br />
            <br />
            <br />
        </asp:Panel>
    </form>
</body>
</html>
