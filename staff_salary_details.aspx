<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="staff_salary_details.aspx.cs" EnableEventValidation="false"  Inherits="staff_salary_details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Staff Salary Details</title>
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
            $('#<%=ddl_unit.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
           
            
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_salary_deuction.ClientID%>').DataTable({
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
                .appendTo('#<%=gv_salary_deuction.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';
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

            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'mm/yy',
               
            });
        }
        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });

          
        });
        //save button
        function Req_validation() {

            var ddl_client_name = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_clientname = ddl_client_name.options[ddl_client_name.selectedIndex].text;

            if (Selected_ddl_clientname == "Select") {

                alert("Please select client name !!!");
                ddl_client_name.focus();
                return false;
            }

            var ddl_unit_name = document.getElementById('<%= ddl_unit.ClientID %>');
            var Selected_ddl_unitname = ddl_unit_name.options[ddl_unit_name.selectedIndex].text;

            if (Selected_ddl_unitname == "Select" || Selected_ddl_unitname == "") {
                alert("Please select unit name !!!");
                ddl_unit_name.focus();
                return false;
            }

            var ddl_emp_list = document.getElementById('<%=ddl_emp_list.ClientID %>');
            var Selected_ddl_emp_list = ddl_emp_list.options[ddl_emp_list.selectedIndex].text;

            if (Selected_ddl_emp_list == "Select" || Selected_ddl_emp_list == "") {

               alert("Please Select Employee !!");
               ddl_emp_list.focus();
               return false;
            }

            var ddl_Jobtype = document.getElementById('<%=ddl_Jobtype.ClientID %>');
            var Selected_ddl_Jobtype = ddl_emp_list.options[ddl_Jobtype.selectedIndex].text;

            if (Selected_ddl_Jobtype == "Select") {

                alert("Please Select Jobtype !!");
                ddl_Jobtype.focus();
                return false;
            }

            var ddl_employmentstatus = document.getElementById('<%=ddl_employmentstatus.ClientID %>');
            var Selected_ddl_employmentstatus = ddl_emp_list.options[ddl_employmentstatus.selectedIndex].text;

            if (Selected_ddl_employmentstatus == "Select") {

                alert("Please Select Employee Status !!");
                ddl_employmentstatus.focus();
                return false;
            }
           
            var txt_amount = document.getElementById('<%=txt_amount.ClientID %>');


            if (txt_amount.value == "" || txt_amount.value == "0") {

                alert("Please Enter Amount !!");
                txt_amount.value == "0";
                txt_amount.focus();
                return false;
            }
           //Snehal
            
             $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

        }

        // salary calculation validation

        function Req_salaryvalidation() {
            var ddl_client_name = document.getElementById('<%= ddl_client.ClientID %>');
            var selectedclientname = ddl_client_name.options[ddl_client_name.selectedIndex].text;

            if (selectedclientname =="Select") {
                alert("Please select client name !!!");
                ddl_client_name.focus();
                return false;
            }

            var ddl_unit_name = document.getElementById('<%= ddl_unit.ClientID %>');
            var selectedunitname = ddl_unit_name.options[ddl_unit_name.selectedIndex].text;

            if (selectedunitname == "Select") {
                alert("Please select unit name !!!");
                ddl_unit_name.focus();
                return false;

            }

            var selectmonthyear = document.getElementById('<%= txt_salarymonth.ClientID %>');

            if (selectmonthyear.value == "") {
                alert("Please select month year !!!");
                selectmonthyear.focus();
                return false;
            }

        }
        function openWindow() {
            window.open("html/staff_salary_details.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
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
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Staff Employee Salary </b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Staff Employee Salary Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>
            <div class="panel-body">
                <div class="container-fluid">
                    <div class="container-fluid" style="border: 1px solid #ddd9d9; background: #f3f1fe; margin-left:-10px; margin-right:-10px; padding-bottom:15px; margin-bottom:15px; border-radius: 10px;margin-bottom:20px;margin-top:20px">
                    <br />
                   
                     <div class="row">
                          <div class="col-sm-2 col-xs-12">
                            <b>Client Name:</b><asp:Label ID="Label2" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                            <asp:DropDownList runat="server" ID="ddl_client" CssClass="form-control text_box" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged1" AutoPostBack="true" meta:resourceKey="ddl_emp_liststatusResource1"></asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Unit Name:</b><asp:Label ID="Label1" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                            <asp:DropDownList runat="server" ID="ddl_unit" CssClass="form-control text_box" OnSelectedIndexChanged="ddl_unit_SelectedIndexChanged"  AutoPostBack="true" meta:resourceKey="ddl_emp_liststatusResource1"></asp:DropDownList>
                        </div>
                        </div>
                    <br />
                        </div>
                       <div id="tabs" style="background: #f3f1fe; border-radius: 10px;  margin-left:-10px; margin-right:-10px; margin-bottom:15px; margin-top:30px; padding:20px 20px 20px 20px; border: 1px solid #ddd9d9">
                        <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                        <ul>

                            <li><a href="#item1"><b>Employee Salary Details</b></a></li>
                            <li><a href="#item2"><b>Salary Calculations</b></a></li>
                            </ul>
                           <div id="item1">
                               <br />
                               <br />
                                 <div class="row">
                        <div class="col-sm-2 col-xs-12">
                           <b> Employee List:</b>
                            <asp:DropDownList runat="server" ID="ddl_emp_list" CssClass="form-control text_box" meta:resourceKey="ddl_emp_liststatusResource1"></asp:DropDownList>
                        </div> 

                         <div class="col-sm-2 col-xs-12">
                                                       <b>Job Type</b>
                                                        <asp:DropDownList ID="ddl_Jobtype" runat="server"  class="form-control text_box" meta:resourceKey="ddl_employmentstatusResource1">
                                                            <asp:ListItem meta:resourceKey="ListItemResource7" Value="Select" Text="Select"></asp:ListItem>
                                                            <asp:ListItem meta:resourceKey="ListItemResource6" Value="permanent" Text="Permanent"></asp:ListItem>
                                                            <asp:ListItem meta:resourceKey="ListItemResource5" Value="contract" Text="Contract"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        </div>
                                                   <div class="col-sm-2 col-xs-12">
                                                       <b>Status</b>
                                                        <asp:DropDownList ID="ddl_employmentstatus" runat="server"  class="form-control text_box" meta:resourceKey="ddl_employmentstatusResource1">
                                                            <asp:ListItem meta:resourceKey="ListItemResource7" Value="Select" Text="Select"></asp:ListItem>
                                                            <asp:ListItem meta:resourceKey="ListItemResource6" Value="monthly" Text="Monthly"></asp:ListItem>
                                                            <asp:ListItem meta:resourceKey="ListItemResource5" Value="daily" Text="Daily"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        </div>
                                                    <div class="col-sm-2 col-xs-12 ">
                                                       <b> CTC(Annual Income)</b>
                                                        <asp:TextBox ID="txt_amount" MaxLength="15"  runat="server" OnTextChanged="calculate"  AutoPostBack="true" class="text_box" onkeypress="return isNumber(event)" CssClass="form-control text_box" meta:resourcekey="txtlhead1Resource1" Width="100%">0</asp:TextBox></td>
                                                    </div>
                    </div>
                    <br />
                               <br />
                               <br />

                    <div class="row">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                        <div class="col-sm-6 col-xs-12">
                            <table border="1" style="border-color: gray" class="table table-striped">
                                <tr style="background-color:#7e7a7a;color:white">
                                    <th class="text-center">
                                        <h4>Earning Heads</h4>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <div class="row">
                                             <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Label ID="lblhead1" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head1"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head1" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                  <asp:Label ID="lblhead2" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head2"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head2" CssClass="form-control text_box"></asp:TextBox></div>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <div class="row">
                                             <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                 <asp:Label ID="lblhead3" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head3"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head3" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                 <asp:Label ID="lblhead4" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head4"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head4" CssClass="form-control text_box"></asp:TextBox></div>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <div class="row">
                                             <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                 <asp:Label ID="lblhead5" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head5"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head5" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                 <asp:Label ID="lblhead6" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head6"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head6" CssClass="form-control text_box"></asp:TextBox></div>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <div class="row">
                                             <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                 <asp:Label ID="lblhead7" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head7"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head7" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                 <asp:Label ID="lblhead8" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head8"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head8" CssClass="form-control text_box"></asp:TextBox></div>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <div class="row">
                                             <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                 <asp:Label ID="lblhead9" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head9"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head9" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                 <asp:Label ID="lblhead10" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head10"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head10" CssClass="form-control text_box"></asp:TextBox></div>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <div class="row">
                                             <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                 <asp:Label ID="lblhead11" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head11"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head11" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                 <asp:Label ID="lblhead12" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head12"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head12" CssClass="form-control text_box"></asp:TextBox></div>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <div class="row">
                                             <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                 <asp:Label ID="lblhead13" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head13"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head13" CssClass="form-control text_box"></asp:TextBox></div>
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                 <asp:Label ID="lblhead14" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head14"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head14" CssClass="form-control text_box"></asp:TextBox></div>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <div class="row">
                                             <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                 <asp:Label ID="lblhead15" runat="server" meta:resourcekey="lblhead12Resource1" Text="Head15"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_head15" CssClass="form-control text_box"></asp:TextBox></div>
                                       <div class="col-sm-2 col-xs-12"></div>
                                            <div class="col-sm-3 col-xs-12">
                                                 <asp:Label ID="lbl_total" runat="server" meta:resourcekey="lblhead12Resource1" Text="Total"></asp:Label>
                                                <asp:TextBox runat="server" ID="txt_total" CssClass="form-control text_box"></asp:TextBox></div>
                                             </div>
                                    </th>
                                </tr>
                            </table>
                        </div>
                                                </ContentTemplate>
                            </asp:UpdatePanel>
                        <div class="col-sm-6 col-xs-12">
                            <div class="row">
                                  <div class="col-sm-4 col-xs-12">
                            <table border="1" style="border-color: gray" class="table table-striped">
                                <tr style="background-color:#7e7a7a;color:white">
                                    <th>
                                        <h4>Lumpsum Heads</h4>
                                    </th>
                                </tr>
                                <tr>
                                    <th> <asp:Label ID="lbl_lhead1" runat="server" meta:resourcekey="lblhead12Resource1" Text="L_Head1"></asp:Label>
                                    <asp:TextBox ID="txt_lhead1" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                         <asp:Label ID="lbl_lhead2" runat="server" meta:resourcekey="lblhead12Resource1" Text="L_Head2"></asp:Label>
                                   <asp:TextBox ID="txt_lhead2" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                            <asp:Label ID="lbl_lhead3" runat="server" meta:resourcekey="lblhead12Resource1" Text="L_Head3"></asp:Label>            
                                 <asp:TextBox ID="txt_lhead3" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:Label ID="lbl_lhead4" runat="server" meta:resourcekey="lblhead12Resource1" Text="L_Head4"></asp:Label>                
                                   <asp:TextBox ID="txt_lhead4" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                               <asp:Label ID="lbl_lhead5" runat="server" meta:resourcekey="lblhead12Resource1" Text="L_Head5"></asp:Label>          
                                 <asp:TextBox ID="txt_lhead5" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>
                            </table>
</div>
                                   <div class="col-sm-8 col-xs-12">
                            <table border="1" style="border-color: gray" class="table table-striped">
                                <tr style="background-color:#7e7a7a;color:white">
                                    <th colspan="2" class="text-center">
                                        <h4>Deduction Heads</h4>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                                 <asp:Label ID="lbl_dhead1" runat="server" meta:resourcekey="lblhead12Resource1" Text="D_Head1"></asp:Label>       
                                          <asp:TextBox ID="txt_dhead1" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                    <th>
                                            <asp:Label ID="lbl_dhead2" runat="server" meta:resourcekey="lblhead12Resource1" Text="D_Head2"></asp:Label>            
                                       <asp:TextBox ID="txt_dhead2" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                       <asp:Label ID="lbl_dhead3" runat="server" meta:resourcekey="lblhead12Resource1" Text="D_Head3"></asp:Label>                 
                                      <asp:TextBox ID="txt_dhead3" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                    <th>
                                              <asp:Label ID="lbl_dhead4" runat="server" meta:resourcekey="lblhead12Resource1" Text="D_Head4"></asp:Label>          
                                    <asp:TextBox ID="txt_dhead4" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                             <asp:Label ID="lbl_dhead5" runat="server" meta:resourcekey="lblhead12Resource1" Text="D_Head5"></asp:Label>            
                               <asp:TextBox ID="txt_dhead5" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:Label ID="lbl_dhead6" runat="server" meta:resourcekey="lblhead12Resource1" Text="D_Head6"></asp:Label>                
                                    <asp:TextBox ID="txt_dhead6" runat="server" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15" class="form-control text_box" Width="100%"></asp:TextBox>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                               <asp:Label ID="lbl_dhead7" runat="server" meta:resourcekey="lblhead12Resource1" Text="D_Head7"></asp:Label>         
                                         <asp:TextBox ID="txt_dhead7" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                    <th>
                                           <asp:Label ID="lbl_dhead8" runat="server" meta:resourcekey="lblhead12Resource1" Text="D_Head8"></asp:Label>            
                                     <asp:TextBox ID="txt_dhead8" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>

                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                       <asp:Label ID="lbl_dhead9" runat="server" meta:resourcekey="lblhead12Resource1" Text="D_Head9"></asp:Label>                 
                                 <asp:TextBox ID="txt_dhead9" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:Label ID="lbl_dhead10" runat="server" meta:resourcekey="lblhead12Resource1" Text="D_Head10"></asp:Label>                
                                       <asp:TextBox ID="txt_dhead10" runat="server" class="form-control text_box" Width="100%" onkeypress="return AllowAlphabet_Number(event)" MaxLength="15"></asp:TextBox>
                                    </th>
                                </tr>

                            </table>
                                       </div>
                                </div>
                            <div class="row">
                            <table border="1" style="border-color: gray;margin-top:-5px;" class="table table-striped">
                                <tr style="background-color:#7e7a7a;color:white">
                                    <th colspan="2" class="text-center">
                                        <h4>Tax Deductions</h4>
                                    </th>
                                </tr>
                                <tr>
                                   <th>
                                       PF Employee:
                                       <asp:DropDownList runat="server" ID="ddl_pfemployee" CssClass="form-control text_box">
                                           <asp:ListItem Value="yes">Yes</asp:ListItem>
                                             <asp:ListItem Value="no">No</asp:ListItem>
                                       </asp:DropDownList>
                                   </th>
                                     <th>
                                       PF Employer:
                                       <asp:DropDownList runat="server" ID="ddl_pfemployer" CssClass="form-control text_box">
                                            <asp:ListItem Value="yes">Yes</asp:ListItem>
                                             <asp:ListItem Value="no">No</asp:ListItem>
                                       </asp:DropDownList>
                                   </th>
                                    </tr>
       <tr>
                                   <th>
                                       ESIC Employee:
                                       <asp:DropDownList runat="server" ID="ddl_esicemployee" CssClass="form-control text_box">
                                            <asp:ListItem Value="yes">Yes</asp:ListItem>
                                             <asp:ListItem Value="no">No</asp:ListItem>
                                       </asp:DropDownList>
                                   </th>
                                     <th>
                                       ESIC Employer:
                                       <asp:DropDownList runat="server" ID="ddl_esicemployer" CssClass="form-control text_box">
                                            <asp:ListItem Value="yes">Yes</asp:ListItem>
                                             <asp:ListItem Value="no">No</asp:ListItem>
                                       </asp:DropDownList>
                                   </th>
                                    </tr>
                                <tr>
                                <th>LWF
                                 <asp:DropDownList runat="server" ID="ddl_lwf_flage" CssClass="form-control text_box">
                                            <asp:ListItem Value="yes">Yes</asp:ListItem>
                                             <asp:ListItem Value="no">No</asp:ListItem>
                                       </asp:DropDownList>
                                    </th>
                                 
                                 <th>LWF Actual / Monthly:
                                 <asp:DropDownList runat="server" ID="ddl_lwfmonthact" CssClass="form-control text_box">
                                            <asp:ListItem Value="monthly">Monthly</asp:ListItem>
                                             <asp:ListItem Value="actual">Actual</asp:ListItem>
                                       </asp:DropDownList>
                                    </th>
                                 </tr>
      </table>
                            </div>
                    </div>
                        </div>

                    <br />
                    <div class="row text-center">
                        <asp:Button ID="btn_add" runat="server" class="btn btn-primary" Text="Save" OnClientClick="return Req_validation();" OnClick="btn_save_click" />
                         <asp:Button ID="btn_update" runat="server" class="btn btn-primary" Text="Update" OnClick="btn_update_details"  Visible="false"/>
                        <asp:Button ID="btnclose" runat="server" class="btn btn-danger" Text="Close"  OnClick="btn_close_click1"/>
                    </div>
                    <br />
                               <div class="container-fluid" style="background: white; border-radius: 10px; margin-top:20px; border: 1px solid white; padding:20px 20px 20px 20px">
                    <asp:Panel ID="gv_staffsalary" runat="server" ScrollBars="Auto" CssClass="grid-view">
                        <asp:GridView ID="gv_salary_structure" class="table" runat="server"
                           Font-Size="X-Small"
                            ForeColor="#333333" OnPreRender="GridView1_files_PreRender" OnRowDataBound="gv_salary_structure_RowDataBound" 
                            OnSelectedIndexChanged="gv_salary_details_SelectedIndexChanged">

                             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" Width="50" CssClass="text-uppercase" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                        </asp:GridView>
                    </asp:Panel>
                                   </div>
                           </div>
                            <div id="item2">
                                <br />
                                <%--<asp:Button ID="btn_attendances" runat="server" class="btn btn-primary" Text="Employee Attendances" OnClick="btn_attendace_muster_click" />--%>
                                <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                    <b> Date :</b>
                                        <asp:TextBox ID="txt_salarymonth" runat="server" class="form-control date-picker1"></asp:TextBox>
                                    </div> 
                                    <br />
                                    <div class="col-sm-2 col-xs-12">
                                <asp:Button ID="btn_salary_calculation" runat="server" class="btn btn-large" Text="Salary Calculation" OnClick="btn_salary_process_click"  OnClientClick="return Req_salaryvalidation();"/>
                               </div>
                                    </div>
                                     <br />
                                <div class="container-fluid">
                                    <br />
                                    <div class="container-fluid" style="background: white; border-radius: 10px; margin-top:20px; border: 1px solid white; padding:20px 20px 20px 20px">
                                  <asp:Panel ID="Panel7" runat="server" ScrollBars="Auto" CssClass="grid-view">
                    <asp:GridView ID="gv_salary_deuction" class="table" runat="server" DataKeyNames="id" ForeColor="#333333" 
                        Font-Size="X-Small" OnPreRender="gv_salary_deuction_PreRender">
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
                           
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                                        </div>
                                    </div>
                            </div>
                           </div>
                  
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
