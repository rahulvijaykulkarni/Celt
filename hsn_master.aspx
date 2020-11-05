<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hsn_master.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="hsn_master" Title="HSN Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
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
    <script src="Scripts/sweetalert.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" type="text/css" />
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
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {
            $('#<%=Grid_hsn_rate.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=Grid_hsn_rate.ClientID%>').DataTable({
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
               .appendTo('#<%=Grid_hsn_rate.ClientID%>_wrapper .col-sm-6:eq(0)');
        }

        function validation() {
            var txt_hsn_code = document.getElementById('<%=txt_hsn_code.ClientID %>');
            var ddl_gst_rate = document.getElementById('<%=ddl_gst_rate.ClientID %>');
            var Selected_ddl_gst_rate = ddl_gst_rate.options[ddl_gst_rate.selectedIndex].text;
            if (txt_hsn_code.value == "") {
                alert("Please Enter HSN Code");
                txt_hsn_code.focus();
                return false;
            }
            if (Selected_ddl_gst_rate == "Select") {
                alert("Please Select GST Rate");
                ddl_gst_rate.focus();
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
        function del() {
            var x = confirm('Are you sure,You want to delete Record?');
            if (x) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }
            else {
                return false;
            }

        }
        function upd() {
            var x = confirm('Are you sure,You want to update Record?');
            if (x) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }
            else {
                return false;
            }

        }

        function openWindow() {
            window.open("html/hsn_master.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="panel panel-primary" style="background-color: white;">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: 16px;" class="text-center text-uppercase"><b>HSN Master</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
             <br />
             <%--<div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>HSN Master Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>
            <div class="panel-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-left:15px; margin-right:15px; margin-bottom:20px; margin-top:20px">
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                    <b>HSN Code:</b><span style="color: red">*</span>
                                    <asp:TextBox ID="txt_hsn_code" CssClass="form-control" runat="server" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                </div>

                                <div class="col-sm-2 col-xs-12">
                                   <b> GST Rate :</b><span style="color: red">*</span>
                                    <asp:DropDownList ID="ddl_gst_rate" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                  <b>  HSN Category :</b>
                            <asp:TextBox ID="txt_hsn_category" CssClass="form-control" runat="server" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-3 col-xs-12" style="margin-top: 17px;">
                                    <asp:Button ID="btn_save" runat="server" CssClass="btn btn-primary"
                                        Text="Save" OnClick="btn_save_Click" OnClientClick="return validation();" />
                                    <asp:Button ID="btn_update" runat="server" CssClass="btn btn-primary"
                                        Text="Update" OnClientClick="return upd();" OnClick="btn_update_click" />

                                    <asp:Button ID="btn_delete" runat="server" CssClass="btn btn-primary" OnClick="btn_delete_click"
                                        Text="Delete" OnClientClick="return del();" />

                                    <asp:Button ID="bnt_close" runat="server" CssClass="btn btn-danger" OnClick="btn_close_click"
                                        Text="Close" />
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <asp:TextBox ID="txt_id" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                </div>


                            </div>
                            <br />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <br />
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-left:15px; margin-right:15px; margin-bottom:20px; margin-top:20px">
                    <asp:Panel ID="Panel2" runat="server" CssClass="grid-view">

                        <asp:GridView ID="Grid_hsn_rate" class="table" runat="server"
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#000"
                            BorderStyle="None" BorderWidth="1px" CellPadding="1" OnSelectedIndexChanged="fill_gridview_OnSelectedIndexChanged" OnRowDataBound="Grid_hsn_rate_RowDataBound"
                            Width="100%" OnPreRender="Grid_hsn_rate_PreRender">
                            <RowStyle ForeColor="#000066" />
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                                <asp:BoundField DataField="hsn_code" HeaderText="HSN CODE" SortExpression="hsn_code" />
                                <asp:BoundField DataField="gst_rate" HeaderText="GST RATE" SortExpression="gst_rate" />
                                <asp:BoundField DataField="hsn_category" HeaderText="HSN CATEGORY" SortExpression="hsn_category" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </div>
        </div>
</asp:Content>

