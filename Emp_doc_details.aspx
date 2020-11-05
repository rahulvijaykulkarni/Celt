<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Emp_doc_details.aspx.cs" Inherits="Emp_doc_details" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Employee Document Details</title>
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: small;"><b>Employee Document Details</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>

            <br />
            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-sm-1 col-xs-12"></div>
                        <%--<div class="col-sm-2 col-xs-12">
                            Type : 
                            <asp:DropDownList ID="ddl_type" runat="server" CssClass="form-control" >
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                 <asp:ListItem Value="ID_Card">ID_Card</asp:ListItem>
                                 <asp:ListItem Value="Uniform">Uniform</asp:ListItem>
                                 <asp:ListItem Value="Shoes">Shoes</asp:ListItem>
                            </asp:DropDownList>
                        </div>--%>
                        <div class="col-sm-2 col-xs-12">
                            Client Name : 
                            <asp:DropDownList ID="ddl_unit_client" runat="server" CssClass="form-control" OnSelectedIndexChanged="get_city_list1" AutoPostBack="true">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            State :
                            <asp:DropDownList ID="ddl_clientwisestate" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            Branch Name : 
                            <asp:DropDownList ID="ddl_unitcode" runat="server" class="form-control">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            Type : 
                            <asp:DropDownList ID="ddl_type" runat="server" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="ID_Card">ID_Card</asp:ListItem>
                                <asp:ListItem Value="Uniform">Uniform</asp:ListItem>
                                <asp:ListItem Value="Shoes">Shoes</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
            <div class="row text-center">
                <asp:Button ID="btn_add" runat="server" class="btn btn-primary" Text=" View " OnClick="btn_rem_documents_Click" />

                <asp:Button ID="btn_close" runat="server" class="btn btn-danger" Text=" close " OnClick="btn_close_click" />
            </div>
            <br />
        </asp:Panel>
    </div>
</asp:Content>
