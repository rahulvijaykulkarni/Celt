<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="paypro_upload.aspx.cs" Inherits="Billing_rates" Title="Paypro Upload Files" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
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
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }

    </script>

    <script type="text/javascript">
        $(document).ready(function () {


            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_fullmonthot.ClientID%>').DataTable({
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
               .appendTo('#<%=gv_fullmonthot.ClientID%>_wrapper .col-sm-6:eq(0)');
        });
        function pageLoad() {
            $('#<%=ddl_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_itemslist.ClientID%>').DataTable({
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
               .appendTo('#<%=gv_itemslist.ClientID%>_wrapper .col-sm-6:eq(0)');
            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "2000:+100",
                maxDate: 0,
                showButtonPanel: true,
                dateFormat: 'mm/yy',
                onClose: function (dateText, inst) {
                    $('.ui-datepicker-calendar').detach();
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, month, 1));
                }
            }).click(function () {
                $('.ui-datepicker-calendar').hide();
            });

            $(".date-picker").attr("readonly", "true");

            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
                yearRange: '1950',

            });
            $(".date-picker1").attr("readonly", "true");

        }
        function openWindow() {
            window.open("html/paypro_upload.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
        function Req_validation() {

            var txt_month = document.getElementById('<%=txt_month_year.ClientID %>');
                        var FileUpload1 = document.getElementById('<%=FileUpload1.ClientID %>');

                        var ddl_bank = document.getElementById('<%=ddl_bank.ClientID %>');
                        var Selected_ddl_bank = ddl_bank.options[ddl_bank.selectedIndex].text;

                        if (Selected_ddl_bank == "Select") {
                            alert("Please Select Bank");
                            ddl_bank.focus();
                            return false;
                        }
                        if (txt_month.value == "") {
                            alert("Please Select month & year ");
                            txt_month.focus();
                            return false;
                        }
                        if (FileUpload1.value == "") {
                            alert("Please Upload the File ");
                            FileUpload1.focus();
                            return false;
                        }

                        //$.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                        //return true;

                    }
                    $(document).ready(function () {
                        var st = $(this).find("input[id*='hidtab']").val();
                        if (st == null)
                            st = 0;
                        $('[id$=tabs]').tabs({ selected: st });
                    });
                    function valid_email() {
                        var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

            if (Selected_ddl_client == "Select") {
                alert("Please Select Client Name");
                ddl_client.focus;
                return false;
            }
            var txttodate = document.getElementById('<%=txttodate.ClientID %>');
            if (txttodate.value == "") {
                alert("Please Select Month/Year");
                txttodate.focus;
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function valid_email1() {
            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

            if (Selected_ddl_client == "Select") {
                alert("Please Select Client Name");
                ddl_client.focus;
                return false;
            }
            var txttodate = document.getElementById('<%=txttodate.ClientID %>');
              if (txttodate.value == "") {
                  alert("Please Select Month/Year");
                  txttodate.focus;
                  return false;
              }
              if (R_validation2() == false) {
                  return false;
              }
          }
          function R_validation2() {

              var r = confirm("Are you sure you want Resend Record ?");
              if (r == true) {

                  $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

              }

              return r;
          }
          function R_validation() {

              var r = confirm("Are you Sure You Want to Delete Record");
              if (r == true) {

                  $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

              }
              else {
                  alert("Record not Available");
              }
              return r;
          }
          function R_validation1() {

              var r = confirm("Are you sure you want send email?");
              if (r == true) {

                  $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

              }
              else {
                  alert("Record not Available");
              }
              return r;
          }
          function btn_hide() {
              var ddl_upload_type = document.getElementById('<%=ddl_upload_type.ClientID %>');
            var Selected_ddl_upload_type = ddl_upload_type.options[ddl_upload_type.selectedIndex].text;
            if (Selected_ddl_upload_type == "Conveyance") {
                $(".convience").hide();
            }
            if (Selected_ddl_upload_type == "Payment") {
                $(".convience").show();
            }

        }
        function PF_validation() {

            var txt_month = document.getElementById('<%=txt_month_year.ClientID %>');
            var FileUpload1 = document.getElementById('<%=FileUpload1.ClientID %>');

            if (txt_month.value == "") {
                alert("Please Select month & year ");
                txt_month.focus();
                return false;
            }
            var ddl_client_name = document.getElementById('<%=ddl_client_name.ClientID %>');
                var Selected_ddl_client_name = ddl_client_name.options[ddl_client_name.selectedIndex].text;

                if (Selected_ddl_client_name == "Select") {
                    alert("Please Select Client Name");
                    ddl_client_name.focus;
                    return false;
                }
                if (FileUpload1.value == "") {
                    alert("Please Upload the File ");
                    FileUpload1.focus();
                    return false;
                }
            //.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;

            }
            function ESIC_validation() {

                var txt_month = document.getElementById('<%=txt_month_year.ClientID %>');
            var FileUpload1 = document.getElementById('<%=FileUpload1.ClientID %>');

            if (txt_month.value == "") {
                alert("Please Select month & year ");
                txt_month.focus();
                return false;
            }
            var ddl_state_payment = document.getElementById('<%=ddl_state_payment.ClientID %>');
            var Selected_ddl_state_payment = ddl_state_payment.options[ddl_state_payment.selectedIndex].text;

            if (Selected_ddl_state_payment == "Select") {
                alert("Please Select State Name");
                ddl_state_payment.focus;
                return false;
            }
            if (FileUpload1.value == "") {
                alert("Please Upload the File ");
                FileUpload1.focus();
                return false;
            }
            //$.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }
    </script>

    <style type="text/css">
        .text-red {
            color: #f00;
        }

        .HeaderFreez {
            position: relative;
            top: expression(this.offsetParent.scrollTop);
            z-index: 10;
        }

        button, input, optgroup, select, textarea {
            color: inherit;
            margin: 0 0 0 0px;
        }

        * {
            box-sizing: border-box;
        }

        .tab-section {
            background-color: #fff;
        }

        .form-control {
            display: inline;
        }

        .grid-view {
            max-height: 300px;
            overflow-x: hidden;
            overflow-y: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: 16px;" class="text-center text-uppercase"><b>PayPro Upload files</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
            <%-- <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>PayPro Upload files Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>
            <div class="panel-body">
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>--%>
                <div class="shadow">
                    <div id="tabs" style="background: #f3f1fe; padding: 25px 25px 25px 25px; border: 1px solid #e2e2dd; margin-bottom: 20px; margin-top: 20px; border-radius: 10px">
                        <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                        <ul>
                            <li><a data-toggle="tab" href="#item1"><b>PayPro Upload Files</b></a></li>
                            <li><a data-toggle="tab" href="#item2"><b>Send Email</b></a></li>
                        </ul>
                        <div id="item1">
                            <br />
                            <div class="row">
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                    <b>Upload Type :</b><span style="color: red">*</span>
                                    <asp:DropDownList ID="ddl_upload_type" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_upload_type_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Payment" Value="Payment" />
                                        <asp:ListItem Text="Conveyance" Value="Conveyance" />
                                        <asp:ListItem Text="Vendor" Value="Vendor" />
                                        <asp:ListItem Text="Material" Value="Material" />
                                        <asp:ListItem Text="PF Payment" Value="PF Payment" />
                                        <asp:ListItem Text="ESIC Payment" Value="ESIC Payment" />

                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12" id="bank_name" runat="server">
                                    <b>Bank :</b><span style="color: red">*</span>
                                    <asp:DropDownList ID="ddl_bank" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Text="AXIS BANK" Value="AXIS BANK" />
                                        <asp:ListItem Text="INDUSIND BANK" Value="INDUSIND BANK" />
                                    </asp:DropDownList>
                                </div>

                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                                    <b>Month/Year :</b><span class="text-red">*</span>
                                    <asp:TextBox ID="txt_month_year" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12" id="client_ddl" runat="server">
                                    Client Name :
                                            <asp:DropDownList ID="ddl_client_name" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12" id="state_ddl" runat="server">
                                    State :
                                            <asp:DropDownList ID="ddl_state_payment" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                </div>

                                <div class=" col-sm-2 col-xs-12" style="margin-top: 9px;">
                                    <b>File :</b><span class="text-red">*</span>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                </div>
                            </div>
                            </br>
                            <div class="row">
                                <div class="col-sm-6 col-xs-12" id="button" runat="server">
                                    <asp:Button ID="btn_save" runat="server" CssClass="btn btn-primary"
                                        Text="Process" OnClick="btn_save_Click" OnClientClick="return Req_validation();" />

                                    <asp:Button ID="btn_new_save" runat="server" CssClass="btn btn-primary" Width="20%"
                                        Text="New Process" OnClick="btn_new_save_Click" OnClientClick="return Req_validation();" />

                                    <asp:Button ID="btn_excel" runat="server" CssClass="btn btn-primary" Width="30%"
                                        Text="Get Employee Excel" OnClick="btn_excel_Click" />

                                </div>
                            </div>
                            </br>
                            <div class="row">
                                <div class=" col-sm-4 col-xs-12" style="margin-top: 14px;" id="chalan_ESIC" runat="server">
                                    <asp:Button ID="btn_ESIC_challan" runat="server" CssClass="btn btn-primary convience"
                                        Text="Employee ECR" OnClick="btn_ESIC_challan_Click" OnClientClick="return ESIC_validation();" />
                                </div>
                                <div class=" col-sm-4 col-xs-12" style="margin-top: 14px;" id="chalan_PF" runat="server">
                                    <asp:Button ID="btn_PF_chalan" runat="server" CssClass="btn btn-primary convience"
                                        Text="Employee ECR" OnClick="btn_PF_chalan_Click" OnClientClick="return PF_validation();" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Panel ID="Panel1" runat="server" Style="height: auto; overflow-x: hidden; overflow-y: hidden; max-height: 500px;" Visible="false">

                                    <asp:GridView ID="gv_fullmonthot" runat="server" ForeColor="#333333" class="table" OnRowDataBound="gv_fullmonthot_RowDataBound" GridLines="Both" OnPreRender="gv_fullmonthot_PreRender">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="50" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                                    </asp:GridView>

                                </asp:Panel>
                            </div>
                        </div>
                        <div id="item2">

                            <br />
                            <div class="row">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-1 col-xs-12">
                                            <b>Month/Year :</b><span style="color: red">*</span>
                                            <asp:TextBox ID="txttodate" runat="server" class="form-control date-picker text_box" Width="95%"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <b>Client Name :</b><span style="color: red">*</span>
                                            <asp:DropDownList ID="ddl_client" DataValueField="client_code" DataTextField="client_name" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-sm-2 col-xs-12 ">
                                            <b>Select State :</b>
                                            <asp:DropDownList ID="ddl_state" runat="server" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <b>Select Branch :</b>
                                            <asp:DropDownList runat="server" ID="ddlunitselect" CssClass="form-control" />
                                        </div>
                                     </ContentTemplate>
                                </asp:UpdatePanel>
                                
                           </div>
                                    <br />
                                        <div class="row">
                                            <div class="col-sm-10 col-xs-12">
                                                <br />
                                                <asp:Button ID="btn_send_email" runat="server" CssClass="btn btn-primary" Width="15%"
                                                    Text="Salary Slip Email" OnClick="btn_send_email_Click" OnClientClick="return valid_email();" />
                                                <asp:Button ID="btn_emails_not_sent" runat="server" CssClass="btn btn-primary" Width="15%"
                                                    Text="Emails Not Sent" OnClick="btn_emails_not_sent_Click" OnClientClick="return valid_email();" />
                                            <asp:Button ID="btn_download_salary" runat="server" CssClass="btn btn-primary" Width="15%"
                                        Text="Download Salary Slip" OnClick="btn_download_salary_Click" OnClientClick="return valid_email1();" />
                                             <asp:Button ID="btn_resend" runat="server" CssClass="btn btn-primary" Width="15%"
                                        Text="Resend Slip" OnClick="btn_resend_Click" OnClientClick="return valid_email1();" />
                                            </div>
                                           
                              </div>
                                    

                        </div>

                        <br />
                        <div class="container-fluid">
                            <asp:Panel ID="Panel5" runat="server">
                                <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    OnRowDataBound="gv_itemslist_RowDataBound" AutoGenerateColumns="False" Width="100%" OnPreRender="gv_itemslist_PreRender">
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="DELETE">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtn_removeitem" OnClientClick="return R_validation();" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sr No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" />
                                        <asp:BoundField DataField="client_code" HeaderText="CLIENT" SortExpression="client_code" />
                                        <asp:BoundField DataField="state_name" HeaderText="STATE" SortExpression="ESIC_ADDRESS" />
                                        <asp:BoundField DataField="unit_code" HeaderText="BRANCH" SortExpression="unit_code" />
                                        <asp:BoundField DataField="month" HeaderText="MONTH" SortExpression="month" />
                                        <asp:BoundField DataField="unit_code1" HeaderText="BRANCH" SortExpression="unit_code1" />
                                        <asp:BoundField DataField="month" HeaderText="MONTH" SortExpression="month" />
                                        <asp:BoundField DataField="year" HeaderText="YEAR" SortExpression="year" />
                                        <asp:BoundField DataField="reason" HeaderText="REASON" SortExpression="reason" />
                                        <asp:TemplateField HeaderText="SEND EMAIL">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_send_email" runat="server" OnClientClick="return R_validation1();" CausesValidation="false" OnClick="lnk_send_email_Click" Text="SEND EMAIL" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row text-center">
                <asp:Button ID="bntclose" runat="server" CssClass="btn btn-danger"
                    Text="Close" OnClick="bntclose_Click" />
            </div>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
