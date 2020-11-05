<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Complaint_category.aspx.cs" Inherits="Complaint_category"
    EnableEventValidation="false" %>

<html>
<head>
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
    <script>
        $(function () {
            var table = $('#<%=SearchGridView.ClientID%>').DataTable({
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
              .appendTo('#<%=SearchGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

        });


        function Req_validation() {

            var t_reason = document.getElementById('<%=txt_category.ClientID %>');
             if (t_reason.value == "") {
                 alert("Please Enter category !!");
                 t_reason.focus();
                 return false;
             }
             return true;

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
    </script>
    <style>
        .grid-view {
            overflow-x: hidden;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <br />
                <div class="col-sm-3 col-xs-12"></div>
                <div class="col-sm-3 col-xs-12">
                    Add Category:  <span style="color: red">*</span>

                    <asp:TextBox ID="txt_category" runat="server" class="form-control" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>

                </div>


                <div class="col-sm-5 col-xs-6" style="margin-top: 7px">
                    <br />
                    <asp:Button ID="btn_save" runat="server" CssClass="btn btn-primary"
                        Text="Save" OnClientClick="return Req_validation();" OnClick="btn_add_Click" />



                </div>
            </div>
            <br />
            <br />
            <div class="container" style="width: 70%">
                <asp:Panel ID="newpanel" runat="server" CssClass="grid-view">
                    <asp:GridView ID="SearchGridView" runat="server" class="table" AutoGenerateColumns="False"
                        CellPadding="3" ForeColor="#333333" Font-Size="X-Small" Width="100%" BorderWidth="1px"
                        ShowFooter="false" OnPreRender="GradeGridView_PreRender">

                        <RowStyle ForeColor="#000066" />
                        <%-- <HeaderStyle Font-Size="Small" />--%>

                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="20px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click" Text="Delete" CssClass="btn btn-primary"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sr No.">
                                <ItemStyle Width="20px" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="category" HeaderText="Category" SortExpression="category" />


                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
