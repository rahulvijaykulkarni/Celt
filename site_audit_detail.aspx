<%@ Page Language="C#" Title="Site Audit Details" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="site_audit_detail.aspx.cs" EnableEventValidation="false" Inherits="site_audit_detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Site Audit Details</title>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="cph_header" runat="server">
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
    <%-- <script src="datatable/pdfmake.min.js"></script>--%>
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/dataTables.fixedColumns.min.js"></script>
    <link href="css/fixedColumns.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script>
        function unblock() {
            $.unblockUI();
        }


        $(function () {
            $('#<%=ddl_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_unit.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=dd1_super.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

            $("#dialog").dialog({

                autoOpen: false,
                modal: true,
                height: 500,
                width: 500,
                title: "Zoomed Image",
                buttons: [{ text: "Close", click: function () { $(this).dialog("close") } }],
            });
            $("[id*=que_1_path]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(400));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=que_2_path]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(400));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=que_3_path]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(400));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=que_4_path]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(400));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=que_5_path]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(400));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=que_6_path]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(400));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });

            var table = $('#<%=companyGridView.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                scrollY: "310px",
                scrollX: true,
                scrollCollapse: true,


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
               .appendTo('#<%=companyGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';

            var table = $('#<%=companyGridView1.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                scrollY: "310px",
                scrollX: true,
                scrollCollapse: true,


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
               .appendTo('#<%=companyGridView1.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';
        });

        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab1']").val();
            if (st == null)
                st = 0;
            $('[id$=tab1]').tab1({ selected: st });
        });

        function Req_validation() {
            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

            if (Selected_ddl_client == "Select") {
                alert("Please Select Client Name !!");
                ddl_client.focus();
                return false;
            }
            var ddl_state = document.getElementById('<%=ddl_state.ClientID %>');
                var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;


                if (Selected_ddl_state == "Select") {
                    alert("Please Select State Name");
                    ddl_state.focus();
                    return false;

                }
                var ddl_unit = document.getElementById('<%=ddl_unit.ClientID %>');
                var Selected_ddl_unit = ddl_unit.options[ddl_unit.selectedIndex].text;
                if (Selected_ddl_unit == "Select") {
                    alert("Please Select Branch Name ");
                    ddl_unit.focus();
                    return false;

                }
                var dd1_super = document.getElementById('<%=dd1_super.ClientID %>');
                var Selected_dd1_super = dd1_super.options[dd1_super.selectedIndex].text;
                if (Selected_dd1_super == "Select") {
                    alert("Please Select Supervisor Name ");
                    dd1_super.focus();
                    return false;

                }
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;

            }
            function R_validation() {

                var r = confirm("Are you Sure You Want to Approved Record");
                if (r == true) {
                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                }
                else {
                    alert("Record not Available");
                }
                return r;
            }
            function R_validation1() {

                var r = confirm("Are you Sure You Want to Approved Record");
                if (r == true) {

                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                }
                else {
                    alert("Record not Available");
                }
                return r;
            }

            function R_validation2() {

                var r = confirm("Are you Sure You Want to Complete this Record");
                if (r == true) {

                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                }
                else {
                    alert("Record not Available");
                }
                return r;
            }
    </script>
    <style>
        .dataTables_filter {
            text-align: right;
        }

        .grid-view {
            height: auto;
            max-height: 300px;
            width: auto;
            overflow-y: auto;
            overflow-x: hidden;
        }
    </style>
    <script>
        //v validation for reject button
        function Req_valid() {


            var text_doc_comment = document.getElementById('<%=text_comment.ClientID %>');


            if (text_doc_comment.value == "") {

                alert("Please Enter Reason !!");
                text_doc_comment.focus();
                return false;
            }
            var r = confirm("Are you Sure You Want to reject Record");
            if (r == true) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            }
            else {
                alert("Record not Available");
            }
            return r;
        }
        function openWindow() {
            window.open("html/Itemmaste1.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
    </script>

</asp:Content>
<asp:Content ContentPlaceHolderID="cph_righrbody" ID="content3" runat="server">
    <div class="container-fluid">

    <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
        <div class="panel-heading">
            <div class="row">
                <div class="col-sm-1"></div>
                <div class="col-sm-9">
                    <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Site Audit</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Site Audit Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>
        <div class="panel-body">

            <div class="container-fluid" style="border: 1px solid #ddd9d9; background: #f3f1fe; border-radius: 10px;padding:15px 15px 15px 15px; margin-bottom:20px; margin-top:20px">
                <br />
                <div class="row">
                     <div class="col-sm-2 col-xs-12 text-left">
                       <b> Name :</b>
                                  <span class="text-red" style="color: red">*</span>
                        <asp:DropDownList ID="ddl_feedback_type" class="form-control" Width="100%" runat="server" OnSelectedIndexChanged="ddl_feedback_type_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                     <asp:ListItem Value="1">Site Audit</asp:ListItem>
                                     <asp:ListItem Value="2">Supervisor Through Feedback</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                        <b>Client Name :</b>
                                  <span class="text-red" style="color: red">*</span>
                        <asp:DropDownList ID="ddl_client" class="form-control" Width="100%" runat="server" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true">
                             </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> State :</b>
                            <span class="text-red" style="color: red">*</span>
                        <asp:DropDownList ID="ddl_state" runat="server" class="form-control" Width="100%" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                       <b> Branch Name :</b>
                            <span class="text-red" style="color: red">*</span>
                        <asp:DropDownList ID="ddl_unit" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_unit_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12 text-left">
                       <b> Supervisor Name :</b>
                            <span class="text-red" style="color: red">*</span>
                        <asp:DropDownList ID="dd1_super" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dd1_super_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                 <%--   <div class="row text-center">
                    <div class="col-sm-2 col-xs-12 text-center" style="margin-top: 17px;">
                        <asp:Button ID="btn_show" runat="server" class="btn btn-primary" OnClientClick="return Req_validation();" Text=" Show " OnClick="btn_show_Click" />
                        <asp:Button ID="btn_close" runat="server" class="btn btn-danger" Text="Close" OnClick="btn_close_click" />
                    </div>
                        </div>--%>
                </div>
                <br />
                <br />
                <%--  this div we use for dialog popup window--%>
                <%--<div id="dialog"></div>--%>
                
                <asp:Panel ID="reject1" runat="server" CssClass="grid-view">
                    <div class="row">
                        <div class="col-sm-4 col-xs-12 text-left"></div>
                        <div class="col-sm-2 col-xs-12 text-left">
                            <span id="comment1">Comment :</span>
                            <asp:TextBox ID="text_comment" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>

                        <div class="col-sm-3 col-xs-12 text-left">
                            <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text=" Reject " DeleteText="Reject" OnClientClick="return Req_valid();" OnClick="Button1_Click" Style="margin-top: 30px" />
                        </div>
                        <div class="col-sm-2 col-xs-12 text-left">
                            <%-- <span>id :</span>--%>
                            <asp:TextBox ID="text_id" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                        </div>
                    </div>
                </asp:Panel>
                <br />
           
                
            <br />
            <br />
           
            <asp:Panel ID="Panel2" runat="server">
                <%--<div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; margin-top:20px; padding:20px 20px 20px 20px">--%>
                <asp:GridView ID="companyGridView" class="table" HeaderStyle-CssClass="FixedHeader" runat="server"
                    AutoGenerateColumns="False" CellPadding="1" Font-Size="X-Small" ForeColor="#333333" DataKeyNames="id" OnPreRender="companyGridView_PreRender"
                    OnRowDataBound="companyGridView_RowDataBound" OnRowEditing="gv_emp_d_varification_RowEditing" OnRowDeleting="gv_emp_varification_reject">
                    <%--  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingRowStyle BackColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="White" ForeColor="#000066" />--%>

                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" Width="50" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />


                    <Columns>

                        <asp:BoundField DataField="Id" HeaderText="ID"
                            SortExpression="Id" Visible="false" />
                        <asp:BoundField DataField="state_name" HeaderText="State_Name"
                            SortExpression="state_name" />
                        <asp:BoundField DataField="unit_name" HeaderText="Unit_Name"
                            SortExpression="unit_name" />
                        <asp:BoundField DataField="visit_date" HeaderText="Visit_Date"
                            SortExpression="visit_date" />

                        <asp:BoundField DataField="emp_code" HeaderText="Employee_Name"
                            SortExpression="emp_code" />
                        <asp:BoundField DataField="grade_name" HeaderText="Grade_name"
                            SortExpression="grade_name" />
                        <asp:BoundField DataField="que_1_ans" HeaderText="Question_1"
                            SortExpression="que_1_ans" />

                        <asp:TemplateField HeaderText="IMAGE">
                            <ItemTemplate>
                                <asp:Image ID="que_1_path" runat="server" Height="50" Width="50" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="que_2_ans" HeaderText="Question_2"
                            SortExpression="que_2_ans" />

                        <asp:TemplateField HeaderText="IMAGE">
                            <ItemTemplate>
                                <asp:Image ID="que_2_path" runat="server" Height="50" Width="50" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="que_3_ans" HeaderText="Question_3"
                            SortExpression="que_3_ans" />

                        <asp:TemplateField HeaderText="IMAGE">
                            <ItemTemplate>
                                <asp:Image ID="que_3_path" runat="server" Height="50" Width="50" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="que_4_ans" HeaderText="Question_4"
                            SortExpression="que_4_ans" />


                        <asp:TemplateField HeaderText="IMAGE">
                            <ItemTemplate>
                                <asp:Image ID="que_4_path" runat="server" Height="50" Width="50" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="que_5_ans" HeaderText="Question_5"
                            SortExpression="que_5_ans" />


                        <asp:TemplateField HeaderText="IMAGE">
                            <ItemTemplate>
                                <asp:Image ID="que_5_path" runat="server" Height="50" Width="50" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="que_6_ans" HeaderText="Question_6"
                            SortExpression="que_6_ans" />


                        <asp:TemplateField HeaderText="IMAGE">
                            <ItemTemplate>
                                <asp:Image ID="que_6_path" runat="server" Height="50" Width="50" />
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:BoundField DataField="remark" HeaderText="Remark"
                            SortExpression="remark" />
                        <asp:BoundField DataField="location" HeaderText="location"
                            SortExpression="location" />
                        <asp:BoundField DataField="comment" HeaderText="Comment"
                            SortExpression="comment" />
                        <%--  <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-primary"  
                                                        ShowDeleteButton="true" EditText="Approved" DeleteText="Reject" ShowEditButton="true"/>
                        --%>
                        <asp:BoundField DataField="Status" HeaderText="Status"
                            SortExpression="Status" />

                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>

                                <asp:LinkButton ID="lnkbtn_edititem" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Approved" OnClientClick="return R_validation();" OnClick="lnkbtn_edititem_Click"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Reject" OnClick="LinkButton2_Click" OnClientClick="return R_validation1();"></asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtn_complete" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Complete" OnClick="lnkbtn_complete_Click" OnClientClick="return R_validation2();"></asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>



                    </Columns>




                </asp:GridView>
            </asp:Panel>
           
            <asp:Panel ID="panelc" runat="server" >
               <%-- <div class="container-fluid" style="background: #f3f1f; border-radius: 10px; margin-top:20px; padding:20px 20px 20px 20px">--%>
                <asp:GridView ID="companyGridView1" class="table" HeaderStyle-CssClass="FixedHeader" runat="server"
                    AutoGenerateColumns="False" CellPadding="1" Font-Size="X-Small" ForeColor="#333333" DataKeyNames="id" OnRowDataBound="companyGridView1_RowDataBound" OnPreRender="companyGridView1_PreRender">

                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    <Columns>

                        <asp:BoundField DataField="ID" HeaderText="ID"
                            SortExpression="ID" />

                        <asp:BoundField DataField="state_name" HeaderText="State_Name"
                            SortExpression="state_name" />
                        <asp:BoundField DataField="unit_name" HeaderText="Unit_Name"
                            SortExpression="unit_name" />
                        <asp:BoundField DataField="cur_date" HeaderText="Visit Date"
                            SortExpression="cur_date" />
                         <asp:BoundField DataField="emp_name" HeaderText="Employee_Name"
                            SortExpression="emp_name" />

                        <asp:BoundField DataField="que_1_ans" HeaderText="Question_1"
                            SortExpression="que_1_ans" />
                        <asp:BoundField DataField="que_2_ans" HeaderText="Question_2"
                            SortExpression="que_2_ans" />
                        <asp:BoundField DataField="que_3_ans" HeaderText="Question_3"
                            SortExpression="que_3_ans" />
                        <asp:BoundField DataField="que_4_ans" HeaderText="Question_4"
                            SortExpression="que_4_ans" />
                        <asp:BoundField DataField="que_5_ans" HeaderText="Question_5"
                            SortExpression="que_5_ans" />
                        <asp:BoundField DataField="que_6_ans" HeaderText="Question_6"
                            SortExpression="que_6_ans" />
                        <asp:BoundField DataField="que_7_ans" HeaderText="Question_7"
                            SortExpression="que_7_ans" />
                        <asp:BoundField DataField="que_8_ans" HeaderText="Question_8"
                            SortExpression="que_8_ans" />
                        <asp:BoundField DataField="que_9_ans" HeaderText="Question_9"
                            SortExpression="que_9_ans" />
                        <asp:BoundField DataField="que_10_ans" HeaderText="Question_10"
                            SortExpression="que_10_ans" />
                          <asp:BoundField DataField="remark" HeaderText="Remark"
                            SortExpression="remark" />
                           <asp:BoundField DataField="Status" HeaderText="Status"
                            SortExpression="Status" />
                         <asp:TemplateField >
                            <ItemStyle HorizontalAlign="Left"   />
                            <ItemTemplate>
                                <div class="row">
                                <asp:LinkButton ID="lnkbtn_edititem" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Approved" Width="100%"  OnClientClick="return R_validation();" OnClick="lnkbtn_edititem_Click"></asp:LinkButton>
                               </div>
                                <br />
                                <div class="row">
                                <asp:LinkButton ID="LinkButton2" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Reject"   OnClick="LinkButton2_Click" OnClientClick="return R_validation1();"></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtn_complete1" runat="server" ControlStyle-CssClass="btn btn-primary"  Width="100%" Text="Complete" OnClick="lnkbtn_complete1_Click" OnClientClick="return R_validation2();"></asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </asp:Panel>
        
        </div>
    </asp:Panel>
         </div>
     </div>
</asp:Content>
