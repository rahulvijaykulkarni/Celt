<%@ Page Language="C#" AutoEventWireup="true" CodeFile="service_category.aspx.cs" Inherits="service_category"
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

    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function Req_validation() {

            var t_reason = document.getElementById('<%=txt_category.ClientID %>');
             if (t_reason.value == "") {
                 alert("Please Enter Description !!");
                 t_reason.focus();
                 return false;
             }
             return true;

         }

    </script>

    <script type="text/javascript">
        function approve() {
            alert("Add Travel Plan Approved");
        }

        function Rejected() {
            alert("Add Travel Plan Rejected");
        }
    </script>

    <style>
        .grid-view {
            height: auto;
            max-height: 250px;
            overflow: scroll;
        }

        .table {
            width: 100%;
        }

        .row {
            margin: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>


            <br />
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <asp:Panel ID="newpanel" runat="server" CssClass="grid-view text-center" ScrollBars="Auto">
                        <asp:GridView ID="SearchGridView" runat="server" class="table" AutoGenerateColumns="False"
                            CellPadding="3" ForeColor="#333333" Font-Size="X-Small"
                            ShowFooter="false" BorderStyle="None"
                            OnRowDataBound="SearchGridView_RowDataBound">
                            <RowStyle ForeColor="#000066" />
                            <HeaderStyle Font-Size="Small" />

                            <Columns>

                                <asp:BoundField DataField="Id" HeaderText="No." SortExpression="Id" />
                                <asp:BoundField DataField="service" HeaderText="Services" SortExpression="services" />
                                <asp:BoundField DataField="category" HeaderText="Category" SortExpression="category" />


                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        </asp:GridView>
                    </asp:Panel>
                    <br />
                    <div class="col-sm-3 col-xs-12">
                        <asp:Label ID="commenrid" Text="Category Desc:" runat="server"></asp:Label>

                    </div>
                    <div class="col-sm-4 col-xs-12">
                        <asp:TextBox ID="txt_category" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="text-center">
                        <asp:Button ID="btn_add" runat="server" OnClientClick="return Req_validation();" class="btn btn-primary" OnClick="btn_add_Click" Text="Add Category" TabIndex="2" Style="font-size: 13px" />
                    </div>
                </div>

            </div>


        </div>
    </form>
</body>
</html>
