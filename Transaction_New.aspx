<%@ Page Title="New Tax Invoice" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Transaction_New.aspx.cs" Inherits="Transaction_New" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Salse Invoice New</title>
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
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>

    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        button, input, optgroup, select, textarea {
            color: inherit;
            margin: 0 0 0 0px;
        }

        * {
            box-sizing: border-box;
        }


        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding: 10px;
            height: 200px;
            width: 600px;
            z-index: 101;
        }

        .container-fluid {
            font-family: Verdana;
            font-size: 10px;
            font-weight: lighter;
        }

        .tab-section {
            background-color: #fff;
        }

        .grid-view {
            height: auto;
            max-height: 250px;
            overflow: scroll;
        }

        .table th {
            width: 50px;
            height: 30px;
        }

        .table td {
            padding: 10px;
            width: 200px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <script>
        var textarea = null;
        window.addEventListener("load", function () {
            textarea = window.document.querySelector("textarea");
            textarea.addEventListener("keypress", function () {
                if (textarea.scrollTop != 0) {
                    textarea.style.height = textarea.scrollHeight + "px";
                }
            }, false);
        }, false);
    </script>
    <script type="text/javascript">
        function pageLoad() {

            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: -180,
                onSelect: function (selected) {
                    $(".date-picker2").datepicker("option", "minDate", selected)
                }
            });


            $(".date-picker2").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
                onSelect: function (selected) {
                    $(".date-picker1").datepicker("option", "maxDate", selected)
                }
            });


            $('.date-picker3').datepicker({
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
            $(".date-picker3").attr("readonly", "true");
            location_hidden();
            cal();
        }


        $(document).ready(function () {


            var evt = null;
            isNumber(evt);

        });

        function location_hidden() {
            //alert("hello");
            var location = document.getElementById('<%=txt_particular.ClientID %>');
            var SelectedText = location.options[location.selectedIndex].text;

            var txt_description = document.getElementById('<%= txt_description.ClientID %>');
            var lbl_hsn = document.getElementById('<%= lbl_hsn.ClientID %>');
            var txt_hsn = document.getElementById('<%= txt_hsn.ClientID %>');


            if (SelectedText == "Select") {

                $('.length1').hide();
                $('.length2').hide();
                $(".js-example-basic-single").select2();
            }

            else {

                $('.length1').show();
                $('.length2').show();
                $(".js-example-basic-single").select2();

            }

        }

        function AllowAlphabet1(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || (keyEntry == '32') || (keyEntry == '9'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
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

        function AllowAlphabet_address(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) ||

                    (keyEntry == '32') || (keyEntry == '39') || (keyEntry == '38') || (keyEntry == '39') || (keyEntry == '44') || ((keyEntry >= '45') && (keyEntry <= '47')) ||

                    (keyEntry == '58') || (keyEntry == '59') || (keyEntry == '61') || (keyEntry == '34') || (keyEntry == '92'))

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
                if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 36 || charCode > 41)) {

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

        function isNumber1(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) || (charCode == 46)) {

                    return false;
                }

            }
            return true;
        }



        function Req_validation() {

            var custgstno = document.getElementById('<%=txt_customer_gst.ClientID %>');
            var ddl_sales_person = document.getElementById('<%=txt_customer_name.ClientID%>');

            var mobileno = document.getElementById('<%=txt_mobile_no.ClientID %>');

            var txt_grossamt = document.getElementById('<%=txt_grossamt.ClientID %>');
            var sub_total_a = document.getElementById('<%=txt_sub_total1.ClientID %>');
            var other_charge = document.getElementById('<%=txt_extra_chrgs.ClientID %>');
            var prise = document.getElementById('<%=txt_extra_chrgs_amt.ClientID %>');

            if (ddl_sales_person.value == "") {
                alert("Please Enter Customer Name!!!");
                ddl_sales_person.focus();
                return false;
            }

            if (custgstno.value == "") {
                alert("Please Enter Customer Gst No!!!");
                custgstno.focus();
                return false;
            }
            //if (duedate.value == "") {
            //    alert("Please Select Due Date!!!");
            //    duedate.focus();
            //    return false;
            //}
            //if (ddl_sales_person.value == "") {
            //    alert("Please Enter Sales Person !!!");
            //    ddl_sales_person.focus();
            //    return false;
            //}

            if (mobileno.value == "") {
                alert("Please Enter Mobile No!!!");
                mobileno.focus();
                return false;
            }
            //if (pono.value == "") {
            //    alert("Please Enter Challan No!!!");
            //    pono.focus();
            //    return false;
            //}
            //if (vechicalno.value == "") {
            //    alert("Please Enter Vehicle No!!!");
            //    vechicalno.focus();
            //    return false;
            //}
            // Gross Amount

            if (txt_grossamt.value == "0" || txt_grossamt.value == "") {
                alert("Please Click On the Add Button(Table) to calculate Gross Amount !!!");
                txt_grossamt.focus();
                return false;
            }

            // Sub total - A

            if (sub_total_a.value == "0" || gross_amout.value == "") {
                alert("Please calculate sub total !!!");
                sub_total_a.focus();
                return false;
            }

            //charges amount

            if (other_charge.value != "") {

                if (prise.value == "0" || prise.value == "") {
                    alert("please fill prise !!!");
                    prise.focus();
                    return false;
                }
            }

            //charges amount

            if (prise.value != "0" && prise.value != "") {

                if (other_charge.value == "0" || other_charge.value == "") {
                    alert("please fill charges !!!");
                    other_charge.focus();
                    return false;
                }
            }
            return true;
        }


        function btn_validation() {

            var e_particular = document.getElementById('<%=txt_particular.ClientID%>');
            var SelectedText = e_particular.options[e_particular.selectedIndex].text;
            var txt_description = document.getElementById('<%=txt_description.ClientID %>');
            var txt_desc = document.getElementById('<%=txt_desc.ClientID%>');
            var ddl_unit = document.getElementById('<%=txt_designation.ClientID%>');
            var select_unit_per = ddl_unit.options[ddl_unit.selectedIndex].text;
            var e_quantity = document.getElementById('<%=txt_quantity.ClientID%>');
            var txt_rate = document.getElementById('<%=txt_rate.ClientID%>');

            // Particular
            if (SelectedText == "Select") {
                alert("Please Select Particular !!!");
                e_particular.focus();
                return false;
            }

            if (txt_desc.value == "") {
                alert("Please Enter Description !!!");
                txt_desc.focus();
                return false;
            }

            if (txt_description.value == "") {
                alert("Please Enter GST% Rate !!!");
                txt_description.focus();
                return false;
            }

            // unit
            if (select_unit_per == "Select Unit") {
                alert("Please Select unit !!!");
                ddl_unit.focus();
                return false;
            }
            if (parseFloat(e_quantity.value) == "0" || e_quantity.value == "") {
                alert("Please Enter Quantity  !!!");
                e_quantity.focus();
                return false;
            }
            if (parseFloat(txt_rate.value) == "0" || txt_rate.value == "") {
                alert("Please Enter Rate  !!!");
                txt_rate.focus();
                return false;
            }
            return true;
        }

        function cal() {

            var sub_totalA = $(".sub_totalA").val();

            var exta_charg = $(".extra_charg").val();
            var exta_tax = $(".extra_tax").val();
            var charg_amont = $(".extra_amount").val();
            var sub_totalB = $(".sub_totalB").val();
            var Total = $(".final_total").val();

            var charg_amont2 = (exta_charg * exta_tax) / 100;
            $(".extra_amount").val(charg_amont2);

            var sub_totalB2 = parseInt($(".extra_charg").val()) + charg_amont2;
            $(".sub_totalB").val(sub_totalB2);

            var Total2 = parseInt($(".sub_totalA").val()) + sub_totalB2;

            $(".final_total").val(Total2);

        }

        function textAreaAdjust(o) {
            o.style.height = "1px";
            o.style.height = (25 + o.scrollHeight) + "px";
        }

        function openWindow() {
            window.open("html/Quotation.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

        $(document).ready(function () {

            $(".js-example-basic-single").select2();

        });

    </script>

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel9" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: 16px;" class="text-center text-uppercase"><b>NEW INVOICE</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>

            <div class="panel-body">

                <div class="row">
                    <div class="col-sm-2 col-xs-12 ">
                        Invoice Number :
          
                                    <asp:TextBox ID="txt_docno_number" runat="server" AutoPostBack="True" class="form-control text_box"
                                        OnTextChanged="txt_docno_TextChanged" MaxLength="10" onkeypress="return AllowAlphabet_Number(event,this);">I</asp:TextBox>
                    </div>
                    <div class="col-sm-3 col-xs-12 ">
                        Customer Name :
                                    <asp:TextBox ID="txt_customername" runat="server" AutoPostBack="True" CssClass="form-control text_box"
                                        OnTextChanged="txt_customername_TextChanged" onkeypress="return AllowAlphabet1(event,this);" MaxLength="30"></asp:TextBox>

                    </div>
                    <br />

                    <div class="col-sm-3 col-xs-12">

                        <asp:Button ID="btn_searchvendor" runat="server" CausesValidation="False" CssClass="btn btn-primary" OnClick="btn_searchvendor_Click" Text=" Search " />
                        <%--   <asp:Button ID="btn_new" runat="server" CausesValidation="False" onclick="btn_new_Click" Text="New" CssClass="btn btn-primary" />--%>
                        <asp:Button ID="btn_Closeup" runat="server" CausesValidation="False"
                            CssClass="btn btn-danger" OnClick="btn_Close_Click" Text="Close" />

                    </div>
                </div>

                <br />
                <asp:Panel ID="Panel5" runat="server" ScrollBars="Auto" CssClass="grid-view">
                    <asp:GridView ID="gv_bynumber_name" class="table" runat="server" AutoGenerateColumns="False"
                        ShowHeader="true" OnSelectedIndexChanged="gv_bynumber_name_SelectedIndexChanged"
                        CellPadding="4" ForeColor="#333333" GridLines="Both">

                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingRowStyle BackColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="White" ForeColor="#000066" />


                        <Columns>
                            <asp:TemplateField HeaderText="Estimate No">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_docnumber" runat="server" Text='<%# Eval("DOC_NO")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estimate Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_docdate" runat="server" Text='<%# Eval("DOC_DATE")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_customername" runat="server"
                                        Text='<%# Eval("CUST_NAME")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_amount" runat="server"
                                        Text='<%# Eval("FINAL_PRICE")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:CommandField ShowSelectButton="True" SelectText="Get Details" HeaderText="Get Details" />
                        </Columns>
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </asp:Panel>
                <br />
                <hr />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel6" runat="server">
                            <asp:Panel ID="Panel7" runat="server">
                                <div class="row">
                                    <div class="col-sm-2 col-xs-12">
                                        Invoice Number :
                                        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:TextBox ID="txt_docno" runat="server" CssClass="form-control text_box" MaxLength="10" onkeypress="return AllowAlphabet1(event,this);"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span>Customer Name:
                                              <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </span>
                                        <span>
                                            <asp:TextBox ID="txt_customer_name" runat="server" onkeypress="return AllowAlphabet1(event,this);" MaxLength="30" CssClass="form-control text_box">
                                            </asp:TextBox>
                                            <%--<asp:DropDownList ID="ddl_customerlist" runat="server" MaxLength="30"
                                                OnSelectedIndexChanged="customer_details" AutoPostBack="true" CssClass="form-control text_box">
                                            </asp:DropDownList>--%>

                                        </span>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        Address:
                                        <span>
                                            <asp:TextBox ID="txt_address" runat="server" TextMode="MultiLine" CssClass="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)">
                                            </asp:TextBox>
                                        </span>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <span>Customer Mobile Number:
                                                                <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </span>
                                        <span>
                                            <asp:TextBox ID="txt_mobile_no" runat="server" MaxLength="10" CssClass="form-control text_box" onKeyPress="return isNumber(event)">
                                            </asp:TextBox>
                                        </span>
                                    </div>

                                    <div class="col-sm-2 col-xs-12">
                                        <span>Customer GST Number:
                                                                <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </span>
                                        <span>
                                            <asp:TextBox ID="txt_customer_gst" runat="server" MaxLength="15" CssClass="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)">
                                            </asp:TextBox>
                                        </span>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        Invoice Date : 
                                        <asp:TextBox ID="txt_docdate" runat="server" Style="margin-bottom: 0px" class="form-control date-picker1 text_box"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12" style="display: none;">
                                        Due Date : 
                                         <%--  <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                        <asp:TextBox ID="txt_expiry_date" runat="server" Style="margin-bottom: 0px" class="form-control date-picker2 text_box"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12" style="display: none;">
                                        Category :

                                        <asp:DropDownList ID="ddlcategory" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Original">Original</asp:ListItem>
                                            <asp:ListItem Value="Duplicate">Duplicate</asp:ListItem>
                                        </asp:DropDownList>


                                    </div>

                                </div>

                            </asp:Panel>
                            <br />
                            <div class="row">
                                <div class="col-sm-2">
                                    <asp:Label ID="Label1" runat="server" Text="Sales Person :"></asp:Label>

                                    <asp:TextBox ID="ddl_sales_person" runat="server" MaxLength="60" class="form-control" onkeypress="return AllowAlphabet1(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    Mobile No :
                                    <asp:TextBox ID="txt_sales_mobile_no" runat="server" MaxLength="10" class="form-control" onKeyPress="return isNumber(event)"></asp:TextBox>

                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    Challan No :
                          <%--  <asp:Label ID="Label11" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>

                                    <asp:TextBox ID="txt_p_o_no" runat="server" MaxLength="10" class="form-control" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>

                                </div>

                                <div class="col-sm-2 col-xs-12">
                                    Mode of Transport :

                                    <asp:DropDownList ID="ddl_transport" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Road">Road</asp:ListItem>
                                        <asp:ListItem Value="Train">Train</asp:ListItem>
                                        <asp:ListItem Value="Airway">Airway</asp:ListItem>
                                        <asp:ListItem Value="Shipping">Shipping</asp:ListItem>

                                    </asp:DropDownList>
                                    <%-- <asp:TextBox ID="ddl_transport" runat="server" MaxLength="20" class="form-control" ></asp:TextBox>--%>
                                </div>

                                <div class="col-sm-2 col-xs-12" hidden="hidden">
                                    Freight :
                                    <asp:TextBox ID="txt_freight" runat="server" MaxLength="6" class="form-control" onkeypress="return isNumber(event)">0</asp:TextBox>

                                </div>

                                <div class="col-sm-2 col-xs-12">
                                    Vehicle No :
                           <%-- <asp:Label ID="Label13" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>

                                    <asp:TextBox ID="txt_vehicle" runat="server" MaxLength="15" class="form-control" onkeypress="return AllowAlphabet_Number(event,this);"></asp:TextBox>

                                </div>
                                <div class="col-sm-2 col-xs-12" style="display: none;">
                                    <span>Bill Month : <span style="color: #FF0000">*</span></span>


                                    <asp:DropDownList ID="ddlcalmonth" runat="server" Width="100%" class="form-control">
                                        <asp:ListItem>JAN</asp:ListItem>
                                        <asp:ListItem>FEB</asp:ListItem>
                                        <asp:ListItem>MAR</asp:ListItem>
                                        <asp:ListItem>APR</asp:ListItem>
                                        <asp:ListItem>MAY</asp:ListItem>
                                        <asp:ListItem>JUN</asp:ListItem>
                                        <asp:ListItem>JUL</asp:ListItem>
                                        <asp:ListItem>AUG</asp:ListItem>
                                        <asp:ListItem>SEP</asp:ListItem>
                                        <asp:ListItem>OCT</asp:ListItem>
                                        <asp:ListItem>NOV</asp:ListItem>
                                        <asp:ListItem>DEC</asp:ListItem>
                                    </asp:DropDownList>
                                </div>


                                <div class="col-sm-2 col-xs-12">
                                    <span>Narration :</span>
                                    <asp:TextBox ID="txt_narration" runat="server" MaxLength="200" class="form-control text_box" onkeypress="return AllowAlphabet1(event,this);"
                                        TextMode="MultiLine" onkeyup="textAreaAdjust(this)" Style="overflow: hidden"></asp:TextBox>
                                    </span>
                                </div>
                            </div>
                            <br />
                            <div class="row">

                                <div class="col-sm-2 col-xs-12" style="display: none;">
                                    Bill Year :
                <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:TextBox ID="txt_year" runat="server" MaxLength="4" class="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                    <asp:TextBox ID="txt_referenceno1" runat="server" MaxLength="20" CssClass="form-control" Visible="false"></asp:TextBox>

                                </div>





                                <%--  <div class="col-sm-2" style="visibility:hidden">
                                <asp:Label ID="lbl_referenceno1" runat="server" Text="Bill Type:"></asp:Label>
                                <asp:TextBox ID="txt_referenceno1" runat="server" MaxLength="20" CssClass="form-control text_box" onkeypress="return AllowAlphabet1(event,this);"></asp:TextBox>
                        </div>--%>
                            </div>


                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <hr />
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel8" runat="server">
                            <table id="maintable" class="table table-responsive" border="1">
                                <tr style="color: #000; background-color: #D1D0CE; text-align: center">
                                    <th align="center">Particular</th>
                                    <th align="center">Description</th>
                                    <th align="center">GST(%)</th>
                                    <th>
                                        <asp:Label ID="lbl_hsn" runat="server" Text="HSN Code"></asp:Label></th>
                                    <%--   <th align="center" class="length1" id="lbl_hsn1">HSN Code/SAC Code</th>--%>
                                    <%--  <th align="center" class="length2">SAC Code</th>--%>
                                    <th align="center">Unit</th>
                                    <th align="center">Per Unit</th>
                                    <th align="center">Quantity</th>
                                    <th align="center">Sales Rate</th>
                                    <th align="center">Discount Rate (%)</th>
                                    <th align="center">Discount Amount</th>
                                    <th align="center">Total Amount</th>
                                    <th align="center" hidden="hidden">VenDor</th>
                                    <th align="center">Table</th>
                                    <th align="center" hidden="hidden">Valid To</th>
                                    <th align="center" hidden="hidden">Vendor</th>
                                </tr>
                                <tr id="rows">
                                    <td>
                                        <asp:DropDownList ID="txt_particular" runat="server" class="js-example-basic-single" Width="120px"
                                            OnSelectedIndexChanged="particular_hsn_sac_code" AutoPostBack="true">
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_desc" runat="server" onkeypress="return AllowAlphabet1(event,this);" Width="120px" TextMode="MultiLine" CssClass="form-control text_box" onkeyup="textAreaAdjust(this)" Style="overflow: hidden"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_description" runat="server" onkeypress="return isNumber_dot(event)" MaxLength="10" CssClass="form-control text_box" Width="70px">0</asp:TextBox>

                                    </td>
                                    <td>

                                        <asp:TextBox ID="txt_hsn" runat="server" onkeypress="return AllowAlphabet1(event,this);" MaxLength="20" Width="80px" CssClass="form-control text_box"></asp:TextBox>

                                    </td>
                                    <%--   <td class="length2">
                                                    <asp:Label ID="lbl_sac" runat="server" Text="SAC Code :"  Visible="false"></asp:Label>
                                                     <asp:TextBox ID="txt_sac" runat="server" onkeypress="return AllowAlphabet1(event,this);"  MaxLength="10" CssClass="form-control text_box"></asp:TextBox>
                           
                                                </td>--%>
                                    <td>
                                        <asp:DropDownList ID="txt_designation" runat="server" CssClass="form-control" Width="80px" OnSelectedIndexChanged="unit_per_price_changes" AutoPostBack="true"></asp:DropDownList>

                                    </td>
                                    <%--  <td>
                                                   <asp:TextBox ID="txt_designation" runat="server" onkeypress="return AllowAlphabet1(event,this);"  MaxLength="10" CssClass="form-control text_box"></asp:TextBox>
                                                
                                                </td>--%>
                                    <td>
                                        <asp:TextBox ID="txt_per_unit" runat="server" class="form-control text_box" Width="80px" onkeypress="return isNumber(event)" OnTextChanged="txt_quantity_TextChanged" AutoPostBack="true" MaxLength="10">0</asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_quantity" runat="server" class="form-control text_box" onkeypress="return isNumber_dot(event)" OnTextChanged="txt_quantity_TextChanged" AutoPostBack="true" MaxLength="10">0</asp:TextBox>
                                        <asp:Label ID="lbl_qty" runat="server" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_rate" runat="server" Width="80px" AutoPostBack="true" class="form-control text_box" OnTextChanged="txt_rate_TextChanged" onkeypress="return isNumber_dot(event)" MaxLength="10">0</asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_discount_rate" runat="server" onkeypress="return isNumber_dot(event)" AutoPostBack="true" MaxLength="10" CssClass="form-control text_box" OnTextChanged="txt_discount_rate_TextChanged">0</asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_discount_price" runat="server" AutoPostBack="true" onkeypress="return isNumber_dot(event)" MaxLength="10" CssClass="form-control text_box" OnTextChanged="txt_discount_price_TextChanged">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_amount" runat="server" onkeypress="return isNumber_dot(event)" MaxLength="10" CssClass="form-control text_box" ReadOnly="true">0</asp:TextBox>

                                    </td>

                                    <td hidden="hidden">
                                        <asp:DropDownList ID="ddl_vendor" runat="server" onchange="location_hidden();"
                                            onkeypress="return AllowAlphabet1(event,this);" class="form-control text_box"
                                            AutoPostBack="true" Width="80px">
                                            <%--  <asp:ListItem Text="Vendor"></asp:ListItem>--%>
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkbtn_addmoreitem" runat="server" CausesValidation="False"
                                            OnClick="lnkbtn_addmoreitem_Click" OnClientClick="return btn_validation();"><img alt="Add Item"  
                                                        src="Images/add_icon.png"  /></asp:LinkButton>

                                    </td>
                                    <td hidden="hidden">
                                        <asp:TextBox ID="txt_start_date" runat="server" Width="100px" MaxLength="10" CssClass="form-control date-picker1 text_box"></asp:TextBox>

                                    </td>
                                    <td hidden="hidden">
                                        <asp:TextBox ID="txt_end_date" runat="server" Width="100px" MaxLength="10" CssClass="form-control date-picker2 text_box"></asp:TextBox>

                                    </td>
                                </tr>
                            </table>

                        </asp:Panel>

                        <asp:Panel ID="Panel2" runat="server">
                            <asp:Panel ID="Panel4" runat="server" CssClass="grid-view" ScrollBars="Auto">

                                <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    OnRowDataBound="gv_itemslist_RowDataBound"
                                    AutoGenerateColumns="False">

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
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Particular">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_particular" ReadOnly="True" runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("PARTICULAR")%>' Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Description">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_Description_final" ReadOnly="True" runat="server" Style="text-align: left" class="form-control grid_desc" onkeyup="textAreaAdjust(this)" TextMode="MultiLine" Text='<%# Eval("Description")%>' Width="100px"></asp:TextBox>
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
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Unit">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="550px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_designation" runat="server" ReadOnly="True" Style="text-align: left" class="form-control" Text='<%# Eval("Designation")%>' Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemStyle Width="70px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_quantity" runat="server" ReadOnly="True" onkeypress="return isNumber(event)" Style="text-align: left" class="form-control" Text='<%# Eval("QUANTITY")%>' AutoPostBack="True" Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Rate">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_rate" runat="server" ReadOnly="True" Style="text-align: left" onkeypress="return isNumber(event)" class="form-control" Text='<%# Eval("RATE")%>' AutoPostBack="True" Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Discount Rate">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_discount" runat="server" ReadOnly="True" Style="text-align: left" onkeypress="return isNumber(event)" class="form-control" Text='<%# Eval("DISCOUNT")%>' AutoPostBack="True" Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Discount Price">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_discount_amt" runat="server" ReadOnly="True" Style="text-align: left" onkeypress="return isNumber(event)" class="form-control" Text='<%# Eval("DISCOUNT_AMT")%>' AutoPostBack="True" Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amount">
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_amount" runat="server" Style="text-align: right" class="form-control" Text='<%# Eval("AMOUNT") %>' ReadOnly="True" Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Date" Visible="false">
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_start_date" runat="server" Style="text-align: right" class="form-control" Text='<%# Eval("START_DATE") %>' ReadOnly="True" Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Date" Visible="false">
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_end_date" runat="server" Style="text-align: right" class="form-control" Text='<%# Eval("END_DATE") %>' ReadOnly="True" Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vendor" Visible="false">
                                            <ItemStyle Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbl_vendor" runat="server" Style="text-align: right" class="form-control" Text='<%# Eval("VENDOR") %>' ReadOnly="True" Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkbtn_addmoreitem" />
                    </Triggers>
                </asp:UpdatePanel>
                <br />
                <hr />

                <div class="row">
                    <div class="col-sm-8 col-xs-12">
                        <div class="row">
                            <div class="col-sm-5 col-xs-12">
                                <span>Customer Notes :</span>
                                <asp:TextBox ID="txt_customer_notes" runat="server" MaxLength="200" Rows="5" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event,this);"
                                    TextMode="MultiLine" onkeyup="textAreaAdjust(this)" Style="overflow: hidden"></asp:TextBox>

                            </div>
                            <div class="col-sm-5 col-xs-12">
                                <span>Terms & Conditions :</span>
                                <asp:TextBox ID="txt_terms_conditions" runat="server" MaxLength="200" Rows="5" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event,this);"
                                    TextMode="MultiLine" onkeyup="textAreaAdjust(this)" Style="overflow: hidden"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 col-xs-12">
                        <div class="row">

                            <div class="col-sm-6" style="text-align: right">
                                <asp:Label ID="lbl_gross_amt" runat="server" Text="Gross Total"></asp:Label>
                            </div>
                            <div class="col-sm-6" style="text-align: right">
                                <asp:TextBox ID="txt_grossamt" runat="server" CssClass="form-control text_box" ReadOnly="true">0</asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6" style="text-align: right">
                                Discount (%)
                                        <asp:TextBox ID="txt_tot_discount_percent" onkeypress="return isNumber_dot(event)" runat="server" AutoPostBack="true" CssClass="form-control text_box" OnTextChanged="txt_tot_discount_percent_TextChanged" MaxLength="5">0</asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                Discount (Rs.)
                                        <asp:TextBox ID="txt_tot_discount_amt" onkeypress="return isNumber_dot(event)" runat="server" AutoPostBack="true" CssClass="form-control text_box" OnTextChanged="txt_tot_discount_amt_TextChanged">0</asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6" style="text-align: right">
                                <asp:Label ID="lbl_taxable_amt" runat="server" Text="Taxable Amount"></asp:Label>
                            </div>
                            <div class="col-sm-6" style="text-align: right">
                                <asp:TextBox ID="txt_taxable_amt" runat="server" ReadOnly="true" Font-Bold="True" class="form-control"
                                    ForeColor="#339966">0</asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <asp:Panel ID="Panel1" runat="server" CssClass="table table-responsive table_run" ScrollBars="Auto"
                                    Width="100%">
                                </asp:Panel>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6" style="text-align: right">
                                <asp:Label ID="lbl_sub_total1" runat="server" Text="Sub Total(A)"></asp:Label>
                            </div>
                            <div class="col-sm-6" style="text-align: right">
                                <asp:TextBox ID="txt_sub_total1" runat="server" ReadOnly="true" CssClass="form-control sub_totalA text_box">0</asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6" style="text-align: right">
                                <asp:TextBox ID="txt_extra_chrgs" runat="server" CssClass="form-control text_box" Placeholder="Other Charges" onkeypress="return AllowAlphabet1(event)"></asp:TextBox>
                            </div>
                            <div class="col-sm-6" style="text-align: left">
                                <asp:TextBox ID="txt_extra_chrgs_amt" runat="server" CssClass="form-control extra_charg text_box" onchange="cal();" onkeypress="return isNumber_dot(event)">0</asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6" style="text-align: right">
                                <asp:TextBox ID="txt_extra_chrgs_tax" runat="server" CssClass="form-control extra_tax text_box" onchange="cal();" Placeholder="GST(%) on Charges" onkeypress="return isNumber_dot(event)" MaxLength="4">0</asp:TextBox>
                            </div>
                            <div class="col-sm-6" style="text-align: left">
                                <asp:TextBox ID="txt_extra_chrgs_tax_amt" ReadOnly="true" runat="server" CssClass="form-control extra_amount text_box">0</asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6" style="text-align: right">
                                <asp:Label ID="lbl_sub_total2" runat="server" Text="Sub Total(B)"></asp:Label>
                            </div>
                            <div class="col-sm-6" style="text-align: right">
                                <asp:TextBox ID="txt_sub_total2" runat="server" ReadOnly="true" CssClass="form-control sub_totalB text_box">0</asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6" style="text-align: right">
                                <asp:Label ID="Label2" runat="server" Text="Total Amount After Tax(A+B)"></asp:Label>
                            </div>
                            <div class="col-sm-6" style="text-align: right">
                                <asp:TextBox ID="txt_final_total" Enabled="false" runat="server" CssClass="form-control final_total text_box">0</asp:TextBox>
                            </div>
                        </div>
                        <%-- Important for Database Query.. Don't Delete or change --%>
                        <div class="row">
                            <div class="col-sm-6">
                                <asp:TextBox ID="txt_net_total_1" runat="server" Font-Bold="True" class="form-control"
                                    ForeColor="#339966" ReadOnly="True" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                        <%-- END --%>
                    </div>
                </div>

                <br />



                <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btn_save_send"
                    CancelControlID="Button2" BackgroundCssClass="Background">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
                    <iframe style="width: 600px; height: 350px; background-color: #fff;" id="irm1" src="p_uploadfile.aspx" runat="server"></iframe>
                    <div class="row text-center" style="width: 100%;">
                        <asp:Button ID="Button2" CssClass="btn btn-danger" runat="server" Text="Close" />
                    </div>

                    <br />

                </asp:Panel>
                <div class="row text-center">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-7 ">

                        <asp:Button ID="btn_Save" runat="server" CssClass="btn btn-primary"
                            OnClick="btn_Save_Click" Text="Save as Draft" OnClientClick=" return Req_validation();" />

                        <asp:Button ID="btn_save_send" runat="server" CssClass="btn btn-primary"
                            Text="Send" />
                        <%--onclick="btn_Save_Click"--%>

                        <asp:Button ID="btn_update" runat="server" CssClass="btn btn-primary" OnClick="btn_update_Click"
                            Text="Update" CausesValidation="False" />

                        <asp:Button ID="btn_delete" runat="server" CausesValidation="False"
                            CssClass="btn btn-primary" OnClick="btn_delete_Click" Text="Delete" />

                        <asp:Button ID="btn_Print" runat="server" CssClass="btn btn-primary"
                            Text="Print" CausesValidation="False"
                            OnClick="btn_Print_Click" />

                        <%-- <asp:Button ID="btn_ShowST" runat="server" CausesValidation="False"
                                OnClick="btn_ShowST_Click" Text="GST Tax Details"
                                CssClass="btn btn-primary" />--%>
                        <%--    <cc1:ModalPopupExtender ID="btn_ShowST_ModalPopupExtender" runat="server"
                                BackgroundCssClass="Background" DropShadow="true"
                                Enabled="True" PopupControlID="divPopUp"
                                PopupDragHandleControlID="panelDragHandle" TargetControlID="btn_ShowST">
                            </cc1:ModalPopupExtender>--%>
                        <asp:Button ID="btn_Closelow" runat="server" CausesValidation="False"
                            CssClass="btn btn-danger" OnClick="btn_Close_Click" Text="Close" />
                        <asp:Label ID="lbl_print_quote" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                </div>

                <br />
            </div>

        </asp:Panel>

        <div class="Popup" id="divPopUp" runat="server" visible="false">
            <asp:Panel runat="Server" ID="panelDragHandle" CssClass="drag">
                <div class="text-center"><b>Hold here to Drag this Box</b></div>
                <br />
                <asp:Panel ID="Panel3" runat="server" Visible="true">
                    <div class="row">

                        <div class="col-sm-4 col-xs-12 ">
                            CGST %

                                                        <asp:TextBox ID="txt_ser_tax_per_pro" runat="server" Style="margin-bottom: 0px"
                                                            AutoPostBack="True" onkeypress="return isNumber_dot(event,this)" class="form-control"
                                                            OnTextChanged="txt_ser_tax_per_pro_TextChanged">9</asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-xs-12 ">
                            SGST %:
                                                   
                                                        <asp:TextBox ID="txt_bharat_education" runat="server" onkeypress="return isNumber_dot(event,this)" AutoPostBack="True" class="form-control"
                                                            OnTextChanged="txt_ser_tax_per_pro_TextChanged" Style="margin-bottom: 0px">9</asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-xs-12 ">
                            IGST & UTGST %:
                                                   
                                                        <asp:TextBox ID="txt_igst" runat="server" onkeypress="return isNumber_dot(event,this)" AutoPostBack="True" class="form-control"
                                                            OnTextChanged="txt_ser_tax_per_pro_TextChanged" Style="margin-bottom: 0px">18</asp:TextBox>
                        </div>
                        <br />


                    </div>
                    <br />
                    <div class="row text-center">

                        <asp:Button ID="btnClose" runat="server" Text="OK" class="btn btn-primary" CausesValidation="False" />

                    </div>
                </asp:Panel>


            </asp:Panel>
        </div>

        <table class="style1">

            <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                HeaderText="Please Fill Necessary Fields" ShowMessageBox="True"
                ShowSummary="False" />

        </table>
    </div>


</asp:Content>

