<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Shift Master" AutoEventWireup="true" CodeFile="Shiftassign.aspx.cs" Inherits="shift" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Shift Creation</title>
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
    <script src="datatable/pdfmake.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var table = $('#<%=GradeGridView.ClientID%>').DataTable({
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
                   .appendTo('#<%=GradeGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

                $.fn.dataTable.ext.errMode = 'none';

            });
    </script>




    <script>

        $(function () {

            $('#<%=btn_add.ClientID%>').click(function () {
                 if (Req_validation()) {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 }

             });
             $('#<%=btn_edit.ClientID%>').click(function () {
                 if (Req_validation()) {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 }
             });
             $('#<%=btn_delete.ClientID%>').click(function () {
                 if (Req_validation()) {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 }
             });
             $('#<%=GradeGridView.ClientID%> td').click(function () {
                 if (Req_validation()) {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 }
             });
         });
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
    </script>
    <style>
        .container {
            max-width: 99%;
        }

        .label_text {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 10px;
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
    </style>
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

            // Req_validation();
        }



        //function Req_validation() {


        //    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
        //    return true;
        //}
        window.onfocus = function () {
            $.unblockUI();

        }

        function openWindow() {
            window.open("html/shiftassign.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

        function isNumber(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    if (charCode == 46) {
                        return true;
                    }
                    return false;
                }

            }
            return true;
        }

        function AllowAlphabet12(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry < '31'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }


        function Req_validation() {


            var t_shift_name = document.getElementById('<%=txt_shift_name.ClientID %>');
            var d_shift_from = document.getElementById('<%=ddl_shift_from.ClientID %>');
            var SelectedText11_from = d_shift_from.options[d_shift_from.selectedIndex].text;
            var d_shift_to = document.getElementById('<%=ddl_shift_to.ClientID %>');
            var SelectedText11_to = d_shift_to.options[d_shift_to.selectedIndex].text;



            if (t_shift_name.value == "") {
                alert("Please Enter Shift Name");

                t_shift_name.focus();
                return false;
            }

            if (SelectedText11_from == "Select Shift From") {
                alert("Please Select Shift From !!!");
                ddl_shift_from.focus();
                return false;
            }

            if (SelectedText11_to == "Select Shift To") {
                alert("Please Select Shift To !!!");
                ddl_shift_to.focus();
                return false;
            }


        }

        function validation(id) {
            var a = document.getElementById('<%=ddl_shift_from.ClientID %>');
            var a = document.getElementById(id);
            var b = toDate(a.value, "h:m")
            var c = b.toString().substr(16, 8);
            var H = +c.substr(0, 2);
            var h = H % 12 || 12;
            var ampm = H < 12 ? "AM" : "PM";
            c = h + c.substr(2, 3) + ampm;
            if (c.length == 7 || c.length == 6) {
                a.value = c;
            }
        }
        function toDate(dStr, format) {
            var now = new Date();
            if (format == "h:m") {
                now.setHours(dStr.substr(0, dStr.indexOf(":")));
                now.setMinutes(dStr.substr(dStr.indexOf(":") + 1));
                now.setSeconds(0);
                return now;
            } else
                return "Invalid Format";
        }
    </script>


</asp:Content>
<%--  --%>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <%-- <asp:UpdatePanel ID="updatepanel" runat="server">
        <ContentTemplate>--%>

    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>

    <div class="container-fluid">
        <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        --%>


        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Shift Creation</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;">
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Shift Creation Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">

               
                    <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">

                    <%--<div class="container client">


                        <div class="well">
                            <div class="tab-content">
                                <div class="tab-pane fade in active" id="tab1">--%>
                                    <div class="row">
                                        <div class="col-md-3  col-xs-12">
                                          <b>  Shift Name :</b>
                                           <asp:TextBox ID="txt_shift_name" Font-Size="X-Small" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                        </div>

                                        <div class="col-md-3 col-xs-12">
                                           <b> Shift From (HH:MM):</b><asp:TextBox ID="ddl_shift_from" runat="server" onblur="return validation(this.id);" class="form-control text_box" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3 col-xs-12">
                                           <b> Shift To (HH:MM):</b><asp:TextBox ID="ddl_shift_to" runat="server" onblur="return validation(this.id);" class="form-control text_box" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                        </div>
                                        <%--     <div class='input-group date' id='datetimepicker3'>
                                                            <input type='text' class="form-control" />
                                                            <span class="input-group-addon">
                                                                <span class="glyphicon glyphicon-time"></span>
                                                            </span>
                                                        </div>--%>
                                        <div class="col-sm-3 col-xs-12">
                                            <br />
                                            <asp:CheckBox runat="server" Text="&nbsp;Send Emails to Employees" ID="chk_send_email" AutoPostBack="true" OnCheckedChanged="chk_send_email_CheckedChanged" />
                                            <%--<asp:Button ID="btn_new" runat="server" CssClass="btn btn-primary" Text=" Add New Shift "
                                                OnClick="btn_new_Click" CausesValidation="False" />--%>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="auto" CssClass="grid-view" class="panel-body">
                                        <%--  <asp:SqlDataSource ID="SqlDataSource" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:CELTPAYConnectionString %>" ProviderName="<%$ ConnectionStrings:celtpayConnectionString.ProviderName %>"
                                            SelectCommand="SELECT comp_code, shift_name, DATE_FORMAT(shift_from,'%h:%i %p')As shift_from,DATE_FORMAT(shift_to,'%h:%i %p') As shift_to FROM pay_shift_master WHERE (comp_code = @comp_code)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="comp_code" SessionField="comp_code" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>--%>

                                        <asp:GridView ID="GradeGridView" class="table" runat="server"
                                            AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="GradeGridView_SelectedIndexChanged"
                                            OnRowDataBound="GradeGridView_RowDataBound" OnPreRender="GradeGridView_PreRender">
                                            <RowStyle ForeColor="#000066" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Shift Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_shift_name" runat="server" Text='<%# Eval("shift_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shift From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_shift_from" runat="server" Text='<%# Eval("shift_from") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shift TO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_shift_to" runat="server" Text='<%# Eval("shift_to") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>

                                    </asp:Panel>
                                    <br />
                                    <br />
                                    
                                    <div class="row text-center">

                                        <asp:Button ID="btn_add" runat="server" class="btn btn-primary" OnClick="btn_add_Click"
                                            Text=" Save " OnClientClick="Req_validation();" />

                                        <asp:Button ID="btn_edit" runat="server" class="btn btn-primary"
                                            Text=" Update " OnClick="btn_edit_Click" />

                                        <asp:Button ID="btn_delete" runat="server" class="btn btn-primary" Text=" Delete "
                                            OnClick="btn_delete_Click" />

                                        <cc1:ConfirmButtonExtender ID="btn_delete_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure want to delete this record?" Enabled="True" TargetControlID="btn_delete"></cc1:ConfirmButtonExtender>

                                        <%--  <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" Text=" Clear "
                                            CausesValidation="False" />--%>

                                        <asp:Button ID="btnclose" runat="server" class="btn btn-danger" OnClick="btn_close"
                                            Text="Close" CausesValidation="False" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </asp:Panel>
    </div>
    <%--</ContentTemplate>           

      </asp:UpdatePanel>--%>
</asp:Content>



