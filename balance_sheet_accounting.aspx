<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="balance_sheet_accounting.aspx.cs" Inherits="balance_sheet_accounting" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Balance Sheet Accounting</title>
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


        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });



            $('.date-picker1').datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "1950",
                maxDate: 0,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
            });
            $(".date-picker1").attr("readonly", "true");


            $(document).ready(function () {
                $(document).on("Keyup", function () {
                    SearchGrid('<%=txt_search.ClientID%>', '<%=gv_receipt_details.ClientID%>');
                });


                 $('[id*=chk_gv_header]').click(function () {
                     $("[id*='chk_client']").attr('checked', this.checked);
                 });


             });




            $.fn.dataTable.ext.errMode = 'none';

           


            var table = $('#<%=gv_ledger.ClientID%>').DataTable({
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

            $.fn.dataTable.ext.errMode = 'none';

            var table = $('#<%=gv_minibank_receipt.ClientID%>').DataTable({
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
                 .appendTo('#<%=gv_minibank_receipt.ClientID%>_wrapper .col-sm-6:eq(0)');



            table.buttons().container()
               .appendTo('#<%=gv_ledger.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';

            var table = $('#<%=gv_upload.ClientID%>').DataTable({
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
               .appendTo('#<%=gv_upload.ClientID%>_wrapper .col-sm-6:eq(0)');


        });

        function Search_Gridview(strKey) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gv_receipt_details.ClientID %>");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }

        function Reject_function()
        {

            var ddl_receipt_details = document.getElementById('<%=ddl_receipt_details.ClientID %>');
            var select_ddl_receipt_details = ddl_receipt_details.options[ddl_receipt_details.selectedIndex].text;

            if (select_ddl_receipt_details == "Select") {
                alert("Please Select Account Type");
                ddl_receipt_details.focus();
                return false;
            }

            //if (ddl_receipt_details == "1") {




            var ddl_receipt_details = document.getElementById('<%=ddl_receipt_details.ClientID %>');
            var select_ddl_receipt_details = ddl_receipt_details.options[ddl_receipt_details.selectedIndex].text;

            if (select_ddl_receipt_details == "Receipt") {

                var ddl_received_type = document.getElementById('<%=ddl_received_type.ClientID %>');
                var select_ddl_received_type = ddl_received_type.options[ddl_received_type.selectedIndex].text;

                if (!select_ddl_received_type.disabled) {
                    if (select_ddl_received_type == "Select") {
                        alert("Please Select Received From");
                        ddl_received_type.focus();
                        return false;
                        //   }
                    }

                }

                var ddl_bill_client_receipt = document.getElementById('<%=ddl_bill_client_receipt.ClientID %>');
                var select_ddl_bill_client_receipt = ddl_bill_client_receipt.options[ddl_bill_client_receipt.selectedIndex].text;


                if (select_ddl_bill_client_receipt == "Select") {
                    alert("Please Select Client");
                    ddl_bill_client_receipt.focus();
                    return false;

                }
            }
            else {

                var ddl_bill_client_receipt = document.getElementById('<%=ddl_bill_client_receipt.ClientID %>');
                var select_ddl_bill_client_receipt = ddl_bill_client_receipt.options[ddl_bill_client_receipt.selectedIndex].text;

           
            if (select_ddl_bill_client_receipt == "Select") {
                alert("Please Select Client");
                ddl_bill_client_receipt.focus();
                return false;
            
        }
              
            var ddl_receving_date = document.getElementById('<%=ddl_receving_date.ClientID %>');
            var select_ddl_receving_date = ddl_receving_date.options[ddl_receving_date.selectedIndex].text;

                if (!select_ddl_receving_date.disabled) {
                    if (select_ddl_receving_date == "Select") {
                        alert("Please Select Payment Receving Date ");
                        ddl_receving_date.focus();
                        return false;
                    }
                }

                var ddl_client_resive_amt = document.getElementById('<%=ddl_client_resive_amt.ClientID %>');
                var select_ddl_client_resive_amt = ddl_client_resive_amt.options[ddl_client_resive_amt.selectedIndex].text;

                if (!select_ddl_client_resive_amt.disabled) {
                    if (select_ddl_client_resive_amt == "Select") {
                        alert("Please Select  Amount");
                        ddl_client_resive_amt.focus();
                        return false;
                    }
                }

             

            }

      
           

            var ddl_receipt_details = document.getElementById('<%=ddl_receipt_details.ClientID %>');
            var select_ddl_receipt_details = ddl_receipt_details.options[ddl_receipt_details.selectedIndex].text;

            if (select_ddl_receipt_details == "Receipt") {

                var isValid_re = false; {

                    var gridView3 = document.getElementById('<%= gv_minibank_receipt.ClientID %>');
                     for (var i = 1; i < gridView3.rows.length; i++) {
                         var inputs = gridView3.rows[i].getElementsByTagName('input');
                         if (inputs != null) {
                             if (inputs[0].type == "checkbox") {
                                 if (inputs[0].checked) {
                                     isValid_re = true;
                                     return true;
                                 }
                             }
                         }
                     }
                     alert("Please select atleast one Record ");
                     return false;

                 }

            }
            else{


            var isValid_re = false; {
                var gridView3 = document.getElementById('<%= gv_receipt_details.ClientID %>');
                 for (var i = 1; i < gridView3.rows.length; i++) {
                     var inputs = gridView3.rows[i].getElementsByTagName('input');
                     if (inputs != null) {
                         if (inputs[0].type == "checkbox") {
                             if (inputs[0].checked) {
                                 isValid_re = true;
                                 return true;
                             }
                         }
                     }
                 }
                 alert("Please select atleast one Record ");
                 return false;

             }


                }




            var r = confirm("Are you Sure You Want to Reject");
            if (r == true) {
                ($.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } }))
                return true;
            }
            else {
                return false;
            }


        }


        function approve_function()
        {
            var ddl_receipt_details = document.getElementById('<%=ddl_receipt_details.ClientID %>');
            var select_ddl_receipt_details = ddl_receipt_details.options[ddl_receipt_details.selectedIndex].text;

            if (select_ddl_receipt_details == "Select") {
                alert("Please Select Account Type");
                ddl_receipt_details.focus();
                return false;
            }

            //if (ddl_receipt_details == "1") {


           

            var ddl_receipt_details = document.getElementById('<%=ddl_receipt_details.ClientID %>');
            var select_ddl_receipt_details = ddl_receipt_details.options[ddl_receipt_details.selectedIndex].text;

            if (select_ddl_receipt_details == "Receipt") {

                var ddl_received_type = document.getElementById('<%=ddl_received_type.ClientID %>');
                var select_ddl_received_type = ddl_received_type.options[ddl_received_type.selectedIndex].text;

                if (!select_ddl_received_type.disabled) {
                    if (select_ddl_received_type == "Select") {
                        alert("Please Select Received From");
                        ddl_received_type.focus();
                        return false;
                        //   }
                    }

                }

                var ddl_bill_client_receipt = document.getElementById('<%=ddl_bill_client_receipt.ClientID %>');
                var select_ddl_bill_client_receipt = ddl_bill_client_receipt.options[ddl_bill_client_receipt.selectedIndex].text;


                if (select_ddl_bill_client_receipt == "Select") {
                    alert("Please Select Client");
                    ddl_bill_client_receipt.focus();
                    return false;

                }
            }
            else {

                var ddl_bill_client_receipt = document.getElementById('<%=ddl_bill_client_receipt.ClientID %>');
                var select_ddl_bill_client_receipt = ddl_bill_client_receipt.options[ddl_bill_client_receipt.selectedIndex].text;


                if (select_ddl_bill_client_receipt == "Select") {
                    alert("Please Select Client");
                    ddl_bill_client_receipt.focus();
                    return false;

                }

                var ddl_receving_date = document.getElementById('<%=ddl_receving_date.ClientID %>');
                var select_ddl_receving_date = ddl_receving_date.options[ddl_receving_date.selectedIndex].text;

                if (!select_ddl_receving_date.disabled) {
                    if (select_ddl_receving_date == "Select") {
                        alert("Please Select Payment Receving Date ");
                        ddl_receving_date.focus();
                        return false;
                    }
                }

                var ddl_client_resive_amt = document.getElementById('<%=ddl_client_resive_amt.ClientID %>');
                var select_ddl_client_resive_amt = ddl_client_resive_amt.options[ddl_client_resive_amt.selectedIndex].text;

                if (!select_ddl_client_resive_amt.disabled) {
                    if (select_ddl_client_resive_amt == "Select") {
                        alert("Please Select  Amount");
                        ddl_client_resive_amt.focus();
                        return false;
                    }
                }

        
            }


            var ddl_receipt_details = document.getElementById('<%=ddl_receipt_details.ClientID %>');
            var select_ddl_receipt_details = ddl_receipt_details.options[ddl_receipt_details.selectedIndex].text;

            if (select_ddl_receipt_details == "Receipt") {

                var isValid_re = false; {

                    var gridView3 = document.getElementById('<%= gv_minibank_receipt.ClientID %>');
                    for (var i = 1; i < gridView3.rows.length; i++) {
                        var inputs = gridView3.rows[i].getElementsByTagName('input');
                        if (inputs != null) {
                            if (inputs[0].type == "checkbox") {
                                if (inputs[0].checked) {
                                    isValid_re = true;
                                    return true;
                                }
                            }
                        }
                    }
                    alert("Please select atleast one Record ");
                    return false;

                }

            }
            else {


                var isValid_re = false; {
                    var gridView3 = document.getElementById('<%= gv_receipt_details.ClientID %>');
                for (var i = 1; i < gridView3.rows.length; i++) {
                    var inputs = gridView3.rows[i].getElementsByTagName('input');
                    if (inputs != null) {
                        if (inputs[0].type == "checkbox") {
                            if (inputs[0].checked) {
                                isValid_re = true;
                                return true;
                            }
                        }
                    }
                }
                alert("Please select atleast one Record ");
                return false;

            }


        }




           // var r = confirm("Are you Sure You Want to Approve");
            //if (r == true) {
               // ($.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } }))
            //    return true;
          //  }
          //  else {
         //      return false;
            //   }

          

        }



        function Group_validation() {

            var txt_groupn = document.getElementById('<%=txt_group.ClientID %>');

            if (txt_groupn.value == "") {
                alert("Please Enter Group Name ");
                //  txt_group.focus();
                return false;
            }
            return true;
        }

        function valid_upload() {

            var date = document.getElementById('<%=txt_date.ClientID %>');
             var document1_file = document.getElementById('<%=file_upload.ClientID %>');
             if (date.value == "") {
                 alert("Please Select Date");
                 date.focus();
                 return false;
             }
             var ddl_bank = document.getElementById('<%=ddl_bank.ClientID %>');
            var Selected_ddl_bank = ddl_bank.options[ddl_bank.selectedIndex].text;

            if (Selected_ddl_bank == "Select") {
                 alert("Please Select Bank");
                 ddl_bank.focus;
                 return false;
             }

             if (document1_file.value == "") {
                 alert("Please Upload File");
                 // file_upload.focus();
                 return false;
             }

             return true;
         }

         function SubGroup_validation() {

             var txt_group_name = document.getElementById('<%=ddl_group_name.ClientID %>');
            var Selected_group = txt_group_name.options[txt_group_name.selectedIndex].text;

            var txt_subgp = document.getElementById('<%=txt_sub_group.ClientID %>');

            if (Selected_group == "Select") {
                alert("Please Select Group Name ");
                //ddl_group_name.focus();
                return false;
            }
            if (Selected_group == "") {
                alert("Please Create Group First ");
                //ddl_group_name.focus();
                return false;
            }

            if (txt_subgp.value == "") {
                alert("Please Enter Sub Group Name ");
                // txt_sub_group.focus();
                return false;
            }
            return true;
        }
        function Ledger_validation() {

            var txt_group_name = document.getElementById('<%=ddl_group.ClientID %>');
            var Selected_group = ddl_group.options[ddl_group.selectedIndex].text;

            var txt_subgroup_name = document.getElementById('<%=ddl_sub_group.ClientID %>');
             var Selected_subgroup = ddl_sub_group.options[ddl_sub_group.selectedIndex].text;

             var txt_ledgern = document.getElementById('<%=txt_ledger.ClientID %>');

            if (Selected_group == "Select") {
                alert("Please Select Group Name ");
                //ddl_group_name.focus();
                return false;
            }
            if (Selected_group == "") {
                alert("Please Create Group First ");
                // ddl_group_name.focus();
                return false;
            }


            if (Selected_subgroup == "Select") {
                alert("Please Select Group Name ");
                // ddl_sub_group.focus();
                return false;
            }
            if (Selected_subgroup == "") {
                alert("Please Create Sub Group First ");
                //ddl_sub_group.focus();
                return false;
            }

            if (txt_ledgern.value == "") {
                alert("Please Enter Sub Group Name ");
                // txt_ledger.focus();
                return false;
            }
            return true;
        }
    </script>
    <style>
        .grid-view {
            max-height: 600px;
            height: auto;
        }

        .grid-view8 {
            max-height: 300px;
            height: auto;
            overflow-x: auto;
            overflow-y: auto;
        }

        .dt-buttons.btn-group {
            display: none;
        }

        .DTFC_LeftBodyWrapper {
            margin-top: -12px;
            overflow: hidden;
        }

        #ctl00_cph_righrbody_gv_attendance_filter {
            display: none;
        }
    </style>

    <style>
        .row {
            margin: 0px;
        }

        .nt_style {
            color: red;
            font: bold;
            text-align: center;
        }

        .text-red {
            color: red;
        }

        .wid {
            width: 120px;
        }

        .DTFC_LeftBodyLiner {
            overflow-x: hidden;
            top: -13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-primary" Style="background-color: white;">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: small;"><b>Balance Sheet Accounting</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />

            <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">

                <div id="tabs" style="background: beige;">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li><a id="A2" href="#menu1" runat="server">Create Group</a></li>
                        <li><a href="#menu2">Upload Bank statement</a></li>
                        <li><a href="#menu3">Bank Receipt</a></li>

                    </ul>
                    <div id="menu1">
                        <br />
                        <asp:Panel ID="Panel2" runat="server" CssClass="panel panel-primary" Style="background-color: white;">
                            <div class="container">
                                <%--<div class="row">--%>

                                <div class="col-sm-1"></div>
                                <div class="col-sm-2 col-xs-12" style="text-align: center;">
                                    <b>Enter Group Name :</b>
                                    <asp:TextBox ID="txt_group" runat="server" class="form-control"></asp:TextBox>
                                    <br />
                                </div>
                                <div class=" col-sm-2 col-xs-12">
                                    <b>Expense :</b>
                                    <asp:DropDownList ID="ddl_expanse" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Direct">Direct</asp:ListItem>
                                        <asp:ListItem Value="Indirect">Indirect</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-sm-2 col-xs-12 ">
                                    <br />
                                    <asp:Button ID="btn_group" runat="server" class="btn btn-large" Text="Create Group" OnClick="btn_group_Click" OnClientClick="return Group_validation();" />
                                </div>
                                <%--</div>--%>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel5" runat="server" CssClass="panel panel-large" Style="background-color: white;">
                            <div class="container">
                                <div class="row">
                                    <div class="col-sm-1"></div>
                                    <div class="col-sm-2 col-xs-12">

                                        <b>Group Name :</b>
                                        <asp:DropDownList ID="ddl_group_name" DataValueField="group_name" DataTextField="group_name" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_group_name_SelectedIndexChanged"></asp:DropDownList>
                                        <br />
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <b>Expese :</b>
                                        <asp:TextBox ID="txt_expese" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                        <br />
                                    </div>
                                    <div class="col-sm-2 col-xs-12">

                                        <b>Enter Sub Group Name :</b>
                                        <asp:TextBox ID="txt_sub_group" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </div>
                                    <div class="col-sm-3 col-xs-12 ">
                                        <br />
                                        <asp:Button ID="btn_subgp" runat="server" class="btn btn-large" Text="Create Sub Group" OnClick="btn_subgp_Click" OnClientClick="return SubGroup_validation();" />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel6" runat="server" CssClass="panel panel-primary" Style="background-color: white;">
                            <div class="container">
                                <div class="row">
                                    <div class="col-sm-1"></div>

                                    <div class="col-sm-2 col-xs-12">

                                        <b>Group Name :</b>
                                        <asp:DropDownList ID="ddl_group" DataValueField="group_name" DataTextField="group_name" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_group_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                        <br />
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <b>Expese :</b>
                                        <asp:TextBox ID="txt_expese1" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                        <br />
                                    </div>
                                    <div class="col-sm-2 col-xs-12">

                                        <b>Sub Group Name :</b>
                                        <asp:DropDownList ID="ddl_sub_group" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <br />
                                    </div>
                                    <div class="col-sm-2 col-xs-12">

                                        <b>Enter ledger Name:</b>
                                        <asp:TextBox ID="txt_ledger" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </div>
                                    <div class="col-sm-2 col-xs-12 ">
                                        <br />
                                        <asp:Button ID="btn_ledger" runat="server" class="btn btn-large" Text="Create Ledger" OnClick="btn_ledger_Click" OnClientClick="return Ledger_validation()" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <%-- <div class="col-sm-3 col-xs-12">
                                <br />
                                <asp:LinkButton ID="lnk_button" runat="server" OnClick="lnk_button_Click">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                </asp:LinkButton>
                            </div>--%>

                        <br />
                        <div class="row">
                            <div class="container">
                                <asp:Panel ID="panel4" runat="server" CssClass="grid-view">
                                    <asp:GridView ID="gv_ledger" class="table" runat="server" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" OnPreRender="gv_ledger_PreRender"
                                        OnRowDataBound="gv_ledger_RowDataBound">
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>


                            </div>
                        </div>

                    </div>
                    <div id="menu2">

                        <asp:Panel ID="Panel7" runat="server" CssClass="panel panel-primary" Style="background-color: white;">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Date :</b>
                                        <asp:TextBox ID="txt_date" runat="server" CssClass="form-control date-picker1 "></asp:TextBox>
                                        <br />
                                    </div>

                                    <div class=" col-md-2  col-xs-12">
                                       <b> Bank  :</b><span class="text-red">*</span>
                                        <asp:DropDownList ID="ddl_bank" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                            <asp:ListItem Value="INDUSIND BANK">INDUSIND BANK</asp:ListItem>
                                            <asp:ListItem Value="SBI BANK">SBI BANK</asp:ListItem>
                                             <asp:ListItem Value="Axis BANK">Axis BANK</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <br />
                                    <div class="col-sm-2 col-xs-12">
                                      <b>  Upload:</b>
                            <asp:FileUpload ID="file_upload" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                                        <asp:Label ID="lbl_note" runat="server" ForeColor="Red"></asp:Label>
                                        <br />
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <asp:Button ID="btn_upload" runat="server" class="btn btn-primary text-center" OnClick="btn_upload_Click" Text="Upload" OnClientClick="return valid_upload();" />
                                        <br />
                                    </div>
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </asp:Panel>

                        <br />
                        <br />
                        <div class="row">
                            <div class="container">
                                <asp:Panel ID="panel8" runat="server" CssClass="grid-view">
                                    <asp:GridView ID="gv_upload" class="table" runat="server" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" OnPreRender="gv_upload_PreRender"
                                        OnRowDataBound="gv_upload_RowDataBound">
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>


                            </div>
                        </div>
                    </div>

                    <%--approve/reject for receipt details komal 20-06-2020--%>
                     <div id="menu3">

                            <div class="row">

                                 <div class=" col-sm-1 col-xs-12" style="width: 10%;">
                                       <b> Account Type:</b><span class="text-red">*</span>
                                        <asp:DropDownList ID="ddl_receipt_details" runat="server" OnSelectedIndexChanged="ddl_receipt_details_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="select">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Receipt</asp:ListItem>
                                            <asp:ListItem Value="2">Receipt Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                <asp:Panel ID="panel_received_type" runat="server">
                                 <div class="col-sm-2 col-xs-12 ">
                                       <b> Received from :</b> 
                                  
                                  <asp:DropDownList ID="ddl_received_type" runat="server" OnSelectedIndexChanged="ddl_received_type_SelectedIndexChanged" class="form-control"  AutoPostBack="true">
                                  <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="0">Client</asp:ListItem>
                                <asp:ListItem Value="1">Other</asp:ListItem>
                            </asp:DropDownList>

                           </div>
                                       </asp:Panel>

                                  

                                 <div class="col-sm-2 col-xs-12">
                                       <b> Client Name : </b><span class="text-red" style="color: red">*</span>
                                        <asp:DropDownList ID="ddl_bill_client_receipt" DataValueField="client_code" OnSelectedIndexChanged="ddl_bill_client_receipt_SelectedIndexChanged" DataTextField="client_name" AutoPostBack="true"  runat="server" CssClass="form-control" >
                                            <%--<asp:ListItem Value="0">Select</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>



                                  <asp:Panel ID="panel_receipt_details" runat="server">

                                  <div class="col-sm-2 col-xs-12">
                                            <b>Payment Receving Date :</b><span style="color: red">*</span>
                                           <%-- <asp:TextBox ID="txt_date" runat="server" CssClass="form-control date-picker1"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddl_receving_date" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_receving_date_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>


                                  <div class="col-sm-2 col-xs-12" >
                                           <b> Select Amount:</b><span style="color: red">*</span>

                                            <asp:DropDownList ID="ddl_client_resive_amt" runat="server" OnSelectedIndexChanged="ddl_client_resive_amt_SelectedIndexChanged" class="form-control" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>

                                </asp:Panel>


                                </div>

                          <br />
                          <br />

                         <asp:Panel ID="panel9" runat="server" Style="overflow-x:auto;" CssClass="grid-view">
                                 <%--   <h4 class="text-center">Transaction</h4>--%>
                                    <div class="container-fluid">

                                        <asp:GridView ID="gv_minibank_receipt" class="table" DataKeyNames="Id" runat="server" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gv_minibank_receipt_RowDataBound" Width="100%" >
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <Columns>

                                                   <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chk_gv_header" runat="server" Text="SELECT CLIENT"  />

                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_client" runat="server" CssClass="center-block" />
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                              <%--  <asp:TemplateField HeaderText="DELETE">
                                                    <ItemStyle Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandArgument='<%# Eval("ID")%>' OnCommand="lnkminiDelete_Command" OnClientClick="return confirm('Are you sure You want to  Delete ?')"><img alt="" height="15" style = "margin-left: 23px;" src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="DOWNLOAD FILE">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_download" runat="server" Style="color: white" CausesValidation="false" CssClass="btn btn-primary" Text="DOWNLOAD" CommandArgument='<%# Eval("upload_file")%>' OnCommand="lnk_download_Command" ></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                                <asp:BoundField DataField="receipt_approve" HeaderText="Status"  SortExpression="receipt_approve" />

                                                 <asp:TemplateField HeaderText="REJECT REASON">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_recive_amt" runat="server" CssClass="form-control" Text='<%# Eval("receipt_reasons")%>' Width="150" onkeypress="return isNumberKey(event,this.id)"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>


                                        <br />
                                    </div>
                                               </asp:Panel>


                        <%-- // for receipt details gridview komal 22-06-2020--%>

                          <asp:Panel ID="panel10" runat="server" Style="overflow-x:auto;overflow-y:auto;height:300px;" CssClass="grid-view">
                                 <%--   <h4 class="text-center">Transaction</h4>--%>
                                    <div class="container-fluid" style="height:300px;overflow-y:auto;">

                                          <div class="row">
                                <div class="col-sm-10 col-xs-12"></div>
                                <div class="col-sm-2 col-xs-12">
                                    Search :
                        <asp:TextBox runat="server" ID="txt_search" CssClass=" form-control" onkeyup="Search_Gridview(this)" />
                                </div>
                            </div>

                                        <asp:GridView ID="gv_receipt_details" class="table" DataKeyNames="Id" runat="server" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" OnRowDataBound="gv_receipt_details_RowDataBound" BorderWidth="1px" OnPreRender="gv_receipt_details_PreRender" CellPadding="3"  Width="100%" >
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <Columns>

                                                   <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chk_gv_header" runat="server" Text="SELECT CLIENT"  />

                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_client" runat="server" CssClass="center-block" />
                                            </ItemTemplate>
                                        </asp:TemplateField> 

                                              <asp:BoundField DataField="receipt_de_approve" HeaderText="Status"  SortExpression="receipt_de_approve" />
                                         
                                                <asp:TemplateField HeaderText="RECEIPT REASON">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_receive_de_reason" runat="server" CssClass="form-control" Text='<%# Eval("receipt_reasons")%>' Width="150" onkeypress="return isNumberKey(event,this.id)"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                
                                                   </Columns>
                                        </asp:GridView>


                                        <br />
                                    </div>
                                               </asp:Panel>

                           <%-- // for receipt details gridview komal 22-06-2020 end --%>
                          <br />
                          <br />

                           <div class="row text-center">
                                  <asp:Button ID="btn_approve_receipt" runat="server" class="btn btn-large" OnClick="btn_approve_receipt_Click" OnClientClick="return  approve_function();" Text="APPROVE" />
                          <asp:Button ID="btn_edit_receipt" runat="server" class="btn btn-primary" OnClick="btn_edit_receipt_Click" OnClientClick="return  Reject_function();" Text="REJECT" />
                                <asp:Button ID="btn_sr_approved" runat="server" class="btn btn-large"  OnClick="btn_sr_approved_Click" OnClientClick="return" Text="Sr Approved Record" />
                                <asp:Button ID="btn_reject_sr" runat="server" class="btn btn-large" OnClick="btn_reject_sr_Click" OnClientClick="return" Text="Sr Reject Record" />
                                 

                               </div>
                         </div>

                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>






