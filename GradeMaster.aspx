<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GradeMaster.aspx.cs" Inherits="GradeDetails" Title="DESIGNATION MASTER" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Designation Master</title>
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
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>

    <link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <%--<script src="datatable/jquery-1.12.3.js"></script>--%>
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/jszip.min.js"></script>
    <%--    <script src="datatable/pdfmake.min.js"></script>--%>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>

    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {
            $('#<%=btn_cancel.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_employee_type.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });


            disable_select_type();

            $(document).ready(function () {
                $('#<%=GradeGridView.ClientID%> td').click(function () {
                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                });
                var table = $('#<%=GradeGridView.ClientID%>').DataTable(
                     {
                         scrollY: "210px", buttons: [
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
                         ],
                         fixedHeader: {
                             header: true,
                             footer: true
                         }

                     });

                table.buttons().container()
                   .appendTo('#<%=GradeGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

                $.fn.dataTable.ext.errMode = 'none';


            });
       }
    </script>
    <style>
        .text-red {
            color: #f00;
        }

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
            height: 300px;
            overflow-x: hidden;
            overflow-y: auto;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }

        .row {
            margin: 0px;
        }
    </style>
    <script>
    </script>
    <script type="text/javascript">
        function Req_validation() {

            var t_GradeCode = document.getElementById('<%=txt_grade.ClientID %>');
            var t_Gradedesc = document.getElementById('<%=txtgradedesc.ClientID %>');

            var txt_hours = document.getElementById('<%=ddl_hours.ClientID %>');
            var Selected_txt_hours = txt_hours.options[txt_hours.selectedIndex].text;
            var ddl_employee_type = document.getElementById('<%=ddl_employee_type.ClientID %>');
            var Selected_ddl_employee_type = ddl_employee_type.options[ddl_employee_type.selectedIndex].text;
            // Grade Code

            if (t_GradeCode.value == "") {
                alert("Please Enter Designation Code");
                t_GradeCode.focus();
                return false;
            }

            // Grade Desc

            if (t_Gradedesc.value == "") {
                alert("Please Enter the  Designation description");
                t_Gradedesc.focus();
                return false;
            }
            var ddl_reportingto = document.getElementById('<%=ddl_reportingto.ClientID %>');
            var Selected_ddl_reportingto = ddl_reportingto.options[ddl_reportingto.selectedIndex].text;
            //if (Selected_ddl_reportingto == "Select Reporting") {
            //    alert("Please Select Reporting");
            //    ddl_reportingto.focus();
            //    return false;
            //}
            var ddl_hours = document.getElementById('<%=ddl_hours.ClientID %>');
            var Selected_ddl_hours = ddl_hours.options[ddl_hours.selectedIndex].text;
            if (Selected_ddl_employee_type == "Permanent") {
                if (Selected_ddl_hours == "Select") {
                    alert("Please Select Categories");
                    ddl_hours.focus();
                    return false;
                }
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
            //if (Selected_txt_hours == "Select") {
            //    alert("Please Select Categories !!!");
            //    txt_hours.focus();
            //    return false;
            //}
            //reporting 

            //if (t_Reporting.value == "" || t_Reporting.value =="Select Reporting") {
            //    alert("Please select reporting to");
            //    t_Reporting.focus();
            //    return false;
            //} 


        }




        function openWindow() {
            window.open("html/GradeMaster.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

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

        function disable_select_type() {
            var txt_ddl_employee_type = document.getElementById('<%=ddl_employee_type.ClientID %>');
            var Selected_txt_ddl_employee_type = txt_ddl_employee_type.options[txt_ddl_employee_type.selectedIndex].text;

            var txt_ddl_hours = document.getElementById('<%=ddl_hours.ClientID %>');
            var Selected_txt_ddl_hours = txt_ddl_hours.options[txt_ddl_hours.selectedIndex].text;



            if (Selected_txt_ddl_employee_type == "Staff") {
                txt_ddl_hours.disabled = true;
                return true;
            }
            else if (Selected_txt_ddl_employee_type == "Temporary") {
                txt_ddl_hours.disabled = true;
                return true;
            }
            else if (Selected_txt_ddl_employee_type == "Repair & Maintenance") {
                txt_ddl_hours.disabled = true;
                return true;
            }



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
        function AllowAlphabet(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45' || (keyEntry == '8'))
                return true;
            else {
                // alert('Please Enter Only Character values.');
                return false;
            }
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
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Designation Master</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Designation Master Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="container-fluid" style="border: 1px solid #ddd9d9; background: #f3f1fe; border-radius: 10px;margin-bottom:20px;margin-top:20px">
                            <br />
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-xs-12">
                                   <b> Employee Type :</b><span class="text-red"> *</span>
                                    <asp:DropDownList ID="ddl_employee_type" runat="server" class="form-control" OnSelectedIndexChanged="ddl_employee_type_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                        <asp:ListItem Value="Permanent">Permanent</asp:ListItem>
                                        <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                        <asp:ListItem Value="Temporary">Temporary</asp:ListItem>
                                        <asp:ListItem Value="RM">Repair & Maintenance</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                         <br />
                            <asp:Panel runat="server" ID="main_body">
                                <div class="row">
                                    <div class="col-md-2 col-sm-2 col-xs-12">
                                        <b>Designation Code :</b><span class="text-red"> *</span>
                                        <asp:TextBox ID="txt_grade" runat="server" class="form-control text_box" ReadOnly="true"
                                            OnTextChanged="txt_grade_TextChanged" MaxLength="10" onKeyPress="return AllowAlphabet(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txt_grade" ErrorMessage="Enter Grade Code"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-3 col-xs-12">
                                       <b> Designation Description :</b><span class="text-red"> *</span>

                                        <asp:TextBox ID="txtgradedesc" runat="server" onKeyPress="return AllowAlphabet_Number(event)" class="form-control text_box" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-xs-12">
                                       <b> Reporting To :</b>
                                        <asp:DropDownList ID="ddl_reportingto" runat="server" class="form-control text_box">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Categories :</b> <span style="color: red">*</span>

                                        <asp:DropDownList ID="ddl_hours" runat="server" CssClass="form-control text_box" Width="100%">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Unskilled">Unskilled</asp:ListItem>
                                            <asp:ListItem Value="Semiskilled">Semiskilled</asp:ListItem>
                                            <asp:ListItem Value="Skilled">Skilled</asp:ListItem>
                                            <asp:ListItem Value=" HighlySkilled"> HighlySkilled</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <%--<div class="col-sm-2 col-xs-12"><br />
                     <asp:Button ID="btn_new" runat="server" CssClass="btn btn-primary" Text=" New " 
                        onclick="btn_new_Click" CausesValidation="False" />
                 </div> --%>
                                </div>
                                <br />
                                <br />
                                <div class="row text-center">

                                    <asp:Button ID="btn_add" runat="server" class="btn btn-primary" OnClick="btn_add_Click"
                                        Text=" Save " OnClientClick=" return Req_validation();" />

                                    <asp:Button ID="btn_edit" runat="server" class="btn btn-primary"
                                        Text=" Update " OnClick="btn_edit_Click" OnClientClick=" return Req_validation();" />

                                    <asp:Button ID="btn_delete" runat="server" class="btn btn-primary" Text=" Delete "
                                        OnClick="btn_delete_Click" OnClientClick=" return R_validation();" />
                                    <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" Text=" Clear "
                                        OnClick="btn_cancel_Click" CausesValidation="False" />
                                    <%-- <asp:Button ID="btnexporttoexcelgrade" runat="server" class="btn btn-primary" 
                         Text="Export To Excel" onclick="btnexporttoexcelgrade_Click" 
                        CausesValidation="False" />--%>
                                    <asp:Button ID="btnclose" runat="server" class="btn btn-danger"
                                        OnClick="btnclose_Click" Text="Close" CausesValidation="False" />
                                </div>
                                <br />
                                <br />
                                <div class="container-fluid" style="background: white; border-radius: 10px; margin:20px 20px 20px 20px; border: 1px solid white; padding:20px 20px 20px 20px">
                               
                                    <asp:Panel ID="Panel2" runat="server" class="panel-body" Width="100%">
                                        <asp:GridView ID="GradeGridView" class="table" runat="server" Font-Size="X-Small"
                                            AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" OnPreRender="GradeGridView_PreRender"
                                            OnSelectedIndexChanged="GradeGridView_SelectedIndexChanged"
                                            OnRowDataBound="GradeGridView_RowDataBound" Width="100%">
                                            <RowStyle ForeColor="#000066" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="DESIGNATION CODE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_GRADE_CODE" runat="server" Text='<%# Eval("GRADE_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DESIGNATION DESCRIPTION">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_GRADE_DESC" runat="server" Text='<%# Eval("GRADE_DESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REPORTING TO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_REPORTING_TO" runat="server" Text='<%# Eval("REPORTING_TO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CATEGORIES">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_WORKING_HOURS" runat="server" Text='<%# Eval("Working_Hours") %>'></asp:Label>
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
                            <br />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
    </div>
</asp:Content>


