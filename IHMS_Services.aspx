<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IHMS_Services.aspx.cs" Inherits="IHMS_Services" Title="IHMS Services" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Service Request</title>
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
        function pageLoad() {
            $(document).ready(function () {
                
                

                });

            var table = $('#<%=unitcomplaintGridView.ClientID%>').DataTable({
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
               .appendTo('#<%=unitcomplaintGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

       


                var table = $('#<%=gv_ihms_service.ClientID%>').DataTable({
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
                  .appendTo('#<%=gv_ihms_service.ClientID%>_wrapper .col-sm-6:eq(0)');

                $.fn.dataTable.ext.errMode = 'none';
                var table = $('#<%=gv_superviser.ClientID%>').DataTable({
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
                  .appendTo('#<%=gv_superviser.ClientID%>_wrapper .col-sm-6:eq(0)');

                $.fn.dataTable.ext.errMode = 'none';
           
                service();
            }
    </script>
    <style>
        .grid-view {
            max-height: 400px;
            height: auto;
            overflow-y: auto;
            overflow-x: hidden;
        }
    </style>
    <style>
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

        h5 {
            font-weight: bold;
            font-size: 15px;
        }

        .text-red {
            color: #f00;
        }

        #ctl00_cph_righrbody_document_upload {
            margin-left: -30px;
        }
    </style>
    <script>

      

    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            var evt = null;
            var e = null;
            AllowAlphabet_Number(e);

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

        function service() {
            var d_service_type = document.getElementById('<%= ddl_service_type1.ClientID %>');
            d_service_type.disabled = true;

        }

        function openWindow() {
            window.open("html/DepartmentMaster.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
        function Req_validation() {
            var ddl_client_name = document.getElementById('<%=ddl_client_name.ClientID %>');
            var Selected_ddl_client_name = ddl_client_name.options[ddl_client_name.selectedIndex].text;
            if (Selected_ddl_client_name == "Select") {
                alert("Please Select Client Name");
                ddl_client_name.focus();
                return false;
            }
            var ddl_service_type = document.getElementById('<%=ddl_service_type.ClientID %>');
            var Selected_ddl_service_type = ddl_service_type.options[ddl_service_type.selectedIndex].text;
            if (Selected_ddl_service_type == "Select") {
                alert("Please Select Service Type");
                ddl_service_type.focus();
                return false;
            }
            var ddl_vendor_type = document.getElementById('<%=ddl_vendor_type.ClientID %>');
            var Selected_ddl_vendor_type = ddl_vendor_type.options[ddl_vendor_type.selectedIndex].text;
            if (Selected_ddl_vendor_type == "Select") {
                alert("Please Select Vendor Type");
                ddl_vendor_type.focus();
                return false;
            }
            var txt_product_specification = document.getElementById('<%=txt_product_specification.ClientID %>');
            if (txt_product_specification.value == "") {
                alert("Please Enter Product Specificaton");
                txt_product_specification.focus();
                return false;
            }
            var txt_type_make = document.getElementById('<%=txt_type_make.ClientID %>');
            if (txt_type_make.value == "") {
                alert("Please Enter Type");
                txt_type_make.focus();
                return false;
            }
            var txt_model_no = document.getElementById('<%=txt_model_no.ClientID %>');
            if (txt_model_no.value == "") {
                alert("Please Enter Model Number");
                txt_model_no.focus();
                return false;
            }
            var txt_costing_amount = document.getElementById('<%=txt_costing_amount.ClientID %>');
            if (txt_costing_amount.value == "") {
                alert("Please Enter Costing Amount");
                txt_costing_amount.focus();
                return false;
            }

        }
        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <div class="container-fluid">


        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">

            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">

                        <div style="text-align: center; color: #fff; font-size: small;"><b>SERVICE REQUEST</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 15px;">
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Service Request Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>
            <div class="panel-body">
                <div id="tabs" style="background: #f3f1fe; padding:10px 10px 10px 10px; border: 1px solid #e2e2dd; margin-bottom:25px; margin-top:25px; border-radius:10px">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li><a href="#tabs-1"><b>Feedback</b></a></li>
                        <li><a href="#tabs-2"><b>Client Request</b></a></li>
                        <li><a href="#tabs-3"><b>Supervisor Request</b></a></li>
                        <li><a href="#tabs-4"><b>Raised Complaints</b></a></li>
                    </ul>

                    <div id="tabs-1">
                        <br />

                        <div class="row">
                            <div class="col-sm-2 col-xs-12"></div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Client Name : </b>
                                        <asp:DropDownList ID="ddl_client_name1" runat="server" Font-Size="X-Small" class="form-control" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged1" AutoPostBack="true">
                                            <%--OnSelectedIndexChanged="ddl_client_SelectedIndexChanged1" AutoPostBack="true"
                                            --%>
                                        </asp:DropDownList>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                              <b>  Zone : </b>
                                        <asp:DropDownList ID="ddl_zone_name" runat="server" Font-Size="X-Small" class="form-control" OnSelectedIndexChanged="ddl_zone_name_SelectedIndexChanged" AutoPostBack="true">
                                            <%--OnSelectedIndexChanged="ddl_state_SelectedIndexChanged1" AutoPostBack="true"--%>
                                        </asp:DropDownList>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Zone User Name : </b>
                                        <asp:DropDownList ID="ddl_zone_user" runat="server" Font-Size="X-Small" class="form-control" OnSelectedIndexChanged="ddl_zone_user_SelectedIndexChanged" AutoPostBack="true">
                                            <%--OnSelectedIndexChanged="ddl_state_SelectedIndexChanged1" AutoPostBack="true"--%>
                                        </asp:DropDownList>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Region User Name  : </b>
                                        <asp:DropDownList ID="ddl_region_user" runat="server" Font-Size="X-Small" class="form-control" OnSelectedIndexChanged="ddl_region_user_SelectedIndexChanged" AutoPostBack="true">
                                            <%--OnSelectedIndexChanged="ddl_state_SelectedIndexChanged1" AutoPostBack="true"--%>
                                        </asp:DropDownList>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Branch FeedBack : </b>
                                        <asp:DropDownList ID="ddl_branch_user" runat="server" Font-Size="X-Small" class="form-control" OnSelectedIndexChanged="ddl_branch_user_SelectedIndexChanged" AutoPostBack="true">
                                            <%--OnSelectedIndexChanged="ddl_state_SelectedIndexChanged1" AutoPostBack="true"--%>
                                        </asp:DropDownList>

                            </div>
                        </div>


                        <br />
                        <asp:Panel ID="Panel6" runat="server" CssClass="grid-view">
                            <asp:GridView ID="gv_feedback" class="table" runat="server" BackColor="White" AutoGenerateColumns="false" OnRowDataBound="gv_feedback_RowDataBound" OnRowDeleting="gv_feedback_RowDeleting" DataKeyNames="ID"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%">
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <Columns>

                                    <asp:CommandField ButtonType="Button" HeaderText="DELETE FEEDBACK"
                                        ControlStyle-CssClass="btn btn-primary"
                                        ShowDeleteButton="true" />
                                    <asp:BoundField DataField="USER ID" HeaderText="USER ID" SortExpression="user_id" />
                                    <asp:BoundField DataField="ID" HeaderText="USER" SortExpression="ID" />
                                    <asp:BoundField DataField="USER NAME" HeaderText="USER NAME" SortExpression="user_name" />
                                    <asp:BoundField DataField="FEEDBACK" HeaderText="FEEDBACK" SortExpression="FEEDBACK" />
                                    <asp:BoundField DataField="SUGGESTION" HeaderText="SUGGESTION" SortExpression="SUGGESTION" />
                                    <asp:BoundField DataField="FEEDBACK DATE" HeaderText="FEEDBACK DATE" SortExpression="FEEDBACK_DATE" />
                                    <asp:TemplateField HeaderText="Attachment File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("ATTACHMENT FILE") %>' runat="server" OnCommand="lnkDownload_Command"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </asp:Panel>


                    </div>
                    <div id="tabs-2">
                        <br />
                        <%--<asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                        <div class="row">
                            <div class="col-sm-2 col-xs-12"></div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Client :</b>
                                        <asp:TextBox ID="txt_client_name" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>Branch :</b>
                                        <asp:TextBox ID="txt_branch_name" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                            </div>

                            <asp:TextBox ID="txt_req_id" ReadOnly="true" runat="server" Visible="false" class="form-control"></asp:TextBox>
                            <asp:TextBox ID="txt_location" ReadOnly="true" Visible="false" runat="server" class="form-control"></asp:TextBox>

                            <div class="col-sm-2 col-xs-12">
                               <b> Services :</b>
                                <asp:DropDownList ID="ddl_service_type1" runat="server" class="form-control">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Electrical">Electrical</asp:ListItem>
                                    <asp:ListItem Value="Plumbing">Plumbing</asp:ListItem>
                                    <asp:ListItem Value="Carpentry">Carpentry</asp:ListItem>
                                    <asp:ListItem Value="Civil">Civil</asp:ListItem>
                                    <asp:ListItem Value="Pest_Control">Pest_Control</asp:ListItem>
                                    <asp:ListItem Value="HVAC">HVAC</asp:ListItem>

                                </asp:DropDownList>
                            </div>



                            <div class="col-sm-3 col-xs-12">
                               <b> Comment :</b>
                                    <asp:TextBox ID="txt_comment" TextMode="multiline" ReadOnly="true" Columns="5" Rows="2" runat="server" class="form-control"></asp:TextBox>
                            </div>

                        </div>

                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-12"></div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Request Date :</b>
                                    <asp:TextBox ID="txt_date" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> Status :</b>
                                    <asp:TextBox ID="txt_status" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <br />
                               <b> Upload Documents :</b>
                                                <br />
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <br />
                                <asp:FileUpload ID="client_documents_upload" runat="server" AllowMultiple="true" CssClass="text_box" />
                            </div>
                            <asp:TextBox ID="txt_unit_code" ReadOnly="true" Visible="false" runat="server" class="form-control"></asp:TextBox>


                        </div>

                        <br />
                        <br />
                        <div class="row">
                            <div class="col-sm-5 col-xs-12"></div>

                            <div class="col-lg-3 col-md-4 col-sm-6  col-xs-12">
                                <br />
                                <asp:Button ID="btn_submit" runat="server" class="btn btn-primary" Text=" Submit " OnClick="btn_submit_Click" />
                                <asp:Button ID="btn_clear" runat="server" class="btn btn-primary" Text="Clear" OnClick="btn_clear_Click" formnovalidate="formnovalidate" />
                                <asp:Button ID="btnclose0" runat="server" class="btn btn-danger" Text="Close" OnClick="btnclose_Click" formnovalidate="formnovalidate" />

                                <asp:Button ID="btn_merged" runat="server" class="btn btn-primary" Text="Merge" Visible="false" />
                            </div>
                        </div>

                        <br />
                        <br />

                        <%-- <asp:UpdatePanel runat="server">
                            <ContentTemplate>--%>
                        <asp:Panel ID="Panel1" runat="server" CssClass="grid-view">

                            <asp:GridView ID="gv_ihms_service" class="table" runat="server" BackColor="White" Width="100%"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                OnRowDataBound="gv_ihms_service_RowDataBound" OnSelectedIndexChanged="gv_ihms_service_SelectedIndexChanged" OnPreRender="gv_ihms_service_PreRender"
                                AutoGenerateColumns="False">
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
                                    <asp:BoundField DataField="id" HeaderText="ID" SortExpression="Id" />
                                    <asp:BoundField DataField="client_code" HeaderText="CLIENT" SortExpression="client_code" />
                                    <asp:BoundField DataField="unit_code" HeaderText="BRANCH" SortExpression="unit_code" />
                                    <asp:BoundField DataField="services" HeaderText="REQUEST SERVICE" SortExpression="services" />
                                    <asp:BoundField DataField="additional_comment" HeaderText="COMMENT" SortExpression="additional_comment" />
                                    <asp:BoundField DataField="date" HeaderText="REQUEST DATE" SortExpression="date" />
                                    <asp:BoundField DataField="status" HeaderText="STATUS" SortExpression="status" />
                                </Columns>
                            </asp:GridView>

                        </asp:Panel>
<%--                        </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        <br />
                        <div class="row">
                            <%--<asp:FileUpload ID="up_quotation" runat="server" Visible="false" meta:resourcekey="adhar_pan_uploadResource1" />--%>
                            <asp:TextBox ID="txt_desc" runat="server" Visible="false" class="form-control text_box"></asp:TextBox>
                        </div>
                        <br />


                        <br />
                        <div class="row text-center">
                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" meta:resourcekey="btnUploadResource1" Visible="false" OnClick="btnUpload_Click" Text="Upload" />
                            <%--<asp:Button ID="btn_add1" runat="server" class="btn btn-primary"
                         Text=" Save " OnClientClick="return Req_validation();"/>--%>
                            <%--<asp:Button ID="btn_edit" runat="server" class="btn btn-primary" 
                         Text=" Update " onclick="btn_edit_Click" OnClientClick="return Req_validation();"/>
                        <asp:Button ID="btn_delete" runat="server" class="btn btn-primary" Text=" Delete " 
                         onclick="btn_delete_Click" />
                    
                        <cc1:ConfirmButtonExtender ID="btn_delete_ConfirmButtonExtender" 
                            runat="server" ConfirmText="Are you sure you want to delete record?" 
                            Enabled="True" TargetControlID="btn_delete">
                        </cc1:ConfirmButtonExtender>
                        <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" Text=" Clear " 
                        onclick="btn_cancel_Click" />
                        <asp:Button ID="btnexporttoexceldepartment" runat="server" class="btn btn-primary" 
                        Text="Export To Excel" onclick="btnexporttoexceldepartment_Click" />--%>
                            <%-- <asp:Button ID="btnclose" runat="server" class="btn btn-danger" 
                                onclick="btnclose_Click" Text="Close" />--%>
                        </div>
                    </div>
                    <div id="tabs-3">
                        <br />

                        <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Client Name :</b>
                           <asp:DropDownList ID="ddl_client_name" DataValueField="client_code" DataTextField="client_name" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12 ">
                                           <b> Select State :</b>
                            <asp:DropDownList ID="ddl_state" DataValueField="state_name" DataTextField="state_name" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                                        </div>

                                        <div class="col-sm-2 col-xs-12">
                                           <b> Branch Name :</b>
                         <asp:DropDownList ID="ddl_branch_name" DataValueField="unit_code" DataTextField="UNIT_NAME" OnSelectedIndexChanged="ddl_branch_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <b>Service type :</b>
                          <asp:DropDownList ID="ddl_service_type" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet(event)" MaxLength="50">
                              <asp:ListItem Value="Select">Select</asp:ListItem>
                              <asp:ListItem Value="Electrical">Electrical</asp:ListItem>
                              <asp:ListItem Value="Plumbing">Plumbing</asp:ListItem>
                              <asp:ListItem Value="Carpentry">Carpentry</asp:ListItem>
                              <asp:ListItem Value="Civil">Civil</asp:ListItem>
                              <asp:ListItem Value="Pest_Control">Pest_Control</asp:ListItem>
                              <asp:ListItem Value="HVAC">HVAC</asp:ListItem>
                          </asp:DropDownList>
                                        </div>


                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Vendor type :</b>
                          <asp:DropDownList ID="ddl_vendor_type" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet(event)" MaxLength="50">
                              <asp:ListItem Value="Select">Select</asp:ListItem>
                              <asp:ListItem Value="Electrical">Electrical</asp:ListItem>
                              <asp:ListItem Value="Plumbing">Plumbing</asp:ListItem>
                              <asp:ListItem Value="Carpentry">Carpentry</asp:ListItem>
                              <asp:ListItem Value="Civil">Civil</asp:ListItem>
                              <asp:ListItem Value="Pest_Control">Pest_Control</asp:ListItem>
                              <asp:ListItem Value="HVAC">HVAC</asp:ListItem>
                          </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Vendor Name :</b>
                        <asp:DropDownList ID="ddl_vendor_name" runat="server" DataValueField="vend_id" DataTextField="vend_name" class="form-control text_box" onkeypress="return AllowAlphabet(event)" MaxLength="50"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Product specification :</b>
                        <asp:TextBox ID="txt_product_specification" runat="server" class="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-2 col-xs-12">
                                           <b> Type/Make :</b>
                        <asp:TextBox ID="txt_type_make" runat="server" class="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>
                                        </div>

                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Model No :</b>
                        <asp:TextBox ID="txt_model_no" runat="server" class="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Costing/Amount :</b>
                        <asp:TextBox ID="txt_costing_amount" runat="server" class="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 col-xs-12">
                                            <br />
                                          <b>  Documents :</b>
                                                <br />
                                            <asp:Label ID="photo" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <br />
                                            <asp:FileUpload ID="document_upload" runat="server" AllowMultiple="true" CssClass="text_box" />
                                        </div>

                                        <div class="col-sm-1 col-xs-12">
                                            <br />
                                            <asp:Label ID="lbl_id" runat="server" Visible="false" Text="Hello" />
                                        </div>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12"></div>
                                        <div class="col-sm-4 col-xs-12">
                                            <br />
                                            <br />
                                            <br />
                                            <asp:Button ID="btn_save_draft" runat="server" class="btn btn-primary" Text=" Save As Draft " OnClick="btn_save_draft_Click" OnClientClick=" return Req_validation();" />
                                            <asp:Button ID="btn_save_details" runat="server" class="btn btn-primary" Text=" Submit " OnClick="btn_save_details_Click" OnClientClick="return confirm('Once you Submit you cannot make changes?');" />
                                            <asp:Button ID="btn_clear_super" runat="server" class="btn btn-primary" Text="Clear" OnClick="btn_clear_super_Click" formnovalidate="formnovalidate" />
                                            <asp:Button ID="btn_close_super" runat="server" class="btn btn-danger" Text="Close" OnClick="btnclose_Click_superviser" formnovalidate="formnovalidate" />



                                        </div>


                                    </div>
                                    <br />
                                    <br />

                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" CssClass="grid-view">

                                        <asp:GridView ID="gv_superviser" runat="server" ForeColor="#333333" class="table" Width="100%"
                                            OnRowDataBound="gv_ihms_superviser_RowDataBound" OnPreRender="gv_superviser_PreRender"
                                            OnSelectedIndexChanged="superviserclient_SelectedIndexChanged">

                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                        </asp:GridView>

                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btn_save_draft" />
                                    <asp:PostBackTrigger ControlID="btn_save_details" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div id="tabs-4">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Client Name :</b>
                        <asp:DropDownList ID="ddlunitclient1" runat="server" DataValueField="client_Code" DataTextField="client_name" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlunitclient1_SelectedIndexChanged" />
                                    </div>

                                    <div class="col-sm-2 col-xs-12">
                                       <b> State Name :</b>
                        <asp:DropDownList ID="ddl_gv_statewise" runat="server" DataValueField="STATE_CODE" DataTextField="STATE_NAME" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlsatewises_SelectedIndexChanged" />
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                      <b>  Branch Name:</b>
                        <asp:DropDownList ID="ddl_gv_branchwise" runat="server" DataValueField="UNIT_CODE" DataTextField="UNIT_NAME" class="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlbeabchwise1_SelectedIndexChanged" />
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Comment Box :</b>
                                <asp:TextBox ID="txt_commentbox" runat="server" class="form-control text_box" TextMode="MultiLine" MaxLength="300" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <br />

                                <div class="row">
                                    <div class="col-sm-4 col-xs-12"></div>
                                    <div class="col-sm-4 col-xs-12"></div>

                                    <div class="col-sm-2 col-xs-12" style="">
                                        <span class="glyphicon glyphicon-plus" style="font-size: 20px; font-weight: bolder; height: 20px; padding-left: 5px; padding-right: 5px; color: #337ab7; border-radius: 20%;"></span>
                                        <asp:LinkButton ID="linkaddcatagory" runat="server" OnClick="linkaddcatagory_Click"
                                            Text="ADD NEW CATEGORY" Style="color: #000;"></asp:LinkButton>

                                        <asp:Button ID="Button2" runat="server" CssClass="hidden" Text="Location" />
                                        <asp:Button ID="Button6" runat="server" CssClass="hidden" OnClick="Button5_Click" />
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel5" TargetControlID="Button8"
                                            CancelControlID="Button7" BackgroundCssClass="Background">
                                        </cc1:ModalPopupExtender>
                                        <asp:Panel ID="Panel5" runat="server" CssClass="Popup" Style="display: none">
                                            <iframe style="width: 1000px; height: 500px; background-color: #fff;" id="Iframe2" src="Complaint_category.aspx" runat="server"></iframe>
                                            <div class="row text-center">
                                                <asp:Button ID="Button7" CssClass="btn btn-danger" runat="server" Text="Close" />
                                            </div>
                                            <asp:Button ID="Button8" runat="server" CssClass="hidden" Text="Add New Travelling Plan" />

                                            <br />

                                        </asp:Panel>

                                    </div>
                                    <div class="col-sm-2 col-xs-12" style="text-align: end;">
                                        <span class="glyphicon glyphicon-plus" style="font-size: 20px; font-weight: bolder; height: 20px; padding-left: 5px; padding-right: 5px; color: #337ab7; border-radius: 20%;"></span>
                                        <asp:LinkButton ID="lnkaddtravelplan" runat="server" OnClick="lnkaddtravelplan_Click"
                                            Text="ADD NEW COMPLAINTS" Style="color: #000;"></asp:LinkButton>

                                        <asp:Button ID="Button1" runat="server" CssClass="hidden" Text="Location" />
                                        <asp:Button ID="Button5" runat="server" CssClass="hidden" OnClick="Button5_Click" />
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel4" TargetControlID="lnkaddtravelplan"
                                            CancelControlID="Button4" BackgroundCssClass="Background">
                                        </cc1:ModalPopupExtender>
                                        <asp:Panel ID="Panel4" runat="server" CssClass="Popup" Style="display: none">
                                            <iframe style="width: 1000px; height: 500px; background-color: #fff;" id="Iframe1" src="Add_New_Complaints.aspx" runat="server"></iframe>
                                            <div class="row text-center">
                                                <asp:Button ID="Button4" CssClass="btn btn-danger" runat="server" Text="Close" />
                                            </div>
                                            <asp:Button ID="Button3" runat="server" CssClass="hidden" Text="Add New Travelling Plan" />

                                            <br />

                                        </asp:Panel>

                                    </div>
                                </div>

                                <br />
                                <asp:Panel ID="Panel11" runat="server" BackColor="White" Visible="False" style="overflow-y:auto" meta:resourcekey="Panel5Resource1">

                                    <asp:GridView ID="unitcomplaintGridView" class="table" Width="100%" AutoGenerateColumns="False" DataKeyNames="id" runat="server"
                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="unitcomplaintGridView_RowDataBound"
                                        CellPadding="3" meta:resourcekey="SearchGridViewResource1" OnPreRender="unitcomplaintGridView_PreRender">
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="No." SortExpression="Id" />
                                             <asp:BoundField DataField="client_name" HeaderText="client name" SortExpression="client_name" />
                                             <asp:BoundField DataField="ZONE" HeaderText="REGION" SortExpression="ZONE" />
                                             <asp:BoundField DataField="state_name" HeaderText="State" SortExpression="state_name" />
                                              <asp:BoundField DataField="unit_name" HeaderText="Branch name" SortExpression="unit_name" />
                                            <asp:BoundField DataField="complaint_name" HeaderText="Type Of Complaint" SortExpression="complaint_name" />
                                            <asp:BoundField DataField="priority" HeaderText="Priority" SortExpression="priority" />
                                            <asp:BoundField DataField="date" HeaderText="Complaint Raise Date" SortExpression="date" />
                                            <asp:BoundField DataField="resole_date" HeaderText="Complaint Resolved Date" SortExpression="resole_date" />
                                            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                            <asp:BoundField DataField="Remark" HeaderText="Remark From Client" ItemStyle-Width="200px" SortExpression="Remark" />


                                            <asp:TemplateField>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtn_edititemcomplaince" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Resolve"  OnClick="lnkbtn_edititemcomplaince_click" OnClientClick="return confirm('Are you sure want to resolve ?')"></asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>

            </div>

        </asp:Panel>
    </div>




</asp:Content>

