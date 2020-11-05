<%@ Page Title="payment_history" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="payment_history.aspx.cs" Inherits="payment_history" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Payment History</title>
</asp:Content>
<asp:Content ID="scripts" runat="server" ContentPlaceHolderID="cph_header">
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
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {
            addition_number();
        }
        $(document).ready(function () {
            var table = $('#<%=gv_payment.ClientID%>').DataTable({
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
              .appendTo('#<%=gv_payment.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';

        });
          function validation() {

              var ddl_client = document.getElementById('<%=ddl_client.ClientID%>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

            if (Selected_ddl_client == "Select") {
                alert("Please select Client");
                ddl_client.focus();
                return false;
            }
            var ddl_state = document.getElementById('<%=ddl_state.ClientID%>');
            var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;

            if (Selected_ddl_state == "Select") {
                alert("Please select Client");
                ddl_state.focus();
                return false;
            }

            var txt_date = document.getElementById('<%=txt_date.ClientID%>');
            if (txt_date.value == "") {
                alert("Please Select Month");
                txt_date.focus();
                return false;
            }
            return true;
        }

    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "",
                showButtonPanel: true,
                dateFormat: 'mm/yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).val($.datepicker.formatDate('mm/yy', new Date(year, month, 1)));
                }
            });
            $(".date-picker").focus(function () {
                $(".ui-datepicker-calendar").hide();
            })
            $(".date-picker").attr("readonly", "true");

            $('.date-picker1').datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "",
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',

            });
            $(".date-picker1").attr("readonly", "true");

        });
        function addition_number() {
            var txt_bill_amount = document.getElementById('<%=txt_bill_amount.ClientID %>');
            if (txt_bill_amount.value == "") { txt_bill_amount.value = "0"; }
            var txt_receive_amount = document.getElementById('<%=txt_receive_amount.ClientID %>');
            if (txt_receive_amount.value == "") { txt_receive_amount.value = "0"; }
            var txt_bal_amount = document.getElementById('<%=txt_bal_amount.ClientID %>');
            var result = parseFloat(txt_bill_amount.value) - parseFloat(txt_receive_amount.value);


            if (!isNaN(result)) {
                txt_bal_amount.value = result;
                return txt_bal_amount.value;
            }
            return false;
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
        function Required_validation() {
            var ddl_client_amount = document.getElementById('<%=ddl_client_amount.ClientID %>');
             var Selected_ddl_client_amount = ddl_client_amount.options[ddl_client_amount.selectedIndex].text;

             var ddl_state_amount = document.getElementById('<%=ddl_state_amount.ClientID %>');
             var Selected_ddl_state_amount = ddl_state_amount.options[ddl_state_amount.selectedIndex].text;

             var txt_receive_amount = document.getElementById('<%=txt_receive_amount.ClientID %>');
             var txt_receive_date = document.getElementById('<%=txt_receive_date.ClientID %>');

             if (Selected_ddl_client_amount == "Select") {
                 alert("Please Select Client Name");
                 ddl_client_amount.focus();
                 return false;

             }
             if (Selected_ddl_state_amount == "Select") {
                 alert("Please Select State Name");
                 ddl_state_amount.focus();
                 return false;

             }
             if (txt_receive_amount.value == "0") {
                 alert("Please Enter Receiving Amount");
                 txt_receive_amount.focus();
                 return false;

             }
             if (txt_receive_date.value == "") {
                 alert("Please Enter Received Date");
                 txt_receive_date.focus();
                 return false;

             }

             return true;
         }
         function req_val() {
             if (!(Required_validation())) {
                 return false;
             }
             $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             return true;
         }
         function openWindow() {
             window.open("html/payment_history.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
         }
    </script>
    <style>
       
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <div class="container" style="width: 100%">
        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: small;"><b>Payment  History</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>

            <div class="panel-body">
                <div class="container">
                    <div class="row" id="client_row">

                        <div class="col-sm-2 col-xs-12">
                            Client:<span style="color: red">*</span>

                            <asp:DropDownList ID="ddl_client" runat="server"
                                class="form-control" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true" onkeypress="return AllowAlphabet(event)"
                                MaxLength="10">
                                <asp:ListItem Value="0" Text="&lt;Select&gt;" Enabled="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            State:<span style="color: red">*</span>

                            <asp:DropDownList ID="ddl_state" runat="server"
                                class="form-control" onkeypress="return AllowAlphabet(event)"
                                MaxLength="20">
                            </asp:DropDownList>
                        </div>

                        <div class="col-sm-2 col-xs-12">
                            Date :
                                    <span style="color: red">*</span>
                            <asp:TextBox ID="txt_date" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12" style="margin-top: 16px;">
                            <asp:Button ID="btn_add" runat="server" Text="Submit" class="btn btn-primary" OnClientClick="return validation()" OnClick="btn_add1" />
                        </div>
                        <div class="col-sm-1 col-xs-22" style="margin-top: 16px;">
                            <asp:Button ID="btn_recived_amt" runat="server" Text="Receiving Amount" Visible="false" class="btn btn-danger" OnClientClick="return validation()" />
                        </div>
                    </div>

                    <br />
                    <asp:Panel runat="server" ID="receving_amount">
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                                Client:<span style="color: red">*</span>

                                <asp:DropDownList ID="ddl_client_amount" runat="server" class="form-control" OnSelectedIndexChanged="ddl_client_amount_SelectedIndexChanged" AutoPostBack="true" onkeypress="return AllowAlphabet(event)" MaxLength="10">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                State:<span style="color: red">*</span>

                                <asp:DropDownList ID="ddl_state_amount" runat="server"
                                    class="form-control" onkeypress="return AllowAlphabet(event)"
                                    MaxLength="20">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                Billing Amount :
                                    
                                        <asp:TextBox ID="txt_bill_amount" Text="0" runat="server" CssClass="form-control" ReadOnly="true" onchange="return final_balance_amount();"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                Receiving Amount :<span style="color: red">*</span>

                                <asp:TextBox ID="txt_receive_amount" runat="server" Text="0" CssClass="form-control" OnTextChanged="final_balance_amount" AutoPostBack="true" onkeypress="return isNumber(event)"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                Received Date :
                                    
                                        <asp:TextBox ID="txt_receive_date" runat="server" CssClass="form-control date-picker1 "></asp:TextBox>
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                                Balance Amount :
                                    
                                        <asp:TextBox ID="txt_bal_amount" runat="server" CssClass="form-control" ReadOnly="true" Text="0"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12" style="margin-top: 16px;">
                                <asp:LinkButton ID="lnk_add_details" OnClick="lnk_zone_add_Click" runat="server" OnClientClick="return Required_validation();">
                                      <img alt="Add Item" src="Images/add_icon.png" />
                                </asp:LinkButton>


                            </div>
                        </div>
                        <br />
                        <div class="row text-center">

                            <asp:Button ID="btn_save" runat="server" Text="Save" class="btn btn-primary" OnClick="add_payment_details" OnClientClick="return req_val();" />
                            <asp:Button ID="btn_close" runat="server" Text="Close" class="btn btn-danger" OnClick="btn_close_click" />

                        </div>
                        <br />
                        <br />
                        <asp:Panel ID="panel_payment_detail" runat="server" CssClass="grid">
                            <asp:GridView ID="gv_payment_detail" class="table" runat="server" BackColor="White"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%">
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_remove_head" runat="server" CausesValidation="false" OnClick="lnk_remove_head_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>

                    </asp:Panel>
                    <br />

                    <asp:Panel ID="Panel6" runat="server" CssClass="grid">
                        <asp:GridView ID="gv_payment" class="table" runat="server" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="GradeGridView_RowDataBound"
                            OnSelectedIndexChanged="onSelected_IndexChange"
                            AutoGenerateColumns="False" Width="100%" OnPreRender="gv_payment_PreRender">
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <Columns>

                                <asp:TemplateField HeaderText="Sr No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_GRADE_CODE" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="CLIENT_NAME" HeaderText="Client Name" SortExpression="CLIENT_NAME" />
                                <asp:BoundField DataField="state_name" HeaderText="state_name" SortExpression="state_name" />

                                <asp:BoundField DataField="billing_amt" HeaderText="Billing Amount" SortExpression="billing_amt" />
                                <asp:BoundField DataField="month_year" HeaderText="Billing Month" SortExpression="month_year" ItemStyle-Font-Bold="true" />
                                <asp:BoundField DataField="recived_amt" HeaderText="Recived Amount" SortExpression="recived_amt" />
                                <asp:BoundField DataField="balance_amt" HeaderText="Balance Amount" SortExpression="balance_amt" />
                                <asp:BoundField DataField="balance_amt" HeaderText="Balance Amount" SortExpression="balance_amt" />

                            </Columns>



                        </asp:GridView>
                    </asp:Panel>

                </div>
            </div>
        </asp:Panel>

    </div>
</asp:Content>
