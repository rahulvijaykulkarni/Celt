<%@ Page Title="Leave Request" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Leave_form_management.aspx.cs" Inherits="Leave_form_management" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Leave Request</title>
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
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/hashfunction.js"></script>
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />

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
    <style type="text/css">
        .container {
            max-width: 99%;
        }

        .grid-view {
            height: auto;
            max-height: 500px;
            overflow-x: hidden;
            overflow-y: auto;
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

        .auto-style1 {
            color: #FFFFFF;
        }

        h2 {
            border-radius: 5px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }

        function pageLoad() {

            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
                onSelect: function (selected) {
                    $(".date-picker2").datepicker("option", "minDate", selected)
                }
            });

            $(".date-picker2").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
                onSelect: function (selected) {
                    $(".date-picker1").datepicker("option", "maxDate", selected)
                }
            });
        }

        $(function () {

            $('#<%=btn_cancel.ClientID%>').click(function () {
                 $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             });


             $('#<%=LeaveTypeGridView.ClientID%> td').click(function () {
                 $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             });

         });


         $(document).ready(function () {


             var evt = null;
             isNumber(evt);
             var e = null;
             AllowAlphabet(e);
         });

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
         function AllowAlphabet(e) {
             if (null != e) {

                 isIE = document.all ? 1 : 0
                 keyEntry = !isIE ? e.which : e.keyCode;
                 if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || (keyEntry < '31'))
                     return true;
                 else {
                     // alert('Please Enter Only Character values.');
                     return false;
                 }
             }
         }
         function openWindow() {
             window.open("html/Leave_form_management.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
         }



         function Req_validation() {


             var t_status_comment = document.getElementById('<%=txt_status_comment.ClientID %>');


                   // Leave Status Comment

             if (t_status_comment.value == "") {
                 alert("Please Enter Leave Comments.");
                 t_status_comment.focus();
                 return false;
             }

             $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             return true;
         }
         $(function () {

             var table = $('#<%=LeaveTypeGridView.ClientID%>').DataTable({
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
                .appendTo('#<%=LeaveTypeGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

             $.fn.dataTable.ext.errMode = 'none';

         });

    </script>

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>


        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Leave Request</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Leave Request Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">
                <div class="row">
                    <div class="col-sm-2 col-xs-12">
                       <b> Employee Code :</b>
                    <asp:TextBox ID="txt_eecode" runat="server" class="form-control text_box"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Employee Name :</b>
                    <asp:TextBox ID="txt_employee_name" runat="server" class="form-control text_box"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Leave Apply Date :</b>
                
                    <asp:TextBox ID="txt_leave_apply_date" runat="server" Width="95%" Style="display: inline" class="form-control date-picker text_box"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                      <b>  Leave Type :</b>
                    <asp:TextBox ID="ddl_leave_type" runat="server" class="form-control text_box"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Reason :</b>
                    <asp:TextBox ID="txt_reason" runat="server" class="form-control text_box" TextMode="MultiLine" Height="50px" Width="250px" MaxLength="200"></asp:TextBox>
                    </div>
                </div>
                    <br />
                    <br />
                <div class="row">

                    <div class="col-sm-2 col-xs-12">
                       <b> Start Date :</b>
                    <asp:TextBox ID="txt_fromdate" runat="server" class="form-control date-picker1 text_box" Width="95%" Style="display: inline"></asp:TextBox>
                    </div>

                    <div class="col-sm-2 col-xs-12">
                       <b> End Date :</b>
                
                    <asp:TextBox ID="txt_todate" runat="server" class="form-control date-picker2 text_box" Width="95%" Style="display: inline"></asp:TextBox>
                    </div>

                    <div class="col-sm-2 col-xs-12">
                       <b> No. Of Days :</b>
                    <asp:TextBox ID="txt_no_of_days" runat="server" class="form-control text_box" onkeypress="return isNumber(event)"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                        <asp:Label ID="lbl_status" runat="server"><b>Leave Status:</b></asp:Label>
                        <asp:DropDownList ID="txt_Status" runat="server" class="form-control text_box">
                            <asp:ListItem Value="In Progress">In Progress</asp:ListItem>
                            <asp:ListItem Value="Approved">Approved</asp:ListItem>
                            <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3 col-xs-12">
                       <b> Comments :</b>
                 <asp:TextBox ID="txt_status_comment" runat="server" class="form-control text_box" Width="250px" MaxLength="200"
                     onKeyPress="return AllowAlphabet(event)"></asp:TextBox>
                    </div>
                </div>
                    </div>
                <br />
                <div class="row text-center">
                    <asp:Button ID="btn_edit" runat="server" class="btn btn-primary" OnClientClick="return Req_validation();"
                        OnClick="btn_edit_Click" Text=" Update " />
                    <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" Text=" Clear "
                        OnClick="btn_cancel_Click" />
                    <asp:Button ID="btnclose" runat="server" class="btn btn-danger"
                        OnClick="btnclose_Click" Text="Close" />
                </div>
                <br />
                <asp:Panel ID="Panel2" runat="server" CssClass="grid-view">
                    <asp:GridView ID="LeaveTypeGridView" class="table" AutoGenerateColumns="False" runat="server"
                        BackColor="White" BorderColor="#CCCCCC" Font-Size="X-Small"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        OnSelectedIndexChanged="LeaveTypeGridView_SelectedIndexChanged" OnRowDataBound="LeaveTypeGridView_RowDataBound" OnPreRender="LeaveTypeGridView_PreRender">
                        <RowStyle ForeColor="#000066" />
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                        <RowStyle ForeColor="#000066" BackColor="#ffffff" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                        <Columns>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="EMPLOYEE CODE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_EMP_CODE" runat="server" Text='<%# Eval("EMP_CODE")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="EMPLOYEE NAME">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_EMP_NAME" runat="server" Text='<%# Eval("EMP_NAME")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="LEAVE APPLY DATE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Leave_Apply_Date" runat="server" Text='<%# Eval("Leave_Apply_Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="LEAVE TYPE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_LEAVE_TYPE" runat="server" Text='<%# Eval("LEAVE_TYPE")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="LEAVE REASON">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_LEAVE_REASON" runat="server" Text='<%# Eval("LEAVE_REASON")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="FROM DATE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_FROM_DATE" runat="server" Text='<%# Eval("FROM_DATE")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="TO DATE">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_TO_DATE" runat="server" Text='<%# Eval("TO_DATE")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="NO OF DAYS">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_NO_OF_DAYS" runat="server" Text='<%# Eval("NO_OF_DAYS")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="LEAVE STATUS">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_LEAVE_STATUS" runat="server" Text='<%# Eval("LEAVE_STATUS")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="STATUS COMMENT">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_STATUS_COMMENT" runat="server" Text='<%# Eval("STATUS_COMMENT")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="LAST UPDATED">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Leave_Approved_Date" runat="server" Text='<%# Eval("Leave_Approved_Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ID">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Leave_ID" runat="server" Text='<%# Eval("leave_id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>

                </asp:Panel>
            </div>
        </asp:Panel>

    </div>



</asp:Content>
