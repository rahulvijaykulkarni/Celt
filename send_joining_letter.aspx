<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="send_joining_letter.aspx.cs" Inherits="Employee_salary_details" Title="Employee salary details" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Client Emails</title>
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
    <link href="css/style.css" rel="stylesheet" />
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
        function pageLoad() {
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=grd_feedback.ClientID%>').DataTable({
                scrollY: "310px",
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
               .appendTo('#<%=grd_feedback.ClientID%>_wrapper .col-sm-6:eq(0)');
              $.fn.dataTable.ext.errMode = 'none';
              var table = $('#<%=gv_itemslist.ClientID%>').DataTable({
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
               .appendTo('#<%=gv_itemslist.ClientID%>_wrapper .col-sm-6:eq(0)');
            $('#<%=ddl_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                maxDate: 0,
                yearRange: "1990:+100",
                showButtonPanel: true,
                dateFormat: 'mm/yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, month, 1));
                }
            });
            $('.date-picker').focus(function () {
                $(".ui-datepicker-calendar").hide();

            });
            $(".date-picker").attr("readonly", "true");

            $(document).ready(function () {
                var st = $(this).find("input[id*='hidtab']").val();
                if (st == null)
                    st = 0;
                $('[id$=tabs]').tabs({ selected: st });
            });

        }
        function unblock()
        { $.unblockUI(); }

        function Req_validation() {
            var t_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selectedclient = t_client.options[t_client.selectedIndex].text;

            if (Selectedclient == "Select") {
                alert("Please Select Client.");
                t_client.focus();
                return false;
            }
            if (R_validation1() == false) { return false; }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function R_validation1() {

            var r = confirm("Are you sure you want send email?");
            if (r == true) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }
            return false;
        }
        function Req_validation1() {
            var t_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selectedclient = t_client.options[t_client.selectedIndex].text;

            if (Selectedclient == "Select") {
                alert("Please Select Client.");
                t_client.focus();
                return false;
            }
            var txt_monthyear = document.getElementById('<%=txt_monthyear.ClientID %>');
            if (txt_monthyear.value == "") {
                alert("Please Select Month/Year.");
                txt_monthyear.focus();
                return false;
            }
            if (R_validation1() == false) { return false; }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function valid_email() {
            var t_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selectedclient = t_client.options[t_client.selectedIndex].text;

            if (Selectedclient == "Select") {
                alert("Please Select Client.");
                t_client.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function openWindow() {
            window.open("html/send_joining_letter.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

    </script>
    <style>
        .grid-view {
            max-height: 300px;
            overflow-x: hidden;
            overflow-y: auto;
        }
    </style>
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
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Empolyee Joining Letter / Client Feedback Email</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Empolyee Joining Letter / Client Feedback Email Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">
              
               <%-- <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                        <div class="row">
                            <div class="col-sm-2 col-xs-12 text-left">
                                <b>Client Name :</b><span style="color: red ;" >*</span>
                                <asp:DropDownList ID="ddl_client" class="form-control pr_state js-example-basic-single" runat="server" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12 text-left">
                               <b> State Name : </b>  
                 <asp:DropDownList ID="ddl_state" runat="server" DataTextField="STATE" DataValueField="STATE" class="form-control text_box" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true">
                 </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12 text-left">
                               <b> Branch Name :   </b>
                <asp:DropDownList ID="ddl_unitcode" class="form-control pr_state js-example-basic-single" runat="server" OnSelectedIndexChanged="ddl_unitcode_SelectedIndexChanged" AutoPostBack="true" />
                            </div>

                        </div>
                    </div>
                   <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
                <br />
             
                <div class="container-fluid">
                <div id="tabs" style="background: #f3f1fe; padding:25px 25px 25px 25px; margin-left:-10px;margin-right:-10px; border-radius:10px;border: 1px solid #e2e2dd ">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li class="Joining_Letter"><a href="#menu1"><b>Joining Letter</b></a></li>
                        <li class="Feedback_Letter"><a href="#menu2"><b>Feedback Letter</b></a></li>
                        <li class="Relieving_Letter"><a href="#menu3"><b>Relieving Letter</b></a></li>
                        
                    </ul>

                    <div id="menu1" class="Joining_Letter">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">

                                    <div class="col-sm-4 col-xs-12">
                                        <br />
                                        <asp:Button ID="btn_send_email" runat="server" class="btn btn-primary" OnClick="btn_send_email_Click" Text="Send Joining Letter" OnClientClick="return Req_validation();" />

                                        <asp:Button ID="btn_emails_not_sent" runat="server" CssClass="btn btn-primary" Text="Report" OnClick="btn_emails_not_sent_Click" OnClientClick="return valid_email();" />
                                    </div>
                                </div>
                                <br />
                                <div class="container-fluid">
                                    <asp:Panel ID="Panel5" runat="server">
                                        <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            OnRowDataBound="gv_itemslist_RowDataBound" AutoGenerateColumns="False" Width="100%" OnPreRender="gv_itemslist_PreRender">
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="client_code" HeaderText="CLIENT" SortExpression="client_code" />
                                                <asp:BoundField DataField="state_name" HeaderText="STATE" SortExpression="ESIC_ADDRESS" />
                                                <asp:BoundField DataField="unit_name" HeaderText="BRANCH" SortExpression="unit_name" />
                                                <asp:BoundField DataField="emp_name" HeaderText="EMPLOYEE" SortExpression="emp_name" />
                                                <asp:BoundField DataField="joining_letter_email" HeaderText="LETTER SENT" SortExpression="joining_letter_email" />
                                                <asp:BoundField DataField="joining_letter_sent_date" HeaderText="SENT DATE" SortExpression="joining_letter_sent_date" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div id="menu2" class="Feedback_Letter">
                        <br />
                        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">--%>
                           <%-- <ContentTemplate>--%>
                                <div class="row">
                                    <div class="col-sm-2 col-xs-12 text-left">
                                        <b>Feedback Month :   </b>
                <asp:TextBox ID="txt_monthyear" runat="server" CssClass="form-control date-picker" />
                                    </div>
                                    <div class="col-sm-2 col-xs-12" style="margin-top: 1.5em">
                                        <asp:Button ID="btn_send_feedback_link" runat="server" class="btn btn-primary" OnClick="btn_send_feedback_link_Click" Text="Send Feedback Link" OnClientClick="return Req_validation1();" />
                                    </div>
                                    <div class="col-sm-2 col-xs-12" style="margin-top: 1.5em">
                                        <asp:Button ID="btn_get_report" runat="server" class="btn btn-primary" OnClick="btn_get_report_Click" Text="Report" OnClientClick="return valid_email();" />
                                    </div>
                                    <br />
                                </div>
                                <br />
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:GridView ID="grd_feedback" class="table" runat="server" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        OnRowDataBound="grd_feedback_RowDataBound" AutoGenerateColumns="False" Width="100%" OnPreRender="grd_feedback_PreRender">
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="state_name" HeaderText="STATE" SortExpression="state_name" />
                                            <asp:BoundField DataField="month" HeaderText="MONTH" SortExpression="month" />
                                            <asp:BoundField DataField="year" HeaderText="YEAR" SortExpression="year" />
                                            <asp:BoundField DataField="unit_name" HeaderText="BRANCH" SortExpression="unit_name" />
                                            <asp:BoundField DataField="email_sent" HeaderText="EMAIL SENT" SortExpression="email_sent" />
                                            <asp:BoundField DataField="percent" HeaderText="FEEDBACK PERCENT" SortExpression="percent" />
                                        </Columns>
                                    </asp:GridView>
                                    <div class="row text-center">
                                        <asp:Button ID="btn_download" runat="server" class="btn btn-primary" OnClick="btn_download_Click" Text="Download" />
                                    </div>
                                    
                                </asp:Panel>
                           <%-- </ContentTemplate>--%>
                       <%-- </asp:UpdatePanel>--%>
                    </div>

                    <div id="menu3" class="Relieving_Letter">
                        <br />
                        <div class="row">
                              <div class="col-sm-2 col-xs-12 text-left">
                               <b> Left Employee:</b>  <span style="color: red">*</span>
                                <asp:DropDownList ID="ddl_left_employee" class="form-control pr_state js-example-basic-single" runat="server" OnSelectedIndexChanged="ddl_left_employee_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <div class="col-sm-2 col-xs-12" style="margin-top: 16px;">

                                <asp:Button ID="btn_releiving_letter"
                              runat="server" class="btn btn-primary"
                              Text="Send Relieving Letter" OnClick="btn_releiving_letter_Click" />

                            </div>

                        </div>
                    </div>
                </div>
                    <br />
                    <br />

            </div>
                </div>
        </asp:Panel>
    </div>
</asp:Content>
