<%@ Page Language="C#" AutoEventWireup="true" CodeFile="p_add_gst_rate.aspx.cs" Inherits="p_add_gst_rate" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
    <script>
        $(document).ready(function () {
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=Grid_gst_rate.ClientID%>').DataTable({
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
               .appendTo('#<%=Grid_gst_rate.ClientID%>_wrapper .col-sm-6:eq(0)');
        });
        function Req_validation() {

            var txt_gst_rate = document.getElementById('<%=txt_gst_rate.ClientID %>');


            // Dept Code

            if (txt_gst_rate.value == "") {
                alert("Please Enter Gst Rate!!!");
                txt_gst_rate.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function Number_Percent(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry < '31') || (keyEntry == '32') || (keyEntry == '46'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }
    </script>
</head>
<body style="background-color: beige">
    <form id="form1" runat="server">
        <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white">
            <br />
            <div class="row">
                <br />
                <div class="col-sm-2 col-xs-12"></div>
                <div class="col-sm-3 col-xs-12">
                    <asp:Label ID="lblerrmsg" runat="server" Font-Size="Small"></asp:Label>
                    Gst Rate:

                                <asp:TextBox ID="txt_gst_rate" runat="server" class="form-control" onkeypress="return Number_Percent(event);" MaxLength="6"></asp:TextBox>

                </div>

                <div class="col-sm-5 col-xs-6" style="margin-top: 7px">
                    <br />
                    <asp:Button ID="btn_save" runat="server" CssClass="btn btn-primary"
                        Text="Save" OnClientClick="return Req_validation();" OnClick="btn_save_Click" />
                    <%-- <asp:Button ID="txt_update" runat="server" CssClass="btn btn-primary" onclick="btn_update_click" 
                                        Text="Update" OnClientClick=" return Req_validation();"/>
                               <asp:Button ID="txt_delete" runat="server" CssClass="btn btn-primary" onclick="btn_delete_click" 
                                        Text="Delete" />--%>
                </div>
            </div>
            <br />
        </div>
        <br />
        <div class="container" style="width: 68%">
            <asp:Panel ID="Panel2" runat="server" CssClass="grid-view">

                <asp:GridView ID="Grid_gst_rate" class="table" runat="server"
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#000"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Width="100%" OnPreRender="Grid_gst_rate_PreRender">
                    <RowStyle ForeColor="#000066" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                        <asp:BoundField DataField="gst_rate" HeaderText="GST RATE" SortExpression="gst_rate" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </asp:Panel>
        </div>
    </form>
</body>
</html>


