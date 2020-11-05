<%@ Page Title="Item Master" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Itemmaster.aspx.cs" Inherits="Itemmaster" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Item Master</title>
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
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/jszip.min.js"></script>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/2.1.2/sweetalert.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.css" />
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {
            ChangeShow();
            //hide_ddl();
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=ItemGridView.ClientID%>').DataTable({
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
               .appendTo('#<%=ItemGridView.ClientID%>_wrapper .col-sm-6:eq(0)');
            }

    </script>

    <style>
        label {
        }

        button, input, optgroup, select, textarea {
            color: inherit;
            margin: 0 0 0 0px;
        }

        * {
            box-sizing: border-box;
        }

        .container {
            font-family: Verdana;
            font-size: 10px;
            font-weight: lighter;
            width: 100%;
        }

        .grid-view {
            height: auto;
            max-height: 250px;
            overflow-y: auto;
            overflow-x: hidden;
        }
    </style>



</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">




    <script type="text/javascript">
        //vikas comment15/11
        //AllowAlphabet_address(event);
        //$(document).ready(function () {


        //    var evt = null;
        //    var e = null;
        //    isNumber(evt);

        //    AllowAlphabet(e);
        //    AllowAlphabet1(e);


        //});
        function callfnc() {

            document.getElementById('<%= Button5.ClientID %>').click();
        }


        function AllowAlphabet(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || (keyEntry < '31'))
                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }

        function AllowAlphabet1(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry < '31') || !(keyEntry <= '122'))

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

        function isNumber(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    if (charCode == 46) {
                        return true;
                    }
                    return false;
                }

            }
            return true;
        }



        function AllowAlphabet1(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry < '31') || (keyEntry == '32'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }

        function validate() {

            //vikas15/11
            var e_product = document.getElementById('<%=ddl_product.ClientID%>');
             var SelectedText = e_product.options[e_product.selectedIndex].text;
             if (SelectedText == "Select") {
                 alert("Please Select Item Type !!!");
                 e_product.focus();
                 return false;
             }
             // Item Name
             var f_item_name = document.getElementById('<%=txt_itemname.ClientID %>');
             if (f_item_name.value == "") {
                 alert("Please Enter Item Name !!!");
                 f_item_name.focus();
                 return false;
             }
             var txt_itembrand = document.getElementById('<%=txt_itembrand.ClientID %>');
             if (txt_itembrand.value == "") {
                 alert("Please Enter Item Brand !!!");
                 txt_itembrand.focus();
                 return false;
             }
             var txt_hsn = document.getElementById('<%=txt_hsn.ClientID %>');
             var Selected_txt_hsn = txt_hsn.options[txt_hsn.selectedIndex].text;
             if (Selected_txt_hsn == "Select") {
                 alert("Please Enter HSN Number !!!");
                 txt_hsn.focus();
                 return false;
             }
             var txt_vat = document.getElementById('<%=txt_vat.ClientID%>');

             if (txt_vat.value == "") {
                 alert("Please Select GST Rate !!!");
                 txt_vat.focus();
                 return false;
             }

             //vikas15/11
             var txt_purchaserate = document.getElementById('<%=txt_purchaserate.ClientID %>');
             if (txt_purchaserate.value == "0" || txt_purchaserate.value == "") {
                 alert("Please Enter Purches Rate !!!");
                 txt_purchaserate.focus();
                 return false;
             }
             var txt_sale_rate = document.getElementById('<%=txt_sale_rate.ClientID %>');
             if (txt_sale_rate.value == "0" || txt_sale_rate.value == "") {
                 alert("Please Enter Sale Rate !!!");
                 txt_sale_rate.focus();
                 return false;
             }
             var ddl_unit = document.getElementById('<%=ddl_unit.ClientID%>');
             var Selected_ddl_unit = ddl_unit.options[ddl_unit.selectedIndex].text;
             if (Selected_ddl_unit == "Select Unit") {
                 alert("Please Select Unit !!!");
                 ddl_unit.focus();
                 return false;
             }

             $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });


             return true;

         }


         function ChangeShow() {
             var e_product = document.getElementById('<%=ddl_product.ClientID%>');
             var SelectedText = e_product.options[e_product.selectedIndex].text;

             if (SelectedText == "Uniform") {

                 document.getElementById('<%=ddl_uniform_size.ClientID%>').style.display = "block";
                 document.getElementById('<%=lb_uniform_size.ClientID%>').style.display = "block";
                 document.getElementById('<%=lb_pantry_jacket_size.ClientID%>').style.display = "none";
                 document.getElementById('<%=lb_shoes_size.ClientID%>').style.display = "none";
                 document.getElementById('<%=lbl_apron_size.ClientID%>').style.display = "none";

             }
             else if (SelectedText == "Pantry Jacket") {
                 document.getElementById('<%=ddl_uniform_size.ClientID%>').style.display = "block";
                 document.getElementById('<%=lb_pantry_jacket_size.ClientID%>').style.display = "block";
                 document.getElementById('<%=lb_uniform_size.ClientID%>').style.display = "none";
                 document.getElementById('<%=lb_shoes_size.ClientID%>').style.display = "none";
                 document.getElementById('<%=lbl_apron_size.ClientID%>').style.display = "none";
             }
             else if (SelectedText == "Shoes") {
                 document.getElementById('<%=ddl_uniform_size.ClientID%>').style.display = "block";
                 document.getElementById('<%=lb_shoes_size.ClientID%>').style.display = "block";
                 document.getElementById('<%=lb_uniform_size.ClientID%>').style.display = "none";
                 document.getElementById('<%=lb_pantry_jacket_size.ClientID%>').style.display = "none";
                 document.getElementById('<%=lbl_apron_size.ClientID%>').style.display = "none";
             }
             else if (SelectedText == "Apron") {

                 document.getElementById('<%=ddl_uniform_size.ClientID%>').style.display = "block";
                 document.getElementById('<%=lb_shoes_size.ClientID%>').style.display = "none";
                 document.getElementById('<%=lb_uniform_size.ClientID%>').style.display = "none";
                 document.getElementById('<%=lb_pantry_jacket_size.ClientID%>').style.display = "none";
                 document.getElementById('<%=lbl_apron_size.ClientID%>').style.display = "block";
             }
             else {
                 document.getElementById('<%=ddl_uniform_size.ClientID%>').style.display = "none";
                 document.getElementById('<%=lb_uniform_size.ClientID%>').style.display = "none";
                 document.getElementById('<%=lb_pantry_jacket_size.ClientID%>').style.display = "none";
                 document.getElementById('<%=lb_shoes_size.ClientID%>').style.display = "none";
                 document.getElementById('<%=lbl_apron_size.ClientID%>').style.display = "none";
             }
    return false;
}
$(function () {
    $('#<%=ddl_product.ClientID%>').change(function () {
        $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
    });
    $('#<%=txt_hsn.ClientID%>').change(function () {
        $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
    });
    $('#<%=btn_cancel.ClientID%>').click(function () {
        $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
    });
    $('#<%=ItemGridView.ClientID%> td').click(function () {
        $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
    });
});
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
function openWindow() {
    window.open("html/Itemmaster3.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
}

    </script>
    <div class="container">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="Panel1" runat="server" class="panel panel-primary" Style="background-color: white">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-lg-11 col-md-11 col-sm-11 col-xs-12">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Item Master</b> </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-12 text-right">
                        <asp:LinkButton ID="panImgLnkBtn" runat="server" OnClientClick="openWindow();return false;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
           <%--  <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Item Master Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>
            <div class="panel-body">
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
                <%-- <ContentTemplate>--%>
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-left:5px; margin-right:5px; margin-bottom:20px; margin-top:20px">
                    <br />
                    <div class="row">
                        <div class="col-md-2  col-xs-12">
                           <b> Item Code :</b>
                                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>

                            <asp:TextBox ID="txt_itemcode" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                          <b>  Item Type :</b>
                                  <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <asp:DropDownList ID="ddl_product" runat="server" class="form-control" Width="170px" MaxLength="30" OnSelectedIndexChanged="ddl_product_SelectedIndexChanged" AutoPostBack="true">
                                <%--  OnSelectedIndexChanged="hide_Click" AutoPostBack="true"--%>
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="chemicals">Chemicals</asp:ListItem>
                                <asp:ListItem Value="housekeeping_material">Housekeeping Materials</asp:ListItem>
                                <asp:ListItem Value="Uniform">Uniform</asp:ListItem>
                                <asp:ListItem Value="pantry_jacket">Pantry Jacket</asp:ListItem>
                                <asp:ListItem Value="Apron">Apron</asp:ListItem>
                                <asp:ListItem Value="Shoes">Shoes</asp:ListItem>
                                <asp:ListItem Value="ID_Card">ID Card</asp:ListItem>
                                <asp:ListItem Value="Machine">Machine</asp:ListItem>
                                <asp:ListItem Value="Other">Other</asp:ListItem>

                            </asp:DropDownList>
                        </div>
                        <div class=" col-sm-2 col-xs-12">
                          <b>  Item Name :</b> 
                                 <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="txt_itemname" runat="server" class="form-control" CausesValidation="True" onkeypress=" return AllowAlphabet1(event)" MaxLength="70"></asp:TextBox>
                        </div>
                        <div class=" col-sm-2 col-xs-12">
                           <b> Item Brand : </b>
                                 <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="txt_itembrand" runat="server" class="form-control" CausesValidation="True" onkeypress=" return AllowAlphabet1(event)" MaxLength="70"></asp:TextBox>
                        </div>
                        <div class=" col-sm-2 col-xs-12">
                          <b>  Item Specification :</b> 
                                      <asp:TextBox ID="txt_item_description" runat="server" class="form-control" CausesValidation="True" MaxLength="140" onkeypress=" return AllowAlphabet1(event)"></asp:TextBox>
                        </div>

                        <div class=" col-sm-2 col-xs-12 ddl_hide_show" runat="server">

                            <asp:Label ID="lb_uniform_size" runat="server" Text="Unifrom Size :"></asp:Label><asp:Label ID="lb_pantry_jacket_size" runat="server" Text="Pantry Jacket Size :"></asp:Label>
                            <asp:Label ID="lb_shoes_size" runat="server" Text="Shoes Size :"></asp:Label><asp:Label ID="lbl_apron_size" runat="server" Text="Apron Size :"></asp:Label>
                            <asp:DropDownList ID="ddl_uniform_size" runat="server" class="form-control" CssClass="form-control"></asp:DropDownList>


                        </div>


                        <div class=" col-sm-2 col-xs-12 slat2">
                            <asp:Label ID="lbl_sac" runat="server" Text="SAC Code :" Visible="false"></asp:Label>

                            <asp:TextBox ID="txt_sac" runat="server" Visible="false" class="form-control" CausesValidation="True" MaxLength="25" onkeypress=" return AllowAlphabet1(event)" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class=" col-sm-2 col-xs-12 slat1">

                            <asp:Label ID="lbl_hsn" runat="server" Font-Bold="true" Text="HSN Code :"></asp:Label>
                            <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <%-- <asp:TextBox ID="" runat="server" class="form-control" CausesValidation="True" MaxLength="25" onkeypress=" return AllowAlphabet1(event)"></asp:TextBox>--%>
                            <asp:DropDownList ID="txt_hsn" runat="server" class="form-control" OnSelectedIndexChanged="txt_hsn_OnSelectedIndexChanged" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> GST Rate :</b>
                                 <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <asp:DropDownList ID="txt_vat" runat="server" class="form-control" CssClass="form-control">
                            <%--<asp:TextBox ID="txt_vat" runat="server" class="form-control" CausesValidation="True" onkeypress=" return AllowAlphabet1(event)"></asp:TextBox>--%>
                           

                            </asp:DropDownList>
                        </div>

                        <div class="col-sm-2 col-xs-12">
                           <b> Purchase Rate :</b>
                                 <asp:Label ID="Label9" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="txt_purchaserate" runat="server" class="form-control" onkeypress="return isNumber(event)" CssClass="form-control" MaxLength="9">0</asp:TextBox>
                        </div>

                        <div class=" col-sm-2 col-xs-12">
                           <b> Sale Rate :</b>
                                 <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <asp:TextBox ID="txt_sale_rate" runat="server" Width="100%" onkeypress="return isNumber(event)" CssClass="form-control" MaxLength="7" />

                        </div>

                        <%-- Unit Of Measurement :
                                 <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                <asp:ListBox ID="ddl_unit" runat="server" class="form-control" Width="100%" SelectionMode="Multiple" CssClass="form-control" > 
                                    
                                </asp:ListBox> --%>
                        <%--OnSelectedIndexChanged="ddl_unit_click" AutoPostBack="true" --%>

                        <div class=" col-sm-2 col-xs-12">
                           <b> Cost/Unit :</b>
                                <asp:TextBox ID="txt_one_resource_wt" runat="server" Width="100%" onkeypress="return isNumber(event)" CssClass="form-control" MaxLength="7">0</asp:TextBox>

                        </div>
                        <div class=" col-sm-2 col-xs-12">
                           <b> Validity(In Days):</b>
                                <asp:TextBox ID="txt_validity" runat="server" Width="100%" onkeypress="return isNumber(event)" CssClass="form-control" MaxLength="7">0</asp:TextBox>

                        </div>

                        <br />


                    </div>
                    <br />
                    <div class="row">
                        <div class=" col-sm-2 col-xs-12">
                           <b> Unit:</b>
                                   <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <asp:DropDownList ID="ddl_unit" runat="server" class="form-control" MaxLength="30">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                            <table class="table table-striped">
                                <tr>
                                    <th>
                                        <span class="glyphicon glyphicon-plus" style="font-size: 13px; font-weight: bolder; height: 20px; padding-left: 5px; padding-right: 5px; color: #337ab7; border-radius: 20%;"></span>
                                        <asp:LinkButton ID="lnkaddtravelplan" runat="server" OnClick="lnkaddtravelplan_Click"
                                            Text="ADD NEW UNIT" Style="color: #000;"></asp:LinkButton>
                                    </th>
                                </tr>
                            </table>
                        </div>
                        <asp:Button ID="Button1" runat="server" CssClass="hidden" Text="Location" />
                        <asp:Button ID="Button5" runat="server" CssClass="hidden" OnClick="Button5_Click" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel4" TargetControlID="Button3"
                            CancelControlID="Button4" BackgroundCssClass="Background">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="Panel4" runat="server" CssClass="Popup" Style="display: none">
                            <iframe style="width: 1000px; height: 450px; background-color: #fff;" id="Iframe1" src="p_add_new_unit.aspx" runat="server"></iframe>
                            <div class="row text-center">
                                <asp:Button ID="Button4" CssClass="btn btn-danger" runat="server" Text="Close" />
                            </div>
                            <asp:Button ID="Button3" runat="server" CssClass="hidden" Text="Add New Travelling Plan" />

                            <br />

                        </asp:Panel>
                        <div class="col-sm-2 col-xs-12">
                            <table class="table table-striped">
                                <tr>
                                    <th>
                                        <span class="glyphicon glyphicon-plus" style="font-size: 13px; font-weight: bolder; height: 20px; padding-left: 5px; padding-right: 5px; color: #337ab7; border-radius: 20%;"></span>
                                        <asp:LinkButton ID="lnk_gst_rate" runat="server" OnClick="lnk_gst_rate_Click"
                                            Text="ADD GST RATE" Style="color: #000;"></asp:LinkButton>
                                    </th>
                                </tr>
                            </table>
                        </div>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel3" TargetControlID="lnk_gst_rate"
                            CancelControlID="Button6" BackgroundCssClass="Background">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="Panel3" runat="server" CssClass="Popup" Style="display: none">
                            <iframe style="width: 1000px; height: 450px; background-color: #fff;" id="Iframe2" src="p_add_gst_rate.aspx" runat="server"></iframe>
                            <div class="row text-center">
                                <asp:Button ID="Button6" CssClass="btn btn-danger" runat="server" Text="Close" />
                            </div>
                        </asp:Panel>

                    </div>
                    <br />




                    <div class="row text-center">
                        <%--  <asp:Button ID="btn_new" runat="server" CssClass="btn btn-primary" Text=" New "
                                OnClick="btn_new_Click" />--%>
                        <asp:Button ID="btn_add" runat="server" class="btn btn-primary" OnClick="btn_add_Click"
                            Text=" Save " OnClientClick="return validate();" />
                        <asp:Button ID="btn_edit" runat="server" class="btn btn-primary"
                            Text=" Update " OnClick="btn_edit_Click" OnClientClick="return validate();" />
                        <asp:Button ID="btn_delete" runat="server" class="btn btn-primary" Text=" Delete "
                            OnClick="btn_delete_Click" OnClientClick="return R_validation();" />
                        <%--<cc1:ConfirmButtonExtender ID="btn_delete_ConfirmButtonExtender"
                                runat="server" ConfirmText="Are you sure you want to delete record?"
                                Enabled="True" TargetControlID="btn_delete"></cc1:ConfirmButtonExtender>--%>

                        <asp:Button ID="btnclose" runat="server" class="btn btn-danger"
                            OnClick="btnclose_Click" Text="Close" CausesValidation="False" />
                        <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" Text=" Clear "
                            OnClick="btn_cancel_Click" Visible="false" />
                        <asp:Button ID="btnexporttoexcel" runat="server" class="btn btn-primary"
                            Text="Export To Excel" OnClick="btnexporttoexcel_Click" Visible="false" />
                    </div>
                    <br />
                </div>

                <br />
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-left:5px; margin-right:5px; margin-bottom:20px; margin-top:20px">
                <asp:Panel ID="Panel2" runat="server" CssClass="grid-view">
                    <asp:GridView ID="ItemGridView" class="table" runat="server" Font-Size="X-Small"
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        OnSelectedIndexChanged="ItemGridView_SelectedIndexChanged" OnPreRender="ItemGridView_PreRender"
                        OnRowDataBound="ItemGridView_RowDataBound">
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingRowStyle BackColor="White" />
                        <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />

                        <Columns>
                            <asp:CommandField SelectText="&lt;" ShowSelectButton="True" />

                            <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM CODE"
                                SortExpression="ITEM_CODE" />
                            <asp:BoundField DataField="product_service" HeaderText="PRODUCT"
                                SortExpression="product_service" />
                            <asp:BoundField DataField="ITEM_NAME" HeaderText="ITEM NAME"
                                SortExpression="ITEM_NAME" />
                            <asp:BoundField DataField="item_description" HeaderText="ITEM_DESCRIPTION"
                                SortExpression="item_description" />

                            <asp:BoundField DataField="sac_number" HeaderText="SAC CODE"
                                SortExpression="sac_number" Visible="false" />

                            <asp:BoundField DataField="unit" HeaderText="UNIT OF MEASUREMENT"
                                SortExpression="unit" />
                            <asp:BoundField DataField="unit_per_piece" HeaderText=" UNIT FOR PER PIECE"
                                SortExpression="unit_per_piece" />

                            <asp:BoundField DataField="VAT" HeaderText="GST RATE"
                                SortExpression="VAT" />
                            <asp:BoundField DataField="item_brand" HeaderText="ITEM BRAND"
                                SortExpression="item_brand" />
                            <asp:BoundField DataField="hsn_number" HeaderText="HSN CODE"
                                SortExpression="hsn_number" />
                            <asp:BoundField DataField="PURCHASE_RATE" HeaderText="PURCHASE RATE"
                                SortExpression="PURCHASE_RATE" />
                            <asp:BoundField DataField="SALES_RATE" HeaderText="SALES RATE"
                                SortExpression="`SALES_RATE`" />
                            <asp:BoundField DataField="validity" HeaderText="VALIDITY"
                                SortExpression="validity" />
                            <asp:BoundField DataField="size" HeaderText="SIZE"
                                SortExpression="size" />

                        </Columns>
                    </asp:GridView>

                </asp:Panel>
                <br />
                <%-- </ContentTemplate>
                                   </asp:UpdatePanel>--%>
            </div>
        </asp:Panel>

    </div>




</asp:Content>



