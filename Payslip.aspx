<%@ Page Title="Payslip" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Payslip.aspx.cs" Inherits="Payslip" EnableEventValidation="false" %>




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

    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style>
        .grid-view {
            height: auto;
            max-height: 500px;
            overflow: scroll;
        }
    </style>
    <style>
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

        .text-red {
            color: red;
        }

        .ui-datepicker-calendar {
            display: none;
        }
    </style>
    <script>
        $(document).ready(function () {
            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "1950",
                maxDate: 0,
                showButtonPanel: true,
                dateFormat: 'mm/yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, month, 1));
                }
            });
            $(".date-picker").attr("readonly", "true");

        });
    </script>
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function openWindow() {
            window.open("html/Payslip.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }


        function Req_validation() {

            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

            var ddlunitselect = document.getElementById('<%=ddlunitselect.ClientID %>');
            var Selected_ddlunitselect = ddlunitselect.options[ddlunitselect.selectedIndex].text;

            var select_payslip_date = document.getElementById('<%=select_payslip_date.ClientID %>');




            if (Selected_ddl_client == "Select") {
                alert("Please Select Client Name !!!");
                ddl_client.focus();
                return false;
            }



            if (Selected_ddlunitselect == "ALL") {
                alert("Please Select Branch Name");
                ddlunitselect.focus();
                return false;
            }
            if (select_payslip_date.value == "") {
                alert("Please Select Date");
                select_payslip_date.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }
        function Req_validation10() {

            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

            var state_name = document.getElementById('<%=ddl_state_name.ClientID %>');
             var Selected_state_name = state_name.options[state_name.selectedIndex].text;

             var ddlunitselect = document.getElementById('<%=ddlunitselect.ClientID %>');
            var Selected_ddlunitselect = ddlunitselect.options[ddlunitselect.selectedIndex].text;

            var select_payslip_date = document.getElementById('<%=select_payslip_date.ClientID %>');




            if (Selected_ddl_client == "Select") {
                alert("Please Select Client Name !!!");
                ddl_client.focus();
                return false;
            }
            if (Selected_state_name == "Select") {
                alert("Please Select State Name !!!");
                state_name.focus();
                return false;
            }
            if (select_payslip_date.value == "") {
                alert("Please Select Date");
                select_payslip_date.focus();
                return false;
            }



        }
        window.onfocus = function () {
            $.unblockUI();
        }
        function pageLoad() {
            $('#<%=btn_send_email.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

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
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>PaySlip</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
           <%-- <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>PaySlip Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>


            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">

                <div class="row">
                      <div class="col-sm-2 col-xs-12">
                       <b> Select Date :</b><span class="text-red">*</span>
                        <asp:TextBox ID="select_payslip_date" runat="server" class="form-control date-picker"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Client Name:</b><span class="text-red">*</span>
                        <asp:DropDownList ID="ddl_client" DataValueField="client_code" DataTextField="client_name" Width="100%" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> State Name:</b><span class="text-red">*</span>
                        <asp:DropDownList ID="ddl_state_name" Width="100%" OnSelectedIndexChanged="ddl_state_name_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12 ">
                       <b> Branch Name :</b><span class="text-red">*</span>
                        <asp:DropDownList ID="ddlunitselect" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlunitselect_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                  




                    <br />
                    <div class="col-sm-4 col-xs-12">

                        <asp:Button ID="btnshow" runat="server" class="btn btn-primary" Text="Salary Slip" Width="30%"
                            OnClick="btnshow_Click" OnClientClick="return Req_validation10();" />
                        <asp:Button ID="btnclose" runat="server" class="btn btn-danger"
                            OnClick="btnclose_Click" Text="Close" />
                        <asp:Button ID="btn_bank" runat="server" class="btn btn-primary" Visible="false" Width="30%"
                            OnClick="btn_bank_Click" Text="Bank Excel" />
                        <asp:Button ID="btn_form_16" runat="server" CssClass="btn btn-primary" Visible="false" Width="30%"
                            OnClick="btn_form_16_Click" Text="Form 16" />
                        <asp:Button ID="btn_count_emp" runat="server" CssClass="btn btn-primary" Width="50%"
                            OnClick="Button1_Click" Text="UAN/PF/ESIC Painding " />

                    </div>
                    <%-- vikas add /05/03/2019--%>
                    <div class="col-sm-1 col-xs-12"></div>
                    <%-- <asp:Panel ID="panel_link" runat="server">
                        <div class="col-sm-3 col-sm-12">
                            <a data-toggle="modal" href="#remain_admin"><font color="red"><b><%= rem_emp_count %></b></font>Employee Name </a>
                                                     
                    </div>
                        </asp:Panel>--%>
                    <%-- end--%>
                    <div class="col-sm-2 col-xs-12 " style="display: none">
                        <asp:Label ID="lbl_month" runat="server" Text="Month :"></asp:Label>
                        <asp:DropDownList ID="ddl_currmon" runat="server" class="form-control">
                            <asp:ListItem Value="00">Select Month :</asp:ListItem>
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
                        </asp:DropDownList>
                        <%--</span>--%>
                    </div>



                    <div class="col-sm-2 col-xs-12" style="display: none">
                        Year :
         
                        <asp:TextBox ID="txtcurrentyr" runat="server" MaxLength="4" class="form-control" OnTextChanged="ddlunitselect_SelectedIndexChanged"></asp:TextBox>
                    </div>

                </div>

                <br />
                    <br />
                    <br />

                <div class="row">
                    <div class="col-sm-2 col-xs-12">
                        <asp:Label ID="lbl_employee" runat="server" Font-Bold="true" Text="Select Employee :"></asp:Label>
                        <asp:ListBox ID="ddl_employee" runat="server" class="form-control" SelectionMode="Multiple"></asp:ListBox>
                    </div>
                    <br />
                    <div class="col-sm-4 col-xs-12">
                        <asp:Button ID="btn_send_email" runat="server" CssClass="btn btn-primary" Width="40%" OnClick="btn_send_email_Click" Text="Send e-mail" Style="margin-top: 16px" />
                    </div>
                </div>
                <div class="col-sm-2 col-xs-12" style="margin-left: -14px">
                    <b>Note : Email will be send to employees having valid email ids in the system.</b>
                </div>
                <%--vikas--%>
                <div class="modal fade" id="remain_admin" role="dialog" data-dismiss="modal" href="#lost">

                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 style="text-align: center">Employee Name </h4>
                            </div>

                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-12" style="padding-left: 1%;">
                                        <asp:GridView ID="gv_rem_emp_count" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                            <Columns>
                                                <asp:BoundField DataField="emp_code" HeaderText="Emp Code" SortExpression="emp_code" />
                                                <asp:BoundField DataField="emp_name" HeaderText="Emp Name" SortExpression="emp_name" />


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div class="modal-footer">
                                <div class="row text-center">
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-4">
                                        <button type="button" class="btn btn-danger" data-dismiss="modal">CLOSE</button>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
                </div>
        </asp:Panel>
    </div>
</asp:Content>

