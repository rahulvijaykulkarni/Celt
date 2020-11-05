<%@  Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"   CodeFile="Credit_note.aspx.cs" Inherits="Credit_note" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>credit Debit Note</title>
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

        $('[id*=chk_gv_header]').click(function () {
            $("[id*='chk_invoice']").attr('checked', this.checked);
        });
        $('[id*=chk_gv_header]').click(function () {
            $("[id*='chk_invoice1']").attr('checked', this.checked);
        });
        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });

        function pageLoad() {

            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                maxDate: 0,
                yearRange: "1990:+100",
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

            $('.date-picker1').datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "1950",
                maxDate: 0,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',

            });
            $(".date-picker1").attr("readonly", "true");


            $(".date_join").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                yearRange: '1950:2050',
                onSelect: function (selected) {
                    $(".confirm_date").datepicker("option", "maxDate", selected)
                    $(".date_left").datepicker("option", "minDate", selected)
                }
            });



            $(document).ready(function () {
                $(document).on("Keyup", function () {
                    SearchGrid('<%=txt_search.ClientID%>', '<%=gv_check_invoice.ClientID%>');
                });


                $('[id*=chk_gv_header]').click(function () {
                    $("[id*='chk_invoice']").attr('checked', this.checked);
                });


            });

            region_validation();

        }


        function Search_Gridview(strKey) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gv_check_invoice.ClientID %>");
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


        //komal 04-06-2020

        function final_invoice_validations() {

            var txt_invoice_date = document.getElementById('<%=txt_invoice_date.ClientID %>');

            if (txt_invoice_date.value == "") {
                alert("Please Enter Invoice Date");
                txt_invoice_date.focus();
                return false;
            }



        }

        function R_validation() {
            var r = confirm("Are you Sure You Want to Final Invoice");
            if (r == true) {
                ($.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } }))
                return true;
            }
            else {
                return false;
            }
        }


        function R_validation_delete() {
            var r = confirm("Are you Sure You Want to Delete Record");
            if (r == true) {
                ($.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } }))
                return true;
            }
            else {
                return false;
            }
        }



        function finance_copy_validation()
        {
            var txt_manual_date = document.getElementById('<%=txt_manual_date.ClientID %>');

            if (txt_manual_date.value == "") {
                alert("Please Select Month");
                txt_manual_date.focus();
                return false;
            }

            var ddl_gst_payed = document.getElementById('<%=ddl_gst_payed.ClientID %>');
            var Selected_ddl_gst_payed = ddl_gst_payed.options[ddl_gst_payed.selectedIndex].text;

            if (Selected_ddl_gst_payed == "Select") {
                alert("Please Select Gst to be Payed");
                ddl_gst_payed.focus();
                return false;
            }

            var ddl_manual_invoice_type = document.getElementById('<%=ddl_manual_invoice_type.ClientID %>');
            var Selected_ddl_manual_invoice_type = ddl_manual_invoice_type.options[ddl_manual_invoice_type.selectedIndex].text;

            if (Selected_ddl_manual_invoice_type == "Select") {
                alert("Please Select Manual Invoice Type");
                ddl_manual_invoice_type.focus();
                return false;
            }


            var ddl_client_manual = document.getElementById('<%=ddl_client_manual.ClientID %>');
            var Selected_ddl_client_manual = ddl_client_manual.options[ddl_client_manual.selectedIndex].text;

            if (Selected_ddl_client_manual == "ALL") {
                alert("Please Select Client Name");
                ddl_client_manual.focus();
                return false;
            }



            var ddl_state = document.getElementById('<%=ddl_state.ClientID %>');
            var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;

            if (Selected_ddl_state == "Select") {
                alert("Please Select State Name");
                ddl_state.focus();
                return false;
            }


            var ddl_state_name_other = document.getElementById('<%=ddl_state_name_other.ClientID %>');
            var Selected_ddl_state_name_other = ddl_state_name_other.options[ddl_state_name_other.selectedIndex].text;

            if (Selected_ddl_state_name_other == "Select") {
                alert("Please Select State Name");
                ddl_state_name_other.focus();
                return false;
            }


            var txt_invoice_date = document.getElementById('<%=txt_invoice_date.ClientID %>');

            if (txt_invoice_date.value == "") {
                alert("Please Enter Invoice Date");
                txt_invoice_date.focus();
                return false;
            }


        }




        function manual_invoice_validation()
        {
            var txt_manual_date = document.getElementById('<%=txt_manual_date.ClientID %>');

            if (txt_manual_date.value == "") {
                alert("Please Select Month");
                txt_manual_date.focus();
                return false;
            }

            var ddl_gst_payed = document.getElementById('<%=ddl_gst_payed.ClientID %>');
            var Selected_ddl_gst_payed = ddl_gst_payed.options[ddl_gst_payed.selectedIndex].text;

            if (Selected_ddl_gst_payed == "Select") {
                alert("Please Select Gst to be payed");
                ddl_gst_payed.focus();
                return false;
            }



            var ddl_manual_invoice_type = document.getElementById('<%=ddl_manual_invoice_type.ClientID %>');
            var Selected_ddl_manual_invoice_type = ddl_manual_invoice_type.options[ddl_manual_invoice_type.selectedIndex].text;

            if (Selected_ddl_manual_invoice_type == "Select") {
                alert("Please Select Manual Invoice Type");
                ddl_manual_invoice_type.focus();
                return false;
            }


            var ddl_client_manual = document.getElementById('<%=ddl_client_manual.ClientID %>');
            var Selected_ddl_client_manual = ddl_client_manual.options[ddl_client_manual.selectedIndex].text;

            if (Selected_ddl_client_manual == "ALL") {
                alert("Please Select Client Name");
                ddl_client_manual.focus();
                return false;
            }



            var ddl_state = document.getElementById('<%=ddl_state.ClientID %>');
            var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;

            if (Selected_ddl_state == "Select") {
                alert("Please Select State Name");
                ddl_state.focus();
                return false;
            }

            var txt_gst_no = document.getElementById('<%=txt_gst_no.ClientID %>');

            if (txt_gst_no.value == "") {
                alert("Please Enter gst Number");
                txt_gst_no.focus();
                return false;
            }



            var txt_amount = document.getElementById('<%=txt_amount.ClientID %>');

            if (txt_amount.value == "0") {
                alert("Please Enter Amount");
                txt_amount.focus();
                return false;
            }


         

           //for other 



            var txt_bill_party = document.getElementById('<%=txt_bill_party.ClientID %>');

            if (txt_bill_party.value == "") {
                alert("Please Enter Bill To Party Name");
                txt_bill_party.focus();
                return false;
            }

            var txt_invoice_category = document.getElementById('<%=txt_invoice_category.ClientID %>');

            if (txt_invoice_category.value == "") {
                alert("Please Enter Invoice Category");
                txt_invoice_category.focus();
                return false;
            }

            var txt_gst_no_other = document.getElementById('<%=txt_gst_no_other.ClientID %>');

            if (txt_gst_no_other.value == "") {
                alert("Please Enter gst number");
                txt_gst_no_other.focus();
                return false;
            }

            var txt_gst_add = document.getElementById('<%=txt_gst_add.ClientID %>');

            if (txt_gst_add.value == "") {
                alert("Please Enter gst address");
                txt_gst_add.focus();
                return false;
            }

            var txt_bill_add = document.getElementById('<%=txt_bill_add.ClientID %>');

            if (txt_bill_add.value == "") {
                alert("Please Enter Bill Shipping Address");
                txt_bill_add.focus();
                return false;
            }


            var ddl_state_name_other = document.getElementById('<%=ddl_state_name_other.ClientID %>');
            var Selected_ddl_state_name_other = ddl_state_name_other.options[ddl_state_name_other.selectedIndex].text;

            if (Selected_ddl_state_name_other == "Select") {
                alert("Please Select State Name");
                ddl_state_name_other.focus();
                return false;
            }


        }


        function region_validation()
        {
            var ddl_client_manual = document.getElementById('<%=ddl_client_manual.ClientID %>');
            var Selected_ddl_client_manual = ddl_client_manual.options[ddl_client_manual.selectedIndex].value;


            var Selected_ddl_client_manual = ddl_client_manual.options[ddl_client_manual.selectedIndex].text;
            if (Selected_ddl_client_manual == "Dewan Housing Finance Corporation Limited") {
                $(".region").show();
            }

            else { $(".region").hide(); }


        }
       
        function process_validation() {

            var ddl_note_type = document.getElementById('<%=ddl_note_type.ClientID %>');
            var ddl_note_type = ddl_note_type.options[ddl_note_type.selectedIndex].text;

            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

             

            if (ddl_note_type == "Select") {
                alert("Please Select Credit Note");
                ddl_note_type.focus();
                return false;
            }

            if (ddl_client == "Select") {
                alert("Please Select Client");
                ddl_client.focus();
                return false;
            }

             // $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }

        $(document).ready(function () {

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_status.ClientID%>').DataTable({
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
              .appendTo('#<%=gv_status.ClientID%>_wrapper .col-sm-6:eq(0)');
            /////// for clientwise manual invoice komal 06-06-2020


            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_manual_invoice.ClientID%>').DataTable({
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
              .appendTo('#<%=gv_manual_invoice.ClientID%>_wrapper .col-sm-6:eq(0)');




            //////// for other manual invoice komal 06-06-2020

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_other_invoice.ClientID%>').DataTable({
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
                          .appendTo('#<%=gv_other_invoice.ClientID%>_wrapper .col-sm-6:eq(0)');






            ///////

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_invoice_details.ClientID%>').DataTable({
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
              .appendTo('#<%=gv_invoice_details.ClientID%>_wrapper .col-sm-6:eq(0)');

          

        });

</script>
    </asp:Content>
 
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="server">
    <div class="container-fluid">
        <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
         <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-primary" Style="background-color: white;">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: small;"><b>credit Debit Note</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />

             <div class="container-fluid">
                   <div id="tabs" style="background: beige; border-color: gray">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li><a id="A1" href="#menu1" runat="server">Credit Debit Note</a></li>
                        <li><a href="#menu2">Manual Invoice</a></li>
                      
                    </ul>
                         <div id="menu1">
                               <div class="panel-body">
             <div class="row">
                  <div class="col-sm-2"></div>
                                                 <div class=" col-sm-2 col-xs-12">
                                    <b>Credit Note :</b>
                                    <asp:DropDownList ID="ddl_note_type" runat="server" CssClass="form-control" >
                                         <asp:ListItem Value="Select">Select</asp:ListItem>
                                        <asp:ListItem Value="1">CREDIT</asp:ListItem>
                                        <asp:ListItem Value="2">DEBIT</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                   <div class="col-sm-2 col-xs-12">
                           <b> Client Name :</b>
                            <asp:DropDownList ID="ddl_client" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                  <div class="col-sm-2 col-xs-12">
                           Date
                        <asp:TextBox ID="txt_date" runat="server" class="form-control date-picker" ></asp:TextBox>
                        </div>
                   <div class="col-sm-2 col-xs-12 ">
                                    <br />
                                    <asp:Button ID="btn_process" runat="server" class="btn btn-primary" Text="Process" OnClick="btn_process_Click" OnClientClick="return process_validation();"/>
                                </div>
                 <br />
             </div>
                     <br />
                     <br />
                     <div class="row">
                     <div class="container" style="width: 80%">

                          <div class="row">
                                <div class="col-sm-10 col-xs-12"></div>
                                <div class="col-sm-2 col-xs-12">
                                    Search :
                        <asp:TextBox runat="server" ID="txt_search" CssClass=" form-control" onkeyup="Search_Gridview(this)" />
                                </div>
                            </div>

                <asp:Panel ID="Panel6" runat="server" Style="overflow-y: auto; max-height: 250px; overflow-x: hidden">
                        <asp:GridView ID="gv_check_invoice" class="table" 
                                  HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" 
                                  OnRowDataBound ="gv_check_invoice_RowDataBound" ShowFooter="false" 
                                  data-toggle="modal" DataKeyNames="Invoice_no" OnPreRender="gv_check_invoice_PreRender"
                            BorderColor="#CCCCCC" BorderStyle="None" 
                                     BorderWidth="1px"  Width="100%">
                                   <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                            <RowStyle ForeColor="#000066" BackColor="#ffffff" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                <Columns>
                                                      <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chk_gv_header" runat="server" Text="SELECT INVOICE"  />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_invoice" runat="server" CssClass="center-block" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="invoice_no" HeaderText="INVOICE NO" SortExpression="invoice_no" />
                                                      <asp:BoundField DataField="state_name" HeaderText="STATE NAME" SortExpression="state_name" />
                                                      <asp:BoundField DataField="type" HeaderText="TYPE" SortExpression="type" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <br />
                 </div>
                         </div>
                     <div class="row text-center">
             
                                    <asp:Button ID="btn_submit" runat="server" class="btn btn-primary" Text="Submit" OnClick="btn_submit_Click" />

                 </div>
                     </div>
                
              <div class="row" id="gv_show" runat="server">
                     <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; margin-top:20px; margin-left:30px; margin-bottom:20px; margin-right:30px; border: 1px solid #ddd9d9 ; padding:25px 25px 25px 25px;overflow-x:auto">
                <asp:Panel ID="Panel2" runat="server" Style=" max-height: 250px" BackColor="#f3f1fe">
                     <asp:GridView ID="gv_invoice_details" runat="server" class="table" AutoGenerateColumns="False" CellPadding="1" 
                          BorderColor="#CCCCCC" BorderStyle="None" DataKeyNames="Invoice_no"
                                     BorderWidth="1px" OnRowDataBound="gv_invoice_details_RowDataBound"  OnPreRender="gv_invoice_details_PreRender" Width="100%" Style="border-collapse: collapse;">
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="True" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <EditRowStyle BackColor="#2461BF" />

                                               <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                            <RowStyle ForeColor="#000066" BackColor="#ffffff" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                <Columns>
                                                     <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                     <ItemTemplate>
                                                <asp:CheckBox ID="chk_invoice1" runat="server" CssClass="center-block" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="CREDIT/DEBIT NOTE DATE">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_credit_not_date" runat="server" CssClass="form-control date-picker1"  Width="150" Text='<%# Eval("CREDIT NOTE DATE") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Invoice_no" SortExpression="Invoice_no" ControlStyle-Width="144px" HeaderText="INVOICE NO" HeaderStyle-Width="144px" ItemStyle-Width="144px" />
                                                    <asp:BoundField DataField="invoice_date" SortExpression="invoice_date" ControlStyle-Width="144px" HeaderText="INVOICE DATE " HeaderStyle-Width="144px" ItemStyle-Width="144px" />
                                                     <asp:BoundField DataField="client_name" SortExpression="client_name" ControlStyle-Width="144px" HeaderText="CLIENT NAME " HeaderStyle-Width="144px" ItemStyle-Width="144px" />
                                                    <asp:BoundField DataField="state_name" SortExpression="state_name" ControlStyle-Width="144px" HeaderText="STATE NAME " HeaderStyle-Width="144px" ItemStyle-Width="144px" />
                                                     <asp:BoundField DataField="type" SortExpression="type" ControlStyle-Width="144px" HeaderText="TYPE" HeaderStyle-Width="144px" ItemStyle-Width="144px" />
                                                     <asp:BoundField DataField="gst_no" SortExpression="gst_no" ControlStyle-Width="144px" HeaderText="GST NO " HeaderStyle-Width="144px" ItemStyle-Width="144px" />
                                                    
                                                     <asp:TemplateField HeaderText="TAXABLE AMOUNT">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_amount" runat="server" CssClass="form-control" Text='<%# Eval("amount","{0:n}")%>' Width="100" OnTextChanged="txt_amount_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CGST AMOUNT">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_cgst" runat="server" CssClass="form-control" Text='<%# Eval("cgst","{0:n}")%>' Width="100" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SGST AMOUNT">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_sgst" runat="server" CssClass="form-control" Text='<%# Eval("sgst","{0:n}")%>' Width="100" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="IGST AMOUNT">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_igst" runat="server" CssClass="form-control" Text='<%# Eval("igst","{0:n}")%>' Width="100" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TOTAL AMOUNT">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_total" runat="server" CssClass="form-control" value="0" Text='<%# Eval("Total","{0:n}")%>'  Width="100" onkeypress="return isNumberKey(event,this.id)"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                </asp:Panel>
                <br />
                 </div>
                         </div>
             <div class="row text-center" id="button" runat="server">

                  <asp:Button ID="btn_save" runat="server" class="btn btn-primary" Text="Save" OnClick="btn_save_Click" />

                 <asp:Button ID="btn_cr_exl" runat="server" class="btn btn-primary" Text="CR Exl" OnClick="btn_cr_exl_Click"/>
                 <asp:Button ID="btn_cr_invoice" runat="server" class="btn btn-primary" Text="CR Invoice" OnClick="btn_cr_invoice_Click" />
                 <asp:Button ID="btn_cr_final_invoice" runat="server" OnClick="btn_cr_final_invoice_Click" class="btn btn-primary" Text="CR Final Invoice" OnClientClick="return confirm('Are you sure want to Final Invoice Print?');"/>
             </div>

                              <div class="row">

                                   <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; margin-top:20px; margin-left:30px; margin-bottom:20px; margin-right:30px; border: 1px solid #ddd9d9 ; padding:25px 25px 25px 25px">
                                     <asp:Panel ID="Panel3" runat="server" BackColor="#f3f1fe" Visible="true" 
                                          CssClass="grid-view">
                                <asp:GridView ID="gv_status" class="table" runat="server" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" 
                                     BorderWidth="1px" OnPreRender="gv_status_PreRender" OnRowDataBound="gv_status_RowDataBound"
                                    AutoGenerateColumns="false" Width="100%">
                                   <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                            <RowStyle ForeColor="#000066" BackColor="#ffffff" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                          <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                        <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                                        <asp:BoundField DataField="credit_note_no" HeaderText="Credit/Debit Note No" SortExpression="credit_note_no" />
                                        <asp:BoundField DataField="month" HeaderText="Month" SortExpression="month" />
                                        <asp:BoundField DataField="year" HeaderText="Year" SortExpression="year" />
                                        <asp:BoundField DataField="original_bill_no" HeaderText="Original Bill No" SortExpression="original_bill_no" />
                                        <asp:BoundField DataField="bill_date" HeaderText="Invoice Date" SortExpression="bill_date" />
                                        <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                        <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                        <asp:BoundField DataField="gst_no" HeaderText="Gst" SortExpression="gst_no" />
                                        <asp:BoundField DataField="cgst" HeaderText="CGST" SortExpression="cgst" />
                                        <asp:BoundField DataField="sgst" HeaderText="SGST" SortExpression="sgst" />
                                        <asp:BoundField DataField="Igst" HeaderText="IGST" SortExpression="Igst" />
                                        <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" />
                                      
                                    </Columns>
                                </asp:GridView>
                                          </asp:Panel>

                           </div>
                                  </div>



             <br />
                             </div>
                      <div id="menu2">
                            <div class="container-fluid">
                    <div class="container-fluid"  style="border: 1px solid #ddd9d9; background: #f3f1fe;  margin-left:-10px; margin-right:-10px; padding:20px 20px 20px 20px; margin-bottom:15px; border-radius: 10px;margin-bottom:20px;margin-top:20px">
                    <br />
                   
                     
                          <%--<div class="col-sm-2 col-xs-12" style="visibility:hidden">
                            <b> Company Code </b> 

                                    <asp:TextBox ID="txt_companycode" runat="server" onkeypress="return AllowAlphabet_Number(event);"
                                        class="form-control" ReadOnly="true" xMaxLength="10"></asp:TextBox>
                                        </div>--%>
                        <div class="row">
                         <div class="col-sm-2 col-xs-12">
                             <b> Type :</b>
                           <asp:TextBox ID="txt_type" runat="server"  class="form-control " value="Manual" ReadOnly="true" ></asp:TextBox>
                        </div>

                             <div class="col-sm-2 col-xs-12">
                           <b> SAC-Code :</b>
                                <asp:TextBox ID="txt_sac_code" runat="server" class="form-control "  ReadOnly="true" ></asp:TextBox>
                        </div>

                             <div class="col-sm-2 col-xs-12">
                         <b>  Month</b>
                        <asp:TextBox ID="txt_manual_date" runat="server" class="form-control date-picker" ></asp:TextBox>
                        </div>


                              <div id="Div3" class="col-sm-2 col-xs-12" runat="server">
                          <b> GST To Be Payed :</b>
                       <asp:DropDownList ID="ddl_gst_payed" runat="server" CssClass="form-control" >
                                    <asp:ListItem Value="SEL">Select</asp:ListItem>
                                    <asp:ListItem Value="R">Regular</asp:ListItem>
                                    <asp:ListItem Value="SEWP">SEZ supplies with payment</asp:ListItem>
                                    <asp:ListItem Value="SEWOP">SEZ supplies without payment</asp:ListItem>
                                    <asp:ListItem Value="DE">Deemed Exp</asp:ListItem>
                                    <asp:ListItem Value="SCU">Supplies covered under section 7 of IGST Act</asp:ListItem>

                                </asp:DropDownList>
                        </div>

                           <%-- 30-05-2020 komal changes--%>
                               <div class="col-sm-2 col-xs-12">
                                 <b>  Manual Invoice Type :</b> <span class="text-red" style="color: red">*</span>
                                    <asp:DropDownList ID="ddl_manual_invoice_type"  runat="server"  OnSelectedIndexChanged="ddl_manual_invoice_type_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                                     <asp:ListItem Value="2">Select</asp:ListItem>
                                            <asp:ListItem Value="0">Clientwise</asp:ListItem>
                                        <asp:ListItem Value="1">Other</asp:ListItem>
                                       

                                    </asp:DropDownList>
                                </div>


                              <asp:Panel ID="panel4" runat="server">
                                 <div class="col-sm-2 col-xs-12" > <%--style="visibility:hidden"--%> 
                           <b> Invoice Number :</b>
                                <asp:TextBox ID="txt_invoice_no" runat="server"  class="form-control" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>
                        </div>
                                    </asp:Panel>


                              </div>
                             <br />
                            
                            <%-- 30-05-2020 komal changes end--%>

                                   <%-- <asp:ListItem Value="1">Manpower</asp:ListItem>
                                    <asp:ListItem Value="2">Material</asp:ListItem>
                                    <asp:ListItem Value="3">Convence</asp:ListItem>
                                    <asp:ListItem Value="4">Deep Cleaning</asp:ListItem>
                                    <asp:ListItem Value="5">Machine Rental</asp:ListItem>--%>
                                    
                   <%--     31-05-2020 komal manual invoice--%>
                               <br />
                                       
                      <div class="row">

                               <asp:Panel ID="panel_clientwise" runat="server">

                         <div class="col-sm-2 col-xs-12">
                       <b> Client Name :</b>
                        <asp:DropDownList ID="ddl_client_manual" runat="server" class="form-control" Width="100%" OnSelectedIndexChanged="ddl_client_manual_SelectedIndexChanged" onchange="region_validation()" AutoPostBack="true">
                        </asp:DropDownList>
                    </div> 

                                   <div class="col-sm-2 col-xs-12 region" style="display: none">
                                   <b> Region :</b>
                            <asp:DropDownList ID="ddlregion_manual" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlregion_manual_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                                </div>

                                   
                                    

                        <div class="col-sm-2 col-xs-12">
                           <b> State Name:</b>
                            <asp:DropDownList ID="ddl_state" runat="server" CssClass="form-control" Width="100%" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true" >
                         </asp:DropDownList>
                        </div>

                                <div class="col-sm-2 col-xs-12">
                           <b> GST Number :</b>
                                <asp:TextBox ID="txt_gst_no" runat="server"  class="form-control" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>
                        </div>
                             </asp:Panel>

                             
                          <asp:Panel ID="panel_invoice_date" runat="server">
                             <div class="col-sm-2 col-xs-12">
                       <b> Invoice Date :</b>
                              <asp:TextBox ID="txt_invoice_date"  runat="server" class="form-control date-picker1"></asp:TextBox>
                    </div>
                           </asp:Panel>

                            </div>

                                          
                        <br /><br />

                        
                        <asp:Panel ID="panel_other" runat="server">

                               <div class="row">

                                     <div class="col-sm-2 col-xs-12">
                           <b> State Name:</b>
                            <asp:DropDownList ID="ddl_state_name_other" runat="server" CssClass="form-control" Width="100%" AutoPostBack="true" >
                         </asp:DropDownList>

                        </div>

                                    <div class="col-sm-2 col-xs-12">
                           <b> Gst Number :</b>
                                <asp:TextBox ID="txt_gst_no_other" runat="server"  class="form-control" onKeyPress="return AllowAlphabet_Number(event) "></asp:TextBox>
                        </div>

                                    <div class="col-sm-2 col-xs-12">
                           <b> Gst Address :</b>
                                <asp:TextBox ID="txt_gst_add" runat="server" class="form-control" onKeyPress="return "></asp:TextBox>
                        </div>

                                    <div class="col-sm-2 col-xs-12">
                           <b> Bill to Party :</b>
                                <asp:TextBox ID="txt_bill_party" runat="server"  class="form-control" onKeyPress="return "></asp:TextBox>
                        </div>

                                    <div class="col-sm-2 col-xs-12">
                           <b> Invoice Category :</b>
                                <asp:TextBox ID="txt_invoice_category" runat="server" class="form-control" onKeyPress="return "></asp:TextBox>
                        </div>


                                    <div class="col-sm-2 col-xs-12">
                           <b> Bill Shipping Address :</b>
                                <asp:TextBox ID="txt_bill_add" runat="server" class="form-control" onKeyPress="return "></asp:TextBox>
                        </div>

                               
                               </div>

                            </asp:Panel>

                         <div class="row">

                            <div class="col-sm-2 col-xs-12">
                           <b> Amount :</b>
                                <asp:TextBox ID="txt_amount" runat="server" class="form-control " OnTextChanged="txt_amount_TextChanged1" value="0"  AutoPostBack="true"></asp:TextBox>
                        </div>
                            <div class="col-sm-2 col-xs-12">
                           <b> CGST :</b>
                                <asp:TextBox ID="txt_cgst" runat="server" Text='<%# Eval("cgst","{0:n}")%>' class="form-control " value="0" ></asp:TextBox>
                        </div>
                            <div class="col-sm-2 col-xs-12">
                           <b> SGST :</b>
                                <asp:TextBox ID="txt_sgst" runat="server" Text='<%# Eval("sgst","{0:n}")%>'  class="form-control" value="0"></asp:TextBox>
                        </div>
                            <div class="col-sm-2 col-xs-12">
                           <b> IGST :</b>
                                <asp:TextBox ID="txt_igst" runat="server" Text='<%# Eval("igst","{0:n}")%>'  class="form-control " value="0" ></asp:TextBox>
                        </div>

                                 <div class="col-sm-2 col-xs-12">
                           <b> Grand Total :</b>
                                <asp:TextBox ID="txt_grand_total" runat="server" Text='<%# Eval("total","{0:n}")%>'  class="form-control " value="0" ></asp:TextBox>
                        </div>
                            
                             
                           

                             <div class="col-sm-2 col-xs-12" style="visibility:hidden">
                           <b> id :</b>
                                <asp:TextBox ID="txt_id" runat="server" class="form-control "></asp:TextBox>
                        </div>

                              


                            
                            
                             </div>
                        <br /><br />
                        
                    
                        <br />
                        <br />

                        <div class="row text-center">

                            <%--    <asp:Panel ID="panel_save" runat="server">--%>

                            <asp:Button ID="btn_save1" runat="server" class="btn btn-primary"  OnClientClick="return manual_invoice_validation();" OnClick="btn_save1_Click"
                                Text=" Process "  />

                                   <%-- </asp:Panel>--%>

                             <asp:Button ID="btn_excel_clientwise" runat="server" class="btn btn-large" OnClientClick="return finance_copy_validation();" OnClick="btn_excel_clientwise_Click"
                                Text=" Finance Copy "  />


                               <asp:Button ID="btn_clientwise_other_invoice" runat="server" class="btn btn-large"  OnClientClick="return finance_copy_validation();" OnClick ="btn_clientwise_other_invoice_Click"
                                Text="Invoice "  />

                           <%-- <asp:Button ID="btn_final_invoice" runat="server" OnClientClick="return finance_copy_validation();" class="btn btn-primary" OnClick="btn_final_invoice_Click"
                                Text=" Final Invoice "  />--%>

                             <asp:Button ID="btn_final_invoice" runat="server" OnClientClick="return R_validation();" class="btn btn-large" OnClick="btn_final_invoice_Click"
                                Text=" Final Invoice "  />
                             
                            <asp:Button ID="btn_update_other" runat="server" class="btn btn-primary" Text=" Update " OnClick="btn_update_other_Click"/>
                            <asp:Button ID="btn_update" runat="server" class="btn btn-primary" Text=" Update " OnClick="btn_update_Click" Visible="false"
                                /> 

                            </div>

                        <br/>
                     
                                 <%--  <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; margin-top:20px; margin-left:30px; margin-bottom:20px; margin-right:30px; border: 1px solid #ddd9d9 ; padding:25px 25px 25px 25px">--%>
                                 <div class ="container" style="overflow-x:auto">
                                <asp:Panel ID="Panel_manual_invoice" runat="server" BackColor="#f3f1fe" Visible="true" 
                                           meta:resourcekey="Panel_manual_invoiceResource1" CssClass="grid-view">
                                <asp:GridView ID="gv_manual_invoice" class="table" runat="server" BackColor="White"
                                    OnRowDataBound="gv_manual_invoice_RowDataBound" OnPreRender="gv_manual_invoice_PreRender"
                                    BorderColor="#CCCCCC" BorderStyle="None" meta:resourcekey="gv_manual_invoiceResource1"
                                     BorderWidth="1px" OnSelectedIndexChanged="gv_manual_invoice_SelectedIndexChanged"

                                    AutoGenerateColumns="false" Width="100%">
                                   <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                            <RowStyle ForeColor="#000066" BackColor="#ffffff" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                      

                                        <asp:BoundField DataField="id" HeaderText="Id" SortExpression="id" />

                                         <asp:TemplateField HeaderText="Sr No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                         <asp:BoundField DataField="manual_invoice_type" HeaderText="Manual Invoice Type" SortExpression="manual_invoice_type" />
                                        <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                        <asp:BoundField DataField="state_name" HeaderText="State name" SortExpression="State_name" />
                                         <asp:BoundField DataField="manual_month" HeaderText="Month" SortExpression="manual_month" />
                                        <asp:BoundField DataField="invoice_no" HeaderText="Invoice Number" SortExpression="invoice_no" />
                                        <asp:BoundField DataField="invoice_date" HeaderText="Invoice Date" SortExpression="invoice_date" />
                                        <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" />
                                        <asp:BoundField DataField="igst" HeaderText="Igst" SortExpression="igst" />
                                         <asp:BoundField DataField="sgst" HeaderText="Sgst" SortExpression="sgst" />
                                         <asp:BoundField DataField="cgst" HeaderText="cgst" SortExpression="cgst" />
                                       <%-- <asp:BoundField DataField="Total_GST" HeaderText="Total Gst" SortExpression="Total_GST" />--%>
                                        <asp:BoundField DataField="manual_grand_total" HeaderText="Grand Total" SortExpression="manual_grand_total" />
                                          <asp:BoundField DataField="gst_to_be" HeaderText="Gst To Be Payed" SortExpression="gst_to_be" />

                                         <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn_edit" Text="Edit" runat="server" CssClass="btn btn-primary" Style="color:white" OnClick="btn_edit_Click" ></asp:LinkButton>
                                        </ItemTemplate>
                                            
                                    </asp:TemplateField>


                                          <asp:TemplateField  HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_remove_manual_invoice" runat="server" CausesValidation="false" OnClick="lnk_remove_manual_invoice_Click" AutoPostBack="true" OnClientClick="return R_validation_delete();">
                                                    <img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    <asp:BoundField DataField="final_invoice_check" HeaderText="Status"  SortExpression="final_invoice_check" />
                                         <asp:BoundField DataField="final_invoice" HeaderText="Status"  SortExpression="final_invoice" />

                                        <asp:BoundField DataField="manual_region" HeaderText="Region"  SortExpression="manual_region" />


                                    </Columns>
                                </asp:GridView>
                                          </asp:Panel>
                                     </div>

                                     <%--  for other gridview code --%>

                                               <div class ="container" style="overflow-x:auto">
                         <asp:Panel ID="Panel_manual_other" runat="server" BackColor="#f3f1fe" Visible="true" 
                                            CssClass="grid-view">
                                <asp:GridView ID="gv_other_invoice" class="table" runat="server" BackColor="White"
                                    OnRowDataBound="gv_other_invoice_RowDataBound" OnPreRender="gv_other_invoice_PreRender"
                                    BorderColor="#CCCCCC" BorderStyle="None"
                                     BorderWidth="1px" 

                                    AutoGenerateColumns="false" Width="100%">
                                   <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                            <RowStyle ForeColor="#000066" BackColor="#ffffff" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                       

                                        <asp:BoundField DataField="id" HeaderText="Id" SortExpression="id" />

                                         <asp:TemplateField HeaderText="Sr No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                          <asp:BoundField DataField="manual_invoice_type" HeaderText="Manual Invoice Type" SortExpression="manual_invoice_type" />
                                         <asp:BoundField DataField="client_name" HeaderText="Bill To Party" SortExpression="client_name" />
                                        <asp:BoundField DataField="state_name" HeaderText="State name" SortExpression="State_name" />
                                         <asp:BoundField DataField="manual_month" HeaderText="Month" SortExpression="manual_month" />
                                        <asp:BoundField DataField="invoice_category" HeaderText="Invoice Category" SortExpression="invoice_category" />
                                          <asp:BoundField DataField="invoice_no" HeaderText="Invoice Number" SortExpression="invoice_no" />
                                           <asp:BoundField DataField="invoice_date" HeaderText="Invoice Date" SortExpression="invoice_date" />
                                        <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" />
                                         <asp:BoundField DataField="igst" HeaderText="Igst" SortExpression="igst" />
                                         <asp:BoundField DataField="sgst" HeaderText="Sgst" SortExpression="sgst" />
                                         <asp:BoundField DataField="cgst" HeaderText="cgst" SortExpression="cgst" />
                                         <asp:BoundField DataField="manual_grand_total" HeaderText="Grand Total" SortExpression="manual_grand_total" />
                                        <asp:BoundField DataField="gst_address" HeaderText="Gst Address" SortExpression="gst_address" />
                                        <asp:BoundField DataField="gst_no" HeaderText="Gst Number" SortExpression="gst_no" />
                                         
                                      <%--  <asp:BoundField DataField="Total_GST" HeaderText="Total Gst" SortExpression="Total_GST" />--%>
                                       
                                        <asp:BoundField DataField="bill_shipping_add" HeaderText="Bill Shipping Add" SortExpression="bill_shipping_add" />
                                       
                                        
                                     
                                          <asp:BoundField DataField="gst_to_be" HeaderText="Gst To Be Payed" SortExpression="gst_to_be" />

                                        <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn_edit_other1" Text="Edit" runat="server" CssClass="btn btn-primary" Style="color:white" OnClick="btn_edit_other1_Click" ></asp:LinkButton>
                                        </ItemTemplate>
                                            
                                    </asp:TemplateField>
                                        

                                         <asp:TemplateField  HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_remove_manual_other" runat="server" CausesValidation="false" OnClick="lnk_remove_manual_other_Click" AutoPostBack="true" OnClientClick="return R_validation_delete();">
                                                    <img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                               <asp:BoundField DataField="final_invoice_check" HeaderText="Status"  SortExpression="final_invoice_check" />
                                         <asp:BoundField DataField="final_invoice" HeaderText="Status"  SortExpression="final_invoice" />

                                       
                                    </Columns>
                                </asp:GridView>
                                          </asp:Panel>
                           
</div>


                                <%--  end for other gridview code --%>
                          <%-- </div>--%>

          
                


                  <%--  </div>--%>
                        </div>
                    
          


                      </div>
                                
             </div>
                  </div>
                 </div>
</asp:Panel>
    </div>

</asp:Content>


    

