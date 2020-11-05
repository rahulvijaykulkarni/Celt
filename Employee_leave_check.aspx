<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Employee_leave_check.aspx.cs" EnableEventValidation="false"  Inherits="Employee_leave_check" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Employee Leave Details</title>
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
    <script src="datatable/jszip.min.js"></script>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {

            $('#<%=ddl_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            //$('#<%=ddl_unit.ClientID%>').change(function () {
              //  $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            //});
       

            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
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


            $(".date-picker1").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");

            var table = $('#<%=gv_salary_structure.ClientID%>').DataTable({
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
            .appendTo('#<%=gv_salary_structure.ClientID%>_wrapper .col-sm-6:eq(0)');

        }

       
        function leave_details_validation() {
            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

            if (Selected_ddl_client == "Select") {
                alert("Please Select Client Name");
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

            if (Selected_ddl_unit == "ALL") {
                alert("Please Select Unit Name");
                ddl_unit.focus();
                return false;
            }

            var txt_satrtdate = document.getElementById('<%=txt_satrtdate.ClientID %>');
            if (txt_satrtdate.value == "") {
                alert("Please Select Start Date");
                txt_satrtdate.focus();
                return false;
            }

           
            var txt_enddate = document.getElementById('<%=txt_enddate.ClientID %>');
            if (txt_enddate.value == "") {
                alert("Please Select End Date");
                txt_enddate.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }
        function openWindow() {

            window.open("html/Employee_leave_check.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');

        }
        
    </script>
    <style>
        .text_box {
            margin-top: 7px;
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
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Employee Leave Details </b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Employee Leave Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid">
                    <div class="container-fluid"  style="border: 1px solid #ddd9d9; background: #f3f1fe;  margin-left:-10px; margin-right:-10px; padding:20px 20px 20px 20px; margin-bottom:15px; border-radius: 10px;margin-bottom:20px;margin-top:20px">
                    <br />
                   
                     <div class="row">
                          <div class="col-sm-2 col-xs-12">
                            <b>Client Name:</b>
                              <asp:DropDownList runat="server" ID="ddl_client" CssClass="form-control" Width="100%" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged1" 
                                 AutoPostBack="true" ></asp:DropDownList>
                         </div>

                         <div class="col-sm-2 col-xs-12">
                       <b> State :</b>
                        <asp:DropDownList ID="ddl_state" runat="server" class="form-control" Width="100%" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" 
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>  

                        <div class="col-sm-2 col-xs-12">
                           <b> Unit Name:</b>
                            <asp:DropDownList ID="ddl_unit" runat="server" CssClass="form-control" Width="100%"  >
                         </asp:DropDownList>
                        </div>

                          <div class="col-sm-2 col-xs-12">
                       <b> From Date :</b>
                              <asp:TextBox ID="txt_satrtdate"  runat="server" class="form-control date-picker1"></asp:TextBox>
                    </div>

                    <div class="col-sm-2 col-xs-12">
                       <b> To Date :</b>
                                        <asp:TextBox ID="txt_enddate" runat="server" class="form-control date-picker2"></asp:TextBox>
                    </div>
                         
                     </div>

                    <br />
                    <br />
                    
                    <div class="row text-center">
                        <asp:Button ID="Button3" runat="server" Text="Show" class="btn btn-primary" OnClientClick="return leave_details_validation();"  OnClick="btn_show_click" 
                            />


                        

                        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnclose" runat="server" class="btn btn-danger" OnClick="btnclose_Click" Text="Close" CausesValidation="False" />
                   
                        </div>
                        </div>
                    <br />
                        </div>
                      
                       
                          
                            
                                <%--<asp:Button ID="btn_attendances" runat="server" class="btn btn-primary" Text="Employee Attendances" OnClick="btn_attendace_muster_click" />--%>
                                
                              
                                    
                                    <div class="container-fluid" style="background: white; border-radius: 10px; margin-top:20px; border: 1px solid white; padding:20px 20px 20px 20px">
                                  
                                    <%--    <asp:Panel ID="Panel7" runat="server" ScrollBars="Auto" CssClass="grid-view">
                    <asp:GridView ID="gv_salary_structure1" class="table" runat="server" DataKeyNames="leave_id" ForeColor="#333333" 
                        Font-Size="X-Small"  
                      
                        ><%--OnRowDataBound="gv_salary_structure_RowDataBound" OnRowDataBound="gv_salary_structure_RowDataBound" OnPreRender="GridView1_files_PreRender"--%>
                       <%-- <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                           
                       <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtn_approval" runat="server" CssClass="btn btn-primary" Text="Approval" OnClick="lnkbtn_approval_Click" ></asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                             --%>
                            <%--<asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtn_reject" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Reject" ></asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>--%>
                           <%-- <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:TextBox  ID="txt_comment" runat="server" class="form-control" Style="text-align: left" Text='<%# Eval("STATUS_COMMENT")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                            <%--<asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="Button1" Text="Select" runat="server" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" />
            </ItemTemplate>
        </asp:TemplateField>

                            <asp:TemplateField HeaderText=" Shoes Size">
                                        <ItemStyle Width="55px"  />
                                        <ItemTemplate>
                                        <asp:TextBox ID="txt_comment" runat="server" Style="width:55px" Text='<%# Eval("STATUS_COMMENT") %>' class="form-control" meta:resourceKey="txt_advance_paymentResource1" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                             <asp:BoundField DataField="client_name" HeaderText="Employee Name" SortExpression="client_name" ItemStyle-Width="300px" />
                                    <asp:BoundField DataField="client_name" HeaderText="Designation" SortExpression="client_name" ItemStyle-Width="100px" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                              --%>       
                                        <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-left:-10px; margin-right:-10px; margin-bottom:10px; margin-top:-10px">
                                         <asp:Panel ID="Panel4" runat="server" Style="overflow-x:auto;overflow-y:auto;height:300px;">
                            <asp:GridView ID="gv_salary_structure" class="table" runat="server" Font-Size="X-Small"
                                AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="leave_id"  OnRowDataBound="gv_salary_structure_RowDataBound" 
                                 Width="100%" OnPreRender="GridView1_files_PreRender">
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Approval">
                                         <ItemStyle Width="30px"  />
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkinv_approval" Text="Approval" Style="color:white;" runat="server" CssClass="btn btn-primary" OnClick="lnkbtn_approval_Click"  ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Reject">
                                         <ItemStyle Width="30px"  />
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkinv_reject" Text="Reject" Style="color:white;" runat="server" CssClass="btn btn-primary" OnClick="lnkbtn_reject_Click"  ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Comment">
                                        <ItemStyle Width="50px"  />
                                        <ItemTemplate>
                                        <asp:TextBox ID="txt_comment" runat="server"  Text='<%# Eval("status_comment") %>' class="form-control"  meta:resourceKey="txt_advance_paymentResource1" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="Leave Status" HeaderText="Leave Status" ItemStyle-Width="50px" />
                                    <asp:BoundField DataField="Client Name" HeaderText="Client Name" ItemStyle-Width="50px" />
                                    <asp:BoundField DataField="Unit Name" HeaderText="Unit Name" SortExpression="Unit Name" ItemStyle-Width="50px" />
                                    <asp:BoundField DataField="Employee Name" HeaderText="Employee Name" SortExpression="Employee Name" ItemStyle-Width="50px" />
                                    <asp:BoundField DataField="Grade Name" HeaderText="Grade Name" SortExpression="Grade Name" ItemStyle-Width="50px" />
                                    <asp:BoundField DataField="From Date" HeaderText="From Date" SortExpression="From Date" ItemStyle-Width="50px" />
                                    <asp:BoundField DataField="To Date" HeaderText="To Date" SortExpression="To Date" ItemStyle-Width="50px" />
                                    <asp:BoundField DataField="No of days Leave" HeaderText="No of days Leave" SortExpression="No of days Leave" ItemStyle-Width="50px" />
                                    <asp:BoundField DataField="Leave Reason" HeaderText="Leave Reason" SortExpression="Leave Reason" ItemStyle-Width="50px" />
                                    <asp:BoundField DataField="Leave apply date" HeaderText="Leave apply date" SortExpression="Leave apply date" ItemStyle-Width="50px" />
                                    
                                    
                                </Columns>
                            </asp:GridView>

                        </asp:Panel>
                                        </div>
                                       
                              </div>      
                            
                             
                          
                  
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

