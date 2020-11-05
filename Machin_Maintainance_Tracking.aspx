<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Machin_Maintainance_Tracking.aspx.cs" Inherits="Machin_Maintainance_Tracking" Title="Machin Maintainance and Tracking" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Machin Maintainance and Tracking</title>
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
    <%--  <script src="datatable/jszip.min.js"></script>--%>
    <%--    <script src="datatable/pdfmake.min.js"></script>--%>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>



    <script type="text/javascript">
        function unblock()
        { $.unblockUI(); }

        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });

            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                yearRange: '1951',
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
            $(".date-picker3").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                yearRange: '1951',
                onSelect: function (selected) {
                    $(".date-picker2").datepicker("option", "minDate", selected)
                }
            });

            $(".date-picker4").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
                onSelect: function (selected) {
                    $(".date-picker1").datepicker("option", "maxDate", selected)
                }
            });
            $(".date-picker5").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,


            });
            $(".date-picker1").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");
            $(".date-picker3").attr("readonly", "true");
            $(".date-picker4").attr("readonly", "true");
            $(".date-picker5").attr("readonly", "true");

            $("#dialog").dialog({

                autoOpen: false,
                modal: true,
                height: 500,
                width: 500,
                title: "Zoomed Image",
                buttons: [{ text: "Close", click: function () { $(this).dialog("close") } }],
            });
            $("[id*=que_4_path]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(400));
                $('#dialog').dialog('open');
            });

        });
        function pageLoad() {
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_services.ClientID%>').DataTable(
       {
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
               .appendTo('#<%=gv_services.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_machine_s.ClientID%>').DataTable(
         {
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
               .appendTo('#<%=gv_machine_s.ClientID%>_wrapper .col-sm-6:eq(0)');
          }
          function Req_validation() {

              var ddl_machine = document.getElementById('<%=ddl_machine.ClientID %>');
            var Selected_ddl_machine = ddl_machine.options[ddl_machine.selectedIndex].text;

            if (Selected_ddl_machine == "Select") {
                alert("Please Select Machine Name");
                ddl_machine.focus();
                return false;
            }

            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

            if (Selected_ddl_client == "Select") {
                alert("Please Select Machine Name");
                ddl_client.focus();
                return false;
            }

            var ddl_state = document.getElementById('<%=ddl_state.ClientID %>');
            var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;

            if (Selected_ddl_state == "Select") {
                alert("Please Select State");
                ddl_state.focus();
                return false;
            }

            var ddl_location = document.getElementById('<%=ddl_location.ClientID %>');
            var Selected_ddl_location = ddl_location.options[ddl_location.selectedIndex].text;

            if (Selected_ddl_location == "Select") {
                alert("Please Select Location");
                ddl_location.focus();
                return false;
            }

            var txt_from_date = document.getElementById('<%=txt_from_date.ClientID %>');

            if (txt_from_date.value == "") {
                alert("Please Select Start Date");
                txt_from_date.focus();
                return false;
            }

            var txt_to_date = document.getElementById('<%=txt_to_date.ClientID %>');

            if (txt_to_date.value == "") {
                alert("Please Select End Date");
                txt_to_date.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
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
        function upload_validation() {

            var ddl_machine_service = document.getElementById('<%=ddl_machine_service.ClientID %>');
            var Selected_ddl_machine_service = ddl_machine_service.options[ddl_machine_service.selectedIndex].text;

            if (Selected_ddl_machine_service == "Select") {
                alert("Please Select Machine Name");
                ddl_machine_service.focus();
                return false;
            }
            var txt_form_date_service = document.getElementById('<%=txt_form_date_service.ClientID %>');

            if (txt_form_date_service.value == "") {
                alert("Please Select Start Date");
                txt_form_date_service.focus();
                return false;
            }
            var txt_end_date_service = document.getElementById('<%=txt_end_date_service.ClientID %>');
            if (txt_end_date_service.value == "") {
                alert("Please Select End Date");
                txt_end_date_service.focus();
                return false;
            }

            var ddl_warranty = document.getElementById('<%=ddl_warranty.ClientID %>');
            var Selected_ddl_warranty = ddl_machine_service.options[ddl_warranty.selectedIndex].text;

            if (Selected_ddl_warranty == "Select") {
                alert("Please Select Warranty Type");
                ddl_warranty.focus();
                return false;
            }
            var txt_warranty = document.getElementById('<%=txt_warranty.ClientID %>');
            if (txt_warranty.value == "") {
                alert("Please Enter Warranty (In Hr/Days/Month)");
                txt_warranty.focus();
                return false;
            }
            var txt_nxt_service = document.getElementById('<%=txt_nxt_service.ClientID %>');
            if (txt_nxt_service.value == "") {
                alert("Please Enter next Servicing");
                txt_nxt_service.focus();
                return false;
            }
            var service_document_file = document.getElementById('<%=service_document_file.ClientID %>');
            if (service_document_file.value == "") {
                alert("Please Select Document to Upload");
                service_document_file.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Machin Maintainance and Tracking</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
            <div class="container-fluid">
                <div id="tabs" style="background: beige;">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li><a href="#menu1">Machin Tracking</a></li>
                        <li><a href="#menu2">Machin Servicing / Maintainance</a></li>
                    </ul>
                    <div id="menu1">
                        <div class="container-fluid" style="border: 1px solid white; border-radius: 10px; background-color: #e7e7fa">
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                    Select Machine:
                                <asp:DropDownList runat="server" ID="ddl_machine" CssClass="form-control">
                                </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    Select Client:
                                <asp:DropDownList runat="server" ID="ddl_client" CssClass="form-control" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    Select State:
                                <asp:DropDownList runat="server" ID="ddl_state" CssClass="form-control" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    Select Location:
                                <asp:DropDownList runat="server" ID="ddl_location" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12" style="width: 15%;">
                                    Rental From Date:
                                <asp:TextBox runat="server" ID="txt_from_date" CssClass="form-control date-picker1"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12" style="width: 10%;">
                                    Rental To Date:
                                <asp:TextBox runat="server" ID="txt_to_date" CssClass="form-control date-picker2"></asp:TextBox>
                                </div>
                                <div class="col-sm-1 col-xs-12" style="margin-top: 1.5em">
                                    <asp:LinkButton ID="btn_add_machine" runat="server" OnClientClick="return Req_validation();" OnClick="btn_add_machine_Click">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <br />
                            <div class="row text-center">

                                <asp:Button ID="btn_close" runat="server" Text="Close" class="btn btn-danger" OnClick="btn_close_Click" />
                            </div>
                            <br />
                        </div>
                        <br />
                        <div class="container-fluid">
                            <asp:Panel ID="panel_service" runat="server" CssClass="grid-view">

                                <asp:GridView ID="gv_services" class="table" runat="server" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    AutoGenerateColumns="False" Width="100%" OnPreRender="gv_services_PreRender" OnRowDataBound="gv_services_RowDataBound">
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="White" ForeColor="#000066" />

                                    <Columns>
                                        <asp:TemplateField>

                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtn_services_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_services_removeitem_Click" OnClientClick="return confirm('Are you sure you want to delete this event?');"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />

                                        <asp:TemplateField HeaderText="Machine Name">

                                            <ItemTemplate>
                                                <asp:Label ID="lbl_machine_name" runat="server" Text='<%# Eval("machine_name")%>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_client_code" runat="server" Text='<%# Eval("client_code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="State Name">

                                            <ItemTemplate>
                                                <asp:Label ID="lbl_state" runat="server" Text='<%# Eval("state")%>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Location">

                                            <ItemTemplate>
                                                <asp:Label ID="lbl_location" runat="server" Text='<%# Eval("unit_code")%>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Renatl From ">

                                            <ItemTemplate>
                                                <asp:Label ID="lbl_rental_from" runat="server" Text='<%# Eval("rental_from")%>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Rental To">

                                            <ItemTemplate>
                                                <asp:Label ID="lbl_rental_to" runat="server" Text='<%# Eval("rental_to")%>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </asp:Panel>
                        </div>

                    </div>
                    <div id="menu2">
                        <div class="container-fluid" style="border: 1px solid white; border-radius: 10px; background-color: #e7e7fa">
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                    Select Machine:
                                <asp:DropDownList runat="server" ID="ddl_machine_service" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12" style="width: 15%;">
                                    servicing Form Date:
                                <asp:TextBox runat="server" ID="txt_form_date_service" CssClass="form-control date-picker3"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12" style="width: 15%;">
                                    servicing To Date:
                                <asp:TextBox runat="server" ID="txt_end_date_service" CssClass="form-control date-picker4"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    select Warranty:
                                <asp:DropDownList runat="server" ID="ddl_warranty" CssClass="form-control">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Hr">Hr</asp:ListItem>
                                    <asp:ListItem Value="Days">Days</asp:ListItem>
                                    <asp:ListItem Value="Month">Month</asp:ListItem>
                                </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    Warranty(In Hr/Days/Month):
                                <asp:TextBox runat="server" ID="txt_warranty" CssClass="form-control" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    Next Servicing:
                                <asp:TextBox runat="server" ID="txt_nxt_service" CssClass="form-control date-picker5"></asp:TextBox>
                                </div>

                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                    <span>File to Upload :</span>
                                    <asp:FileUpload ID="service_document_file" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                                    <b style="color: #f00; text-align: center">Note :</b> <span style="font-size: 8px; font-weight: bold;">Only JPG, PNG and PDF files will be uploaded.</span>
                                </div>

                                <div class="col-sm-2 col-xs-12" style="display: none">
                                    id:
                                <asp:TextBox runat="server" ID="txt_id" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row text-center">
                                <asp:Button ID="btn_save_service" runat="server" Text="Save" class="btn btn-primary" OnClick="btn_save_service_Click" OnClientClick="return upload_validation();" />
                                <asp:Button ID="btn_update_service" runat="server" Text="Update" OnClick="btn_update_service_Click" class="btn btn-primary" />
                                <asp:Button ID="btn_delete" runat="server" Text="Delete" OnClick="btn_delete_Click" OnClientClick="return confirm('Are you sure you want to delete this event?');" class="btn btn-primary" />
                                <asp:Button ID="btn_close1" runat="server" Text="Close" class="btn btn-danger" OnClick="btn_close1_Click" />
                            </div>
                            <br />
                        </div>
                        <br />
                        <div class="container-fluid">
                            <asp:Panel ID="panel2" runat="server" CssClass="grid-view">
                                <asp:GridView ID="gv_machine_s" class="table" runat="server" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="gv_machine_s_SelectedIndexChanged" OnRowDataBound="gv_machine_s_RowDataBound" OnPreRender="gv_machine_s_PreRender">
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <Columns>

                                        <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
                                        <asp:BoundField DataField="machine_name" HeaderText="machine Name" SortExpression="machine_name" />
                                        <asp:BoundField DataField="servicing_from_date" HeaderText="Servicing Date From" SortExpression="servicing_from_date" />
                                        <asp:BoundField DataField="servicing_to_date" HeaderText="Servicing To Date" SortExpression="servicing_to_date" />
                                        <asp:BoundField DataField="warranty_type" HeaderText="Warranty Type" SortExpression="warranty_type" />
                                        <asp:BoundField DataField="warranty_in" HeaderText="warranty" SortExpression="warranty_in" />
                                        <asp:BoundField DataField="next_servicing_date" HeaderText="Next Service Date" SortExpression="next_servicing_date" />
                                        <asp:TemplateField HeaderText="IMAGE">
                                            <ItemTemplate>
                                                <asp:Image ID="que_4_path" runat="server" Height="50" Width="50" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
                <br />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
