<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="reliance_image_upload.aspx.cs" Inherits="reliance_image_upload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Working Images Upload</title>
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

    <style>
        .grid-view {
            overflow-x: hidden;
            overflow-y: hidden;
            font-family: Verdana;
        }
    </style>

    <script>
        function pageLoad() {
            var table = $('#<%=gv_image_upload.ClientID%>').DataTable({
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
               .appendTo('#<%=gv_image_upload.ClientID%>_wrapper .col-sm-6:eq(0)');
            $.fn.dataTable.ext.errMode = 'none';
        }

        function openWindow() {
            window.open("html/reliance_image_upload.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel1" runat="server" class="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-lg-11 col-md-11 col-sm-11 col-xs-12">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Image Upload</b> </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-12 text-right">
                        <asp:LinkButton ID="panImgLnkBtn" runat="server" OnClientClick="openWindow();return false;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Image Upload Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-bottom:20px; margin-top:20px">
                <div class="row">
                    <div class="col-md-3 col-xs-12">
                       <b>Client :</b> 
                         <asp:Label ID="Label9" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        <asp:DropDownList ID="ddl_client" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" class="form-control" MaxLength="20">
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-3 col-xs-12">
                       <b> State :</b> <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        <asp:DropDownList ID="ddl_state" runat="server" class="form-control" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true" MaxLength="20">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3 col-xs-12">
                       <b> Email IDs :</b> <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        <asp:TextBox ID="txt_email_id" runat="server" class="form-control" />
                        <%--<asp:TextBox ID="txt_email_id" runat="server" class="form-control" Text="vinod@celtsoft.com" />--%>
                    </div>
                </div>
                <br />
                <div class="container-fluid">
                    <asp:Panel ID="Panel2" runat="server" CssClass="grid-view">
                        <asp:GridView ID="gv_image_upload" class="table" runat="server" OnRowDataBound="gv_image_upload_RowDataBound" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" OnPreRender="gv_image_upload_PreRender">
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <Columns>
                                <asp:BoundField DataField="unit_code" HeaderText="ID" SortExpression="unit_code" />
                                <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                <asp:TemplateField HeaderText="Reception">
                                    <ItemTemplate>
                                        <asp:FileUpload ID="image_1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Washroom">
                                    <ItemTemplate>
                                        <asp:FileUpload ID="image_2" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pantry">
                                    <ItemTemplate>
                                        <asp:FileUpload ID="image_3" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Common Area1">
                                    <ItemTemplate>
                                        <asp:FileUpload ID="image_4" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Common Area2">
                                    <ItemTemplate>
                                        <asp:FileUpload ID="image_5" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                    </asp:Panel>
                </div>

            </div>
                <br />
            <div class="row text-center">
                <asp:Button ID="btn_save" runat="server" class="btn btn-primary" OnClick="btn_save_Click" Text=" Upload " />
                <asp:Button ID="btn_send_mail" runat="server" class="btn btn-primary" OnClick="btn_send_mail_Click" Text=" Send Email " />
                <asp:Button ID="btn_report" runat="server" class="btn btn-primary" OnClick="btn_report_Click" Text=" Report " />
                <asp:Button ID="btnclose" runat="server" class="btn btn-danger" Text="Close" OnClick="btnclose_Click" />
            </div>
            <br />
                <br />
        </asp:Panel>
    </div>
</asp:Content>
