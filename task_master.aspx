<%@ Page Language="C#" AutoEventWireup="true" CodeFile="task_master.aspx.cs" Inherits="task_master" MasterPageFile="~/MasterPage.master" Title="Task Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Task Master</title>
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
    <script src="Scripts/datetimepicker.js"></script>
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>
    <link href="css/select2.min.css" rel="stylesheet" />
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


    <script>
        function unblock()
        { $.unblockUI(); }
        $(document).ready(function () {

        });
        //branch location
        function pageLoad() {
            $(function () {
                $('#<%=ddl_client.ClientID%>').change(function () {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 });
                 $('#<%=ddl_client_b.ClientID%>').change(function () {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 });
                 $('#<%=ddl_state_b.ClientID%>').change(function () {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 });
                 $('#<%=ddl_branch_b.ClientID%>').change(function () {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 });
                 $('#<%=DropDownList1.ClientID%>').change(function () {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 });
                 $('#<%=DropDownList2.ClientID%>').change(function () {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 });
                 $('#<%=DropDownList3.ClientID%>').change(function () {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 });
                 $('#<%=ddl_client_attendance.ClientID%>').change(function () {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 });
                 $('#<%=client_Billing.ClientID%>').change(function () {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 });
                 $('#<%=state_Billing.ClientID%>').change(function () {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 });
                 $('#<%=ddl_client_sallary.ClientID%>').change(function () {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 });
                 $('#<%=ddl_client_empCompliances.ClientID%>').change(function () {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 });
                 $('#<%=ddl_sate_empCompliances.ClientID%>').change(function () {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 });
                 $('#<%=ddl_brachCompliances.ClientID%>').change(function () {
                     $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                 });
                 $('.date-picker').datepicker({
                     changeMonth: true,
                     changeYear: true,
                     yearRange: "1950",
                     showButtonPanel: true,
                     dateFormat: 'mm/yy',
                     onClose: function (dateText, inst) {
                         var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                         var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                         $(this).val($.datepicker.formatDate('mm/yy', new Date(year, month, 1)));
                     }
                 });
                 $(".date-picker").focus(function () {
                     $(".ui-datepicker-calendar").hide();
                 })
                 $(".date-picker").attr("readonly", "true");

                 $('.date-picker1').datepicker({
                     changeMonth: true,
                     changeYear: true,
                     yearRange: "1950",
                     showButtonPanel: true,
                     dateFormat: 'mm/yy',
                     onClose: function (dateText, inst) {
                         var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                         var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                         $(this).val($.datepicker.formatDate('mm/yy', new Date(year, month, 1)));
                     }
                 });
                 $(".date-picker1").focus(function () {
                     $(".ui-datepicker-calendar").hide();
                 })
                 $(".date-picker1").attr("readonly", "true");

             });

             $.fn.dataTable.ext.errMode = 'none';
             var table = $('#<%=grd_sallary.ClientID%>').DataTable({
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
                .appendTo('#<%=grd_sallary.ClientID%>_wrapper .col-sm-6:eq(0)');


               $.fn.dataTable.ext.errMode = 'none';
               var table = $('#<%=grd_client.ClientID%>').DataTable({
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
               .appendTo('#<%=grd_client.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=location_branch.ClientID%>').DataTable({
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
               .appendTo('#<%=location_branch.ClientID%>_wrapper .col-sm-6:eq(0)');

              $.fn.dataTable.ext.errMode = 'none';
        }

        function openWindow() {
            window.open("html/TaskCodeHeader.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
          function validation() {
              var ddl_client_attendance = document.getElementById('<%=ddl_client_attendance.ClientID %>');
             var Selected_ddl_client_attendance = ddl_client_attendance.options[ddl_client_attendance.selectedIndex].text;

             if (Selected_ddl_client_attendance == "Select") {
                 alert("Please Select Client Name");
                 ddl_client_attendance.focus();
                 return false;
             }
             var ddl_unitcode1 = document.getElementById('<%=ddl_unitcode1.ClientID %>');
             var Selected_ddl_unitcode1 = ddl_unitcode1.options[ddl_unitcode1.selectedIndex].text;

             if (Selected_ddl_unitcode1 == "Select") {
                 alert("Please Select State Name");
                 ddl_unitcode1.focus();
                 return false;
             }
             var txt_date1 = document.getElementById('<%=txt_date1.ClientID %>');

             if (txt_date1.value == "") {
                 alert("Please Select Date");
                 txt_date1.focus();
                 return false;
             }
             $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             return true;

         }
         function R_validation() {

             var ddl_client_sallary = document.getElementById('<%=ddl_client_sallary.ClientID %>');
             var Selected_ddl_client_sallary = ddl_client_sallary.options[ddl_client_sallary.selectedIndex].text;

             if (Selected_ddl_client_sallary == "Select") {
                 alert("Please Select Client Name");
                 ddl_client_sallary.focus();
                 return false;
             }
             var ddl_state_sallary = document.getElementById('<%=ddl_state_sallary.ClientID %>');
             var Selected_ddl_state_sallary = ddl_state_sallary.options[ddl_state_sallary.selectedIndex].text;

             if (Selected_ddl_state_sallary == "Select") {
                 alert("Please Select State Name");
                 ddl_state_sallary.focus();
                 return false;
             }
             var txt_date = document.getElementById('<%=txt_date.ClientID %>');

             if (txt_date.value == "") {
                 alert("Please Select Date");
                 txt_date.focus();
                 return false;
             }
             $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             return true;
         }
         $(document).ready(function () {
             var st = $(this).find("input[id*='hidtab']").val();
             if (st == null)
                 st = 0;
             $('[id$=tabs]').tabs({ selected: st });
         });
         $(document).ready(function () {
             var st = $(this).find("input[id*='HiddenField1']").val();
             if (st == null)
                 st = 0;
             $('[id$=attendance_remain]').tabs({ selected: st });
         });
    </script>
    <style>
        .grid-view {
            max-height: 300px;
            height: auto;
            overflow-x: hidden;
            overflow-y: auto;
        }

        .grid-view1 {
            max-height: 300px;
            height: auto;
            overflow-x: auto;
            overflow-y: auto;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <div class="container-fluid">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-primary">

            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; text-align: center; font-size: small;"><b>TASK MASTER</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image13" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />

            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Task Master Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>

            <br />
            <div class="container-fluid">
                <div id="tabs" style="background: #f3f1fe; border-radius: 10px; padding:20px 20px 20px 20px; border: 1px solid white;margin-bottom:15px; margin-top:15px">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li><a href="#menu1" runat="server"><b>Client</b></a></li>
                        <li><a href="#menu2" runat="server"><b>Branch/Location</b> </a></li>
                        <li><a href="#menu3" runat="server"><b>Employee</b></a></li>
                        <li><a href="#menu4" runat="server"><b>Billing</b></a></li>
                        <li><a href="#menu5" runat="server"><b>Salary</b></a></li>
                        <li><a href="#menu6" runat="server"><b>Employee Compliances</b></a></li>
                    </ul>
                    <div id="menu1">
                        <asp:Panel runat="server" ID="panel_client">
                            <br />
                            <br />
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel2">
                                <ContentTemplate>
                                    <div class="row" runat="server">

                                        <div class="col-sm-3 col-xs-12">
                                           <b> Client Name :</b>
                                              <asp:DropDownList ID="ddl_client" runat="server" Width="100%" AutoPostBack="true"
                                                  class=" form-control" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged">
                                              </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Client Address :</b>
                                                <asp:TextBox ID="txt_client_address" runat="server"
                                                    class="form-control" Columns="2" Rows="2" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Total Employee Count :</b>
                                 <asp:TextBox ID="txt_emp_count" runat="server"
                                     class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12" style="width: 18%">
                                          <b>  Remaining Count For Deployment:</b>
                                 <asp:TextBox ID="txt_differance_count" runat="server"
                                     class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> No Of Diployed Employee:</b>
                                 <asp:TextBox ID="txt_diploy_emp" runat="server"
                                     class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="container">
                                            <asp:Panel ID="Panel4" runat="server">

                                                <asp:GridView ID="grd_client" class="table" runat="server" BackColor="White"
                                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                    AutoGenerateColumns="False" Width="100%" OnRowDataBound="grd_client_RowDataBound" OnPreRender="grd_client_PreRender">
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                    <Columns>


                                                        <%--                                        <asp:TemplateField HeaderText="Sr.No">
                                       <ItemTemplate>
                                            <asp:Label ID="lbl_GRADE_CODE" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                </asp:TemplateField>--%>
                                                        <asp:BoundField DataField="STATE" HeaderText="State" SortExpression="STATE" />
                                                        <asp:BoundField DataField="DESIGNATION" HeaderText="Designation" SortExpression="DESIGNATION" />
                                                        <asp:BoundField DataField="COUNT" HeaderText="Count" SortExpression="COUNT" />
                                                        <asp:BoundField DataField="HOURS" HeaderText="Hours" SortExpression="HOURS" />


                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="container">
                                            <asp:Panel ID="Panel7" runat="server" CssClass="grid-view panel-body">
                                                <asp:GridView ID="grd_client_pending" class="table" runat="server" Width="100%" ForeColor="#333333" ShowHeaderWhenEmpty="True">
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>

                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                    <div id="menu2">
                        <br />
                        <br />
                        <asp:Panel runat="server" ID="panel_banch_location">
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel1">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Client Name :</b>
                                              <asp:DropDownList ID="ddl_client_b" runat="server" Width="100%" AutoPostBack="true"
                                                  class=" form-control" OnSelectedIndexChanged="ddl_client_b_SelectedIndexChanged">
                                              </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> State Name :</b>
                                              <asp:DropDownList ID="ddl_state_b" runat="server" Width="100%" AutoPostBack="true"
                                                  class=" form-control" OnSelectedIndexChanged="ddl_state_b_SelectedIndexChanged">
                                              </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Branch Name :</b>
                                              <asp:DropDownList ID="ddl_branch_b" runat="server" Width="100%" AutoPostBack="true"
                                                  class=" form-control" OnSelectedIndexChanged="ddl_branch_b_OnSelectedIndexChanged">
                                              </asp:DropDownList>
                                        </div>

                                        <div class="col-sm-2 col-xs-12">
                                           <b> Admin Staff :</b>
                                 <asp:TextBox ID="txt_admin" runat="server"
                                     class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Operation Staff :</b>
                                 <asp:TextBox ID="txt_operation" runat="server"
                                     class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Finance Staff :</b>
                                 <asp:TextBox ID="txt_fainance" runat="server"
                                     class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="container">
                                            <asp:Panel ID="Panel2" runat="server" CssClass="grid-view1">
                                                <asp:GridView ID="location_branch" class="table" runat="server" BackColor="White"
                                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                    AutoGenerateColumns="False" Width="100%" OnRowDataBound="location_branch_DataBound" OnPreRender="location_branch_PreRender">
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                    <Columns>
                                                        <%-- <asp:TemplateField HeaderText="Sr.No">
                                       <ItemTemplate>
                                            <asp:Label ID="lbl_GRADE_CODE" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                </asp:TemplateField>
                                                        --%>
                                                        <asp:BoundField DataField="UNIT_NAME" HeaderText="Branch Name" SortExpression="UNIT_NAME" />
                                                        <asp:BoundField DataField="UNIT_CITY" HeaderText="Branch City" SortExpression="UNIT_CITY" />
                                                        <asp:BoundField DataField="DESIGNATION" HeaderText="Designation" SortExpression="DESIGNATION" />
                                                        <asp:BoundField DataField="HOURS" HeaderText="Working Hours" SortExpression="HOURS" />
                                                        <asp:BoundField DataField="Emp_count" HeaderText="Employee Count" SortExpression="Emp_count" />
                                                        <asp:BoundField DataField="File_No" HeaderText="Remaining Count" SortExpression="File_No" />


                                                        <asp:TemplateField HeaderText="LOCATION HEAD">
                                                            <ItemTemplate>
                                                                Name:
                                                    <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("LocationHead_Name") %>'></asp:TextBox>
                                                                Mobile No:
                                                    <asp:TextBox ID="TextBox2" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("LocationHead_mobileno") %>'></asp:TextBox>
                                                                <br />
                                                                Email ID:
                                                    <asp:TextBox ID="TextBox3" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("LocationHead_EmailId") %>'></asp:TextBox>
                                                                <br />



                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="OPERATION HEADS">
                                                            <ItemTemplate>
                                                                Name:
                                                    <asp:TextBox ID="TextBox4" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("OperationHead_Name") %>'></asp:TextBox>
                                                                Mobile No
                                                    <asp:TextBox ID="TextBox5" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("OperationHead_Mobileno") %>'></asp:TextBox>
                                                                <br />
                                                                Email ID:
                                                    <asp:TextBox ID="TextBox6" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("OperationHead_EmailId") %>'></asp:TextBox>
                                                                <br />



                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="FINANCE HEAD">
                                                            <ItemTemplate>
                                                                Name:
                                                    <asp:TextBox ID="TextBox7" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("FinanceHead_Name") %>'></asp:TextBox>
                                                                Mobile No:
                                                    <asp:TextBox ID="TextBox8" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("FinanceHead_Mobileno") %>'></asp:TextBox>
                                                                <br />
                                                                Email ID:
                                                    <asp:TextBox ID="TextBox9" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("FinanceHead_EmailId") %>'></asp:TextBox>
                                                                <br />



                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ADMIN HEAD">
                                                            <ItemTemplate>
                                                                Name:
                                                    <asp:TextBox ID="TextBox13" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("adminhead_name") %>'></asp:TextBox>
                                                                Mobile No:
                                                    <asp:TextBox ID="TextBox14" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("adminhead_mobile") %>'></asp:TextBox>
                                                                <br />
                                                                Email:
                                                    <asp:TextBox ID="TextBox15" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("adminhead_email") %>'></asp:TextBox>
                                                                <br />



                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="OTHER">
                                                            <ItemTemplate>
                                                                Name:
                                                    <asp:TextBox ID="TextBox10" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("OtherHead_Name") %>'></asp:TextBox>
                                                                Mobile_NO:
                                                    <asp:TextBox ID="TextBox11" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("OtherHead_Monileno") %>'></asp:TextBox>
                                                                <br />
                                                                Email:
                                                    <asp:TextBox ID="TextBox12" runat="server" ReadOnly="true" Width="238px" Text='<%# Eval("OtherHead_Emailid") %>'></asp:TextBox>
                                                                <br />



                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>



                                            </asp:Panel>
                                            <br />
                                            <br />
                                            <asp:GridView ID="location_branch_pending" class="table" runat="server" BackColor="White"
                                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                AutoGenerateColumns="False" Width="100%" OnRowDataBound="location_branch_pending_RowDataBound">

                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                <Columns>
                                                    <asp:BoundField DataField="UNIT_NAME" HeaderText="Branch Name" SortExpression="UNIT_NAME" />
                                                    <asp:BoundField DataField="UNIT_CITY" HeaderText="Branch City" SortExpression="UNIT_CITY" />
                                                    <%--<asp:BoundField DataField="DESIGNATION" HeaderText="Designation" SortExpression="DESIGNATION" />--%>
                                                    <asp:BoundField DataField="Client_branch_code" HeaderText="Client Branch Code" SortExpression="Client_branch_code" />
                                                    <asp:BoundField DataField="OPus_NO" HeaderText="Opus Code" SortExpression="OPus_NO" />
                                                    <asp:BoundField DataField="txt_zone" HeaderText="Zone" SortExpression="`txt_zone`" />
                                                    <asp:BoundField DataField="ZONE" HeaderText="Region Name" SortExpression="`ZONE`" />
                                                    <asp:BoundField DataField="UNIT_EMAIL_ID" HeaderText="Branch Email Id" SortExpression="UNIT_EMAIL_ID" />

                                                </Columns>

                                            </asp:GridView>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                    <div id="menu3">
                        <asp:Panel runat="server" ID="panel_employee">
                            <br />
                            
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="employee">
                                <ContentTemplate>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                          <b>  Client Name :</b>
                                              <asp:DropDownList ID="DropDownList1" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                                  class=" form-control">
                                              </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> State Name :</b>
                                              <asp:DropDownList ID="DropDownList2" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                                                  class=" form-control">
                                              </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Brach Name :</b>
                                              <asp:DropDownList ID="DropDownList3" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                                                  class=" form-control">
                                              </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Admin Staff :</b>
                                 <asp:TextBox ID="txt_emp_admin" runat="server"
                                     class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                          <b>  Operation Staff :</b>
                                 <asp:TextBox ID="txt_op_name" runat="server"
                                     class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Finance Staff :</b>
                                 <asp:TextBox ID="txt_emp_finance" runat="server"
                                     class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <br />

                                    <div class="row">
                                        <div class="container">
                                            <asp:Panel ID="Panel3" runat="server" CssClass="grid">
                                                <asp:GridView ID="employee_grd" class="table" runat="server" BackColor="White"
                                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                    AutoGenerateColumns="False" Width="100%" OnRowDataBound="employee_grd_RowDataBound">

                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                    <Columns>


                                                        <%--                                        <asp:TemplateField HeaderText="Sr.No">
                                       <ItemTemplate>
                                            <asp:Label ID="lbl_GRADE_CODE" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                </asp:TemplateField>--%>
                                                        <asp:BoundField DataField="emp_name" HeaderText="NAME" SortExpression="emp_name" />
                                                        <asp:BoundField DataField="EMP_MOBILE_NO" HeaderText="MOB_NO" SortExpression="EMP_MOBILE_NO" />
                                                        <asp:BoundField DataField="original_bank_account_no" HeaderText="ORIGNAL_A/C NO" SortExpression="original_bank_account_no" />
                                                        <asp:BoundField DataField="PF_NOMINEE_RELATION" HeaderText="NOMINEE NAME" SortExpression="PF_NOMINEE_RELATION" />
                                                        <asp:BoundField DataField="PF_NOMINEE_NAME" HeaderText="NOMINEE RELATION" SortExpression="PF_NOMINEE_NAME" />

                                                        <asp:BoundField DataField="original_photo" HeaderText="ORIGINAL PHOTO" SortExpression="original_photo" />
                                                        <asp:BoundField DataField="original_adhar_card" HeaderText="ORIGINAL ADHAR CARD" SortExpression="original_adhar_card" />
                                                        <asp:BoundField DataField="original_policy_document" HeaderText="ORIGINAL POLICE VERIFICATION " SortExpression="original_policy_document" />
                                                        <asp:BoundField DataField="original_address_proof" HeaderText="ORIGINAL PROOF OF ADDRESS" SortExpression="original_address_proof" />
                                                        <asp:BoundField DataField="bank_passbook" HeaderText="BANK PASSBOOK" SortExpression="bank_passbook" />
                                                        <asp:BoundField DataField="emp_signature" HeaderText="EMPLOYEE SIGNATURE" SortExpression="emp_signature" />
                                                        <asp:BoundField DataField="UNIFORM" HeaderText="UNIFORM" SortExpression="UNIFORM" />
                                                        <asp:BoundField DataField="ID CARD" HeaderText="ID CARD" SortExpression="ID CARD" />
                                                        <asp:BoundField DataField="SWEATER" HeaderText="SWEATER" SortExpression="SWEATER" />


                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>


                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </asp:Panel>


                    </div>
                    <div id="menu4">
                        <asp:Panel runat="server" ID="panel_billing">
                            <br />

                            <div id="attendance_remain" style="background: white; border-radius: 10px; padding:20px 20px 20px 20px; border: 1px solid white">
                                <asp:HiddenField ID="HiddenField1" Value="0" runat="server" />
                                <ul>
                                    <li><a href="#m1" runat="server"><b>Attendance Remaining</b></a></li>
                                    <li><a href="#m2" runat="server"><b>Policy Remaining</b></a></li>
                                </ul>
                                <div id="m1">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <br />
                                            <div class="row">

                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Client Name :</b>
                                              <asp:DropDownList ID="ddl_client_attendance" runat="server" Width="100%" AutoPostBack="true"
                                                  class=" form-control" OnSelectedIndexChanged="ddl_client_attendance_SelectedIndexChanged">
                                              </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  State Name :</b>
                                              <asp:DropDownList ID="ddl_unitcode1" runat="server" Width="100%" AutoPostBack="true"
                                                  class=" form-control">
                                              </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Date :</b>
                                    <asp:TextBox ID="txt_date1" runat="server" CssClass="form-control date-picker1"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12" style="margin-top: 16px;">
                                                    <asp:Button ID="btn_attendance_search" runat="server" Text="Search" class="btn btn-primary" OnClick="btn_attendance_search_Click" OnClientClick="return validation();" />
                                                </div>

                                            </div>

                                            <br />

                                            <br />
                                            <br />
                                            <asp:Panel runat="server" ID="panel17" CssClass="grid-view" Width="100%">
                                                <div class="row">
                                                    <div class="col-sm-3 col-xs-12"></div>
                                                    <div class="col-sm-6 col-xs-12">
                                                        <asp:GridView ID="gridService" runat="server" AutoGenerateColumns="false" ShowFooter="false" OnRowDataBound="gridService_RowDataBound" Width="100%" Style="text-align: center; height: 180px">
                                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="40px" CssClass="text-uppercase" />
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EFF3FB" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                            <Columns>
                                                                <asp:BoundField DataField="CUNIT" HeaderText="Branches" SortExpression="CUNIT" HeaderStyle-CssClass="text-center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>




                                </div>
                                <div id="m2">
                                    <br />
                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel3">
                                        <ContentTemplate>
                                            <br />
                                            <div class="row">

                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Client Name :</b>
                                              <asp:DropDownList ID="client_Billing" runat="server" Width="100%" AutoPostBack="true"
                                                  class=" form-control" OnSelectedIndexChanged="client_Billing_SelectedIndexChanged">
                                              </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    <b>State Name :</b>
                                              <asp:DropDownList ID="state_Billing" runat="server" Width="100%" AutoPostBack="true"
                                                  class=" form-control" OnSelectedIndexChanged="state_Billing_SelectedIndexChanged">
                                              </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-3 col-xs-12 text-left">
                                                   <b> Branch Having Policy :</b>
                                                <asp:ListBox ID="ddl_unitcode" runat="server" class="form-control pr_state js-example-basic-single text_box" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                                </div>
                                                <div class="col-sm-3 col-xs-12 text-left">
                                                  <b>  Branch Not Having Policy :</b>
                                                <asp:ListBox ID="ddl_unitcode_without" runat="server" class="form-control pr_state js-example-basic-single text_box" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                                </div>
                                            </div>

                                            <asp:GridView ID="Billing_attendance" class="table" runat="server" BackColor="White"
                                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                AutoGenerateColumns="False" Width="100%">

                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                <Columns>

                                                    <asp:BoundField DataField="UNIT_EMAIL_ID" HeaderText="Attendance Pending Branch Name" SortExpression="UNIT_EMAIL_ID" />

                                                </Columns>
                                            </asp:GridView>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <br />


                        </asp:Panel>


                    </div>
                    <div id="menu5">
                        <asp:Panel runat="server" ID="panel_salary">
                            <br />
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel4">
                                <ContentTemplate>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Client:</b><span style="color: red">*</span>

                                            <asp:DropDownList ID="ddl_client_sallary" runat="server"
                                                class="form-control" AutoPostBack="true" MaxLength="10" OnSelectedIndexChanged="ddl_client_sallary_SelectedIndexChanged">
                                                <%--<asp:ListItem Value="0" Text="&lt;Select&gt;" Enabled="True"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                          <b>  State:</b><span style="color: red">*</span>

                                            <asp:DropDownList ID="ddl_state_sallary" runat="server"
                                                class="form-control" MaxLength="20">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Date :</b><span style="color: red">*</span>
                                            <asp:TextBox ID="txt_date" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 col-xs-12" style="margin-top: 16px;">
                                            <asp:Button ID="btn_search" runat="server" Text="Search" class="btn btn-primary" OnClick="btn_search_Click" OnClientClick="return R_validation();" />
                                        </div>

                                        <div class="col-sm-3 col-xs-12 text-left">
                                           <b> Branch Not Having Bill Process :</b> 
                                                <asp:ListBox ID="Bill_not_process" runat="server" class="form-control pr_state js-example-basic-single text_box" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                        </div>
                                    </div>
                                    <br />
                                    <br />

                                    <asp:Panel ID="Panel6" runat="server" CssClass="grid">
                                        <asp:GridView ID="grd_sallary" class="table" runat="server" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            AutoGenerateColumns="False" Width="100%" OnRowDataBound="grd_sallary_RowDataBound" OnPreRender="grd_sallary_PreRender">


                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <Columns>

                                                <%-- <asp:TemplateField HeaderText="Sr No.">
                                       <ItemTemplate>
                                            <asp:Label ID="lbl_GRADE_CODE" runat="server" Text='<%# Eval("sr_no") %>'></asp:Label>
                                        </ItemTemplate>
                                </asp:TemplateField>--%>

                                                <asp:BoundField DataField="CLIENT_NAME" HeaderText="Client Name" SortExpression="CLIENT_NAME" />
                                                <asp:BoundField DataField="state" HeaderText="State" SortExpression="state" />

                                                <asp:BoundField DataField="billing_amt" HeaderText="Billing Amount" SortExpression="billing_amt" />
                                                <asp:BoundField DataField="month_year" HeaderText="Billing Month" SortExpression="month_year" ItemStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="recived_amt" HeaderText="Recived Amount" SortExpression="recived_amt" />
                                                <asp:BoundField DataField="balance_amt" HeaderText="Balance Amount" SortExpression="balance_amt" />
                                                <asp:BoundField DataField="balance_amt" HeaderText="Balance Amount" SortExpression="balance_amt" />

                                            </Columns>



                                        </asp:GridView>
                                    </asp:Panel>





                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                    <div id="menu6">
                        <asp:Panel runat="server" ID="panel_Employee_Compliances">
                            <br />
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel5">
                                <ContentTemplate>
                                    <br />
                                    <div id="Div1" class="row" runat="server">
                                        <div class="col-sm-2 col-xs-12">
                                          <b>  Client Name :</b>
                                              <asp:DropDownList ID="ddl_client_empCompliances" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_client_empCompliances_SelectedIndexChanged"
                                                  class=" form-control">
                                              </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                          <b>  State Name :</b>
                                              <asp:DropDownList ID="ddl_sate_empCompliances" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_sate_empCompliances_SelectedIndexChanged"
                                                  class=" form-control">
                                              </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Brach Name :</b>
                                              <asp:DropDownList ID="ddl_brachCompliances" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_brachCompliances_SelectedIndexChanged" class=" form-control">
                                              </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Admin Staff :</b>
                                 <asp:TextBox ID="txt_emp_co" runat="server"
                                     class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Operation Staff :</b>
                                 <asp:TextBox ID="txt_emp_co_name" runat="server"
                                     class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Finance Staff :</b>
                                 <asp:TextBox ID="txt_emp_co_finance" runat="server"
                                     class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="row">
                                            <br />
                                            <div class="col-sm-2 col-xs-12">

                                                <asp:TextBox ID="txt_count" runat="server"
                                                    class="form-control" ReadOnly="true" Visible="false"></asp:TextBox>


                                            </div>
                                        </div>






                                    </div>

                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="container">
                                            <asp:Panel ID="Panel5" runat="server" CssClass="grid">
                                                <asp:GridView ID="Grid_compliances" class="table" runat="server" BackColor="White"
                                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                    AutoGenerateColumns="False" Width="100%" OnRowDataBound="Grid_compliances_RowDataBound">

                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                    <Columns>


                                                        <%--                                        <asp:TemplateField HeaderText="Sr.No">
                                       <ItemTemplate>
                                            <asp:Label ID="lbl_GRADE_CODE" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                </asp:TemplateField>--%>
                                                        <asp:BoundField DataField="emp_name" HeaderText="NAME" SortExpression="emp_name" />
                                                        <%--<asp:BoundField DataField="" HeaderText="UAN NUMBER" SortExpression="" />--%>
                                                        <asp:BoundField DataField="EMP_NEW_PAN_NO" HeaderText="PAN NUM" SortExpression="EMP_NEW_PAN_NO" />
                                                        <asp:BoundField DataField="ESIC_NUMBER" HeaderText="ESIC NUM" SortExpression="ESIC_NUMBER" />
                                                        <asp:BoundField DataField="PF_NUMBER" HeaderText="PF NUM" SortExpression="PF_NUMBER" />

                                                        <asp:BoundField DataField="BANK_HOLDER_NAME" HeaderText="A/C HOLDER NAME" SortExpression="BANK_HOLDER_NAME" />
                                                        <asp:BoundField DataField="original_bank_account_no" HeaderText=" BANK A/C NUM" SortExpression="original_bank_account_no" />
                                                        <asp:BoundField DataField="PF_IFSC_CODE" HeaderText="BANK IFSC CODE" SortExpression="PF_IFSC_CODE" />
                                                        <asp:BoundField DataField="cca" HeaderText="CCA" SortExpression="cca" />
                                                        <asp:BoundField DataField="gratuity" HeaderText="GRATUITY" SortExpression="gratuity" />
                                                        <asp:BoundField DataField="special_allow" HeaderText="SPECIAL ALLOWANCE" SortExpression="special_allow" />



                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </asp:Panel>
                    </div>
                </div>
            </div>
            <br />
        </asp:Panel>
    </div>
</asp:Content>
