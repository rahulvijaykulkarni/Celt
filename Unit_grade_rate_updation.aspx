<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Unit_grade_rate_updation.aspx.cs" Inherits="Unit_grade_rate_updation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Unit Designation Rate Updation</title>
    <style type="text/css">
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
        $(document).ready(function () {


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

            $.fn.dataTable.ext.errMode = 'none';

        });
    </script>

    <script type="text/javascript">

        function pageLoad() {

            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });
        }
        function openWindow() {
            window.open("html/unit_Grade_rate_updation.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }



        function Req_validation() {

            var txt_ucode = document.getElementById('<%=ddl_unitcode.ClientID %>');
            var Selected_txt_ucode = txt_ucode.options[txt_ucode.selectedIndex].text;


            if (Selected_txt_ucode == "Select") {
                alert("Please Select Unit ");
                txt_ucode.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }
        $(function () {
            $('#<%=ddl_unitcode.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

            $('#<%=ddl_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
        });
    </script>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="container-fluid">

        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>--%>
        <asp:Panel ID="Panel2" runat="server" CssClass="panel panel-primary">

            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: small;"><b>UNIT GRADE RATE UPDATION</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 15px;">
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Unit Grade Rate Updation Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid" style="border: 1px solid #ddd9d9; background: #f3f1fe; border-radius: 10px;margin-bottom:20px; margin-top:20px; padding:15px 15px 15px 15px;">
                    <br />
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                            <b>Client :</b><span style="color: red">*</span>

                            <asp:DropDownList ID="ddl_client" DataValueField="client_code" DataTextField="client_name" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <asp:Panel ID="unit_panel" runat="server">
                            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                              <b>  Unit :</b><span style="color: red">*</span>

                                <asp:DropDownList ID="ddl_unitcode" DataValueField="unit_code" DataTextField="unit_name" OnSelectedIndexChanged="ddl_unitcode_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                             <b>   State :</b><span style="color: red">*</span>

                                <asp:TextBox ID="txt_state" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                               <b> City :</b><span style="color: red">*</span>

                                <asp:TextBox ID="txt_city" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                               <b> Location :</b><span style="color: red">*</span>

                                <asp:TextBox ID="txt_location" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </asp:Panel>
                    </div>
                    <br />
                    <br />
                    <div class="row text-center">
                        <%-- &nbsp;<asp:Button ID="btn_process" runat="server" CssClass="btn btn-primary"
                        OnClick="btn_process_Click" Text="Show Details" OnClientClick="return Req_validation();" />--%>

                        <%-- &nbsp&nbsp <asp:Button ID="btn_updateemprate" runat="server" CssClass="btn btn-primary" 
                                onclick="btn_updateemprate_Click" Text="Update Employee Rate" />--%>
                            &nbsp;<asp:Button ID="btn_save" runat="server" CssClass="btn btn-primary"
                                OnClick="btn_save_Click" Text="Save" OnClientClick="return Req_validation();" />
                        &nbsp;&nbsp;<asp:Button ID="bntclose" runat="server" CssClass="btn btn-danger"
                            OnClick="bntclose_Click" Text="Close" />
                    </div>
                    <br />
                    <asp:Panel ID="Panel1" runat="server" CssClass="grid-view">

                        <asp:GridView ID="gv_fullmonthot" runat="server" CellPadding="4" ForeColor="#333333"
                            AutoGenerateColumns="False" class="table" GridLines="Both" OnPreRender="gv_fullmonthot_PreRender" OnRowDataBound="gv_fullmonthot_RowDataBound">
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
                            <Columns>
                                <asp:TemplateField HeaderText="UNIT NAME" ControlStyle-Width="50px">
                                    <ItemTemplate>
                                        <%# Eval("UNIT_NAME")%>'
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UNIT CODE" ControlStyle-Width="40px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblunitcode" runat="server" Text='<%# Eval("UNIT_CODE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GRADE CODE" ControlStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgradecode" runat="server" Text='<%# Eval("GRADE_CODE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GRADE DESCRIPTION" ControlStyle-Width="50px">
                                    <ItemTemplate>
                                        <%# Eval("grade_desc")%>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BASIC">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD01" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday01" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD01")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DA">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD02" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday02" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD02")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HRA">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD03" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday03" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD03")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WASHING">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD04" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday04" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD04")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CONV">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD05" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday05" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD05")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HEAD06">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD06" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday06" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD06")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HEAD07">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD07" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday07" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD07")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HEAD08">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD08" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday08" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD08")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HEAD09">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD09" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday09" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD09")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HEAD10">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD10" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday10" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD10")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HEAD11">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD11" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday11" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD11")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HEAD12">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD12" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday12" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD12")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="HEAD13">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD13" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday13" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD13")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="HEAD14">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD14" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday14" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD14")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="HEAD15">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_EHEAD15" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday15" runat="server" Width="50px"
                                            Text='<%# Eval("E_HEAD15")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="LHEAD1">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_LHEAD01" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday16" runat="server" Width="50px"
                                            Text='<%# Eval("L_HEAD01")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="LHEAD2">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_LHEAD02" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday17" runat="server" Width="50px"
                                            Text='<%# Eval("L_HEAD02")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="LHEAD3">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_LHEAD03" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday18" runat="server" Width="50px"
                                            Text='<%# Eval("L_HEAD03")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="LHEAD4">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_LHEAD04" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday19" runat="server" Width="50px"
                                            Text='<%# Eval("L_HEAD04")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="LHEAD5">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_LHEAD05" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday20" runat="server" Width="50px"
                                            Text='<%# Eval("L_HEAD05")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="DHEAD1" ControlStyle-Width="50px">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_DHEAD01" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday21" runat="server" Width="50px"
                                            Text='<%# Eval("D_HEAD01")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="DHEAD2">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_DHEAD02" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday22" runat="server" Width="50px"
                                            Text='<%# Eval("D_HEAD02")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DHEAD3">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_DHEAD03" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday23" runat="server" Width="50px"
                                            Text='<%# Eval("D_HEAD03")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DHEAD4">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_DHEAD04" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday24" runat="server" Width="50px"
                                            Text='<%# Eval("D_HEAD04")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DHEAD5">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_DHEAD05" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday25" runat="server" Width="50px"
                                            Text='<%# Eval("D_HEAD05")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DHEAD6">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_DHEAD06" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday26" runat="server" Width="50px"
                                            Text='<%# Eval("D_HEAD06")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DHEAD7">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_DHEAD07" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday27" runat="server" Width="50px"
                                            Text='<%# Eval("D_HEAD07")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DHEAD8">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_DHEAD08" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday28" runat="server" Width="50px"
                                            Text='<%# Eval("D_HEAD08")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DHEAD9">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_DHEAD09" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday29" runat="server" Width="50px"
                                            Text='<%# Eval("D_HEAD09")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DHEAD10">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbl_DHEAD10" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtday30" runat="server" Width="50px"
                                            Text='<%# Eval("D_HEAD10")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </asp:Panel>
                    <br />
                </div>
                <%-- <div class="row">
                <div class="col-sm-4 col-xs-12"></div>
                <div class="col-sm-2 col-xs-12">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </div>
                <div class="col-sm-2 col-xs-12">
                    <asp:Button ID="btn_upload" runat="server" CssClass="btn btn-primary" OnClick="btn_Upload_Click" Text="Upload" />
                    <asp:Button ID="btn_Export" runat="server" CssClass="btn btn-primary" OnClick="btn_Export_CheckedChanged" Text="Export" />
                </div>
            </div>--%>
            </div>
        </asp:Panel>
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>



