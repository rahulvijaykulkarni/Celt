<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Pay_Send_Email.aspx.cs" Inherits="Pay_Send_Email" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Employee Send Email</title>
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
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>
    <style>
      
    </style>

    <script>
        function unblock()
        {
            $.unblockUI();
        }
        $(function () {
            $('#<%=ddl_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_billing_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_unitcode.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_send_morningemail.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
        });
        $(document).ready(function () {

            $('.date-picker1').datepicker({
                changeMonth: true,
                changeYear: true,
                maxDate: 0,
                yearRange: "1990:+100",
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                onClose: function (dateText, inst) {


                }
            });
            $(".date-picker1").attr("readonly", "true");

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

        });


        function req_validation() {

            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;
            var t_UnitCode = document.getElementById('<%=ddl_unitcode.ClientID %>');
            var SelectedText = t_UnitCode.options[t_UnitCode.selectedIndex].text;
            var val_date = document.getElementById('<%=txttodate.ClientID %>');

            if (Selected_ddl_client == "Select") {
                alert("Please Select Client Name... ");
                ddl_client.focus();
                return false;
            }
            if (SelectedText == "ALL") {
                alert("Please Select Branch... ");
                t_UnitCode.focus();
                return false;
            }
            // Month/Year

            if (val_date.value == "") {
                alert("Please Select Month/Year ");
                val_date.focus();
                return false;
            }

            return true;
        }


        //////////////////////

      

        function req_validation1() {

           

            var ddl_client_attach = document.getElementById('<%=ddl_client_attach.ClientID %>');
            var Selected_ddl_client = ddl_client_attach.options[ddl_client_attach.selectedIndex].text;
            if (Selected_ddl_client == "Select") {
                alert("Please Select Client Name... ");
                ddl_client_attach.focus();
                return false;
            }


            var FileUpload_file = document.getElementById('<%=FileUpload1.ClientID %>');

            if (FileUpload_file.value == "") {
                alert("Please Select File ");
                FileUpload1.focus();
                return false;
            }


            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        


        //upload attendance
        function save_validate() {

            var document1 = document.getElementById('<%=txt_document1.ClientID %>');

            var filePath = document.getElementById('<%= this.document1_file.ClientID %>').value;

            if (document1.value == "") {
                alert("Please enter Description :  ");
                document1.focus();
                return false;
            }

            if (filePath.length < 1) {
                alert("Please select file.");
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function isNumber1(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode == 34 || charCode == 39) {
                    return false;
                }
                return true;
            }
        }

        function isNumberKey(evt, id) {
            try {
                var charCode = (evt.which) ? evt.which : event.keyCode;

                if (charCode == 46) {
                    var txt = document.getElementById(id).value;
                    if (!(txt.indexOf(".") > -1)) {

                        return true;
                    }
                }
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            } catch (w) {
                alert(w);
            }
        }
        function openWindow() {
            window.open("html/emp_sample.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

        function r_validation1() {
            if (req_validation()) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }
            else { return false; }

        }
        function validate() {

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            return true;


        }
        function confirm_box() {

            var elems = document.getElementById('<%=lnk_conformationmail.ClientID %>');

            var confirmIt = function (e) {
                if (!confirm('Are you sure?')) e.preventDefault();
            };
            for (var i = 0, l = elems.length; i < l; i++) {
                elems[i].addEventListener('click', confirmIt, false);
            }
        }

        function valid_grid() {
            var left_date_date = document.getElementById('left_date_date');
            var txt_emp_sample_left_reson = document.getElementById('txt_emp_sample_left_reson');
            if (left_date_date.value != "") {
                alert("Please Enter Reason");
                txt_emp_sample_left_reson.focus();
                return false;
            }


        }
        function required_validation() {
            var txttodate = document.getElementById('<%=txttodate.ClientID %>');
            if (txttodate.value == "") {
                alert("Please select month and year");
                txttodate.focus();
                return false;
            }
            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;
            if (Selected_ddl_client == "Select") {
                alert("Please select client");
                ddl_client.focus();
                return false;
            }
            var ddl_billing_state = document.getElementById('<%=ddl_billing_state.ClientID %>');
            var Selected_ddl_billing_state = ddl_billing_state.options[ddl_billing_state.selectedIndex].text;
            if (Selected_ddl_billing_state == "ALL") {
                alert("Please select state");
                ddl_billing_state.focus();
                return true;

            }
        }
        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
            var st = $(this).find("input[id*='HiddenField1']").val();
            if (st == null)
                st = 0;
            $('[id$=Div1]').tabs({ selected: st });
            var st = $(this).find("input[id*='HiddenField2']").val();
            if (st == null)
                st = 0;
            $('[id$=Div2]').tabs({ selected: st });

        });
    </script>
    <script type="text/javascript">
        function pageLoad() {

            


            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=grd_company_files.ClientID%>').DataTable({
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
                .appendTo('#<%=grd_company_files.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=attachment_gv.ClientID%>').DataTable({
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
                .appendTo('#<%=attachment_gv.ClientID%>_wrapper .col-sm-6:eq(0)');

            $('[id*=chk_gv_header]').click(function () {
                $("[id*='chk_client']").attr('checked', this.checked);
            });


        }
      
        function validateCheckBoxes() {
            var isValid = false;
            var gridView = document.getElementById('<%= gv_fullmonthot.ClientID %>');
             for (var i = 1; i < gridView.rows.length; i++) {
                 var inputs = gridView.rows[i].getElementsByTagName('input');
                 if (inputs != null) {
                     if (inputs[0].type == "checkbox") {
                         if (inputs[0].checked) {
                             isValid = true;
                             return true;
                         }
                     }
                 }
             }
             alert("Please select atleast one Employee");
            
             return false;
        }

        function openWindow() {
            window.open("html/Pay_Send_Email.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

    </script>
    <style>
        .row {
            margin: 0px;
        }

        .text-red {
            color: red;
        }

        .grid-view {
            max-height: 300px;
            overflow-x: hidden;
            overflow-y: auto;
            width: 100%;
        }

        .row {
            margin-right: 0px;
            margin-left: 0px;
        }
    </style>
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
                        <div style="text-align: center; color: #fff; font-size: small;"><b>EMPLOYEE SEND EMAIL</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
            <div id="tabs" style="background: beige;">
                <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                <ul>
                    <li><a id="A2" href="#menu0" runat="server">Send Email</a></li>
                    <li><a href="#menu5" id="A1" runat="server">Attachment</a></li>
                </ul>

                <div id="menu0">
                    <asp:HiddenField ID="hidden_month" runat="server" />
                    <asp:HiddenField ID="hidden_year" runat="server" />
                    <div class="container-fluid">
                        <div class="row">

                    <div class="col-sm-2 col-xs-12">
                       <b> Month / Year :<span class="text-red">*</span></b>
                        <asp:TextBox ID="txttodate" runat="server" Visible="true" class="form-control date-picker text_box"></asp:TextBox>

                    </div>
                    <div class="col-sm-2 col-xs-12">
                      <b>  Client Name : <span class="text-red">*</span></b>
                        <asp:DropDownList ID="ddl_client" DataValueField="client_code" DataTextField="client_name" AutoPostBack="true" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" runat="server" CssClass="form-control">
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12">
                       <b> State :</b>
                            <asp:DropDownList ID="ddl_billing_state" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged1" AutoPostBack="true" />
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Branch Name : <span class="text-red">*</span></b>
                        <asp:DropDownList ID="ddl_unitcode" runat="server" class="form-control" OnSelectedIndexChanged="ddl_unit_click" AutoPostBack="true">
                            <asp:ListItem Value="ALL">ALL</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12 ">

                        <asp:Label ID="lbl_branck_emaillist" runat="server" Text=" Branch Having Not EmailId :"></asp:Label>
                        <asp:ListBox ID="list_unitname" runat="server" SelectionMode="Multiple" Height="100" class="form-control text_box"></asp:ListBox>
                    </div>
                    <div class="col-sm-2 col-xs-12 ">

                        <asp:Label ID="Label1" runat="server" Visible="false" Text=" Branch Having Not Send Attendance Sheet :"></asp:Label>
                        <asp:ListBox ID="list_emailnotsend" runat="server" Visible="false" SelectionMode="Multiple" Height="100" class="form-control text_box"></asp:ListBox>
                    </div>
                    &nbsp &nbsp
                   
                     
                </div>
            </div>
            <br />

            <br />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" TargetControlID="btn_add_emp"
                CancelControlID="Button1" BackgroundCssClass="Background">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="Popup" Style="display: none">
                <iframe style="width: 1000px; height: 400px; background-color: #fff;" id="Iframe1" src="add_emp.aspx" runat="server"></iframe>
                <div class="row text-center">
                    <asp:Button ID="Button1" CssClass="btn btn-danger" OnClientClick="callfnc()" runat="server" Text="Close" />
                </div>
                <asp:Button ID="Button3" runat="server" CssClass="hidden" />

                <br />

            </asp:Panel>
            <br />
            <br />
            <div class="container" style="width: 70%">
                <asp:Panel runat="server" CssClass="grid-view" ID="pan">
                    <asp:GridView ID="gv_fullmonthot" runat="server" AutoGenerateColumns="false" ForeColor="#333333" class="table" GridLines="Both" OnRowDataBound="gv_fullmonthot_RowDataBound" DataKeyNames="emp_code">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="50" VerticalAlign="Middle" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chk_gv_header" runat="server" Text="SELECT EMPLOYEE" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_client" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UNIT_CODE" HeaderText="BRANCH NAME" SortExpression="UNIT_CODE" />
                            <asp:BoundField DataField="emp_code" HeaderText="EMPLOYEE CODE" SortExpression="emp_code" />
                            <asp:BoundField DataField="emp_name" HeaderText="EMPLOYEE NAME" SortExpression="emp_name" />
                            <asp:BoundField DataField="Employee_type" HeaderText="TYPE" SortExpression="Employee_type" />
                            <asp:TemplateField HeaderText="LEFT DATE">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="left_date_date" Width="100px" CssClass="form-control date-picker1" Text='<%# Eval("LEFT_DATE") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="LEFT REASON">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txt_emp_sample_left_reson" Width="100px" CssClass="form-control" Text='<%# Eval("LEFT_REASON") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="emp_code1" HeaderText="EMPLOYEE CODE1" SortExpression="emp_code1" />
                        </Columns>
                    </asp:GridView>

                </asp:Panel>
            </div>
            <div class="row text-center">
                <br />
                <asp:Button ID="btn_process" runat="server" class="btn btn-primary" OnClick="btn_process_Click" Text="Send Email" OnClientClick="return validateCheckBoxes()" />
                  <asp:Button ID="btn_blank" runat="server" class="btn btn-primary" OnClick="btn_process_blank_Click" Text="Send Blank Email" OnClientClick="return validateCheckBoxes()" />
                <asp:Button ID="btn_send_morningemail" runat="server" class="btn btn-large" OnClick="btn_morningemail_Click" Text="Send Morning Email" />
                <asp:Button ID="btn_add_emp" runat="server" Text="Add Employee" class="btn btn-primary" OnClick="btn_add_emp_Click" />
                <asp:Button ID="add_mail" runat="server" Text="ADD EMAIL" class="btn btn-primary" OnClick="add_mail_Click" />
                <asp:Button ID="BtnClose" runat="server" Text="CLOSE" class="btn btn-danger" OnClick="BtnClose_Click" />
                <asp:Button ID="btn_attendace_pdf" runat="server" class="btn btn-large" Width="15%" OnClick="btn_attendace_pdf_Click" Text="Attendance PDF Send Email" OnClientClick="return validateCheckBoxes()" />
            </div>
            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel2" TargetControlID="add_mail"
                CancelControlID="Button2" BackgroundCssClass="Background">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel2" runat="server" CssClass="Popup" Style="display: none">
                <iframe style="width: 1000px; height: 350px; background-color: #fff;" id="Iframe2" src="add_mail.aspx" runat="server"></iframe>
                <div class="row text-center">
                    <asp:Button ID="Button2" CssClass="btn btn-danger" OnClientClick="callfnc()" runat="server" Text="Close" />
                </div>
                <asp:Button ID="Button5" runat="server" CssClass="hidden" />

                <br />

            </asp:Panel>




            <div class="col-sm-2 col-xs-12">
                <br />
                <asp:Panel ID="pnl_branch" runat="server"><a data-toggle="modal" href="#attendance"><font color="red"><b><%=Message%></b></font>Branches Remaining</a></asp:Panel>
                <asp:LinkButton ID="lnk_conformationmail" runat="server" Text="Attendance Sheet All Ready Send For This Client" Style="color: red" OnClick="lnk_conformation" OnClientClick="return confirm('Are you sure ! Do you want to Resend Attendance?');"></asp:LinkButton>
            </div>


            <br />




          
            <asp:Panel Visible="false" ID="upload2" runat="server" CssClass="panel panel-default">

                <div class="panel-heading">
                    <div class="row">

                        <div style="text-align: center; color: black; font-size: small;"><b>Client Approved Attendance File Only</b></div>

                    </div>
                </div>
                <br />
                <br />

                <div class="container">

                    <div class="row" id="files_upload" runat="server">

                        <div class="col-sm-2 col-xs-12">
                           <b> Description :</b>
                                      <asp:TextBox ID="txt_document1" runat="server" class="form-control text_box" onkeypress="return  isNumber1(event)" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <span class="text_margin">File to Upload :</span>

                            <asp:FileUpload ID="document1_file" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                            <b style="color: #f00; text-align: center">Note :</b><span style="font-size: 8px; font-weight: bold;"> Only JPG, PNG,JPEG and PDF files will be uploaded.</span>
                        </div>
                        <div class="col-sm-3 col-xs-12 text-left" style="padding-top: 1%">
                            <asp:Button ID="upload" runat="server" class="btn btn-primary" Text=" Upload " OnClientClick="return save_validate();" OnClick="upload_Click" />
                        </div>


                        <br />

                    </div>
                </div>
                <br />
                <br />

                <div class="container">
                    <asp:Panel ID="Panel5" runat="server" CssClass="grid-view" Style="overflow-x: hidden;">
                        <asp:GridView ID="grd_company_files" class="table" runat="server" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            OnRowDataBound="grd_company_files_RowDataBound" AutoGenerateColumns="False" DataKeyNames="id" Width="100%" OnPreRender="grd_company_files_PreRender">
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
                                <asp:BoundField DataField="Id" HeaderText="ID" />

                                <%-- <asp:TemplateField HeaderText="DOWNLOAD FILE">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload" Text="DOWNLOAD" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <asp:BoundField DataField="month" HeaderText="MONTH"
                                    SortExpression="month" />
                                <asp:BoundField DataField="year" HeaderText="YEAR"
                                    SortExpression="year" />
                                <asp:BoundField DataField="uploaded_by" HeaderText="UPLOADED BY"
                                    SortExpression="uploaded_by" />
                                <asp:BoundField DataField="uploaded_date" HeaderText="UPLOADED DATE"
                                    SortExpression="uploaded_date" />
                                <asp:BoundField DataField="description" HeaderText="DESCRIPTION"
                                    SortExpression="description" />
                                <asp:TemplateField HeaderText="DOWNLOAD FILE">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" Text="DOWNLOAD" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>



                    </asp:Panel>
                </div>
                <div id="menu5">
                    <div class="row">
                        <div class="col-sm-2">
                           <b> Client Name :</b>
                           <asp:DropDownList ID="ddl_client_attach" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                          <b>  File Name and :</b>
                           <asp:DropDownList ID="ddl_filename_and" runat="server" CssClass="form-control" >
                               <asp:ListItem Value="0">Nothing</asp:ListItem>
                               <asp:ListItem Value="1">Branch</asp:ListItem>
                               <asp:ListItem Value="2">State</asp:ListItem>
                           </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <span class="text_margin">File to Upload :</span>

                            <asp:FileUpload ID="FileUpload1" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                            <b style="color: #f00; text-align: center">Note :</b><span style="font-size: 8px; font-weight: bold;"> Only XLS and PDF files will be uploaded.</span>
                        </div>
                        <div class="col-sm-3 col-xs-12 text-left" style="padding-top: 1%">
                           <asp:Button ID="btn_upload" runat="server" class="btn btn-primary" Text=" Upload" OnClick="btn_upload_Click" OnClientClick="return req_validation1();"/>
                        </div>


                

                        <br />
                    </div>
                    <br />
                    <div class="container">
                        <asp:Panel ID="Panel4" runat="server" CssClass="grid-view" Style="overflow-x: hidden;">
                            <asp:GridView ID="attachment_gv" class="table" runat="server" BackColor="White"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="attachment_gv_RowDataBound"
                                AutoGenerateColumns="False" DataKeyNames="id" Width="100%" OnPreRender="attachment_gv_PreRender">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <%--<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />--%>
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#424D7A" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <%--<RowStyle BackColor="#EFF3FB" />--%>
                                <EditRowStyle BackColor="#2461BF" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="ID" />
                                    <asp:TemplateField>

                                                                  <ItemStyle Width="20px" />
                                                                  <ItemTemplate>
                                                                      <asp:LinkButton ID="lnk_remove_attach" runat="server" CausesValidation="false" OnClick="lnk_remove_attach_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>

                                    
                                    <asp:BoundField DataField="client_name" HeaderText="Client Name"
                                        SortExpression="client_name" />
                                    <asp:BoundField DataField="File_name" HeaderText="File Name"
                                        SortExpression="File_name" />
                                    <asp:BoundField DataField="Type" HeaderText="Type"
                                        SortExpression="Type" />
                                    <asp:BoundField DataField="filename_and" HeaderText="FileName_And"
                                        SortExpression="filename_and" />
                                    <asp:TemplateField HeaderText="Download File">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:BoundField DataField="Uploaded_by" HeaderText="Uploaded By"
                                        SortExpression="Uploaded_by" />
                                    <asp:BoundField DataField="Uploaded_date" HeaderText="Uploaded Date"
                                        SortExpression="Uploaded_date" />
                                    <%--  <asp:TemplateField HeaderText="DOWNLOAD FILE">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" Text="DOWNLOAD" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

    <br />




</asp:Content>

