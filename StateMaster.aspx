<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StateMaster.aspx.cs" Inherits="StateDetails" Title="STATE MASTER" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>State Master</title>
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

    <%--  <script src="datatable/pdfmake.min.js"></script> --%>
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {
            $('#<%=ddl_tiers.ClientID%>').change(function () {

                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

                });
                $('#<%=ddl_states.ClientID%>').change(function () {

                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

                });
            }
            $(document).ready(function () {
                $('#<%=StateGridView.ClientID%> td').click(function () {
                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                });
                var table = $('#<%=StateGridView.ClientID%>').DataTable({
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
                   .appendTo('#<%=StateGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

                $.fn.dataTable.ext.errMode = 'none';

            });
    </script>
    <style>
        .label_text {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 10px;
        }

        .auto-style1 {
            color: #FFFFFF;
        }

        .grid-view {
            height: auto;
            max-height: 300px;
            width: auto;
            overflow-y: auto;
            overflow-x: hidden;
        }

        h2 {
            border-radius: 5px;
        }



        h5 {
            font-weight: bold;
            font-size: 15px;
        }

        .text-red {
            color: #f00;
        }

        .panel-body {
            overflow-x: hidden;
        }
    </style>

    <script>

        $(function () {
            $('#<%=btn_cancel.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            var evt = null;
            var e = null;

        });

        function AllowAlphabet_Number(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }

        function Req_validation() {
            var t_StateCode = document.getElementById('<%=txt_statecode.ClientID %>');
            var t_state_city = document.getElementById('<%=txt_city.ClientID %>');
            var t_StateName = document.getElementById('<%=txt_statename.ClientID %>');
            if (t_StateCode.value == "") {
                alert("Please Enter State Code");
                t_StateCode.focus();
                return false;
            }

            if (t_state_city.value == "") {
                alert("Please Enter City Name");
                t_state_city.focus();
                return false;
            }

            // State Name

            if (t_StateName.value == "") {
                alert("Please Enter the State Name");
                t_StateName.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }



        function openWindow() {
            window.open("html/StateD.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
        function R_validation() {

            var r = confirm("Are you Sure You Want to Delete Record");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }
        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });

        function validate() {
            var ddl_tiers = document.getElementById('<%=ddl_tiers.ClientID %>');
            var Selected_ddl_tiers = ddl_tiers.options[ddl_tiers.selectedIndex].text;
            if (Selected_ddl_tiers == "Select") {
                alert("Please Select Tiers");
                ddl_tiers.focus();
                return false;
            }
            var lstRight = document.getElementById('<%=lstRight.ClientID %>');

            if (lstRight.value == "") {
                alert("Please Select Assign City");
                lstRight.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function select_city() {
            var lstLeft = document.getElementById('<%=lstLeft.ClientID %>');

            if (lstLeft.value == "") {
                alert("Please Select City Name");
                lstLeft.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function assign_city() {
            var lstRight = document.getElementById('<%=lstRight.ClientID %>');

            if (lstRight.value == "") {
                alert("Please Select Assign City");
                lstRight.focus();
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
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>State Master</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;">
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
                        <div style="text-align: left; color:white; font-size: small;"><b>State Master Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>

            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-bottom:20px; margin-top:20px">
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                           <b> State Code :</b>
                    <span class="text-red">*</span>
                            <asp:TextBox ID="txt_statecode" runat="server" MaxLength="7" class="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-xs-12">
                           <b> City Name :</b><span class="text-red"> *</span>
                            <asp:TextBox ID="txt_city" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event)" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-xs-12">
                          <b>  State Name :</b><span class="text-red"> *</span>
                            <%--<asp:TextBox ID="txt_statename" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet(event)" MaxLength="50"></asp:TextBox>--%>
                            <asp:DropDownList ID="txt_statename" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet(event)" MaxLength="50"></asp:DropDownList>
                        </div>
                    </div>
                    <br />
                </div>
                <br />
                
                <div id="tabs" style="background: #f3f1fe; padding:20px 20px 20px 20px; border-radius:10px">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li><a href="#menu1" runat="server"><b>State</b></a></li>
                        <li><a href="#menu2" runat="server"><b>Tier Cities</b></a></li>
                    </ul>
                    <div id="menu1">
                        <br />


                        <asp:Panel ID="Panel2" runat="server" CssClass="grid-view " class="panel-body">
                            <asp:GridView ID="StateGridView" class="table" runat="server" Font-Size="X-Small" OnPreRender="StateGridView_PreRender"
                                RowStyle-Width="100" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="StateGridView_SelectedIndexChanged"
                                OnRowDataBound="StateGridView_RowDataBound">
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField DataField="STATE_CODE" HeaderText="State Code"
                                        SortExpression="STATE_CODE" />
                                    <asp:BoundField DataField="city" HeaderText="City Name"
                                        SortExpression="city" />
                                    <asp:BoundField DataField="STATE_NAME" HeaderText="State Name"
                                        SortExpression="STATE_NAME" />
                                </Columns>
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </asp:Panel>

                        <br />

                        <div class="row text-center">
                            <asp:Button ID="btn_add" runat="server" class="btn btn-primary" OnClick="btn_add_Click"
                                Text=" Save " OnClientClick=" return Req_validation();" />

                            <asp:Button ID="btn_edit" runat="server" class="btn btn-primary" Text=" Update "
                                OnClick="btn_edit_Click" OnClientClick=" return Req_validation();" />

                            <asp:Button ID="btn_delete" runat="server" class="btn btn-primary" Text=" Delete "
                                OnClick="btn_delete_Click" OnClientClick=" return R_validation();" />


                            <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" Text=" Clear "
                                OnClick="btn_cancel_Click" />

                            <asp:Button ID="btnexporttoexcelstate" runat="server" class="btn btn-primary" Width="10%"
                                Text="Export To Excel" OnClick="btnexporttoexcelstate_Click" />

                            <asp:Button ID="btnclose" runat="server" class="btn btn-danger"
                                Text="Close" OnClick="btnclose_Click" />
                        </div>
                        <div class="row">
                            <asp:GridView ID="GridView1" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                                runat="server" AutoGenerateColumns="false" OnDataBound="OnDataBound">
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="menu2">
                        <br />

                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-2 col-sm-3 col-xs-12">
                                       <b> Tiers :</b>
                                 <asp:DropDownList AppendDataBoundItems="true" ID="ddl_tiers" runat="server" class="form-control text_box" OnSelectedIndexChanged="ddl_tiers_SelectedIndexChanged" AutoPostBack="true">
                                     <asp:ListItem Value="Select">Select</asp:ListItem>
                                     <asp:ListItem Value="1">First Tier</asp:ListItem>
                                     <asp:ListItem Value="2">Second Tier</asp:ListItem>
                                     <asp:ListItem Value="3">Third Tier</asp:ListItem>
                                     <asp:ListItem Value="4">Fourth Tier</asp:ListItem>
                                 </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-3 col-xs-12">
                                       <b> State Name :</b>
                                 <asp:DropDownList AppendDataBoundItems="true" ID="ddl_states" runat="server" class="form-control text_box" OnSelectedIndexChanged="ddl_states_SelectedIndexChanged" AutoPostBack="true">
                                 </asp:DropDownList>
                                    </div>
                                </div>
                                <br />

                                <asp:Panel ID="employeepanel" runat="server">
                                    <div class="row" style="margin-left: 8em;">
                                        <div class="col-sm-1 col-xs-12 "></div>
                                        <div class="col-sm-3 col-xs-12 ">
                                           <b> City Name:</b>
                                    <asp:ListBox ID="lstLeft" runat="server" SelectionMode="Multiple" Width="100%" Visible="true" Height="300"></asp:ListBox>

                                        </div>
                                        <div class="col-sm-1 col-xs-12 "></div>
                                        <div class="col-sm-2 col-xs-12 ">
                                            <br />
                                            <br />
                                            <div class="row">
                                                <asp:Button ID="brnallleft" value=">>" OnClick="brnallleft_Click" runat="server" Class="btn btn-primary" Style="padding-top: 11px; padding-right: 9px; margin-left: 3em;" Text=">>" OnClientClick="return select_city();" />
                                            </div>
                                            <br />
                                            <div class="row">
                                                <asp:Button ID="btnright" value="<" OnClick="btnright_click" runat="server" Class="btn btn-primary" Text=">" Style="padding-left: 16px; padding-top: 10px; margin-left: 3em;" OnClientClick="return select_city();" />
                                            </div>
                                            <br />
                                            <div class="row">
                                                <asp:Button ID="btnleft" value="<" OnClick="btnleft_click" runat="server" Class="btn btn-primary" Text="<" Style="padding-left: 16px; padding-top: 10px; margin-left: 3em;" OnClientClick="return assign_city();" />
                                            </div>
                                            <br />
                                            <div class="row">
                                                <asp:Button ID="allriht" value="<<" OnClick="allriht_click" runat="server" Class="btn btn-primary" Text="<<" Style="padding-top: 11px; padding-right: 9px; margin-left: 3em;" OnClientClick="return assign_city();" />
                                            </div>
                                            <br />

                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                           <b> Assign City:</b>
                                    <asp:ListBox ID="lstRight" runat="server" SelectionMode="Multiple" Width="100%" Height="300"></asp:ListBox>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <br />
                                <div class="row text-center">
                                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_click" OnClientClick="return validate();" Class="btn btn-primary" />
                                    <asp:Button ID="Button1" Text="Close" runat="server" class="btn btn-danger" OnClick="btnclose_click" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

        </asp:Panel>

    </div>

</asp:Content>

