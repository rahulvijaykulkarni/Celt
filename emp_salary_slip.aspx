<%@ Page Title="Employee PaySlip" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="emp_salary_slip.aspx.cs" Inherits="Payslip" EnableEventValidation="false" %>




<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


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
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.blockUI.js"></script>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <link href="Scripts/jquery-ui.css" rel="stylesheet" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <style>
        .container {
            max-width: 99%;
        }

        .grid-view {
            height: auto;
            max-height: 500px;
            overflow: scroll;
        }
    </style>
    <style>
        .container {
            max-width: 99%;
        }

        .label_text {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 10px;
        }

        .text_box {
            margin-top: 7px;
        }

        .auto-style1 {
            color: #FFFFFF;
        }

        h2 {
            border-radius: 5px;
        }

        .grid-view {
            height: auto;
            max-height: 300px;
            overflow: scroll;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }

        .ui-datepicker-calendar {
            display: none;
        }
    </style>
    <style type="text/css">
        .style1 {
        }

        .style2 {
            width: 70px;
        }

        .tab-section {
            background-color: #fff;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $('.date-picker').datepicker({
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'yy',
                maxDate: 0,
                onClose: function (dateText, inst) {
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, 1));
                }
            });

            $(".date-picker").attr("readonly", "true");

        }
        function openWindow() {
            window.open("html/Payslip.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
        function isNumber(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {

                    return false;
                }

            }
            return true;
        }

        function Req_validation() {

            var tcurrentyr = document.getElementById('<%=txtcurrentyr.ClientID %>');

            var l_currmon = document.getElementById('<%=ddl_currmon.ClientID %>');
            var SelectedText1 = l_currmon.options[l_currmon.selectedIndex].text;

            //Unit Code 

            if (SelectedText1 == "Select Month") {
                alert("Please Select Month !!!");
                l_currmon.focus();
                return false;
            }

            // MONTH / YEAR

            if (tcurrentyr.value == "") {
                alert("Please Enter Year");
                tcurrentyr.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        window.onfocus = function () {
            $.unblockUI();

        }
        function openWindow() {
            window.open("html/emp_salary_slip.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="UpdatePanel1" runat="server"></asp:Panel>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Employee PaySlip</b></div>
                    </div>
                    <div class="col-sm-2 text-right">

                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Employee PaySlip Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>


            <div class="panel-body">
               <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">
                    <div class="row">

                        <div class="col-sm-1 col-xs-12"></div>
                        <div class="col-sm-1 col-xs-12 text-right" style="margin-top: 7px;">
                            <asp:Label ID="lbl_month" runat="server" Font-Bold="true" Text="Month :"></asp:Label>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <asp:DropDownList ID="ddl_currmon" runat="server" class="form-control">
                                <asp:ListItem Value="Select Month">Select Month</asp:ListItem>
                                <asp:ListItem Value="01">JAN</asp:ListItem>
                                <asp:ListItem Value="02">FEB</asp:ListItem>
                                <asp:ListItem Value="03">MAR</asp:ListItem>
                                <asp:ListItem Value="04">APR</asp:ListItem>
                                <asp:ListItem Value="05">MAY</asp:ListItem>
                                <asp:ListItem Value="06">JUN</asp:ListItem>
                                <asp:ListItem Value="07">JUL</asp:ListItem>
                                <asp:ListItem Value="08">AUG</asp:ListItem>
                                <asp:ListItem Value="09">SEP</asp:ListItem>
                                <asp:ListItem Value="10">OCT</asp:ListItem>
                                <asp:ListItem Value="11">NOV</asp:ListItem>
                                <asp:ListItem Value="12">DEC</asp:ListItem>
                            </asp:DropDownList><br />
                            <%--</span>--%>
                        </div>
                        <div class="col-sm-1 col-xs-12 text-right" style="margin-top: 7px;"><b>Year :</b></div>
                        <div class="col-sm-2 col-xs-12">
                            <asp:TextBox ID="txtcurrentyr" runat="server" MaxLength="4" class="form-control date-picker" onkeypress="return isNumber(event)"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-xs-12">
                            <asp:Button ID="btnshow" runat="server" class="btn btn-large" Text="Salary Slip"
                                OnClick="btnshow_Click" OnClientClick="return Req_validation();" />
                            <asp:Button ID="btnclose" runat="server" class="btn btn-danger"
                                OnClick="btnclose_Click" Text="Close" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                            <asp:Label ID="Label2" runat="server" Text="Payslip of Emp Code :"></asp:Label>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <asp:TextBox ID="txtfrmempcode" runat="server" MaxLength="6" class="form-control"></asp:TextBox>
                            <%--</span>--%>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <asp:Label ID="Label3" runat="server" Text="Payslip To Emp Code :"></asp:Label>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <asp:TextBox ID="txttoempcode" runat="server" MaxLength="6" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-1 col-xs-12"></div>

                    </div>
                    <br />
                    <div class="col-sm-4"></div>
                </div>
            </div>
            <%-- <asp:Panel runat="server" Height="800px"  ID="Panel2" ScrollBars="Both">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
            EnableParameterPrompt="False" HasRefreshButton="True" PageZoomFactor="75"
            ToolPanelView="None"  />
    
        </asp:Panel>--%>
        </asp:Panel>
    </div>
    <%-- <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
            EnableParameterPrompt="False" HasRefreshButton="True" 
            ToolPanelView="None"  />--%>
    <%--</div>--%>


    <%--</div>--%>
</asp:Content>

