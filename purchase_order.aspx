<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="purchase_order.aspx.cs" Inherits="purchase_order" MasterPageFile="~/MasterPage.master" Title="Purchase Order" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Purchase Order</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_header" runat="Server">

    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta charset="utf-8" />


    <!-- Contain the script binding the form submit event -->
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
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>
    <script src="datatable/pdfmake.min.js"></script>
    <script src="Scripts/sweetalert.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" type="text/css" />
    <script>
        function unblock()
        { $.unblockUI(); }
        function pageLoad() {
            item_type();
            $('#<%=ddl_item_type.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_item_name.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
          //  $('#<%=ddl_vendortype.ClientID%>').change(function () {
           //     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
           // });
            $('#<%=ddl_customerlist.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            var table = $('#<%=gv_bynumber_name.ClientID%>').DataTable({
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
              .appendTo('#<%=gv_bynumber_name.ClientID%>_wrapper .col-sm-6:eq(0)');

            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: -180,
            });

            $('.date-picker2').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });
            $(".date-picker1").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");
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

        //  AllowAlphabet_address(event);

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
        function isNumber_dot(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 36 || charCode > 41) && (charCode < 45 || charCode > 47)) {

                    return false;

                }

            }
            return true;
        }

        function amount_save()
        {
            var txt_sub_total = document.getElementById('<%=txt_sub_total.ClientID %>');

            if (txt_sub_total.value == "" || txt_sub_total.value == "0") {
                alert("Please Enter Sub Total");
                txt_sub_total.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }

        function Req_validation() {
            var txt_docno = document.getElementById('<%=txt_docno.ClientID %>');
            if (txt_docno.value == "") {
                alert("Please Enter Purchase Order Number");
                txt_docno.focus();
                return false;
            }
            var ddl_vendortype = document.getElementById('<%=ddl_vendortype.ClientID %>');
            var Selected_ddl_vendortype = ddl_vendortype.options[ddl_vendortype.selectedIndex].text;

            if (Selected_ddl_vendortype == "Select") {
                alert("Please Select Vendor Type");
                ddl_vendortype.focus();
                return false;
            }
            var ddl_vendor_categories = document.getElementById('<%=ddl_vendor_categories.ClientID %>');
            var Selected_ddl_vendor_categories = ddl_vendor_categories.options[ddl_vendor_categories.selectedIndex].text;

            if (Selected_ddl_vendor_categories == "Select") {
                alert("Please Select Vendor Categories ");
                ddl_vendor_categories.focus();
                return false;
            }

            var ddl_customerlist = document.getElementById('<%=ddl_customerlist.ClientID %>');
            var Selected_ddl_customerlist = ddl_customerlist.options[ddl_customerlist.selectedIndex].text;

            if (Selected_ddl_customerlist == "Select") {
                alert("Please Select Vendor Name");
                ddl_customerlist.focus();
                return false;
            }
            var txt_customer_gst = document.getElementById('<%=txt_customer_gst.ClientID %>');
            if (txt_customer_gst.value == "") {
                alert("Please Enter Vendor GST Number");
                txt_customer_gst.focus();
                return false;
            }
            var ddl_sales_person = document.getElementById('<%=ddl_sales_person.ClientID %>');
            if (ddl_sales_person.value == "") {
                alert("Please Enter P.O Generated By");
                ddl_sales_person.focus();
                return false;
            }
            var txt_mobileno = document.getElementById('<%=txt_mobileno.ClientID %>');
            if (txt_mobileno.value == "") {
                alert("Please Enter Mobile Number");
                txt_mobileno.focus();
                return false;
            }
            var ddl_item_type = document.getElementById('<%=ddl_item_type.ClientID %>');
            var Selected_ddl_item_type = ddl_item_type.options[ddl_item_type.selectedIndex].text;

            if (Selected_ddl_item_type == "Select") {
                alert("Please Select Item Type");
                ddl_item_type.focus();
                return false;
            }
            var ddl_item_name = document.getElementById('<%=ddl_item_name.ClientID %>');
            var Selected_ddl_item_name = ddl_item_name.options[ddl_item_name.selectedIndex].text;

            if (Selected_ddl_item_name == "Select") {
                alert("Please Select Item Name");
                ddl_item_name.focus();
                return false;
            }
            var ddl_uniform_size = document.getElementById('<%=ddl_uniform_size.ClientID %>');
            var Selected_ddl_uniform_size = ddl_uniform_size.options[ddl_uniform_size.selectedIndex].text;

            if (Selected_ddl_item_type == "Uniform") {
                if (Selected_ddl_uniform_size == "Select") {
                    alert("Please Select Uniform Size");
                    ddl_uniform_size.focus();
                    return false;
                }
            }
            var ddl_shoose_size = document.getElementById('<%=ddl_shoes_size.ClientID %>');
            var Selected_ddl_shoose_size = ddl_shoose_size.options[ddl_shoose_size.selectedIndex].text;

            if (Selected_ddl_item_type == "Shoes") {
                if (Selected_ddl_shoose_size == "Select") {
                    alert("Please Select Shoes Size");
                    ddl_shoose_size.focus();
                    return false;
                }
            }

            var ddl_pantry_jacket = document.getElementById('<%=ddl_pantry_jacket.ClientID %>');
            var Selected_ddl_pantry_jacket = ddl_pantry_jacket.options[ddl_pantry_jacket.selectedIndex].text;

            if (Selected_ddl_item_type == "Pantry Jacket") {
                if (Selected_ddl_pantry_jacket == "Select") {
                    alert("Please Select Shoes Size");
                    ddl_pantry_jacket.focus();
                    return false;
                }
            }

            var ddl_apron_size = document.getElementById('<%=ddl_apron_size.ClientID %>');
            var Selected_ddl_apron_size = ddl_apron_size.options[ddl_apron_size.selectedIndex].text;

            if (Selected_ddl_item_type == "Apron") {
                if (Selected_ddl_apron_size == "Select") {
                    alert("Please Select Apron Size");
                    ddl_apron_size.focus();
                    return false;
                }
            }

            var txt_description = document.getElementById('<%=txt_description.ClientID %>');

            if (txt_description.value == "") {
                alert("Please Enter Description");
                txt_description.focus();
                return false;
            }

            var txt_hsn_code = document.getElementById('<%=txt_hsn_code.ClientID %>');

            if (txt_hsn_code.value == "") {
                alert("Please Enter HSN Code");
                txt_hsn_code.focus();
                return false;
            }
            var txt_qty = document.getElementById('<%=txt_qty.ClientID %>');

            if (txt_qty.value == "" || txt_qty.value == "0") {
                alert("Please Enter Quantity");
                txt_qty.focus();
                return false;
            }

            var txt_per_unit_rate = document.getElementById('<%=txt_per_unit_rate.ClientID %>');

            if (txt_per_unit_rate.value == "" || txt_per_unit_rate.value == "0") {
                alert("Please Enter Per Unit Rate");
                txt_per_unit_rate.focus();
                return false;
            }

            var txt_amount = document.getElementById('<%=txt_amount.ClientID %>');

            if (txt_amount.value == "" || txt_amount.value == "0") {
                alert("Please Enter Amount");
                txt_amount.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function item_type() {
            var ddl_item_type = document.getElementById('<%=ddl_item_type.ClientID %>');
            var Selected_ddl_item_type = ddl_item_type.options[ddl_item_type.selectedIndex].text;
            if (Selected_ddl_item_type == "Uniform") {
                $(".uniform").show();
                $(".shoes").hide();
                $(".pantry").hide();
                $(".apron").hide();
            }
            else if (Selected_ddl_item_type == "Shoes") {
                $(".uniform").hide();
                $(".pantry").hide();
                $(".apron").hide();
                $(".shoes").show();
            }
            else if (Selected_ddl_item_type == "Pantry Jacket") {
                $(".pantry").show();
                $(".uniform").hide();
                $(".shoes").hide();
                $(".apron").hide();
            }
            else if (Selected_ddl_item_type == "Apron") {
                $(".apron").show();
                $(".uniform").hide();
                $(".shoes").hide();
                $(".pantry").hide();
            }
            else {
                $(".uniform").hide();
                $(".shoes").hide();
                $(".pantry").hide();
                $(".apron").hide();
            }
        }

        function add_percent() {
            var txt_advance = document.getElementById('<%=txt_advance.ClientID %>');
            var txt_remain = document.getElementById('<%=txt_remain.ClientID %>');
            if (txt_advance.value == "") {
                txt_advance.value = 25;
                txt_remain.value = 75;
            }
            var total = 100;
            var percent1 = (total) - parseInt(txt_advance.value);
            txt_remain.value = percent1;
            var percent2 = (total) - parseInt(txt_remain.value);
            txt_advance.value = percent2;
            if (txt_advance.value < 0 || txt_advance.value > 100) {
                alert("Enter Percentage Less than 100 and Greater than 0");
                txt_advance.value = 25;
                txt_remain.value = 75;
                txt_advance.focus();
            }

        }
        function add_percent2() {
            var txt_advance = document.getElementById('<%=txt_advance.ClientID %>');
            var txt_remain = document.getElementById('<%=txt_remain.ClientID %>');
            var total = 100;
            var percent2 = (total) - parseInt(txt_remain.value);
            txt_advance.value = percent2;
            if (txt_remain.value < 0 || txt_remain.value > 100) {
                alert("Enter Percentage Less than 100 and Greater than 0");
                txt_remain.value = 75;
                txt_advance.value = 25;
                txt_remain.focus();

            }
            if (txt_remain.value == "") {
                txt_advance.value = 25;
                txt_remain.value = 75;
            }
        }
        function AllowAlphabet_address(e) {
            if (null != e) {
                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) ||
                    (keyEntry == '32') || (keyEntry == '38') || ((keyEntry == '39') && (keyEntry == '34')) || (keyEntry == '44') || ((keyEntry >= '45') && (keyEntry <= '47')) ||
                    (keyEntry == '58') || (keyEntry == '59') || (keyEntry == '61') || (keyEntry == '92'))
                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
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
        function AllowAlphabet(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45' || (keyEntry == '8'))
                return true;
            else {
                // alert('Please Enter Only Character values.');
                return false;
            }
        }
        function R_validation() {
            var r = confirm("Are you Sure You Want to Delete Record");
            if (r == true) {
                ($.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } }))
                return true;
            }
            else {
                return false;
            }
        }
        function R_validation1() {
            var r = confirm("Are you Sure You Want to Final This PO ");
            if (r == true) {
                ($.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } }))
                return true;
            }
            else {
                return false;
            }
        }
        function openWindow() {
            window.open("html/purchase_order.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

 </script>
    
    <style>
        th, td
        {
            border-color: #d8d6d6;
            width: 10%;
        }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="cph_righrbody" runat="server">
    <div class="container-fluid">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: small;"><b>Purchase Order</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow()">
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Purchase Order Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body" >
                <asp:Panel runat="server" ID="search_order">
                    <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-left:15px; margin-right:15px; margin-bottom:20px; margin-top:20px">
                        <br />
                        <div class="row">
                            <div class="col-sm-1 col-xs-12 "></div>
                            <div class="col-sm-2 col-xs-12 ">
                              <b>  Purchase Order Number :</b>
          
                        <asp:TextBox ID="txt_docno_number" runat="server" class="form-control text_box"
                            OnTextChanged="txt_docno_number_TextChanged"  MaxLength="10" onkeypress="return AllowAlphabet_Number(event,this);">P</asp:TextBox>

                            </div>
                            <div class="col-sm-3 col-xs-12 ">
                               <b> Vendor Name :</b>
                    <asp:TextBox ID="txt_customername" runat="server" CssClass="form-control text_box"
                        OnTextChanged="txt_customername_TextChanged" onkeypress="return AllowAlphabet_address(event);" MaxLength="30"></asp:TextBox>

                            </div>
                            <br />
                            <div class="col-sm-3 col-xs-12">

                                <asp:Button ID="btn_searchvendor" runat="server" CausesValidation="False" CssClass="btn btn-primary" Text=" Search " OnClick="btn_searchvendor_Click" />
                                <asp:Button ID="btn_Closeup" OnClick="btn_Close_Click" runat="server" CausesValidation="False" CssClass="btn btn-danger" Text="Close" />

                            </div>
                        </div>
                        <br />

                        <asp:Panel ID="Panel5" runat="server">
                            <asp:GridView ID="gv_bynumber_name" class="table" runat="server" AutoGenerateColumns="False"
                                ShowHeader="true" OnRowDataBound="gv_bynumber_name_RowDataBound"
                                CellPadding="4" ForeColor="#333333" Height="36px" GridLines="Both" OnPreRender="gv_bynumber_name_PreRender">

                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="11px" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="White" ForeColor="#000066" />


                                <Columns>
                                    <asp:BoundField DataField="PO_NO" HeaderText="PO_NO"
                                        SortExpression="PO_NO" />
                                    <asp:TemplateField HeaderText="Estimate No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_pocnumber" runat="server" Text='<%# Eval("PO_NO")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Estimate Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_podate" runat="server" Text='<%# Eval("PO_DATE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_customername" runat="server"
                                                Text='<%# Eval("CUST_NAME")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_amount" runat="server"
                                                Text='<%# Eval("GRAND_TOTAL")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Get Details">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnk_button" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Get Details" OnClick="lnk_button_Click" Style="color: white" OnClientClick="return R_valid();"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </asp:Panel>
                        <br />

                    </div>
                </asp:Panel>
                <br />
                <asp:Panel runat="server" ID="order_details">
                    <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-left:15px; margin-right:15px; margin-bottom:20px; margin-top:20px">
                        <br />
                        <div class="row">
                            <div class="col-sm-1 col-xs-12"></div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Purchase Order Number :</b>
                                         <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                <asp:TextBox ID="txt_docno" runat="server" ReadOnly="true" CssClass="form-control text_box" MaxLength="10" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <span><b>Vendor Type:</b><span style="color: red">*</span>
                                </span>
                                <asp:DropDownList ID="ddl_vendortype" runat="server" MaxLength="30"  CssClass="form-control text_box" >
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Regular">Regular</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                             <div class="col-sm-2 col-xs-12">
                          <b>  Vendor Categories :</b><span style="color: red">*</span>
                                <asp:DropDownList ID="ddl_vendor_categories" runat="server" OnSelectedIndexChanged="ddl_vendortype_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="Electrical">Electrical</asp:ListItem>
                                    <asp:ListItem Value="Plumbing">Plumbing</asp:ListItem>
                                    <asp:ListItem Value="Carpentry">Carpentry</asp:ListItem>
                                    <asp:ListItem Value="Civil">Civil</asp:ListItem>
                                    <asp:ListItem Value="Pest_Control">Pest Control</asp:ListItem>
                                    <asp:ListItem Value="HVAC">HVAC</asp:ListItem>
                                    <asp:ListItem Value="Stationery">Stationery</asp:ListItem>
                                    <asp:ListItem Value="Chemical_Supplier">Chemical Supplier</asp:ListItem>
                                    <asp:ListItem Value="Shoes">Shoes</asp:ListItem>
                                    <asp:ListItem Value="Uniform">Uniform</asp:ListItem>
                                    <asp:ListItem Value="courier_services">Courier Services</asp:ListItem>
                                    <asp:ListItem Value="Housekeeping_services">Housekeeping services</asp:ListItem>
                                     <asp:ListItem Value="Other">Other</asp:ListItem>

                                </asp:DropDownList>
                        </div>
                            <div class="col-sm-2 col-xs-12">
                                <span>
                                    <asp:Label ID="lbl_vendor" runat="server" Font-Bold="true" Text="Vendor Name:"></asp:Label>
                                    <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </span>
                                    <asp:DropDownList ID="ddl_customerlist" runat="server" MaxLength="30"
                                        OnSelectedIndexChanged="customer_details" CssClass="form-control text_box" AutoPostBack="true">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Vendor GST Number:</b><span style="color: red">*</span>
                                <asp:TextBox ID="txt_customer_gst" runat="server" MaxLength="30" CssClass="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)">
                                </asp:TextBox>
                            </div>
                           
                        </div>
                        <div class="col-sm-2 col-xs-12" style="display: none;">
                           <b> Bill Year :</b>
                                    <asp:TextBox ID="txt_year" runat="server" MaxLength="4" class="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                            <asp:TextBox ID="txt_referenceno1" runat="server" MaxLength="20" CssClass="form-control" Visible="false"></asp:TextBox>

                        </div>

                        <br />
                        <div class="row">
                            <div class="col-sm-1 col-xs-12"></div>
                             <div class="col-sm-2 col-xs-12">
                               <b> Purchase Order Date : </b>
                                        <asp:TextBox ID="txt_docdate" runat="server" Style="margin-bottom: 0px" class="form-control date-picker2 text_box"></asp:TextBox>
                            </div>
                             <div class="col-sm-2 col-xs-12">
                                <asp:Label ID="Label12" runat="server" Font-Bold="true" Text="Address :"></asp:Label>
                                <asp:TextBox ID="txt_address" runat="server" TextMode="MultiLine" Rows="2" Columns="2" MaxLength="70" ReadOnly="true" Class="form-control text_box"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="P.O Generated By :"></asp:Label><span style="color:red">*</span>
                                <asp:TextBox ID="ddl_sales_person" runat="server" MaxLength="70" Class="form-control text_box" onkeypress="return AllowAlphabet(event);"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Mobile No :"></asp:Label><span style="color:red">*</span>
                                <asp:TextBox ID="txt_mobileno" runat="server" MaxLength="10" Class="form-control text_box" onkeypress="return isNumber(event)"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <asp:Label ID="Label8" runat="server" Font-Bold="true" Text="Advance in %:"></asp:Label>
                                <asp:TextBox ID="txt_advance" runat="server" MaxLength="10" Class="form-control text_box Maths" Text="25" onkeypress="return isNumber_dot(event);" onchange="add_percent();"></asp:TextBox>
                            </div>
                           
                             
                        </div>
                        <br />
                         <div class="row">
                            <div class="col-sm-1 col-xs-12"></div>
                              <div class="col-sm-2">
                                <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Remaining % After Advance :"></asp:Label>
                                <asp:TextBox ID="txt_remain" runat="server" MaxLength="10" Class="form-control text_box Maths" Text="75" onkeypress="return isNumber(event)" onchange="add_percent2();"></asp:TextBox>
                            </div>
                              <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12" runat="server" visible="false">
                               <b> Client Name :</b><span class="text-red">*</span>
                                <asp:DropDownList ID="ddl_client" DataValueField="client_code" DataTextField="client_name"  runat="server" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12" runat="server" visible="false">
                               <b> Branch Name :</b><span class="text-red">*</span>
                                <asp:DropDownList ID="ddl_branch" DataValueField="client_code" DataTextField="client_name"  runat="server" CssClass="form-control" >
                                </asp:DropDownList>
                            </div>
                             </div>
                    </div>
                </asp:Panel>
                <br />
                <asp:Panel runat="server" ID="add_order">
                    <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-left:15px; margin-right:15px; margin-bottom:20px; margin-top:20px">
                        <br />
                        <table class="table table-striped" border="1" style="border-color: #d8d6d6">
                            <tr>
                                <th>Item Type</th>
                                <th>Item Name</th>
                                <th style="display: none;" class="uniform">Uniform Size</th>
                                <th style="display: none;" class="shoes">Shoes Size</th>
                                <th style="display: none;" class="pantry">Pantry Jacket</th>
                                <th style="display: none;" class="apron">Apron Size</th>
                                <th>Description</th>
                                <th>HSN Code</th>
                                <th>GST RATE</th>
                                <th>Quantity</th>
                                <th>Per Unit Rate</th>
                                <th>Amount</th>
                                <th>Add</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddl_item_type" runat="server" MaxLength="30" OnSelectedIndexChanged="ddl_item_type_SelectedIndexChanged" CssClass="form-control text_box" AutoPostBack="true" onchange="item_type();">
                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                        <asp:ListItem Value="chemicals">Chemicals</asp:ListItem>
                                        <asp:ListItem Value="housekeeping_material">Housekeeping Materials</asp:ListItem>
                                        <asp:ListItem Value="Shoes">Shoes</asp:ListItem>
                                        <asp:ListItem Value="Uniform">Uniform</asp:ListItem>
                                        <asp:ListItem Value="pantry_jacket">Pantry Jacket</asp:ListItem>
                                        <asp:ListItem Value="Apron">Apron</asp:ListItem>
                                         <asp:ListItem Value="courier_services">Courier Services</asp:ListItem>
                                        <asp:ListItem Value="ID_Card">ID Card</asp:ListItem>
                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList ID="ddl_item_name" runat="server" MaxLength="30" CssClass="form-control text_box"
                                        OnSelectedIndexChanged="particular_hsn_sac_code" AutoPostBack="true">
                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td style="display: none;" class="uniform">
                                    <asp:DropDownList ID="ddl_uniform_size" runat="server" MaxLength="30" CssClass="form-control text_box">
                                        <asp:ListItem Value="Select">Select</asp:ListItem>

                                    </asp:DropDownList></td>
                                <td style="display: none;" class="shoes">
                                    <asp:DropDownList ID="ddl_shoes_size" runat="server" MaxLength="30" CssClass="form-control text_box">
                                        <asp:ListItem Value="Select">Select</asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                                <td style="display: none;" class="pantry">
                                    <asp:DropDownList ID="ddl_pantry_jacket" runat="server" MaxLength="30" CssClass="form-control text_box">
                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="display: none;" class="apron">
                                    <asp:DropDownList ID="ddl_apron_size" runat="server" MaxLength="30" CssClass="form-control text_box">
                                        <asp:ListItem Value="Select">Select</asp:ListItem>

                                    </asp:DropDownList>

                                </td>
                                <td>
                                    <asp:TextBox ID="txt_description" TextMode="MultiLine" Rows="1" Columns="1" runat="server" CssClass="form-control text_box" onkeypress="return AllowAlphabet_address(event);"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txt_hsn_code" runat="server" CssClass="form-control text_box" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                <td>
                                <asp:TextBox ID="txt_vat" runat="server"  CssClass="form-control text_box"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_qty" runat="server" CssClass="form-control text_box" onkeypress="return isNumber(event);">0</asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txt_per_unit_rate" runat="server" OnTextChanged="txt_quantity_TextChanged" AutoPostBack="true" CssClass="form-control text_box" onkeypress="return isNumber_dot(event);">0</asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txt_amount" runat="server" ReadOnly="true" CssClass="form-control text_box">0</asp:TextBox></td>
                                <td>
                                    <asp:LinkButton ID="lnkbtn_addmoreitem" OnClick="lnkbtn_addmoreitem_Click" runat="server" OnClientClick="return Req_validation();"><img alt="Add Item" src="Images/add_icon.png"  /></asp:LinkButton></td>
                            </tr>
                        </table>
                        <br />
                    </div>
                    <br />
                    <asp:Panel ID="Panel4" runat="server" CssClass="grid-view" ScrollBars="Auto">

                        <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            AutoGenerateColumns="False" OnRowDataBound="gv_itemslist_RowDataBound" >

                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="White" ForeColor="#000066" />

                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sr No.">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CODE">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_item_code" runat="server" Text='<%# Eval("ITEM_CODE")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Type">
                                    <ItemStyle Width="85px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_item_type" runat="server" Text='<%# Eval("item_type")%>' Width="155px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Particular">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_particular" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("PARTICULAR")%>' Width="150px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Uniform Size">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="35px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_uniformsize" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("size_uniform")%>' Width="150px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Shoes Size">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="35px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_shoessize" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("size_shoes")%>' Width="150px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="pantry_jacket Size">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="35px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_pantrysize" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("size_pantry")%>' Width="150px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Apron Size">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="35px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_apronsize" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("size_apron")%>' Width="150px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Description">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_Description_final" ReadOnly="True" TextMode="MultiLine" runat="server" Style="text-align: left" class="form-control lstyle" Text='<%# Eval("Description")%>' onkeyup="textAreaAdjust(this)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="GST">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_vat" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("VAT")%>' Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="HSN_Code">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_hsn_code" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("HSN_Code")%>' Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemStyle Width="70px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_quantity" runat="server" ReadOnly="True" onkeypress="return isNumber_dot(event)" Style="text-align: left" class="form-control" Text='<%# Eval("QUANTITY")%>' AutoPostBack="True" Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Per_Unit_Rate">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="90px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_rate" runat="server" ReadOnly="True" Style="text-align: left" onkeypress="return isNumber_dot(event)" class="form-control" Text='<%# Eval("RATE")%>' AutoPostBack="True" Width="90px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemStyle Width="90px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_amount" runat="server" Style="text-align: right" class="form-control" Text='<%# Eval("AMOUNT") %>' ReadOnly="True" Width="90px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
                <br />
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-left:15px; margin-right:15px; margin-bottom:20px; margin-top:20px">
                    <br />
                    <div class="row">

                        <div class="col-sm-8 col-xs-12">
                        </div>
                        <div class="col-sm-4 col-xs-12">
                            <div class="row">
                                <div class="col-sm-6" style="text-align: right">
                                    <asp:Label runat="server" Font-Bold="true" Text="Sub Total :"></asp:Label>
                                </div>
                                <div class="col-sm-6" style="text-align: right">
                                    <asp:TextBox ID="txt_sub_total" runat="server" ReadOnly="True" CssClass="form-control text_box">0</asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-8 col-xs-12">
                        </div>
                        <div class="col-sm-4 col-xs-12">
                            <div class="row">
                                <div class="col-sm-6" style="text-align: right">
                                    <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="CGST (%):"></asp:Label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txt_CGST" runat="server" ReadOnly="true" onkeypress="return isNumber(event)" CssClass="form-control text_box">0</asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-8 col-xs-12">
                        </div>
                        <div class="col-sm-4 col-xs-12">
                            <div class="row">
                                <div class="col-sm-6" style="text-align: right">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="SGST (%):"></asp:Label>

                                </div>

                                <div class="col-sm-6">
                                    <asp:TextBox ID="txt_sgst" runat="server" ReadOnly="true" onkeypress="return isNumber(event)" CssClass="form-control text_box">0</asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-8 col-xs-12">
                        </div>
                        <div class="col-sm-4 col-xs-12">
                            <div class="row">
                                <div class="col-sm-6" style="text-align: right">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="IGST (%):"></asp:Label>

                                </div>

                                <div class="col-sm-6">
                                    <asp:TextBox ID="txt_igst" runat="server" ReadOnly="true" onkeypress="return isNumber(event)" CssClass="form-control text_box">0</asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-8 col-xs-12">
                        </div>
                        <div class="col-sm-4 col-xs-12">
                            <div class="row">
                                <div class="col-sm-6" style="text-align: right">
                                    <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Grand Total:"></asp:Label>

                                </div>

                                <div class="col-sm-6">
                                    <asp:TextBox ID="txt_grand_total" runat="server" ReadOnly="true" onkeypress="return isNumber(event)" CssClass="form-control text_box">0</asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-8 col-xs-12">
                        </div>
                        <div class="col-sm-4 col-xs-12">
                            <div class="row">
                                <div class="col-sm-6" style="text-align: right">
                                    <asp:Label ID="Label10" runat="server" Font-Bold="true" Text="Advance Payment:"></asp:Label>

                                </div>
                                <div class="col-sm-6">

                                    <asp:TextBox ID="txt_advacamt" runat="server" ReadOnly="true" onkeypress="return isNumber(event)" CssClass="form-control text_box">0</asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-8 col-xs-12">
                        </div>
                        <div class="col-sm-4 col-xs-12">
                            <div class="row">
                                <div class="col-sm-6" style="text-align: right">
                                    <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="Remaining Payment:"></asp:Label>

                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txt_remainamt" runat="server" ReadOnly="true" onkeypress="return isNumber(event)" CssClass="form-control text_box">0</asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
                <br />
            </div>
            <br />
            <div class="container-fluid">
                <div class="row text-center">

                    <asp:Button ID="btn_Save" runat="server" CssClass="btn btn-primary"
                        OnClick="btn_Save_Click" OnClientClick="return amount_save();" Text="Save" />
                    <asp:Button ID="btn_budget" runat="server" CssClass="btn btn-primary"
                        OnClick="btn_budget_Click" Text="Budget Approval" />

                    <asp:Button ID="btn_get_po" runat="server" CssClass="btn btn-primary"
                           onclick="btn_Print_Click"  Text="Get P.O" />
                  
                    <asp:Button ID="btn_update" runat="server" CssClass="btn btn-primary"
                        OnClick="btn_update_Click" Text="Update" CausesValidation="False" />

                    <asp:Button ID="btn_delete" runat="server" CausesValidation="False"
                        OnClick="btn_delete_Click" CssClass="btn btn-primary" Text="Delete" OnClientClick="return R_validation();" />

                     <asp:Button ID="btn_final" runat="server" CausesValidation="False"
                        OnClick="btn_final_Click" CssClass="btn btn-primary" Text="Final PO" Visible="false" OnClientClick="return R_validation1();" />

                    <asp:Button ID="btn_close" runat="server" CausesValidation="False"
                        CssClass="btn btn-danger" Text="Close" />
                    <asp:Label ID="lbl_print_quote" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <br />
                <br />
            </div>
    </div>
        </asp:Panel>
    </div>
</asp:Content>
