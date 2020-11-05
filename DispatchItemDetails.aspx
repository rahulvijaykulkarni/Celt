<%@ Page Title="Inventory Status" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DispatchItemDetails.aspx.cs" Inherits="DispatchItemDetails " EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <%-- <title>Inventory Status</title>--%>
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
    <script src="js/select2.min.js"></script>
    <link href="css/select2.min.css" rel="stylesheet" />
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>

    <script type="text/javascript">
      
    </script>


    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .container {
            font-family: Verdana;
            font-size: 10px;
            font-weight: lighter;
        }

        .popUpStyle {
            font: normal 11px auto "Trebuchet MS", Verdana;
            background-color: #ffffff;
            color: #4f6b72;
            padding: 6px;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }

        .grid-view {
            height: auto;
            max-height: 500px;
            overflow-x: hidden;
            overflow-y: auto;
        }
    </style>

    <script>


        function openWindow() {
            window.open("html/ItemInventoryquery.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>


            <div class="container-fluid">


                <asp:Panel ID="Panel1" runat="server" class="panel panel-primary">


                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-lg-11 col-md-11 col-sm-11 col-xs-12">
                                <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Dispatch Item Details </b></div>
                            </div>
                            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-12 text-right">

                                <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;">
                                    <asp:Image runat="server" ID="Image2" Width="20" Height="20" ImageUrl="Images/help_ico.png" />
                                </asp:LinkButton>
                            </div>
                        </div>

                    </div>
                    <br />
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                                Select Client :
                        <asp:DropDownList ID="ddlunitclient1" runat="server" DataValueField="client_Code" DataTextField="client_name" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlunitclient1_SelectedIndexChanged" />
                            </div>

                            <div class="col-sm-2 col-xs-12">
                                Select State :
                        <asp:DropDownList ID="ddl_gv_statewise" runat="server" DataValueField="STATE_CODE" DataTextField="STATE_NAME" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlsatewises_SelectedIndexChanged" />
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                Select Branch :
                        <asp:DropDownList ID="ddl_gv_branchwise" runat="server" DataValueField="UNIT_CODE" DataTextField="UNIT_NAME" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlbeabchwise1_SelectedIndexChanged" />
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                Select Type :
                            <asp:DropDownList ID="ddl_product_type" runat="server" class="form-control text-box" OnSelectedIndexChanged="ddl_product_type_SelectedIndexedChanged" AutoPostBack="true">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Uniform</asp:ListItem>
                                <asp:ListItem Value="2">Shoes</asp:ListItem>
                                <asp:ListItem Value="3">Sweater</asp:ListItem>
                                <asp:ListItem Value="4">ID_Card</asp:ListItem>
                                <asp:ListItem Value="5">Raincoat </asp:ListItem>
                                <asp:ListItem Value="6">Torch</asp:ListItem>
                                <asp:ListItem Value="7">Whistle</asp:ListItem>
                                <asp:ListItem Value="8">Baton</asp:ListItem>
                                <asp:ListItem Value="9">Belt</asp:ListItem>



                            </asp:DropDownList>
                            </div>
                        </div>


                        <br />
                        <asp:Panel ID="Panel9" runat="server" ScrollBars="Auto">
                            <asp:GridView ID="gv_itemdetails" CssClass="table table-striped table-hover" runat="server" BackColor="White" Font-Size="X-Small"
                                BorderColor="#CCCCCC" BorderStyle="None" OnPreRender="gv_itemdetails_PreRender" BorderWidth="1px" CellPadding="3">
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                        </asp:Panel>

                        <br />
                        <div class="row text-center">

                            <asp:Button ID="btn_Close" runat="server" class="btn btn-danger"
                                OnClick="btn_Close_Click" Text="Close" Style="font-size: 10px" />
                        </div>
                    </div>

                    <br />



                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

