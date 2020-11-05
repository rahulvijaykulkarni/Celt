<%@ Page Title="TO DO LIST" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ToDoList.aspx.cs" Inherits="ToDoList" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>TO DO LIST</title>
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
    <style>
        .container {
            max-width: 99%;
        }

        .grid-view {
            height: auto;
            max-height: 500px;
            overflow-y: auto;
            overflow-x: hidden;
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

        h5 {
            font-weight: bold;
            font-size: 15px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <script>

        $(function () {

            $('#<%=btn_search.ClientID%>').click(function () {

                   $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

               });


               $('#<%=btn_export_exel.ClientID%>').click(function () {

                   $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

               });

               $('#<%=btn_add.ClientID%>').click(function () {

                   $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

               });

               $('#<%=btn_edit.ClientID%>').click(function () {

                   $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

               });

               $('#<%=btn_delete.ClientID%>').click(function () {

                   $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

               });

               $('#<%=SearchGridView.ClientID%> td').click(function () {

                   $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

               });

           });

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=SearchGridView.ClientID%>', '#<%=gv_itemslist.ClientID%>').DataTable({
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
                  .appendTo('#<%=SearchGridView.ClientID%>_wrapper .col-sm-6:eq(0)', '#<%=gv_itemslist.ClientID%>_wrapper .col-sm-6:eq(0)');
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
        });
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

         $(document).ready(function () {


             var evt = null;
             isNumber(evt);
             var e = null;
             AllowAlphabet1(e);
         });
         function AllowAlphabet1(e) {
             isIE = document.all ? 1 : 0
             keyEntry = !isIE ? e.which : event.keyCode;
             if (((keyEntry >= '48') && (keyEntry <= '57')) || ((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '32') || keyEntry == '45' || (keyEntry == '8'))
                 return true;
             else {
                 // alert('Please Enter Only Character values.');
                 return false;
             }
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

         function validateFloatKeyPress(el) {
             var v = parseFloat(el.value);
             el.value = (isNaN(v)) ? '' : v.toFixed(2);
         }





         function validate() {

             var t_Task_Name = document.getElementById('<%=txt_Task_Name.ClientID %>');
             var t_Task_Description = document.getElementById('<%=txt_Task_Description.ClientID %>');
             var t_Start_Date = document.getElementById('<%=txt_Start_Date.ClientID %>');

             var t_Remind_Till = document.getElementById('<%=txt_Remind_Till.ClientID %>');

             var Reminder = document.getElementById('<%=ddl_Reminder.ClientID %>');
             var SelectedText1 = Reminder.options[Reminder.selectedIndex].text;


             if (t_Task_Name.value == "") {

                 alert("Please Enter the Task Name !!!");
                 t_Task_Name.focus();
                 return false;
             }


             //t_Task_Description

             if (t_Task_Description.value == "") {
                 alert("Please Enter Task Description !!!");
                 t_Task_Description.focus();
                 return false;
             }

             //----------------------------------

             //t_Start_Date  

             if (t_Start_Date.value == "") {
                 alert("Please Select Start Date !!!");
                 t_Start_Date.focus();
                 return false;
             }

             //t_Remind_Till  

             if (t_Remind_Till.value == "") {
                 alert("Please Select Remind Till (Date) !!!");
                 t_Remind_Till.focus();
                 return false;
             }
             if (SelectedText1 == "Select Frequency") {
                 alert("Please Select Reminder :  !!!");
                 Reminder.focus();
                 return false;
             }

             return true;
         }

         window.onfocus = function () {

             $.unblockUI();
         }

         function openWindow() {
             window.open("html/ToDoList.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
         }

    </script>

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>


        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>TO DO LIST</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>To Do List Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>

            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-bottom:20px; margin-top:20px">
                <div class="nav nav-tabs" style="background: #f3f1fe; ">
                   
                    
                    <li class="active"><a data-toggle="tab" href="#home"><b>To Do</b></a></li>
                    <li><a id="A1" data-toggle="tab" href="#menu1" runat="server"><b>Operation To Do</b></a></li>


                </div>
                <div class="tab-content">
                    <div id="home" class="tab-pane fade in active">
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-3 text-left" style="margin-left: 15px;">
                                <asp:TextBox ID="txt_ref_no" runat="server" placeholder="Search ToDo List" CssClass="form-control text_box"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <br />
                                <asp:Button ID="btn_search" runat="server" class="btn btn-primary" Text="Search" OnClick="btn_search_Click1" BackColor="#428BCA" />
                            </div>
                        </div>
                        <br />
                        <asp:Panel ID="Panel2" runat="server" CssClass="grid-view" class="panel-body">
                            <asp:GridView ID="SearchGridView" class="table" AutoGenerateColumns="False" runat="server"
                                OnRowDataBound="OnRowDataBound" Font-Size="X-Small" OnSelectedIndexChanged="SearchGridView_SelectedIndexChanged"
                                Width="100%" OnPreRender="SearchGridView_PreRender">
                                <%--OnRowEditing="RowEditing",OnRowDeleting="GridView1_RowDeleting"-    --%>
                                <Columns>
                                    <asp:CommandField meta:resourcekey="CommandFieldResource1" SelectText="&gt;" ShowSelectButton="True" />

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ID">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Task Name">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Task_Name" runat="server" Text='<%# Eval("Task_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Start Date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Start_Date" runat="server" Text='<%# Eval("Start_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Task Description">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Task_Description" runat="server" Text='<%# Eval("Task_Description")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Reminder">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Reminder" runat="server" Text='<%# Eval("Reminder")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>

                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#000066" BackColor="White" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                        </asp:Panel>
                        
                        <%--   <div class="row">
              <div class="col-sm-2 col-xs-12">
                  Task Name :
                  </div>
                <div class="col-sm-3 col-xs-12 ">
                 
                    <asp:TextBox ID="txt_Task_Name" runat="server" placeholder="Enter Task Name." class="form-control" ></asp:TextBox>
               </div>
              </div>--%>
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                                <%-- <label for="ex2">--%>
                                <b> Task Name :</b>
                                 <%--</label>--%>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_Task_Name" runat="server" class="form-control" onkeypress="return AllowAlphabet1(event)" MaxLength="30"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                               <b>  Task Description :</b>
                            </div>
                            <div class="col-sm-3 ">

                                <asp:TextBox ID="txt_Task_Description" runat="server" class="form-control" onkeypress="return AllowAlphabet1(event)" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                              <b>   Start Date :</b>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_Start_Date" runat="server" onkeypress="return isNumber(event)" class="form-control date-picker1" Style="display: inline"></asp:TextBox>

                                <%-- <asp:TextBox ID="txt_Start_Date" runat="server" Width="85%" class="form-control datepicker"    AutoPostBack="true" ></asp:TextBox>--%>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                              <b>   Remind Till :</b>
                            </div>

                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_Remind_Till" runat="server" Width="100%" class="form-control date-picker2" Style="display: inline"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                               <b>  Reminder :</b>
                            </div>
                            <div class="col-sm-3 ">

                                <asp:DropDownList ID="ddl_Reminder" class="form-control" runat="server">
                                    <asp:ListItem Value="Select Frequency">Select Frequency</asp:ListItem>
                                    <asp:ListItem Value="Daily">Daily</asp:ListItem>
                                    <asp:ListItem Value="Weekly">Weekly</asp:ListItem>
                                    <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                    <asp:ListItem Value="Quaterly">Quaterly</asp:ListItem>
                                    <asp:ListItem Value="Half Yearly">Half Yearly</asp:ListItem>
                                    <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <br />
                        <br />
                        <div class="row text-center" style="margin-left: 86px;">
                            <asp:Button ID="btn_add" runat="server" class="btn btn-primary" OnClientClick="return validate();" OnClick="btn_add_Click" Text=" Save " />
                            <asp:Button ID="btn_edit" runat="server" class="btn btn-primary" OnClientClick="return validate();" OnClick="btn_edit_Click" Text=" Update " />
                            <asp:Button ID="btn_delete" runat="server" class="btn btn-primary" OnClick="btn_delete_Click" Text=" Delete " />
                            <cc1:ConfirmButtonExtender ID="btn_delete_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure you want to delete record?"
                                Enabled="True" TargetControlID="btn_delete"></cc1:ConfirmButtonExtender>
                            <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" OnClick="btn_cancel_Click" Text=" Clear " />
                            <asp:Button ID="btn_export_exel" runat="server" class="btn btn-large" Text="Export To Excel" OnClick="btnexporttoexceldesignation_Click" />
                            <asp:Button ID="btnclose" runat="server" class="btn btn-danger" OnClick="btnclose_Click" Text="Close" />
                        </div>
                        <br />
                        
                    </div>
                    <div id="menu1" class="tab-pane fade">
                        <br />
                        <br />
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel4" runat="server" CssClass="grid-view">

                                        <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            OnRowDataBound="gv_itemslist_RowDataBound"
                                            AutoGenerateColumns="False" Font-Size="X-Small" OnPreRender="gv_itemslist_PreRender">

                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                            <Columns>
                                                <%--<Columns>--%>
                                                <asp:TemplateField HeaderText="Sr No.">
                                                    <ItemStyle Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:BoundField DataField="CLIENT_CODE" HeaderText="CLLIENT" SortExpression="CLIENT_CODE" />
                                                <asp:BoundField DataField="STATE" HeaderText="STATE" SortExpression="STATE" />
                                                <asp:BoundField DataField="UNIT_CODE" HeaderText="UNIT CODE" SortExpression="UNIT_CODE" />
                                                <asp:BoundField DataField="OPERATION_DATE" HeaderText="DATE" SortExpression="OPERATION_DATE" />
                                                <asp:BoundField DataField="START_TIME" HeaderText="START TIME" SortExpression="START_TIME" />
                                                <asp:BoundField DataField="END_TIME" HeaderText="END TIME" SortExpression="END_TIME" />
                                               
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                    </div>
                    </div>

                </div>
            </div>

        </asp:Panel>

    </div>



</asp:Content>
