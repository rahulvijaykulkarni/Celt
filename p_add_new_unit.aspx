<%@ Page Language="C#" AutoEventWireup="true" CodeFile="p_add_new_unit.aspx.cs" Inherits="p_add_new_unit" EnableEventValidation="false" %>

<html>
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta charset="utf-8" />
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
            var table = $('#<%=DepartmentGridView.ClientID%>').DataTable({
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
               .appendTo('#<%=DepartmentGridView.ClientID%>_wrapper .col-sm-6:eq(0)');
        });
          //function pageLoad() {

          //    $(".date-picker1").datepicker({
          //        changeMonth: true,
          //        changeYear: true,
          //        showButtonPanel: true,
          //        dateFormat: 'dd/mm/yy',
          //        minDate: 0,
          //        onSelect: function (selected) {
          //            $(".date-picker2").datepicker("option", "minDate", selected)
          //        }
          //    });


          //    $(".date-picker2").datepicker({
          //        changeMonth: true,
          //        changeYear: true,
          //        showButtonPanel: true,
          //        dateFormat: 'dd/mm/yy',
          //        minDate: 0,
          //        onSelect: function (selected) {
          //            $(".date-picker1").datepicker("option", "maxDate", selected)
          //        }
          //    });


          //    $(".date-picker1").attr("readonly", "true");
          //    $(".date-picker2").attr("readonly", "true");



          //}



          function Req_validation() {

              var t_DeptCode = document.getElementById('<%=txt_item_name.ClientID %>');
            var t_DeptName = document.getElementById('<%=txt_piece_per.ClientID %>');

            // Dept Code

            if (t_DeptCode.value == "") {
                alert("Please Enter Unit Name");
                t_DeptCode.focus();
                return false;
            }

            // Department Name

            if (t_DeptName.value == "") {
                alert("Please Enter Piece per unit");
                t_DeptName.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function isNumber(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {

                    return false;
                }

            }
            return true;
        }

        function AllowAlphabet1(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry < '31') || (keyEntry == '32') || (keyEntry == '46'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }
        function AllowAlphabet_itemname(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry < '31') || !(keyEntry <= '122') || (keyEntry = '45') || (keyEntry = '46'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }
        function openWindow() {
            window.open("html/DepartmentMaster.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
    </script>
    <style>
        .grid-view {
            height: auto;
            max-height: 400px;
            overflow-x: hidden;
            overflow-y: auto;
        }

        .table {
            width: 100%;
        }

        .row {
            margin: 0px;
        }

        body {
            font-family: Verdana;
            font-size: 10px;
        }

        .form-control {
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="background-color: beige;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white">
            <br />
            <div class="row">
                <br />
                <div class="col-sm-1 col-xs-12"></div>
                <div class="col-sm-3 col-xs-12">
                    <asp:Label ID="lblerrmsg" runat="server" Font-Size="Small"></asp:Label>
                    Unit Name:

                                <asp:TextBox ID="txt_item_name" runat="server" class="form-control" onkeypress=" return AllowAlphabet1(event)" MaxLength="50"></asp:TextBox>

                </div>
                <div class="col-sm-3 col-xs-12" style="margin-top: 4px">
                    Piece per unit:
                           
                                <asp:TextBox ID="txt_piece_per" runat="server" class="form-control" onkeypress=" return isNumber(event)" MaxLength="10"></asp:TextBox>

                </div>
                <div class="col-sm-5 col-xs-6" style="margin-top: 7px">
                    <br />
                    <asp:Button ID="btn_newn" runat="server" CssClass="btn btn-primary" OnClick="btnnew_Click"
                        Text="Save" OnClientClick=" return Req_validation();" />
                    <asp:Button ID="txt_update" runat="server" CssClass="btn btn-primary" OnClick="btn_update_click"
                        Text="Update" OnClientClick=" return Req_validation();" />
                    <asp:Button ID="txt_delete" runat="server" CssClass="btn btn-primary" OnClick="btn_delete_click"
                        Text="Delete" />


                </div>
            </div>
            <br />
        </div>
        <br />
        <asp:Panel ID="Panel2" runat="server" CssClass="grid-view">
            <div class="container" style="width: 83%">
                <asp:GridView ID="DepartmentGridView" class="table" runat="server"
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#000"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    OnRowDataBound="DesignationGridView_RowDataBound"
                    OnSelectedIndexChanged="DepartmentGridView_SelectedIndexChanged" Width="100%" OnPreRender="DepartmentGridView_PreRender" Font-Size="Small">
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingRowStyle BackColor="White" />
                    <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" Font-Size="Small" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />

                    <Columns>
                        <asp:CommandField HeaderText="-&gt;" SelectText="-&gt;"
                            ShowSelectButton="True" />
                        <asp:TemplateField HeaderText="Unit Name">
                            <ItemTemplate>
                                <asp:Label ID="lbl_item_name" runat="server" Text='<%# Eval("item_unit_name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--  <asp:BoundField DataField="item_unit_name" HeaderText="Unit Name" 
                                                SortExpression="item_unit_name" />--%>
                        <asp:BoundField DataField="item_pieces" HeaderText="Unit Pieces"
                            SortExpression="item_pieces" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </div>

        </asp:Panel>
        <br />
        <br />







        </ContentTemplate> </asp:UpdatePanel>
    </form>
</body>
</html>
